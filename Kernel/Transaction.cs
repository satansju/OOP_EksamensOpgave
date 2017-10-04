using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave2017.Kernel {
  public abstract class Transaction : IComparable {
    public static List<Transaction> All = new List<Transaction>();
    int _transactionID;
    User _transUser;
    DateTime _date;
    decimal _amount;
    private decimal _boughtFor;

    #region Properties
    public decimal BoughtFor {
      get { return _boughtFor; }
      set { _boughtFor = value; }
    }

    public decimal Amount {
      get { return _amount; }
      set { _amount = value; }
    }

    public DateTime Date {
      get { return _date; }
      set { _date = value; }
    }

    internal User TransUser {
      get { return _transUser; }
      set { _transUser = value; }
    }

    public int TransactionID {
      get { return _transactionID; }
      set { _transactionID = value; }
    }
    #endregion

    public Transaction(int id, User user, DateTime date, decimal amount) {
      _transactionID = id;
      _transUser = user;
      _date = date;
      _amount = amount;
    }

    public abstract void Execute();

    //Consider Interface for objects able to log
    public void LogTransaction(string logEntry, System.IO.StreamWriter w) {
      w.WriteLine("$*********************$");
      w.WriteLine("Transaction:\n");
      w.WriteLine(Date.ToShortDateString() + " " + Date.ToShortTimeString());
      w.WriteLine(logEntry);
    }

    public int CompareTo(object obj) {
      if (obj == null)
        return 1;

      Transaction otherTrans = obj as Transaction;
      if (otherTrans != null)
        return TransactionID.CompareTo(otherTrans.TransactionID);
      else
        throw new ArgumentException("Objektet er ikke af typen 'Transaktion'");
    }
  }
}
