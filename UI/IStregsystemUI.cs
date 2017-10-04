using Eksamensopgave2017.Kernel;

namespace Eksamensopgave2017
{
  public interface IStregsystemUI
  {
    void DisplayUserNotFound(string username);
    void DisplayProductNotFound(string product);
    void DisplayUserInfo(User user);
    void DisplayTooManyArgumentsError(string command);
    void DisplayAdminCommandNotFoundMessage(string adminCommand);
    void DisplayUserBuysProduct(BuyTransaction transaction);
    void DisplayUserBuysProduct(bool var, BuyTransaction transaction);
    void Close();
    void DisplayInsufficientCash(User user, Product product);
    void DisplayGeneralError(string errorString);
    void DisplayAddCreditsToAccount(User user, decimal amount);
    //event StregsystemEvent CommandEntered;

    void Start();
    void DisplayReadyForCommands();
  }
}