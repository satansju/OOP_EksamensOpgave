using Eksamensopgave2017.Kernel;
using System;
using System.Collections.Generic;

namespace Eksamensopgave2017
{
  class StregsystemCLI : IStregsystemUI {
    Stregsystem _stregsys;

    public Stregsystem StregSys {
      get { return _stregsys; }
    }

#region properties
    public StregsystemCLI(Stregsystem system) {
      _stregsys = system;
    }
    
    public StregsystemCLI(IStregsystem stregsystem)
    {
        throw new NotImplementedException();
    }
#endregion

    public void Start()
    {
      

    }

    public void PrintAll(dynamic xd) {
      foreach (var p in xd) {
        Console.WriteLine(p.ToString());
      }
    }

    public void DisplayError(string errormessage) {
      Console.WriteLine("=!`#%&@�$ARSAD@�$*��1");
      Console.WriteLine("Error:");
      Console.WriteLine(errormessage);
      Console.WriteLine("_____________________");
    }

    public void Display(string message) {
      Console.WriteLine("");
      Console.WriteLine(message);
      Console.WriteLine("_____________________");
    }

    public void AdminDisplay(string message) {
      Console.WriteLine("/////////////////////");
      Console.WriteLine("Admincommand:");
      Console.WriteLine(message);
      Console.WriteLine("_____________________");

    }

    public void DisplayReadyForCommands() {
      DisplayActiveProducts();
      Console.Write("Indtast besked herunder:\n\n>");
    }

    public void DisplayActiveProducts() {
      Console.WriteLine("---------Produktliste:----------");
      PrintAll(StregSys.ActiveProducts());
      Console.WriteLine("--------------------------------");
    }

    public void DisplayUserNotFound(string username) {
      string dunf= $"Det indtastede brugernavn [{username}] er ikke i brug.";
      DisplayError(dunf);
      DisplayHelpCommand();
      Console.ReadKey();
    }

    public void DisplayProductNotFound(string product) {
      string dpnf = $" Det valgte produkt [{product}] eksisterer ikke.";
      DisplayError(dpnf);
      DisplayHelpCommand();
      Console.ReadKey();
    }

    public void DisplayUserInfo(User user) {
      Display(user.ToString());
      Console.ReadKey();
    }

    public void DisplayTooManyArgumentsError(string command) {
      string dtmae= $"Der er for mange argumenter i din indtastning [{command}].";
      DisplayError(dtmae);
      DisplayHelpCommand();
      Console.ReadKey();
    }

    public void DisplayAdminCommandNotFoundMessage(string adminCommand) {
      string dacnfm = $"Den indtastede adminkommando [{adminCommand}] findes ikke.";
      DisplayError(dacnfm);
      DisplayHelpCommand();
      Console.ReadKey();
    }

    public void DisplayUserBuysProduct(BuyTransaction transaction) {
      Display(transaction.ToString());
      Console.ReadKey();
    }

    public void DisplayUserBuysProduct(bool var, BuyTransaction transaction) {
      Display(transaction.ToString());
      if (var) {
        Console.ReadKey();
      }   
    }

    public void DisplayAddCreditsToAccount(User user, decimal amount) {
      string dacta = $"Der er nu overf�rt [{amount}] til [{user.Username}s] saldo \n [{user.Username}] har nu [{user.Balance} coins]";
      AdminDisplay(dacta);
      Console.ReadKey();
    }

    public void Close() {
      AdminDisplay("Stregsystemt lukkes nu ned \n * Farvel *");
      Console.ReadKey();
      Environment.Exit(0);
    }

    public void DisplayActivation(int id, bool status) {
      AdminDisplay($"Produkt [{id}] er nu " + (status ? "aktiveret" : "deaktiveret" ));
      Console.ReadKey();
    }

    public void DisplayCreditActivation(int id, bool status) {
      AdminDisplay($"Produkt [{id}]" + (status ? "kan nu" : "kan ikke l�ngere") + " blive k�bt p� kredit");
      Console.ReadKey();
    }

    public void DisplayInsufficientCash(User user, Product product) {
      string dic = $"Der er desv�rre ikke penge nok p� [{user}s] saldo til at k�be [{product}]";
      DisplayError(dic);
      DisplayHelpCommand();
      Console.ReadKey();
    }

    public void DisplayGeneralError(string errorString) {
      DisplayError(errorString);
      DisplayHelpCommand();
      Console.ReadKey();
    }

    public void DisplayHelpCommand() {
      Console.WriteLine("Tryk [?] for hj�lp til kommandosyntaks");
    }

    public void DisplayHelp() {
      string helpcommandos = "De mulige kommandoer skrives som f�lger: (uden [])\n\n" +
        "Tryk p�: [?] for at f� vist kommandosyntaks \n" + 
        "Indtast: [brugernavn] for at f� vist en bruger \n" +
        "Indtast: [brugernavn] [produkt id] for at k�be et produkt \n" +
        "Indtast: [brugernavn] [antal] [produkt id] for at k�be produkt et produkt [antal] gange \n\n" +
        "For at f� adgang til administraterkommandoer, kontakt en administrater";
      Display(helpcommandos);
      Console.ReadKey();
    }
  }
}