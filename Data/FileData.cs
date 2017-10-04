//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.IO;
//using Eksamensopgave2017.Kernel;

//namespace Eksamensopgave2017 {
//  public static class FileData {
//    public static List<User> users = new List<User>();
//    public static List<Product> products = new List<Product>();
//    public static List<SeasonalProduct> sproducts = new List<SeasonalProduct>();

//    public static void Load() {
//      string[] user;
//      string[] product;

//      if (users.Count == 0)
//        return;
//      if (products.Count == 0)
//        return;

//      string[] userData;
//      string[] productData;

//      string[] userDataLines = File.ReadAllLines((Directory.GetCurrentDirectory() + "\\Data\\users.csv"), Encoding.GetEncoding("iso-8859-1"));
//      string[] productDataLines = File.ReadAllLines((Directory.GetCurrentDirectory() + "\\Data\\products.csv"), Encoding.GetEncoding("iso-8859-1"));

//      var userlist = new List<string>();
//      var productlist = new List<string>();

//      for (int i = 1; i < userDataLines.Length; i++) {
//        userlist.AddRange(new string[] { userDataLines[i] } );
//      }

//      for (int i = 1; i < productDataLines.Length; i++) {
//        productlist.AddRange(new string[] { productDataLines[i] });
//      }

//      users = new List<User>();
//      products = new List<Product>();


//      // Load from lines into Users and products;

//      for (int i = 1; i < userDataLines.Length; i++) {
//        User temp = new User();
//        userData = userDataLines[i].Split(';');
//        int.TryParse(userData[0], out temp._userID);
//        temp._firstname = userData[1].Trim('"');
//        temp._lastname = userData[2].Trim('"');
//        temp._username = userData[3].Trim('"');
//        temp._email = userData[4].Trim('"');
//        double.TryParse(userData[5], out temp._balance);
//        users.Add(temp);
//      }

//      for (int i = 1; i < productDataLines.Length; i++) {
//        Product productTemp = new Product();
//        SeasonalProduct sproductTemp = new SeasonalProduct();
//        int active;
//        productData = productDataLines[i].Split(';');

//        if (productData[i].Length == 4) {
//          int.TryParse(productData[0], out productTemp.ProductID);
//          productTemp.Name = productData[1].Trim('"');
//          double.TryParse(productData[2], out productTemp.Price);
//          int.TryParse(productData[3], out active);
//          if (active == 0)
//            productTemp.Active = false;
//          else
//            productTemp.Active = true;
//          products.Add(productTemp);
//        } else if (productData[i].Length == 5) {
//          int.TryParse(productData[0], out sproductTemp.ProductID);
//          sproductTemp.Name = productData[1].Trim('"');
//          double.TryParse(productData[2], out sproductTemp.Price);
//          int.TryParse(productData[3], out active);
//          if (active == 0)
//            sproductTemp.Active = false;
//          else
//            sproductTemp.Active = true;
//          sproductTemp._seasonEndDate = Convert.ToDateTime(productData[5]);
//          sproducts.Add(sproductTemp);
//        }
//      }
//    }



//  }
//}
