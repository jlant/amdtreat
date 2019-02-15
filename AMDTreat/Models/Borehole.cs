using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMDTreat.Models
{
    public class Borehole : PropertyChangedBase
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { ChangeAndNotify(ref _name, value); }
        }

        private double _diameter;
        public double Diameter
        {
            get { return _diameter; }
            set { ChangeAndNotify(ref _diameter, value); }
        }

        private decimal _cost;
        public decimal Cost
        {
            get { return _cost; }
            set { ChangeAndNotify(ref _cost, value); }
        }
    }
}
