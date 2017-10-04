using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave2017.Kernel {
  public class Product {
    public static List<Product> All = new List<Product>();
    public int ProductID;
    public string Name;
    public decimal Price;
    public bool Active;
    public bool CanBeBoughtOnCredit;

#region properties
    

#endregion


    public Product() {
    }

    public Product(int productID, string name, decimal price, bool active, bool credit) {
      this.ProductID = productID;
      this.Name = name;
      this.Price = price;
      this.Active = active;
      this.CanBeBoughtOnCredit = credit;
    }

    public override string ToString() {
      return string.Format($"{ProductID.ToString()} {Name} koster {Price.ToString()} kr");
    }
  }
}
