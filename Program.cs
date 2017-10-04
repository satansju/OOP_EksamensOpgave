//Studienummer_fuldt_navn <- her skriver du dit studienummer og fulde navn som kommentar. Det skal være det første i filen!
//Eksempel:
//20121234_Peter_Aage_Nielsen

using Eksamensopgave2017.Kernel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave2017
{
  class Program
  {
    static void Main(string[] args)
    {
      //Console.WriteLine("** All Users **");

      //stregsystem.LoadUsers();
      //stregsystem.LoadProdutcs();
      //Console.WriteLine(stregsystem.AllUsers.ElementAt(0));

      ////foreach (User item in User.All) {
      ////  Console.WriteLine(item.ToString());
      ////}

      //Console.WriteLine("** All Products **");



      //StregsystemCLI cli = new StregsystemCLI(stregsystem);
      //cli.DisplayHelp();
      //cli.PrintAll(stregsystem.ActiveProducts());

      //InsertCashTransaction ist = new InsertCashTransaction(1, stregsystem.AllUsers.ElementAt(0), DateTime.Now, 100);
      //ist.Execute(); //(int id, User user, DateTime date, double amount)
      //BuyTransaction bt = new BuyTransaction(1, stregsystem.AllUsers.ElementAt(0), DateTime.Now, stregsystem.AllProducts.ElementAt(10)); //(int id, User user, DateTime date, Product item)
      //bt.ToString();
      //bt.Execute();
      //Console.WriteLine(stregsystem.AllUsers.ElementAt(0));

      //Console.WriteLine(stregsystem.GetProductByID(10));
      //Console.WriteLine(stregsystem.GetUserByUsername("gdegn"));
      //foreach (Product item in Product.All) {
      //  Console.WriteLine(item.ToString());
      //}

      Stregsystem stregsystem = new Stregsystem();
      IStregsystemUI ui = new StregsystemCLI(stregsystem);
      StregsystemController sc = new StregsystemController(ui, stregsystem);

      Console.ReadKey();

    }
  }
}
