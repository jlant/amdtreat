using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMDTreat.Models
{
    public class SiloSystem : PropertyChangedBase
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

        private double _weight;
        public double Weight
        {
            get { return _weight; }
            set { ChangeAndNotify(ref _weight, value); }
        }

        private decimal _unitCost;
        public decimal UnitCost
        {
            get { return _unitCost; }
            set { ChangeAndNotify(ref _unitCost, value); }
        }

        private double _foundationArea;
        public double FoundationArea
        {
            get { return _foundationArea; }
            set { ChangeAndNotify(ref _foundationArea, value); }
        }

        private double _diameter;
        public double Diameter
        {
            get { return _diameter; }
            set { ChangeAndNotify(ref _diameter, value); }
        }

        private double _concreteThickness;
        public double ConcreteThickness
        {
            get { return _concreteThickness; }
            set { ChangeAndNotify(ref _concreteThickness, value); }
        }

        private double _concreteVolume;
        public double ConcreteVolume
        {
            get { return _concreteVolume; }
            set { ChangeAndNotify(ref _concreteVolume, value); }
        }
    }
}
