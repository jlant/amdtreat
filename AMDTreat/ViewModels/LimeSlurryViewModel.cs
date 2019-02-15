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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;

namespace AMDTreat.ViewModels
{

    public class LimeSlurryViewModel : PropertyChangedBase, IObserver<SharedData>
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

        #region Properties - Water Quality Inflows

        private double _designFlow;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double DesignFlow
        {
            get { return _designFlow; }
            set { ChangeAndNotify(ref _designFlow, value, nameof(DesignFlow), CalcPropertiesStringArray); }
        }

        private double _hotAcidity;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double HotAcidity
        {
            get { return _hotAcidity; }
            set { ChangeAndNotify(ref _hotAcidity, value, nameof(HotAcidity), CalcPropertiesStringArray); }
        }

        #endregion

        #region Properties - Lime Slurry Information

        private double _limeSlurryPercentage;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double LimeSlurryPercentage
        {
            get { return _limeSlurryPercentage; }
            set { ChangeAndNotify(ref _limeSlurryPercentage, value, nameof(LimeSlurryPercentage), CalcPropertiesStringArray); }
        }

        private double _limeSlurryPurity;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double LimeSlurryPurity
        {
            get { return _limeSlurryPurity; }
            set { ChangeAndNotify(ref _limeSlurryPurity, value, nameof(LimeSlurryPurity), CalcPropertiesStringArray); }
        }

        private double _limeSlurryDissolutionEfficiency;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double LimeSlurryDissolutionEfficiency
        {
            get { return _limeSlurryDissolutionEfficiency; }
            set { ChangeAndNotify(ref _limeSlurryDissolutionEfficiency, value, nameof(LimeSlurryDissolutionEfficiency), CalcPropertiesStringArray); }
        }

        private decimal _limeSlurryUnitCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal LimeSlurryUnitCost
        {
            get { return _limeSlurryUnitCost; }
            set { ChangeAndNotify(ref _limeSlurryUnitCost, value, nameof(LimeSlurryUnitCost), CalcPropertiesStringArray); }
        }
        #endregion

        #region Properties - Chemical Consumption Methods

        /// <summary>
        ///  Radio button binding with enumeration for sizing methods
        /// </summary>
        public enum RadioButtonsChemicalConsumptionOptionsEnum
        {
            OptionStoichiometric,
            OptionTitration,
            OptionUserSpecifiedQuantity,
        }

        private RadioButtonsChemicalConsumptionOptionsEnum _chemicalConsumptionOptionsProperty;
        /// <summary>
        ///  User specified 
        /// </summary>
        public RadioButtonsChemicalConsumptionOptionsEnum ChemicalConsumptionOptionsProperty
        {
            get { return _chemicalConsumptionOptionsProperty; }
            set { ChangeAndNotify(ref _chemicalConsumptionOptionsProperty, value, nameof(ChemicalConsumptionOptionsProperty), CalcPropertiesStringArray); }
        }

        private double _titrationQuantity;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double TitrationQuantity
        {
            get { return _titrationQuantity; }
            set { ChangeAndNotify(ref _titrationQuantity, value, nameof(TitrationQuantity), CalcPropertiesStringArray); }
        }

        private double _limeSlurryDryTonsUserSpecifiedQuantity;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double LimeSlurryDryTonsUserSpecifiedQuantity
        {
            get { return _limeSlurryDryTonsUserSpecifiedQuantity; }
            set { ChangeAndNotify(ref _limeSlurryDryTonsUserSpecifiedQuantity, value, nameof(LimeSlurryDryTonsUserSpecifiedQuantity), CalcPropertiesStringArray); }
        }

        #endregion

        #region Properties - Equipment: Storage and Dispensing System

        private decimal _electricServicePanelCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal ElectricServicePanelCost
        {
            get { return _electricServicePanelCost; }
            set { ChangeAndNotify(ref _electricServicePanelCost, value, nameof(ElectricServicePanelCost), CalcPropertiesStringArray); }
        }

        private decimal _electricRateCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal ElectricRateCost
        {
            get { return _electricRateCost; }
            set { ChangeAndNotify(ref _electricRateCost, value, nameof(ElectricRateCost), CalcPropertiesStringArray); }
        }

        private double _limeSlurryTankVolume;
        /// <summary>
        /// User specified
        /// </summary>
        public double LimeSlurryTankVolume
        {
            get { return _limeSlurryTankVolume; }
            set { ChangeAndNotify(ref _limeSlurryTankVolume, value, nameof(LimeSlurryTankVolume), CalcPropertiesStringArray); }
        }

        private decimal _limeSlurryTankUnitCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal LimeSlurryTankUnitCost
        {
            get { return _limeSlurryTankUnitCost; }
            set { ChangeAndNotify(ref _limeSlurryTankUnitCost, value, nameof(LimeSlurryTankUnitCost), CalcPropertiesStringArray); }
        }

        /// <summary>
        ///  Radio button binding with enumeration for sizing methods
        /// </summary>
        public enum RadioButtonsLimeSlurryRefillVolumeOptionsEnum
        {
            OptionSemiTruck,
            OptionStraightTruck,
            OptionUserSpecified,
        }

        private RadioButtonsLimeSlurryRefillVolumeOptionsEnum _limeSlurryRefillVolumeOptionsProperty;
        /// <summary>
        ///  User specified 
        /// </summary>
        public RadioButtonsLimeSlurryRefillVolumeOptionsEnum LimeSlurryRefillVolumeOptionsProperty
        {
            get { return _limeSlurryRefillVolumeOptionsProperty; }
            set { ChangeAndNotify(ref _limeSlurryRefillVolumeOptionsProperty, value, nameof(LimeSlurryRefillVolumeOptionsProperty), CalcPropertiesStringArray); }
        }

        private double _limeSlurryRefillVolumeSemiTruck;
        /// <summary>
        /// User specified
        /// </summary>
        public double LimeSlurryRefillVolumeSemiTruck
        {
            get { return _limeSlurryRefillVolumeSemiTruck; }
            set { ChangeAndNotify(ref _limeSlurryRefillVolumeSemiTruck, value, nameof(LimeSlurryRefillVolumeSemiTruck), CalcPropertiesStringArray); }
        }

        private double _limeSlurryRefillVolumeStraightTruck;
        /// <summary>
        /// User specified
        /// </summary>
        public double LimeSlurryRefillVolumeStraightTruck
        {
            get { return _limeSlurryRefillVolumeStraightTruck; }
            set { ChangeAndNotify(ref _limeSlurryRefillVolumeStraightTruck, value, nameof(LimeSlurryRefillVolumeStraightTruck), CalcPropertiesStringArray); }
        }

        private double _limeSlurryRefillVolumeUserSpecified;
        /// <summary>
        /// User specified
        /// </summary>
        public double LimeSlurryRefillVolumeUserSpecified
        {
            get { return _limeSlurryRefillVolumeUserSpecified; }
            set { ChangeAndNotify(ref _limeSlurryRefillVolumeUserSpecified, value, nameof(LimeSlurryRefillVolumeUserSpecified), CalcPropertiesStringArray); }
        }

        private double _rubberTubingLength;
        /// <summary>
        /// User specified
        /// </summary>
        public double RubberTubingLength
        {
            get { return _rubberTubingLength; }
            set { ChangeAndNotify(ref _rubberTubingLength, value, nameof(RubberTubingLength), CalcPropertiesStringArray); }
        }

        private decimal _rubberTubingUnitCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal RubberTubingUnitCost
        {
            get { return _rubberTubingUnitCost; }
            set { ChangeAndNotify(ref _rubberTubingUnitCost, value, nameof(RubberTubingUnitCost), CalcPropertiesStringArray); }
        }

        private decimal _limeSlurryAgitatorUnitCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal LimeSlurryAgitatorUnitCost
        {
            get { return _limeSlurryAgitatorUnitCost; }
            set { ChangeAndNotify(ref _limeSlurryAgitatorUnitCost, value, nameof(LimeSlurryAgitatorUnitCost), CalcPropertiesStringArray); }
        }

        private double _limeSlurryAgitatorPowerRequirement;
        /// <summary>
        /// User specified
        /// </summary>
        public double LimeSlurryAgitatorPowerRequirement
        {
            get { return _limeSlurryAgitatorPowerRequirement; }
            set { ChangeAndNotify(ref _limeSlurryAgitatorPowerRequirement, value, nameof(LimeSlurryAgitatorPowerRequirement), CalcPropertiesStringArray); }
        }

        private double _limeSlurryAgitatorHoursPerDay;
        /// <summary>
        /// User specified
        /// </summary>
        public double LimeSlurryAgitatorHoursPerDay
        {
            get { return _limeSlurryAgitatorHoursPerDay; }
            set { ChangeAndNotify(ref _limeSlurryAgitatorHoursPerDay, value, nameof(LimeSlurryAgitatorHoursPerDay), CalcPropertiesStringArray); }
        }

        private double _limeSlurryAgitatorDaysPerYear;
        /// <summary>
        /// User specified
        /// </summary>
        public double LimeSlurryAgitatorDaysPerYear
        {
            get { return _limeSlurryAgitatorDaysPerYear; }
            set { ChangeAndNotify(ref _limeSlurryAgitatorDaysPerYear, value, nameof(LimeSlurryAgitatorDaysPerYear), CalcPropertiesStringArray); }
        }

        private decimal _chemicalMeteringPumpUnitCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal ChemicalMeteringPumpUnitCost
        {
            get { return _chemicalMeteringPumpUnitCost; }
            set { ChangeAndNotify(ref _chemicalMeteringPumpUnitCost, value, nameof(ChemicalMeteringPumpUnitCost), CalcPropertiesStringArray); }
        }

        private double _chemicalMeteringPumpQuantity;
        /// <summary>
        /// User specified
        /// </summary>
        public double ChemicalMeteringPumpQuantity
        {
            get { return _chemicalMeteringPumpQuantity; }
            set { ChangeAndNotify(ref _chemicalMeteringPumpQuantity, value, nameof(ChemicalMeteringPumpQuantity), CalcPropertiesStringArray); }
        }

        private double _chemicalMeteringPumpPowerRequirement;
        /// <summary>
        /// User specified
        /// </summary>
        public double ChemicalMeteringPumpPowerRequirement
        {
            get { return _chemicalMeteringPumpPowerRequirement; }
            set { ChangeAndNotify(ref _chemicalMeteringPumpPowerRequirement, value, nameof(ChemicalMeteringPumpPowerRequirement), CalcPropertiesStringArray); }
        }

        private double _chemicalMeteringPumpHoursPerDay;
        /// <summary>
        /// User specified
        /// </summary>
        public double ChemicalMeteringPumpHoursPerDay
        {
            get { return _chemicalMeteringPumpHoursPerDay; }
            set { ChangeAndNotify(ref _chemicalMeteringPumpHoursPerDay, value, nameof(ChemicalMeteringPumpHoursPerDay), CalcPropertiesStringArray); }
        }

        private double _chemicalMeteringPumpDaysPerYear;
        /// <summary>
        /// User specified
        /// </summary>
        public double ChemicalMeteringPumpDaysPerYear
        {
            get { return _chemicalMeteringPumpDaysPerYear; }
            set { ChangeAndNotify(ref _chemicalMeteringPumpDaysPerYear, value, nameof(ChemicalMeteringPumpDaysPerYear), CalcPropertiesStringArray); }
        }

        private bool _isPHEquipment;
        /// <summary>
        /// User specified
        /// </summary>
        public bool IsPHEquipment
        {
            get { return _isPHEquipment; }
            set { ChangeAndNotify(ref _isPHEquipment, value, nameof(IsPHEquipment), CalcPropertiesStringArray); }
        }

        private decimal _pHControllerCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal PHControllerCost
        {
            get { return _pHControllerCost; }
            set { ChangeAndNotify(ref _pHControllerCost, value, nameof(PHControllerCost), CalcPropertiesStringArray); }
        }

        private decimal _pHProbeCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal PHProbeCost
        {
            get { return _pHProbeCost; }
            set { ChangeAndNotify(ref _pHProbeCost, value, nameof(PHProbeCost), CalcPropertiesStringArray); }
        }

        private bool _isCellularEquipment;
        /// <summary>
        /// User specified
        /// </summary>
        public bool IsCellularEquipment
        {
            get { return _isCellularEquipment; }
            set { ChangeAndNotify(ref _isCellularEquipment, value, nameof(IsCellularEquipment), CalcPropertiesStringArray); }
        }

        private decimal _cellularTelemetryCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal CellularTelemetryCost
        {
            get { return _cellularTelemetryCost; }
            set { ChangeAndNotify(ref _cellularTelemetryCost, value, nameof(CellularTelemetryCost), CalcPropertiesStringArray); }
        }

        private decimal _cellularMonthlyCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal CellularMonthlyCost
        {
            get { return _cellularMonthlyCost; }
            set { ChangeAndNotify(ref _cellularMonthlyCost, value, nameof(CellularMonthlyCost), CalcPropertiesStringArray); }
        }

        private bool _isSiliconeRubberHeatBlanket;
        /// <summary>
        /// User specified
        /// </summary>
        public bool IsSiliconeRubberHeatBlanket
        {
            get { return _isSiliconeRubberHeatBlanket; }
            set { ChangeAndNotify(ref _isSiliconeRubberHeatBlanket, value, nameof(IsSiliconeRubberHeatBlanket), CalcPropertiesStringArray); }
        }

        private decimal _siliconeRubberHeatBlanketUnitCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal SiliconeRubberHeatBlanketUnitCost
        {
            get { return _siliconeRubberHeatBlanketUnitCost; }
            set { ChangeAndNotify(ref _siliconeRubberHeatBlanketUnitCost, value, nameof(SiliconeRubberHeatBlanketUnitCost), CalcPropertiesStringArray); }
        }

        private double _siliconeRubberHeatBlanketQuantity;
        /// <summary>
        /// User specified
        /// </summary>
        public double SiliconeRubberHeatBlanketQuantity
        {
            get { return _siliconeRubberHeatBlanketQuantity; }
            set { ChangeAndNotify(ref _siliconeRubberHeatBlanketQuantity, value, nameof(SiliconeRubberHeatBlanketQuantity), CalcPropertiesStringArray); }
        }

        private double _siliconeRubberHeatBlanketPowerRequirement;
        /// <summary>
        /// User specified
        /// </summary>
        public double SiliconeRubberHeatBlanketPowerRequirement
        {
            get { return _siliconeRubberHeatBlanketPowerRequirement; }
            set { ChangeAndNotify(ref _siliconeRubberHeatBlanketPowerRequirement, value, nameof(SiliconeRubberHeatBlanketPowerRequirement), CalcPropertiesStringArray); }
        }

        private double _siliconeRubberHeatBlanketHoursPerDay;
        /// <summary>
        /// User specified
        /// </summary>
        public double SiliconeRubberHeatBlanketHoursPerDay
        {
            get { return _siliconeRubberHeatBlanketHoursPerDay; }
            set { ChangeAndNotify(ref _siliconeRubberHeatBlanketHoursPerDay, value, nameof(SiliconeRubberHeatBlanketHoursPerDay), CalcPropertiesStringArray); }
        }

        private double _siliconeRubberHeatBlanketMonthsPerYear;
        /// <summary>
        /// User specified
        /// </summary>
        public double SiliconeRubberHeatBlanketMonthsPerYear
        {
            get { return _siliconeRubberHeatBlanketMonthsPerYear; }
            set { ChangeAndNotify(ref _siliconeRubberHeatBlanketMonthsPerYear, value, nameof(SiliconeRubberHeatBlanketMonthsPerYear), CalcPropertiesStringArray); }
        }

        private bool _isTubingHeatTracing;
        /// <summary>
        /// User specified
        /// </summary>
        public bool IsTubingHeatTracing
        {
            get { return _isTubingHeatTracing; }
            set { ChangeAndNotify(ref _isTubingHeatTracing, value, nameof(IsTubingHeatTracing), CalcPropertiesStringArray); }
        }

        private decimal _tubingHeatTracingUnitCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal TubingHeatTracingUnitCost
        {
            get { return _tubingHeatTracingUnitCost; }
            set { ChangeAndNotify(ref _tubingHeatTracingUnitCost, value, nameof(TubingHeatTracingUnitCost), CalcPropertiesStringArray); }
        }

        private double _tubingHeatTracingLength;
        /// <summary>
        /// User specified
        /// </summary>
        public double TubingHeatTracingLength
        {
            get { return _tubingHeatTracingLength; }
            set { ChangeAndNotify(ref _tubingHeatTracingLength, value, nameof(TubingHeatTracingLength), CalcPropertiesStringArray); }
        }

        private double _tubingHeatTracingPowerRequirement;
        /// <summary>
        /// User specified
        /// </summary>
        public double TubingHeatTracingPowerRequirement
        {
            get { return _tubingHeatTracingPowerRequirement; }
            set { ChangeAndNotify(ref _tubingHeatTracingPowerRequirement, value, nameof(TubingHeatTracingPowerRequirement), CalcPropertiesStringArray); }
        }

        private double _tubingHeatTracingHoursPerDay;
        /// <summary>
        /// User specified
        /// </summary>
        public double TubingHeatTracingHoursPerDay
        {
            get { return _tubingHeatTracingHoursPerDay; }
            set { ChangeAndNotify(ref _tubingHeatTracingHoursPerDay, value, nameof(TubingHeatTracingHoursPerDay), CalcPropertiesStringArray); }
        }

        private double _tubingHeatTracingMonthsPerYear;
        /// <summary>
        /// User specified
        /// </summary>
        public double TubingHeatTracingMonthsPerYear
        {
            get { return _tubingHeatTracingMonthsPerYear; }
            set { ChangeAndNotify(ref _tubingHeatTracingMonthsPerYear, value, nameof(TubingHeatTracingMonthsPerYear), CalcPropertiesStringArray); }
        }

        /// <summary>
        ///  Radio button binding with enumeration for sizing methods
        /// </summary>
        public enum RadioButtonsLimeSlurryTankFoundationAreaOptionsEnum
        {
            OptionEstimate,
            OptionUserSpecified,
        }

        private RadioButtonsLimeSlurryTankFoundationAreaOptionsEnum _limeSlurryTankFoundationAreaOptionsProperty;
        /// <summary>
        ///  User specified 
        /// </summary>
        public RadioButtonsLimeSlurryTankFoundationAreaOptionsEnum LimeSlurryTankFoundationAreaOptionsProperty
        {
            get { return _limeSlurryTankFoundationAreaOptionsProperty; }
            set { ChangeAndNotify(ref _limeSlurryTankFoundationAreaOptionsProperty, value, nameof(LimeSlurryTankFoundationAreaOptionsProperty), CalcPropertiesStringArray); }
        }

        private bool _isLimeSlurryTankFoundation;
        /// <summary>
        /// User specified
        /// </summary>
        public bool IsLimeSlurryTankFoundation
        {
            get { return _isLimeSlurryTankFoundation; }
            set { ChangeAndNotify(ref _isLimeSlurryTankFoundation, value, nameof(IsLimeSlurryTankFoundation), CalcPropertiesStringArray); }
        }

        private double _limeSlurryTankFoundationConcreteSlabThickness;
        /// <summary>
        /// User specified
        /// </summary>
        public double LimeSlurryTankFoundationConcreteSlabThickness
        {
            get { return _limeSlurryTankFoundationConcreteSlabThickness; }
            set { ChangeAndNotify(ref _limeSlurryTankFoundationConcreteSlabThickness, value, nameof(LimeSlurryTankFoundationConcreteSlabThickness), CalcPropertiesStringArray); }
        }

        private decimal _limeSlurryTankFoundationConcreteMaterialAndPlacementCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal LimeSlurryTankFoundationConcreteMaterialAndPlacementCost
        {
            get { return _limeSlurryTankFoundationConcreteMaterialAndPlacementCost; }
            set { ChangeAndNotify(ref _limeSlurryTankFoundationConcreteMaterialAndPlacementCost, value, nameof(LimeSlurryTankFoundationConcreteMaterialAndPlacementCost), CalcPropertiesStringArray); }
        }

        private double _limeSlurryTankHeight;
        /// <summary>
        /// User specified
        /// </summary>
        public double LimeSlurryTankHeight
        {
            get { return _limeSlurryTankHeight; }
            set { ChangeAndNotify(ref _limeSlurryTankHeight, value, nameof(LimeSlurryTankHeight), CalcPropertiesStringArray); }
        }

        private double _limeSlurryTankFoundationAreaUserSpecified;
        /// <summary>
        /// User specified
        /// </summary>
        public double LimeSlurryTankFoundationAreaUserSpecified
        {
            get { return _limeSlurryTankFoundationAreaUserSpecified; }
            set { ChangeAndNotify(ref _limeSlurryTankFoundationAreaUserSpecified, value, nameof(LimeSlurryTankFoundationAreaUserSpecified), CalcPropertiesStringArray); }
        }

        private bool _isPumpHousing;
        /// <summary>
        /// User specified
        /// </summary>
        public bool IsPumpHousing
        {
            get { return _isPumpHousing; }
            set { ChangeAndNotify(ref _isPumpHousing, value, nameof(IsPumpHousing), CalcPropertiesStringArray); }
        }

        private decimal _pumpHousingCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal PumpHousingCost
        {
            get { return _pumpHousingCost; }
            set { ChangeAndNotify(ref _pumpHousingCost, value, nameof(PumpHousingCost), CalcPropertiesStringArray); }
        }
        #endregion

        #region Properties - Other Capital Items

        private string _OtherCapitalItemDescription1;
        /// <summary>
        /// User specified
        /// </summary>
        public string OtherCapitalItemDescription1
        {
            get { return _OtherCapitalItemDescription1; }
            set { ChangeAndNotify(ref _OtherCapitalItemDescription1, value, nameof(OtherCapitalItemDescription1), CalcPropertiesStringArray); }
        }

        private double _OtherCapitalItemQuantity1;
        /// <summary>
        /// User specified
        /// </summary>
        public double OtherCapitalItemQuantity1
        {
            get { return _OtherCapitalItemQuantity1; }
            set { ChangeAndNotify(ref _OtherCapitalItemQuantity1, value, nameof(OtherCapitalItemQuantity1), CalcPropertiesStringArray); }
        }

        private decimal _OtherCapitalItemUnitCost1;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal OtherCapitalItemUnitCost1
        {
            get { return _OtherCapitalItemUnitCost1; }
            set { ChangeAndNotify(ref _OtherCapitalItemUnitCost1, value, nameof(OtherCapitalItemUnitCost1), CalcPropertiesStringArray); }
        }

        private string _OtherCapitalItemDescription2;
        /// <summary>
        /// User specified
        /// </summary>
        public string OtherCapitalItemDescription2
        {
            get { return _OtherCapitalItemDescription2; }
            set { ChangeAndNotify(ref _OtherCapitalItemDescription2, value, nameof(OtherCapitalItemDescription2), CalcPropertiesStringArray); }
        }

        private double _OtherCapitalItemQuantity2;
        /// <summary>
        /// User specified
        /// </summary>
        public double OtherCapitalItemQuantity2
        {
            get { return _OtherCapitalItemQuantity2; }
            set { ChangeAndNotify(ref _OtherCapitalItemQuantity2, value, nameof(OtherCapitalItemQuantity2), CalcPropertiesStringArray); }
        }

        private decimal _OtherCapitalItemUnitCost2;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal OtherCapitalItemUnitCost2
        {
            get { return _OtherCapitalItemUnitCost2; }
            set { ChangeAndNotify(ref _OtherCapitalItemUnitCost2, value, nameof(OtherCapitalItemUnitCost2), CalcPropertiesStringArray); }
        }

        private string _OtherCapitalItemDescription3;
        /// <summary>
        /// User specified
        /// </summary>
        public string OtherCapitalItemDescription3
        {
            get { return _OtherCapitalItemDescription3; }
            set { ChangeAndNotify(ref _OtherCapitalItemDescription3, value, nameof(OtherCapitalItemDescription3), CalcPropertiesStringArray); }
        }

        private double _OtherCapitalItemQuantity3;
        /// <summary>
        /// User specified
        /// </summary>
        public double OtherCapitalItemQuantity3
        {
            get { return _OtherCapitalItemQuantity3; }
            set { ChangeAndNotify(ref _OtherCapitalItemQuantity3, value, nameof(OtherCapitalItemQuantity3), CalcPropertiesStringArray); }
        }

        private decimal _OtherCapitalItemUnitCost3;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal OtherCapitalItemUnitCost3
        {
            get { return _OtherCapitalItemUnitCost3; }
            set { ChangeAndNotify(ref _OtherCapitalItemUnitCost3, value, nameof(OtherCapitalItemUnitCost3), CalcPropertiesStringArray); }
        }

        private string _OtherCapitalItemDescription4;
        /// <summary>
        /// User specified
        /// </summary>
        public string OtherCapitalItemDescription4
        {
            get { return _OtherCapitalItemDescription4; }
            set { ChangeAndNotify(ref _OtherCapitalItemDescription4, value, nameof(OtherCapitalItemDescription4), CalcPropertiesStringArray); }
        }

        private double _OtherCapitalItemQuantity4;
        /// <summary>
        /// User specified
        /// </summary>
        public double OtherCapitalItemQuantity4
        {
            get { return _OtherCapitalItemQuantity4; }
            set { ChangeAndNotify(ref _OtherCapitalItemQuantity4, value, nameof(OtherCapitalItemQuantity4), CalcPropertiesStringArray); }
        }

        private decimal _OtherCapitalItemUnitCost4;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal OtherCapitalItemUnitCost4
        {
            get { return _OtherCapitalItemUnitCost4; }
            set { ChangeAndNotify(ref _OtherCapitalItemUnitCost4, value, nameof(OtherCapitalItemUnitCost4), CalcPropertiesStringArray); }
        }

        private string _OtherCapitalItemDescription5;
        /// <summary>
        /// User specified
        /// </summary>
        public string OtherCapitalItemDescription5
        {
            get { return _OtherCapitalItemDescription5; }
            set { ChangeAndNotify(ref _OtherCapitalItemDescription5, value, nameof(OtherCapitalItemDescription5), CalcPropertiesStringArray); }
        }

        private double _OtherCapitalItemQuantity5;
        /// <summary>
        /// User specified
        /// </summary>
        public double OtherCapitalItemQuantity5
        {
            get { return _OtherCapitalItemQuantity5; }
            set { ChangeAndNotify(ref _OtherCapitalItemQuantity5, value, nameof(OtherCapitalItemQuantity5), CalcPropertiesStringArray); }
        }

        private decimal _OtherCapitalItemUnitCost5;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal OtherCapitalItemUnitCost5
        {
            get { return _OtherCapitalItemUnitCost5; }
            set { ChangeAndNotify(ref _OtherCapitalItemUnitCost5, value, nameof(OtherCapitalItemUnitCost5), CalcPropertiesStringArray); }
        }

        #endregion

        #region Properties - Other Annual Items

        private string _OtherAnnualItemDescription1;
        /// <summary>
        /// User specified
        /// </summary>
        public string OtherAnnualItemDescription1
        {
            get { return _OtherAnnualItemDescription1; }
            set { ChangeAndNotify(ref _OtherAnnualItemDescription1, value, nameof(OtherAnnualItemDescription1), CalcPropertiesStringArray); }
        }

        private double _OtherAnnualItemQuantity1;
        /// <summary>
        /// User specified
        /// </summary>
        public double OtherAnnualItemQuantity1
        {
            get { return _OtherAnnualItemQuantity1; }
            set { ChangeAndNotify(ref _OtherAnnualItemQuantity1, value, nameof(OtherAnnualItemQuantity1), CalcPropertiesStringArray); }
        }

        private decimal _OtherAnnualItemUnitCost1;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal OtherAnnualItemUnitCost1
        {
            get { return _OtherAnnualItemUnitCost1; }
            set { ChangeAndNotify(ref _OtherAnnualItemUnitCost1, value, nameof(OtherAnnualItemUnitCost1), CalcPropertiesStringArray); }
        }

        private string _OtherAnnualItemDescription2;
        /// <summary>
        /// User specified
        /// </summary>
        public string OtherAnnualItemDescription2
        {
            get { return _OtherAnnualItemDescription2; }
            set { ChangeAndNotify(ref _OtherAnnualItemDescription2, value, nameof(OtherAnnualItemDescription2), CalcPropertiesStringArray); }
        }

        private double _OtherAnnualItemQuantity2;
        /// <summary>
        /// User specified
        /// </summary>
        public double OtherAnnualItemQuantity2
        {
            get { return _OtherAnnualItemQuantity2; }
            set { ChangeAndNotify(ref _OtherAnnualItemQuantity2, value, nameof(OtherAnnualItemQuantity2), CalcPropertiesStringArray); }
        }

        private decimal _OtherAnnualItemUnitCost2;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal OtherAnnualItemUnitCost2
        {
            get { return _OtherAnnualItemUnitCost2; }
            set { ChangeAndNotify(ref _OtherAnnualItemUnitCost2, value, nameof(OtherAnnualItemUnitCost2), CalcPropertiesStringArray); }
        }

        private string _OtherAnnualItemDescription3;
        /// <summary>
        /// User specified
        /// </summary>
        public string OtherAnnualItemDescription3
        {
            get { return _OtherAnnualItemDescription3; }
            set { ChangeAndNotify(ref _OtherAnnualItemDescription3, value, nameof(OtherAnnualItemDescription3), CalcPropertiesStringArray); }
        }

        private double _OtherAnnualItemQuantity3;
        /// <summary>
        /// User specified
        /// </summary>
        public double OtherAnnualItemQuantity3
        {
            get { return _OtherAnnualItemQuantity3; }
            set { ChangeAndNotify(ref _OtherAnnualItemQuantity3, value, nameof(OtherAnnualItemQuantity3), CalcPropertiesStringArray); }
        }

        private decimal _OtherAnnualItemUnitCost3;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal OtherAnnualItemUnitCost3
        {
            get { return _OtherAnnualItemUnitCost3; }
            set { ChangeAndNotify(ref _OtherAnnualItemUnitCost3, value, nameof(OtherAnnualItemUnitCost3), CalcPropertiesStringArray); }
        }

        private string _OtherAnnualItemDescription4;
        /// <summary>
        /// User specified
        /// </summary>
        public string OtherAnnualItemDescription4
        {
            get { return _OtherAnnualItemDescription4; }
            set { ChangeAndNotify(ref _OtherAnnualItemDescription4, value, nameof(OtherAnnualItemDescription4), CalcPropertiesStringArray); }
        }

        private double _OtherAnnualItemQuantity4;
        /// <summary>
        /// User specified
        /// </summary>
        public double OtherAnnualItemQuantity4
        {
            get { return _OtherAnnualItemQuantity4; }
            set { ChangeAndNotify(ref _OtherAnnualItemQuantity4, value, nameof(OtherAnnualItemQuantity4), CalcPropertiesStringArray); }
        }

        private decimal _OtherAnnualItemUnitCost4;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal OtherAnnualItemUnitCost4
        {
            get { return _OtherAnnualItemUnitCost4; }
            set { ChangeAndNotify(ref _OtherAnnualItemUnitCost4, value, nameof(OtherAnnualItemUnitCost4), CalcPropertiesStringArray); }
        }

        private string _OtherAnnualItemDescription5;
        /// <summary>
        /// User specified
        /// </summary>
        public string OtherAnnualItemDescription5
        {
            get { return _OtherAnnualItemDescription5; }
            set { ChangeAndNotify(ref _OtherAnnualItemDescription5, value, nameof(OtherAnnualItemDescription5), CalcPropertiesStringArray); }
        }

        private double _OtherAnnualItemQuantity5;
        /// <summary>
        /// User specified
        /// </summary>
        public double OtherAnnualItemQuantity5
        {
            get { return _OtherAnnualItemQuantity5; }
            set { ChangeAndNotify(ref _OtherAnnualItemQuantity5, value, nameof(OtherAnnualItemQuantity5), CalcPropertiesStringArray); }
        }

        private decimal _OtherAnnualItemUnitCost5;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal OtherAnnualItemUnitCost5
        {
            get { return _OtherAnnualItemUnitCost5; }
            set { ChangeAndNotify(ref _OtherAnnualItemUnitCost5, value, nameof(OtherAnnualItemUnitCost5), CalcPropertiesStringArray); }
        }

        #endregion

        #region Properties - Lime Slurry Refill Volume

        private double _calcLimeSlurryRefillVolume;
        /// <summary>
        /// User specified
        /// </summary>
        public double CalcLimeSlurryRefillVolume
        {
            get
            {
                switch (LimeSlurryRefillVolumeOptionsProperty)
                {
                    case RadioButtonsLimeSlurryRefillVolumeOptionsEnum.OptionSemiTruck:
                        _calcLimeSlurryRefillVolume = LimeSlurryRefillVolumeSemiTruck;
                        break;
                    case RadioButtonsLimeSlurryRefillVolumeOptionsEnum.OptionStraightTruck:
                        _calcLimeSlurryRefillVolume = LimeSlurryRefillVolumeStraightTruck;
                        break;
                    case RadioButtonsLimeSlurryRefillVolumeOptionsEnum.OptionUserSpecified:
                        _calcLimeSlurryRefillVolume = LimeSlurryRefillVolumeUserSpecified;
                        break;
                    default:
                        break;
                }
                return _calcLimeSlurryRefillVolume;
            }
            set { ChangeAndNotify(ref _calcLimeSlurryRefillVolume, value, nameof(CalcLimeSlurryRefillVolume), CalcPropertiesStringArray); }
        }
        #endregion

        #region Properties - Sizing Summary: Lime Slurry Density and Specific Gravity

        private double _calcLimeSlurryDensity;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLimeSlurryDensity
        {
            get { return LimeSlurryCalculations.CalcLimeSlurryDensity(LimeSlurryPercentage); }
            set { ChangeAndNotify(ref _calcLimeSlurryDensity, value); }
        }

        private double _calcLimeSlurrySpecificGravity;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLimeSlurrySpecificGravity
        {
            get { return LimeSlurryCalculations.CalcLimeSlurrySpecificGravity(CalcLimeSlurryDensity); }
            set { ChangeAndNotify(ref _calcLimeSlurrySpecificGravity, value); }
        }

        #endregion

        #region Properties - Sizing Summary: Stoichiometric       

        private double _calcLimeSlurryFeedRateStoichiometric;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLimeSlurryFeedRateStoichiometric
        {
            get { return LimeSlurryCalculations.CalcLimeSlurryFeedRateStoichiometric(CalcLimeSlurryDailyConsumptionGallonsStoichiometric); }
            set { ChangeAndNotify(ref _calcLimeSlurryFeedRateStoichiometric, value); }
        }

        private double _calcLimeSlurryDailyConsumptionDryTonsStoichiometric;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLimeSlurryDailyConsumptionDryTonsStoichiometric
        {
            get { return LimeSlurryCalculations.CalcLimeSlurryDailyConsumptionDryTonsStoichiometric(DesignFlow, HotAcidity, LimeSlurryPurity, LimeSlurryDissolutionEfficiency); }
            set { ChangeAndNotify(ref _calcLimeSlurryDailyConsumptionDryTonsStoichiometric, value); }
        }

        private double _calcLimeSlurryDailyConsumptionGallonsStoichiometric;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLimeSlurryDailyConsumptionGallonsStoichiometric
        {
            get { return LimeSlurryCalculations.CalcLimeSlurryDailyConsumptionGallonsStoichiometric(DesignFlow, HotAcidity, LimeSlurryPurity, LimeSlurryDissolutionEfficiency, CalcLimeSlurryDensity); }
            set { ChangeAndNotify(ref _calcLimeSlurryDailyConsumptionGallonsStoichiometric, value); }
        }

        private double _calcLimeSlurryAnnualConsumptionDryTonsStoichiometric;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLimeSlurryAnnualConsumptionDryTonsStoichiometric
        {
            get { return LimeSlurryCalculations.CalcLimeSlurryAnnualConsumptionDryTonsStoichiometric(CalcLimeSlurryDailyConsumptionDryTonsStoichiometric); }
            set { ChangeAndNotify(ref _calcLimeSlurryAnnualConsumptionDryTonsStoichiometric, value); }
        }

        private double _calcLimeSlurryAnnualConsumptionGallonsStoichiometric;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLimeSlurryAnnualConsumptionGallonsStoichiometric
        {
            get { return LimeSlurryCalculations.CalcLimeSlurryAnnualConsumptionGallonsStoichiometric(CalcLimeSlurryDailyConsumptionGallonsStoichiometric); }
            set { ChangeAndNotify(ref _calcLimeSlurryAnnualConsumptionGallonsStoichiometric, value); }
        }

        private double _calcLimeSlurryDaysOfTreatmentPerTruckLoadStoichiometric;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLimeSlurryDaysOfTreatmentPerTruckLoadStoichiometric
        {
            get { return LimeSlurryCalculations.CalcLimeSlurryDaysOfTreatmentPerTruckLoadStoichiometric(LimeSlurryTankVolume, CalcLimeSlurryRefillVolume, CalcLimeSlurryDailyConsumptionGallonsStoichiometric); }
            set { ChangeAndNotify(ref _calcLimeSlurryDaysOfTreatmentPerTruckLoadStoichiometric, value); }
        }

        private double _calcLimeSlurryDaysOfExcessSupplyStoichiometric;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLimeSlurryDaysOfExcessSupplyStoichiometric
        {
            get { return LimeSlurryCalculations.CalcLimeSlurryDaysOfExcessSupplyStoichiometric(LimeSlurryTankVolume, CalcLimeSlurryRefillVolume, CalcLimeSlurryDailyConsumptionGallonsStoichiometric); }
            set { ChangeAndNotify(ref _calcLimeSlurryDaysOfExcessSupplyStoichiometric, value); }
        }

        #endregion

        #region Properties - Sizing Summary: Titration

        private double _calcLimeSlurryFeedRateTitration;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLimeSlurryFeedRateTitration
        {
            get { return LimeSlurryCalculations.CalcLimeSlurryFeedRateTitration(TitrationQuantity, DesignFlow); }
            set { ChangeAndNotify(ref _calcLimeSlurryFeedRateTitration, value); }
        }

        private double _calcLimeSlurryDailyConsumptionDryTonsTitration;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLimeSlurryDailyConsumptionDryTonsTitration
        {
            get { return LimeSlurryCalculations.CalcLimeSlurryDailyConsumptionDryTonsTitration(CalcLimeSlurryDailyConsumptionGallonsTitration, CalcLimeSlurryDensity); }
            set { ChangeAndNotify(ref _calcLimeSlurryDailyConsumptionDryTonsTitration, value); }
        }

        private double _calcLimeSlurryDailyConsumptionGallonsTitration;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLimeSlurryDailyConsumptionGallonsTitration
        {
            get { return LimeSlurryCalculations.CalcLimeSlurryDailyConsumptionGallonsTitration(TitrationQuantity, DesignFlow); }
            set { ChangeAndNotify(ref _calcLimeSlurryDailyConsumptionGallonsTitration, value); }
        }

        private double _calcLimeSlurryAnnualConsumptionDryTonsTitration;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLimeSlurryAnnualConsumptionDryTonsTitration
        {
            get { return LimeSlurryCalculations.CalcLimeSlurryAnnualConsumptionDryTonsTitration(CalcLimeSlurryDailyConsumptionDryTonsTitration); }
            set { ChangeAndNotify(ref _calcLimeSlurryAnnualConsumptionDryTonsTitration, value); }
        }

        private double _calcLimeSlurryAnnualConsumptionGallonsTitration;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLimeSlurryAnnualConsumptionGallonsTitration
        {
            get { return LimeSlurryCalculations.CalcLimeSlurryAnnualConsumptionGallonsTitration(CalcLimeSlurryDailyConsumptionGallonsTitration); }
            set { ChangeAndNotify(ref _calcLimeSlurryAnnualConsumptionGallonsTitration, value); }
        }

        private double _calcLimeSlurryDaysOfTreatmentPerTruckLoadTitration;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLimeSlurryDaysOfTreatmentPerTruckLoadTitration
        {
            get { return LimeSlurryCalculations.CalcLimeSlurryDaysOfTreatmentPerTruckLoadTitration(LimeSlurryTankVolume, CalcLimeSlurryRefillVolume, CalcLimeSlurryDailyConsumptionGallonsTitration); }
            set { ChangeAndNotify(ref _calcLimeSlurryDaysOfTreatmentPerTruckLoadTitration, value); }
        }

        private double _calcLimeSlurryDaysOfExcessSupplyTitration;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLimeSlurryDaysOfExcessSupplyTitration
        {
            get { return LimeSlurryCalculations.CalcLimeSlurryDaysOfExcessSupplyTitration(LimeSlurryTankVolume, CalcLimeSlurryRefillVolume); }
            set { ChangeAndNotify(ref _calcLimeSlurryDaysOfExcessSupplyTitration, value); }
        }

        #endregion

        #region Properties - Sizing Summary: User Specified Dry Ton Lime Slurry Quantity

        private double _calcLimeSlurryFeedRateUserSpecified;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLimeSlurryFeedRateUserSpecified
        {
            get { return LimeSlurryCalculations.CalcLimeSlurryFeedRateUserSpecified(CalcLimeSlurryDailyConsumptionGallonsUserSpecified); }
            set { ChangeAndNotify(ref _calcLimeSlurryFeedRateUserSpecified, value); }
        }

        private double _calcLimeSlurryDailyConsumptionDryTonsUserSpecified;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLimeSlurryDailyConsumptionDryTonsUserSpecified
        {
            get { return LimeSlurryCalculations.CalcLimeSlurryDailyConsumptionDryTonsUserSpecified(CalcLimeSlurryAnnualConsumptionDryTonsUserSpecified); }
            set { ChangeAndNotify(ref _calcLimeSlurryDailyConsumptionDryTonsUserSpecified, value); }
        }

        private double _calcLimeSlurryDailyConsumptionGallonsUserSpecified;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLimeSlurryDailyConsumptionGallonsUserSpecified
        {
            get { return LimeSlurryCalculations.CalcLimeSlurryDailyConsumptionGallonsUserSpecified(CalcLimeSlurryAnnualConsumptionGallonsUserSpecified); }
            set { ChangeAndNotify(ref _calcLimeSlurryDailyConsumptionGallonsUserSpecified, value); }
        }

        private double _calcLimeSlurryAnnualConsumptionDryTonsUserSpecified;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLimeSlurryAnnualConsumptionDryTonsUserSpecified
        {
            get { return LimeSlurryCalculations.CalcLimeSlurryAnnualConsumptionDryTonsUserSpecified(LimeSlurryDryTonsUserSpecifiedQuantity); }
            set { ChangeAndNotify(ref _calcLimeSlurryAnnualConsumptionDryTonsUserSpecified, value); }
        }

        private double _calcLimeSlurryAnnualConsumptionGallonsUserSpecified;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLimeSlurryAnnualConsumptionGallonsUserSpecified
        {
            get { return LimeSlurryCalculations.CalcLimeSlurryAnnualConsumptionGallonsUserSpecified(CalcLimeSlurryAnnualConsumptionDryTonsUserSpecified, CalcLimeSlurryDensity); }
            set { ChangeAndNotify(ref _calcLimeSlurryAnnualConsumptionGallonsUserSpecified, value); }
        }

        private double _calcLimeSlurryDaysOfTreatmentPerTruckLoadUserSpecified;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLimeSlurryDaysOfTreatmentPerTruckLoadUserSpecified
        {
            get { return LimeSlurryCalculations.CalcLimeSlurryDaysOfTreatmentPerTruckLoadUserSpecified(LimeSlurryTankVolume, CalcLimeSlurryRefillVolume, CalcLimeSlurryDailyConsumptionGallonsUserSpecified); }
            set { ChangeAndNotify(ref _calcLimeSlurryDaysOfTreatmentPerTruckLoadUserSpecified, value); }
        }

        private double _calcLimeSlurryDaysOfExcessSupplyUserSpecified;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLimeSlurryDaysOfExcessSupplyUserSpecified
        {
            get { return LimeSlurryCalculations.CalcLimeSlurryDaysOfExcessSupplyUserSpecified(LimeSlurryTankVolume, CalcLimeSlurryRefillVolume); }
            set { ChangeAndNotify(ref _calcLimeSlurryDaysOfExcessSupplyUserSpecified, value); }
        }


        #endregion

        #region Properties - Sizing Summary: Lime Slurry Feed Rate

        private double _calcLimeSlurryFeedRate;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLimeSlurryFeedRate
        {
            get
            {
                switch (ChemicalConsumptionOptionsProperty)
                {
                    case RadioButtonsChemicalConsumptionOptionsEnum.OptionStoichiometric:
                        _calcLimeSlurryFeedRate = CalcLimeSlurryFeedRateStoichiometric;
                        break;
                    case RadioButtonsChemicalConsumptionOptionsEnum.OptionTitration:
                        _calcLimeSlurryFeedRate = CalcLimeSlurryFeedRateTitration;
                        break;
                    case RadioButtonsChemicalConsumptionOptionsEnum.OptionUserSpecifiedQuantity:
                        _calcLimeSlurryFeedRate = CalcLimeSlurryFeedRateUserSpecified;
                        break;
                    default:
                        break;
                }

                return _calcLimeSlurryFeedRate;
            }
            set { ChangeAndNotify(ref _calcLimeSlurryFeedRate, value); }
        }

        #endregion

        #region Properties - Sizing Summary: Daily Consumption and Annual Consumption

        private double _calcLimeSlurryDailyConsumptionDryTons;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLimeSlurryDailyConsumptionDryTons
        {
            get
            {
                switch (ChemicalConsumptionOptionsProperty)
                {
                    case RadioButtonsChemicalConsumptionOptionsEnum.OptionStoichiometric:
                        _calcLimeSlurryDailyConsumptionDryTons = CalcLimeSlurryDailyConsumptionDryTonsStoichiometric;
                        break;
                    case RadioButtonsChemicalConsumptionOptionsEnum.OptionTitration:
                        _calcLimeSlurryDailyConsumptionDryTons = CalcLimeSlurryDailyConsumptionDryTonsTitration;
                        break;
                    case RadioButtonsChemicalConsumptionOptionsEnum.OptionUserSpecifiedQuantity:
                        _calcLimeSlurryDailyConsumptionDryTons = CalcLimeSlurryDailyConsumptionDryTonsUserSpecified;
                        break;
                    default:
                        break;
                }

                return _calcLimeSlurryDailyConsumptionDryTons;
            }
            set { ChangeAndNotify(ref _calcLimeSlurryDailyConsumptionDryTons, value); }
        }

        private double _calcLimeSlurryDailyConsumptionGallons;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLimeSlurryDailyConsumptionGallons
        {
            get
            {
                switch (ChemicalConsumptionOptionsProperty)
                {
                    case RadioButtonsChemicalConsumptionOptionsEnum.OptionStoichiometric:
                        _calcLimeSlurryDailyConsumptionGallons = CalcLimeSlurryDailyConsumptionGallonsStoichiometric;
                        break;
                    case RadioButtonsChemicalConsumptionOptionsEnum.OptionTitration:
                        _calcLimeSlurryDailyConsumptionGallons = CalcLimeSlurryDailyConsumptionGallonsTitration;
                        break;
                    case RadioButtonsChemicalConsumptionOptionsEnum.OptionUserSpecifiedQuantity:
                        _calcLimeSlurryDailyConsumptionGallons = CalcLimeSlurryDailyConsumptionGallonsUserSpecified;
                        break;
                    default:
                        break;
                }

                return _calcLimeSlurryDailyConsumptionGallons;
            }
            set { ChangeAndNotify(ref _calcLimeSlurryDailyConsumptionGallons, value); }
        }

        private double _calcLimeSlurryAnnualConsumptionDryTons;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLimeSlurryAnnualConsumptionDryTons
        {
            get
            {
                switch (ChemicalConsumptionOptionsProperty)
                {
                    case RadioButtonsChemicalConsumptionOptionsEnum.OptionStoichiometric:
                        _calcLimeSlurryAnnualConsumptionDryTons = CalcLimeSlurryAnnualConsumptionDryTonsStoichiometric;
                        break;
                    case RadioButtonsChemicalConsumptionOptionsEnum.OptionTitration:
                        _calcLimeSlurryAnnualConsumptionDryTons = CalcLimeSlurryAnnualConsumptionDryTonsTitration;
                        break;
                    case RadioButtonsChemicalConsumptionOptionsEnum.OptionUserSpecifiedQuantity:
                        _calcLimeSlurryAnnualConsumptionDryTons = CalcLimeSlurryAnnualConsumptionDryTonsUserSpecified;
                        break;
                    default:
                        break;
                }

                return _calcLimeSlurryAnnualConsumptionDryTons;
            }
            set { ChangeAndNotify(ref _calcLimeSlurryAnnualConsumptionDryTons, value); }
        }

        private double _calcLimeSlurryAnnualConsumptionGallons;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLimeSlurryAnnualConsumptionGallons
        {
            get
            {
                switch (ChemicalConsumptionOptionsProperty)
                {
                    case RadioButtonsChemicalConsumptionOptionsEnum.OptionStoichiometric:
                        _calcLimeSlurryAnnualConsumptionGallons = CalcLimeSlurryAnnualConsumptionGallonsStoichiometric;
                        break;
                    case RadioButtonsChemicalConsumptionOptionsEnum.OptionTitration:
                        _calcLimeSlurryAnnualConsumptionGallons = CalcLimeSlurryAnnualConsumptionGallonsTitration;
                        break;
                    case RadioButtonsChemicalConsumptionOptionsEnum.OptionUserSpecifiedQuantity:
                        _calcLimeSlurryAnnualConsumptionGallons = CalcLimeSlurryAnnualConsumptionGallonsUserSpecified;
                        break;
                    default:
                        break;
                }

                return _calcLimeSlurryAnnualConsumptionGallons;
            }
            set { ChangeAndNotify(ref _calcLimeSlurryAnnualConsumptionGallons, value); }
        }
        #endregion

        #region Properties - Sizing Summary: Days of Treatment per Truck Load and Days of Excess Supply

        private double _calcLimeSlurryDaysOfTreatmentPerTruckLoad;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLimeSlurryDaysOfTreatmentPerTruckLoad
        {
            get
            {
                switch (ChemicalConsumptionOptionsProperty)
                {
                    case RadioButtonsChemicalConsumptionOptionsEnum.OptionStoichiometric:
                        _calcLimeSlurryDaysOfTreatmentPerTruckLoad = CalcLimeSlurryDaysOfTreatmentPerTruckLoadStoichiometric;
                        break;
                    case RadioButtonsChemicalConsumptionOptionsEnum.OptionTitration:
                        _calcLimeSlurryDaysOfTreatmentPerTruckLoad = CalcLimeSlurryDaysOfTreatmentPerTruckLoadTitration;
                        break;
                    case RadioButtonsChemicalConsumptionOptionsEnum.OptionUserSpecifiedQuantity:
                        _calcLimeSlurryDaysOfTreatmentPerTruckLoad = CalcLimeSlurryDaysOfTreatmentPerTruckLoadUserSpecified;
                        break;
                    default:
                        break;
                }

                return _calcLimeSlurryDaysOfTreatmentPerTruckLoad;
            }
            set { ChangeAndNotify(ref _calcLimeSlurryDaysOfTreatmentPerTruckLoad, value); }
        }

        private double _calcLimeSlurryDaysOfExcessSupply;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLimeSlurryDaysOfExcessSupply
        {
            get
            {
                switch (ChemicalConsumptionOptionsProperty)
                {
                    case RadioButtonsChemicalConsumptionOptionsEnum.OptionStoichiometric:
                        _calcLimeSlurryDaysOfExcessSupply = CalcLimeSlurryDaysOfExcessSupplyStoichiometric;
                        break;
                    case RadioButtonsChemicalConsumptionOptionsEnum.OptionTitration:
                        _calcLimeSlurryDaysOfExcessSupply = CalcLimeSlurryDaysOfExcessSupplyTitration;
                        break;
                    case RadioButtonsChemicalConsumptionOptionsEnum.OptionUserSpecifiedQuantity:
                        _calcLimeSlurryDaysOfExcessSupply = CalcLimeSlurryDaysOfExcessSupplyUserSpecified;
                        break;
                    default:
                        break;
                }

                return _calcLimeSlurryDaysOfExcessSupply;
            }
            set { ChangeAndNotify(ref _calcLimeSlurryDaysOfExcessSupply, value); }
        }

        #endregion

        #region Properties - Sizing Summary: Lime Slurry Tank Foundation Area and Conrete Volume

        private double _calcLimeSlurryTankFoundationArea;
        /// <summary>
        /// User specified
        /// </summary>
        public double CalcLimeSlurryTankFoundationArea
        {
            get
            {
                if (IsLimeSlurryTankFoundation)
                {
                    switch (LimeSlurryTankFoundationAreaOptionsProperty)
                    {
                        case RadioButtonsLimeSlurryTankFoundationAreaOptionsEnum.OptionEstimate:
                            _calcLimeSlurryTankFoundationArea = LimeSlurryCalculations.CalcLimeSlurryTankFoundationAreaEstimate(LimeSlurryTankVolume, LimeSlurryTankHeight);
                            break;
                        case RadioButtonsLimeSlurryTankFoundationAreaOptionsEnum.OptionUserSpecified:
                            _calcLimeSlurryTankFoundationArea = LimeSlurryTankFoundationAreaUserSpecified;
                            break;
                    }

                    return _calcLimeSlurryTankFoundationArea;
                }
                else
                {
                    return 0;
                }

            }
            set { ChangeAndNotify(ref _calcLimeSlurryTankFoundationArea, value, nameof(CalcLimeSlurryTankFoundationArea), CalcPropertiesStringArray); }
        }

        private double _foundationAreaData;
        /// <summary>
        /// Calculated
        /// </summary>
        public double FoundationAreaData
        {
            get { return CalcLimeSlurryTankFoundationArea; }
            set { ChangeAndNotify(ref _foundationAreaData, value, nameof(FoundationAreaData)); }
        }

        private double _calcFoundationAreaTimesDepth;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcFoundationAreaTimesDepth
        {
            get
            {
                _calcFoundationAreaTimesDepth = CalcLimeSlurryTankFoundationArea * LimeSlurryTankFoundationConcreteSlabThickness;

                return _calcFoundationAreaTimesDepth;
            }
            set { ChangeAndNotify(ref _calcFoundationAreaTimesDepth, value); }
        }

        private double _foundationAreaTimesDepthData;
        /// <summary>
        /// Calculated
        /// </summary>
        public double FoundationAreaTimesDepthData
        {
            get { return CalcFoundationAreaTimesDepth; }
            set { ChangeAndNotify(ref _foundationAreaTimesDepthData, value, nameof(FoundationAreaTimesDepthData)); }
        }

        private double _calcLimeSlurryTankFoundationConcreteVolume;
        /// <summary>
        /// User specified
        /// </summary>
        public double CalcLimeSlurryTankFoundationConcreteVolume
        {
            get
            {
                if (IsLimeSlurryTankFoundation)
                {
                    return LimeSlurryCalculations.CalcLimeSlurryTankFoundationConcreteVolume(CalcLimeSlurryTankFoundationArea, LimeSlurryTankFoundationConcreteSlabThickness);
                }
                else
                {
                    return 0;
                }
                
            }
            set { ChangeAndNotify(ref _calcLimeSlurryTankFoundationConcreteVolume, value, nameof(CalcLimeSlurryTankFoundationConcreteVolume), CalcPropertiesStringArray); }
        }


        #endregion

        #region Properties - Capital Costs

        
        private decimal _calcRubberTubingCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRubberTubingCost
        {
            get { return LimeSlurryCalculations.CalcRubberTubingCost(RubberTubingLength, RubberTubingUnitCost); }
            set { ChangeAndNotify(ref _calcRubberTubingCost, value); }

        }
        
        private decimal _calcChemicalMeteringPumpCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcChemicalMeteringPumpCost
        {
            get { return LimeSlurryCalculations.CalcChemicalMeteringPumpCost(ChemicalMeteringPumpQuantity, ChemicalMeteringPumpUnitCost); } 
            set { ChangeAndNotify(ref _calcChemicalMeteringPumpCost, value); }

        }

        private decimal _calcSiliconeRubberHeatBlanketCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcSiliconeRubberHeatBlanketCost
        {
            get
            {
                if (IsSiliconeRubberHeatBlanket)
                {
                    _calcSiliconeRubberHeatBlanketCost = LimeSlurryCalculations.CalcSiliconeRubberHeatBlanketCost(SiliconeRubberHeatBlanketQuantity, SiliconeRubberHeatBlanketUnitCost);
                }
                else
                {
                    _calcSiliconeRubberHeatBlanketCost = 0m;
                }
                return _calcSiliconeRubberHeatBlanketCost;
            }
            set { ChangeAndNotify(ref _calcSiliconeRubberHeatBlanketCost, value); }

        }

        private decimal _calcTubingHeatTracingCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcTubingHeatTracingCost
        {
            get
            {
                if (IsTubingHeatTracing)
                {
                    _calcTubingHeatTracingCost = LimeSlurryCalculations.CalcTubingHeatTracingCost(TubingHeatTracingLength, TubingHeatTracingUnitCost);
                }
                else
                {
                    _calcTubingHeatTracingCost = 0m;
                }
                return _calcTubingHeatTracingCost;
            }
            set { ChangeAndNotify(ref _calcTubingHeatTracingCost, value); }

        }

        private decimal _calcPHControllerCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcPHControllerCost
        {
            get
            {
                if (IsPHEquipment)
                {
                    _calcPHControllerCost = PHControllerCost;
                }
                else
                {
                    _calcPHControllerCost = 0m;
                }
                return _calcPHControllerCost;
            }
            set { ChangeAndNotify(ref _calcPHControllerCost, value); }
        }

        private decimal _calcPHProbeCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcPHProbeCost
        {
            get
            {
                if (IsPHEquipment)
                {
                    _calcPHProbeCost = PHProbeCost;
                }
                else
                {
                    _calcPHProbeCost = 0m;
                }
                return _calcPHProbeCost;
            }
            set { ChangeAndNotify(ref _calcPHProbeCost, value); }
        }

        private decimal _calcCellularTelemetryCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcCellularTelemetryCost
        {
            get
            {
                if (IsCellularEquipment) 
                {
                    _calcCellularTelemetryCost = CellularTelemetryCost;
                }
                else
                {
                    _calcCellularTelemetryCost = 0m;
                }
                return _calcCellularTelemetryCost;
            }
            set { ChangeAndNotify(ref _calcCellularTelemetryCost, value); }
        }

        private decimal _calcCellularMonthlyCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcCellularMonthlyCost
        {
            get
            {
                if (IsCellularEquipment)
                {
                    _calcCellularMonthlyCost = CellularMonthlyCost;
                }
                else
                {
                    _calcCellularMonthlyCost = 0m;
                }
                return _calcCellularMonthlyCost;
            }
            set { ChangeAndNotify(ref _calcCellularMonthlyCost, value); }
        }


        private decimal _calcStorageAndDispensingCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcStorageAndDispensingCost
        {
            get
            {
                return LimeSlurryCalculations.CalcStorageAndDispensingCost(ElectricServicePanelCost, LimeSlurryTankUnitCost, CalcRubberTubingCost,
                                                                           LimeSlurryAgitatorUnitCost,  CalcChemicalMeteringPumpCost, CalcSiliconeRubberHeatBlanketCost,
                                                                           CalcTubingHeatTracingCost,  CalcPHControllerCost,  CalcPHProbeCost,
                                                                           CalcCellularTelemetryCost,  CalcCellularMonthlyCost);
            }
            set { ChangeAndNotify(ref _calcStorageAndDispensingCost, value); }

        }

        private decimal _calcPumpHousingCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcPumpHousingCost
        {
            get
            {
                if (IsPumpHousing)
                {
                    _calcPumpHousingCost = PumpHousingCost;
                }
                else
                {
                    _calcPumpHousingCost = 0m;
                }
                return _calcPumpHousingCost;
            }
            set { ChangeAndNotify(ref _calcPumpHousingCost, value); }
        }

        private decimal _calcTankFoundationAndPumpHousingCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcTankFoundationAndPumpHousingCost
        {
            get { return LimeSlurryCalculations.CalcTankFoundationAndPumpHousingCost(CalcLimeSlurryTankFoundationConcreteVolume, LimeSlurryTankFoundationConcreteMaterialAndPlacementCost, CalcPumpHousingCost); }
            set { ChangeAndNotify(ref _calcTankFoundationAndPumpHousingCost, value); }

        }

        private decimal _calcOtherCapitalItemsCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcOtherCapitalItemsCost
        {
            get
            {
                return LimeSlurryCalculations.CalcOtherCapitalItemsCost(OtherCapitalItemQuantity1, OtherCapitalItemUnitCost1,
                                                                  OtherCapitalItemQuantity2, OtherCapitalItemUnitCost2,
                                                                  OtherCapitalItemQuantity3, OtherCapitalItemUnitCost3,
                                                                  OtherCapitalItemQuantity4, OtherCapitalItemUnitCost4,
                                                                  OtherCapitalItemQuantity5, OtherCapitalItemUnitCost5);
            }
            set { ChangeAndNotify(ref _calcOtherCapitalItemsCost, value); }

        }

        private decimal _calcCapitalCostSubTotal;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcCapitalCostSubTotal
        {
            get { return LimeSlurryCalculations.CalcCapitalCostSubTotal(CalcStorageAndDispensingCost, CalcTankFoundationAndPumpHousingCost, CalcOtherCapitalItemsCost); }
            set { ChangeAndNotify(ref _calcCapitalCostSubTotal, value); }

        }

        public enum RadioButtonsSystemInstallationOptionsEnum
        {
            OptionCostMultiplier,
            OptionUserSpecified,
        }

        private RadioButtonsSystemInstallationOptionsEnum _systemInstallCostOptionsProperty;
        /// <summary>
        ///  User specified 
        /// </summary>
        public RadioButtonsSystemInstallationOptionsEnum SystemInstallCostOptionsProperty
        {
            get { return _systemInstallCostOptionsProperty; }
            set { ChangeAndNotify(ref _systemInstallCostOptionsProperty, value, nameof(SystemInstallCostOptionsProperty), CalcPropertiesStringArray); }
        }

        private double _systemInstallCostMultiplier;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double SystemInstallCostMultiplier
        {
            get { return _systemInstallCostMultiplier; }
            set { ChangeAndNotify(ref _systemInstallCostMultiplier, value, nameof(SystemInstallCostMultiplier), CalcPropertiesStringArray); }
        }

        private decimal _calcSystemInstallCostMultiplier;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcSystemInstallCostMultiplier
        {
            get { return LimeSlurryCalculations.CalcSystemInstallCost(SystemInstallCostMultiplier, CalcCapitalCostSubTotal); }
            set { ChangeAndNotify(ref _calcSystemInstallCostMultiplier, value); }

        }

        private decimal _systemInstallCostUserSpecified;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal SystemInstallCostUserSpecified
        {
            get { return _systemInstallCostUserSpecified; }
            set { ChangeAndNotify(ref _systemInstallCostUserSpecified, value, nameof(SystemInstallCostUserSpecified), CalcPropertiesStringArray); }
        }

        private decimal _calcSystemInstallCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcSystemInstallCost
        {
            get
            {
                switch (SystemInstallCostOptionsProperty)
                {
                    case RadioButtonsSystemInstallationOptionsEnum.OptionCostMultiplier:
                        _calcSystemInstallCost = CalcSystemInstallCostMultiplier;
                        break;
                    case RadioButtonsSystemInstallationOptionsEnum.OptionUserSpecified:
                        _calcSystemInstallCost = SystemInstallCostUserSpecified;
                        break;
                    default:
                        break;
                }
                return _calcSystemInstallCost;
            }
            set { ChangeAndNotify(ref _calcSystemInstallCost, value); }
        }

        private decimal _calcCapitalCostTotal;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcCapitalCostTotal
        {
            get
            {
                _calcCapitalCostTotal = LimeSlurryCalculations.CalcCapitalCostTotal(CalcCapitalCostSubTotal, CalcSystemInstallCost);

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
        ///  Radio button binding with enumeration for chemical consumption
        /// </summary>
        public enum RadioButtonsAnnualCostChemicalOptionsEnum
        {
            OptionEstimate,
            OptionUserSpecified,
        }

        private RadioButtonsAnnualCostChemicalOptionsEnum _annualCostChemicalOptionsProperty;
        /// <summary>
        ///  User specified 
        /// </summary>
        public RadioButtonsAnnualCostChemicalOptionsEnum AnnualCostChemicalOptionsProperty
        {
            get { return _annualCostChemicalOptionsProperty; }
            set { ChangeAndNotify(ref _annualCostChemicalOptionsProperty, value, nameof(AnnualCostChemicalOptionsProperty), CalcPropertiesStringArray); }
        }

        private decimal _calcAnnualCostChemicalEstimated;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcAnnualCostChemicalEstimated
        {
            get { return LimeSlurryCalculations.CalcAnnualCostChemical(CalcLimeSlurryAnnualConsumptionDryTons, LimeSlurryUnitCost); }
            set { ChangeAndNotify(ref _calcAnnualCostChemicalEstimated, value); }

        }

        private decimal _annualCostChemicalUserSpecified;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal AnnualCostChemicalUserSpecified
        {
            get { return _annualCostChemicalUserSpecified; }
            set { ChangeAndNotify(ref _annualCostChemicalUserSpecified, value, nameof(AnnualCostChemicalUserSpecified), CalcPropertiesStringArray); }
        }

        private decimal _calcAnnualCostChemical;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcAnnualCostChemical
        {
            get
            {
                switch (AnnualCostChemicalOptionsProperty)
                {
                    case RadioButtonsAnnualCostChemicalOptionsEnum.OptionEstimate:
                        _calcAnnualCostChemical = CalcAnnualCostChemicalEstimated;
                        break;
                    case RadioButtonsAnnualCostChemicalOptionsEnum.OptionUserSpecified:
                        _calcAnnualCostChemical = AnnualCostChemicalUserSpecified;
                        break;
                    default:
                        break;
                }
                return _calcAnnualCostChemical;
            }
            set { ChangeAndNotify(ref _calcAnnualCostChemical, value); }
        }


        /// <summary>
        /// Radio button binding with enumeration for annual costs
        /// </summary>
        public enum RadioButtonsAnnualCostOperationAndMaintanenceOptionsEnum
        {
            OptionCostMultiplier, 
            OptionUserSpecified
        }

        private RadioButtonsAnnualCostOperationAndMaintanenceOptionsEnum _annualCostOperationAndMaintanenceOptionsProperty;
        /// <summary>
        ///  User specified 
        /// </summary>
        public RadioButtonsAnnualCostOperationAndMaintanenceOptionsEnum AnnualCostOperationAndMaintanenceOptionsProperty
        {
            get { return _annualCostOperationAndMaintanenceOptionsProperty; }
            set { ChangeAndNotify(ref _annualCostOperationAndMaintanenceOptionsProperty, value, nameof(AnnualCostOperationAndMaintanenceOptionsProperty), CalcPropertiesStringArray); }
        }

        private double _annualCostOperationAndMaintanenceMultiplier;
        /// <summary>
        /// User specified
        /// </summary>
        public double AnnualCostOperationAndMaintanenceMultiplier
        {
            get { return _annualCostOperationAndMaintanenceMultiplier; }
            set { ChangeAndNotify(ref _annualCostOperationAndMaintanenceMultiplier, value, nameof(AnnualCostOperationAndMaintanenceMultiplier), CalcPropertiesStringArray); }
        }

        private decimal _calcAnnualCostOperationAndMaintanenceMultiplier;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcAnnualCostOperationAndMaintanenceMultiplier
        {
            get { return LimeSlurryCalculations.CalcAnnualCostOperationAndMaintanence(AnnualCostOperationAndMaintanenceMultiplier, CalcCapitalCostTotal); }
            set { ChangeAndNotify(ref _calcAnnualCostOperationAndMaintanenceMultiplier, value); }

        }

        private decimal _annualCostOperationAndMaintanenceUserSpecified;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal AnnualCostOperationAndMaintanenceUserSpecified
        {
            get { return _annualCostOperationAndMaintanenceUserSpecified; }
            set { ChangeAndNotify(ref _annualCostOperationAndMaintanenceUserSpecified, value, nameof(AnnualCostOperationAndMaintanenceUserSpecified), CalcPropertiesStringArray); }
        }

        private decimal _calcAnnualCostOperationAndMaintanence;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcAnnualCostOperationAndMaintanence
        {
            get
            {
                switch (AnnualCostOperationAndMaintanenceOptionsProperty)
                {
                    case RadioButtonsAnnualCostOperationAndMaintanenceOptionsEnum.OptionCostMultiplier:
                        _calcAnnualCostOperationAndMaintanence = CalcAnnualCostOperationAndMaintanenceMultiplier;
                        break;
                    case RadioButtonsAnnualCostOperationAndMaintanenceOptionsEnum.OptionUserSpecified:
                        _calcAnnualCostOperationAndMaintanence = AnnualCostOperationAndMaintanenceUserSpecified;
                        break;
                    default:
                        break;
                }
                return _calcAnnualCostOperationAndMaintanence;
            }
            set { ChangeAndNotify(ref _calcAnnualCostOperationAndMaintanence, value); }
        }



        public enum RadioButtonsAnnualCostElectricOptionsEnum
        {
            OptionAnnualCostElectricEstimate,
            OptionAnnualCostElectricUserSpecified
        }

        private RadioButtonsAnnualCostElectricOptionsEnum _annualCostElectricOptionsProperty;
        /// <summary>
        ///  User specified 
        /// </summary>
        public RadioButtonsAnnualCostElectricOptionsEnum AnnualCostElectricOptionsProperty
        {
            get { return _annualCostElectricOptionsProperty; }
            set { ChangeAndNotify(ref _annualCostElectricOptionsProperty, value, nameof(AnnualCostElectricOptionsProperty), CalcPropertiesStringArray); }
        }

        private double _calcLimeSlurryAgitatorElectricAmount;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLimeSlurryAgitatorElectricAmount
        {
            get { return LimeSlurryCalculations.CalcLimeSlurryAgitatorElectricAmount(LimeSlurryAgitatorPowerRequirement, LimeSlurryAgitatorHoursPerDay, LimeSlurryAgitatorDaysPerYear); }
            set { ChangeAndNotify(ref _calcLimeSlurryAgitatorElectricAmount, value); }
        }

        private double _calcChemicalMeteringPumpElectricAmount;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcChemicalMeteringPumpElectricAmount
        {
            get { return LimeSlurryCalculations.CalcChemicalMeteringPumpElectricAmount(ChemicalMeteringPumpPowerRequirement, ChemicalMeteringPumpHoursPerDay, ChemicalMeteringPumpDaysPerYear); }
            set { ChangeAndNotify(ref _calcChemicalMeteringPumpElectricAmount, value); }
        }

        private double _calcSiliconeRubberHeatBlanketElectricAmount;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcSiliconeRubberHeatBlanketElectricAmount
        {
            get
            {
                if (IsSiliconeRubberHeatBlanket)
                {
                    _calcSiliconeRubberHeatBlanketElectricAmount = LimeSlurryCalculations.CalcSiliconeRubberHeatBlanketElectricAmount(SiliconeRubberHeatBlanketPowerRequirement, SiliconeRubberHeatBlanketQuantity, 
                                                                                                                                     SiliconeRubberHeatBlanketHoursPerDay, SiliconeRubberHeatBlanketMonthsPerYear);
                }
                else
                {
                    _calcSiliconeRubberHeatBlanketElectricAmount = 0;
                }
                return _calcSiliconeRubberHeatBlanketElectricAmount;
            }
            set { ChangeAndNotify(ref _calcSiliconeRubberHeatBlanketElectricAmount, value); }
        }

        private double _calcTubingHeatTracingElectricAmount;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcTubingHeatTracingElectricAmount
        {
            get
            {
                if (IsTubingHeatTracing)
                {
                    _calcTubingHeatTracingElectricAmount = LimeSlurryCalculations.CalcTubingHeatTracingElectricAmount(TubingHeatTracingPowerRequirement, TubingHeatTracingLength,
                                                                                                                      TubingHeatTracingHoursPerDay, TubingHeatTracingMonthsPerYear);
                }
                else
                {
                    _calcTubingHeatTracingElectricAmount = 0;
                }
                return _calcTubingHeatTracingElectricAmount;
            }
            set { ChangeAndNotify(ref _calcTubingHeatTracingElectricAmount, value); }
        }

        private decimal _calcAnnualCostElectricEstimated;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcAnnualCostElectricEstimated
        {
            get { return LimeSlurryCalculations.CalcAnnualCostElectric(CalcLimeSlurryAgitatorElectricAmount, CalcChemicalMeteringPumpElectricAmount, 
                                                                       CalcSiliconeRubberHeatBlanketElectricAmount, CalcTubingHeatTracingElectricAmount,                                                                     
                                                                       ElectricRateCost); }
            set { ChangeAndNotify(ref _calcAnnualCostElectricEstimated, value); }
        }

        private decimal _annualCostElectricUserSpecified;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal AnnualCostElectricUserSpecified
        {
            get { return _annualCostElectricUserSpecified; }
            set { ChangeAndNotify(ref _annualCostElectricUserSpecified, value, nameof(AnnualCostElectricUserSpecified), CalcPropertiesStringArray); }
        }

        private decimal _calcAnnualCostElectric;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcAnnualCostElectric
        {
            get
            {
                switch (AnnualCostElectricOptionsProperty)
                {
                    case RadioButtonsAnnualCostElectricOptionsEnum.OptionAnnualCostElectricEstimate:
                        _calcAnnualCostElectric = CalcAnnualCostElectricEstimated;
                        break;
                    case RadioButtonsAnnualCostElectricOptionsEnum.OptionAnnualCostElectricUserSpecified:
                        _calcAnnualCostElectric = AnnualCostElectricUserSpecified;
                        break;
                    default:
                        break;
                }
                return _calcAnnualCostElectric;
            }
            set { ChangeAndNotify(ref _calcAnnualCostElectric, value); }
        }

        private decimal _calcOtherAnnualItemsCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcOtherAnnualItemsCost
        {
            get
            {
                return LimeSlurryCalculations.CalcOtherAnnualItemsCost(OtherAnnualItemQuantity1, OtherAnnualItemUnitCost1,
                                                                        OtherAnnualItemQuantity2, OtherAnnualItemUnitCost2,
                                                                        OtherAnnualItemQuantity3, OtherAnnualItemUnitCost3,
                                                                        OtherAnnualItemQuantity4, OtherAnnualItemUnitCost4,
                                                                        OtherAnnualItemQuantity5, OtherAnnualItemUnitCost5);
            }
            set { ChangeAndNotify(ref _calcOtherAnnualItemsCost, value); }

        }

        private decimal _calcAnnualCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcAnnualCost
        {
            get
            {
                _calcAnnualCost = LimeSlurryCalculations.CalcAnnualCostTotal(CalcAnnualCostChemical, CalcAnnualCostOperationAndMaintanence, CalcAnnualCostElectric, CalcOtherAnnualItemsCost);

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
            set { ChangeAndNotify(ref _annualCostData, value, nameof(AnnualCostData)); }
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

        private double _recapitalizationCostLifeCycleTank;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostLifeCycleTank
        {
            get { return _recapitalizationCostLifeCycleTank; }
            set { ChangeAndNotify(ref _recapitalizationCostLifeCycleTank, value, nameof(RecapitalizationCostLifeCycleTank), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostLifeCycleAgitator;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostLifeCycleAgitator
        {
            get { return _recapitalizationCostLifeCycleAgitator; }
            set { ChangeAndNotify(ref _recapitalizationCostLifeCycleAgitator, value, nameof(RecapitalizationCostLifeCycleAgitator), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostLifeCycleChemicalMeteringPump;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostLifeCycleChemicalMeteringPump
        {
            get { return _recapitalizationCostLifeCycleChemicalMeteringPump; }
            set { ChangeAndNotify(ref _recapitalizationCostLifeCycleChemicalMeteringPump, value, nameof(RecapitalizationCostLifeCycleChemicalMeteringPump), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostLifeCyclePHController;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostLifeCyclePHController
        {
            get { return _recapitalizationCostLifeCyclePHController; }
            set { ChangeAndNotify(ref _recapitalizationCostLifeCyclePHController, value, nameof(RecapitalizationCostLifeCyclePHController), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostLifeCyclePHProbe;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostLifeCyclePHProbe
        {
            get { return _recapitalizationCostLifeCyclePHProbe; }
            set { ChangeAndNotify(ref _recapitalizationCostLifeCyclePHProbe, value, nameof(RecapitalizationCostLifeCyclePHProbe), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostLifeCycleHeatBlanket;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostLifeCycleHeatBlanket
        {
            get { return _recapitalizationCostLifeCycleHeatBlanket; }
            set { ChangeAndNotify(ref _recapitalizationCostLifeCycleHeatBlanket, value, nameof(RecapitalizationCostLifeCycleHeatBlanket), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostLifeCyclePumpHousing;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostLifeCyclePumpHousing
        {
            get { return _recapitalizationCostLifeCyclePumpHousing; }
            set { ChangeAndNotify(ref _recapitalizationCostLifeCyclePumpHousing, value, nameof(RecapitalizationCostLifeCyclePumpHousing), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostPercentReplacementTank;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostPercentReplacementTank
        {
            get { return _recapitalizationCostPercentReplacementTank; }
            set { ChangeAndNotify(ref _recapitalizationCostPercentReplacementTank, value, nameof(RecapitalizationCostPercentReplacementTank), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostPercentReplacementAgitator;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostPercentReplacementAgitator
        {
            get { return _recapitalizationCostPercentReplacementAgitator; }
            set { ChangeAndNotify(ref _recapitalizationCostPercentReplacementAgitator, value, nameof(RecapitalizationCostPercentReplacementAgitator), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostPercentReplacementChemicalMeteringPump;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostPercentReplacementChemicalMeteringPump
        {
            get { return _recapitalizationCostPercentReplacementChemicalMeteringPump; }
            set { ChangeAndNotify(ref _recapitalizationCostPercentReplacementChemicalMeteringPump, value, nameof(RecapitalizationCostPercentReplacementChemicalMeteringPump), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostPercentReplacementPHController;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostPercentReplacementPHController
        {
            get { return _recapitalizationCostPercentReplacementPHController; }
            set { ChangeAndNotify(ref _recapitalizationCostPercentReplacementPHController, value, nameof(RecapitalizationCostPercentReplacementPHController), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostPercentReplacementPHProbe;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostPercentReplacementPHProbe
        {
            get { return _recapitalizationCostPercentReplacementPHProbe; }
            set { ChangeAndNotify(ref _recapitalizationCostPercentReplacementPHProbe, value, nameof(RecapitalizationCostPercentReplacementPHProbe), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostPercentReplacementHeatBlanket;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostPercentReplacementHeatBlanket
        {
            get { return _recapitalizationCostPercentReplacementHeatBlanket; }
            set { ChangeAndNotify(ref _recapitalizationCostPercentReplacementHeatBlanket, value, nameof(RecapitalizationCostPercentReplacementHeatBlanket), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostPercentReplacementPumpHousing;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostPercentReplacementPumpHousing
        {
            get { return _recapitalizationCostPercentReplacementPumpHousing; }
            set { ChangeAndNotify(ref _recapitalizationCostPercentReplacementPumpHousing, value, nameof(RecapitalizationCostPercentReplacementPumpHousing), CalcPropertiesStringArray); }
        }

        private decimal _calcRapitalizationCostTank;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostTank
        {
            get
            {
                return LimeSlurryCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
                                                                        RecapitalizationCostInflationRate, RecapitalizationCostLifeCycleTank,
                                                                        LimeSlurryTankUnitCost, RecapitalizationCostPercentReplacementTank);
            }
            set { ChangeAndNotify(ref _calcRapitalizationCostTank, value); }
        }

        private decimal _calcRapitalizationCostAgitator;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostAgitator
        {
            get
            {
                return LimeSlurryCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
                                                                        RecapitalizationCostInflationRate, RecapitalizationCostLifeCycleAgitator,
                                                                        LimeSlurryAgitatorUnitCost, RecapitalizationCostPercentReplacementAgitator);
            }
            set { ChangeAndNotify(ref _calcRapitalizationCostAgitator, value); }
        }

        private decimal _calcRapitalizationCostChemicalMeteringPump;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostChemicalMeteringPump
        {
            get
            {
                return LimeSlurryCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
                                                                        RecapitalizationCostInflationRate, RecapitalizationCostLifeCycleChemicalMeteringPump,
                                                                        CalcChemicalMeteringPumpCost, RecapitalizationCostPercentReplacementChemicalMeteringPump);
            }
            set { ChangeAndNotify(ref _calcRapitalizationCostChemicalMeteringPump, value); }
        }

        private decimal _calcRapitalizationCostPHController;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostPHController
        {
            get
            {
                return LimeSlurryCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
                                                                        RecapitalizationCostInflationRate, RecapitalizationCostLifeCyclePHController,
                                                                        CalcPHControllerCost, RecapitalizationCostPercentReplacementPHController);
            }
            set { ChangeAndNotify(ref _calcRapitalizationCostPHController, value); }
        }

        private decimal _calcRapitalizationCostPHProbe;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostPHProbe
        {
            get
            {
                return LimeSlurryCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
                                                                        RecapitalizationCostInflationRate, RecapitalizationCostLifeCyclePHProbe,
                                                                        CalcPHProbeCost, RecapitalizationCostPercentReplacementPHProbe);
            }
            set { ChangeAndNotify(ref _calcRapitalizationCostPHProbe, value); }
        }

        private decimal _calcRapitalizationCostHeatBlanket;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostHeatBlanket
        {
            get
            {
                return LimeSlurryCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
                                                                       RecapitalizationCostInflationRate, RecapitalizationCostLifeCycleHeatBlanket,
                                                                       CalcSiliconeRubberHeatBlanketCost + CalcTubingHeatTracingCost, RecapitalizationCostPercentReplacementHeatBlanket);
            }
            set { ChangeAndNotify(ref _calcRapitalizationCostHeatBlanket, value); }
        }

        private decimal _calcRapitalizationCostPumpHousing;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostPumpHousing
        {
            get
            {
                return LimeSlurryCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
                                                                        RecapitalizationCostInflationRate, RecapitalizationCostLifeCyclePumpHousing,
                                                                        CalcPumpHousingCost, RecapitalizationCostPercentReplacementPumpHousing);
            }
            set { ChangeAndNotify(ref _calcRapitalizationCostPumpHousing, value); }
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
            get { return LimeSlurryCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn, 
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
                    case "Tank":
                        item.MaterialCostDefault = LimeSlurryTankUnitCost;
                        break;
                    case "Agitator":
                        item.MaterialCostDefault = LimeSlurryAgitatorUnitCost;
                        break;
                    case "ChemicalMeteringPump":
                        item.MaterialCostDefault = CalcChemicalMeteringPumpCost;
                        break;
                    case "PHController":
                        item.MaterialCostDefault = CalcPHControllerCost;
                        break;
                    case "PHProbe":
                        item.MaterialCostDefault = CalcPHProbeCost;
                        break;
                    case "HeatBlanket":
                        item.MaterialCostDefault = CalcSiliconeRubberHeatBlanketCost + CalcTubingHeatTracingCost;
                        break;
                    case "PumpHousing":
                        item.MaterialCostDefault = CalcPumpHousingCost;
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
                    case "Tank":
                        item.TotalCost = CalcRecapitalizationCostTank;
                        break;
                    case "Agitator":
                        item.TotalCost = CalcRecapitalizationCostAgitator;
                        break;
                    case "ChemicalMeteringPump":
                        item.TotalCost = CalcRecapitalizationCostChemicalMeteringPump;
                        break;
                    case "PHController":
                        item.TotalCost = CalcRecapitalizationCostPHController;
                        break;
                    case "PHProbe":
                        item.TotalCost = CalcRecapitalizationCostPHProbe;
                        break;
                    case "HeatBlanket":
                        item.TotalCost = CalcRecapitalizationCostHeatBlanket;
                        break;
                    case "PumpHousing":
                        item.TotalCost = CalcRecapitalizationCostPumpHousing;
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
                Name = "Tank",
                NameFixed = "Tank",
                LifeCycle = RecapitalizationCostLifeCycleTank,
                PercentReplacement = RecapitalizationCostPercentReplacementTank,
                MaterialCostDefault = LimeSlurryTankUnitCost,
                UseCustomCost = false,
                MaterialCostCustom = 0m,
                TotalCost = CalcRecapitalizationCostTank
            });
            RecapMaterials.Add(new RecapMaterial
            {
                IsSelected = true,
                Name = "Agitator",
                NameFixed = "Agitator",
                LifeCycle = RecapitalizationCostLifeCycleAgitator,
                PercentReplacement = RecapitalizationCostPercentReplacementAgitator,
                MaterialCostDefault = LimeSlurryAgitatorUnitCost,
                UseCustomCost = false,
                MaterialCostCustom = 0m,
                TotalCost = CalcRecapitalizationCostAgitator
            });
            RecapMaterials.Add(new RecapMaterial
            {
                IsSelected = true,
                Name = "Chemical Pump",
                NameFixed = "ChemicalMeteringPump",
                LifeCycle = RecapitalizationCostLifeCycleChemicalMeteringPump,
                PercentReplacement = RecapitalizationCostPercentReplacementChemicalMeteringPump,
                MaterialCostDefault = CalcChemicalMeteringPumpCost,
                UseCustomCost = false,
                MaterialCostCustom = 0m,
                TotalCost = CalcRecapitalizationCostChemicalMeteringPump
            });
            RecapMaterials.Add(new RecapMaterial
            {
                IsSelected = true,
                Name = "pH Controller",
                NameFixed = "PHController",
                LifeCycle = RecapitalizationCostLifeCyclePHController,
                PercentReplacement = RecapitalizationCostPercentReplacementPHController,
                MaterialCostDefault = CalcPHControllerCost,
                UseCustomCost = false,
                MaterialCostCustom = 0m,
                TotalCost = CalcRecapitalizationCostPHController
            });
            RecapMaterials.Add(new RecapMaterial
            {
                IsSelected = true,
                Name = "pH Probe",
                NameFixed = "PHProbe",
                LifeCycle = RecapitalizationCostLifeCyclePHProbe,
                PercentReplacement = RecapitalizationCostPercentReplacementPHProbe,
                MaterialCostDefault = CalcPHProbeCost,
                UseCustomCost = false,
                MaterialCostCustom = 0m,
                TotalCost = CalcRecapitalizationCostPHProbe
            });
            RecapMaterials.Add(new RecapMaterial
            {
                IsSelected = true,
                Name = "Heat Blanket/Tape",
                NameFixed = "HeatBlanket",
                LifeCycle = RecapitalizationCostLifeCycleHeatBlanket,
                PercentReplacement = RecapitalizationCostPercentReplacementHeatBlanket,
                MaterialCostDefault = CalcSiliconeRubberHeatBlanketCost + CalcTubingHeatTracingCost,
                UseCustomCost = false,
                MaterialCostCustom = 0m,
                TotalCost = CalcRecapitalizationCostHeatBlanket
            });
            RecapMaterials.Add(new RecapMaterial
            {
                IsSelected = true,
                Name = "Pump Housing",
                NameFixed = "PumpHousing",
                LifeCycle = RecapitalizationCostLifeCyclePumpHousing,
                PercentReplacement = RecapitalizationCostPercentReplacementPumpHousing,
                MaterialCostDefault = CalcPumpHousingCost,
                UseCustomCost = false,
                MaterialCostCustom = 0m,
                TotalCost = CalcRecapitalizationCostPumpHousing
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
            ((RecapMaterial)sender).TotalCost = LimeSlurryCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
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

        #region Commands

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
            var customDialog = new CustomDialog() { Title = "About Lime Slurry" };

            var customDialogViewModel = new CustomDialogViewModel(instance =>
            {
                _dialogCoordinator.HideMetroDialogAsync(this, customDialog);
            });
            customDialogViewModel.Message = Resources.infoLS;
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

        private ICommand _showMessageDialogCommandWaterQuality;
        public ICommand ShowMessageDialogCommandWaterQuality
        {
            get
            {
                return _showMessageDialogCommandWaterQuality ?? (this._showMessageDialogCommandWaterQuality = new SimpleCommand
                {
                    CanExecuteDelegate = x => true,
                    ExecuteDelegate = async x =>
                    {
                        string message = Resources.infoWaterQualityLS;
                        await _dialogCoordinator.ShowMessageAsync(this, "Water Quality", message);
                    }
                });
            }
        }

        private ICommand _showMessageDialogCommandChemicalSolution;
        public ICommand ShowMessageDialogCommandChemicalSolution
        {
            get
            {
                return _showMessageDialogCommandChemicalSolution ?? (this._showMessageDialogCommandChemicalSolution = new SimpleCommand
                {
                    CanExecuteDelegate = x => true,
                    ExecuteDelegate = async x =>
                    {
                        string message = Resources.infoChemicalSolutionLS;
                        await _dialogCoordinator.ShowMessageAsync(this, "Chemical Solution", message);
                    }
                });
            }
        }

        private ICommand _showMessageDialogCommandChemicalConsumption;
        public ICommand ShowMessageDialogCommandChemicalConsumption
        {
            get
            {
                return _showMessageDialogCommandChemicalConsumption ?? (this._showMessageDialogCommandChemicalConsumption = new SimpleCommand
                {
                    CanExecuteDelegate = x => true,
                    ExecuteDelegate = async x =>
                    {
                        string message = Resources.infoChemicalConsumptionLS;
                        await _dialogCoordinator.ShowMessageAsync(this, "Chemical Consumption", message);
                    }
                });
            }
        }

        private ICommand _showMessageDialogCommandEquipment;
        public ICommand ShowMessageDialogCommandEquipment
        {
            get
            {
                return _showMessageDialogCommandEquipment ?? (this._showMessageDialogCommandEquipment = new SimpleCommand
                {
                    CanExecuteDelegate = x => true,
                    ExecuteDelegate = async x =>
                    {
                        string message = Resources.infoEquipmentLS;
                        await _dialogCoordinator.ShowMessageAsync(this, "Equipment", message);
                    }
                });
            }
        }

        private ICommand _showMessageDialogCommandOtherCapitalItems;
        public ICommand ShowMessageDialogCommandOtherCapitalItems
        {
            get
            {
                return _showMessageDialogCommandOtherCapitalItems ?? (this._showMessageDialogCommandOtherCapitalItems = new SimpleCommand
                {
                    CanExecuteDelegate = x => true,
                    ExecuteDelegate = async x =>
                    {
                        string message = Resources.infoOtherItemsCapitalLS;
                        await _dialogCoordinator.ShowMessageAsync(this, "Other Capital Cost Items", message);
                    }
                });
            }
        }

        private ICommand _showMessageDialogCommandOtherAnnualItems;
        public ICommand ShowMessageDialogCommandOtherAnnualItems
        {
            get
            {
                return _showMessageDialogCommandOtherAnnualItems ?? (this._showMessageDialogCommandOtherAnnualItems = new SimpleCommand
                {
                    CanExecuteDelegate = x => true,
                    ExecuteDelegate = async x =>
                    {
                        string message = Resources.infoOtherItemsAnnualLS;
                        await _dialogCoordinator.ShowMessageAsync(this, "Other Annual Cost Items", message);
                    }
                });
            }
        }

        private ICommand _showMessageDialogCommandSizingSummary;
        public ICommand ShowMessageDialogCommandSizingSummary
        {
            get
            {
                return _showMessageDialogCommandSizingSummary ?? (this._showMessageDialogCommandSizingSummary = new SimpleCommand
                {
                    CanExecuteDelegate = x => true,
                    ExecuteDelegate = async x =>
                    {
                        string message = Resources.infoSizingSummaryLS;
                        await _dialogCoordinator.ShowMessageAsync(this, "Sizing Summary", message);
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
                        string message = Resources.infoCapitalCostLS;
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
                        string message = Resources.infoAnnualCostLS;
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
                        string message = Resources.infoRecapitalizationCostLS;
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

        public LimeSlurryViewModel(IDialogCoordinator dialogCoordinator)
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
            ModuleType = "Lime Slurry";
            ModuleName = "Untitled module";
            ModuleTreatmentType = "Active";
            ModuleId = random.Next(1000);

            IsError = false;
            IsMajorError = false;
            ErrorMessage = "";
            ErrorMessageShort = "";

            // Initialize check boxes
            IsPHEquipment = true;
            IsCellularEquipment = true;
            IsSiliconeRubberHeatBlanket = true;
            IsTubingHeatTracing = true;
            IsLimeSlurryTankFoundation = true;
            IsPumpHousing = true;
            
            // Initialize radio buttons
            ChemicalConsumptionOptionsProperty = RadioButtonsChemicalConsumptionOptionsEnum.OptionStoichiometric;
            LimeSlurryRefillVolumeOptionsProperty = RadioButtonsLimeSlurryRefillVolumeOptionsEnum.OptionSemiTruck;
            LimeSlurryTankFoundationAreaOptionsProperty = RadioButtonsLimeSlurryTankFoundationAreaOptionsEnum.OptionEstimate;
            SystemInstallCostOptionsProperty = RadioButtonsSystemInstallationOptionsEnum.OptionCostMultiplier;
            AnnualCostChemicalOptionsProperty = RadioButtonsAnnualCostChemicalOptionsEnum.OptionEstimate;
            AnnualCostOperationAndMaintanenceOptionsProperty = RadioButtonsAnnualCostOperationAndMaintanenceOptionsEnum.OptionCostMultiplier;
            AnnualCostElectricOptionsProperty = RadioButtonsAnnualCostElectricOptionsEnum.OptionAnnualCostElectricEstimate;

            // Dummy variables to force syncing
            Dummy = 0;

            // Read the xml data file and assign property values accordingly.
            OpenXmlAndAssignValues(@"..\..\Data\default-data-lime-slurry.xml");

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
