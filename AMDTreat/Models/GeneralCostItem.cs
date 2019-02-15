using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMDTreat.Models
{
    public class GeneralCostItem : PropertyChangedBase
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { ChangeAndNotify(ref _name, value); }
        }

        private decimal _cost;
        public decimal Cost
        {
            get { return _cost; }
            set { ChangeAndNotify(ref _cost, value); }
        }
    }
}
