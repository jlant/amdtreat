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
    public class ModuleItem
    {
        #region Fields and Properties

        public string Name { get; set; }
        public PropertyChangedBase ViewModel { get; set; }
        public System.Type ModuleType { get; set; }
        public PackIconModernKind Icon { get; set; }
        public string BackgroundColor { get; set; }
        public string Opacity { get; set; } // only used for "PlaceholderViewModels"

        private string _green = "#27ae60";
        private string _blue = "#2980b9";
        private string _orange = "#f39c12";

        #endregion

        #region Methods

        private void SetViewModel(System.Type moduleType)
        {
            switch (moduleType.Name)
            {
                case "VerticalFlowPondViewModel":
                    ViewModel = new VerticalFlowPondViewModel(DialogCoordinator.Instance);
                    break;
                case "LimestoneBedViewModel":
                    ViewModel = new LimestoneBedViewModel(DialogCoordinator.Instance);
                    break;
                case "ManganeseRemovalBedViewModel":
                    ViewModel = new ManganeseRemovalBedViewModel(DialogCoordinator.Instance);
                    break;
                case "WetlandViewModel":
                    ViewModel = new WetlandViewModel(DialogCoordinator.Instance);
                    break;
                case "BioReactorViewModel":
                    ViewModel = new BioReactorViewModel(DialogCoordinator.Instance);
                    break;
                case "AnoxicLimestoneDrainViewModel":
                    ViewModel = new AnoxicLimestoneDrainViewModel(DialogCoordinator.Instance);
                    break;
                case "CausticSodaViewModel":
                    ViewModel = new CausticSodaViewModel(DialogCoordinator.Instance);
                    break;
                case "LimeSlurryViewModel":
                    ViewModel = new LimeSlurryViewModel(DialogCoordinator.Instance);
                    break;
                case "DryLimeViewModel":
                    ViewModel = new DryLimeViewModel(DialogCoordinator.Instance);
                    break;
                case "PumpingViewModel":
                    ViewModel = new PumpingViewModel(DialogCoordinator.Instance);
                    break;
                case "ConveyanceDitchViewModel":
                    ViewModel = new ConveyanceDitchViewModel(DialogCoordinator.Instance);
                    break;
                case "SamplingViewModel":
                    ViewModel = new SamplingViewModel(DialogCoordinator.Instance);
                    break;
                case "PondsViewModel":
                    ViewModel = new PondsViewModel(DialogCoordinator.Instance);
                    break;
                case "ClarifierViewModel":
                    ViewModel = new ClarifierViewModel(DialogCoordinator.Instance);
                    break;
                case "ReactionTankViewModel":
                    ViewModel = new ReactionTankViewModel(DialogCoordinator.Instance);
                    break;
                case "SiteDevelopmentViewModel":
                    ViewModel = new SiteDevelopmentViewModel(DialogCoordinator.Instance);
                    break;
                case "PlaceholderViewModel":
                    ViewModel = new PlaceholderViewModel();
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
                case "VerticalFlowPondViewModel":
                    BackgroundColor = _green; 
                    break;
                case "LimestoneBedViewModel":
                    BackgroundColor = _green; 
                    break;
                case "ManganeseRemovalBedViewModel":
                    BackgroundColor = _green;
                    break;
                case "WetlandViewModel":
                    BackgroundColor = _green;
                    break;
                case "BioReactorViewModel":
                    BackgroundColor = _green;
                    break;
                case "AnoxicLimestoneDrainViewModel":
                    BackgroundColor = _green;
                    break;
                case "CausticSodaViewModel":
                    BackgroundColor = _blue; 
                    break;
                case "LimeSlurryViewModel":
                    BackgroundColor = _blue;
                    break;
                case "DryLimeViewModel":
                    BackgroundColor = _blue;
                    break;
                case "PumpingViewModel":
                    BackgroundColor = _orange;
                    break;
                case "ConveyanceDitchViewModel":
                    BackgroundColor = _orange;
                    break;
                case "SamplingViewModel":
                    BackgroundColor = _orange;
                    break;
                case "PondsViewModel":
                    BackgroundColor = _orange;
                    break;
                case "ClarifierViewModel":
                    BackgroundColor = _green;
                    break;
                case "ReactionTankViewModel":
                    BackgroundColor = _blue;
                    break;
                case "SiteDevelopmentViewModel":
                    BackgroundColor = _orange;
                    break;
                case "ChemicalCostViewModel":
                    BackgroundColor = _orange;
                    break;
                case "PlaceholderViewModel":
                    BackgroundColor = "Gray";
                    break;
                default:
                    break;
            }
        }

        private void SetOpacity(System.Type moduleType)
        {
            switch (moduleType.Name)
            {
                case "PlaceholderViewModel":
                    Opacity = "0";
                    break;
                default:
                    break;
            }

        }
        #endregion

        #region Constructors

        public ModuleItem(string name, System.Type moduleType, PackIconModernKind icon)
        {
            Name = name;
            Icon = icon;
            ModuleType = moduleType;
            SetViewModel(ModuleType);
            SetBackgroundColor(ModuleType);
            SetOpacity(ModuleType);
        }

        // Copy constructor
        public ModuleItem(ModuleItem moduleItem)
        {
            this.Name = moduleItem.Name;
            this.Icon = moduleItem.Icon;
            this.ModuleType = moduleItem.ModuleType;
            SetViewModel(moduleItem.ModuleType);
            SetBackgroundColor(moduleItem.ModuleType);
            SetOpacity(ModuleType);
        }


        #endregion

    }
}
