using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMDTreat.Models
{
    public class Polymer : PropertyChangedBase
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { ChangeAndNotify(ref _name, value); }
        }

        private double _solutionStrength;
        public double SolutionStrength
        {
            get { return _solutionStrength; }
            set { ChangeAndNotify(ref _solutionStrength, value); }
        }

        private double _percentActive;
        public double PercentActive
        {
            get { return _percentActive; }
            set { ChangeAndNotify(ref _percentActive, value); }
        }

        private decimal _cost;
        public decimal Cost
        {
            get { return _cost; }
            set { ChangeAndNotify(ref _cost, value); }
        }
    }
}
