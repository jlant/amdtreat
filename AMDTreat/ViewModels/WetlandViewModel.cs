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

    public class WetlandViewModel : PropertyChangedBase, IObserver<SharedData>
    {

        #region Radio Button Enums

        /// <summary>
        ///  Radio button binding with enumeration for sizing methods
        /// </summary>
        public enum RadioButtonsSizingMethodsOptionsEnum
        {
            OptionRetentionTime,
            OptionMetalRemovalRates,
            OptionDimensions
        }

        /// <summary>
        /// Radio button binding with enumeration for annual costs
        /// </summary>
        public enum RadioButtonsAnnualCostOptionsEnum
        {
            OptionAnnualCostMultiplier,
            OptionAnnualCostFlatFee
        }

        /// <summary>
        /// Radio button binding for liner
        /// </summary>
        public enum RadioButtonsLinerOptionsEnum
        {
            OptionNoLiner,
            OptionClayLiner,
            OptionSyntheticLiner,
            OptionGeosyntheticClayLiner
        }

        #endregion

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

        private double _dissolvedIron;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double DissolvedIron
        {
            get { return _dissolvedIron; }
            set { ChangeAndNotify(ref _dissolvedIron, value, nameof(DissolvedIron), CalcPropertiesStringArray); }
        }

        private double _dissolvedManganese;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double DissolvedManganese
        {
            get { return _dissolvedManganese; }
            set { ChangeAndNotify(ref _dissolvedManganese, value, nameof(DissolvedManganese), CalcPropertiesStringArray); }
        }

        private double _netAcidity;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double NetAcidity
        {
            get
            {
                if (_netAcidity >= 0)
                {
                    ShowErrorMetalRemovalRate();
                    IronRemovalRate = 5;
                    ManganeseRemovalRate = 0.5;
                }
                else if (_netAcidity < 0)
                {
                    IronRemovalRate = 20;
                    ManganeseRemovalRate = 1.0;
                }
                else
                {
                    ShowNoError();
                }
                return _netAcidity;
            }
            set { ChangeAndNotify(ref _netAcidity, value, nameof(NetAcidity), CalcPropertiesStringArray); }
        }

        #endregion

        #region Properties - Sizing Methods: Radio Buttons 

        private RadioButtonsSizingMethodsOptionsEnum _sizingMethodsOptionsProperty;
        /// <summary>
        ///  User specified 
        /// </summary>
        public RadioButtonsSizingMethodsOptionsEnum SizingMethodsOptionsProperty
        {
            get { return _sizingMethodsOptionsProperty; }
            set { ChangeAndNotify(ref _sizingMethodsOptionsProperty, value, nameof(SizingMethodsOptionsProperty), CalcPropertiesStringArray); }
        }

        #endregion

        #region Properties - Sizing Methods: Retention Time

        private double _retentionTime;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double RetentionTime
        {
            get { return _retentionTime; }
            set { ChangeAndNotify(ref _retentionTime, value, nameof(RetentionTime), CalcPropertiesStringArray); }
        }

        private double _calcWaterVolumeRetentionTime;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcWaterVolumeRetentionTime
        {
            get { return WetlandCalculations.CalcWaterVolumeBasedOnRetentionTime(RetentionTime, DesignFlow); }
            set { ChangeAndNotify(ref _calcWaterVolumeRetentionTime, value); }
        }


        private double _calcWaterBottomLengthRetentionTime;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcWaterBottomLengthRetentionTime
        {
            get { return WetlandCalculations.CalcWaterLayerBottomLengthBasedOnRetentionTime(CalcWaterVolumeRetentionTime, FreeStandingWaterDepth, PondInsideSlope, BottomLengthToWidthRatio); }
            set { ChangeAndNotify(ref _calcWaterBottomLengthRetentionTime, value); }
        }

        private double _calcWaterBottomWidthRetentionTime;
        /// <summary>
        /// Calculated  
        /// Checking that the water bottom width value is not equal to zero.  If limestone bottom width value is equal to zero, then
        /// an error message is displayed. A value of 1.0 is returned in order to prevent calculations for the bottom length to width (CalcBottomLayerLengthToWidthRatio*)
        /// ratio from dividing by zero.
        /// </summary>
        public double CalcWaterBottomWidthRetentionTime
        {
            get
            {
                double value = WetlandCalculations.CalcWaterLayerBottomWidthBasedOnRetentionTime(CalcWaterBottomLengthRetentionTime, BottomLengthToWidthRatio);

                if (value == 0.0)
                {
                    ShowMajorError();

                    return 1.0;
                }
                return value;
            }

            set { ChangeAndNotify(ref _calcWaterBottomWidthRetentionTime, value); }
        }

        private double _calcWaterSurfaceAreaRetentionTime;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcWaterSurfaceAreaRetentionTime
        {
            get { return WetlandCalculations.CalcWaterSurfaceAreaBasedOnRetentionTime(CalcWaterBottomLengthRetentionTime, CalcWaterBottomWidthRetentionTime,
                                                                                      FreeStandingWaterDepth, PondInsideSlope); }
            set { ChangeAndNotify(ref _calcWaterSurfaceAreaRetentionTime, value); }
        }

        #endregion

        #region Properties - Sizing Methods: Metal Removal Rates

        private double _ironRemovalRate;
        /// <summary>
        /// User specified
        /// </summary>
        public double IronRemovalRate
        {
            get { return _ironRemovalRate; }
            set { ChangeAndNotify(ref _ironRemovalRate, value, nameof(IronRemovalRate), CalcPropertiesStringArray); }
        }

        private double _manganeseRemovalRate;
        /// <summary>
        /// User specified
        /// </summary>
        public double ManganeseRemovalRate
        {
            get { return _manganeseRemovalRate; }
            set { ChangeAndNotify(ref _manganeseRemovalRate, value, nameof(ManganeseRemovalRate), CalcPropertiesStringArray); }
        }

        private double _calcIronLoadingMetalRemovalRates;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcIronLoadingMetalRemovalRates
        {
            get { return WetlandCalculations.CalcMetalLoadingMetalRemovalRates(DesignFlow, DissolvedIron); }
            set { ChangeAndNotify(ref _calcIronLoadingMetalRemovalRates, value); }
        }

        private double _calcManganeseLoadingMetalRemovalRates;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcManganeseLoadingMetalRemovalRates
        {
            get { return WetlandCalculations.CalcMetalLoadingMetalRemovalRates(DesignFlow, DissolvedManganese); }
            set { ChangeAndNotify(ref _calcManganeseLoadingMetalRemovalRates, value); }
        }

        private double _calcIronSurfaceAreaMetalRemovalRates;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcIronSurfaceAreaMetalRemovalRates
        {
            get { return WetlandCalculations.CalcMetalSurfaceAreaMetalRemovalRates(CalcIronLoadingMetalRemovalRates, IronRemovalRate); }
            set { ChangeAndNotify(ref _calcIronSurfaceAreaMetalRemovalRates, value); }
        }

        private double _calcManganeseSurfaceAreaMetalRemovalRates;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcManganeseSurfaceAreaMetalRemovalRates
        {
            get { return WetlandCalculations.CalcMetalSurfaceAreaMetalRemovalRates(CalcManganeseLoadingMetalRemovalRates, ManganeseRemovalRate); }
            set { ChangeAndNotify(ref _calcManganeseSurfaceAreaMetalRemovalRates, value); }
        }

        private double _calcWaterSurfaceAreaMetalRemovalRates;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcWaterSurfaceAreaMetalRemovalRates
        {
            get { return WetlandCalculations.CalcWaterSurfaceAreaMetalRemovalRates(CalcIronSurfaceAreaMetalRemovalRates, CalcManganeseSurfaceAreaMetalRemovalRates); }
            set { ChangeAndNotify(ref _calcWaterSurfaceAreaMetalRemovalRates, value); }
        }


        #endregion

        #region Properties - Sizing Methods: Dimensions

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

        private double _calcWetlandFreeboardLengthDimensions;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcWetlandFreeboardLengthDimensions
        {
            get { return WetlandCalculations.CalcWetlandFreeboardLengthBasedOnDimensionsEntered(FreeboardTopLengthDimensions, InletPoolLength, OutletPoolLength); }
            set { ChangeAndNotify(ref _calcWetlandFreeboardLengthDimensions, value); }
        }

        private double _calcWetlandFreeboardSurfaceAreaDimensions;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcWetlandFreeboardSurfaceAreaDimensions
        {
            get { return WetlandCalculations.CalcWetlandFreeboardSurfaceAreaBasedOnDimensionsEntered(CalcWetlandFreeboardLengthDimensions, FreeboardTopWidthDimensions); }
            set { ChangeAndNotify(ref _calcWetlandFreeboardSurfaceAreaDimensions, value); }
        }


        private double _calcWetlandWaterTopLengthDimensions;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcWetlandWaterTopLengthDimensions
        {
            get { return WetlandCalculations.CalcWetlandWaterTopDimensionBasedOnDimensionsEntered(FreeboardTopLengthDimensions, FreeboardDepth, PondInsideSlope); }
            set { ChangeAndNotify(ref _calcWetlandWaterTopLengthDimensions, value); }
        }

        private double _calcWetlandWaterTopWidthDimensions;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcWetlandWaterTopWidthDimensions
        {
            get { return WetlandCalculations.CalcWetlandWaterTopDimensionBasedOnDimensionsEntered(FreeboardTopWidthDimensions, FreeboardDepth, PondInsideSlope); }
            set { ChangeAndNotify(ref _calcWetlandWaterTopWidthDimensions, value); }
        }


        private double _calcWetlandWaterSurfaceAreaDimensions;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcWetlandWaterSurfaceAreaDimensions
        {
            get { return WetlandCalculations.CalcWetlandWaterSurfaceAreaBasedOnDimensionsEntered(CalcWetlandFreeboardLengthDimensions, FreeboardTopWidthDimensions); }
            set { ChangeAndNotify(ref _calcWetlandWaterSurfaceAreaDimensions, value); }
        }

        private double _calcTotalSurfaceAreaTopFreeboardDimensions;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcTotalSurfaceAreaTopFreeboardDimensions
        {
            get { return WetlandCalculations.CalcLayerTopArea(FreeboardTopLengthDimensions, FreeboardTopWidthDimensions); }
            set { ChangeAndNotify(ref _calcTotalSurfaceAreaTopFreeboardDimensions, value); }
        }

        private double _calcWetlandBottomLengthDimensions;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcWetlandBottomLengthDimensions
        {
            get { return WetlandCalculations.CalcWetlandBottomDimensionBasedOnDimensionsEntered(CalcWetlandWaterTopLengthDimensions, FreeStandingWaterDepth, CompostMixDepth, PondInsideSlope); }
            set { ChangeAndNotify(ref _calcWetlandBottomLengthDimensions, value); }
        }

        private double _calcWetlandBottomWidthDimensions;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcWetlandBottomWidthDimensions
        {
            get { return WetlandCalculations.CalcWetlandBottomDimensionBasedOnDimensionsEntered(CalcWetlandWaterTopWidthDimensions, FreeStandingWaterDepth, CompostMixDepth, PondInsideSlope); }
            set { ChangeAndNotify(ref _calcWetlandBottomWidthDimensions, value); }
        }

        private double _calcBottomLengthToWidthRatioDimensions;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcBottomLengthToWidthRatioDimensions
        {
            get { return WetlandCalculations.CalcBottomLayerLengthToWidthRatioBasedOnDimensionsEntered(CalcWetlandBottomLengthDimensions, CalcWetlandBottomWidthDimensions); }
            set { ChangeAndNotify(ref _calcBottomLengthToWidthRatioDimensions, value); }
        }

        #endregion

        #region Properties - System Properties

        private double _pondInsideSlope;
        /// <summary>
        /// User specified
        /// </summary>
        public double PondInsideSlope
        {
            get { return _pondInsideSlope; }
            set { ChangeAndNotify(ref _pondInsideSlope, value, nameof(PondInsideSlope), CalcPropertiesStringArray); }
        }

        private double _bottomLengthToWidthRatio;
        /// <summary>
        /// User specified
        /// </summary>
        public double BottomLengthToWidthRatio
        {
            get { return _bottomLengthToWidthRatio; }
            set { ChangeAndNotify(ref _bottomLengthToWidthRatio, value, nameof(BottomLengthToWidthRatio), CalcPropertiesStringArray); }
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

        private double _inletPoolDepth;
        /// <summary>
        /// User specified
        /// </summary>
        public double InletPoolDepth
        {
            get { return _inletPoolDepth; }
            set { ChangeAndNotify(ref _inletPoolDepth, value, nameof(InletPoolDepth), CalcPropertiesStringArray); }
        }

        private double _outletPoolDepth;
        /// <summary>
        /// User specified
        /// </summary>
        public double OutletPoolDepth
        {
            get { return _outletPoolDepth; }
            set { ChangeAndNotify(ref _outletPoolDepth, value, nameof(OutletPoolDepth), CalcPropertiesStringArray); }
        }

        private double _inletPoolLength;
        /// <summary>
        /// User specified
        /// </summary>
        public double InletPoolLength
        {
            get { return _inletPoolLength; }
            set { ChangeAndNotify(ref _inletPoolLength, value, nameof(InletPoolLength), CalcPropertiesStringArray); }
        }

        private double _outletPoolLength;
        /// <summary>
        /// User specified
        /// </summary>
        public double OutletPoolLength
        {
            get { return _outletPoolLength; }
            set { ChangeAndNotify(ref _outletPoolLength, value, nameof(OutletPoolLength), CalcPropertiesStringArray); }
        }

        private bool _inletPoolLengthOption;
        /// <summary>
        /// User specified
        /// </summary>
        public bool InletPoolLengthOption
        {
            get { return _inletPoolLengthOption; }
            set { ChangeAndNotify(ref _inletPoolLengthOption, value, nameof(InletPoolLengthOption), CalcPropertiesStringArray); }
        }

        private bool _outletPoolLengthOption;
        /// <summary>
        /// User specified
        /// </summary>
        public bool OutletPoolLengthOption
        {
            get { return _outletPoolLengthOption; }
            set { ChangeAndNotify(ref _outletPoolLengthOption, value, nameof(OutletPoolLengthOption), CalcPropertiesStringArray); }
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

        #region Properties - Layer Materials: Water

         private double _freeStandingWaterDepth;
        /// <summary>
        /// User specified
        /// </summary>
        public double FreeStandingWaterDepth
        {
            get { return _freeStandingWaterDepth; }
            set { ChangeAndNotify(ref _freeStandingWaterDepth, value, nameof(FreeStandingWaterDepth), CalcPropertiesStringArray); }
        }

        #endregion

        #region Properties - Layer Materials: Compost Mix

        private double _compostMixDepth;
        /// <summary>
        /// User specified
        /// </summary>
        public double CompostMixDepth
        {
            get { return _compostMixDepth; }
            set { ChangeAndNotify(ref _compostMixDepth, value, nameof(CompostMixDepth), CalcPropertiesStringArray); }
        }

        private double _limestoneFinesPercentage;
        /// <summary>
        /// User specified
        /// </summary>
        public double LimestoneFinesPercentage
        {
            get { return _limestoneFinesPercentage; }
            set { ChangeAndNotify(ref _limestoneFinesPercentage, value, nameof(LimestoneFinesPercentage), CalcPropertiesStringArray); }
        }

        private double _limestoneFinesVoidSpace;
        /// <summary>
        /// User specified
        /// </summary>
        public double LimestoneFinesVoidSpace
        {
            get { return _limestoneFinesVoidSpace; }
            set { ChangeAndNotify(ref _limestoneFinesVoidSpace, value, nameof(LimestoneFinesVoidSpace), CalcPropertiesStringArray); }
        }

        private decimal _compostMixUnitCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal CompostMixUnitCost
        {
            get { return _compostMixUnitCost; }
            set { ChangeAndNotify(ref _compostMixUnitCost, value, nameof(CompostMixUnitCost), CalcPropertiesStringArray); }
        }

        private decimal _compostMixPlacementUnitCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal CompostMixPlacementUnitCost
        {
            get { return _compostMixPlacementUnitCost; }
            set { ChangeAndNotify(ref _compostMixPlacementUnitCost, value, nameof(CompostMixPlacementUnitCost), CalcPropertiesStringArray); }
        }

        private decimal _limestoneFinesUnitCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal LimestoneFinesUnitCost
        {
            get { return _limestoneFinesUnitCost; }
            set { ChangeAndNotify(ref _limestoneFinesUnitCost, value, nameof(LimestoneFinesUnitCost), CalcPropertiesStringArray); }
        }

        private decimal _wetlandPlantingUnitCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal WetlandPlantingUnitCost
        {
            get { return _wetlandPlantingUnitCost; }
            set { ChangeAndNotify(ref _wetlandPlantingUnitCost, value, nameof(WetlandPlantingUnitCost), CalcPropertiesStringArray); }
        }

        #endregion

        #region Properties - Layer Materials: Rock Baffles

        private double _rockBaffleSpace;
        /// <summary>
        /// User specified
        /// </summary>
        public double RockBaffleSpace
        {
            get { return _rockBaffleSpace; }
            set { ChangeAndNotify(ref _rockBaffleSpace, value, nameof(RockBaffleSpace), CalcPropertiesStringArray); }
        }

        private double _limestonePorosity;
        /// <summary>
        /// User specified
        /// </summary>
        public double LimestonePorosity
        {
            get { return _limestonePorosity; }
            set { ChangeAndNotify(ref _limestonePorosity, value, nameof(LimestonePorosity), CalcPropertiesStringArray); }
        }

        private decimal _limestoneUnitCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal LimestoneUnitCost
        {
            get { return _limestoneUnitCost; }
            set { ChangeAndNotify(ref _limestoneUnitCost, value, nameof(LimestoneUnitCost), CalcPropertiesStringArray); }
        }

        private decimal _limestonePlacementUnitCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal LimestonePlacementUnitCost
        {
            get { return _limestonePlacementUnitCost; }
            set { ChangeAndNotify(ref _limestonePlacementUnitCost, value, nameof(LimestonePlacementUnitCost), CalcPropertiesStringArray); }
        }

        #endregion

        #region Properties - Layer Materials: Liner

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

        private double _headerPipeSegmentLength;
        /// <summary>
        /// User specified
        /// </summary>
        public double HeaderPipeSegmentLength
        {
            get { return _headerPipeSegmentLength; }
            set { ChangeAndNotify(ref _headerPipeSegmentLength, value, nameof(HeaderPipeSegmentLength), CalcPropertiesStringArray); }
        }

        private double _headerPipeQuantity;
        /// <summary>
        /// User specified
        /// </summary>
        public double HeaderPipeQuantity
        {
            get { return _headerPipeQuantity; }
            set { ChangeAndNotify(ref _headerPipeQuantity, value, nameof(HeaderPipeQuantity), CalcPropertiesStringArray); }
        }

        private double _headerPipeInstallRate;
        /// <summary>
        /// User specified
        /// </summary>
        public double HeaderPipeInstallRate
        {
            get { return _headerPipeInstallRate; }
            set { ChangeAndNotify(ref _headerPipeInstallRate, value, nameof(HeaderPipeInstallRate), CalcPropertiesStringArray); }
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

        private decimal _inOutPipeUnitCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal InOutPipeUnitCost
        {
            get { return _inOutPipeUnitCost; }
            set { ChangeAndNotify(ref _inOutPipeUnitCost, value, nameof(InOutPipeUnitCost), CalcPropertiesStringArray); }
        }

        private decimal _headerPipeUnitCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal HeaderPipeUnitCost
        {
            get { return _headerPipeUnitCost; }
            set { ChangeAndNotify(ref _headerPipeUnitCost, value, nameof(HeaderPipeUnitCost), CalcPropertiesStringArray); }
        }

        private decimal _headerPipeCouplerUnitCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal HeaderPipeCouplerUnitCost
        {
            get { return _headerPipeCouplerUnitCost; }
            set { ChangeAndNotify(ref _headerPipeCouplerUnitCost, value, nameof(HeaderPipeCouplerUnitCost), CalcPropertiesStringArray); }
        }

        private decimal _teeConnectorUnitCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal TeeConnectorUnitCost
        {
            get { return _teeConnectorUnitCost; }
            set { ChangeAndNotify(ref _teeConnectorUnitCost, value, nameof(TeeConnectorUnitCost), CalcPropertiesStringArray); }
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

        #region Properties - Other Items

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

        #region Properties - Sizing Summary: Excavation Volume, Clear and Grub

        private double _calcExcavationVolume;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcExcavationVolume
        {
            get { return WetlandCalculations.CalcExcavationVolume(CalcCompostMixVolume, CalcFreeStandingWaterVolume, CalcInletPoolVolume, CalcOutletPoolVolume, CalcLinerVolume); }
            set { ChangeAndNotify(ref _calcExcavationVolume, value); }
        }

        private double _calcClearAndGrubArea;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcClearAndGrubArea
        {
            get { return WetlandCalculations.CalcClearAndGrubArea(CalcFreeboardTopLength, CalcFreeboardTopWidth, FreeboardDepth); }
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
                double totalDepth = FreeboardDepth + InletPoolLength + OutletPoolLength;
                double calculatedFreeboardTopLength = WetlandCalculations.CalcTopDimension(CalcFreeStandingWaterTopLength, totalDepth, PondInsideSlope);

                switch (SizingMethodsOptionsProperty)
                {
                    case RadioButtonsSizingMethodsOptionsEnum.OptionRetentionTime:
                        _calcFreeboardTopLength = calculatedFreeboardTopLength;
                        break;
                    case RadioButtonsSizingMethodsOptionsEnum.OptionMetalRemovalRates:
                        _calcFreeboardTopLength = calculatedFreeboardTopLength;
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
                double calculatedFreeboardTopWidth = WetlandCalculations.CalcTopDimension(CalcFreeStandingWaterTopWidth, FreeStandingWaterDepth, PondInsideSlope);

                switch (SizingMethodsOptionsProperty)
                {
                    case RadioButtonsSizingMethodsOptionsEnum.OptionRetentionTime:
                        _calcFreeboardTopWidth = calculatedFreeboardTopWidth;
                        break;
                    case RadioButtonsSizingMethodsOptionsEnum.OptionMetalRemovalRates:
                        _calcFreeboardTopWidth = calculatedFreeboardTopWidth;
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

        private double _calcFreeboardVolume;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcFreeboardVolume
        {
            get
            {
                return WetlandCalculations.CalcFreeboardVolume(CalcFreeboardTopLength, CalcFreeboardTopWidth, 
                                                               CalcWaterInletPoolTopLength, CalcWaterOutletPoolTopLength, 
                                                               CalcFreeStandingWaterTopLength, CalcFreeStandingWaterTopWidth, 
                                                               FreeboardDepth);
            }
            set { ChangeAndNotify(ref _calcFreeboardVolume, value); }
        }

        #endregion

        #region Properties - Sizing Summary: Water, Inlet Pool, Outlet Pool

        private double _waterSurfaceArea;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double WaterSurfaceArea
        {
            get { return _waterSurfaceArea; }
            set { ChangeAndNotify(ref _waterSurfaceArea, value, nameof(WaterSurfaceArea), CalcPropertiesStringArray); }
        }

        private double _calcWetlandBottomLength;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcWetlandBottomLength
        {
            get
            {
                switch (SizingMethodsOptionsProperty)
                {
                    case RadioButtonsSizingMethodsOptionsEnum.OptionRetentionTime:
                        BottomLengthToWidthRatio = AssignBottomLengthToWidthRatio(BottomLengthToWidthRatio);
                        WaterSurfaceArea = CalcWaterSurfaceAreaRetentionTime;
                        break;
                    case RadioButtonsSizingMethodsOptionsEnum.OptionMetalRemovalRates:
                        BottomLengthToWidthRatio = AssignBottomLengthToWidthRatio(BottomLengthToWidthRatio);
                        WaterSurfaceArea = CalcWaterSurfaceAreaMetalRemovalRates;
                        break;
                    case RadioButtonsSizingMethodsOptionsEnum.OptionDimensions:
                        BottomLengthToWidthRatio = CalcBottomLengthToWidthRatioDimensions;
                        WaterSurfaceArea = CalcWetlandWaterSurfaceAreaDimensions;
                        break;                 
                }

                return WetlandCalculations.CalcWetlandBottomLength(WaterSurfaceArea, FreeStandingWaterDepth, CompostMixDepth, BottomLengthToWidthRatio, PondInsideSlope);
            }
            set { ChangeAndNotify(ref _calcWetlandBottomLength, value); }
        }

        private double _calcWetlandBottomWidth;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcWetlandBottomWidth
        {
            get
            {
                double wetlandBottomWidth = WetlandCalculations.CalcWetlandBottomWidth(CalcWetlandBottomLength, BottomLengthToWidthRatio);

                if (wetlandBottomWidth < 10.0 &&  wetlandBottomWidth > 0.0)
                {
                    ShowError();
                }
                else if (wetlandBottomWidth <= 0.0)
                {
                    ShowMajorError();
                    wetlandBottomWidth = 1.0;
                }
                else
                {
                    ShowNoError();                   
                }

                return wetlandBottomWidth;
            }
            set { ChangeAndNotify(ref _calcWetlandBottomWidth, value); }
        }

        private double _calcFreeStandingWaterTopLength;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcFreeStandingWaterTopLength
        {
            get
            {
                double totalDepth = FreeStandingWaterDepth + CompostMixDepth;
                return WetlandCalculations.CalcTopDimension(CalcWetlandBottomLength, totalDepth, PondInsideSlope);
            }
            set { ChangeAndNotify(ref _calcFreeStandingWaterTopLength, value); }
        }

        private double _calcFreeStandingWaterTopWidth;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcFreeStandingWaterTopWidth
        {
            get
            {
                double totalDepth = FreeStandingWaterDepth + CompostMixDepth;
                return WetlandCalculations.CalcTopDimension(CalcWetlandBottomWidth, totalDepth, PondInsideSlope);
            }
            set { ChangeAndNotify(ref _calcFreeStandingWaterTopWidth, value); }
        }

        private double _calcFreeStandingWaterSurfaceArea;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcFreeStandingWaterSurfaceArea
        {
            get { return WetlandCalculations.CalcLayerTopArea(CalcFreeStandingWaterTopLength, CalcFreeStandingWaterTopWidth); }
            set { ChangeAndNotify(ref _calcFreeStandingWaterSurfaceArea, value); }
        }

        private double _calcFreeStandingWaterVolume;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcFreeStandingWaterVolume
        {
            get { return WetlandCalculations.CalcLayerVolume(CalcFreeStandingWaterTopLength, CalcFreeStandingWaterTopWidth, CalcCompostMixTopLength, CalcCompostMixTopWidth, FreeStandingWaterDepth); }
            set { ChangeAndNotify(ref _calcFreeStandingWaterVolume, value); }
        }


        private double _calcWaterInletPoolTopLength;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcWaterInletPoolTopLength
        {
            get { return WetlandCalculations.CalcPoolDimension(InletPoolLength, FreeboardDepth, PondInsideSlope); }
            set { ChangeAndNotify(ref _calcWaterInletPoolTopLength, value); }
        }

        private double _calcWaterOutletPoolTopLength;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcWaterOutletPoolTopLength
        {
            get { return WetlandCalculations.CalcPoolDimension(OutletPoolLength, FreeboardDepth, PondInsideSlope); }
            set { ChangeAndNotify(ref _calcWaterOutletPoolTopLength, value); }
        }

        private double _calcInletPoolBottomLength;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcInletPoolBottomLength
        {
            get
            {
                double inletBottomLength = WetlandCalculations.CalcPoolDimension(CalcWaterInletPoolTopLength, InletPoolDepth, PondInsideSlope);
                double checkValue = inletBottomLength + PondInsideSlope * (FreeboardDepth + InletPoolDepth) + PondInsideSlope * InletPoolDepth;

                if (inletBottomLength < 5.0)
                {
                    ShowErrorPoolBottomLength();
                }              
                else if (checkValue > InletPoolLength)
                {
                    ShowErrorPoolBottomLength();
                }
                else
                {
                    ShowNoErrorPoolBottomLength();
                }

                return inletBottomLength;
            }
            set { ChangeAndNotify(ref _calcInletPoolBottomLength, value); }
        }

        private double _calcOutletPoolBottomLength;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcOutletPoolBottomLength
        {
            get
            {
                double outletBottomLength = WetlandCalculations.CalcPoolDimension(CalcWaterOutletPoolTopLength, OutletPoolDepth, PondInsideSlope);
                double checkValue = outletBottomLength + PondInsideSlope * (FreeboardDepth + OutletPoolDepth) + PondInsideSlope * OutletPoolDepth;

                if (outletBottomLength < 5.0)
                {
                    ShowErrorPoolBottomLength();
                }
                else if (checkValue > OutletPoolLength)
                {
                    ShowNoErrorPoolBottomLength();
                }
                else
                {
                    ShowNoError();
                }

                return outletBottomLength;
            }
            set { ChangeAndNotify(ref _calcOutletPoolBottomLength, value); }
        }

        private double _calcInletPoolBottomWidth;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcInletPoolBottomWidth
        {
            get
            {
                double totalDepth = FreeboardDepth + InletPoolDepth;
                return WetlandCalculations.CalcPoolDimension(CalcFreeboardTopWidth, totalDepth, PondInsideSlope);
            }
            set { ChangeAndNotify(ref _calcInletPoolBottomWidth, value); }
        }

        private double _calcOutletPoolBottomWidth;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcOutletPoolBottomWidth
        {
            get
            {
                double totalDepth = FreeboardDepth + OutletPoolDepth;
                return WetlandCalculations.CalcPoolDimension(CalcFreeboardTopWidth, totalDepth, PondInsideSlope);
            }
            set { ChangeAndNotify(ref _calcOutletPoolBottomWidth, value); }
        }

        private double _calcInletPoolVolume;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcInletPoolVolume
        {
            get
            {
                if (InletPoolLengthOption)
                {
                    _calcInletPoolVolume = WetlandCalculations.CalcLayerVolume(CalcWaterInletPoolTopLength, CalcFreeStandingWaterTopWidth, CalcInletPoolBottomLength, CalcInletPoolBottomWidth, InletPoolDepth);
                }
                else
                {
                    _calcInletPoolVolume = 0;
                }
                return _calcInletPoolVolume;
            }
            set { ChangeAndNotify(ref _calcInletPoolVolume, value); }
        }

        private double _calcOutletPoolVolume;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcOutletPoolVolume
        {
            get
            {
                if (OutletPoolLengthOption)
                {
                    _calcOutletPoolVolume = WetlandCalculations.CalcLayerVolume(CalcWaterOutletPoolTopLength, CalcFreeStandingWaterTopWidth, CalcOutletPoolBottomLength, CalcOutletPoolBottomWidth, OutletPoolDepth);
                }
                else
                {
                    _calcOutletPoolVolume = 0;
                }
                return _calcOutletPoolVolume;
            }
            set { ChangeAndNotify(ref _calcOutletPoolVolume, value); }
        }

        private double _calcWaterVolumeTotal;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcWaterVolumeTotal
        {
            get { return WetlandCalculations.CalcWaterVolumeTotal(CalcFreeStandingWaterVolume, CalcInletPoolVolume, CalcOutletPoolVolume); }
            set { ChangeAndNotify(ref _calcWaterVolumeTotal, value); }
        }

        private double _calcInletPoolLinerSlopeLength;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcInletPoolLinerSlopeLength
        {
            get { return WetlandCalculations.CalcPoolLinerSlopeLength(InletPoolDepth, FreeboardDepth, PondInsideSlope); }
            set { ChangeAndNotify(ref _calcInletPoolLinerSlopeLength, value); }
        }

        private double _calcOutletPoolLinerSlopeLength;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcOutletPoolLinerSlopeLength
        {
            get { return WetlandCalculations.CalcPoolLinerSlopeLength(OutletPoolDepth, FreeboardDepth, PondInsideSlope); }
            set { ChangeAndNotify(ref _calcOutletPoolLinerSlopeLength, value); }
        }

        private double _calcInletPoolSyntheticLinerArea;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcInletPoolSyntheticLinerArea
        {
            get { return WetlandCalculations.CalcPoolSyntheticLinerArea(InletPoolLength, CalcInletPoolBottomLength, CalcInletPoolBottomWidth, CalcFreeboardTopWidth, CalcInletPoolLinerSlopeLength); }
            set { ChangeAndNotify(ref _calcInletPoolSyntheticLinerArea, value); }
        }

        private double _calcOutletPoolSyntheticLinerArea;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcOutletPoolSyntheticLinerArea
        {
            get { return WetlandCalculations.CalcPoolSyntheticLinerArea(OutletPoolLength, CalcOutletPoolBottomLength, CalcOutletPoolBottomWidth, CalcFreeboardTopWidth, CalcOutletPoolLinerSlopeLength); }
            set { ChangeAndNotify(ref _calcOutletPoolSyntheticLinerArea, value); }
        }

        private double _calcInletPoolClayLinerArea;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcInletPoolClayLinerArea
        {
            get { return WetlandCalculations.CalcPoolClayLinerArea(InletPoolLength, CalcInletPoolBottomLength, CalcInletPoolBottomWidth, CalcFreeboardTopWidth, CalcInletPoolLinerSlopeLength); }
            set { ChangeAndNotify(ref _calcInletPoolClayLinerArea, value); }
        }

        private double _calcOutletPoolClayLinerArea;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcOutletPoolClayLinerArea
        {
            get { return WetlandCalculations.CalcPoolClayLinerArea(OutletPoolLength, CalcOutletPoolBottomLength, CalcOutletPoolBottomWidth, CalcFreeboardTopWidth, CalcOutletPoolLinerSlopeLength); }
            set { ChangeAndNotify(ref _calcOutletPoolClayLinerArea, value); }
        }

        private double _calcInletPoolGeosyntheticClayLinerArea;
        /// <summary>
        /// Calculated.  Same as synthetic liner area.
        /// </summary>
        public double CalcInletPoolGeosyntheticClayLinerArea
        {
            get { return WetlandCalculations.CalcPoolGeosyntheticLinerArea(OutletPoolLength, CalcOutletPoolBottomLength, CalcOutletPoolBottomWidth, CalcFreeboardTopWidth, CalcOutletPoolLinerSlopeLength); }
            set { ChangeAndNotify(ref _calcInletPoolGeosyntheticClayLinerArea, value); }
        }

        private double _calcOutletPoolGeosyntheticClayLinerArea;
        /// <summary>
        /// Calculated.  Same as synthetic liner area.
        /// </summary>
        public double CalcOutletPoolGeosyntheticClayLinerArea
        {
            get { return WetlandCalculations.CalcPoolGeosyntheticLinerArea(OutletPoolLength, CalcOutletPoolBottomLength, CalcOutletPoolBottomWidth, CalcFreeboardTopWidth, CalcOutletPoolLinerSlopeLength); }
            set { ChangeAndNotify(ref _calcOutletPoolGeosyntheticClayLinerArea, value); }
        }


        #endregion

        #region Properties - Sizing Summary: Compost Mix

        private double _calcCompostMixTopLength;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcCompostMixTopLength
        {
            get { return WetlandCalculations.CalcTopDimension(CalcWetlandBottomLength, CompostMixDepth, PondInsideSlope); }
            set { ChangeAndNotify(ref _calcCompostMixTopLength, value); }
        }

        private double _calcCompostMixTopWidth;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcCompostMixTopWidth
        {
            get { return WetlandCalculations.CalcTopDimension(CalcWetlandBottomWidth, CompostMixDepth, PondInsideSlope); }
            set { ChangeAndNotify(ref _calcCompostMixTopWidth, value); }
        }

        private double _calcCompostMixTopArea;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcCompostMixTopArea
        {
            get { return WetlandCalculations.CalcLayerTopArea(CalcCompostMixTopLength, CalcCompostMixTopWidth); }
            set { ChangeAndNotify(ref _calcCompostMixTopArea, value); }
        }

        private double _calcCompostMixVolume;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcCompostMixVolume
        {
            get { return WetlandCalculations.CalcCompostMixVolume(CalcCompostMixTopLength, CalcCompostMixTopWidth, CalcWetlandBottomLength, CalcWetlandBottomWidth, CompostMixDepth); }
            set { ChangeAndNotify(ref _calcCompostMixVolume, value); }
        }

        private double _calcCompostMixLimestoneFinesBulkDensity;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcCompostMixLimestoneFinesBulkDensity
        {
            get { return WetlandCalculations.CalcBulkDensity(LimestoneFinesVoidSpace); }
            set { ChangeAndNotify(ref _calcCompostMixLimestoneFinesBulkDensity, value); }
        }

        private double _calcCompostMixLimestoneFinesVolume;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcCompostMixLimestoneFinesVolume
        {
            get { return WetlandCalculations.CalcCompostMixLimestoneFinesVolume(CalcCompostMixVolume, LimestoneFinesPercentage); }
            set { ChangeAndNotify(ref _calcCompostMixLimestoneFinesVolume, value); }
        }

        private double _calcCompostMixLimestoneFinesWeight;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcCompostMixLimestoneFinesWeight
        {
            get { return WetlandCalculations.CalcMaterialWeight(CalcCompostMixLimestoneFinesVolume, CalcCompostMixLimestoneFinesBulkDensity); }
            set { ChangeAndNotify(ref _calcCompostMixLimestoneFinesWeight, value); }
        }

        private double _calcCompostMixOrganicMaterialVolume;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcCompostMixOrganicMaterialVolume
        {
            get { return WetlandCalculations.CalcCompostMixLayerOrganicMaterialVolume(CalcCompostMixVolume, CalcCompostMixLimestoneFinesVolume); }
            set { ChangeAndNotify(ref _calcCompostMixOrganicMaterialVolume, value); }
        }

        private double _calcCompostMixPorosity;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcCompostMixPorosity
        {
            get { return WetlandCalculations.CalcCompostMixPorosity(LimestoneFinesPercentage); }
            set { ChangeAndNotify(ref _calcCompostMixPorosity, value); }
        }

        #endregion

        #region Properties - Sizing Summary: Limestone, Rock Baffles

        private double _calcLimestoneBulkDensity;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLimestoneBulkDensity
        {
            get { return WetlandCalculations.CalcBulkDensity(LimestonePorosity); }
            set { ChangeAndNotify(ref _calcLimestoneBulkDensity, value); }
        }

        private double _calcRockBaffleHeight;
        /// <summary>
        /// User specified
        /// </summary>
        public double CalcRockBaffleHeight
        {
            get
            { return WetlandCalculations.CalcRockBaffleHeight(FreeStandingWaterDepth, CompostMixDepth); }
            set { ChangeAndNotify(ref _calcRockBaffleHeight, value, nameof(CalcRockBaffleHeight), CalcPropertiesStringArray); }
        }

        private double _calcRockBaffleQuantity;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcRockBaffleQuantity
        {
            get { return WetlandCalculations.CalcRockBaffleQuantity(RockBaffleSpace, CalcFreeStandingWaterTopLength); }
            set { ChangeAndNotify(ref _calcRockBaffleQuantity, value); }
        }

        private double _calcRockBaffleLimestoneVolume;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcRockBaffleLimestoneVolume
        {
            get { return WetlandCalculations.CalcRockBaffleLimestoneVolume(CalcRockBaffleQuantity, CalcRockBaffleHeight,  CalcFreeStandingWaterTopWidth, PondInsideSlope); }
            set { ChangeAndNotify(ref _calcRockBaffleLimestoneVolume, value); }
        }

        private double _calcRockBaffleLimestoneWeight;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcRockBaffleLimestoneWeight
        {
            get { return WetlandCalculations.CalcRockBaffleLimestoneWeight(CalcRockBaffleLimestoneVolume, CalcLimestoneBulkDensity); }
            set { ChangeAndNotify(ref _calcRockBaffleLimestoneWeight, value); }
        }

        #endregion

        #region Properties - Sizing Summary: Pipe

        private double _calcHeaderPipeLength;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcHeaderPipeLength
        {
            get { return WetlandCalculations.CalcHeaderPipeLength(CalcOutletPoolBottomWidth); }
            set { ChangeAndNotify(ref _calcHeaderPipeLength, value); }
        }

        private double _calcHeaderPipeCouplerQuantity;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcHeaderPipeCouplerQuantity
        {
            get { return WetlandCalculations.CalcHeaderPipeCouplerQuantity(CalcHeaderPipeLength, HeaderPipeSegmentLength, HeaderPipeQuantity); }
            set { ChangeAndNotify(ref _calcHeaderPipeCouplerQuantity, value); }
        }

        #endregion

        #region Properties - Sizing Summary: Liner

        private double _calcLinerSlopeLength;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLinerSlopeLength
        {
            get { return WetlandCalculations.CalcLinerSlopeLength(CompostMixDepth, FreeStandingWaterDepth, FreeboardDepth, PondInsideSlope); }
            set { ChangeAndNotify(ref _calcLinerSlopeLength, value); }
        }

        private double _calcSyntheticLinerArea;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcSyntheticLinerArea
        {
            get { return WetlandCalculations.CalcSyntheticLinerArea(CalcWetlandBottomLength, CalcWetlandBottomWidth, CalcFreeboardTopLength, CalcFreeboardTopWidth, CalcLinerSlopeLength); }
            set { ChangeAndNotify(ref _calcSyntheticLinerArea, value); }
        }

        private double _calcSyntheticLinerAreaTotal;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcSyntheticLinerAreaTotal
        {
            get
            {
                return CalcSyntheticLinerArea + CalcInletPoolSyntheticLinerArea + CalcOutletPoolSyntheticLinerArea;
            }
            set { ChangeAndNotify(ref _calcSyntheticLinerAreaTotal, value); }
        }

        private double _calcClayLinerArea;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcClayLinerArea
        {
            get { return WetlandCalculations.CalcClayLinerArea(CalcWetlandBottomLength, CalcWetlandBottomWidth, CalcFreeboardTopLength, CalcFreeboardTopWidth, CalcLinerSlopeLength); }
            set { ChangeAndNotify(ref _calcClayLinerArea, value); }
        }

        private double _calcClayLinerAreaTotal;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcClayLinerAreaTotal
        {
            get
            {
                return CalcClayLinerArea + CalcInletPoolClayLinerArea + CalcOutletPoolClayLinerArea;
            }
            set { ChangeAndNotify(ref _calcClayLinerAreaTotal, value); }
        }

        private double _calcClayLinerVolume;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcClayLinerVolume
        {
            get
            {
                return WetlandCalculations.CalcClayLinerVolume(CalcClayLinerAreaTotal, ClayLinerThickness);
            }
            set { ChangeAndNotify(ref _calcClayLinerVolume, value); }
        }

        private double _calcGeosyntheticClayLinerArea;
        /// <summary>
        /// Calculated.  Same as synthetic liner area.
        /// </summary>
        public double CalcGeosyntheticClayLinerArea
        {
            get { return WetlandCalculations.CalcGeosyntheticClayLinerArea(CalcWetlandBottomLength, CalcWetlandBottomWidth, CalcFreeboardTopLength, CalcFreeboardTopWidth, CalcLinerSlopeLength); }
            set { ChangeAndNotify(ref _calcGeosyntheticClayLinerArea, value); }
        }

        private double _calcGeosyntheticClayLinerAreaTotal;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcGeosyntheticClayLinerAreaTotal
        {
            get
            {
                return CalcGeosyntheticClayLinerArea + CalcInletPoolGeosyntheticClayLinerArea + CalcOutletPoolGeosyntheticClayLinerArea;
            }
            set { ChangeAndNotify(ref _calcGeosyntheticClayLinerAreaTotal, value); }
        }

        private double _calcGeosyntheticClayLinerVolume;
        /// <summary>
        /// Calculated.
        /// </summary>
        public double CalcGeosyntheticClayLinerVolume
        {
            get { return WetlandCalculations.CalcGeosyntheticClayLinerVolume(CalcGeosyntheticClayLinerAreaTotal, GeosyntheticClayLinerSoilCover); }
            set { ChangeAndNotify(ref _calcGeosyntheticClayLinerVolume, value); }
        }

        private double _calcNonWovenGeotextileSlopeLength;
        /// <summary>
        /// Calculated.  Same as synthetic liner area.
        /// </summary>
        public double CalcNonWovenGeotextileSlopeLength
        {
            get { return WetlandCalculations.CalcNonWovenGeotextileSlopeLength(CompostMixDepth, PondInsideSlope); }
            set { ChangeAndNotify(ref _calcNonWovenGeotextileSlopeLength, value); }
        }

        private double _calcNonWovenGeotextileArea;
        /// <summary>
        /// Calculated.  
        /// </summary>
        public double CalcNonWovenGeotextileArea
        {
            get { return WetlandCalculations.CalcNonWovenGeotextileArea(CalcCompostMixTopLength, CalcCompostMixTopWidth, CalcWetlandBottomLength, CalcWetlandBottomWidth, CalcNonWovenGeotextileSlopeLength); }
            set { ChangeAndNotify(ref _calcNonWovenGeotextileArea, value); }
        }

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
                        _calcLinerArea = CalcClayLinerAreaTotal;
                        break;
                    case RadioButtonsLinerOptionsEnum.OptionSyntheticLiner:
                        _calcLinerArea = CalcSyntheticLinerAreaTotal;
                        break;
                    case RadioButtonsLinerOptionsEnum.OptionGeosyntheticClayLiner:
                        _calcLinerArea = CalcGeosyntheticClayLinerAreaTotal;
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

        private double _calcWaterLayerRetentionTime;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcWaterLayerRetentionTime
        {
            get { return WetlandCalculations.CalcFreeStandingWaterLayerRetentionTime(CalcFreeStandingWaterVolume, DesignFlow); }
            set { ChangeAndNotify(ref _calcWaterLayerRetentionTime, value); }
        }

        private double _calcInletPoolRetentionTime;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcInletPoolRetentionTime
        {
            get { return WetlandCalculations.CalcPoolRetentionTime(CalcInletPoolVolume, DesignFlow); }
            set { ChangeAndNotify(ref _calcInletPoolRetentionTime, value); }
        }

        private double _calcOutletPoolRetentionTime;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcOutletPoolRetentionTime
        {
            get { return WetlandCalculations.CalcPoolRetentionTime(CalcOutletPoolVolume, DesignFlow); }
            set { ChangeAndNotify(ref _calcOutletPoolRetentionTime, value); }
        }

        private double _calcWaterLayerRetentionTimeTotal;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcWaterLayerRetentionTimeTotal
        {
            get { return CalcWaterLayerRetentionTime + CalcInletPoolRetentionTime + CalcOutletPoolRetentionTime; }
            set { ChangeAndNotify(ref _calcWaterLayerRetentionTimeTotal, value); }
        }

        private double _calcCompostMixLayerRetentionTime;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcCompostMixLayerRetentionTime
        {
            get { return WetlandCalculations.CalcCompostMixLayerRetentionTime(CalcCompostMixVolume, CalcCompostMixPorosity, DesignFlow); }
            set { ChangeAndNotify(ref _calcCompostMixLayerRetentionTime, value); }
        }

        #endregion

        #region Properties - Capital Costs

        private decimal _calcExcavationCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcExcavationCost
        {
            get { return WetlandCalculations.CalcExcavationCost(CalcExcavationVolume, ExcavationUnitCost); }
            set { ChangeAndNotify(ref _calcExcavationCost, value); }
        }

        private decimal _calcCompostMixMaterialCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcCompostMixMaterialCost
        {
            get { return WetlandCalculations.CalcCompostMixMaterialCost(CalcCompostMixOrganicMaterialVolume, CompostMixUnitCost, CalcCompostMixLimestoneFinesWeight, LimestoneFinesUnitCost); }
            set { ChangeAndNotify(ref _calcCompostMixMaterialCost, value); }
        }

        private decimal _calcCompostMixAndLimestonePlacementCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcCompostMixAndLimestonePlacementCost
        {
            get { return WetlandCalculations.CalcCompostMixAndLimestonePlacementCost(CalcCompostMixVolume, CompostMixPlacementUnitCost); }
            set { ChangeAndNotify(ref _calcCompostMixAndLimestonePlacementCost, value); }
        }

        private decimal _calcWetlandPlantingCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcWetlandPlantingCost
        {
            get { return WetlandCalculations.CalcWetlandPlantingCost(CalcCompostMixTopArea, WetlandPlantingUnitCost); }
            set { ChangeAndNotify(ref _calcWetlandPlantingCost, value); }
        }
        

        private decimal _calcRockBaffleLimestoneMaterialCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRockBaffleLimestoneMaterialCost
        {
            get { return WetlandCalculations.CalcRockBaffleLimestoneMaterialCost(CalcRockBaffleLimestoneWeight, LimestoneUnitCost); }
            set { ChangeAndNotify(ref _calcRockBaffleLimestoneMaterialCost, value); }
        }

        private decimal _calcRockBaffleLimestonePlacementCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRockBaffleLimestonePlacementCost
        {
            get { return WetlandCalculations.CalcRockBaffleLimestonePlacementCost(CalcRockBaffleLimestoneVolume, LimestonePlacementUnitCost); }
            set { ChangeAndNotify(ref _calcRockBaffleLimestonePlacementCost, value); }
        }

        private decimal _calcInOutPipeCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcInOutPipeCost
        {
            get { return WetlandCalculations.CalcInOutPipeCost(InOutPipeLength, InOutPipeUnitCost); }
            set { ChangeAndNotify(ref _calcInOutPipeCost, value); }
        }

        private decimal _calcHeaderPipeCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcHeaderPipeCost
        {
            get { return WetlandCalculations.CalcHeaderPipeCost(CalcHeaderPipeLength, HeaderPipeQuantity, HeaderPipeUnitCost); }
            set { ChangeAndNotify(ref _calcHeaderPipeCost, value); }
        }

        private decimal _calcHeaderPipeCouplerCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcHeaderPipeCouplerCost
        {
            get { return WetlandCalculations.CalcHeaderPipeCouplerCost(CalcHeaderPipeCouplerQuantity, HeaderPipeCouplerUnitCost); }
            set { ChangeAndNotify(ref _calcHeaderPipeCouplerCost, value); }
        }

        private decimal _calcTeeConnectorCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcTeeConnectorCost
        {
            get { return WetlandCalculations.CalcTeeConnectorCost(HeaderPipeQuantity, TeeConnectorUnitCost); }
            set { ChangeAndNotify(ref _calcTeeConnectorCost, value); }
        }

        private decimal _calcPipeCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcPipeCost
        {
            get
            {
                decimal amdtreatPipe = WetlandCalculations.CalcAmdtreatPipeCost(CalcInOutPipeCost,
                                                                                CalcHeaderPipeCost,
                                                                                CalcHeaderPipeCouplerCost,
                                                                                CalcTeeConnectorCost,
                                                                                InOutPipeLength,
                                                                                CalcHeaderPipeLength,
                                                                                HeaderPipeQuantity,
                                                                                InOutPipeInstallRate,
                                                                                HeaderPipeInstallRate,
                                                                                LaborRate);

                decimal customPipe = WetlandCalculations.CalcCustomPipeCost(CustomPipeLength1, CustomPipeUnitCost1,
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
                return WetlandCalculations.CalcOtherItemsCost(FlowDistributionStructureQuantity, FlowDistributionStructureUnitCost,
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
            get { return VerticalFlowPondCalculations.CalcClayLinerCost(CalcClayLinerVolume, ClayLinerUnitCost); }
            set { ChangeAndNotify(ref _calcClayLinerCost, value); }
        }

        private decimal _calcSyntheticLinerCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcSyntheticLinerCost
        {
            get { return VerticalFlowPondCalculations.CalcSyntheticLinerCost(CalcSyntheticLinerAreaTotal, SyntheticLinerUnitCost); }
            set { ChangeAndNotify(ref _calcSyntheticLinerCost, value); }
        }

        private decimal _calcGeosyntheticClayLinerCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcGeosyntheticClayLinerCost
        {
            get { return WetlandCalculations.CalcGeosyntheticClayLinerCost(CalcGeosyntheticClayLinerAreaTotal, GeosyntheticClayLinerUnitCost); }
            set { ChangeAndNotify(ref _calcGeosyntheticClayLinerCost, value); }
        }

        private decimal _calcGeosyntheticClayLinerCoverCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcGeosyntheticClayLinerCoverCost
        {
            get { return WetlandCalculations.CalcGeosyntheticClayLinerCoverCost(CalcGeosyntheticClayLinerVolume, GeosyntheticClayLinerCoverUnitCost); }
            set { ChangeAndNotify(ref _calcGeosyntheticClayLinerCoverCost, value); }
        }

        private decimal _calcNonWovenGeotextileCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcNonWovenGeotextileCost
        {
            get { return WetlandCalculations.CalcNonWovenGeotextileCost(CalcNonWovenGeotextileArea, NonWovenGeotextileUnitCost); }
            set { ChangeAndNotify(ref _calcNonWovenGeotextileCost, value); }
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

        private decimal _calcCapitalCostTotal;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcCapitalCostTotal
        {
            get
            {
                _calcCapitalCostTotal = WetlandCalculations.CalcCapitalCostTotal(CalcCompostMixMaterialCost, 
                                                                                 CalcCompostMixAndLimestonePlacementCost,
                                                                                 CalcWetlandPlantingCost,
                                                                                 CalcRockBaffleLimestoneMaterialCost,
                                                                                 CalcRockBaffleLimestonePlacementCost,
                                                                                 CalcExcavationCost, 
                                                                                 CalcLinerCost, 
                                                                                 CalcPipeCost, 
                                                                                 CalcOtherItemsCost);

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
            set
            {
                ChangeAndNotify(ref _capitalCostData, value, nameof(CapitalCostData));
            }
        }

        #endregion

        #region Properties - Annual (Operations and Maintenance) Costs

        private RadioButtonsAnnualCostOptionsEnum _annualCostOptionsProperty;
        /// <summary>
        ///  User specified 
        /// </summary>
        public RadioButtonsAnnualCostOptionsEnum AnnualCostOptionsProperty
        {
            get { return _annualCostOptionsProperty; }
            set { ChangeAndNotify(ref _annualCostOptionsProperty, value, nameof(AnnualCostOptionsProperty), CalcPropertiesStringArray); }
        }

        private double _annualCostMultiplier;
        /// <summary>
        /// User specified
        /// </summary>
        public double AnnualCostMultiplier
        {
            get { return _annualCostMultiplier; }
            set
            {
                ChangeAndNotify(ref _annualCostMultiplier, value, nameof(AnnualCostMultiplier), CalcPropertiesStringArray);
            }
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
                    case RadioButtonsAnnualCostOptionsEnum.OptionAnnualCostMultiplier:
                        _calcAnnualCost = WetlandCalculations.CalcAnnualCost(AnnualCostMultiplier, CalcCapitalCostTotal);
                        break;
                    case RadioButtonsAnnualCostOptionsEnum.OptionAnnualCostFlatFee:
                        _calcAnnualCost = AnnualCostFlatFee;
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

        private double _recapitalizationCostLifeCycleLimestone;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostLifeCycleRockBaffleLimestone
        {
            get { return _recapitalizationCostLifeCycleLimestone; }
            set { ChangeAndNotify(ref _recapitalizationCostLifeCycleLimestone, value, nameof(RecapitalizationCostLifeCycleRockBaffleLimestone), CalcPropertiesStringArray); }
        }

        private double _RecapitalizationCostLifeCycleCompostMix;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostLifeCycleCompostMix
        {
            get { return _RecapitalizationCostLifeCycleCompostMix; }
            set { ChangeAndNotify(ref _RecapitalizationCostLifeCycleCompostMix, value, nameof(RecapitalizationCostLifeCycleCompostMix), CalcPropertiesStringArray); }
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

        private double _recapitalizationCostLifeCyclePipe;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostLifeCyclePipe
        {
            get { return _recapitalizationCostLifeCyclePipe; }
            set { ChangeAndNotify(ref _recapitalizationCostLifeCyclePipe, value, nameof(RecapitalizationCostLifeCyclePipe), CalcPropertiesStringArray); }
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

        private double _recapitalizationCostPercentReplacementCompostMix;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostPercentReplacementCompostMix
        {
            get { return _recapitalizationCostPercentReplacementCompostMix; }
            set { ChangeAndNotify(ref _recapitalizationCostPercentReplacementCompostMix, value, nameof(RecapitalizationCostPercentReplacementCompostMix), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostPercentReplacementLimestone;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostPercentReplacementRockBaffleLimestone
        {
            get { return _recapitalizationCostPercentReplacementLimestone; }
            set { ChangeAndNotify(ref _recapitalizationCostPercentReplacementLimestone, value, nameof(RecapitalizationCostPercentReplacementRockBaffleLimestone), CalcPropertiesStringArray); }
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

        private double _recapitalizationCostPercentReplacementPipe;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostPercentReplacementPipe
        {
            get { return _recapitalizationCostPercentReplacementPipe; }
            set { ChangeAndNotify(ref _recapitalizationCostPercentReplacementPipe, value, nameof(RecapitalizationCostPercentReplacementPipe), CalcPropertiesStringArray); }
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

        private decimal _calcRecapitalizationCostMaterialTotalCostCompost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostMaterialTotalCostCompost
        {
            get { return WetlandCalculations.CalcRecapitalizationCostMaterialTotalCostCompost(CalcCompostMixMaterialCost, CalcCompostMixAndLimestonePlacementCost, CalcWetlandPlantingCost); }
            set { ChangeAndNotify(ref _calcRecapitalizationCostMaterialTotalCostCompost, value); }
        }

        private decimal _calcRecapitalizationCostMaterialTotalCostRockBaffleLimestone;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostMaterialTotalCostRockBaffleLimestone
        {
            get { return WetlandCalculations.CalcRecapitalizationCostMaterialTotalCostRockBaffleLimestone(CalcRockBaffleLimestoneVolume, CalcRockBaffleLimestoneWeight, LimestoneUnitCost, LimestonePlacementUnitCost); }
            set { ChangeAndNotify(ref _calcRecapitalizationCostMaterialTotalCostRockBaffleLimestone, value); }
        }

        private decimal _calcRapitalizationCostRockBaffleLimestone;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostRockBaffleLimestone
        {
            get
            {
                return WetlandCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
                                                                    RecapitalizationCostInflationRate, RecapitalizationCostLifeCycleRockBaffleLimestone,
                                                                    CalcRecapitalizationCostMaterialTotalCostRockBaffleLimestone, RecapitalizationCostPercentReplacementRockBaffleLimestone);
            }
            set { ChangeAndNotify(ref _calcRapitalizationCostRockBaffleLimestone, value); }
        }

        private decimal _calcRecapitalizationCostCompost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostCompost
        {
            get
            {
                return WetlandCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
                                                                             RecapitalizationCostInflationRate, RecapitalizationCostLifeCycleCompostMix,
                                                                             CalcRecapitalizationCostMaterialTotalCostCompost, RecapitalizationCostPercentReplacementCompostMix);
            }
            set { ChangeAndNotify(ref _calcRecapitalizationCostCompost, value); }
        }

        private decimal _calcRapitalizationCostLiner;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostLiner
        {
            get
            {
                return WetlandCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
                                                                    RecapitalizationCostInflationRate, RecapitalizationCostLifeCycleLiner,
                                                                    CalcLinerCost, RecapitalizationCostPercentReplacementLiner);
            }
            set { ChangeAndNotify(ref _calcRapitalizationCostLiner, value); }
        }

        private decimal _calcRapitalizationCostPipe;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostPipe
        {
            get
            {
                return WetlandCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
                                                                    RecapitalizationCostInflationRate, RecapitalizationCostLifeCyclePipe,
                                                                    CalcPipeCost, RecapitalizationCostPercentReplacementPipe);
            }
            set { ChangeAndNotify(ref _calcRapitalizationCostPipe, value); }
        }

        private decimal _calcRapitalizationCostOtherItems;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostOtherItems
        {
            get
            {
                return WetlandCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
                                                                    RecapitalizationCostInflationRate, RecapitalizationCostLifeCycleOtherItems,
                                                                    CalcOtherItemsCost, RecapitalizationCostPercentReplacementOtherItems);
            }
            set { ChangeAndNotify(ref _calcRapitalizationCostOtherItems, value); }
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
            get { return WetlandCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn, RecapitalizationCostInflationRate, 1.0, CalcAnnualCost, 100.0); }
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
                    case "RockBaffle":
                        item.MaterialCostDefault = CalcRecapitalizationCostMaterialTotalCostRockBaffleLimestone;
                        break;
                    case "CompostMix":
                        item.MaterialCostDefault = CalcRecapitalizationCostMaterialTotalCostCompost;
                        break;
                    case "Liner":
                        item.MaterialCostDefault = CalcLinerCost;
                        break;
                    case "Pipe":
                        item.MaterialCostDefault = CalcPipeCost;
                        break;
                    case "OtherItems":
                        item.MaterialCostDefault = CalcOtherItemsCost;
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
                    case "RockBaffle":
                        item.MaterialCostDefault = CalcRecapitalizationCostRockBaffleLimestone;
                        break;
                    case "CompostMix":
                        item.MaterialCostDefault = CalcRecapitalizationCostCompost;
                        break;
                    case "Liner":
                        item.MaterialCostDefault = CalcRecapitalizationCostLiner;
                        break;
                    case "Pipe":
                        item.MaterialCostDefault = CalcRecapitalizationCostPipe;
                        break;
                    case "OtherItems":
                        item.MaterialCostDefault = CalcRecapitalizationCostOtherItems;
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
                Name = "Rock Baffle",
                NameFixed = "RockBaffle",
                LifeCycle = RecapitalizationCostLifeCycleRockBaffleLimestone,
                PercentReplacement = RecapitalizationCostPercentReplacementRockBaffleLimestone,
                MaterialCostDefault = CalcRecapitalizationCostMaterialTotalCostRockBaffleLimestone,
                UseCustomCost = false,
                MaterialCostCustom = 0m,
                TotalCost = CalcRecapitalizationCostRockBaffleLimestone
            });
            RecapMaterials.Add(new RecapMaterial
            {
                IsSelected = true,
                Name = "Compost Mix",
                NameFixed = "CompostMix",
                LifeCycle = RecapitalizationCostLifeCycleCompostMix,
                PercentReplacement = RecapitalizationCostPercentReplacementCompostMix,
                MaterialCostDefault = CalcRecapitalizationCostMaterialTotalCostCompost,
                UseCustomCost = false,
                MaterialCostCustom = 0m,
                TotalCost = CalcRecapitalizationCostCompost
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
            ((RecapMaterial)sender).TotalCost = WetlandCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
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
            var customDialog = new CustomDialog() { Title = "About Vertical Flow Pond" };

            var customDialogViewModel = new CustomDialogViewModel(instance =>
            {
                _dialogCoordinator.HideMetroDialogAsync(this, customDialog);
            });
            customDialogViewModel.Message = Resources.infoWetland;
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
                        string message = Resources.infoWaterQualityWetland;
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
                        string message = Resources.infoSizingMethodsWetland;
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
                        string message = Resources.infoSystemPropertiesWetland;
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
                        string message = Resources.infoLayerMaterialsWetland;
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
                        string message = Resources.infoOtherItemsWetland;
                        await _dialogCoordinator.ShowMessageAsync(this, "Other Items", message);
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
                        string message = Resources.infoSizingSummaryWetland;
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
                        string message = Resources.infoCapitalCostWetland;
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
                        string message = Resources.infoAnnualCostWetland;
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
                        string message = Resources.infoRecapitalizationCostWetland;
                        await _dialogCoordinator.ShowMessageAsync(this, "Net Present Value of Replacement and Annual Costs (Recapitalization Cost)", message);
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
            if (ratio == CalcBottomLengthToWidthRatioDimensions)
            {
                // Use the backup copy
                ratio = BottomLengthToWidthRatioBackup;
            }
            else
            {
                // Update the backup copy
                BottomLengthToWidthRatioBackup = ratio;
            }

            return ratio;
        }

        #endregion

        #region Error Handling and Information

        private double _calcMaxPondDepth;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcMaxPondDepth
        {
            get { return WetlandCalculations.CalcMaxPondDepth(CalcFreeboardTopWidth); }
            set { ChangeAndNotify(ref _calcMaxPondDepth, value); }
        }

        private double _calcPondDepth;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcPondDepth
        {
            get { return WetlandCalculations.CalcPondDepth(FreeStandingWaterDepth, CompostMixDepth); }
            set { ChangeAndNotify(ref _calcPondDepth, value); }
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

        private bool _isErrorPoolBottomLength;
        public bool IsErrorPoolBottomLength
        {
            get { return _isErrorPoolBottomLength; }
            set { ChangeAndNotify(ref _isErrorPoolBottomLength, value); }
        }

        private bool _isErrorMetalRemovalRate;
        public bool IsErrorMetalRemovalRate
        {
            get { return _isErrorMetalRemovalRate; }
            set { ChangeAndNotify(ref _isErrorMetalRemovalRate, value); }
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

        private void CheckWaterBottomWidthValue(double value)
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
            if (IsErrorPoolBottomLength)
            {
                IsError = true;
            }
            else if (IsErrorMetalRemovalRate)
            {
                IsError = true;
            }
            else
            {
                IsError = false;
                IsMajorError = false;
                IsErrorPoolBottomLength = false;
                IsErrorMetalRemovalRate = false;
                ErrorMessage = "";
                ErrorMessageShort = "";
            }
        }

        private void ShowError()
        {
            IsError = true;
            IsMajorError = false;
            IsOpenFlyoutError = true;
            ErrorMessage = Resources.errorPondWetland;
            ErrorMessageShort = "ERROR - Pond cannot be constructed";
            IsExpandedFreeboard = true;
            IsExpandedWater = true;
            IsExpandedCompostMix = true;
            IsExpandedRockBaffles = true;
        }

        private void ShowMajorError()
        {
            IsError = true;
            IsMajorError = true;
            IsOpenFlyoutError = true;
            ErrorMessage = Resources.errorPondMajorWetland;
            ErrorMessageShort = "MAJOR ERROR - All calculations are invalid";
            IsExpandedFreeboard = true;
            IsExpandedWater = true;
            IsExpandedCompostMix = true;
            IsExpandedRockBaffles = true;
        }

        private void ShowErrorMetalRemovalRate() 
        {
            IsError = true;
            IsErrorMetalRemovalRate = true;
            IsOpenFlyoutError = true;
            ErrorMessage = Resources.errorPondWetlandMetalRemovalRate;
            ErrorMessageShort = "ERROR - Net Acidity is greater than 0";
        }

        private void ShowErrorPoolBottomLength()
        {
            IsError = true;
            IsErrorPoolBottomLength = true;
            IsOpenFlyoutError = true;
            ErrorMessage = Resources.errorPoolWetlandBottomLength;
            ErrorMessageShort = "ERROR - Increase the inlet or outlet pool lengths or decrease the inlet or outlet depths.";
        }

        private void ShowNoErrorPoolBottomLength()
        {
            IsErrorPoolBottomLength = false;
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

        private bool _isExpandedRockBaffles;
        public bool IsExpandedRockBaffles
        {
            get { return _isExpandedRockBaffles; }
            set { ChangeAndNotify(ref _isExpandedRockBaffles, value); }
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

        public WetlandViewModel(IDialogCoordinator dialogCoordinator)
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
            ModuleType = "Wetland";
            ModuleName = "Untitled module";
            ModuleTreatmentType = "Passive";
            ModuleId = random.Next(1000);

            IsError = false;
            IsMajorError = false;
            ErrorMessage = "";
            ErrorMessageShort = "";

            // Initialize check boxes
            PipeOptionAmdtreat = true;
            PipeOptionCustom = false;

            // Initialize radio buttons
            SizingMethodsOptionsProperty = RadioButtonsSizingMethodsOptionsEnum.OptionRetentionTime;
            LinerOptionsProperty = RadioButtonsLinerOptionsEnum.OptionNoLiner;
            AnnualCostOptionsProperty = RadioButtonsAnnualCostOptionsEnum.OptionAnnualCostMultiplier;

            // Dummy variables to force syncing
            Dummy = 0;

            // Read the xml data file and assign property values accordingly.
            OpenXmlAndAssignValues(@"..\..\Data\default-data-wetland.xml");

            // Make an initial backup of the bottom length to width ratio
            BottomLengthToWidthRatioBackup = BottomLengthToWidthRatio;

            // Recapitalization parameters that are set one time by a user within the main ui and are not shown in each module
            RecapitalizationCostCalculationPeriod = 75;
            RecapitalizationCostInflationRate = 5.0;
            RecapitalizationCostNetRateOfReturn = 8.0;
            InitRecapMaterials();
            CalcRecapMaterialsTotalCost();
            CalcRecapitalizationCostTotal = CalcRecapMaterialsTotalCost();

            // Add method to shared data collection
            SharedDataCollection.CollectionChanged += SharedDataCollectionChanged;
        }

        #endregion

    }

}
