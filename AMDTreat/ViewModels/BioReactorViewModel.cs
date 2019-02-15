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

    public class BioReactorViewModel : PropertyChangedBase, IObserver<SharedData>
    {

        #region Radio Button Enums

        /// <summary>
        ///  Radio button binding with enumeration for sizing methods
        /// </summary>
        public enum RadioButtonsSizingMethodsOptionsEnum
        {
            OptionSulfateReduction,
            OptionAlkalinityGenerationRate,
            OptionPilotTesting,
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

        private double _netAcidity;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double NetAcidity
        {
            get { return _netAcidity; }
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

        #region Properties - Sizing Methods: Sulfate Reduction

        private double _sulfateReductionRate;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double SulfateReductionRate
        {
            get { return _sulfateReductionRate; }
            set { ChangeAndNotify(ref _sulfateReductionRate, value, nameof(SulfateReductionRate), CalcPropertiesStringArray); }
        }

        private double _sulfateReductionAmount;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double SulfateReductionAmount
        {
            get { return _sulfateReductionAmount; }
            set { ChangeAndNotify(ref _sulfateReductionAmount, value, nameof(SulfateReductionAmount), CalcPropertiesStringArray); }
        }

        private double _calcBioMixVolumeBasedOnSulfateReduction;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcBioMixVolumeBasedOnSulfateReduction
        {
            get { return BioReactorCalculations.CalcBioMixVolumeBasedOnSulfateReduction(DesignFlow, SulfateReductionAmount, SulfateReductionRate, BioMixShrinkage ); }
            set { ChangeAndNotify(ref _calcBioMixVolumeBasedOnSulfateReduction, value); }
        }

        #endregion

        #region Properties - Sizing Methods: Alkalinity Generation Rate

        private double _alkalinityGenerationRate;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double AlkalinityGenerationRate
        {
            get { return _alkalinityGenerationRate; }
            set { ChangeAndNotify(ref _alkalinityGenerationRate, value, nameof(AlkalinityGenerationRate), CalcPropertiesStringArray); }
        }

        private double _calcAcidityRate;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcAcidityRate
        {
            get { return BioReactorCalculations.CalcAcidityRateBasedOnAlkalinityGenerationRate(DesignFlow, NetAcidity); }
            set { ChangeAndNotify(ref _calcAcidityRate, value); }
        }

        private double _calcSurfaceAreaTopBioMixBasedOnAlkalinityGeneration;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcSurfaceAreaTopBioBixBasedOnAlkalinityGeneration
        {
            get { return BioReactorCalculations.CalcSurfaceAreaTopOfBioMixBasedOnAlkalinityGenerationRate(CalcAcidityRate, AlkalinityGenerationRate); }
            set { ChangeAndNotify(ref _calcSurfaceAreaTopBioMixBasedOnAlkalinityGeneration, value); }
        }

        private double _calcBioMixBottomLengthBasedOnAlkalinityGeneration;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcBioMixBottomLengthBasedOnAlkalinityGeneration
        {
            get { return BioReactorCalculations.CalcBioMixBottomLengthBasedOnAlkalinityGenerationRate(CalcSurfaceAreaTopBioBixBasedOnAlkalinityGeneration, 
                                                                                                  BioMixDepth, 
                                                                                                  PondInsideSlope, 
                                                                                                  BottomLengthToWidthRatio); }
            set { ChangeAndNotify(ref _calcBioMixBottomLengthBasedOnAlkalinityGeneration, value); }
        }

        private double _calcBioMixVolumeBasedOnAlkalinityGenerationRate;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcBioMixVolumeBasedOnAlkalinityGenerationRate
        {
            get { return BioReactorCalculations.CalcBioMixVolumeBasedOnAlkalinityGenerationRate(CalcBioMixBottomLengthBasedOnAlkalinityGeneration,
                                                                                            BioMixDepth,
                                                                                            PondInsideSlope,
                                                                                            BottomLengthToWidthRatio); }
            set { ChangeAndNotify(ref _calcBioMixVolumeBasedOnAlkalinityGenerationRate, value); }
        }
        
        #endregion

        #region Properties - Sizing Methods: Pilot Test Results

        private double _volumetricLoading;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double VolumetricLoading
        {
            get { return _volumetricLoading; }
            set { ChangeAndNotify(ref _volumetricLoading, value, nameof(VolumetricLoading), CalcPropertiesStringArray); }
        }

        private double _calcBioMixVolumeBasedOnPilotTesting;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcBioMixVolumeBasedOnPilotTesting
        {
            get { return BioReactorCalculations.CalcBioMixVolumeBasedOnPilotTesting(DesignFlow, VolumetricLoading); }
            set { ChangeAndNotify(ref _calcBioMixVolumeBasedOnPilotTesting, value); }
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

        private double _calcBioMixBottomWidthBasedOnDimensionsEntered;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcBioMixBottomWidthBasedOnDimensionsEntered
        {
            get { return BioReactorCalculations.CalcBioMixBottomWidthBasedOnDimensionsEntered(FreeboardTopWidthDimensions, FreeboardDepth, FreeStandingWaterDepth, BioMixDepth, PondInsideSlope); }
            set { ChangeAndNotify(ref _calcBioMixBottomWidthBasedOnDimensionsEntered, value); }
        }

        private double _calcBioMixBottomLengthBasedOnDimensionsEntered;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcBioMixBottomLengthBasedOnDimensionsEntered
        {
            get { return BioReactorCalculations.CalcBioMixBottomLengthBasedOnDimensionsEntered(FreeboardTopLengthDimensions, FreeboardDepth, FreeStandingWaterDepth, BioMixDepth, PondInsideSlope); }
            set { ChangeAndNotify(ref _calcBioMixBottomLengthBasedOnDimensionsEntered, value); }
        }

        private double _calcBioMixTopWidthBasedOnDimensionsEntered;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcBioMixTopWidthBasedOnDimensionsEntered
        {
            get { return BioReactorCalculations.CalcBioMixTopWidthBasedOnDimensionsEntered(CalcBioMixBottomWidthBasedOnDimensionsEntered, BioMixDepth, PondInsideSlope); }
            set { ChangeAndNotify(ref _calcBioMixTopWidthBasedOnDimensionsEntered, value); }
        }

        private double _calcBioMixTopLengthBasedOnDimensionsEntered;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcBioMixTopLengthBasedOnDimensionsEntered
        {
            get { return BioReactorCalculations.CalcBioMixTopLengthBasedOnDimensionsEntered(CalcBioMixBottomLengthBasedOnDimensionsEntered, BioMixDepth, PondInsideSlope); }
            set { ChangeAndNotify(ref _calcBioMixTopLengthBasedOnDimensionsEntered, value); }
        }

        private double _calcBioMixVolumeBasedOnDimensionsEntered;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcBioMixVolumeBasedOnDimensionsEntered
        {
            get { return BioReactorCalculations.CalcBioMixVolumeBasedOnDimensionsEntered(CalcBioMixTopLengthBasedOnDimensionsEntered, CalcBioMixTopWidthBasedOnDimensionsEntered,
                                                                                         CalcBioMixBottomLengthBasedOnDimensionsEntered, CalcBioMixBottomWidthBasedOnDimensionsEntered,
                                                                                         BioMixDepth); }
            set { ChangeAndNotify(ref _calcBioMixVolumeBasedOnDimensionsEntered, value); }
        }

        private double _calcBottomLengthToWidthRatioDimensions;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcBottomLengthToWidthRatioDimensions
        {
            get { return BioReactorCalculations.CalcBottomLengthToWidthRatioBasedOnDimensionsEntered(CalcBioMixBottomLengthBasedOnDimensionsEntered, CalcBioMixTopWidthBasedOnDimensionsEntered); }
            set { ChangeAndNotify(ref _calcBottomLengthToWidthRatioDimensions, value); }
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
            set
            {
                ChangeAndNotify(ref _bottomLengthToWidthRatio, value, nameof(BottomLengthToWidthRatio), CalcPropertiesStringArray);
            }
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

        #region Properties - Layer Materials: Bio Mix

        private double _bioMixManurePercentage;
        /// <summary>
        /// User specified
        /// </summary>
        public double BioMixManurePercentage
        {
            get { return _bioMixManurePercentage; }
            set { ChangeAndNotify(ref _bioMixManurePercentage, value, nameof(BioMixManurePercentage), CalcPropertiesStringArray); }
        }

        private double _bioMixManureDensity;
        /// <summary>
        /// User specified
        /// </summary>
        public double BioMixManureDensity
        {
            get { return _bioMixManureDensity; }
            set { ChangeAndNotify(ref _bioMixManureDensity, value, nameof(BioMixManureDensity), CalcPropertiesStringArray); }
        }

        private double _bioMixHayPercentage;
        /// <summary>
        /// User specified
        /// </summary>
        public double BioMixHayPercentage
        {
            get { return _bioMixHayPercentage; }
            set { ChangeAndNotify(ref _bioMixHayPercentage, value, nameof(BioMixHayPercentage), CalcPropertiesStringArray); }
        }

        private double _bioMixHayDensity;
        /// <summary>
        /// User specified
        /// </summary>
        public double BioMixHayDensity
        {
            get { return _bioMixHayDensity; }
            set { ChangeAndNotify(ref _bioMixHayDensity, value, nameof(BioMixHayDensity), CalcPropertiesStringArray); }
        }

        private double _bioMixLimestoneFinesPercentage;
        /// <summary>
        /// User specified
        /// </summary>
        public double BioMixLimestoneFinesPercentage
        {
            get { return _bioMixLimestoneFinesPercentage; }
            set { ChangeAndNotify(ref _bioMixLimestoneFinesPercentage, value, nameof(BioMixLimestoneFinesPercentage), CalcPropertiesStringArray); }
        }

        private double _bioMixLimestoneFinesDensity;
        /// <summary>
        /// User specified
        /// </summary>
        public double BioMixLimestoneFinesDensity
        {
            get { return _bioMixLimestoneFinesDensity; }
            set { ChangeAndNotify(ref _bioMixLimestoneFinesDensity, value, nameof(BioMixLimestoneFinesDensity), CalcPropertiesStringArray); }
        }

        private double _bioMixWoodChipsDensity;
        /// <summary>
        /// User specified
        /// </summary>
        public double BioMixWoodChipsDensity
        {
            get { return _bioMixWoodChipsDensity; }
            set { ChangeAndNotify(ref _bioMixWoodChipsDensity, value, nameof(BioMixWoodChipsDensity), CalcPropertiesStringArray); }
        }

        private double _bioMixOtherPercentage;
        /// <summary>
        /// User specified
        /// </summary>
        public double BioMixOtherPercentage
        {
            get { return _bioMixOtherPercentage; }
            set { ChangeAndNotify(ref _bioMixOtherPercentage, value, nameof(BioMixOtherPercentage), CalcPropertiesStringArray); }
        }

        private double _bioMixOtherDensity;
        /// <summary>
        /// User specified
        /// </summary>
        public double BioMixOtherDensity
        {
            get { return _bioMixOtherDensity; }
            set { ChangeAndNotify(ref _bioMixOtherDensity, value, nameof(BioMixOtherDensity), CalcPropertiesStringArray); }
        }

        private double _bioMixDepth;
        /// <summary>
        /// User specified
        /// </summary>
        public double BioMixDepth
        {
            get { return _bioMixDepth; }
            set { ChangeAndNotify(ref _bioMixDepth, value, nameof(BioMixDepth), CalcPropertiesStringArray); }
        }

        private double _bioMixShrinkage;
        /// <summary>
        /// User specified
        /// </summary>
        public double BioMixShrinkage
        {
            get { return _bioMixShrinkage; }
            set { ChangeAndNotify(ref _bioMixShrinkage, value, nameof(BioMixShrinkage), CalcPropertiesStringArray); }
        }

        private decimal _bioMixManureUnitCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal BioMixManureUnitCost
        {
            get { return _bioMixManureUnitCost; }
            set { ChangeAndNotify(ref _bioMixManureUnitCost, value, nameof(BioMixManureUnitCost), CalcPropertiesStringArray); }
        }

        private decimal _bioMixHayUnitCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal BioMixHayUnitCost
        {
            get { return _bioMixHayUnitCost; }
            set { ChangeAndNotify(ref _bioMixHayUnitCost, value, nameof(BioMixHayUnitCost), CalcPropertiesStringArray); }
        }

        private decimal _bioMixLimestoneFinesUnitCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal BioMixLimestoneFinesUnitCost
        {
            get { return _bioMixLimestoneFinesUnitCost; }
            set { ChangeAndNotify(ref _bioMixLimestoneFinesUnitCost, value, nameof(BioMixLimestoneFinesUnitCost), CalcPropertiesStringArray); }
        }

        private decimal _bioMixWoodChipsUnitCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal BioMixWoodChipsUnitCost
        {
            get { return _bioMixWoodChipsUnitCost; }
            set { ChangeAndNotify(ref _bioMixWoodChipsUnitCost, value, nameof(BioMixWoodChipsUnitCost), CalcPropertiesStringArray); }
        }

        private decimal _bioMixOtherUnitCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal BioMixOtherUnitCost
        {
            get { return _bioMixOtherUnitCost; }
            set { ChangeAndNotify(ref _bioMixOtherUnitCost, value, nameof(BioMixOtherUnitCost), CalcPropertiesStringArray); }
        }

        private decimal _bioMixPlacementUnitCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal BioMixPlacementUnitCost
        {
            get { return _bioMixPlacementUnitCost; }
            set { ChangeAndNotify(ref _bioMixPlacementUnitCost, value, nameof(BioMixPlacementUnitCost), CalcPropertiesStringArray); }
        }
        #endregion

        #region Properties - Layer Materials: Drainage Aggregate

        private double _aggregateDepth;
        /// <summary>
        /// User specified
        /// </summary>
        public double AggregateDepth
        {
            get { return _aggregateDepth; }
            set { ChangeAndNotify(ref _aggregateDepth, value, nameof(AggregateDepth), CalcPropertiesStringArray); }
        }

        private double _aggregateBulkDensity;
        /// <summary>
        /// User specified
        /// </summary>
        public double AggregateBulkDensity
        {
            get { return _aggregateBulkDensity; }
            set { ChangeAndNotify(ref _aggregateBulkDensity, value, nameof(AggregateBulkDensity), CalcPropertiesStringArray); }
        }

        private decimal _aggregateUnitCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal AggregateUnitCost
        {
            get { return _aggregateUnitCost; }
            set { ChangeAndNotify(ref _aggregateUnitCost, value, nameof(AggregateUnitCost), CalcPropertiesStringArray); }
        }

        private decimal _aggregatePlacementUnitCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal AggregatePlacementUnitCost
        {
            get { return _aggregatePlacementUnitCost; }
            set { ChangeAndNotify(ref _aggregatePlacementUnitCost, value, nameof(AggregatePlacementUnitCost), CalcPropertiesStringArray); }
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

        private double _trunkPipeSegmentLength;
        /// <summary>
        /// User specified
        /// </summary>
        public double TrunkPipeSegmentLength
        {
            get { return _trunkPipeSegmentLength; }
            set { ChangeAndNotify(ref _trunkPipeSegmentLength, value, nameof(TrunkPipeSegmentLength), CalcPropertiesStringArray); }
        }

        private double _trunkSpurPipeInstallRate;
        /// <summary>
        /// User specified
        /// </summary>
        public double TrunkSpurPipeInstallRate
        {
            get { return _trunkSpurPipeInstallRate; }
            set { ChangeAndNotify(ref _trunkSpurPipeInstallRate, value, nameof(TrunkSpurPipeInstallRate), CalcPropertiesStringArray); }
        }

        private double _SpurPipeSegmentLength;
        /// <summary>
        /// User specified
        /// </summary>
        public double SpurPipeSegmentLength
        {
            get { return _SpurPipeSegmentLength; }
            set { ChangeAndNotify(ref _SpurPipeSegmentLength, value, nameof(SpurPipeSegmentLength), CalcPropertiesStringArray); }
        }

        private double _spurPipeSpacing;
        /// <summary>
        /// User specified
        /// </summary>
        public double SpurPipeSpacing
        {
            get { return _spurPipeSpacing; }
            set { ChangeAndNotify(ref _spurPipeSpacing, value, nameof(SpurPipeSpacing), CalcPropertiesStringArray); }
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

        private decimal _trunkPipeUnitCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal TrunkPipeUnitCost
        {
            get { return _trunkPipeUnitCost; }
            set { ChangeAndNotify(ref _trunkPipeUnitCost, value, nameof(TrunkPipeUnitCost), CalcPropertiesStringArray); }
        }

        private decimal _trunkPipeCouplerUnitCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal TrunkPipeCouplerUnitCost
        {
            get { return _trunkPipeCouplerUnitCost; }
            set { ChangeAndNotify(ref _trunkPipeCouplerUnitCost, value, nameof(TrunkPipeCouplerUnitCost), CalcPropertiesStringArray); }
        }

        private decimal _spurPipeUnitCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal SpurPipeUnitCost
        {
            get { return _spurPipeUnitCost; }
            set { ChangeAndNotify(ref _spurPipeUnitCost, value, nameof(SpurPipeUnitCost), CalcPropertiesStringArray); }
        }

        private decimal _spurPipeCouplerUnitCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal SpurPipeCouplerUnitCost
        {
            get { return _spurPipeCouplerUnitCost; }
            set { ChangeAndNotify(ref _spurPipeCouplerUnitCost, value, nameof(SpurPipeCouplerUnitCost), CalcPropertiesStringArray); }
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

        #region Properties - Other Items

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

        #region Properties - Sizing Summary: Excavation Volume, Clear and Grub Area

        private double _calcExcavationVolume;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcExcavationVolume
        {
            get { return BioReactorCalculations.CalcExcavationVolume(CalcBioMixVolume, CalcFreeStandingWaterVolume, CalcFreeboardVolume, CalcLinerVolume); }
            set { ChangeAndNotify(ref _calcExcavationVolume, value); }
        }

        private double _calcClearAndGrubArea;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcClearAndGrubArea
        {
            get { return BioReactorCalculations.CalcClearAndGrubArea(CalcFreeboardTopLength, CalcFreeboardTopWidth, FreeboardDepth); }
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
                switch (SizingMethodsOptionsProperty)
                {
                    case RadioButtonsSizingMethodsOptionsEnum.OptionSulfateReduction:
                        _calcFreeboardTopLength = BioReactorCalculations.CalcFreeboardTopLength(CalcFreeStandingWaterTopLength, FreeboardDepth, PondInsideSlope);
                        break;
                    case RadioButtonsSizingMethodsOptionsEnum.OptionAlkalinityGenerationRate:
                        _calcFreeboardTopLength = BioReactorCalculations.CalcFreeboardTopLength(CalcFreeStandingWaterTopLength, FreeboardDepth, PondInsideSlope);
                        break;
                    case RadioButtonsSizingMethodsOptionsEnum.OptionPilotTesting:
                        _calcFreeboardTopLength = BioReactorCalculations.CalcFreeboardTopLength(CalcFreeStandingWaterTopLength, FreeboardDepth, PondInsideSlope);
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
                    case RadioButtonsSizingMethodsOptionsEnum.OptionSulfateReduction:
                        _calcFreeboardTopWidth = BioReactorCalculations.CalcFreeboardTopWidth(CalcFreeStandingWaterTopWidth, FreeStandingWaterDepth, PondInsideSlope);
                        break;
                    case RadioButtonsSizingMethodsOptionsEnum.OptionAlkalinityGenerationRate:
                        _calcFreeboardTopWidth = BioReactorCalculations.CalcFreeboardTopWidth(CalcFreeStandingWaterTopWidth, FreeStandingWaterDepth, PondInsideSlope);
                        break;
                    case RadioButtonsSizingMethodsOptionsEnum.OptionPilotTesting:
                        _calcFreeboardTopWidth = BioReactorCalculations.CalcFreeboardTopWidth(CalcFreeStandingWaterTopWidth, FreeStandingWaterDepth, PondInsideSlope);
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
            get { return BioReactorCalculations.CalcFreeboardVolume(CalcFreeboardTopLength, CalcFreeboardTopWidth, CalcFreeStandingWaterTopLength, CalcFreeStandingWaterTopWidth, FreeboardDepth); }
            set { ChangeAndNotify(ref _calcFreeboardVolume, value); }
        }

        #endregion

        #region Properties - Sizing Summary: Water

        private double _calcFreeStandingWaterTopLength;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcFreeStandingWaterTopLength
        { 
            get { return BioReactorCalculations.CalcWaterTopLength(CalcBioMixTopLength, FreeStandingWaterDepth, PondInsideSlope); }
            set { ChangeAndNotify(ref _calcFreeStandingWaterTopLength, value); }
        }

        private double _calcFreeStandingWaterTopWidth;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcFreeStandingWaterTopWidth
        {
            get { return BioReactorCalculations.CalcWaterTopWidth(CalcBioMixTopWidth, FreeStandingWaterDepth, PondInsideSlope); }
            set { ChangeAndNotify(ref _calcFreeStandingWaterTopWidth, value); }
        }

        private double _calcFreeStandingWaterSurfaceArea;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcFreeStandingWaterSurfaceArea
        {
            get { return BioReactorCalculations.CalcWaterTopArea(CalcFreeStandingWaterTopLength, CalcFreeStandingWaterTopWidth); }
            set { ChangeAndNotify(ref _calcFreeStandingWaterSurfaceArea, value); }
        }

        private double _calcFreeStandingWaterVolume;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcFreeStandingWaterVolume
        {
            get { return BioReactorCalculations.CalcWaterVolume(CalcFreeStandingWaterTopLength, CalcFreeStandingWaterTopWidth, CalcBioMixTopLength, CalcBioMixTopWidth, FreeStandingWaterDepth); }
            set { ChangeAndNotify(ref _calcFreeStandingWaterVolume, value); }
        }

        #endregion

        #region - Sizing Summary: Compost Mix

        private double _calcCompostMixPorosity;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcCompostMixPorosity
        {
            get { return BioReactorCalculations.CalcCompostMixPorosity(BioMixLimestoneFinesPercentage); }
            set { ChangeAndNotify(ref _calcCompostMixPorosity, value); }
        }

        #endregion

        #region Properties - Sizing Summary: Bio Mix

        private double _calcBioMixBulkDensity;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcBioMixBulkDensity
        {
            get { return BioReactorCalculations.CalcBioMixBulkDensity(BioMixManureDensity, BioMixManurePercentage,
                                                                      BioMixHayDensity, BioMixHayPercentage,
                                                                      BioMixWoodChipsDensity, CalcBioMixWoodChipsPercentage,
                                                                      BioMixOtherDensity, BioMixOtherPercentage,
                                                                      BioMixLimestoneFinesDensity, BioMixLimestoneFinesPercentage); }
            set { ChangeAndNotify(ref _calcBioMixBulkDensity, value); }
        }

        private double _calcBioMixWoodChipsPercentage;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcBioMixWoodChipsPercentage
        {
            get
            {
                double bioMixWoodChipsPercentage = BioReactorCalculations.CalcBioMixWoodChipsPercentage(BioMixManurePercentage, BioMixHayPercentage, BioMixOtherPercentage, BioMixLimestoneFinesPercentage);

                if (bioMixWoodChipsPercentage < 0)
                {
                    ShowBioMixWoodChipsError();                   
                }
                else
                {
                    ShowNoBioMixWoodChipsErrorError();
                }

                return bioMixWoodChipsPercentage;
            }
            set { ChangeAndNotify(ref _calcBioMixWoodChipsPercentage, value); }
        }

        private double _bioMixVolumeFromSizingMethod;
        /// <summary>
        /// Calculated
        /// </summary>
        public double BioMixVolumeFromSizingMethod
        {
            get { return _bioMixVolumeFromSizingMethod; }
            set { ChangeAndNotify(ref _bioMixVolumeFromSizingMethod, value, nameof(BioMixVolumeFromSizingMethod), CalcPropertiesStringArray); }
        }

        private double _calcBioMixBottomLength;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcBioMixBottomLength
        {
            get
            {
                double volume = 0;
                switch (SizingMethodsOptionsProperty)
                {
                    case RadioButtonsSizingMethodsOptionsEnum.OptionSulfateReduction:
                        BottomLengthToWidthRatio = AssignBottomLengthToWidthRatio(BottomLengthToWidthRatio);
                        volume = CalcBioMixVolumeBasedOnSulfateReduction;
                        break;
                    case RadioButtonsSizingMethodsOptionsEnum.OptionAlkalinityGenerationRate:
                        BottomLengthToWidthRatio = AssignBottomLengthToWidthRatio(BottomLengthToWidthRatio);
                        volume = CalcBioMixVolumeBasedOnAlkalinityGenerationRate;
                        break;
                    case RadioButtonsSizingMethodsOptionsEnum.OptionPilotTesting:
                        BottomLengthToWidthRatio = AssignBottomLengthToWidthRatio(BottomLengthToWidthRatio);
                        volume = CalcBioMixVolumeBasedOnPilotTesting;
                        break;
                    case RadioButtonsSizingMethodsOptionsEnum.OptionDimensions:
                        BottomLengthToWidthRatio = CalcBottomLengthToWidthRatioDimensions;
                        volume = CalcBioMixVolumeBasedOnDimensionsEntered;
                        break;
                }
                return BioReactorCalculations.CalcBioMixBottomLength(volume, BioMixDepth, PondInsideSlope, BottomLengthToWidthRatio);
            }
            set { ChangeAndNotify(ref _calcBioMixBottomLength, value); }
        }

        private double _calcBioMixBottomWidth;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcBioMixBottomWidth
        {
            get
            {
                double bioMixBottomWidth = BioReactorCalculations.CalcBioMixBottomWidth(CalcBioMixBottomLength, BottomLengthToWidthRatio);

                if (bioMixBottomWidth < 10.0 && bioMixBottomWidth > 0.0)
                {
                    ShowError();
                }
                else if (bioMixBottomWidth <= 0.0)
                {
                    ShowMajorError();
                    bioMixBottomWidth = 1.0;
                }
                else
                {
                    ShowNoError();
                }

                return bioMixBottomWidth;
            }

            set { ChangeAndNotify(ref _calcBioMixBottomWidth, value); }
        }

        private double _calcBioMixTopLength;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcBioMixTopLength
        {
            get { return BioReactorCalculations.CalcBioMixTopLength(CalcBioMixBottomLength, BioMixDepth, PondInsideSlope); }
            set { ChangeAndNotify(ref _calcBioMixTopLength, value); }
        }

        private double _calcBioMixTopWidth;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcBioMixTopWidth
        {
            get { return BioReactorCalculations.CalcBioMixTopWidth(CalcBioMixBottomWidth, BioMixDepth, PondInsideSlope); }
            set { ChangeAndNotify(ref _calcBioMixTopWidth, value); }
        }

        private double _calcBioMixTopArea;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcBioMixTopArea
        {
            get { return BioReactorCalculations.CalcBioMixTopArea(CalcBioMixTopLength, CalcBioMixTopWidth); }
            set { ChangeAndNotify(ref _calcBioMixTopArea, value); }
        }

        private double _calcBioMixVolume;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcBioMixVolume
        {
            get { return BioReactorCalculations.CalcBioMixVolume(CalcBioMixTopLength, CalcBioMixTopWidth, CalcBioMixBottomLength, CalcBioMixBottomWidth, BioMixDepth); }
            set { ChangeAndNotify(ref _calcBioMixVolume, value, nameof(CalcBioMixVolume), CalcPropertiesStringArray); }
        }

        private double _calcBioMixWeight;
        /// <summary>
        /// Calculated.
        /// </summary>
        public double CalcBioMixWeight
        {
            get { return BioReactorCalculations.CalcBioMixWeight(CalcBioMixVolume, CalcBioMixBulkDensity); }
            set { ChangeAndNotify(ref _calcBioMixWeight, value); }
        }

        private double _calcBioMixLimestoneFinesVolume;
        /// <summary>
        /// Calculated. 
        /// </summary>
        public double CalcBioMixLimestoneFinesVolume
        {
            get { return BioReactorCalculations.CalcBioMixLimestoneFinesVolume(CalcBioMixVolume, BioMixLimestoneFinesPercentage); }
            set { ChangeAndNotify(ref _calcBioMixLimestoneFinesVolume, value); }
        }

        private double _calcBioMixLimestoneFinesWeight;
        /// <summary>
        /// Calculated. 
        /// </summary>
        public double CalcBioMixLimestoneFinesWeight
        {
            get { return BioReactorCalculations.CalcBioMixLimestoneFinesWeight(CalcBioMixLimestoneFinesVolume, BioMixLimestoneFinesDensity); }
            set { ChangeAndNotify(ref _calcBioMixLimestoneFinesWeight, value); }
        }

        private double _calcBioMixManureVolume;
        /// <summary>
        /// Calculated. 
        /// </summary>
        public double CalcBioMixManureVolume
        {
            get { return BioReactorCalculations.CalcBioMixManureVolume(CalcBioMixVolume, BioMixManurePercentage); }
            set { ChangeAndNotify(ref _calcBioMixManureVolume, value); }
        }

        private double _calcBioMixHayVolume;
        /// <summary>
        /// Calculated. 
        /// </summary>
        public double CalcBioMixHayVolume
        {
            get { return BioReactorCalculations.CalcBioMixHayVolume(CalcBioMixVolume, BioMixHayPercentage); }
            set { ChangeAndNotify(ref _calcBioMixHayVolume, value); }
        }

        private double _calcBioMixWoodChipsVolume;
        /// <summary>
        /// Calculated. 
        /// </summary>
        public double CalcBioMixWoodChipsVolume
        {
            get { return BioReactorCalculations.CalcBioMixWoodChipsVolume(CalcBioMixVolume, CalcBioMixWoodChipsPercentage); }
            set { ChangeAndNotify(ref _calcBioMixWoodChipsVolume, value); }
        }

        private double _calcBioMixOtherVolume;
        /// <summary>
        /// Calculated. 
        /// </summary>
        public double CalcBioMixOtherVolume
        {
            get { return BioReactorCalculations.CalcBioMixOtherVolume(CalcBioMixVolume, BioMixOtherPercentage); }
            set { ChangeAndNotify(ref _calcBioMixOtherVolume, value); }
        }

        private double _calcBioMixManureWeight;
        /// <summary>
        /// Calculated. 
        /// </summary>
        public double CalcBioMixManureWeight
        {
            get { return BioReactorCalculations.CalcBioMixManureWeight(CalcBioMixManureVolume, BioMixManureDensity); }
            set { ChangeAndNotify(ref _calcBioMixManureWeight, value); }
        }

        private double _calcBioMixHayWeight;
        /// <summary>
        /// Calculated. 
        /// </summary>
        public double CalcBioMixHayWeight
        {
            get { return BioReactorCalculations.CalcBioMixHayWeight(CalcBioMixHayVolume, BioMixHayDensity); }
            set { ChangeAndNotify(ref _calcBioMixHayWeight, value); }
        }

        private double _calcBioMixWoodChipsWeight;
        /// <summary>
        /// Calculated. 
        /// </summary>
        public double CalcBioMixWoodChipsWeight
        {
            get { return BioReactorCalculations.CalcBioMixWoodChipsWeight(CalcBioMixWoodChipsVolume, BioMixWoodChipsDensity);}
            set { ChangeAndNotify(ref _calcBioMixWoodChipsWeight, value); }
        }

        private double _calcBioMixOtherWeight;
        /// <summary>
        /// Calculated. 
        /// </summary>
        public double CalcBioMixOtherWeight
        {
            get { return BioReactorCalculations.CalcBioMixOtherWeight(CalcBioMixOtherVolume, BioMixOtherDensity); }
            set { ChangeAndNotify(ref _calcBioMixOtherWeight, value); }
        }

        private double _calcBioMixOrganicsVolume;
        /// <summary>
        /// Calculated. 
        /// </summary>
        public double CalcBioMixOrganicsVolume
        {
            get { return BioReactorCalculations.CalcBioMixOrganicsVolume(CalcBioMixVolume, CalcBioMixLimestoneFinesVolume); }
            set { ChangeAndNotify(ref _calcBioMixOrganicsVolume, value); }
        }

        private double _calcBioMixOrganicsVoidsVolume;
        /// <summary>
        /// Calculated. 
        /// </summary>
        public double CalcBioMixOrganicsVoidsVolume
        {
            get { return BioReactorCalculations.CalcBioMixOrganicsVoidsVolume(CalcBioMixVolume, CalcCompostMixPorosity); }
            set { ChangeAndNotify(ref _calcBioMixOrganicsVoidsVolume, value); }
        }

        #endregion

        #region Properties - Sizing Summary: Drainage Aggregate

        private double _calcAggregateTopWidth;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcAggregateTopWidth
        {
            get { return BioReactorCalculations.CalcAggregateTopWidth(CalcBioMixBottomWidth); }
            set { ChangeAndNotify(ref _calcAggregateTopWidth, value); }
        }

        private double _calcAggregateTopLength;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcAggregateTopLength
        {
            get { return BioReactorCalculations.CalcAggregateTopLength(CalcBioMixBottomLength); }
            set { ChangeAndNotify(ref _calcAggregateTopLength, value); }
        }

        private double _calcAggregateBottomWidth;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcAggregateBottomWidth
        {
            get { return BioReactorCalculations.CalcAggregateBottomWidth(CalcAggregateTopWidth, AggregateDepth, PondInsideSlope); }
            set { ChangeAndNotify(ref _calcAggregateBottomWidth, value); }
        }

        private double _calcAggregateBottomLength;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcAggregateBottomLength
        {
            get { return BioReactorCalculations.CalcAggregateBottomLength(CalcAggregateTopLength, AggregateDepth, PondInsideSlope); }
            set { ChangeAndNotify(ref _calcAggregateBottomLength, value); }
        }

        private double _calcAggregateVolume;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcAggregateVolume
        {
            get { return BioReactorCalculations.CalcAggregateVolume(CalcAggregateTopLength, CalcAggregateTopWidth, CalcAggregateBottomLength, CalcAggregateBottomWidth, AggregateDepth); }
            set { ChangeAndNotify(ref _calcAggregateVolume, value); }
        }

        private double _calcAggregateWeight;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcAggregateWeight
        {
            get { return BioReactorCalculations.CalcAggregateWeight(CalcAggregateVolume, AggregateBulkDensity); }
            set { ChangeAndNotify(ref _calcAggregateWeight, value); }
        }

        #endregion

        #region Properties - Sizing Summary: Pipe

        private double _calcTrunkPipeSegmentLengthTotal;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcTrunkPipeSegmentLengthTotal
        {
            get { return BioReactorCalculations.CalcTrunkPipeSegmentLengthTotal(CalcAggregateBottomLength); }
            set { ChangeAndNotify(ref _calcTrunkPipeSegmentLengthTotal, value); }
        }

        private double _calcTrunkPipeCouplerQuantity;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcTrunkPipeCouplerQuantity
        {
            get { return BioReactorCalculations.CalcTrunkPipeCouplerQuantity(CalcTrunkPipeSegmentLengthTotal, TrunkPipeSegmentLength); }
            set { ChangeAndNotify(ref _calcTrunkPipeCouplerQuantity, value); }
        }

        private double _calcSpurPipeQuantity;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcSpurPipeQuantity
        {
            get { return BioReactorCalculations.CalcSpurPipeQuantity(SpurPipeSpacing, CalcAggregateBottomLength); }
            set { ChangeAndNotify(ref _calcSpurPipeQuantity, value); }
        }

        private double _calcSpurPipeSegmentLengthTotal;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcSpurPipeSegmentLengthTotal
        {
            get { return BioReactorCalculations.CalcSpurPipeSegmentLengthTotal(CalcSpurPipeQuantity, CalcAggregateBottomWidth); }
            set { ChangeAndNotify(ref _calcSpurPipeSegmentLengthTotal, value); }
        }

        private double _calcSpurPipeCouplerQuantity;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcSpurPipeCouplerQuantity
        {
            get { return BioReactorCalculations.CalcSpurPipeCouplerQuantity(CalcSpurPipeQuantity, SpurPipeSegmentLength, CalcAggregateBottomWidth); }
            set { ChangeAndNotify(ref _calcSpurPipeCouplerQuantity, value); }
        }
        #endregion

        #region Properties - Sizing Summary: Liner

        private double _calcLinerSlopeLength;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLinerSlopeLength
        {
            get { return BioReactorCalculations.CalcLinerSlopeLength(FreeboardDepth, FreeStandingWaterDepth, BioMixDepth, AggregateDepth, PondInsideSlope); }
            set { ChangeAndNotify(ref _calcLinerSlopeLength, value); }
        }

        private double _calcNonWovenGeotextileSlopeLength;
        /// <summary>
        /// Calculated.  Same as synthetic liner area.
        /// </summary>
        public double CalcNonWovenGeotextileSlopeLength
        {
            get { return BioReactorCalculations.CalcNonWovenGeotextileSlopeLength(BioMixDepth, AggregateDepth, PondInsideSlope); }
            set { ChangeAndNotify(ref _calcNonWovenGeotextileSlopeLength, value); }
        }

        private double _calcSyntheticLinerArea;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcSyntheticLinerArea
        {
            get { return BioReactorCalculations.CalcSyntheticLinerArea(CalcFreeboardTopLength, CalcFreeboardTopWidth,
                                                                       CalcAggregateBottomLength, CalcAggregateBottomWidth,
                                                                       CalcLinerSlopeLength); }
            set { ChangeAndNotify(ref _calcSyntheticLinerArea, value); }
        }

        private double _calcClayLinerArea;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcClayLinerArea
        {
            get { return BioReactorCalculations.CalcClayLinerArea(CalcFreeboardTopLength, CalcFreeboardTopWidth,
                                                                  CalcAggregateBottomLength, CalcAggregateBottomWidth,
                                                                  CalcBioMixBottomLength, CalcLinerSlopeLength); }
            set { ChangeAndNotify(ref _calcClayLinerArea, value); }
        }

        private double _calcNonWovenGeotextileArea;
        /// <summary>
        /// Calculated.  
        /// </summary>
        public double CalcNonWovenGeotextileArea
        {
            get { return BioReactorCalculations.CalcNonWovenGeoTextileArea(BioMixDepth, AggregateDepth,
                                                                           CalcAggregateBottomLength, CalcAggregateBottomWidth,
                                                                           CalcBioMixBottomWidth, CalcNonWovenGeotextileSlopeLength); }
            set { ChangeAndNotify(ref _calcNonWovenGeotextileArea, value); }
        }

        private double _calcGeosyntheticClayLinerArea;
        /// <summary>
        /// Calculated.  Same as synthetic liner area.
        /// </summary>
        public double CalcGeosyntheticClayLinerArea
        {
            get { return BioReactorCalculations.CalcGeosyntheticClayLinerArea(CalcFreeboardTopLength, CalcFreeboardTopWidth,
                                                                              CalcAggregateBottomLength, CalcAggregateBottomWidth,
                                                                              CalcLinerSlopeLength); }
            set { ChangeAndNotify(ref _calcGeosyntheticClayLinerArea, value); }
        }

        private double _calcClayLinerVolume;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcClayLinerVolume
        {
            get { return BioReactorCalculations.CalcClayLinerVolume(CalcClayLinerArea, ClayLinerThickness); }
            set { ChangeAndNotify(ref _calcClayLinerVolume, value); }
        }

        private double _calcGeosyntheticClayLinerVolume;
        /// <summary>
        /// Calculated.
        /// </summary>
        public double CalcGeosyntheticClayLinerVolume
        {
            get { return BioReactorCalculations.CalcGeosyntheticClayLinerVolume(CalcGeosyntheticClayLinerArea, GeosyntheticClayLinerSoilCover); }
            set { ChangeAndNotify(ref _calcGeosyntheticClayLinerVolume, value); }
        }

        private decimal _calcGeosyntheticClayLinerCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcGeosyntheticClayLinerCost
        {
            get { return BioReactorCalculations.CalcGeosyntheticClayLinerCost(CalcGeosyntheticClayLinerArea, GeosyntheticClayLinerUnitCost); }
            set { ChangeAndNotify(ref _calcGeosyntheticClayLinerCost, value); }
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

        private double _calcWaterLayerRetentionTime;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcWaterLayerRetentionTime
        {
            get { return BioReactorCalculations.CalcWaterRetentionTime(CalcFreeStandingWaterVolume, DesignFlow); }
            set { ChangeAndNotify(ref _calcWaterLayerRetentionTime, value); }
        }

        private double _calcBioMixLayerRetentionTime;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcBioMixLayerRetentionTime
        {
            get { return BioReactorCalculations.CalcBioMixLayerRetentionTime(CalcBioMixVolume, CalcCompostMixPorosity, DesignFlow); }
            set { ChangeAndNotify(ref _calcBioMixLayerRetentionTime, value); }
        }

        #endregion

        #region Properties - Capital Costs

        private decimal _calcExcavationCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcExcavationCost
        {
            get { return BioReactorCalculations.CalcExcavationCost(CalcExcavationVolume, ExcavationUnitCost); }
            set { ChangeAndNotify(ref _calcExcavationCost, value); }
        }

        private decimal _calcBioMixManureCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcBioMixManureCost
        {
            get { return BioReactorCalculations.CalcBioMixManureCost(CalcBioMixManureWeight, BioMixManureUnitCost); }
            set { ChangeAndNotify(ref _calcBioMixManureCost, value); }
        }

        private decimal _calcBioMixHayCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcBioMixHayCost
        {
            get { return BioReactorCalculations.CalcBioMixHayCost(CalcBioMixHayWeight, BioMixHayUnitCost); }
            set { ChangeAndNotify(ref _calcBioMixHayCost, value); }
        }

        private decimal _calcBioMixWoodChipsCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcBioMixWoodChipsCost
        {
            get { return BioReactorCalculations.CalcBioMixWoodChipsCost(CalcBioMixWoodChipsWeight, BioMixWoodChipsUnitCost); }
            set { ChangeAndNotify(ref _calcBioMixWoodChipsCost, value); }
        }

        private decimal _calcBioMixOtherCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcBioMixOtherCost
        {
            get { return BioReactorCalculations.CalcBioMixOtherCost(CalcBioMixOtherWeight, BioMixOtherUnitCost); }
            set { ChangeAndNotify(ref _calcBioMixOtherCost, value); }
        }

        private decimal _calcBioMixLimestoneFinesCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcBioMixLimestoneFinesCost
        {
            get { return BioReactorCalculations.CalcBioMixLimestoneFinesCost(CalcBioMixLimestoneFinesWeight, BioMixLimestoneFinesUnitCost); }
            set { ChangeAndNotify(ref _calcBioMixLimestoneFinesCost, value); }
        }

        private decimal _calcBioMixPlacementCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcBioMixPlacementCost
        {
            get { return BioReactorCalculations.CalcBioMixPlacementCost(CalcBioMixVolume, BioMixPlacementUnitCost); }
            set { ChangeAndNotify(ref _calcBioMixPlacementCost, value); }
        }

        private decimal _calcBioMixCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcBioMixCost
        {
            get { return BioReactorCalculations.CalcBioMixCost(CalcBioMixManureCost, CalcBioMixHayCost, CalcBioMixWoodChipsCost,
                                                               CalcBioMixOtherCost, CalcBioMixLimestoneFinesCost, CalcBioMixPlacementCost); }
            set { ChangeAndNotify(ref _calcBioMixCost, value); }
        }

        private decimal _calcAggregateCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcAggregateCost
        {
            get { return BioReactorCalculations.CalcAggregateCost(CalcAggregateVolume, CalcAggregateWeight, AggregateUnitCost, AggregatePlacementUnitCost); }
            set { ChangeAndNotify(ref _calcAggregateCost, value); }
        }


        private decimal _calcInOutPipeCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcInOutPipeCost
        {
            get { return BioReactorCalculations.CalcInOutPipeCost(InOutPipeLength, InOutPipeUnitCost); }
            set { ChangeAndNotify(ref _calcInOutPipeCost, value); }
        }

        private decimal _calcTrunkPipeCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcTrunkPipeCost
        {
            get { return BioReactorCalculations.CalcTrunkPipeCost(CalcTrunkPipeSegmentLengthTotal, TrunkPipeUnitCost); }
            set { ChangeAndNotify(ref _calcTrunkPipeCost, value); }
        }

        private decimal _calcTrunkPipeCouplerCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcTrunkPipeCouplerCost
        {
            get { return BioReactorCalculations.CalcTrunkPipeCouplerCost(CalcTrunkPipeCouplerQuantity, TrunkPipeCouplerUnitCost); }
            set { ChangeAndNotify(ref _calcTrunkPipeCouplerCost, value); }
        }

        private decimal _calcSpurPipeCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcSpurPipeCost
        {
            get { return BioReactorCalculations.CalcSpurPipeCost(CalcSpurPipeSegmentLengthTotal, SpurPipeUnitCost); }
            set { ChangeAndNotify(ref _calcSpurPipeCost, value); }
        }

        private decimal _calcSpurPipeCouplerCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcSpurPipeCouplerCost
        {
            get { return BioReactorCalculations.CalcSpurPipeCouplerCost(CalcSpurPipeCouplerQuantity, SpurPipeCouplerUnitCost); }
            set { ChangeAndNotify(ref _calcSpurPipeCouplerCost, value); }
        }

        private decimal _calcTeeConnectorCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcTeeConnectorCost
        {
            get { return BioReactorCalculations.CalcTeeConnectorCost(CalcSpurPipeQuantity, TeeConnectorUnitCost); }
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
                decimal amdtreatPipe = BioReactorCalculations.CalcAmdtreatPipeCost(CalcInOutPipeCost,
                                                                                   CalcTrunkPipeCost,
                                                                                   CalcTrunkPipeCouplerCost,
                                                                                   CalcTeeConnectorCost,
                                                                                   CalcSpurPipeCost,
                                                                                   CalcSpurPipeCouplerCost,
                                                                                   InOutPipeLength,
                                                                                   CalcAggregateBottomLength,
                                                                                   CalcSpurPipeSegmentLengthTotal,
                                                                                   InOutPipeInstallRate,
                                                                                   TrunkSpurPipeInstallRate,
                                                                                   LaborRate);

                decimal customPipe = BioReactorCalculations.CalcCustomPipeCost(CustomPipeLength1, CustomPipeUnitCost1,
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

        private decimal _calcValveCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcValveCost
        {
            get { return BioReactorCalculations.CalcValveCost(ValveQuantity, ValveUnitCost); }
            set { ChangeAndNotify(ref _calcValveCost, value); }

        }

        private decimal _calcOtherItemsCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcOtherItemsCost
        {
            get
            {
                return BioReactorCalculations.CalcOtherItemsCost(CalcValveCost,
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
            get { return BioReactorCalculations.CalcClayLinerCost(CalcClayLinerVolume, ClayLinerUnitCost); }
            set { ChangeAndNotify(ref _calcClayLinerCost, value); }
        }

        private decimal _calcSyntheticLinerCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcSyntheticLinerCost
        {
            get { return BioReactorCalculations.CalcSyntheticLinerCost(CalcSyntheticLinerArea, SyntheticLinerUnitCost); }
            set { ChangeAndNotify(ref _calcSyntheticLinerCost, value); }
        }

        private decimal _calcGeosyntheticClayLinerCoverCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcGeosyntheticClayLinerCoverCost
        {
            get { return BioReactorCalculations.CalcGeosyntheticClayLinerCoverCost(CalcGeosyntheticClayLinerVolume, GeosyntheticClayLinerCoverUnitCost); }
            set { ChangeAndNotify(ref _calcGeosyntheticClayLinerCoverCost, value); }
        }

        private decimal _calcNonWovenGeotextileCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcNonWovenGeotextileCost
        {
            get { return BioReactorCalculations.CalcNonWovenGeotextileCost(CalcNonWovenGeotextileArea, NonWovenGeotextileUnitCost); }
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
                _calcCapitalCostTotal = BioReactorCalculations.CalcCapitalCostTotal(CalcBioMixCost, 
                                                                                    CalcAggregateCost, 
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
            set { ChangeAndNotify(ref _capitalCostData, value, nameof(CapitalCostData)); }
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
                        _calcAnnualCost = BioReactorCalculations.CalcAnnualCost(AnnualCostMultiplier, CalcCapitalCostTotal);
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

        private double _recapitalizationCostLifeCycleBioMix;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostLifeCycleBioMix
        {
            get { return _recapitalizationCostLifeCycleBioMix; }
            set { ChangeAndNotify(ref _recapitalizationCostLifeCycleBioMix, value, nameof(RecapitalizationCostLifeCycleBioMix), CalcPropertiesStringArray); }
        }

        private double _RecapitalizationCostLifeCycleAggregate;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostLifeCycleAggregate
        {
            get { return _RecapitalizationCostLifeCycleAggregate; }
            set { ChangeAndNotify(ref _RecapitalizationCostLifeCycleAggregate, value, nameof(RecapitalizationCostLifeCycleAggregate), CalcPropertiesStringArray); }
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

        private double _recapitalizationCostPercentReplacementBioMix;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostPercentReplacementBioMix
        {
            get { return _recapitalizationCostPercentReplacementBioMix; }
            set { ChangeAndNotify(ref _recapitalizationCostPercentReplacementBioMix, value, nameof(RecapitalizationCostPercentReplacementBioMix), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostPercentReplacementAggregate;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostPercentReplacementAggregate
        {
            get { return _recapitalizationCostPercentReplacementAggregate; }
            set { ChangeAndNotify(ref _recapitalizationCostPercentReplacementAggregate, value, nameof(RecapitalizationCostPercentReplacementAggregate), CalcPropertiesStringArray); }
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

        private decimal _calcRapitalizationCostBioMix;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostBioMix
        {
            get
            {
                return BioReactorCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
                                                                       RecapitalizationCostInflationRate, RecapitalizationCostLifeCycleBioMix,
                                                                       CalcBioMixCost, RecapitalizationCostPercentReplacementBioMix);
            }
            set { ChangeAndNotify(ref _calcRapitalizationCostBioMix, value); }
        }

        private decimal _calcRecapitalizationCostAggregate;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostAggregate
        {
            get
            {
                return BioReactorCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
                                                                       RecapitalizationCostInflationRate, RecapitalizationCostLifeCycleAggregate,
                                                                       CalcAggregateCost, RecapitalizationCostPercentReplacementAggregate);
            }
            set { ChangeAndNotify(ref _calcRecapitalizationCostAggregate, value); }
        }

        private decimal _calcRapitalizationCostLiner;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostLiner
        {
            get
            {
                return BioReactorCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
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
                return BioReactorCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
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
                return BioReactorCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
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
            get { return BioReactorCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn, 
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
                    case "BioMix":
                        item.MaterialCostDefault = CalcBioMixCost;
                        break;
                    case "Aggregate":
                        item.MaterialCostDefault = CalcAggregateCost;
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
                    case "BioMix":
                        item.TotalCost = CalcRecapitalizationCostBioMix;
                        break;
                    case "Aggregate":
                        item.TotalCost = CalcRecapitalizationCostAggregate;
                        break;
                    case "Liner":
                        item.TotalCost = CalcRecapitalizationCostLiner;
                        break;
                    case "Pipe":
                        item.TotalCost = CalcRecapitalizationCostPipe;
                        break;
                    case "OtherItems":
                        item.TotalCost = CalcRecapitalizationCostOtherItems;
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
                Name = "Bio Mix",
                NameFixed = "BioMix",
                LifeCycle = RecapitalizationCostLifeCycleBioMix,
                PercentReplacement = RecapitalizationCostPercentReplacementBioMix,
                MaterialCostDefault = CalcBioMixCost,
                UseCustomCost = false,
                MaterialCostCustom = 0m,
                TotalCost = CalcRecapitalizationCostBioMix
            });
            RecapMaterials.Add(new RecapMaterial
            {
                IsSelected = true,
                Name = "Aggregate",
                NameFixed = "Aggregate",
                LifeCycle = RecapitalizationCostLifeCycleAggregate,
                PercentReplacement = RecapitalizationCostPercentReplacementAggregate,
                MaterialCostDefault = CalcAggregateCost,
                UseCustomCost = false,
                MaterialCostCustom = 0m,
                TotalCost = CalcRecapitalizationCostAggregate
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
            ((RecapMaterial)sender).TotalCost = BioReactorCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
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
            customDialogViewModel.Message = Resources.infoBIO;
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
                        string message = Resources.infoWaterQualityBIO;
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
                        string message = Resources.infoSizingMethodsBIO;
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
                        string message = Resources.infoSystemPropertiesBIO;
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
                        string message = Resources.infoLayerMaterialsBIO;
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
                        string message = Resources.infoOtherItemsBIO;
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
                        string message = Resources.infoSizingSummaryBIO;
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
                        string message = Resources.infoCapitalCostBIO;
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
                        string message = Resources.infoAnnualCostBIO;
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
                        string message = Resources.infoRecapitalizationCostBIO;
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

        #region Error Handling and Information, Flyouts

        private double _calcMaxPondDepth;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcMaxPondDepth
        {
            get { return BioReactorCalculations.CalcMaxPondDepth(CalcFreeboardTopWidth); }
            set { ChangeAndNotify(ref _calcMaxPondDepth, value); }
        }

        private double _calcPondDepth;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcPondDepth
        { 
            get { return BioReactorCalculations.CalcPondDepth(FreeboardDepth, FreeStandingWaterDepth, BioMixDepth, AggregateDepth); }
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

        private string _errorMessageBioMixMaterialsPercentage;
        public string ErrorMessageBioMixMaterialsPercentage
        {
            get { return _errorMessageBioMixMaterialsPercentage; }
            set { ChangeAndNotify(ref _errorMessageBioMixMaterialsPercentage, value); }
        }

        private string _errorMessageShortBioMixMaterialsPercentage;
        public string ErrorMessageShortBioMixMaterialsPercentage
        {
            get { return _errorMessageShortBioMixMaterialsPercentage; }
            set { ChangeAndNotify(ref _errorMessageShortBioMixMaterialsPercentage, value); }
        }

        private bool _isErrorBioMixWoodChips;
        public bool IsErrorBioMixWoodChips
        {
            get { return _isErrorBioMixWoodChips; }
            set { ChangeAndNotify(ref _isErrorBioMixWoodChips, value); }
        }

        private void ShowNoError()
        {
            if (IsErrorBioMixWoodChips)
            {
                IsError = true;
            }
            else
            {
                IsError = false;
                IsMajorError = false;
                IsErrorBioMixWoodChips = false;
                ErrorMessage = "";
                ErrorMessageShort = "";
            }
        }

        private void ShowError()
        {
            IsError = true;
            IsMajorError = false;
            IsOpenFlyoutError = true;
            ErrorMessage = Resources.errorPondBIO;
            ErrorMessageShort = "ERROR - Pond cannot be constructed";
            IsExpandedFreeboard = true;
            IsExpandedWater = true;
            IsExpandedBioMix = true;
            IsExpandedDrainageAggregate = true;
        }

        private void ShowMajorError()
        {
            IsError = true;
            IsMajorError = true;
            IsOpenFlyoutError = true;
            ErrorMessage = Resources.errorPondMajorBIO;
            ErrorMessageShort = "MAJOR ERROR - All calculations are invalid";
            IsExpandedFreeboard = true;
            IsExpandedWater = true;
            IsExpandedBioMix = true;
            IsExpandedDrainageAggregate = true;
        }

        private void ShowBioMixWoodChipsError()
        {
            IsError = true;
            IsErrorBioMixWoodChips = true;
            IsOpenFlyoutError = true;
            ErrorMessage = Resources.errorPondBIOWoodChips;
            ErrorMessageShort = "The Bio Mix Wood Chips Percentage is less than 0%.";
        }

        private void ShowNoBioMixWoodChipsErrorError()
        {
            IsErrorBioMixWoodChips = false;
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
        public bool IsExpandedBioMix
        {
            get { return _isExpandedCompostMix; }
            set { ChangeAndNotify(ref _isExpandedCompostMix, value); }
        }

        private bool _isExpandedLimestone;
        public bool IsExpandedDrainageAggregate
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

        public BioReactorViewModel(IDialogCoordinator dialogCoordinator)
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
            ModuleType = "BioReactor";
            ModuleName = "Untitled module";
            ModuleTreatmentType = "Passive";
            ModuleId = random.Next(1000);

            IsError = false;
            IsMajorError = false;
            IsErrorBioMixWoodChips = false;
            ErrorMessage = "";
            ErrorMessageShort = "";

            // Initialize check boxes
            PipeOptionAmdtreat = true;
            PipeOptionCustom = false;

            // Initialize radio buttons
            SizingMethodsOptionsProperty = RadioButtonsSizingMethodsOptionsEnum.OptionSulfateReduction;
            LinerOptionsProperty = RadioButtonsLinerOptionsEnum.OptionNoLiner;
            AnnualCostOptionsProperty = RadioButtonsAnnualCostOptionsEnum.OptionAnnualCostMultiplier;

            // Dummy variables to force syncing
            Dummy = 0;

            // Read the xml data file and assign property values accordingly.
            OpenXmlAndAssignValues(@"..\..\Data\default-data-bioreactor.xml");

            // Make an initial backup of the bottom length to width ratio
            BottomLengthToWidthRatioBackup = BottomLengthToWidthRatio;

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
