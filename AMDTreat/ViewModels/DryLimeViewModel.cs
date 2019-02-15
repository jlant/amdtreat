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

    public class DryLimeViewModel : PropertyChangedBase, IObserver<SharedData>
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

        #region Properties - Lime Information

        /// <summary>
        ///  Radio button binding
        /// </summary>
        public enum RadioButtonsLimeOptionsEnum
        {
            OptionHydratedLime,
            OptionDryLime,
        }

        private RadioButtonsLimeOptionsEnum _limeOptionsProperty;
        /// <summary>
        ///  User specified 
        /// </summary>
        public RadioButtonsLimeOptionsEnum LimeOptionsProperty
        {
            get { return _limeOptionsProperty; }
            set { ChangeAndNotify(ref _limeOptionsProperty, value, nameof(LimeOptionsProperty), CalcPropertiesStringArray); }
        }

        private double _hydratedLimePurity;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double HydratedLimePurity
        {
            get { return _hydratedLimePurity; }
            set { ChangeAndNotify(ref _hydratedLimePurity, value, nameof(HydratedLimePurity), CalcPropertiesStringArray); }
        }

        private double _dryLimePurity;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double DryLimePurity
        {
            get { return _dryLimePurity; }
            set { ChangeAndNotify(ref _dryLimePurity, value, nameof(DryLimePurity), CalcPropertiesStringArray); }
        }

        private double _hydratedLimeDissolutionEfficiency;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double HydratedLimeDissolutionEfficiency
        {
            get { return _hydratedLimeDissolutionEfficiency; }
            set { ChangeAndNotify(ref _hydratedLimeDissolutionEfficiency, value, nameof(HydratedLimeDissolutionEfficiency), CalcPropertiesStringArray); }
        }

        private double _dryLimeDissolutionEfficiency;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double DryLimeDissolutionEfficiency
        {
            get { return _dryLimeDissolutionEfficiency; }
            set { ChangeAndNotify(ref _dryLimeDissolutionEfficiency, value, nameof(DryLimeDissolutionEfficiency), CalcPropertiesStringArray); }
        }

        private decimal _hydratedLimeUnitCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal HydratedLimeUnitCost
        {
            get { return _hydratedLimeUnitCost; }
            set { ChangeAndNotify(ref _hydratedLimeUnitCost, value, nameof(HydratedLimeUnitCost), CalcPropertiesStringArray); }
        }

        private decimal _dryLimeUnitCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal DryLimeUnitCost
        {
            get { return _dryLimeUnitCost; }
            set { ChangeAndNotify(ref _dryLimeUnitCost, value, nameof(DryLimeUnitCost), CalcPropertiesStringArray); }
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

        private double _limeUserSpecifiedQuantity;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double LimeUserSpecifiedQuantity
        {
            get { return _limeUserSpecifiedQuantity; }
            set { ChangeAndNotify(ref _limeUserSpecifiedQuantity, value, nameof(LimeUserSpecifiedQuantity), CalcPropertiesStringArray); }
        }

        #endregion

        #region Properties - Equipment: Storage and Dispensing System

        private bool _isSiloSystem;
        /// <summary>
        /// User specified
        /// </summary>
        public bool IsSiloSystem
        {
            get { return _isSiloSystem; }
            set { ChangeAndNotify(ref _isSiloSystem, value, nameof(IsSiloSystem), CalcPropertiesStringArray); }
        }

        /// <summary>
        ///  Radio button binding with enumeration 
        /// </summary>
        public enum RadioButtonsSiloSystemOptionsEnum
        {
            OptionEstimated,
            OptionUserSpecified,
        }

        private RadioButtonsSiloSystemOptionsEnum _siloSystemOptionsProperty;
        /// <summary>
        ///  User specified 
        /// </summary>
        public RadioButtonsSiloSystemOptionsEnum SiloSystemOptionsProperty
        {
            get { return _siloSystemOptionsProperty; }
            set { ChangeAndNotify(ref _siloSystemOptionsProperty, value, nameof(SiloSystemOptionsProperty), CalcPropertiesStringArray); }
        }

        private List<SiloSystem> _siloSystems;
        /// <summary>
        /// Collection 
        /// </summary>
        public List<SiloSystem> SiloSystems
        {
            get { return _siloSystems; }

            set { ChangeAndNotify(ref _siloSystems, value, nameof(SiloSystems), CalcPropertiesStringArray); }
        }

        private string _siloSystem30TonName;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string SiloSystem30TonName
        {
            get { return _siloSystem30TonName; }
            set { ChangeAndNotify(ref _siloSystem30TonName, value, nameof(SiloSystem30TonName), CalcPropertiesStringArray); }
        }

        private double _siloSystem30TonQuantity;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double SiloSystem30TonQuantity
        {
            get { return _siloSystem30TonQuantity; }
            set { ChangeAndNotify(ref _siloSystem30TonQuantity, value, nameof(SiloSystem30TonQuantity), CalcPropertiesStringArray); }
        }

        private double _siloSystem30TonWeight;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double SiloSystem30TonWeight
        {
            get { return _siloSystem30TonWeight; }
            set { ChangeAndNotify(ref _siloSystem30TonWeight, value, nameof(SiloSystem30TonWeight), CalcPropertiesStringArray); }
        }

        private decimal _siloSystem30TonDryLimeUnitCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal SiloSystem30TonDryLimeUnitCost
        {
            get { return _siloSystem30TonDryLimeUnitCost; }
            set { ChangeAndNotify(ref _siloSystem30TonDryLimeUnitCost, value, nameof(SiloSystem30TonDryLimeUnitCost), CalcPropertiesStringArray); }
        }

        private decimal _siloSystem30TonHydratedLimeUnitCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal SiloSystem30TonHydratedLimeUnitCost
        {
            get { return _siloSystem30TonHydratedLimeUnitCost; }
            set { ChangeAndNotify(ref _siloSystem30TonHydratedLimeUnitCost, value, nameof(SiloSystem30TonHydratedLimeUnitCost), CalcPropertiesStringArray); }
        }

        private double _siloSystem30TonFoundationArea;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double SiloSystem30TonFoundationArea
        {
            get { return _siloSystem30TonFoundationArea; }
            set { ChangeAndNotify(ref _siloSystem30TonFoundationArea, value, nameof(SiloSystem30TonFoundationArea), CalcPropertiesStringArray); }
        }

        private double _siloSystem30TonDiameter;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double SiloSystem30TonDiameter
        {
            get { return _siloSystem30TonDiameter; }
            set { ChangeAndNotify(ref _siloSystem30TonDiameter, value, nameof(SiloSystem30TonDiameter), CalcPropertiesStringArray); }
        }

        private double _siloSystem30TonConcreteThickness;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double SiloSystem30TonConcreteThickness
        {
            get { return _siloSystem30TonConcreteThickness; }
            set { ChangeAndNotify(ref _siloSystem30TonConcreteThickness, value, nameof(SiloSystem30TonConcreteThickness), CalcPropertiesStringArray); }
        }

        private double _siloSystem30TonConcreteVolume;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double SiloSystem30TonConcreteVolume
        {
            get { return _siloSystem30TonConcreteVolume; }
            set { ChangeAndNotify(ref _siloSystem30TonConcreteVolume, value, nameof(SiloSystem30TonConcreteVolume), CalcPropertiesStringArray); }
        }

        private string _siloSystem60TonName;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string SiloSystem60TonName
        {
            get { return _siloSystem60TonName; }
            set { ChangeAndNotify(ref _siloSystem60TonName, value, nameof(SiloSystem60TonName), CalcPropertiesStringArray); }
        }

        private double _siloSystem60TonQuantity;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double SiloSystem60TonQuantity
        {
            get { return _siloSystem60TonQuantity; }
            set { ChangeAndNotify(ref _siloSystem60TonQuantity, value, nameof(SiloSystem60TonQuantity), CalcPropertiesStringArray); }
        }

        private double _siloSystem60TonWeight;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double SiloSystem60TonWeight
        {
            get { return _siloSystem60TonWeight; }
            set { ChangeAndNotify(ref _siloSystem60TonWeight, value, nameof(SiloSystem60TonWeight), CalcPropertiesStringArray); }
        }

        private decimal _siloSystem60TonDryLimeUnitCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal SiloSystem60TonDryLimeUnitCost
        {
            get { return _siloSystem60TonDryLimeUnitCost; }
            set { ChangeAndNotify(ref _siloSystem60TonDryLimeUnitCost, value, nameof(SiloSystem60TonDryLimeUnitCost), CalcPropertiesStringArray); }
        }

        private decimal _siloSystem60TonHydratedLimeUnitCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal SiloSystem60TonHydratedLimeUnitCost
        {
            get { return _siloSystem60TonHydratedLimeUnitCost; }
            set { ChangeAndNotify(ref _siloSystem60TonHydratedLimeUnitCost, value, nameof(SiloSystem60TonHydratedLimeUnitCost), CalcPropertiesStringArray); }
        }

        private double _siloSystem60TonFoundationArea;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double SiloSystem60TonFoundationArea
        {
            get { return _siloSystem60TonFoundationArea; }
            set { ChangeAndNotify(ref _siloSystem60TonFoundationArea, value, nameof(SiloSystem60TonFoundationArea), CalcPropertiesStringArray); }
        }

        private double _siloSystem60TonDiameter;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double SiloSystem60TonDiameter
        {
            get { return _siloSystem60TonDiameter; }
            set { ChangeAndNotify(ref _siloSystem60TonDiameter, value, nameof(SiloSystem60TonDiameter), CalcPropertiesStringArray); }
        }

        private double _siloSystem60TonConcreteThickness;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double SiloSystem60TonConcreteThickness
        {
            get { return _siloSystem60TonConcreteThickness; }
            set { ChangeAndNotify(ref _siloSystem60TonConcreteThickness, value, nameof(SiloSystem60TonConcreteThickness), CalcPropertiesStringArray); }
        }

        private double _siloSystem60TonConcreteVolume;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double SiloSystem60TonConcreteVolume
        {
            get { return _siloSystem60TonConcreteVolume; }
            set { ChangeAndNotify(ref _siloSystem60TonConcreteVolume, value, nameof(SiloSystem60TonConcreteVolume), CalcPropertiesStringArray); }
        }

        private string _siloSystem90TonName;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string SiloSystem90TonName
        {
            get { return _siloSystem90TonName; }
            set { ChangeAndNotify(ref _siloSystem90TonName, value, nameof(SiloSystem90TonName), CalcPropertiesStringArray); }
        }

        private double _siloSystem90TonQuantity;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double SiloSystem90TonQuantity
        {
            get { return _siloSystem90TonQuantity; }
            set { ChangeAndNotify(ref _siloSystem90TonQuantity, value, nameof(SiloSystem90TonQuantity), CalcPropertiesStringArray); }
        }

        private double _siloSystem90TonWeight;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double SiloSystem90TonWeight
        {
            get { return _siloSystem90TonWeight; }
            set { ChangeAndNotify(ref _siloSystem90TonWeight, value, nameof(SiloSystem90TonWeight), CalcPropertiesStringArray); }
        }

        private decimal _siloSystem90TonDryLimeUnitCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal SiloSystem90TonDryLimeUnitCost
        {
            get { return _siloSystem90TonDryLimeUnitCost; }
            set { ChangeAndNotify(ref _siloSystem90TonDryLimeUnitCost, value, nameof(SiloSystem90TonDryLimeUnitCost), CalcPropertiesStringArray); }
        }

        private decimal _siloSystem90TonHydratedLimeUnitCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal SiloSystem90TonHydratedLimeUnitCost
        {
            get { return _siloSystem90TonHydratedLimeUnitCost; }
            set { ChangeAndNotify(ref _siloSystem90TonHydratedLimeUnitCost, value, nameof(SiloSystem90TonHydratedLimeUnitCost), CalcPropertiesStringArray); }
        }

        private double _siloSystem90TonFoundationArea;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double SiloSystem90TonFoundationArea
        {
            get { return _siloSystem90TonFoundationArea; }
            set { ChangeAndNotify(ref _siloSystem90TonFoundationArea, value, nameof(SiloSystem90TonFoundationArea), CalcPropertiesStringArray); }
        }

        private double _siloSystem90TonDiameter;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double SiloSystem90TonDiameter
        {
            get { return _siloSystem90TonDiameter; }
            set { ChangeAndNotify(ref _siloSystem90TonDiameter, value, nameof(SiloSystem90TonDiameter), CalcPropertiesStringArray); }
        }

        private double _siloSystem90TonConcreteThickness;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double SiloSystem90TonConcreteThickness
        {
            get { return _siloSystem90TonConcreteThickness; }
            set { ChangeAndNotify(ref _siloSystem90TonConcreteThickness, value, nameof(SiloSystem90TonConcreteThickness), CalcPropertiesStringArray); }
        }

        private double _siloSystem90TonConcreteVolume;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double SiloSystem90TonConcreteVolume
        {
            get { return _siloSystem90TonConcreteVolume; }
            set { ChangeAndNotify(ref _siloSystem90TonConcreteVolume, value, nameof(SiloSystem90TonConcreteVolume), CalcPropertiesStringArray); }
        }

        private string _siloSystem120TonName;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string SiloSystem120TonName
        {
            get { return _siloSystem120TonName; }
            set { ChangeAndNotify(ref _siloSystem120TonName, value, nameof(SiloSystem120TonName), CalcPropertiesStringArray); }
        }

        private double _siloSystem120TonQuantity;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double SiloSystem120TonQuantity
        {
            get { return _siloSystem120TonQuantity; }
            set { ChangeAndNotify(ref _siloSystem120TonQuantity, value, nameof(SiloSystem120TonQuantity), CalcPropertiesStringArray); }
        }

        private double _siloSystem120TonWeight;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double SiloSystem120TonWeight
        {
            get { return _siloSystem120TonWeight; }
            set { ChangeAndNotify(ref _siloSystem120TonWeight, value, nameof(SiloSystem120TonWeight), CalcPropertiesStringArray); }
        }

        private decimal _siloSystem120TonDryLimeUnitCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal SiloSystem120TonDryLimeUnitCost
        {
            get { return _siloSystem120TonDryLimeUnitCost; }
            set { ChangeAndNotify(ref _siloSystem120TonDryLimeUnitCost, value, nameof(SiloSystem120TonDryLimeUnitCost), CalcPropertiesStringArray); }
        }

        private decimal _siloSystem120TonHydratedLimeUnitCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal SiloSystem120TonHydratedLimeUnitCost
        {
            get { return _siloSystem120TonHydratedLimeUnitCost; }
            set { ChangeAndNotify(ref _siloSystem120TonHydratedLimeUnitCost, value, nameof(SiloSystem120TonHydratedLimeUnitCost), CalcPropertiesStringArray); }
        }

        private double _siloSystem120TonFoundationArea;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double SiloSystem120TonFoundationArea
        {
            get { return _siloSystem120TonFoundationArea; }
            set { ChangeAndNotify(ref _siloSystem120TonFoundationArea, value, nameof(SiloSystem120TonFoundationArea), CalcPropertiesStringArray); }
        }

        private double _siloSystem120TonDiameter;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double SiloSystem120TonDiameter
        {
            get { return _siloSystem120TonDiameter; }
            set { ChangeAndNotify(ref _siloSystem120TonDiameter, value, nameof(SiloSystem120TonDiameter), CalcPropertiesStringArray); }
        }

        private double _siloSystem120TonConcreteThickness;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double SiloSystem120TonConcreteThickness
        {
            get { return _siloSystem120TonConcreteThickness; }
            set { ChangeAndNotify(ref _siloSystem120TonConcreteThickness, value, nameof(SiloSystem120TonConcreteThickness), CalcPropertiesStringArray); }
        }

        private double _siloSystem120TonConcreteVolume;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double SiloSystem120TonConcreteVolume
        {
            get { return _siloSystem120TonConcreteVolume; }
            set { ChangeAndNotify(ref _siloSystem120TonConcreteVolume, value, nameof(SiloSystem120TonConcreteVolume), CalcPropertiesStringArray); }
        }

        private string _siloSystemNameEstimated;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string SiloSystemNameEstimated
        {
            get { return _siloSystemNameEstimated; }
            set { ChangeAndNotify(ref _siloSystemNameEstimated, value, nameof(SiloSystemNameEstimated), CalcPropertiesStringArray); }
        }

        private double _siloSystemQuantityEstimated;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double SiloSystemQuantityEstimated
        {
            get { return _siloSystemQuantityEstimated; }
            set { ChangeAndNotify(ref _siloSystemQuantityEstimated, value, nameof(SiloSystemQuantityEstimated), CalcPropertiesStringArray); }
        }

        private double _siloSystemWeightEstimated;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double SiloSystemWeightEstimated
        {
            get { return _siloSystemWeightEstimated; }
            set { ChangeAndNotify(ref _siloSystemWeightEstimated, value, nameof(SiloSystemWeightEstimated), CalcPropertiesStringArray); }
        }

        private decimal _siloSystemUnitCostEstimated;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal SiloSystemUnitCostEstimated
        {
            get { return _siloSystemUnitCostEstimated; }
            set { ChangeAndNotify(ref _siloSystemUnitCostEstimated, value, nameof(SiloSystemUnitCostEstimated), CalcPropertiesStringArray); }
        }

        private double _siloSystemFoundationAreaEstimated;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double SiloSystemFoundationAreaEstimated
        {
            get { return _siloSystemFoundationAreaEstimated; }
            set { ChangeAndNotify(ref _siloSystemFoundationAreaEstimated, value, nameof(SiloSystemFoundationAreaEstimated), CalcPropertiesStringArray); }
        }

        private double _siloSystemDiameterEstimated;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double SiloSystemDiameterEstimated
        {
            get { return _siloSystemDiameterEstimated; }
            set { ChangeAndNotify(ref _siloSystemDiameterEstimated, value, nameof(SiloSystemDiameterEstimated), CalcPropertiesStringArray); }
        }

        private double _siloSystemConcreteThicknessEstimated;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double SiloSystemConcreteThicknessEstimated
        {
            get { return _siloSystemConcreteThicknessEstimated; }
            set { ChangeAndNotify(ref _siloSystemConcreteThicknessEstimated, value, nameof(SiloSystemConcreteThicknessEstimated), CalcPropertiesStringArray); }
        }

        private double _siloSystemConcreteVolumeEstimated;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double SiloSystemConcreteVolumeEstimated
        {
            get { return _siloSystemConcreteVolumeEstimated; }
            set { ChangeAndNotify(ref _siloSystemConcreteVolumeEstimated, value, nameof(SiloSystemConcreteVolumeEstimated), CalcPropertiesStringArray); }
        }

        private string _siloSystemNameUserSpecified;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string SiloSystemNameUserSpecified
        {
            get { return _siloSystemNameUserSpecified; }
            set { ChangeAndNotify(ref _siloSystemNameUserSpecified, value, nameof(SiloSystemNameUserSpecified), CalcPropertiesStringArray); }
        }

        private double _siloSystemQuantityUserSpecified;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double SiloSystemQuantityUserSpecified
        {
            get { return _siloSystemQuantityUserSpecified; }
            set { ChangeAndNotify(ref _siloSystemQuantityUserSpecified, value, nameof(SiloSystemQuantityUserSpecified), CalcPropertiesStringArray); }
        }

        private double _siloSystemWeightUserSpecified;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double SiloSystemWeightUserSpecified
        {
            get { return _siloSystemWeightUserSpecified; }
            set { ChangeAndNotify(ref _siloSystemWeightUserSpecified, value, nameof(SiloSystemWeightUserSpecified), CalcPropertiesStringArray); }
        }

        private decimal _siloSystemUnitCostUserSpecified;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal SiloSystemUnitCostUserSpecified
        {
            get { return _siloSystemUnitCostUserSpecified; }
            set { ChangeAndNotify(ref _siloSystemUnitCostUserSpecified, value, nameof(SiloSystemUnitCostUserSpecified), CalcPropertiesStringArray); }
        }

        private double _siloSystemFoundationAreaUserSpecified;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double SiloSystemFoundationAreaUserSpecified
        {
            get { return _siloSystemFoundationAreaUserSpecified; }
            set { ChangeAndNotify(ref _siloSystemFoundationAreaUserSpecified, value, nameof(SiloSystemFoundationAreaUserSpecified), CalcPropertiesStringArray); }
        }

        private double _siloSystemDiameterUserSpecified;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double SiloSystemDiameterUserSpecified
        {
            get { return _siloSystemDiameterUserSpecified; }
            set { ChangeAndNotify(ref _siloSystemDiameterUserSpecified, value, nameof(SiloSystemDiameterUserSpecified), CalcPropertiesStringArray); }
        }

        private double _siloSystemConcreteThicknessUserSpecified;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double SiloSystemConcreteThicknessUserSpecified
        {
            get { return _siloSystemConcreteThicknessUserSpecified; }
            set { ChangeAndNotify(ref _siloSystemConcreteThicknessUserSpecified, value, nameof(SiloSystemConcreteThicknessUserSpecified), CalcPropertiesStringArray); }
        }

        private double _siloSystemConcreteVolumeUserSpecified;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double SiloSystemConcreteVolumeUserSpecified
        {
            get { return _siloSystemConcreteVolumeUserSpecified; }
            set { ChangeAndNotify(ref _siloSystemConcreteVolumeUserSpecified, value, nameof(SiloSystemConcreteVolumeUserSpecified), CalcPropertiesStringArray); }
        }

        
        private string _siloSystemName;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string SiloSystemName
        {
            get
            {
                switch (SiloSystemOptionsProperty)
                {
                    case RadioButtonsSiloSystemOptionsEnum.OptionEstimated:
                        _siloSystemName = SiloSystemNameEstimated;
                        break;
                    case RadioButtonsSiloSystemOptionsEnum.OptionUserSpecified:
                        _siloSystemName = SiloSystemNameUserSpecified;
                        break;
                    default:
                        break;
                }
                return _siloSystemName;
            }
            set { ChangeAndNotify(ref _siloSystemName, value, nameof(SiloSystemName), CalcPropertiesStringArray); }
        }

        private double _siloSystemQuantity;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double SiloSystemQuantity
        {
            get
            {
                switch (SiloSystemOptionsProperty)
                {
                    case RadioButtonsSiloSystemOptionsEnum.OptionEstimated:
                        _siloSystemQuantity = SiloSystemQuantityEstimated;
                        break;
                    case RadioButtonsSiloSystemOptionsEnum.OptionUserSpecified:
                        _siloSystemQuantity = SiloSystemQuantityUserSpecified;
                        break;
                    default:
                        break;
                }
                return _siloSystemQuantity;
            }
            set { ChangeAndNotify(ref _siloSystemQuantity, value, nameof(SiloSystemQuantity), CalcPropertiesStringArray); }
        }

        private double _siloSystemWeight;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double SiloSystemWeight
        {
            get
            {
                switch (SiloSystemOptionsProperty)
                {
                    case RadioButtonsSiloSystemOptionsEnum.OptionEstimated:
                        _siloSystemWeight = SiloSystemWeightEstimated;
                        break;
                    case RadioButtonsSiloSystemOptionsEnum.OptionUserSpecified:
                        _siloSystemWeight = SiloSystemWeightUserSpecified;
                        break;
                    default:
                        break;
                }
                return _siloSystemWeight;
            }
            set { ChangeAndNotify(ref _siloSystemWeight, value, nameof(SiloSystemWeight), CalcPropertiesStringArray); }
        }

        private decimal _siloSystemUnitCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal SiloSystemUnitCost
        {
            get
            {
                if (IsSiloSystem)
                {
                    switch (SiloSystemOptionsProperty)
                    {
                        case RadioButtonsSiloSystemOptionsEnum.OptionEstimated:
                            _siloSystemUnitCost = SiloSystemUnitCostEstimated;
                            break;
                        case RadioButtonsSiloSystemOptionsEnum.OptionUserSpecified:
                            _siloSystemUnitCost = SiloSystemUnitCostUserSpecified;
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    _siloSystemUnitCost = 0m;
                }

                return _siloSystemUnitCost;
            }
            set { ChangeAndNotify(ref _siloSystemUnitCost, value, nameof(SiloSystemUnitCost), CalcPropertiesStringArray); }
        }

        private double _siloSystemFoundationArea;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double SiloSystemFoundationArea
        {
            get
            {
                switch (SiloSystemOptionsProperty)
                {
                    case RadioButtonsSiloSystemOptionsEnum.OptionEstimated:
                        _siloSystemFoundationArea = SiloSystemFoundationAreaEstimated;
                        break;
                    case RadioButtonsSiloSystemOptionsEnum.OptionUserSpecified:
                        _siloSystemFoundationArea = SiloSystemFoundationAreaUserSpecified;
                        break;
                    default:
                        break;
                }
                return _siloSystemFoundationArea;
            }
            set { ChangeAndNotify(ref _siloSystemFoundationArea, value, nameof(SiloSystemFoundationArea), CalcPropertiesStringArray); }
        }

        private double _siloSystemDiameter;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double SiloSystemDiameter
        {
            get
            {
                switch (SiloSystemOptionsProperty)
                {
                    case RadioButtonsSiloSystemOptionsEnum.OptionEstimated:
                        _siloSystemDiameter = SiloSystemDiameterEstimated;
                        break;
                    case RadioButtonsSiloSystemOptionsEnum.OptionUserSpecified:
                        _siloSystemDiameter = SiloSystemDiameterUserSpecified;
                        break;
                    default:
                        break;
                }
                return _siloSystemDiameter;
            }
            set { ChangeAndNotify(ref _siloSystemDiameter, value, nameof(SiloSystemDiameter), CalcPropertiesStringArray); }
        }

        private double _siloSystemConcreteThickness;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double SiloSystemConcreteThickness
        {
            get
            {
                switch (SiloSystemOptionsProperty)
                {
                    case RadioButtonsSiloSystemOptionsEnum.OptionEstimated:
                        _siloSystemConcreteThickness = SiloSystemConcreteThicknessEstimated;
                        break;
                    case RadioButtonsSiloSystemOptionsEnum.OptionUserSpecified:
                        _siloSystemConcreteThickness = SiloSystemConcreteThicknessUserSpecified;
                        break;
                    default:
                        break;
                }
                return _siloSystemConcreteThickness;
            }
            set { ChangeAndNotify(ref _siloSystemConcreteThickness, value, nameof(SiloSystemConcreteThickness), CalcPropertiesStringArray); }
        }

        private double _siloSystemConcreteVolume;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double SiloSystemConcreteVolume
        {
            get
            {
                switch (SiloSystemOptionsProperty)
                {
                    case RadioButtonsSiloSystemOptionsEnum.OptionEstimated:
                        _siloSystemConcreteVolume = SiloSystemConcreteVolumeEstimated;
                        break;
                    case RadioButtonsSiloSystemOptionsEnum.OptionUserSpecified:
                        _siloSystemConcreteVolume = SiloSystemConcreteVolumeUserSpecified;
                        break;
                    default:
                        break;
                }
                return _siloSystemConcreteVolume;
            }
            set { ChangeAndNotify(ref _siloSystemConcreteVolume, value, nameof(SiloSystemConcreteVolume), CalcPropertiesStringArray); }
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

        private bool _isDustCollectorBlower;
        /// <summary>
        /// User specified
        /// </summary>
        public bool IsDustCollectorBlower
        {
            get { return _isDustCollectorBlower; }
            set { ChangeAndNotify(ref _isDustCollectorBlower, value, nameof(IsDustCollectorBlower), CalcPropertiesStringArray); }
        }

        private double _dustCollectorBlowerTime;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double DustCollectorBlowerTime
        {
            get { return _dustCollectorBlowerTime; }
            set { ChangeAndNotify(ref _dustCollectorBlowerTime, value, nameof(DustCollectorBlowerTime), CalcPropertiesStringArray); }
        }

        private double _dustCollectorBlowerPowerRequirement;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double DustCollectorBlowerPowerRequirement
        {
            get { return _dustCollectorBlowerPowerRequirement; }
            set { ChangeAndNotify(ref _dustCollectorBlowerPowerRequirement, value, nameof(DustCollectorBlowerPowerRequirement), CalcPropertiesStringArray); }
        }

        private bool _isBinActivator;
        /// <summary>
        /// User specified
        /// </summary>
        public bool IsBinActivator
        {
            get { return _isBinActivator; }
            set { ChangeAndNotify(ref _isBinActivator, value, nameof(IsBinActivator), CalcPropertiesStringArray); }
        }

        private double _binActivatorPowerRequirement;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double BinActivatorPowerRequirement
        {
            get { return _binActivatorPowerRequirement; }
            set { ChangeAndNotify(ref _binActivatorPowerRequirement, value, nameof(BinActivatorPowerRequirement), CalcPropertiesStringArray); }
        }

        private double _binActivatorCycleFrequency;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double BinActivatorCycleFrequency
        {
            get { return _binActivatorCycleFrequency; }
            set { ChangeAndNotify(ref _binActivatorCycleFrequency, value, nameof(BinActivatorCycleFrequency), CalcPropertiesStringArray); }
        }

        private double _binActivatorDaysPerYear;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double BinActivatorDaysPerYear
        {
            get { return _binActivatorDaysPerYear; }
            set { ChangeAndNotify(ref _binActivatorDaysPerYear, value, nameof(BinActivatorDaysPerYear), CalcPropertiesStringArray); }
        }

        private bool _isScrewFeeder;
        /// <summary>
        /// User specified
        /// </summary>
        public bool IsScrewFeeder
        {
            get { return _isScrewFeeder; }
            set { ChangeAndNotify(ref _isScrewFeeder, value, nameof(IsScrewFeeder), CalcPropertiesStringArray); }
        }

        private double _screwFeederPowerRequirement;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double ScrewFeederPowerRequirement
        {
            get { return _screwFeederPowerRequirement; }
            set { ChangeAndNotify(ref _screwFeederPowerRequirement, value, nameof(ScrewFeederPowerRequirement), CalcPropertiesStringArray); }
        }

        private double _screwFeederHoursPerDay;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double ScrewFeederHoursPerDay
        {
            get { return _screwFeederHoursPerDay; }
            set { ChangeAndNotify(ref _screwFeederHoursPerDay, value, nameof(ScrewFeederHoursPerDay), CalcPropertiesStringArray); }
        }

        private double _screwFeederDaysPerYear;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double ScrewFeederDaysPerYear
        {
            get { return _screwFeederDaysPerYear; }
            set { ChangeAndNotify(ref _screwFeederDaysPerYear, value, nameof(ScrewFeederDaysPerYear), CalcPropertiesStringArray); }
        }

        private bool _isSlurryMixerAndPump;
        /// <summary>
        /// User specified
        /// </summary>
        public bool IsSlurryMixerAndPump
        {
            get { return _isSlurryMixerAndPump; }
            set { ChangeAndNotify(ref _isSlurryMixerAndPump, value, nameof(IsSlurryMixerAndPump), CalcPropertiesStringArray); }
        }

        private double _slurryMixerPowerRequirement;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double SlurryMixerPowerRequirement
        {
            get { return _slurryMixerPowerRequirement; }
            set { ChangeAndNotify(ref _slurryMixerPowerRequirement, value, nameof(SlurryMixerPowerRequirement), CalcPropertiesStringArray); }
        }

        private double _slurryPumpPowerRequirement;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double SlurryPumpPowerRequirement
        {
            get { return _slurryPumpPowerRequirement; }
            set { ChangeAndNotify(ref _slurryPumpPowerRequirement, value, nameof(SlurryPumpPowerRequirement), CalcPropertiesStringArray); }
        }

        private double _slurryMixerAndPumpHoursPerDay;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double SlurryMixerAndPumpHoursPerDay
        {
            get { return _slurryMixerAndPumpHoursPerDay; }
            set { ChangeAndNotify(ref _slurryMixerAndPumpHoursPerDay, value, nameof(SlurryMixerAndPumpHoursPerDay), CalcPropertiesStringArray); }
        }

        private double _slurryMixerAndPumpDaysPerYear;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double SlurryMixerAndPumpDaysPerYear
        {
            get { return _slurryMixerAndPumpDaysPerYear; }
            set { ChangeAndNotify(ref _slurryMixerAndPumpDaysPerYear, value, nameof(SlurryMixerAndPumpDaysPerYear), CalcPropertiesStringArray); }
        }

        private bool _isSiloExhaustFan;
        /// <summary>
        /// User specified
        /// </summary>
        public bool IsSiloExhaustFan
        {
            get { return _isSiloExhaustFan; }
            set { ChangeAndNotify(ref _isSiloExhaustFan, value, nameof(IsSiloExhaustFan), CalcPropertiesStringArray); }
        }

        private double _slurryPumpExhaustFanPowerRequirement;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double SiloExhaustFanPowerRequirement
        {
            get { return _slurryPumpExhaustFanPowerRequirement; }
            set { ChangeAndNotify(ref _slurryPumpExhaustFanPowerRequirement, value, nameof(SiloExhaustFanPowerRequirement), CalcPropertiesStringArray); }
        }

        private double _slurryPumpExhaustFanHoursPerDay;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double SiloExhaustFanHoursPerDay
        {
            get { return _slurryPumpExhaustFanHoursPerDay; }
            set { ChangeAndNotify(ref _slurryPumpExhaustFanHoursPerDay, value, nameof(SiloExhaustFanHoursPerDay), CalcPropertiesStringArray); }
        }

        private double _slurryPumpExhaustFanDaysPerYear;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double SiloExhaustFanDaysPerYear
        {
            get { return _slurryPumpExhaustFanDaysPerYear; }
            set { ChangeAndNotify(ref _slurryPumpExhaustFanDaysPerYear, value, nameof(SiloExhaustFanDaysPerYear), CalcPropertiesStringArray); }
        }

        private bool _isSiloSpaceHeater;
        /// <summary>
        /// User specified
        /// </summary>
        public bool IsSiloSpaceHeater
        {
            get { return _isSiloSpaceHeater; }
            set { ChangeAndNotify(ref _isSiloSpaceHeater, value, nameof(IsSiloSpaceHeater), CalcPropertiesStringArray); }
        }

        private double _slurryPumpSpaceHeaterPowerRequirement;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double SiloSpaceHeaterPowerRequirement
        {
            get { return _slurryPumpSpaceHeaterPowerRequirement; }
            set { ChangeAndNotify(ref _slurryPumpSpaceHeaterPowerRequirement, value, nameof(SiloSpaceHeaterPowerRequirement), CalcPropertiesStringArray); }
        }

        private double _slurryPumpSpaceHeaterHoursPerDay;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double SiloSpaceHeaterHoursPerDay
        {
            get { return _slurryPumpSpaceHeaterHoursPerDay; }
            set { ChangeAndNotify(ref _slurryPumpSpaceHeaterHoursPerDay, value, nameof(SiloSpaceHeaterHoursPerDay), CalcPropertiesStringArray); }
        }

        private double _slurryPumpSpaceHeaterDaysPerYear;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double SiloSpaceHeaterDaysPerYear
        {
            get { return _slurryPumpSpaceHeaterDaysPerYear; }
            set { ChangeAndNotify(ref _slurryPumpSpaceHeaterDaysPerYear, value, nameof(SiloSpaceHeaterDaysPerYear), CalcPropertiesStringArray); }
        }


        private decimal _siloPumpUnitCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal SiloUnitCost
        {
            get { return _siloPumpUnitCost; }
            set { ChangeAndNotify(ref _siloPumpUnitCost, value, nameof(SiloUnitCost), CalcPropertiesStringArray); }
        }

        private decimal _binActivatorUnitCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal BinActivatorUnitCost
        {
            get { return _binActivatorUnitCost; }
            set { ChangeAndNotify(ref _binActivatorUnitCost, value, nameof(BinActivatorUnitCost), CalcPropertiesStringArray); }
        }

        private decimal _screwFeederUnitCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal ScrewFeederUnitCost
        {
            get { return _screwFeederUnitCost; }
            set { ChangeAndNotify(ref _screwFeederUnitCost, value, nameof(ScrewFeederUnitCost), CalcPropertiesStringArray); }
        }

        private decimal _slurryMixerUnitCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal SlurryMixerUnitCost
        {
            get { return _slurryMixerUnitCost; }
            set { ChangeAndNotify(ref _slurryMixerUnitCost, value, nameof(SlurryMixerUnitCost), CalcPropertiesStringArray); }
        }

        private decimal _slurryPumpUnitCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal SlurryPumpUnitCost
        {
            get { return _slurryPumpUnitCost; }
            set { ChangeAndNotify(ref _slurryPumpUnitCost, value, nameof(SlurryPumpUnitCost), CalcPropertiesStringArray); }
        }

        private decimal _siloSpaceHeaterPumpUnitCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal SiloSpaceHeaterUnitCost
        {
            get { return _siloSpaceHeaterPumpUnitCost; }
            set { ChangeAndNotify(ref _siloSpaceHeaterPumpUnitCost, value, nameof(SiloSpaceHeaterUnitCost), CalcPropertiesStringArray); }
        }

        private decimal _slurryTankPumpUnitCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal SlurryTankUnitCost
        {
            get { return _slurryTankPumpUnitCost; }
            set { ChangeAndNotify(ref _slurryTankPumpUnitCost, value, nameof(SlurryTankUnitCost), CalcPropertiesStringArray); }
        }

        private decimal _hardwareSoftwarePumpUnitCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal HardwareSoftwareUnitCost
        {
            get { return _hardwareSoftwarePumpUnitCost; }
            set { ChangeAndNotify(ref _hardwareSoftwarePumpUnitCost, value, nameof(HardwareSoftwareUnitCost), CalcPropertiesStringArray); }
        }

        private decimal _slakerPumpUnitCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal SlakerUnitCost
        {
            get { return _slakerPumpUnitCost; }
            set { ChangeAndNotify(ref _slakerPumpUnitCost, value, nameof(SlakerUnitCost), CalcPropertiesStringArray); }
        }
        #endregion

        #region Properties - Foundation

        private decimal _foundationConcreteMaterialAndPlacementCost;
        /// <summary>
        ///  User specified 
        /// </summary> 
        public decimal FoundationConcreteMaterialAndPlacementCost
        {
            get { return _foundationConcreteMaterialAndPlacementCost; }
            set { ChangeAndNotify(ref _foundationConcreteMaterialAndPlacementCost, value, nameof(FoundationConcreteMaterialAndPlacementCost), CalcPropertiesStringArray); }
        }

        /// <summary>
        ///  Radio button binding with enumeration 
        /// </summary>
        public enum RadioButtonsFoundationOptionsEnum
        {
            OptionEstimated,
            OptionUserSpecified,
        }

        private RadioButtonsFoundationOptionsEnum _foundationOptionsProperty;
        /// <summary>
        ///  User specified 
        /// </summary>
        public RadioButtonsFoundationOptionsEnum FoundationOptionsProperty
        {
            get { return _foundationOptionsProperty; }
            set { ChangeAndNotify(ref _foundationOptionsProperty, value, nameof(FoundationOptionsProperty), CalcPropertiesStringArray); }
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

        private double _foundationVolumeUserSpecified;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double FoundationVolumeUserSpecified
        {
            get { return _foundationVolumeUserSpecified; }
            set { ChangeAndNotify(ref _foundationVolumeUserSpecified, value, nameof(FoundationVolumeUserSpecified), CalcPropertiesStringArray); }
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
       
        private double _calcLimeDailyConsumptionTonsStoichiometric;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLimeDailyConsumptionTonsStoichiometric
        {
            get
            {
                switch (LimeOptionsProperty)
                {
                    case RadioButtonsLimeOptionsEnum.OptionHydratedLime:
                        _calcLimeDailyConsumptionTonsStoichiometric = DryLimeCalculations.CalcLimeDailyConsumptionTonsStoichiometric(DesignFlow, HotAcidity, HydratedLimePurity, HydratedLimeDissolutionEfficiency);
                        break;
                    case RadioButtonsLimeOptionsEnum.OptionDryLime:
                        _calcLimeDailyConsumptionTonsStoichiometric = DryLimeCalculations.CalcLimeDailyConsumptionTonsStoichiometric(DesignFlow, HotAcidity, DryLimePurity, DryLimeDissolutionEfficiency);
                        break;
                    default:
                        break;
                }
                return _calcLimeDailyConsumptionTonsStoichiometric;
            }
            set { ChangeAndNotify(ref _calcLimeDailyConsumptionTonsStoichiometric, value); }
        }

        private double _calcLimeMonthlyConsumptionTonsStoichiometric;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLimeMonthlyConsumptionTonsStoichiometric
        {
            get { return DryLimeCalculations.CalcLimeMonthlyConsumptionTonsStoichiometric(CalcLimeDailyConsumptionTonsStoichiometric); }
            set { ChangeAndNotify(ref _calcLimeMonthlyConsumptionTonsStoichiometric, value); }
        }


        private double _calcLimeAnnualConsumptionTonsStoichiometric;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLimeAnnualConsumptionTonsStoichiometric
        {
            get { return DryLimeCalculations.CalcLimeAnnualConsumptionTonsStoichiometric(CalcLimeDailyConsumptionTonsStoichiometric); }
            set { ChangeAndNotify(ref _calcLimeAnnualConsumptionTonsStoichiometric, value); }
        }

        #endregion

        #region Properties - Sizing Summary: Titration

        private double _calcLimeDailyConsumptionTonsTitration;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLimeDailyConsumptionTonsTitration
        {
            get { return DryLimeCalculations.CalcLimeDailyConsumptionTonsTitration(TitrationQuantity, DesignFlow); }
            set { ChangeAndNotify(ref _calcLimeDailyConsumptionTonsTitration, value); }
        }

        private double _calcLimeMonthlyConsumptionTonsTitration;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLimeMonthlyConsumptionTonsTitration
        {
            get { return DryLimeCalculations.CalcLimeMonthlyConsumptionTonsTitration(CalcLimeDailyConsumptionTonsTitration); }
            set { ChangeAndNotify(ref _calcLimeMonthlyConsumptionTonsTitration, value); }
        }


        private double _calcLimeAnnualConsumptionTonsTitration;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLimeAnnualConsumptionTonsTitration
        {
            get { return DryLimeCalculations.CalcLimeAnnualConsumptionTonsTitration(CalcLimeDailyConsumptionTonsTitration); }
            set { ChangeAndNotify(ref _calcLimeAnnualConsumptionTonsTitration, value); }
        }
        #endregion

        #region Properties - Sizing Summary: User Specified Lime Quantity

        private double _calcLimeDailyConsumptionTonsUserSpecified;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLimeDailyConsumptionTonsUserSpecified
        {
            get { return DryLimeCalculations.CalcLimeDailyConsumptionTonsUserSpecified(LimeUserSpecifiedQuantity); }
            set { ChangeAndNotify(ref _calcLimeDailyConsumptionTonsUserSpecified, value); }
        }

        private double _calcLimeMonthlyConsumptionTonsUserSpecified;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLimeMonthlyConsumptionTonsUserSpecified
        {
            get { return DryLimeCalculations.CalcLimeMonthlyConsumptionTonsUserSpecified(LimeUserSpecifiedQuantity); }
            set { ChangeAndNotify(ref _calcLimeMonthlyConsumptionTonsUserSpecified, value); }
        }


        private double _calcLimeAnnualConsumptionTonsUserSpecified;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLimeAnnualConsumptionTonsUserSpecified
        {
            get { return DryLimeCalculations.CalcLimeAnnualConsumptionTonsUserSpecified(LimeUserSpecifiedQuantity); }
            set { ChangeAndNotify(ref _calcLimeAnnualConsumptionTonsUserSpecified, value); }
        }


        #endregion

        #region Properties - Sizing Summary: Daily Consumption and Annual Consumption

        private double _calcLimeDailyConsumptionTons;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLimeDailyConsumptionTons
        {
            get
            {
                switch (ChemicalConsumptionOptionsProperty)
                {
                    case RadioButtonsChemicalConsumptionOptionsEnum.OptionStoichiometric:
                        _calcLimeDailyConsumptionTons = CalcLimeDailyConsumptionTonsStoichiometric;
                        break;
                    case RadioButtonsChemicalConsumptionOptionsEnum.OptionTitration:
                        _calcLimeDailyConsumptionTons = CalcLimeDailyConsumptionTonsTitration;
                        break;
                    case RadioButtonsChemicalConsumptionOptionsEnum.OptionUserSpecifiedQuantity:
                        _calcLimeDailyConsumptionTons = CalcLimeDailyConsumptionTonsUserSpecified;
                        break;
                    default:
                        break;
                }

                return _calcLimeDailyConsumptionTons;
            }
            set { ChangeAndNotify(ref _calcLimeDailyConsumptionTons, value); }
        }

        private double _calcLimeMonthlyConsumptionTons;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLimeMonthlyConsumptionTons
        {
            get
            {
                switch (ChemicalConsumptionOptionsProperty)
                {
                    case RadioButtonsChemicalConsumptionOptionsEnum.OptionStoichiometric:
                        _calcLimeMonthlyConsumptionTons = CalcLimeMonthlyConsumptionTonsStoichiometric;
                        break;
                    case RadioButtonsChemicalConsumptionOptionsEnum.OptionTitration:
                        _calcLimeMonthlyConsumptionTons = CalcLimeMonthlyConsumptionTonsTitration;
                        break;
                    case RadioButtonsChemicalConsumptionOptionsEnum.OptionUserSpecifiedQuantity:
                        _calcLimeMonthlyConsumptionTons = CalcLimeMonthlyConsumptionTonsUserSpecified;
                        break;
                    default:
                        break;
                }

                return _calcLimeMonthlyConsumptionTons;
            }
            set { ChangeAndNotify(ref _calcLimeMonthlyConsumptionTons, value); }
        }

        private double _calcLimeAnnualConsumptionTons;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLimeAnnualConsumptionTons
        {
            get
            {
                switch (ChemicalConsumptionOptionsProperty)
                {
                    case RadioButtonsChemicalConsumptionOptionsEnum.OptionStoichiometric:
                        _calcLimeAnnualConsumptionTons = CalcLimeAnnualConsumptionTonsStoichiometric;
                        break;
                    case RadioButtonsChemicalConsumptionOptionsEnum.OptionTitration:
                        _calcLimeAnnualConsumptionTons = CalcLimeAnnualConsumptionTonsTitration;
                        break;
                    case RadioButtonsChemicalConsumptionOptionsEnum.OptionUserSpecifiedQuantity:
                        _calcLimeAnnualConsumptionTons = CalcLimeAnnualConsumptionTonsUserSpecified;
                        break;
                    default:
                        break;
                }

                return _calcLimeAnnualConsumptionTons;
            }
            set { ChangeAndNotify(ref _calcLimeAnnualConsumptionTons, value); }
        }
        #endregion

        #region Properties - Sizing Summary: Silo Refill Frequency, Foundation Area, Foundation Depth, and Foundation Volume

        private double _calcLimeSiloRefillFrequency;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLimeSiloRefillFrequency
        {
            get { return DryLimeCalculations.CalcLimeSiloRefillFrequency(SiloSystemWeight, CalcLimeDailyConsumptionTonsStoichiometric); }
            set { ChangeAndNotify(ref _calcLimeSiloRefillFrequency, value); }
        }

        private double _calcSiloFoundationArea;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcSiloFoundationArea
        {
            get
            {
                switch (FoundationOptionsProperty)
                {
                    case RadioButtonsFoundationOptionsEnum.OptionEstimated:
                        if (IsSiloSystem)
                        {
                            _calcSiloFoundationArea = DryLimeCalculations.CalcSiloFoundationArea(SiloSystemFoundationArea); 
                        }
                        else
                        {
                            _calcSiloFoundationArea = 0;
                        }
                        break;
                    case RadioButtonsFoundationOptionsEnum.OptionUserSpecified:
                        _calcSiloFoundationArea = 0;
                        break;
                    default:
                        break;
                }
                return _calcSiloFoundationArea;
            }
            set { ChangeAndNotify(ref _calcSiloFoundationArea, value); }
        }

        private double _foundationAreaData;
        /// <summary>
        /// Calculated
        /// </summary>
        public double FoundationAreaData
        {
            get { return CalcSiloFoundationArea; }
            set { ChangeAndNotify(ref _foundationAreaData, value, nameof(FoundationAreaData)); }
        }


        private double _calcSiloFoundationDepth;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcSiloFoundationDepth
        {
            get
            {
                switch (FoundationOptionsProperty)
                {
                    case RadioButtonsFoundationOptionsEnum.OptionEstimated:
                        if (IsSiloSystem)
                        {
                            _calcSiloFoundationDepth = DryLimeCalculations.CalcSiloFoundationDepth(FoundationSiteSoilLoadBearingMultiplier, SiloSystemConcreteThickness);
                        }
                        else
                        {
                            _calcSiloFoundationDepth = 0;
                        }
                        break;
                    case RadioButtonsFoundationOptionsEnum.OptionUserSpecified:
                        _calcSiloFoundationDepth = 0;
                        break;
                    default:
                        break;
                }
                return _calcSiloFoundationDepth;
            }
            set { ChangeAndNotify(ref _calcSiloFoundationDepth, value); }
        }

        private double _calcFoundationAreaTimesDepth;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcFoundationAreaTimesDepth
        {
            get
            {
                _calcFoundationAreaTimesDepth = CalcSiloFoundationArea * CalcSiloFoundationDepth;

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

        private double _calcSiloFoundationVolume;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcSiloFoundationVolume
        {
            get
            {
                switch (FoundationOptionsProperty)
                {
                    case RadioButtonsFoundationOptionsEnum.OptionEstimated:
                        if (IsSiloSystem)
                        {
                            _calcSiloFoundationVolume = DryLimeCalculations.CalcSiloFoundationVolume(CalcSiloFoundationDepth, CalcSiloFoundationArea);
                        }
                        else
                        {
                            _calcSiloFoundationVolume = 0;
                        }
                        break;
                    case RadioButtonsFoundationOptionsEnum.OptionUserSpecified:
                        _calcSiloFoundationVolume = FoundationVolumeUserSpecified;
                        break;
                    default:
                        break;
                }
                return _calcSiloFoundationVolume;
            }
            set { ChangeAndNotify(ref _calcSiloFoundationVolume, value); }
        }

        #endregion

        #region Properties - Capital Costs

        public enum RadioButtonsSystemInstallOptionsEnum
        {
            OptionCostMultiplier,
            OptionUserSpecified,
        }

        private RadioButtonsSystemInstallOptionsEnum _systemInstallCostOptionsProperty;
        /// <summary>
        ///  User specified 
        /// </summary>
        public RadioButtonsSystemInstallOptionsEnum SystemInstallCostOptionsProperty
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
            get { return DryLimeCalculations.CalcSystemInstallCost(CalcStorageAndDispensingSystemCost, CalcFoundationCost, SystemInstallCostMultiplier); }
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
                    case RadioButtonsSystemInstallOptionsEnum.OptionCostMultiplier:
                        _calcSystemInstallCost = CalcSystemInstallCostMultiplier;
                        break;
                    case RadioButtonsSystemInstallOptionsEnum.OptionUserSpecified:
                        _calcSystemInstallCost = SystemInstallCostUserSpecified;
                        break;
                    default:
                        break;
                }
                return _calcSystemInstallCost;
            }
            set { ChangeAndNotify(ref _calcSystemInstallCost, value); }
        }

        private decimal _calcStorageAndDispensingSystemCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcStorageAndDispensingSystemCost
        {
            get { return DryLimeCalculations.CalcStorageAndDispensingSystemCost(SiloSystemQuantity, SiloSystemUnitCost); }
            set { ChangeAndNotify(ref _calcStorageAndDispensingSystemCost, value); }
        }

        private decimal _calcFoundationCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcFoundationCost
        {
            get { return DryLimeCalculations.CalcFoundationCost(CalcSiloFoundationVolume, FoundationConcreteMaterialAndPlacementCost); }
            set { ChangeAndNotify(ref _calcFoundationCost, value); }
        }

        private decimal _calcOtherCapitalItemsCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcOtherCapitalItemsCost
        {
            get
            {
                return DryLimeCalculations.CalcOtherCapitalItemsCost(OtherCapitalItemQuantity1, OtherCapitalItemUnitCost1,
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
                _calcCapitalCostTotal = DryLimeCalculations.CalcCapitalCostTotal(CalcStorageAndDispensingSystemCost, CalcFoundationCost, CalcSystemInstallCost, CalcOtherCapitalItemsCost);

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


        private decimal _calcSiloCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal CalcSiloCost
        {
            get
            {
                if (IsSiloSystem)
                {
                    _calcSiloCost = DryLimeCalculations.CalcSiloCost(SiloUnitCost, SystemInstallCostMultiplier);
                }
                else
                {
                    _calcSiloCost = 0m;
                }
                return _calcSiloCost;
            }
            set { ChangeAndNotify(ref _calcSiloCost, value, nameof(CalcSiloCost), CalcPropertiesStringArray); }
        }

        private decimal _calcBinActivatorCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal CalcBinActivatorCost
        {
            get
            {
                if (IsBinActivator)
                {
                    _calcBinActivatorCost = DryLimeCalculations.CalcBinActivatorCost(BinActivatorUnitCost, SystemInstallCostMultiplier);
                }
                else
                {
                    _calcBinActivatorCost = 0m;
                }
                return _calcBinActivatorCost;
            }
            set { ChangeAndNotify(ref _calcBinActivatorCost, value, nameof(CalcBinActivatorCost), CalcPropertiesStringArray); }
        }

        private decimal _calcScrewFeederCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal CalcScrewFeederCost
        {
            get
            {
                if (IsScrewFeeder)
                {
                    _calcScrewFeederCost = DryLimeCalculations.CalcScrewFeederCost(ScrewFeederUnitCost, SystemInstallCostMultiplier);
                }
                else
                {
                    _calcScrewFeederCost = 0m;
                }
                return _calcScrewFeederCost;
            }
            set { ChangeAndNotify(ref _calcScrewFeederCost, value, nameof(CalcScrewFeederCost), CalcPropertiesStringArray); }
        }

        private decimal _calcSlurryMixerCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal CalcSlurryMixerCost
        {
            get
            {
                if (IsSlurryMixerAndPump)
                {
                    _calcSlurryMixerCost = DryLimeCalculations.CalcSlurryMixerCost(SlurryMixerUnitCost, SystemInstallCostMultiplier);
                }
                else
                {
                    _calcSlurryMixerCost = 0m;
                }
                return _calcSlurryMixerCost;
            }
            set { ChangeAndNotify(ref _calcSlurryMixerCost, value, nameof(CalcSlurryMixerCost), CalcPropertiesStringArray); }
        }

        private decimal _calcSlurryPumpCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal CalcSlurryPumpCost
        {
            get
            {
                if (IsSlurryMixerAndPump)
                {
                    _calcSlurryPumpCost = DryLimeCalculations.CalcSlurryPumpCost(SlurryPumpUnitCost, SystemInstallCostMultiplier);
                }
                else
                {
                    _calcSlurryPumpCost = 0m;
                }
                return _calcSlurryPumpCost;
            }
            set { ChangeAndNotify(ref _calcSlurryPumpCost, value, nameof(CalcSlurryPumpCost), CalcPropertiesStringArray); }
        }

        private decimal _calcSiloSpaceHeaterCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal CalcSiloSpaceHeaterCost
        {
            get
            {
                if (IsSiloSpaceHeater)
                {
                    _calcSiloSpaceHeaterCost = DryLimeCalculations.CalcSiloSpaceHeaterCost(SiloSpaceHeaterUnitCost, SystemInstallCostMultiplier);
                }
                else
                {
                    _calcSiloSpaceHeaterCost = 0m;
                }
                return _calcSiloSpaceHeaterCost;
            }
            set { ChangeAndNotify(ref _calcSiloSpaceHeaterCost, value, nameof(CalcSiloSpaceHeaterCost), CalcPropertiesStringArray); }
        }

        private decimal _calcSlurryTankCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal CalcSlurryTankCost
        {
            get { return DryLimeCalculations.CalcSlurryTankCost(SlurryTankUnitCost, SystemInstallCostMultiplier); }
            set { ChangeAndNotify(ref _calcSlurryTankCost, value, nameof(CalcSlurryTankCost), CalcPropertiesStringArray); }
        }

        private decimal _calcHardwareSoftwareCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal CalcHardwareSoftwareCost
        {
            get { return DryLimeCalculations.CalcHardwareSoftwareCost(HardwareSoftwareUnitCost, SystemInstallCostMultiplier); }
            set { ChangeAndNotify(ref _calcHardwareSoftwareCost, value, nameof(CalcHardwareSoftwareCost), CalcPropertiesStringArray); }
        }

        private decimal _calcSlakerCost;
        /// <summary>
        /// User specified
        /// </summary>
        public decimal CalcSlakerCost
        {
            get
            {
                switch (LimeOptionsProperty)
                {
                    case RadioButtonsLimeOptionsEnum.OptionHydratedLime:
                        _calcSlakerCost = 0m;
                        break;
                    case RadioButtonsLimeOptionsEnum.OptionDryLime:
                        _calcSlakerCost = DryLimeCalculations.CalcSlakerCost(SlakerUnitCost, SystemInstallCostMultiplier);
                        break;
                    default:
                        break;
                }
                return _calcSlakerCost;
            }
            set { ChangeAndNotify(ref _calcSlakerCost, value, nameof(CalcSlakerCost), CalcPropertiesStringArray); }
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
            get
            {
                switch (LimeOptionsProperty)
                {
                    case RadioButtonsLimeOptionsEnum.OptionHydratedLime:
                        _calcAnnualCostChemicalEstimated = DryLimeCalculations.CalcAnnualCostChemical(CalcLimeAnnualConsumptionTons, HydratedLimeUnitCost);
                        break;
                    case RadioButtonsLimeOptionsEnum.OptionDryLime:
                        _calcAnnualCostChemicalEstimated = DryLimeCalculations.CalcAnnualCostChemical(CalcLimeAnnualConsumptionTons, DryLimeUnitCost);
                        break;
                    default:
                        break;
                }
                return _calcAnnualCostChemicalEstimated;  
            }
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
            get { return DryLimeCalculations.CalcAnnualCostOperationAndMaintanence(AnnualCostOperationAndMaintanenceMultiplier, CalcCapitalCostTotal); }
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

        // FIXME
        private decimal _calcDustCollectorBlowerElectricAmount;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcDustCollectorBlowerElectricAmount
        {
            get
            {
                if (IsDustCollectorBlower & CalcLimeSiloRefillFrequency != 0)
                {
                    _calcDustCollectorBlowerElectricAmount = DryLimeCalculations.CalcDustCollectorBlowerElectricAmount(DustCollectorBlowerPowerRequirement, CalcLimeSiloRefillFrequency, DustCollectorBlowerTime);
                }
                else
                {
                    _calcDustCollectorBlowerElectricAmount = 0;
                }
                return _calcDustCollectorBlowerElectricAmount;
            }
            set { ChangeAndNotify(ref _calcDustCollectorBlowerElectricAmount, value); }
        }

        private decimal _calcScrewFeederElectricAmount;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcScrewFeederElectricAmount
        {
            get
            {
                if (IsScrewFeeder)
                {
                    _calcScrewFeederElectricAmount = DryLimeCalculations.CalcScrewFeederElectricAmount(ScrewFeederPowerRequirement, ScrewFeederHoursPerDay, ScrewFeederDaysPerYear);
                }
                else
                {
                    _calcScrewFeederElectricAmount = 0;
                }
                return _calcScrewFeederElectricAmount;
            }
            set { ChangeAndNotify(ref _calcScrewFeederElectricAmount, value); }
        }

        private decimal _calcBinActivatorElectricAmount;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcBinActivatorElectricAmount
        {
            get
            {
                if (IsBinActivator)
                {
                    _calcBinActivatorElectricAmount = DryLimeCalculations.CalcBinActivatorElectricAmount(BinActivatorPowerRequirement, BinActivatorCycleFrequency, BinActivatorDaysPerYear);
                }
                else
                {
                    _calcBinActivatorElectricAmount = 0;
                }
                return _calcBinActivatorElectricAmount;
            }
            set { ChangeAndNotify(ref _calcBinActivatorElectricAmount, value); }
        }

        private decimal _calcSlurryMixerAndPumpElectricAmount;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcSlurryMixerAndPumpElectricAmount
        {
            get
            {
                if (IsSlurryMixerAndPump)
                {
                    _calcSlurryMixerAndPumpElectricAmount = DryLimeCalculations.CalcSlurryMixerAndPumpElectricAmount(SlurryMixerPowerRequirement, SlurryPumpPowerRequirement, SlurryMixerAndPumpHoursPerDay, SlurryMixerAndPumpDaysPerYear);
                }
                else
                {
                    _calcSlurryMixerAndPumpElectricAmount = 0;
                }
                return _calcSlurryMixerAndPumpElectricAmount;
            }
            set { ChangeAndNotify(ref _calcSlurryMixerAndPumpElectricAmount, value); }
        }

        private decimal _calcSiloExhaustFanElectricAmount;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcSiloExhaustFanElectricAmount
        {
            get
            {
                if (IsSlurryMixerAndPump)
                {
                    _calcSiloExhaustFanElectricAmount = DryLimeCalculations.CalcSiloExhaustFanElectricAmount(SiloExhaustFanPowerRequirement, SiloExhaustFanHoursPerDay, SiloExhaustFanDaysPerYear);
                }
                else
                {
                    _calcSiloExhaustFanElectricAmount = 0;
                }
                return _calcSiloExhaustFanElectricAmount;
            }
            set { ChangeAndNotify(ref _calcSiloExhaustFanElectricAmount, value); }
        }

        private decimal _calcSiloSpaceHeaterElectricAmount;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcSiloSpaceHeaterElectricAmount
        {
            get
            {
                if (IsSlurryMixerAndPump)
                {
                    _calcSiloSpaceHeaterElectricAmount = DryLimeCalculations.CalcSiloSpaceHeaterElectricAmount(SiloSpaceHeaterPowerRequirement, SiloSpaceHeaterHoursPerDay, SiloSpaceHeaterDaysPerYear);
                }
                else
                {
                    _calcSiloSpaceHeaterElectricAmount = 0;
                }
                return _calcSiloSpaceHeaterElectricAmount;
            }
            set { ChangeAndNotify(ref _calcSiloSpaceHeaterElectricAmount, value); }
        }

        private decimal _calcAnnualCostElectricEstimated;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcAnnualCostElectricEstimated
        {
            get { return DryLimeCalculations.CalcAnnualCostElectric(CalcDustCollectorBlowerElectricAmount, CalcScrewFeederElectricAmount, CalcBinActivatorElectricAmount,
                                                                    CalcSlurryMixerAndPumpElectricAmount, CalcSiloExhaustFanElectricAmount, CalcSiloSpaceHeaterElectricAmount,
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
                return DryLimeCalculations.CalcOtherAnnualItemsCost(OtherAnnualItemQuantity1, OtherAnnualItemUnitCost1,
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
                _calcAnnualCost = DryLimeCalculations.CalcAnnualCostTotal(CalcAnnualCostChemical, CalcAnnualCostOperationAndMaintanence, CalcAnnualCostElectric, CalcOtherAnnualItemsCost);

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

        private double _recapitalizationCostLifeCycleSilo;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostLifeCycleSilo
        {
            get { return _recapitalizationCostLifeCycleSilo; }
            set { ChangeAndNotify(ref _recapitalizationCostLifeCycleSilo, value, nameof(RecapitalizationCostLifeCycleSilo), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostLifeCycleBinActivator;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostLifeCycleBinActivator
        {
            get { return _recapitalizationCostLifeCycleBinActivator; }
            set { ChangeAndNotify(ref _recapitalizationCostLifeCycleBinActivator, value, nameof(RecapitalizationCostLifeCycleBinActivator), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostLifeCycleScrewFeeder;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostLifeCycleScrewFeeder
        {
            get { return _recapitalizationCostLifeCycleScrewFeeder; }
            set { ChangeAndNotify(ref _recapitalizationCostLifeCycleScrewFeeder, value, nameof(RecapitalizationCostLifeCycleScrewFeeder), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostLifeCycleSlurryMixer;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostLifeCycleSlurryMixer
        {
            get { return _recapitalizationCostLifeCycleSlurryMixer; }
            set { ChangeAndNotify(ref _recapitalizationCostLifeCycleSlurryMixer, value, nameof(RecapitalizationCostLifeCycleSlurryMixer), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostLifeCycleSlurryPump;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostLifeCycleSlurryPump
        {
            get { return _recapitalizationCostLifeCycleSlurryPump; }
            set { ChangeAndNotify(ref _recapitalizationCostLifeCycleSlurryPump, value, nameof(RecapitalizationCostLifeCycleSlurryPump), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostLifeCycleSiloSpaceHeater;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostLifeCycleSiloSpaceHeater
        {
            get { return _recapitalizationCostLifeCycleSiloSpaceHeater; }
            set { ChangeAndNotify(ref _recapitalizationCostLifeCycleSiloSpaceHeater, value, nameof(RecapitalizationCostLifeCycleSiloSpaceHeater), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostLifeCycleSlurryTank;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostLifeCycleSlurryTank
        {
            get { return _recapitalizationCostLifeCycleSlurryTank; }
            set { ChangeAndNotify(ref _recapitalizationCostLifeCycleSlurryTank, value, nameof(RecapitalizationCostLifeCycleSlurryTank), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostLifeCycleHardwareSoftware;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostLifeCycleHardwareSoftware
        {
            get { return _recapitalizationCostLifeCycleHardwareSoftware; }
            set { ChangeAndNotify(ref _recapitalizationCostLifeCycleHardwareSoftware, value, nameof(RecapitalizationCostLifeCycleHardwareSoftware), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostLifeCycleSlaker;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostLifeCycleSlaker
        {
            get { return _recapitalizationCostLifeCycleSlaker; }
            set { ChangeAndNotify(ref _recapitalizationCostLifeCycleSlaker, value, nameof(RecapitalizationCostLifeCycleSlaker), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostPercentReplacementSilo;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostPercentReplacementSilo
        {
            get { return _recapitalizationCostPercentReplacementSilo; }
            set { ChangeAndNotify(ref _recapitalizationCostPercentReplacementSilo, value, nameof(RecapitalizationCostPercentReplacementSilo), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostPercentReplacementBinActivator;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostPercentReplacementBinActivator
        {
            get { return _recapitalizationCostPercentReplacementBinActivator; }
            set { ChangeAndNotify(ref _recapitalizationCostPercentReplacementBinActivator, value, nameof(RecapitalizationCostPercentReplacementBinActivator), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostPercentReplacementScrewFeeder;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostPercentReplacementScrewFeeder
        {
            get { return _recapitalizationCostPercentReplacementScrewFeeder; }
            set { ChangeAndNotify(ref _recapitalizationCostPercentReplacementScrewFeeder, value, nameof(RecapitalizationCostPercentReplacementScrewFeeder), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostPercentReplacementSlurryMixer;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostPercentReplacementSlurryMixer
        {
            get { return _recapitalizationCostPercentReplacementSlurryMixer; }
            set { ChangeAndNotify(ref _recapitalizationCostPercentReplacementSlurryMixer, value, nameof(RecapitalizationCostPercentReplacementSlurryMixer), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostPercentReplacementSlurryPump;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostPercentReplacementSlurryPump
        {
            get { return _recapitalizationCostPercentReplacementSlurryPump; }
            set { ChangeAndNotify(ref _recapitalizationCostPercentReplacementSlurryPump, value, nameof(RecapitalizationCostPercentReplacementSlurryPump), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostPercentReplacementSiloSpaceHeater;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostPercentReplacementSiloSpaceHeater
        {
            get { return _recapitalizationCostPercentReplacementSiloSpaceHeater; }
            set { ChangeAndNotify(ref _recapitalizationCostPercentReplacementSiloSpaceHeater, value, nameof(RecapitalizationCostPercentReplacementSiloSpaceHeater), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostPercentReplacementSlurryTank;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostPercentReplacementSlurryTank
        {
            get { return _recapitalizationCostPercentReplacementSlurryTank; }
            set { ChangeAndNotify(ref _recapitalizationCostPercentReplacementSlurryTank, value, nameof(RecapitalizationCostPercentReplacementSlurryTank), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostPercentReplacementHardwareSoftware;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostPercentReplacementHardwareSoftware
        {
            get { return _recapitalizationCostPercentReplacementHardwareSoftware; }
            set { ChangeAndNotify(ref _recapitalizationCostPercentReplacementHardwareSoftware, value, nameof(RecapitalizationCostPercentReplacementHardwareSoftware), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostPercentReplacementSlaker;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostPercentReplacementSlaker
        {
            get { return _recapitalizationCostPercentReplacementSlaker; }
            set { ChangeAndNotify(ref _recapitalizationCostPercentReplacementSlaker, value, nameof(RecapitalizationCostPercentReplacementSlaker), CalcPropertiesStringArray); }
        }

        private decimal _calcRapitalizationCostSilo;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostSilo
        {
            get
            {
                return DryLimeCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
                                                                    RecapitalizationCostInflationRate, RecapitalizationCostLifeCycleSilo,
                                                                    CalcSiloCost, RecapitalizationCostPercentReplacementSilo);
            }
            set { ChangeAndNotify(ref _calcRapitalizationCostSilo, value); }
        }

        private decimal _calcRapitalizationCostBinActivator;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostBinActivator
        {
            get
            {
                return DryLimeCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
                                                                    RecapitalizationCostInflationRate, RecapitalizationCostLifeCycleBinActivator,
                                                                    CalcBinActivatorCost, RecapitalizationCostPercentReplacementBinActivator);
            }
            set { ChangeAndNotify(ref _calcRapitalizationCostBinActivator, value); }
        }

        private decimal _calcRapitalizationCostScrewFeeder;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostScrewFeeder
        {
            get
            {
                return DryLimeCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
                                                                    RecapitalizationCostInflationRate, RecapitalizationCostLifeCycleScrewFeeder,
                                                                    CalcScrewFeederCost, RecapitalizationCostPercentReplacementScrewFeeder);
            }
            set { ChangeAndNotify(ref _calcRapitalizationCostScrewFeeder, value); }
        }

        private decimal _calcRapitalizationCostSlurryMixer;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostSlurryMixer
        {
            get
            {
                return DryLimeCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
                                                                    RecapitalizationCostInflationRate, RecapitalizationCostLifeCycleSlurryMixer,
                                                                    CalcSlurryMixerCost, RecapitalizationCostPercentReplacementSlurryMixer);
            }
            set { ChangeAndNotify(ref _calcRapitalizationCostSlurryMixer, value); }
        }

        private decimal _calcRapitalizationCostSlurryPump;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostSlurryPump
        {
            get
            {
                return DryLimeCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
                                                                    RecapitalizationCostInflationRate, RecapitalizationCostLifeCycleSlurryPump,
                                                                    CalcSlurryPumpCost, RecapitalizationCostPercentReplacementSlurryPump);
            }
            set { ChangeAndNotify(ref _calcRapitalizationCostSlurryPump, value); }
        }

        private decimal _calcRapitalizationCostSiloSpaceHeater;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostSiloSpaceHeater
        {
            get
            {
                return DryLimeCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
                                                                    RecapitalizationCostInflationRate, RecapitalizationCostLifeCycleSiloSpaceHeater,
                                                                    CalcSiloSpaceHeaterCost, RecapitalizationCostPercentReplacementSiloSpaceHeater);
            }
            set { ChangeAndNotify(ref _calcRapitalizationCostSiloSpaceHeater, value); }
        }

        private decimal _calcRapitalizationCostSlurryTank;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostSlurryTank
        {
            get
            {
                return DryLimeCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
                                                                    RecapitalizationCostInflationRate, RecapitalizationCostLifeCycleSlurryTank,
                                                                    CalcSlurryTankCost, RecapitalizationCostPercentReplacementSlurryTank);
            }
            set { ChangeAndNotify(ref _calcRapitalizationCostSlurryTank, value); }
        }

        private decimal _calcRapitalizationCostHardwareSoftware;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostHardwareSoftware
        {
            get
            {
                return DryLimeCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
                                                                    RecapitalizationCostInflationRate, RecapitalizationCostLifeCycleHardwareSoftware,
                                                                    CalcHardwareSoftwareCost, RecapitalizationCostPercentReplacementHardwareSoftware);
            }
            set { ChangeAndNotify(ref _calcRapitalizationCostHardwareSoftware, value); }
        }

        private decimal _calcRapitalizationCostSlaker;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostSlaker
        {
            get
            {
                return DryLimeCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
                                                                    RecapitalizationCostInflationRate, RecapitalizationCostLifeCycleSlaker,
                                                                    CalcSlakerCost, RecapitalizationCostPercentReplacementSlaker);
            }
            set { ChangeAndNotify(ref _calcRapitalizationCostSlaker, value); }
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
            get { return DryLimeCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn, 
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
                    case "Silo":
                        item.MaterialCostDefault = CalcSiloCost;
                        break;
                    case "BinActivator":
                        item.MaterialCostDefault = CalcBinActivatorCost;
                        break;
                    case "ScrewFeeder":
                        item.MaterialCostDefault = CalcScrewFeederCost;
                        break;
                    case "Slurry Mixer":
                        item.MaterialCostDefault = CalcSlurryMixerCost;
                        break;
                    case "Slurry Pump":
                        item.MaterialCostDefault = CalcSlurryPumpCost;
                        break;
                    case "SiloSpaceHeater":
                        item.MaterialCostDefault = CalcSiloSpaceHeaterCost;
                        break;
                    case "SlurryTank":
                        item.MaterialCostDefault = CalcSlurryTankCost;
                        break;
                    case "HardwareSoftware":
                        item.MaterialCostDefault = CalcHardwareSoftwareCost;
                        break;
                    case "Slaker":
                        item.MaterialCostDefault = CalcSlakerCost;
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
                    case "Silo":
                        item.TotalCost = CalcRecapitalizationCostSilo;
                        break;
                    case "BinActivator":
                        item.TotalCost = CalcRecapitalizationCostBinActivator;
                        break;
                    case "ScrewFeeder":
                        item.TotalCost = CalcRecapitalizationCostScrewFeeder;
                        break;
                    case "Slurry Mixer":
                        item.TotalCost = CalcRecapitalizationCostSlurryMixer;
                        break;
                    case "Slurry Pump":
                        item.TotalCost = CalcRecapitalizationCostSlurryPump;
                        break;
                    case "SiloSpaceHeater":
                        item.TotalCost = CalcRecapitalizationCostSiloSpaceHeater;
                        break;
                    case "SlurryTank":
                        item.TotalCost = CalcRecapitalizationCostSlurryTank;
                        break;
                    case "HardwareSoftware":
                        item.TotalCost = CalcRecapitalizationCostHardwareSoftware;
                        break;
                    case "Slaker":
                        item.TotalCost = CalcRecapitalizationCostSlaker;
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
                Name = "Silo",
                NameFixed = "Silo",
                LifeCycle = RecapitalizationCostLifeCycleSilo,
                PercentReplacement = RecapitalizationCostPercentReplacementSilo,
                MaterialCostDefault = CalcSiloCost,
                UseCustomCost = false,
                MaterialCostCustom = 0m,
                TotalCost = CalcRecapitalizationCostSilo
            });
            RecapMaterials.Add(new RecapMaterial
            {
                IsSelected = true,
                Name = "Bin Activator",
                NameFixed = "BinActivator",
                LifeCycle = RecapitalizationCostLifeCycleBinActivator,
                PercentReplacement = RecapitalizationCostPercentReplacementBinActivator,
                MaterialCostDefault = CalcBinActivatorCost,
                UseCustomCost = false,
                MaterialCostCustom = 0m,
                TotalCost = CalcRecapitalizationCostBinActivator
            });
            RecapMaterials.Add(new RecapMaterial
            {
                IsSelected = true,
                Name = "Screw Feeder",
                NameFixed = "ScrewFeeder",
                LifeCycle = RecapitalizationCostLifeCycleScrewFeeder,
                PercentReplacement = RecapitalizationCostPercentReplacementScrewFeeder,
                MaterialCostDefault = CalcScrewFeederCost,
                UseCustomCost = false,
                MaterialCostCustom = 0m,
                TotalCost = CalcRecapitalizationCostScrewFeeder
            });
            RecapMaterials.Add(new RecapMaterial
            {
                IsSelected = true,
                Name = "SlurryMixer",
                NameFixed = "SlurryMixer",
                LifeCycle = RecapitalizationCostLifeCycleSlurryMixer,
                PercentReplacement = RecapitalizationCostPercentReplacementSlurryMixer,
                MaterialCostDefault = CalcSlurryMixerCost,
                UseCustomCost = false,
                MaterialCostCustom = 0m,
                TotalCost = CalcRecapitalizationCostSlurryMixer
            });
            RecapMaterials.Add(new RecapMaterial
            {
                IsSelected = true,
                Name = "SlurryPump",
                NameFixed = "SlurryPump",
                LifeCycle = RecapitalizationCostLifeCycleSlurryPump,
                PercentReplacement = RecapitalizationCostPercentReplacementSlurryPump,
                MaterialCostDefault = CalcSlurryPumpCost,
                UseCustomCost = false,
                MaterialCostCustom = 0m,
                TotalCost = CalcRecapitalizationCostSlurryPump
            });
            RecapMaterials.Add(new RecapMaterial
            {
                IsSelected = true,
                Name = "SiloSpaceHeater",
                NameFixed = "SiloSpaceHeater",
                LifeCycle = RecapitalizationCostLifeCycleSiloSpaceHeater,
                PercentReplacement = RecapitalizationCostPercentReplacementSiloSpaceHeater,
                MaterialCostDefault = CalcSiloSpaceHeaterCost,
                UseCustomCost = false,
                MaterialCostCustom = 0m,
                TotalCost = CalcRecapitalizationCostSiloSpaceHeater
            });
            RecapMaterials.Add(new RecapMaterial
            {
                IsSelected = true,
                Name = "SlurryTank",
                NameFixed = "SlurryTank",
                LifeCycle = RecapitalizationCostLifeCycleSlurryTank,
                PercentReplacement = RecapitalizationCostPercentReplacementSlurryTank,
                MaterialCostDefault = CalcSlurryTankCost,
                UseCustomCost = false,
                MaterialCostCustom = 0m,
                TotalCost = CalcRecapitalizationCostSlurryTank
            });
            RecapMaterials.Add(new RecapMaterial
            {
                IsSelected = true,
                Name = "HardwareSoftware",
                NameFixed = "HardwareSoftware",
                LifeCycle = RecapitalizationCostLifeCycleHardwareSoftware,
                PercentReplacement = RecapitalizationCostPercentReplacementHardwareSoftware,
                MaterialCostDefault = CalcHardwareSoftwareCost,
                UseCustomCost = false,
                MaterialCostCustom = 0m,
                TotalCost = CalcRecapitalizationCostHardwareSoftware
            });
            RecapMaterials.Add(new RecapMaterial
            {
                IsSelected = true,
                Name = "Slaker",
                NameFixed = "Slaker",
                LifeCycle = RecapitalizationCostLifeCycleSlaker,
                PercentReplacement = RecapitalizationCostPercentReplacementSlaker,
                MaterialCostDefault = CalcSlakerCost,
                UseCustomCost = false,
                MaterialCostCustom = 0m,
                TotalCost = CalcRecapitalizationCostSlaker
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
            ((RecapMaterial)sender).TotalCost = DryLimeCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
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
        public ICommand SetSiloSystemCostCommand { get; }
        public ICommand SetSiloSystemCommand { get; }
        public ICommand SetFoundationSiteSoilCommand { get; }
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
            var customDialog = new CustomDialog() { Title = "About Dry Lime" };

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
                        string message = Resources.infoWaterQualityDL;
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
                        string message = Resources.infoChemicalSolutionDL;
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
                        string message = Resources.infoChemicalConsumptionDL;
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
                        string message = Resources.infoEquipmentDL;
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
                        string message = Resources.infoOtherItemsCapitalDL;
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
                        string message = Resources.infoOtherItemsAnnualDL;
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
                        string message = Resources.infoSizingSummaryDL;
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
                        string message = Resources.infoCapitalCostDL;
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
                        string message = Resources.infoAnnualCostDL;
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
                        string message = Resources.infoRecapitalizationCostDL;
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

        public void SetSiloSystemCostEstimated(object siloSystemName)
        {
            string itemSiloSystemName = (string)siloSystemName;

            switch (itemSiloSystemName)
            {
                case "30 Ton":
                    SiloSystemNameEstimated = SiloSystem30TonName;
                    SiloSystemWeightEstimated = SiloSystem30TonWeight;
                    SiloSystemQuantityEstimated = SiloSystem30TonQuantity;
                    SiloSystemFoundationAreaEstimated = SiloSystem30TonFoundationArea;
                    SiloSystemDiameterEstimated = SiloSystem30TonDiameter;
                    SiloSystemConcreteThicknessEstimated = SiloSystem30TonConcreteThickness;
                    SiloSystemConcreteVolumeEstimated = SiloSystem30TonConcreteVolume;
                    switch (LimeOptionsProperty)
                    {
                        case RadioButtonsLimeOptionsEnum.OptionHydratedLime:
                            SiloSystemUnitCostEstimated = SiloSystem30TonHydratedLimeUnitCost;
                            break;
                        case RadioButtonsLimeOptionsEnum.OptionDryLime:
                            SiloSystemUnitCostEstimated = SiloSystem30TonDryLimeUnitCost;
                            break;
                        default:
                            break;
                    }
                    break;
                case "60 Ton":
                    SiloSystemNameEstimated = SiloSystem60TonName;
                    SiloSystemWeightEstimated = SiloSystem60TonWeight;
                    SiloSystemQuantityEstimated = SiloSystem60TonQuantity;
                    SiloSystemFoundationAreaEstimated = SiloSystem60TonFoundationArea;
                    SiloSystemDiameterEstimated = SiloSystem60TonDiameter;
                    SiloSystemConcreteThicknessEstimated = SiloSystem60TonConcreteThickness;
                    SiloSystemConcreteVolumeEstimated = SiloSystem60TonConcreteVolume;
                    switch (LimeOptionsProperty)
                    {
                        case RadioButtonsLimeOptionsEnum.OptionHydratedLime:
                            SiloSystemUnitCostEstimated = SiloSystem60TonHydratedLimeUnitCost;
                            break;
                        case RadioButtonsLimeOptionsEnum.OptionDryLime:
                            SiloSystemUnitCostEstimated = SiloSystem60TonDryLimeUnitCost;
                            break;
                        default:
                            break;
                    }
                    break;
                case "90 Ton":
                    SiloSystemNameEstimated = SiloSystem90TonName;
                    SiloSystemWeightEstimated = SiloSystem90TonWeight;
                    SiloSystemQuantityEstimated = SiloSystem90TonQuantity;
                    SiloSystemFoundationAreaEstimated = SiloSystem90TonFoundationArea;
                    SiloSystemDiameterEstimated = SiloSystem90TonDiameter;
                    SiloSystemConcreteThicknessEstimated = SiloSystem90TonConcreteThickness;
                    SiloSystemConcreteVolumeEstimated = SiloSystem90TonConcreteVolume;
                    switch (LimeOptionsProperty)
                    {
                        case RadioButtonsLimeOptionsEnum.OptionHydratedLime:
                            SiloSystemUnitCostEstimated = SiloSystem90TonHydratedLimeUnitCost;
                            break;
                        case RadioButtonsLimeOptionsEnum.OptionDryLime:
                            SiloSystemUnitCostEstimated = SiloSystem90TonDryLimeUnitCost;
                            break;
                        default:
                            break;
                    }
                    break;
                case "120 Ton":
                    SiloSystemNameEstimated = SiloSystem120TonName;
                    SiloSystemWeightEstimated = SiloSystem120TonWeight;
                    SiloSystemQuantityEstimated = SiloSystem120TonQuantity;
                    SiloSystemFoundationAreaEstimated = SiloSystem120TonFoundationArea;
                    SiloSystemDiameterEstimated = SiloSystem120TonDiameter;
                    SiloSystemConcreteThicknessEstimated = SiloSystem120TonConcreteThickness;
                    SiloSystemConcreteVolumeEstimated = SiloSystem120TonConcreteVolume;
                    switch (LimeOptionsProperty)
                    {
                        case RadioButtonsLimeOptionsEnum.OptionHydratedLime:
                            SiloSystemUnitCostEstimated = SiloSystem120TonHydratedLimeUnitCost;
                            break;
                        case RadioButtonsLimeOptionsEnum.OptionDryLime:
                            SiloSystemUnitCostEstimated = SiloSystem120TonDryLimeUnitCost;
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
        }

        public void SetSiloSystemEstimated(object siloSystem)
        {
            SiloSystem itemSiloSystem = (SiloSystem)siloSystem;

            switch (itemSiloSystem.Name)
            {
                case "30 Ton":
                    SiloSystemNameEstimated = SiloSystem30TonName;
                    SiloSystemWeightEstimated = SiloSystem30TonWeight;
                    SiloSystemQuantityEstimated = SiloSystem30TonQuantity;
                    SiloSystemFoundationAreaEstimated = SiloSystem30TonFoundationArea;
                    SiloSystemDiameterEstimated = SiloSystem30TonDiameter;
                    SiloSystemConcreteThicknessEstimated = SiloSystem30TonConcreteThickness;
                    SiloSystemConcreteVolumeEstimated = SiloSystem30TonConcreteVolume;
                    switch (LimeOptionsProperty)
                    {
                        case RadioButtonsLimeOptionsEnum.OptionHydratedLime:
                            SiloSystemUnitCostEstimated = SiloSystem30TonHydratedLimeUnitCost;
                            break;
                        case RadioButtonsLimeOptionsEnum.OptionDryLime:
                            SiloSystemUnitCostEstimated = SiloSystem30TonDryLimeUnitCost;
                            break;
                        default:
                            break;
                    }                  
                    break;
                case "60 Ton":
                    SiloSystemNameEstimated = SiloSystem60TonName;
                    SiloSystemWeightEstimated = SiloSystem60TonWeight;
                    SiloSystemQuantityEstimated = SiloSystem60TonQuantity;
                    SiloSystemFoundationAreaEstimated = SiloSystem60TonFoundationArea;
                    SiloSystemDiameterEstimated = SiloSystem60TonDiameter;
                    SiloSystemConcreteThicknessEstimated = SiloSystem60TonConcreteThickness;
                    SiloSystemConcreteVolumeEstimated = SiloSystem60TonConcreteVolume;
                    switch (LimeOptionsProperty)
                    {
                        case RadioButtonsLimeOptionsEnum.OptionHydratedLime:
                            SiloSystemUnitCostEstimated = SiloSystem60TonHydratedLimeUnitCost;
                            break;
                        case RadioButtonsLimeOptionsEnum.OptionDryLime:
                            SiloSystemUnitCostEstimated = SiloSystem60TonDryLimeUnitCost;
                            break;
                        default:
                            break;
                    }
                    break;
                case "90 Ton":
                    SiloSystemNameEstimated = SiloSystem90TonName;
                    SiloSystemWeightEstimated = SiloSystem90TonWeight;
                    SiloSystemQuantityEstimated = SiloSystem90TonQuantity;
                    SiloSystemFoundationAreaEstimated = SiloSystem90TonFoundationArea;
                    SiloSystemDiameterEstimated = SiloSystem90TonDiameter;
                    SiloSystemConcreteThicknessEstimated = SiloSystem90TonConcreteThickness;
                    SiloSystemConcreteVolumeEstimated = SiloSystem90TonConcreteVolume;
                    switch (LimeOptionsProperty)
                    {
                        case RadioButtonsLimeOptionsEnum.OptionHydratedLime:
                            SiloSystemUnitCostEstimated = SiloSystem90TonHydratedLimeUnitCost;
                            break;
                        case RadioButtonsLimeOptionsEnum.OptionDryLime:
                            SiloSystemUnitCostEstimated = SiloSystem90TonDryLimeUnitCost;
                            break;
                        default:
                            break;
                    }
                    break;
                case "120 Ton":
                    SiloSystemNameEstimated = SiloSystem120TonName;
                    SiloSystemWeightEstimated = SiloSystem120TonWeight;
                    SiloSystemQuantityEstimated = SiloSystem120TonQuantity;
                    SiloSystemFoundationAreaEstimated = SiloSystem120TonFoundationArea;
                    SiloSystemDiameterEstimated = SiloSystem120TonDiameter;
                    SiloSystemConcreteThicknessEstimated = SiloSystem120TonConcreteThickness;
                    SiloSystemConcreteVolumeEstimated = SiloSystem120TonConcreteVolume;
                    switch (LimeOptionsProperty)
                    {
                        case RadioButtonsLimeOptionsEnum.OptionHydratedLime:
                            SiloSystemUnitCostEstimated = SiloSystem120TonHydratedLimeUnitCost;
                            break;
                        case RadioButtonsLimeOptionsEnum.OptionDryLime:
                            SiloSystemUnitCostEstimated = SiloSystem120TonDryLimeUnitCost;
                            break;
                        default:
                            break;
                    }
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

        public DryLimeViewModel(IDialogCoordinator dialogCoordinator)
        {
            // MahApps dialog coordinator
            _dialogCoordinator = dialogCoordinator;

            // Assign the proper functions to the open and save commands
            OpenCommand = new RelayCommand(OpenFile);
            SaveCommand = new RelayCommand(SaveFile);
            HelpCommand = new RelayCommand(ShowHelp);
            SetSiloSystemCostCommand = new RelayCommandWithParameter(SetSiloSystemCostEstimated);
            SetSiloSystemCommand = new RelayCommandWithParameter(SetSiloSystemEstimated);
            SetFoundationSiteSoilCommand = new RelayCommandWithParameter(SetFoundationSiteSoil);
            SyncCommand = new RelayCommand(SyncWithMainUi);

            // Get a list of property names and filter the names to include only those that start with "Calc" in order
            // to use with the NotifyAndChange.  This eliminates the need to write every property name that needs 
            // to be notified of changes by the user.
            PropertiesStringList = Helpers.GetNamesOfClassProperties(this);
            CalcPropertiesStringArray = Helpers.FilterPropertiesList(PropertiesStringList, "Calc");

            // Initialize the model name and user name
            ModuleType = "Dry Lime";
            ModuleName = "Untitled module";
            ModuleTreatmentType = "Active";
            ModuleId = random.Next(1000);

            IsError = false;
            IsMajorError = false;
            ErrorMessage = "";
            ErrorMessageShort = "";

            // Initialize check boxes
            IsSiloSystem = true;
            IsDustCollectorBlower = true;
            IsBinActivator = true;
            IsScrewFeeder = true;
            IsSlurryMixerAndPump = true;
            IsSiloExhaustFan = true;
            IsSiloSpaceHeater = true;

            // Initialize radio buttons
            LimeOptionsProperty = RadioButtonsLimeOptionsEnum.OptionHydratedLime;
            ChemicalConsumptionOptionsProperty = RadioButtonsChemicalConsumptionOptionsEnum.OptionStoichiometric;
            SiloSystemOptionsProperty = RadioButtonsSiloSystemOptionsEnum.OptionEstimated;
            FoundationOptionsProperty = RadioButtonsFoundationOptionsEnum.OptionEstimated;
            SystemInstallCostOptionsProperty = RadioButtonsSystemInstallOptionsEnum.OptionCostMultiplier;
            AnnualCostChemicalOptionsProperty = RadioButtonsAnnualCostChemicalOptionsEnum.OptionEstimate;
            AnnualCostOperationAndMaintanenceOptionsProperty = RadioButtonsAnnualCostOperationAndMaintanenceOptionsEnum.OptionCostMultiplier;
            AnnualCostElectricOptionsProperty = RadioButtonsAnnualCostElectricOptionsEnum.OptionAnnualCostElectricEstimate;

            // Dummy variables to force syncing
            Dummy = 0;

            // Read the xml data file and assign property values accordingly.
            OpenXmlAndAssignValues(@"..\..\Data\default-data-dry-lime.xml");

            // Silo System
            SiloSystems = new List<SiloSystem>
            {
                new SiloSystem {Name = SiloSystem30TonName, Quantity = SiloSystem30TonQuantity, Weight = SiloSystem30TonWeight, UnitCost = SiloSystem30TonHydratedLimeUnitCost, FoundationArea = SiloSystem30TonFoundationArea, Diameter = SiloSystem30TonDiameter, ConcreteThickness = SiloSystem30TonConcreteThickness, ConcreteVolume = SiloSystem30TonConcreteVolume},
                new SiloSystem {Name = SiloSystem60TonName, Quantity = SiloSystem60TonQuantity, Weight = SiloSystem60TonWeight, UnitCost = SiloSystem60TonHydratedLimeUnitCost, FoundationArea = SiloSystem60TonFoundationArea, Diameter = SiloSystem60TonDiameter, ConcreteThickness = SiloSystem60TonConcreteThickness, ConcreteVolume = SiloSystem60TonConcreteVolume},
                new SiloSystem {Name = SiloSystem90TonName, Quantity = SiloSystem90TonQuantity, Weight = SiloSystem90TonWeight, UnitCost = SiloSystem90TonHydratedLimeUnitCost, FoundationArea = SiloSystem90TonFoundationArea, Diameter = SiloSystem90TonDiameter, ConcreteThickness = SiloSystem90TonConcreteThickness, ConcreteVolume = SiloSystem90TonConcreteVolume},
                new SiloSystem {Name = SiloSystem120TonName, Quantity = SiloSystem120TonQuantity, Weight = SiloSystem120TonWeight, UnitCost = SiloSystem120TonHydratedLimeUnitCost, FoundationArea = SiloSystem120TonFoundationArea, Diameter = SiloSystem120TonDiameter, ConcreteThickness = SiloSystem120TonConcreteThickness, ConcreteVolume = SiloSystem120TonConcreteVolume},
            };

            SiloSystemName = SiloSystem30TonName;
            SiloSystemWeight = SiloSystem30TonWeight;
            SiloSystemQuantity = SiloSystem30TonQuantity;
            SiloSystemUnitCost = SiloSystem30TonHydratedLimeUnitCost;
            SiloSystemFoundationArea = SiloSystem30TonFoundationArea;
            SiloSystemDiameter = SiloSystem30TonDiameter;
            SiloSystemConcreteThickness = SiloSystem30TonConcreteThickness;
            SiloSystemConcreteVolume = SiloSystem30TonConcreteVolume;

            SiloSystemNameEstimated = SiloSystem30TonName;
            SiloSystemWeightEstimated = SiloSystem30TonWeight;
            SiloSystemQuantityEstimated = SiloSystem30TonQuantity;
            SiloSystemUnitCostEstimated = SiloSystem30TonHydratedLimeUnitCost;
            SiloSystemFoundationAreaEstimated = SiloSystem30TonFoundationArea;
            SiloSystemDiameterEstimated = SiloSystem30TonDiameter;
            SiloSystemConcreteThicknessEstimated = SiloSystem30TonConcreteThickness;
            SiloSystemConcreteVolumeEstimated = SiloSystem30TonConcreteVolume;


            // Foundation Site Soils
            FoundationSiteSoils = new List<FoundationSiteSoil>
            {
                new FoundationSiteSoil {Name = FoundationSiteSoilLoadBearing1500Name, Rating = FoundationSiteSoilLoadBearing1500Rating, Multiplier = FoundationSiteSoilLoadBearing1500Multiplier, Quantity = FoundationSiteSoilLoadBearing1500Quantity },
                new FoundationSiteSoil {Name = FoundationSiteSoilLoadBearing3000Name, Rating = FoundationSiteSoilLoadBearing3000Rating, Multiplier = FoundationSiteSoilLoadBearing3000Multiplier, Quantity = FoundationSiteSoilLoadBearing3000Quantity },
                new FoundationSiteSoil {Name = FoundationSiteSoilLoadBearing4500Name, Rating = FoundationSiteSoilLoadBearing4500Rating, Multiplier = FoundationSiteSoilLoadBearing4500Multiplier, Quantity = FoundationSiteSoilLoadBearing4500Quantity },
            };

            FoundationSiteSoilLoadBearingName = FoundationSiteSoilLoadBearing1500Name;
            FoundationSiteSoilLoadBearingQuantity = FoundationSiteSoilLoadBearing1500Quantity;
            FoundationSiteSoilLoadBearingMultiplier = FoundationSiteSoilLoadBearing1500Multiplier;
            FoundationSiteSoilLoadBearingRating = FoundationSiteSoilLoadBearing1500Rating;

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
