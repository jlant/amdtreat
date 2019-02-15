using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMDTreat.Models
{
    public class RecapMaterial : PropertyChangedBase
    {

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set { ChangeAndNotify(ref _isSelected, value); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { ChangeAndNotify(ref _name, value); }
        }

        private string _nameFixed;
        public string NameFixed
        {
            get { return _nameFixed; }
            set { ChangeAndNotify(ref _nameFixed, value); }
        }

        private double _lifeCycle;
        public double LifeCycle
        {
            get { return _lifeCycle; }
            set { ChangeAndNotify(ref _lifeCycle, value); }
        }

        private double _percentReplacement;
        public double PercentReplacement
        {
            get { return _percentReplacement; }
            set { ChangeAndNotify(ref _percentReplacement, value); }
        }

        private decimal _materialCostDefault;
        public decimal MaterialCostDefault
        {
            get { return _materialCostDefault; }
            set { ChangeAndNotify(ref _materialCostDefault, value); }
        }

        private bool _useCustomCost;
        public bool UseCustomCost
        {
            get { return _useCustomCost; }
            set { ChangeAndNotify(ref _useCustomCost, value); }
        }

        private decimal _materialCostCustom;
        public decimal MaterialCostCustom
        {
            get { return _materialCostCustom; }
            set { ChangeAndNotify(ref _materialCostCustom, value); }
        }

        private decimal _totalCost;
        public decimal TotalCost
        {
            get { return _totalCost; }
            set { ChangeAndNotify(ref _totalCost, value); }
        }

        public RecapMaterial()
        {
            IsSelected = true;
            UseCustomCost = true;
        }

    }
}
