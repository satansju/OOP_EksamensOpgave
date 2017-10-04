using System;
using System.Collections.Generic;
using StringExtensions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


namespace Eksamensopgave2017.Kernel {
  public class User : IComparable {
    public static List<User> All = new List<User>();
    public int _userID;
    public string _firstname;
    public string _lastname;
    public string _username;
    public string _email;
    public decimal _balance;


    #region properties
  public int UserID {
      get { return _userID; }
      set { _userID = value; }
    }

    public string Firstname {
      get { return _firstname; }
      set { _firstname = value; }
    }

    public string Lastname {
      get { return _lastname; }
      set { _lastname = value; }
    }
    public string Username {
      get { return _username; }
      set { _username = value; }
    }

    public string Email {
      get { return _email; }
      set { _email = value; }
    }

    public decimal Balance {
      get { return _balance; }
      set { _balance = value; }
    }
#endregion

    public User() {
      this._userID = 1;
      this._firstname = "Henrik";
      this._lastname = "Larsen";
      this._username = "heinemeier23";
      this._email = "heinemeier23@msn.dk";
      this._balance = 0;
    }
    
    public User(string firstname, string lastname, string email) {
      
      if (StringCheckExtensions.CheckMail(email))
        this._email = email;
      else
        throw new ArgumentException("Invalid emailadresse");

      if (StringCheckExtensions.IsValidUsername(GenerateUsername(firstname, lastname)))
        this._username = GenerateUsername(firstname, lastname);

      this._userID = All.Count;
      this._firstname = firstname;
      this._lastname = lastname;
      this._balance = 0;

      if (firstname == null || lastname == null)
        throw new ArgumentNullException("Brugers fornavn og/eller efternavn kan ikke være 'null'");
    }

    public User(int id,  string firstname, string lastname, string username, string email, decimal balance) {
      if (StringCheckExtensions.CheckMail(email))
        this._email = email;
      else
       throw new ArgumentException("Unvalid email adress!");


      _userID = id;
      _firstname = firstname;
      _lastname = lastname;
      _username = username;
      _balance = balance;

      if (firstname == null || lastname == null)
        throw new ArgumentNullException("Brugers fornavn og/eller efternavn kan ikke være 'null'");

    }

    public int CompareTo(Object item) {
      if (item == null)
        return 1;
      User otherUser = item as User;
      if (otherUser != null)
        return this.UserID.CompareTo(otherUser.UserID);
      else
        return 0;
    }

    public override bool Equals(Object item) {
      if (item == null || !(item is User))
        return false;
      else 
        return Username == ((User)item).Username;
    }

    private string GenerateUsername(string firstname, string lastname) {
      string first = firstname.ToLower();
      return first[0] + lastname.ToLower();
    }


    public override string ToString() {
      return String.Format($"{UserID}: {Firstname} {Lastname} mail:{Email}, balance: {Balance}");
    }

    public override int GetHashCode() {
      //16 is an arbitrary number, but it is the same for every instance of User, which is the important part.
      return this.Username.GetHashCode() * 16;
    }

    public bool UserBalanceNotification(User user, decimal balance) {
      bool flag = false;
      if (user.Balance <= 50) 
        flag = true;

      return flag;
    }
  }
}
