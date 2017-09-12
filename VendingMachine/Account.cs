using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachineExercise
{
    public interface IAccount
    {
        int AccountNumber { get;  }
        List<ICard> Cards { get; set; }
        double Balance { get; set; }
    }


    public class Account: IAccount
    {
        public int AccountNumber { get; private set; }
        public List<ICard> Cards { get; set; }
        public double Balance { get; set; }

        public Account(int accountNumber)
        {
            AccountNumber = accountNumber;
        }
      
    }
}
