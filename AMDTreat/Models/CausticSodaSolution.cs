using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMDTreat.Models
{
    public class CausticSodaSolution : PropertyChangedBase
    {

        private string _name;
        public string Name
        {
            get { return _name; }
            set { ChangeAndNotify(ref _name, value); }
        }

        private double _percentage;
        public double Percentage
        {
            get { return _percentage; }
            set { ChangeAndNotify(ref _percentage, value); }
        }

        private double _weight;
        public double Weight
        {
            get { return _weight; }
            set { ChangeAndNotify(ref _weight, value); }
        }
    }
}
