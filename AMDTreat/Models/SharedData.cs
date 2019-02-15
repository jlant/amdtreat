using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMDTreat.Models
{
    public class SharedData 
    {
        public int ModuleId { get; set; }
        public string ModuleTreatmentType { get; set; }
        public Dictionary<string, object> Data { get; set; }
        public static Action<object, NotifyCollectionChangedEventArgs> CollectionChanged { get; internal set; }

        public SharedData()
        {
        }

        public SharedData(int id, string moduleTreatmentType, Dictionary<string, object> data)
        {
            ModuleId = id;
            ModuleTreatmentType = moduleTreatmentType;
            Data = data;
        }
    }
}
