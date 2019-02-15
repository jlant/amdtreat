﻿using AMDTreat.Commands;
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

    public class ManganeseRemovalBedViewModel : PropertyChangedBase, IObserver<SharedData>
    {

        #region Radio Button Enums

        /// <summary>
        ///  Radio button binding with enumeration for sizing methods
        /// </summary>
        public enum RadioButtonsSizingMethodsOptionsEnum
        {
            OptionRetentionTime,
            OptionKinetics,
            OptionLimestoneQuantity,
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

        private double _dissolvedManganese;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double DissolvedManganese
        {
            get { return _dissolvedManganese; }
            set { ChangeAndNotify(ref _dissolvedManganese, value, nameof(DissolvedManganese), CalcPropertiesStringArray); }
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
            set
            {
                ChangeAndNotify(ref _retentionTime, value, nameof(RetentionTime), CalcPropertiesStringArray);
            }
        }

        private double _calcWaterVolumeRetentionTime;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcWaterVolumeRetentionTime
        {
            get { return ManganeseRemovalBedCalculations.CalcWaterVolumeBasedOnRetentionTime(RetentionTime, DesignFlow); }
            set { ChangeAndNotify(ref _calcWaterVolumeRetentionTime, value); }
        }

        private double _calcLimestoneVolumeRetentionTime;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLimestoneVolumeRetentionTime
        {
            get { return ManganeseRemovalBedCalculations.CalcLimestoneVolumeBasedOnRetentionTime(CalcWaterVolumeRetentionTime, LimestoneLayerPorosity); }
            set { ChangeAndNotify(ref _calcLimestoneVolumeRetentionTime, value); }
        }

        private double _calcLimestoneWeightRetentionTime;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLimestoneWeightRetentionTime
        {
            get { return ManganeseRemovalBedCalculations.CalcMaterialWeight(CalcLimestoneVolumeRetentionTime, CalcLimestoneBulkDensity); }
            set { ChangeAndNotify(ref _calcLimestoneWeightRetentionTime, value); }
        }

        #endregion

        #region Properties - Sizing Methods: Kinetics

        private double _kineticsRateConstant;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double KineticsRateConstant
        {
            get { return _kineticsRateConstant; }
            set { ChangeAndNotify(ref _kineticsRateConstant, value, nameof(KineticsRateConstant), CalcPropertiesStringArray); }
        }

        private double _kineticsEffluentManganeseConcentration;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double KineticsEffluentManganeseConcentration
        {
            get { return _kineticsEffluentManganeseConcentration;}
            set { ChangeAndNotify(ref _kineticsEffluentManganeseConcentration, value, nameof(KineticsEffluentManganeseConcentration), CalcPropertiesStringArray); }
        }

        private double _kineticsParticleSurfaceArea;
        /// <summary>
        ///  Set based upon user selection of AASHTO Size Options 
        /// </summary>
        public double KineticsParticleSurfaceArea
        {
            get { return _kineticsParticleSurfaceArea; }
            set { ChangeAndNotify(ref _kineticsParticleSurfaceArea, value, nameof(KineticsParticleSurfaceArea), CalcPropertiesStringArray); }
        }

        private double _kineticsParticleVolume;
        /// <summary>
        ///  Set based upon user selection of AASHTO Size Options 
        /// </summary>
        public double KineticsParticleVolume
        {
            get { return _kineticsParticleVolume; }
            set { ChangeAndNotify(ref _kineticsParticleVolume, value, nameof(KineticsParticleVolume), CalcPropertiesStringArray); }
        }

        private double _calcKineticsLimestoneSurfaceArea;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcKineticsLimestoneSurfaceArea
        {
            get { return ManganeseRemovalBedCalculations.CalcKineticsLimestoneSurfaceArea(KineticsParticleSurfaceArea, KineticsParticleVolume, LimestoneLayerPorosity); }
            set { ChangeAndNotify(ref _calcKineticsLimestoneSurfaceArea, value); }
        }

        private double _calcKineticsSaturatedLimestoneArea;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcKineticsSaturatedLimestoneArea
        {
            get { return ManganeseRemovalBedCalculations.CalcKineticsSaturatedLimestoneArea(DesignFlow, KineticsEffluentManganeseConcentration, DissolvedManganese,
                                                                                            KineticsRateConstant, CalcKineticsLimestoneSurfaceArea, LimestoneDepth); }
            set { ChangeAndNotify(ref _calcKineticsSaturatedLimestoneArea, value); }
        }

        private double _calcKineticsSaturatedLimestoneAreaSquareFt;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcKineticsSaturatedLimestoneAreaSquareFt
        {
            get {return ManganeseRemovalBedCalculations.CalcKineticsSaturatedLimestoneAreaSquareFt(CalcKineticsSaturatedLimestoneArea); }
            set { ChangeAndNotify(ref _calcKineticsSaturatedLimestoneAreaSquareFt, value); }
        }


        private double _calcLimestoneLayerBottomLengthKinetics;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLimestoneLayerBottomLengthKinetics
        {
            get
            {
                return ManganeseRemovalBedCalculations.CalcLimestoneLayerBottomLengthBasedOnKinetics(CalcKineticsSaturatedLimestoneAreaSquareFt,
                                                                                                     LimestoneDepth,
                                                                                                     PondInsideSlope,
                                                                                                     BottomLengthToWidthRatio);
            }

            set { ChangeAndNotify(ref _calcLimestoneLayerBottomLengthKinetics, value); }
        }

        private double _calcLimestoneLayerBottomWidthKinetics;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLimestoneLayerBottomWidthKinetics
        {
            get { return ManganeseRemovalBedCalculations.CalcLimestoneLayerBottomWidthBasedOnKinetics(CalcLimestoneLayerBottomLengthKinetics, BottomLengthToWidthRatio); }
            set { ChangeAndNotify(ref _calcLimestoneLayerBottomWidthKinetics, value); }
        }

        private double _calcLimestoneLayerTopLengthKinetics;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLimestoneLayerTopLengthKinetics
        {
            get { return ManganeseRemovalBedCalculations.CalcLimestoneLayerTopLengthBasedOnKinetics(CalcLimestoneLayerBottomLengthKinetics, LimestoneDepth, PondInsideSlope); }
            set { ChangeAndNotify(ref _calcLimestoneLayerTopLengthKinetics, value); }
        }

        private double _calcLimestoneLayerTopWidthKinetics;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLimestoneLayerTopWidthKinetics
        {
            get { return ManganeseRemovalBedCalculations.CalcLimestoneLayerTopWidthBasedOnKinetics(CalcLimestoneLayerBottomWidthKinetics, LimestoneDepth, PondInsideSlope); }
            set { ChangeAndNotify(ref _calcLimestoneLayerTopWidthKinetics, value); }
        }

        private double _calcLimestoneVolumeKinetics;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLimestoneVolumeKinetics
        {
            get
            {
                return ManganeseRemovalBedCalculations.CalcLimestoneVolumeBasedOnKinetics(CalcLimestoneLayerBottomLengthKinetics,
                                                                                          CalcLimestoneLayerBottomWidthKinetics,
                                                                                          CalcLimestoneLayerTopLengthKinetics,
                                                                                          CalcLimestoneLayerTopWidthKinetics,
                                                                                          LimestoneDepth);
            }

            set { ChangeAndNotify(ref _calcLimestoneVolumeKinetics, value); }
        }

        private double _calcLimestoneWeightKinetics;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLimestoneWeightKinetics
        {
            get { return ManganeseRemovalBedCalculations.CalcMaterialWeight(CalcLimestoneVolumeKinetics, CalcLimestoneBulkDensity); }
            set { ChangeAndNotify(ref _calcLimestoneWeightKinetics, value); }
        }

        #endregion

        #region Properties - Sizing Methods: User-Specified Limestone

        private double _limestoneQuantity;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double LimestoneQuantity
        {
            get { return _limestoneQuantity; }
            set { ChangeAndNotify(ref _limestoneQuantity, value, nameof(LimestoneQuantity), CalcPropertiesStringArray); }
        }

        private double _calcLimestoneVolumeLimestoneQuantity;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLimestoneVolumeLimestoneQuantity
        {
            get { return ManganeseRemovalBedCalculations.CalcLimestoneVolumeBasedOnLimestoneEntered(LimestoneQuantity, CalcLimestoneBulkDensity); }
            set { ChangeAndNotify(ref _calcLimestoneVolumeLimestoneQuantity, value); }
        }

        private double _calcLimestoneWeightLimestoneQuantity;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLimestoneWeightLimestoneQuantity
        {
            get { return ManganeseRemovalBedCalculations.CalcMaterialWeight(CalcLimestoneVolumeLimestoneQuantity, CalcLimestoneBulkDensity); }
            set { ChangeAndNotify(ref _calcLimestoneWeightLimestoneQuantity, value); }
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

        private double _calcLimestoneBottomLengthDimensions;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLimestoneBottomLengthDimensions
        {
            get { return ManganeseRemovalBedCalculations.CalcLimestoneBottomDimensionBasedOnDimensionsEntered(FreeboardTopLengthDimensions, FreeboardDepth, LimestoneDepth, PondInsideSlope); }
            set { ChangeAndNotify(ref _calcLimestoneBottomLengthDimensions, value); }
        }

        private double _calcLimestoneBottomWidthDimensions;
        /// <summary>
        /// Calculated  
        /// Checking that the limestone bottom width value is not equal to zero.  If limestone bottom width value is equal to zero, then
        /// an error message is displayed. A value of 1.0 is returned in order to prevent calculations for the bottom length to width (CalcBottomLayerLengthToWidthRatio*)
        /// ratio from dividing by zero.
        /// </summary>
        public double CalcLimestoneBottomWidthDimensions
        {
            get
            {
                double value = ManganeseRemovalBedCalculations.CalcLimestoneBottomDimensionBasedOnDimensionsEntered(FreeboardTopWidthDimensions, FreeboardDepth, LimestoneDepth, PondInsideSlope);

                if (value == 0.0)
                {
                    ShowMajorError();

                    return 1.0;
                }
                return value;
            }

            set { ChangeAndNotify(ref _calcLimestoneBottomWidthDimensions, value); }
        }

        private double _calcLimestoneVolumeDimensions;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLimestoneVolumeDimensions
        {
            get
            {
                return ManganeseRemovalBedCalculations.CalcLimestoneVolumeBasedOnDimensionsEntered(FreeboardTopLengthDimensions, FreeboardTopWidthDimensions,
                                                                                            FreeboardDepth, LimestoneDepth, PondInsideSlope);
            }
            set { ChangeAndNotify(ref _calcLimestoneVolumeDimensions, value); }
        }

        private double _calcLimestoneWeightDimensions;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLimestoneWeightDimensions
        {
            get { return ManganeseRemovalBedCalculations.CalcMaterialWeight(CalcLimestoneVolumeDimensions, CalcLimestoneBulkDensity); }
            set { ChangeAndNotify(ref _calcLimestoneWeightDimensions, value); }
        }

        private double _calcBottomLengthToWidthRatioDimensions;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcBottomLengthToWidthRatioDimensions
        {
            get { return ManganeseRemovalBedCalculations.CalcBottomLayerLengthToWidthRatioBasedOnDimensionsEntered(CalcLimestoneBottomLengthDimensions, CalcLimestoneBottomWidthDimensions); }
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

        #region Properties - Layer Materials: Limestone

        private double _limestoneDepth;
        /// <summary>
        /// User specified
        /// </summary>
        public double LimestoneDepth
        {
            get { return _limestoneDepth; }
            set { ChangeAndNotify(ref _limestoneDepth, value, nameof(LimestoneDepth), CalcPropertiesStringArray); }
        }

        private double _LimestoneLayerPorosity;
        /// <summary>
        /// User specified
        /// </summary>
        public double LimestoneLayerPorosity
        {
            get { return _LimestoneLayerPorosity; }
            set { ChangeAndNotify(ref _LimestoneLayerPorosity, value, nameof(LimestoneLayerPorosity), CalcPropertiesStringArray); }
        }

        private string _stoneGradationName;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string StoneGradationName
        {
            get { return _stoneGradationName; }
            set { ChangeAndNotify(ref _stoneGradationName, value, nameof(StoneGradationName), CalcPropertiesStringArray); }
        }

        private string _stoneGradationAASHTO1Name;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string StoneGradationAASHTO1Name
        {
            get { return _stoneGradationAASHTO1Name; }
            set { ChangeAndNotify(ref _stoneGradationAASHTO1Name, value, nameof(StoneGradationAASHTO1Name), CalcPropertiesStringArray); }
        }

        private string _stoneGradationAASHTO3Name;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string StoneGradationAASHTO3Name
        {
            get { return _stoneGradationAASHTO3Name; }
            set { ChangeAndNotify(ref _stoneGradationAASHTO3Name, value, nameof(StoneGradationAASHTO3Name), CalcPropertiesStringArray); }
        }

        private string _stoneGradationAASHTO5Name;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string StoneGradationAASHTO5Name
        {
            get { return _stoneGradationAASHTO5Name; }
            set { ChangeAndNotify(ref _stoneGradationAASHTO5Name, value, nameof(StoneGradationAASHTO5Name), CalcPropertiesStringArray); }
        }

        private string _stoneGradationAASHTO57Name;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string StoneGradationAASHTO57Name
        {
            get { return _stoneGradationAASHTO57Name; }
            set { ChangeAndNotify(ref _stoneGradationAASHTO57Name, value, nameof(StoneGradationAASHTO57Name), CalcPropertiesStringArray); }
        }

        private double _stoneGradationAASHTO1SurfaceArea;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double StoneGradationAASHTO1SurfaceArea
        {
            get { return _stoneGradationAASHTO1SurfaceArea; }
            set { ChangeAndNotify(ref _stoneGradationAASHTO1SurfaceArea, value, nameof(StoneGradationAASHTO1SurfaceArea), CalcPropertiesStringArray); }
        }

        private double _stoneGradationAASHTO3SurfaceArea;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double StoneGradationAASHTO3SurfaceArea
        {
            get { return _stoneGradationAASHTO3SurfaceArea; }
            set { ChangeAndNotify(ref _stoneGradationAASHTO3SurfaceArea, value, nameof(StoneGradationAASHTO3SurfaceArea), CalcPropertiesStringArray); }
        }

        private double _stoneGradationAASHTO5SurfaceArea;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double StoneGradationAASHTO5SurfaceArea
        {
            get { return _stoneGradationAASHTO5SurfaceArea; }
            set { ChangeAndNotify(ref _stoneGradationAASHTO5SurfaceArea, value, nameof(StoneGradationAASHTO5SurfaceArea), CalcPropertiesStringArray); }
        }

        private double _stoneGradationAASHTO57SurfaceArea;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double StoneGradationAASHTO57SurfaceArea
        {
            get { return _stoneGradationAASHTO57SurfaceArea; }
            set { ChangeAndNotify(ref _stoneGradationAASHTO57SurfaceArea, value, nameof(StoneGradationAASHTO57SurfaceArea), CalcPropertiesStringArray); }
        }

        private double _stoneGradationAASHTO1Volume;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double StoneGradationAASHTO1Volume
        {
            get { return _stoneGradationAASHTO1Volume; }
            set { ChangeAndNotify(ref _stoneGradationAASHTO1Volume, value, nameof(StoneGradationAASHTO1Volume), CalcPropertiesStringArray); }
        }

        private double _stoneGradationAASHTO3Volume;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double StoneGradationAASHTO3Volume
        {
            get { return _stoneGradationAASHTO3Volume; }
            set { ChangeAndNotify(ref _stoneGradationAASHTO3Volume, value, nameof(StoneGradationAASHTO3Volume), CalcPropertiesStringArray); }
        }

        private double _stoneGradationAASHTO5Volume;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double StoneGradationAASHTO5Volume
        {
            get { return _stoneGradationAASHTO5Volume; }
            set { ChangeAndNotify(ref _stoneGradationAASHTO5Volume, value, nameof(StoneGradationAASHTO5Volume), CalcPropertiesStringArray); }
        }

        private double _stoneGradationAASHTO57Volume;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double StoneGradationAASHTO57Volume
        {
            get { return _stoneGradationAASHTO57Volume; }
            set { ChangeAndNotify(ref _stoneGradationAASHTO57Volume, value, nameof(StoneGradationAASHTO57Volume), CalcPropertiesStringArray); }
        }

        private List<StoneGradation> _stoneGradations;
        /// <summary>
        /// Collection used for the stone gradations dropdown menu.
        /// </summary>
        public List<StoneGradation> StoneGradations
        {
            get { return _stoneGradations; }

            set { ChangeAndNotify(ref _stoneGradations, value, nameof(StoneGradations), CalcPropertiesStringArray); }
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

        private double _clayLinerThickness;
        /// <summary>
        /// User specified
        /// </summary>
        public double ClayLinerThickness
        {
            get { return _clayLinerThickness; }
            set { ChangeAndNotify(ref _clayLinerThickness, value, nameof(ClayLinerThickness), CalcPropertiesStringArray); }
        }

        private decimal _clayLinerUnitCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal ClayLinerUnitCost
        {
            get { return _clayLinerUnitCost; }
            set { ChangeAndNotify(ref _clayLinerUnitCost, value, nameof(ClayLinerUnitCost), CalcPropertiesStringArray); }
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

        private decimal _nonWovenGeotextileUnitCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal NonWovenGeotextileUnitCost
        {
            get { return _nonWovenGeotextileUnitCost; }
            set { ChangeAndNotify(ref _nonWovenGeotextileUnitCost, value, nameof(NonWovenGeotextileUnitCost), CalcPropertiesStringArray); }
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

         #endregion

        #region Properties - Sizing Summary: Excavation Volume, Clear and Grub

        private double _calcExcavationVolume;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcExcavationVolume
        {
            get { return ManganeseRemovalBedCalculations.CalcExcavationVolume(CalcLimestoneVolume, CalcLinerVolume); }
            set { ChangeAndNotify(ref _calcExcavationVolume, value); }
        }

        private double _calcLimestoneVolumeSaturatedDepth;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLimestoneVolumeSaturatedDepth
        {
            get { return ManganeseRemovalBedCalculations.CalcLimestoneVolumeSaturatedDepth(CalcLimestoneTopLength, CalcLimestoneTopWidth, CalcLimestoneBottomLength, CalcLimestoneBottomWidth, LimestoneDepth); }
            set { ChangeAndNotify(ref _calcLimestoneVolumeSaturatedDepth, value); }
        }

        private double _calcClearAndGrubArea;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcClearAndGrubArea
        {
            get { return ManganeseRemovalBedCalculations.CalcClearAndGrubArea(CalcFreeboardTopLength, CalcFreeboardTopWidth, FreeboardDepth); }
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

        #region Properties - Sizing Summary: Freeboard

        private double _calcFreeboardTopLength;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcFreeboardTopLength
        {
            get { return ManganeseRemovalBedCalculations.CalcTopDimension(CalcLimestoneTopLength, FreeboardDepth, PondInsideSlope); }
            set { ChangeAndNotify(ref _calcFreeboardTopLength, value); }
        }

        private double _calcFreeboardTopWidth;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcFreeboardTopWidth
        {
            get { return ManganeseRemovalBedCalculations.CalcTopDimension(CalcLimestoneTopWidth, FreeboardDepth, PondInsideSlope); }
            set { ChangeAndNotify(ref _calcFreeboardTopWidth, value); }
        }

        private double _calcFreeboardVolume;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcFreeboardVolume
        {
            get { return ManganeseRemovalBedCalculations.CalcLayerVolume(CalcLimestoneTopLength, CalcLimestoneTopWidth, FreeboardDepth, PondInsideSlope); }
            set { ChangeAndNotify(ref _calcFreeboardVolume, value); }
        }
        #endregion

        #region Properties - Sizing Summary: Limestone

        private double _calcLimestoneBulkDensity;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLimestoneBulkDensity
        {
            get { return ManganeseRemovalBedCalculations.CalcBulkDensity(LimestoneLayerPorosity); }
            set { ChangeAndNotify(ref _calcLimestoneBulkDensity, value); }
        }

        private double _calcLimestoneBottomLength;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLimestoneBottomLength
        {
            get { return ManganeseRemovalBedCalculations.CalcLimestoneLayerBottomLength(CalcLimestoneVolume, LimestoneDepth, PondInsideSlope, BottomLengthToWidthRatio); }
            set { ChangeAndNotify(ref _calcLimestoneBottomLength, value); }
        }

        private double _calcLimestoneBottomWidth;
        /// <summary>
        /// Calculated
        /// Checking that the limestone bottom width value is not equal to zero.  If limestone bottom width value is equal to zero, then
        /// an error message is displayed. A value of 1.0 is returned in order to prevent calculations for the bottom length to width (CalcBottomLayerLengthToWidthRatio*)
        /// ratio from dividing by zero.
        /// </summary>
        public double CalcLimestoneBottomWidth
        {
            get
            {
                double value = ManganeseRemovalBedCalculations.CalcLimestoneLayerBottomWidth(CalcLimestoneBottomLength, BottomLengthToWidthRatio);

                if (value == 0.0)
                {
                    ShowMajorError();

                    // Return 1.0 so calculations are valid until error is fixed by user
                    return 1.0;
                }
                return value;

            }
            set { ChangeAndNotify(ref _calcLimestoneBottomWidth, value); }
        }

        private double _calcLimestoneTopLength;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLimestoneTopLength
        {
            get { return ManganeseRemovalBedCalculations.CalcTopDimension(CalcLimestoneBottomLength, LimestoneDepth, PondInsideSlope); }
            set { ChangeAndNotify(ref _calcLimestoneTopLength, value); }
        }

        private double _calcLimestoneTopWidth;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLimestoneTopWidth
        {
            get { return ManganeseRemovalBedCalculations.CalcTopDimension(CalcLimestoneBottomWidth, LimestoneDepth, PondInsideSlope); }
            set { ChangeAndNotify(ref _calcLimestoneTopWidth, value); }
        }

        private double _calcLimestoneSurfaceArea;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLimestoneSurfaceArea
        {
            get { return ManganeseRemovalBedCalculations.CalcLayerTopArea(CalcLimestoneTopLength, CalcLimestoneTopWidth); }
            set { ChangeAndNotify(ref _calcLimestoneSurfaceArea, value); }
        }

        private double _calcLimestoneVolume;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLimestoneVolume
        {
            get { return _calcLimestoneVolume; }
            set { ChangeAndNotify(ref _calcLimestoneVolume, value, nameof(CalcLimestoneVolume), CalcPropertiesStringArray); }
        }

        private double _calcLimestoneWeight;
        /// <summary>
        /// Calculated. Checking and assigning of multiple properties is completed based on current program state.
        /// </summary>
        public double CalcLimestoneWeight
        {
            get
            {
                switch (SizingMethodsOptionsProperty)
                {
                    case RadioButtonsSizingMethodsOptionsEnum.OptionRetentionTime:
                        CheckLimestoneBottomWidthValue(CalcLimestoneBottomWidth);
                        BottomLengthToWidthRatio = AssignBottomLengthToWidthRatio(BottomLengthToWidthRatio);
                        CalcFreeboardTopLength = CalcFreeboardTopLength;
                        CalcFreeboardTopWidth = CalcFreeboardTopWidth;
                        CalcLimestoneVolume = CalcLimestoneVolumeRetentionTime;
                        _calcLimestoneWeight = CalcLimestoneWeightRetentionTime;
                        break;
                    case RadioButtonsSizingMethodsOptionsEnum.OptionKinetics:
                        KineticsEffluentManganeseConcentration = AssignEffluentManganeseConcentration(KineticsEffluentManganeseConcentration);
                        CheckLimestoneBottomWidthValue(CalcLimestoneBottomWidth);
                        BottomLengthToWidthRatio = AssignBottomLengthToWidthRatio(BottomLengthToWidthRatio);
                        CalcFreeboardTopLength = CalcFreeboardTopLength;
                        CalcFreeboardTopWidth = CalcFreeboardTopWidth;
                        CalcLimestoneVolume = CalcLimestoneVolumeKinetics;
                        _calcLimestoneWeight = CalcLimestoneWeightKinetics;
                        break;
                    case RadioButtonsSizingMethodsOptionsEnum.OptionLimestoneQuantity:
                        CheckLimestoneBottomWidthValue(CalcLimestoneBottomWidth);
                        BottomLengthToWidthRatio = AssignBottomLengthToWidthRatio(BottomLengthToWidthRatio);
                        CalcFreeboardTopLength = CalcFreeboardTopLength;
                        CalcFreeboardTopWidth = CalcFreeboardTopWidth;
                        CalcLimestoneVolume = CalcLimestoneVolumeLimestoneQuantity;
                        _calcLimestoneWeight = CalcLimestoneWeightLimestoneQuantity;
                        break;
                    case RadioButtonsSizingMethodsOptionsEnum.OptionDimensions:
                        CheckLimestoneBottomWidthValue(CalcLimestoneBottomWidth);
                        BottomLengthToWidthRatio = CalcBottomLengthToWidthRatioDimensions;
                        CalcFreeboardTopLength = FreeboardTopLengthDimensions;
                        CalcFreeboardTopWidth = FreeboardTopWidthDimensions;
                        CalcLimestoneVolume = CalcLimestoneVolumeDimensions;
                        _calcLimestoneWeight = CalcLimestoneWeightDimensions;
                        break;
                }

                return _calcLimestoneWeight;

            }
            set { ChangeAndNotify(ref _calcLimestoneWeight, value); }
        }

        #endregion

        #region Properties - Sizing Summary: Pipe

        private double _calcHeaderPipeLengthTotal;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcHeaderPipeLengthTotal
        {
            get { return ManganeseRemovalBedCalculations.CalcHeaderPipeLengthTotal(HeaderPipeQuantity, CalcLimestoneBottomWidth); }
            set { ChangeAndNotify(ref _calcHeaderPipeLengthTotal, value); }
        }

        private double _calcHeaderPipeSegmentQuantity;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcHeaderPipeSegmentQuantity
        {
            get { return ManganeseRemovalBedCalculations.CalcHeaderPipeSegmentQuantity(HeaderPipeSegmentLength, CalcHeaderPipeLengthTotal); }
            set { ChangeAndNotify(ref _calcHeaderPipeSegmentQuantity, value); }
        }

        private double _calcHeaderPipeCouplerQuantity;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcHeaderPipeCouplerQuantity
        {
            get { return ManganeseRemovalBedCalculations.CalcHeaderPipeCouplerQuantity(HeaderPipeSegmentLength, CalcHeaderPipeLengthTotal); }
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
            get { return ManganeseRemovalBedCalculations.CalcLinerSlopeLength(LimestoneDepth, FreeboardDepth, PondInsideSlope); }
            set { ChangeAndNotify(ref _calcLinerSlopeLength, value); }
        }

        private double _calcSyntheticLinerArea;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcSyntheticLinerArea
        {
            get { return ManganeseRemovalBedCalculations.CalcSyntheticLinerArea(CalcLimestoneBottomLength, CalcLimestoneBottomWidth, CalcFreeboardTopLength, CalcFreeboardTopWidth, CalcLinerSlopeLength); }
            set { ChangeAndNotify(ref _calcSyntheticLinerArea, value); }
        }

        private double _calcClayLinerArea;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcClayLinerArea
        {
            get { return ManganeseRemovalBedCalculations.CalcClayLinerArea(CalcLimestoneBottomLength, CalcLimestoneBottomWidth, CalcFreeboardTopLength, CalcFreeboardTopWidth, CalcLinerSlopeLength); }
            set { ChangeAndNotify(ref _calcClayLinerArea, value); }
        }

        private double _calcClayLinerVolume;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcClayLinerVolume
        {
            get { return ManganeseRemovalBedCalculations.CalcClayLinerVolume(CalcClayLinerArea, ClayLinerThickness); }
            set { ChangeAndNotify(ref _calcClayLinerVolume, value); }
        }

        private double _calcGeosyntheticClayLinerArea;
        /// <summary>
        /// Calculated.  Same as synthetic liner area.
        /// </summary>
        public double CalcGeosyntheticClayLinerArea
        {
            get { return ManganeseRemovalBedCalculations.CalcGeosyntheticClayLinerArea(CalcLimestoneBottomLength, CalcLimestoneBottomWidth, CalcFreeboardTopLength, CalcFreeboardTopWidth, CalcLinerSlopeLength); }
            set { ChangeAndNotify(ref _calcGeosyntheticClayLinerArea, value); }
        }

        private double _calcGeosyntheticClayLinerVolume;
        /// <summary>
        /// Calculated.
        /// </summary>
        public double CalcGeosyntheticClayLinerVolume
        {
            get { return ManganeseRemovalBedCalculations.CalcGeosyntheticClayLinerVolume(CalcGeosyntheticClayLinerArea, GeosyntheticClayLinerSoilCover); }
            set { ChangeAndNotify(ref _calcGeosyntheticClayLinerVolume, value); }
        }

        private double _calcNonWovenGeotextileSlopeLength;
        /// <summary>
        /// Calculated.  Same as synthetic liner area.
        /// </summary>
        public double CalcNonWovenGeotextileSlopeLength
        {
            get { return ManganeseRemovalBedCalculations.CalcNonWovenGeotextileSlopeLength(LimestoneDepth, PondInsideSlope); }
            set { ChangeAndNotify(ref _calcNonWovenGeotextileSlopeLength, value); }
        }

        private double _calcNonWovenGeotextileArea;
        /// <summary>
        /// Calculated.  
        /// </summary>
        public double CalcNonWovenGeotextileArea
        {
            get { return ManganeseRemovalBedCalculations.CalcNonWovenGeotextileArea(CalcLimestoneTopLength, CalcLimestoneBottomLength, CalcLimestoneTopWidth, CalcLimestoneBottomWidth, CalcNonWovenGeotextileSlopeLength); }
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

        private double _calcLimestoneLayerRetentionTime;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLimestoneLayerRetentionTime
        {
            get { return ManganeseRemovalBedCalculations.CalcLimestoneLayerRetentionTime(CalcLimestoneVolumeSaturatedDepth, LimestoneLayerPorosity, DesignFlow); }
            set { ChangeAndNotify(ref _calcLimestoneLayerRetentionTime, value); } 
        }

        #endregion

        #region Properties - Capital Costs

        private decimal _calcExcavationCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcExcavationCost
        {
            get { return ManganeseRemovalBedCalculations.CalcExcavationCost(CalcExcavationVolume, ExcavationUnitCost); }
            set { ChangeAndNotify(ref _calcExcavationCost, value); }
        }

        private decimal _calcLimestoneMaterialCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcLimestoneMaterialCost
        {
            get { return ManganeseRemovalBedCalculations.CalcLimestoneMaterialCost(CalcLimestoneWeight, LimestoneUnitCost); }
            set { ChangeAndNotify(ref _calcLimestoneMaterialCost, value); }
        }

        private decimal _calcLimestonePlacementCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcLimestonePlacementCost
        {
            get { return ManganeseRemovalBedCalculations.CalcLimestonePlacementCost(CalcLimestoneVolume, LimestonePlacementUnitCost); }
            set { ChangeAndNotify(ref _calcLimestonePlacementCost, value); }
        }

        private decimal _calcInOutPipeCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcInOutPipeCost
        {
            get { return ManganeseRemovalBedCalculations.CalcInOutPipeCost(InOutPipeLength, InOutPipeUnitCost); }
            set { ChangeAndNotify(ref _calcInOutPipeCost, value); }
        }

        private decimal _calcHeaderPipeCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcHeaderPipeCost
        {
            get { return ManganeseRemovalBedCalculations.CalcHeaderPipeCost(CalcHeaderPipeLengthTotal, HeaderPipeUnitCost); }
            set { ChangeAndNotify(ref _calcHeaderPipeCost, value); }
        }

        private decimal _calcHeaderPipeCouplerCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcHeaderPipeCouplerCost
        {
            get { return ManganeseRemovalBedCalculations.CalcHeaderPipeCouplerCost(CalcHeaderPipeCouplerQuantity, HeaderPipeCouplerUnitCost); }
            set { ChangeAndNotify(ref _calcHeaderPipeCouplerCost, value); }
        }

        private decimal _calcTeeConnectorCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcTeeConnectorCost
        {
            get { return ManganeseRemovalBedCalculations.CalcTeeConnectorCost(HeaderPipeQuantity, TeeConnectorUnitCost); }
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
                decimal amdtreatPipe = ManganeseRemovalBedCalculations.CalcAmdtreatPipeCost(CalcInOutPipeCost,
                                                                                            CalcHeaderPipeCost,
                                                                                            CalcHeaderPipeCouplerCost,
                                                                                            CalcTeeConnectorCost,
                                                                                            InOutPipeLength,
                                                                                            CalcHeaderPipeLengthTotal,
                                                                                            InOutPipeInstallRate,
                                                                                            HeaderPipeInstallRate,
                                                                                            LaborRate);

                decimal customPipe = ManganeseRemovalBedCalculations.CalcCustomPipeCost(CustomPipeLength1, CustomPipeUnitCost1,
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

        private decimal _calcClayLinerCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcClayLinerCost
        {
            get { return ManganeseRemovalBedCalculations.CalcClayLinerCost(CalcClayLinerVolume, ClayLinerUnitCost); }
            set { ChangeAndNotify(ref _calcClayLinerCost, value); }
        }

        private decimal _calcSyntheticLinerCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcSyntheticLinerCost
        {
            get { return ManganeseRemovalBedCalculations.CalcSyntheticLinerCost(CalcSyntheticLinerArea, SyntheticLinerUnitCost); }
            set { ChangeAndNotify(ref _calcSyntheticLinerCost, value); }
        }

        private decimal _calcGeosyntheticClayLinerCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcGeosyntheticClayLinerCost
        {
            get { return ManganeseRemovalBedCalculations.CalcGeosyntheticClayLinerCost(CalcGeosyntheticClayLinerArea, GeosyntheticClayLinerUnitCost); }
            set { ChangeAndNotify(ref _calcGeosyntheticClayLinerCost, value); }
        }

        private decimal _calcGeosyntheticClayLinerCoverCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcGeosyntheticClayLinerCoverCost
        {
            get { return ManganeseRemovalBedCalculations.CalcGeosyntheticClayLinerCoverCost(CalcGeosyntheticClayLinerVolume, GeosyntheticClayLinerCoverUnitCost); }
            set { ChangeAndNotify(ref _calcGeosyntheticClayLinerCoverCost, value); }
        }

        private decimal _calcNonWovenGeotextileCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcNonWovenGeotextileCost
        {
            get { return ManganeseRemovalBedCalculations.CalcNonWovenGeotextileCost(CalcNonWovenGeotextileArea, NonWovenGeotextileUnitCost); }
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

        private decimal _calcValveCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcValveCost
        {
            get { return ManganeseRemovalBedCalculations.CalcValveCost(ValveQuantity, ValveUnitCost); }
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
                return ManganeseRemovalBedCalculations.CalcOtherItemsCost(CalcValveCost,
                                                                          FlowDistributionStructureQuantity, FlowDistributionStructureUnitCost,
                                                                          WaterLevelControlStructureQuantity, WaterLevelControlStructureUnitCost,
                                                                          OutletProtectionStructureQuantity, OutletProtectionStructureUnitCost);
            }
            set { ChangeAndNotify(ref _calcOtherItemsCost, value); }

        }

        private decimal _calcCapitalCostTotal;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcCapitalCostTotal
        {
            get
            {
                _calcCapitalCostTotal = ManganeseRemovalBedCalculations.CalcCapitalCostTotal(CalcLimestoneMaterialCost, 
                                                                                             CalcLimestonePlacementCost,
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
                        _calcAnnualCost = ManganeseRemovalBedCalculations.CalcAnnualCost(AnnualCostMultiplier, CalcCapitalCostTotal);
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
        public double RecapitalizationCostLifeCycleLimestone
        {
            get { return _recapitalizationCostLifeCycleLimestone; }
            set { ChangeAndNotify(ref _recapitalizationCostLifeCycleLimestone, value, nameof(RecapitalizationCostLifeCycleLimestone), CalcPropertiesStringArray); }
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


        private double _recapitalizationCostPercentReplacementLimestone;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostPercentReplacementLimestone
        {
            get { return _recapitalizationCostPercentReplacementLimestone; }
            set { ChangeAndNotify(ref _recapitalizationCostPercentReplacementLimestone, value, nameof(RecapitalizationCostPercentReplacementLimestone), CalcPropertiesStringArray); }
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

        private decimal _calcRecapitalizationCostMaterialTotalCostLimestone;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostMaterialTotalCostLimestone
        {
            get { return ManganeseRemovalBedCalculations.CalcRecapitalizationCostMaterialTotalCostLimestone(CalcLimestoneMaterialCost, CalcLimestonePlacementCost); }
            set { ChangeAndNotify(ref _calcRecapitalizationCostMaterialTotalCostLimestone, value); }
        }

        private decimal _calcRapitalizationCostLimestone;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostLimestone
        {
            get
            {
                return ManganeseRemovalBedCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
                                                                             RecapitalizationCostInflationRate, RecapitalizationCostLifeCycleLimestone,
                                                                             CalcRecapitalizationCostMaterialTotalCostLimestone, RecapitalizationCostPercentReplacementLimestone);
            }
            set { ChangeAndNotify(ref _calcRapitalizationCostLimestone, value); }
        }


        private decimal _calcRapitalizationCostLiner;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostLiner
        {
            get
            {
                return ManganeseRemovalBedCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
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
                return ManganeseRemovalBedCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
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
                return ManganeseRemovalBedCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
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
            get { return ManganeseRemovalBedCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn, RecapitalizationCostInflationRate, 1.0, CalcAnnualCost, 100.0); }
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
                    case "Limestone":
                        item.MaterialCostDefault = CalcRecapitalizationCostMaterialTotalCostLimestone;
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
                    case "Limestone":
                        item.TotalCost = CalcRecapitalizationCostLimestone;
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
                Name = "Limestone",
                NameFixed = "Limestone",
                LifeCycle = RecapitalizationCostLifeCycleLimestone,
                PercentReplacement = RecapitalizationCostPercentReplacementLimestone,
                MaterialCostDefault = CalcRecapitalizationCostMaterialTotalCostLimestone,
                UseCustomCost = false,
                MaterialCostCustom = 0m,
                TotalCost = CalcRecapitalizationCostLimestone
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
            ((RecapMaterial)sender).TotalCost = ManganeseRemovalBedCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
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
        public ICommand SetStoneGradationCommand { get; }
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
            var customDialog = new CustomDialog() { Title = "About Manganese Removal Bed" };

            var customDialogViewModel = new CustomDialogViewModel(instance =>
            {
                _dialogCoordinator.HideMetroDialogAsync(this, customDialog);
            });
            customDialogViewModel.Message = Resources.infoManganeseRemovalBedMRB;
            customDialogViewModel.Image = new Uri("/Images/limestone-bed-brent-means.png", UriKind.Relative);
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
                        string message = Resources.infoWaterQualityMRB;
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
                        string message = Resources.infoSizingMethodsMRB;
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
                        string message = Resources.infoSystemPropertiesMRB;
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
                        string message = Resources.infoLayerMaterialsMRB;
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
                        string message = Resources.infoOtherItemsMRB;
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
                        string message = Resources.infoSizingSummaryMRB;
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
                        string message = Resources.infoCapitalCostMRB;
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
                        string message = Resources.infoAnnualCostMRB;
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
                        string message = Resources.infoRecapitalizationCostMRB;
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

        private double AssignEffluentManganeseConcentration(double effluentManganeseConcentration)
        {

            if (DissolvedManganese <= effluentManganeseConcentration)
            {
                effluentManganeseConcentration = DissolvedManganese - 0.01;
            }

            return effluentManganeseConcentration;
        }

        /// <summary>
        /// Set the particle size and surface area for the stone gradations
        /// </summary>
        /// <param name="stoneGradation"></param>
        public void SetStoneGradation(object stoneGradation)
        {
            StoneGradation itemStoneGradation = (StoneGradation)stoneGradation;

            switch (itemStoneGradation.Name)
            {
                case "AASHTO #1 (1.5-3.5 in)":
                    KineticsParticleSurfaceArea = StoneGradationAASHTO1SurfaceArea;
                    KineticsParticleVolume = StoneGradationAASHTO1Volume;
                    StoneGradationName = StoneGradationAASHTO1Name;
                    break;
                case "AASHTO #3 (1-2 in)":
                    KineticsParticleSurfaceArea = StoneGradationAASHTO3SurfaceArea;
                    KineticsParticleVolume = StoneGradationAASHTO3Volume;
                    StoneGradationName = StoneGradationAASHTO3Name;
                    break;
                case "AASHTO #5 (0.75-1.5 in)":
                    KineticsParticleSurfaceArea = StoneGradationAASHTO5SurfaceArea;
                    KineticsParticleVolume = StoneGradationAASHTO5Volume;
                    StoneGradationName = StoneGradationAASHTO5Name;
                    break;
                case "AASHTO #57 (0.2-1.0 in)":
                    KineticsParticleSurfaceArea = StoneGradationAASHTO57SurfaceArea;
                    KineticsParticleVolume = StoneGradationAASHTO57Volume;
                    StoneGradationName = StoneGradationAASHTO57Name;
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
            get { return ManganeseRemovalBedCalculations.CalcMaxPondDepth(CalcFreeboardTopWidth); }
            set { ChangeAndNotify(ref _calcMaxPondDepth, value); }
        }

        private double _calcPondDepth;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcPondDepth
        {
            get { return ManganeseRemovalBedCalculations.CalcPondDepth(FreeboardDepth, LimestoneDepth); }
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

        private void CheckLimestoneBottomWidthValue(double value)
        {
            if (value < 2.0 && value > 0.0)
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
            ErrorMessage = Resources.errorPondMRB;
            ErrorMessageShort = "ERROR - Pond cannot be constructed";
            IsExpandedFreeboard = true;
            IsExpandedWater = true;
            IsExpandedLimestone = true;
        }

        private void ShowMajorError()
        {
            IsError = true;
            IsMajorError = true;
            IsOpenFlyoutError = true;
            ErrorMessage = Resources.errorPondMajorMRB;
            ErrorMessageShort = "MAJOR ERROR - All calculations are invalid";
            IsExpandedFreeboard = true;
            IsExpandedWater = true;
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

        public ManganeseRemovalBedViewModel(IDialogCoordinator dialogCoordinator)
        {
            
               // MahApps dialog coordinator
               _dialogCoordinator = dialogCoordinator;

            // Assign the proper functions to the open and save commands
            OpenCommand = new RelayCommand(OpenFile);
            SaveCommand = new RelayCommand(SaveFile);
            HelpCommand = new RelayCommand(ShowHelp);
            SetStoneGradationCommand = new RelayCommandWithParameter(SetStoneGradation);
            SyncCommand = new RelayCommand(SyncWithMainUi);

            // Get a list of property names and filter the names to include only those that start with "Calc" in order
            // to use with the NotifyAndChange.  This eliminates the need to write every property name that needs 
            // to be notified of changes by the user.
            PropertiesStringList = Helpers.GetNamesOfClassProperties(this);
            CalcPropertiesStringArray = Helpers.FilterPropertiesList(PropertiesStringList, "Calc");

            // Initialize the model name and user name
            ModuleType = "Manganese Removal Bed";
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
            OpenXmlAndAssignValues(@"..\..\Data\default-data-manganese-removal-bed.xml");

            // Make an initial backup of the bottom length to width ratio
            BottomLengthToWidthRatioBackup = BottomLengthToWidthRatio;

            // Initialize the calculated limestone volume and weight
            CalcLimestoneVolume = CalcLimestoneVolumeRetentionTime;
            CalcLimestoneWeight = CalcLimestoneWeightRetentionTime;

            // Stone Gradations
            StoneGradations = new List<StoneGradation>
            {
                new StoneGradation {Name = StoneGradationAASHTO1Name, ParticleSurfaceArea = StoneGradationAASHTO1SurfaceArea, ParticleVolume = StoneGradationAASHTO1Volume},
                new StoneGradation {Name = StoneGradationAASHTO3Name, ParticleSurfaceArea = StoneGradationAASHTO3SurfaceArea, ParticleVolume = StoneGradationAASHTO3Volume},
                new StoneGradation {Name = StoneGradationAASHTO5Name, ParticleSurfaceArea = StoneGradationAASHTO5SurfaceArea, ParticleVolume = StoneGradationAASHTO5Volume},
                new StoneGradation {Name = StoneGradationAASHTO57Name, ParticleSurfaceArea = StoneGradationAASHTO57SurfaceArea, ParticleVolume = StoneGradationAASHTO57Volume},
            };

            KineticsParticleSurfaceArea = StoneGradationAASHTO1SurfaceArea;
            KineticsParticleVolume = StoneGradationAASHTO1Volume;
            StoneGradationName = StoneGradationAASHTO1Name;

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
