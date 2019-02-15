using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMDTreat.Models
{
    public interface IWindowItem
    {
        PropertyChangedBase ViewModel { get; set; }
    }
}
