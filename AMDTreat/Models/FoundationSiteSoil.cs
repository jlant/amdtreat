using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMDTreat.Models
{
    public class FoundationSiteSoil : PropertyChangedBase
    {

        private string _name;
        public string Name
        {
            get { return _name; }
            set { ChangeAndNotify(ref _name, value); }
        }

        private double _quantity;
        public double Quantity
        {
            get { return _quantity; }
            set { ChangeAndNotify(ref _quantity, value); }
        }

        private string _rating;
        public string Rating
        {
            get { return _rating; }
            set { ChangeAndNotify(ref _rating, value); }
        }

        private double _multiplier;
        public double Multiplier
        {
            get { return _multiplier; }
            set { ChangeAndNotify(ref _multiplier, value); }
        }

    }
}
