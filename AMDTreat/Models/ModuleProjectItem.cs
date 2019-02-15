using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MahApps.Metro.IconPacks;
using AMDTreat.ViewModels;
using MahApps.Metro.Controls.Dialogs;

namespace AMDTreat.Models
{
    public class ModuleProjectItem
    {
        #region Fields and Properties

        public string Name { get; set; }
        public PropertyChangedBase ViewModel { get; set; }
        public System.Type ModuleType { get; set; }
        public PackIconModernKind Icon { get; set; }
        public string BackgroundColor { get; set; }

        private string _green = "#27ae60";
        private string _blue = "#2980b9";
        private string _orange = "#f39c12";

        #endregion

        #region Methods

        private void SetViewModel(System.Type moduleType)
        {
            switch (moduleType.Name)
            {
                case "PumpingViewModel":
                    ViewModel = new PumpingViewModel(DialogCoordinator.Instance);
                    break;
                default:
                    break;
            }

        }

        /// <summary>
        /// Set the background color for a tile based on its type.
        /// 
        /// Using colors from: https://flatuicolors.com/
        /// Nephritis (greenish) - "#27ae60"
        /// Belize Hole (bluish) - "#2980b9"
        /// Orange - "#f39c12"
        /// </summary>
        /// <param name="moduleType"></param>
        private void SetBackgroundColor(System.Type moduleType)
        {
            switch (moduleType.Name)
            {
                case "Pumping":
                    BackgroundColor = _green; 
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region Constructors

        public ModuleProjectItem(string name, System.Type moduleType)
        {
            Name = name;
            ModuleType = moduleType;
            SetViewModel(ModuleType);
            SetBackgroundColor(ModuleType);

        }


        #endregion

    }
}
