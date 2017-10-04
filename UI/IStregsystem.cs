using Eksamensopgave2017.Kernel;
using System;
using System.Collections.Generic;

namespace Eksamensopgave2017
{
  public interface IStregsystem
  {

    IEnumerable<Product> ActiveProducts { get; }
    BuyTransaction BuyProduct(User user, Product product);
    void AddCreditsToAccount(User user, decimal amount);
    Product GetProductByID(int productID);
    IEnumerable<Transaction> GetTransactions(User user, int count);
    User GetUser(User user);
    User GetUserByUsername(string username);
    //event UserBalanceNotification UserBalanceWarning;

  }
}
