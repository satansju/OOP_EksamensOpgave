using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave2017.Kernel {
  public class BuyTransaction : Transaction {
    /// <summary>
    /// Dette er en købstransaktion. Der er to constructers 
    /// </summary>
    Product _product;
    int _number;

#region Properties
    public Product TransProduct {
      get { return _product; }
      set { _product = value; }
    }

    public int Number {
      get { return _number; }
      set { _number = value; }
    }
#endregion

    public BuyTransaction(int id, User user, DateTime date, Product item)
      :base(id, user, date, item.Price){
      _product = item;
      _number = 1;
      }

    public BuyTransaction(int id, User user, DateTime date, Product item, int number)
     : base(id, user, date, item.Price) {
      _product = item;
      _number = number;
    }

    public override string ToString() {
      return string.Format($"TA-id: {TransactionID} { Date.ToShortDateString()} købte {TransUser.Username} " 
        + ((Number == 1)? $"{TransProduct.Name} for {BoughtFor.ToString()} kr" 
        : $"{ Number} x {TransProduct.Name} for {BoughtFor.ToString()} kr "));
    }

    /// <summary>
    /// I Execute metoden trækkes det købte produkts pris fra brugerens saldo.
    /// Dette kan enten ske, hvis produktet kan købes på kredit,
    /// eller hvis brugeren har penge nok på kontoen.
    /// </summary>
    public override void Execute() {
      if (TransProduct.Active) {
        if (TransProduct.CanBeBoughtOnCredit) {
            TransUser.Balance -= TransProduct.Price * Number;
            BoughtFor = TransProduct.Price;
          } else if (TransUser.Balance >= TransProduct.Price * Number) {
            TransUser.Balance -= TransProduct.Price * Number;
            BoughtFor = TransProduct.Price * Number;
        } else 
          throw new ArgumentException("Der er ikke nok penge på saldoen til at købe dette produkt");
      } else
        throw new ArgumentException("Produktet er ikke i salg længere");
    }
  }
}
