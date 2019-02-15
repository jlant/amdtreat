using AMDTreat.Models;
using AMDTreat.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMDTreat.ViewModels
{
    public class TestViewModel : PropertyChangedBase
    {

        //public string ModuleName { get; set; }

        public TestViewModel()
        {
            ModuleName = "My Test Module";
        }
    }
}
