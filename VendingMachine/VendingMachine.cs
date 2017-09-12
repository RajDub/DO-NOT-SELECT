using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachineExercise
{
   public class VendingMachine
    {
        readonly double minAccountBalance = Global.minAccountBalance;
        readonly double pricePerVend = Global.pricePerVend;

        public int Items { get; private set; }
        public Dictionary<long,int> CardDetails { get; set; }
        private readonly object syncroot = new object();

        public VendingMachine(int items)
        {
            Items = items;
        }
        public bool Vend(ICard card, int PIN)
        {
            int passwordvalue;
            if(CardDetails !=null && CardDetails.TryGetValue(card.CardNumber,out passwordvalue))
            {
                if (passwordvalue == PIN)
                {
                    if (Items == 0)
                    {
                        throw new Exception(Global.VendEmpty);
                    }

                    lock (syncroot)
                    {
                        if (card.Account.Balance >= minAccountBalance)
                        {
                            Items--;
                            card.Account.Balance -= pricePerVend;

                        }
                        else
                        {
                            throw new Exception(Global.NotEnoughBalance);

                        }
                    }
                }
                else
                {
                    throw new Exception(Global.IncorrectPIN);
                }
                return true;
            }
            else
            {
                throw new Exception(Global.UnregisteredCard);
            }
           

        }
    }
}
