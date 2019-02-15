using AMDTreat.Commands;
using AMDTreat.Models;
using AMDTreat.Properties;
using AMDTreat.Views;
using GongSolutions.Wpf.DragDrop;
using GongSolutions.Wpf.DragDrop.Utilities;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.IconPacks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.Specialized;
using System.Globalization;
using System.Xml.Serialization;
using Microsoft.Win32;

namespace AMDTreat.ViewModels
{
    public class MainViewModel : PropertyChangedBase, IDropTarget, IObserver<SharedData>
    {

        #region Properties / Fields
        public TestViewModel Test { get; private set; }

        private IDialogCoordinator _dialogCoordinator;
        public ObservableCollection<ModuleItem> PassiveModuleItems { get; set; } = new ObservableCollection<ModuleItem>();
        public ObservableCollection<ModuleItem> ActiveModuleItems { get; set; } = new ObservableCollection<ModuleItem>();
        public ObservableCollection<ModuleItem> ProjectModuleItems { get; set; } = new ObservableCollection<ModuleItem>();
        public ObservableCollection<ModuleItem> PlaceholderModuleItems { get; set; } = new ObservableCollection<ModuleItem>();

        public ObservableCollection<SharedData> SharedData { get; set; } = new ObservableCollection<SharedData>();

        public ModuleItem moduleItem;
        public VerticalFlowPondWindow vfpWindow;        

        /// <summary>
        /// 
        /// </summary>
        private ObservableCollection<ModuleItem> _moduleTiles;
        /// <summary>
        /// Collection used for the Recapitalization table (data grid).
        /// </summary>
        public ObservableCollection<ModuleItem> ModuleTiles
        {
            get { return _moduleTiles; }

            set { ChangeAndNotify(ref _moduleTiles, value, nameof(ModuleTiles)); }
        }

        /// <summary>
        /// 
        /// </summary>
        private ObservableCollection<ModuleItem> _moduleTiles1;
        /// <summary>
        /// Collection used for the Recapitalization table (data grid).
        /// </summary>
        public ObservableCollection<ModuleItem> ModuleTiles1
        {
            get { return _moduleTiles1; }

            set { ChangeAndNotify(ref _moduleTiles1, value, nameof(ModuleTiles1)); }
        }

        /// <summary>
        /// 
        /// </summary>
        private ObservableCollection<ModuleItem> _moduleTiles2;
        /// <summary>
        /// Collection used for the Recapitalization table (data grid).
        /// </summary>
        public ObservableCollection<ModuleItem> ModuleTiles2
        {
            get { return _moduleTiles2; }

            set { ChangeAndNotify(ref _moduleTiles2, value, nameof(ModuleTiles2)); }
        }

        #endregion

        #region Recapitalization (Net Present Value) Properties

        private double _recapitalizationCostCalculationPeriod;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostCalculationPeriod
        {
            get
            {
                RecapitalizationCostCalculationPeriod_SharedData = _recapitalizationCostCalculationPeriod;

                return _recapitalizationCostCalculationPeriod;
            }
            set { ChangeAndNotify(ref _recapitalizationCostCalculationPeriod, value, nameof(RecapitalizationCostCalculationPeriod)); }
        }

        private double _recapitalizationCostCalculationPeriod_SharedData;
        /// <summary>
        /// Compute the total capital cost for all modules
        /// </summary>
        public double RecapitalizationCostCalculationPeriod_SharedData
        {
            get { return _recapitalizationCostCalculationPeriod_SharedData; }
            set { ChangeAndNotify(ref _recapitalizationCostCalculationPeriod_SharedData, value, nameof(RecapitalizationCostCalculationPeriod_SharedData)); }
        }

        private double _recapitalizationCostInflationRate;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostInflationRate
        {
            get
            {
                RecapitalizationCostInflationRate_SharedData = _recapitalizationCostInflationRate;

                return _recapitalizationCostInflationRate;
            }
            set { ChangeAndNotify(ref _recapitalizationCostInflationRate, value, nameof(RecapitalizationCostInflationRate)); }
        }

        private double _recapitalizationCostInflationRate_SharedData;
        /// <summary>
        /// Compute the total capital cost for all modules
        /// </summary>
        public double RecapitalizationCostInflationRate_SharedData
        {
            get { return _recapitalizationCostInflationRate_SharedData; }
            set { ChangeAndNotify(ref _recapitalizationCostInflationRate_SharedData, value, nameof(RecapitalizationCostInflationRate_SharedData)); }
        }

        private double _recapitalizationCostNetRateOfReturn;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostNetRateOfReturn
        {
            get
            {
                RecapitalizationCostNetRateOfReturn_SharedData = _recapitalizationCostNetRateOfReturn;

                return _recapitalizationCostNetRateOfReturn;
            }
            set { ChangeAndNotify(ref _recapitalizationCostNetRateOfReturn, value, nameof(RecapitalizationCostNetRateOfReturn)); }
        }

        private double _recapitalizationCostNetRateOfReturn_SharedData;
        /// <summary>
        /// Compute the total capital cost for all modules
        /// </summary>
        public double RecapitalizationCostNetRateOfReturn_SharedData
        {
            get { return _recapitalizationCostNetRateOfReturn_SharedData; }
            set { ChangeAndNotify(ref _recapitalizationCostNetRateOfReturn_SharedData, value, nameof(RecapitalizationCostNetRateOfReturn_SharedData)); }
        }
        #endregion

        #region Total Capital Cost

        private decimal _capitalCostDataAll;
        /// <summary>
        /// Compute the total capital cost for all modules
        /// </summary>
        public decimal CapitalCostDataAll
        {
            get
            {
                Dictionary<string, List<decimal>> capitalCostTotals = CalcCapitalCostTotals();

                _capitalCostDataAll = capitalCostTotals["All"].Sum();

                return _capitalCostDataAll;
            }
            set { ChangeAndNotify(ref _capitalCostDataAll, value, nameof(CapitalCostDataAll)); }
        }

        private decimal _capitalCostDataTotalPassive;
        /// <summary>
        /// Return the total passive capital cost
        /// </summary>
        public decimal CapitalCostDataTotalPassive
        {
            get
            {
                Dictionary <string, List<decimal>> capitalCostTotals = CalcCapitalCostTotals();

                _capitalCostDataTotalPassive = capitalCostTotals["Passive"].Sum();
                CapitalCostPassive_SharedData = _capitalCostDataTotalPassive;

                return _capitalCostDataTotalPassive;
            }
            set { ChangeAndNotify(ref _capitalCostDataTotalPassive, value, nameof(CapitalCostDataTotalPassive)); }
        }

        private decimal _capitalCostPassive_SharedData;
        /// <summary>
        /// Compute the total capital cost for all modules
        /// </summary>
        public decimal CapitalCostPassive_SharedData
        {
            get { return _capitalCostPassive_SharedData; }
            set { ChangeAndNotify(ref _capitalCostPassive_SharedData, value, nameof(CapitalCostPassive_SharedData)); }
        }

        private decimal _capitalCostDataTotalActive;
        /// <summary>
        /// Return the total active capital cost
        /// </summary>
        public decimal CapitalCostDataTotalActive
        {
            get
            {
                Dictionary<string, List<decimal>> capitalCostTotals = CalcCapitalCostTotals();

                _capitalCostDataTotalActive = capitalCostTotals["Active"].Sum();
                CapitalCostActive_SharedData = _capitalCostDataTotalActive;

                return _capitalCostDataTotalActive;
            }
            set { ChangeAndNotify(ref _capitalCostDataTotalActive, value, nameof(CapitalCostDataTotalActive)); }
        }

        private decimal _capitalCostActive_SharedData;
        /// <summary>
        /// Compute the total capital cost for all modules
        /// </summary>
        public decimal CapitalCostActive_SharedData
        {
            get { return _capitalCostActive_SharedData; }
            set { ChangeAndNotify(ref _capitalCostActive_SharedData, value, nameof(CapitalCostActive_SharedData)); }
        }

        private decimal _capitalCostDataTotalProject;
        /// <summary>
        /// Return the total active capital cost
        /// </summary>
        public decimal CapitalCostDataTotalProject
        {
            get
            {
                Dictionary<string, List<decimal>> capitalCostTotals = CalcCapitalCostTotals();

                return capitalCostTotals["Project"].Sum();
            }
            set { ChangeAndNotify(ref _capitalCostDataTotalProject, value, nameof(CapitalCostDataTotalProject)); }
        }

        /// <summary>
        /// Calculate the total capital cost for the entire treatment system
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, List<decimal>> CalcCapitalCostTotals()
        {
            Dictionary<string, List<decimal>> capitalCostTotals = new Dictionary<string, List<decimal>>();
            List<decimal> capitalCostTotalValuesAll = new List<decimal>();
            List<decimal> capitalCostTotalValuesPassive = new List<decimal>();
            List<decimal> capitalCostTotalValuesActive = new List<decimal>();
            List<decimal> capitalCostTotalValuesProject = new List<decimal>();

            foreach (var data in SharedData)
            {
                switch (data.ModuleTreatmentType)
                {
                    case "Passive":
                        capitalCostTotalValuesPassive.Add((decimal)data.Data["CapitalCostData"]);
                        break;
                    case "Active":
                        capitalCostTotalValuesActive.Add((decimal)data.Data["CapitalCostData"]);
                        break;
                    case "Project":
                        capitalCostTotalValuesProject.Add((decimal)data.Data["CapitalCostData"]);
                        break;
                    default:
                        break;
                }

                capitalCostTotalValuesAll.Add((decimal)data.Data["CapitalCostData"]);
            }

            capitalCostTotals["Passive"] = capitalCostTotalValuesPassive;
            capitalCostTotals["Active"] = capitalCostTotalValuesActive;
            capitalCostTotals["Project"] = capitalCostTotalValuesProject;
            capitalCostTotals["All"] = capitalCostTotalValuesAll;

            return capitalCostTotals;
        }
        #endregion

        #region Total Annual Cost

        private decimal _annualCostDataAll;
        /// <summary>
        /// Compute the total annual cost for all modules
        /// </summary>
        public decimal AnnualCostDataAll
        {
            get
            {
                Dictionary<string, List<decimal>> annualCostTotals = CalcAnnualCostTotals();

                return annualCostTotals["All"].Sum();
            }
            set { ChangeAndNotify(ref _annualCostDataAll, value, nameof(AnnualCostDataAll)); }
        }

        private decimal _annualCostDataTotalPassive;
        /// <summary>
        /// Return the total passive annual cost
        /// </summary>
        public decimal AnnualCostDataTotalPassive
        {
            get
            {
                Dictionary<string, List<decimal>> annualCostTotals = CalcAnnualCostTotals();

                return annualCostTotals["Passive"].Sum();
            }
            set { ChangeAndNotify(ref _annualCostDataTotalPassive, value, nameof(AnnualCostDataTotalPassive)); }
        }

        private decimal _annualCostDataTotalActive;
        /// <summary>
        /// Return the total active annual cost
        /// </summary>
        public decimal AnnualCostDataTotalActive
        {
            get
            {
                Dictionary<string, List<decimal>> annualCostTotals = CalcAnnualCostTotals();

                return annualCostTotals["Active"].Sum();
            }
            set { ChangeAndNotify(ref _annualCostDataTotalActive, value, nameof(AnnualCostDataTotalActive)); }
        }

        private decimal _annualCostDataTotalProject;
        /// <summary>
        /// Return the total other annual cost
        /// </summary>
        public decimal AnnualCostDataTotalProject
        {
            get
            {
                Dictionary<string, List<decimal>> annualCostTotals = CalcAnnualCostTotals();

                return annualCostTotals["Project"].Sum();
            }
            set { ChangeAndNotify(ref _annualCostDataTotalProject, value, nameof(AnnualCostDataTotalProject)); }
        }

        /// <summary>
        /// Calculate the total capital cost for the entire treatment system
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, List<decimal>> CalcAnnualCostTotals()
        {
            Dictionary<string, List<decimal>> annualCostTotals = new Dictionary<string, List<decimal>>();
            List<decimal> annualCostTotalValuesAll = new List<decimal>();
            List<decimal> annualCostTotalValuesPassive = new List<decimal>();
            List<decimal> annualCostTotalValuesActive = new List<decimal>();
            List<decimal> annualCostTotalValuesProject = new List<decimal>();

            foreach (var data in SharedData)
            {
                switch (data.ModuleTreatmentType)
                {
                    case "Passive":
                        annualCostTotalValuesPassive.Add((decimal)data.Data["AnnualCostData"]);
                        break;
                    case "Active":
                        annualCostTotalValuesActive.Add((decimal)data.Data["AnnualCostData"]);
                        break;
                    case "Project":
                        annualCostTotalValuesProject.Add((decimal)data.Data["AnnualCostData"]);
                        break;
                    default:
                        break;
                }

                annualCostTotalValuesAll.Add((decimal)data.Data["AnnualCostData"]);
            }

            annualCostTotals["Passive"] = annualCostTotalValuesPassive;
            annualCostTotals["Active"] = annualCostTotalValuesActive;
            annualCostTotals["Project"] = annualCostTotalValuesProject;
            annualCostTotals["All"] = annualCostTotalValuesAll;

            return annualCostTotals;
        }
        #endregion

        #region Total Recapitalization Cost

        private decimal _recapCostDataAll;
        /// <summary>
        /// Compute the total recap cost for all modules
        /// </summary>
        public decimal RecapCostDataAll
        {
            get
            {
                Dictionary<string, List<decimal>> recapCostTotals = CalcRecapCostTotals();

                return recapCostTotals["All"].Sum();
            }
            set { ChangeAndNotify(ref _recapCostDataAll, value, nameof(RecapCostDataAll)); }
        }

        private decimal _recapCostDataTotalPassive;
        /// <summary>
        /// Return the total passive recap cost
        /// </summary>
        public decimal RecapCostDataTotalPassive
        {
            get
            {
                Dictionary<string, List<decimal>> recapCostTotals = CalcRecapCostTotals();

                return recapCostTotals["Passive"].Sum();
            }
            set { ChangeAndNotify(ref _recapCostDataTotalPassive, value, nameof(RecapCostDataTotalPassive)); }
        }

        private decimal _recapCostDataTotalActive;
        /// <summary>
        /// Return the total active recap cost
        /// </summary>
        public decimal RecapCostDataTotalActive
        {
            get
            {
                Dictionary<string, List<decimal>> recapCostTotals = CalcRecapCostTotals();

                return recapCostTotals["Active"].Sum();
            }
            set { ChangeAndNotify(ref _recapCostDataTotalActive, value, nameof(RecapCostDataTotalActive)); }
        }

        private decimal _recapCostDataTotalProject;
        /// <summary>
        /// Return the total other recap cost
        /// </summary>
        public decimal RecapCostDataTotalProject
        {
            get
            {
                Dictionary<string, List<decimal>> recapCostTotals = CalcRecapCostTotals();

                return recapCostTotals["Project"].Sum();
            }
            set { ChangeAndNotify(ref _recapCostDataTotalProject, value, nameof(RecapCostDataTotalProject)); }
        }

        /// <summary>
        /// Calculate the total capital cost for the entire treatment system
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, List<decimal>> CalcRecapCostTotals()
        {
            Dictionary<string, List<decimal>> recapCostTotals = new Dictionary<string, List<decimal>>();
            List<decimal> recapCostTotalValuesAll = new List<decimal>();
            List<decimal> recapCostTotalValuesPassive = new List<decimal>();
            List<decimal> recapCostTotalValuesActive = new List<decimal>();
            List<decimal> recapCostTotalValuesProject = new List<decimal>();

            foreach (var data in SharedData)
            {
                switch (data.ModuleTreatmentType)
                {
                    case "Passive":
                        recapCostTotalValuesPassive.Add((decimal)data.Data["RecapCostData"]);
                        break;
                    case "Active":
                        recapCostTotalValuesActive.Add((decimal)data.Data["RecapCostData"]);
                        break;
                    case "Project":
                        recapCostTotalValuesProject.Add((decimal)data.Data["RecapCostData"]);
                        break;
                    default:
                        break;
                }

                recapCostTotalValuesAll.Add((decimal)data.Data["RecapCostData"]);
            }

            recapCostTotals["Passive"] = recapCostTotalValuesPassive;
            recapCostTotals["Active"] = recapCostTotalValuesActive;
            recapCostTotals["Project"] = recapCostTotalValuesProject;
            recapCostTotals["All"] = recapCostTotalValuesAll;

            return recapCostTotals;
        }
        #endregion

        #region Total Clear And Grub Area

        private double _clearAndGrubAreaDataTotalPassive;
        /// <summary>
        /// Return the total passive capital cost
        /// </summary>
        public double ClearAndGrubAreaDataTotalPassive
        {
            get
            {
                Dictionary<string, List<double>> clearAndGrubAreaTotals = CalcClearAndGrubAreaTotals();

                _clearAndGrubAreaDataTotalPassive = clearAndGrubAreaTotals["Passive"].Sum();
                ClearAndGrubAreaPassive_SharedData = _clearAndGrubAreaDataTotalPassive;

                return _clearAndGrubAreaDataTotalPassive;
            }
            set { ChangeAndNotify(ref _clearAndGrubAreaDataTotalPassive, value, nameof(ClearAndGrubAreaDataTotalPassive)); }
        }

        private double _clearAndGrubAreaPassive_SharedData;
        /// <summary>
        /// Compute the total capital cost for all modules
        /// </summary>
        public double ClearAndGrubAreaPassive_SharedData
        {
            get { return _clearAndGrubAreaPassive_SharedData; }
            set { ChangeAndNotify(ref _clearAndGrubAreaPassive_SharedData, value, nameof(ClearAndGrubAreaPassive_SharedData)); }
        }

        /// <summary>
        /// Calculate the total capital cost for the entire treatment system
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, List<double>> CalcClearAndGrubAreaTotals()
        {
            Dictionary<string, List<double>> clearAndGrubAreaTotals = new Dictionary<string, List<double>>();
            List<double> clearAndGrubAreaTotalValuesPassive = new List<double>();

            foreach (var data in SharedData)
            {
                switch (data.ModuleTreatmentType)
                {
                    case "Passive":
                        clearAndGrubAreaTotalValuesPassive.Add((double)data.Data["ClearAndGrubAreaData"]);
                        break;
                    default:
                        break;
                }
            }

            clearAndGrubAreaTotals["Passive"] = clearAndGrubAreaTotalValuesPassive;

            return clearAndGrubAreaTotals;
        }
        #endregion

        #region Total Foundation Area


        private double _foundationAreaDataTotalPassiveAndActive;
        /// <summary>
        /// Return the total passive capital cost
        /// </summary>
        public double FoundationAreaDataTotalPassiveAndActive
        {
            get
            {
                Dictionary<string, List<double>> foundationAreaTotals = CalcFoundationAreaTotals();

                _foundationAreaDataTotalPassiveAndActive = foundationAreaTotals["PassiveAndActive"].Sum();
                FoundationAreaPassiveAndActive_SharedData = _foundationAreaDataTotalPassiveAndActive;

                return _foundationAreaDataTotalPassiveAndActive;
            }
            set { ChangeAndNotify(ref _foundationAreaDataTotalPassiveAndActive, value, nameof(FoundationAreaDataTotalPassiveAndActive)); }
        }

        private double _foundationAreaPassiveAndActive_SharedData;
        /// <summary>
        /// Compute the total capital cost for all modules
        /// </summary>
        public double FoundationAreaPassiveAndActive_SharedData
        {
            get { return _foundationAreaPassiveAndActive_SharedData; }
            set { ChangeAndNotify(ref _foundationAreaPassiveAndActive_SharedData, value, nameof(FoundationAreaPassiveAndActive_SharedData)); }
        }

        /// <summary>
        /// Calculate the total capital cost for the entire treatment system
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, List<double>> CalcFoundationAreaTotals()
        {
            Dictionary<string, List<double>> foundationAreaTotals = new Dictionary<string, List<double>>();
            List<double> foundationAreaTotalValuesPassiveAndActive = new List<double>();
            List<double> foundationAreaTotalValuesPassive = new List<double>();
            List<double> foundationAreaTotalValuesActive = new List<double>();

            foreach (var data in SharedData)
            {
                if (data.Data.ContainsKey("FoundationAreaData"))
                {
                    switch (data.ModuleTreatmentType)
                    {
                        case "Passive":
                            foundationAreaTotalValuesPassive.Add((double)data.Data["FoundationAreaData"]);
                            break;
                        case "Active":
                            foundationAreaTotalValuesActive.Add((double)data.Data["FoundationAreaData"]);
                            break;
                        default:
                            break;
                    }
                    foundationAreaTotalValuesPassiveAndActive.Add((double)data.Data["FoundationAreaData"]);
                }
            }

            foundationAreaTotals["PassiveAndActive"] = foundationAreaTotalValuesPassiveAndActive;

            return foundationAreaTotals;
        }
        #endregion

        #region Total Foundation Area Times Depth

        private double _foundationAreaTimesDepthDataTotalPassiveAndActive;
        /// <summary>
        /// Return the total passive capital cost
        /// </summary>
        public double FoundationAreaTimesDepthDataTotalPassiveAndActive
        {
            get
            {
                Dictionary<string, List<double>> foundationAreaTimesDepthTotals = CalcFoundationAreaTimesDepthTotals();

                _foundationAreaTimesDepthDataTotalPassiveAndActive = foundationAreaTimesDepthTotals["PassiveAndActive"].Sum();
                FoundationAreaTimesDepthPassiveAndActive_SharedData = _foundationAreaTimesDepthDataTotalPassiveAndActive;

                return _foundationAreaTimesDepthDataTotalPassiveAndActive;
            }
            set { ChangeAndNotify(ref _foundationAreaTimesDepthDataTotalPassiveAndActive, value, nameof(FoundationAreaTimesDepthDataTotalPassiveAndActive)); }
        }

        private double _foundationAreaTimesDepthPassiveAndActive_SharedData;
        /// <summary>
        /// Compute the total capital cost for all modules
        /// </summary>
        public double FoundationAreaTimesDepthPassiveAndActive_SharedData
        {
            get { return _foundationAreaTimesDepthPassiveAndActive_SharedData; }
            set { ChangeAndNotify(ref _foundationAreaTimesDepthPassiveAndActive_SharedData, value, nameof(FoundationAreaTimesDepthPassiveAndActive_SharedData)); }
        }

        /// <summary>
        /// Calculate the total capital cost for the entire treatment system
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, List<double>> CalcFoundationAreaTimesDepthTotals()
        {
            Dictionary<string, List<double>> foundationAreaTimesDepthTotals = new Dictionary<string, List<double>>();
            List<double> foundationAreaTimesDepthTotalValuesPassiveAndActive = new List<double>();
            List<double> foundationAreaTimesDepthTotalValuesPassive = new List<double>();
            List<double> foundationAreaTimesDepthTotalValuesActive = new List<double>();

            foreach (var data in SharedData)
            {
                if (data.Data.ContainsKey("FoundationAreaTimesDepthData"))
                {
                    switch (data.ModuleTreatmentType)
                    {
                        case "Passive":
                            foundationAreaTimesDepthTotalValuesPassive.Add((double)data.Data["FoundationAreaTimesDepthData"]);
                            break;
                        case "Active":
                            foundationAreaTimesDepthTotalValuesActive.Add((double)data.Data["FoundationAreaTimesDepthData"]);
                            break;
                        default:
                            break;
                    }
                    foundationAreaTimesDepthTotalValuesPassiveAndActive.Add((double)data.Data["FoundationAreaTimesDepthData"]);
                }
            }

            foundationAreaTimesDepthTotals["PassiveAndActive"] = foundationAreaTimesDepthTotalValuesPassiveAndActive;

            return foundationAreaTimesDepthTotals;
        }
        #endregion

        #region Commands

        public ICommand OpenCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand HelpCommand { get; }
        public ICommand ShowWindowCommand { get; }
        public ICommand RemoveTileCommand { get; }
        public ICommand TileClickCommand { get; }

        private ICommand _showMessageDialogCommandAbout;
        public ICommand ShowMessageDialogCommandAbout
        {
            get
            {
                return this._showMessageDialogCommandAbout ?? (this._showMessageDialogCommandAbout = new SimpleCommand
                {
                    CanExecuteDelegate = x => true,
                    ExecuteDelegate = x => RunCustomDialog()
                });
            }
        }
    
        private ICommand _showMessageDialogCommandTbd;
        public ICommand ShowMessageDialogCommandTbd
        {
            get
            {
                return _showMessageDialogCommandTbd ?? (this._showMessageDialogCommandTbd = new SimpleCommand
                {
                    CanExecuteDelegate = x => true,
                    ExecuteDelegate = async x =>
                    {
                        string message = "To Be Determined";
                        await _dialogCoordinator.ShowMessageAsync(this, "AMDTreat", message);
                    }
                });
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Show help *.html files.
        /// </summary>
        public void ShowHelp()
        {
            System.Diagnostics.Process.Start(@"..\..\Help\TestHtmlPage.html");
        }

        private async void RunCustomDialog()
        {
            var customDialog = new CustomDialog() { Title = "About AMDTreat" };

            var customDialogViewModel = new CustomDialogViewModel(instance =>
            {
                _dialogCoordinator.HideMetroDialogAsync(this, customDialog);
            });
            customDialogViewModel.Message = Resources.infoAmdtreatVFP;
            customDialogViewModel.Image = new Uri("/Images/vertical-flow-pond.png", UriKind.Relative);
            customDialog.Content = new CustomDialogInfo() { DataContext = customDialogViewModel };

            await _dialogCoordinator.ShowMetroDialogAsync(this, customDialog);
        }


        public void RemoveTile(object moduleTile)
        {
            ModuleItem itemRemove = (ModuleItem)moduleTile;
            //ModuleTiles.Remove(item);   

            // Loop through each column and find the item to be removed and then remove it.
            List<ObservableCollection<ModuleItem>> columns = new List<ObservableCollection<ModuleItem>>() { ModuleTiles, ModuleTiles1, ModuleTiles2};

            foreach (ObservableCollection<ModuleItem> column in columns)
            {
                foreach (ModuleItem item in column.ToArray())
                {
                    if (item.ViewModel.ModuleId == itemRemove.ViewModel.ModuleId)
                    {
                        column.Remove(item);
                    }
                }
            }
        }

        #endregion

        #region Drag and Drop Functionality

        public void DragOver(IDropInfo dropInfo)
        {
            dropInfo.DropTargetAdorner = DropTargetAdorners.Highlight;
            dropInfo.Effects = DragDropEffects.Copy;           
        }

        /// <summary>
        /// Here you override the drop handler to provide definition for the drop,
        /// Provided is functionality that allows the data to be copied from the moduleList
        /// and for data to be moved within the same list by dragging and droping
        /// </summary>
        /// <param name="dropInfo"></param>
        void IDropTarget.Drop(IDropInfo dropInfo)
        {
            if (dropInfo == null || dropInfo.DragInfo == null)
            {
                return;
            }

            //Find index for insertion
            var insertIndex = dropInfo.InsertIndex != dropInfo.UnfilteredInsertIndex ? dropInfo.UnfilteredInsertIndex : dropInfo.InsertIndex;

            var itemsControl = dropInfo.VisualTarget as ItemsControl;
            if (itemsControl != null)
            {
                var editableItems = itemsControl.Items as IEditableCollectionView;
                if (editableItems != null)
                {
                    var newItemPlaceholderPosition = editableItems.NewItemPlaceholderPosition;
                    if (newItemPlaceholderPosition == NewItemPlaceholderPosition.AtBeginning && insertIndex == 0)
                    {
                        ++insertIndex;
                    }
                    else if (newItemPlaceholderPosition == NewItemPlaceholderPosition.AtEnd && insertIndex == itemsControl.Items.Count)
                    {
                        --insertIndex;
                    }
                }
            }

            var destinationList = dropInfo.TargetCollection.TryGetList();
            List<object> data = ExtractData(dropInfo.Data).OfType<object>().ToList();
            var copyData = ShouldCopyData(dropInfo);

            // Moving data
            if (!copyData)
            {
                var sourceList = dropInfo.DragInfo.SourceCollection.TryGetList();
                if (sourceList != null)
                {
                    foreach (var o in data)
                    {
                        var index = sourceList.IndexOf(o);
                        if (index != -1)
                        {
                            sourceList.RemoveAt(index);
                            // is the source list the destination list too?
                            if (destinationList != null && Equals(sourceList, destinationList) && index < insertIndex)
                            {
                                --insertIndex;
                            }
                        }
                    }
                }
            }
            // Copying data
            else
            {
                //extract item from data then create a copy of it
                ModuleItem item = data[0] as ModuleItem;
                if (item != null)
                {
                    // create copy and insert back into original data list
                    var copyItem = new ModuleItem(item);
                    data[0] = copyItem;
                }
            }

            if (destinationList != null)
            {
                var listBoxControl = dropInfo.VisualTarget as ListBox;

                // check for cloning
                var cloneData = dropInfo.Effects.HasFlag(DragDropEffects.Copy) || dropInfo.Effects.HasFlag(DragDropEffects.Link);
                foreach (var o in data)
                {
                    var obj2Insert = o;
                    if (cloneData)
                    {
                        var cloneable = o as ICloneable;
                        if (cloneable != null)
                        {
                            obj2Insert = cloneable.Clone();
                        }
                    }

                    destinationList.Insert(insertIndex++, obj2Insert);

                    Notify(nameof(ModuleTiles));
                    Notify(nameof(ModuleTiles1));
                    Notify(nameof(ModuleTiles2));

                    if (listBoxControl != null)
                    {
                        var container = listBoxControl.ItemContainerGenerator.ContainerFromItem(obj2Insert) as ListBox;
                        container?.ApplyTemplate();

                        // for better experience: select the dragged ListBox
                        listBoxControl.SetSelectedItem(obj2Insert);
                    }
                }
            }
        }

        /// <summary>
        /// Retrieved from DefaultDropHandler within GongSolutions source code to be used in above method
        /// Retrieves if the data should be copied by identifying if the source and target collections are the same
        /// and checking if the effects is set to copy
        /// </summary>
        /// <param name="dropInfo"></param>
        /// <returns></returns>
        public static bool ShouldCopyData(IDropInfo dropInfo)
        {
            // default should always the move action/effect
            if (dropInfo == null || dropInfo.DragInfo == null)
            {
                return false;
            }
            var copyData = ((dropInfo.DragInfo.DragDropCopyKeyState != default(DragDropKeyStates)) && dropInfo.KeyStates.HasFlag(dropInfo.DragInfo.DragDropCopyKeyState))
                           || dropInfo.DragInfo.DragDropCopyKeyState.HasFlag(DragDropKeyStates.LeftMouseButton);
            copyData = copyData
                       //&& (dropInfo.DragInfo.VisualSource != dropInfo.VisualTarget)
                       && !(dropInfo.DragInfo.SourceItem is HeaderedContentControl)
                       && !(dropInfo.DragInfo.SourceItem is HeaderedItemsControl)
                       && !(dropInfo.DragInfo.SourceItem is ListBoxItem);
            return copyData;
        }

        /// <summary>
        /// Retrieved from DefaultDropHandler within GongSolutions source code to be used in above method
        /// Purpose is to extract selected data to allow a foreach loop to insert multiple selections into a list
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static IEnumerable ExtractData(object data)
        {
            if (data is IEnumerable && !(data is string))
            {
                return (IEnumerable)data;
            }
            else
            {
                return Enumerable.Repeat(data, 1);
            }
        }

        #endregion

        #region IOberserver Interface - Observer receives notifications from a provider/subject

        private IDisposable unsubscriber;
        private List<Unsubscriber> unsubscribers = new List<Unsubscriber>();

        public virtual void Subscribe(IObservable<SharedData> provider)
        {
            unsubscriber = provider.Subscribe(this);
        }

        public virtual void Unsubscribe()
        {
            unsubscriber.Dispose();
        }

        /// <summary>
        /// Called by the provider which supplies the observer with new or current information.
        /// </summary>
        /// <param name="value"></param>
        public void OnNext(SharedData data)
        {

            //search list for previous entries from same ID
            if (SharedData.Count != 0)
            {
                int indexToUpdate = 0;
                foreach (var c in SharedData)
                {
                    if (c.ModuleId == data.ModuleId)
                    {
                        SharedData[indexToUpdate] = data;
                        return;
                    }
                    indexToUpdate++;
                }
            }

            SharedData.Add(data);
            UpdateCapitalCosts();
            UpdateAnnualCosts();
            UpdateRecapCosts();
            UpdateClearAndGrubArea();
            UpdateFoundationArea();
            UpdateFoundationAreaTimesDepth();
        }

        /// <summary>
        /// Called by the provider which informs the observer that an error has occurred.
        /// </summary>
        /// <param name="error"></param>
        public void OnError(Exception error)
        {
            Console.Write("OnError()!");
        }

        /// <summary>
        /// Called by the provider which indicates that the provider has finished sending notifications.
        /// </summary>
        public void OnCompleted()
        {
            Console.Write("OnCompleted()");
        }


        /// <summary>
        /// Method to handle changes to ModuleTiles
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ModuleTilesItemCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // Additions
            if (e.NewItems != null)
            {
                foreach (ModuleItem item in e.NewItems)
                {
                    // add this view model as an observer
                    IObserver<SharedData> observer = this;
                    Unsubscriber unsubscriber = (Unsubscriber)item.ViewModel.Subscribe(observer);
                    unsubscribers.Add(unsubscriber);

                    if (item.ModuleType == typeof(SiteDevelopmentViewModel))
                    {
                        IObserver<SharedData> observerSiteDev = (SiteDevelopmentViewModel)item.ViewModel;
                        Unsubscriber unsubscriberSiteDev = (Unsubscriber)item.ViewModel.Subscribe(observerSiteDev);
                        unsubscribers.Add(unsubscriberSiteDev);
                    }

                    if (item.ModuleType == typeof(AnoxicLimestoneDrainViewModel))
                    {
                        IObserver<SharedData> observerAnoxicLimestoneDrain = (AnoxicLimestoneDrainViewModel)item.ViewModel;
                        Unsubscriber unsubscriberAnoxicLimestoneDrain = (Unsubscriber)item.ViewModel.Subscribe(observerAnoxicLimestoneDrain);
                        unsubscribers.Add(unsubscriberAnoxicLimestoneDrain);
                    }

                    if (item.ModuleType == typeof(BioReactorViewModel))
                    {
                        IObserver<SharedData> observerBioReactor = (BioReactorViewModel)item.ViewModel;
                        Unsubscriber unsubscriberBioReactor = (Unsubscriber)item.ViewModel.Subscribe(observerBioReactor);
                        unsubscribers.Add(unsubscriberBioReactor);
                    }

                    if (item.ModuleType == typeof(CausticSodaViewModel))
                    {
                        IObserver<SharedData> observerCausticSoda = (CausticSodaViewModel)item.ViewModel;
                        Unsubscriber unsubscriberCausticSoda = (Unsubscriber)item.ViewModel.Subscribe(observerCausticSoda);
                        unsubscribers.Add(unsubscriberCausticSoda);
                    }

                    if (item.ModuleType == typeof(ClarifierViewModel))
                    {
                        IObserver<SharedData> observerClarifier = (ClarifierViewModel)item.ViewModel;
                        Unsubscriber unsubscriberClarifier = (Unsubscriber)item.ViewModel.Subscribe(observerClarifier);
                        unsubscribers.Add(unsubscriberClarifier);
                    }
                    
                    if (item.ModuleType == typeof(ConveyanceDitchViewModel))
                    {
                        IObserver<SharedData> observerConveyanceDitch = (ConveyanceDitchViewModel)item.ViewModel;
                        Unsubscriber unsubscriberConveyanceDitch = (Unsubscriber)item.ViewModel.Subscribe(observerConveyanceDitch);
                        unsubscribers.Add(unsubscriberConveyanceDitch);
                    }

                    if (item.ModuleType == typeof(DryLimeViewModel))
                    {
                        IObserver<SharedData> observerDryLime = (DryLimeViewModel)item.ViewModel;
                        Unsubscriber unsubscriberDryLime = (Unsubscriber)item.ViewModel.Subscribe(observerDryLime);
                        unsubscribers.Add(unsubscriberDryLime);
                    }

                    if (item.ModuleType == typeof(LimeSlurryViewModel))
                    {
                        IObserver<SharedData> observerLimeSlurry = (LimeSlurryViewModel)item.ViewModel;
                        Unsubscriber unsubscriberLimeSlurry = (Unsubscriber)item.ViewModel.Subscribe(observerLimeSlurry);
                        unsubscribers.Add(unsubscriberLimeSlurry);
                    }

                    if (item.ModuleType == typeof(LimestoneBedViewModel))
                    {
                        IObserver<SharedData> observerLimestoneBed = (LimestoneBedViewModel)item.ViewModel;
                        Unsubscriber unsubscriberLimestoneBed = (Unsubscriber)item.ViewModel.Subscribe(observerLimestoneBed);
                        unsubscribers.Add(unsubscriberLimestoneBed);
                    }

                    if (item.ModuleType == typeof(ManganeseRemovalBedViewModel))
                    {
                        IObserver<SharedData> observerManganeseRemoval = (ManganeseRemovalBedViewModel)item.ViewModel;
                        Unsubscriber unsubscriberManganeseRemoval = (Unsubscriber)item.ViewModel.Subscribe(observerManganeseRemoval);
                        unsubscribers.Add(unsubscriberManganeseRemoval);
                    }

                    if (item.ModuleType == typeof(PondsViewModel))
                    {
                        IObserver<SharedData> observerPonds = (PondsViewModel)item.ViewModel;
                        Unsubscriber unsubscriberPonds = (Unsubscriber)item.ViewModel.Subscribe(observerPonds);
                        unsubscribers.Add(unsubscriberPonds);
                    }

                    if (item.ModuleType == typeof(PumpingViewModel))
                    {
                        IObserver<SharedData> observerPumping = (PumpingViewModel)item.ViewModel;
                        Unsubscriber unsubscriberPumping = (Unsubscriber)item.ViewModel.Subscribe(observerPumping);
                        unsubscribers.Add(unsubscriberPumping);
                    }

                    if (item.ModuleType == typeof(ReactionTankViewModel))
                    {
                        IObserver<SharedData> observerReactionTank = (ReactionTankViewModel)item.ViewModel;
                        Unsubscriber unsubscriberReactionTank = (Unsubscriber)item.ViewModel.Subscribe(observerReactionTank);
                        unsubscribers.Add(unsubscriberReactionTank);
                    }

                    if (item.ModuleType == typeof(SamplingViewModel))
                    {
                        IObserver<SharedData> observerSampling = (SamplingViewModel)item.ViewModel;
                        Unsubscriber unsubscriberSampling = (Unsubscriber)item.ViewModel.Subscribe(observerSampling);
                        unsubscribers.Add(unsubscriberSampling);
                    }

                    if (item.ModuleType == typeof(VerticalFlowPondViewModel))
                    {
                        IObserver<SharedData> observerVerticalFlowPond = (VerticalFlowPondViewModel)item.ViewModel;
                        Unsubscriber unsubscriberVerticalFlowPond = (Unsubscriber)item.ViewModel.Subscribe(observerVerticalFlowPond);
                        unsubscribers.Add(unsubscriberVerticalFlowPond);
                    }

                    if (item.ModuleType == typeof(WetlandViewModel))
                    {
                        IObserver<SharedData> observerWetland = (WetlandViewModel)item.ViewModel;
                        Unsubscriber unsubscriberWetland = (Unsubscriber)item.ViewModel.Subscribe(observerWetland);
                        unsubscribers.Add(unsubscriberWetland);
                    }
                }
            }

            // Deletions
            if (e.OldItems != null)
            {
                RemoveTileItem(e.OldItems);
                UpdateCapitalCosts();
                UpdateAnnualCosts();
                UpdateRecapCosts();
                UpdateClearAndGrubArea();
                UpdateFoundationArea();
                UpdateFoundationAreaTimesDepth();
            }
        }


        private void SharedDataCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateCapitalCosts();
            UpdateAnnualCosts();
            UpdateRecapCosts();
            UpdateClearAndGrubArea();
            UpdateFoundationArea();
            UpdateFoundationAreaTimesDepth();

        }

        private void UpdateCapitalCosts()
        {
            OnPropertyChanged(nameof(CapitalCostDataAll));
            OnPropertyChanged(nameof(CapitalCostDataTotalPassive));
            OnPropertyChanged(nameof(CapitalCostDataTotalActive));
            OnPropertyChanged(nameof(CapitalCostDataTotalProject));
        }


        private void UpdateAnnualCosts()
        {
            OnPropertyChanged(nameof(AnnualCostDataAll));
            OnPropertyChanged(nameof(AnnualCostDataTotalPassive));
            OnPropertyChanged(nameof(AnnualCostDataTotalActive));
            OnPropertyChanged(nameof(AnnualCostDataTotalProject));
        }

        private void UpdateRecapCosts()
        {
            OnPropertyChanged(nameof(RecapCostDataAll));
            OnPropertyChanged(nameof(RecapCostDataTotalPassive));
            OnPropertyChanged(nameof(RecapCostDataTotalActive));
            OnPropertyChanged(nameof(RecapCostDataTotalProject));
        }

        private void UpdateClearAndGrubArea()
        {
            OnPropertyChanged(nameof(ClearAndGrubAreaDataTotalPassive));
        }

        private void UpdateFoundationArea()
        {
            OnPropertyChanged(nameof(FoundationAreaDataTotalPassiveAndActive));
        }

        private void UpdateFoundationAreaTimesDepth()
        {
            OnPropertyChanged(nameof(FoundationAreaTimesDepthDataTotalPassiveAndActive));
        }

        private void RemoveTileItem(IList items)
        {
            foreach (ModuleItem item in items)
            {
                // unsubscribe the main view model from a particular view model
                foreach (var unsubscriber in unsubscribers)
                {
                    if (item.ViewModel.ModuleId == unsubscriber.ModuleId)
                    {
                        unsubscriber.Dispose();
                    }
                }

                // update the cost
                foreach (var data in SharedData.ToArray())
                {
                    if (item.ViewModel.ModuleId == data.ModuleId)
                    {
                        SharedData.Remove(data);
                    }
                }
            }
        }

        #endregion

        #region Constructor

        public MainViewModel(IDialogCoordinator dialogCoordinator)
        {
            // MahApps dialog coordinator
            _dialogCoordinator = dialogCoordinator;

            // Assign module treatment type
            ModuleTreatmentType = "Main";

            // Assign the proper functions to the commands
            OpenCommand = new RelayCommand(OpenFile);
            //SaveCommand = new RelayCommand(SaveFile);
            SaveCommand = new RelayCommand(SaveMainUiFile);
            HelpCommand = new RelayCommand(ShowHelp);
            RemoveTileCommand = new RelayCommandWithParameter(RemoveTile);

            PassiveModuleItems.Add(new ModuleItem("Vertical Flow Pond", typeof(VerticalFlowPondViewModel), PackIconModernKind.BoxLayered));
            PassiveModuleItems.Add(new ModuleItem("Limestone Bed", typeof(LimestoneBedViewModel), PackIconModernKind.BoxLayered));
            PassiveModuleItems.Add(new ModuleItem("Manganese Removal Bed", typeof(ManganeseRemovalBedViewModel), PackIconModernKind.BoxLayered));
            PassiveModuleItems.Add(new ModuleItem("Wetland", typeof(WetlandViewModel), PackIconModernKind.BoxLayered));
            PassiveModuleItems.Add(new ModuleItem("Bio Reactor", typeof(BioReactorViewModel), PackIconModernKind.BoxLayered));
            PassiveModuleItems.Add(new ModuleItem("Anoxic Limestone Drain", typeof(AnoxicLimestoneDrainViewModel), PackIconModernKind.BoxLayered));

            PassiveModuleItems.Add(new ModuleItem("Clarifier", typeof(ClarifierViewModel), PackIconModernKind.BoxLayered));

            ActiveModuleItems.Add(new ModuleItem("Caustic Soda", typeof(CausticSodaViewModel), PackIconModernKind.Potion));
            ActiveModuleItems.Add(new ModuleItem("Lime Slurry", typeof(LimeSlurryViewModel), PackIconModernKind.Potion));
            ActiveModuleItems.Add(new ModuleItem("Dry Lime", typeof(DryLimeViewModel), PackIconModernKind.Potion));

            ActiveModuleItems.Add(new ModuleItem("Reaction Tank", typeof(ReactionTankViewModel), PackIconModernKind.Potion));

            ProjectModuleItems.Add(new ModuleItem("Pumping", typeof(PumpingViewModel), PackIconModernKind.ClipboardPaperCheck));
            ProjectModuleItems.Add(new ModuleItem("Conveyance Ditch", typeof(ConveyanceDitchViewModel), PackIconModernKind.ClipboardPaperCheck));
            ProjectModuleItems.Add(new ModuleItem("Sampling", typeof(SamplingViewModel), PackIconModernKind.ClipboardPaperCheck));
            ProjectModuleItems.Add(new ModuleItem("Ponds", typeof(PondsViewModel), PackIconModernKind.ClipboardPaperCheck));
            ProjectModuleItems.Add(new ModuleItem("Site Development", typeof(SiteDevelopmentViewModel), PackIconModernKind.ClipboardPaperCheck));

            PlaceholderModuleItems.Add(new ModuleItem("Placeholder", typeof(PlaceholderViewModel), PackIconModernKind.Minus));

            ModuleTiles = new ObservableCollection<ModuleItem>();
            ModuleTiles1 = new ObservableCollection<ModuleItem>();
            ModuleTiles2 = new ObservableCollection<ModuleItem>();

            // Add method to handle when items change in the collection
            ModuleTiles.CollectionChanged += ModuleTilesItemCollectionChanged;
            ModuleTiles1.CollectionChanged += ModuleTilesItemCollectionChanged;
            ModuleTiles2.CollectionChanged += ModuleTilesItemCollectionChanged;
            SharedData.CollectionChanged += SharedDataCollectionChanged;

            // Recapitalization (Net Rate of Return) initial values
            RecapitalizationCostCalculationPeriod = 75;
            RecapitalizationCostInflationRate = 5.0;
            RecapitalizationCostNetRateOfReturn = 8.0;

        }

        #endregion

    }
}
