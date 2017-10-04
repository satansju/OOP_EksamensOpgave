using System.Linq;
using System.Text.RegularExpressions;

namespace StringExtensions {
  public static class StringCheckExtensions {

    public static bool UserCommand(this string[] s) {
      if (s.Length == 3) {
        if (IntToBool(int.Parse(s[2])) && IntToBool(int.Parse(s[1])))
          return true;
      } else if (s.Length == 2) {
        if (IntToBool(int.Parse(s[1])))
          return true;
      } else if (s.Length == 1) { 
          return true;
        }
        return false;
    }

    internal static bool CheckEmpty(this string str) {
      if (str == "" || str == " " || str == "  " || str == null) {
        return true;
      }

      return false;
    }

    public static bool IntToBool(this int number) {
      if (number > 0)
        return true;
      return false;
    }

    public static bool AdminCommand(this string s) {
      return s[0] == ':';
    }

    public static bool IsValidUsername(string username) {
      if (username == "" || username == null)
        return false;
      return true;
    }

    public static bool CheckMail(string email) {
      bool local = false, domain = false;
      string[] split = email.Split('@');
      if (split.Length != 2)
        return false;

      //local check
      if (Regex.IsMatch(split[0], @"^[\w]+$")) {
        local = true;
      }

      //domain check
      if (Regex.IsMatch(split[0], @"^[\w-_.]{1,64}$") && split[1].Contains('.') && !split[1].StartsWith("-") && !split[1].StartsWith(".")
        && !split[1].EndsWith("-") && !split[1].EndsWith(".")) {
        domain = true;
      }

      return (local && domain);
    }
  }
}
