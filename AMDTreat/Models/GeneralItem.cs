using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMDTreat.Models
{
    public class GeneralItem : PropertyChangedBase
    {

        private string _name;
        public string Name
        {
            get { return _name; }
            set { ChangeAndNotify(ref _name, value); }
        }

        private double _value;
        public double Value
        {
            get { return _value; }
            set { ChangeAndNotify(ref _value, value); }
        }

    }
}
