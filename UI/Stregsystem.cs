using Eksamensopgave2017.Kernel;
using StringExtensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Eksamensopgave2017
{
  class Stregsystem : IStregsystem {
    public List<Transaction> AllTransactions = new List<Transaction>();
    public List<Product> AllProducts;
    public List<User> AllUsers;
    //public List<SeasonalProduct> AllSProducts = new List<SeasonalProduct>();

    //public event UserBalanceNotification UserBalanceWarning {
    //  get { }
      

    //}

    //public static void Func() {

    //}



    //void IStregsystem.UserBalanceWarning() {

    //}

    IEnumerable<Product> IStregsystem.ActiveProducts => throw new NotImplementedException();

    public Stregsystem() {
      AllTransactions = Transaction.All;
      AllUsers = User.All;
      AllProducts = Product.All;
      //AllSProducts = SeasonalProduct.All;
    }

     public BuyTransaction BuyProduct(User user, Product product) {
       BuyTransaction transaction = new BuyTransaction(CreateTransID(), user, DateTime.Now, product);
       AllTransactions.Add(transaction);
       ExecuteTransaction(transaction);
       return transaction;
     }

    public BuyTransaction BuyProduct(User user, Product product, int num) {
      BuyTransaction transaction = new BuyTransaction(CreateTransID(), user, DateTime.Now, product, num);
      AllTransactions.Add(transaction);
      ExecuteTransaction(transaction);
      return transaction;
    }


    public void AddCreditsToAccount(User user, decimal amount) {
        InsertCashTransaction transaction = new InsertCashTransaction(CreateTransID(), user, DateTime.Now, amount);
        AllTransactions.Add(transaction);
        ExecuteTransaction(transaction);
     }

    private int CreateTransID() {
      return AllTransactions.Count;
    }

    public void ExecuteTransaction(Transaction transaction) {
      transaction.Execute();
      using (StreamWriter log = File.AppendText(@"C:\Users\sataa\Desktop\Skabelon til Eksamensopgaven 2017\Eksamensopgave2017\Eksamensopgave2017\Data\LogFile.txt"))
      transaction.LogTransaction(transaction.ToString(), log);
    }

    public void LoadProdutcs() {
      string[] productLines = File.ReadAllLines(@"C: \Users\sataa\Dropbox\Skabelon til Eksamensopgaven 2017\products.csv");
      foreach (string item in productLines) {
        string[] split = item.Split(';');
        if (split[0] == "id")
          continue;

        //if (split.Length == 4) {
          //If product id is larger than zero
          if (int.Parse(split[0]) > 0) {
            AllProducts.Add(new Product(int.Parse(split[0]), RemoveHtml(split[1]), decimal.Parse(split[2]) / 100, IntToBool(int.Parse(split[3])), false));
          }
        //} else if (split.Length == 5) {
        //  //If product id is larger than zero
        //  if (int.Parse(split[0]) > 0) {
        //    AllSProducts.Add(new SeasonalProduct(int.Parse(split[0]), split[1].Trim('"'), decimal.Parse(split[2]) / 100, IntToBool(int.Parse(split[3])), false, ToDateTime.ParseExact(split[5], "d/M/yyyy", none)));
        //  }
        //}
      }
    }

    /// <summary>
    /// LoadUsers is a function that loads users from a path.
    /// It does this line after line and splits each line 
    /// </summary>
    public void LoadUsers() {
      //Endcoding is found on stack overflow. http://stackoverflow.com/questions/8089357/how-to-read-special-character-like-%C3%A9-%C3%A2-and-others-in-c-sharp
      string[] userLines = File.ReadAllLines(@"C: \Users\sataa\Dropbox\Skabelon til Eksamensopgaven 2017\users.csv");
        //(Directory.GetCurrentDirectory() + "\\Data\\users.csv"), Encoding.GetEncoding("iso-8859-1"));
      foreach (string item in userLines) {
        string[] split = item.Split(';');
        if (split[0] == "id")
          continue;

          //If product id is larger than zero
          if (int.Parse(split[0]) > 0) {
          AllUsers.Add(new User(int.Parse(split[0]), split[1].Trim('"'), split[2].Trim('"'), split[3].Trim('"'), split[4].Trim('"'), decimal.Parse(split[5])));
        }
      }
    }

    /// <summary>
    /// RemoveHtml removes html tags buy hardreplacing them with "". 
    /// HardRemoveHtml is an alternative to Regex ".Replace" function.
    /// </summary>
    string RemoveHtml(string s) {
      Regex regex = new Regex(@"([<]\/?[\w]{0,20}\/?[>])");
      return regex.Replace(s, "");
    }

    string HardRemoveHtml(string s) {
      return s.Replace("<h1>", "").Replace("</h1>", "").Replace("<h2>", "").Replace("</h2>", "").Replace("<h3", "").Replace("</h3>", "")
        .Replace("<b>", "").Replace("</b>", "").Replace("<blink>", "").Replace("</blink>", "").Replace("<h3/>","");
    }

    /// <summary>
    /// IntToBool converts an integer to a boolean
    /// </summary>
    public bool IntToBool(int num) {
        if (num > 0)
          return true;
        else
          return false;
    }

    public Product GetProductByID(int id) {
      foreach (Product p in AllProducts) {
        if (p.ProductID == id)
          return p;
      }
      throw new ArgumentException("Produktet findes ikke");
    }

    public bool CheckIfUserExists(string username) {
      if (StringCheckExtensions.IsValidUsername(username)) {
        foreach (User u in AllUsers) {
          if (u.Username == username)
            return true;
        }
      }
      return false;
    }

    public List<User> GetUsersWithBalanceLessThanFiddy() {
      List<User> OnSU = new List<User>();
      foreach (User u in AllUsers) {
        if (u.Balance <= 50)
          OnSU.Add(u);

      }
      return OnSU;
    }

    public User GetUserByUsername(string username) {
      if (StringCheckExtensions.IsValidUsername(username)) {
        foreach (User u in AllUsers) {
          if (u.Username == username)
            return u;
        }
        throw new ArgumentException($"Der eksisterer ikke en bruger med brugernavnet {username}");
      } else
         throw new ArgumentException("Brugernavne er skrevet med små bogstaver og må kun indeholde bogstaverne fra a-z og tal");
    }

    public User GetUser(User user) {
      if (StringCheckExtensions.IsValidUsername(user.Username)) {
        foreach (User u in AllUsers) {
          if (u.Username == user.Username)
            return u;
        }
        throw new ArgumentException($"Der eksisterer ikke en bruger med brugernavnet {user.Username}");
      } else
        throw new ArgumentException("Brugernavne er skrevet med små bogstaver og må kun indeholde bogstaverne fra a-z og tal");
    }

    public IEnumerable<Transaction> GetTransactions(User user, int count) {
      List<Transaction> ta = new List<Transaction>();
      foreach (Transaction t in AllTransactions) {
        if (t.TransUser == user)
          ta.Add(t);
      }
      if (ta.Count >= count)
        return ta.OrderBy(x => x.Date).Take(count).ToList();
      else
        throw new ArgumentNullException($"bruger: {user.Username} har ikke foretaget {count} ");
    }

    public IEnumerable<Transaction> GetTransactions(string username, int count) {
      List<Transaction> ta = new List<Transaction>();
      foreach (Transaction t in AllTransactions) {
        if (t.TransUser.Username == username)
          ta.Add(t);
      }
      if (ta.Count >= count)
        return ta.OrderBy(x => x.Date).Take(count).ToList();
      else
        throw new ArgumentNullException($"bruger: {username} har ikke foretaget {count} ");
    }

    public IEnumerable<Transaction> GetTransactions(User u, DateTime start, DateTime end) {
      List<Transaction> ta = new List<Transaction>();
      foreach (Transaction t in AllTransactions) {
        if (t.TransUser.Username == u.Username && (t.Date > start && t.Date < end))
          ta.Add(t);
      }
      if (ta.Count != 0)
        return ta.OrderBy(x => x.Date).ToList();
      else
        throw new ArgumentNullException($"bruger: {u.Username} har ikke foretaget nogen transaktioner i perioden {start} - {end}");
    }

    public List<Product> ActiveProducts() {
      List<Product> activeproducts = new List<Product>();
      foreach (Product p in AllProducts) {
        if (p.Active )
          activeproducts.Add(p);
      }
      if (activeproducts.Count != 0)
        return activeproducts;
      else throw new NullReferenceException("Ingen produkter er aktive");
    }

    public void ChangeProductActive(int id, bool val) {
      GetProductByID(id).Active = val;
    }

    public void ChangeProductCredit(int id, bool val) {
      GetProductByID(id).CanBeBoughtOnCredit = val;
    }
  }
}