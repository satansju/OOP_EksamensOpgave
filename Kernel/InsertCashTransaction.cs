using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave2017.Kernel {
  public class InsertCashTransaction : Transaction {
    public InsertCashTransaction(int id, User user, DateTime date, decimal amount) 
      :base(id,  user,  date, amount) { }

    public override string ToString() {
      return string.Format($"{Date.ToShortDateString()} {TransUser.Username} overførte {BoughtFor.ToString()} kr; T-ID: {TransactionID}");
    }

    public override void Execute() {
      if (Amount > 0) {
        TransUser.Balance += Amount;
        BoughtFor = Amount;
      } else
          throw new ArgumentNullException("Der skal overføres et positivt beløb");
    }
  }
}
