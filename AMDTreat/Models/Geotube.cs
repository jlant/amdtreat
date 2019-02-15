using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMDTreat.Models
{
    public class Geotube : PropertyChangedBase
    {

        private string _name;
        public string Name
        {
            get { return _name; }
            set { ChangeAndNotify(ref _name, value); }
        }

        private double _volume;
        public double Volume
        {
            get { return _volume; }
            set { ChangeAndNotify(ref _volume, value); }
        }

        private double _capacity;
        public double Capacity
        {
            get { return _capacity; }
            set { ChangeAndNotify(ref _capacity, value); }
        }

        private decimal _cost;
        public decimal Cost
        {
            get { return _cost; }
            set { ChangeAndNotify(ref _cost, value); }
        }

    }
}
