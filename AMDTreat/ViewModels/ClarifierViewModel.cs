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

    public class ClarifierViewModel : PropertyChangedBase, IObserver<SharedData>
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

        #region Properties - Clarifier Design

        /// <summary>
        ///  Radio button binding
        /// </summary>
        public enum RadioButtonsClarifierDesignOptionsEnum
        {
            OptionConventional,
            OptionSolidsContact,
        }

        private RadioButtonsClarifierDesignOptionsEnum _clarifierDesignOptionsProperty;
        /// <summary>
        ///  User specified 
        /// </summary>
        public RadioButtonsClarifierDesignOptionsEnum ClarifierDesignOptionsProperty
        {
            get { return _clarifierDesignOptionsProperty; }
            set { ChangeAndNotify(ref _clarifierDesignOptionsProperty, value, nameof(ClarifierDesignOptionsProperty), CalcPropertiesStringArray); }
        }

        private double _clarifierFlowPerAreaConventional;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double ClarifierFlowPerAreaConventional
        {
            get { return _clarifierFlowPerAreaConventional; }
            set { ChangeAndNotify(ref _clarifierFlowPerAreaConventional, value, nameof(ClarifierFlowPerAreaConventional), CalcPropertiesStringArray); }
        }

        private double _clarifierFlowPerAreaSolidsContact;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double ClarifierFlowPerAreaSolidsContact
        {
            get { return _clarifierFlowPerAreaSolidsContact; }
            set { ChangeAndNotify(ref _clarifierFlowPerAreaSolidsContact, value, nameof(ClarifierFlowPerAreaSolidsContact), CalcPropertiesStringArray); }
        }

        private decimal _clarifierInternalsCostFactorConventional;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal ClarifierInternalsCostFactorConventional
        {
            get { return _clarifierInternalsCostFactorConventional; }
            set { ChangeAndNotify(ref _clarifierInternalsCostFactorConventional, value, nameof(ClarifierInternalsCostFactorConventional), CalcPropertiesStringArray); }
        }

        private decimal _clarifierInternalsCostFactorSolidsContact;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal ClarifierInternalsCostFactorSolidsContact
        {
            get { return _clarifierInternalsCostFactorSolidsContact; }
            set { ChangeAndNotify(ref _clarifierInternalsCostFactorSolidsContact, value, nameof(ClarifierInternalsCostFactorSolidsContact), CalcPropertiesStringArray); }
        }

        private List<GeneralCostItem> _clarifierConstructionMaterials;
        /// <summary>
        /// Collection 
        /// </summary>
        public List<GeneralCostItem> ClarifierConstructionMaterials
        {
            get { return _clarifierConstructionMaterials; }

            set { ChangeAndNotify(ref _clarifierConstructionMaterials, value, nameof(ClarifierConstructionMaterials), CalcPropertiesStringArray); }
        }

        private string _clarifierConstructionMaterialName;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string ClarifierConstructionMaterialName
        {
            get { return _clarifierConstructionMaterialName; }
            set { ChangeAndNotify(ref _clarifierConstructionMaterialName, value, nameof(ClarifierConstructionMaterialName), CalcPropertiesStringArray); }
        }

        private string _clarifierConstructionMaterialNameConcrete;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string ClarifierConstructionMaterialNameConcrete
        {
            get { return _clarifierConstructionMaterialNameConcrete; }
            set { ChangeAndNotify(ref _clarifierConstructionMaterialNameConcrete, value, nameof(ClarifierConstructionMaterialNameConcrete), CalcPropertiesStringArray); }
        }

        private string _clarifierConstructionMaterialNameBoltedSteel;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string ClarifierConstructionMaterialNameBoltedSteel
        {
            get { return _clarifierConstructionMaterialNameBoltedSteel; }
            set { ChangeAndNotify(ref _clarifierConstructionMaterialNameBoltedSteel, value, nameof(ClarifierConstructionMaterialNameBoltedSteel), CalcPropertiesStringArray); }
        }

        private string _clarifierConstructionMaterialNameWeldedSteel;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string ClarifierConstructionMaterialNameWeldedSteel
        {
            get { return _clarifierConstructionMaterialNameWeldedSteel; }
            set { ChangeAndNotify(ref _clarifierConstructionMaterialNameWeldedSteel, value, nameof(ClarifierConstructionMaterialNameWeldedSteel), CalcPropertiesStringArray); }
        }

        private decimal _clarifierConstructionMaterialCostFactor;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal ClarifierConstructionMaterialCostFactor
        {
            get { return _clarifierConstructionMaterialCostFactor; }
            set { ChangeAndNotify(ref _clarifierConstructionMaterialCostFactor, value, nameof(ClarifierConstructionMaterialCostFactor), CalcPropertiesStringArray); }
        }

        private decimal _clarifierConstructionMaterialCostFactorConcrete;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal ClarifierConstructionMaterialCostFactorConcrete
        {
            get { return _clarifierConstructionMaterialCostFactorConcrete; }
            set { ChangeAndNotify(ref _clarifierConstructionMaterialCostFactorConcrete, value, nameof(ClarifierConstructionMaterialCostFactorConcrete), CalcPropertiesStringArray); }
        }

        private decimal _clarifierConstructionMaterialCostFactorBoltedSteel;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal ClarifierConstructionMaterialCostFactorBoltedSteel
        {
            get { return _clarifierConstructionMaterialCostFactorBoltedSteel; }
            set { ChangeAndNotify(ref _clarifierConstructionMaterialCostFactorBoltedSteel, value, nameof(ClarifierConstructionMaterialCostFactorBoltedSteel), CalcPropertiesStringArray); }
        }

        private decimal _clarifierConstructionMaterialCostFactorWeldedSteel;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal ClarifierConstructionMaterialCostFactorWeldedSteel
        {
            get { return _clarifierConstructionMaterialCostFactorWeldedSteel; }
            set { ChangeAndNotify(ref _clarifierConstructionMaterialCostFactorWeldedSteel, value, nameof(ClarifierConstructionMaterialCostFactorWeldedSteel), CalcPropertiesStringArray); }
        }

        private double _clarifierQuantity;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double ClarifierQuantity
        {
            get { return _clarifierQuantity; }
            set { ChangeAndNotify(ref _clarifierQuantity, value, nameof(ClarifierQuantity), CalcPropertiesStringArray); }
        }

        private double _clarifierWaterHeight;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double ClarifierWaterHeight
        {
            get { return _clarifierWaterHeight; }
            set { ChangeAndNotify(ref _clarifierWaterHeight, value, nameof(ClarifierWaterHeight), CalcPropertiesStringArray); }
        }

        private double _clarifierFreeboard;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double ClarifierFreeboard
        {
            get { return _clarifierFreeboard; }
            set { ChangeAndNotify(ref _clarifierFreeboard, value, nameof(ClarifierFreeboard), CalcPropertiesStringArray); }
        }

        private double _sludgeBlanketDepth;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double SludgeBlanketDepth
        {
            get { return _sludgeBlanketDepth; }
            set { ChangeAndNotify(ref _sludgeBlanketDepth, value, nameof(SludgeBlanketDepth), CalcPropertiesStringArray); }
        }

        #endregion

        #region Equipment

        private decimal _overflowWeirUnitCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal OverflowWeirUnitCost
        {
            get { return _overflowWeirUnitCost; }
            set { ChangeAndNotify(ref _overflowWeirUnitCost, value, nameof(OverflowWeirUnitCost), CalcPropertiesStringArray); }
        }

        private decimal _catwalkUnitCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal CatwalkUnitCost
        {
            get { return _catwalkUnitCost; }
            set { ChangeAndNotify(ref _catwalkUnitCost, value, nameof(CatwalkUnitCost), CalcPropertiesStringArray); }
        }

        private bool _isDensityCurrentBaffle;
        /// <summary>
        ///  User specified 
        /// </summary>
        public bool IsDensityCurrentBaffle
        {
            get { return _isDensityCurrentBaffle; }
            set { ChangeAndNotify(ref _isDensityCurrentBaffle, value, nameof(IsDensityCurrentBaffle), CalcPropertiesStringArray); }
        }

        private decimal _densityCurrentBaffleUnitCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal DensityCurrentBaffleUnitCost
        {
            get { return _densityCurrentBaffleUnitCost; }
            set { ChangeAndNotify(ref _densityCurrentBaffleUnitCost, value, nameof(DensityCurrentBaffleUnitCost), CalcPropertiesStringArray); }
        }

        private List<GeneralCostItem> _tankProtectiveCoatingsConcrete;
        /// <summary>
        /// Collection 
        /// </summary>
        public List<GeneralCostItem> TankProtectiveCoatingsConcrete
        {
            get { return _tankProtectiveCoatingsConcrete; }

            set { ChangeAndNotify(ref _tankProtectiveCoatingsConcrete, value, nameof(TankProtectiveCoatingsConcrete), CalcPropertiesStringArray); }
        }

        private List<GeneralCostItem> _tankProtectiveCoatingsSteel;
        /// <summary>
        /// Collection 
        /// </summary>
        public List<GeneralCostItem> TankProtectiveCoatingsSteel
        {
            get { return _tankProtectiveCoatingsSteel; }

            set { ChangeAndNotify(ref _tankProtectiveCoatingsSteel, value, nameof(TankProtectiveCoatingsSteel), CalcPropertiesStringArray); }
        }

        private List<GeneralCostItem> _tankProtectiveCoatings;
        /// <summary>
        /// Collection 
        /// </summary>
        public List<GeneralCostItem> TankProtectiveCoatings
        {
            get
            {
                if (ClarifierConstructionMaterialName == ClarifierConstructionMaterialNameConcrete)
                {
                    _tankProtectiveCoatings = TankProtectiveCoatingsConcrete;
                }
                else
                {
                    _tankProtectiveCoatings = TankProtectiveCoatingsSteel;
                }

                return _tankProtectiveCoatings;
            }

            set { ChangeAndNotify(ref _tankProtectiveCoatings, value, nameof(TankProtectiveCoatings), CalcPropertiesStringArray); }
        }

        private string _tankProtectiveCoatingName;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string TankProtectiveCoatingName
        {
            get { return _tankProtectiveCoatingName; }
            set { ChangeAndNotify(ref _tankProtectiveCoatingName, value, nameof(TankProtectiveCoatingName), CalcPropertiesStringArray); }
        }

        private string _tankProtectiveCoatingNameAlkyd;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string TankProtectiveCoatingNameAlkyd
        {
            get { return _tankProtectiveCoatingNameAlkyd; }
            set { ChangeAndNotify(ref _tankProtectiveCoatingNameAlkyd, value, nameof(TankProtectiveCoatingNameAlkyd), CalcPropertiesStringArray); }
        }

        private string _tankProtectiveCoatingNameEpoxy;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string TankProtectiveCoatingNameEpoxy
        {
            get { return _tankProtectiveCoatingNameEpoxy; }
            set { ChangeAndNotify(ref _tankProtectiveCoatingNameEpoxy, value, nameof(TankProtectiveCoatingNameEpoxy), CalcPropertiesStringArray); }
        }

        private string _tankProtectiveCoatingNameZincUrethane;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string TankProtectiveCoatingNameZincUrethane
        {
            get { return _tankProtectiveCoatingNameZincUrethane; }
            set { ChangeAndNotify(ref _tankProtectiveCoatingNameZincUrethane, value, nameof(TankProtectiveCoatingNameZincUrethane), CalcPropertiesStringArray); }
        }

        private string _tankProtectiveCoatingNameNone;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string TankProtectiveCoatingNameNone
        {
            get { return _tankProtectiveCoatingNameNone; }
            set { ChangeAndNotify(ref _tankProtectiveCoatingNameNone, value, nameof(TankProtectiveCoatingNameNone), CalcPropertiesStringArray); }
        }

        private decimal _tankProtectiveCoatingUnitCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal TankProtectiveCoatingUnitCost
        {
            get { return _tankProtectiveCoatingUnitCost; }
            set { ChangeAndNotify(ref _tankProtectiveCoatingUnitCost, value, nameof(TankProtectiveCoatingUnitCost), CalcPropertiesStringArray); }
        }

        private decimal _tankProtectiveCoatingUnitCostAlkyd;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal TankProtectiveCoatingUnitCostAlkyd
        {
            get { return _tankProtectiveCoatingUnitCostAlkyd; }
            set { ChangeAndNotify(ref _tankProtectiveCoatingUnitCostAlkyd, value, nameof(TankProtectiveCoatingUnitCostAlkyd), CalcPropertiesStringArray); }
        }

        private decimal _tankProtectiveCoatingUnitCostEpoxy;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal TankProtectiveCoatingUnitCostEpoxy
        {
            get { return _tankProtectiveCoatingUnitCostEpoxy; }
            set { ChangeAndNotify(ref _tankProtectiveCoatingUnitCostEpoxy, value, nameof(TankProtectiveCoatingUnitCostEpoxy), CalcPropertiesStringArray); }
        }

        private decimal _tankProtectiveCoatingUnitCostZincUrethane;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal TankProtectiveCoatingUnitCostZincUrethane
        {
            get { return _tankProtectiveCoatingUnitCostZincUrethane; }
            set { ChangeAndNotify(ref _tankProtectiveCoatingUnitCostZincUrethane, value, nameof(TankProtectiveCoatingUnitCostZincUrethane), CalcPropertiesStringArray); }
        }

        private decimal _tankProtectiveCoatingUnitCostNone;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal TankProtectiveCoatingUnitCostNone
        {
            get { return _tankProtectiveCoatingUnitCostNone; }
            set { ChangeAndNotify(ref _tankProtectiveCoatingUnitCostNone, value, nameof(TankProtectiveCoatingUnitCostNone), CalcPropertiesStringArray); }
        }

        private double _clarifierMotorOperationalTimeHoursPerDay;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double ClarifierMotorOperationalTimeHoursPerDay
        {
            get { return _clarifierMotorOperationalTimeHoursPerDay; }
            set { ChangeAndNotify(ref _clarifierMotorOperationalTimeHoursPerDay, value, nameof(ClarifierMotorOperationalTimeHoursPerDay), CalcPropertiesStringArray); }
        }

        private double _clarifierMotorOperationalTimeDaysPerYear;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double ClarifierMotorOperationalTimeDaysPerYear
        {
            get { return _clarifierMotorOperationalTimeDaysPerYear; }
            set { ChangeAndNotify(ref _clarifierMotorOperationalTimeDaysPerYear, value, nameof(ClarifierMotorOperationalTimeDaysPerYear), CalcPropertiesStringArray); }
        }

        /// <summary>
        ///  Radio button binding
        /// </summary>
        public enum RadioButtonsImpellerMotorPowerOptionsEnum
        {
            OptionEstimate,
            OptionUserSpecified,
        }

        private RadioButtonsImpellerMotorPowerOptionsEnum _impellerMotorOptionsProperty;
        /// <summary>
        ///  User specified 
        /// </summary>
        public RadioButtonsImpellerMotorPowerOptionsEnum ImpellerMotorOptionsProperty
        {
            get { return _impellerMotorOptionsProperty; }
            set { ChangeAndNotify(ref _impellerMotorOptionsProperty, value, nameof(ImpellerMotorOptionsProperty), CalcPropertiesStringArray); }
        }

        private double _impellerMotorPowerUserSpecified;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double ImpellerMotorPowerUserSpecified
        {
            get { return _impellerMotorPowerUserSpecified; }
            set { ChangeAndNotify(ref _impellerMotorPowerUserSpecified, value, nameof(ImpellerMotorPowerUserSpecified), CalcPropertiesStringArray); }
        }

        private double _rakeDriveMotorPower;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double RakeDriveMotorPower
        {
            get { return _rakeDriveMotorPower; }
            set { ChangeAndNotify(ref _rakeDriveMotorPower, value, nameof(RakeDriveMotorPower), CalcPropertiesStringArray); }
        }

        private bool _isSludgeRecirculationPump;
        /// <summary>
        ///  User specified 
        /// </summary>
        public bool IsSludgeRecirculationPump
        {
            get { return _isSludgeRecirculationPump; }
            set { ChangeAndNotify(ref _isSludgeRecirculationPump, value, nameof(IsSludgeRecirculationPump), CalcPropertiesStringArray); }
        }

        /// <summary>
        ///  Radio button binding
        /// </summary>
        public enum RadioButtonsSludgeRecirculationPumpOptionsEnum
        {
            OptionEstimate,
            OptionUserSpecified,
        }

        private RadioButtonsSludgeRecirculationPumpOptionsEnum _sludgeRecirculationPumpOptionsProperty;
        /// <summary>
        ///  User specified 
        /// </summary>
        public RadioButtonsSludgeRecirculationPumpOptionsEnum SludgeRecirculationPumpOptionsProperty
        {
            get { return _sludgeRecirculationPumpOptionsProperty; }
            set { ChangeAndNotify(ref _sludgeRecirculationPumpOptionsProperty, value, nameof(SludgeRecirculationPumpOptionsProperty), CalcPropertiesStringArray); }
        }


        private double _sludgeRecirculationPumpFlowUserSpecified;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double SludgeRecirculationPumpFlowUserSpecified
        {
            get { return _sludgeRecirculationPumpFlowUserSpecified; }
            set { ChangeAndNotify(ref _sludgeRecirculationPumpFlowUserSpecified, value, nameof(SludgeRecirculationPumpFlowUserSpecified), CalcPropertiesStringArray); }
        }

        private double _sludgeDisposalHorizontalCentrifugalPumpQuantity;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double SludgeDisposalHorizontalCentrifugalPumpQuantity
        {
            get { return _sludgeDisposalHorizontalCentrifugalPumpQuantity; }
            set { ChangeAndNotify(ref _sludgeDisposalHorizontalCentrifugalPumpQuantity, value, nameof(SludgeDisposalHorizontalCentrifugalPumpQuantity), CalcPropertiesStringArray); }
        }

        /// <summary>
        ///  Radio button binding
        /// </summary>
        public enum RadioButtonsSludgeDisposalPumpOptionsEnum
        {
            OptionEstimate,
            OptionUserSpecified,
        }

        private RadioButtonsSludgeDisposalPumpOptionsEnum _sludgeDisposalPumpOptionsProperty;
        /// <summary>
        ///  User specified 
        /// </summary>
        public RadioButtonsSludgeDisposalPumpOptionsEnum SludgeDisposalPumpOptionsProperty
        {
            get { return _sludgeDisposalPumpOptionsProperty; }
            set { ChangeAndNotify(ref _sludgeDisposalPumpOptionsProperty, value, nameof(SludgeDisposalPumpOptionsProperty), CalcPropertiesStringArray); }
        }

        private double _sludgeDisposalPumpFlowUserSpecified;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double SludgeDisposalPumpFlowUserSpecified
        {
            get { return _sludgeDisposalPumpFlowUserSpecified; }
            set { ChangeAndNotify(ref _sludgeDisposalPumpFlowUserSpecified, value, nameof(SludgeDisposalPumpFlowUserSpecified), CalcPropertiesStringArray); }
        }

        private double _sludgeVolume;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double SludgeVolume
        {
            get { return _sludgeVolume; }
            set { ChangeAndNotify(ref _sludgeVolume, value, nameof(SludgeVolume), CalcPropertiesStringArray); }
        }

        private double _sludgeDisposalPumpEfficiency;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double SludgeDisposalPumpEfficiency
        {
            get { return _sludgeDisposalPumpEfficiency; }
            set { ChangeAndNotify(ref _sludgeDisposalPumpEfficiency, value, nameof(SludgeDisposalPumpEfficiency), CalcPropertiesStringArray); }
        }

        private double _sludgeDisposalPumpSizingSafetyFactor;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double SludgeDisposalPumpSizingSafetyFactor
        {
            get { return _sludgeDisposalPumpSizingSafetyFactor; }
            set { ChangeAndNotify(ref _sludgeDisposalPumpSizingSafetyFactor, value, nameof(SludgeDisposalPumpSizingSafetyFactor), CalcPropertiesStringArray); }
        }

        private double _aluminumSpecificGravity;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double AluminumSpecificGravity
        {
            get { return _aluminumSpecificGravity; }
            set { ChangeAndNotify(ref _aluminumSpecificGravity, value, nameof(AluminumSpecificGravity), CalcPropertiesStringArray); }
        }

        private double _ferrousIronSpecificGravity;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double FerrousIronSpecificGravity
        {
            get { return _ferrousIronSpecificGravity; }
            set { ChangeAndNotify(ref _ferrousIronSpecificGravity, value, nameof(FerrousIronSpecificGravity), CalcPropertiesStringArray); }
        }

        private double _ferricIronSpecificGravity;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double FerricIronSpecificGravity
        {
            get { return _ferricIronSpecificGravity; }
            set { ChangeAndNotify(ref _ferricIronSpecificGravity, value, nameof(FerricIronSpecificGravity), CalcPropertiesStringArray); }
        }

        private double _manganeseSpecificGravity;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double ManganeseSpecificGravity
        {
            get { return _manganeseSpecificGravity; }
            set { ChangeAndNotify(ref _manganeseSpecificGravity, value, nameof(ManganeseSpecificGravity), CalcPropertiesStringArray); }
        }

        private double _calciteSpecificGravity;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double CalciteSpecificGravity
        {
            get { return _calciteSpecificGravity; }
            set { ChangeAndNotify(ref _calciteSpecificGravity, value, nameof(CalciteSpecificGravity), CalcPropertiesStringArray); }
        }

        private decimal _foundationConcreteMaterialAndPlacementCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal FoundationConcreteMaterialAndPlacementCost
        {
            get { return _foundationConcreteMaterialAndPlacementCost; }
            set { ChangeAndNotify(ref _foundationConcreteMaterialAndPlacementCost, value, nameof(FoundationConcreteMaterialAndPlacementCost), CalcPropertiesStringArray); }
        }

        private List<FoundationSiteSoil> _foundationSiteSoils;
        /// <summary>
        /// Collection 
        /// </summary>
        public List<FoundationSiteSoil> FoundationSiteSoils
        {
            get { return _foundationSiteSoils; }

            set { ChangeAndNotify(ref _foundationSiteSoils, value, nameof(FoundationSiteSoils), CalcPropertiesStringArray); }
        }

        private string _foundationSiteSoilLoadBearing1500Name;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string FoundationSiteSoilLoadBearing1500Name
        {
            get { return _foundationSiteSoilLoadBearing1500Name; }
            set { ChangeAndNotify(ref _foundationSiteSoilLoadBearing1500Name, value, nameof(FoundationSiteSoilLoadBearing1500Name), CalcPropertiesStringArray); }
        }

        private string _foundationSiteSoilLoadBearing3000Name;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string FoundationSiteSoilLoadBearing3000Name
        {
            get { return _foundationSiteSoilLoadBearing3000Name; }
            set { ChangeAndNotify(ref _foundationSiteSoilLoadBearing3000Name, value, nameof(FoundationSiteSoilLoadBearing3000Name), CalcPropertiesStringArray); }
        }

        private string _foundationSiteSoilLoadBearing4500Name;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string FoundationSiteSoilLoadBearing4500Name
        {
            get { return _foundationSiteSoilLoadBearing4500Name; }
            set { ChangeAndNotify(ref _foundationSiteSoilLoadBearing4500Name, value, nameof(FoundationSiteSoilLoadBearing4500Name), CalcPropertiesStringArray); }
        }

        private double _foundationSiteSoilLoadBearing1500Quantity;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double FoundationSiteSoilLoadBearing1500Quantity
        {
            get { return _foundationSiteSoilLoadBearing1500Quantity; }
            set { ChangeAndNotify(ref _foundationSiteSoilLoadBearing1500Quantity, value, nameof(FoundationSiteSoilLoadBearing1500Quantity), CalcPropertiesStringArray); }
        }

        private double _foundationSiteSoilLoadBearing3000Quantity;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double FoundationSiteSoilLoadBearing3000Quantity
        {
            get { return _foundationSiteSoilLoadBearing3000Quantity; }
            set { ChangeAndNotify(ref _foundationSiteSoilLoadBearing3000Quantity, value, nameof(FoundationSiteSoilLoadBearing3000Quantity), CalcPropertiesStringArray); }
        }

        private double _foundationSiteSoilLoadBearing4500Quantity;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double FoundationSiteSoilLoadBearing4500Quantity
        {
            get { return _foundationSiteSoilLoadBearing4500Quantity; }
            set { ChangeAndNotify(ref _foundationSiteSoilLoadBearing4500Quantity, value, nameof(FoundationSiteSoilLoadBearing4500Quantity), CalcPropertiesStringArray); }
        }

        private double _foundationSiteSoilLoadBearing1500Multiplier;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double FoundationSiteSoilLoadBearing1500Multiplier
        {
            get { return _foundationSiteSoilLoadBearing1500Multiplier; }
            set { ChangeAndNotify(ref _foundationSiteSoilLoadBearing1500Multiplier, value, nameof(FoundationSiteSoilLoadBearing1500Multiplier), CalcPropertiesStringArray); }
        }

        private double _foundationSiteSoilLoadBearing3000Multiplier;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double FoundationSiteSoilLoadBearing3000Multiplier
        {
            get { return _foundationSiteSoilLoadBearing3000Multiplier; }
            set { ChangeAndNotify(ref _foundationSiteSoilLoadBearing3000Multiplier, value, nameof(FoundationSiteSoilLoadBearing3000Multiplier), CalcPropertiesStringArray); }
        }

        private double _foundationSiteSoilLoadBearing4500Multiplier;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double FoundationSiteSoilLoadBearing4500Multiplier
        {
            get { return _foundationSiteSoilLoadBearing4500Multiplier; }
            set { ChangeAndNotify(ref _foundationSiteSoilLoadBearing4500Multiplier, value, nameof(FoundationSiteSoilLoadBearing4500Multiplier), CalcPropertiesStringArray); }
        }

        private string _foundationSiteSoilLoadBearing1500Rating;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string FoundationSiteSoilLoadBearing1500Rating
        {
            get { return _foundationSiteSoilLoadBearing1500Rating; }
            set { ChangeAndNotify(ref _foundationSiteSoilLoadBearing1500Rating, value, nameof(FoundationSiteSoilLoadBearing1500Rating), CalcPropertiesStringArray); }
        }

        private string _foundationSiteSoilLoadBearing3000Rating;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string FoundationSiteSoilLoadBearing3000Rating
        {
            get { return _foundationSiteSoilLoadBearing3000Rating; }
            set { ChangeAndNotify(ref _foundationSiteSoilLoadBearing3000Rating, value, nameof(FoundationSiteSoilLoadBearing3000Rating), CalcPropertiesStringArray); }
        }

        private string _foundationSiteSoilLoadBearing4500Rating;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string FoundationSiteSoilLoadBearing4500Rating
        {
            get { return _foundationSiteSoilLoadBearing4500Rating; }
            set { ChangeAndNotify(ref _foundationSiteSoilLoadBearing4500Rating, value, nameof(FoundationSiteSoilLoadBearing4500Rating), CalcPropertiesStringArray); }
        }

        private string _foundationSiteSoilLoadBearingName;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string FoundationSiteSoilLoadBearingName
        {
            get { return _foundationSiteSoilLoadBearingName; }
            set { ChangeAndNotify(ref _foundationSiteSoilLoadBearingName, value, nameof(FoundationSiteSoilLoadBearingName), CalcPropertiesStringArray); }
        }

        private double _foundationSiteSoilLoadBearingQuantity;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double FoundationSiteSoilLoadBearingQuantity
        {
            get { return _foundationSiteSoilLoadBearingQuantity; }
            set { ChangeAndNotify(ref _foundationSiteSoilLoadBearingQuantity, value, nameof(FoundationSiteSoilLoadBearingQuantity), CalcPropertiesStringArray); }
        }

        private double _foundationSiteSoilLoadBearingMultiplier;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double FoundationSiteSoilLoadBearingMultiplier
        {
            get { return _foundationSiteSoilLoadBearingMultiplier; }
            set { ChangeAndNotify(ref _foundationSiteSoilLoadBearingMultiplier, value, nameof(FoundationSiteSoilLoadBearingMultiplier), CalcPropertiesStringArray); }
        }

        private string _foundationSiteSoilLoadBearingRating;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string FoundationSiteSoilLoadBearingRating
        {
            get { return _foundationSiteSoilLoadBearingRating; }
            set { ChangeAndNotify(ref _foundationSiteSoilLoadBearingRating, value, nameof(FoundationSiteSoilLoadBearingRating), CalcPropertiesStringArray); }
        }

        #endregion

        #region Sludge Disposal Pipeline / Piping
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

        private string _nominalPipeOutsideDiameterInchesName15;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string NominalPipeOutsideDiameterInchesName15
        {
            get { return _nominalPipeOutsideDiameterInchesName15; }
            set { ChangeAndNotify(ref _nominalPipeOutsideDiameterInchesName15, value, nameof(NominalPipeOutsideDiameterInchesName15), CalcPropertiesStringArray); }
        }

        private string _nominalPipeOutsideDiameterInchesName2;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string NominalPipeOutsideDiameterInchesName2
        {
            get { return _nominalPipeOutsideDiameterInchesName2; }
            set { ChangeAndNotify(ref _nominalPipeOutsideDiameterInchesName2, value, nameof(NominalPipeOutsideDiameterInchesName2), CalcPropertiesStringArray); }
        }

        private string _nominalPipeOutsideDiameterInchesName3;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string NominalPipeOutsideDiameterInchesName3
        {
            get { return _nominalPipeOutsideDiameterInchesName3; }
            set { ChangeAndNotify(ref _nominalPipeOutsideDiameterInchesName3, value, nameof(NominalPipeOutsideDiameterInchesName3), CalcPropertiesStringArray); }
        }

        private string _nominalPipeOutsideDiameterInchesName4;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string NominalPipeOutsideDiameterInchesName4
        {
            get { return _nominalPipeOutsideDiameterInchesName4; }
            set { ChangeAndNotify(ref _nominalPipeOutsideDiameterInchesName4, value, nameof(NominalPipeOutsideDiameterInchesName4), CalcPropertiesStringArray); }
        }

        private string _nominalPipeOutsideDiameterInchesName6;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string NominalPipeOutsideDiameterInchesName6
        {
            get { return _nominalPipeOutsideDiameterInchesName6; }
            set { ChangeAndNotify(ref _nominalPipeOutsideDiameterInchesName6, value, nameof(NominalPipeOutsideDiameterInchesName6), CalcPropertiesStringArray); }
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

        private double _nominalPipeOutsideDiameterInches15;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double NominalPipeOutsideDiameterInches15
        {
            get { return _nominalPipeOutsideDiameterInches15; }
            set { ChangeAndNotify(ref _nominalPipeOutsideDiameterInches15, value, nameof(NominalPipeOutsideDiameterInches15), CalcPropertiesStringArray); }
        }

        private double _nominalPipeOutsideDiameterInches2;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double NominalPipeOutsideDiameterInches2
        {
            get { return _nominalPipeOutsideDiameterInches2; }
            set { ChangeAndNotify(ref _nominalPipeOutsideDiameterInches2, value, nameof(NominalPipeOutsideDiameterInches2), CalcPropertiesStringArray); }
        }

        private double _nominalPipeOutsideDiameterInches3;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double NominalPipeOutsideDiameterInches3
        {
            get { return _nominalPipeOutsideDiameterInches3; }
            set { ChangeAndNotify(ref _nominalPipeOutsideDiameterInches3, value, nameof(NominalPipeOutsideDiameterInches3), CalcPropertiesStringArray); }
        }

        private double _nominalPipeOutsideDiameterInches4;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double NominalPipeOutsideDiameterInches4
        {
            get { return _nominalPipeOutsideDiameterInches4; }
            set { ChangeAndNotify(ref _nominalPipeOutsideDiameterInches4, value, nameof(NominalPipeOutsideDiameterInches4), CalcPropertiesStringArray); }
        }

        private double _nominalPipeOutsideDiameterInches6;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double NominalPipeOutsideDiameterInches6
        {
            get { return _nominalPipeOutsideDiameterInches6; }
            set { ChangeAndNotify(ref _nominalPipeOutsideDiameterInches6, value, nameof(NominalPipeOutsideDiameterInches6), CalcPropertiesStringArray); }
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

        private double _pipelineLength;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PipelineLength
        {
            get { return _pipelineLength; }
            set { ChangeAndNotify(ref _pipelineLength, value, nameof(PipelineLength), CalcPropertiesStringArray); }
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

        private double _pipeBeddingAggregateThickness;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PipeBeddingAggregateThickness
        {
            get { return _pipeBeddingAggregateThickness; }
            set { ChangeAndNotify(ref _pipeBeddingAggregateThickness, value, nameof(PipeBeddingAggregateThickness), CalcPropertiesStringArray); }
        }

        private decimal _aggregateUnitCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal AggregateUnitCost
        {
            get { return _aggregateUnitCost; }
            set { ChangeAndNotify(ref _aggregateUnitCost, value, nameof(AggregateUnitCost), CalcPropertiesStringArray); }
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

        private bool _isPigLaunchersReceiversAssemblies;
        /// <summary>
        ///  User specified 
        /// </summary>
        public bool IsPigLaunchersReceiversAssemblies
        {
            get { return _isPigLaunchersReceiversAssemblies; }
            set { ChangeAndNotify(ref _isPigLaunchersReceiversAssemblies, value, nameof(IsPigLaunchersReceiversAssemblies), CalcPropertiesStringArray); }
        }

        private double _pigLaunchersReceiversAssembliesQuantity;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PigLaunchersReceiversAssembliesQuantity
        {
            get { return _pigLaunchersReceiversAssembliesQuantity; }
            set { ChangeAndNotify(ref _pigLaunchersReceiversAssembliesQuantity, value, nameof(PigLaunchersReceiversAssembliesQuantity), CalcPropertiesStringArray); }
        }

        private decimal _pigLaunchersReceiversAssembliesUnitCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal PigLaunchersReceiversAssembliesUnitCost
        {
            get { return _pigLaunchersReceiversAssembliesUnitCost; }
            set { ChangeAndNotify(ref _pigLaunchersReceiversAssembliesUnitCost, value, nameof(PigLaunchersReceiversAssembliesUnitCost), CalcPropertiesStringArray); }
        }

        private decimal _pipelineMaintenanceFactor;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal PipelineMaintenanceFactor
        {
            get { return _pipelineMaintenanceFactor; }
            set { ChangeAndNotify(ref _pipelineMaintenanceFactor, value, nameof(PipelineMaintenanceFactor), CalcPropertiesStringArray); }
        }

        private decimal _pumpMaintenanceFactor;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal PumpMaintenanceFactor
        {
            get { return _pumpMaintenanceFactor; }
            set { ChangeAndNotify(ref _pumpMaintenanceFactor, value, nameof(PumpMaintenanceFactor), CalcPropertiesStringArray); }
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

        private double _pipeInsideDiameterInchesSDR7NOD15;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PipeInsideDiameterInchesSDR7NOD15
        {
            get { return _pipeInsideDiameterInchesSDR7NOD15; }
            set { ChangeAndNotify(ref _pipeInsideDiameterInchesSDR7NOD15, value, nameof(PipeInsideDiameterInchesSDR7NOD15), CalcPropertiesStringArray); }
        }

        private double _pipeInsideDiameterInchesSDR9NOD15;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PipeInsideDiameterInchesSDR9NOD15
        {
            get { return _pipeInsideDiameterInchesSDR9NOD15; }
            set { ChangeAndNotify(ref _pipeInsideDiameterInchesSDR9NOD15, value, nameof(PipeInsideDiameterInchesSDR9NOD15), CalcPropertiesStringArray); }
        }

        private double _pipeInsideDiameterInchesSDR11NOD15;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PipeInsideDiameterInchesSDR11NOD15
        {
            get { return _pipeInsideDiameterInchesSDR11NOD15; }
            set { ChangeAndNotify(ref _pipeInsideDiameterInchesSDR11NOD15, value, nameof(PipeInsideDiameterInchesSDR11NOD15), CalcPropertiesStringArray); }
        }

        private double _pipeInsideDiameterInchesSDR135NOD15;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PipeInsideDiameterInchesSDR135NOD15
        {
            get { return _pipeInsideDiameterInchesSDR135NOD15; }
            set { ChangeAndNotify(ref _pipeInsideDiameterInchesSDR135NOD15, value, nameof(PipeInsideDiameterInchesSDR135NOD15), CalcPropertiesStringArray); }
        }

        private double _pipeInsideDiameterInchesSDR17NOD15;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PipeInsideDiameterInchesSDR17NOD15
        {
            get { return _pipeInsideDiameterInchesSDR17NOD15; }
            set { ChangeAndNotify(ref _pipeInsideDiameterInchesSDR17NOD15, value, nameof(PipeInsideDiameterInchesSDR17NOD15), CalcPropertiesStringArray); }
        }

        private double _pipeInsideDiameterInchesSDR7NOD2;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PipeInsideDiameterInchesSDR7NOD2
        {
            get { return _pipeInsideDiameterInchesSDR7NOD2; }
            set { ChangeAndNotify(ref _pipeInsideDiameterInchesSDR7NOD2, value, nameof(PipeInsideDiameterInchesSDR7NOD2), CalcPropertiesStringArray); }
        }

        private double _pipeInsideDiameterInchesSDR9NOD2;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PipeInsideDiameterInchesSDR9NOD2
        {
            get { return _pipeInsideDiameterInchesSDR9NOD2; }
            set { ChangeAndNotify(ref _pipeInsideDiameterInchesSDR9NOD2, value, nameof(PipeInsideDiameterInchesSDR9NOD2), CalcPropertiesStringArray); }
        }

        private double _pipeInsideDiameterInchesSDR11NOD2;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PipeInsideDiameterInchesSDR11NOD2
        {
            get { return _pipeInsideDiameterInchesSDR11NOD2; }
            set { ChangeAndNotify(ref _pipeInsideDiameterInchesSDR11NOD2, value, nameof(PipeInsideDiameterInchesSDR11NOD2), CalcPropertiesStringArray); }
        }

        private double _pipeInsideDiameterInchesSDR135NOD2;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PipeInsideDiameterInchesSDR135NOD2
        {
            get { return _pipeInsideDiameterInchesSDR135NOD2; }
            set { ChangeAndNotify(ref _pipeInsideDiameterInchesSDR135NOD2, value, nameof(PipeInsideDiameterInchesSDR135NOD2), CalcPropertiesStringArray); }
        }

        private double _pipeInsideDiameterInchesSDR17NOD2;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PipeInsideDiameterInchesSDR17NOD2
        {
            get { return _pipeInsideDiameterInchesSDR17NOD2; }
            set { ChangeAndNotify(ref _pipeInsideDiameterInchesSDR17NOD2, value, nameof(PipeInsideDiameterInchesSDR17NOD2), CalcPropertiesStringArray); }
        }

        private double _pipeInsideDiameterInchesSDR7NOD3;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PipeInsideDiameterInchesSDR7NOD3
        {
            get { return _pipeInsideDiameterInchesSDR7NOD3; }
            set { ChangeAndNotify(ref _pipeInsideDiameterInchesSDR7NOD3, value, nameof(PipeInsideDiameterInchesSDR7NOD3), CalcPropertiesStringArray); }
        }

        private double _pipeInsideDiameterInchesSDR9NOD3;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PipeInsideDiameterInchesSDR9NOD3
        {
            get { return _pipeInsideDiameterInchesSDR9NOD3; }
            set { ChangeAndNotify(ref _pipeInsideDiameterInchesSDR9NOD3, value, nameof(PipeInsideDiameterInchesSDR9NOD3), CalcPropertiesStringArray); }
        }

        private double _pipeInsideDiameterInchesSDR11NOD3;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PipeInsideDiameterInchesSDR11NOD3
        {
            get { return _pipeInsideDiameterInchesSDR11NOD3; }
            set { ChangeAndNotify(ref _pipeInsideDiameterInchesSDR11NOD3, value, nameof(PipeInsideDiameterInchesSDR11NOD3), CalcPropertiesStringArray); }
        }

        private double _pipeInsideDiameterInchesSDR135NOD3;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PipeInsideDiameterInchesSDR135NOD3
        {
            get { return _pipeInsideDiameterInchesSDR135NOD3; }
            set { ChangeAndNotify(ref _pipeInsideDiameterInchesSDR135NOD3, value, nameof(PipeInsideDiameterInchesSDR135NOD3), CalcPropertiesStringArray); }
        }

        private double _pipeInsideDiameterInchesSDR17NOD3;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PipeInsideDiameterInchesSDR17NOD3
        {
            get { return _pipeInsideDiameterInchesSDR17NOD3; }
            set { ChangeAndNotify(ref _pipeInsideDiameterInchesSDR17NOD3, value, nameof(PipeInsideDiameterInchesSDR17NOD3), CalcPropertiesStringArray); }
        }

        private double _pipeInsideDiameterInchesSDR7NOD4;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PipeInsideDiameterInchesSDR7NOD4
        {
            get { return _pipeInsideDiameterInchesSDR7NOD4; }
            set { ChangeAndNotify(ref _pipeInsideDiameterInchesSDR7NOD4, value, nameof(PipeInsideDiameterInchesSDR7NOD4), CalcPropertiesStringArray); }
        }

        private double _pipeInsideDiameterInchesSDR9NOD4;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PipeInsideDiameterInchesSDR9NOD4
        {
            get { return _pipeInsideDiameterInchesSDR9NOD4; }
            set { ChangeAndNotify(ref _pipeInsideDiameterInchesSDR9NOD4, value, nameof(PipeInsideDiameterInchesSDR9NOD4), CalcPropertiesStringArray); }
        }

        private double _pipeInsideDiameterInchesSDR11NOD4;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PipeInsideDiameterInchesSDR11NOD4
        {
            get { return _pipeInsideDiameterInchesSDR11NOD4; }
            set { ChangeAndNotify(ref _pipeInsideDiameterInchesSDR11NOD4, value, nameof(PipeInsideDiameterInchesSDR11NOD4), CalcPropertiesStringArray); }
        }

        private double _pipeInsideDiameterInchesSDR135NOD4;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PipeInsideDiameterInchesSDR135NOD4
        {
            get { return _pipeInsideDiameterInchesSDR135NOD4; }
            set { ChangeAndNotify(ref _pipeInsideDiameterInchesSDR135NOD4, value, nameof(PipeInsideDiameterInchesSDR135NOD4), CalcPropertiesStringArray); }
        }

        private double _pipeInsideDiameterInchesSDR17NOD4;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PipeInsideDiameterInchesSDR17NOD4
        {
            get { return _pipeInsideDiameterInchesSDR17NOD4; }
            set { ChangeAndNotify(ref _pipeInsideDiameterInchesSDR17NOD4, value, nameof(PipeInsideDiameterInchesSDR17NOD4), CalcPropertiesStringArray); }
        }

        private double _pipeInsideDiameterInchesSDR7NOD6;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PipeInsideDiameterInchesSDR7NOD6
        {
            get { return _pipeInsideDiameterInchesSDR7NOD6; }
            set { ChangeAndNotify(ref _pipeInsideDiameterInchesSDR7NOD6, value, nameof(PipeInsideDiameterInchesSDR7NOD6), CalcPropertiesStringArray); }
        }

        private double _pipeInsideDiameterInchesSDR9NOD6;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PipeInsideDiameterInchesSDR9NOD6
        {
            get { return _pipeInsideDiameterInchesSDR9NOD6; }
            set { ChangeAndNotify(ref _pipeInsideDiameterInchesSDR9NOD6, value, nameof(PipeInsideDiameterInchesSDR9NOD6), CalcPropertiesStringArray); }
        }

        private double _pipeInsideDiameterInchesSDR11NOD6;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PipeInsideDiameterInchesSDR11NOD6
        {
            get { return _pipeInsideDiameterInchesSDR11NOD6; }
            set { ChangeAndNotify(ref _pipeInsideDiameterInchesSDR11NOD6, value, nameof(PipeInsideDiameterInchesSDR11NOD6), CalcPropertiesStringArray); }
        }

        private double _pipeInsideDiameterInchesSDR135NOD6;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PipeInsideDiameterInchesSDR135NOD6
        {
            get { return _pipeInsideDiameterInchesSDR135NOD6; }
            set { ChangeAndNotify(ref _pipeInsideDiameterInchesSDR135NOD6, value, nameof(PipeInsideDiameterInchesSDR135NOD6), CalcPropertiesStringArray); }
        }

        private double _pipeInsideDiameterInchesSDR17NOD6;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PipeInsideDiameterInchesSDR17NOD6
        {
            get { return _pipeInsideDiameterInchesSDR17NOD6; }
            set { ChangeAndNotify(ref _pipeInsideDiameterInchesSDR17NOD6, value, nameof(PipeInsideDiameterInchesSDR17NOD6), CalcPropertiesStringArray); }
        }

        private decimal _pipeFusionCostNOD15;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal PipeFusionCostNOD15
        {
            get { return _pipeFusionCostNOD15; }
            set { ChangeAndNotify(ref _pipeFusionCostNOD15, value, nameof(PipeFusionCostNOD15), CalcPropertiesStringArray); }
        }

        private decimal _pipeFusionCostNOD2;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal PipeFusionCostNOD2
        {
            get { return _pipeFusionCostNOD2; }
            set { ChangeAndNotify(ref _pipeFusionCostNOD2, value, nameof(PipeFusionCostNOD2), CalcPropertiesStringArray); }
        }

        private decimal _pipeFusionCostNOD3;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal PipeFusionCostNOD3
        {
            get { return _pipeFusionCostNOD3; }
            set { ChangeAndNotify(ref _pipeFusionCostNOD3, value, nameof(PipeFusionCostNOD3), CalcPropertiesStringArray); }
        }

        private decimal _pipeFusionCostNOD4;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal PipeFusionCostNOD4
        {
            get { return _pipeFusionCostNOD4; }
            set { ChangeAndNotify(ref _pipeFusionCostNOD4, value, nameof(PipeFusionCostNOD4), CalcPropertiesStringArray); }
        }

        private decimal _pipeFusionCostNOD6;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal PipeFusionCostNOD6
        {
            get { return _pipeFusionCostNOD6; }
            set { ChangeAndNotify(ref _pipeFusionCostNOD6, value, nameof(PipeFusionCostNOD6), CalcPropertiesStringArray); }
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

        #region Annual Cost Input

        private decimal _electricUnitCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal ElectricUnitCost
        {
            get { return _electricUnitCost; }
            set { ChangeAndNotify(ref _electricUnitCost, value, nameof(ElectricUnitCost), CalcPropertiesStringArray); }
        }

        private double _sludgeSolidsPercentage;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double SludgeSolidsPercentage
        {
            get { return _sludgeSolidsPercentage; }
            set { ChangeAndNotify(ref _sludgeSolidsPercentage, value, nameof(SludgeSolidsPercentage), CalcPropertiesStringArray); }
        }

        private double _calcite;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double Calcite
        {
            get { return _calcite; }
            set { ChangeAndNotify(ref _calcite, value, nameof(Calcite), CalcPropertiesStringArray); }
        }

        private double _miscellaneousSolids;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double MiscellaneousSolids
        {
            get { return _miscellaneousSolids; }
            set { ChangeAndNotify(ref _miscellaneousSolids, value, nameof(MiscellaneousSolids), CalcPropertiesStringArray); }
        }

        private double _miscellaneousSolidsDensity;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double MiscellaneousSolidsDensity
        {
            get { return _miscellaneousSolidsDensity; }
            set { ChangeAndNotify(ref _miscellaneousSolidsDensity, value, nameof(MiscellaneousSolidsDensity), CalcPropertiesStringArray); }
        }

        /// <summary>
        ///  Radio button binding
        /// </summary>
        public enum RadioButtonsSludgeDisposalOptionsEnum
        {
            OptionBorehole,
            OptionGeotube,
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

        private bool _isBorehole1;
        /// <summary>
        /// User specified
        /// </summary>
        public bool IsBorehole1
        {
            get { return _isBorehole1; }
            set { ChangeAndNotify(ref _isBorehole1, value, nameof(IsBorehole1), CalcPropertiesStringArray); }
        }

        private List<Borehole> _borehole1InsideDiameters;
        /// <summary>
        /// Collection 
        /// </summary>
        public List<Borehole> Borehole1InsideDiameters
        {
            get { return _borehole1InsideDiameters; }

            set { ChangeAndNotify(ref _borehole1InsideDiameters, value, nameof(Borehole1InsideDiameters), CalcPropertiesStringArray); }
        }

        private string _borehole1InsideDiameterName;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string Borehole1InsideDiameterName
        {
            get { return _borehole1InsideDiameterName; }
            set { ChangeAndNotify(ref _borehole1InsideDiameterName, value, nameof(Borehole1InsideDiameterName), CalcPropertiesStringArray); }
        }

        private string _borehole1InsideDiameterName6;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string Borehole1InsideDiameterName6
        {
            get { return _borehole1InsideDiameterName6; }
            set { ChangeAndNotify(ref _borehole1InsideDiameterName6, value, nameof(Borehole1InsideDiameterName6), CalcPropertiesStringArray); }
        }

        private string _borehole1InsideDiameterName8;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string Borehole1InsideDiameterName8
        {
            get { return _borehole1InsideDiameterName8; }
            set { ChangeAndNotify(ref _borehole1InsideDiameterName8, value, nameof(Borehole1InsideDiameterName8), CalcPropertiesStringArray); }
        }

        private double _borehole1InsideDiameter;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double Borehole1InsideDiameter
        {
            get { return _borehole1InsideDiameter; }
            set { ChangeAndNotify(ref _borehole1InsideDiameter, value, nameof(Borehole1InsideDiameter), CalcPropertiesStringArray); }
        }

        private double _borehole1InsideDiameter6;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double Borehole1InsideDiameter6
        {
            get { return _borehole1InsideDiameter6; }
            set { ChangeAndNotify(ref _borehole1InsideDiameter6, value, nameof(Borehole1InsideDiameter6), CalcPropertiesStringArray); }
        }

        private double _borehole1InsideDiameter8;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double Borehole1InsideDiameter8
        {
            get { return _borehole1InsideDiameter8; }
            set { ChangeAndNotify(ref _borehole1InsideDiameter8, value, nameof(Borehole1InsideDiameter8), CalcPropertiesStringArray); }
        }

        private double _borehole1SizingMultiplier;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double Borehole1SizingMultiplier
        {
            get { return _borehole1SizingMultiplier; }
            set { ChangeAndNotify(ref _borehole1SizingMultiplier, value, nameof(Borehole1SizingMultiplier), CalcPropertiesStringArray); }
        }

        private double _borehole1DrillingDepth;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double Borehole1DrillingDepth
        {
            get { return _borehole1DrillingDepth; }
            set { ChangeAndNotify(ref _borehole1DrillingDepth, value, nameof(Borehole1DrillingDepth), CalcPropertiesStringArray); }
        }

        private decimal _borehole1DrillingAndCastingInstallationUnitCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal Borehole1DrillingAndCastingInstallationUnitCost
        {
            get { return _borehole1DrillingAndCastingInstallationUnitCost; }
            set { ChangeAndNotify(ref _borehole1DrillingAndCastingInstallationUnitCost, value, nameof(Borehole1DrillingAndCastingInstallationUnitCost), CalcPropertiesStringArray); }
        }

        private decimal _borehole1DrillingAndCastingInstallationUnitCost6;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal Borehole1DrillingAndCastingInstallationUnitCost6
        {
            get { return _borehole1DrillingAndCastingInstallationUnitCost6; }
            set { ChangeAndNotify(ref _borehole1DrillingAndCastingInstallationUnitCost6, value, nameof(Borehole1DrillingAndCastingInstallationUnitCost6), CalcPropertiesStringArray); }
        }

        private decimal _borehole1DrillingAndCastingInstallationUnitCost8;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal Borehole1DrillingAndCastingInstallationUnitCost8
        {
            get { return _borehole1DrillingAndCastingInstallationUnitCost8; }
            set { ChangeAndNotify(ref _borehole1DrillingAndCastingInstallationUnitCost8, value, nameof(Borehole1DrillingAndCastingInstallationUnitCost8), CalcPropertiesStringArray); }
        }

        private decimal _borehole1Cost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal Borehole1Cost
        {
            get { return _borehole1Cost; }
            set { ChangeAndNotify(ref _borehole1Cost, value, nameof(Borehole1Cost), CalcPropertiesStringArray); }
        }

        private bool _isBorehole2;
        /// <summary>
        /// User specified
        /// </summary>
        public bool IsBorehole2
        {
            get { return _isBorehole2; }
            set { ChangeAndNotify(ref _isBorehole2, value, nameof(IsBorehole2), CalcPropertiesStringArray); }
        }

        private List<Borehole> _borehole2InsideDiameters;
        /// <summary>
        /// Collection 
        /// </summary>
        public List<Borehole> Borehole2InsideDiameters
        {
            get { return _borehole2InsideDiameters; }

            set { ChangeAndNotify(ref _borehole2InsideDiameters, value, nameof(Borehole2InsideDiameters), CalcPropertiesStringArray); }
        }

        private string _borehole2InsideDiameterName;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string Borehole2InsideDiameterName
        {
            get { return _borehole2InsideDiameterName; }
            set { ChangeAndNotify(ref _borehole2InsideDiameterName, value, nameof(Borehole2InsideDiameterName), CalcPropertiesStringArray); }
        }

        private string _borehole2InsideDiameterName6;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string Borehole2InsideDiameterName6
        {
            get { return _borehole2InsideDiameterName6; }
            set { ChangeAndNotify(ref _borehole2InsideDiameterName6, value, nameof(Borehole2InsideDiameterName6), CalcPropertiesStringArray); }
        }

        private string _borehole2InsideDiameterName8;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string Borehole2InsideDiameterName8
        {
            get { return _borehole2InsideDiameterName8; }
            set { ChangeAndNotify(ref _borehole2InsideDiameterName8, value, nameof(Borehole2InsideDiameterName8), CalcPropertiesStringArray); }
        }

        private double _borehole2InsideDiameter;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double Borehole2InsideDiameter
        {
            get { return _borehole2InsideDiameter; }
            set { ChangeAndNotify(ref _borehole2InsideDiameter, value, nameof(Borehole2InsideDiameter), CalcPropertiesStringArray); }
        }

        private double _borehole2InsideDiameter6;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double Borehole2InsideDiameter6
        {
            get { return _borehole2InsideDiameter6; }
            set { ChangeAndNotify(ref _borehole2InsideDiameter6, value, nameof(Borehole2InsideDiameter6), CalcPropertiesStringArray); }
        }

        private double _borehole2InsideDiameter8;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double Borehole2InsideDiameter8
        {
            get { return _borehole2InsideDiameter8; }
            set { ChangeAndNotify(ref _borehole2InsideDiameter8, value, nameof(Borehole2InsideDiameter8), CalcPropertiesStringArray); }
        }

        private double _borehole2SizingMultiplier;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double Borehole2SizingMultiplier
        {
            get { return _borehole2SizingMultiplier; }
            set { ChangeAndNotify(ref _borehole2SizingMultiplier, value, nameof(Borehole2SizingMultiplier), CalcPropertiesStringArray); }
        }

        private double _borehole2DrillingDepth;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double Borehole2DrillingDepth
        {
            get { return _borehole2DrillingDepth; }
            set { ChangeAndNotify(ref _borehole2DrillingDepth, value, nameof(Borehole2DrillingDepth), CalcPropertiesStringArray); }
        }

        private decimal _borehole2DrillingAndCastingInstallationUnitCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal Borehole2DrillingAndCastingInstallationUnitCost
        {
            get { return _borehole2DrillingAndCastingInstallationUnitCost; }
            set { ChangeAndNotify(ref _borehole2DrillingAndCastingInstallationUnitCost, value, nameof(Borehole2DrillingAndCastingInstallationUnitCost), CalcPropertiesStringArray); }
        }

        private decimal _borehole2DrillingAndCastingInstallationUnitCost6;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal Borehole2DrillingAndCastingInstallationUnitCost6
        {
            get { return _borehole2DrillingAndCastingInstallationUnitCost6; }
            set { ChangeAndNotify(ref _borehole2DrillingAndCastingInstallationUnitCost6, value, nameof(Borehole2DrillingAndCastingInstallationUnitCost6), CalcPropertiesStringArray); }
        }

        private decimal _borehole2DrillingAndCastingInstallationUnitCost8;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal Borehole2DrillingAndCastingInstallationUnitCost8
        {
            get { return _borehole2DrillingAndCastingInstallationUnitCost8; }
            set { ChangeAndNotify(ref _borehole2DrillingAndCastingInstallationUnitCost8, value, nameof(Borehole2DrillingAndCastingInstallationUnitCost8), CalcPropertiesStringArray); }
        }

        private decimal _borehole2Cost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal Borehole2Cost
        {
            get { return _borehole2Cost; }
            set { ChangeAndNotify(ref _borehole2Cost, value, nameof(Borehole2Cost), CalcPropertiesStringArray); }
        }

        private List<Borehole> _borehole3InsideDiameters;
        /// <summary>
        /// Collection 
        /// </summary>
        public List<Borehole> Borehole3InsideDiameters
        {
            get { return _borehole3InsideDiameters; }
            set { ChangeAndNotify(ref _borehole3InsideDiameters, value, nameof(Borehole3InsideDiameters), CalcPropertiesStringArray); }
        }

        private bool _isBorehole3;
        /// <summary>
        /// User specified
        /// </summary>
        public bool IsBorehole3
        {
            get { return _isBorehole3; }
            set { ChangeAndNotify(ref _isBorehole3, value, nameof(IsBorehole3), CalcPropertiesStringArray); }
        }

        private string _borehole3InsideDiameterName;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string Borehole3InsideDiameterName
        {
            get { return _borehole3InsideDiameterName; }
            set { ChangeAndNotify(ref _borehole3InsideDiameterName, value, nameof(Borehole3InsideDiameterName), CalcPropertiesStringArray); }
        }

        private string _borehole3InsideDiameterName6;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string Borehole3InsideDiameterName6
        {
            get { return _borehole3InsideDiameterName6; }
            set { ChangeAndNotify(ref _borehole3InsideDiameterName6, value, nameof(Borehole3InsideDiameterName6), CalcPropertiesStringArray); }
        }

        private string _borehole3InsideDiameterName8;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string Borehole3InsideDiameterName8
        {
            get { return _borehole3InsideDiameterName8; }
            set { ChangeAndNotify(ref _borehole3InsideDiameterName8, value, nameof(Borehole3InsideDiameterName8), CalcPropertiesStringArray); }
        }

        private double _borehole3InsideDiameter;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double Borehole3InsideDiameter
        {
            get { return _borehole3InsideDiameter; }
            set { ChangeAndNotify(ref _borehole3InsideDiameter, value, nameof(Borehole3InsideDiameter), CalcPropertiesStringArray); }
        }

        private double _borehole3InsideDiameter6;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double Borehole3InsideDiameter6
        {
            get { return _borehole3InsideDiameter6; }
            set { ChangeAndNotify(ref _borehole3InsideDiameter6, value, nameof(Borehole3InsideDiameter6), CalcPropertiesStringArray); }
        }

        private double _borehole3InsideDiameter8;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double Borehole3InsideDiameter8
        {
            get { return _borehole3InsideDiameter8; }
            set { ChangeAndNotify(ref _borehole3InsideDiameter8, value, nameof(Borehole3InsideDiameter8), CalcPropertiesStringArray); }
        }

        private double _borehole3SizingMultiplier;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double Borehole3SizingMultiplier
        {
            get { return _borehole3SizingMultiplier; }
            set { ChangeAndNotify(ref _borehole3SizingMultiplier, value, nameof(Borehole3SizingMultiplier), CalcPropertiesStringArray); }
        }

        private double _borehole3DrillingDepth;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double Borehole3DrillingDepth
        {
            get { return _borehole3DrillingDepth; }
            set { ChangeAndNotify(ref _borehole3DrillingDepth, value, nameof(Borehole3DrillingDepth), CalcPropertiesStringArray); }
        }

        private decimal _borehole3DrillingAndCastingInstallationUnitCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal Borehole3DrillingAndCastingInstallationUnitCost
        {
            get { return _borehole3DrillingAndCastingInstallationUnitCost; }
            set { ChangeAndNotify(ref _borehole3DrillingAndCastingInstallationUnitCost, value, nameof(Borehole3DrillingAndCastingInstallationUnitCost), CalcPropertiesStringArray); }
        }

        private decimal _borehole3DrillingAndCastingInstallationUnitCost6;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal Borehole3DrillingAndCastingInstallationUnitCost6
        {
            get { return _borehole3DrillingAndCastingInstallationUnitCost6; }
            set { ChangeAndNotify(ref _borehole3DrillingAndCastingInstallationUnitCost6, value, nameof(Borehole3DrillingAndCastingInstallationUnitCost6), CalcPropertiesStringArray); }
        }

        private decimal _borehole3DrillingAndCastingInstallationUnitCost8;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal Borehole3DrillingAndCastingInstallationUnitCost8
        {
            get { return _borehole3DrillingAndCastingInstallationUnitCost8; }
            set { ChangeAndNotify(ref _borehole3DrillingAndCastingInstallationUnitCost8, value, nameof(Borehole3DrillingAndCastingInstallationUnitCost8), CalcPropertiesStringArray); }
        }

        private decimal _borehole3Cost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal Borehole3Cost
        {
            get { return _borehole3Cost; }
            set { ChangeAndNotify(ref _borehole3Cost, value, nameof(Borehole3Cost), CalcPropertiesStringArray); }
        }

        private double _borehole3Quantity;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double Borehole3Quantity
        {
            get { return _borehole3Quantity; }
            set { ChangeAndNotify(ref _borehole3Quantity, value, nameof(Borehole3Quantity), CalcPropertiesStringArray); }
        }

        private double _geotubeFillCapacity;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double GeotubeFillCapacity
        {
            get { return _geotubeFillCapacity; }
            set { ChangeAndNotify(ref _geotubeFillCapacity, value, nameof(GeotubeFillCapacity), CalcPropertiesStringArray); }
        }

        private double _dewateredSolidsPercentage;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double DewateredSolidsPercentage
        {
            get { return _dewateredSolidsPercentage; }
            set { ChangeAndNotify(ref _dewateredSolidsPercentage, value, nameof(DewateredSolidsPercentage), CalcPropertiesStringArray); }
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

        private string _geotubeSizeName;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string GeotubeSizeName
        {
            get { return _geotubeSizeName; }
            set { ChangeAndNotify(ref _geotubeSizeName, value, nameof(GeotubeSizeName), CalcPropertiesStringArray); }
        }

        private string _geotubeSizeName22_5x22;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string GeotubeSizeName22_5x22
        {
            get { return _geotubeSizeName22_5x22; }
            set { ChangeAndNotify(ref _geotubeSizeName22_5x22, value, nameof(GeotubeSizeName22_5x22), CalcPropertiesStringArray); }
        }

        private string _geotubeSizeName30x50;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string GeotubeSizeName30x50
        {
            get { return _geotubeSizeName30x50; }
            set { ChangeAndNotify(ref _geotubeSizeName30x50, value, nameof(GeotubeSizeName30x50), CalcPropertiesStringArray); }
        }

        private string _geotubeSizeName45x57;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string GeotubeSizeName45x57
        {
            get { return _geotubeSizeName45x57; }
            set { ChangeAndNotify(ref _geotubeSizeName45x57, value, nameof(GeotubeSizeName45x57), CalcPropertiesStringArray); }
        }

        private string _geotubeSizeName45x100;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string GeotubeSizeName45x100
        {
            get { return _geotubeSizeName45x100; }
            set { ChangeAndNotify(ref _geotubeSizeName45x100, value, nameof(GeotubeSizeName45x100), CalcPropertiesStringArray); }
        }

        private string _geotubeSizeName60x100;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string GeotubeSizeName60x100
        {
            get { return _geotubeSizeName60x100; }
            set { ChangeAndNotify(ref _geotubeSizeName60x100, value, nameof(GeotubeSizeName60x100), CalcPropertiesStringArray); }
        }

        private string _geotubeSizeName60x200;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string GeotubeSizeName60x200
        {
            get { return _geotubeSizeName60x200; }
            set { ChangeAndNotify(ref _geotubeSizeName60x200, value, nameof(GeotubeSizeName60x200), CalcPropertiesStringArray); }
        }

        private string _geotubeSizeName120x100;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string GeotubeSizeName120x100
        {
            get { return _geotubeSizeName120x100; }
            set { ChangeAndNotify(ref _geotubeSizeName120x100, value, nameof(GeotubeSizeName120x100), CalcPropertiesStringArray); }
        }

        private double _geotubeCapacity;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double GeotubeCapacity
        {
            get { return _geotubeCapacity; }
            set { ChangeAndNotify(ref _geotubeCapacity, value, nameof(GeotubeCapacity), CalcPropertiesStringArray); }
        }

        private double _geotubeCapacity22_5x22;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double GeotubeCapacity22_5x22
        {
            get { return _geotubeCapacity22_5x22; }
            set { ChangeAndNotify(ref _geotubeCapacity22_5x22, value, nameof(GeotubeCapacity22_5x22), CalcPropertiesStringArray); }
        }

        private double _geotubeCapacity30x50;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double GeotubeCapacity30x50
        {
            get { return _geotubeCapacity30x50; }
            set { ChangeAndNotify(ref _geotubeCapacity30x50, value, nameof(GeotubeCapacity30x50), CalcPropertiesStringArray); }
        }

        private double _geotubeCapacity45x57;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double GeotubeCapacity45x57
        {
            get { return _geotubeCapacity45x57; }
            set { ChangeAndNotify(ref _geotubeCapacity45x57, value, nameof(GeotubeCapacity45x57), CalcPropertiesStringArray); }
        }

        private double _geotubeCapacity45x100;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double GeotubeCapacity45x100
        {
            get { return _geotubeCapacity45x100; }
            set { ChangeAndNotify(ref _geotubeCapacity45x100, value, nameof(GeotubeCapacity45x100), CalcPropertiesStringArray); }
        }

        private double _geotubeCapacity60x100;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double GeotubeCapacity60x100
        {
            get { return _geotubeCapacity60x100; }
            set { ChangeAndNotify(ref _geotubeCapacity60x100, value, nameof(GeotubeCapacity60x100), CalcPropertiesStringArray); }
        }

        private double _geotubeCapacity60x200;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double GeotubeCapacity60x200
        {
            get { return _geotubeCapacity60x200; }
            set { ChangeAndNotify(ref _geotubeCapacity60x200, value, nameof(GeotubeCapacity60x200), CalcPropertiesStringArray); }
        }

        private double _geotubeCapacity120x100;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double GeotubeCapacity120x100
        {
            get { return _geotubeCapacity120x100; }
            set { ChangeAndNotify(ref _geotubeCapacity120x100, value, nameof(GeotubeCapacity120x100), CalcPropertiesStringArray); }
        }

        private decimal _geotubeUnitCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal GeotubeUnitCost
        {
            get { return _geotubeUnitCost; }
            set { ChangeAndNotify(ref _geotubeUnitCost, value, nameof(GeotubeUnitCost), CalcPropertiesStringArray); }
        }

        private decimal _geotubeUnitCost22_5x22;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal GeotubeUnitCost22_5x22
        {
            get { return _geotubeUnitCost22_5x22; }
            set { ChangeAndNotify(ref _geotubeUnitCost22_5x22, value, nameof(GeotubeUnitCost22_5x22), CalcPropertiesStringArray); }
        }

        private decimal _geotubeUnitCost30x50;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal GeotubeUnitCost30x50
        {
            get { return _geotubeUnitCost30x50; }
            set { ChangeAndNotify(ref _geotubeUnitCost30x50, value, nameof(GeotubeUnitCost30x50), CalcPropertiesStringArray); }
        }

        private decimal _geotubeUnitCost45x57;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal GeotubeUnitCost45x57
        {
            get { return _geotubeUnitCost45x57; }
            set { ChangeAndNotify(ref _geotubeUnitCost45x57, value, nameof(GeotubeUnitCost45x57), CalcPropertiesStringArray); }
        }

        private decimal _geotubeUnitCost45x100;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal GeotubeUnitCost45x100
        {
            get { return _geotubeUnitCost45x100; }
            set { ChangeAndNotify(ref _geotubeUnitCost45x100, value, nameof(GeotubeUnitCost45x100), CalcPropertiesStringArray); }
        }

        private decimal _geotubeUnitCost60x100;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal GeotubeUnitCost60x100
        {
            get { return _geotubeUnitCost60x100; }
            set { ChangeAndNotify(ref _geotubeUnitCost60x100, value, nameof(GeotubeUnitCost60x100), CalcPropertiesStringArray); }
        }

        private decimal _geotubeUnitCost60x200;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal GeotubeUnitCost60x200
        {
            get { return _geotubeUnitCost60x200; }
            set { ChangeAndNotify(ref _geotubeUnitCost60x200, value, nameof(GeotubeUnitCost60x200), CalcPropertiesStringArray); }
        }

        private decimal _geotubeUnitCost120x100;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal GeotubeUnitCost120x100
        {
            get { return _geotubeUnitCost120x100; }
            set { ChangeAndNotify(ref _geotubeUnitCost120x100, value, nameof(GeotubeUnitCost120x100), CalcPropertiesStringArray); }
        }

        private bool _isPolymer;
        /// <summary>
        ///  User specified 
        /// </summary>
        public bool IsPolymer
        {
            get { return _isPolymer; }
            set { ChangeAndNotify(ref _isPolymer, value, nameof(IsPolymer), CalcPropertiesStringArray); }
        }

        private bool _isEmulsionPolymer;
        /// <summary>
        ///  User specified 
        /// </summary>
        public bool IsEmulsionPolymer
        {
            get { return _isEmulsionPolymer; }
            set { ChangeAndNotify(ref _isEmulsionPolymer, value, nameof(IsEmulsionPolymer), CalcPropertiesStringArray); }
        }

        private List<Polymer> _polymers;
        /// <summary>
        /// Collection 
        /// </summary>
        public List<Polymer> Polymers
        {
            get { return _polymers; }
            set { ChangeAndNotify(ref _polymers, value, nameof(Polymers), CalcPropertiesStringArray); }
        }

        private string _polymerName;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string PolymerName
        {
            get { return _polymerName; }
            set { ChangeAndNotify(ref _polymerName, value, nameof(PolymerName), CalcPropertiesStringArray); }
        }

        private string _polymerNameEmulsion;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string PolymerNameEmulsion
        {
            get { return _polymerNameEmulsion; }
            set { ChangeAndNotify(ref _polymerNameEmulsion, value, nameof(PolymerNameEmulsion), CalcPropertiesStringArray); }
        }

        private string _polymerNameDry;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string PolymerNameDry
        {
            get { return _polymerNameDry; }
            set { ChangeAndNotify(ref _polymerNameDry, value, nameof(PolymerNameDry), CalcPropertiesStringArray); }
        }

        private decimal _polymerUnitCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal PolymerUnitCost
        {
            get { return _polymerUnitCost; }
            set { ChangeAndNotify(ref _polymerUnitCost, value, nameof(PolymerUnitCost), CalcPropertiesStringArray); }
        }

        private decimal _polymerUnitCostEmulsion;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal PolymerUnitCostEmulsion
        {
            get { return _polymerUnitCostEmulsion; }
            set { ChangeAndNotify(ref _polymerUnitCostEmulsion, value, nameof(PolymerUnitCostEmulsion), CalcPropertiesStringArray); }
        }

        private decimal _polymerUnitCostDry;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal PolymerUnitCostDry
        {
            get { return _polymerUnitCostDry; }
            set { ChangeAndNotify(ref _polymerUnitCostDry, value, nameof(PolymerUnitCostDry), CalcPropertiesStringArray); }
        }

        private double _emulsionPolymerDensity;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double EmulsionPolymerDensity
        {
            get { return _emulsionPolymerDensity; }
            set { ChangeAndNotify(ref _emulsionPolymerDensity, value, nameof(EmulsionPolymerDensity), CalcPropertiesStringArray); }
        }

        private double _polymerActivePercentage;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PolymerActivePercentage
        {
            get { return _polymerActivePercentage; }
            set { ChangeAndNotify(ref _polymerActivePercentage, value, nameof(PolymerActivePercentage), CalcPropertiesStringArray); }
        }

        private double _polymerActivePercentageDry;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PolymerActivePercentageDry
        {
            get { return _polymerActivePercentageDry; }
            set { ChangeAndNotify(ref _polymerActivePercentageDry, value, nameof(PolymerActivePercentageDry), CalcPropertiesStringArray); }
        }

        private double _polymerActivePercentageEmulsion;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PolymerActivePercentageEmulsion
        {
            get { return _polymerActivePercentageEmulsion; }
            set { ChangeAndNotify(ref _polymerActivePercentageEmulsion, value, nameof(PolymerActivePercentageEmulsion), CalcPropertiesStringArray); }
        }

        private double _polymerSolutionStrength;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PolymerSolutionStrength
        {
            get { return _polymerSolutionStrength; }
            set { ChangeAndNotify(ref _polymerSolutionStrength, value, nameof(PolymerSolutionStrength), CalcPropertiesStringArray); }
        }

        private double _polymerSolutionStrengthDry;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PolymerSolutionStrengthDry
        {
            get { return _polymerSolutionStrengthDry; }
            set { ChangeAndNotify(ref _polymerSolutionStrengthDry, value, nameof(PolymerSolutionStrengthDry), CalcPropertiesStringArray); }
        }

        private double _polymerSolutionStrengthEmulsion;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PolymerSolutionStrengthEmulsion
        {
            get { return _polymerSolutionStrengthEmulsion; }
            set { ChangeAndNotify(ref _polymerSolutionStrengthEmulsion, value, nameof(PolymerSolutionStrengthEmulsion), CalcPropertiesStringArray); }
        }

        private double _polymerTargetDose;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PolymerTargetDose
        {
            get { return _polymerTargetDose; }
            set { ChangeAndNotify(ref _polymerTargetDose, value, nameof(PolymerTargetDose), CalcPropertiesStringArray); }
        }

        private double _polymerPumpPower;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double PolymerPumpPower
        {
            get { return _polymerPumpPower; }
            set { ChangeAndNotify(ref _polymerPumpPower, value, nameof(PolymerPumpPower), CalcPropertiesStringArray); }
        }

        private bool _isIsLandfillTippingFee;
        /// <summary>
        ///  User specified 
        /// </summary>
        public bool IsLandfillTippingFee
        {
            get { return _isIsLandfillTippingFee; }
            set { ChangeAndNotify(ref _isIsLandfillTippingFee, value, nameof(IsLandfillTippingFee), CalcPropertiesStringArray); }
        }

        private decimal _landfillTippingFee;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal LandfillTippingFee
        {
            get { return _landfillTippingFee; }
            set { ChangeAndNotify(ref _landfillTippingFee, value, nameof(LandfillTippingFee), CalcPropertiesStringArray); }
        }

        private double _truckVolume;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double TruckVolume
        {
            get { return _truckVolume; }
            set { ChangeAndNotify(ref _truckVolume, value, nameof(TruckVolume), CalcPropertiesStringArray); }
        }

        private double _excavationHaulDisposeTime;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double ExcavationHaulDisposeTime
        {
            get { return _excavationHaulDisposeTime; }
            set { ChangeAndNotify(ref _excavationHaulDisposeTime, value, nameof(ExcavationHaulDisposeTime), CalcPropertiesStringArray); }
        }

        private decimal _excavatorUnitCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal ExcavatorUnitCost
        {
            get { return _excavatorUnitCost; }
            set { ChangeAndNotify(ref _excavatorUnitCost, value, nameof(ExcavatorUnitCost), CalcPropertiesStringArray); }
        }

        private double _roundtripDistance;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double RoundtripDistance
        {
            get { return _roundtripDistance; }
            set { ChangeAndNotify(ref _roundtripDistance, value, nameof(RoundtripDistance), CalcPropertiesStringArray); }
        }

        private decimal _haulingTransportationUnitCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal HaulingTransportationUnitCost
        {
            get { return _haulingTransportationUnitCost; }
            set { ChangeAndNotify(ref _haulingTransportationUnitCost, value, nameof(HaulingTransportationUnitCost), CalcPropertiesStringArray); }
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

        #region Properties - Sizing Summary: Clarifier Design

        private double _calcRateOfRise;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcRateOfRise
        {
            get { return ClarifierCalculations.CalcRateOfRise(ClarifierFlowPerAreaConventional); }
            set { ChangeAndNotify(ref _calcRateOfRise, value); }
        }

        private double _calcClarifierDiameter;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcClarifierDiameter
        {
            get
            {
                switch (ClarifierDesignOptionsProperty)
                {
                    case RadioButtonsClarifierDesignOptionsEnum.OptionConventional:
                        _calcClarifierDiameter = ClarifierCalculations.CalcClarifierDiameter(DesignFlow, ClarifierFlowPerAreaConventional);
                        break;
                    case RadioButtonsClarifierDesignOptionsEnum.OptionSolidsContact:
                        _calcClarifierDiameter = ClarifierCalculations.CalcClarifierDiameter(DesignFlow, ClarifierFlowPerAreaSolidsContact);
                        break;
                    default:
                        break;
                }
                return _calcClarifierDiameter;
            }
            set { ChangeAndNotify(ref _calcClarifierDiameter, value); }
        }

        private double _calcClarifierVolume;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcClarifierVolume
        {
            get { return ClarifierCalculations.CalcClarifierVolume(CalcClarifierDiameter, ClarifierWaterHeight, ClarifierFreeboard); }
            set { ChangeAndNotify(ref _calcClarifierVolume, value); }
        }

        private double _calcClarifierRetentionTime;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcClarifierRetentionTime
        {
            get { return ClarifierCalculations.CalcClarifierRetentionTime(DesignFlow, CalcClarifierDiameter, ClarifierWaterHeight); }
            set { ChangeAndNotify(ref _calcClarifierRetentionTime, value); }
        }

        private double _calcSludgeBlanketVolume;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcSludgeBlanketVolume
        {
            get { return ClarifierCalculations.CalcSludgeBlanketVolume(CalcClarifierDiameter, SludgeBlanketDepth); }
            set { ChangeAndNotify(ref _calcSludgeBlanketVolume, value); }
        }

        private double _calcSludgeBlanketRetentionTime;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcSludgeBlanketRetentionTime
        {
            get
            {
                if (CalcSludgeDisposalPumpFlowRate == 0)
                {
                    _calcSludgeBlanketRetentionTime = 0;
                }
                else
                {
                    _calcSludgeBlanketRetentionTime = ClarifierCalculations.CalcSludgeBlanketRetentionTime(CalcSludgeBlanketVolume, CalcSludgeDisposalPumpFlowRate); 
                }
                return _calcSludgeBlanketRetentionTime;
            }
            set { ChangeAndNotify(ref _calcSludgeBlanketRetentionTime, value); }
        }

        private double _calcTankProtectiveCoatingSurfaceArea;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcTankProtectiveCoatingSurfaceArea
        {
            get { return ClarifierCalculations.CalcTankProtectiveCoatingSurfaceArea(CalcClarifierDiameter, ClarifierWaterHeight, ClarifierFreeboard); }
            set { ChangeAndNotify(ref _calcTankProtectiveCoatingSurfaceArea, value); }
        }

        private double _calcFoundationArea;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcFoundationArea
        {
            get { return ClarifierCalculations.CalcFoundationArea(CalcClarifierDiameter); }
            set { ChangeAndNotify(ref _calcFoundationArea, value); }
        }

        private double _foundationAreaData;
        /// <summary>
        /// Calculated
        /// </summary>
        public double FoundationAreaData
        {
            get { return CalcFoundationArea; }
            set { ChangeAndNotify(ref _foundationAreaData, value, nameof(FoundationAreaData)); }
        }

        private double _calcFoundationDepth;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcFoundationDepth
        {
            get
            {
                if (ClarifierConstructionMaterialName == ClarifierConstructionMaterialNameConcrete)
                {
                    return ClarifierCalculations.CalcFoundationDepth(3.5);
                }
                else
                {
                    return ClarifierCalculations.CalcFoundationDepth(1.5);
                }
            }
            set { ChangeAndNotify(ref _calcFoundationDepth, value); }
        }

        private double _calcFoundationAreaTimesDepth;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcFoundationAreaTimesDepth
        {
            get
            {
                _calcFoundationAreaTimesDepth = CalcFoundationArea * CalcFoundationDepth;

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

        private double _calcFoundationVolume;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcFoundationVolume
        {
            get { return ClarifierCalculations.CalcFoundationVolume(CalcFoundationDepth, CalcFoundationArea); }
            set { ChangeAndNotify(ref _calcFoundationVolume, value); }
        }

        private double _calcImpellerMotorPower;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcImpellerMotorPower
        {
            get
            {
                switch (ClarifierDesignOptionsProperty)
                {
                    case RadioButtonsClarifierDesignOptionsEnum.OptionConventional:
                        _calcImpellerMotorPower = 0;
                        break;
                    case RadioButtonsClarifierDesignOptionsEnum.OptionSolidsContact:
                        switch (ImpellerMotorOptionsProperty)
                        {
                            case RadioButtonsImpellerMotorPowerOptionsEnum.OptionEstimate:
                                _calcImpellerMotorPower = ClarifierCalculations.CalcImpellerMotorPower(CalcClarifierDiameter);
                                break;
                            case RadioButtonsImpellerMotorPowerOptionsEnum.OptionUserSpecified:
                                _calcImpellerMotorPower = ImpellerMotorPowerUserSpecified;
                                break;
                            default:
                                break;
                        }                        
                        break;
                    default:
                        break;
                }
                return _calcImpellerMotorPower;
            }
            set { ChangeAndNotify(ref _calcImpellerMotorPower, value); }
        }

        #endregion

        #region Properties - Sizing Summary: Sludge Recirculation Pump

        private double _calcSludgeRecirculationPumpFlowRate;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcSludgeRecirculationPumpFlowRate
        {
            get
            {
                if (IsSludgeRecirculationPump)
                {
                    switch (SludgeRecirculationPumpOptionsProperty)
                    {
                        case RadioButtonsSludgeRecirculationPumpOptionsEnum.OptionEstimate:
                            _calcSludgeRecirculationPumpFlowRate = ClarifierCalculations.CalcSludgeRecirculationPumpFlowRate(DesignFlow, SludgeVolume);
                            break;
                        case RadioButtonsSludgeRecirculationPumpOptionsEnum.OptionUserSpecified:
                            _calcSludgeRecirculationPumpFlowRate = SludgeRecirculationPumpFlowUserSpecified;
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    _calcSludgeRecirculationPumpFlowRate = 0;
                }
                return _calcSludgeRecirculationPumpFlowRate;
            }
            set { ChangeAndNotify(ref _calcSludgeRecirculationPumpFlowRate, value); }
        }

        private double _calcSludgeRecirculationPumpPower;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcSludgeRecirculationPumpPower
        {
            get { return ClarifierCalculations.CalcSludgeRecirculationPumpPower(CalcSludgeRecirculationPumpFlowRate, ClarifierWaterHeight, ClarifierFreeboard, SludgeDisposalPumpEfficiency, SludgeDisposalPumpSizingSafetyFactor, CalcSludgeThickened); }
            set { ChangeAndNotify(ref _calcSludgeRecirculationPumpPower, value); }
        }

        #endregion

        #region Sizing Summary: Sludge Disposal Pump

        private double _calcSludgeDisposalPumpFlowRate;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcSludgeDisposalPumpFlowRate
        {
            get
            {
                switch (SludgeDisposalPumpOptionsProperty)
                {
                    case RadioButtonsSludgeDisposalPumpOptionsEnum.OptionEstimate:
                        _calcSludgeDisposalPumpFlowRate = ClarifierCalculations.CalcSludgeDisposalPumpFlowRate(DesignFlow, SludgeVolume);
                        break;
                    case RadioButtonsSludgeDisposalPumpOptionsEnum.OptionUserSpecified:
                        _calcSludgeDisposalPumpFlowRate = SludgeDisposalPumpFlowUserSpecified;
                        break;
                    default:
                        break;
                }
                return _calcSludgeDisposalPumpFlowRate;
            }
            set { ChangeAndNotify(ref _calcSludgeDisposalPumpFlowRate, value); }
        }

        private double _calcSludgeDisposalPumpPower;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcSludgeDisposalPumpPower
        {
            get { return ClarifierCalculations.CalcSludgeDisposalPumpPower(CalcSludgeDisposalPumpFlowRate, CalcTotalDynamicHead, SludgeDisposalPumpEfficiency, SludgeDisposalPumpSizingSafetyFactor, CalcSludgeThickened); }
            set { ChangeAndNotify(ref _calcSludgeDisposalPumpPower, value); }
        }

        private double _calcSludgeDisposalPumpTimeHoursPerDay;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcSludgeDisposalPumpTimeHoursPerDay
        {
            get
            {
                if (CalcSludgeDisposalPumpFlowRate == 0)  // to prevent from dividing by zero
                {
                    _calcSludgeDisposalPumpTimeHoursPerDay = 0;
                }
                else
                {
                    _calcSludgeDisposalPumpTimeHoursPerDay = ClarifierCalculations.CalcSludgeDisposalPumpTimeHoursPerDay(CalcSludgeProductionGallonsPerDay, CalcSludgeDisposalPumpFlowRate);
                }
                return _calcSludgeDisposalPumpTimeHoursPerDay;
            }
            set { ChangeAndNotify(ref _calcSludgeDisposalPumpTimeHoursPerDay, value); }
        }

        private double _calcAluminumConcentrationAsSolid;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcAluminumConcentrationAsSolid
        {
            get { return ClarifierCalculations.CalcAluminumConcentrationAsSolid(Aluminum); }
            set { ChangeAndNotify(ref _calcAluminumConcentrationAsSolid, value); }
        }

        private double _calcFerrousIronConcentrationAsSolid;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcFerrousIronConcentrationAsSolid
        {
            get { return ClarifierCalculations.CalcFerrousIronConcentrationAsSolid(FerrousIron); }
            set { ChangeAndNotify(ref _calcFerrousIronConcentrationAsSolid, value); }
        }

        private double _calcFerricIronConcentrationAsSolid;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcFerricIronConcentrationAsSolid
        {
            get { return ClarifierCalculations.CalcFerricIronConcentrationAsSolid(FerricIron); }
            set { ChangeAndNotify(ref _calcFerricIronConcentrationAsSolid, value); }
        }

        private double _calcManganeseConcentrationAsSolid;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcManganeseConcentrationAsSolid
        {
            get { return ClarifierCalculations.CalcManganeseConcentrationAsSolid(Manganese); }
            set { ChangeAndNotify(ref _calcManganeseConcentrationAsSolid, value); }
        }

        private double _calcCalciteConcentrationAsSolid;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcCalciteConcentrationAsSolid
        {
            get { return ClarifierCalculations.CalcCalciteConcentrationAsSolid(Calcite); }
            set { ChangeAndNotify(ref _calcCalciteConcentrationAsSolid, value); }
        }

        private double _calcMiscellaneousSolidsConcentrationAsSolid;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcMiscellaneousSolidsConcentrationAsSolid
        {
            get { return ClarifierCalculations.CalcMiscellaneousSolidsConcentrationAsSolid(MiscellaneousSolids); }
            set { ChangeAndNotify(ref _calcMiscellaneousSolidsConcentrationAsSolid, value); }
        }

        private double _calcTotalConcentrationAsSolid;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcTotalConcentrationAsSolid
        {
            get { return ClarifierCalculations.CalcTotalConcentrationAsSolid(CalcAluminumConcentrationAsSolid, CalcFerrousIronConcentrationAsSolid, CalcFerricIronConcentrationAsSolid, CalcManganeseConcentrationAsSolid, CalcCalciteConcentrationAsSolid, CalcMiscellaneousSolidsConcentrationAsSolid); }
            set { ChangeAndNotify(ref _calcTotalConcentrationAsSolid, value); }
        }

        private double _calcAluminumFraction;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcAluminumFraction
        {
            get { return ClarifierCalculations.CalcAluminumFraction(CalcAluminumConcentrationAsSolid, CalcFerrousIronConcentrationAsSolid, CalcFerricIronConcentrationAsSolid, CalcManganeseConcentrationAsSolid, CalcCalciteConcentrationAsSolid, CalcMiscellaneousSolidsConcentrationAsSolid); }
            set { ChangeAndNotify(ref _calcAluminumFraction, value); }
        }

        private double _calcFerrousIronFraction;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcFerrousIronFraction
        {
            get { return ClarifierCalculations.CalcFerrousIronFraction(CalcAluminumConcentrationAsSolid, CalcFerrousIronConcentrationAsSolid, CalcFerricIronConcentrationAsSolid, CalcManganeseConcentrationAsSolid, CalcCalciteConcentrationAsSolid, CalcMiscellaneousSolidsConcentrationAsSolid); }
            set { ChangeAndNotify(ref _calcFerrousIronFraction, value); }
        }

        private double _calcFerricIronFraction;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcFerricIronFraction
        {
            get { return ClarifierCalculations.CalcFerricIronFraction(CalcAluminumConcentrationAsSolid, CalcFerrousIronConcentrationAsSolid, CalcFerricIronConcentrationAsSolid, CalcManganeseConcentrationAsSolid, CalcCalciteConcentrationAsSolid, CalcMiscellaneousSolidsConcentrationAsSolid); }
            set { ChangeAndNotify(ref _calcFerricIronFraction, value); }
        }

        private double _calcManganeseFraction;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcManganeseFraction
        {
            get { return ClarifierCalculations.CalcManganeseFraction(CalcAluminumConcentrationAsSolid, CalcFerrousIronConcentrationAsSolid, CalcFerricIronConcentrationAsSolid, CalcManganeseConcentrationAsSolid, CalcCalciteConcentrationAsSolid, CalcMiscellaneousSolidsConcentrationAsSolid); }
            set { ChangeAndNotify(ref _calcManganeseFraction, value); }
        }

        private double _calcCalciteFraction;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcCalciteFraction
        {
            get { return ClarifierCalculations.CalcCalciteFraction(CalcAluminumConcentrationAsSolid, CalcFerrousIronConcentrationAsSolid, CalcFerricIronConcentrationAsSolid, CalcManganeseConcentrationAsSolid, CalcCalciteConcentrationAsSolid, CalcMiscellaneousSolidsConcentrationAsSolid); }
            set { ChangeAndNotify(ref _calcCalciteFraction, value); }
        }

        private double _calcMiscellaneousSolidsFraction;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcMiscellaneousSolidsFraction
        {
            get { return ClarifierCalculations.CalcMiscellaneousSolidsFraction(CalcAluminumConcentrationAsSolid, CalcFerrousIronConcentrationAsSolid, CalcFerricIronConcentrationAsSolid, CalcManganeseConcentrationAsSolid, CalcCalciteConcentrationAsSolid, CalcMiscellaneousSolidsConcentrationAsSolid); }
            set { ChangeAndNotify(ref _calcMiscellaneousSolidsFraction, value); }
        }

        private double _calcMiscellaneousSolidsSpecificGravity;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcMiscellaneousSolidsSpecificGravity
        {
            get { return ClarifierCalculations.CalcMiscellaneousSolidsSpecificGravity(MiscellaneousSolidsDensity); }
            set { ChangeAndNotify(ref _calcMiscellaneousSolidsSpecificGravity, value); }
        }

        private double _calcTotalSpecificGravity;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcTotalSpecificGravity
        {
            get { return ClarifierCalculations.CalcTotalSpecificGravity(CalcTotalWeightedDensity); }
            set { ChangeAndNotify(ref _calcTotalSpecificGravity, value); }
        }

        private double _calcAluminumWeightedDensity;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcAluminumWeightedDensity
        {
            get { return ClarifierCalculations.CalcAluminumWeightedDensity(CalcAluminumFraction, AluminumSpecificGravity); }
            set { ChangeAndNotify(ref _calcAluminumWeightedDensity, value); }
        }

        private double _calcFerrousIronWeightedDensity;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcFerrousIronWeightedDensity
        {
            get { return ClarifierCalculations.CalcFerrousIronWeightedDensity(CalcFerrousIronFraction, FerrousIronSpecificGravity); }
            set { ChangeAndNotify(ref _calcFerrousIronWeightedDensity, value); }
        }

        private double _calcFerricIronWeightedDensity;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcFerricIronWeightedDensity
        {
            get { return ClarifierCalculations.CalcFerricIronWeightedDensity(CalcFerricIronFraction, FerricIronSpecificGravity); }
            set { ChangeAndNotify(ref _calcFerricIronWeightedDensity, value); }
        }

        private double _calcManganeseWeightedDensity;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcManganeseWeightedDensity
        {
            get { return ClarifierCalculations.CalcManganeseWeightedDensity(CalcManganeseFraction, ManganeseSpecificGravity); }
            set { ChangeAndNotify(ref _calcManganeseWeightedDensity, value); }
        }

        private double _calcCalciteWeightedDensity;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcCalciteWeightedDensity
        {
            get { return ClarifierCalculations.CalcCalciteWeightedDensity(CalcCalciteFraction, CalciteSpecificGravity); }
            set { ChangeAndNotify(ref _calcCalciteWeightedDensity, value); }
        }

        private double _calcMiscellaneousSolidsWeightedDensity;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcMiscellaneousSolidsWeightedDensity
        {
            get { return ClarifierCalculations.CalcMiscellaneousSolidsWeightedDensity(CalcMiscellaneousSolidsFraction, CalcMiscellaneousSolidsSpecificGravity); }
            set { ChangeAndNotify(ref _calcMiscellaneousSolidsWeightedDensity, value); }
        }

        private double _calcTotalWeightedDensity;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcTotalWeightedDensity
        {
            get { return ClarifierCalculations.CalcTotalWeightedDensity(CalcAluminumWeightedDensity, CalcFerrousIronWeightedDensity, CalcFerricIronWeightedDensity, CalcManganeseWeightedDensity, CalcCalciteWeightedDensity, CalcMiscellaneousSolidsWeightedDensity); }
            set { ChangeAndNotify(ref _calcTotalWeightedDensity, value); }
        }
        #endregion

        #region Sizing Summary: Sludge Characteristics

        private double _calcSludgeThickened;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcSludgeThickened
        {
            get { return ClarifierCalculations.CalcSludgeThickened(CalcSludgeSpecificGravity); }
            set { ChangeAndNotify(ref _calcSludgeThickened, value); }
        }

        private double _calcSludgeSpecificGravity;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcSludgeSpecificGravity
        {
            get { return ClarifierCalculations.CalcSludgeSpecificGravity(CalcTotalSpecificGravity, SludgeSolidsPercentage); }
            set { ChangeAndNotify(ref _calcSludgeSpecificGravity, value); }
        }

        private double _calcSludgeVolumeSolidsPercentage;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcSludgeVolumeSolidsPercentage
        { 
            get { return ClarifierCalculations.CalcSludgeVolumeSolidsPercentage(CalcTotalSpecificGravity, SludgeSolidsPercentage); }
            set { ChangeAndNotify(ref _calcSludgeVolumeSolidsPercentage, value); }
        }

        private double _calcSludgeProductionPoundsPerMin;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcSludgeProductionPoundsPerMin
        {
            get { return ClarifierCalculations.CalcSludgeProductionPoundsPerMin(TypicalFlow, CalcTotalConcentrationAsSolid); }
            set { ChangeAndNotify(ref _calcSludgeProductionPoundsPerMin, value); }
        }

        private double _calcSludgeProductionPoundsPerDay;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcSludgeProductionPoundsPerDay
        {
            get { return ClarifierCalculations.CalcSludgeProductionPoundsPerDay(CalcSludgeProductionPoundsPerMin); }
            set { ChangeAndNotify(ref _calcSludgeProductionPoundsPerDay, value); }
        }

        private double _calcSludgeProductionGallonsPerMin;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcSludgeProductionGallonsPerMin
        {
            get { return ClarifierCalculations.CalcSludgeProductionGallonsPerMin(CalcSludgeProductionPoundsPerMin, CalcSludgeThickened); }
            set { ChangeAndNotify(ref _calcSludgeProductionGallonsPerMin, value); }
        }

        private double _calcSludgeProductionGallonsPerDay;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcSludgeProductionGallonsPerDay
        {
            get { return ClarifierCalculations.CalcSludgeProductionGallonsPerDay(CalcSludgeProductionGallonsPerMin); }
            set { ChangeAndNotify(ref _calcSludgeProductionGallonsPerDay, value); }
        }

        private double _calcSludgeProductionGallonsPerYear;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcSludgeProductionGallonsPerYear
        {
            get { return ClarifierCalculations.CalcSludgeProductionGallonsPerYear(CalcSludgeProductionGallonsPerDay); }
            set { ChangeAndNotify(ref _calcSludgeProductionGallonsPerYear, value); }
        }
        #endregion

        #region Sizing Summary: Sludge Disposal Pipeline

        private double _calcPipeBeddingAggregateWeight;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcPipeBeddingAggregateWeight
        {
            get { return ClarifierCalculations.CalcPipeBeddingAggregateWeight(NominalPipeOutsideDiameterInches, PipeBeddingAggregateThickness, PipelineLength); }
            set { ChangeAndNotify(ref _calcPipeBeddingAggregateWeight, value); }
        }

        private double _calcPipeInsideDiameter;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcPipeInsideDiameter
        {
            get
            {
                if (EstimatedIronPipeSizeStandardDiameterRatioNumber == 7 && NominalPipeOutsideDiameterInches == 1.5)
                {
                    _calcPipeInsideDiameter = PipeInsideDiameterInchesSDR7NOD15;
                }
                else if (EstimatedIronPipeSizeStandardDiameterRatioNumber == 7 && NominalPipeOutsideDiameterInches == 2)
                {
                    _calcPipeInsideDiameter = PipeInsideDiameterInchesSDR7NOD2;
                }
                else if (EstimatedIronPipeSizeStandardDiameterRatioNumber == 7 && NominalPipeOutsideDiameterInches == 3)
                {
                    _calcPipeInsideDiameter = PipeInsideDiameterInchesSDR7NOD3;
                }
                else if (EstimatedIronPipeSizeStandardDiameterRatioNumber == 7 && NominalPipeOutsideDiameterInches == 4)
                {
                    _calcPipeInsideDiameter = PipeInsideDiameterInchesSDR7NOD4;
                }
                else if (EstimatedIronPipeSizeStandardDiameterRatioNumber == 7 && NominalPipeOutsideDiameterInches == 6)
                {
                    _calcPipeInsideDiameter = PipeInsideDiameterInchesSDR7NOD6;
                }
                if (EstimatedIronPipeSizeStandardDiameterRatioNumber == 9 && NominalPipeOutsideDiameterInches == 1.5)
                {
                    _calcPipeInsideDiameter = PipeInsideDiameterInchesSDR9NOD15;
                }
                else if (EstimatedIronPipeSizeStandardDiameterRatioNumber == 9 && NominalPipeOutsideDiameterInches == 2)
                {
                    _calcPipeInsideDiameter = PipeInsideDiameterInchesSDR9NOD2;
                }
                else if (EstimatedIronPipeSizeStandardDiameterRatioNumber == 9 && NominalPipeOutsideDiameterInches == 3)
                {
                    _calcPipeInsideDiameter = PipeInsideDiameterInchesSDR9NOD3;
                }
                else if (EstimatedIronPipeSizeStandardDiameterRatioNumber == 9 && NominalPipeOutsideDiameterInches == 4)
                {
                    _calcPipeInsideDiameter = PipeInsideDiameterInchesSDR9NOD4;
                }
                else if (EstimatedIronPipeSizeStandardDiameterRatioNumber == 9 && NominalPipeOutsideDiameterInches == 6)
                {
                    _calcPipeInsideDiameter = PipeInsideDiameterInchesSDR9NOD6;
                }
                if (EstimatedIronPipeSizeStandardDiameterRatioNumber == 11 && NominalPipeOutsideDiameterInches == 1.5)
                {
                    _calcPipeInsideDiameter = PipeInsideDiameterInchesSDR11NOD15;
                }
                else if (EstimatedIronPipeSizeStandardDiameterRatioNumber == 11 && NominalPipeOutsideDiameterInches == 2)
                {
                    _calcPipeInsideDiameter = PipeInsideDiameterInchesSDR11NOD2;
                }
                else if (EstimatedIronPipeSizeStandardDiameterRatioNumber == 11 && NominalPipeOutsideDiameterInches == 3)
                {
                    _calcPipeInsideDiameter = PipeInsideDiameterInchesSDR11NOD3;
                }
                else if (EstimatedIronPipeSizeStandardDiameterRatioNumber == 11 && NominalPipeOutsideDiameterInches == 4)
                {
                    _calcPipeInsideDiameter = PipeInsideDiameterInchesSDR11NOD4;
                }
                else if (EstimatedIronPipeSizeStandardDiameterRatioNumber == 11 && NominalPipeOutsideDiameterInches == 6)
                {
                    _calcPipeInsideDiameter = PipeInsideDiameterInchesSDR11NOD6;
                }
                if (EstimatedIronPipeSizeStandardDiameterRatioNumber == 13.5 && NominalPipeOutsideDiameterInches == 1.5)
                {
                    _calcPipeInsideDiameter = PipeInsideDiameterInchesSDR135NOD15;
                }
                else if (EstimatedIronPipeSizeStandardDiameterRatioNumber == 13.5 && NominalPipeOutsideDiameterInches == 2)
                {
                    _calcPipeInsideDiameter = PipeInsideDiameterInchesSDR135NOD2;
                }
                else if (EstimatedIronPipeSizeStandardDiameterRatioNumber == 13.5 && NominalPipeOutsideDiameterInches == 3)
                {
                    _calcPipeInsideDiameter = PipeInsideDiameterInchesSDR135NOD3;
                }
                else if (EstimatedIronPipeSizeStandardDiameterRatioNumber == 13.5 && NominalPipeOutsideDiameterInches == 4)
                {
                    _calcPipeInsideDiameter = PipeInsideDiameterInchesSDR135NOD4;
                }
                else if (EstimatedIronPipeSizeStandardDiameterRatioNumber == 13.5 && NominalPipeOutsideDiameterInches == 6)
                {
                    _calcPipeInsideDiameter = PipeInsideDiameterInchesSDR135NOD6;
                }
                if (EstimatedIronPipeSizeStandardDiameterRatioNumber == 17 && NominalPipeOutsideDiameterInches == 1.5)
                {
                    // FIXME: When this condition occurs, throw an error saying this is invalid as indicated on excel sheet
                    _calcPipeInsideDiameter = PipeInsideDiameterInchesSDR17NOD15;
                }
                else if (EstimatedIronPipeSizeStandardDiameterRatioNumber == 17 && NominalPipeOutsideDiameterInches == 2)
                {
                    _calcPipeInsideDiameter = PipeInsideDiameterInchesSDR17NOD2;
                }
                else if (EstimatedIronPipeSizeStandardDiameterRatioNumber == 17 && NominalPipeOutsideDiameterInches == 3)
                {
                    _calcPipeInsideDiameter = PipeInsideDiameterInchesSDR17NOD3;
                }
                else if (EstimatedIronPipeSizeStandardDiameterRatioNumber == 17 && NominalPipeOutsideDiameterInches == 4)
                {
                    _calcPipeInsideDiameter = PipeInsideDiameterInchesSDR17NOD4;
                }
                else if (EstimatedIronPipeSizeStandardDiameterRatioNumber == 17 && NominalPipeOutsideDiameterInches == 6)
                {
                    _calcPipeInsideDiameter = PipeInsideDiameterInchesSDR17NOD6;
                }

                return _calcPipeInsideDiameter;
            }
            set { ChangeAndNotify(ref _calcPipeInsideDiameter, value); }

        }

        private double _calcPipeDynamicLosses;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcPipeDynamicLosses
        {
            get { return ClarifierCalculations.CalcPipeDynamicLosses(PipelineLength, CalcPipeInsideDiameter, CalcSludgeDisposalPumpFlowRate); }
            set { ChangeAndNotify(ref _calcPipeDynamicLosses, value); }
        }

        private double _calcTotalDynamicHead;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcTotalDynamicHead
        {
            get { return ClarifierCalculations.CalcTotalDynamicHead(CalcPipeDynamicLosses, TotalStaticHead, IncidentalHeadLosses); }
            set { ChangeAndNotify(ref _calcTotalDynamicHead, value); }
        }

        private double _calcTotalDynamicHeadPressure;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcTotalDynamicHeadPressure
        {
            get { return ClarifierCalculations.CalcTotalDynamicHeadPressure(CalcTotalDynamicHead); }
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

        private double _calcPipeFluidVelocity;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcPipeFluidVelocity
        {
            get { return ClarifierCalculations.CalcPipeFluidVelocity(CalcSludgeDisposalPumpFlowRate, CalcPipeInsideDiameter); }
            set { ChangeAndNotify(ref _calcPipeFluidVelocity, value); }
        }

        #endregion

        #region Sizing Summary: Geotube Sludge Disposal

        private double _calcGeotubeVolume;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcGeotubeVolume
        {
            get { return ClarifierCalculations.CalcGeotubeVolume(GeotubeCapacity); }
            set { ChangeAndNotify(ref _calcGeotubeVolume, value); }
        }

        private double _calcGeotubeFillVolume;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcGeotubeFillVolume
        {
            get { return ClarifierCalculations.CalcGeotubeFillVolume(CalcGeotubeVolume, GeotubeFillCapacity); }
            set { ChangeAndNotify(ref _calcGeotubeFillVolume, value); }
        }

        private double _calcDewateredSludge;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcDewateredSludge
        {
            get { return ClarifierCalculations.CalcDewateredSludge(CalcDewateredSludgeSpecificGravity); }
            set { ChangeAndNotify(ref _calcDewateredSludge, value); }
        }

        private double _calcDewateredSludgeSpecificGravity;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcDewateredSludgeSpecificGravity
        {
            get { return ClarifierCalculations.CalcDewateredSludgeSpecificGravity(CalcTotalSpecificGravity, DewateredSolidsPercentage); }
            set { ChangeAndNotify(ref _calcDewateredSludgeSpecificGravity, value); }
        }

        private double _calcDewateredSludgeGenerationRateCubicYardsPerDay;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcDewateredSludgeGenerationRateCubicYardsPerDay
        {
            get { return ClarifierCalculations.CalcDewateredSludgeGenerationRateCubicYardsPerDay(CalcSludgeProductionPoundsPerDay, CalcDewateredSludge); }
            set { ChangeAndNotify(ref _calcDewateredSludgeGenerationRateCubicYardsPerDay, value); }
        }

        private double _calcDewateredSludgeGenerationRateCubicYardsPerYear;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcDewateredSludgeGenerationRateCubicYardsPerYear
        {
            get { return ClarifierCalculations.CalcDewateredSludgeGenerationRateCubicYardsPerYear(CalcDewateredSludgeGenerationRateCubicYardsPerDay); }
            set { ChangeAndNotify(ref _calcDewateredSludgeGenerationRateCubicYardsPerYear, value); }
        }

        private double _calcGeotubeFillTime;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcGeotubeFillTime
        {
            get { return ClarifierCalculations.CalcGeotubeFillTime(CalcGeotubeFillVolume, CalcDewateredSludgeGenerationRateCubicYardsPerDay); }
            set { ChangeAndNotify(ref _calcGeotubeFillTime, value); }
        }

        private double _calcGeotubeRequiredPerYear;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcGeotubeRequiredPerYear
        {
            get { return ClarifierCalculations.CalcGeotubeRequiredPerYear(CalcGeotubeFillTime); }
            set { ChangeAndNotify(ref _calcGeotubeRequiredPerYear, value); }
        }

        private double _calcTriaxleLoadsRequiredPerYear;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcTriaxleLoadsRequiredPerYear
        {
            get { return Math.Round(ClarifierCalculations.CalcTriaxleLoadsRequiredPerYear(CalcDewateredSludgeGenerationRateCubicYardsPerYear, TruckVolume)); }
            set { ChangeAndNotify(ref _calcTriaxleLoadsRequiredPerYear, value); }
        }

        private double _calcPolymerRequirementPoundsPerDay;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcPolymerRequirementPoundsPerDay
        {
            get { return ClarifierCalculations.CalcPolymerRequirementPoundsPerDay(CalcSludgeDisposalPumpFlowRate, PolymerTargetDose, PolymerActivePercentage, CalcSludgeDisposalPumpTimeHoursPerDay); }
            set { ChangeAndNotify(ref _calcPolymerRequirementPoundsPerDay, value); }
        }

        private double _calcPolymerRequirementPoundsPerYear;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcPolymerRequirementPoundsPerYear
        {
            get { return ClarifierCalculations.CalcPolymerRequirementPoundsPerYear(CalcPolymerRequirementPoundsPerDay); }
            set { ChangeAndNotify(ref _calcPolymerRequirementPoundsPerYear, value); }
        }

        private double _calcEmulsionPolymerRequirementGallonsPerYear;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcEmulsionPolymerRequirementGallonsPerYear
        {
            get
            {
                if (PolymerName == PolymerNameEmulsion)
                {
                    _calcEmulsionPolymerRequirementGallonsPerYear = ClarifierCalculations.CalcEmulsionPolymerRequirementGallonsPerYear(CalcPolymerRequirementPoundsPerYear, EmulsionPolymerDensity);
                }
                else
                {
                    _calcEmulsionPolymerRequirementGallonsPerYear = 0;
                }
                return _calcEmulsionPolymerRequirementGallonsPerYear;
            }
            set { ChangeAndNotify(ref _calcEmulsionPolymerRequirementGallonsPerYear, value); }
        }

        #endregion

        #region Properties - Capital Costs

        private decimal _calcClarifierTankCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcClarifierTankCost
        {
            get { return ClarifierCalculations.CalcClarifierTankCost(CalcClarifierVolume, ClarifierQuantity, ClarifierConstructionMaterialCostFactor); }
            set { ChangeAndNotify(ref _calcClarifierTankCost, value); }
        }

        private decimal _calcClarifierInternalsCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcClarifierInternalsCost
        {
            get
            {
                switch (ClarifierDesignOptionsProperty)
                {
                    case RadioButtonsClarifierDesignOptionsEnum.OptionConventional:
                        _calcClarifierInternalsCost = ClarifierCalculations.CalcClarifierInternalsCost(ClarifierInternalsCostFactorConventional, CalcClarifierDiameter, ClarifierQuantity);
                        break;
                    case RadioButtonsClarifierDesignOptionsEnum.OptionSolidsContact:
                        _calcClarifierInternalsCost = ClarifierCalculations.CalcClarifierInternalsCost(ClarifierInternalsCostFactorSolidsContact, CalcClarifierDiameter, ClarifierQuantity);
                        break;
                    default:
                        break;
                }
                return _calcClarifierInternalsCost;
            }
            set { ChangeAndNotify(ref _calcClarifierInternalsCost, value); }
        }

        private decimal _calcOverflowWeirCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcOverflowWeirCost
        {
            get { return ClarifierCalculations.CalcOverflowWeirCost(CalcClarifierDiameter, ClarifierQuantity, OverflowWeirUnitCost); }
            set { ChangeAndNotify(ref _calcOverflowWeirCost, value); }
        }

        private decimal _calcCatwalkCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcCatwalkCost
        {
            get { return ClarifierCalculations.CalcCatwalkCost(CalcClarifierDiameter, ClarifierQuantity, CatwalkUnitCost); }
            set { ChangeAndNotify(ref _calcCatwalkCost, value); }
        }

        private decimal _calcDensityCurrentBaffleCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcDensityCurrentBaffleCost
        {
            get
            {
                if (IsDensityCurrentBaffle)
                {
                    _calcDensityCurrentBaffleCost = ClarifierCalculations.CalcDensityCurrentBaffleCost(CalcClarifierDiameter, ClarifierQuantity, DensityCurrentBaffleUnitCost);
                }
                else
                {
                    _calcDensityCurrentBaffleCost = 0m;
                }
                return _calcDensityCurrentBaffleCost;
            }
            set { ChangeAndNotify(ref _calcDensityCurrentBaffleCost, value); }
        }

        private decimal _calcTankProtectiveCoatingCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcTankProtectiveCoatingCost
        {
            get { return ClarifierCalculations.CalcTankProtectiveCoatingCost(CalcTankProtectiveCoatingSurfaceArea, ClarifierQuantity, TankProtectiveCoatingUnitCost); }
            set { ChangeAndNotify(ref _calcTankProtectiveCoatingCost, value); }
        }

        private decimal _calcSludgeRecirculationPumpCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcSludgeRecirculationPumpCost
        {
            get { return ClarifierCalculations.CalcSludgeRecirculationPumpCost(CalcSludgeRecirculationPumpPower, ClarifierQuantity); }
            set { ChangeAndNotify(ref _calcSludgeRecirculationPumpCost, value); }
        }

        private decimal _calcSludgeDisposalPumpCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcSludgeDisposalPumpCost
        {
            get { return ClarifierCalculations.CalcSludgeDisposalPumpCost(CalcSludgeDisposalPumpPower, ClarifierQuantity, SludgeDisposalHorizontalCentrifugalPumpQuantity); }
            set { ChangeAndNotify(ref _calcSludgeDisposalPumpCost, value); }
        }

        private decimal _calcSludgeDisposalPumpConcretePadCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcSludgeDisposalPumpConcretePadCost
        {
            get { return ClarifierCalculations.CalcSludgeDisposalPumpConcretePadCost(ClarifierQuantity, SludgeDisposalHorizontalCentrifugalPumpQuantity, FoundationSiteSoilLoadBearingMultiplier, FoundationConcreteMaterialAndPlacementCost); }
            set { ChangeAndNotify(ref _calcSludgeDisposalPumpConcretePadCost, value); }
        }

        private decimal _calcClarifierEquipmentCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcClarifierEquipmentCost
        {
            get { return ClarifierCalculations.CalcClarifierEquipmentCost(CalcClarifierTankCost, CalcClarifierInternalsCost, CalcOverflowWeirCost,
                                                        CalcCatwalkCost, CalcDensityCurrentBaffleCost, CalcTankProtectiveCoatingCost,
                                                        CalcSludgeRecirculationPumpCost, CalcSludgeDisposalPumpCost, CalcSludgeDisposalPumpConcretePadCost); }
            set { ChangeAndNotify(ref _calcClarifierEquipmentCost, value); }
        }

        private decimal _calcFoundationCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcFoundationCost
        {
            get { return ClarifierCalculations.CalcFoundationCost(CalcFoundationVolume, ClarifierQuantity, FoundationConcreteMaterialAndPlacementCost); }
            set { ChangeAndNotify(ref _calcFoundationCost, value); }
        }

        /// <summary>
        ///  Radio button binding
        /// </summary>
        public enum RadioButtonsCapitalCostSystemInstallOptionsEnum
        {
            OptionCostMultiplier,
            OptionUserSpecified,
        }

        private RadioButtonsCapitalCostSystemInstallOptionsEnum _capitalCostSystemInstallOptionsProperty;
        /// <summary>
        ///  User specified 
        /// </summary>
        public RadioButtonsCapitalCostSystemInstallOptionsEnum CapitalCostSystemInstallOptionsProperty
        {
            get { return _capitalCostSystemInstallOptionsProperty; }
            set { ChangeAndNotify(ref _capitalCostSystemInstallOptionsProperty, value, nameof(CapitalCostSystemInstallOptionsProperty), CalcPropertiesStringArray); }
        }

        private double _capitalCostSystemInstallationMultiplier;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double CapitalCostSystemInstallationMultiplier
        {
            get { return _capitalCostSystemInstallationMultiplier; }
            set { ChangeAndNotify(ref _capitalCostSystemInstallationMultiplier, value, nameof(CapitalCostSystemInstallationMultiplier), CalcPropertiesStringArray); }
        }

        private decimal _capitalCostSystemInstallationUserSpecified;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal CapitalCostSystemInstallationUserSpecified
        {
            get { return _capitalCostSystemInstallationUserSpecified; }
            set { ChangeAndNotify(ref _capitalCostSystemInstallationUserSpecified, value, nameof(CapitalCostSystemInstallationUserSpecified), CalcPropertiesStringArray); }
        }

        private decimal _calcInstallationCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcInstallationCost
        {
            get
            {
                switch (CapitalCostSystemInstallOptionsProperty)
                {
                    case RadioButtonsCapitalCostSystemInstallOptionsEnum.OptionCostMultiplier:
                        _calcInstallationCost = ClarifierCalculations.CalcInstallationCost(CalcClarifierTankCost, CalcClarifierInternalsCost, CalcOverflowWeirCost, CalcCatwalkCost, CalcDensityCurrentBaffleCost, CalcTankProtectiveCoatingCost, CalcFoundationCost, CapitalCostSystemInstallationMultiplier);
                        break;

                    case RadioButtonsCapitalCostSystemInstallOptionsEnum.OptionUserSpecified:
                        _calcInstallationCost = CapitalCostSystemInstallationUserSpecified;
                        break;
                }

                return _calcInstallationCost;
            }
            set { ChangeAndNotify(ref _calcInstallationCost, value); }
        }

        private decimal _calcSludgeDisposalBoreholeCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcSludgeDisposalBoreholeCost
        {
            get
            {
                switch (SludgeDisposalOptionsProperty)
                {
                    case RadioButtonsSludgeDisposalOptionsEnum.OptionBorehole:
                        if (IsBorehole1)
                        {
                            Borehole1Cost = ClarifierCalculations.CalcSludgeDisposalBoreholeCost(Borehole1SizingMultiplier, Borehole1DrillingDepth, Borehole1DrillingAndCastingInstallationUnitCost);
                        }
                        else
                        {
                            Borehole1Cost = 0m;
                        }

                        if (IsBorehole2)
                        {
                            Borehole2Cost = ClarifierCalculations.CalcSludgeDisposalBoreholeCost(Borehole2SizingMultiplier, Borehole2DrillingDepth, Borehole2DrillingAndCastingInstallationUnitCost);
                        }
                        else
                        {
                            Borehole2Cost = 0m;
                        }

                        if (IsBorehole3)
                        {
                            Borehole3Cost = ClarifierCalculations.CalcSludgeDisposalBoreholeCost(Borehole3SizingMultiplier, Borehole3DrillingDepth, Borehole3DrillingAndCastingInstallationUnitCost);
                        }
                        else
                        {
                            Borehole3Cost = 0m;
                        }
                        _calcSludgeDisposalBoreholeCost = Borehole1Cost + Borehole2Cost + Borehole3Cost;
                        break;
                    case RadioButtonsSludgeDisposalOptionsEnum.OptionGeotube:
                        _calcSludgeDisposalBoreholeCost = 0m;
                        break;
                    default:
                        break;
                }
                return _calcSludgeDisposalBoreholeCost;
            }
            set { ChangeAndNotify(ref _calcSludgeDisposalBoreholeCost, value); }
        }

 
        private double _calcBoreholeQuantity;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcBoreholeQuantity
        {
            get
            {
                double quantity = 0;

                if (IsBorehole1)
                {
                    quantity += 1;
                }

                if (IsBorehole2)
                {
                    quantity += 1;
                }

                if (IsBorehole3)
                {
                    quantity += 1;
                }

                _calcBoreholeQuantity = quantity;

                return _calcBoreholeQuantity;
            }
            set { ChangeAndNotify(ref _calcBoreholeQuantity, value); }
        }

        private decimal _calcSludgeDisposalBoreholeCostAvg;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcSludgeDisposalBoreholeCostAvg
        {
            get
            {
                if (CalcBoreholeQuantity == 0)
                {
                    _calcSludgeDisposalBoreholeCostAvg = 0;
                }
                else
                {
                    _calcSludgeDisposalBoreholeCostAvg = ClarifierCalculations.CalcSludgeDisposalBoreholeCostAvg(CalcBoreholeQuantity, CalcSludgeDisposalBoreholeCost);
                }
                return _calcSludgeDisposalBoreholeCostAvg;
            }
            set { ChangeAndNotify(ref _calcSludgeDisposalBoreholeCostAvg, value); }
        }

        private decimal _calcExcavationCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcExcavationCost
        {
            get { return ClarifierCalculations.CalcExcavationCost(NominalPipeOutsideDiameterInches, PipelineLength, ExcavationUnitCost); }
            set { ChangeAndNotify(ref _calcExcavationCost, value); }
        }

        private decimal _calcBackfillAndCompactionCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcBackfillAndCompactionCost
        {
            get { return ClarifierCalculations.CalcBackfillAndCompactionCost(NominalPipeOutsideDiameterInches, PipelineLength, BackfillAndCompactionUnitCost); }
            set { ChangeAndNotify(ref _calcBackfillAndCompactionCost, value); }
        }

        private decimal _calcPipeBeddingAggregateCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcPipeBeddingAggregateCost
        {
            get { return ClarifierCalculations.CalcPipeBeddingAggregateCost(CalcPipeBeddingAggregateWeight, AggregateUnitCost); }
            set { ChangeAndNotify(ref _calcPipeBeddingAggregateCost, value); }
        }

        private decimal _calcPipeCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcPipeCost
        {
            get
            {
                if (NominalPipeOutsideDiameterInches == 1.5)
                {
                    _calcPipeCost = ClarifierCalculations.CalcPipeCostNOD15(PipelineLength);
                }
                else if (NominalPipeOutsideDiameterInches == 2)
                {
                    _calcPipeCost = ClarifierCalculations.CalcPipeCostNOD2(PipelineLength);
                }
                else if (NominalPipeOutsideDiameterInches == 3)
                {
                    _calcPipeCost = ClarifierCalculations.CalcPipeCostNOD3(PipelineLength);
                }
                else if (NominalPipeOutsideDiameterInches == 4)
                {
                    _calcPipeCost = ClarifierCalculations.CalcPipeCostNOD4(PipelineLength);
                }
                else if (NominalPipeOutsideDiameterInches == 6)
                {
                    _calcPipeCost = ClarifierCalculations.CalcPipeCostNOD6(PipelineLength);
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
                if (NominalPipeOutsideDiameterInches == 1.5)
                {
                    _calcPipeFusionAndInstallationCost = ClarifierCalculations.CalcPipeFusionAndInstallationCost(PipelineLength, PipeFusionCostNOD15);
                }
                else if (NominalPipeOutsideDiameterInches == 2)
                {
                    _calcPipeFusionAndInstallationCost = ClarifierCalculations.CalcPipeFusionAndInstallationCost(PipelineLength, PipeFusionCostNOD2);
                }
                else if (NominalPipeOutsideDiameterInches == 3)
                {
                    _calcPipeFusionAndInstallationCost = ClarifierCalculations.CalcPipeFusionAndInstallationCost(PipelineLength, PipeFusionCostNOD3);
                }
                else if (NominalPipeOutsideDiameterInches == 4)
                {
                    _calcPipeFusionAndInstallationCost = ClarifierCalculations.CalcPipeFusionAndInstallationCost(PipelineLength, PipeFusionCostNOD4);
                }
                else if (NominalPipeOutsideDiameterInches == 6)
                {
                    _calcPipeFusionAndInstallationCost = ClarifierCalculations.CalcPipeFusionAndInstallationCost(PipelineLength, PipeFusionCostNOD6);
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
                    _calcAirVacuumReleaseAssembiesCost = ClarifierCalculations.CalcAirVacuumReleaseAssembliesCost(AirVacuumReleaseAssembliesQuantity, AirVacuumReleaseAssembliesUnitCost);
                }
                else
                {
                    _calcAirVacuumReleaseAssembiesCost = 0m;
                }
                return _calcAirVacuumReleaseAssembiesCost;
            }
            set { ChangeAndNotify(ref _calcAirVacuumReleaseAssembiesCost, value); }
        }

        private decimal _calcPigLaunchersReceiversAssembiesCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcPigLaunchersReceiversAssembiesCost
        {
            get
            {
                if (IsPigLaunchersReceiversAssemblies)
                {
                    _calcPigLaunchersReceiversAssembiesCost = ClarifierCalculations.CalcPigLaunchersReceiversAssembliesCost(PigLaunchersReceiversAssembliesQuantity, PigLaunchersReceiversAssembliesUnitCost);
                }
                else
                {
                    _calcPigLaunchersReceiversAssembiesCost = 0m;
                }
                return _calcPigLaunchersReceiversAssembiesCost;
            }
            set { ChangeAndNotify(ref _calcPigLaunchersReceiversAssembiesCost, value); }
        }

        private decimal _calcSludgeDisposalPipelineCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcSludgeDisposalPipelineCost
        {
            get
            {
                return ClarifierCalculations.CalcSludgeDisposalPipelineCost(CalcExcavationCost, CalcBackfillAndCompactionCost,
                                                             CalcPipeBeddingAggregateCost, CalcPipeCost,
                                                             CalcPipeFusionAndInstallationCost, CalcAirVacuumReleaseAssembiesCost,
                                                             CalcPigLaunchersReceiversAssembiesCost); 
            }
            set { ChangeAndNotify(ref _calcSludgeDisposalPipelineCost, value); }
        }

        private decimal _calcOtherCapitalItemsCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcOtherCapitalItemsCost
        {
            get
            {
                return ClarifierCalculations.CalcOtherCapitalItemsCost(OtherCapitalItemQuantity1, OtherCapitalItemUnitCost1,
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
                _calcCapitalCostTotal = ClarifierCalculations.CalcCapitalCostTotal(CalcClarifierEquipmentCost, CalcFoundationCost, CalcInstallationCost, CalcSludgeDisposalBoreholeCost, CalcSludgeDisposalPipelineCost, CalcOtherCapitalItemsCost);

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

        private double _annualCostOperationAndMaintenanceMultiplier;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double AnnualCostOperationAndMaintenanceMultiplier
        {
            get { return _annualCostOperationAndMaintenanceMultiplier; }
            set { ChangeAndNotify(ref _annualCostOperationAndMaintenanceMultiplier, value, nameof(AnnualCostOperationAndMaintenanceMultiplier), CalcPropertiesStringArray); }
        }

        private decimal _annualCostOperationAndMaintenanceUserSpecified;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal AnnualCostOperationAndMaintenanceUserSpecified
        {
            get { return _annualCostOperationAndMaintenanceUserSpecified; }
            set { ChangeAndNotify(ref _annualCostOperationAndMaintenanceUserSpecified, value, nameof(AnnualCostOperationAndMaintenanceUserSpecified), CalcPropertiesStringArray); }
        }

        private decimal _calcAnnualCostOperationAndMaintenanceEstimate;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcAnnualCostOperationAndMaintenanceEstimate
        {
            get { return ClarifierCalculations.CalcAnnualCostOperationAndMaintenance(AnnualCostOperationAndMaintenanceMultiplier, CalcCapitalCostTotal); }
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
                        _calcAnnualCostOperationAndMaintenance = AnnualCostOperationAndMaintenanceUserSpecified;
                        break;
                }

                return _calcAnnualCostOperationAndMaintenance;
            }
            set { ChangeAndNotify(ref _calcAnnualCostOperationAndMaintenance, value); }
        }


        public enum RadioButtonsAnnualCostElectricOptionsEnum
        {
            OptionAnnualCostEstimated,
            OptionAnnualCostUserSpecified
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


        private decimal _calcClarifierElectricCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcClarifierElectricCost
        {
            get { return ClarifierCalculations.CalcClarifierElectricCost(RakeDriveMotorPower, ClarifierMotorOperationalTimeHoursPerDay, ClarifierMotorOperationalTimeDaysPerYear, ClarifierQuantity, ElectricUnitCost); }
            set { ChangeAndNotify(ref _calcClarifierElectricCost, value); }
        }

        private decimal _calcImpellerMotorElectricCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcImpellerMotorElectricCost
        {
            get { return ClarifierCalculations.CalcImpellerMotorElectricCost(CalcImpellerMotorPower, ClarifierMotorOperationalTimeHoursPerDay, ClarifierMotorOperationalTimeDaysPerYear, ElectricUnitCost); }
            set { ChangeAndNotify(ref _calcImpellerMotorElectricCost, value); }
        }

        private decimal _calcSludgeRecirculationPumpElectricCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcSludgeRecirculationPumpElectricCost
        {
            get { return ClarifierCalculations.CalcSludgeRecirculationPumpElectricCost(CalcSludgeRecirculationPumpPower, ClarifierMotorOperationalTimeHoursPerDay, ClarifierMotorOperationalTimeDaysPerYear, ClarifierQuantity, ElectricUnitCost); }
            set { ChangeAndNotify(ref _calcSludgeRecirculationPumpElectricCost, value); }
        }

        private decimal _calcSludgeDisposalPumpElectricCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcSludgeDisposalPumpElectricCost
        {
            get { return ClarifierCalculations.CalcSludgeDisposalPumpElectricCost(CalcSludgeDisposalPumpPower, CalcSludgeDisposalPumpTimeHoursPerDay, ElectricUnitCost); }
            set { ChangeAndNotify(ref _calcSludgeDisposalPumpElectricCost, value); }
        }

        private decimal _calcPolymerPumpElectricCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcPolymerPumpElectricCost
        {
            get { return ClarifierCalculations.CalcPolymerPumpElectricCost(PolymerPumpPower, CalcSludgeDisposalPumpTimeHoursPerDay, ElectricUnitCost); }
            set { ChangeAndNotify(ref _calcPolymerPumpElectricCost, value); }
        }

        private decimal _calcAnnualCostElectricEstimated;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcAnnualCostElectricEstimated
        {
            get
            {
                return ClarifierCalculations.CalcAnnualCostElectric(CalcClarifierElectricCost, CalcImpellerMotorElectricCost,
                                                                       CalcSludgeRecirculationPumpElectricCost, CalcSludgeDisposalPumpElectricCost,
                                                                       CalcPolymerPumpElectricCost);
            }
            set { ChangeAndNotify(ref _calcAnnualCostElectricEstimated, value); }
        }

        private decimal _annualCostElectricUserSpecified;
        /// <summary>
        ///  User specified 
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
                    case RadioButtonsAnnualCostElectricOptionsEnum.OptionAnnualCostEstimated:
                        _calcAnnualCostElectric = CalcAnnualCostElectricEstimated;
                        break;
                    case RadioButtonsAnnualCostElectricOptionsEnum.OptionAnnualCostUserSpecified:
                        _calcAnnualCostElectric = AnnualCostElectricUserSpecified;
                        break;
                    default:
                        break;
                }
                return _calcAnnualCostElectric;
            }
            set { ChangeAndNotify(ref _calcAnnualCostElectric, value); }
        }


        private decimal _annualCostGeotubeSludgeDisposalUserSpecified;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal AnnualCostGeotubeSludgeDisposalUserSpecified
        {
            get { return _annualCostGeotubeSludgeDisposalUserSpecified; }
            set { ChangeAndNotify(ref _annualCostGeotubeSludgeDisposalUserSpecified, value, nameof(AnnualCostGeotubeSludgeDisposalUserSpecified), CalcPropertiesStringArray); }
        }

        public enum RadioButtonsAnnualCostGeotubeSludgeDisposalOptionsEnum
        {
            OptionAnnualCostEstimated,
            OptionAnnualCostUserSpecified
        }

        private RadioButtonsAnnualCostGeotubeSludgeDisposalOptionsEnum _annualCostGeotubeSludgeDisposalOptionsProperty;
        /// <summary>
        ///  User specified 
        /// </summary>
        public RadioButtonsAnnualCostGeotubeSludgeDisposalOptionsEnum AnnualCostGeotubeSludgeDisposalOptionsProperty
        {
            get { return _annualCostGeotubeSludgeDisposalOptionsProperty; }
            set { ChangeAndNotify(ref _annualCostGeotubeSludgeDisposalOptionsProperty, value, nameof(AnnualCostGeotubeSludgeDisposalOptionsProperty), CalcPropertiesStringArray); }
        }

        private decimal _calcGeotubeCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcGeotubeCost
        {
            get
            {
                switch (SludgeDisposalOptionsProperty)
                {
                    case RadioButtonsSludgeDisposalOptionsEnum.OptionBorehole:
                        _calcGeotubeCost = 0m;
                        break;
                    case RadioButtonsSludgeDisposalOptionsEnum.OptionGeotube:
                        _calcGeotubeCost = ClarifierCalculations.CalcGeotubeCost(CalcGeotubeRequiredPerYear, GeotubeUnitCost);
                        break;
                    default:
                        break;
                }
                return _calcGeotubeCost;
            }
            set { ChangeAndNotify(ref _calcGeotubeCost, value); }
        }

        private decimal _calcGeotubeDisposalCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcGeotubeDisposalCost
        {
            get
            {
                switch (SludgeDisposalOptionsProperty)
                {
                    case RadioButtonsSludgeDisposalOptionsEnum.OptionBorehole:
                        _calcGeotubeDisposalCost = 0m;
                        break;
                    case RadioButtonsSludgeDisposalOptionsEnum.OptionGeotube:
                        _calcGeotubeDisposalCost = ClarifierCalculations.CalcGeotubeDisposalCost(CalcTriaxleLoadsRequiredPerYear, RoundtripDistance, HaulingTransportationUnitCost, LandfillTippingFee, ExcavationHaulDisposeTime, ExcavatorUnitCost);
                        break;
                    default:
                        break;
                }
                return _calcGeotubeDisposalCost;
            }
            set { ChangeAndNotify(ref _calcGeotubeDisposalCost, value); }
        }

        private decimal _calcGeotubeDryPolymerCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcGeotubeDryPolymerCost
        {
            get { return ClarifierCalculations.CalcGeotubeDryPolymerCost(CalcPolymerRequirementPoundsPerYear, PolymerUnitCostDry); }
            set { ChangeAndNotify(ref _calcGeotubeDryPolymerCost, value); }
        }

        private decimal _calcGeotubeEmulsionPolymerCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcGeotubeEmulsionPolymerCost
        {
            get { return ClarifierCalculations.CalcGeotubeEmulsionPolymerCost(CalcEmulsionPolymerRequirementGallonsPerYear, PolymerUnitCostEmulsion); }
            set { ChangeAndNotify(ref _calcGeotubeEmulsionPolymerCost, value); }
        }

        private decimal _calcGeotubePolymerCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcGeotubePolymerCost
        {
            get
            {
                switch (SludgeDisposalOptionsProperty)
                {
                    case RadioButtonsSludgeDisposalOptionsEnum.OptionBorehole:
                        _calcGeotubePolymerCost = 0m;
                        break;
                    case RadioButtonsSludgeDisposalOptionsEnum.OptionGeotube:
                        if (PolymerName == "Emulsion")
                        {
                            _calcGeotubePolymerCost = CalcGeotubeEmulsionPolymerCost;
                        }
                        else
                        {
                            _calcGeotubePolymerCost = CalcGeotubeDryPolymerCost;
                        }
                        break;
                    default:
                        break;
                }

                return _calcGeotubePolymerCost;
            }
            set { ChangeAndNotify(ref _calcGeotubePolymerCost, value); }
        }

        private decimal _calcAnnualCostGeotubeSludgeDisposalEstimate;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcAnnualCostGeotubeSludgeDisposalEstimate
        {
            get { return ClarifierCalculations.CalcTotalGeotubeSludgeDisposalCost(CalcGeotubeCost, CalcGeotubeDisposalCost, CalcGeotubePolymerCost); }
            set { ChangeAndNotify(ref _calcAnnualCostGeotubeSludgeDisposalEstimate, value); }
        }

        private decimal _calcAnnualCostGeotubeSludgeDisposal;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcAnnualCostGeotubeSludgeDisposal
        {
            get
            {
                switch (SludgeDisposalOptionsProperty)
                {
                    case RadioButtonsSludgeDisposalOptionsEnum.OptionBorehole:
                        _calcAnnualCostGeotubeSludgeDisposal = 0m;
                        break;
                    case RadioButtonsSludgeDisposalOptionsEnum.OptionGeotube:
                        switch (AnnualCostGeotubeSludgeDisposalOptionsProperty)
                        {
                            case RadioButtonsAnnualCostGeotubeSludgeDisposalOptionsEnum.OptionAnnualCostEstimated:
                                _calcAnnualCostGeotubeSludgeDisposal = CalcAnnualCostGeotubeSludgeDisposalEstimate;
                                break;
                            case RadioButtonsAnnualCostGeotubeSludgeDisposalOptionsEnum.OptionAnnualCostUserSpecified:
                                _calcAnnualCostGeotubeSludgeDisposal = AnnualCostGeotubeSludgeDisposalUserSpecified;
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }

                return _calcAnnualCostGeotubeSludgeDisposal;
            }
            set { ChangeAndNotify(ref _calcAnnualCostGeotubeSludgeDisposal, value); }
        }

        private decimal _calcOtherAnnualItemsCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcOtherAnnualItemsCost
        {
            get
            {
                return ClarifierCalculations.CalcOtherAnnualItemsCost(OtherAnnualItemQuantity1, OtherAnnualItemUnitCost1,
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
                _calcAnnualCost = ClarifierCalculations.CalcAnnualCostTotal(CalcAnnualCostOperationAndMaintenance, CalcAnnualCostElectric, CalcAnnualCostGeotubeSludgeDisposal, CalcOtherAnnualItemsCost);
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

        private double _recapitalizationCostLifeCycleClarifierTankInternals;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostLifeCycleClarifierTankInternals
        {
            get
            {
                if (ClarifierConstructionMaterialName == ClarifierConstructionMaterialNameConcrete && TankProtectiveCoatingName == TankProtectiveCoatingNameNone)
                {
                    _recapitalizationCostLifeCycleClarifierTankInternals = RecapitalizationCostLifeCycleTankConcreteWithoutCoating;
                }
                else if (ClarifierConstructionMaterialName == ClarifierConstructionMaterialNameConcrete && TankProtectiveCoatingName != TankProtectiveCoatingNameNone)
                {
                    _recapitalizationCostLifeCycleClarifierTankInternals = RecapitalizationCostLifeCycleTankConcreteWithCoating;
                }
                else if (ClarifierConstructionMaterialName == ClarifierConstructionMaterialNameBoltedSteel && TankProtectiveCoatingName == TankProtectiveCoatingNameNone)
                {
                    _recapitalizationCostLifeCycleClarifierTankInternals = RecapitalizationCostLifeCycleTankSteelWithoutCoating;
                }
                else if (ClarifierConstructionMaterialName == ClarifierConstructionMaterialNameBoltedSteel && TankProtectiveCoatingName != TankProtectiveCoatingNameNone)
                {
                    _recapitalizationCostLifeCycleClarifierTankInternals = RecapitalizationCostLifeCycleTankSteelWithCoating;
                }
                else if (ClarifierConstructionMaterialName == ClarifierConstructionMaterialNameWeldedSteel && TankProtectiveCoatingName == TankProtectiveCoatingNameNone)
                {
                    _recapitalizationCostLifeCycleClarifierTankInternals = RecapitalizationCostLifeCycleTankSteelWithoutCoating;
                }
                else // (ClarifierConstructionMaterialName == ClarifierConstructionMaterialNameWeldedSteel && TankProtectiveCoatingName != TankProtectiveCoatingNameNone)
                {
                    _recapitalizationCostLifeCycleClarifierTankInternals = RecapitalizationCostLifeCycleTankSteelWithCoating;
                }

                return _recapitalizationCostLifeCycleClarifierTankInternals;
            }
            set { ChangeAndNotify(ref _recapitalizationCostLifeCycleClarifierTankInternals, value, nameof(RecapitalizationCostLifeCycleClarifierTankInternals), CalcPropertiesStringArray); }
        }



        private double _recapitalizationCostLifeCycleTankConcreteWithCoating;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostLifeCycleTankConcreteWithCoating
        {
            get { return _recapitalizationCostLifeCycleTankConcreteWithCoating; }
            set { ChangeAndNotify(ref _recapitalizationCostLifeCycleTankConcreteWithCoating, value, nameof(RecapitalizationCostLifeCycleTankConcreteWithCoating), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostLifeCycleTankConcreteWithoutCoating;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostLifeCycleTankConcreteWithoutCoating
        {
            get { return _recapitalizationCostLifeCycleTankConcreteWithoutCoating; }
            set { ChangeAndNotify(ref _recapitalizationCostLifeCycleTankConcreteWithoutCoating, value, nameof(RecapitalizationCostLifeCycleTankConcreteWithoutCoating), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostLifeCycleTankSteelWithCoating;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostLifeCycleTankSteelWithCoating
        {
            get { return _recapitalizationCostLifeCycleTankSteelWithCoating; }
            set { ChangeAndNotify(ref _recapitalizationCostLifeCycleTankSteelWithCoating, value, nameof(RecapitalizationCostLifeCycleTankSteelWithCoating), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostLifeCycleTankSteelWithoutCoating;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostLifeCycleTankSteelWithoutCoating
        {
            get { return _recapitalizationCostLifeCycleTankSteelWithoutCoating; }
            set { ChangeAndNotify(ref _recapitalizationCostLifeCycleTankSteelWithoutCoating, value, nameof(RecapitalizationCostLifeCycleTankSteelWithoutCoating), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostLifeCycleTankProtectiveCoating;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostLifeCycleTankProtectiveCoating
        {
            get
            {
                if (TankProtectiveCoatingName == TankProtectiveCoatingNameAlkyd)
                {
                    _recapitalizationCostLifeCycleTankProtectiveCoating = RecapitalizationCostLifeCycleTankProtectiveCoatingAlkyd;
                }
                else if (TankProtectiveCoatingName == TankProtectiveCoatingNameEpoxy)
                {
                    _recapitalizationCostLifeCycleTankProtectiveCoating = RecapitalizationCostLifeCycleTankProtectiveCoatingEpoxy;
                }
                else if (TankProtectiveCoatingName == TankProtectiveCoatingNameZincUrethane)
                {
                    _recapitalizationCostLifeCycleTankProtectiveCoating = RecapitalizationCostLifeCycleTankProtectiveCoatingZincUrethane;
                }
                else // (TankProtectiveCoatingName == TankProtectiveCoatingNameNone)
                {
                    _recapitalizationCostLifeCycleTankProtectiveCoating = RecapitalizationCostLifeCycleTankProtectiveCoatingNone;
                }
                return _recapitalizationCostLifeCycleTankProtectiveCoating;
            }
            set { ChangeAndNotify(ref _recapitalizationCostLifeCycleTankProtectiveCoating, value, nameof(RecapitalizationCostLifeCycleTankProtectiveCoating), CalcPropertiesStringArray); }
        }


        private double _recapitalizationCostLifeCycleTankProtectiveCoatingAlkyd;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostLifeCycleTankProtectiveCoatingAlkyd
        {
            get { return _recapitalizationCostLifeCycleTankProtectiveCoatingAlkyd; }
            set { ChangeAndNotify(ref _recapitalizationCostLifeCycleTankProtectiveCoatingAlkyd, value, nameof(RecapitalizationCostLifeCycleTankProtectiveCoatingAlkyd), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostLifeCycleTankProtectiveCoatingEpoxy;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostLifeCycleTankProtectiveCoatingEpoxy
        {
            get { return _recapitalizationCostLifeCycleTankProtectiveCoatingEpoxy; }
            set { ChangeAndNotify(ref _recapitalizationCostLifeCycleTankProtectiveCoatingEpoxy, value, nameof(RecapitalizationCostLifeCycleTankProtectiveCoatingEpoxy), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostLifeCycleTankProtectiveCoatingZincUrethane;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostLifeCycleTankProtectiveCoatingZincUrethane
        {
            get { return _recapitalizationCostLifeCycleTankProtectiveCoatingZincUrethane; }
            set { ChangeAndNotify(ref _recapitalizationCostLifeCycleTankProtectiveCoatingZincUrethane, value, nameof(RecapitalizationCostLifeCycleTankProtectiveCoatingZincUrethane), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostLifeCycleTankProtectiveCoatingNone;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostLifeCycleTankProtectiveCoatingNone
        {
            get { return _recapitalizationCostLifeCycleTankProtectiveCoatingNone; }
            set { ChangeAndNotify(ref _recapitalizationCostLifeCycleTankProtectiveCoatingNone, value, nameof(RecapitalizationCostLifeCycleTankProtectiveCoatingNone), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostLifeCycleCatwalk;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostLifeCycleCatwalk
        {
            get { return _recapitalizationCostLifeCycleCatwalk; }
            set { ChangeAndNotify(ref _recapitalizationCostLifeCycleCatwalk, value, nameof(RecapitalizationCostLifeCycleCatwalk), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostLifeCycleOverflowWeir;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostLifeCycleOverflowWeir
        {
            get { return _recapitalizationCostLifeCycleOverflowWeir; }
            set { ChangeAndNotify(ref _recapitalizationCostLifeCycleOverflowWeir, value, nameof(RecapitalizationCostLifeCycleOverflowWeir), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostLifeCycleSludgePumpRebuild;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostLifeCycleSludgePumpRebuild
        {
            get { return _recapitalizationCostLifeCycleSludgePumpRebuild; }
            set { ChangeAndNotify(ref _recapitalizationCostLifeCycleSludgePumpRebuild, value, nameof(RecapitalizationCostLifeCycleSludgePumpRebuild), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostLifeCycleSludgePumpReplacement;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostLifeCycleSludgePumpReplacement
        {
            get { return _recapitalizationCostLifeCycleSludgePumpReplacement; }
            set { ChangeAndNotify(ref _recapitalizationCostLifeCycleSludgePumpReplacement, value, nameof(RecapitalizationCostLifeCycleSludgePumpReplacement), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostLifeCycleSludgeDisposalBorehole;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostLifeCycleSludgeDisposalBorehole
        {
            get { return _recapitalizationCostLifeCycleSludgeDisposalBorehole; }
            set { ChangeAndNotify(ref _recapitalizationCostLifeCycleSludgeDisposalBorehole, value, nameof(RecapitalizationCostLifeCycleSludgeDisposalBorehole), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostLifeCycleClarifierCleanout;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostLifeCycleClarifierCleanout
        {
            get { return _recapitalizationCostLifeCycleClarifierCleanout; }
            set { ChangeAndNotify(ref _recapitalizationCostLifeCycleClarifierCleanout, value, nameof(RecapitalizationCostLifeCycleClarifierCleanout), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostLifeCycleSludgeRecirculationPumpRebuild;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostLifeCycleSludgeRecirculationPumpRebuild
        {
            get { return _recapitalizationCostLifeCycleSludgeRecirculationPumpRebuild; }
            set { ChangeAndNotify(ref _recapitalizationCostLifeCycleSludgeRecirculationPumpRebuild, value, nameof(RecapitalizationCostLifeCycleSludgeRecirculationPumpRebuild), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostLifeCycleSludgeRecirculationPumpReplacement;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostLifeCycleSludgeRecirculationPumpReplacement
        {
            get { return _recapitalizationCostLifeCycleSludgeRecirculationPumpReplacement; }
            set { ChangeAndNotify(ref _recapitalizationCostLifeCycleSludgeRecirculationPumpReplacement, value, nameof(RecapitalizationCostLifeCycleSludgeRecirculationPumpReplacement), CalcPropertiesStringArray); }
        }


        private double _recapitalizationCostPercentReplacementClarifierTankInternals;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostPercentReplacementClarifierTankInternals
        {
            get { return _recapitalizationCostPercentReplacementClarifierTankInternals; }
            set { ChangeAndNotify(ref _recapitalizationCostPercentReplacementClarifierTankInternals, value, nameof(RecapitalizationCostPercentReplacementClarifierTankInternals), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostPercentReplacementTankProtectiveCoating;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostPercentReplacementTankProtectiveCoating
        {
            get { return _recapitalizationCostPercentReplacementTankProtectiveCoating; }
            set { ChangeAndNotify(ref _recapitalizationCostPercentReplacementTankProtectiveCoating, value, nameof(RecapitalizationCostPercentReplacementTankProtectiveCoating), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostPercentReplacementCatwalk;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostPercentReplacementCatwalk
        {
            get { return _recapitalizationCostPercentReplacementCatwalk; }
            set { ChangeAndNotify(ref _recapitalizationCostPercentReplacementCatwalk, value, nameof(RecapitalizationCostPercentReplacementCatwalk), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostPercentReplacementOverflowWeir;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostPercentReplacementOverflowWeir
        {
            get { return _recapitalizationCostPercentReplacementOverflowWeir; }
            set { ChangeAndNotify(ref _recapitalizationCostPercentReplacementOverflowWeir, value, nameof(RecapitalizationCostPercentReplacementOverflowWeir), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostPercentReplacementSludgePumpRebuild;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostPercentReplacementSludgePumpRebuild
        {
            get { return _recapitalizationCostPercentReplacementSludgePumpRebuild; }
            set { ChangeAndNotify(ref _recapitalizationCostPercentReplacementSludgePumpRebuild, value, nameof(RecapitalizationCostPercentReplacementSludgePumpRebuild), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostPercentReplacementSludgePumpReplacement;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostPercentReplacementSludgePumpReplacement
        {
            get { return _recapitalizationCostPercentReplacementSludgePumpReplacement; }
            set { ChangeAndNotify(ref _recapitalizationCostPercentReplacementSludgePumpReplacement, value, nameof(RecapitalizationCostPercentReplacementSludgePumpReplacement), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostPercentReplacementSludgeDisposalBorehole;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostPercentReplacementSludgeDisposalBorehole
        {
            get { return _recapitalizationCostPercentReplacementSludgeDisposalBorehole; }
            set { ChangeAndNotify(ref _recapitalizationCostPercentReplacementSludgeDisposalBorehole, value, nameof(RecapitalizationCostPercentReplacementSludgeDisposalBorehole), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostPercentReplacementClarifierCleanout;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostPercentReplacementClarifierCleanout
        {
            get { return _recapitalizationCostPercentReplacementClarifierCleanout; }
            set { ChangeAndNotify(ref _recapitalizationCostPercentReplacementClarifierCleanout, value, nameof(RecapitalizationCostPercentReplacementClarifierCleanout), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostPercentReplacementSludgeRecirculationPumpRebuild;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostPercentReplacementSludgeRecirculationPumpRebuild
        {
            get { return _recapitalizationCostPercentReplacementSludgeRecirculationPumpRebuild; }
            set { ChangeAndNotify(ref _recapitalizationCostPercentReplacementSludgeRecirculationPumpRebuild, value, nameof(RecapitalizationCostPercentReplacementSludgeRecirculationPumpRebuild), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostPercentReplacementSludgeRecirculationPumpReplacement;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostPercentReplacementSludgeRecirculationPumpReplacement
        {
            get { return _recapitalizationCostPercentReplacementSludgeRecirculationPumpReplacement; }
            set { ChangeAndNotify(ref _recapitalizationCostPercentReplacementSludgeRecirculationPumpReplacement, value, nameof(RecapitalizationCostPercentReplacementSludgeRecirculationPumpReplacement), CalcPropertiesStringArray); }
        }

        private decimal _calcRecapitalizationCostClarifierTankInternalsMaterialCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostClarifierTankInternalsMaterialCost
        {
            get
            {
                return CalcClarifierTankCost + CalcClarifierInternalsCost;
            }
            set { ChangeAndNotify(ref _calcRecapitalizationCostClarifierTankInternalsMaterialCost, value); }
        }

        private decimal _calcRapitalizationCostClarifierTankInternals;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostClarifierTankInternals
        {
            get
            {
                return ClarifierCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
                                                                    RecapitalizationCostInflationRate, RecapitalizationCostLifeCycleClarifierTankInternals,
                                                                    CalcRecapitalizationCostClarifierTankInternalsMaterialCost, RecapitalizationCostPercentReplacementClarifierTankInternals);
            }
            set { ChangeAndNotify(ref _calcRapitalizationCostClarifierTankInternals, value); }
        }

        private decimal _calcRapitalizationCostCatwalk;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostCatwalk
        {
            get
            {
                return ClarifierCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
                                                                    RecapitalizationCostInflationRate, RecapitalizationCostLifeCycleCatwalk,
                                                                    CalcCatwalkCost, RecapitalizationCostPercentReplacementCatwalk);
            }
            set { ChangeAndNotify(ref _calcRapitalizationCostCatwalk, value); }
        }

        private decimal _calcRapitalizationCostOverflowWeir;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostOverflowWeir
        {
            get
            {
                return ClarifierCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
                                                                    RecapitalizationCostInflationRate, RecapitalizationCostLifeCycleOverflowWeir,
                                                                    CalcOverflowWeirCost, RecapitalizationCostPercentReplacementOverflowWeir);
            }
            set { ChangeAndNotify(ref _calcRapitalizationCostOverflowWeir, value); }
        }

        private decimal _calcRapitalizationCostTankProtectiveCoating;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostTankProtectiveCoating
        {
            get
            {
                return ClarifierCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
                                                                    RecapitalizationCostInflationRate, RecapitalizationCostLifeCycleTankProtectiveCoating,
                                                                    CalcTankProtectiveCoatingCost, RecapitalizationCostPercentReplacementTankProtectiveCoating);
            }
            set { ChangeAndNotify(ref _calcRapitalizationCostTankProtectiveCoating, value); }
        }


        private decimal _calcRecapitalizationCostSludgePumpRebuildMaterialCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostSludgePumpRebuildMaterialCost
        {
            get
            {
                return CalcSludgeDisposalPumpCost * (decimal)(RecapitalizationCostPercentReplacementSludgeRecirculationPumpRebuild / 100);
            }
            set { ChangeAndNotify(ref _calcRecapitalizationCostSludgePumpRebuildMaterialCost, value); }
        }

        private decimal _calcRapitalizationCostSludgePumpRebuild;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostSludgePumpRebuild
        {
            get
            {
                return ClarifierCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
                                                                    RecapitalizationCostInflationRate, RecapitalizationCostLifeCycleSludgePumpRebuild,
                                                                    CalcRecapitalizationCostSludgePumpRebuildMaterialCost, RecapitalizationCostPercentReplacementSludgePumpRebuild);
            }
            set { ChangeAndNotify(ref _calcRapitalizationCostSludgePumpRebuild, value); }
        }

        private decimal _calcRapitalizationCostSludgePumpReplacement;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostSludgePumpReplacement
        {
            get
            {
                return ClarifierCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
                                                                    RecapitalizationCostInflationRate, RecapitalizationCostLifeCycleSludgePumpReplacement,
                                                                    CalcSludgeDisposalPumpCost, RecapitalizationCostPercentReplacementSludgePumpReplacement);
            }
            set { ChangeAndNotify(ref _calcRapitalizationCostSludgePumpReplacement, value); }
        }

        private decimal _calcRapitalizationCostSludgeDisposalBorehole;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostSludgeDisposalBorehole
        {
            get
            {
                return ClarifierCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
                                                                    RecapitalizationCostInflationRate, RecapitalizationCostLifeCycleSludgeDisposalBorehole,
                                                                    CalcSludgeDisposalBoreholeCostAvg, RecapitalizationCostPercentReplacementSludgeDisposalBorehole);
            }
            set { ChangeAndNotify(ref _calcRapitalizationCostSludgeDisposalBorehole, value); }
        }

        private decimal _calcClarifierCleanoutCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcClarifierCleanoutCost
        {
            get { return 5000m; }
            set { ChangeAndNotify(ref _calcClarifierCleanoutCost, value); }
        }

        private decimal _calcRapitalizationCostClarifierCleanout;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostClarifierCleanout
        {
            get
            {
                return ClarifierCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
                                                                        RecapitalizationCostInflationRate, RecapitalizationCostLifeCycleClarifierCleanout,
                                                                        CalcClarifierCleanoutCost, RecapitalizationCostPercentReplacementClarifierCleanout);
            }
            set { ChangeAndNotify(ref _calcRapitalizationCostClarifierCleanout, value); }
        }

        private decimal _calcRecapitalizationCostSludgeRecirculationPumpRebuildMaterialCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostSludgeRecirculationPumpRebuildMaterialCost
        {
            get
            {
                return CalcSludgeRecirculationPumpCost * (decimal)(RecapitalizationCostPercentReplacementSludgeRecirculationPumpRebuild / 100);
            }
            set { ChangeAndNotify(ref _calcRecapitalizationCostSludgeRecirculationPumpRebuildMaterialCost, value); }
        }

        private decimal _calcRapitalizationCostSludgeRecirculationPumpRebuild;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostSludgeRecirculationPumpRebuild
        {
            get
            {
                return ClarifierCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
                                                                    RecapitalizationCostInflationRate, RecapitalizationCostLifeCycleSludgeRecirculationPumpRebuild,
                                                                    CalcRecapitalizationCostSludgeRecirculationPumpRebuildMaterialCost, RecapitalizationCostPercentReplacementSludgeRecirculationPumpRebuild);
            }
            set { ChangeAndNotify(ref _calcRapitalizationCostSludgeRecirculationPumpRebuild, value); }
        }

        private decimal _calcRapitalizationCostSludgeRecirculationPumpReplacement;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostSludgeRecirculationPumpReplacement
        {
            get
            {
                return ClarifierCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
                                                                    RecapitalizationCostInflationRate, RecapitalizationCostLifeCycleSludgeRecirculationPumpReplacement,
                                                                    CalcSludgeRecirculationPumpCost, RecapitalizationCostPercentReplacementSludgeRecirculationPumpReplacement);
            }
            set { ChangeAndNotify(ref _calcRapitalizationCostSludgeRecirculationPumpReplacement, value); }
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
                    case "ClarifierTankInternals":
                        item.MaterialCostDefault = CalcRecapitalizationCostClarifierTankInternalsMaterialCost;
                        break;
                    case "Catwalk":
                        item.MaterialCostDefault = CalcCatwalkCost;
                        break;
                    case "OverflowWeir":
                        item.MaterialCostDefault = CalcOverflowWeirCost;
                        break;
                    case "TankProtectiveCoating":
                        item.MaterialCostDefault = CalcTankProtectiveCoatingCost;
                        break;
                    case "SludgePumpRebuild":
                        item.MaterialCostDefault = CalcRecapitalizationCostSludgePumpRebuildMaterialCost;
                        break;
                    case "SludgePumpReplacement":
                        item.MaterialCostDefault = CalcSludgeDisposalPumpCost;
                        break;
                    case "SludgeDisposalBorehole":
                        item.MaterialCostDefault = CalcSludgeDisposalBoreholeCostAvg;
                        break; 
                    case "ClarifierCleanout":
                        item.MaterialCostDefault = CalcClarifierCleanoutCost;
                        break;
                    case "SludgeRecirculationPumpRebuild":
                        item.MaterialCostDefault = CalcRecapitalizationCostSludgeRecirculationPumpRebuildMaterialCost;
                        break;
                    case "SludgeRecirculationPumpReplacement":
                        item.MaterialCostDefault = CalcSludgeRecirculationPumpCost;
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
                    case "ClarifierTankInternals":
                        item.TotalCost = CalcRecapitalizationCostClarifierTankInternals;
                        break;
                    case "Catwalk":
                        item.TotalCost = CalcRecapitalizationCostCatwalk;
                        break;
                    case "OverflowWeir":
                        item.TotalCost = CalcRecapitalizationCostOverflowWeir;
                        break;
                    case "TankProtectiveCoating":
                        item.TotalCost = CalcRecapitalizationCostTankProtectiveCoating;
                        break;
                    case "SludgePumpRebuild":
                        item.TotalCost = CalcRecapitalizationCostSludgePumpRebuild;
                        break;
                    case "SludgePumpReplacement":
                        item.TotalCost = CalcRecapitalizationCostSludgePumpReplacement;
                        break;
                    case "SludgeDisposalBorehole":
                        item.TotalCost = CalcRecapitalizationCostSludgeDisposalBorehole;
                        break;
                    case "ClarifierCleanout":
                        item.TotalCost = CalcRecapitalizationCostClarifierCleanout;
                        break;
                    case "SludgeRecirculationPumpRebuild":
                        item.TotalCost = CalcRecapitalizationCostSludgeRecirculationPumpRebuild;
                        break;
                    case "SludgeRecirculationPumpReplacement":
                        item.TotalCost = CalcRecapitalizationCostSludgeRecirculationPumpReplacement;
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
                Name = "Clarifier Tank and Internals",
                NameFixed = "ClarifierTankInternals",
                LifeCycle = RecapitalizationCostLifeCycleClarifierTankInternals,
                PercentReplacement = RecapitalizationCostPercentReplacementClarifierTankInternals,
                MaterialCostDefault = CalcRecapitalizationCostClarifierTankInternalsMaterialCost,
                UseCustomCost = false,
                MaterialCostCustom = 0m,
                TotalCost = CalcRecapitalizationCostClarifierTankInternals
            });
            RecapMaterials.Add(new RecapMaterial
            {
                IsSelected = true,
                Name = "Catwalk",
                NameFixed = "Catwalk",
                LifeCycle = RecapitalizationCostLifeCycleCatwalk,
                PercentReplacement = RecapitalizationCostPercentReplacementCatwalk,
                MaterialCostDefault = CalcCatwalkCost,
                UseCustomCost = false,
                MaterialCostCustom = 0m,
                TotalCost = CalcRecapitalizationCostCatwalk
            });
            RecapMaterials.Add(new RecapMaterial
            {
                IsSelected = true,
                Name = "Overflow Weir",
                NameFixed = "OverflowWeir",
                LifeCycle = RecapitalizationCostLifeCycleOverflowWeir,
                PercentReplacement = RecapitalizationCostPercentReplacementOverflowWeir,
                MaterialCostDefault = CalcOverflowWeirCost,
                UseCustomCost = false,
                MaterialCostCustom = 0m,
                TotalCost = CalcRecapitalizationCostOverflowWeir
            });
            RecapMaterials.Add(new RecapMaterial
            {
                IsSelected = true,
                Name = "Tank Protective Coating",
                NameFixed = "TankProtectiveCoating",
                LifeCycle = RecapitalizationCostLifeCycleTankProtectiveCoating,
                PercentReplacement = RecapitalizationCostPercentReplacementTankProtectiveCoating,
                MaterialCostDefault = CalcTankProtectiveCoatingCost,
                UseCustomCost = false,
                MaterialCostCustom = 0m,
                TotalCost = CalcRecapitalizationCostTankProtectiveCoating
            });
            RecapMaterials.Add(new RecapMaterial
            {
                IsSelected = true,
                Name = "Sludge Pump Rebuild",
                NameFixed = "SludgePumpRebuild",
                LifeCycle = RecapitalizationCostLifeCycleSludgePumpRebuild,
                PercentReplacement = RecapitalizationCostPercentReplacementSludgePumpRebuild,
                MaterialCostDefault = CalcRecapitalizationCostSludgePumpRebuildMaterialCost,
                UseCustomCost = false,
                MaterialCostCustom = 0m,
                TotalCost = CalcRecapitalizationCostSludgePumpRebuild
            });
            RecapMaterials.Add(new RecapMaterial
            {
                IsSelected = true,
                Name = "Sludge Pump Replacement",
                NameFixed = "SludgePumpReplacement",
                LifeCycle = RecapitalizationCostLifeCycleSludgePumpReplacement,
                PercentReplacement = RecapitalizationCostPercentReplacementSludgePumpReplacement,
                MaterialCostDefault = CalcSludgeDisposalPumpCost,
                UseCustomCost = false,
                MaterialCostCustom = 0m,
                TotalCost = CalcRecapitalizationCostSludgePumpReplacement
            });
            RecapMaterials.Add(new RecapMaterial
            {
                IsSelected = true,
                Name = "Sludge Disposal Borehole",
                NameFixed = "SludgeDisposalBorehole",
                LifeCycle = RecapitalizationCostLifeCycleSludgeDisposalBorehole,
                PercentReplacement = RecapitalizationCostPercentReplacementSludgeDisposalBorehole,
                MaterialCostDefault = CalcSludgeDisposalBoreholeCostAvg,
                UseCustomCost = false,
                MaterialCostCustom = 0m,
                TotalCost = CalcRecapitalizationCostSludgeDisposalBorehole
            });
            RecapMaterials.Add(new RecapMaterial
            {
                IsSelected = true,
                Name = "Clarifier Cleanout",
                NameFixed = "ClarifierCleanout",
                LifeCycle = RecapitalizationCostLifeCycleClarifierCleanout,
                PercentReplacement = RecapitalizationCostPercentReplacementClarifierCleanout,
                MaterialCostDefault = CalcClarifierCleanoutCost,
                UseCustomCost = false,
                MaterialCostCustom = 0m,
                TotalCost = CalcRecapitalizationCostClarifierCleanout
            });
            RecapMaterials.Add(new RecapMaterial
            {
                IsSelected = true,
                Name = "Sludge Recirculation Pump Rebuild",
                NameFixed = "SludgeRecirculationPumpRebuild",
                LifeCycle = RecapitalizationCostLifeCycleSludgeRecirculationPumpRebuild,
                PercentReplacement = RecapitalizationCostPercentReplacementSludgeRecirculationPumpRebuild,
                MaterialCostDefault = CalcRecapitalizationCostSludgeRecirculationPumpRebuildMaterialCost,
                UseCustomCost = false,
                MaterialCostCustom = 0m,
                TotalCost = CalcRecapitalizationCostSludgeRecirculationPumpRebuild
            });
            RecapMaterials.Add(new RecapMaterial
            {
                IsSelected = true,
                Name = "Sludge Recirculation Pump Replacement",
                NameFixed = "SludgeRecirculationPumpReplacement",
                LifeCycle = RecapitalizationCostLifeCycleSludgeRecirculationPumpReplacement,
                PercentReplacement = RecapitalizationCostPercentReplacementSludgeRecirculationPumpReplacement,
                MaterialCostDefault = CalcSludgeRecirculationPumpCost,
                UseCustomCost = false,
                MaterialCostCustom = 0m,
                TotalCost = CalcRecapitalizationCostSludgeRecirculationPumpReplacement
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
            ((RecapMaterial)sender).TotalCost = ClarifierCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
                                                                                                 RecapitalizationCostInflationRate, ((RecapMaterial)sender).LifeCycle,
                                                                                                 materialCost, ((RecapMaterial)sender).PercentReplacement);

            // Update the total Recapitalization Cost
            CalcRecapitalizationCostTotal = CalcRecapMaterialsTotalCost();
        }

        private double _calcUpdateRecapClarifierTankInternalsLifeCycle;
        /// <summary>
        /// Wrapper for updating the life cycle for Recapitalization
        /// </summary>
        public double CalcUpdateRecapClarifierTankInternalsLifeCycle
        {
            get
            {
                UpdateRecapClarifierTankInternalsLifeCycle();
                return _calcUpdateRecapClarifierTankInternalsLifeCycle;
            }
            set { ChangeAndNotify(ref _calcUpdateRecapClarifierTankInternalsLifeCycle, value); }
        }

        /// <summary>
        /// Method to update the default material costs for Recapitalization
        /// </summary>
        public void UpdateRecapClarifierTankInternalsLifeCycle()
        {
            foreach (RecapMaterial item in RecapMaterials)
                switch (item.NameFixed)
                {
                    case "ClarifierTankInternals":
                        if (ClarifierConstructionMaterialName == ClarifierConstructionMaterialNameConcrete && TankProtectiveCoatingName == TankProtectiveCoatingNameNone)
                        {
                            //RecapitalizationCostLifeCycleClarifierTankInternals = RecapitalizationCostLifeCycleTankConcreteWithoutCoating;
                            item.LifeCycle = RecapitalizationCostLifeCycleTankConcreteWithoutCoating;
                        }
                        else if (ClarifierConstructionMaterialName == ClarifierConstructionMaterialNameConcrete && TankProtectiveCoatingName != TankProtectiveCoatingNameNone)
                        {
                            //RecapitalizationCostLifeCycleClarifierTankInternals = RecapitalizationCostLifeCycleTankConcreteWithCoating;
                            item.LifeCycle = RecapitalizationCostLifeCycleTankConcreteWithCoating;
                        }
                        else if (ClarifierConstructionMaterialName == ClarifierConstructionMaterialNameBoltedSteel && TankProtectiveCoatingName == TankProtectiveCoatingNameNone)
                        {
                            //RecapitalizationCostLifeCycleClarifierTankInternals = RecapitalizationCostLifeCycleTankSteelWithoutCoating;
                            item.LifeCycle = RecapitalizationCostLifeCycleTankSteelWithoutCoating;
                        }
                        else if (ClarifierConstructionMaterialName == ClarifierConstructionMaterialNameBoltedSteel && TankProtectiveCoatingName != TankProtectiveCoatingNameNone)
                        {
                            //RecapitalizationCostLifeCycleClarifierTankInternals = RecapitalizationCostLifeCycleTankSteelWithCoating;
                            item.LifeCycle = RecapitalizationCostLifeCycleTankSteelWithCoating;
                        }
                        else if (ClarifierConstructionMaterialName == ClarifierConstructionMaterialNameWeldedSteel && TankProtectiveCoatingName == TankProtectiveCoatingNameNone)
                        {
                            //RecapitalizationCostLifeCycleClarifierTankInternals = RecapitalizationCostLifeCycleTankSteelWithoutCoating;
                            item.LifeCycle = RecapitalizationCostLifeCycleTankSteelWithoutCoating;
                        }
                        else // (ClarifierConstructionMaterialName == ClarifierConstructionMaterialNameWeldedSteel && TankProtectiveCoatingName != TankProtectiveCoatingNameNone)
                        {
                            //RecapitalizationCostLifeCycleClarifierTankInternals = RecapitalizationCostLifeCycleTankSteelWithCoating;
                            item.LifeCycle = RecapitalizationCostLifeCycleTankSteelWithCoating;
                        }
                        break;
                    case "TankProtectiveCoating":
                        if (TankProtectiveCoatingName == TankProtectiveCoatingNameAlkyd)
                        {
                            item.LifeCycle = RecapitalizationCostLifeCycleTankProtectiveCoatingAlkyd;
                        }
                        else if (TankProtectiveCoatingName == TankProtectiveCoatingNameEpoxy)
                        {
                            item.LifeCycle = RecapitalizationCostLifeCycleTankProtectiveCoatingEpoxy;
                        }
                        else if (TankProtectiveCoatingName == TankProtectiveCoatingNameZincUrethane)
                        {
                            item.LifeCycle = RecapitalizationCostLifeCycleTankProtectiveCoatingZincUrethane;
                        }
                        else //(TankProtectiveCoatingName == TankProtectiveCoatingNameNone)
                        {
                            item.LifeCycle = RecapitalizationCostLifeCycleTankProtectiveCoatingNone;
                        }
                        break;
                }
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
        public ICommand SetClarifierConstructionMaterialCommand { get; }
        public ICommand SetFoundationSiteSoilCommand { get; }
        public ICommand SetEstimatedIronPipeSizeStandardDiameterRatioNumberCommand { get; }
        public ICommand SetNominalOutsideDiameterCommand { get; }
        public ICommand SetTankProtectiveCoatingCommand { get; }
        public ICommand SetBorehole1InsideDiameterCommand { get; }
        public ICommand SetBorehole2InsideDiameterCommand { get; }
        public ICommand SetBorehole3InsideDiameterCommand { get; }
        public ICommand SetPolymerCommand { get; }
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
            var customDialog = new CustomDialog() { Title = "About Clarifier" };

            var customDialogViewModel = new CustomDialogViewModel(instance =>
            {
                _dialogCoordinator.HideMetroDialogAsync(this, customDialog);
            });
            customDialogViewModel.Message = Resources.infoClarifier;
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
                        string message = Resources.infoWaterQualityClarifier;
                        await _dialogCoordinator.ShowMessageAsync(this, "Water Quality", message);
                    }
                });
            }
        }


        private ICommand _showMessageDialogCommandClarifierDesign;
        public ICommand ShowMessageDialogCommandClarifierDesign
        {
            get
            {
                return _showMessageDialogCommandClarifierDesign ?? (this._showMessageDialogCommandClarifierDesign = new SimpleCommand
                {
                    CanExecuteDelegate = x => true,
                    ExecuteDelegate = async x =>
                    {
                        string message = Resources.infoClarifierDesignClarifier;
                        await _dialogCoordinator.ShowMessageAsync(this, "Clarifier Design", message);
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
                        string message = Resources.infoEquipmentClarifier;
                        await _dialogCoordinator.ShowMessageAsync(this, "Equipment", message);
                    }
                });
            }
        }

        private ICommand _showMessageDialogCommandSludgeDisposalPipeline;
        public ICommand ShowMessageDialogCommandSludgeDisposalPipeline
        {
            get
            {
                return _showMessageDialogCommandSludgeDisposalPipeline ?? (this._showMessageDialogCommandSludgeDisposalPipeline = new SimpleCommand
                {
                    CanExecuteDelegate = x => true,
                    ExecuteDelegate = async x =>
                    {
                        string message = Resources.infoSludgeDisposalPipelineClarifier;
                        await _dialogCoordinator.ShowMessageAsync(this, "Sludge Disposal Pipeline", message);
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
                        string message = Resources.infoOtherItemsCapitalClarifier;
                        await _dialogCoordinator.ShowMessageAsync(this, "Other Capital Items", message);
                    }
                });
            }
        }

        private ICommand _showMessageDialogCommandAnnualCostInput;
        public ICommand ShowMessageDialogCommandAnnualCostInput
        {
            get
            {
                return _showMessageDialogCommandAnnualCostInput ?? (this._showMessageDialogCommandAnnualCostInput = new SimpleCommand
                {
                    CanExecuteDelegate = x => true,
                    ExecuteDelegate = async x =>
                    {
                        string message = Resources.infoAnnualCostInputClarifier;
                        await _dialogCoordinator.ShowMessageAsync(this, "Annual Cost Input", message);
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
                        string message = Resources.infoOtherItemsAnnualClarifier;
                        await _dialogCoordinator.ShowMessageAsync(this, "Other Annual Items", message);
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
                        string message = Resources.infoSizingSummaryClarifier;
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
                        string message = Resources.infoCapitalCostClarifier;
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
                        string message = Resources.infoAnnualCostClarifier;
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
                        string message = Resources.infoRecapitalizationCostClarifier;
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
                case "1.5":
                    NominalPipeOutsideDiameterInchesName = NominalPipeOutsideDiameterInchesName15;
                    NominalPipeOutsideDiameterInches = NominalPipeOutsideDiameterInches15;
                    break;
                case "2":
                    NominalPipeOutsideDiameterInchesName = NominalPipeOutsideDiameterInchesName2;
                    NominalPipeOutsideDiameterInches = NominalPipeOutsideDiameterInches2;
                    break;
                case "3":
                    NominalPipeOutsideDiameterInchesName = NominalPipeOutsideDiameterInchesName3;
                    NominalPipeOutsideDiameterInches = NominalPipeOutsideDiameterInches3;
                    break;
                case "4":
                    NominalPipeOutsideDiameterInchesName = NominalPipeOutsideDiameterInchesName4;
                    NominalPipeOutsideDiameterInches = NominalPipeOutsideDiameterInches4;
                    break;
                case "6":
                    NominalPipeOutsideDiameterInchesName = NominalPipeOutsideDiameterInchesName6;
                    NominalPipeOutsideDiameterInches = NominalPipeOutsideDiameterInches6;
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

        public void SetFoundationSiteSoil(object foundationSiteSoil)
        {
            FoundationSiteSoil itemFoundationSiteSoil = (FoundationSiteSoil)foundationSiteSoil;

            switch (itemFoundationSiteSoil.Name)
            {
                case "Load Bearing 1500":
                    FoundationSiteSoilLoadBearingName = FoundationSiteSoilLoadBearing1500Name;
                    FoundationSiteSoilLoadBearingQuantity = FoundationSiteSoilLoadBearing1500Quantity;
                    FoundationSiteSoilLoadBearingMultiplier = FoundationSiteSoilLoadBearing1500Multiplier;
                    FoundationSiteSoilLoadBearingRating = FoundationSiteSoilLoadBearing1500Rating;
                    break;
                case "Load Bearing 3000":
                    FoundationSiteSoilLoadBearingName = FoundationSiteSoilLoadBearing3000Name;
                    FoundationSiteSoilLoadBearingQuantity = FoundationSiteSoilLoadBearing3000Quantity;
                    FoundationSiteSoilLoadBearingMultiplier = FoundationSiteSoilLoadBearing3000Multiplier;
                    FoundationSiteSoilLoadBearingRating = FoundationSiteSoilLoadBearing3000Rating;
                    break;
                case "Load Bearing 4500":
                    FoundationSiteSoilLoadBearingName = FoundationSiteSoilLoadBearing4500Name;
                    FoundationSiteSoilLoadBearingQuantity = FoundationSiteSoilLoadBearing4500Quantity;
                    FoundationSiteSoilLoadBearingMultiplier = FoundationSiteSoilLoadBearing4500Multiplier;
                    FoundationSiteSoilLoadBearingRating = FoundationSiteSoilLoadBearing4500Rating;
                    break;
                default:
                    break;
            }
        }

        public void SetClarifierConstructionMaterial(object clarifierConstructionMaterial)
        {
            GeneralCostItem itemClarifierConstructionMaterial = (GeneralCostItem)clarifierConstructionMaterial;

            switch (itemClarifierConstructionMaterial.Name)
            {

                case "Concrete":
                    ClarifierConstructionMaterialName = ClarifierConstructionMaterialNameConcrete;
                    ClarifierConstructionMaterialCostFactor = ClarifierConstructionMaterialCostFactorConcrete;
                    TankProtectiveCoatings = TankProtectiveCoatingsConcrete;
                    if (TankProtectiveCoatingName == TankProtectiveCoatingNameAlkyd || TankProtectiveCoatingName == TankProtectiveCoatingNameZincUrethane)
                    {
                        TankProtectiveCoatingName = TankProtectiveCoatingNameEpoxy;
                        TankProtectiveCoatingUnitCost = TankProtectiveCoatingUnitCostEpoxy;
                    }
                        break;
                case "Bolted Steel":
                    ClarifierConstructionMaterialName = ClarifierConstructionMaterialNameBoltedSteel;
                    ClarifierConstructionMaterialCostFactor = ClarifierConstructionMaterialCostFactorBoltedSteel;
                    TankProtectiveCoatings = TankProtectiveCoatingsSteel;
                    break;
                case "Welded Steel":
                    ClarifierConstructionMaterialName = ClarifierConstructionMaterialNameWeldedSteel;
                    ClarifierConstructionMaterialCostFactor = ClarifierConstructionMaterialCostFactorWeldedSteel;
                    TankProtectiveCoatings = TankProtectiveCoatingsSteel;
                    break;
                default:
                    break;
            }
        }

        public void SetTankProtectiveCoating(object tankProtectiveCoating)
        {
            GeneralCostItem itemTankProtectiveCoating = (GeneralCostItem)tankProtectiveCoating;

            switch (itemTankProtectiveCoating.Name)
            {
                case "Alkyd/Alkyd/Alkyd":
                    TankProtectiveCoatingName = TankProtectiveCoatingNameAlkyd;
                    TankProtectiveCoatingUnitCost = TankProtectiveCoatingUnitCostAlkyd;
                    break;
                case "Epoxy/Epoxy/Epoxy":
                    TankProtectiveCoatingName = TankProtectiveCoatingNameEpoxy;
                    TankProtectiveCoatingUnitCost = TankProtectiveCoatingUnitCostEpoxy;
                    break;
                case "Zinc/Urethane":
                    TankProtectiveCoatingName = TankProtectiveCoatingNameZincUrethane;
                    TankProtectiveCoatingUnitCost = TankProtectiveCoatingUnitCostZincUrethane;
                    break;
                case "None":
                    TankProtectiveCoatingName = TankProtectiveCoatingNameNone;
                    TankProtectiveCoatingUnitCost = TankProtectiveCoatingUnitCostNone;
                    break;
                default:
                    break;
            }
        }

        public void SetBorehole1InsideDiameter(object borehole1InsideDiameter)
        {
            Borehole itemBorehole1InsideDiameter = (Borehole)borehole1InsideDiameter;

            switch (itemBorehole1InsideDiameter.Name)
            {
                case "6":
                    Borehole1InsideDiameterName = Borehole1InsideDiameterName6;
                    Borehole1InsideDiameter = Borehole1InsideDiameter6;
                    Borehole1DrillingAndCastingInstallationUnitCost = Borehole1DrillingAndCastingInstallationUnitCost6;
                    break;
                case "8":
                    Borehole1InsideDiameterName = Borehole1InsideDiameterName8;
                    Borehole1InsideDiameter = Borehole1InsideDiameter8;
                    Borehole1DrillingAndCastingInstallationUnitCost = Borehole1DrillingAndCastingInstallationUnitCost8;
                    break;
                default:
                    break;
            }
        }

        public void SetBorehole2InsideDiameter(object borehole2InsideDiameter)
        {
            Borehole itemBorehole2InsideDiameter = (Borehole)borehole2InsideDiameter;

            switch (itemBorehole2InsideDiameter.Name)
            {
                case "6":
                    Borehole2InsideDiameterName = Borehole2InsideDiameterName6;
                    Borehole2InsideDiameter = Borehole2InsideDiameter6;
                    Borehole2DrillingAndCastingInstallationUnitCost = Borehole2DrillingAndCastingInstallationUnitCost6;
                    break;
                case "8":
                    Borehole2InsideDiameterName = Borehole2InsideDiameterName8;
                    Borehole2InsideDiameter = Borehole2InsideDiameter8;
                    Borehole2DrillingAndCastingInstallationUnitCost = Borehole2DrillingAndCastingInstallationUnitCost8;
                    break;
                default:
                    break;
            }
        }

        public void SetBorehole3InsideDiameter(object borehole3InsideDiameter)
        {
            Borehole itemBorehole3InsideDiameter = (Borehole)borehole3InsideDiameter;

            switch (itemBorehole3InsideDiameter.Name)
            {
                case "6":
                    Borehole3InsideDiameterName = Borehole3InsideDiameterName6;
                    Borehole3InsideDiameter = Borehole3InsideDiameter6;
                    Borehole3DrillingAndCastingInstallationUnitCost = Borehole3DrillingAndCastingInstallationUnitCost6;
                    break;
                case "8":
                    Borehole3InsideDiameterName = Borehole3InsideDiameterName8;
                    Borehole3InsideDiameter = Borehole3InsideDiameter8;
                    Borehole3DrillingAndCastingInstallationUnitCost = Borehole3DrillingAndCastingInstallationUnitCost8;
                    break;
                default:
                    break;
            }
        }

        public void SetPolymer(object polymer)
        {
            Polymer itemPolymer = (Polymer)polymer;

            switch (itemPolymer.Name)
            {
                case "Emulsion":
                    PolymerName = PolymerNameEmulsion;
                    PolymerActivePercentage = PolymerActivePercentageEmulsion;
                    PolymerSolutionStrength = PolymerSolutionStrengthEmulsion;
                    PolymerUnitCost = PolymerUnitCostEmulsion;
                    IsEmulsionPolymer = true;
                    break;
                case "Dry":
                    PolymerName = PolymerNameDry;
                    PolymerActivePercentage = PolymerActivePercentageDry;
                    PolymerSolutionStrength = PolymerSolutionStrengthDry;
                    PolymerUnitCost = PolymerUnitCostDry;
                    IsEmulsionPolymer = false;
                    break;
                default:
                    break;
            }
        }

        public void SetGeotubeSize(object geotubeSize)
        {
            Geotube itemGeotubeSize = (Geotube)geotubeSize;

            switch (itemGeotubeSize.Name)
            {
                case "22.5' x 22'":
                    GeotubeSizeName = GeotubeSizeName22_5x22;
                    GeotubeCapacity = GeotubeCapacity22_5x22;                    
                    GeotubeUnitCost = GeotubeUnitCost22_5x22;
                    break;
                case "30' x 50'":
                    GeotubeSizeName = GeotubeSizeName30x50;
                    GeotubeCapacity = GeotubeCapacity30x50;
                    GeotubeUnitCost = GeotubeUnitCost30x50;
                    break;
                case "45' x 57'":
                    GeotubeSizeName = GeotubeSizeName45x57;
                    GeotubeCapacity = GeotubeCapacity45x57;
                    GeotubeUnitCost = GeotubeUnitCost45x57;
                    break;
                case "45' x 100'":
                    GeotubeSizeName = GeotubeSizeName45x100;
                    GeotubeCapacity = GeotubeCapacity45x100;
                    GeotubeUnitCost = GeotubeUnitCost45x100;
                    break;
                case "60' x 100'":
                    GeotubeSizeName = GeotubeSizeName60x100;
                    GeotubeCapacity = GeotubeCapacity60x100;
                    GeotubeUnitCost = GeotubeUnitCost60x100;
                    break;
                case "60' x 200'":
                    GeotubeSizeName = GeotubeSizeName60x200;
                    GeotubeCapacity = GeotubeCapacity60x200;
                    GeotubeUnitCost = GeotubeUnitCost60x200;
                    break;
                case "120' x 100'":
                    GeotubeSizeName = GeotubeSizeName120x100;
                    GeotubeCapacity = GeotubeCapacity120x100;
                    GeotubeUnitCost = GeotubeUnitCost120x100;
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

        public ClarifierViewModel(IDialogCoordinator dialogCoordinator)
        {
            // MahApps dialog coordinator
            _dialogCoordinator = dialogCoordinator;

            // Assign the proper functions to the open and save commands
            OpenCommand = new RelayCommand(OpenFile);
            SaveCommand = new RelayCommand(SaveFile);
            HelpCommand = new RelayCommand(ShowHelp);
            SyncCommand = new RelayCommand(SyncWithMainUi);

            SetClarifierConstructionMaterialCommand = new RelayCommandWithParameter(SetClarifierConstructionMaterial);
            SetFoundationSiteSoilCommand = new RelayCommandWithParameter(SetFoundationSiteSoil);
            SetEstimatedIronPipeSizeStandardDiameterRatioNumberCommand = new RelayCommandWithParameter(SetEstimatedIronPipeSizeStandardDiameterRatioNumber);
            SetNominalOutsideDiameterCommand = new RelayCommandWithParameter(SetNominalOutsideDiameter);
            SetTankProtectiveCoatingCommand = new RelayCommandWithParameter(SetTankProtectiveCoating);
            SetBorehole1InsideDiameterCommand = new RelayCommandWithParameter(SetBorehole1InsideDiameter);
            SetBorehole2InsideDiameterCommand = new RelayCommandWithParameter(SetBorehole2InsideDiameter);
            SetBorehole3InsideDiameterCommand = new RelayCommandWithParameter(SetBorehole3InsideDiameter);
            SetPolymerCommand = new RelayCommandWithParameter(SetPolymer);
            SetGeotubeCommand = new RelayCommandWithParameter(SetGeotubeSize);

            // Get a list of property names and filter the names to include only those that start with "Calc" in order
            // to use with the NotifyAndChange.  This eliminates the need to write every property name that needs 
            // to be notified of changes by the user.
            PropertiesStringList = Helpers.GetNamesOfClassProperties(this);
            CalcPropertiesStringArray = Helpers.FilterPropertiesList(PropertiesStringList, "Calc");

            // Initialize the model name and user name
            ModuleType = "Clarifier";
            ModuleName = "Untitled module";
            ModuleTreatmentType = "Project";
            ModuleId = random.Next(1000);

            IsError = false;
            IsMajorError = false;
            ErrorMessage = "";
            ErrorMessageShort = "";

            // Initialize check boxes
            IsDensityCurrentBaffle = true;
            IsSludgeRecirculationPump = true;
            IsAirVacuumReleaseAssemblies = true;
            IsPigLaunchersReceiversAssemblies = true;
            IsBorehole1 = true;
            IsBorehole2 = true;
            IsBorehole3 = true;
            IsPolymer = true;
            IsEmulsionPolymer = true;
            IsLandfillTippingFee = true;

            // Initialize radio buttons
            ClarifierDesignOptionsProperty = RadioButtonsClarifierDesignOptionsEnum.OptionConventional;
            ImpellerMotorOptionsProperty = RadioButtonsImpellerMotorPowerOptionsEnum.OptionEstimate;
            SludgeRecirculationPumpOptionsProperty = RadioButtonsSludgeRecirculationPumpOptionsEnum.OptionEstimate;
            SludgeDisposalPumpOptionsProperty = RadioButtonsSludgeDisposalPumpOptionsEnum.OptionEstimate;
            SludgeDisposalOptionsProperty = RadioButtonsSludgeDisposalOptionsEnum.OptionBorehole;

            CapitalCostSystemInstallOptionsProperty = RadioButtonsCapitalCostSystemInstallOptionsEnum.OptionCostMultiplier;

            AnnualCostOperationAndMaintenanceOptionsProperty = RadioButtonsAnnualCostOperationAndMaintenanceOptionsEnum.OptionAnnualCostMultiplier;
            AnnualCostElectricOptionsProperty = RadioButtonsAnnualCostElectricOptionsEnum.OptionAnnualCostEstimated;
            AnnualCostGeotubeSludgeDisposalOptionsProperty = RadioButtonsAnnualCostGeotubeSludgeDisposalOptionsEnum.OptionAnnualCostEstimated;

            // Dummy variables to force syncing
            Dummy = 0;

            // Read the xml data file and assign property values accordingly.
            OpenXmlAndAssignValues(@"..\..\Data\default-data-clarifier.xml");

            // Clarifier Construction Material
            ClarifierConstructionMaterialName = ClarifierConstructionMaterialNameConcrete;
            ClarifierConstructionMaterialCostFactor = ClarifierConstructionMaterialCostFactorConcrete;

            ClarifierConstructionMaterials = new List<GeneralCostItem>
            {
                new GeneralCostItem {Name = ClarifierConstructionMaterialNameConcrete, Cost = ClarifierConstructionMaterialCostFactorConcrete },
                new GeneralCostItem {Name = ClarifierConstructionMaterialNameBoltedSteel, Cost = ClarifierConstructionMaterialCostFactorBoltedSteel },
                new GeneralCostItem {Name = ClarifierConstructionMaterialNameWeldedSteel, Cost = ClarifierConstructionMaterialCostFactorWeldedSteel },
            };

            // Tank Protective Coatings
            TankProtectiveCoatingName = TankProtectiveCoatingNameEpoxy;
            TankProtectiveCoatingUnitCost = TankProtectiveCoatingUnitCostEpoxy;

            TankProtectiveCoatingsSteel = new List<GeneralCostItem>
            {
                new GeneralCostItem {Name = TankProtectiveCoatingNameAlkyd, Cost = TankProtectiveCoatingUnitCostAlkyd },
                new GeneralCostItem {Name = TankProtectiveCoatingNameEpoxy, Cost = TankProtectiveCoatingUnitCostEpoxy },
                new GeneralCostItem {Name = TankProtectiveCoatingNameZincUrethane, Cost = TankProtectiveCoatingUnitCostZincUrethane },
                new GeneralCostItem {Name = TankProtectiveCoatingNameNone, Cost = TankProtectiveCoatingUnitCostNone },
            };

            TankProtectiveCoatingsConcrete = new List<GeneralCostItem>
            {
                new GeneralCostItem {Name = TankProtectiveCoatingNameEpoxy, Cost = TankProtectiveCoatingUnitCostEpoxy },
                new GeneralCostItem {Name = TankProtectiveCoatingNameNone, Cost = TankProtectiveCoatingUnitCostNone },
            };

            // Foundation Site Soils
            FoundationSiteSoilLoadBearingName = FoundationSiteSoilLoadBearing1500Name;
            FoundationSiteSoilLoadBearingQuantity = FoundationSiteSoilLoadBearing1500Quantity;
            FoundationSiteSoilLoadBearingMultiplier = FoundationSiteSoilLoadBearing1500Multiplier;
            FoundationSiteSoilLoadBearingRating = FoundationSiteSoilLoadBearing1500Rating;

            FoundationSiteSoils = new List<FoundationSiteSoil>
            {
                new FoundationSiteSoil {Name = FoundationSiteSoilLoadBearing1500Name, Rating = FoundationSiteSoilLoadBearing1500Rating, Multiplier = FoundationSiteSoilLoadBearing1500Multiplier, Quantity = FoundationSiteSoilLoadBearing1500Quantity },
                new FoundationSiteSoil {Name = FoundationSiteSoilLoadBearing3000Name, Rating = FoundationSiteSoilLoadBearing3000Rating, Multiplier = FoundationSiteSoilLoadBearing3000Multiplier, Quantity = FoundationSiteSoilLoadBearing3000Quantity },
                new FoundationSiteSoil {Name = FoundationSiteSoilLoadBearing4500Name, Rating = FoundationSiteSoilLoadBearing4500Rating, Multiplier = FoundationSiteSoilLoadBearing4500Multiplier, Quantity = FoundationSiteSoilLoadBearing4500Quantity },
            };

            // Nominal Pipe Outside Diameter
            NominalPipeOutsideDiameterInchesName15 = NominalPipeOutsideDiameterInches15.ToString();
            NominalPipeOutsideDiameterInchesName2 = NominalPipeOutsideDiameterInches2.ToString();
            NominalPipeOutsideDiameterInchesName3 = NominalPipeOutsideDiameterInches3.ToString();
            NominalPipeOutsideDiameterInchesName4 = NominalPipeOutsideDiameterInches4.ToString();
            NominalPipeOutsideDiameterInchesName6 = NominalPipeOutsideDiameterInches6.ToString();

            NominalPipeOutsideDiameterInchesName = NominalPipeOutsideDiameterInchesName6;
            NominalPipeOutsideDiameterInches = NominalPipeOutsideDiameterInches6;

            NominalPipeOutsideDiameterInchesList = new List<GeneralItem>
            {
                new GeneralItem {Name = NominalPipeOutsideDiameterInchesName15, Value = NominalPipeOutsideDiameterInches15},
                new GeneralItem {Name = NominalPipeOutsideDiameterInchesName2, Value = NominalPipeOutsideDiameterInches2},
                new GeneralItem {Name = NominalPipeOutsideDiameterInchesName3, Value = NominalPipeOutsideDiameterInches3},
                new GeneralItem {Name = NominalPipeOutsideDiameterInchesName4, Value = NominalPipeOutsideDiameterInches4},
                new GeneralItem {Name = NominalPipeOutsideDiameterInchesName6, Value = NominalPipeOutsideDiameterInches6},
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

            // Boreholes
            Borehole1InsideDiameterName = Borehole1InsideDiameterName6;
            Borehole1InsideDiameter = Borehole1InsideDiameter6;
            Borehole1DrillingAndCastingInstallationUnitCost = Borehole1DrillingAndCastingInstallationUnitCost6;

            Borehole2InsideDiameterName = Borehole2InsideDiameterName6;
            Borehole2InsideDiameter = Borehole2InsideDiameter6;
            Borehole2DrillingAndCastingInstallationUnitCost = Borehole2DrillingAndCastingInstallationUnitCost6;

            Borehole3InsideDiameterName = Borehole3InsideDiameterName6;
            Borehole3InsideDiameter = Borehole3InsideDiameter6;
            Borehole3DrillingAndCastingInstallationUnitCost = Borehole3DrillingAndCastingInstallationUnitCost6;

            Borehole1InsideDiameters = new List<Borehole>
            {
                new Borehole {Name = Borehole1InsideDiameterName6, Diameter = Borehole1InsideDiameter6, Cost = Borehole1DrillingAndCastingInstallationUnitCost6 },
                new Borehole {Name = Borehole1InsideDiameterName8, Diameter = Borehole1InsideDiameter8, Cost = Borehole1DrillingAndCastingInstallationUnitCost8 }
            };

            Borehole2InsideDiameters = new List<Borehole>
            {
                new Borehole {Name = Borehole2InsideDiameterName6, Diameter = Borehole2InsideDiameter6, Cost = Borehole2DrillingAndCastingInstallationUnitCost6 },
                new Borehole {Name = Borehole2InsideDiameterName8, Diameter = Borehole2InsideDiameter8, Cost = Borehole2DrillingAndCastingInstallationUnitCost8 }
            };

            Borehole3InsideDiameters = new List<Borehole>
            {
                new Borehole {Name = Borehole3InsideDiameterName6, Diameter = Borehole3InsideDiameter6, Cost = Borehole3DrillingAndCastingInstallationUnitCost6 },
                new Borehole {Name = Borehole3InsideDiameterName8, Diameter = Borehole3InsideDiameter8, Cost = Borehole3DrillingAndCastingInstallationUnitCost8 }
            };

            // Geotube
            GeotubeSizeName = GeotubeSizeName30x50;
            GeotubeCapacity = GeotubeCapacity30x50;
            GeotubeUnitCost = GeotubeUnitCost30x50;

            Geotubes = new List<Geotube>
            {
                new Geotube {Name = GeotubeSizeName22_5x22, Capacity = GeotubeCapacity22_5x22, Cost = GeotubeUnitCost22_5x22 },
                new Geotube {Name = GeotubeSizeName30x50, Capacity = GeotubeCapacity30x50, Cost = GeotubeUnitCost30x50 },
                new Geotube {Name = GeotubeSizeName45x57, Capacity = GeotubeCapacity45x57, Cost = GeotubeUnitCost45x57 },
                new Geotube {Name = GeotubeSizeName45x100, Capacity = GeotubeCapacity45x100, Cost = GeotubeUnitCost45x100 },
                new Geotube {Name = GeotubeSizeName60x100, Capacity = GeotubeCapacity60x100, Cost = GeotubeUnitCost60x100 },
                new Geotube {Name = GeotubeSizeName60x200, Capacity = GeotubeCapacity60x200, Cost = GeotubeUnitCost60x200 },
                new Geotube {Name = GeotubeSizeName120x100, Capacity = GeotubeCapacity120x100, Cost = GeotubeUnitCost120x100 },
            };

            // Polymer
            PolymerName = PolymerNameEmulsion;
            PolymerActivePercentage = PolymerActivePercentageEmulsion;
            PolymerSolutionStrength = PolymerSolutionStrengthEmulsion;
            PolymerUnitCost = PolymerUnitCostEmulsion;

            Polymers = new List<Polymer>
            {
                new Polymer {Name = PolymerNameEmulsion, PercentActive = PolymerActivePercentageEmulsion, SolutionStrength = PolymerSolutionStrengthEmulsion, Cost = PolymerUnitCostEmulsion },
                new Polymer {Name = PolymerNameDry, PercentActive = PolymerActivePercentageDry, SolutionStrength = PolymerSolutionStrengthDry, Cost = PolymerUnitCostDry },
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
