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

    public class PondsViewModel : PropertyChangedBase, IObserver<SharedData>
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

        //private string _ModuleName;
        ///// <summary>
        ///// User specified
        ///// </summary>
        //public string ModuleName
        //{
        //    get { return _ModuleName; }
        //    set { ChangeAndNotify(ref _ModuleName, value); }
        //}

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

        private double _typicalFlow;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double TypicalFlow
        {
            get { return _typicalFlow; }
            set { ChangeAndNotify(ref _typicalFlow, value, nameof(TypicalFlow), CalcPropertiesStringArray); }
        }

        private double _ferrousIron;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double FerrousIron
        {
            get { return _ferrousIron; }
            set { ChangeAndNotify(ref _ferrousIron, value, nameof(FerrousIron), CalcPropertiesStringArray); }
        }

        private double _ferricIron;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double FerricIron
        {
            get { return _ferricIron; }
            set { ChangeAndNotify(ref _ferricIron, value, nameof(FerricIron), CalcPropertiesStringArray); }
        }

        private double _aluminum;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double Aluminum
        {
            get { return _aluminum; }
            set { ChangeAndNotify(ref _aluminum, value, nameof(Aluminum), CalcPropertiesStringArray); }
        }

        private double _manganese;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double Manganese
        {
            get { return _manganese; }
            set { ChangeAndNotify(ref _manganese, value, nameof(Manganese), CalcPropertiesStringArray); }
        }

        #endregion

        #region Properties - Pond Design

        public enum RadioButtonsSizingMethodsOptionsEnum
        {
            OptionOxidationSettlingSludge,
            OptionDimensions
        }

        private RadioButtonsSizingMethodsOptionsEnum _sizingMethodsOptionsProperty;
        /// <summary>
        ///  User specified 
        /// </summary>
        public RadioButtonsSizingMethodsOptionsEnum SizingMethodsOptionsProperty
        {
            get { return _sizingMethodsOptionsProperty; }
            set { ChangeAndNotify(ref _sizingMethodsOptionsProperty, value, nameof(SizingMethodsOptionsProperty), CalcPropertiesStringArray); }
        }

        private double _freeboardTopLengthDimensions;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double FreeboardTopLengthDimensions
        {
            get { return _freeboardTopLengthDimensions; }
            set { ChangeAndNotify(ref _freeboardTopLengthDimensions, value, nameof(FreeboardTopLengthDimensions), CalcPropertiesStringArray); }
        }

        private double _freeboardTopWidthDimensions;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double FreeboardTopWidthDimensions
        {
            get { return _freeboardTopWidthDimensions; }
            set { ChangeAndNotify(ref _freeboardTopWidthDimensions, value, nameof(FreeboardTopWidthDimensions), CalcPropertiesStringArray); }
        }
        #endregion

        #region Properties - System Properties

        private double _PondInsideSlope;
        /// <summary>
        /// User specified
        /// </summary>
        public double PondInsideSlope
        {
            get { return _PondInsideSlope; }
            set { ChangeAndNotify(ref _PondInsideSlope, value, nameof(PondInsideSlope), CalcPropertiesStringArray); }
        }

        private double _bottomLengthToWidthRatio;
        /// <summary>
        /// User specified
        /// </summary>
        public double BottomLengthToWidthRatio
        {
            get { return _bottomLengthToWidthRatio; }
            set { ChangeAndNotify(ref _bottomLengthToWidthRatio, value, nameof(BottomLengthToWidthRatio), CalcPropertiesStringArray);}
        }

        private double _bottomLengthToWidthRatioBackup;
        /// <summary>
        /// A backup of the bottom length to width ratio
        /// </summary>
        public double BottomLengthToWidthRatioBackup
        {
            get { return _bottomLengthToWidthRatioBackup; }
            set { _bottomLengthToWidthRatioBackup = value; }
        }

        private decimal _excavationUnitCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal ExcavationUnitCost
        {
            get { return _excavationUnitCost; }
            set { ChangeAndNotify(ref _excavationUnitCost, value, nameof(ExcavationUnitCost), CalcPropertiesStringArray); }
        }

        #endregion

        #region Properties - Layer Materials: Freeboard

        private double _freeboardDepth;
        /// <summary>
        /// User specified
        /// </summary>
        public double FreeboardDepth
        {
            get { return _freeboardDepth; }
            set { ChangeAndNotify(ref _freeboardDepth, value, nameof(FreeboardDepth), CalcPropertiesStringArray); }
        }

        #endregion

        #region Properties - Layer Materials: Oxidation

        private bool _isOxidation;
        /// <summary>
        /// User specified
        /// </summary>
        public bool IsOxidation
        {
            get { return _isOxidation; }
            set { ChangeAndNotify(ref _isOxidation, value, nameof(IsOxidation), CalcPropertiesStringArray); }
        }

        private double _oxidationDepth;
        /// <summary>
        /// User specified
        /// </summary>
        public double OxidationDepth
        {
            get { return _oxidationDepth; }
            set { ChangeAndNotify(ref _oxidationDepth, value, nameof(OxidationDepth), CalcPropertiesStringArray); }
        }

        #endregion

        #region Properties - Layer Materials: Retention / Settling

        private double _settlingDepth;
        /// <summary>
        /// User specified
        /// </summary>
        public double SettlingDepth
        {
            get { return _settlingDepth; }
            set { ChangeAndNotify(ref _settlingDepth, value, nameof(SettlingDepth), CalcPropertiesStringArray); }
        }

        private double _settlingRetentionTime;
        /// <summary>
        /// User specified
        /// </summary>
        public double SettlingRetentionTime
        {
            get { return _settlingRetentionTime; }
            set { ChangeAndNotify(ref _settlingRetentionTime, value, nameof(SettlingRetentionTime), CalcPropertiesStringArray); }
        }

        #endregion

        #region Properties - Layer Materials: Sludge Capacity

        private bool _isSludge;
        /// <summary>
        /// User specified
        /// </summary>
        public bool IsSludge
        {
            get { return _isSludge; }
            set { ChangeAndNotify(ref _isSludge, value, nameof(IsSludge), CalcPropertiesStringArray); }
        }

        private double _sludgeDepth;
        /// <summary>
        /// User specified
        /// </summary>
        public double SludgeDepth
        {
            get { return _sludgeDepth; }
            set { ChangeAndNotify(ref _sludgeDepth, value, nameof(SludgeDepth), CalcPropertiesStringArray); }
        }

        #endregion

        #region Properties - Layer Materials: Pipe

        private bool _pipeOptionAmdtreat;
        /// <summary>
        ///  User specified 
        /// </summary>
        public bool PipeOptionAmdtreat
        {
            get { return _pipeOptionAmdtreat; }
            set { ChangeAndNotify(ref _pipeOptionAmdtreat, value, nameof(PipeOptionAmdtreat), CalcPropertiesStringArray); }
        }

        private bool _pipeOptionCustom;
        /// <summary>
        ///  User specified 
        /// </summary>
        public bool PipeOptionCustom
        {
            get { return _pipeOptionCustom; }
            set { ChangeAndNotify(ref _pipeOptionCustom, value, nameof(PipeOptionCustom), CalcPropertiesStringArray); }
        }

        private double _inOutPipeLength;
        /// <summary>
        /// User specified
        /// </summary>
        public double InOutPipeLength
        {
            get { return _inOutPipeLength; }
            set { ChangeAndNotify(ref _inOutPipeLength, value, nameof(InOutPipeLength), CalcPropertiesStringArray); }
        }

        private double _inOutPipeInstallRate;
        /// <summary>
        /// User specified
        /// </summary>
        public double InOutPipeInstallRate
        {
            get { return _inOutPipeInstallRate; }
            set { ChangeAndNotify(ref _inOutPipeInstallRate, value, nameof(InOutPipeInstallRate), CalcPropertiesStringArray); }
        }

        private decimal _inOutPipeUnitCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal InOutPipeUnitCost
        {
            get { return _inOutPipeUnitCost; }
            set { ChangeAndNotify(ref _inOutPipeUnitCost, value, nameof(InOutPipeUnitCost), CalcPropertiesStringArray); }
        }

        private decimal _laborRate;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal LaborRate
        {
            get { return _laborRate; }
            set { ChangeAndNotify(ref _laborRate, value, nameof(LaborRate), CalcPropertiesStringArray); }
        }

        private string _customPipeName1;
        /// <summary>
        /// User specified
        /// </summary>
        public string CustomPipeName1
        {
            get { return _customPipeName1; }
            set { ChangeAndNotify(ref _customPipeName1, value, nameof(CustomPipeName1), CalcPropertiesStringArray); }
        }

        private double _customPipeLength1;
        /// <summary>
        /// User specified
        /// </summary>
        public double CustomPipeLength1
        {
            get { return _customPipeLength1; }
            set { ChangeAndNotify(ref _customPipeLength1, value, nameof(CustomPipeLength1), CalcPropertiesStringArray); }
        }

        private decimal _customPipeUnitCost1;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal CustomPipeUnitCost1
        {
            get { return _customPipeUnitCost1; }
            set { ChangeAndNotify(ref _customPipeUnitCost1, value, nameof(CustomPipeUnitCost1), CalcPropertiesStringArray); }
        }

        private string _customPipeName2;
        /// <summary>
        /// User specified
        /// </summary>
        public string CustomPipeName2
        {
            get { return _customPipeName2; }
            set { ChangeAndNotify(ref _customPipeName2, value, nameof(CustomPipeName2), CalcPropertiesStringArray); }
        }

        private double _customPipeLength2;
        /// <summary>
        /// User specified
        /// </summary>
        public double CustomPipeLength2
        {
            get { return _customPipeLength2; }
            set { ChangeAndNotify(ref _customPipeLength2, value, nameof(CustomPipeLength2), CalcPropertiesStringArray); }
        }

        private decimal _customPipeUnitCost2;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal CustomPipeUnitCost2
        {
            get { return _customPipeUnitCost2; }
            set { ChangeAndNotify(ref _customPipeUnitCost2, value, nameof(CustomPipeUnitCost2), CalcPropertiesStringArray); }
        }

        private string _customPipeName3;
        /// <summary>
        /// User specified
        /// </summary>
        public string CustomPipeName3
        {
            get { return _customPipeName3; }
            set { ChangeAndNotify(ref _customPipeName3, value, nameof(CustomPipeName3), CalcPropertiesStringArray); }
        }

        private double _customPipeLength3;
        /// <summary>
        /// User specified
        /// </summary>
        public double CustomPipeLength3
        {
            get { return _customPipeLength3; }
            set { ChangeAndNotify(ref _customPipeLength3, value, nameof(CustomPipeLength3), CalcPropertiesStringArray); }
        }

        private decimal _customPipeUnitCost3;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal CustomPipeUnitCost3
        {
            get { return _customPipeUnitCost3; }
            set { ChangeAndNotify(ref _customPipeUnitCost3, value, nameof(CustomPipeUnitCost3), CalcPropertiesStringArray); }
        }
        #endregion

        #region Properties - Appurtenances / Other Items

        private double _valveQuantity;
        /// <summary>
        /// User specified
        /// </summary>
        public double ValveQuantity
        {
            get { return _valveQuantity; }
            set { ChangeAndNotify(ref _valveQuantity, value, nameof(ValveQuantity), CalcPropertiesStringArray); }
        }

        private decimal _valveUnitCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal ValveUnitCost
        {
            get { return _valveUnitCost; }
            set { ChangeAndNotify(ref _valveUnitCost, value, nameof(ValveUnitCost), CalcPropertiesStringArray); }
        }

        private double _pumpQuantity;
        /// <summary>
        /// User specified
        /// </summary>
        public double PumpQuantity
        {
            get { return _pumpQuantity; }
            set { ChangeAndNotify(ref _pumpQuantity, value, nameof(PumpQuantity), CalcPropertiesStringArray); }
        }

        private decimal _pumpUnitCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal PumpUnitCost
        {
            get { return _pumpUnitCost; }
            set { ChangeAndNotify(ref _pumpUnitCost, value, nameof(PumpUnitCost), CalcPropertiesStringArray); }
        }

        private double _intakeStructureQuantity;
        /// <summary>
        /// User specified
        /// </summary>
        public double IntakeStructureQuantity
        {
            get { return _intakeStructureQuantity; }
            set { ChangeAndNotify(ref _intakeStructureQuantity, value, nameof(IntakeStructureQuantity), CalcPropertiesStringArray); }
        }

        private decimal _intakeStructureUnitCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal IntakeStructureUnitCost
        {
            get { return _intakeStructureUnitCost; }
            set { ChangeAndNotify(ref _intakeStructureUnitCost, value, nameof(IntakeStructureUnitCost), CalcPropertiesStringArray); }
        }

        private double _flowDistributionStructureQuantity;
        /// <summary>
        /// User specified
        /// </summary>
        public double FlowDistributionStructureQuantity
        {
            get { return _flowDistributionStructureQuantity; }
            set { ChangeAndNotify(ref _flowDistributionStructureQuantity, value, nameof(FlowDistributionStructureQuantity), CalcPropertiesStringArray); }
        }

        private decimal _flowDistributionStructureUnitCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal FlowDistributionStructureUnitCost
        {
            get { return _flowDistributionStructureUnitCost; }
            set { ChangeAndNotify(ref _flowDistributionStructureUnitCost, value, nameof(FlowDistributionStructureUnitCost), CalcPropertiesStringArray); }
        }

        private double _waterLevelControlStructureQuantity;
        /// <summary>
        /// User specified
        /// </summary>
        public double WaterLevelControlStructureQuantity
        {
            get { return _waterLevelControlStructureQuantity; }
            set { ChangeAndNotify(ref _waterLevelControlStructureQuantity, value, nameof(WaterLevelControlStructureQuantity), CalcPropertiesStringArray); }
        }

        private decimal _waterLevelControlStructureUnitCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal WaterLevelControlStructureUnitCost
        {
            get { return _waterLevelControlStructureUnitCost; }
            set { ChangeAndNotify(ref _waterLevelControlStructureUnitCost, value, nameof(WaterLevelControlStructureUnitCost), CalcPropertiesStringArray); }
        }

        private double _outletProtectionStructureQuantity;
        /// <summary>
        /// User specified
        /// </summary>
        public double OutletProtectionStructureQuantity
        {
            get { return _outletProtectionStructureQuantity; }
            set { ChangeAndNotify(ref _outletProtectionStructureQuantity, value, nameof(OutletProtectionStructureQuantity), CalcPropertiesStringArray); }
        }

        private decimal _outletProtectionStructureUnitCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal OutletProtectionStructureUnitCost
        {
            get { return _outletProtectionStructureUnitCost; }
            set { ChangeAndNotify(ref _outletProtectionStructureUnitCost, value, nameof(OutletProtectionStructureUnitCost), CalcPropertiesStringArray); }
        }

        #endregion

        #region Properties - Baffle

        private bool _isBaffle;
        /// <summary>
        ///  User specified 
        /// </summary>
        public bool IsBaffle
        {
            get { return _isBaffle; }
            set { ChangeAndNotify(ref _isBaffle, value, nameof(IsBaffle), CalcPropertiesStringArray); }
        }

        public enum RadioButtonsBaffleOptionsEnum
        {
            OptionEstimate,
            OptionUserSpecified
        }

        private RadioButtonsBaffleOptionsEnum _baffleOptionsProperty;
        /// <summary>
        ///  User specified 
        /// </summary>
        public RadioButtonsBaffleOptionsEnum BaffleOptionsProperty
        {
            get { return _baffleOptionsProperty; }
            set { ChangeAndNotify(ref _baffleOptionsProperty, value, nameof(BaffleOptionsProperty), CalcPropertiesStringArray); }
        }

        private double _baffleQuantity;
        /// <summary>
        /// User specified
        /// </summary>
        public double BaffleQuantity
        {
            get { return _baffleQuantity; }
            set { ChangeAndNotify(ref _baffleQuantity, value, nameof(BaffleQuantity), CalcPropertiesStringArray); }
        }

        private decimal _baffleUnitCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal BaffleUnitCost
        {
            get { return _baffleUnitCost; }
            set { ChangeAndNotify(ref _baffleUnitCost, value, nameof(BaffleUnitCost), CalcPropertiesStringArray); }
        }

        private double _baffleLengthUserSpecified;
        /// <summary>
        /// User specified
        /// </summary>
        public double BaffleLengthUserSpecified
        {
            get { return _baffleLengthUserSpecified; }
            set { ChangeAndNotify(ref _baffleLengthUserSpecified, value, nameof(BaffleLengthUserSpecified), CalcPropertiesStringArray); }
        }
        #endregion

        #region Properties - Layer Materials: Liner

        public enum RadioButtonsLinerOptionsEnum
        {
            OptionNoLiner,
            OptionClayLiner,
            OptionSyntheticLiner,
            OptionGeosyntheticClayLiner
        }

        private RadioButtonsLinerOptionsEnum _linerCostOptionsProperty;
        /// <summary>
        ///  User specified 
        /// </summary>
        public RadioButtonsLinerOptionsEnum LinerOptionsProperty
        {
            get { return _linerCostOptionsProperty; }
            set { ChangeAndNotify(ref _linerCostOptionsProperty, value, nameof(LinerOptionsProperty), CalcPropertiesStringArray); }
        }

        private decimal _clayLinerUnitCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal ClayLinerUnitCost
        {
            get { return _clayLinerUnitCost; }
            set
            {
                ChangeAndNotify(ref _clayLinerUnitCost, value, nameof(ClayLinerUnitCost), CalcPropertiesStringArray);
            }
        }

        private double _clayLinerThickness;
        /// <summary>
        /// User specified
        /// </summary>
        public double ClayLinerThickness
        {
            get { return _clayLinerThickness; }
            set
            {
                ChangeAndNotify(ref _clayLinerThickness, value, nameof(ClayLinerThickness), CalcPropertiesStringArray);
            }
        }

        private decimal _syntheticLinerUnitCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal SyntheticLinerUnitCost
        {
            get { return _syntheticLinerUnitCost; }
            set
            {
                ChangeAndNotify(ref _syntheticLinerUnitCost, value, nameof(SyntheticLinerUnitCost), CalcPropertiesStringArray);
            }
        }

        private decimal _geosyntheticClayLinerUnitCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal GeosyntheticClayLinerUnitCost
        {
            get { return _geosyntheticClayLinerUnitCost; }
            set { ChangeAndNotify(ref _geosyntheticClayLinerUnitCost, value, nameof(GeosyntheticClayLinerUnitCost), CalcPropertiesStringArray); }
        }

        private double _geosyntheticClayLinerThickness;
        /// <summary>
        /// User specified
        /// </summary>
        public double GeosyntheticClayLinerThickness
        {
            get { return _geosyntheticClayLinerThickness; }
            set
            { ChangeAndNotify(ref _geosyntheticClayLinerThickness, value, nameof(GeosyntheticClayLinerThickness), CalcPropertiesStringArray); }
        }

        private double _geosyntheticClayLinerSoilCover;
        /// <summary>
        /// User specified
        /// </summary>
        public double GeosyntheticClayLinerSoilCover
        {
            get { return _geosyntheticClayLinerSoilCover; }
            set
            { ChangeAndNotify(ref _geosyntheticClayLinerSoilCover, value, nameof(GeosyntheticClayLinerSoilCover), CalcPropertiesStringArray); }
        }

        private decimal _geosyntheticClayLinerCoverUnitCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal GeosyntheticClayLinerCoverUnitCost
        {
            get { return _geosyntheticClayLinerCoverUnitCost; }
            set { ChangeAndNotify(ref _geosyntheticClayLinerCoverUnitCost, value, nameof(GeosyntheticClayLinerCoverUnitCost), CalcPropertiesStringArray); }
        }

        private bool _nonWovenGeotextileOption;
        /// <summary>
        ///  User specified 
        /// </summary>
        public bool NonWovenGeotextileOption
        {
            get { return _nonWovenGeotextileOption; }
            set { ChangeAndNotify(ref _nonWovenGeotextileOption, value, nameof(NonWovenGeotextileOption), CalcPropertiesStringArray); }
        }

        private decimal _nonWovenGeotextileUnitCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal NonWovenGeotextileUnitCost
        {
            get { return _nonWovenGeotextileUnitCost; }
            set { ChangeAndNotify(ref _nonWovenGeotextileUnitCost, value, nameof(NonWovenGeotextileUnitCost), CalcPropertiesStringArray); }
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

        #region Properties - Sludge Generation

        private double _calcite;
        /// <summary>
        /// User specified
        /// </summary>
        public double Calcite
        {
            get { return _calcite; }
            set { ChangeAndNotify(ref _calcite, value, nameof(Calcite), CalcPropertiesStringArray); }
        }

        private double _miscellaneousSolids;
        /// <summary>
        /// User specified
        /// </summary>
        public double MiscellaneousSolids
        {
            get { return _miscellaneousSolids; }
            set { ChangeAndNotify(ref _miscellaneousSolids, value, nameof(MiscellaneousSolids), CalcPropertiesStringArray); }
        }

        private double _miscellaneousSolidsDensity;
        /// <summary>
        /// User specified
        /// </summary>
        public double MiscellaneousSolidsDensity
        {
            get { return _miscellaneousSolidsDensity; }
            set { ChangeAndNotify(ref _miscellaneousSolidsDensity, value, nameof(MiscellaneousSolidsDensity), CalcPropertiesStringArray); }
        }

        private double _percentSolids;
        /// <summary>
        /// User specified
        /// </summary>
        public double PercentSolids
        {
            get { return _percentSolids; }
            set { ChangeAndNotify(ref _percentSolids, value, nameof(PercentSolids), CalcPropertiesStringArray); }
        }

        private double _percentDewateredSolids;
        /// <summary>
        /// User specified
        /// </summary>
        public double PercentDewateredSolids
        {
            get { return _percentDewateredSolids; }
            set { ChangeAndNotify(ref _percentDewateredSolids, value, nameof(PercentDewateredSolids), CalcPropertiesStringArray); }
        }
        #endregion

        #region Properties - Sludge Handling

        private bool _isMobilizationDemobilization;
        /// <summary>
        /// User specified
        /// </summary>
        public bool IsMobilizationDemobilization
        {
            get { return _isMobilizationDemobilization; }
            set { ChangeAndNotify(ref _isMobilizationDemobilization, value, nameof(IsMobilizationDemobilization), CalcPropertiesStringArray); }
        }

        private decimal _mobilizationDemobilizationCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal MobilizationDemobilizationCost
        {
            get { return _mobilizationDemobilizationCost; }
            set { ChangeAndNotify(ref _mobilizationDemobilizationCost, value, nameof(MobilizationDemobilizationCost), CalcPropertiesStringArray); }
        }

        private bool _isSludgeHandlingDisposal;
        /// <summary>
        /// User specified
        /// </summary>
        public bool IsSludgeHandlingDisposal
        {
            get { return _isSludgeHandlingDisposal; }
            set { ChangeAndNotify(ref _isSludgeHandlingDisposal, value, nameof(IsSludgeHandlingDisposal), CalcPropertiesStringArray); }
        }

        private decimal _sludgeHandlingDisposalUnitCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal SludgeHandlingDisposalUnitCost
        {
            get { return _sludgeHandlingDisposalUnitCost; }
            set { ChangeAndNotify(ref _sludgeHandlingDisposalUnitCost, value, nameof(SludgeHandlingDisposalUnitCost), CalcPropertiesStringArray); }
        }

        private bool _isSludgeRemovalVacuumTruck;
        /// <summary>
        /// User specified
        /// </summary>
        public bool IsSludgeRemovalVacuumTruck
        {
            get { return _isSludgeRemovalVacuumTruck; }
            set { ChangeAndNotify(ref _isSludgeRemovalVacuumTruck, value, nameof(IsSludgeRemovalVacuumTruck), CalcPropertiesStringArray); }
        }

        private double _sludgeHandlingVacuumTruckVolume;
        /// <summary>
        /// User specified
        /// </summary>
        public double SludgeHandlingVacuumTruckVolume
        {
            get { return _sludgeHandlingVacuumTruckVolume; }
            set { ChangeAndNotify(ref _sludgeHandlingVacuumTruckVolume, value, nameof(SludgeHandlingVacuumTruckVolume), CalcPropertiesStringArray); }
        }

        private double _sludgeHandlingVacuumTruckTimeToFill;
        /// <summary>
        /// User specified
        /// </summary>
        public double SludgeHandlingVacuumTruckTimeToFill
        {
            get { return _sludgeHandlingVacuumTruckTimeToFill; }
            set { ChangeAndNotify(ref _sludgeHandlingVacuumTruckTimeToFill, value, nameof(SludgeHandlingVacuumTruckTimeToFill), CalcPropertiesStringArray); }
        }

        private decimal _sludgeHandlingVacuumTruckUnitCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal SludgeHandlingVacuumTruckUnitCost
        {
            get { return _sludgeHandlingVacuumTruckUnitCost; }
            set { ChangeAndNotify(ref _sludgeHandlingVacuumTruckUnitCost, value, nameof(SludgeHandlingVacuumTruckUnitCost), CalcPropertiesStringArray); }
        }

        private bool _isSludgeRemovalPump;
        /// <summary>
        /// User specified
        /// </summary>
        public bool IsSludgeRemovalPump
        {
            get { return _isSludgeRemovalPump; }
            set { ChangeAndNotify(ref _isSludgeRemovalPump, value, nameof(IsSludgeRemovalPump), CalcPropertiesStringArray); }
        }

        private bool _isSludgeRemovalPumpRental;
        /// <summary>
        /// User specified
        /// </summary>
        public bool IsSludgeRemovalPumpRental
        {
            get { return _isSludgeRemovalPumpRental; }
            set { ChangeAndNotify(ref _isSludgeRemovalPumpRental, value, nameof(IsSludgeRemovalPumpRental), CalcPropertiesStringArray); }
        }

        private decimal _pumpRentalCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal PumpRentalCost
        {
            get { return _pumpRentalCost; }
            set { ChangeAndNotify(ref _pumpRentalCost, value, nameof(PumpRentalCost), CalcPropertiesStringArray); }
        }

        private double _pumpRate;
        /// <summary>
        /// User specified
        /// </summary>
        public double PumpRate
        {
            get { return _pumpRate; }
            set { ChangeAndNotify(ref _pumpRate, value, nameof(PumpRate), CalcPropertiesStringArray); }
        }

        public enum RadioButtonsPumpOptionsEnum
        {
            OptionElectric,
            OptionFuel
        }

        private RadioButtonsPumpOptionsEnum _pumpOptionsProperty;
        /// <summary>
        ///  User specified 
        /// </summary>
        public RadioButtonsPumpOptionsEnum PumpOptionsProperty
        {
            get { return _pumpOptionsProperty; }
            set { ChangeAndNotify(ref _pumpOptionsProperty, value, nameof(PumpOptionsProperty), CalcPropertiesStringArray); }
        }

        private double _powerRequirement;
        /// <summary>
        /// User specified
        /// </summary>
        public double PowerRequirement
        {
            get { return _powerRequirement; }
            set { ChangeAndNotify(ref _powerRequirement, value, nameof(PowerRequirement), CalcPropertiesStringArray); }
        }

        private decimal _electricRateUnitCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal ElectricRateUnitCost
        {
            get { return _electricRateUnitCost; }
            set { ChangeAndNotify(ref _electricRateUnitCost, value, nameof(ElectricRateUnitCost), CalcPropertiesStringArray); }
        }

        private double _fuelRate;
        /// <summary>
        /// User specified
        /// </summary>
        public double FuelRate
        {
            get { return _fuelRate; }
            set { ChangeAndNotify(ref _fuelRate, value, nameof(FuelRate), CalcPropertiesStringArray); }
        }

        private decimal _fuelUnitCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal FuelUnitCost
        {
            get { return _fuelUnitCost; }
            set { ChangeAndNotify(ref _fuelUnitCost, value, nameof(FuelUnitCost), CalcPropertiesStringArray); }
        }

        private bool _isMechanicalExcavation;
        /// <summary>
        /// User specified
        /// </summary>
        public bool IsMechanicalExcavation
        {
            get { return _isMechanicalExcavation; }
            set { ChangeAndNotify(ref _isMechanicalExcavation, value, nameof(IsMechanicalExcavation), CalcPropertiesStringArray); }
        }

        private decimal _mechanicalExcavationUnitCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal MechanicalExcavationUnitCost
        {
            get { return _mechanicalExcavationUnitCost; }
            set { ChangeAndNotify(ref _mechanicalExcavationUnitCost, value, nameof(MechanicalExcavationUnitCost), CalcPropertiesStringArray); }
        }


        #endregion

        #region Properties - Sludge Disposal

        private bool _isLandfillTippingFee;
        /// <summary>
        /// User specified
        /// </summary>
        public bool IsLandfillTippingFee
        {
            get { return _isLandfillTippingFee; }
            set { ChangeAndNotify(ref _isLandfillTippingFee, value, nameof(IsLandfillTippingFee), CalcPropertiesStringArray); }
        }

        private decimal _landfillTippingFeeUnitCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal LandfillTippingFeeUnitCost
        {
            get { return _landfillTippingFeeUnitCost; }
            set { ChangeAndNotify(ref _landfillTippingFeeUnitCost, value, nameof(LandfillTippingFeeUnitCost), CalcPropertiesStringArray); }
        }

        public enum RadioButtonsSludgeDisposalOptionsEnum
        {
            OptionTriaxle,
            OptionVacuumTruck,
            OptionBorehole,
            OptionGeotube
        }

        private RadioButtonsSludgeDisposalOptionsEnum _sludgeDisposalOptionsProperty;
        /// <summary>
        ///  User specified 
        /// </summary>
        public RadioButtonsSludgeDisposalOptionsEnum SludgeDisposalOptionsProperty
        {
            get { return _sludgeDisposalOptionsProperty; }
            set { ChangeAndNotify(ref _sludgeDisposalOptionsProperty, value, nameof(SludgeDisposalOptionsProperty), CalcPropertiesStringArray); }
        }

        private double _sludgeDisposalTriaxleTruckVolume;
        /// <summary>
        /// User specified
        /// </summary>
        public double SludgeDisposalTriaxleTruckVolume
        {
            get { return _sludgeDisposalTriaxleTruckVolume; }
            set { ChangeAndNotify(ref _sludgeDisposalTriaxleTruckVolume, value, nameof(SludgeDisposalTriaxleTruckVolume), CalcPropertiesStringArray); }
        }

        private double _sludgeDisposalTriaxleRoundtripDistance;
        /// <summary>
        /// User specified
        /// </summary>
        public double SludgeDisposalTriaxleRoundtripDistance
        {
            get { return _sludgeDisposalTriaxleRoundtripDistance; }
            set { ChangeAndNotify(ref _sludgeDisposalTriaxleRoundtripDistance, value, nameof(SludgeDisposalTriaxleRoundtripDistance), CalcPropertiesStringArray); }
        }

        private decimal _sludgeDisposalTriaxleTransportationUnitCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal SludgeDisposalTriaxleTransportationUnitCost
        {
            get { return _sludgeDisposalTriaxleTransportationUnitCost; }
            set { ChangeAndNotify(ref _sludgeDisposalTriaxleTransportationUnitCost, value, nameof(SludgeDisposalTriaxleTransportationUnitCost), CalcPropertiesStringArray); }
        }

        private double _sludgeDisposalVacuumTruckVolume;
        /// <summary>
        /// User specified
        /// </summary>
        public double SludgeDisposalVacuumTruckVolume
        {
            get { return _sludgeDisposalVacuumTruckVolume; }
            set { ChangeAndNotify(ref _sludgeDisposalVacuumTruckVolume, value, nameof(SludgeDisposalVacuumTruckVolume), CalcPropertiesStringArray); }
        }

        private double _sludgeDisposalVacuumTruckHoursPerTrip;
        /// <summary>
        /// User specified
        /// </summary>
        public double SludgeDisposalVacuumTruckHoursPerTrip
        {
            get { return _sludgeDisposalVacuumTruckHoursPerTrip; }
            set { ChangeAndNotify(ref _sludgeDisposalVacuumTruckHoursPerTrip, value, nameof(SludgeDisposalVacuumTruckHoursPerTrip), CalcPropertiesStringArray); }
        }

        private decimal _sludgeDisposalVacuumTruckUnitCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal SludgeDisposalVacuumTruckUnitCost
        {
            get { return _sludgeDisposalVacuumTruckUnitCost; }
            set { ChangeAndNotify(ref _sludgeDisposalVacuumTruckUnitCost, value, nameof(SludgeDisposalVacuumTruckUnitCost), CalcPropertiesStringArray); }
        }

        private double _sludgeDisposalBoreholeDepth;
        /// <summary>
        /// User specified
        /// </summary>
        public double SludgeDisposalBoreholeDepth
        {
            get { return _sludgeDisposalBoreholeDepth; }
            set { ChangeAndNotify(ref _sludgeDisposalBoreholeDepth, value, nameof(SludgeDisposalBoreholeDepth), CalcPropertiesStringArray); }
        }

        private decimal _sludgeDisposalBoreholeUnitCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal SludgeDisposalBoreholeUnitCost
        {
            get { return _sludgeDisposalBoreholeUnitCost; }
            set { ChangeAndNotify(ref _sludgeDisposalBoreholeUnitCost, value, nameof(SludgeDisposalBoreholeUnitCost), CalcPropertiesStringArray); }
        }

        private List<Geotube> _geotubes;
        /// <summary>
        /// Collection 
        /// </summary>
        public List<Geotube> Geotubes
        {
            get { return _geotubes; }

            set { ChangeAndNotify(ref _geotubes, value, nameof(Geotubes), CalcPropertiesStringArray); }
        }

        private string _geotubeName;
        /// <summary>
        /// User specified
        /// </summary>
        public string GeotubeName
        {
            get { return _geotubeName; }
            set { ChangeAndNotify(ref _geotubeName, value, nameof(GeotubeName), CalcPropertiesStringArray); }
        }

        private double _geotubeVolume;
        /// <summary>
        /// User specified
        /// </summary>
        public double GeotubeVolume
        {
            get { return _geotubeVolume; }
            set { ChangeAndNotify(ref _geotubeVolume, value, nameof(GeotubeVolume), CalcPropertiesStringArray); }
        }

        private decimal _geotubeUnitCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal GeotubeUnitCost
        {
            get { return _geotubeUnitCost; }
            set { ChangeAndNotify(ref _geotubeUnitCost, value, nameof(GeotubeUnitCost), CalcPropertiesStringArray); }
        }

        private string _geotubeName25;
        /// <summary>
        /// User specified
        /// </summary>
        public string GeotubeName25
        {
            get { return _geotubeName25; }
            set { ChangeAndNotify(ref _geotubeName25, value, nameof(GeotubeName25), CalcPropertiesStringArray); }
        }

        private double _geotubeVolume25;
        /// <summary>
        /// User specified
        /// </summary>
        public double GeotubeVolume25
        {
            get { return _geotubeVolume25; }
            set { ChangeAndNotify(ref _geotubeVolume25, value, nameof(GeotubeVolume25), CalcPropertiesStringArray); }
        }

        private decimal _geotubeUnitCost25;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal GeotubeUnitCost25
        {
            get { return _geotubeUnitCost25; }
            set { ChangeAndNotify(ref _geotubeUnitCost25, value, nameof(GeotubeUnitCost25), CalcPropertiesStringArray); }
        }

        private string _geotubeName85;
        /// <summary>
        /// User specified
        /// </summary>
        public string GeotubeName85
        {
            get { return _geotubeName85; }
            set { ChangeAndNotify(ref _geotubeName85, value, nameof(GeotubeName85), CalcPropertiesStringArray); }
        }

        private double _geotubeVolume85;
        /// <summary>
        /// User specified
        /// </summary>
        public double GeotubeVolume85
        {
            get { return _geotubeVolume85; }
            set { ChangeAndNotify(ref _geotubeVolume85, value, nameof(GeotubeVolume85), CalcPropertiesStringArray); }
        }

        private decimal _geotubeUnitCost85;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal GeotubeUnitCost85
        {
            get { return _geotubeUnitCost85; }
            set { ChangeAndNotify(ref _geotubeUnitCost85, value, nameof(GeotubeUnitCost85), CalcPropertiesStringArray); }
        }

        private string _geotubeName175;
        /// <summary>
        /// User specified
        /// </summary>
        public string GeotubeName175
        {
            get { return _geotubeName175; }
            set { ChangeAndNotify(ref _geotubeName175, value, nameof(GeotubeName175), CalcPropertiesStringArray); }
        }

        private double _geotubeVolume175;
        /// <summary>
        /// User specified
        /// </summary>
        public double GeotubeVolume175
        {
            get { return _geotubeVolume175; }
            set { ChangeAndNotify(ref _geotubeVolume175, value, nameof(GeotubeVolume175), CalcPropertiesStringArray); }
        }

        private decimal _geotubeUnitCost175;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal GeotubeUnitCost175
        {
            get { return _geotubeUnitCost175; }
            set { ChangeAndNotify(ref _geotubeUnitCost175, value, nameof(GeotubeUnitCost175), CalcPropertiesStringArray); }
        }

        private string _geotubeName310;
        /// <summary>
        /// User specified
        /// </summary>
        public string GeotubeName310
        {
            get { return _geotubeName310; }
            set { ChangeAndNotify(ref _geotubeName310, value, nameof(GeotubeName310), CalcPropertiesStringArray); }
        }

        private double _geotubeVolume310;
        /// <summary>
        /// User specified
        /// </summary>
        public double GeotubeVolume310
        {
            get { return _geotubeVolume310; }
            set { ChangeAndNotify(ref _geotubeVolume310, value, nameof(GeotubeVolume310), CalcPropertiesStringArray); }
        }

        private decimal _geotubeUnitCost310;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal GeotubeUnitCost310
        {
            get { return _geotubeUnitCost310; }
            set { ChangeAndNotify(ref _geotubeUnitCost310, value, nameof(GeotubeUnitCost310), CalcPropertiesStringArray); }
        }

        private string _geotubeName465;
        /// <summary>
        /// User specified
        /// </summary>
        public string GeotubeName465
        {
            get { return _geotubeName465; }
            set { ChangeAndNotify(ref _geotubeName465, value, nameof(GeotubeName465), CalcPropertiesStringArray); }
        }

        private double _geotubeVolume465;
        /// <summary>
        /// User specified
        /// </summary>
        public double GeotubeVolume465
        {
            get { return _geotubeVolume465; }
            set { ChangeAndNotify(ref _geotubeVolume465, value, nameof(GeotubeVolume465), CalcPropertiesStringArray); }
        }

        private decimal _geotubeUnitCost465;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal GeotubeUnitCost465
        {
            get { return _geotubeUnitCost465; }
            set { ChangeAndNotify(ref _geotubeUnitCost465, value, nameof(GeotubeUnitCost465), CalcPropertiesStringArray); }
        }

        private string _geotubeName950;
        /// <summary>
        /// User specified
        /// </summary>
        public string GeotubeName950
        {
            get { return _geotubeName950; }
            set { ChangeAndNotify(ref _geotubeName950, value, nameof(GeotubeName950), CalcPropertiesStringArray); }
        }

        private double _geotubeVolume950;
        /// <summary>
        /// User specified
        /// </summary>
        public double GeotubeVolume950
        {
            get { return _geotubeVolume950; }
            set { ChangeAndNotify(ref _geotubeVolume950, value, nameof(GeotubeVolume950), CalcPropertiesStringArray); }
        }

        private decimal _geotubeUnitCost950;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal GeotubeUnitCost950
        {
            get { return _geotubeUnitCost950; }
            set { ChangeAndNotify(ref _geotubeUnitCost950, value, nameof(GeotubeUnitCost950), CalcPropertiesStringArray); }
        }

        private string _geotubeName1185;
        /// <summary>
        /// User specified
        /// </summary>
        public string GeotubeName1185
        {
            get { return _geotubeName1185; }
            set { ChangeAndNotify(ref _geotubeName1185, value, nameof(GeotubeName1185), CalcPropertiesStringArray); }
        }

        private double _geotubeVolume1185;
        /// <summary>
        /// User specified
        /// </summary>
        public double GeotubeVolume1185
        {
            get { return _geotubeVolume1185; }
            set { ChangeAndNotify(ref _geotubeVolume1185, value, nameof(GeotubeVolume1185), CalcPropertiesStringArray); }
        }

        private decimal _geotubeUnitCost1185;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal GeotubeUnitCost1185
        {
            get { return _geotubeUnitCost1185; }
            set { ChangeAndNotify(ref _geotubeUnitCost1185, value, nameof(GeotubeUnitCost1185), CalcPropertiesStringArray); }
        }

        private double _geotubeTruckVolume;
        /// <summary>
        /// User specified
        /// </summary>
        public double GeotubeTruckVolume
        {
            get { return _geotubeTruckVolume; }
            set { ChangeAndNotify(ref _geotubeTruckVolume, value, nameof(GeotubeTruckVolume), CalcPropertiesStringArray); }
        }

        private double _geotubeTimeToLoadTruck;
        /// <summary>
        /// User specified
        /// </summary>
        public double GeotubeTimeToLoadTruck
        {
            get { return _geotubeTimeToLoadTruck; }
            set { ChangeAndNotify(ref _geotubeTimeToLoadTruck, value, nameof(GeotubeTimeToLoadTruck), CalcPropertiesStringArray); }
        }

        private decimal _geotubeExcavatorHourlyRate;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal GeotubeExcavatorHourlyRate
        {
            get { return _geotubeExcavatorHourlyRate; }
            set { ChangeAndNotify(ref _geotubeExcavatorHourlyRate, value, nameof(GeotubeExcavatorHourlyRate), CalcPropertiesStringArray); }
        }

        private double _geotubeTruckRoundtripDistance;
        /// <summary>
        /// User specified
        /// </summary>
        public double GeotubeTruckRoundtripDistance
        {
            get { return _geotubeTruckRoundtripDistance; }
            set { ChangeAndNotify(ref _geotubeTruckRoundtripDistance, value, nameof(GeotubeTruckRoundtripDistance), CalcPropertiesStringArray); }
        }

        private decimal _geotubeTruckTransportationUnitCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal GeotubeTruckTransportationUnitCost
        {
            get { return _geotubeTruckTransportationUnitCost; }
            set { ChangeAndNotify(ref _geotubeTruckTransportationUnitCost, value, nameof(GeotubeTruckTransportationUnitCost), CalcPropertiesStringArray); }
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

        #region Properties - Sizing Summary: Excavation Volume, Clear and Grub Area, Total Pond Depth, Bottom Width to Length Ratio for Dimensions option

        private double _calcExcavationVolume;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcExcavationVolume
        {
            get { return PondsCalculations.CalcExcavationVolume(CalcSludgeVolume, CalcSettlingVolume, CalcOxidationVolume, CalcLinerVolume); }
            set { ChangeAndNotify(ref _calcExcavationVolume, value); }
        }

        private double _calcClearAndGrubArea;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcClearAndGrubArea
        {
            get { return PondsCalculations.CalcClearAndGrubArea(CalcFreeboardTopLength, CalcFreeboardTopWidth, FreeboardDepth); }
            set { ChangeAndNotify(ref _calcClearAndGrubArea, value); }
        }

        private double _clearAndGrubAreaData;
        /// <summary>
        ///  Data to be shared with main user interface
        /// </summary>
        public double ClearAndGrubAreaData
        {
            get { return CalcClearAndGrubArea; }
            set { ChangeAndNotify(ref _clearAndGrubAreaData, value, nameof(ClearAndGrubAreaData)); }
        }

        private double _calcPondDepth;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcPondDepth
        {
            get
            {
                double oxidationDepth;
                double sludgeDepth;
                if (IsOxidation)
                {
                    oxidationDepth = OxidationDepth;
                }
                else
                {
                    oxidationDepth = 0;
                }
                if (IsSludge)
                {
                    sludgeDepth = SludgeDepth;
                }
                else
                {
                    sludgeDepth = 0;
                }
                return PondsCalculations.CalcPondDepth(FreeboardDepth, oxidationDepth, SettlingDepth, sludgeDepth);
            }
            set { ChangeAndNotify(ref _calcPondDepth, value); }

        }

        private double _calcBottomLengthToWidthRatioBasedOnDimensions;
        /// <summary>
        /// User specified
        /// </summary>
        public double CalcBottomLengthToWidthRatioBasedOnDimensions
        {
            get { return PondsCalculations.CalcBottomLengthToWidthRatioBasedOnDimensions(CalcSettlingBottomLength, CalcSettlingBottomWidth); }
            set { ChangeAndNotify(ref _calcBottomLengthToWidthRatioBasedOnDimensions, value, nameof(CalcBottomLengthToWidthRatioBasedOnDimensions), CalcPropertiesStringArray); }
        }

        //private double _calcBottomLengthToWidthRatio;
        ///// <summary>
        ///// User specified
        ///// </summary>
        //public double CalcBottomLengthToWidthRatio
        //{
        //    get
        //    {
        //        switch (SizingMethodsOptionsProperty)
        //        {
        //            case RadioButtonsSizingMethodsOptionsEnum.OptionOxidationSettlingSludge:
        //                _calcBottomLengthToWidthRatio = BottomLengthToWidthRatio;
        //                break;
        //            case RadioButtonsSizingMethodsOptionsEnum.OptionDimensions:
        //                _calcBottomLengthToWidthRatio = CalcBottomLengthToWidthRatioBasedOnDimensions;
        //                break;
        //            default:
        //                break;
        //        }
        //        AssignBottomLengthToWidthRatio(_calcBottomLengthToWidthRatio);
        //        return _calcBottomLengthToWidthRatio;
        //    }
        //    set { ChangeAndNotify(ref _calcBottomLengthToWidthRatio, value, nameof(CalcBottomLengthToWidthRatio), CalcPropertiesStringArray); }
        //}
        #endregion

        #region Properties - Sizing Summary: Freeboard

        private double _calcFreeboardTopLength;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcFreeboardTopLength
        {
            get
            {
                switch (SizingMethodsOptionsProperty)
                {
                    case RadioButtonsSizingMethodsOptionsEnum.OptionOxidationSettlingSludge:
                        _calcFreeboardTopLength = PondsCalculations.CalcFreeboardTopLengthBasedOnRetention(CalcOxidationTopLength, FreeboardDepth, PondInsideSlope);
                        break;
                    case RadioButtonsSizingMethodsOptionsEnum.OptionDimensions:
                        _calcFreeboardTopLength = FreeboardTopLengthDimensions;
                        break;
                    default:
                        break;
                }
                return _calcFreeboardTopLength;
            }
            set { ChangeAndNotify(ref _calcFreeboardTopLength, value); }
        }

        private double _calcFreeboardTopWidth;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcFreeboardTopWidth
        {
            get
            {
                switch (SizingMethodsOptionsProperty)
                {
                    case RadioButtonsSizingMethodsOptionsEnum.OptionOxidationSettlingSludge:
                        _calcFreeboardTopWidth = PondsCalculations.CalcFreeboardTopWidthBasedOnRetention(CalcOxidationTopWidth, FreeboardDepth, PondInsideSlope);
                        break;
                    case RadioButtonsSizingMethodsOptionsEnum.OptionDimensions:
                        _calcFreeboardTopWidth = FreeboardTopWidthDimensions;
                        break;
                    default:
                        break;
                }
                return _calcFreeboardTopWidth;
            }
            set { ChangeAndNotify(ref _calcFreeboardTopWidth, value); }
        }

        private double _calcFreeboardTopArea;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcFreeboardTopArea
        {
            get
            {
                switch (SizingMethodsOptionsProperty)
                {
                    case RadioButtonsSizingMethodsOptionsEnum.OptionOxidationSettlingSludge:
                        _calcFreeboardTopArea = PondsCalculations.CalcFreeboardTopAreaBasedOnRetention(CalcFreeboardTopLength, CalcFreeboardTopWidth);
                        break;
                    case RadioButtonsSizingMethodsOptionsEnum.OptionDimensions:
                        _calcFreeboardTopArea = PondsCalculations.CalcFreeboardTopAreaBasedOnDimensions(CalcFreeboardTopLength, CalcFreeboardTopWidth);
                        break;
                    default:
                        break;
                }
                return _calcFreeboardTopArea;
            }
            set { ChangeAndNotify(ref _calcFreeboardTopArea, value); }
        }

        private double _calcFreeboardVolume;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcFreeboardVolume
        {
            get
            {
                switch (SizingMethodsOptionsProperty)
                {
                    case RadioButtonsSizingMethodsOptionsEnum.OptionOxidationSettlingSludge:
                        _calcFreeboardVolume = PondsCalculations.CalcFreeboardVolumeBasedOnRetention(CalcFreeboardTopLength, CalcFreeboardTopWidth, CalcSettlingTopLength, CalcSettlingTopWidth, FreeboardDepth);
                        break;
                    case RadioButtonsSizingMethodsOptionsEnum.OptionDimensions:
                        _calcFreeboardVolume = PondsCalculations.CalcFreeboardVolumeBasedOnDimensions(CalcFreeboardTopLength, CalcFreeboardTopWidth, CalcOxidationTopLength, CalcOxidationTopWidth, FreeboardDepth);
                        break;
                    default:
                        break;
                }
                return _calcFreeboardVolume;
            }
            set { ChangeAndNotify(ref _calcFreeboardVolume, value); }
        }

        #endregion

        #region Properties - Sizing Summary: Oxidation

        private double _calcOxidationBottomLength;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcOxidationBottomLength
        {
            get
            {
                if (IsOxidation)
                {
                    switch (SizingMethodsOptionsProperty)
                    {
                        case RadioButtonsSizingMethodsOptionsEnum.OptionOxidationSettlingSludge:
                            _calcOxidationBottomLength = PondsCalculations.CalcOxidationBottomLengthBasedOnRetention(CalcSettlingTopLength);
                            break;
                        case RadioButtonsSizingMethodsOptionsEnum.OptionDimensions:
                            _calcOxidationBottomLength = PondsCalculations.CalcOxidationBottomLengthBasedOnDimensions(CalcSettlingTopLength);
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    _calcOxidationBottomLength = 0;
                }
                return _calcOxidationBottomLength;
            }
            set { ChangeAndNotify(ref _calcOxidationBottomLength, value); }
        }

        private double _calcOxidationBottomWidth;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcOxidationBottomWidth
        {
            get
            {
                if (IsOxidation)
                {
                    switch (SizingMethodsOptionsProperty)
                    {
                        case RadioButtonsSizingMethodsOptionsEnum.OptionOxidationSettlingSludge:
                            double calcBottomWidth = PondsCalculations.CalcOxidationBottomWidthBasedOnRetention(CalcSettlingTopWidth);
                            CheckBottomWidthValue(calcBottomWidth);
                            _calcOxidationBottomWidth = calcBottomWidth;
                            break;
                        case RadioButtonsSizingMethodsOptionsEnum.OptionDimensions:
                            double calcBottomWidthDim = PondsCalculations.CalcOxidationBottomWidthBasedOnDimensions(CalcSettlingTopWidth);
                            CheckBottomWidthValue(calcBottomWidthDim);
                            _calcOxidationBottomWidth = calcBottomWidthDim;
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    _calcOxidationBottomWidth = 0;
                }

                return _calcOxidationBottomWidth;
            }
            set { ChangeAndNotify(ref _calcOxidationBottomWidth, value); }
        }


        private double _calcOxidationTopLength;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcOxidationTopLength
        {
            get
            {
                if (IsOxidation)
                {
                    switch (SizingMethodsOptionsProperty)
                    {
                        case RadioButtonsSizingMethodsOptionsEnum.OptionOxidationSettlingSludge:
                            _calcOxidationTopLength = PondsCalculations.CalcOxidationTopLengthBasedOnRetention(CalcOxidationBottomLength, OxidationDepth, PondInsideSlope);
                            break;
                        case RadioButtonsSizingMethodsOptionsEnum.OptionDimensions:
                            _calcOxidationTopLength = PondsCalculations.CalcOxidationTopLengthBasedOnDimensions(CalcOxidationBottomLength, OxidationDepth, PondInsideSlope);
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    _calcOxidationTopLength = 0; 
                }

                return _calcOxidationTopLength;
            }
            set { ChangeAndNotify(ref _calcOxidationTopLength, value); }
        }

        private double _calcOxidationTopWidth;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcOxidationTopWidth
        {
            get
            {
                if (IsOxidation)
                {
                    switch (SizingMethodsOptionsProperty)
                    {
                        case RadioButtonsSizingMethodsOptionsEnum.OptionOxidationSettlingSludge:
                            _calcOxidationTopWidth = PondsCalculations.CalcOxidationTopWidthBasedOnRetention(CalcOxidationBottomWidth, OxidationDepth, PondInsideSlope);
                            break;
                        case RadioButtonsSizingMethodsOptionsEnum.OptionDimensions:
                            _calcOxidationTopWidth = PondsCalculations.CalcOxidationTopWidthBasedOnDimensions(CalcOxidationBottomWidth, OxidationDepth, PondInsideSlope);
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    _calcOxidationTopWidth = 0;
                }

                return _calcOxidationTopWidth;
            }
            set { ChangeAndNotify(ref _calcOxidationTopWidth, value); }
        }

        private double _calcOxidationTopArea;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcOxidationTopArea
        {
            get
            {
                if (IsOxidation)
                {
                    switch (SizingMethodsOptionsProperty)
                    {
                        case RadioButtonsSizingMethodsOptionsEnum.OptionOxidationSettlingSludge:
                            _calcOxidationTopArea = PondsCalculations.CalcOxidationTopAreaBasedOnRetention(CalcOxidationTopLength, CalcOxidationTopWidth);
                            break;
                        case RadioButtonsSizingMethodsOptionsEnum.OptionDimensions:
                            _calcOxidationTopArea = PondsCalculations.CalcOxidationTopAreaBasedOnDimensions(CalcOxidationTopLength, CalcOxidationTopWidth);
                            break;
                        default:
                            break;
                    }
                    
                }
                else
                {
                    _calcOxidationTopArea = 0;
                }
                return _calcOxidationTopArea;
            }
            set { ChangeAndNotify(ref _calcOxidationTopArea, value); }
        }

        private double _calcOxidationVolume;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcOxidationVolume
        {
            get
            {
                if (IsOxidation)
                {
                    switch (SizingMethodsOptionsProperty)
                    {
                        case RadioButtonsSizingMethodsOptionsEnum.OptionOxidationSettlingSludge:
                            _calcOxidationVolume = PondsCalculations.CalcOxidationVolumeBasedOnRetention(CalcOxidationTopLength, CalcOxidationTopWidth, CalcOxidationBottomLength, CalcOxidationBottomWidth, OxidationDepth);
                            break;
                        case RadioButtonsSizingMethodsOptionsEnum.OptionDimensions:
                            _calcOxidationVolume = PondsCalculations.CalcOxidationVolumeBasedOnDimensions(CalcOxidationTopLength, CalcOxidationTopWidth, CalcOxidationBottomLength, CalcOxidationBottomWidth, OxidationDepth);
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    _calcOxidationVolume = 0;
                }
                return _calcOxidationVolume;

            }
            set { ChangeAndNotify(ref _calcOxidationVolume, value); }
        }

        #endregion

        #region Properties - Sizing Summary: Settling

        private double _calcSettlingWaterVolume;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcSettlingWaterVolume
        {
            get { return PondsCalculations.CalcSettlingWaterVolumeBasedOnRetention(SettlingRetentionTime, DesignFlow); }
            set { ChangeAndNotify(ref _calcSettlingWaterVolume, value); }

        }
        
        private double _calcSettlingBottomLength;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcSettlingBottomLength
        {
            get
            {
                switch (SizingMethodsOptionsProperty)
                {
                    case RadioButtonsSizingMethodsOptionsEnum.OptionOxidationSettlingSludge:
                        _calcSettlingBottomLength = PondsCalculations.CalcSettlingBottomLengthBasedOnRetention(CalcSettlingWaterVolume, SettlingDepth, PondInsideSlope, BottomLengthToWidthRatio);
                        break;
                    case RadioButtonsSizingMethodsOptionsEnum.OptionDimensions:
                        double oxidationDepthDim;
                        if (IsOxidation)
                        {
                            oxidationDepthDim = OxidationDepth;
                        }
                        else
                        {
                            oxidationDepthDim = 0;
                        }
                        _calcSettlingBottomLength = PondsCalculations.CalcSettlingBottomLengthBasedOnDimensions(CalcFreeboardTopLength, FreeboardDepth, oxidationDepthDim, SettlingDepth, PondInsideSlope);
                        break;
                    default:
                        break;
                }
                return _calcSettlingBottomLength;
            }
            set { ChangeAndNotify(ref _calcSettlingBottomLength, value); }
        }

        private double _calcSettlingBottomWidth;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcSettlingBottomWidth
        {
            get
            {
                switch (SizingMethodsOptionsProperty)
                {
                    case RadioButtonsSizingMethodsOptionsEnum.OptionOxidationSettlingSludge:
                        double calcBottomWidth = PondsCalculations.CalcSettlingBottomWidthBasedOnRetention(CalcSettlingBottomLength, BottomLengthToWidthRatio);
                        CheckBottomWidthValue(calcBottomWidth);
                        _calcSettlingBottomWidth = calcBottomWidth;
                        break;
                    case RadioButtonsSizingMethodsOptionsEnum.OptionDimensions:
                        double oxidationDepthDim;
                        if (IsOxidation)
                        {
                            oxidationDepthDim = OxidationDepth;
                        }
                        else
                        {
                            oxidationDepthDim = 0;
                        }
                        double calcBottomWidthDim = PondsCalculations.CalcSettlingBottomWidthBasedOnDimensions(CalcFreeboardTopWidth, FreeboardDepth, oxidationDepthDim, SettlingDepth, PondInsideSlope);
                        CheckBottomWidthValue(calcBottomWidthDim);
                        _calcSettlingBottomWidth = calcBottomWidthDim;
                        break;
                    default:
                        break;
                }
                return _calcSettlingBottomWidth;
            }
            set { ChangeAndNotify(ref _calcSettlingBottomWidth, value); }
        }


        private double _calcSettlingTopLength;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcSettlingTopLength
        {
            get
            {
                switch (SizingMethodsOptionsProperty)
                {
                    case RadioButtonsSizingMethodsOptionsEnum.OptionOxidationSettlingSludge:
                        _calcSettlingTopLength = PondsCalculations.CalcSettlingTopLengthBasedOnRetention(CalcSettlingBottomLength, SettlingDepth, PondInsideSlope);
                        break;
                    case RadioButtonsSizingMethodsOptionsEnum.OptionDimensions:
                        _calcSettlingTopLength = PondsCalculations.CalcSettlingTopLengthBasedOnDimensions(CalcSettlingBottomLength, SettlingDepth, PondInsideSlope);
                        break;
                    default:
                        break;
                }
                return _calcSettlingTopLength;
            }
            set { ChangeAndNotify(ref _calcSettlingTopLength, value); }
        }

        private double _calcSettlingTopWidth;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcSettlingTopWidth
        {
            get
            {
                switch (SizingMethodsOptionsProperty)
                {
                    case RadioButtonsSizingMethodsOptionsEnum.OptionOxidationSettlingSludge:
                        _calcSettlingTopWidth = PondsCalculations.CalcSettlingTopWidthBasedOnRetention(CalcSettlingBottomWidth, SettlingDepth, PondInsideSlope);
                        break;
                    case RadioButtonsSizingMethodsOptionsEnum.OptionDimensions:
                        _calcSettlingTopWidth = PondsCalculations.CalcSettlingTopWidthBasedOnDimensions(CalcSettlingBottomWidth, SettlingDepth, PondInsideSlope);
                        break;
                    default:
                        break;
                }
                return _calcSettlingTopWidth;
            }
            set { ChangeAndNotify(ref _calcSettlingTopWidth, value); }
        }

        private double _calcSettlingTopArea;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcSettlingTopArea
        {
            get
            {
                switch (SizingMethodsOptionsProperty)
                {
                    case RadioButtonsSizingMethodsOptionsEnum.OptionOxidationSettlingSludge:
                        _calcSettlingTopArea = PondsCalculations.CalcSettlingTopAreaBasedOnRetention(CalcSettlingTopLength, CalcSettlingTopWidth);
                        break;
                    case RadioButtonsSizingMethodsOptionsEnum.OptionDimensions:
                        _calcSettlingTopArea = PondsCalculations.CalcSettlingTopAreaBasedOnDimensions(CalcSettlingTopLength, CalcSettlingTopWidth);
                        break;
                    default:
                        break;
                }
                return _calcSettlingTopArea;
            }
            set { ChangeAndNotify(ref _calcSettlingTopArea, value); }
        }

        private double _calcSettlingVolume;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcSettlingVolume
        {
            get
            {
                switch (SizingMethodsOptionsProperty)
                {
                    case RadioButtonsSizingMethodsOptionsEnum.OptionOxidationSettlingSludge:
                        BottomLengthToWidthRatio = AssignBottomLengthToWidthRatio(BottomLengthToWidthRatio);
                        _calcSettlingVolume = PondsCalculations.CalcSettlingVolumeBasedOnRetention(CalcSettlingTopLength, CalcSettlingTopWidth, CalcSettlingBottomLength, CalcSettlingBottomWidth, SettlingDepth);
                        break;
                    case RadioButtonsSizingMethodsOptionsEnum.OptionDimensions:
                        BottomLengthToWidthRatio = CalcBottomLengthToWidthRatioBasedOnDimensions;
                        _calcSettlingVolume = PondsCalculations.CalcSettlingVolumeBasedOnDimensions(CalcSettlingTopLength, CalcSettlingTopWidth, CalcSettlingBottomLength, CalcSettlingBottomWidth, SettlingDepth);
                        break;
                    default:
                        break;
                }
                return _calcSettlingVolume;
            }
            set { ChangeAndNotify(ref _calcSettlingVolume, value); }
        }

        #endregion

        #region Properties - Sizing Summary: Sludge

        private double _calcSludgeBottomLength;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcSludgeBottomLength
        {
            get
            {
                if (IsSludge)
                {
                    switch (SizingMethodsOptionsProperty)
                    {
                        case RadioButtonsSizingMethodsOptionsEnum.OptionOxidationSettlingSludge:
                            _calcSludgeBottomLength = PondsCalculations.CalcSludgeBottomLengthBasedOnRetention(CalcSettlingBottomLength, SludgeDepth, PondInsideSlope);
                            break;
                        case RadioButtonsSizingMethodsOptionsEnum.OptionDimensions:
                            _calcSludgeBottomLength = PondsCalculations.CalcSludgeBottomLengthBasedOnDimensions(CalcSettlingBottomLength, SludgeDepth, PondInsideSlope);
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    _calcSludgeBottomLength = 0;
                }
                return _calcSludgeBottomLength;
            }
            set { ChangeAndNotify(ref _calcSludgeBottomLength, value); }
        }

        private double _calcSludgeBottomWidth;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcSludgeBottomWidth
        {
            get
            {
                if (IsSludge)
                {
                    switch (SizingMethodsOptionsProperty)
                    {
                        case RadioButtonsSizingMethodsOptionsEnum.OptionOxidationSettlingSludge:

                            double calcBottomWidth = PondsCalculations.CalcSludgeBottomWidthBasedOnRetention(CalcSettlingBottomWidth, SludgeDepth, PondInsideSlope);
                            CheckBottomWidthValue(calcBottomWidth);
                            _calcSludgeBottomWidth = calcBottomWidth;
                            break;
                        case RadioButtonsSizingMethodsOptionsEnum.OptionDimensions:
                            double calcBottomWidthDim = PondsCalculations.CalcSludgeBottomWidthBasedOnDimensions(CalcSettlingBottomWidth, SludgeDepth, PondInsideSlope);
                            CheckBottomWidthValue(calcBottomWidthDim);
                            _calcSludgeBottomWidth = calcBottomWidthDim;
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    _calcSludgeBottomWidth = 0;
                }
                return _calcSludgeBottomWidth;
            }
            set { ChangeAndNotify(ref _calcSludgeBottomWidth, value); }
        }


        private double _calcSludgeTopLength;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcSludgeTopLength
        {
            get
            {
                if (IsSludge)
                {
                    switch (SizingMethodsOptionsProperty)
                    {
                        case RadioButtonsSizingMethodsOptionsEnum.OptionOxidationSettlingSludge:
                            _calcSludgeTopLength = PondsCalculations.CalcSludgeTopLengthBasedOnRetention(CalcSettlingBottomLength);
                            break;
                        case RadioButtonsSizingMethodsOptionsEnum.OptionDimensions:
                            _calcSludgeTopLength = PondsCalculations.CalcSludgeTopLengthBasedOnDimensions(CalcSettlingBottomLength);
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    _calcSludgeTopLength = 0;
                }

                return _calcSludgeTopLength;
            }
            set { ChangeAndNotify(ref _calcSludgeTopLength, value); }
        }

        private double _calcSludgeTopWidth;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcSludgeTopWidth
        {
            get
            {
                if (IsSludge)
                {
                    switch (SizingMethodsOptionsProperty)
                    {
                        case RadioButtonsSizingMethodsOptionsEnum.OptionOxidationSettlingSludge:
                            _calcSludgeTopWidth = PondsCalculations.CalcSludgeTopWidthBasedOnRetention(CalcSettlingBottomWidth);
                            break;
                        case RadioButtonsSizingMethodsOptionsEnum.OptionDimensions:
                            _calcSludgeTopWidth = PondsCalculations.CalcSludgeTopWidthBasedOnDimensions(CalcSettlingBottomWidth);
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    _calcSludgeTopWidth = 0;
                }
                return _calcSludgeTopWidth;
            }
            set { ChangeAndNotify(ref _calcSludgeTopWidth, value); }
        }

        private double _calcSludgeTopArea;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcSludgeTopArea
        {
            get
            {
                if (IsSludge)
                {
                    switch (SizingMethodsOptionsProperty)
                    {
                        case RadioButtonsSizingMethodsOptionsEnum.OptionOxidationSettlingSludge:
                            _calcSludgeTopArea = PondsCalculations.CalcSludgeTopAreaBasedOnRetention(CalcSludgeTopLength, CalcSludgeTopWidth);
                            break;
                        case RadioButtonsSizingMethodsOptionsEnum.OptionDimensions:
                            _calcSludgeTopArea = PondsCalculations.CalcSludgeTopAreaBasedOnDimensions(CalcSludgeTopLength, CalcSludgeTopWidth);
                            break;
                        default:
                            break;
                    }                    
                }
                else
                {
                    _calcSludgeTopArea = 0;
                }
                return _calcSludgeTopArea;
            }
            set { ChangeAndNotify(ref _calcSludgeTopArea, value); }
        }

        private double _calcSludgeVolume;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcSludgeVolume
        {
            get
            {
                if (IsSludge)
                {
                    switch (SizingMethodsOptionsProperty)
                    {
                        case RadioButtonsSizingMethodsOptionsEnum.OptionOxidationSettlingSludge:
                            _calcSludgeVolume = PondsCalculations.CalcSludgeVolumeBasedOnRetention(CalcSludgeTopLength, CalcSludgeTopWidth, CalcSludgeBottomLength, CalcSludgeBottomWidth, SludgeDepth);
                            break;
                        case RadioButtonsSizingMethodsOptionsEnum.OptionDimensions:
                            _calcSludgeVolume = PondsCalculations.CalcSludgeVolumeBasedOnDimensions(CalcSludgeTopLength, CalcSludgeTopWidth, CalcSludgeBottomLength, CalcSludgeBottomWidth, SludgeDepth);
                            break;
                        default:
                            break;
                    }
                    
                }
                else
                {
                    _calcSludgeVolume = 0;
                }
                return _calcSludgeVolume;
            }
            set { ChangeAndNotify(ref _calcSludgeVolume, value); }
        }

        #endregion

        #region Sizing Summary: Annual sludge produced, total removal of metals, partial removal of metals

        private double _calcAluminumHydroxideConcentration;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcAluminumHydroxideConcentration
        {
            get { return PondsCalculations.CalcAluminumHydroxideConcentration(Aluminum); }
            set { ChangeAndNotify(ref _calcAluminumHydroxideConcentration, value); }
        }

        private double _calcFerrousHydroxideConcentration;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcFerrousHydroxideConcentration
        {
            get { return PondsCalculations.CalcFerrousHydroxideConcentration(FerrousIron); }
            set { ChangeAndNotify(ref _calcFerrousHydroxideConcentration, value); }
        }

        private double _calcFerricHydroxideConcentration;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcFerricHydroxideConcentration
        {
            get { return PondsCalculations.CalcFerricHydroxideConcentration(FerricIron); }
            set { ChangeAndNotify(ref _calcFerricHydroxideConcentration, value); }
        }

        private double _calcManganeseHydroxideConcentration;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcManganeseHydroxideConcentration
        {
            get { return PondsCalculations.CalcManganeseHydroxideConcentration(Manganese); }
            set { ChangeAndNotify(ref _calcManganeseHydroxideConcentration, value); }
        }

        private double _calcCalciteConcentration;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcCalciteConcentration
        {
            get { return PondsCalculations.CalcCalciteConcentration(Calcite); }
            set { ChangeAndNotify(ref _calcCalciteConcentration, value); }
        }

        private double _calcMiscellaneousConcentration;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcMiscellaneousConcentration
        {
            get { return PondsCalculations.CalcMiscellaneousConcentration(MiscellaneousSolids); }
            set { ChangeAndNotify(ref _calcMiscellaneousConcentration, value); }
        }

        private double _calcAluminumHydroxideFraction;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcAluminumHydroxideFraction
        {
            get { return PondsCalculations.CalcAluminumHydroxideFraction(CalcAluminumHydroxideConcentration, CalcFerrousHydroxideConcentration, CalcFerricHydroxideConcentration,
                                                                         CalcManganeseHydroxideConcentration, CalcCalciteConcentration, CalcMiscellaneousConcentration); }
            set { ChangeAndNotify(ref _calcAluminumHydroxideFraction, value); }
        }

        private double _calcFerrousHydroxideFraction;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcFerrousHydroxideFraction
        {
            get
            {
                return PondsCalculations.CalcFerrousHydroxideFraction(CalcAluminumHydroxideConcentration, CalcFerrousHydroxideConcentration, CalcFerricHydroxideConcentration,
                                                                       CalcManganeseHydroxideConcentration, CalcCalciteConcentration, CalcMiscellaneousConcentration);
            }
            set { ChangeAndNotify(ref _calcFerrousHydroxideFraction, value); }
        }

        private double _calcFerricHydroxideFraction;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcFerricHydroxideFraction
        {
            get
            {
                return PondsCalculations.CalcFerricHydroxideFraction(CalcAluminumHydroxideConcentration, CalcFerrousHydroxideConcentration, CalcFerricHydroxideConcentration,
                                                                       CalcManganeseHydroxideConcentration, CalcCalciteConcentration, CalcMiscellaneousConcentration);
            }
            set { ChangeAndNotify(ref _calcFerricHydroxideFraction, value); }
        }

        private double _calcManganeseHydroxideFraction;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcManganeseHydroxideFraction
        {
            get
            {
                return PondsCalculations.CalcManganeseHydroxideFraction(CalcAluminumHydroxideConcentration, CalcFerrousHydroxideConcentration, CalcFerricHydroxideConcentration,
                                                                       CalcManganeseHydroxideConcentration, CalcCalciteConcentration, CalcMiscellaneousConcentration);
            }
            set { ChangeAndNotify(ref _calcManganeseHydroxideFraction, value); }
        }

        private double _calcCalciteFraction;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcCalciteFraction
        {
            get
            {
                return PondsCalculations.CalcCalciteFraction(CalcAluminumHydroxideConcentration, CalcFerrousHydroxideConcentration, CalcFerricHydroxideConcentration,
                                                                       CalcManganeseHydroxideConcentration, CalcCalciteConcentration, CalcMiscellaneousConcentration);
            }
            set { ChangeAndNotify(ref _calcCalciteFraction, value); }
        }

        private double _calcMiscellaneousFraction;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcMiscellaneousFraction
        {
            get
            {
                return PondsCalculations.CalcMiscellaneousFraction(CalcAluminumHydroxideConcentration, CalcFerrousHydroxideConcentration, CalcFerricHydroxideConcentration,
                                                                       CalcManganeseHydroxideConcentration, CalcCalciteConcentration, CalcMiscellaneousConcentration);
            }
            set { ChangeAndNotify(ref _calcMiscellaneousFraction, value); }
        }

        private double _calcAluminumHydroxideDensity;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcAluminumHydroxideDensity
        {
            get { return PondsCalculations.CalcAluminumHydroxideDensity(CalcAluminumHydroxideFraction); }
            set { ChangeAndNotify(ref _calcAluminumHydroxideDensity, value); }
        }

        private double _calcFerrousHydroxideDensity;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcFerrousHydroxideDensity
        {
            get { return PondsCalculations.CalcFerrousHydroxideDensity(CalcFerrousHydroxideFraction); }
            set { ChangeAndNotify(ref _calcFerrousHydroxideDensity, value); }
        }

        private double _calcFerricHydroxideDensity;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcFerricHydroxideDensity
        {
            get { return PondsCalculations.CalcFerricHydroxideDensity(CalcFerricHydroxideFraction); }
            set { ChangeAndNotify(ref _calcFerricHydroxideDensity, value); }
        }

        private double _calcManganeseHydroxideDensity;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcManganeseHydroxideDensity
        {
            get { return PondsCalculations.CalcManganeseHydroxideDensity(CalcManganeseHydroxideFraction); }
            set { ChangeAndNotify(ref _calcManganeseHydroxideDensity, value); }
        }

        private double _calcCalciteDensity;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcCalciteDensity
        {
            get { return PondsCalculations.CalcCalciteDensity(CalcCalciteFraction); }
            set { ChangeAndNotify(ref _calcCalciteDensity, value); }
        }

        private double _calcMiscellaneousDensity;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcMiscellaneousDensity
        {
            get { return PondsCalculations.CalcMiscellaneousDensity(CalcMiscellaneousFraction, MiscellaneousSolidsDensity); }
            set { ChangeAndNotify(ref _calcMiscellaneousDensity, value); }
        }

        private double _calcTotalDensity;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcTotalDensity
        {
            get { return PondsCalculations.CalcTotalDensity(CalcAluminumHydroxideDensity, CalcFerrousHydroxideDensity, CalcFerricHydroxideDensity,
                                                                       CalcManganeseHydroxideDensity, CalcCalciteDensity, CalcMiscellaneousDensity); }
            set { ChangeAndNotify(ref _calcTotalDensity, value); }
        }

        private double _calcSludgeSpecificGravity;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcSludgeSpecificGravity
        {
            get { return PondsCalculations.CalcSludgeSpecificGravity(CalcTotalDensity); }
            set { ChangeAndNotify(ref _calcSludgeSpecificGravity, value); }
        }

        private double _calcSludgeDensity;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcSludgeDensity
        {
            get { return PondsCalculations.CalcSludgeDensity(CalcSludgeSpecificGravity, PercentSolids); }
            set { ChangeAndNotify(ref _calcSludgeDensity, value); }
        }

        private double _calcSludgeProducedPerMinute;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcSludgeProducedPerMinute
        {
            get
            {
                return PondsCalculations.CalcSludgeProducedPerMinute(CalcAluminumHydroxideConcentration, CalcFerrousHydroxideConcentration, CalcFerricHydroxideConcentration,
                                                                     CalcManganeseHydroxideConcentration, CalcCalciteConcentration, CalcMiscellaneousConcentration, TypicalFlow);
            }
            set { ChangeAndNotify(ref _calcSludgeProducedPerMinute, value); }
        }

        private double _calcSludgeProducedPerDay;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcSludgeProducedPerDay
        {
            get { return PondsCalculations.CalcSludgeProducedPerDay(CalcSludgeProducedPerMinute); }
            set { ChangeAndNotify(ref _calcSludgeProducedPerDay, value); }
        }

        private double _calcSludgeProducedPerYear;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcSludgeProducedPerYear
        {
            get { return PondsCalculations.CalcSludgeProducedPerYear(CalcSludgeProducedPerDay); }
            set { ChangeAndNotify(ref _calcSludgeProducedPerYear, value); }
        }

        private double _calcSludgeProducedVolumeGallonsPerYear;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcSludgeProducedVolumeGallonsPerYear
        {
            get { return PondsCalculations.CalcSludgeProducedVolumeGallonsPerYear(CalcSludgeProducedPerYear, CalcSludgeDensity); }
            set { ChangeAndNotify(ref _calcSludgeProducedVolumeGallonsPerYear, value); }
        }

        private double _calcSludgeProducedVolumeCubicYardsPerYear;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcSludgeProducedVolumeCubicYardsPerYear
        {
            get { return PondsCalculations.CalcSludgeProducedVolumeCubicYardsPerYear(CalcSludgeProducedPerYear, CalcSludgeDensity); }
            set { ChangeAndNotify(ref _calcSludgeProducedVolumeCubicYardsPerYear, value); }
        }

        private double _calcDewateredSludgeDensity;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcDewateredSludgeDensity
        {
            get { return PondsCalculations.CalcDewateredSludgeDensity(CalcSludgeSpecificGravity, PercentDewateredSolids); }
            set { ChangeAndNotify(ref _calcDewateredSludgeDensity, value); }
        }

        private double _calcDewateredSludgeProducedCubicYardsPerYear;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcDewateredSludgeProducedCubicYardsPerYear
        {
            get { return PondsCalculations.CalcDewateredSludgeProducedCubicYardsPerYear(CalcSludgeProducedPerYear, CalcDewateredSludgeDensity); }
            set { ChangeAndNotify(ref _calcDewateredSludgeProducedCubicYardsPerYear, value); }
        }

        private double _calcSludgeRemovalPerYear;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcSludgeRemovalPerYear
        {
            get
            {
                if (CalcSludgeVolume != 0)
                {
                    _calcSludgeRemovalPerYear = PondsCalculations.CalcSludgeRemovalPerYear(CalcSludgeProducedVolumeCubicYardsPerYear, CalcSludgeVolume);
                }
                else
                {
                    _calcSludgeRemovalPerYear = 0;
                }
                return _calcSludgeRemovalPerYear;
            }
            set { ChangeAndNotify(ref _calcSludgeRemovalPerYear, value); }
        }

        private double _calcSludgeRemovalPerMonth;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcSludgeRemovalPerMonth
        {
            get { return PondsCalculations.CalcSludgeRemovalPerMonth(CalcSludgeRemovalPerYear); }
            set { ChangeAndNotify(ref _calcSludgeRemovalPerMonth, value); }
        }
        #endregion

        #region Sludge removal and disposal estimates

        private decimal _calcMobilizationDemobilizationCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcMobilizationDemobilizationCost
        {
            get
            {
                if (IsMobilizationDemobilization)
                {
                    _calcMobilizationDemobilizationCost = MobilizationDemobilizationCost;
                }
                else
                {
                    _calcMobilizationDemobilizationCost = 0m;
                }
                return _calcMobilizationDemobilizationCost;
            }
            set { ChangeAndNotify(ref _calcMobilizationDemobilizationCost, value); }
        }

        private decimal _calcSludgeRemovalCostPerGallon;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcSludgeRemovalCostPerGallon
        {
            get
            {
                if (IsSludgeHandlingDisposal)
                {
                    _calcSludgeRemovalCostPerGallon = PondsCalculations.CalcSludgeRemovalCostPerGallon(CalcSludgeProducedVolumeGallonsPerYear, SludgeHandlingDisposalUnitCost);
                }
                else
                {
                    return 0m;
                }
                return _calcSludgeRemovalCostPerGallon;
            }
            set { ChangeAndNotify(ref _calcSludgeRemovalCostPerGallon, value); }
        }

        private double _calcVacuumTruckTrips;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcVacuumTruckTrips
        {
            get { return PondsCalculations.CalcVacuumTruckTrips(CalcSludgeProducedVolumeCubicYardsPerYear, SludgeHandlingVacuumTruckVolume); }
            set { ChangeAndNotify(ref _calcVacuumTruckTrips, value); }
        }

        private decimal _calcVacuumTruckHandlingCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcVacuumTruckHandlingCost
        {
            get
            {
                if (IsSludgeRemovalVacuumTruck)
                {
                    _calcVacuumTruckHandlingCost = PondsCalculations.CalcVacuumTruckHandlingCost(CalcVacuumTruckTrips, SludgeHandlingVacuumTruckTimeToFill, SludgeHandlingVacuumTruckUnitCost); 
                }
                else
                {
                    _calcVacuumTruckHandlingCost = 0m;
                }
                return _calcVacuumTruckHandlingCost;
            }
            set { ChangeAndNotify(ref _calcVacuumTruckHandlingCost, value); }
        }

        private double _calcPumpingTime;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcPumpingTime
        {
            get { return PondsCalculations.CalcPumpingTime(CalcSludgeProducedVolumeGallonsPerYear, PumpRate); }
            set { ChangeAndNotify(ref _calcPumpingTime, value); }
        }

        private decimal _calcPumpRentalCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcPumpRentalCost
        {
            get
            {
                if (IsSludgeRemovalPumpRental)
                {
                    _calcPumpRentalCost = PondsCalculations.CalcPumpRentalCost(CalcPumpingTime, PumpRentalCost);
                }
                else
                {
                    _calcPumpRentalCost = 0m;
                }
                return _calcPumpRentalCost;
            }
            set { ChangeAndNotify(ref _calcPumpRentalCost, value); }
        }

        private decimal _calcElectricCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcElectricCost
        {
            get { return PondsCalculations.CalcElectricCost(CalcSludgeProducedVolumeGallonsPerYear, PumpRate, PowerRequirement, ElectricRateUnitCost); }
            set { ChangeAndNotify(ref _calcElectricCost, value); }
        }

        private decimal _calcFuelCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcFuelCost
        {
            get { return PondsCalculations.CalcFuelCost(CalcSludgeProducedVolumeGallonsPerYear, PumpRate, FuelRate, FuelUnitCost); }
            set { ChangeAndNotify(ref _calcFuelCost, value); }
        }

        private decimal _calcPumpRateCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcPumpRateCost
        {
            get
            {
                switch (PumpOptionsProperty)
                {
                    case RadioButtonsPumpOptionsEnum.OptionElectric:
                        _calcPumpRateCost = CalcElectricCost;
                        break;
                    case RadioButtonsPumpOptionsEnum.OptionFuel:
                        _calcPumpRateCost = CalcFuelCost;
                        break;
                    default:
                        break;
                }
                return _calcPumpRateCost;
            }
            set { ChangeAndNotify(ref _calcPumpRateCost, value); }
        }

        private decimal _calcPumpCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcPumpCost
        {
            get
            {
                if (IsSludgeRemovalPump)
                {
                    _calcPumpCost = CalcPumpRentalCost + CalcPumpRateCost;
                }
                else
                {
                    _calcPumpCost = 0m;
                }
                return _calcPumpCost;
            }
            set { ChangeAndNotify(ref _calcPumpCost, value); }
        }

        private decimal _calcMechanicalExcavationCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcMechanicalExcavationCost
        {
            get
            {
                if (IsMechanicalExcavation)
                {
                    _calcMechanicalExcavationCost = PondsCalculations.CalcMechanicalExcavationCost(CalcDewateredSludgeProducedCubicYardsPerYear, MechanicalExcavationUnitCost);
                }
                else
                {
                    _calcMechanicalExcavationCost = 0m;
                }
                return _calcMechanicalExcavationCost;
            }
            set { ChangeAndNotify(ref _calcMechanicalExcavationCost, value); }
        }


        private decimal _calcTipCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcTipCost
        {
            get
            {
                if (IsLandfillTippingFee)
                {
                    _calcTipCost = PondsCalculations.CalcTipCost(CalcSludgeProducedPerYear, LandfillTippingFeeUnitCost);
                }
                else
                {
                    _calcTipCost = 0m;
                }
                return _calcTipCost;
            }
            set { ChangeAndNotify(ref _calcTipCost, value); }
        }

        private decimal _calcTriaxleDisposalCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcTriaxleDisposalCost
        {
            get { return PondsCalculations.CalcTriaxleDisposalCost(CalcDewateredSludgeProducedCubicYardsPerYear, SludgeDisposalTriaxleTruckVolume, SludgeDisposalTriaxleRoundtripDistance, SludgeDisposalTriaxleTransportationUnitCost); }
            set { ChangeAndNotify(ref _calcTriaxleDisposalCost, value); }
        }

        private decimal _calcVacuumTruckDisposalCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcVacuumTruckDisposalCost
        {
            get { return PondsCalculations.CalcVacuumTruckDisposalCost(CalcVacuumTruckTrips, SludgeDisposalVacuumTruckHoursPerTrip, SludgeDisposalVacuumTruckUnitCost); }
            set { ChangeAndNotify(ref _calcVacuumTruckDisposalCost, value); }
        }

        private double _calcGeotubeQuantity;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcGeotubeQuantity
        {
            get { return PondsCalculations.CalcGeotubeQuantity(CalcDewateredSludgeProducedCubicYardsPerYear, GeotubeVolume); }
            set { ChangeAndNotify(ref _calcGeotubeQuantity, value); }
        }

        private decimal _calcGeotubeCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcGeotubeCost
        {
            get { return PondsCalculations.CalcGeotubeCost(CalcGeotubeQuantity, GeotubeUnitCost); }
            set { ChangeAndNotify(ref _calcGeotubeCost, value); }
        }

        private decimal _calcGeotubeLoadingCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcGeotubeLoadingCost
        {
            get { return PondsCalculations.CalcGeotubeLoadingCost(CalcDewateredSludgeProducedCubicYardsPerYear, GeotubeTruckVolume, GeotubeTimeToLoadTruck, GeotubeExcavatorHourlyRate); }
            set { ChangeAndNotify(ref _calcGeotubeLoadingCost, value); }
        }

        private decimal _calcGeotubeTransportationCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcGeotubeTransportationCost
        {
            get { return PondsCalculations.CalcGeotubeTransportationCost(CalcDewateredSludgeProducedCubicYardsPerYear, GeotubeTruckVolume, GeotubeTruckRoundtripDistance, GeotubeTruckTransportationUnitCost); }
            set { ChangeAndNotify(ref _calcGeotubeTransportationCost, value); }
        }

        private decimal _calcGeotubeTotalCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcGeotubeTotalCost
        {
            get { return PondsCalculations.CalcGeotubeTotalCost(CalcGeotubeCost, CalcGeotubeLoadingCost, CalcGeotubeTransportationCost); }
            set { ChangeAndNotify(ref _calcGeotubeTotalCost, value); }
        }

        private decimal _calcSludgeDisposalCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcSludgeDisposalCost
        {
            get
            {
                switch (SludgeDisposalOptionsProperty)
                {
                    case RadioButtonsSludgeDisposalOptionsEnum.OptionTriaxle:
                        _calcSludgeDisposalCost = CalcTriaxleDisposalCost;
                        break;
                    case RadioButtonsSludgeDisposalOptionsEnum.OptionVacuumTruck:
                        _calcSludgeDisposalCost = CalcVacuumTruckDisposalCost;
                        break;
                    case RadioButtonsSludgeDisposalOptionsEnum.OptionBorehole:
                        _calcSludgeDisposalCost = CalcBoreholeCost;
                        break;
                    case RadioButtonsSludgeDisposalOptionsEnum.OptionGeotube:
                        _calcSludgeDisposalCost = CalcGeotubeTotalCost;
                        break;
                    default:
                        break;
                }
                return _calcSludgeDisposalCost;
            }
            set { ChangeAndNotify(ref _calcSludgeDisposalCost, value); }
        }
    #endregion

        #region Baffle

        private double _calcBaffleLength;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcBaffleLength
        {
            get
            {
                switch (BaffleOptionsProperty)
                {
                    case RadioButtonsBaffleOptionsEnum.OptionEstimate:
                        _calcBaffleLength = PondsCalculations.CalcBaffleLength(BaffleQuantity, CalcFreeboardTopWidth, FreeboardDepth, PondInsideSlope);
                        break;
                    case RadioButtonsBaffleOptionsEnum.OptionUserSpecified:
                        _calcBaffleLength = PondsCalculations.CalcBaffleLengthUserSpecified(BaffleQuantity, BaffleLengthUserSpecified);
                        break;
                    default:
                        break;
                }

                return _calcBaffleLength;
            }
            set { ChangeAndNotify(ref _calcBaffleLength, value); }
        }

        #endregion

        #region Properties - Sizing Summary: Liner

        private double _calcLinerSlopeLength;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLinerSlopeLength
        {
            get
            {
                double oxidationDepth;
                double sludgeDepth;
                if (IsOxidation)
                {
                    oxidationDepth = OxidationDepth;
                }
                else
                {
                    oxidationDepth = 0;
                }
                if (IsSludge)
                {
                    sludgeDepth = SludgeDepth;
                }
                else
                {
                    sludgeDepth = 0;
                }
                return PondsCalculations.CalcLinerSlopeLength(FreeboardDepth, oxidationDepth, SettlingDepth, sludgeDepth, PondInsideSlope);
            }
            set { ChangeAndNotify(ref _calcLinerSlopeLength, value); }
        }

        private double _calcSyntheticLinerArea;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcSyntheticLinerArea
        {
            get { return PondsCalculations.CalcSyntheticLinerArea(CalcSludgeBottomLength, CalcSludgeBottomWidth, CalcFreeboardTopLength, CalcFreeboardTopWidth, CalcLinerSlopeLength); }
            set { ChangeAndNotify(ref _calcSyntheticLinerArea, value); }
        }

        private double _calcClayLinerArea;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcClayLinerArea
        {
            get { return PondsCalculations.CalcClayLinerArea(CalcSludgeBottomLength, CalcSludgeBottomWidth, CalcFreeboardTopLength, CalcFreeboardTopWidth, CalcLinerSlopeLength); }
            set { ChangeAndNotify(ref _calcClayLinerArea, value); }
        }

        private double _calcClayLinerVolume;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcClayLinerVolume
        {
            get { return PondsCalculations.CalcClayLinerVolume(CalcClayLinerArea, ClayLinerThickness); }
            set { ChangeAndNotify(ref _calcClayLinerVolume, value); }
        }

        private double _calcGeosyntheticClayLinerArea;
        /// <summary>
        /// Calculated.  Same as synthetic liner area.
        /// </summary>
        public double CalcGeosyntheticClayLinerArea
        {
            get { return PondsCalculations.CalcGeosyntheticClayLinerArea(CalcSludgeBottomLength, CalcSludgeBottomWidth, CalcFreeboardTopLength, CalcFreeboardTopWidth, CalcLinerSlopeLength); }
            set { ChangeAndNotify(ref _calcGeosyntheticClayLinerArea, value); }
        }

        private double _calcGeosyntheticClayLinerVolume;
        /// <summary>
        /// Calculated.
        /// </summary>
        public double CalcGeosyntheticClayLinerVolume
        {
            get { return PondsCalculations.CalcGeosyntheticClayLinerVolume(CalcGeosyntheticClayLinerArea, GeosyntheticClayLinerSoilCover); }
            set { ChangeAndNotify(ref _calcGeosyntheticClayLinerVolume, value); }
        }

        private double _calcNonWovenGeotextileArea;
        /// <summary>
        /// Calculated.  
        /// </summary>
        public double CalcNonWovenGeotextileArea
        {
            get { return PondsCalculations.CalcNonWovenGeotextileArea(CalcSludgeBottomLength, CalcSludgeBottomWidth, CalcFreeboardTopLength, CalcFreeboardTopWidth, CalcLinerSlopeLength); }
            set { ChangeAndNotify(ref _calcNonWovenGeotextileArea, value); }
        }

        private decimal _calcNonWovenGeotextileCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcNonWovenGeotextileCost
        {
            get { return PondsCalculations.CalcNonWovenGeotextileCost(CalcNonWovenGeotextileArea, NonWovenGeotextileUnitCost); }
            set { ChangeAndNotify(ref _calcNonWovenGeotextileCost, value); }
        }

        //private double _calcSoilCoverArea;
        ///// <summary>
        ///// Calculated
        ///// </summary>
        //public double CalcSoilCoverArea
        //{
        //    get { return PondsCalculations.CalcSoilCoverArea(CalcSludgeBottomLength, CalcSludgeBottomWidth, CalcFreeboardTopLength, CalcFreeboardTopWidth, CalcLinerSlopeLength); }
        //    set { ChangeAndNotify(ref _calcSoilCoverArea, value); }
        //}

        //private double _calcSoilCoverVolume;
        ///// <summary>
        ///// Calculated
        ///// </summary>
        //public double CalcSoilCoverVolume
        //{
        //    get { return PondsCalculations.CalcSoilCoverVolume(CalcSoilCoverArea, SoilCoverThickness); }
        //    set { ChangeAndNotify(ref _calcSoilCoverVolume, value); }
        //}

        private double _calcLinerArea;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLinerArea
        {
            get
            {
                switch (LinerOptionsProperty)
                {
                    case RadioButtonsLinerOptionsEnum.OptionNoLiner:
                        _calcLinerArea = 0;
                        break;
                    case RadioButtonsLinerOptionsEnum.OptionClayLiner:
                        _calcLinerArea = CalcClayLinerArea;
                        break;
                    case RadioButtonsLinerOptionsEnum.OptionSyntheticLiner:
                        _calcLinerArea = CalcSyntheticLinerArea;
                        break;
                    case RadioButtonsLinerOptionsEnum.OptionGeosyntheticClayLiner:
                        _calcLinerArea = CalcGeosyntheticClayLinerArea;
                        break;
                }

                return _calcLinerArea;

            }
            set { ChangeAndNotify(ref _calcLinerArea, value); }
        }

        private double _calcLinerVolume;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLinerVolume
        {
            get
            {
                switch (LinerOptionsProperty)
                {
                    case RadioButtonsLinerOptionsEnum.OptionNoLiner:
                        _calcLinerVolume = 0;
                        break;
                    case RadioButtonsLinerOptionsEnum.OptionClayLiner:
                        _calcLinerVolume = CalcClayLinerVolume;
                        break;
                    case RadioButtonsLinerOptionsEnum.OptionSyntheticLiner:
                        _calcLinerVolume = 0;
                        break;
                    case RadioButtonsLinerOptionsEnum.OptionGeosyntheticClayLiner:
                        _calcLinerVolume = CalcGeosyntheticClayLinerVolume;
                        break;
                }

                return _calcLinerVolume;

            }
            set { ChangeAndNotify(ref _calcLinerVolume, value); }
        }
        #endregion

        #region Properties - Sizing Summary: Retention Times

        private double _calcRetentionTimeSettling;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcRetentionTimeSettling
        {
            get { return PondsCalculations.CalcSettlingRetentionTime(DesignFlow, CalcSettlingVolume); }
            set { ChangeAndNotify(ref _calcRetentionTimeSettling, value); }

        }

        private double _calcRetentionTimeOxidation;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcRetentionTimeOxidation
        {
            get { return PondsCalculations.CalcOxidationRetentionTime(DesignFlow, CalcOxidationVolume); }
            set { ChangeAndNotify(ref _calcRetentionTimeOxidation, value); }

        }

        private double _calcRetentionTimeTotal;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcRetentionTimeTotal
        {
            get { return PondsCalculations.CalcTotalRetentionTime(DesignFlow, CalcSludgeVolume, CalcSettlingVolume, CalcOxidationVolume); }
            set { ChangeAndNotify(ref _calcRetentionTimeTotal, value); }

        }

        #endregion

        #region Properties - Capital Costs

        private decimal _calcExcavationCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcExcavationCost
        {
            get { return PondsCalculations.CalcExcavationCost(CalcExcavationVolume, ExcavationUnitCost); }
            set { ChangeAndNotify(ref _calcExcavationCost, value); }
        }

 
        private decimal _calcInOutPipeCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcInOutPipeCost
        {
            get { return PondsCalculations.CalcInOutPipeCost(InOutPipeLength, InOutPipeUnitCost); }
            set { ChangeAndNotify(ref _calcInOutPipeCost, value); }
        }

 
        private decimal _calcPipeCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcPipeCost
        {
            get
            {
                decimal amdtreatPipe = PondsCalculations.CalcAmdtreatPipeCost(InOutPipeLength, CalcInOutPipeCost, InOutPipeInstallRate, LaborRate);

                decimal customPipe = PondsCalculations.CalcCustomPipeCost(CustomPipeLength1, CustomPipeUnitCost1,
                                                                                     CustomPipeLength2, CustomPipeUnitCost2,
                                                                                     CustomPipeLength3, CustomPipeUnitCost3);

                if (PipeOptionAmdtreat && PipeOptionCustom)
                {
                    _calcPipeCost = amdtreatPipe + customPipe;
                }
                else if (PipeOptionAmdtreat && PipeOptionCustom == false)
                {
                    _calcPipeCost = amdtreatPipe;
                }
                else if (PipeOptionCustom && PipeOptionAmdtreat == false)
                {
                    _calcPipeCost = customPipe;
                }
                else if (PipeOptionAmdtreat == false && PipeOptionCustom == false)
                {
                    _calcPipeCost = 0m;
                }

                return _calcPipeCost;

            }
            set { ChangeAndNotify(ref _calcPipeCost, value); }
        }


        private decimal _calcOtherItemsCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcOtherItemsCost
        {
            get
            {
                return PondsCalculations.CalcOtherItemsCost(ValveQuantity, ValveUnitCost,
                                                            PumpQuantity, PumpUnitCost,
                                                            IntakeStructureQuantity, IntakeStructureUnitCost,
                                                            FlowDistributionStructureQuantity, FlowDistributionStructureUnitCost,
                                                            WaterLevelControlStructureQuantity, WaterLevelControlStructureUnitCost,
                                                            OutletProtectionStructureQuantity, OutletProtectionStructureUnitCost);
            }
            set { ChangeAndNotify(ref _calcOtherItemsCost, value); }

        }


        private decimal _calcClayLinerCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcClayLinerCost
        {
            get { return PondsCalculations.CalcClayLinerCost(CalcClayLinerVolume, ClayLinerUnitCost); }
            set { ChangeAndNotify(ref _calcClayLinerCost, value); }
        }

        private decimal _calcSyntheticLinerCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcSyntheticLinerCost
        {
            get { return PondsCalculations.CalcSyntheticLinerCost(CalcSyntheticLinerArea, SyntheticLinerUnitCost); }
            set { ChangeAndNotify(ref _calcSyntheticLinerCost, value); }
        }

        private decimal _calcGeosyntheticClayLinerCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcGeosyntheticClayLinerCost
        {
            get { return PondsCalculations.CalcGeosyntheticClayLinerCost(CalcGeosyntheticClayLinerArea, GeosyntheticClayLinerUnitCost); }
            set { ChangeAndNotify(ref _calcGeosyntheticClayLinerCost, value); }
        }

        private decimal _calcGeosyntheticClayLinerCoverCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcGeosyntheticClayLinerCoverCost
        {
            get { return PondsCalculations.CalcGeosyntheticClayLinerCoverCost(CalcGeosyntheticClayLinerVolume, GeosyntheticClayLinerCoverUnitCost); }
            set { ChangeAndNotify(ref _calcGeosyntheticClayLinerCoverCost, value); }
        }

        private decimal _calcLinerCost;
        /// <summary>
        /// Calculated.
        /// </summary>
        public decimal CalcLinerCost
        {
            get
            {
                switch (LinerOptionsProperty)
                {
                    case RadioButtonsLinerOptionsEnum.OptionNoLiner:
                        if (NonWovenGeotextileOption)
                        {
                            _calcLinerCost = 0 + CalcNonWovenGeotextileCost;
                        }
                        else
                        {
                            _calcLinerCost = 0;
                        }
                        break;

                    case RadioButtonsLinerOptionsEnum.OptionClayLiner:
                        if (NonWovenGeotextileOption)
                        {
                            _calcLinerCost = CalcClayLinerCost + CalcNonWovenGeotextileCost;
                        }
                        else
                        {
                            _calcLinerCost = CalcClayLinerCost;
                        }
                        break;

                    case RadioButtonsLinerOptionsEnum.OptionSyntheticLiner:
                        if (NonWovenGeotextileOption)
                        {
                            _calcLinerCost = CalcSyntheticLinerCost + CalcNonWovenGeotextileCost;
                        }
                        else
                        {
                            _calcLinerCost = CalcSyntheticLinerCost;
                        }
                        break;

                    case RadioButtonsLinerOptionsEnum.OptionGeosyntheticClayLiner:
                        if (NonWovenGeotextileOption)
                        {
                            _calcLinerCost = CalcGeosyntheticClayLinerCost + CalcGeosyntheticClayLinerCoverCost + CalcNonWovenGeotextileCost;
                        }
                        else
                        {
                            _calcLinerCost = CalcGeosyntheticClayLinerCost + CalcGeosyntheticClayLinerCoverCost;
                        }
                        break;
                }

                return _calcLinerCost;

            }
            set { ChangeAndNotify(ref _calcLinerCost, value); }
        }

        private decimal _calcBaffleCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcBaffleCost
        {
            get
            {
                if (IsBaffle)
                {
                    _calcBaffleCost = PondsCalculations.CalcBaffleCost(CalcBaffleLength, BaffleUnitCost);
                }
                else
                {
                    _calcBaffleCost = 0m;
                }
                return _calcBaffleCost;
            }
            set { ChangeAndNotify(ref _calcBaffleCost, value); }
        }

        private decimal _calcBoreholeCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcBoreholeCost
        {
            get
            {
                switch (SludgeDisposalOptionsProperty)
                {
                    case RadioButtonsSludgeDisposalOptionsEnum.OptionTriaxle:
                        _calcBoreholeCost = 0m;
                        break;
                    case RadioButtonsSludgeDisposalOptionsEnum.OptionVacuumTruck:
                        _calcBoreholeCost = 0m;
                        break;
                    case RadioButtonsSludgeDisposalOptionsEnum.OptionBorehole:
                        _calcBoreholeCost = PondsCalculations.CalcBoreholeCost(SludgeDisposalBoreholeDepth, SludgeDisposalBoreholeUnitCost);
                        break;
                    case RadioButtonsSludgeDisposalOptionsEnum.OptionGeotube:
                        _calcBoreholeCost = 0m;
                        break;
                    default:
                        break;
                }
                return _calcBoreholeCost;
            }
            set { ChangeAndNotify(ref _calcBoreholeCost, value); }
        }

        private decimal _calcOtherCapitalItemsCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcOtherCapitalItemsCost
        {
            get
            {
                return PondsCalculations.CalcOtherCapitalItemsCost(OtherCapitalItemQuantity1, OtherCapitalItemUnitCost1,
                                                                  OtherCapitalItemQuantity2, OtherCapitalItemUnitCost2,
                                                                  OtherCapitalItemQuantity3, OtherCapitalItemUnitCost3,
                                                                  OtherCapitalItemQuantity4, OtherCapitalItemUnitCost4,
                                                                  OtherCapitalItemQuantity5, OtherCapitalItemUnitCost5);
            }
            set { ChangeAndNotify(ref _calcOtherCapitalItemsCost, value); }

        }

        private decimal _calcCapitalCostTotal;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcCapitalCostTotal
        {
            get
            {
                _calcCapitalCostTotal = PondsCalculations.CalcCapitalCostTotal(CalcExcavationCost, CalcLinerCost, CalcPipeCost, CalcOtherItemsCost, CalcBaffleCost, CalcOtherCapitalItemsCost, CalcBoreholeCost);

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

        #region Properties - Annual (Operation and Maintenance) Costs

        public enum RadioButtonsAnnualCostOperationAndMaintenanceOptionsEnum
        {
            OptionAnnualCostMultiplier,
            OptionAnnualCostFlatFee
        }

        private RadioButtonsAnnualCostOperationAndMaintenanceOptionsEnum _annualCostOperationAndMaintenanceOptionsProperty;
        /// <summary>
        ///  User specified 
        /// </summary>
        public RadioButtonsAnnualCostOperationAndMaintenanceOptionsEnum AnnualCostOperationAndMaintenanceOptionsProperty
        {
            get { return _annualCostOperationAndMaintenanceOptionsProperty; }
            set { ChangeAndNotify(ref _annualCostOperationAndMaintenanceOptionsProperty, value, nameof(AnnualCostOperationAndMaintenanceOptionsProperty), CalcPropertiesStringArray); }
        }

        private double _annualCostMultiplier;
        /// <summary>
        /// User specified
        /// </summary>
        public double AnnualCostMultiplier
        {
            get { return _annualCostMultiplier; }
            set { ChangeAndNotify(ref _annualCostMultiplier, value, nameof(AnnualCostMultiplier), CalcPropertiesStringArray); }
        }

        private decimal _annualCostFlatFee;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal AnnualCostFlatFee
        {
            get { return _annualCostFlatFee; }
            set { ChangeAndNotify(ref _annualCostFlatFee, value, nameof(AnnualCostFlatFee), CalcPropertiesStringArray); }
        }

        private decimal _calcAnnualCostOperationAndMaintenanceEstimate;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcAnnualCostOperationAndMaintenanceEstimate
        {
            get { return PondsCalculations.CalcAnnualCostOperationAndMaintenance(AnnualCostMultiplier, CalcCapitalCostTotal); }
            set { ChangeAndNotify(ref _calcAnnualCostOperationAndMaintenanceEstimate, value); }
        }

        private decimal _calcAnnualCostOperationAndMaintenance;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcAnnualCostOperationAndMaintenance
        {
            get
            {
                switch (AnnualCostOperationAndMaintenanceOptionsProperty)
                {
                    case RadioButtonsAnnualCostOperationAndMaintenanceOptionsEnum.OptionAnnualCostMultiplier:
                        _calcAnnualCostOperationAndMaintenance = CalcAnnualCostOperationAndMaintenanceEstimate; 
                        break;
                    case RadioButtonsAnnualCostOperationAndMaintenanceOptionsEnum.OptionAnnualCostFlatFee:
                        _calcAnnualCostOperationAndMaintenance = AnnualCostFlatFee;
                        break;
                }

                return _calcAnnualCostOperationAndMaintenance;
            }
            set { ChangeAndNotify(ref _calcAnnualCostOperationAndMaintenance, value); }
        }

        public enum RadioButtonsAnnualCostSludgeHandlingOptionsEnum
        {
            OptionAnnualCostSludgeHandlingEstimated,
            OptionAnnualCostSludgeHandlingUserSpecified
        }

        private RadioButtonsAnnualCostSludgeHandlingOptionsEnum _annualCostSludgeHandlingOptionsProperty;
        /// <summary>
        ///  User specified 
        /// </summary>
        public RadioButtonsAnnualCostSludgeHandlingOptionsEnum AnnualCostSludgeHandlingOptionsProperty
        {
            get { return _annualCostSludgeHandlingOptionsProperty; }
            set { ChangeAndNotify(ref _annualCostSludgeHandlingOptionsProperty, value, nameof(AnnualCostSludgeHandlingOptionsProperty), CalcPropertiesStringArray); }
        }

        private decimal _calcAnnualCostSludgeHandlingEstimate;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcAnnualCostSludgeHandlingEstimate
        {
            get { return PondsCalculations.CalcAnnualCostSludgeHandling(CalcMobilizationDemobilizationCost, CalcSludgeRemovalCostPerGallon, CalcVacuumTruckHandlingCost, CalcMechanicalExcavationCost, CalcPumpCost); }
            set { ChangeAndNotify(ref _calcAnnualCostSludgeHandlingEstimate, value); }
        }

        private decimal _annualCostSludgeHandlingUserSpecified;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal AnnualCostSludgeHandlingUserSpecified
        {
            get { return _annualCostSludgeHandlingUserSpecified; }
            set { ChangeAndNotify(ref _annualCostSludgeHandlingUserSpecified, value, nameof(AnnualCostSludgeHandlingUserSpecified), CalcPropertiesStringArray); }
        }

        private decimal _calcAnnualCostSludgeHandling;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcAnnualCostSludgeHandling
        {
            get
            {
                switch (AnnualCostSludgeHandlingOptionsProperty)
                {
                    case RadioButtonsAnnualCostSludgeHandlingOptionsEnum.OptionAnnualCostSludgeHandlingEstimated:
                        _calcAnnualCostSludgeHandling = CalcAnnualCostSludgeHandlingEstimate;
                        break;
                    case RadioButtonsAnnualCostSludgeHandlingOptionsEnum.OptionAnnualCostSludgeHandlingUserSpecified:
                        _calcAnnualCostSludgeHandling = AnnualCostSludgeHandlingUserSpecified;
                        break;
                }

                return _calcAnnualCostSludgeHandling;
            }
            set { ChangeAndNotify(ref _calcAnnualCostSludgeHandling, value); }
        }


        public enum RadioButtonsAnnualCostSludgeDisposalOptionsEnum
        {
            OptionAnnualCostSludgeDisposalEstimated,
            OptionAnnualCostSludgeDisposalUserSpecified
        }

        private RadioButtonsAnnualCostSludgeDisposalOptionsEnum _annualCostSludgeDisposalOptionsProperty;
        /// <summary>
        ///  User specified 
        /// </summary>
        public RadioButtonsAnnualCostSludgeDisposalOptionsEnum AnnualCostSludgeDisposalOptionsProperty
        {
            get { return _annualCostSludgeDisposalOptionsProperty; }
            set { ChangeAndNotify(ref _annualCostSludgeDisposalOptionsProperty, value, nameof(AnnualCostSludgeDisposalOptionsProperty), CalcPropertiesStringArray); }
        }

        private decimal _calcAnnualCostSludgeDisposalEstimate;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcAnnualCostSludgeDisposalEstimate
        {
            get { return CalcSludgeDisposalCost + CalcTipCost; }
            set { ChangeAndNotify(ref _calcAnnualCostSludgeDisposalEstimate, value); }
        }

        private decimal _annualCostSludgeDisposalUserSpecified;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal AnnualCostSludgeDisposalUserSpecified
        {
            get { return _annualCostSludgeDisposalUserSpecified; }
            set { ChangeAndNotify(ref _annualCostSludgeDisposalUserSpecified, value, nameof(AnnualCostSludgeDisposalUserSpecified), CalcPropertiesStringArray); }
        }

        private decimal _calcAnnualCostSludgeDisposal;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcAnnualCostSludgeDisposal
        {
            get
            {
                switch (AnnualCostSludgeDisposalOptionsProperty)
                {
                    case RadioButtonsAnnualCostSludgeDisposalOptionsEnum.OptionAnnualCostSludgeDisposalEstimated:
                        _calcAnnualCostSludgeDisposal = CalcAnnualCostSludgeDisposalEstimate;
                        break;
                    case RadioButtonsAnnualCostSludgeDisposalOptionsEnum.OptionAnnualCostSludgeDisposalUserSpecified:
                        _calcAnnualCostSludgeDisposal = AnnualCostSludgeDisposalUserSpecified;
                        break;
                }

                return _calcAnnualCostSludgeDisposal;
            }
            set { ChangeAndNotify(ref _calcAnnualCostSludgeDisposal, value); }
        }

        
        private decimal _calcAnnualCostOtherAnnualItems;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcAnnualCostOtherAnnualItems
        {
            get
            {
                return PondsCalculations.CalcAnnualCostOtherAnnualItems(OtherAnnualItemQuantity1, OtherAnnualItemUnitCost1,
                                                                        OtherAnnualItemQuantity2, OtherAnnualItemUnitCost2,
                                                                        OtherAnnualItemQuantity3, OtherAnnualItemUnitCost3,
                                                                        OtherAnnualItemQuantity4, OtherAnnualItemUnitCost4,
                                                                        OtherAnnualItemQuantity5, OtherAnnualItemUnitCost5);
            }
            set { ChangeAndNotify(ref _calcAnnualCostOtherAnnualItems, value); }

        }

        private decimal _calcAnnualCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcAnnualCost
        {
            get
            {
                _calcAnnualCost = PondsCalculations.CalcAnnualCost(CalcAnnualCostOperationAndMaintenance, CalcAnnualCostSludgeHandling, CalcAnnualCostSludgeDisposal, CalcAnnualCostOtherAnnualItems);

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

        private double _recapitalizationCostLifeCyclePipe;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostLifeCyclePipe
        {
            get { return _recapitalizationCostLifeCyclePipe; }
            set { ChangeAndNotify(ref _recapitalizationCostLifeCyclePipe, value, nameof(RecapitalizationCostLifeCyclePipe), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostLifeCycleLiner;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostLifeCycleLiner
        {
            get { return _recapitalizationCostLifeCycleLiner; }
            set { ChangeAndNotify(ref _recapitalizationCostLifeCycleLiner, value, nameof(RecapitalizationCostLifeCycleLiner), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostLifeCycleOtherItems;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostLifeCycleOtherItems
        {
            get { return _recapitalizationCostLifeCycleOtherItems; }
            set { ChangeAndNotify(ref _recapitalizationCostLifeCycleOtherItems, value, nameof(RecapitalizationCostLifeCycleOtherItems), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostLifeCycleOtherCapitalItems;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostLifeCycleOtherCapitalItems
        {
            get { return _recapitalizationCostLifeCycleOtherCapitalItems; }
            set { ChangeAndNotify(ref _recapitalizationCostLifeCycleOtherCapitalItems, value, nameof(RecapitalizationCostLifeCycleOtherCapitalItems), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostLifeCycleBorehole;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostLifeCycleBorehole
        {
            get { return _recapitalizationCostLifeCycleBorehole; }
            set { ChangeAndNotify(ref _recapitalizationCostLifeCycleBorehole, value, nameof(RecapitalizationCostLifeCycleBorehole), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostPercentReplacementPipe;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostPercentReplacementPipe
        {
            get { return _recapitalizationCostPercentReplacementPipe; }
            set { ChangeAndNotify(ref _recapitalizationCostPercentReplacementPipe, value, nameof(RecapitalizationCostPercentReplacementPipe), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostPercentReplacementLiner;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostPercentReplacementLiner
        {
            get { return _recapitalizationCostPercentReplacementLiner; }
            set { ChangeAndNotify(ref _recapitalizationCostPercentReplacementLiner, value, nameof(RecapitalizationCostPercentReplacementLiner), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostPercentReplacementOtherItems;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostPercentReplacementOtherItems
        {
            get { return _recapitalizationCostPercentReplacementOtherItems; }
            set { ChangeAndNotify(ref _recapitalizationCostPercentReplacementOtherItems, value, nameof(RecapitalizationCostPercentReplacementOtherItems), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostPercentReplacementOtherCapitalItems;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostPercentReplacementOtherCapitalItems
        {
            get { return _recapitalizationCostPercentReplacementOtherCapitalItems; }
            set { ChangeAndNotify(ref _recapitalizationCostPercentReplacementOtherCapitalItems, value, nameof(RecapitalizationCostPercentReplacementOtherCapitalItems), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostPercentReplacementBorehole;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostPercentReplacementBorehole
        {
            get { return _recapitalizationCostPercentReplacementBorehole; }
            set { ChangeAndNotify(ref _recapitalizationCostPercentReplacementBorehole, value, nameof(RecapitalizationCostPercentReplacementBorehole), CalcPropertiesStringArray); }
        }

        private decimal _calcRapitalizationCostPipe;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostPipe
        {
            get
            {
                return PondsCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
                                                                             RecapitalizationCostInflationRate, RecapitalizationCostLifeCyclePipe,
                                                                             CalcPipeCost, RecapitalizationCostPercentReplacementPipe);
            }
            set { ChangeAndNotify(ref _calcRapitalizationCostPipe, value); }
        }

        private decimal _calcRapitalizationCostLiner;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostLiner
        {
            get
            {
                return PondsCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
                                                                             RecapitalizationCostInflationRate, RecapitalizationCostLifeCycleLiner,
                                                                             CalcLinerCost, RecapitalizationCostPercentReplacementLiner);
            }
            set { ChangeAndNotify(ref _calcRapitalizationCostLiner, value); }
        }


        private decimal _calcRapitalizationCostOtherItems;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostOtherItems
        {
            get
            {
                return PondsCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
                                                                             RecapitalizationCostInflationRate, RecapitalizationCostLifeCycleOtherItems,
                                                                             CalcOtherItemsCost, RecapitalizationCostPercentReplacementOtherItems);
            }
            set { ChangeAndNotify(ref _calcRapitalizationCostOtherItems, value); }
        }

        private decimal _calcRapitalizationCostOtherCapitalItems;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostOtherCapitalItems
        {
            get
            {
                return PondsCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
                                                                             RecapitalizationCostInflationRate, RecapitalizationCostLifeCycleOtherCapitalItems,
                                                                             CalcOtherCapitalItemsCost, RecapitalizationCostPercentReplacementOtherCapitalItems);
            }
            set { ChangeAndNotify(ref _calcRapitalizationCostOtherCapitalItems, value); }
        }

        private decimal _calcRapitalizationCostBorehole;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostBorehole
        {
            get
            {
                return PondsCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
                                                                             RecapitalizationCostInflationRate, RecapitalizationCostLifeCycleBorehole,
                                                                             CalcBoreholeCost, RecapitalizationCostPercentReplacementBorehole);
            }
            set { ChangeAndNotify(ref _calcRapitalizationCostBorehole, value); }
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
            get { return PondsCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn, RecapitalizationCostInflationRate, 1.0, CalcAnnualCost, 100.0); }
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

            CalcRecapitalizationCostTotal = selectedItemsTotals.Sum();
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
            set
            {
                ChangeAndNotify(ref _recapCostData, value, nameof(RecapCostData));
            }
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
                    case "Pipe":
                        item.MaterialCostDefault = CalcPipeCost;
                        break;
                    case "Liner":
                        item.MaterialCostDefault = CalcLinerCost;
                        break;
                    case "OtherItems":
                        item.MaterialCostDefault = CalcOtherItemsCost;
                        break;
                    case "OtherCapitalItems":
                        item.MaterialCostDefault = CalcOtherCapitalItemsCost;
                        break;
                    case "Borehole":
                        item.MaterialCostDefault = CalcBoreholeCost;
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
                    case "Pipe":
                        item.MaterialCostDefault = CalcRecapitalizationCostPipe;
                        break;
                    case "Liner":
                        item.MaterialCostDefault = CalcRecapitalizationCostLiner;
                        break;
                    case "OtherItems":
                        item.MaterialCostDefault = CalcRecapitalizationCostOtherItems;
                        break;
                    case "OtherCapitalItems":
                        item.MaterialCostDefault = CalcRecapitalizationCostOtherCapitalItems;
                        break;
                    case "Borehole":
                        item.MaterialCostDefault = CalcRecapitalizationCostBorehole;
                        break;
                    case "Annual":
                        item.MaterialCostDefault = CalcRecapitalizationCostAnnual;
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
                Name = "Pipe",
                NameFixed = "Pipe",
                LifeCycle = RecapitalizationCostLifeCyclePipe,
                PercentReplacement = RecapitalizationCostPercentReplacementPipe,
                MaterialCostDefault = CalcPipeCost,
                UseCustomCost = false,
                MaterialCostCustom = 0m,
                TotalCost = CalcRecapitalizationCostPipe
            });
            RecapMaterials.Add(new RecapMaterial
            {
                IsSelected = true,
                Name = "Liner",
                NameFixed = "Liner",
                LifeCycle = RecapitalizationCostLifeCycleLiner,
                PercentReplacement = RecapitalizationCostPercentReplacementLiner,
                MaterialCostDefault = CalcLinerCost,
                UseCustomCost = false,
                MaterialCostCustom = 0m,
                TotalCost = CalcRecapitalizationCostLiner
            });
            RecapMaterials.Add(new RecapMaterial
            {
                IsSelected = true,
                Name = "Other Items",
                NameFixed = "OtherItems",
                LifeCycle = RecapitalizationCostLifeCycleOtherItems,
                PercentReplacement = RecapitalizationCostPercentReplacementOtherItems,
                MaterialCostDefault = CalcOtherItemsCost,
                UseCustomCost = false,
                MaterialCostCustom = 0m,
                TotalCost = CalcRecapitalizationCostOtherItems
            });
            RecapMaterials.Add(new RecapMaterial
            {
                IsSelected = true,
                Name = "Other Capital Items",
                NameFixed = "OthercapitalItems",
                LifeCycle = RecapitalizationCostLifeCycleOtherCapitalItems,
                PercentReplacement = RecapitalizationCostPercentReplacementOtherCapitalItems,
                MaterialCostDefault = CalcOtherCapitalItemsCost,
                UseCustomCost = false,
                MaterialCostCustom = 0m,
                TotalCost = CalcRecapitalizationCostOtherCapitalItems
            });
            RecapMaterials.Add(new RecapMaterial
            {
                IsSelected = true,
                Name = "Borehole",
                NameFixed = "Borehole",
                LifeCycle = RecapitalizationCostLifeCycleBorehole,
                PercentReplacement = RecapitalizationCostPercentReplacementBorehole,
                MaterialCostDefault = CalcBoreholeCost,
                UseCustomCost = false,
                MaterialCostCustom = 0m,
                TotalCost = CalcRecapitalizationCostBorehole
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
            ((RecapMaterial)sender).TotalCost = PondsCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
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
        public ICommand SetGeotubeCommand { get; }
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
            var customDialog = new CustomDialog() { Title = "About Ponds" };

            var customDialogViewModel = new CustomDialogViewModel(instance =>
            {
                _dialogCoordinator.HideMetroDialogAsync(this, customDialog);
            });
            customDialogViewModel.Message = Resources.infoPONDS;
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
                        string message = Resources.infoWaterQualityPONDS;
                        await _dialogCoordinator.ShowMessageAsync(this, "Water Quality", message);
                    }
                });
            }
        }

        private ICommand _showMessageDialogCommandSizingMethods;
        public ICommand ShowMessageDialogCommandSizingMethods
        {
            get
            {
                return _showMessageDialogCommandSizingMethods ?? (this._showMessageDialogCommandSizingMethods = new SimpleCommand
                {
                    CanExecuteDelegate = x => true,
                    ExecuteDelegate = async x =>
                    {
                        string message = Resources.infoSizingMethodsPONDS;
                        await _dialogCoordinator.ShowMessageAsync(this, "Sizing Methods", message);
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
                        string message = Resources.infoSystemPropertiesPONDS;
                        await _dialogCoordinator.ShowMessageAsync(this, "System Properties", message);
                    }
                });
            }
        }

        private ICommand _showMessageDialogCommandLayerMaterials;
        public ICommand ShowMessageDialogCommandLayerMaterials
        {
            get
            {
                return _showMessageDialogCommandLayerMaterials ?? (this._showMessageDialogCommandLayerMaterials = new SimpleCommand
                {
                    CanExecuteDelegate = x => true,
                    ExecuteDelegate = async x =>
                    {
                        string message = Resources.infoLayerMaterialsPONDS;
                        await _dialogCoordinator.ShowMessageAsync(this, "Layer Materials", message);
                    }
                });
            }
        }

        private ICommand _showMessageDialogCommandOtherItems;
        public ICommand ShowMessageDialogCommandOtherItems
        {
            get
            {
                return _showMessageDialogCommandOtherItems ?? (this._showMessageDialogCommandOtherItems = new SimpleCommand
                {
                    CanExecuteDelegate = x => true,
                    ExecuteDelegate = async x =>
                    {
                        string message = Resources.infoOtherItemsPONDS;
                        await _dialogCoordinator.ShowMessageAsync(this, "Other Items", message);
                    }
                });
            }
        }

        private ICommand _showMessageDialogCommandOtherItemsCapital;
        public ICommand ShowMessageDialogCommandOtherItemsCapital
        {
            get
            {
                return _showMessageDialogCommandOtherItemsCapital ?? (this._showMessageDialogCommandOtherItemsCapital = new SimpleCommand
                {
                    CanExecuteDelegate = x => true,
                    ExecuteDelegate = async x =>
                    {
                        string message = Resources.infoOtherItemsCapitalPONDS;
                        await _dialogCoordinator.ShowMessageAsync(this, "Other Items", message);
                    }
                });
            }
        }

        private ICommand _showMessageDialogCommandOtherItemsAnnual;
        public ICommand ShowMessageDialogCommandOtherItemsAnnual
        {
            get
            {
                return _showMessageDialogCommandOtherItemsAnnual ?? (this._showMessageDialogCommandOtherItemsAnnual = new SimpleCommand
                {
                    CanExecuteDelegate = x => true,
                    ExecuteDelegate = async x =>
                    {
                        string message = Resources.infoOtherItemsAnnualPONDS;
                        await _dialogCoordinator.ShowMessageAsync(this, "Other Items", message);
                    }
                });
            }
        }


        private ICommand _showMessageDialogCommandSludgeGenerationHandlingDisposal;
        public ICommand ShowMessageDialogCommandSludgeGenerationHandlingDisposal
        {
            get
            {
                return _showMessageDialogCommandSludgeGenerationHandlingDisposal ?? (this._showMessageDialogCommandSludgeGenerationHandlingDisposal = new SimpleCommand
                {
                    CanExecuteDelegate = x => true,
                    ExecuteDelegate = async x =>
                    {
                        string message = Resources.infoSludgeGenerationHandlingDisposalPONDS;
                        await _dialogCoordinator.ShowMessageAsync(this, "Sludge", message);
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
                        string message = Resources.infoSizingSummaryPONDS;
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
                        string message = Resources.infoCapitalCostPONDS;
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
                        string message = Resources.infoAnnualCostPONDS;
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
                        string message = Resources.infoRecapitalizationCostPONDS;
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

        /// <summary>
        /// Assign/restore a previous value of the bottom length to width ratio when its value
        /// equals the calculated bottom length to width ratio which occurs when the dimensions 
        /// sizing method is chosen.
        /// </summary>
        /// <param name="ratio">The current bottom length to width ratio value, a user specified or calculated value</param>
        /// <returns>The bottom length to width ratio</returns>
        private double AssignBottomLengthToWidthRatio(double ratio)
        {
            switch (SizingMethodsOptionsProperty)
            {
                case RadioButtonsSizingMethodsOptionsEnum.OptionOxidationSettlingSludge:
                    // Update the backup copy
                    BottomLengthToWidthRatioBackup = ratio;
                    break;
                case RadioButtonsSizingMethodsOptionsEnum.OptionDimensions:
                    // Use the backup copy
                    ratio = BottomLengthToWidthRatioBackup;
                    break;
                default:
                    break;
            }

            return ratio;
        }


        public void SetGeotube(object geotube)
        {
            Geotube itemGeotube = (Geotube)geotube;

            switch (itemGeotube.Name)
            {
                case "25":
                    GeotubeName =GeotubeName25;
                    GeotubeVolume = GeotubeVolume25;
                    GeotubeUnitCost = GeotubeUnitCost25;
                    break;
                case "85":
                    GeotubeName = GeotubeName85;
                    GeotubeVolume = GeotubeVolume85;
                    GeotubeUnitCost = GeotubeUnitCost85;
                    break;
                case "175":
                    GeotubeName = GeotubeName175;
                    GeotubeVolume = GeotubeVolume175;
                    GeotubeUnitCost = GeotubeUnitCost175;
                    break;
                case "310":
                    GeotubeName = GeotubeName310;
                    GeotubeVolume = GeotubeVolume310;
                    GeotubeUnitCost = GeotubeUnitCost310;
                    break;
                case "465":
                    GeotubeName = GeotubeName465;
                    GeotubeVolume = GeotubeVolume465;
                    GeotubeUnitCost = GeotubeUnitCost465;
                    break;
                case "950":
                    GeotubeName = GeotubeName950;
                    GeotubeVolume = GeotubeVolume950;
                    GeotubeUnitCost = GeotubeUnitCost950;
                    break;
                case "1185":
                    GeotubeName = GeotubeName1185;
                    GeotubeVolume = GeotubeVolume1185;
                    GeotubeUnitCost = GeotubeUnitCost1185;
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region Error Handling and Information

        private double _calcMaxPondDepth;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcMaxPondDepth
        {
            get { return PondsCalculations.CalcMaxPondDepth(CalcFreeboardTopWidth); }
            set { ChangeAndNotify(ref _calcMaxPondDepth, value); }
        }

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

        private void CheckBottomWidthValue(double value)
        {
            if (value < 10.0 && value > 0.0)
            {
                ShowError();
            }
            else if (value <= 0.0)
            {
                ShowMajorError();
            }
            else
            {
                ShowNoError();
            }
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
            ErrorMessage = Resources.errorPondVFP;
            ErrorMessageShort = "ERROR - Pond cannot be constructed";
            IsExpandedFreeboard = true;
            IsExpandedWater = true;
            IsExpandedCompostMix = true;
            IsExpandedLimestone = true;
        }

        private void ShowMajorError()
        {
            IsError = true;
            IsMajorError = true;
            IsOpenFlyoutError = true;
            ErrorMessage = Resources.errorPondMajorVFP;
            ErrorMessageShort = "MAJOR ERROR - All calculations are invalid";
            IsExpandedFreeboard = true;
            IsExpandedWater = true;
            IsExpandedCompostMix = true;
            IsExpandedLimestone = true;
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

        private bool _isExpandedFreeboard;
        public bool IsExpandedFreeboard
        {
            get { return _isExpandedFreeboard; }
            set { ChangeAndNotify(ref _isExpandedFreeboard, value); }
        }

        private bool _isExpandedWater;
        public bool IsExpandedWater
        {
            get { return _isExpandedWater; }
            set { ChangeAndNotify(ref _isExpandedWater, value); }
        }

        private bool _isExpandedCompostMix;
        public bool IsExpandedCompostMix
        {
            get { return _isExpandedCompostMix; }
            set { ChangeAndNotify(ref _isExpandedCompostMix, value); }
        }

        private bool _isExpandedLimestone;
        public bool IsExpandedLimestone
        {
            get { return _isExpandedLimestone; }
            set { ChangeAndNotify(ref _isExpandedLimestone, value); }
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

        public PondsViewModel(IDialogCoordinator dialogCoordinator)
        {
            // MahApps dialog coordinator
            _dialogCoordinator = dialogCoordinator;

            // Assign the proper functions to the open and save commands
            OpenCommand = new RelayCommand(OpenFile);
            SaveCommand = new RelayCommand(SaveFile);
            HelpCommand = new RelayCommand(ShowHelp);
            SetGeotubeCommand = new RelayCommandWithParameter(SetGeotube);
            SyncCommand = new RelayCommand(SyncWithMainUi);

            // Get a list of property names and filter the names to include only those that start with "Calc" in order
            // to use with the NotifyAndChange.  This eliminates the need to write every property name that needs 
            // to be notified of changes by the user.
            PropertiesStringList = Helpers.GetNamesOfClassProperties(this);
            CalcPropertiesStringArray = Helpers.FilterPropertiesList(PropertiesStringList, "Calc");

            // Initialize the model name and user name
            ModuleType = "Ponds";
            ModuleName = "Untitled module";
            ModuleTreatmentType = "Passive";
            ModuleId = random.Next(1000);

            IsError = false;
            IsMajorError = false;
            ErrorMessage = "";
            ErrorMessageShort = "";

            // Initialize check boxes
            IsOxidation = true;
            IsSludge = true;
            PipeOptionAmdtreat = true;
            PipeOptionCustom = false;
            IsBaffle = true;
            NonWovenGeotextileOption = false;
            IsMobilizationDemobilization = true;
            IsSludgeHandlingDisposal = true;
            IsSludgeRemovalVacuumTruck = true;
            IsSludgeRemovalPump = true;
            IsSludgeRemovalPumpRental = false;
            IsMechanicalExcavation = true;
            IsLandfillTippingFee = true;

            // Initialize radio buttons
            SizingMethodsOptionsProperty = RadioButtonsSizingMethodsOptionsEnum.OptionOxidationSettlingSludge;
            BaffleOptionsProperty = RadioButtonsBaffleOptionsEnum.OptionEstimate;
            LinerOptionsProperty = RadioButtonsLinerOptionsEnum.OptionNoLiner;
            PumpOptionsProperty = RadioButtonsPumpOptionsEnum.OptionElectric;
            SludgeDisposalOptionsProperty = RadioButtonsSludgeDisposalOptionsEnum.OptionTriaxle;
            AnnualCostOperationAndMaintenanceOptionsProperty = RadioButtonsAnnualCostOperationAndMaintenanceOptionsEnum.OptionAnnualCostMultiplier;
            AnnualCostSludgeHandlingOptionsProperty = RadioButtonsAnnualCostSludgeHandlingOptionsEnum.OptionAnnualCostSludgeHandlingEstimated;
            AnnualCostSludgeDisposalOptionsProperty = RadioButtonsAnnualCostSludgeDisposalOptionsEnum.OptionAnnualCostSludgeDisposalEstimated;

            // Dummy variables to force syncing
            Dummy = 0;

            // Read the xml data file and assign property values accordingly.
            OpenXmlAndAssignValues(@"..\..\Data\default-data-ponds.xml");

            // Make an initial backup of the bottom length to width ratio
            BottomLengthToWidthRatioBackup = BottomLengthToWidthRatio;

            // Geotubes
            Geotubes = new List<Geotube>
            {
                new Geotube { Name = GeotubeName25, Volume = GeotubeVolume25, Cost = GeotubeUnitCost25},
                new Geotube { Name = GeotubeName85, Volume = GeotubeVolume85, Cost = GeotubeUnitCost85},
                new Geotube { Name = GeotubeName175, Volume = GeotubeVolume175, Cost = GeotubeUnitCost175},
                new Geotube { Name = GeotubeName310, Volume = GeotubeVolume310, Cost = GeotubeUnitCost310},
                new Geotube { Name = GeotubeName465, Volume = GeotubeVolume465, Cost = GeotubeUnitCost465},
                new Geotube { Name = GeotubeName950, Volume = GeotubeVolume950, Cost = GeotubeUnitCost950},
                new Geotube { Name = GeotubeName1185, Volume = GeotubeVolume1185, Cost = GeotubeUnitCost1185},
            };

            GeotubeName = GeotubeName25;
            GeotubeVolume = GeotubeVolume25;
            GeotubeUnitCost = GeotubeUnitCost25;

            // Recapitalization parameters that are set one time by a user within the main ui and are not shown in each module
            RecapitalizationCostCalculationPeriod = 75;
            RecapitalizationCostInflationRate = 5.0;
            RecapitalizationCostNetRateOfReturn = 8.0;
            InitRecapMaterials();
            CalcRecapMaterialsTotalCost();

            // Add method to shared data collection
            SharedDataCollection.CollectionChanged += SharedDataCollectionChanged;

        }

        #endregion

    }

}
