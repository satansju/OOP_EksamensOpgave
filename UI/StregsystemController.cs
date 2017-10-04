using System;
using System.Collections.Generic;
using StringExtensions;
using Eksamensopgave2017.Kernel;

namespace Eksamensopgave2017
{
  class StregsystemController {
    IStregsystemUI _userinterface;
    Stregsystem _system;
    Dictionary<string, Action<dynamic, dynamic>> _admincommands;

#region properties
    public IStregsystemUI UI {
      get { return _userinterface; }
    }

    public Stregsystem SYS {
      get { return _system; }
    }
#endregion

    public StregsystemController(IStregsystemUI ui, Stregsystem stregsystem)
    {
      _userinterface = ui;
      _system = stregsystem;
      UpdateSystem();
      _admincommands = new Dictionary<string, Action<dynamic, dynamic>>();
      AdminCommands(ref _admincommands);

      Start();
    }

    void AdminCommands(ref Dictionary<string, Action<dynamic, dynamic>> _admincommands) {
      _admincommands.Add(":q", (x, y) => { UI.Close(); });
      _admincommands.Add(":quit", (x, y) => { UI.Close(); });
      _admincommands.Add(":activate", (x, y) => { SYS.ChangeProductActive(int.Parse(x), true); ((StregsystemCLI)UI).DisplayActivation(int.Parse(x), true); });
      _admincommands.Add(":deactivate", (x, y) => { SYS.ChangeProductActive(int.Parse(x), false); ((StregsystemCLI)UI).DisplayActivation(int.Parse(x), false); });
      _admincommands.Add(":crediton", (x, y) => { SYS.ChangeProductCredit(int.Parse(x), true); ((StregsystemCLI)UI).DisplayCreditActivation(int.Parse(x), true); });
      _admincommands.Add(":creditoff", (x, y) => { SYS.ChangeProductCredit(int.Parse(x), false); ((StregsystemCLI)UI).DisplayCreditActivation(int.Parse(x), false); });
      _admincommands.Add(":addcredits", (x, y) => { SYS.AddCreditsToAccount(SYS.GetUserByUsername(x), decimal.Parse(y)); UI.DisplayAddCreditsToAccount(SYS.GetUserByUsername(x), decimal.Parse(y)); });
    }

    void Start() {
      UI.DisplayReadyForCommands();
      ParseCommand(Console.ReadLine());
      Console.Clear();
      Start();
    }

    void UpdateSystem() {
      try {
        SYS.LoadProdutcs();
      } catch (ArgumentOutOfRangeException e) {
        UI.DisplayGeneralError(e.Message);
      } catch (ArgumentNullException e) {
        UI.DisplayGeneralError(e.Message);
      }

      try {
        SYS.LoadUsers();
      } catch (ArgumentOutOfRangeException e) {
        UI.DisplayGeneralError(e.Message);
      } catch (ArgumentNullException e) {
        UI.DisplayGeneralError(e.Message);
      }
    }

    void ParseCommand(string command) {
      User user = null;
      Product product = null;
      int num = 1;
      string[] split = command.Split(' ');
      if (StringCheckExtensions.CheckEmpty(command)) {
        UI.DisplayGeneralError("Der blev ikke indtastet nogen kommandoer");
        return;
      } else if (split.Length > 3) {
        UI.DisplayTooManyArgumentsError(command);
        return;
      } else if (command == "?") {
        ((StregsystemCLI)UI).DisplayHelp();
        return;
      }

      if (StringCheckExtensions.AdminCommand(command)) {
        switch (split.Length) {
          case 1:
            UI.Close();
            _admincommands[split[0]].Invoke(split[0], "");
            Console.ReadKey();
            break;
          case 2:
            break;

          case 3:
            _admincommands[split[0]].Invoke(split[1], split[2]);
            Console.ReadKey();
            break;

          default:
            UI.DisplayTooManyArgumentsError(command);
            break;


        }
      } else if (StringCheckExtensions.UserCommand(split)) {
        switch (split.Length) {
          case 1:
            try {
              UI.DisplayUserInfo(SYS.GetUserByUsername(split[0]));
            } catch (UserNotFoundException e) {
              UI.DisplayUserNotFound(e.Message);
            }
            break;
          case 2:
            try {
              UI.DisplayUserInfo(user = SYS.GetUserByUsername(split[0]));
              product = SYS.GetProductByID(int.Parse(split[1]));
              UserBuy(user, product, num);
            } catch (UserNotFoundException e) {
              UI.DisplayUserNotFound(e.Message);
            } catch (ProductNotFoundException e) {
              UI.DisplayUserNotFound(e.Message);
            }

            break;

          case 3:
            try {
              UI.DisplayUserInfo(user = SYS.GetUserByUsername(split[0]));
              product = SYS.GetProductByID(int.Parse(split[2]));
              num = int.Parse(split[1]);
              UserBuy(user, product, num);
            } catch (UserNotFoundException e) {
              UI.DisplayUserNotFound(e.Message);
            } catch (ProductNotFoundException e) {
              UI.DisplayProductNotFound(e.Message);
            } catch (FormatException e) {
              UI.DisplayGeneralError(e.Message);
            }

            break;

          default:
            UI.DisplayTooManyArgumentsError(command);
            Console.ReadKey();
            break;
        }
      }
    }

    bool CanAfford(int num, User user, Product product) {
      if (product.CanBeBoughtOnCredit) {
        return true;
      } else if (user.Balance >= (num * product.Price) && !product.CanBeBoughtOnCredit)  {
        return true;
      } else {
        UI.DisplayInsufficientCash(user, product);
        return false;
      }
    }

    void UserBuy(User user, Product product, int num) {
      if (CanAfford(num, user, product)) {
        if (num == 1) {
          UI.DisplayUserBuysProduct(SYS.BuyProduct(user, product));
        } else if (num > 1) {
          UI.DisplayUserBuysProduct(SYS.BuyProduct(user, product, num));
        }
      }
    }
      
  }
}
