using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachineExercise
{
   public class Global
    {
       public static readonly double minAccountBalance = 0.5;
       public static readonly double pricePerVend = 0.5;
       public static readonly string VendEmpty = "Vend Empty";
        public static readonly string NotEnoughBalance = "Not enough balance in the account";
        public static readonly string IncorrectPIN = "Incorrect PIN";
       public static readonly string UnregisteredCard="Card Not registered";
    }
}
