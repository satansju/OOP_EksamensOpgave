using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave2017.Kernel {
  public class SeasonalProduct : Product {

    public static List<SeasonalProduct> All1 = new List<SeasonalProduct>();

    public DateTime _seasonStartDate = new DateTime();
    public DateTime _seasonEndDate = new DateTime();

    #region properties
    public DateTime SeasonStarDate {
      get { return (DateTime)_seasonStartDate; }
      set { _seasonStartDate = value; }
    }

    public DateTime SeasonEndDate {
      get { return (DateTime)_seasonEndDate; }
      set { _seasonEndDate = value; }
    }
    #endregion

    public SeasonalProduct(int productID, string name, decimal price, bool active, bool credit) 
      :base(productID, name, price, active, credit) {
    }


    public SeasonalProduct(int productID, string name, decimal price, bool active, bool credit, DateTime endDate) 
      :base(productID, name, price, active, credit) {
      _seasonEndDate = endDate;
    }

    public SeasonalProduct(int productID, string name, decimal price, bool active, bool credit, DateTime endDate, DateTime startDate) 
      :base(productID, name, price, active, credit) {
      _seasonEndDate = endDate;
      _seasonStartDate = startDate;
    }
  }
}
