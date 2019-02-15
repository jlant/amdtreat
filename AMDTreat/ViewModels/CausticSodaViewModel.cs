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

    public class CausticSodaViewModel : PropertyChangedBase, IObserver<SharedData>
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

        private double _typicalFlow;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double TypicalFlow
        {
            get { return _typicalFlow; }
            set { ChangeAndNotify(ref _typicalFlow, value, nameof(TypicalFlow), CalcPropertiesStringArray); }
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

        #region Properties - Caustic Soda Information

        private List<CausticSodaSolution> _causticSodaSolutions;
        /// <summary>
        /// Collection used for the stone gradations dropdown menu.
        /// </summary>
        public List<CausticSodaSolution> CausticSodaSolutions
        {
            get { return _causticSodaSolutions; }

            set { ChangeAndNotify(ref _causticSodaSolutions, value, nameof(CausticSodaSolutions), CalcPropertiesStringArray); }
        }

        private string _causticSodaSolutionName;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string CausticSodaSolutionName
        {
            get { return _causticSodaSolutionName; }
            set { ChangeAndNotify(ref _causticSodaSolutionName, value, nameof(CausticSodaSolutionName), CalcPropertiesStringArray); }
        }

        private string _causticSodaSolutionName20;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string CausticSodaSolutionName20
        {
            get { return _causticSodaSolutionName20; }
            set { ChangeAndNotify(ref _causticSodaSolutionName20, value, nameof(CausticSodaSolutionName20), CalcPropertiesStringArray); }
        }

        private string _causticSodaSolutionName25;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string CausticSodaSolutionName25
        {
            get { return _causticSodaSolutionName25; }
            set { ChangeAndNotify(ref _causticSodaSolutionName25, value, nameof(CausticSodaSolutionName25), CalcPropertiesStringArray); }
        }

        private string _causticSodaSolutionName50;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string CausticSodaSolutionName50
        {
            get { return _causticSodaSolutionName50; }
            set { ChangeAndNotify(ref _causticSodaSolutionName50, value, nameof(CausticSodaSolutionName50), CalcPropertiesStringArray); }
        }

        private double _causticSodaSolutionPercentage;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double CausticSodaSolutionPercentage
        {
            get { return _causticSodaSolutionPercentage; }
            set { ChangeAndNotify(ref _causticSodaSolutionPercentage, value, nameof(CausticSodaSolutionPercentage), CalcPropertiesStringArray); }
        }

        private double _causticSodaSolutionPercentage20;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double CausticSodaSolutionPercentage20
        {
            get { return _causticSodaSolutionPercentage20; }
            set { ChangeAndNotify(ref _causticSodaSolutionPercentage20, value, nameof(CausticSodaSolutionPercentage20), CalcPropertiesStringArray); }
        }

        private double _causticSodaSolutionPercentage25;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double CausticSodaSolutionPercentage25
        {
            get { return _causticSodaSolutionPercentage25; }
            set { ChangeAndNotify(ref _causticSodaSolutionPercentage25, value, nameof(CausticSodaSolutionPercentage25), CalcPropertiesStringArray); }
        }

        private double _causticSodaSolutionPercentage50;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double CausticSodaSolutionPercentage50
        {
            get { return _causticSodaSolutionPercentage50; }
            set { ChangeAndNotify(ref _causticSodaSolutionPercentage50, value, nameof(CausticSodaSolutionPercentage50), CalcPropertiesStringArray); }
        }

        private double _causticSodaSolutionWeight;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double CausticSodaSolutionWeight
        {
            get { return _causticSodaSolutionWeight; }
            set { ChangeAndNotify(ref _causticSodaSolutionWeight, value, nameof(CausticSodaSolutionWeight), CalcPropertiesStringArray); }
        }

        private double _causticSodaSolutionWeight20;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double CausticSodaSolutionWeight20
        {
            get { return _causticSodaSolutionWeight20; }
            set { ChangeAndNotify(ref _causticSodaSolutionWeight20, value, nameof(CausticSodaSolutionWeight20), CalcPropertiesStringArray); }
        }

        private double _causticSodaSolutionWeight25;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double CausticSodaSolutionWeight25
        {
            get { return _causticSodaSolutionWeight25; }
            set { ChangeAndNotify(ref _causticSodaSolutionWeight25, value, nameof(CausticSodaSolutionWeight25), CalcPropertiesStringArray); }
        }

        private double _causticSodaSolutionWeight50; 
        /// <summary>
        ///  User specified 
        /// </summary>
        public double CausticSodaSolutionWeight50
        {
            get { return _causticSodaSolutionWeight50; }
            set { ChangeAndNotify(ref _causticSodaSolutionWeight50, value, nameof(CausticSodaSolutionWeight50), CalcPropertiesStringArray); }
        }

        private double _causticSodaPurity;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double CausticSodaPurity
        {
            get { return _causticSodaPurity; }
            set { ChangeAndNotify(ref _causticSodaPurity, value, nameof(CausticSodaPurity), CalcPropertiesStringArray); }
        }

        private double _causticSodaMixingEfficiency;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double CausticSodaMixingEfficiency
        {
            get { return _causticSodaMixingEfficiency; }
            set { ChangeAndNotify(ref _causticSodaMixingEfficiency, value, nameof(CausticSodaMixingEfficiency), CalcPropertiesStringArray); }
        }

        private decimal _causticSodaUnitCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal CausticSodaUnitCost
        {
            get { return _causticSodaUnitCost; }
            set { ChangeAndNotify(ref _causticSodaUnitCost, value, nameof(CausticSodaUnitCost), CalcPropertiesStringArray); }
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

        private double _causticSodaUserSpecifiedQuantity;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double CausticSodaUserSpecifiedQuantity
        {
            get { return _causticSodaUserSpecifiedQuantity; }
            set { ChangeAndNotify(ref _causticSodaUserSpecifiedQuantity, value, nameof(CausticSodaUserSpecifiedQuantity), CalcPropertiesStringArray); }
        }


        #endregion

        #region Properties - Equipment: Storage and Dispensing System

        private bool _automatedSystem;
        /// <summary>
        ///  User specified 
        /// </summary>
        public bool AutomatedSystem
        {
            get { return _automatedSystem; }
            set { ChangeAndNotify(ref _automatedSystem, value, nameof(AutomatedSystem), CalcPropertiesStringArray); }
        }

        private decimal _pIDpHControllerCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal PIDpHControllerCost
        {
            get { return _pIDpHControllerCost; }
            set { ChangeAndNotify(ref _pIDpHControllerCost, value, nameof(PIDpHControllerCost), CalcPropertiesStringArray); }
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

        private decimal _chemicalMeteringPumpCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal ChemicalMeteringPumpCost
        {
            get { return _chemicalMeteringPumpCost; }
            set { ChangeAndNotify(ref _chemicalMeteringPumpCost, value, nameof(ChemicalMeteringPumpCost), CalcPropertiesStringArray); }
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

        private decimal _chemicalMeteringPumpElectricityUnitCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal ChemicalMeteringPumpElectricityUnitCost
        {
            get { return _chemicalMeteringPumpElectricityUnitCost; }
            set { ChangeAndNotify(ref _chemicalMeteringPumpElectricityUnitCost, value, nameof(ChemicalMeteringPumpElectricityUnitCost), CalcPropertiesStringArray); }
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

        private double _tankVolume;
        /// <summary>
        /// User specified
        /// </summary>
        public double TankVolume
        {
            get { return _tankVolume; }
            set { ChangeAndNotify(ref _tankVolume, value, nameof(TankVolume), CalcPropertiesStringArray); }
        }

        /// <summary>
        ///  Radio button binding with enumeration for tank estimated cost
        /// </summary>
        public enum RadioButtonsTankTypeOptionsEnum
        {
            OptionPlastic,
            OptionSteel,
        }

        private RadioButtonsTankTypeOptionsEnum _tankTypeOptionsProperty;
        /// <summary>
        ///  User specified 
        /// </summary>
        public RadioButtonsTankTypeOptionsEnum TankTypeOptionsProperty
        {
            get { return _tankTypeOptionsProperty; }
            set { ChangeAndNotify(ref _tankTypeOptionsProperty, value, nameof(TankTypeOptionsProperty), CalcPropertiesStringArray); }
        }

        private decimal _tankPlasticUnitCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal TankPlasticUnitCost
        {
            get { return _tankPlasticUnitCost; }
            set { ChangeAndNotify(ref _tankPlasticUnitCost, value, nameof(TankPlasticUnitCost), CalcPropertiesStringArray); }
        }

        private decimal _tankSteelUnitCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal TankSteelUnitCost
        {
            get { return _tankSteelUnitCost; }
            set { ChangeAndNotify(ref _tankSteelUnitCost, value, nameof(TankSteelUnitCost), CalcPropertiesStringArray); }
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

        private double _feederLineLength;
        /// <summary>
        /// User specified
        /// </summary>
        public double FeederLineLength
        {
            get { return _feederLineLength; }
            set { ChangeAndNotify(ref _feederLineLength, value, nameof(FeederLineLength), CalcPropertiesStringArray); }
        }

        private decimal _feederLineUnitCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal FeederLineUnitCost
        {
            get { return _feederLineUnitCost; }
            set { ChangeAndNotify(ref _feederLineUnitCost, value, nameof(FeederLineUnitCost), CalcPropertiesStringArray); }
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

        #region Properties - Sizing Summary: Stoichiometric       

        private double _calcMolesAcidityPerYear;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcMolesAcidityPerYear
        {
            get { return CausticSodaCalculations.CalcMolesAcidityPerYear(NetAcidity, TypicalFlow); }
            set { ChangeAndNotify(ref _calcMolesAcidityPerYear, value); }
        }

        private double _calcPoundsCausticSodaPerYear;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcPoundsCausticSodaPerYear
        {
            get { return CausticSodaCalculations.CalcPoundsCausticSodaPerYear(CalcMolesAcidityPerYear); }
            set { ChangeAndNotify(ref _calcPoundsCausticSodaPerYear, value); }
        }

        private double _calcGallonsCausticSodaPerYearStoichiometric;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcGallonsCausticSodaPerYearStoichiometric
        {
            get { return CausticSodaCalculations.CalcGallonsCausticSodaPerYearStoichiometric(CalcPoundsCausticSodaPerYear, CausticSodaSolutionWeight, CausticSodaMixingEfficiency, CausticSodaPurity); }
            set { ChangeAndNotify(ref _calcGallonsCausticSodaPerYearStoichiometric, value); }
        }

        private double _calcGallonsCausticSodaPerMonthStoichiometric;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcGallonsCausticSodaPerMonthStoichiometric
        {
            get { return CausticSodaCalculations.CalcGallonsCausticSodaPerMonthStoichiometric(CalcGallonsCausticSodaPerYearStoichiometric); }
            set { ChangeAndNotify(ref _calcGallonsCausticSodaPerMonthStoichiometric, value); }
        }
        
        private double _calcGallonsCausticSodaPerDayStoichiometric;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcGallonsCausticSodaPerDayStoichiometric
        {
            get { return CausticSodaCalculations.CalcGallonsCausticSodaPerDayStoichiometric(CalcGallonsCausticSodaPerYearStoichiometric); }
            set { ChangeAndNotify(ref _calcGallonsCausticSodaPerDayStoichiometric, value); }
        }

        #endregion

        #region Properties - Sizing Summary: Titration

        private double _calcGallonsCausticSodaPerYearTitration;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcGallonsCausticSodaPerYearTitration
        {
            get { return CausticSodaCalculations.CalcGallonsCausticSodaPerYearTitration(TitrationQuantity, TypicalFlow, CausticSodaMixingEfficiency); }
            set { ChangeAndNotify(ref _calcGallonsCausticSodaPerYearTitration, value); }
        }

        private double _calcGallonsCausticSodaPerMonthTitration;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcGallonsCausticSodaPerMonthTitration
        {
            get { return CausticSodaCalculations.CalcGallonsCausticSodaPerMonthTitration(CalcGallonsCausticSodaPerYearTitration); }
            set { ChangeAndNotify(ref _calcGallonsCausticSodaPerMonthTitration, value); }
        }

        private double _calcGallonsCausticSodaPerDayTitration;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcGallonsCausticSodaPerDayTitration
        {
            get { return CausticSodaCalculations.CalcGallonsCausticSodaPerDayTitration(CalcGallonsCausticSodaPerYearTitration); }
            set { ChangeAndNotify(ref _calcGallonsCausticSodaPerDayTitration, value); }
        }

        #endregion

        #region Properties - Sizing Summary: User Specified Quantity

        private double _calcGallonsCausticSodaPerYearUserSpecifiedQuantity;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcGallonsCausticSodaPerYearUserSpecifiedQuantity
        {
            get { return CausticSodaCalculations.CalcGallonsCausticSodaPerYearUserSpecifiedQuantity(CausticSodaUserSpecifiedQuantity); }
            set { ChangeAndNotify(ref _calcGallonsCausticSodaPerYearUserSpecifiedQuantity, value); }
        }
        
        private double _calcGallonsCausticSodaPerMonthUserSpecifiedQuantity;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcGallonsCausticSodaPerMonthUserSpecifiedQuantity
        {
            get { return CausticSodaCalculations.CalcGallonsCausticSodaPerMonthUserSpecifiedQuantity(CalcGallonsCausticSodaPerYearUserSpecifiedQuantity); }
            set { ChangeAndNotify(ref _calcGallonsCausticSodaPerMonthUserSpecifiedQuantity, value); }
        }

        private double _calcGallonsCausticSodaPerDayUserSpecifiedQuantity;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcGallonsCausticSodaPerDayUserSpecifiedQuantity
        {
            get { return CausticSodaCalculations.CalcGallonsCausticSodaPerDayUserSpecifiedQuantity(CalcGallonsCausticSodaPerMonthUserSpecifiedQuantity); }
            set { ChangeAndNotify(ref _calcGallonsCausticSodaPerDayUserSpecifiedQuantity, value); }
        }

        #endregion

        #region Properties - Sizing Summary: Caustic Soda Consumption

        private double _calcGallonsCausticSodaPerDay;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcGallonsCausticSodaPerDay
        {
            get
            {
                switch (ChemicalConsumptionOptionsProperty)
                {
                    case RadioButtonsChemicalConsumptionOptionsEnum.OptionStoichiometric:
                        _calcGallonsCausticSodaPerDay = CalcGallonsCausticSodaPerDayStoichiometric;
                        break;
                    case RadioButtonsChemicalConsumptionOptionsEnum.OptionTitration:
                        _calcGallonsCausticSodaPerDay = CalcGallonsCausticSodaPerDayTitration;
                        break;
                    case RadioButtonsChemicalConsumptionOptionsEnum.OptionUserSpecifiedQuantity:
                        _calcGallonsCausticSodaPerDay = CalcGallonsCausticSodaPerDayUserSpecifiedQuantity;
                        break;
                    default:
                        break;
                }
                return _calcGallonsCausticSodaPerDay;
            }
            set { ChangeAndNotify(ref _calcGallonsCausticSodaPerDay, value); }
        }

        private double _calcGallonsCausticSodaPerMonth;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcGallonsCausticSodaPerMonth
        {
            get
            {
                switch (ChemicalConsumptionOptionsProperty)
                {
                    case RadioButtonsChemicalConsumptionOptionsEnum.OptionStoichiometric:
                        _calcGallonsCausticSodaPerMonth = CalcGallonsCausticSodaPerMonthStoichiometric;
                        break;
                    case RadioButtonsChemicalConsumptionOptionsEnum.OptionTitration:
                        _calcGallonsCausticSodaPerMonth = CalcGallonsCausticSodaPerMonthTitration;
                        break;
                    case RadioButtonsChemicalConsumptionOptionsEnum.OptionUserSpecifiedQuantity:
                        _calcGallonsCausticSodaPerMonth = CalcGallonsCausticSodaPerMonthUserSpecifiedQuantity;
                        break;
                    default:
                        break;
                }
                return _calcGallonsCausticSodaPerMonth;
            }
            set { ChangeAndNotify(ref _calcGallonsCausticSodaPerMonth, value); }
        }

        private double _calcGallonsCausticSodaPerYear;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcGallonsCausticSodaPerYear
        {
            get
            {
                switch (ChemicalConsumptionOptionsProperty)
                {
                    case RadioButtonsChemicalConsumptionOptionsEnum.OptionStoichiometric:
                        _calcGallonsCausticSodaPerYear = CalcGallonsCausticSodaPerYearStoichiometric;
                        break;
                    case RadioButtonsChemicalConsumptionOptionsEnum.OptionTitration:
                        _calcGallonsCausticSodaPerYear = CalcGallonsCausticSodaPerYearTitration;
                        break;
                    case RadioButtonsChemicalConsumptionOptionsEnum.OptionUserSpecifiedQuantity:
                        _calcGallonsCausticSodaPerYear = CalcGallonsCausticSodaPerYearUserSpecifiedQuantity;
                        break;
                    default:
                        break;
                }
                return _calcGallonsCausticSodaPerYear;
            }
            set { ChangeAndNotify(ref _calcGallonsCausticSodaPerYear, value); }
        }

        #endregion

        #region Properties - Sizing Summary: Tank Refill Frequency

        private double _calcTankRefillFrequency;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcTankRefillFrequency
        {
            get { return CausticSodaCalculations.CalcTankRefillFrequency(CalcGallonsCausticSodaPerYear, TankVolume); }
            set { ChangeAndNotify(ref _calcTankRefillFrequency, value); }
        }

        #endregion

        #region Properties - Sizing Summary: Operational Period of Chemical Pump

        private double _calcChemicalMeteringPumpOperationPeriodTotalDaysPerYear;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcChemicalMeteringPumpOperationPeriodTotalDaysPerYear
        {
            get { return CausticSodaCalculations.CalcChemicalMeteringPumpOperationPeriodTotalDaysPerYear(ChemicalMeteringPumpHoursPerDay, ChemicalMeteringPumpDaysPerYear); }
            set { ChangeAndNotify(ref _calcChemicalMeteringPumpOperationPeriodTotalDaysPerYear, value); }
        }

        private double _calcChemicalMeteringPumpOperationPeriodTotalWeeksPerYear;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcChemicalMeteringPumpOperationPeriodTotalWeeksPerYear
        {
            get { return CausticSodaCalculations.CalcChemicalMeteringPumpOperationPeriodTotalWeeksPerYear(CalcChemicalMeteringPumpOperationPeriodTotalDaysPerYear); }
            set { ChangeAndNotify(ref _calcChemicalMeteringPumpOperationPeriodTotalWeeksPerYear, value); }
        }
        
        private double _calcChemicalMeteringPumpOperationPeriodTotalMonthsPerYear;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcChemicalMeteringPumpOperationPeriodTotalMonthsPerYear
        {
            get { return CausticSodaCalculations.CalcChemicalMeteringPumpOperationPeriodTotalMonthsPerYear(CalcChemicalMeteringPumpOperationPeriodTotalWeeksPerYear); }
            set { ChangeAndNotify(ref _calcChemicalMeteringPumpOperationPeriodTotalMonthsPerYear, value); }
        }
        #endregion

        #region Properties - Capital Costs

        /// <summary>
        ///  Radio button binding with enumeration for tank estimated cost
        /// </summary>

        private decimal _calcAutomatedSystemCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcAutomatedSystemCost
        {
            get
            {
                if (AutomatedSystem)
                {
                    _calcAutomatedSystemCost = CausticSodaCalculations.CalcAutomatedSystemCost(PIDpHControllerCost, PHProbeCost, ChemicalMeteringPumpCost);
                }
                else
                {
                    _calcAutomatedSystemCost = 0m;
                }

                return _calcAutomatedSystemCost;
            }
            set { ChangeAndNotify(ref _calcAutomatedSystemCost, value); }

        }

        /// <summary>
        ///  Radio button binding with enumeration for tank cost
        /// </summary>
        public enum RadioButtonsTankCostOptionsEnum
        {
            OptionEstimate,
            OptionUserSpecified,
        }

        private RadioButtonsTankCostOptionsEnum _tankCostOptionsProperty;
        /// <summary>
        ///  User specified 
        /// </summary>
        public RadioButtonsTankCostOptionsEnum TankCostOptionsProperty
        {
            get { return _tankCostOptionsProperty; }
            set { ChangeAndNotify(ref _tankCostOptionsProperty, value, nameof(TankCostOptionsProperty), CalcPropertiesStringArray); }
        }

        private decimal _calcTankCostEstimatedPlastic;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcTankCostEstimatedPlastic
        {
            get { return CausticSodaCalculations.CalcTankCost(TankVolume, TankPlasticUnitCost); }
            set { ChangeAndNotify(ref _calcTankCostEstimatedPlastic, value); }
        }

        private decimal _calcTankCostEstimatedSteel;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcTankCostEstimatedSteel
        {
            get { return CausticSodaCalculations.CalcTankCost(TankVolume, TankSteelUnitCost); }
            set { ChangeAndNotify(ref _calcTankCostEstimatedSteel, value); }
        }

        private decimal _calcTankCostEstimated;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcTankCostEstimated
        {
            get
            {
                switch (TankTypeOptionsProperty)
                {
                    case RadioButtonsTankTypeOptionsEnum.OptionPlastic:
                        _calcTankCostEstimated = CalcTankCostEstimatedPlastic;
                        break;
                    case RadioButtonsTankTypeOptionsEnum.OptionSteel:
                        _calcTankCostEstimated = CalcTankCostEstimatedSteel;
                        break;
                    default:
                        break;
                }

                return _calcTankCostEstimated;
            }
            set { ChangeAndNotify(ref _calcTankCostEstimated, value); }

        }

        private decimal _tankCostUserSpecified;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal TankCostUserSpecified
        {
            get { return _tankCostUserSpecified; }
            set { ChangeAndNotify(ref _tankCostUserSpecified, value, nameof(TankCostUserSpecified), CalcPropertiesStringArray); }
        }

        private decimal _calcTankCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcTankCost
        {
            get
            {
                switch (TankCostOptionsProperty)
                {
                    case RadioButtonsTankCostOptionsEnum.OptionEstimate:
                        _calcTankCost = CalcTankCostEstimated;
                        break;
                    case RadioButtonsTankCostOptionsEnum.OptionUserSpecified:
                        _calcTankCost = TankCostUserSpecified;
                        break;
                    default:
                        break;
                }

                return _calcTankCost;
            }
            set { ChangeAndNotify(ref _calcTankCost, value); }

        }

        private decimal _calcValveCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcValveCost
        {
            get { return CausticSodaCalculations.CalcValveCost(ValveQuantity, ValveUnitCost); }
            set { ChangeAndNotify(ref _calcValveCost, value); }

        }

        private decimal _calcFeederLineCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcFeederLineCost
        {
            get { return CausticSodaCalculations.CalcFeederLineCost(FeederLineLength, FeederLineUnitCost); }
            set { ChangeAndNotify(ref _calcFeederLineCost, value); }

        }

        private decimal _calcStorageAndDispensingCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcStorageAndDispensingCost
        {
            get
            {                
                if (AutomatedSystem)
                {
                    _calcStorageAndDispensingCost = CausticSodaCalculations.CalcStorageAndDispensingCost(CalcAutomatedSystemCost, CalcTankCost, CalcValveCost, CalcFeederLineCost);
                }
                else
                {
                    _calcStorageAndDispensingCost = CausticSodaCalculations.CalcStorageAndDispensingCost(0m, CalcTankCost, CalcValveCost, CalcFeederLineCost);
                }

                return _calcStorageAndDispensingCost;
            }
            set { ChangeAndNotify(ref _calcStorageAndDispensingCost, value); }

        }

        private decimal _calcOtherCapitalItemsCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcOtherCapitalItemsCost
        {
            get
            {
                return CausticSodaCalculations.CalcOtherCapitalItemsCost(OtherCapitalItemQuantity1, OtherCapitalItemUnitCost1,
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
            get { return CausticSodaCalculations.CalcCapitalCostSubTotal(CalcAutomatedSystemCost, CalcTankCost, CalcValveCost, CalcFeederLineCost, CalcOtherCapitalItemsCost); }
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
            get { return CausticSodaCalculations.CalcSystemInstallCost(SystemInstallCostMultiplier, CalcCapitalCostSubTotal); }
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
                _calcCapitalCostTotal = CausticSodaCalculations.CalcCapitalCostTotal(CalcCapitalCostSubTotal, CalcSystemInstallCost);

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
            get { return CausticSodaCalculations.CalcAnnualCostChemical(CalcGallonsCausticSodaPerYear, CausticSodaUnitCost); }
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
            get { return CausticSodaCalculations.CalcAnnualCostOperationAndMaintanence(AnnualCostOperationAndMaintanenceMultiplier, CalcCapitalCostTotal); }
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
   
        private decimal _calcAnnualCostpHProbe;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcAnnualCostpHProbe
        {
            get { return CausticSodaCalculations.CalcAnnualCostpHProbe(PHProbeCost); }
            set { ChangeAndNotify(ref _calcAnnualCostpHProbe, value); }
        }


        private decimal _calcAnnualCostElectricEstimated;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcAnnualCostElectricEstimated
        {
            get { return CausticSodaCalculations.CalcAnnualCostElectric(ChemicalMeteringPumpPowerRequirement, ChemicalMeteringPumpHoursPerDay, ChemicalMeteringPumpDaysPerYear, ChemicalMeteringPumpElectricityUnitCost); }
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
                return CausticSodaCalculations.CalcOtherAnnualItemsCost(OtherAnnualItemQuantity1, OtherAnnualItemUnitCost1,
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
                _calcAnnualCost = CausticSodaCalculations.CalcAnnualCostTotal(CalcAnnualCostChemical, CalcAnnualCostOperationAndMaintanence, CalcAnnualCostElectric, CalcAnnualCostpHProbe, CalcOtherAnnualItemsCost);

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

        private double _recapitalizationCostLifeCyclePIDpHController;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostLifeCyclePIDpHController
        {
            get { return _recapitalizationCostLifeCyclePIDpHController; }
            set { ChangeAndNotify(ref _recapitalizationCostLifeCyclePIDpHController, value, nameof(RecapitalizationCostLifeCyclePIDpHController), CalcPropertiesStringArray); }
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

        private double _recapitalizationCostPercentReplacementTank;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostPercentReplacementTank
        {
            get { return _recapitalizationCostPercentReplacementTank; }
            set { ChangeAndNotify(ref _recapitalizationCostPercentReplacementTank, value, nameof(RecapitalizationCostPercentReplacementTank), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostPercentReplacementPIDpHController;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostPercentReplacementPIDpHController
        {
            get { return _recapitalizationCostPercentReplacementPIDpHController; }
            set { ChangeAndNotify(ref _recapitalizationCostPercentReplacementPIDpHController, value, nameof(RecapitalizationCostPercentReplacementPIDpHController), CalcPropertiesStringArray); }
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

        private decimal _calcRecapitalizationCostMaterialTotalCostTank;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostMaterialTotalCostTank
        {
            get { return CausticSodaCalculations.CalcRecapitalizationCostMaterialTotalCostTank(CalcTankCost, CalcFeederLineCost, CalcValveCost); }
            set { ChangeAndNotify(ref _calcRecapitalizationCostMaterialTotalCostTank, value); }
        }

        private decimal _calcRapitalizationCostTank;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostTank
        {
            get
            {
                return CausticSodaCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
                                                                                 RecapitalizationCostInflationRate, RecapitalizationCostLifeCycleTank,
                                                                                 CalcRecapitalizationCostMaterialTotalCostTank, RecapitalizationCostPercentReplacementTank);
            }
            set { ChangeAndNotify(ref _calcRapitalizationCostTank, value); }
        }

        private decimal _calcRapitalizationCostPIDpHController;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostPIDpHController
        {
            get
            {
                return CausticSodaCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
                                                                        RecapitalizationCostInflationRate, RecapitalizationCostLifeCyclePIDpHController,
                                                                        PIDpHControllerCost, RecapitalizationCostPercentReplacementPIDpHController);
            }
            set { ChangeAndNotify(ref _calcRapitalizationCostPIDpHController, value); }
        }

        private decimal _calcRapitalizationCostChemicalMeteringPump;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostChemicalMeteringPump
        {
            get
            {
                return CausticSodaCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
                                                                        RecapitalizationCostInflationRate, RecapitalizationCostLifeCycleChemicalMeteringPump,
                                                                        ChemicalMeteringPumpCost, RecapitalizationCostPercentReplacementChemicalMeteringPump);
            }
            set { ChangeAndNotify(ref _calcRapitalizationCostChemicalMeteringPump, value); }
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
            get { return CausticSodaCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn, 
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
                    case "Tank":
                        item.MaterialCostDefault = CalcRecapitalizationCostMaterialTotalCostTank;
                        break;
                    case "PIDpHController":
                        item.MaterialCostDefault = PIDpHControllerCost;
                        break;
                    case "ChemicalMeteringPump":
                        item.MaterialCostDefault = ChemicalMeteringPumpCost;
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
                    case "PIDpHController":
                        item.TotalCost = CalcRecapitalizationCostPIDpHController;
                        break;
                    case "ChemicalMeteringPump":
                        item.TotalCost = CalcRecapitalizationCostChemicalMeteringPump;
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
                MaterialCostDefault = CalcRecapitalizationCostMaterialTotalCostTank,
                UseCustomCost = false,
                MaterialCostCustom = 0m,
                TotalCost = CalcRecapitalizationCostTank
            });
            RecapMaterials.Add(new RecapMaterial
            {
                IsSelected = true,
                Name = "PID pH Controller",
                NameFixed = "PIDpHController",
                LifeCycle = RecapitalizationCostLifeCyclePIDpHController,
                PercentReplacement = RecapitalizationCostPercentReplacementPIDpHController,
                MaterialCostDefault = PIDpHControllerCost,
                UseCustomCost = false,
                MaterialCostCustom = 0m,
                TotalCost = CalcRecapitalizationCostPIDpHController
            });
            RecapMaterials.Add(new RecapMaterial
            {
                IsSelected = true,
                Name = "Chemical Pump",
                NameFixed = "ChemicalMeteringPump",
                LifeCycle = RecapitalizationCostLifeCycleChemicalMeteringPump,
                PercentReplacement = RecapitalizationCostPercentReplacementChemicalMeteringPump,
                MaterialCostDefault = ChemicalMeteringPumpCost,
                UseCustomCost = false,
                MaterialCostCustom = 0m,
                TotalCost = CalcRecapitalizationCostChemicalMeteringPump
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
            ((RecapMaterial)sender).TotalCost = CausticSodaCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
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
        public ICommand SetCausticSodaSolutionCommand { get; }
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
            var customDialog = new CustomDialog() { Title = "About Caustic Soda" };

            var customDialogViewModel = new CustomDialogViewModel(instance =>
            {
                _dialogCoordinator.HideMetroDialogAsync(this, customDialog);
            });
            customDialogViewModel.Message = Resources.infoCaustic;
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
                        string message = Resources.infoWaterQualityCaustic;
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
                        string message = Resources.infoChemicalSolutionCaustic;
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
                        string message = Resources.infoChemicalConsumptionCaustic;
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
                        string message = Resources.infoEquipmentCaustic;
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
                        string message = Resources.infoOtherItemsCapitalCaustic;
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
                        string message = Resources.infoOtherItemsAnnualCaustic;
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
                        string message = Resources.infoSizingSummaryCaustic;
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
                        string message = Resources.infoCapitalCostCaustic;
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
                        string message = Resources.infoAnnualCostCaustic;
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
                        string message = Resources.infoRecapitalizationCostCaustic;
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
        /// 
        /// </summary>
        /// <param name="causticSodaPercentage"></param>
        public void SetCausticSodaSolution(object causticSodaSolution)
        {
            CausticSodaSolution itemCausticSodaSolution = (CausticSodaSolution)causticSodaSolution;

            switch (itemCausticSodaSolution.Name)
            {
                case "20":
                    CausticSodaSolutionName = CausticSodaSolutionName20;
                    CausticSodaSolutionPercentage = CausticSodaSolutionPercentage20;
                    CausticSodaSolutionWeight = CausticSodaSolutionWeight20;
                    break;
                case "25":
                    CausticSodaSolutionName = CausticSodaSolutionName25;
                    CausticSodaSolutionPercentage = CausticSodaSolutionPercentage25;
                    CausticSodaSolutionWeight = CausticSodaSolutionWeight25;
                    break;
                case "50":
                    CausticSodaSolutionName = CausticSodaSolutionName50;
                    CausticSodaSolutionPercentage = CausticSodaSolutionPercentage50;
                    CausticSodaSolutionWeight = CausticSodaSolutionWeight50;
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

        public CausticSodaViewModel(IDialogCoordinator dialogCoordinator)
        {
            // MahApps dialog coordinator
            _dialogCoordinator = dialogCoordinator;

            // Assign the proper functions to the open and save commands
            OpenCommand = new RelayCommand(OpenFile);
            SaveCommand = new RelayCommand(SaveFile);
            HelpCommand = new RelayCommand(ShowHelp);
            SetCausticSodaSolutionCommand = new RelayCommandWithParameter(SetCausticSodaSolution);
            SyncCommand = new RelayCommand(SyncWithMainUi);

            // Get a list of property names and filter the names to include only those that start with "Calc" in order
            // to use with the NotifyAndChange.  This eliminates the need to write every property name that needs 
            // to be notified of changes by the user.
            PropertiesStringList = Helpers.GetNamesOfClassProperties(this);
            CalcPropertiesStringArray = Helpers.FilterPropertiesList(PropertiesStringList, "Calc");

            // Initialize the model name and user name
            ModuleType = "Caustic Soda";
            ModuleName = "Untitled module";
            ModuleTreatmentType = "Active";
            ModuleId = random.Next(1000);

            IsError = false;
            IsMajorError = false;
            ErrorMessage = "";
            ErrorMessageShort = "";

            // Initialize check boxes
            AutomatedSystem = true;

            // Initialize radio buttons
            AnnualCostChemicalOptionsProperty = RadioButtonsAnnualCostChemicalOptionsEnum.OptionEstimate;
            ChemicalConsumptionOptionsProperty = RadioButtonsChemicalConsumptionOptionsEnum.OptionStoichiometric;
            TankTypeOptionsProperty = RadioButtonsTankTypeOptionsEnum.OptionPlastic;
            TankCostOptionsProperty = RadioButtonsTankCostOptionsEnum.OptionEstimate;
            SystemInstallCostOptionsProperty = RadioButtonsSystemInstallationOptionsEnum.OptionCostMultiplier;
            AnnualCostOperationAndMaintanenceOptionsProperty = RadioButtonsAnnualCostOperationAndMaintanenceOptionsEnum.OptionCostMultiplier;
            AnnualCostElectricOptionsProperty = RadioButtonsAnnualCostElectricOptionsEnum.OptionAnnualCostElectricEstimate;

            // Dummy variables to force syncing
            Dummy = 0;

            // Read the xml data file and assign property values accordingly.
            OpenXmlAndAssignValues(@"..\..\Data\default-data-caustic-soda.xml");

            // Caustic Soda Solutions
            CausticSodaSolutions = new List<CausticSodaSolution>
            {
                new CausticSodaSolution {Name = CausticSodaSolutionName20, Percentage = CausticSodaSolutionPercentage20, Weight = CausticSodaSolutionWeight20},
                new CausticSodaSolution {Name = CausticSodaSolutionName25, Percentage = CausticSodaSolutionPercentage25, Weight = CausticSodaSolutionWeight25},
                new CausticSodaSolution {Name = CausticSodaSolutionName50, Percentage = CausticSodaSolutionPercentage50, Weight = CausticSodaSolutionWeight50},
            };

            CausticSodaSolutionName = CausticSodaSolutionName20;
            CausticSodaSolutionPercentage = CausticSodaSolutionPercentage20;
            CausticSodaSolutionWeight = CausticSodaSolutionWeight20;


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
