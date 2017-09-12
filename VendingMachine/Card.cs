using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachineExercise
{
    public interface ICard
    {
        IAccount Account { get; set; }
        long CardNumber { get; set; }
    }


    public class Card: ICard
    {
        public IAccount Account { get; set; }
        public long CardNumber { get; set; }

    }
}
