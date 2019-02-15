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

    public class ReactionTankViewModel : PropertyChangedBase, IObserver<SharedData>
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

        #endregion

        #region Properties - Reaction Tank Design

        /// <summary>
        ///  Radio button binding
        /// </summary>
        public enum RadioButtonsReactionTankDesignOptionsEnum
        {
            OptionFiberglass,
            OptionConcreteSteel,
        }

        private RadioButtonsReactionTankDesignOptionsEnum _reactionTankDesignOptionsProperty;
        /// <summary>
        ///  User specified 
        /// </summary>
        public RadioButtonsReactionTankDesignOptionsEnum ReactionTankDesignOptionsProperty
        {
            get { return _reactionTankDesignOptionsProperty; }
            set { ChangeAndNotify(ref _reactionTankDesignOptionsProperty, value, nameof(ReactionTankDesignOptionsProperty), CalcPropertiesStringArray); }
        }

        private List<GeneralItem> _fiberglassVolumes;
        /// <summary>
        /// Collection 
        /// </summary>
        public List<GeneralItem> FiberglassVolumes
        {
            get { return _fiberglassVolumes; }
            set { ChangeAndNotify(ref _fiberglassVolumes, value, nameof(FiberglassVolumes), CalcPropertiesStringArray); }
        }

        private string _fiberglassVolumeName;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string FiberglassVolumeName
        {
            get { return _fiberglassVolumeName; }
            set { ChangeAndNotify(ref _fiberglassVolumeName, value, nameof(FiberglassVolumeName), CalcPropertiesStringArray); }
        }

        private string _fiberglassVolumeName10000;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string FiberglassVolumeName10000
        {
            get { return _fiberglassVolumeName10000; }
            set { ChangeAndNotify(ref _fiberglassVolumeName10000, value, nameof(FiberglassVolumeName10000), CalcPropertiesStringArray); }
        }

        private string _fiberglassVolumeName20000;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string FiberglassVolumeName20000
        {
            get { return _fiberglassVolumeName20000; }
            set { ChangeAndNotify(ref _fiberglassVolumeName20000, value, nameof(FiberglassVolumeName20000), CalcPropertiesStringArray); }
        }

        private string _fiberglassVolumeName30000;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string FiberglassVolumeName30000
        {
            get { return _fiberglassVolumeName30000; }
            set { ChangeAndNotify(ref _fiberglassVolumeName30000, value, nameof(FiberglassVolumeName30000), CalcPropertiesStringArray); }
        }

        private double _fiberglassVolume;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double FiberglassVolume
        {
            get { return _fiberglassVolume; }
            set { ChangeAndNotify(ref _fiberglassVolume, value, nameof(FiberglassVolume), CalcPropertiesStringArray); }
        }

        private double _fiberglassVolume10000;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double FiberglassVolume10000
        {
            get { return _fiberglassVolume10000; }
            set { ChangeAndNotify(ref _fiberglassVolume10000, value, nameof(FiberglassVolume10000), CalcPropertiesStringArray); }
        }

        private double _fiberglassVolume20000;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double FiberglassVolume20000
        {
            get { return _fiberglassVolume20000; }
            set { ChangeAndNotify(ref _fiberglassVolume20000, value, nameof(FiberglassVolume20000), CalcPropertiesStringArray); }
        }

        private double _fiberglassVolume30000;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double FiberglassVolume30000
        {
            get { return _fiberglassVolume30000; }
            set { ChangeAndNotify(ref _fiberglassVolume30000, value, nameof(FiberglassVolume30000), CalcPropertiesStringArray); }
        }

        private decimal _fiberglassCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal FiberglassCost
        {
            get { return _fiberglassCost; }
            set { ChangeAndNotify(ref _fiberglassCost, value, nameof(FiberglassCost), CalcPropertiesStringArray); }
        }

        private decimal _fiberglassCost10000;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal FiberglassCost10000
        {
            get { return _fiberglassCost10000; }
            set { ChangeAndNotify(ref _fiberglassCost10000, value, nameof(FiberglassCost10000), CalcPropertiesStringArray); }
        }

        private decimal _fiberglassCost20000;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal FiberglassCost20000
        {
            get { return _fiberglassCost20000; }
            set { ChangeAndNotify(ref _fiberglassCost20000, value, nameof(FiberglassCost20000), CalcPropertiesStringArray); }
        }

        private decimal _fiberglassCost30000;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal FiberglassCost30000
        {
            get { return _fiberglassCost30000; }
            set { ChangeAndNotify(ref _fiberglassCost30000, value, nameof(FiberglassCost30000), CalcPropertiesStringArray); }
        }

        private double _fiberglassDiameter;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double FiberglassDiameter
        {
            get { return _fiberglassDiameter; }
            set { ChangeAndNotify(ref _fiberglassDiameter, value, nameof(FiberglassDiameter), CalcPropertiesStringArray); }
        }

        private double _fiberglassDiameter10000;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double FiberglassDiameter10000
        {
            get { return _fiberglassDiameter10000; }
            set { ChangeAndNotify(ref _fiberglassDiameter10000, value, nameof(FiberglassDiameter10000), CalcPropertiesStringArray); }
        }

        private double _fiberglassDiameter20000;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double FiberglassDiameter20000
        {
            get { return _fiberglassDiameter20000; }
            set { ChangeAndNotify(ref _fiberglassDiameter20000, value, nameof(FiberglassDiameter20000), CalcPropertiesStringArray); }
        }

        private double _fiberglassDiameter30000;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double FiberglassDiameter30000
        {
            get { return _fiberglassDiameter30000; }
            set { ChangeAndNotify(ref _fiberglassDiameter30000, value, nameof(FiberglassDiameter30000), CalcPropertiesStringArray); }
        }

        private double _fiberglassHeight;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double FiberglassHeight
        {
            get { return _fiberglassHeight; }
            set { ChangeAndNotify(ref _fiberglassHeight, value, nameof(FiberglassHeight), CalcPropertiesStringArray); }
        }

        private double _fiberglassHeight10000;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double FiberglassHeight10000
        {
            get { return _fiberglassHeight10000; }
            set { ChangeAndNotify(ref _fiberglassHeight10000, value, nameof(FiberglassHeight10000), CalcPropertiesStringArray); }
        }

        private double _fiberglassHeight20000;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double FiberglassHeight20000
        {
            get { return _fiberglassHeight20000; }
            set { ChangeAndNotify(ref _fiberglassHeight20000, value, nameof(FiberglassHeight20000), CalcPropertiesStringArray); }
        }

        private double _fiberglassHeight30000;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double FiberglassHeight30000
        {
            get { return _fiberglassHeight30000; }
            set { ChangeAndNotify(ref _fiberglassHeight30000, value, nameof(FiberglassHeight30000), CalcPropertiesStringArray); }
        }


        private List<GeneralItem> _concreteSteelMaterials;
        /// <summary>
        /// Collection 
        /// </summary>
        public List<GeneralItem> ConcreteSteelMaterials
        {
            get { return _concreteSteelMaterials; }
            set { ChangeAndNotify(ref _concreteSteelMaterials, value, nameof(ConcreteSteelMaterials), CalcPropertiesStringArray); }
        }

        private List<GeneralItem> _concreteSteelVolumes;
        /// <summary>
        /// Collection 
        /// </summary>
        public List<GeneralItem> ConcreteSteelVolumes
        {
            get { return _concreteSteelVolumes; }
            set { ChangeAndNotify(ref _concreteSteelVolumes, value, nameof(ConcreteSteelVolumes), CalcPropertiesStringArray); }
        }

        private string _concreteSteelName;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string ConcreteSteelName
        {
            get { return _concreteSteelName; }
            set { ChangeAndNotify(ref _concreteSteelName, value, nameof(ConcreteSteelName), CalcPropertiesStringArray); }
        }

        private string _concreteSteelNameConcrete;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string ConcreteSteelNameConcrete
        {
            get { return _concreteSteelNameConcrete; }
            set { ChangeAndNotify(ref _concreteSteelNameConcrete, value, nameof(ConcreteSteelNameConcrete), CalcPropertiesStringArray); }
        }

        private string _concreteSteelNameBoltedSteel;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string ConcreteSteelNameBoltedSteel
        {
            get { return _concreteSteelNameBoltedSteel; }
            set { ChangeAndNotify(ref _concreteSteelNameBoltedSteel, value, nameof(ConcreteSteelNameBoltedSteel), CalcPropertiesStringArray); }
        }

        private string _concreteSteelNameWeldedSteel;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string ConcreteSteelNameWeldedSteel
        {
            get { return _concreteSteelNameWeldedSteel; }
            set { ChangeAndNotify(ref _concreteSteelNameWeldedSteel, value, nameof(ConcreteSteelNameWeldedSteel), CalcPropertiesStringArray); }
        }

        private string _concreteSteelVolumeName;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string ConcreteSteelVolumeName
        {
            get { return _concreteSteelVolumeName; }
            set { ChangeAndNotify(ref _concreteSteelVolumeName, value, nameof(ConcreteSteelVolumeName), CalcPropertiesStringArray); }
        }

        private string _concreteSteelVolumeName30000;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string ConcreteSteelVolumeName30000
        {
            get { return _concreteSteelVolumeName30000; }
            set { ChangeAndNotify(ref _concreteSteelVolumeName30000, value, nameof(ConcreteSteelVolumeName30000), CalcPropertiesStringArray); }
        }

        private string _concreteSteelVolumeName60000;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string ConcreteSteelVolumeName60000
        {
            get { return _concreteSteelVolumeName60000; }
            set { ChangeAndNotify(ref _concreteSteelVolumeName60000, value, nameof(ConcreteSteelVolumeName60000), CalcPropertiesStringArray); }
        }

        private string _concreteSteelVolumeName90000;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string ConcreteSteelVolumeName90000
        {
            get { return _concreteSteelVolumeName90000; }
            set { ChangeAndNotify(ref _concreteSteelVolumeName90000, value, nameof(ConcreteSteelVolumeName90000), CalcPropertiesStringArray); }
        }

        private double _concreteSteelVolume;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double ConcreteSteelVolume
        {
            get { return _concreteSteelVolume; }
            set { ChangeAndNotify(ref _concreteSteelVolume, value, nameof(ConcreteSteelVolume), CalcPropertiesStringArray); }
        }

        private double _concreteSteelVolume30000;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double ConcreteSteelVolume30000
        {
            get { return _concreteSteelVolume30000; }
            set { ChangeAndNotify(ref _concreteSteelVolume30000, value, nameof(ConcreteSteelVolume30000), CalcPropertiesStringArray); }
        }

        private double _concreteSteelVolume60000;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double ConcreteSteelVolume60000
        {
            get { return _concreteSteelVolume60000; }
            set { ChangeAndNotify(ref _concreteSteelVolume60000, value, nameof(ConcreteSteelVolume60000), CalcPropertiesStringArray); }
        }

        private double _concreteSteelVolume90000;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double ConcreteSteelVolume90000
        {
            get { return _concreteSteelVolume90000; }
            set { ChangeAndNotify(ref _concreteSteelVolume90000, value, nameof(ConcreteSteelVolume90000), CalcPropertiesStringArray); }
        }

        private decimal _concreteSteelCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal ConcreteSteelCost
        {
            get { return _concreteSteelCost; }
            set { ChangeAndNotify(ref _concreteSteelCost, value, nameof(ConcreteSteelCost), CalcPropertiesStringArray); }
        }

        private decimal _concreteCost30000;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal ConcreteCost30000
        {
            get { return _concreteCost30000; }
            set { ChangeAndNotify(ref _concreteCost30000, value, nameof(ConcreteCost30000), CalcPropertiesStringArray); }
        }

        private decimal _concreteCost60000;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal ConcreteCost60000
        {
            get { return _concreteCost60000; }
            set { ChangeAndNotify(ref _concreteCost60000, value, nameof(ConcreteCost60000), CalcPropertiesStringArray); }
        }

        private decimal _concreteCost90000;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal ConcreteCost90000
        {
            get { return _concreteCost90000; }
            set { ChangeAndNotify(ref _concreteCost90000, value, nameof(ConcreteCost90000), CalcPropertiesStringArray); }
        }

        private decimal _boltedSteelCost30000;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal BoltedSteelCost30000
        {
            get { return _boltedSteelCost30000; }
            set { ChangeAndNotify(ref _boltedSteelCost30000, value, nameof(BoltedSteelCost30000), CalcPropertiesStringArray); }
        }

        private decimal _boltedSteelCost60000;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal BoltedSteelCost60000
        {
            get { return _boltedSteelCost60000; }
            set { ChangeAndNotify(ref _boltedSteelCost60000, value, nameof(BoltedSteelCost60000), CalcPropertiesStringArray); }
        }

        private decimal _boltedSteelCost90000;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal BoltedSteelCost90000
        {
            get { return _boltedSteelCost90000; }
            set { ChangeAndNotify(ref _boltedSteelCost90000, value, nameof(BoltedSteelCost90000), CalcPropertiesStringArray); }
        }

        private decimal _weldedSteelCost30000;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal WeldedSteelCost30000
        {
            get { return _weldedSteelCost30000; }
            set { ChangeAndNotify(ref _weldedSteelCost30000, value, nameof(WeldedSteelCost30000), CalcPropertiesStringArray); }
        }

        private decimal _weldedSteelCost60000;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal WeldedSteelCost60000
        {
            get { return _weldedSteelCost60000; }
            set { ChangeAndNotify(ref _weldedSteelCost60000, value, nameof(WeldedSteelCost60000), CalcPropertiesStringArray); }
        }

        private decimal _weldedSteelCost90000;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal WeldedSteelCost90000
        {
            get { return _weldedSteelCost90000; }
            set { ChangeAndNotify(ref _weldedSteelCost90000, value, nameof(WeldedSteelCost90000), CalcPropertiesStringArray); }
        }

        private double _concreteSteelDiameter;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double ConcreteSteelDiameter
        {
            get { return _concreteSteelDiameter; }
            set { ChangeAndNotify(ref _concreteSteelDiameter, value, nameof(ConcreteSteelDiameter), CalcPropertiesStringArray); }
        }

        private double _concreteSteelDiameter30000;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double ConcreteSteelDiameter30000
        {
            get { return _concreteSteelDiameter30000; }
            set { ChangeAndNotify(ref _concreteSteelDiameter30000, value, nameof(ConcreteSteelDiameter30000), CalcPropertiesStringArray); }
        }

        private double _concreteSteelDiameter60000;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double ConcreteSteelDiameter60000
        {
            get { return _concreteSteelDiameter60000; }
            set { ChangeAndNotify(ref _concreteSteelDiameter60000, value, nameof(ConcreteSteelDiameter60000), CalcPropertiesStringArray); }
        }

        private double _concreteSteelDiameter90000;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double ConcreteSteelDiameter90000
        {
            get { return _concreteSteelDiameter90000; }
            set { ChangeAndNotify(ref _concreteSteelDiameter90000, value, nameof(ConcreteSteelDiameter90000), CalcPropertiesStringArray); }
        }

        private double _concreteSteelHeight;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double ConcreteSteelHeight
        {
            get { return _concreteSteelHeight; }
            set { ChangeAndNotify(ref _concreteSteelHeight, value, nameof(ConcreteSteelHeight), CalcPropertiesStringArray); }
        }

        private double _concreteSteelHeight30000;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double ConcreteSteelHeight30000
        {
            get { return _concreteSteelHeight30000; }
            set { ChangeAndNotify(ref _concreteSteelHeight30000, value, nameof(ConcreteSteelHeight30000), CalcPropertiesStringArray); }
        }

        private double _concreteSteelHeight60000;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double ConcreteSteelHeight60000
        {
            get { return _concreteSteelHeight60000; }
            set { ChangeAndNotify(ref _concreteSteelHeight60000, value, nameof(ConcreteSteelHeight60000), CalcPropertiesStringArray); }
        }

        private double _concreteSteelHeight90000;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double ConcreteSteelHeight90000
        {
            get { return _concreteSteelHeight90000; }
            set { ChangeAndNotify(ref _concreteSteelHeight90000, value, nameof(ConcreteSteelHeight90000), CalcPropertiesStringArray); }
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
                if (ConcreteSteelName == ConcreteSteelNameConcrete)
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

        private double _tankQuantity;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double TankQuantity
        {
            get { return _tankQuantity; }
            set { ChangeAndNotify(ref _tankQuantity, value, nameof(TankQuantity), CalcPropertiesStringArray); }
        }

        private double _tankFreeboardHeight;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double TankFreeboardHeight
        {
            get { return _tankFreeboardHeight; }
            set { ChangeAndNotify(ref _tankFreeboardHeight, value, nameof(TankFreeboardHeight), CalcPropertiesStringArray); }
        }

        #endregion

        #region Equipment: Mixer

        private bool _isMixer;
        /// <summary>
        ///  User specified 
        /// </summary>
        public bool IsMixer
        {
            get { return _isMixer; }
            set { ChangeAndNotify(ref _isMixer, value, nameof(IsMixer), CalcPropertiesStringArray); }
        }

        /// <summary>
        ///  Radio button binding
        /// </summary>
        public enum RadioButtonsMixerOptionsEnum
        {
            OptionEstimate,
            OptionUserSpecified,
        }

        private RadioButtonsMixerOptionsEnum _mixerOptionsProperty;
        /// <summary>
        ///  User specified 
        /// </summary>
        public RadioButtonsMixerOptionsEnum MixerOptionsProperty
        {
            get { return _mixerOptionsProperty; }
            set { ChangeAndNotify(ref _mixerOptionsProperty, value, nameof(MixerOptionsProperty), CalcPropertiesStringArray); }
        }

        private List<GeneralItem> _mixers;
        /// <summary>
        /// Collection 
        /// </summary>
        public List<GeneralItem> Mixers
        {
            get { return _mixers; }

            set { ChangeAndNotify(ref _mixers, value, nameof(Mixers), CalcPropertiesStringArray); }
        }

        private string _mixerName;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string MixerName
        {
            get { return _mixerName; }
            set { ChangeAndNotify(ref _mixerName, value, nameof(MixerName), CalcPropertiesStringArray); }
        }

        private string _mixerNameSurfaceAerator;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string MixerNameSurfaceAerator
        {
            get { return _mixerNameSurfaceAerator; }
            set { ChangeAndNotify(ref _mixerNameSurfaceAerator, value, nameof(MixerNameSurfaceAerator), CalcPropertiesStringArray); }
        }

        private string _mixerNameMechanical;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string MixerNameMechanical
        {
            get { return _mixerNameMechanical; }
            set { ChangeAndNotify(ref _mixerNameMechanical, value, nameof(MixerNameMechanical), CalcPropertiesStringArray); }
        }

        private double _mixerPower;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double MixerPower
        {
            get { return _mixerPower; }
            set { ChangeAndNotify(ref _mixerPower, value, nameof(MixerPower), CalcPropertiesStringArray); }
        }

        private double _mixerPowerSurfaceAerator10000;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double MixerPowerSurfaceAerator10000
        {
            get { return _mixerPowerSurfaceAerator10000; }
            set { ChangeAndNotify(ref _mixerPowerSurfaceAerator10000, value, nameof(MixerPowerSurfaceAerator10000), CalcPropertiesStringArray); }
        }

        private double _mixerPowerSurfaceAerator20000;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double MixerPowerSurfaceAerator20000
        {
            get { return _mixerPowerSurfaceAerator20000; }
            set { ChangeAndNotify(ref _mixerPowerSurfaceAerator20000, value, nameof(MixerPowerSurfaceAerator20000), CalcPropertiesStringArray); }
        }

        private double _mixerPowerSurfaceAerator30000;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double MixerPowerSurfaceAerator30000
        {
            get { return _mixerPowerSurfaceAerator30000; }
            set { ChangeAndNotify(ref _mixerPowerSurfaceAerator30000, value, nameof(MixerPowerSurfaceAerator30000), CalcPropertiesStringArray); }
        }

        private double _mixerPowerSurfaceAerator60000;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double MixerPowerSurfaceAerator60000
        {
            get { return _mixerPowerSurfaceAerator60000; }
            set { ChangeAndNotify(ref _mixerPowerSurfaceAerator60000, value, nameof(MixerPowerSurfaceAerator60000), CalcPropertiesStringArray); }
        }

        private double _mixerPowerSurfaceAerator90000;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double MixerPowerSurfaceAerator90000
        {
            get { return _mixerPowerSurfaceAerator90000; }
            set { ChangeAndNotify(ref _mixerPowerSurfaceAerator90000, value, nameof(MixerPowerSurfaceAerator90000), CalcPropertiesStringArray); }
        }

        private double _mixerPowerMechanical10000;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double MixerPowerMechanical10000
        {
            get { return _mixerPowerMechanical10000; }
            set { ChangeAndNotify(ref _mixerPowerMechanical10000, value, nameof(MixerPowerMechanical10000), CalcPropertiesStringArray); }
        }

        private double _mixerPowerMechanical20000;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double MixerPowerMechanical20000
        {
            get { return _mixerPowerMechanical20000; }
            set { ChangeAndNotify(ref _mixerPowerMechanical20000, value, nameof(MixerPowerMechanical20000), CalcPropertiesStringArray); }
        }

        private double _mixerPowerMechanical30000;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double MixerPowerMechanical30000
        {
            get { return _mixerPowerMechanical30000; }
            set { ChangeAndNotify(ref _mixerPowerMechanical30000, value, nameof(MixerPowerMechanical30000), CalcPropertiesStringArray); }
        }

        private double _mixerPowerMechanical60000;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double MixerPowerMechanical60000
        {
            get { return _mixerPowerMechanical60000; }
            set { ChangeAndNotify(ref _mixerPowerMechanical60000, value, nameof(MixerPowerMechanical60000), CalcPropertiesStringArray); }
        }

        private double _mixerPowerMechanical90000;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double MixerPowerMechanical90000
        {
            get { return _mixerPowerMechanical90000; }
            set { ChangeAndNotify(ref _mixerPowerMechanical90000, value, nameof(MixerPowerMechanical90000), CalcPropertiesStringArray); }
        }

        private decimal _mixerCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal MixerCost
        {
            get { return _mixerCost; }
            set { ChangeAndNotify(ref _mixerCost, value, nameof(MixerCost), CalcPropertiesStringArray); }
        }

        private decimal _mixerCostSurfaceAerator10000;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal MixerCostSurfaceAerator10000
        {
            get { return _mixerCostSurfaceAerator10000; }
            set { ChangeAndNotify(ref _mixerCostSurfaceAerator10000, value, nameof(MixerCostSurfaceAerator10000), CalcPropertiesStringArray); }
        }

        private decimal _mixerCostSurfaceAerator20000;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal MixerCostSurfaceAerator20000
        {
            get { return _mixerCostSurfaceAerator20000; }
            set { ChangeAndNotify(ref _mixerCostSurfaceAerator20000, value, nameof(MixerCostSurfaceAerator20000), CalcPropertiesStringArray); }
        }

        private decimal _mixerCostSurfaceAerator30000;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal MixerCostSurfaceAerator30000
        {
            get { return _mixerCostSurfaceAerator30000; }
            set { ChangeAndNotify(ref _mixerCostSurfaceAerator30000, value, nameof(MixerCostSurfaceAerator30000), CalcPropertiesStringArray); }
        }

        private decimal _mixerCostSurfaceAerator60000;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal MixerCostSurfaceAerator60000
        {
            get { return _mixerCostSurfaceAerator60000; }
            set { ChangeAndNotify(ref _mixerCostSurfaceAerator60000, value, nameof(MixerCostSurfaceAerator60000), CalcPropertiesStringArray); }
        }

        private decimal _mixerCostSurfaceAerator90000;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal MixerCostSurfaceAerator90000
        {
            get { return _mixerCostSurfaceAerator90000; }
            set { ChangeAndNotify(ref _mixerCostSurfaceAerator90000, value, nameof(MixerCostSurfaceAerator90000), CalcPropertiesStringArray); }
        }

        private decimal _mixerCostMechanical10000;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal MixerCostMechanical10000
        {
            get { return _mixerCostMechanical10000; }
            set { ChangeAndNotify(ref _mixerCostMechanical10000, value, nameof(MixerCostMechanical10000), CalcPropertiesStringArray); }
        }

        private decimal _mixerCostMechanical20000;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal MixerCostMechanical20000
        {
            get { return _mixerCostMechanical20000; }
            set { ChangeAndNotify(ref _mixerCostMechanical20000, value, nameof(MixerCostMechanical20000), CalcPropertiesStringArray); }
        }

        private decimal _mixerCostMechanical30000;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal MixerCostMechanical30000
        {
            get { return _mixerCostMechanical30000; }
            set { ChangeAndNotify(ref _mixerCostMechanical30000, value, nameof(MixerCostMechanical30000), CalcPropertiesStringArray); }
        }

        private decimal _mixerCostMechanical60000;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal MixerCostMechanical60000
        {
            get { return _mixerCostMechanical60000; }
            set { ChangeAndNotify(ref _mixerCostMechanical60000, value, nameof(MixerCostMechanical60000), CalcPropertiesStringArray); }
        }

        private decimal _mixerCostMechanical90000;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal MixerCostMechanical90000
        {
            get { return _mixerCostMechanical90000; }
            set { ChangeAndNotify(ref _mixerCostMechanical90000, value, nameof(MixerCostMechanical90000), CalcPropertiesStringArray); }
        }

        private double _mixerPowerUserSpecified;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double MixerPowerUserSpecified
        {
            get { return _mixerPowerUserSpecified; }
            set { ChangeAndNotify(ref _mixerPowerUserSpecified, value, nameof(MixerPowerUserSpecified), CalcPropertiesStringArray); }
        }

        private decimal _mixerCostUserSpecified;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal MixerCostUserSpecified
        {
            get { return _mixerCostUserSpecified; }
            set { ChangeAndNotify(ref _mixerCostUserSpecified, value, nameof(MixerCostUserSpecified), CalcPropertiesStringArray); }
        }

        private double _mixerOperationalTimeHoursPerDay;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double MixerOperationalTimeHoursPerDay
        {
            get { return _mixerOperationalTimeHoursPerDay; }
            set { ChangeAndNotify(ref _mixerOperationalTimeHoursPerDay, value, nameof(MixerOperationalTimeHoursPerDay), CalcPropertiesStringArray); }
        }

        private double _mixerOperationalTimeDaysPerYear;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double MixerOperationalTimeDaysPerYear
        {
            get { return _mixerOperationalTimeDaysPerYear; }
            set { ChangeAndNotify(ref _mixerOperationalTimeDaysPerYear, value, nameof(MixerOperationalTimeDaysPerYear), CalcPropertiesStringArray); }
        }

        private bool _isVariableFrequencyDrive;
        /// <summary>
        ///  User specified 
        /// </summary>
        public bool IsVariableFrequencyDrive
        {
            get { return _isVariableFrequencyDrive; }
            set { ChangeAndNotify(ref _isVariableFrequencyDrive, value, nameof(IsVariableFrequencyDrive), CalcPropertiesStringArray); }
        }

        private decimal _variableFrequencyDriveUnitCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal VariableFrequencyDriveUnitCost
        {
            get { return _variableFrequencyDriveUnitCost; }
            set { ChangeAndNotify(ref _variableFrequencyDriveUnitCost, value, nameof(VariableFrequencyDriveUnitCost), CalcPropertiesStringArray); }
        }

        private bool _isStairs;
        /// <summary>
        ///  User specified 
        /// </summary>
        public bool IsStairs
        {
            get { return _isStairs; }
            set { ChangeAndNotify(ref _isStairs, value, nameof(IsStairs), CalcPropertiesStringArray); }
        }

        private decimal _stairsUnitCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal StairsUnitCost
        {
            get { return _stairsUnitCost; }
            set { ChangeAndNotify(ref _stairsUnitCost, value, nameof(StairsUnitCost), CalcPropertiesStringArray); }
        }

        private bool _isCatwalk;
        /// <summary>
        ///  User specified 
        /// </summary>
        public bool IsCatwalk
        {
            get { return _isCatwalk; }
            set { ChangeAndNotify(ref _isCatwalk, value, nameof(IsCatwalk), CalcPropertiesStringArray); }
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

        #endregion

        #region Equipment: Foundation

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

        private double _foundationDepthFiberglass;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double FoundationDepthFiberglass
        {
            get { return _foundationDepthFiberglass; }
            set { ChangeAndNotify(ref _foundationDepthFiberglass, value, nameof(FoundationDepthFiberglass), CalcPropertiesStringArray); }
        }

        private double _foundationDepthConcrete;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double FoundationDepthConcrete
        {
            get { return _foundationDepthConcrete; }
            set { ChangeAndNotify(ref _foundationDepthConcrete, value, nameof(FoundationDepthConcrete), CalcPropertiesStringArray); }
        }

        private double _foundationDepthBoltedSteel;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double FoundationDepthBoltedSteel
        {
            get { return _foundationDepthBoltedSteel; }
            set { ChangeAndNotify(ref _foundationDepthBoltedSteel, value, nameof(FoundationDepthBoltedSteel), CalcPropertiesStringArray); }
        }

        private double _foundationDepthWeldedSteel;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double FoundationDepthWeldedSteel
        {
            get { return _foundationDepthWeldedSteel; }
            set { ChangeAndNotify(ref _foundationDepthWeldedSteel, value, nameof(FoundationDepthWeldedSteel), CalcPropertiesStringArray); }
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

        #region Properties - Sizing Summary: Tank Design

        private double _calcPlugFlowRetentionTime;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcPlugFlowRetentionTime
        {
            get { return ReactionTankCalculations.CalcPlugFlowRetentionTime(DesignFlow, CalcTankTotalHeight, TankFreeboardHeight, CalcTankDiameter); }
            set { ChangeAndNotify(ref _calcPlugFlowRetentionTime, value); }
        }

        private double _calcTankTotalHeight;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcTankTotalHeight
        {
            get
            {
                switch (ReactionTankDesignOptionsProperty)
                {
                    case RadioButtonsReactionTankDesignOptionsEnum.OptionFiberglass:
                        _calcTankTotalHeight = ReactionTankCalculations.CalcTankTotalHeight(FiberglassHeight);
                        break;
                    case RadioButtonsReactionTankDesignOptionsEnum.OptionConcreteSteel:
                        _calcTankTotalHeight = ReactionTankCalculations.CalcTankTotalHeight(ConcreteSteelHeight);
                        break;
                    default:
                        break;
                }
                return _calcTankTotalHeight;  
            }
            set { ChangeAndNotify(ref _calcTankTotalHeight, value); }
        }

        private double _calcTankFreeboardHeight;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcTankFreeboardHeight
        {
            get { return ReactionTankCalculations.CalcTankFreeboardHeight(TankFreeboardHeight); }
            set { ChangeAndNotify(ref _calcTankFreeboardHeight, value); }
        }

        private double _calcTankDiameter;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcTankDiameter
        {
            get
            {
                switch (ReactionTankDesignOptionsProperty)
                {
                    case RadioButtonsReactionTankDesignOptionsEnum.OptionFiberglass:
                        _calcTankDiameter = ReactionTankCalculations.CalcTankDiameter(FiberglassDiameter);
                        break;
                    case RadioButtonsReactionTankDesignOptionsEnum.OptionConcreteSteel:
                        _calcTankDiameter = ReactionTankCalculations.CalcTankDiameter(ConcreteSteelDiameter);
                        break;
                    default:
                        break;
                }
                return _calcTankDiameter;
            }
            set { ChangeAndNotify(ref _calcTankDiameter, value); }
        }

        private double _calcTankProtectiveCoatingSurfaceArea;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcTankProtectiveCoatingSurfaceArea
        {
            get
            {
                if (TankProtectiveCoatingName == TankProtectiveCoatingNameNone)
                {
                    _calcTankProtectiveCoatingSurfaceArea = 0;
                }
                else
                {
                    _calcTankProtectiveCoatingSurfaceArea = ReactionTankCalculations.CalcTankProtectiveCoatingSurfaceArea(CalcTankDiameter, CalcTankTotalHeight);
                }
                return _calcTankProtectiveCoatingSurfaceArea;
            }
            set { ChangeAndNotify(ref _calcTankProtectiveCoatingSurfaceArea, value); }
        }

        private double _calcMixerPower;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcMixerPower
        {
            get
            {
                UpdateMixer();

                switch (MixerOptionsProperty)
                {
                    case RadioButtonsMixerOptionsEnum.OptionEstimate:
                        _calcMixerPower = ReactionTankCalculations.CalcMixerPower(MixerPower);
                        break;
                    case RadioButtonsMixerOptionsEnum.OptionUserSpecified:
                        _calcMixerPower = MixerPowerUserSpecified;
                        break;
                    default:
                        break;
                }
                return _calcMixerPower;
            }
            set { ChangeAndNotify(ref _calcMixerPower, value); }
        }        

        private double _calcFoundationArea;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcFoundationArea
        {
            get { return ReactionTankCalculations.CalcFoundationArea(FoundationSiteSoilLoadBearingMultiplier, CalcTankDiameter); }
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
                switch (ReactionTankDesignOptionsProperty)
                {
                    case RadioButtonsReactionTankDesignOptionsEnum.OptionFiberglass:
                        _calcFoundationDepth = ReactionTankCalculations.CalcFoundationDepth(FoundationDepthFiberglass);
                        break;
                    case RadioButtonsReactionTankDesignOptionsEnum.OptionConcreteSteel:
                        if (ConcreteSteelName == ConcreteSteelNameConcrete)
                        {
                            _calcFoundationDepth = ReactionTankCalculations.CalcFoundationDepth(FoundationDepthConcrete);
                        }
                        else if (ConcreteSteelName == ConcreteSteelNameBoltedSteel)
                        {
                            _calcFoundationDepth = ReactionTankCalculations.CalcFoundationDepth(FoundationDepthBoltedSteel);
                        }
                        else // ConcreteSteelName == ConcreteSteelNameWeldedSteel
                        {
                            _calcFoundationDepth = ReactionTankCalculations.CalcFoundationDepth(FoundationDepthWeldedSteel);
                        }
                        break;
                    default:
                        break;
                }
                return _calcFoundationDepth;
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
            get { return ReactionTankCalculations.CalcFoundationVolume(CalcFoundationDepth, CalcFoundationArea, TankQuantity); }
            set { ChangeAndNotify(ref _calcFoundationVolume, value); }
        }

        #endregion

        #region Properties - Capital Costs

        private decimal _calcTankMaterialCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcTankMaterialCost
        {
            get
            {
                switch (ReactionTankDesignOptionsProperty)
                {
                    case RadioButtonsReactionTankDesignOptionsEnum.OptionFiberglass:
                        _calcTankMaterialCost = ReactionTankCalculations.CalcTankMaterialCost(TankQuantity, FiberglassCost);
                        break;
                    case RadioButtonsReactionTankDesignOptionsEnum.OptionConcreteSteel:
                        _calcTankMaterialCost = ReactionTankCalculations.CalcTankMaterialCost(TankQuantity, ConcreteSteelCost);
                        break;
                    default:
                        break;
                }
                return _calcTankMaterialCost;
            }
            set { ChangeAndNotify(ref _calcTankMaterialCost, value); }
        }

        private decimal _calcTankProtectiveCoatingCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcTankProtectiveCoatingCost
        {
            get
            {
                switch (ReactionTankDesignOptionsProperty)
                {
                    case RadioButtonsReactionTankDesignOptionsEnum.OptionFiberglass:
                        _calcTankProtectiveCoatingCost = 0m;
                        break;
                    case RadioButtonsReactionTankDesignOptionsEnum.OptionConcreteSteel:
                        _calcTankProtectiveCoatingCost = ReactionTankCalculations.CalcTankProtectiveCoatingCost(CalcTankProtectiveCoatingSurfaceArea, TankQuantity, TankProtectiveCoatingUnitCost);
                        break;
                    default:
                        break;
                }
                return _calcTankProtectiveCoatingCost;
            }
            set { ChangeAndNotify(ref _calcTankProtectiveCoatingCost, value); }
        }

        private decimal _calcMixerCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcMixerCost
        {
            get
            {
                if (IsMixer)
                {
                    UpdateMixer();
                    _calcMixerCost = ReactionTankCalculations.CalcMixerCost(TankQuantity, MixerCost);
                }
                else
                {
                    _calcMixerCost = 0m;
                }
                return _calcMixerCost;
            }
            set { ChangeAndNotify(ref _calcMixerCost, value); }
        }

        private decimal _calcVariableFrequencyDriveCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcVariableFrequencyDriveCost
        {
            get
            {
                if (IsVariableFrequencyDrive)
                {
                    _calcVariableFrequencyDriveCost = ReactionTankCalculations.CalcVariableFrequencyDriveCost(TankQuantity, CalcMixerPower, VariableFrequencyDriveUnitCost);
                }
                else
                {
                    _calcVariableFrequencyDriveCost = 0m;
                }
                return _calcVariableFrequencyDriveCost;
            }
            set { ChangeAndNotify(ref _calcVariableFrequencyDriveCost, value); }
        }

        private decimal _calcCatwalkCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcCatwalkCost
        {
            get
            {
                if (IsCatwalk)
                {
                    _calcCatwalkCost = ReactionTankCalculations.CalcCatwalkCost(CalcTankDiameter, TankQuantity, CatwalkUnitCost);
                }
                else
                    _calcCatwalkCost = 0m;
                return _calcCatwalkCost;
            }
            set { ChangeAndNotify(ref _calcCatwalkCost, value); }
        }

        private decimal _calcStairsCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcStairsCost
        {
            get
            {
                if (IsStairs)
                {
                    _calcStairsCost = ReactionTankCalculations.CalcStairsCost(CalcTankTotalHeight, TankQuantity, StairsUnitCost);
                }
                else
                {
                    _calcStairsCost = 0m;
                }
                return _calcStairsCost;
            }
            set { ChangeAndNotify(ref _calcStairsCost, value); }
        }

        private decimal _calcTankEquipmentCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcTankEquipmentCost
        {
            get { return ReactionTankCalculations.CalcTankEquipmentCost(CalcTankMaterialCost, CalcTankProtectiveCoatingCost, CalcMixerCost,
                                                                        CalcVariableFrequencyDriveCost, CalcStairsCost, CalcCatwalkCost); }
            set { ChangeAndNotify(ref _calcTankEquipmentCost, value); }
        }


        private decimal _calcFoundationCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcFoundationCost
        {
            get { return ReactionTankCalculations.CalcFoundationCost(CalcFoundationVolume, FoundationConcreteMaterialAndPlacementCost); }
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
                        _calcInstallationCost = ReactionTankCalculations.CalcInstallationCost(CalcTankEquipmentCost, CalcFoundationCost, CapitalCostSystemInstallationMultiplier);
                        break;

                    case RadioButtonsCapitalCostSystemInstallOptionsEnum.OptionUserSpecified:
                        _calcInstallationCost = CapitalCostSystemInstallationUserSpecified;
                        break;
                }

                return _calcInstallationCost;
            }
            set { ChangeAndNotify(ref _calcInstallationCost, value); }
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
                _calcCapitalCostTotal = ReactionTankCalculations.CalcCapitalCostTotal(CalcTankEquipmentCost, CalcFoundationCost, CalcInstallationCost, CalcOtherCapitalItemsCost);

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
            get { return ReactionTankCalculations.CalcAnnualCostOperationAndMaintenance(AnnualCostOperationAndMaintenanceMultiplier, CalcCapitalCostTotal); }
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


        private decimal _calcTankElectricCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcTankElectricCost
        {
            get { return ReactionTankCalculations.CalcTankElectricCost(CalcMixerPower, MixerOperationalTimeHoursPerDay, MixerOperationalTimeDaysPerYear, TankQuantity, ElectricUnitCost); }
            set { ChangeAndNotify(ref _calcTankElectricCost, value); }
        }

        private decimal _calcAnnualCostElectricEstimated;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcAnnualCostElectricEstimated
        {
            get { return ReactionTankCalculations.CalcAnnualCostElectric(CalcTankElectricCost);}
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

        private decimal _calcOtherAnnualItemsCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcOtherAnnualItemsCost
        {
            get
            {
                return ReactionTankCalculations.CalcOtherAnnualItemsCost(OtherAnnualItemQuantity1, OtherAnnualItemUnitCost1,
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
                _calcAnnualCost = ReactionTankCalculations.CalcAnnualCostTotal(CalcAnnualCostOperationAndMaintenance, CalcAnnualCostElectric, CalcOtherAnnualItemsCost);
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
            get
            {
                switch (ReactionTankDesignOptionsProperty)
                {
                    case RadioButtonsReactionTankDesignOptionsEnum.OptionFiberglass:
                        _recapitalizationCostLifeCycleTank = RecapitalizationCostLifeCycleTankFiberglass;
                        break;
                    case RadioButtonsReactionTankDesignOptionsEnum.OptionConcreteSteel:
                        if (ConcreteSteelName == ConcreteSteelNameConcrete && TankProtectiveCoatingName == TankProtectiveCoatingNameNone)
                        {
                            _recapitalizationCostLifeCycleTank = RecapitalizationCostLifeCycleTankConcreteWithoutCoating;
                        }
                        else if (ConcreteSteelName == ConcreteSteelNameConcrete && TankProtectiveCoatingName != TankProtectiveCoatingNameNone)
                        {
                            _recapitalizationCostLifeCycleTank = RecapitalizationCostLifeCycleTankConcreteWithCoating;
                        }
                        else if (ConcreteSteelName == ConcreteSteelNameBoltedSteel && TankProtectiveCoatingName == TankProtectiveCoatingNameNone)
                        {
                            _recapitalizationCostLifeCycleTank = RecapitalizationCostLifeCycleTankSteelWithoutCoating;
                        }
                        else if (ConcreteSteelName == ConcreteSteelNameWeldedSteel && TankProtectiveCoatingName != TankProtectiveCoatingNameNone)
                        {
                            _recapitalizationCostLifeCycleTank = RecapitalizationCostLifeCycleTankSteelWithCoating;
                        }
                        break;
                    default:
                        break;
                }
                return _recapitalizationCostLifeCycleTank;
            }
            set { ChangeAndNotify(ref _recapitalizationCostLifeCycleTank, value, nameof(RecapitalizationCostLifeCycleTank), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostLifeCycleTankFiberglass;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostLifeCycleTankFiberglass
        {
            get { return _recapitalizationCostLifeCycleTankFiberglass; }
            set { ChangeAndNotify(ref _recapitalizationCostLifeCycleTankFiberglass, value, nameof(RecapitalizationCostLifeCycleTankFiberglass), CalcPropertiesStringArray); }
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

        private double _recapitalizationCostLifeCycleCatwalk;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostLifeCycleCatwalk
        {
            get { return _recapitalizationCostLifeCycleCatwalk; }
            set { ChangeAndNotify(ref _recapitalizationCostLifeCycleCatwalk, value, nameof(RecapitalizationCostLifeCycleCatwalk), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostLifeCycleStairs;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostLifeCycleStairs
        {
            get { return _recapitalizationCostLifeCycleStairs; }
            set { ChangeAndNotify(ref _recapitalizationCostLifeCycleStairs, value, nameof(RecapitalizationCostLifeCycleStairs), CalcPropertiesStringArray); }
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

        private double _recapitalizationCostLifeCycleMixerMotorRebuild;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostLifeCycleMixerMotorRebuild
        {
            get { return _recapitalizationCostLifeCycleMixerMotorRebuild; }
            set { ChangeAndNotify(ref _recapitalizationCostLifeCycleMixerMotorRebuild, value, nameof(RecapitalizationCostLifeCycleMixerMotorRebuild), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostLifeCycleMixerMotorReplacement;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostLifeCycleMixerMotorReplacement
        {
            get { return _recapitalizationCostLifeCycleMixerMotorReplacement; }
            set { ChangeAndNotify(ref _recapitalizationCostLifeCycleMixerMotorReplacement, value, nameof(RecapitalizationCostLifeCycleMixerMotorReplacement), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostLifeCycleVariableFrequencyDrive;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostLifeCycleVariableFrequencyDrive
        {
            get { return _recapitalizationCostLifeCycleVariableFrequencyDrive; }
            set { ChangeAndNotify(ref _recapitalizationCostLifeCycleVariableFrequencyDrive, value, nameof(RecapitalizationCostLifeCycleVariableFrequencyDrive), CalcPropertiesStringArray); }
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

        private double _recapitalizationCostPercentReplacementCatwalk;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostPercentReplacementCatwalk
        {
            get { return _recapitalizationCostPercentReplacementCatwalk; }
            set { ChangeAndNotify(ref _recapitalizationCostPercentReplacementCatwalk, value, nameof(RecapitalizationCostPercentReplacementCatwalk), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostPercentReplacementStairs;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostPercentReplacementStairs
        {
            get { return _recapitalizationCostPercentReplacementStairs; }
            set { ChangeAndNotify(ref _recapitalizationCostPercentReplacementStairs, value, nameof(RecapitalizationCostPercentReplacementStairs), CalcPropertiesStringArray); }
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

        private double _recapitalizationCostPercentReplacementMixerMotorRebuild;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostPercentReplacementMixerMotorRebuild
        {
            get { return _recapitalizationCostPercentReplacementMixerMotorRebuild; }
            set { ChangeAndNotify(ref _recapitalizationCostPercentReplacementMixerMotorRebuild, value, nameof(RecapitalizationCostPercentReplacementMixerMotorRebuild), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostPercentReplacementMixerMotorReplacement;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostPercentReplacementMixerMotorReplacement
        {
            get { return _recapitalizationCostPercentReplacementMixerMotorReplacement; }
            set { ChangeAndNotify(ref _recapitalizationCostPercentReplacementMixerMotorReplacement, value, nameof(RecapitalizationCostPercentReplacementMixerMotorReplacement), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostPercentReplacementVariableFrequencyDrive;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostPercentReplacementVariableFrequencyDrive
        {
            get { return _recapitalizationCostPercentReplacementVariableFrequencyDrive; }
            set { ChangeAndNotify(ref _recapitalizationCostPercentReplacementVariableFrequencyDrive, value, nameof(RecapitalizationCostPercentReplacementVariableFrequencyDrive), CalcPropertiesStringArray); }
        }

        private decimal _calcRapitalizationCostTank;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostTank
        {
            get
            {
                return ReactionTankCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
                                                                    RecapitalizationCostInflationRate, RecapitalizationCostLifeCycleTank,
                                                                    CalcTankMaterialCost, RecapitalizationCostPercentReplacementTank);
            }
            set { ChangeAndNotify(ref _calcRapitalizationCostTank, value); }
        }

        private decimal _calcRapitalizationCostCatwalk;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostCatwalk
        {
            get
            {
                return ReactionTankCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
                                                                    RecapitalizationCostInflationRate, RecapitalizationCostLifeCycleCatwalk,
                                                                    CalcCatwalkCost, RecapitalizationCostPercentReplacementCatwalk);
            }
            set { ChangeAndNotify(ref _calcRapitalizationCostCatwalk, value); }
        }

        private decimal _calcRapitalizationCostStairs;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostStairs
        {
            get
            {
                return ReactionTankCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
                                                                    RecapitalizationCostInflationRate, RecapitalizationCostLifeCycleStairs,
                                                                    CalcStairsCost, RecapitalizationCostPercentReplacementStairs);
            }
            set { ChangeAndNotify(ref _calcRapitalizationCostStairs, value); }
        }


        private decimal _calcRapitalizationCostTankProtectiveCoating;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostTankProtectiveCoating
        {
            get
            {
                return ReactionTankCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
                                                                    RecapitalizationCostInflationRate, RecapitalizationCostLifeCycleTankProtectiveCoating,
                                                                    CalcTankProtectiveCoatingCost, RecapitalizationCostPercentReplacementTankProtectiveCoating);
            }
            set { ChangeAndNotify(ref _calcRapitalizationCostTankProtectiveCoating, value); }
        }


        private decimal _calcRecapitalizationCostMixerMotorRebuildMaterialCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostMixerMotorRebuildMaterialCost
        {
            get
            {
                return CalcMixerCost * (decimal)(RecapitalizationCostPercentReplacementMixerMotorRebuild / 100);
            }
            set { ChangeAndNotify(ref _calcRecapitalizationCostMixerMotorRebuildMaterialCost, value); }
        }

        private decimal _calcRapitalizationCostMixerMotorRebuild;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostMixerMotorRebuild
        {
            get
            {
                return ReactionTankCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
                                                                    RecapitalizationCostInflationRate, RecapitalizationCostLifeCycleMixerMotorRebuild,
                                                                    CalcRecapitalizationCostMixerMotorRebuildMaterialCost, RecapitalizationCostPercentReplacementMixerMotorRebuild);
            }
            set { ChangeAndNotify(ref _calcRapitalizationCostMixerMotorRebuild, value); }
        }

        private decimal _calcRapitalizationCostMixerMotorReplacement;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostMixerMotorReplacement
        {
            get
            {
                return ReactionTankCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
                                                                    RecapitalizationCostInflationRate, RecapitalizationCostLifeCycleMixerMotorReplacement,
                                                                    CalcMixerCost, RecapitalizationCostPercentReplacementMixerMotorReplacement);
            }
            set { ChangeAndNotify(ref _calcRapitalizationCostMixerMotorReplacement, value); }
        }

        private decimal _calcRapitalizationCostVariableFrequencyDrive;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostVariableFrequencyDrive
        {
            get
            {
                return ReactionTankCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
                                                                    RecapitalizationCostInflationRate, RecapitalizationCostLifeCycleVariableFrequencyDrive,
                                                                    CalcVariableFrequencyDriveCost, RecapitalizationCostPercentReplacementVariableFrequencyDrive);
            }
            set { ChangeAndNotify(ref _calcRapitalizationCostVariableFrequencyDrive, value); }
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
                    case "Tank":
                        item.MaterialCostDefault = CalcTankMaterialCost;
                        break;
                    case "Catwalk":
                        item.MaterialCostDefault = CalcCatwalkCost;
                        break;
                    case "Stairs":
                        item.MaterialCostDefault = CalcStairsCost;
                        break;
                    case "TankProtectiveCoating":
                        item.MaterialCostDefault = CalcTankProtectiveCoatingCost;
                        break;
                    case "MixerMotorRebuild":
                        item.MaterialCostDefault = CalcRecapitalizationCostMixerMotorRebuildMaterialCost;
                        break;
                    case "MixerMotorReplacement":
                        item.MaterialCostDefault = CalcMixerCost;
                        break;
                    case "VariableFrequencyDrive":
                        item.MaterialCostDefault = CalcVariableFrequencyDriveCost;
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
                    case "Catwalk":
                        item.TotalCost = CalcRecapitalizationCostCatwalk;
                        break;
                    case "Stairs":
                        item.TotalCost = CalcRecapitalizationCostStairs;
                        break;
                    case "TankProtectiveCoating":
                        item.TotalCost = CalcRecapitalizationCostTankProtectiveCoating;
                        break;
                    case "MixerMotorRebuild":
                        item.TotalCost = CalcRecapitalizationCostMixerMotorRebuild;
                        break;
                    case "MixerMotorReplacement":
                        item.TotalCost = CalcRecapitalizationCostMixerMotorReplacement;
                        break;
                    case "VariableFrequencyDrive":
                        item.TotalCost = CalcRecapitalizationCostVariableFrequencyDrive;
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
                Name = "Reaction Tank",
                NameFixed = "Tank",
                LifeCycle = RecapitalizationCostLifeCycleTank,
                PercentReplacement = RecapitalizationCostPercentReplacementTank,
                MaterialCostDefault = CalcTankMaterialCost,
                UseCustomCost = false,
                MaterialCostCustom = 0m,
                TotalCost = CalcRecapitalizationCostTank
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
                Name = "Stairs",
                NameFixed = "Stairs",
                LifeCycle = RecapitalizationCostLifeCycleStairs,
                PercentReplacement = RecapitalizationCostPercentReplacementStairs,
                MaterialCostDefault = CalcStairsCost,
                UseCustomCost = false,
                MaterialCostCustom = 0m,
                TotalCost = CalcRecapitalizationCostStairs
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
                Name = "Mixer Motor Rebuild",
                NameFixed = "MixerMotorRebuild",
                LifeCycle = RecapitalizationCostLifeCycleMixerMotorRebuild,
                PercentReplacement = RecapitalizationCostPercentReplacementMixerMotorRebuild,
                MaterialCostDefault = CalcRecapitalizationCostMixerMotorRebuildMaterialCost,
                UseCustomCost = false,
                MaterialCostCustom = 0m,
                TotalCost = CalcRecapitalizationCostMixerMotorRebuild
            });
            RecapMaterials.Add(new RecapMaterial
            {
                IsSelected = true,
                Name = "Mixer Motor Replacement",
                NameFixed = "MixerMotorReplacement",
                LifeCycle = RecapitalizationCostLifeCycleMixerMotorReplacement,
                PercentReplacement = RecapitalizationCostPercentReplacementMixerMotorReplacement,
                MaterialCostDefault = CalcMixerCost,
                UseCustomCost = false,
                MaterialCostCustom = 0m,
                TotalCost = CalcRecapitalizationCostMixerMotorReplacement
            });
            RecapMaterials.Add(new RecapMaterial
            {
                IsSelected = true,
                Name = "Variable Frequency Drive",
                NameFixed = "VariableFrequencyDrive",
                LifeCycle = RecapitalizationCostLifeCycleVariableFrequencyDrive,
                PercentReplacement = RecapitalizationCostPercentReplacementVariableFrequencyDrive,
                MaterialCostDefault = CalcVariableFrequencyDriveCost,
                UseCustomCost = false,
                MaterialCostCustom = 0m,
                TotalCost = CalcRecapitalizationCostVariableFrequencyDrive
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

            //// Check if Life Cycle needs to be updated
            //if (((RecapMaterial)sender).NameFixed == "Tank")
            //{
            //    switch (ReactionTankDesignOptionsProperty)
            //    {
            //        case RadioButtonsReactionTankDesignOptionsEnum.OptionFiberglass:
            //            ((RecapMaterial)sender).LifeCycle = RecapitalizationCostLifeCycleTankFiberglass;
            //            break;
            //        case RadioButtonsReactionTankDesignOptionsEnum.OptionConcreteSteel:
            //            if (ConcreteSteelName == ConcreteSteelNameConcrete && TankProtectiveCoatingName == TankProtectiveCoatingNameNone)
            //            {
            //                ((RecapMaterial)sender).LifeCycle = RecapitalizationCostLifeCycleTankConcreteWithoutCoating;
            //            }
            //            else if (ConcreteSteelName == ConcreteSteelNameConcrete && TankProtectiveCoatingName != TankProtectiveCoatingNameNone)
            //            {
            //                ((RecapMaterial)sender).LifeCycle = RecapitalizationCostLifeCycleTankConcreteWithCoating;
            //            }
            //            else if (ConcreteSteelName == ConcreteSteelNameBoltedSteel && TankProtectiveCoatingName == TankProtectiveCoatingNameNone)
            //            {
            //                ((RecapMaterial)sender).LifeCycle = RecapitalizationCostLifeCycleTankSteelWithoutCoating;
            //            }
            //            else if (ConcreteSteelName == ConcreteSteelNameWeldedSteel && TankProtectiveCoatingName != TankProtectiveCoatingNameNone)
            //            {
            //                ((RecapMaterial)sender).LifeCycle = RecapitalizationCostLifeCycleTankSteelWithCoating;
            //            }
            //            break;
            //        default:
            //            break;
            //    }
            //}
            //else if (((RecapMaterial)sender).NameFixed == "TankProtectiveCoating")
            //{
            //    if (TankProtectiveCoatingName == TankProtectiveCoatingNameAlkyd)
            //    {
            //        ((RecapMaterial)sender).LifeCycle = RecapitalizationCostLifeCycleTankProtectiveCoatingAlkyd;
            //    }
            //    else if (TankProtectiveCoatingName == TankProtectiveCoatingNameEpoxy)
            //    {
            //        ((RecapMaterial)sender).LifeCycle = RecapitalizationCostLifeCycleTankProtectiveCoatingEpoxy;
            //    }
            //    else if (TankProtectiveCoatingName == TankProtectiveCoatingNameZincUrethane)
            //    {
            //        ((RecapMaterial)sender).LifeCycle = RecapitalizationCostLifeCycleTankProtectiveCoatingZincUrethane;
            //    }
            //}


            // Calculate the new total for the material
            ((RecapMaterial)sender).TotalCost = ReactionTankCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
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
                    case "Tank":
                        switch (ReactionTankDesignOptionsProperty)
                        {
                            case RadioButtonsReactionTankDesignOptionsEnum.OptionFiberglass:
                                item.LifeCycle = RecapitalizationCostLifeCycleTankFiberglass;
                                break;
                            case RadioButtonsReactionTankDesignOptionsEnum.OptionConcreteSteel:
                                if (ConcreteSteelName == ConcreteSteelNameConcrete && TankProtectiveCoatingName == TankProtectiveCoatingNameNone)
                                {
                                    item.LifeCycle = RecapitalizationCostLifeCycleTankConcreteWithoutCoating;
                                }
                                else if (ConcreteSteelName == ConcreteSteelNameConcrete && TankProtectiveCoatingName != TankProtectiveCoatingNameNone)
                                {
                                    item.LifeCycle = RecapitalizationCostLifeCycleTankConcreteWithCoating;
                                }
                                else if (ConcreteSteelName == ConcreteSteelNameBoltedSteel && TankProtectiveCoatingName == TankProtectiveCoatingNameNone)
                                {
                                    item.LifeCycle = RecapitalizationCostLifeCycleTankSteelWithoutCoating;
                                }
                                else if (ConcreteSteelName == ConcreteSteelNameWeldedSteel && TankProtectiveCoatingName != TankProtectiveCoatingNameNone)
                                {
                                    item.LifeCycle = RecapitalizationCostLifeCycleTankSteelWithCoating;
                                }
                                break;
                            default:
                                break;
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
        public ICommand SetFiberglassVolumeCommand { get; }
        public ICommand SetConcreteSteelMaterialCommand { get; }
        public ICommand SetConcreteSteelVolumeCommand { get; }
        public ICommand SetMixerCommand { get; }
        public ICommand SetFoundationSiteSoilCommand { get; }
        public ICommand SetTankProtectiveCoatingCommand { get; }
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

        public void SetFiberglassVolume(object fiberglassVolume)
        {
            GeneralItem itemFiberglassVolume = (GeneralItem)fiberglassVolume;

            switch (itemFiberglassVolume.Name)
            {
                case "10,000":
                    FiberglassVolumeName = FiberglassVolumeName10000;
                    FiberglassVolume = FiberglassVolume10000;
                    FiberglassCost = FiberglassCost10000;
                    FiberglassDiameter = FiberglassDiameter10000;
                    FiberglassHeight = FiberglassHeight10000;
                    if (MixerName == MixerNameMechanical)
                    {
                        MixerPower = MixerPowerMechanical10000;
                        MixerCost = MixerCostMechanical10000;
                    }
                    else // MixerName == MixerNameSurfaceAerator
                    {
                        MixerPower = MixerPowerSurfaceAerator10000;
                        MixerCost = MixerCostSurfaceAerator10000;
                    }
                    break;
                case "20,000":
                    FiberglassVolumeName = FiberglassVolumeName20000;
                    FiberglassVolume = FiberglassVolume20000;
                    FiberglassCost = FiberglassCost20000;
                    FiberglassDiameter = FiberglassDiameter20000;
                    FiberglassHeight = FiberglassHeight20000;
                    if (MixerName == MixerNameMechanical)
                    {
                        MixerPower = MixerPowerMechanical20000;
                        MixerCost = MixerCostMechanical20000;
                    }
                    else // MixerName == MixerNameSurfaceAerator
                    {
                        MixerPower = MixerPowerSurfaceAerator20000;
                        MixerCost = MixerCostSurfaceAerator20000;
                    }
                    break;
                case "30,000":
                    FiberglassVolumeName = FiberglassVolumeName30000;
                    FiberglassVolume = FiberglassVolume30000;
                    FiberglassCost = FiberglassCost30000;
                    FiberglassDiameter = FiberglassDiameter30000;
                    FiberglassHeight = FiberglassHeight30000;
                    if (MixerName == MixerNameMechanical)
                    {
                        MixerPower = MixerPowerMechanical30000;
                        MixerCost = MixerCostMechanical30000;
                    }
                    else // MixerName == MixerNameSurfaceAerator
                    {
                        MixerPower = MixerPowerSurfaceAerator30000;
                        MixerCost = MixerCostSurfaceAerator30000;
                    }
                    break;
                default:
                    break;
            }
        }

        public void SetConcreteSteelMaterial(object concreteSteelMaterial)
        {
            GeneralItem itemConcreteSteelMaterial = (GeneralItem)concreteSteelMaterial;

            switch (itemConcreteSteelMaterial.Name)
            {
                case "Concrete":
                    ConcreteSteelName = ConcreteSteelNameConcrete;

                    if (ConcreteSteelVolume == ConcreteSteelVolume30000)
                    {
                        ConcreteSteelCost = ConcreteCost30000;
                        ConcreteSteelDiameter = ConcreteSteelDiameter30000;
                        ConcreteSteelHeight = ConcreteSteelHeight30000;
                    }
                    else if (ConcreteSteelVolume == ConcreteSteelVolume60000)
                    {
                        ConcreteSteelCost = ConcreteCost60000;
                        ConcreteSteelDiameter = ConcreteSteelDiameter60000;
                        ConcreteSteelHeight = ConcreteSteelHeight60000;
                    }
                    else // ConcreteSteelVolume == ConcreteSteelVolume90000
                    {
                        ConcreteSteelCost = ConcreteCost90000;
                        ConcreteSteelDiameter = ConcreteSteelDiameter90000;
                        ConcreteSteelHeight = ConcreteSteelHeight90000;
                    }

                    TankProtectiveCoatings = TankProtectiveCoatingsConcrete;
                    if (TankProtectiveCoatingName == TankProtectiveCoatingNameAlkyd || TankProtectiveCoatingName == TankProtectiveCoatingNameZincUrethane)
                    {
                        TankProtectiveCoatingName = TankProtectiveCoatingNameEpoxy;
                        TankProtectiveCoatingUnitCost = TankProtectiveCoatingUnitCostEpoxy;
                    }
                    break;
                case "Bolted Steel":
                    ConcreteSteelName = ConcreteSteelNameBoltedSteel;

                    if (ConcreteSteelVolume == ConcreteSteelVolume30000)
                    {
                        ConcreteSteelCost = BoltedSteelCost30000;
                        ConcreteSteelDiameter = ConcreteSteelDiameter30000;
                        ConcreteSteelHeight = ConcreteSteelHeight30000;
                    }
                    else if (ConcreteSteelVolume == ConcreteSteelVolume60000)
                    {
                        ConcreteSteelCost = BoltedSteelCost60000;
                        ConcreteSteelDiameter = ConcreteSteelDiameter60000;
                        ConcreteSteelHeight = ConcreteSteelHeight60000;
                    }
                    else // ConcreteSteelVolume == ConcreteSteelVolume90000
                    {
                        ConcreteSteelCost = BoltedSteelCost90000;
                        ConcreteSteelDiameter = ConcreteSteelDiameter90000;
                        ConcreteSteelHeight = ConcreteSteelHeight90000;
                    }

                    TankProtectiveCoatings = TankProtectiveCoatingsSteel;
                    break;
                case "Welded Steel":
                    ConcreteSteelName = ConcreteSteelNameWeldedSteel;

                    if (ConcreteSteelVolume == ConcreteSteelVolume30000)
                    {
                        ConcreteSteelCost = WeldedSteelCost30000;
                        ConcreteSteelDiameter = ConcreteSteelDiameter30000;
                        ConcreteSteelHeight = ConcreteSteelHeight30000;
                    }
                    else if (ConcreteSteelVolume == ConcreteSteelVolume60000)
                    {
                        ConcreteSteelCost = WeldedSteelCost60000;
                        ConcreteSteelDiameter = ConcreteSteelDiameter60000;
                        ConcreteSteelHeight = ConcreteSteelHeight60000;
                    }
                    else // ConcreteSteelVolume == ConcreteSteelVolume90000
                    {
                        ConcreteSteelCost = WeldedSteelCost90000;
                        ConcreteSteelDiameter = ConcreteSteelDiameter90000;
                        ConcreteSteelHeight = ConcreteSteelHeight90000;
                    }

                    TankProtectiveCoatings = TankProtectiveCoatingsSteel;
                    break;
                default:
                    break;
            }
        }

        public void SetConcreteSteelVolume(object concreteSteelVolume)
        {
            GeneralItem itemConcreteSteelVolume = (GeneralItem)concreteSteelVolume;

            switch (itemConcreteSteelVolume.Name)
            {
                case "30,000":
                    ConcreteSteelVolumeName = ConcreteSteelVolumeName30000;
                    ConcreteSteelVolume = ConcreteSteelVolume30000;
                    ConcreteSteelDiameter = ConcreteSteelDiameter30000;
                    ConcreteSteelHeight = ConcreteSteelHeight30000;
                    if (ConcreteSteelName == ConcreteSteelNameConcrete)
                    {
                        ConcreteSteelCost = ConcreteCost30000;
                    }
                    else if (ConcreteSteelName == ConcreteSteelNameBoltedSteel)
                    {
                        ConcreteSteelCost = BoltedSteelCost30000;
                    }
                    else  //ConcreteSteelName == ConcreteSteelNameWeldedSteel
                    {
                        ConcreteSteelCost = WeldedSteelCost30000;
                    }

                    break;
                case "60,000":
                    ConcreteSteelVolumeName = ConcreteSteelVolumeName60000;
                    ConcreteSteelVolume = ConcreteSteelVolume60000;
                    ConcreteSteelDiameter = ConcreteSteelDiameter60000;
                    ConcreteSteelHeight = ConcreteSteelHeight60000;
                    if (ConcreteSteelName == ConcreteSteelNameConcrete)
                    {
                        ConcreteSteelCost = ConcreteCost60000;
                    }
                    else if (ConcreteSteelName == ConcreteSteelNameBoltedSteel)
                    {
                        ConcreteSteelCost = BoltedSteelCost60000;
                    }
                    else  //ConcreteSteelName == ConcreteSteelNameWeldedSteel
                    {
                        ConcreteSteelCost = WeldedSteelCost60000;
                    }

                    break;
                case "90,000":
                    ConcreteSteelVolumeName = ConcreteSteelVolumeName90000;
                    ConcreteSteelVolume = ConcreteSteelVolume90000;
                    ConcreteSteelDiameter = ConcreteSteelDiameter90000;
                    ConcreteSteelHeight = ConcreteSteelHeight90000;
                    if (ConcreteSteelName == ConcreteSteelNameConcrete)
                    {
                        ConcreteSteelCost = ConcreteCost90000;
                    }
                    else if (ConcreteSteelName == ConcreteSteelNameBoltedSteel)
                    {
                        ConcreteSteelCost = BoltedSteelCost90000;
                    }
                    else  //ConcreteSteelName == ConcreteSteelNameWeldedSteel
                    {
                        ConcreteSteelCost = WeldedSteelCost90000;
                    }

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

        public void SetMixer(object mixer)
        {
            GeneralItem itemMixer = (GeneralItem)mixer;

            switch (itemMixer.Name)
            {
                case "Surface Aerator":
                    MixerName = MixerNameSurfaceAerator;

                    switch (ReactionTankDesignOptionsProperty)
                    {
                        case RadioButtonsReactionTankDesignOptionsEnum.OptionFiberglass:
                            if (FiberglassVolume == FiberglassVolume10000)
                            {
                                MixerPower = MixerPowerSurfaceAerator10000;
                                MixerCost = MixerCostSurfaceAerator10000;
                            }
                            else if (FiberglassVolume == FiberglassVolume20000)
                            {
                                MixerPower = MixerPowerSurfaceAerator20000;
                                MixerCost = MixerCostSurfaceAerator20000;
                            }
                            else  //FiberglassVolume == FiberglassVolume30000
                            {
                                MixerPower = MixerPowerSurfaceAerator30000;
                                MixerCost = MixerCostSurfaceAerator30000;
                            }
                            break;
                        case RadioButtonsReactionTankDesignOptionsEnum.OptionConcreteSteel:
                            if (ConcreteSteelVolume == ConcreteSteelVolume30000)
                            {
                                MixerPower = MixerPowerSurfaceAerator30000;
                                MixerCost = MixerCostSurfaceAerator30000;
                            }
                            else if (ConcreteSteelVolume == ConcreteSteelVolume60000)
                            {
                                MixerPower = MixerPowerSurfaceAerator60000;
                                MixerCost = MixerCostSurfaceAerator60000;
                            }
                            else  //ConcreteSteelVolume == ConcreteSteelVolume30000
                            {
                                MixerPower = MixerPowerSurfaceAerator90000;
                                MixerCost = MixerCostSurfaceAerator90000;
                            }
                            break;
                        default:
                            break;
                    }

                    break;

                case "Mechanical":
                    MixerName = MixerNameMechanical;

                    switch (ReactionTankDesignOptionsProperty)
                    {
                        case RadioButtonsReactionTankDesignOptionsEnum.OptionFiberglass:
                            if (FiberglassVolume == FiberglassVolume10000)
                            {
                                MixerPower = MixerPowerMechanical10000;
                                MixerCost = MixerCostMechanical10000;
                            }
                            else if (FiberglassVolume == FiberglassVolume20000)
                            {
                                MixerPower = MixerPowerMechanical20000;
                                MixerCost = MixerCostMechanical20000;
                            }
                            else  //FiberglassVolume == FiberglassVolume30000
                            {
                                MixerPower = MixerPowerMechanical30000;
                                MixerCost = MixerCostMechanical30000;
                            }
                            break;
                        case RadioButtonsReactionTankDesignOptionsEnum.OptionConcreteSteel:
                            if (ConcreteSteelVolume == ConcreteSteelVolume30000)
                            {
                                MixerPower = MixerPowerMechanical30000;
                                MixerCost = MixerCostMechanical30000;
                            }
                            else if (ConcreteSteelVolume == ConcreteSteelVolume60000)
                            {
                                MixerPower = MixerPowerMechanical60000;
                                MixerCost = MixerCostMechanical60000;
                            }
                            else  //ConcreteSteelVolume == ConcreteSteelVolume90000
                            {
                                MixerPower = MixerPowerMechanical90000;
                                MixerCost = MixerCostMechanical90000;
                            }
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

        public void UpdateMixer()
        {
            if (MixerName == MixerNameSurfaceAerator)
            {
                switch (ReactionTankDesignOptionsProperty)
                {
                    case RadioButtonsReactionTankDesignOptionsEnum.OptionFiberglass:
                        if (FiberglassVolume == FiberglassVolume10000)
                        {
                            MixerPower = MixerPowerSurfaceAerator10000;
                            MixerCost = MixerCostSurfaceAerator10000;
                        }
                        else if (FiberglassVolume == FiberglassVolume20000)
                        {
                            MixerPower = MixerPowerSurfaceAerator20000;
                            MixerCost = MixerCostSurfaceAerator20000;
                        }
                        else  //FiberglassVolume == FiberglassVolume30000
                        {
                            MixerPower = MixerPowerSurfaceAerator30000;
                            MixerCost = MixerCostSurfaceAerator30000;
                        }
                        break;
                    case RadioButtonsReactionTankDesignOptionsEnum.OptionConcreteSteel:
                        if (ConcreteSteelVolume == ConcreteSteelVolume30000)
                        {
                            MixerPower = MixerPowerSurfaceAerator30000;
                            MixerCost = MixerCostSurfaceAerator30000;
                        }
                        else if (ConcreteSteelVolume == ConcreteSteelVolume60000)
                        {
                            MixerPower = MixerPowerSurfaceAerator60000;
                            MixerCost = MixerCostSurfaceAerator60000;
                        }
                        else  //ConcreteSteelVolume == ConcreteSteelVolume30000
                        {
                            MixerPower = MixerPowerSurfaceAerator90000;
                            MixerCost = MixerCostSurfaceAerator90000;
                        }
                        break;
                    default:
                        break;
                }
            }
            else // "Mechanical"
            {
                switch (ReactionTankDesignOptionsProperty)
                {
                    case RadioButtonsReactionTankDesignOptionsEnum.OptionFiberglass:
                        if (FiberglassVolume == FiberglassVolume10000)
                        {
                            MixerPower = MixerPowerMechanical10000;
                            MixerCost = MixerCostMechanical10000;
                        }
                        else if (FiberglassVolume == FiberglassVolume20000)
                        {
                            MixerPower = MixerPowerMechanical20000;
                            MixerCost = MixerCostMechanical20000;
                        }
                        else  //FiberglassVolume == FiberglassVolume30000
                        {
                            MixerPower = MixerPowerMechanical30000;
                            MixerCost = MixerCostMechanical30000;
                        }
                        break;
                    case RadioButtonsReactionTankDesignOptionsEnum.OptionConcreteSteel:
                        if (ConcreteSteelVolume == ConcreteSteelVolume30000)
                        {
                            MixerPower = MixerPowerMechanical30000;
                            MixerCost = MixerCostMechanical30000;
                        }
                        else if (ConcreteSteelVolume == ConcreteSteelVolume60000)
                        {
                            MixerPower = MixerPowerMechanical60000;
                            MixerCost = MixerCostMechanical60000;
                        }
                        else  //ConcreteSteelVolume == ConcreteSteelVolume90000
                        {
                            MixerPower = MixerPowerMechanical90000;
                            MixerCost = MixerCostMechanical90000;
                        }
                        break;
                    default:
                        break;
                }
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

        public ReactionTankViewModel(IDialogCoordinator dialogCoordinator)
        {
            // MahApps dialog coordinator
            _dialogCoordinator = dialogCoordinator;

            // Assign the proper functions to the open and save commands
            OpenCommand = new RelayCommand(OpenFile);
            SaveCommand = new RelayCommand(SaveFile);
            HelpCommand = new RelayCommand(ShowHelp);
            SyncCommand = new RelayCommand(SyncWithMainUi);

            SetFiberglassVolumeCommand = new RelayCommandWithParameter(SetFiberglassVolume);
            SetConcreteSteelMaterialCommand = new RelayCommandWithParameter(SetConcreteSteelMaterial);
            SetConcreteSteelVolumeCommand = new RelayCommandWithParameter(SetConcreteSteelVolume);
            SetMixerCommand = new RelayCommandWithParameter(SetMixer);
            SetFoundationSiteSoilCommand = new RelayCommandWithParameter(SetFoundationSiteSoil);
            SetTankProtectiveCoatingCommand = new RelayCommandWithParameter(SetTankProtectiveCoating);

            // Get a list of property names and filter the names to include only those that start with "Calc" in order
            // to use with the NotifyAndChange.  This eliminates the need to write every property name that needs 
            // to be notified of changes by the user.
            PropertiesStringList = Helpers.GetNamesOfClassProperties(this);
            CalcPropertiesStringArray = Helpers.FilterPropertiesList(PropertiesStringList, "Calc");

            // Initialize the model name and user name
            ModuleType = "ReactionTank";
            ModuleName = "Untitled module";
            ModuleTreatmentType = "Active";
            ModuleId = random.Next(1000);

            IsError = false;
            IsMajorError = false;
            ErrorMessage = "";
            ErrorMessageShort = "";

            // Initialize check boxes
            IsMixer = true;
            IsVariableFrequencyDrive = true;
            IsStairs = true;
            IsCatwalk = true;


            // Initialize radio buttons
            ReactionTankDesignOptionsProperty = RadioButtonsReactionTankDesignOptionsEnum.OptionConcreteSteel;
            MixerOptionsProperty = RadioButtonsMixerOptionsEnum.OptionEstimate;

            CapitalCostSystemInstallOptionsProperty = RadioButtonsCapitalCostSystemInstallOptionsEnum.OptionCostMultiplier;

            AnnualCostOperationAndMaintenanceOptionsProperty = RadioButtonsAnnualCostOperationAndMaintenanceOptionsEnum.OptionAnnualCostMultiplier;
            AnnualCostElectricOptionsProperty = RadioButtonsAnnualCostElectricOptionsEnum.OptionAnnualCostEstimated;

            // Dummy variables to force syncing
            Dummy = 0;

            // Read the xml data file and assign property values accordingly.
            OpenXmlAndAssignValues(@"..\..\Data\default-data-reaction-tank.xml");

            // Fiberglass 
            FiberglassVolumeName = FiberglassVolumeName30000;
            FiberglassVolume = FiberglassVolume30000;
            FiberglassCost = FiberglassCost30000;
            FiberglassDiameter = FiberglassDiameter30000;
            FiberglassHeight = FiberglassHeight30000;

            FiberglassVolumes = new List<GeneralItem>
            {
                new GeneralItem {Name = FiberglassVolumeName10000, Value = FiberglassVolume10000 },
                new GeneralItem {Name = FiberglassVolumeName20000, Value = FiberglassVolume20000 },
                new GeneralItem {Name = FiberglassVolumeName30000, Value = FiberglassVolume30000 },
            };

            // Concrete Steel 
            ConcreteSteelName = ConcreteSteelNameConcrete;
            ConcreteSteelVolume = ConcreteSteelVolume30000;
            ConcreteSteelVolumeName = ConcreteSteelVolumeName30000;
            ConcreteSteelCost = ConcreteCost30000;
            ConcreteSteelDiameter = ConcreteSteelDiameter30000;
            ConcreteSteelHeight = ConcreteSteelHeight30000;

            ConcreteSteelMaterials = new List<GeneralItem>
            {
                new GeneralItem {Name = ConcreteSteelNameConcrete},
                new GeneralItem {Name = ConcreteSteelNameBoltedSteel },
                new GeneralItem {Name = ConcreteSteelNameWeldedSteel },
            };

            ConcreteSteelVolumes = new List<GeneralItem>
            {
                new GeneralItem {Name = ConcreteSteelVolumeName30000, Value = ConcreteSteelVolume30000 },
                new GeneralItem {Name = ConcreteSteelVolumeName60000, Value = ConcreteSteelVolume60000 },
                new GeneralItem {Name = ConcreteSteelVolumeName90000, Value = ConcreteSteelVolume90000 },
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

            // Mixer
            MixerName = MixerNameSurfaceAerator;
            MixerCost = MixerCostSurfaceAerator30000;
            MixerPower = MixerPowerSurfaceAerator30000;

            Mixers = new List<GeneralItem>
            {
                new GeneralItem {Name = MixerNameSurfaceAerator},
                new GeneralItem {Name =  MixerNameMechanical},
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
