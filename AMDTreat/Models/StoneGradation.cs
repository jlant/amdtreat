using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMDTreat.Models
{
    public class StoneGradation : PropertyChangedBase
    {

        private string _name;
        public string Name
        {
            get { return _name; }
            set { ChangeAndNotify(ref _name, value); }
        }

        private double _particleSurfaceArea;
        public double ParticleSurfaceArea
        {
            get { return _particleSurfaceArea; }
            set { ChangeAndNotify(ref _particleSurfaceArea, value); }
        }

        private double _particleVolume;
        public double ParticleVolume
        {
            get { return _particleVolume; }
            set { ChangeAndNotify(ref _particleVolume, value); }
        }

    }
}
