using AMDTreat.Commands;
using AMDTreat.Models;
using AMDTreat.Properties;
using AMDTreat.Views;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace AMDTreat.ViewModels
{

    public class SamplingViewModel: PropertyChangedBase, IObserver<SharedData>
    {

        #region Properties - Module Name and Notes
        // NOTE: Fields are set above each corresponding property  

        private string _ModuleType;
        /// <summary>
        /// Set in constructor
        /// </summary>
        public string ModuleType
        {
            get { return _ModuleType; }
            set { ChangeAndNotify(ref _ModuleType, value); }
        }

        /// <summary>
        /// User specified
        /// </summary>
        public string UserNotes { get; set; }

        #endregion

        #region Properties - Sampling

        private bool _isRoundTripTravelTime;
        /// <summary>
        ///  User specified 
        /// </summary>
        public bool IsRoundTripTravelTime
        {
            get { return _isRoundTripTravelTime; }
            set { ChangeAndNotify(ref _isRoundTripTravelTime, value, nameof(IsRoundTripTravelTime), CalcPropertiesStringArray); }
        }

        private bool _isRoundTripMileage;
        /// <summary>
        ///  User specified 
        /// </summary>
        public bool IsRoundTripMileage
        {
            get { return _isRoundTripMileage; }
            set { ChangeAndNotify(ref _isRoundTripMileage, value, nameof(IsRoundTripMileage), CalcPropertiesStringArray); }
        }


        private double _roundTripTravelTime;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double RoundTripTravelTime
        {
            get { return _roundTripTravelTime; }
            set { ChangeAndNotify(ref _roundTripTravelTime, value, nameof(RoundTripTravelTime), CalcPropertiesStringArray); }
        }

        private double _roundTripMileage;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double RoundTripMileage
        {
            get { return _roundTripMileage; }
            set { ChangeAndNotify(ref _roundTripMileage, value, nameof(RoundTripMileage), CalcPropertiesStringArray); }
        }

        private double _npdesMonitoringNumberOfSamplePoints;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double NPDESMonitoringNumberOfSamplePoints
        {
            get { return _npdesMonitoringNumberOfSamplePoints; }
            set { ChangeAndNotify(ref _npdesMonitoringNumberOfSamplePoints, value, nameof(NPDESMonitoringNumberOfSamplePoints), CalcPropertiesStringArray); }
        }

        private double _npdesMonitoringCollectionTimePerSample;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double NPDESMonitoringCollectionTimePerSample
        {
            get { return _npdesMonitoringCollectionTimePerSample; }
            set { ChangeAndNotify(ref _npdesMonitoringCollectionTimePerSample, value, nameof(NPDESMonitoringCollectionTimePerSample), CalcPropertiesStringArray); }
        }

        private double _npdesMonitoringSampleFrequency;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double NPDESMonitoringSampleFrequency
        {
            get { return _npdesMonitoringSampleFrequency; }
            set { ChangeAndNotify(ref _npdesMonitoringSampleFrequency, value, nameof(NPDESMonitoringSampleFrequency), CalcPropertiesStringArray); }
        }

        private double _hydrologicMonitoringNumberOfSamplePoints;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double HydrologicMonitoringNumberOfSamplePoints
        {
            get { return _hydrologicMonitoringNumberOfSamplePoints; }
            set { ChangeAndNotify(ref _hydrologicMonitoringNumberOfSamplePoints, value, nameof(HydrologicMonitoringNumberOfSamplePoints), CalcPropertiesStringArray); }
        }

        private double _hydrologicMonitoringCollectionTimePerSample;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double HydrologicMonitoringCollectionTimePerSample
        {
            get { return _hydrologicMonitoringCollectionTimePerSample; }
            set { ChangeAndNotify(ref _hydrologicMonitoringCollectionTimePerSample, value, nameof(HydrologicMonitoringCollectionTimePerSample), CalcPropertiesStringArray); }
        }

        private double _hydrologicMonitoringSampleFrequency;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double HydrologicMonitoringSampleFrequency
        {
            get { return _hydrologicMonitoringSampleFrequency; }
            set { ChangeAndNotify(ref _hydrologicMonitoringSampleFrequency, value, nameof(HydrologicMonitoringSampleFrequency), CalcPropertiesStringArray); }
        }


        private bool _isSamplingEquipmentCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public bool IsSamplingEquipmentCost
        {
            get { return _isSamplingEquipmentCost; }
            set { ChangeAndNotify(ref _isSamplingEquipmentCost, value, nameof(IsSamplingEquipmentCost), CalcPropertiesStringArray); }
        }

        private decimal _samplingEquipmentCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal SamplingEquipmentCost
        {
            get { return _samplingEquipmentCost; ; }
            set { ChangeAndNotify(ref _samplingEquipmentCost, value, nameof(SamplingEquipmentCost), CalcPropertiesStringArray); }
        }

        private decimal _laborUnitCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal LaborUnitCost
        {
            get { return _laborUnitCost; }
            set { ChangeAndNotify(ref _laborUnitCost, value, nameof(LaborUnitCost), CalcPropertiesStringArray); }
        }

        private decimal _labNPDESSampleUnitCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal LabNPDESSampleUnitCost
        {
            get { return _labNPDESSampleUnitCost; }
            set { ChangeAndNotify(ref _labNPDESSampleUnitCost, value, nameof(LabNPDESSampleUnitCost), CalcPropertiesStringArray); }
        }

        private decimal _labHydrologicSampleUnitCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal LabHydrologicSampleUnitCost
        {
            get { return _labHydrologicSampleUnitCost; }
            set { ChangeAndNotify(ref _labHydrologicSampleUnitCost, value, nameof(LabHydrologicSampleUnitCost), CalcPropertiesStringArray); }
        }

        private decimal _mileageRateUnitCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal MileageRateUnitCost
        {
            get { return _mileageRateUnitCost; }
            set { ChangeAndNotify(ref _mileageRateUnitCost, value, nameof(MileageRateUnitCost), CalcPropertiesStringArray); }
        }

        #endregion

        #region Properties - Capital Costs

       private decimal _calcSamplingEquipmentCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcSamplingEquipmentCost
        {
            get
            {
                if (IsSamplingEquipmentCost)
                {
                    _calcSamplingEquipmentCost = SamplingCalculations.CalcSamplingEquipmentCost(SamplingEquipmentCost);
                }
                else
                {
                    _calcSamplingEquipmentCost = 0m;
                }

                return _calcSamplingEquipmentCost;
            }
            set { ChangeAndNotify(ref _calcSamplingEquipmentCost, value); }
        }

  
        private decimal _calcCapitalCostTotal;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcCapitalCostTotal
        {
            get
            {
                _calcCapitalCostTotal = CalcSamplingEquipmentCost;               

                CapitalCostData = _calcCapitalCostTotal;

                return _calcCapitalCostTotal;
            }
            set { ChangeAndNotify(ref _calcCapitalCostTotal, value); }

        }

        private decimal _capitalCostData;
        /// <summary>
        ///  Data to be shared with main user interface
        /// </summary>
        public decimal CapitalCostData
        {
            get { return _capitalCostData; }
            set { ChangeAndNotify(ref _capitalCostData, value, nameof(CapitalCostData)); }
        }


        #endregion

        #region Properties - Annual (Operations and Maintenance) Costs

        /// <summary>
        /// Radio button binding with enumeration for annual costs
        /// </summary>
        public enum RadioButtonsAnnualCostOptionsEnum
        {
            OptionAnnualCostCalculated,
            OptionAnnualCostUserSpecified
        }

        private RadioButtonsAnnualCostOptionsEnum _annualCostOptionsProperty;
        /// <summary>
        ///  User specified 
        /// </summary>
        public RadioButtonsAnnualCostOptionsEnum AnnualCostOptionsProperty
        {
            get { return _annualCostOptionsProperty; }
            set { ChangeAndNotify(ref _annualCostOptionsProperty, value, nameof(AnnualCostOptionsProperty), CalcPropertiesStringArray); }
        }

        private decimal _annualCostUserSpecified;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal AnnualCostUserSpecified
        {
            get { return _annualCostUserSpecified; }
            set { ChangeAndNotify(ref _annualCostUserSpecified, value, nameof(AnnualCostUserSpecified), CalcPropertiesStringArray); }
        }


        private double _calcAnnualCostNPDESMonitoringSampleFrequencyTime;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcAnnualCostNPDESMonitoringSampleFrequencyTime
        {
            get { return SamplingCalculations.CalcAnnualCostNPDESMonitoringSampleFrequencyTime(NPDESMonitoringSampleFrequency, RoundTripTravelTime); }
            set { ChangeAndNotify(ref _calcAnnualCostNPDESMonitoringSampleFrequencyTime, value); }
        }

        private double _calcAnnualCostHydrologicMonitoringSampleFrequencyTime;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcAnnualCostHydrologicMonitoringSampleFrequencyTime
        {
            get { return SamplingCalculations.CalcAnnualCostHydrologicMonitoringSampleFrequencyTime(HydrologicMonitoringSampleFrequency, RoundTripTravelTime); }
            set { ChangeAndNotify(ref _calcAnnualCostHydrologicMonitoringSampleFrequencyTime, value); }
        }

        private decimal _calcAnnualCostLabor;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcAnnualCostLabor
        {
            get
            {
                if (NPDESMonitoringNumberOfSamplePoints == 0)
                {
                    return SamplingCalculations.CalcAnnualCostLabor(NPDESMonitoringNumberOfSamplePoints, NPDESMonitoringCollectionTimePerSample, NPDESMonitoringSampleFrequency,
                                                                    HydrologicMonitoringNumberOfSamplePoints, HydrologicMonitoringCollectionTimePerSample, HydrologicMonitoringSampleFrequency,
                                                                    RoundTripTravelTime, CalcAnnualCostHydrologicMonitoringSampleFrequencyTime, LaborUnitCost);
                }
                else
                {
                    return SamplingCalculations.CalcAnnualCostLabor(NPDESMonitoringNumberOfSamplePoints, NPDESMonitoringCollectionTimePerSample, NPDESMonitoringSampleFrequency,
                                                                    HydrologicMonitoringNumberOfSamplePoints, HydrologicMonitoringCollectionTimePerSample, HydrologicMonitoringSampleFrequency,
                                                                    RoundTripTravelTime, CalcAnnualCostNPDESMonitoringSampleFrequencyTime, LaborUnitCost);
                }
            }
            set { ChangeAndNotify(ref _calcAnnualCostLabor, value); }
        }

        private decimal _calcAnnualCostLab;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcAnnualCostLab
        {
            get { return SamplingCalculations.CalcAnnualCostLab(NPDESMonitoringNumberOfSamplePoints, NPDESMonitoringSampleFrequency,
                                                                HydrologicMonitoringNumberOfSamplePoints, HydrologicMonitoringSampleFrequency,
                                                                LabNPDESSampleUnitCost, LabHydrologicSampleUnitCost); }
            set { ChangeAndNotify(ref _calcAnnualCost, value); }
        }

        private double _calcAnnualCostNPDESMonitoringSampleFrequencyMileage;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcAnnualCostNPDESMonitoringSampleFrequencyMileage
        {
            get { return SamplingCalculations.CalcAnnualCostNPDESMonitoringSampleFrequencyMileage(NPDESMonitoringSampleFrequency, RoundTripMileage); }
            set { ChangeAndNotify(ref _calcAnnualCostNPDESMonitoringSampleFrequencyMileage, value); }
        }

        private double _calcAnnualCostHydrologicMonitoringSampleFrequencyMileage;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcAnnualCostHydrologicMonitoringSampleFrequencyMileage
        {
            get { return SamplingCalculations.CalcAnnualCostHydrologicMonitoringSampleFrequencyMileage(HydrologicMonitoringSampleFrequency, RoundTripMileage); }
            set { ChangeAndNotify(ref _calcAnnualCostHydrologicMonitoringSampleFrequencyMileage, value); }
        }

        private decimal _calcAnnualCostMileage;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcAnnualCostMileage
        {
            get
            {
                if (NPDESMonitoringNumberOfSamplePoints == 0)
                {
                    return SamplingCalculations.CalcAnnualCostMileage(CalcAnnualCostHydrologicMonitoringSampleFrequencyMileage, MileageRateUnitCost);
                }
                else
                {
                    return SamplingCalculations.CalcAnnualCostMileage(CalcAnnualCostNPDESMonitoringSampleFrequencyMileage, MileageRateUnitCost);
                }
            }
            set { ChangeAndNotify(ref _calcAnnualCost, value); }
        }

        private decimal _calcAnnualCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcAnnualCost
        {
            get
            {
                switch (AnnualCostOptionsProperty)
                {
                    case RadioButtonsAnnualCostOptionsEnum.OptionAnnualCostCalculated:
                        _calcAnnualCost = SamplingCalculations.CalcAnnualCost(CalcAnnualCostLabor, CalcAnnualCostLab, CalcAnnualCostMileage);
                        break;
                    case RadioButtonsAnnualCostOptionsEnum.OptionAnnualCostUserSpecified:
                        _calcAnnualCost = AnnualCostUserSpecified;
                        break;
                }

                AnnualCostData = _calcAnnualCost;

                return _calcAnnualCost;
            }
            set { ChangeAndNotify(ref _calcAnnualCost, value); }
        }

        private decimal _annualCostData;
        /// <summary>
        ///  Data to be shared with main user interface 
        /// </summary>
        public decimal AnnualCostData
        {
            get { return _annualCostData; }
            set
            {
                ChangeAndNotify(ref _annualCostData, value, nameof(AnnualCostData));
            }
        }

        #endregion

        #region Properties - Recapitalization Costs

        private double _recapitalizationCostCalculationPeriod;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostCalculationPeriod
        {
            get
            {
                if (SharedDataCollection.Count > 0)
                {
                    double recapitalizationCostCalculationPeriod = new double();

                    foreach (var data in SharedDataCollection)
                    {
                        recapitalizationCostCalculationPeriod = (double)data.Data["RecapitalizationCostCalculationPeriod_SharedData"];
                    }

                    _recapitalizationCostCalculationPeriod = recapitalizationCostCalculationPeriod;
                }

                return _recapitalizationCostCalculationPeriod;
            }
            set { ChangeAndNotify(ref _recapitalizationCostCalculationPeriod, value, nameof(RecapitalizationCostCalculationPeriod), CalcPropertiesStringArray); }
        }

        private double _calcRecapitalizationCostCalculationPeriod;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcRecapitalizationCostCalculationPeriod
        {
            get { return RecapitalizationCostCalculationPeriod; }
            set { ChangeAndNotify(ref _calcRecapitalizationCostCalculationPeriod, value); }
        }

        private double _recapitalizationCostInflationRate;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostInflationRate
        {
            get
            {
                if (SharedDataCollection.Count > 0)
                {
                    double recapitalizationCostInflationRate = new double();

                    foreach (var data in SharedDataCollection)
                    {
                        recapitalizationCostInflationRate = (double)data.Data["RecapitalizationCostInflationRate_SharedData"];
                    }

                    _recapitalizationCostInflationRate = recapitalizationCostInflationRate;
                }

                return _recapitalizationCostInflationRate;
            }
            set { ChangeAndNotify(ref _recapitalizationCostInflationRate, value, nameof(RecapitalizationCostInflationRate), CalcPropertiesStringArray); }
        }

        private double _calcRecapitalizationCostInflationRate;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcRecapitalizationCostInflationRate
        {
            get { return RecapitalizationCostInflationRate; }
            set { ChangeAndNotify(ref _calcRecapitalizationCostInflationRate, value); }
        }

        private double _recapitalizationCostNetRateOfReturn;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostNetRateOfReturn
        {
            get
            {
                if (SharedDataCollection.Count > 0)
                {
                    double recapitalizationCostNetRateOfReturn = new double();

                    foreach (var data in SharedDataCollection)
                    {
                        recapitalizationCostNetRateOfReturn = (double)data.Data["RecapitalizationCostNetRateOfReturn_SharedData"];
                    }

                    _recapitalizationCostNetRateOfReturn = recapitalizationCostNetRateOfReturn;
                }

                return _recapitalizationCostNetRateOfReturn;
            }
            set { ChangeAndNotify(ref _recapitalizationCostNetRateOfReturn, value, nameof(RecapitalizationCostNetRateOfReturn), CalcPropertiesStringArray); }
        }

        private double _calcRecapitalizationCostNetRateOfReturn;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcRecapitalizationCostNetRateOfReturn
        {
            get { return RecapitalizationCostNetRateOfReturn; }
            set { ChangeAndNotify(ref _calcRecapitalizationCostNetRateOfReturn, value); }
        }

        private double _recapitalizationCostLifeCycleSamplingEquipment;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostLifeCycleSamplingEquipment
        {
            get { return _recapitalizationCostLifeCycleSamplingEquipment; }
            set { ChangeAndNotify(ref _recapitalizationCostLifeCycleSamplingEquipment, value, nameof(RecapitalizationCostLifeCycleSamplingEquipment), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostPercentReplacementSamplingEquipment;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostPercentReplacementSamplingEquipment
        {
            get { return _recapitalizationCostPercentReplacementSamplingEquipment; }
            set { ChangeAndNotify(ref _recapitalizationCostPercentReplacementSamplingEquipment, value, nameof(RecapitalizationCostPercentReplacementSamplingEquipment), CalcPropertiesStringArray); }
        }
 

        private decimal _calcRapitalizationCostSamplingEquipment;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostSamplingEquipment
        {
            get
            {
                return SamplingCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
                                                                    RecapitalizationCostInflationRate, RecapitalizationCostLifeCycleSamplingEquipment,
                                                                    CalcSamplingEquipmentCost, RecapitalizationCostPercentReplacementSamplingEquipment);
            }
            set { ChangeAndNotify(ref _calcRapitalizationCostSamplingEquipment, value); }
        }

 
        private decimal _calcRapitalizationCostAnnual;
        /// <summary>
        /// Calculated. 
        /// <remarks>
        /// The life cycle of annual costs is 1 year, so the life cycle parameter is set to 1.0.
        /// There is no percent replacement for annual cost, so the life cycle parameter is 100 so the percent replacement factor will equal 1 in the calculation.
        /// </remarks>
        /// </summary>
        public decimal CalcRecapitalizationCostAnnual
        {
            get { return SamplingCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn, 
                                                                          RecapitalizationCostInflationRate, 1.0, 
                                                                          CalcAnnualCost, 100.0); }
            set { ChangeAndNotify(ref _calcRapitalizationCostAnnual, value); }
        }

        private decimal _calcRecapitalizationCostTotal;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostTotal
        {
            get { return _calcRecapitalizationCostTotal; }
            set { ChangeAndNotify(ref _calcRecapitalizationCostTotal, value); }
        }

        /// <summary>
        /// Calculate the total cost of all the selected Recapitalization Materials in the data grid
        /// </summary>
        /// <returns>The total cost</returns>
        public decimal CalcRecapMaterialsTotalCost()
        {
            List<decimal> selectedItemsTotals = new List<decimal>();

            foreach (RecapMaterial item in RecapMaterials)
                if (item.IsSelected)
                {
                    selectedItemsTotals.Add(item.TotalCost);
                }

            RecapCostData = selectedItemsTotals.Sum();

            return selectedItemsTotals.Sum();
        }


        private decimal _recapCostData;
        /// <summary>
        ///  Data to be shared with main user interface 
        /// </summary>
        public decimal RecapCostData
        {
            get { return _recapCostData; }
            set { ChangeAndNotify(ref _recapCostData, value, nameof(RecapCostData)); }
        }

        private ObservableCollection<RecapMaterial> _recapMaterials;
        /// <summary>
        /// Collection used for the Recapitalization table (data grid).
        /// </summary>
        public ObservableCollection<RecapMaterial> RecapMaterials
        {
            get { return _recapMaterials; }
            set { ChangeAndNotify(ref _recapMaterials, value, nameof(RecapMaterials), CalcPropertiesStringArray); }
        }

        private double _calcUpdateRecapMaterialCostDefault;
        /// <summary>
        /// Wrapper for updating the default material costs for Recapitalization
        /// </summary>
        public double CalcUpdateRecapMaterialCostDefault
        {
            get
            {
                UpdateRecapMaterialCostDefault();
                return _calcUpdateRecapMaterialCostDefault;
            }
            set { ChangeAndNotify(ref _calcUpdateRecapMaterialCostDefault, value); }
        }

        /// <summary>
        /// Method to update the default material costs for Recapitalization
        /// </summary>
        public void UpdateRecapMaterialCostDefault()
        {
            foreach (RecapMaterial item in RecapMaterials)
                switch (item.NameFixed)
                {
                    case "SamplingEquipment":
                        item.MaterialCostDefault = CalcSamplingEquipmentCost;
                        break;
                    case "Annual":
                        item.MaterialCostDefault = CalcAnnualCost;
                        break;
                }
        }

        private double _calcUpdateRecapMaterialCostTotal;
        /// <summary>
        /// Wrapper for updating the default material costs for Recapitalization
        /// </summary>
        public double CalcUpdateRecapMaterialCostTotal
        {
            get
            {
                UpdateRecapMaterialCostTotal();
                return _calcUpdateRecapMaterialCostTotal;
            }
            set { ChangeAndNotify(ref _calcUpdateRecapMaterialCostTotal, value); }
        }

        /// <summary>
        /// Method to update the default material costs for Recapitalization
        /// </summary>
        public void UpdateRecapMaterialCostTotal()
        {
            foreach (RecapMaterial item in RecapMaterials)
                switch (item.NameFixed)
                {
                    case "SamplingEquipment":
                        item.TotalCost = CalcRecapitalizationCostSamplingEquipment;
                        break;
                    case "Annual":
                        item.TotalCost = CalcRecapitalizationCostAnnual;
                        break;
                }
        }

        public void InitRecapMaterials()
        {
            RecapMaterials = new ObservableCollection<RecapMaterial>();

            // Add method to handle when items change in the collection
            RecapMaterials.CollectionChanged += RecapMaterialsItemCollectionChanged;

            RecapMaterials.Add(new RecapMaterial
            {
                IsSelected = true,
                Name = "SamplingEquipment",
                NameFixed = "SamplingEquipment",
                LifeCycle = RecapitalizationCostLifeCycleSamplingEquipment,
                PercentReplacement = RecapitalizationCostPercentReplacementSamplingEquipment,
                MaterialCostDefault = CalcSamplingEquipmentCost,
                UseCustomCost = false,
                MaterialCostCustom = 0m,
                TotalCost = CalcRecapitalizationCostSamplingEquipment
            });
            RecapMaterials.Add(new RecapMaterial
            {
                IsSelected = true,
                Name = "Annual O&M",
                NameFixed = "Annual",
                LifeCycle = 1,
                PercentReplacement = 100,
                MaterialCostDefault = CalcAnnualCost,
                UseCustomCost = false,
                MaterialCostCustom = 0m,
                TotalCost = CalcRecapitalizationCostAnnual
            });
        }

        /// <summary>
        /// Method to handle user edits to the Recapitalization data grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void RecapMaterialsItemCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // Additions
            if (e.NewItems != null)
                foreach (RecapMaterial item in e.NewItems)
                {
                    item.PropertyChanged += RecapMaterialPropertyChanged;

                    // Check if new item was added and override the LifeCycle value from 0 to 1
                    // This prevents a division by zero in the recap calculation (specifically in the "numCalcPeriodsPerMaterial" calculation)
                    if (item.LifeCycle == 0)
                    {
                        item.LifeCycle = 1;
                    }
                }

            // Deletions
            if (e.OldItems != null)
            {
                foreach (RecapMaterial item in e.OldItems)
                {
                    item.PropertyChanged -= RecapMaterialPropertyChanged;
                }

                // Update the total Recapitalization Cost after any deletions
                CalcRecapitalizationCostTotal = CalcRecapMaterialsTotalCost();
            }

        }

        /// <summary>
        /// Method to handle users edits that influence the Recapitalization data grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void RecapMaterialPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Check if Material Cost is Custom or Default
            decimal materialCost;
            if (((RecapMaterial)sender).UseCustomCost)
            {
                materialCost = ((RecapMaterial)sender).MaterialCostCustom;
            }
            else
            {
                materialCost = ((RecapMaterial)sender).MaterialCostDefault;
            }

            // Calculate the new total for the material
            ((RecapMaterial)sender).TotalCost = SamplingCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
                                                                                                 RecapitalizationCostInflationRate, ((RecapMaterial)sender).LifeCycle,
                                                                                                 materialCost, ((RecapMaterial)sender).PercentReplacement);

            // Update the total Recapitalization Cost
            CalcRecapitalizationCostTotal = CalcRecapMaterialsTotalCost();
        }

        #endregion

        #region Properties - Helpers for property names
        public List<string> PropertiesStringList { get; private set; }
        public string[] CalcPropertiesStringArray { get; set; }

        #endregion

        #region Properties - Commands

        public ICommand OpenCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand HelpCommand { get; }
        public ICommand SyncCommand { get; }

        #endregion

        #region Properties - MahApps Dialog boxes

        private IDialogCoordinator _dialogCoordinator;

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

        private async void RunCustomDialog()
        {
            var customDialog = new CustomDialog() { Title = "About Sampling" };

            var customDialogViewModel = new CustomDialogViewModel(instance =>
            {
                _dialogCoordinator.HideMetroDialogAsync(this, customDialog);
            });
            customDialogViewModel.Message = Resources.infoSAMPLING;
            customDialogViewModel.Image = new Uri("/Images/vertical-flow-pond.png", UriKind.Relative);
            customDialog.Content = new CustomDialogInfo() { DataContext = customDialogViewModel };

            await _dialogCoordinator.ShowMetroDialogAsync(this, customDialog);
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
                        await _dialogCoordinator.ShowMessageAsync(this, ModuleType, message);
                    }
                });
            }
        }

        private ICommand _showMessageDialogCommandSystemProperties;
        public ICommand ShowMessageDialogCommandSystemProperties
        {
            get
            {
                return _showMessageDialogCommandSystemProperties ?? (this._showMessageDialogCommandSystemProperties = new SimpleCommand
                {
                    CanExecuteDelegate = x => true,
                    ExecuteDelegate = async x =>
                    {
                        string message = Resources.infoSystemPropertiesSAMPLING;
                        await _dialogCoordinator.ShowMessageAsync(this, "Sampling", message);
                    }
                });
            }
        }

        private ICommand _showMessageDialogCommandCapitalCost;
        public ICommand ShowMessageDialogCommandCapitalCost
        {
            get
            {
                return _showMessageDialogCommandCapitalCost ?? (this._showMessageDialogCommandCapitalCost = new SimpleCommand
                {
                    CanExecuteDelegate = x => true,
                    ExecuteDelegate = async x =>
                    {
                        string message = Resources.infoCapitalCostSAMPLING;
                        await _dialogCoordinator.ShowMessageAsync(this, "Captial Cost", message);
                    }
                });
            }
        }

        private ICommand _showMessageDialogCommandAnnualCost;
        public ICommand ShowMessageDialogCommandAnnualCost
        {
            get
            {
                return _showMessageDialogCommandAnnualCost ?? (this._showMessageDialogCommandAnnualCost = new SimpleCommand
                {
                    CanExecuteDelegate = x => true,
                    ExecuteDelegate = async x =>
                    {
                        string message = Resources.infoAnnualCostSAMPLING;
                        await _dialogCoordinator.ShowMessageAsync(this, "Annual Cost", message);
                    }
                });
            }
        }

        private ICommand _showMessageDialogCommandRecapitalizationCost;
        public ICommand ShowMessageDialogCommandRecapitalizationCost
        {
            get
            {
                return _showMessageDialogCommandRecapitalizationCost ?? (this._showMessageDialogCommandRecapitalizationCost = new SimpleCommand
                {
                    CanExecuteDelegate = x => true,
                    ExecuteDelegate = async x =>
                    {
                        string message = Resources.infoRecapitalizationCostSAMPLING;
                        await _dialogCoordinator.ShowMessageAsync(this, "Recapitalization Cost", message);
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

        #endregion

        #region Error Handling and Information


        private bool _isError;
        public bool IsError
        {
            get { return _isError; }
            set { ChangeAndNotify(ref _isError, value); }
        }

        private bool _isMajorError;
        public bool IsMajorError
        {
            get { return _isMajorError; }
            set { ChangeAndNotify(ref _isMajorError, value); }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { ChangeAndNotify(ref _errorMessage, value); }
        }

        private string _errorMessageShort;
        public string ErrorMessageShort
        {
            get { return _errorMessageShort; }
            set { ChangeAndNotify(ref _errorMessageShort, value); }
        }

        private void ShowNoError()
        {
            IsError = false;
            IsMajorError = false;
            ErrorMessage = "";
            ErrorMessageShort = "";
        }

        private void ShowError()
        {
            IsError = true;
            IsMajorError = false;
            IsOpenFlyoutError = true;
            ErrorMessage = "";
            ErrorMessageShort = "";
        }

         private void ShowMajorError()
        {
            IsError = true;
            IsMajorError = true;
            IsOpenFlyoutError = true;
            ErrorMessage = "";
            ErrorMessageShort = "";
        }

        public async void ErrorMessageAsync(string message)
        {

            await _dialogCoordinator.ShowMessageAsync(this, ModuleType, message);
        }

        private bool _isOpenFlyoutError;
        public bool IsOpenFlyoutError
        {
            get { return _isOpenFlyoutError; }
            set { ChangeAndNotify(ref _isOpenFlyoutError, value); }
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
            if (SharedDataCollection.Count != 0)
            {
                int indexToUpdate = 0;
                foreach (var c in SharedDataCollection)
                {
                    if (c.ModuleId == data.ModuleId)
                    {
                        SharedDataCollection[indexToUpdate] = data;
                        return;
                    }
                    indexToUpdate++;
                }
            }

            SharedDataCollection.Add(data);
            UpdateRecapitalizationValues();
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

        private void SharedDataCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateRecapitalizationValues();
        }


        public void UpdateRecapitalizationValues()
        {
            foreach (var data in SharedDataCollection.ToList())
            {
                RecapitalizationCostCalculationPeriod = Convert.ToDouble(data.Data["RecapitalizationCostCalculationPeriod_SharedData"]);
                RecapitalizationCostInflationRate = Convert.ToDouble(data.Data["RecapitalizationCostInflationRate_SharedData"]);
                RecapitalizationCostNetRateOfReturn = Convert.ToDouble(data.Data["RecapitalizationCostNetRateOfReturn_SharedData"]);
            }
        }

        public void SyncWithMainUi()
        {
            // Dummy is used to force update of all calculations.
            Dummy = random.Next(1000);

            UpdateRecapitalizationValues();
        }

        private double _dummy;
        /// <summary>
        /// User specified
        /// </summary>
        public double Dummy
        {
            get { return _dummy; }
            set { ChangeAndNotify(ref _dummy, value, nameof(Dummy), CalcPropertiesStringArray); }
        }
        #endregion

        #region Constructor

        public SamplingViewModel(IDialogCoordinator dialogCoordinator)
        {
            // MahApps dialog coordinator
            _dialogCoordinator = dialogCoordinator;

            // Assign the proper functions to the open and save commands
            OpenCommand = new RelayCommand(OpenFile);
            SaveCommand = new RelayCommand(SaveFile);
            HelpCommand = new RelayCommand(ShowHelp);
            SyncCommand = new RelayCommand(SyncWithMainUi);

            // Get a list of property names and filter the names to include only those that start with "Calc" in order
            // to use with the NotifyAndChange.  This eliminates the need to write every property name that needs 
            // to be notified of changes by the user.
            PropertiesStringList = Helpers.GetNamesOfClassProperties(this);
            CalcPropertiesStringArray = Helpers.FilterPropertiesList(PropertiesStringList, "Calc");

            // Initialize the model name and user name
            ModuleType = "Sampling";
            ModuleName = "Untitled module";
            ModuleTreatmentType = "Project";
            ModuleId = random.Next(1000);

            IsError = false;
            IsMajorError = false;
            ErrorMessage = "";
            ErrorMessageShort = "";

            // Initialize radio buttons
            AnnualCostOptionsProperty = RadioButtonsAnnualCostOptionsEnum.OptionAnnualCostCalculated;

            // Initialize checkboxes
            IsRoundTripTravelTime = true;
            IsRoundTripMileage = true;
            IsSamplingEquipmentCost = true;

            // Dummy variables to force syncing
            Dummy = 0;

            // Read the xml data file and assign property values accordingly.
            OpenXmlAndAssignValues(@"..\..\Data\default-data-sampling.xml");

            // Recapitalization parameters that are set one time by a user within the main ui and are not shown in each module
            RecapitalizationCostCalculationPeriod = 75;
            RecapitalizationCostInflationRate = 5.0;
            RecapitalizationCostNetRateOfReturn = 8.0;
            InitRecapMaterials();
            CalcRecapitalizationCostTotal = CalcRecapMaterialsTotalCost();

            // Add method to shared data collection
            SharedDataCollection.CollectionChanged += SharedDataCollectionChanged;
        }

        #endregion

    }

}
