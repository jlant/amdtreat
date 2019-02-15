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

    public class PumpingViewModel : PropertyChangedBase, IObserver<SharedData>
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

        #region Properties - Vertical Turbine Pump Bore Holes

        private double _wellQuantity;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double WellQuantity
        {
            get { return _wellQuantity; }
            set { ChangeAndNotify(ref _wellQuantity, value, nameof(WellQuantity), CalcPropertiesStringArray); }
        }

        /// <summary>
        ///  Radio button binding
        /// </summary>
        public enum RadioButtonsBoreholeInsideDiameterOptionsEnum
        {
            Option16Inches,
            Option24Inches,
            Option30Inches,
            Option36Inches,
        }

        private RadioButtonsBoreholeInsideDiameterOptionsEnum _boreHoleInsideDiameterOptionsProperty;
        /// <summary>
        ///  User specified 
        /// </summary>
        public RadioButtonsBoreholeInsideDiameterOptionsEnum BoreHoleInsideDiameterOptionsProperty
        {
            get { return _boreHoleInsideDiameterOptionsProperty; }
            set { ChangeAndNotify(ref _boreHoleInsideDiameterOptionsProperty, value, nameof(BoreHoleInsideDiameterOptionsProperty), CalcPropertiesStringArray); }
        }

        private double _boreHoleSizingMultiplier;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double BoreHoleSizingMultiplier
        {
            get { return _boreHoleSizingMultiplier; }
            set { ChangeAndNotify(ref _boreHoleSizingMultiplier, value, nameof(BoreHoleSizingMultiplier), CalcPropertiesStringArray); }
        }

        private double _drillingDepthToMinePool;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double DrillingDepthToMinePool
        {
            get { return _drillingDepthToMinePool; }
            set { ChangeAndNotify(ref _drillingDepthToMinePool, value, nameof(DrillingDepthToMinePool), CalcPropertiesStringArray); }
        }

        private decimal _drillingBoreholeAndCastingInstallationCost16;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal DrillingBoreholeAndCastingInstallationCost16
        {
            get { return _drillingBoreholeAndCastingInstallationCost16; }
            set { ChangeAndNotify(ref _drillingBoreholeAndCastingInstallationCost16, value, nameof(DrillingBoreholeAndCastingInstallationCost16), CalcPropertiesStringArray); }
        }

        private decimal _drillingBoreholeAndCastingInstallationCost24;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal DrillingBoreholeAndCastingInstallationCost24
        {
            get { return _drillingBoreholeAndCastingInstallationCost24; }
            set { ChangeAndNotify(ref _drillingBoreholeAndCastingInstallationCost24, value, nameof(DrillingBoreholeAndCastingInstallationCost24), CalcPropertiesStringArray); }
        }

        private decimal _drillingBoreholeAndCastingInstallationCost30;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal DrillingBoreholeAndCastingInstallationCost30
        {
            get { return _drillingBoreholeAndCastingInstallationCost30; }
            set { ChangeAndNotify(ref _drillingBoreholeAndCastingInstallationCost30, value, nameof(DrillingBoreholeAndCastingInstallationCost30), CalcPropertiesStringArray); }
        }

        private decimal _drillingBoreholeAndCastingInstallationCost36;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal DrillingBoreholeAndCastingInstallationCost36
        {
            get { return _drillingBoreholeAndCastingInstallationCost36; }
            set { ChangeAndNotify(ref _drillingBoreholeAndCastingInstallationCost36, value, nameof(DrillingBoreholeAndCastingInstallationCost36), CalcPropertiesStringArray); }
        }
        #endregion

        #region Properties - Converyance Pipeline

        private double _flowRate;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double FlowRate
        {
            get { return _flowRate; }
            set { ChangeAndNotify(ref _flowRate, value, nameof(FlowRate), CalcPropertiesStringArray); }
        }

        private List<GeneralItem> _nominalPipeOutsideDiameterInchesList;
        /// <summary>
        /// Collection 
        /// </summary>
        public List<GeneralItem> NominalPipeOutsideDiameterInchesList
        {
            get { return _nominalPipeOutsideDiameterInchesList; }

            set { ChangeAndNotify(ref _nominalPipeOutsideDiameterInchesList, value, nameof(NominalPipeOutsideDiameterInchesList), CalcPropertiesStringArray); }
        }

        private string _nominalPipeOutsideDiameterInchesName;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string NominalPipeOutsideDiameterInchesName
        {
            get { return _nominalPipeOutsideDiameterInchesName; }
            set { ChangeAndNotify(ref _nominalPipeOutsideDiameterInchesName, value, nameof(NominalPipeOutsideDiameterInchesName), CalcPropertiesStringArray); }
        }

        private string _nominalPipeOutsideDiameterInchesName8;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string NominalPipeOutsideDiameterInchesName8
        {
            get { return _nominalPipeOutsideDiameterInchesName8; }
            set { ChangeAndNotify(ref _nominalPipeOutsideDiameterInchesName8, value, nameof(NominalPipeOutsideDiameterInchesName8), CalcPropertiesStringArray); }
        }

        private string _nominalPipeOutsideDiameterInchesName10;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string NominalPipeOutsideDiameterInchesName10
        {
            get { return _nominalPipeOutsideDiameterInchesName10; }
            set { ChangeAndNotify(ref _nominalPipeOutsideDiameterInchesName10, value, nameof(NominalPipeOutsideDiameterInchesName10), CalcPropertiesStringArray); }
        }

        private string _nominalPipeOutsideDiameterInchesName12;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string NominalPipeOutsideDiameterInchesName12
        {
            get { return _nominalPipeOutsideDiameterInchesName12; }
            set { ChangeAndNotify(ref _nominalPipeOutsideDiameterInchesName12, value, nameof(NominalPipeOutsideDiameterInchesName12), CalcPropertiesStringArray); }
        }

        private string _nominalPipeOutsideDiameterInchesName16;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string NominalPipeOutsideDiameterInchesName16
        {
            get { return _nominalPipeOutsideDiameterInchesName16; }
            set { ChangeAndNotify(ref _nominalPipeOutsideDiameterInchesName16, value, nameof(NominalPipeOutsideDiameterInchesName16), CalcPropertiesStringArray); }
        }

        private string _nominalPipeOutsideDiameterInchesName18;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string NominalPipeOutsideDiameterInchesName18
        {
            get { return _nominalPipeOutsideDiameterInchesName18; }
            set { ChangeAndNotify(ref _nominalPipeOutsideDiameterInchesName18, value, nameof(NominalPipeOutsideDiameterInchesName18), CalcPropertiesStringArray); }
        }

        private double _nominalPipeOutsideDiameterInches;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double NominalPipeOutsideDiameterInches
        {
            get { return _nominalPipeOutsideDiameterInches; }
            set { ChangeAndNotify(ref _nominalPipeOutsideDiameterInches, value, nameof(NominalPipeOutsideDiameterInches), CalcPropertiesStringArray); }
        }

        private double _nominalPipeOutsideDiameterInches8;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double NominalPipeOutsideDiameterInches8
        {
            get { return _nominalPipeOutsideDiameterInches8; }
            set { ChangeAndNotify(ref _nominalPipeOutsideDiameterInches8, value, nameof(NominalPipeOutsideDiameterInches8), CalcPropertiesStringArray); }
        }

        private double _nominalPipeOutsideDiameterInches10;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double NominalPipeOutsideDiameterInches10
        {
            get { return _nominalPipeOutsideDiameterInches10; }
            set { ChangeAndNotify(ref _nominalPipeOutsideDiameterInches10, value, nameof(NominalPipeOutsideDiameterInches10), CalcPropertiesStringArray); }
        }

        private double _nominalPipeOutsideDiameterInches12;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double NominalPipeOutsideDiameterInches12
        {
            get { return _nominalPipeOutsideDiameterInches12; }
            set { ChangeAndNotify(ref _nominalPipeOutsideDiameterInches12, value, nameof(NominalPipeOutsideDiameterInches12), CalcPropertiesStringArray); }
        }

        private double _nominalPipeOutsideDiameterInches16;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double NominalPipeOutsideDiameterInches16
        {
            get { return _nominalPipeOutsideDiameterInches16; }
            set { ChangeAndNotify(ref _nominalPipeOutsideDiameterInches16, value, nameof(NominalPipeOutsideDiameterInches16), CalcPropertiesStringArray); }
        }

        private double _nominalPipeOutsideDiameterInches18;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double NominalPipeOutsideDiameterInches18
        {
            get { return _nominalPipeOutsideDiameterInches18; }
            set { ChangeAndNotify(ref _nominalPipeOutsideDiameterInches18, value, nameof(NominalPipeOutsideDiameterInches18), CalcPropertiesStringArray); }
        }

        private List<GeneralItem> _estimatedIronPipeSizeStandardDiameterRatioNumberList;
        /// <summary>
        /// Collection 
        /// </summary>
        public List<GeneralItem> EstimatedIronPipeSizeStandardDiameterRatioNumberList
        {
            get { return _estimatedIronPipeSizeStandardDiameterRatioNumberList; }

            set { ChangeAndNotify(ref _estimatedIronPipeSizeStandardDiameterRatioNumberList, value, nameof(EstimatedIronPipeSizeStandardDiameterRatioNumberList), CalcPropertiesStringArray); }
        }

        private string _estimatedIronPipeSizeStandardDiameterRatioNumberName;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string EstimatedIronPipeSizeStandardDiameterRatioNumberName
        {
            get { return _estimatedIronPipeSizeStandardDiameterRatioNumberName; }
            set { ChangeAndNotify(ref _estimatedIronPipeSizeStandardDiameterRatioNumberName, value, nameof(EstimatedIronPipeSizeStandardDiameterRatioNumberName), CalcPropertiesStringArray); }
        }

        private string _estimatedIronPipeSizeStandardDiameterRatioNumberName7;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string EstimatedIronPipeSizeStandardDiameterRatioNumberName7
        {
            get { return _estimatedIronPipeSizeStandardDiameterRatioNumberName7; }
            set { ChangeAndNotify(ref _estimatedIronPipeSizeStandardDiameterRatioNumberName7, value, nameof(EstimatedIronPipeSizeStandardDiameterRatioNumberName7), CalcPropertiesStringArray); }
        }

        private string _estimatedIronPipeSizeStandardDiameterRatioNumberName9;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string EstimatedIronPipeSizeStandardDiameterRatioNumberName9
        {
            get { return _estimatedIronPipeSizeStandardDiameterRatioNumberName9; }
            set { ChangeAndNotify(ref _estimatedIronPipeSizeStandardDiameterRatioNumberName9, value, nameof(EstimatedIronPipeSizeStandardDiameterRatioNumberName9), CalcPropertiesStringArray); }
        }

        private string _estimatedIronPipeSizeStandardDiameterRatioNumberName11;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string EstimatedIronPipeSizeStandardDiameterRatioNumberName11
        {
            get { return _estimatedIronPipeSizeStandardDiameterRatioNumberName11; }
            set { ChangeAndNotify(ref _estimatedIronPipeSizeStandardDiameterRatioNumberName11, value, nameof(EstimatedIronPipeSizeStandardDiameterRatioNumberName11), CalcPropertiesStringArray); }
        }

        private string _estimatedIronPipeSizeStandardDiameterRatioNumberName135;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string EstimatedIronPipeSizeStandardDiameterRatioNumberName135
        {
            get { return _estimatedIronPipeSizeStandardDiameterRatioNumberName135; }
            set { ChangeAndNotify(ref _estimatedIronPipeSizeStandardDiameterRatioNumberName135, value, nameof(EstimatedIronPipeSizeStandardDiameterRatioNumberName135), CalcPropertiesStringArray); }
        }

        private string _estimatedIronPipeSizeStandardDiameterRatioNumberName17;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string EstimatedIronPipeSizeStandardDiameterRatioNumberName17
        {
            get { return _estimatedIronPipeSizeStandardDiameterRatioNumberName17; }
            set { ChangeAndNotify(ref _estimatedIronPipeSizeStandardDiameterRatioNumberName17, value, nameof(EstimatedIronPipeSizeStandardDiameterRatioNumberName17), CalcPropertiesStringArray); }
        }

        private double _estimatedIronPipeSizeStandardDiameterRatioNumber;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double EstimatedIronPipeSizeStandardDiameterRatioNumber
        {
            get { return _estimatedIronPipeSizeStandardDiameterRatioNumber; }
            set { ChangeAndNotify(ref _estimatedIronPipeSizeStandardDiameterRatioNumber, value, nameof(EstimatedIronPipeSizeStandardDiameterRatioNumber), CalcPropertiesStringArray); }
        }

        private double _estimatedIronPipeSizeStandardDiameterRatioNumber7;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double EstimatedIronPipeSizeStandardDiameterRatioNumber7
        {
            get { return _estimatedIronPipeSizeStandardDiameterRatioNumber7; }
            set { ChangeAndNotify(ref _estimatedIronPipeSizeStandardDiameterRatioNumber7, value, nameof(EstimatedIronPipeSizeStandardDiameterRatioNumber7), CalcPropertiesStringArray); }
        }

        private double _estimatedIronPipeSizeStandardDiameterRatioNumber9;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double EstimatedIronPipeSizeStandardDiameterRatioNumber9
        {
            get { return _estimatedIronPipeSizeStandardDiameterRatioNumber9; }
            set { ChangeAndNotify(ref _estimatedIronPipeSizeStandardDiameterRatioNumber9, value, nameof(EstimatedIronPipeSizeStandardDiameterRatioNumber9), CalcPropertiesStringArray); }
        }

        private double _estimatedIronPipeSizeStandardDiameterRatioNumber11;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double EstimatedIronPipeSizeStandardDiameterRatioNumber11
        {
            get { return _estimatedIronPipeSizeStandardDiameterRatioNumber11; }
            set { ChangeAndNotify(ref _estimatedIronPipeSizeStandardDiameterRatioNumber11, value, nameof(EstimatedIronPipeSizeStandardDiameterRatioNumber11), CalcPropertiesStringArray); }
        }

        private double _estimatedIronPipeSizeStandardDiameterRatioNumber135;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double EstimatedIronPipeSizeStandardDiameterRatioNumber135
        {
            get { return _estimatedIronPipeSizeStandardDiameterRatioNumber135; }
            set { ChangeAndNotify(ref _estimatedIronPipeSizeStandardDiameterRatioNumber135, value, nameof(EstimatedIronPipeSizeStandardDiameterRatioNumber135), CalcPropertiesStringArray); }
        }

        private double _estimatedIronPipeSizeStandardDiameterRatioNumber17;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double EstimatedIronPipeSizeStandardDiameterRatioNumber17
        {
            get { return _estimatedIronPipeSizeStandardDiameterRatioNumber17; }
            set { ChangeAndNotify(ref _estimatedIronPipeSizeStandardDiameterRatioNumber17, value, nameof(EstimatedIronPipeSizeStandardDiameterRatioNumber17), CalcPropertiesStringArray); }
        }

        private double _totalStaticHead;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double TotalStaticHead
        {
            get { return _totalStaticHead; }
            set { ChangeAndNotify(ref _totalStaticHead, value, nameof(TotalStaticHead), CalcPropertiesStringArray); }
        }

        private double _pipeLayingLength;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PipeLayingLength
        {
            get { return _pipeLayingLength; }
            set { ChangeAndNotify(ref _pipeLayingLength, value, nameof(PipeLayingLength), CalcPropertiesStringArray); }
        }

        private double _incidentalHeadLosses;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double IncidentalHeadLosses
        {
            get { return _incidentalHeadLosses; }
            set { ChangeAndNotify(ref _incidentalHeadLosses, value, nameof(IncidentalHeadLosses), CalcPropertiesStringArray); }
        }

        private double _pipeBeddingThickness;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PipeBeddingThickness
        {
            get { return _pipeBeddingThickness; }
            set { ChangeAndNotify(ref _pipeBeddingThickness, value, nameof(PipeBeddingThickness), CalcPropertiesStringArray); }
        }

        private decimal _gravelForPipeBeddingUnitCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal GravelForPipeBeddingUnitCost
        {
            get { return _gravelForPipeBeddingUnitCost; }
            set { ChangeAndNotify(ref _gravelForPipeBeddingUnitCost, value, nameof(GravelForPipeBeddingUnitCost), CalcPropertiesStringArray); }
        }

        private decimal _excavationUnitCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal ExcavationUnitCost
        {
            get { return _excavationUnitCost; }
            set { ChangeAndNotify(ref _excavationUnitCost, value, nameof(ExcavationUnitCost), CalcPropertiesStringArray); }
        }

        private decimal _backfillAndCompactionUnitCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal BackfillAndCompactionUnitCost
        {
            get { return _backfillAndCompactionUnitCost; }
            set { ChangeAndNotify(ref _backfillAndCompactionUnitCost, value, nameof(BackfillAndCompactionUnitCost), CalcPropertiesStringArray); }
        }

        private bool _isAirVacuumReleaseAssemblies;
        /// <summary>
        ///  User specified 
        /// </summary>
        public bool IsAirVacuumReleaseAssemblies
        {
            get { return _isAirVacuumReleaseAssemblies; }
            set { ChangeAndNotify(ref _isAirVacuumReleaseAssemblies, value, nameof(IsAirVacuumReleaseAssemblies), CalcPropertiesStringArray); }
        }

        private double _airVacuumReleaseAssembliesQuantity;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double AirVacuumReleaseAssembliesQuantity
        {
            get { return _airVacuumReleaseAssembliesQuantity; }
            set { ChangeAndNotify(ref _airVacuumReleaseAssembliesQuantity, value, nameof(AirVacuumReleaseAssembliesQuantity), CalcPropertiesStringArray); }
        }

        private decimal _airVacuumReleaseAssembliesUnitCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal AirVacuumReleaseAssembliesUnitCost
        {
            get { return _airVacuumReleaseAssembliesUnitCost; }
            set { ChangeAndNotify(ref _airVacuumReleaseAssembliesUnitCost, value, nameof(AirVacuumReleaseAssembliesUnitCost), CalcPropertiesStringArray); }
        }

        private double _pressureRatingPsi335;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PressureRatingPsi335
        {
            get { return _pressureRatingPsi335; }
            set { ChangeAndNotify(ref _pressureRatingPsi335, value, nameof(PressureRatingPsi335), CalcPropertiesStringArray); }
        }

        private double _pressureRatingPsi250;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PressureRatingPsi250
        {
            get { return _pressureRatingPsi250; }
            set { ChangeAndNotify(ref _pressureRatingPsi250, value, nameof(PressureRatingPsi250), CalcPropertiesStringArray); }
        }

        private double _pressureRatingPsi200;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PressureRatingPsi200
        {
            get { return _pressureRatingPsi200; }
            set { ChangeAndNotify(ref _pressureRatingPsi200, value, nameof(PressureRatingPsi200), CalcPropertiesStringArray); }
        }

        private double _pressureRatingPsi160;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PressureRatingPsi160
        {
            get { return _pressureRatingPsi160; }
            set { ChangeAndNotify(ref _pressureRatingPsi160, value, nameof(PressureRatingPsi160), CalcPropertiesStringArray); }
        }

        private double _pressureRatingPsi125;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PressureRatingPsi125
        {
            get { return _pressureRatingPsi125; }
            set { ChangeAndNotify(ref _pressureRatingPsi125, value, nameof(PressureRatingPsi125), CalcPropertiesStringArray); }
        }

        private double _pipeInsideDiameterInchesSDR7NOD8;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PipeInsideDiameterInchesSDR7NOD8
        {
            get { return _pipeInsideDiameterInchesSDR7NOD8; }
            set { ChangeAndNotify(ref _pipeInsideDiameterInchesSDR7NOD8, value, nameof(PipeInsideDiameterInchesSDR7NOD8), CalcPropertiesStringArray); }
        }

        private double _pipeInsideDiameterInchesSDR9NOD8;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PipeInsideDiameterInchesSDR9NOD8
        {
            get { return _pipeInsideDiameterInchesSDR9NOD8; }
            set { ChangeAndNotify(ref _pipeInsideDiameterInchesSDR9NOD8, value, nameof(PipeInsideDiameterInchesSDR9NOD8), CalcPropertiesStringArray); }
        }

        private double _pipeInsideDiameterInchesSDR11NOD8;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PipeInsideDiameterInchesSDR11NOD8
        {
            get { return _pipeInsideDiameterInchesSDR11NOD8; }
            set { ChangeAndNotify(ref _pipeInsideDiameterInchesSDR11NOD8, value, nameof(PipeInsideDiameterInchesSDR11NOD8), CalcPropertiesStringArray); }
        }

        private double _pipeInsideDiameterInchesSDR135NOD8;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PipeInsideDiameterInchesSDR135NOD8
        {
            get { return _pipeInsideDiameterInchesSDR135NOD8; }
            set { ChangeAndNotify(ref _pipeInsideDiameterInchesSDR135NOD8, value, nameof(PipeInsideDiameterInchesSDR135NOD8), CalcPropertiesStringArray); }
        }

        private double _pipeInsideDiameterInchesSDR17NOD8;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PipeInsideDiameterInchesSDR17NOD8
        {
            get { return _pipeInsideDiameterInchesSDR17NOD8; }
            set { ChangeAndNotify(ref _pipeInsideDiameterInchesSDR17NOD8, value, nameof(PipeInsideDiameterInchesSDR17NOD8), CalcPropertiesStringArray); }
        }

        private double _pipeInsideDiameterInchesSDR7NOD10;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PipeInsideDiameterInchesSDR7NOD10
        {
            get { return _pipeInsideDiameterInchesSDR7NOD10; }
            set { ChangeAndNotify(ref _pipeInsideDiameterInchesSDR7NOD10, value, nameof(PipeInsideDiameterInchesSDR7NOD10), CalcPropertiesStringArray); }
        }

        private double _pipeInsideDiameterInchesSDR9NOD10;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PipeInsideDiameterInchesSDR9NOD10
        {
            get { return _pipeInsideDiameterInchesSDR9NOD10; }
            set { ChangeAndNotify(ref _pipeInsideDiameterInchesSDR9NOD10, value, nameof(PipeInsideDiameterInchesSDR9NOD10), CalcPropertiesStringArray); }
        }

        private double _pipeInsideDiameterInchesSDR11NOD10;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PipeInsideDiameterInchesSDR11NOD10
        {
            get { return _pipeInsideDiameterInchesSDR11NOD10; }
            set { ChangeAndNotify(ref _pipeInsideDiameterInchesSDR11NOD10, value, nameof(PipeInsideDiameterInchesSDR11NOD10), CalcPropertiesStringArray); }
        }

        private double _pipeInsideDiameterInchesSDR135NOD10;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PipeInsideDiameterInchesSDR135NOD10
        {
            get { return _pipeInsideDiameterInchesSDR135NOD10; }
            set { ChangeAndNotify(ref _pipeInsideDiameterInchesSDR135NOD10, value, nameof(PipeInsideDiameterInchesSDR135NOD10), CalcPropertiesStringArray); }
        }

        private double _pipeInsideDiameterInchesSDR17NOD10;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PipeInsideDiameterInchesSDR17NOD10
        {
            get { return _pipeInsideDiameterInchesSDR17NOD10; }
            set { ChangeAndNotify(ref _pipeInsideDiameterInchesSDR17NOD10, value, nameof(PipeInsideDiameterInchesSDR17NOD10), CalcPropertiesStringArray); }
        }

        private double _pipeInsideDiameterInchesSDR7NOD12;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PipeInsideDiameterInchesSDR7NOD12
        {
            get { return _pipeInsideDiameterInchesSDR7NOD12; }
            set { ChangeAndNotify(ref _pipeInsideDiameterInchesSDR7NOD12, value, nameof(PipeInsideDiameterInchesSDR7NOD12), CalcPropertiesStringArray); }
        }

        private double _pipeInsideDiameterInchesSDR9NOD12;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PipeInsideDiameterInchesSDR9NOD12
        {
            get { return _pipeInsideDiameterInchesSDR9NOD12; }
            set { ChangeAndNotify(ref _pipeInsideDiameterInchesSDR9NOD12, value, nameof(PipeInsideDiameterInchesSDR9NOD12), CalcPropertiesStringArray); }
        }

        private double _pipeInsideDiameterInchesSDR11NOD12;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PipeInsideDiameterInchesSDR11NOD12
        {
            get { return _pipeInsideDiameterInchesSDR11NOD12; }
            set { ChangeAndNotify(ref _pipeInsideDiameterInchesSDR11NOD12, value, nameof(PipeInsideDiameterInchesSDR11NOD12), CalcPropertiesStringArray); }
        }

        private double _pipeInsideDiameterInchesSDR135NOD12;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PipeInsideDiameterInchesSDR135NOD12
        {
            get { return _pipeInsideDiameterInchesSDR135NOD12; }
            set { ChangeAndNotify(ref _pipeInsideDiameterInchesSDR135NOD12, value, nameof(PipeInsideDiameterInchesSDR135NOD12), CalcPropertiesStringArray); }
        }

        private double _pipeInsideDiameterInchesSDR17NOD12;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PipeInsideDiameterInchesSDR17NOD12
        {
            get { return _pipeInsideDiameterInchesSDR17NOD12; }
            set { ChangeAndNotify(ref _pipeInsideDiameterInchesSDR17NOD12, value, nameof(PipeInsideDiameterInchesSDR17NOD12), CalcPropertiesStringArray); }
        }

        private double _pipeInsideDiameterInchesSDR7NOD16;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PipeInsideDiameterInchesSDR7NOD16
        {
            get { return _pipeInsideDiameterInchesSDR7NOD16; }
            set { ChangeAndNotify(ref _pipeInsideDiameterInchesSDR7NOD16, value, nameof(PipeInsideDiameterInchesSDR7NOD16), CalcPropertiesStringArray); }
        }

        private double _pipeInsideDiameterInchesSDR9NOD16;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PipeInsideDiameterInchesSDR9NOD16
        {
            get { return _pipeInsideDiameterInchesSDR9NOD16; }
            set { ChangeAndNotify(ref _pipeInsideDiameterInchesSDR9NOD16, value, nameof(PipeInsideDiameterInchesSDR9NOD16), CalcPropertiesStringArray); }
        }

        private double _pipeInsideDiameterInchesSDR11NOD16;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PipeInsideDiameterInchesSDR11NOD16
        {
            get { return _pipeInsideDiameterInchesSDR11NOD16; }
            set { ChangeAndNotify(ref _pipeInsideDiameterInchesSDR11NOD16, value, nameof(PipeInsideDiameterInchesSDR11NOD16), CalcPropertiesStringArray); }
        }

        private double _pipeInsideDiameterInchesSDR135NOD16;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PipeInsideDiameterInchesSDR135NOD16
        {
            get { return _pipeInsideDiameterInchesSDR135NOD16; }
            set { ChangeAndNotify(ref _pipeInsideDiameterInchesSDR135NOD16, value, nameof(PipeInsideDiameterInchesSDR135NOD16), CalcPropertiesStringArray); }
        }

        private double _pipeInsideDiameterInchesSDR17NOD16;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PipeInsideDiameterInchesSDR17NOD16
        {
            get { return _pipeInsideDiameterInchesSDR17NOD16; }
            set { ChangeAndNotify(ref _pipeInsideDiameterInchesSDR17NOD16, value, nameof(PipeInsideDiameterInchesSDR17NOD16), CalcPropertiesStringArray); }
        }

        private double _pipeInsideDiameterInchesSDR7NOD18;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PipeInsideDiameterInchesSDR7NOD18
        {
            get { return _pipeInsideDiameterInchesSDR7NOD18; }
            set { ChangeAndNotify(ref _pipeInsideDiameterInchesSDR7NOD18, value, nameof(PipeInsideDiameterInchesSDR7NOD18), CalcPropertiesStringArray); }
        }

        private double _pipeInsideDiameterInchesSDR9NOD18;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PipeInsideDiameterInchesSDR9NOD18
        {
            get { return _pipeInsideDiameterInchesSDR9NOD18; }
            set { ChangeAndNotify(ref _pipeInsideDiameterInchesSDR9NOD18, value, nameof(PipeInsideDiameterInchesSDR9NOD18), CalcPropertiesStringArray); }
        }

        private double _pipeInsideDiameterInchesSDR11NOD18;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PipeInsideDiameterInchesSDR11NOD18
        {
            get { return _pipeInsideDiameterInchesSDR11NOD18; }
            set { ChangeAndNotify(ref _pipeInsideDiameterInchesSDR11NOD18, value, nameof(PipeInsideDiameterInchesSDR11NOD18), CalcPropertiesStringArray); }
        }

        private double _pipeInsideDiameterInchesSDR135NOD18;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PipeInsideDiameterInchesSDR135NOD18
        {
            get { return _pipeInsideDiameterInchesSDR135NOD18; }
            set { ChangeAndNotify(ref _pipeInsideDiameterInchesSDR135NOD18, value, nameof(PipeInsideDiameterInchesSDR135NOD18), CalcPropertiesStringArray); }
        }

        private double _pipeInsideDiameterInchesSDR17NOD18;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PipeInsideDiameterInchesSDR17NOD18
        {
            get { return _pipeInsideDiameterInchesSDR17NOD18; }
            set { ChangeAndNotify(ref _pipeInsideDiameterInchesSDR17NOD18, value, nameof(PipeInsideDiameterInchesSDR17NOD18), CalcPropertiesStringArray); }
        }

        private decimal _pipeFusionCostNOD8;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal PipeFusionCostNOD8
        {
            get { return _pipeFusionCostNOD8; }
            set { ChangeAndNotify(ref _pipeFusionCostNOD8, value, nameof(PipeFusionCostNOD8), CalcPropertiesStringArray); }
        }

        private decimal _pipeFusionCostNOD10;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal PipeFusionCostNOD10
        {
            get { return _pipeFusionCostNOD10; }
            set { ChangeAndNotify(ref _pipeFusionCostNOD10, value, nameof(PipeFusionCostNOD10), CalcPropertiesStringArray); }
        }

        private decimal _pipeFusionCostNOD12;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal PipeFusionCostNOD12
        {
            get { return _pipeFusionCostNOD12; }
            set { ChangeAndNotify(ref _pipeFusionCostNOD12, value, nameof(PipeFusionCostNOD12), CalcPropertiesStringArray); }
        }

        private decimal _pipeFusionCostNOD16;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal PipeFusionCostNOD16
        {
            get { return _pipeFusionCostNOD16; }
            set { ChangeAndNotify(ref _pipeFusionCostNOD16, value, nameof(PipeFusionCostNOD16), CalcPropertiesStringArray); }
        }

        private decimal _pipeFusionCostNOD18;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal PipeFusionCostNOD18
        {
            get { return _pipeFusionCostNOD18; }
            set { ChangeAndNotify(ref _pipeFusionCostNOD18, value, nameof(PipeFusionCostNOD18), CalcPropertiesStringArray); }
        }

        private decimal _pipeCostWithShippingNOD8;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal PipeCostWithShippingNOD8
        {
            get { return _pipeCostWithShippingNOD8; }
            set { ChangeAndNotify(ref _pipeCostWithShippingNOD8, value, nameof(PipeCostWithShippingNOD8), CalcPropertiesStringArray); }
        }

        private decimal _pipeCostWithShippingNOD10;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal PipeCostWithShippingNOD10
        {
            get { return _pipeCostWithShippingNOD10; }
            set { ChangeAndNotify(ref _pipeCostWithShippingNOD10, value, nameof(PipeCostWithShippingNOD10), CalcPropertiesStringArray); }
        }

        private decimal _pipeCostWithShippingNOD12;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal PipeCostWithShippingNOD12
        {
            get { return _pipeCostWithShippingNOD12; }
            set { ChangeAndNotify(ref _pipeCostWithShippingNOD12, value, nameof(PipeCostWithShippingNOD12), CalcPropertiesStringArray); }
        }

        private decimal _pipeCostWithShippingNOD16;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal PipeCostWithShippingNOD16
        {
            get { return _pipeCostWithShippingNOD16; }
            set { ChangeAndNotify(ref _pipeCostWithShippingNOD16, value, nameof(PipeCostWithShippingNOD16), CalcPropertiesStringArray); }
        }

        private decimal _pipeCostWithShippingNOD18;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal PipeCostWithShippingNOD18
        {
            get { return _pipeCostWithShippingNOD18; }
            set { ChangeAndNotify(ref _pipeCostWithShippingNOD18, value, nameof(PipeCostWithShippingNOD18), CalcPropertiesStringArray); }
        }
        #endregion

        #region Properties - Vertical Turbine Pumps

        private double _pumpQuantity;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PumpQuantity
        {
            get { return _pumpQuantity; }
            set { ChangeAndNotify(ref _pumpQuantity, value, nameof(PumpQuantity), CalcPropertiesStringArray); }
        }

        private double _estimatedPumpEfficiency;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double EstimatedPumpEfficiency
        {
            get { return _estimatedPumpEfficiency; }
            set { ChangeAndNotify(ref _estimatedPumpEfficiency, value, nameof(EstimatedPumpEfficiency), CalcPropertiesStringArray); }
        }

        private double _pumpSizingSafetyFactor;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PumpSizingSafetyFactor
        {
            get { return _pumpSizingSafetyFactor; }
            set { ChangeAndNotify(ref _pumpSizingSafetyFactor, value, nameof(PumpSizingSafetyFactor), CalcPropertiesStringArray); }
        }

        private bool _isSoftStartVfd;
        /// <summary>
        ///  User specified 
        /// </summary>
        public bool IsSoftStartVfd
        {
            get { return _isSoftStartVfd; }
            set { ChangeAndNotify(ref _isSoftStartVfd, value, nameof(IsSoftStartVfd), CalcPropertiesStringArray); }
        }

        private string _softStartVfd;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string SoftStartVfd
        {
            get { return _softStartVfd; }
            set { ChangeAndNotify(ref _softStartVfd, value, nameof(SoftStartVfd), CalcPropertiesStringArray); }
        }

        private string _softStartVfdConstant;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string SoftStartVfdConstant
        {
            get { return _softStartVfdConstant; }
            set { ChangeAndNotify(ref _softStartVfdConstant, value, nameof(SoftStartVfdConstant), CalcPropertiesStringArray); }
        }

        private decimal _concreteUnitCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal ConcreteUnitCost
        {
            get { return _concreteUnitCost; }
            set { ChangeAndNotify(ref _concreteUnitCost, value, nameof(ConcreteUnitCost), CalcPropertiesStringArray); }
        }

        private decimal _electricalUnitCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal ElectricalUnitCost
        {
            get { return _electricalUnitCost; }
            set { ChangeAndNotify(ref _electricalUnitCost, value, nameof(ElectricalUnitCost), CalcPropertiesStringArray); }
        }

        private double _pumpingTimeHoursPerDay;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PumpingTimeHoursPerDay
        {
            get { return _pumpingTimeHoursPerDay; }
            set { ChangeAndNotify(ref _pumpingTimeHoursPerDay, value, nameof(PumpingTimeHoursPerDay), CalcPropertiesStringArray); }
        }

        private double _pumpingTimeDaysPerYear;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PumpingTimeDaysPerYear
        {
            get { return _pumpingTimeDaysPerYear; }
            set { ChangeAndNotify(ref _pumpingTimeDaysPerYear, value, nameof(PumpingTimeDaysPerYear), CalcPropertiesStringArray); }
        }

        private double _pipelineMaintenanceFactor;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PipelineMaintenanceFactor
        {
            get { return _pipelineMaintenanceFactor; }
            set { ChangeAndNotify(ref _pipelineMaintenanceFactor, value, nameof(PipelineMaintenanceFactor), CalcPropertiesStringArray); }
        }

        private double _pumpMaintenanceFactor;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PumpMaintenanceFactor
        {
            get { return _pumpMaintenanceFactor; }
            set { ChangeAndNotify(ref _pumpMaintenanceFactor, value, nameof(PumpMaintenanceFactor), CalcPropertiesStringArray); }
        }

        #endregion

        #region Properties - Sizing Summary

        private double _calcPipeInsideDiameter;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcPipeInsideDiameter
        {
            get
            {
                if (EstimatedIronPipeSizeStandardDiameterRatioNumber == 7 && NominalPipeOutsideDiameterInches == 8)
                {
                    _calcPipeInsideDiameter = PipeInsideDiameterInchesSDR7NOD8;
                }
                else if (EstimatedIronPipeSizeStandardDiameterRatioNumber == 7 && NominalPipeOutsideDiameterInches == 10)
                {
                    _calcPipeInsideDiameter = PipeInsideDiameterInchesSDR7NOD10;
                }
                else if (EstimatedIronPipeSizeStandardDiameterRatioNumber == 7 && NominalPipeOutsideDiameterInches == 12)
                {
                    _calcPipeInsideDiameter = PipeInsideDiameterInchesSDR7NOD12;
                }
                else if (EstimatedIronPipeSizeStandardDiameterRatioNumber == 7 && NominalPipeOutsideDiameterInches == 16)
                {
                    _calcPipeInsideDiameter = PipeInsideDiameterInchesSDR7NOD16;
                }
                else if (EstimatedIronPipeSizeStandardDiameterRatioNumber == 7 && NominalPipeOutsideDiameterInches == 18)
                {
                    _calcPipeInsideDiameter = PipeInsideDiameterInchesSDR7NOD18;
                }
                if (EstimatedIronPipeSizeStandardDiameterRatioNumber == 9 && NominalPipeOutsideDiameterInches == 8)
                {
                    _calcPipeInsideDiameter = PipeInsideDiameterInchesSDR9NOD8;
                }
                else if (EstimatedIronPipeSizeStandardDiameterRatioNumber == 9 && NominalPipeOutsideDiameterInches == 10)
                {
                    _calcPipeInsideDiameter = PipeInsideDiameterInchesSDR9NOD10;
                }
                else if (EstimatedIronPipeSizeStandardDiameterRatioNumber == 9 && NominalPipeOutsideDiameterInches == 12)
                {
                    _calcPipeInsideDiameter = PipeInsideDiameterInchesSDR9NOD12;
                }
                else if (EstimatedIronPipeSizeStandardDiameterRatioNumber == 9 && NominalPipeOutsideDiameterInches == 16)
                {
                    _calcPipeInsideDiameter = PipeInsideDiameterInchesSDR9NOD16;
                }
                else if (EstimatedIronPipeSizeStandardDiameterRatioNumber == 9 && NominalPipeOutsideDiameterInches == 18)
                {
                    _calcPipeInsideDiameter = PipeInsideDiameterInchesSDR9NOD18;
                }
                if (EstimatedIronPipeSizeStandardDiameterRatioNumber == 11 && NominalPipeOutsideDiameterInches == 8)
                {
                    _calcPipeInsideDiameter = PipeInsideDiameterInchesSDR11NOD8;
                }
                else if (EstimatedIronPipeSizeStandardDiameterRatioNumber == 11 && NominalPipeOutsideDiameterInches == 10)
                {
                    _calcPipeInsideDiameter = PipeInsideDiameterInchesSDR11NOD10;
                }
                else if (EstimatedIronPipeSizeStandardDiameterRatioNumber == 11 && NominalPipeOutsideDiameterInches == 12)
                {
                    _calcPipeInsideDiameter = PipeInsideDiameterInchesSDR11NOD12;
                }
                else if (EstimatedIronPipeSizeStandardDiameterRatioNumber == 11 && NominalPipeOutsideDiameterInches == 16)
                {
                    _calcPipeInsideDiameter = PipeInsideDiameterInchesSDR11NOD16;
                }
                else if (EstimatedIronPipeSizeStandardDiameterRatioNumber == 11 && NominalPipeOutsideDiameterInches == 18)
                {
                    _calcPipeInsideDiameter = PipeInsideDiameterInchesSDR11NOD18;
                }
                if (EstimatedIronPipeSizeStandardDiameterRatioNumber == 13.5 && NominalPipeOutsideDiameterInches == 8)
                {
                    _calcPipeInsideDiameter = PipeInsideDiameterInchesSDR135NOD8;
                }
                else if (EstimatedIronPipeSizeStandardDiameterRatioNumber == 13.5 && NominalPipeOutsideDiameterInches == 10)
                {
                    _calcPipeInsideDiameter = PipeInsideDiameterInchesSDR135NOD10;
                }
                else if (EstimatedIronPipeSizeStandardDiameterRatioNumber == 13.5 && NominalPipeOutsideDiameterInches == 12)
                {
                    _calcPipeInsideDiameter = PipeInsideDiameterInchesSDR135NOD12;
                }
                else if (EstimatedIronPipeSizeStandardDiameterRatioNumber == 13.5 && NominalPipeOutsideDiameterInches == 16)
                {
                    _calcPipeInsideDiameter = PipeInsideDiameterInchesSDR135NOD16;
                }
                else if (EstimatedIronPipeSizeStandardDiameterRatioNumber == 13.5 && NominalPipeOutsideDiameterInches == 18)
                {
                    _calcPipeInsideDiameter = PipeInsideDiameterInchesSDR135NOD18;
                }
                if (EstimatedIronPipeSizeStandardDiameterRatioNumber == 17 && NominalPipeOutsideDiameterInches == 8)
                {
                    _calcPipeInsideDiameter = PipeInsideDiameterInchesSDR17NOD8;
                }
                else if (EstimatedIronPipeSizeStandardDiameterRatioNumber == 17 && NominalPipeOutsideDiameterInches == 10)
                {
                    _calcPipeInsideDiameter = PipeInsideDiameterInchesSDR17NOD10;
                }
                else if (EstimatedIronPipeSizeStandardDiameterRatioNumber == 17 && NominalPipeOutsideDiameterInches == 12)
                {
                    _calcPipeInsideDiameter = PipeInsideDiameterInchesSDR17NOD12;
                }
                else if (EstimatedIronPipeSizeStandardDiameterRatioNumber == 17 && NominalPipeOutsideDiameterInches == 16)
                {
                    _calcPipeInsideDiameter = PipeInsideDiameterInchesSDR17NOD16;
                }
                else if (EstimatedIronPipeSizeStandardDiameterRatioNumber == 17 && NominalPipeOutsideDiameterInches == 18)
                {
                    _calcPipeInsideDiameter = PipeInsideDiameterInchesSDR17NOD18;
                }

                return _calcPipeInsideDiameter;
            }
            set { ChangeAndNotify(ref _calcPipeInsideDiameter, value); }

        }

        private double _calcDynamicPipeLosses;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcDynamicPipeLosses
        {
            get { return PumpingCalculations.CalcDynamicPipeLosses(PipeLayingLength, CalcPipeInsideDiameter, FlowRate); }
            set { ChangeAndNotify(ref _calcDynamicPipeLosses, value); }
        }

        private double _calcTotalDynamicHead;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcTotalDynamicHead
        {
            get { return PumpingCalculations.CalcTotalDynamicHead(CalcDynamicPipeLosses, TotalStaticHead, IncidentalHeadLosses); }
            set { ChangeAndNotify(ref _calcTotalDynamicHead, value); }
        }

        private double _calcTotalDynamicHeadPressure;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcTotalDynamicHeadPressure
        {
            get { return PumpingCalculations.CalcTotalDynamicHeadPressure(CalcTotalDynamicHead); }
            set { ChangeAndNotify(ref _calcTotalDynamicHeadPressure, value); }
        }


        private double _calcPressureClass;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcPressureClass
        {
            get
            {
                if (CalcTotalDynamicHeadPressure <= PressureRatingPsi335 && CalcTotalDynamicHeadPressure > PressureRatingPsi250)
                {
                    _calcPressureClass = EstimatedIronPipeSizeStandardDiameterRatioNumber7;
                    ShowNoError();
                }
                else if (CalcTotalDynamicHeadPressure <= PressureRatingPsi250 && CalcTotalDynamicHeadPressure > PressureRatingPsi200)
                {
                    _calcPressureClass = EstimatedIronPipeSizeStandardDiameterRatioNumber9;
                    ShowNoError();
                }
                else if (CalcTotalDynamicHeadPressure <= PressureRatingPsi200 && CalcTotalDynamicHeadPressure > PressureRatingPsi160)
                {
                    _calcPressureClass = EstimatedIronPipeSizeStandardDiameterRatioNumber11;
                    ShowNoError();
                }
                else if (CalcTotalDynamicHeadPressure <= PressureRatingPsi160 && CalcTotalDynamicHeadPressure > PressureRatingPsi125)
                {
                    _calcPressureClass = EstimatedIronPipeSizeStandardDiameterRatioNumber135;
                    ShowNoError();
                }
                else if (CalcTotalDynamicHeadPressure <= PressureRatingPsi125)
                {
                    _calcPressureClass = EstimatedIronPipeSizeStandardDiameterRatioNumber17;
                    ShowNoError();
                }
                else
                {
                    _calcPressureClass = 0;
                    ShowPressureClassError();
                }

                return _calcPressureClass;
            }
            set { ChangeAndNotify(ref _calcPressureClass, value); }

        }

        private double _calcFluidPipeVelocity;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcFluidPipeVelocity
        {
            get { return PumpingCalculations.CalcFluidPipeVelocity(FlowRate, CalcPipeInsideDiameter); }
            set { ChangeAndNotify(ref _calcFluidPipeVelocity, value); }
        }

        private double _calcShaftHorsePower;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcShaftHorsePower
        {
            get { return PumpingCalculations.CalcShaftHorsePower(FlowRate, CalcTotalDynamicHead, EstimatedPumpEfficiency, PumpSizingSafetyFactor); }
            set { ChangeAndNotify(ref _calcShaftHorsePower, value); }
        }

        private double _calcGravelPipeBeddingWeight;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcGravelPipeBeddingWeight
        {
            get { return PumpingCalculations.CalcGravelPipeBeddingWeight(NominalPipeOutsideDiameterInches, PipeLayingLength, PipeBeddingThickness); }
            set { ChangeAndNotify(ref _calcGravelPipeBeddingWeight, value); }
        }

        
        #endregion

        #region Properties - Capital Costs

        private decimal _calcBoreholeCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcBoreholeCost
        {
            get
            {
                switch (BoreHoleInsideDiameterOptionsProperty)
                {
                    case RadioButtonsBoreholeInsideDiameterOptionsEnum.Option16Inches:
                        _calcBoreholeCost = PumpingCalculations.CalcBoreholeCost(WellQuantity, DrillingDepthToMinePool, BoreHoleSizingMultiplier, DrillingBoreholeAndCastingInstallationCost16);
                        break;
                    case RadioButtonsBoreholeInsideDiameterOptionsEnum.Option24Inches:
                        _calcBoreholeCost = PumpingCalculations.CalcBoreholeCost(WellQuantity, DrillingDepthToMinePool, BoreHoleSizingMultiplier, DrillingBoreholeAndCastingInstallationCost24);
                        break;
                    case RadioButtonsBoreholeInsideDiameterOptionsEnum.Option30Inches:
                        _calcBoreholeCost = PumpingCalculations.CalcBoreholeCost(WellQuantity, DrillingDepthToMinePool, BoreHoleSizingMultiplier, DrillingBoreholeAndCastingInstallationCost30);
                        break;
                    case RadioButtonsBoreholeInsideDiameterOptionsEnum.Option36Inches:
                        _calcBoreholeCost = PumpingCalculations.CalcBoreholeCost(WellQuantity, DrillingDepthToMinePool, BoreHoleSizingMultiplier, DrillingBoreholeAndCastingInstallationCost36);
                        break;
                    default:
                        break;
                }               
                return _calcBoreholeCost;
            }
            set { ChangeAndNotify(ref _calcBoreholeCost, value); }
        }

        private decimal _calcExcavationCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcExcavationCost
        {
            get { return PumpingCalculations.CalcExcavationCost(NominalPipeOutsideDiameterInches, PipeLayingLength, ExcavationUnitCost); }
            set { ChangeAndNotify(ref _calcExcavationCost, value); }
        }

        private decimal _calcBackfillAndCompactionCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcBackfillAndCompactionCost
        {
            get { return PumpingCalculations.CalcBackfillAndCompactionCost(NominalPipeOutsideDiameterInches, PipeLayingLength, BackfillAndCompactionUnitCost); }
            set { ChangeAndNotify(ref _calcBackfillAndCompactionCost, value); }
        }

        private decimal _calcPipeBeddingCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcPipeBeddingCost
        {
            get { return PumpingCalculations.CalcPipeBeddingCost(CalcGravelPipeBeddingWeight, GravelForPipeBeddingUnitCost); }
            set { ChangeAndNotify(ref _calcPipeBeddingCost, value); }
        }

        private decimal _calcPipeCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcPipeCost
        {
            get
            {
                if (NominalPipeOutsideDiameterInches == 8)
                {
                    _calcPipeCost = PumpingCalculations.CalcPipeCostNOD8(PipeLayingLength);
                }
                else if (NominalPipeOutsideDiameterInches == 10)
                {
                    _calcPipeCost = PumpingCalculations.CalcPipeCostNOD10(PipeLayingLength);
                }
                else if (NominalPipeOutsideDiameterInches == 12)
                {
                    _calcPipeCost = PumpingCalculations.CalcPipeCostNOD12(PipeLayingLength);
                }
                else if (NominalPipeOutsideDiameterInches == 16)
                {
                    _calcPipeCost = PumpingCalculations.CalcPipeCostNOD16(PipeLayingLength);
                }
                else if (NominalPipeOutsideDiameterInches == 18)
                {
                    _calcPipeCost = PumpingCalculations.CalcPipeCostNOD18(PipeLayingLength);
                }
                return _calcPipeCost;
            }
            set { ChangeAndNotify(ref _calcPipeCost, value); }
        }

        private decimal _calcPipeFusionAndInstallationCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcPipeFusionAndInstallationCost
        {
            get
            {
                if (NominalPipeOutsideDiameterInches == 8)
                {
                    _calcPipeFusionAndInstallationCost = PumpingCalculations.CalcPipeFusionAndInstallationCost(PipeLayingLength, PipeFusionCostNOD8);
                }
                else if (NominalPipeOutsideDiameterInches == 10)
                {
                    _calcPipeFusionAndInstallationCost = PumpingCalculations.CalcPipeFusionAndInstallationCost(PipeLayingLength, PipeFusionCostNOD10);
                }
                else if (NominalPipeOutsideDiameterInches == 12)
                {
                    _calcPipeFusionAndInstallationCost = PumpingCalculations.CalcPipeFusionAndInstallationCost(PipeLayingLength, PipeFusionCostNOD12);
                }
                else if (NominalPipeOutsideDiameterInches == 16)
                {
                    _calcPipeFusionAndInstallationCost = PumpingCalculations.CalcPipeFusionAndInstallationCost(PipeLayingLength, PipeFusionCostNOD16);
                }
                else if (NominalPipeOutsideDiameterInches == 18)
                {
                    _calcPipeFusionAndInstallationCost = PumpingCalculations.CalcPipeFusionAndInstallationCost(PipeLayingLength, PipeFusionCostNOD18);
                }
                return _calcPipeFusionAndInstallationCost;
            }
            set { ChangeAndNotify(ref _calcPipeFusionAndInstallationCost, value); }
        }

        private decimal _calcAirVacuumReleaseAssembiesCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcAirVacuumReleaseAssembiesCost
        {
            get
            {
                if (IsAirVacuumReleaseAssemblies)
                {
                    _calcAirVacuumReleaseAssembiesCost = PumpingCalculations.CalcAirVacuumReleaseAssembliesCost(AirVacuumReleaseAssembliesQuantity, AirVacuumReleaseAssembliesUnitCost);
                }
                else
                {
                    _calcAirVacuumReleaseAssembiesCost = 0m;
                }
                return _calcAirVacuumReleaseAssembiesCost;
            }
            set { ChangeAndNotify(ref _calcAirVacuumReleaseAssembiesCost, value); }
        }

        private decimal _calcTotalVerticalConveyancePipelineCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcTotalVerticalConveyancePipelineCost
        {
            get { return PumpingCalculations.CalcTotalVerticalConveyancePipelineCost(CalcExcavationCost, CalcBackfillAndCompactionCost, CalcPipeBeddingCost, CalcPipeCost, CalcPipeFusionAndInstallationCost, CalcAirVacuumReleaseAssembiesCost); }
            set { ChangeAndNotify(ref _calcTotalVerticalConveyancePipelineCost, value); }
        }

        private decimal _calcControlPanelAndScadaCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcControlPanelAndScadaCost
        {
            get { return PumpingCalculations.CalcControlPanelAndScadaCost(CalcShaftHorsePower); }
            set { ChangeAndNotify(ref _calcControlPanelAndScadaCost, value); }
        }

        private decimal _calcPumpCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcPumpCost
        {
            get { return PumpingCalculations.CalcPumpCost(CalcShaftHorsePower); }
            set { ChangeAndNotify(ref _calcPumpCost, value); }
        }

        private decimal _calcConcretePadCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcConcretePadCost
        {
            get { return PumpingCalculations.CalcConcretePadCost(PumpQuantity, ConcreteUnitCost); }
            set { ChangeAndNotify(ref _calcConcretePadCost, value); }
        }


        private decimal _calcSoftStartVfdCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcSoftStartVfdCost
        {
            get
            {
                if (IsSoftStartVfd)
                {
                    _calcSoftStartVfdCost = PumpingCalculations.CalcSoftStartVfdCost(CalcShaftHorsePower);
                }
                else
                {
                    _calcSoftStartVfdCost = 0m;
                }

                // Round to left 3 decimal places
                //_calcSoftStartVfdCost = (decimal)Round((double)_calcSoftStartVfdCost, -3);

                return _calcSoftStartVfdCost;
            }
            set { ChangeAndNotify(ref _calcSoftStartVfdCost, value); }
        }



        private decimal _calcTotalVerticalTurbinePumpCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcTotalVerticalTurbinePumpCost
        {
            get { return PumpingCalculations.CalcTotalVerticalTurbinePumpCost(CalcControlPanelAndScadaCost, CalcPumpCost, CalcConcretePadCost, CalcSoftStartVfdCost, PumpQuantity); }
            set { ChangeAndNotify(ref _calcTotalVerticalTurbinePumpCost, value); }
        }

        private decimal _calcCapitalCostTotal;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcCapitalCostTotal
        {
            get
            {
                _calcCapitalCostTotal = PumpingCalculations.CalcCapitalCostTotal(CalcBoreholeCost, CalcTotalVerticalConveyancePipelineCost, CalcTotalVerticalTurbinePumpCost);

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

        private decimal _calcAnnualCostElectric;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcAnnualCostElectric
        {
            get { return PumpingCalculations.CalcAnnualCostElectric(ElectricalUnitCost, PumpingTimeHoursPerDay, PumpingTimeDaysPerYear, CalcShaftHorsePower); }
            set { ChangeAndNotify(ref _calcAnnualCostElectric, value); }
        }

        private decimal _calcAnnualCostPumpMaintenance;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcAnnualCostPumpMaintenance
        {
            get { return PumpingCalculations.CalcAnnualCostPumpMaintenance(CalcPumpCost, PumpingTimeHoursPerDay, PumpingTimeDaysPerYear,  PumpMaintenanceFactor); }
            set { ChangeAndNotify(ref _calcAnnualCostPumpMaintenance, value); }
        }

        private decimal _calcAnnualCostPipelineMaintenance;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcAnnualCostPipelineMaintenance
        {
            get { return PumpingCalculations.CalcAnnualCostPipelineMaintenance(CalcTotalVerticalConveyancePipelineCost, PipelineMaintenanceFactor); }
            set { ChangeAndNotify(ref _calcAnnualCostPipelineMaintenance, value); }
        }

        private decimal _calcAnnualCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcAnnualCost
        {
            get
            {
                _calcAnnualCost = PumpingCalculations.CalcAnnualCostTotal(CalcAnnualCostElectric, CalcAnnualCostPumpMaintenance, CalcAnnualCostPipelineMaintenance);
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

        private double _recapitalizationCostLifeCycleBorehole;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostLifeCycleBorehole
        {
            get { return _recapitalizationCostLifeCycleBorehole; }
            set { ChangeAndNotify(ref _recapitalizationCostLifeCycleBorehole, value, nameof(RecapitalizationCostLifeCycleBorehole), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostLifeCyclePipeline;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostLifeCyclePipeline
        {
            get { return _recapitalizationCostLifeCyclePipeline; }
            set { ChangeAndNotify(ref _recapitalizationCostLifeCyclePipeline, value, nameof(RecapitalizationCostLifeCyclePipeline), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostLifeCycleControl;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostLifeCycleControl
        {
            get { return _recapitalizationCostLifeCycleControl; }
            set { ChangeAndNotify(ref _recapitalizationCostLifeCycleControl, value, nameof(RecapitalizationCostLifeCycleControl), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostLifeCyclePump;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostLifeCyclePump
        {
            get { return _recapitalizationCostLifeCyclePump; }
            set { ChangeAndNotify(ref _recapitalizationCostLifeCyclePump, value, nameof(RecapitalizationCostLifeCyclePump), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostLifeCycleVfd;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostLifeCycleVfd
        {
            get { return _recapitalizationCostLifeCycleVfd; }
            set { ChangeAndNotify(ref _recapitalizationCostLifeCycleVfd, value, nameof(RecapitalizationCostLifeCycleVfd), CalcPropertiesStringArray); }
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

        private double _recapitalizationCostPercentReplacementPipeline;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostPercentReplacementPipeline
        {
            get { return _recapitalizationCostPercentReplacementPipeline; }
            set { ChangeAndNotify(ref _recapitalizationCostPercentReplacementPipeline, value, nameof(RecapitalizationCostPercentReplacementPipeline), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostPercentReplacementControl;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostPercentReplacementControl
        {
            get { return _recapitalizationCostPercentReplacementControl; }
            set { ChangeAndNotify(ref _recapitalizationCostPercentReplacementControl, value, nameof(RecapitalizationCostPercentReplacementControl), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostPercentReplacementPump;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostPercentReplacementPump
        {
            get { return _recapitalizationCostPercentReplacementPump; }
            set { ChangeAndNotify(ref _recapitalizationCostPercentReplacementPump, value, nameof(RecapitalizationCostPercentReplacementPump), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostPercentReplacementVfd;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostPercentReplacementVfd
        {
            get { return _recapitalizationCostPercentReplacementVfd; }
            set { ChangeAndNotify(ref _recapitalizationCostPercentReplacementVfd, value, nameof(RecapitalizationCostPercentReplacementVfd), CalcPropertiesStringArray); }
        }

        private decimal _calcRapitalizationCostBorehole;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostBorehole
        {
            get
            {
                return PumpingCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
                                                                    RecapitalizationCostInflationRate, RecapitalizationCostLifeCycleBorehole,
                                                                    CalcBoreholeCost, RecapitalizationCostPercentReplacementBorehole);
            }
            set { ChangeAndNotify(ref _calcRapitalizationCostBorehole, value); }
        }

        private decimal _calcRapitalizationCostPipeline;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostPipeline
        {
            get
            {
                return PumpingCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
                                                                        RecapitalizationCostInflationRate, RecapitalizationCostLifeCyclePipeline,
                                                                        CalcTotalVerticalConveyancePipelineCost, RecapitalizationCostPercentReplacementPipeline);
            }
            set { ChangeAndNotify(ref _calcRapitalizationCostPipeline, value); }
        }

        private decimal _calcRapitalizationCostControl;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostControl
        {
            get
            {
                return PumpingCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
                                                                        RecapitalizationCostInflationRate, RecapitalizationCostLifeCycleControl,
                                                                        CalcControlPanelAndScadaCost, RecapitalizationCostPercentReplacementControl);
            }
            set { ChangeAndNotify(ref _calcRapitalizationCostControl, value); }
        }

        private decimal _calcRapitalizationCostPump;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostPump
        {
            get
            {
                return PumpingCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
                                                                        RecapitalizationCostInflationRate, RecapitalizationCostLifeCyclePump,
                                                                        CalcPumpCost, RecapitalizationCostPercentReplacementPump);
            }
            set { ChangeAndNotify(ref _calcRapitalizationCostPump, value); }
        }

        private decimal _calcRapitalizationCostVfd;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostVfd
        {
            get
            {
                return PumpingCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
                                                                        RecapitalizationCostInflationRate, RecapitalizationCostLifeCycleVfd,
                                                                        CalcSoftStartVfdCost, RecapitalizationCostPercentReplacementVfd);
            }
            set { ChangeAndNotify(ref _calcRapitalizationCostVfd, value); }
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
            get { return PumpingCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn, 
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
                    case "Borehole":
                        item.MaterialCostDefault = CalcBoreholeCost;
                        break;
                    case "Pipeline":
                        item.MaterialCostDefault = CalcTotalVerticalConveyancePipelineCost;
                        break;
                    case "ControlScada":
                        item.MaterialCostDefault = CalcControlPanelAndScadaCost;
                        break;
                    case "Pump":
                        item.MaterialCostDefault = CalcPumpCost;
                        break;
                    case "Vfd":
                        item.MaterialCostDefault = CalcSoftStartVfdCost;
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
                    case "Borehole":
                        item.TotalCost = CalcRecapitalizationCostBorehole;
                        break;
                    case "Pipeline":
                        item.TotalCost = CalcRecapitalizationCostPipeline;
                        break;
                    case "ControlScada":
                        item.TotalCost = CalcRecapitalizationCostControl;
                        break;
                    case "Pump":
                        item.TotalCost = CalcRecapitalizationCostPump;
                        break;
                    case "Vfd":
                        item.TotalCost = CalcRecapitalizationCostVfd;
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
                Name = "Pipeline",
                NameFixed = "Pipeline",
                LifeCycle = RecapitalizationCostLifeCyclePipeline,
                PercentReplacement = RecapitalizationCostPercentReplacementPipeline,
                MaterialCostDefault = CalcTotalVerticalConveyancePipelineCost,
                UseCustomCost = false,
                MaterialCostCustom = 0m,
                TotalCost = CalcRecapitalizationCostPipeline
            });
            RecapMaterials.Add(new RecapMaterial
            { 
                IsSelected = true,
                Name = "Control/SCADA",
                NameFixed = "ControlScada",
                LifeCycle = RecapitalizationCostLifeCycleControl,
                PercentReplacement = RecapitalizationCostPercentReplacementControl,
                MaterialCostDefault = CalcControlPanelAndScadaCost,
                UseCustomCost = false,
                MaterialCostCustom = 0m,
                TotalCost = CalcRecapitalizationCostControl
            });
            RecapMaterials.Add(new RecapMaterial
            {
                IsSelected = true,
                Name = "Pump",
                NameFixed = "Pump",
                LifeCycle = RecapitalizationCostLifeCyclePump,
                PercentReplacement = RecapitalizationCostPercentReplacementPump,
                MaterialCostDefault = CalcPumpCost,
                UseCustomCost = false,
                MaterialCostCustom = 0m,
                TotalCost = CalcRecapitalizationCostPump
            });
            RecapMaterials.Add(new RecapMaterial
            {
                IsSelected = true,
                Name = "VFD",
                NameFixed = "SoftVfd",
                LifeCycle = RecapitalizationCostLifeCycleVfd,
                PercentReplacement = RecapitalizationCostPercentReplacementVfd,
                MaterialCostDefault = CalcSoftStartVfdCost,
                UseCustomCost = false,
                MaterialCostCustom = 0m,
                TotalCost = CalcRecapitalizationCostVfd
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
            ((RecapMaterial)sender).TotalCost = PumpingCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
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
        public ICommand SetNominalOutsideDiameterCommand { get; }
        public ICommand SetEstimatedIronPipeSizeStandardDiameterRatioNumberCommand { get; }
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
            var customDialog = new CustomDialog() { Title = "About Pumping" };

            var customDialogViewModel = new CustomDialogViewModel(instance =>
            {
                _dialogCoordinator.HideMetroDialogAsync(this, customDialog);
            });
            customDialogViewModel.Message = Resources.infoPumping;
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

        private ICommand _showMessageDialogCommandVerticalTurbinePumpBoreHoles;
        public ICommand ShowMessageDialogCommandVerticalTurbinePumpBoreHoles
        {
            get
            {
                return _showMessageDialogCommandVerticalTurbinePumpBoreHoles ?? (this._showMessageDialogCommandVerticalTurbinePumpBoreHoles = new SimpleCommand
                {
                    CanExecuteDelegate = x => true,
                    ExecuteDelegate = async x =>
                    {
                        string message = Resources.infoVerticalTurbinePumpBoreHolesPumping;
                        await _dialogCoordinator.ShowMessageAsync(this, "Vertical Turbine Pump Boreholes", message);
                    }
                });
            }
        }

        private ICommand _showMessageDialogCommandConveyancePipeline;
        public ICommand ShowMessageDialogCommandConveyancePipeline
        {
            get
            {
                return _showMessageDialogCommandConveyancePipeline ?? (this._showMessageDialogCommandConveyancePipeline = new SimpleCommand
                {
                    CanExecuteDelegate = x => true,
                    ExecuteDelegate = async x =>
                    {
                        string message = Resources.infoConveyancePipelinePumping;
                        await _dialogCoordinator.ShowMessageAsync(this, "Conveyance Pipeline", message);
                    }
                });
            }
        }

        private ICommand _showMessageDialogCommandVerticalTurbinePumps;
        public ICommand ShowMessageDialogCommandVerticalTurbinePumps
        {
            get
            {
                return _showMessageDialogCommandVerticalTurbinePumps ?? (this._showMessageDialogCommandVerticalTurbinePumps = new SimpleCommand
                {
                    CanExecuteDelegate = x => true,
                    ExecuteDelegate = async x =>
                    {
                        string message = Resources.infoVerticalTurbinePumpsPumping;
                        await _dialogCoordinator.ShowMessageAsync(this, "Vertical Turbine Pumps", message);
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
                        string message = Resources.infoSizingSummaryPumping;
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
                        string message = Resources.infoCapitalCostPumping;
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
                        string message = Resources.infoAnnualCostPumping;
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
                        string message = Resources.infoRecapitalizationCostPumping;
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

        public void SetNominalOutsideDiameter(object nominalOutsideDiameter)
        {
            GeneralItem itemNominalOutsideDiameter = (GeneralItem)nominalOutsideDiameter;

            switch (itemNominalOutsideDiameter.Name)
            {
                case "8":
                    NominalPipeOutsideDiameterInchesName = NominalPipeOutsideDiameterInchesName8;
                    NominalPipeOutsideDiameterInches = NominalPipeOutsideDiameterInches8;
                    break;
                case "10":
                    NominalPipeOutsideDiameterInchesName = NominalPipeOutsideDiameterInchesName10;
                    NominalPipeOutsideDiameterInches = NominalPipeOutsideDiameterInches10;
                    break;
                case "12":
                    NominalPipeOutsideDiameterInchesName = NominalPipeOutsideDiameterInchesName12;
                    NominalPipeOutsideDiameterInches = NominalPipeOutsideDiameterInches12;
                    break;
                case "16":
                    NominalPipeOutsideDiameterInchesName = NominalPipeOutsideDiameterInchesName16;
                    NominalPipeOutsideDiameterInches = NominalPipeOutsideDiameterInches16;
                    break;
                case "18":
                    NominalPipeOutsideDiameterInchesName = NominalPipeOutsideDiameterInchesName18;
                    NominalPipeOutsideDiameterInches = NominalPipeOutsideDiameterInches18;
                    break;
                default:
                    break;
            }
        }

        public void SetEstimatedIronPipeSizeStandardDiameterRatioNumber(object estimatedIronPipeSizeStandardDiameterRatioNumber)
        {
            GeneralItem itemEstimatedIronPipeSizeStandardDiameterRatioNumber = (GeneralItem)estimatedIronPipeSizeStandardDiameterRatioNumber;
            
            switch (itemEstimatedIronPipeSizeStandardDiameterRatioNumber.Name)
            {
                case "7":
                    EstimatedIronPipeSizeStandardDiameterRatioNumberName = EstimatedIronPipeSizeStandardDiameterRatioNumberName7;
                    EstimatedIronPipeSizeStandardDiameterRatioNumber = EstimatedIronPipeSizeStandardDiameterRatioNumber7;
                    break;
                case "9":
                    EstimatedIronPipeSizeStandardDiameterRatioNumberName = EstimatedIronPipeSizeStandardDiameterRatioNumberName9;
                    EstimatedIronPipeSizeStandardDiameterRatioNumber = EstimatedIronPipeSizeStandardDiameterRatioNumber9;
                    break;
                case "11":
                    EstimatedIronPipeSizeStandardDiameterRatioNumberName = EstimatedIronPipeSizeStandardDiameterRatioNumberName11;
                    EstimatedIronPipeSizeStandardDiameterRatioNumber = EstimatedIronPipeSizeStandardDiameterRatioNumber11;
                    break;
                case "13.5":
                    EstimatedIronPipeSizeStandardDiameterRatioNumberName = EstimatedIronPipeSizeStandardDiameterRatioNumberName135;
                    EstimatedIronPipeSizeStandardDiameterRatioNumber = EstimatedIronPipeSizeStandardDiameterRatioNumber135;
                    break;
                case "17":
                    EstimatedIronPipeSizeStandardDiameterRatioNumberName = EstimatedIronPipeSizeStandardDiameterRatioNumberName17;
                    EstimatedIronPipeSizeStandardDiameterRatioNumber = EstimatedIronPipeSizeStandardDiameterRatioNumber17;
                    break;
                default:
                    break;
            }
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

        private void ShowPressureClassError()
        {
            IsError = true;
            IsMajorError = false;
            IsOpenFlyoutError = true;
            ErrorMessage = "Use of HDPE is Not Recommended";
            ErrorMessageShort = "The pressure rating does not fit within the recommended range of 125-335 PSI. Calculated pressure class does not Match SDR. In order to bring the pressure within acceptable limits of HDPE Pipe, please either adjust the SDR, increase Nominal Outside Pipe Diameter, Increase SDR Number, Decrease Pipe Laying Length or Decrease Static and or Incidental Head losses.";
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

        public PumpingViewModel(IDialogCoordinator dialogCoordinator)
        {
            // MahApps dialog coordinator
            _dialogCoordinator = dialogCoordinator;

            // Assign the proper functions to the open and save commands
            OpenCommand = new RelayCommand(OpenFile);
            SaveCommand = new RelayCommand(SaveFile);
            HelpCommand = new RelayCommand(ShowHelp);
            SetNominalOutsideDiameterCommand = new RelayCommandWithParameter(SetNominalOutsideDiameter);
            SetEstimatedIronPipeSizeStandardDiameterRatioNumberCommand = new RelayCommandWithParameter(SetEstimatedIronPipeSizeStandardDiameterRatioNumber);
            SyncCommand = new RelayCommand(SyncWithMainUi);

            // Get a list of property names and filter the names to include only those that start with "Calc" in order
            // to use with the NotifyAndChange.  This eliminates the need to write every property name that needs 
            // to be notified of changes by the user.
            PropertiesStringList = Helpers.GetNamesOfClassProperties(this);
            CalcPropertiesStringArray = Helpers.FilterPropertiesList(PropertiesStringList, "Calc");

            // Initialize the model name and user name
            ModuleType = "Pumping";
            ModuleName = "Untitled module";
            ModuleTreatmentType = "Project";
            ModuleId = random.Next(1000);

            IsError = false;
            IsMajorError = false;
            ErrorMessage = "";
            ErrorMessageShort = "";

            // Initialize check boxes
            IsAirVacuumReleaseAssemblies = true;
            IsSoftStartVfd = true;

            // Initialize radio buttons
            BoreHoleInsideDiameterOptionsProperty = RadioButtonsBoreholeInsideDiameterOptionsEnum.Option16Inches;

            // Dummy variables to force syncing
            Dummy = 0;

            // Read the xml data file and assign property values accordingly.
            OpenXmlAndAssignValues(@"..\..\Data\default-data-pumping.xml");

            // Nominal Pipe Outside Diameter
            NominalPipeOutsideDiameterInchesName8 = NominalPipeOutsideDiameterInches8.ToString();
            NominalPipeOutsideDiameterInchesName10 = NominalPipeOutsideDiameterInches10.ToString();
            NominalPipeOutsideDiameterInchesName12 = NominalPipeOutsideDiameterInches12.ToString();
            NominalPipeOutsideDiameterInchesName16 = NominalPipeOutsideDiameterInches16.ToString();
            NominalPipeOutsideDiameterInchesName18 = NominalPipeOutsideDiameterInches18.ToString();

            NominalPipeOutsideDiameterInchesName = NominalPipeOutsideDiameterInchesName8;
            NominalPipeOutsideDiameterInches = NominalPipeOutsideDiameterInches8;

            NominalPipeOutsideDiameterInchesList = new List<GeneralItem>
            {
                new GeneralItem {Name = NominalPipeOutsideDiameterInchesName8, Value = NominalPipeOutsideDiameterInches8},
                new GeneralItem {Name = NominalPipeOutsideDiameterInchesName10, Value = NominalPipeOutsideDiameterInches10},
                new GeneralItem {Name = NominalPipeOutsideDiameterInchesName12, Value = NominalPipeOutsideDiameterInches12},
                new GeneralItem {Name = NominalPipeOutsideDiameterInchesName16, Value = NominalPipeOutsideDiameterInches16},
                new GeneralItem {Name = NominalPipeOutsideDiameterInchesName18, Value = NominalPipeOutsideDiameterInches18},
            };

            // Estimated Iron Pipe Size Standard Diameter Ratio Number
            EstimatedIronPipeSizeStandardDiameterRatioNumberName7 = EstimatedIronPipeSizeStandardDiameterRatioNumber7.ToString();
            EstimatedIronPipeSizeStandardDiameterRatioNumberName9 = EstimatedIronPipeSizeStandardDiameterRatioNumber9.ToString();
            EstimatedIronPipeSizeStandardDiameterRatioNumberName11 = EstimatedIronPipeSizeStandardDiameterRatioNumber11.ToString();
            EstimatedIronPipeSizeStandardDiameterRatioNumberName135 = EstimatedIronPipeSizeStandardDiameterRatioNumber135.ToString();
            EstimatedIronPipeSizeStandardDiameterRatioNumberName17 = EstimatedIronPipeSizeStandardDiameterRatioNumber17.ToString();

            EstimatedIronPipeSizeStandardDiameterRatioNumberName = EstimatedIronPipeSizeStandardDiameterRatioNumberName17;
            EstimatedIronPipeSizeStandardDiameterRatioNumber = EstimatedIronPipeSizeStandardDiameterRatioNumber17;

            EstimatedIronPipeSizeStandardDiameterRatioNumberList = new List<GeneralItem>
            {
                new GeneralItem {Name = EstimatedIronPipeSizeStandardDiameterRatioNumberName7, Value = EstimatedIronPipeSizeStandardDiameterRatioNumber7 },
                new GeneralItem {Name = EstimatedIronPipeSizeStandardDiameterRatioNumberName9, Value = EstimatedIronPipeSizeStandardDiameterRatioNumber9 },
                new GeneralItem {Name = EstimatedIronPipeSizeStandardDiameterRatioNumberName11, Value = EstimatedIronPipeSizeStandardDiameterRatioNumber11 },
                new GeneralItem {Name = EstimatedIronPipeSizeStandardDiameterRatioNumberName135, Value = EstimatedIronPipeSizeStandardDiameterRatioNumber135 },
                new GeneralItem {Name = EstimatedIronPipeSizeStandardDiameterRatioNumberName17, Value = EstimatedIronPipeSizeStandardDiameterRatioNumber17 },
            };



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
