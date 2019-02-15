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

    public class SiteDevelopmentViewModel : PropertyChangedBase, IObserver<SharedData>
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

        #region Properties - Property Acquisition Access

        /// <summary>
        ///  Radio button binding
        /// </summary>
        public enum RadioButtonsLandQuantityOptionsEnum
        {
            OptionEstimate,
            OptionUserSpecified,
        }

        private RadioButtonsLandQuantityOptionsEnum _landQuantityOptionsProperty;
        /// <summary>
        ///  User specified 
        /// </summary>
        public RadioButtonsLandQuantityOptionsEnum LandQuantityOptionsProperty
        {
            get { return _landQuantityOptionsProperty; }
            set { ChangeAndNotify(ref _landQuantityOptionsProperty, value, nameof(LandQuantityOptionsProperty), CalcPropertiesStringArray); }
        }

        private double _landQuantityUserSpecified;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double LandQuantityUserSpecified
        {
            get { return _landQuantityUserSpecified; }
            set { ChangeAndNotify(ref _landQuantityUserSpecified, value, nameof(LandQuantityUserSpecified), CalcPropertiesStringArray); }
        }

        /// <summary>
        ///  Radio button binding
        /// </summary>
        public enum RadioButtonsLandCostOptionsEnum
        {
            OptionPurchase,
            OptionLease,
        }

        private RadioButtonsLandCostOptionsEnum _landCostOptionsProperty;
        /// <summary>
        ///  User specified 
        /// </summary>
        public RadioButtonsLandCostOptionsEnum LandCostOptionsProperty
        {
            get { return _landCostOptionsProperty; }
            set { ChangeAndNotify(ref _landCostOptionsProperty, value, nameof(LandCostOptionsProperty), CalcPropertiesStringArray); }
        }

        private decimal _landPurchaseUnitCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal LandPurchaseUnitCost
        {
            get { return _landPurchaseUnitCost; }
            set { ChangeAndNotify(ref _landPurchaseUnitCost, value, nameof(LandPurchaseUnitCost), CalcPropertiesStringArray); }
        }

        private decimal _landPurchaseClosingCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal LandPurchaseClosingCost
        {
            get { return _landPurchaseClosingCost; }
            set { ChangeAndNotify(ref _landPurchaseClosingCost, value, nameof(LandPurchaseClosingCost), CalcPropertiesStringArray); }
        }

        private decimal _landLeaseCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal LandLeaseCost
        {
            get { return _landLeaseCost; }
            set { ChangeAndNotify(ref _landLeaseCost, value, nameof(LandLeaseCost), CalcPropertiesStringArray); }
        }

        #endregion

        #region Properties - Site Work Unit Costs and Information

        private double _mobilizationDemobilizationPercentage;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double MobilizationDemobilizationPercentage
        {
            get { return _mobilizationDemobilizationPercentage; }
            set { ChangeAndNotify(ref _mobilizationDemobilizationPercentage, value, nameof(MobilizationDemobilizationPercentage), CalcPropertiesStringArray); }
        }

        private bool _isConstructionOfficeTrailer;
        /// <summary>
        ///  User specified 
        /// </summary>
        public bool IsConstructionOfficeTrailer
        {
            get { return _isConstructionOfficeTrailer; }
            set { ChangeAndNotify(ref _isConstructionOfficeTrailer, value, nameof(IsConstructionOfficeTrailer), CalcPropertiesStringArray); }
        }

        private decimal _constructionOfficeTrailerCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal ConstructionOfficeTrailerCost
        {
            get { return _constructionOfficeTrailerCost; }
            set { ChangeAndNotify(ref _constructionOfficeTrailerCost, value, nameof(ConstructionOfficeTrailerCost), CalcPropertiesStringArray); }
        }

        private double _averageSiteSlope;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double AverageSiteSlope
        {
            get { return _averageSiteSlope; }
            set { ChangeAndNotify(ref _averageSiteSlope, value, nameof(AverageSiteSlope), CalcPropertiesStringArray); }
        }

        private double _cutFillSlope;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double CutFillSlope
        {
            get { return _cutFillSlope; }
            set { ChangeAndNotify(ref _cutFillSlope, value, nameof(CutFillSlope), CalcPropertiesStringArray); }
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

        private decimal _maintenanceRepairLaborRate;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal MaintenanceRepairLaborRate
        {
            get { return _maintenanceRepairLaborRate; }
            set { ChangeAndNotify(ref _maintenanceRepairLaborRate, value, nameof(MaintenanceRepairLaborRate), CalcPropertiesStringArray); }
        }

        private bool _isFoundationOverExcavation;
        /// <summary>
        ///  User specified 
        /// </summary>
        public bool IsFoundationOverExcavation
        {
            get { return _isFoundationOverExcavation; }
            set { ChangeAndNotify(ref _isFoundationOverExcavation, value, nameof(IsFoundationOverExcavation), CalcPropertiesStringArray); }
        }


        private decimal _foundationOverExcavationUnitCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal FoundationOverExcavationUnitCost
        {
            get { return _foundationOverExcavationUnitCost; }
            set { ChangeAndNotify(ref _foundationOverExcavationUnitCost, value, nameof(FoundationOverExcavationUnitCost), CalcPropertiesStringArray); }
        }

        private decimal _foundationImprovementUnitCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal FoundationImprovementUnitCost
        {
            get { return _foundationImprovementUnitCost; }
            set { ChangeAndNotify(ref _foundationImprovementUnitCost, value, nameof(FoundationImprovementUnitCost), CalcPropertiesStringArray); }
        }
        #endregion

        #region Properties - Access Road and Parking

        private bool _isAccessRoad;
        /// <summary>
        ///  User specified 
        /// </summary>
        public bool IsAccessRoad
        {
            get { return _isAccessRoad; }
            set { ChangeAndNotify(ref _isAccessRoad, value, nameof(IsAccessRoad), CalcPropertiesStringArray); }
        }

        private List<GeneralCostItem> _roadMaterials;
        /// <summary>
        /// Collection 
        /// </summary>
        public List<GeneralCostItem> RoadMaterials
        {
            get { return _roadMaterials; }

            set { ChangeAndNotify(ref _roadMaterials, value, nameof(RoadMaterials), CalcPropertiesStringArray); }
        }

        private string _roadMaterialName;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string RoadMaterialName
        {
            get { return _roadMaterialName; }
            set { ChangeAndNotify(ref _roadMaterialName, value, nameof(RoadMaterialName), CalcPropertiesStringArray); }
        }

        private string _roadMaterialNameAggregate;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string RoadMaterialNameAggregate
        {
            get { return _roadMaterialNameAggregate; }
            set { ChangeAndNotify(ref _roadMaterialNameAggregate, value, nameof(RoadMaterialNameAggregate), CalcPropertiesStringArray); }
        }

        private string _roadMaterialNameAsphalt;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string RoadMaterialNameAsphalt
        {
            get { return _roadMaterialNameAsphalt; }
            set { ChangeAndNotify(ref _roadMaterialNameAsphalt, value, nameof(RoadMaterialNameAsphalt), CalcPropertiesStringArray); }
        }

        private decimal _roadMaterialUnitCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal RoadMaterialUnitCost
        {
            get
            {
                if (RoadMaterialName == RoadMaterialNameAggregate)
                {
                    _roadMaterialUnitCost = RoadMaterialUnitCostAggregate;
                }
                else
                {
                    _roadMaterialUnitCost = RoadMaterialUnitCostAsphalt;
                }
                return _roadMaterialUnitCost;
            }
            set { ChangeAndNotify(ref _roadMaterialUnitCost, value, nameof(RoadMaterialUnitCost), CalcPropertiesStringArray); }
        }

        private decimal _roadMaterialUnitCostAggregate;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal RoadMaterialUnitCostAggregate
        {
            get { return _roadMaterialUnitCostAggregate; }
            set { ChangeAndNotify(ref _roadMaterialUnitCostAggregate, value, nameof(RoadMaterialUnitCostAggregate), CalcPropertiesStringArray); }
        }

        private decimal _roadMaterialUnitCostSubbase;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal RoadMaterialUnitCostSubbase
        {
            get { return _roadMaterialUnitCostSubbase; }
            set { ChangeAndNotify(ref _roadMaterialUnitCostSubbase, value, nameof(RoadMaterialUnitCostSubbase), CalcPropertiesStringArray); }
        }

        private decimal _roadMaterialUnitCostAsphalt;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal RoadMaterialUnitCostAsphalt
        {
            get { return _roadMaterialUnitCostAsphalt; }
            set { ChangeAndNotify(ref _roadMaterialUnitCostAsphalt, value, nameof(RoadMaterialUnitCostAsphalt), CalcPropertiesStringArray); }
        }

        private double _accessRoadLength;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double AccessRoadLength
        {
            get { return _accessRoadLength; }
            set { ChangeAndNotify(ref _accessRoadLength, value, nameof(AccessRoadLength), CalcPropertiesStringArray); }
        }

        private double _accessRoadWidth;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double AccessRoadWidth
        {
            get { return _accessRoadWidth; }
            set { ChangeAndNotify(ref _accessRoadWidth, value, nameof(AccessRoadWidth), CalcPropertiesStringArray); }
        }

        private bool _isAccessRoadGeotextile;
        /// <summary>
        ///  User specified 
        /// </summary>
        public bool IsAccessRoadGeotextile
        {
            get { return _isAccessRoadGeotextile; }
            set { ChangeAndNotify(ref _isAccessRoadGeotextile, value, nameof(IsAccessRoadGeotextile), CalcPropertiesStringArray); }
        }

        private double _accessRoadGeotextileLength;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double AccessRoadGeotextileLength
        {
            get { return _accessRoadGeotextileLength; }
            set { ChangeAndNotify(ref _accessRoadGeotextileLength, value, nameof(AccessRoadGeotextileLength), CalcPropertiesStringArray); }
        }

        private bool _isParkingArea;
        /// <summary>
        ///  User specified 
        /// </summary>
        public bool IsParkingArea
        {
            get { return _isParkingArea; }
            set { ChangeAndNotify(ref _isParkingArea, value, nameof(IsParkingArea), CalcPropertiesStringArray); }
        }

        private List<GeneralCostItem> _parkingAreaMaterials;
        /// <summary>
        /// Collection 
        /// </summary>
        public List<GeneralCostItem> ParkingAreaMaterials
        {
            get { return _parkingAreaMaterials; }

            set { ChangeAndNotify(ref _parkingAreaMaterials, value, nameof(ParkingAreaMaterials), CalcPropertiesStringArray); }
        }

        private string _parkingAreaMaterialName;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string ParkingAreaMaterialName
        {
            get { return _parkingAreaMaterialName; }
            set { ChangeAndNotify(ref _parkingAreaMaterialName, value, nameof(ParkingAreaMaterialName), CalcPropertiesStringArray); }
        }

        private string _parkingAreaMaterialNameAggregate;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string ParkingAreaMaterialNameAggregate
        {
            get { return _parkingAreaMaterialNameAggregate; }
            set { ChangeAndNotify(ref _parkingAreaMaterialNameAggregate, value, nameof(ParkingAreaMaterialNameAggregate), CalcPropertiesStringArray); }
        }

        private string _parkingAreaMaterialNameAsphalt;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string ParkingAreaMaterialNameAsphalt
        {
            get { return _parkingAreaMaterialNameAsphalt; }
            set { ChangeAndNotify(ref _parkingAreaMaterialNameAsphalt, value, nameof(ParkingAreaMaterialNameAsphalt), CalcPropertiesStringArray); }
        }

        private decimal _parkingMaterialUnitCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal ParkingMaterialUnitCost
        {
            get { return _parkingMaterialUnitCost; }
            set { ChangeAndNotify(ref _parkingMaterialUnitCost, value, nameof(ParkingMaterialUnitCost), CalcPropertiesStringArray); }
        }

        private double _parkingSpacesQuantity;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double ParkingSpacesQuantity
        {
            get { return _parkingSpacesQuantity; }
            set { ChangeAndNotify(ref _parkingSpacesQuantity, value, nameof(ParkingSpacesQuantity), CalcPropertiesStringArray); }
        }

        private double _averageParkingSpaceArea;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double AverageParkingSpaceArea
        {
            get { return _averageParkingSpaceArea; }
            set { ChangeAndNotify(ref _averageParkingSpaceArea, value, nameof(AverageParkingSpaceArea), CalcPropertiesStringArray); }
        }

        private double _deliveryTruckTurnaroundArea;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double DeliveryTruckTurnaroundArea
        {
            get { return _deliveryTruckTurnaroundArea; }
            set { ChangeAndNotify(ref _deliveryTruckTurnaroundArea, value, nameof(DeliveryTruckTurnaroundArea), CalcPropertiesStringArray); }
        }

        private decimal _parkingLotAccessoriesCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal ParkingLotAccessoriesCost
        {
            get { return _parkingLotAccessoriesCost; }
            set { ChangeAndNotify(ref _parkingLotAccessoriesCost, value, nameof(ParkingLotAccessoriesCost), CalcPropertiesStringArray); }
        }

        private decimal _geotextileUnitCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal GeotextileUnitCost
        {
            get { return _geotextileUnitCost; }
            set { ChangeAndNotify(ref _geotextileUnitCost, value, nameof(GeotextileUnitCost), CalcPropertiesStringArray); }
        }
        #endregion

        #region Properties - ES Controls

        /// <summary>
        ///  Radio button binding
        /// </summary>
        public enum RadioButtonsESControlOptionsEnum
        {
            OptionEstimate,
            OptionUserSpecified,
        }

        private RadioButtonsESControlOptionsEnum _esControlOptionsProperty;
        /// <summary>
        ///  User specified 
        /// </summary>
        public RadioButtonsESControlOptionsEnum ESControlOptionsProperty
        {
            get { return _esControlOptionsProperty; }
            set { ChangeAndNotify(ref _esControlOptionsProperty, value, nameof(ESControlOptionsProperty), CalcPropertiesStringArray); }
        }

        private decimal _esControlUnitCostEstimate;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal ESControlUnitCostEstimate
        {
            get { return _esControlUnitCostEstimate; }
            set { ChangeAndNotify(ref _esControlUnitCostEstimate, value, nameof(ESControlUnitCostEstimate), CalcPropertiesStringArray); }
        }

        private bool _isESControlSiltFenceCompostFilterSock;
        /// <summary>
        ///  User specified 
        /// </summary>
        public bool IsESControlSiltFenceCompostFilterSock
        {
            get { return _isESControlSiltFenceCompostFilterSock; }
            set { ChangeAndNotify(ref _isESControlSiltFenceCompostFilterSock, value, nameof(IsESControlSiltFenceCompostFilterSock), CalcPropertiesStringArray); }
        }

        private List<GeneralItem> _esControlMaterials;
        /// <summary>
        /// Collection 
        /// </summary>
        public List<GeneralItem> ESControlMaterials
        {
            get { return _esControlMaterials; }

            set { ChangeAndNotify(ref _esControlMaterials, value, nameof(ESControlMaterials), CalcPropertiesStringArray); }
        }

        private string _esControlName;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string ESControlName
        {
            get { return _esControlName; }
            set { ChangeAndNotify(ref _esControlName, value, nameof(ESControlName), CalcPropertiesStringArray); }
        }

        private string _esControlNameSiltFence;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string ESControlNameSiltFence
        {
            get { return _esControlNameSiltFence; }
            set { ChangeAndNotify(ref _esControlNameSiltFence, value, nameof(ESControlNameSiltFence), CalcPropertiesStringArray); }
        }

        private string _esControlNameCompostFilterSock;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string ESControlNameCompostFilterSock
        {
            get { return _esControlNameCompostFilterSock; }
            set { ChangeAndNotify(ref _esControlNameCompostFilterSock, value, nameof(ESControlNameCompostFilterSock), CalcPropertiesStringArray); }
        }

        private double _esControlLength;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double ESControlLength
        {
            get { return _esControlLength; }
            set { ChangeAndNotify(ref _esControlLength, value, nameof(ESControlLength), CalcPropertiesStringArray); }
        }

        private decimal _esControlUnitCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal ESControlUnitCost
        {
            get { return _esControlUnitCost; }
            set { ChangeAndNotify(ref _esControlUnitCost, value, nameof(ESControlUnitCost), CalcPropertiesStringArray); }
        }

        private bool _isSedimentBasins;
        /// <summary>
        ///  User specified 
        /// </summary>
        public bool IsSedimentBasins
        {
            get { return _isSedimentBasins; }
            set { ChangeAndNotify(ref _isSedimentBasins, value, nameof(IsSedimentBasins), CalcPropertiesStringArray); }
        }

        private double _sedimentBasinsQuantity;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double SedimentBasinsQuantity
        {
            get { return _sedimentBasinsQuantity; }
            set { ChangeAndNotify(ref _sedimentBasinsQuantity, value, nameof(SedimentBasinsQuantity), CalcPropertiesStringArray); }
        }

        private decimal _sedimentBasinsUnitCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal SedimentBasinsUnitCost
        {
            get { return _sedimentBasinsUnitCost; }
            set { ChangeAndNotify(ref _sedimentBasinsUnitCost, value, nameof(SedimentBasinsUnitCost), CalcPropertiesStringArray); }
        }

        private bool _isESControlOther;
        /// <summary>
        ///  User specified 
        /// </summary>
        public bool IsESControlOther
        {
            get { return _isESControlOther; }
            set { ChangeAndNotify(ref _isESControlOther, value, nameof(IsESControlOther), CalcPropertiesStringArray); }
        }

        private decimal _esControlUnitCostOther;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal ESControlUnitCostOther
        {
            get { return _esControlUnitCostOther; }
            set { ChangeAndNotify(ref _esControlUnitCostOther, value, nameof(ESControlUnitCostOther), CalcPropertiesStringArray); }
        }

        #endregion

        #region Properties - Diversion Ditches and Culverts

        private bool _isRockLinedDitch;
        /// <summary>
        ///  User specified 
        /// </summary>
        public bool IsRockLinedDitch
        {
            get { return _isRockLinedDitch; }
            set { ChangeAndNotify(ref _isRockLinedDitch, value, nameof(IsRockLinedDitch), CalcPropertiesStringArray); }
        }

        private double _rockLinedDitchLength;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double RockLinedDitchLength
        {
            get { return _rockLinedDitchLength; }
            set { ChangeAndNotify(ref _rockLinedDitchLength, value, nameof(RockLinedDitchLength), CalcPropertiesStringArray); }
        }

        private double _rockLinedDitchBottomWidth;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double RockLinedDitchBottomWidth
        {
            get { return _rockLinedDitchBottomWidth; }
            set { ChangeAndNotify(ref _rockLinedDitchBottomWidth, value, nameof(RockLinedDitchBottomWidth), CalcPropertiesStringArray); }
        }

        private double _rockLinedDitchDepth;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double RockLinedDitchDepth
        {
            get { return _rockLinedDitchDepth; }
            set { ChangeAndNotify(ref _rockLinedDitchDepth, value, nameof(RockLinedDitchDepth), CalcPropertiesStringArray); }
        }

        private double _rockLinedDitchAggregateThickness;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double RockLinedDitchAggregateThickness
        {
            get { return _rockLinedDitchAggregateThickness; }
            set { ChangeAndNotify(ref _rockLinedDitchAggregateThickness, value, nameof(RockLinedDitchAggregateThickness), CalcPropertiesStringArray); }
        }

        private double _rockLinedDitchSideSlope;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double RockLinedDitchSideSlope
        {
            get { return _rockLinedDitchSideSlope; }
            set { ChangeAndNotify(ref _rockLinedDitchSideSlope, value, nameof(RockLinedDitchSideSlope), CalcPropertiesStringArray); }
        }

        private double _rockLinedDitchGeotextileLength;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double RockLinedDitchGeotextileLength
        {
            get { return _rockLinedDitchGeotextileLength; }
            set { ChangeAndNotify(ref _rockLinedDitchGeotextileLength, value, nameof(RockLinedDitchGeotextileLength), CalcPropertiesStringArray); }
        }

        private double _rockLinedDitchAggregateBulkDensity;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double RockLinedDitchAggregateBulkDensity
        {
            get { return _rockLinedDitchAggregateBulkDensity; }
            set { ChangeAndNotify(ref _rockLinedDitchAggregateBulkDensity, value, nameof(RockLinedDitchAggregateBulkDensity), CalcPropertiesStringArray); }
        }

        private decimal _rockLinedDitchAggregateUnitCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal RockLinedDitchAggregateUnitCost
        {
            get { return _rockLinedDitchAggregateUnitCost; }
            set { ChangeAndNotify(ref _rockLinedDitchAggregateUnitCost, value, nameof(RockLinedDitchAggregateUnitCost), CalcPropertiesStringArray); }
        }

        private decimal _rockLinedDitchAggregatePlacementUnitCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal RockLinedDitchAggregatePlacementUnitCost
        {
            get { return _rockLinedDitchAggregatePlacementUnitCost; }
            set { ChangeAndNotify(ref _rockLinedDitchAggregatePlacementUnitCost, value, nameof(RockLinedDitchAggregatePlacementUnitCost), CalcPropertiesStringArray); }
        }

        private bool _isVegetatedDitch;
        /// <summary>
        ///  User specified 
        /// </summary>
        public bool IsVegetatedDitch
        {
            get { return _isVegetatedDitch; }
            set { ChangeAndNotify(ref _isVegetatedDitch, value, nameof(IsVegetatedDitch), CalcPropertiesStringArray); }
        }

        private double _vegetatedDitchLength;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double VegetatedDitchLength
        {
            get { return _vegetatedDitchLength; }
            set { ChangeAndNotify(ref _vegetatedDitchLength, value, nameof(VegetatedDitchLength), CalcPropertiesStringArray); }
        }

        private decimal _vegetatedDitchUnitCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal VegetatedDitchUnitCost
        {
            get { return _vegetatedDitchUnitCost; }
            set { ChangeAndNotify(ref _vegetatedDitchUnitCost, value, nameof(VegetatedDitchUnitCost), CalcPropertiesStringArray); }
        }

        private bool _isCulvert;
        /// <summary>
        ///  User specified 
        /// </summary>
        public bool IsCulvert
        {
            get { return _isCulvert; }
            set { ChangeAndNotify(ref _isCulvert, value, nameof(IsCulvert), CalcPropertiesStringArray); }
        }

        private double _culvertLength;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double CulvertLength
        {
            get { return _culvertLength; }
            set { ChangeAndNotify(ref _culvertLength, value, nameof(CulvertLength), CalcPropertiesStringArray); }
        }

        private decimal _culvertUnitCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal CulvertUnitCost
        {
            get { return _culvertUnitCost; }
            set { ChangeAndNotify(ref _culvertUnitCost, value, nameof(CulvertUnitCost), CalcPropertiesStringArray); }
        }
        #endregion

        #region Properties - Control Building

        /// <summary>
        ///  Radio button binding
        /// </summary>
        public enum RadioButtonsFoundationVolumeOptionsEnum
        {
            OptionEstimate,
            OptionUserSpecified,
        }

        private RadioButtonsFoundationVolumeOptionsEnum _foundationVolumeOptionsProperty;
        /// <summary>
        ///  User specified 
        /// </summary>
        public RadioButtonsFoundationVolumeOptionsEnum FoundationVolumeOptionsProperty
        {
            get { return _foundationVolumeOptionsProperty; }
            set { ChangeAndNotify(ref _foundationVolumeOptionsProperty, value, nameof(FoundationVolumeOptionsProperty), CalcPropertiesStringArray); }
        }

        private List<GeneralItem> _foundationSiteSoils;
        /// <summary>
        /// Collection 
        /// </summary>
        public List<GeneralItem> FoundationSiteSoils
        {
            get { return _foundationSiteSoils; }

            set { ChangeAndNotify(ref _foundationSiteSoils, value, nameof(FoundationSiteSoils), CalcPropertiesStringArray); }
        }

        private string _foundationSiteSoilName;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string FoundationSiteSoilName
        {
            get { return _foundationSiteSoilName; }
            set { ChangeAndNotify(ref _foundationSiteSoilName, value, nameof(FoundationSiteSoilName), CalcPropertiesStringArray); }
        }

        private string _foundationSiteSoilNamePoor;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string FoundationSiteSoilNamePoor
        {
            get { return _foundationSiteSoilNamePoor; }
            set { ChangeAndNotify(ref _foundationSiteSoilNamePoor, value, nameof(FoundationSiteSoilNamePoor), CalcPropertiesStringArray); }
        }

        private string _foundationSiteSoilNameAverage;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string FoundationSiteSoilNameAverage
        {
            get { return _foundationSiteSoilNameAverage; }
            set { ChangeAndNotify(ref _foundationSiteSoilNameAverage, value, nameof(FoundationSiteSoilNameAverage), CalcPropertiesStringArray); }
        }

        private string _foundationSiteSoilNameExcellent;
        /// <summary>
        ///  User specified 
        /// </summary>
        public string FoundationSiteSoilNameExcellent
        {
            get { return _foundationSiteSoilNameExcellent; }
            set { ChangeAndNotify(ref _foundationSiteSoilNameExcellent, value, nameof(FoundationSiteSoilNameExcellent), CalcPropertiesStringArray); }
        }

        private double _foundationSiteSoilFactor;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double FoundationSiteSoilFactor
        {
            get { return _foundationSiteSoilFactor; }
            set { ChangeAndNotify(ref _foundationSiteSoilFactor, value, nameof(FoundationSiteSoilFactor), CalcPropertiesStringArray); }
        }

        private double _foundationSiteSoilFactorPoor;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double FoundationSiteSoilFactorPoor
        {
            get { return _foundationSiteSoilFactorPoor; }
            set { ChangeAndNotify(ref _foundationSiteSoilFactorPoor, value, nameof(FoundationSiteSoilFactorPoor), CalcPropertiesStringArray); }
        }

        private double _foundationSiteSoilFactorAverage;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double FoundationSiteSoilFactorAverage
        {
            get { return _foundationSiteSoilFactorAverage; }
            set { ChangeAndNotify(ref _foundationSiteSoilFactorAverage, value, nameof(FoundationSiteSoilFactorAverage), CalcPropertiesStringArray); }
        }

        private double _foundationSiteSoilFactorExcellent;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double FoundationSiteSoilFactorExcellent
        {
            get { return _foundationSiteSoilFactorExcellent; }
            set { ChangeAndNotify(ref _foundationSiteSoilFactorExcellent, value, nameof(FoundationSiteSoilFactorExcellent), CalcPropertiesStringArray); }
        }

        private double _foundationDepth;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double FoundationDepth
        {
            get { return _foundationDepth; }
            set { ChangeAndNotify(ref _foundationDepth, value, nameof(FoundationDepth), CalcPropertiesStringArray); }
        }

        private decimal _foundationConcreteUnitCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal FoundationConcreteUnitCost
        {
            get { return _foundationConcreteUnitCost; }
            set { ChangeAndNotify(ref _foundationConcreteUnitCost, value, nameof(FoundationConcreteUnitCost), CalcPropertiesStringArray); }
        }

        private double _foundationSlabVolumeUserSpecified;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double FoundationSlabVolumeUserSpecified
        {
            get { return _foundationSlabVolumeUserSpecified; }
            set { ChangeAndNotify(ref _foundationSlabVolumeUserSpecified, value, nameof(FoundationSlabVolumeUserSpecified), CalcPropertiesStringArray); }
        }

        /// <summary>
        ///  Radio button binding
        /// </summary>
        public enum RadioButtonsControlBuildingOptionsEnum
        {
            OptionEstimate,
            OptionUserSpecified,
        }

        private RadioButtonsControlBuildingOptionsEnum _controlBuildingOptionsProperty;
        /// <summary>
        ///  User specified 
        /// </summary>
        public RadioButtonsControlBuildingOptionsEnum ControlBuildingOptionsProperty
        {
            get { return _controlBuildingOptionsProperty; }
            set { ChangeAndNotify(ref _controlBuildingOptionsProperty, value, nameof(ControlBuildingOptionsProperty), CalcPropertiesStringArray); }
        }

        private double _controlBuildingLength;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double ControlBuildingLength
        {
            get { return _controlBuildingLength; }
            set { ChangeAndNotify(ref _controlBuildingLength, value, nameof(ControlBuildingLength), CalcPropertiesStringArray); }
        }

        private double _controlBuildingWidth;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double ControlBuildingWidth
        {
            get { return _controlBuildingWidth; }
            set { ChangeAndNotify(ref _controlBuildingWidth, value, nameof(ControlBuildingWidth), CalcPropertiesStringArray); }
        }

        private decimal _controlBuildingUnitCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal ControlBuildingUnitCost
        {
            get { return _controlBuildingUnitCost; }
            set { ChangeAndNotify(ref _controlBuildingUnitCost, value, nameof(ControlBuildingUnitCost), CalcPropertiesStringArray); }
        }

        private bool _isLab;
        /// <summary>
        ///  User specified 
        /// </summary>
        public bool IsLab
        {
            get { return _isLab; }
            set { ChangeAndNotify(ref _isLab, value, nameof(IsLab), CalcPropertiesStringArray); }
        }

        private decimal _controlBuildingLabCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal ControlBuildingLabCost
        {
            get { return _controlBuildingLabCost; }
            set { ChangeAndNotify(ref _controlBuildingLabCost, value, nameof(ControlBuildingLabCost), CalcPropertiesStringArray); }
        }

        private bool _isWaterSewer;
        /// <summary>
        ///  User specified 
        /// </summary>
        public bool IsWaterSewer
        {
            get { return _isWaterSewer; }
            set { ChangeAndNotify(ref _isWaterSewer, value, nameof(IsWaterSewer), CalcPropertiesStringArray); }
        }

        private decimal _controlBuildingWaterSewerCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal ControlBuildingWaterSewerCost
        {
            get { return _controlBuildingWaterSewerCost; }
            set { ChangeAndNotify(ref _controlBuildingWaterSewerCost, value, nameof(ControlBuildingWaterSewerCost), CalcPropertiesStringArray); }
        }

        private bool _isHVAC;
        /// <summary>
        ///  User specified 
        /// </summary>
        public bool IsHVAC
        {
            get { return _isHVAC; }
            set { ChangeAndNotify(ref _isHVAC, value, nameof(IsHVAC), CalcPropertiesStringArray); }
        }

        private decimal _controlBuildingHVACCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal ControlBuildingHVACCost
        {
            get { return _controlBuildingHVACCost; }
            set { ChangeAndNotify(ref _controlBuildingHVACCost, value, nameof(ControlBuildingHVACCost), CalcPropertiesStringArray); }
        }

        private decimal _controlBuildingCostUserSpecified;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal ControlBuildingCostUserSpecified
        {
            get { return _controlBuildingCostUserSpecified; }
            set { ChangeAndNotify(ref _controlBuildingCostUserSpecified, value, nameof(ControlBuildingCostUserSpecified), CalcPropertiesStringArray); }
        }
        #endregion

        #region Properties - Electric Service / Utilities

        private double _electricLineExtensionLength;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double ElectricLineExtensionLength
        {
            get { return _electricLineExtensionLength; }
            set { ChangeAndNotify(ref _electricLineExtensionLength, value, nameof(ElectricLineExtensionLength), CalcPropertiesStringArray); }
        }

        private decimal _electricLineExtensionUnitCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal ElectricLineExtensionUnitCost
        {
            get { return _electricLineExtensionUnitCost; }
            set { ChangeAndNotify(ref _electricLineExtensionUnitCost, value, nameof(ElectricLineExtensionUnitCost), CalcPropertiesStringArray); }
        }

        private decimal _electricBackupGeneratorCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal ElectricBackupGeneratorCost
        {
            get { return _electricBackupGeneratorCost; }
            set { ChangeAndNotify(ref _electricBackupGeneratorCost, value, nameof(ElectricBackupGeneratorCost), CalcPropertiesStringArray); }
        }

        private bool _isStepdownTransformer;
        /// <summary>
        ///  User specified 
        /// </summary>
        public bool IsStepdownTransformer
        {
            get { return _isStepdownTransformer; }
            set { ChangeAndNotify(ref _isStepdownTransformer, value, nameof(IsStepdownTransformer), CalcPropertiesStringArray); }
        }

        private decimal _electricStepdownTransformerCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal ElectricStepdownTransformerCost
        {
            get { return _electricStepdownTransformerCost; }
            set { ChangeAndNotify(ref _electricStepdownTransformerCost, value, nameof(ElectricStepdownTransformerCost), CalcPropertiesStringArray); }
        }

        private decimal _electricPanelCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal ElectricPanelCost
        {
            get { return _electricPanelCost; }
            set { ChangeAndNotify(ref _electricPanelCost, value, nameof(ElectricPanelCost), CalcPropertiesStringArray); }
        }

        private double _electricWiringPercentage;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double ElectricWiringPercentage
        {
            get { return _electricWiringPercentage; }
            set { ChangeAndNotify(ref _electricWiringPercentage, value, nameof(ElectricWiringPercentage), CalcPropertiesStringArray); }
        }

        private bool _isTelecommunications;
        /// <summary>
        ///  User specified 
        /// </summary>
        public bool IsTelecommunications
        {
            get { return _isTelecommunications; }
            set { ChangeAndNotify(ref _isTelecommunications, value, nameof(IsTelecommunications), CalcPropertiesStringArray); }
        }

        private decimal _electricTelecommunicationsCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal ElectricTelecommunicationsCost
        {
            get { return _electricTelecommunicationsCost; }
            set { ChangeAndNotify(ref _electricTelecommunicationsCost, value, nameof(ElectricTelecommunicationsCost), CalcPropertiesStringArray); }
        }

        private bool _isPlantAutomation;
        /// <summary>
        ///  User specified 
        /// </summary>
        public bool IsPlantAutomation
        {
            get { return _isPlantAutomation; }
            set { ChangeAndNotify(ref _isPlantAutomation, value, nameof(IsPlantAutomation), CalcPropertiesStringArray); }
        }

        private decimal _electricPlantAutomationCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal ElectricPlantAutomationCost
        {
            get { return _electricPlantAutomationCost; }
            set { ChangeAndNotify(ref _electricPlantAutomationCost, value, nameof(ElectricPlantAutomationCost), CalcPropertiesStringArray); }
        }

        private bool _isElectricOtherUtility;
        /// <summary>
        ///  User specified 
        /// </summary>
        public bool IsElectricOtherUtility
        {
            get { return _isElectricOtherUtility; }
            set { ChangeAndNotify(ref _isElectricOtherUtility, value, nameof(IsElectricOtherUtility), CalcPropertiesStringArray); }
        }

        private decimal _electricOtherUtilityCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal ElectricOtherUtilityCost
        {
            get { return _electricOtherUtilityCost; }
            set { ChangeAndNotify(ref _electricOtherUtilityCost, value, nameof(ElectricOtherUtilityCost), CalcPropertiesStringArray); }
        }
        #endregion

        #region Properties - Fencing and Access Gates

        /// <summary>
        ///  Radio button binding
        /// </summary>
        public enum RadioButtonsFencingOptionsEnum
        {
            OptionKnown,
            OptionEstimate,
            OptionUserSpecified,
        }

        private RadioButtonsFencingOptionsEnum _fencingOptionsProperty;
        /// <summary>
        ///  User specified 
        /// </summary>
        public RadioButtonsFencingOptionsEnum FencingOptionsProperty
        {
            get { return _fencingOptionsProperty; }
            set { ChangeAndNotify(ref _fencingOptionsProperty, value, nameof(FencingOptionsProperty), CalcPropertiesStringArray); }
        }

        private double _fencingLength;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double FencingLength
        {
            get { return _fencingLength; }
            set { ChangeAndNotify(ref _fencingLength, value, nameof(FencingLength), CalcPropertiesStringArray); }
        }

        private decimal _fencingUnitCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal FencingUnitCost
        {
            get { return _fencingUnitCost; }
            set { ChangeAndNotify(ref _fencingUnitCost, value, nameof(FencingUnitCost), CalcPropertiesStringArray); }
        }

        private decimal _fencingCostUserSpecified;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal FencingCostUserSpecified
        {
            get { return _fencingCostUserSpecified; }
            set { ChangeAndNotify(ref _fencingCostUserSpecified, value, nameof(FencingCostUserSpecified), CalcPropertiesStringArray); }
        }

        private bool _isAccessGate;
        /// <summary>
        ///  User specified 
        /// </summary>
        public bool IsAccessGate
        {
            get { return _isAccessGate; }
            set { ChangeAndNotify(ref _isAccessGate, value, nameof(IsAccessGate), CalcPropertiesStringArray); }
        }

        private double _accessGateQuantity;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double AccessGateQuantity
        {
            get { return _accessGateQuantity; }
            set { ChangeAndNotify(ref _accessGateQuantity, value, nameof(AccessGateQuantity), CalcPropertiesStringArray); }
        }

        private decimal _accessGateUnitCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal AccessGateUnitCost
        {
            get { return _accessGateUnitCost; }
            set { ChangeAndNotify(ref _accessGateUnitCost, value, nameof(AccessGateUnitCost), CalcPropertiesStringArray); }
        }
        #endregion

        #region Properties - Clearing and Grubbing

        private decimal _clearAndGrubUnitCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal ClearAndGrubUnitCost
        {
            get { return _clearAndGrubUnitCost; }
            set { ChangeAndNotify(ref _clearAndGrubUnitCost, value, nameof(ClearAndGrubUnitCost), CalcPropertiesStringArray); }
        }

        private double _clearAndGrubRevegetationPercentage;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double ClearAndGrubRevegetationPercentage
        {
            get { return _clearAndGrubRevegetationPercentage; }
            set { ChangeAndNotify(ref _clearAndGrubRevegetationPercentage, value, nameof(ClearAndGrubRevegetationPercentage), CalcPropertiesStringArray); }
        }

        private decimal _revegetationUnitCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal RevegetationUnitCost
        {
            get { return _revegetationUnitCost; }
            set { ChangeAndNotify(ref _revegetationUnitCost, value, nameof(RevegetationUnitCost), CalcPropertiesStringArray); }
        }
        #endregion

        #region Properties - Engineering / Permitting, Surveying, Construction Inspection

        private bool _isEngineeringPermitting;
        /// <summary>
        ///  User specified 
        /// </summary>
        public bool IsEngineeringPermitting
        {
            get { return _isEngineeringPermitting; }
            set { ChangeAndNotify(ref _isEngineeringPermitting, value, nameof(IsEngineeringPermitting), CalcPropertiesStringArray); }
        }

        /// <summary>
        ///  Radio button binding
        /// </summary>
        public enum RadioButtonsEngineeringPermittingOptionsEnum
        {
            OptionEstimate,
            OptionUserSpecified,
        }

        private RadioButtonsEngineeringPermittingOptionsEnum _engineeringPermittingOptionsProperty;
        /// <summary>
        ///  User specified 
        /// </summary>
        public RadioButtonsEngineeringPermittingOptionsEnum EngineeringPermittingOptionsProperty
        {
            get { return _engineeringPermittingOptionsProperty; }
            set { ChangeAndNotify(ref _engineeringPermittingOptionsProperty, value, nameof(EngineeringPermittingOptionsProperty), CalcPropertiesStringArray); }
        }

        private double _engineeringPermittingPercentage;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double EngineeringPermittingPercentage
        {
            get { return _engineeringPermittingPercentage; }
            set { ChangeAndNotify(ref _engineeringPermittingPercentage, value, nameof(EngineeringPermittingPercentage), CalcPropertiesStringArray); }
        }

        private decimal _engineeringPermittingCostUserSpecified;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal EngineeringPermittingCostUserSpecified
        {
            get { return _engineeringPermittingCostUserSpecified; }
            set { ChangeAndNotify(ref _engineeringPermittingCostUserSpecified, value, nameof(EngineeringPermittingCostUserSpecified), CalcPropertiesStringArray); }
        }

        private bool _isSurveying;
        /// <summary>
        ///  User specified 
        /// </summary>
        public bool IsSurveying
        {
            get { return _isSurveying; }
            set { ChangeAndNotify(ref _isSurveying, value, nameof(IsSurveying), CalcPropertiesStringArray); }
        }

        /// <summary>
        ///  Radio button binding
        /// </summary>
        public enum RadioButtonsSurveyingOptionsEnum
        {
            OptionEstimate,
            OptionUserSpecified,
        }

        private RadioButtonsSurveyingOptionsEnum _surveyingOptionsProperty;
        /// <summary>
        ///  User specified 
        /// </summary>
        public RadioButtonsSurveyingOptionsEnum SurveyingOptionsProperty
        {
            get { return _surveyingOptionsProperty; }
            set { ChangeAndNotify(ref _surveyingOptionsProperty, value, nameof(SurveyingOptionsProperty), CalcPropertiesStringArray); }
        }

        private double _surveyingPercentage;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double SurveyingPercentage
        {
            get { return _surveyingPercentage; }
            set { ChangeAndNotify(ref _surveyingPercentage, value, nameof(SurveyingPercentage), CalcPropertiesStringArray); }
        }

        private decimal _surveyingCostUserSpecified;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal SurveyingCostUserSpecified
        {
            get { return _surveyingCostUserSpecified; }
            set { ChangeAndNotify(ref _surveyingCostUserSpecified, value, nameof(SurveyingCostUserSpecified), CalcPropertiesStringArray); }
        }

        private bool _isConstructionInspection;
        /// <summary>
        ///  User specified 
        /// </summary>
        public bool IsConstructionInspection
        {
            get { return _isConstructionInspection; }
            set { ChangeAndNotify(ref _isConstructionInspection, value, nameof(IsConstructionInspection), CalcPropertiesStringArray); }
        }

        /// <summary>
        ///  Radio button binding
        /// </summary>
        public enum RadioButtonsConstructionInspectionOptionsEnum
        {
            OptionEstimate,
            OptionUserSpecified,
        }

        private RadioButtonsConstructionInspectionOptionsEnum _inspectionOptionsProperty;
        /// <summary>
        ///  User specified 
        /// </summary>
        public RadioButtonsConstructionInspectionOptionsEnum ConstructionInspectionOptionsProperty
        {
            get { return _inspectionOptionsProperty; }
            set { ChangeAndNotify(ref _inspectionOptionsProperty, value, nameof(ConstructionInspectionOptionsProperty), CalcPropertiesStringArray); }
        }


        private double _constructionInspectionPercentage;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double ConstructionInspectionPercentage
        {
            get { return _constructionInspectionPercentage; }
            set { ChangeAndNotify(ref _constructionInspectionPercentage, value, nameof(ConstructionInspectionPercentage), CalcPropertiesStringArray); }
        }

        private decimal _constructionInspectionCostUserSpecified;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal ConstructionInspectionCostUserSpecified
        {
            get { return _constructionInspectionCostUserSpecified; }
            set { ChangeAndNotify(ref _constructionInspectionCostUserSpecified, value, nameof(ConstructionInspectionCostUserSpecified), CalcPropertiesStringArray); }
        }
        #endregion

        #region Properties - Contingency

        /// <summary>
        ///  Radio button binding
        /// </summary>
        public enum RadioButtonsContingencyOptionsEnum
        {
            OptionEstimate,
            OptionUserSpecified,
        }

        private RadioButtonsContingencyOptionsEnum _contingencyOptionsProperty;
        /// <summary>
        ///  User specified 
        /// </summary>
        public RadioButtonsContingencyOptionsEnum ContingencyOptionsProperty
        {
            get { return _contingencyOptionsProperty; }
            set { ChangeAndNotify(ref _contingencyOptionsProperty, value, nameof(ContingencyOptionsProperty), CalcPropertiesStringArray); }
        }

        private double _contingencyPercentage;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double ContingencyPercentage
        {
            get { return _contingencyPercentage; }
            set { ChangeAndNotify(ref _contingencyPercentage, value, nameof(ContingencyPercentage), CalcPropertiesStringArray); }
        }

        private decimal _contingencyCostUserSpecified;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal ContingencyCostUserSpecified
        {
            get { return _contingencyCostUserSpecified; }
            set { ChangeAndNotify(ref _contingencyCostUserSpecified, value, nameof(ContingencyCostUserSpecified), CalcPropertiesStringArray); }
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

        private decimal _annualCostPropertyTax;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal AnnualCostPropertyTax
        {
            get { return _annualCostPropertyTax; }
            set { ChangeAndNotify(ref _annualCostPropertyTax, value, nameof(AnnualCostPropertyTax), CalcPropertiesStringArray); }
        }

        private decimal _annualCostUtility;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal AnnualCostUtility
        {
            get { return _annualCostUtility; }
            set { ChangeAndNotify(ref _annualCostUtility, value, nameof(AnnualCostUtility), CalcPropertiesStringArray); }
        }

        private bool _isAnnualCostOperationOther;
        /// <summary>
        ///  User specified 
        /// </summary>
        public bool IsAnnualCostOperationOther
        {
            get { return _isAnnualCostOperationOther; }
            set { ChangeAndNotify(ref _isAnnualCostOperationOther, value, nameof(IsAnnualCostOperationOther), CalcPropertiesStringArray); }
        }

        private decimal _annualCostOperationOther;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal AnnualCostOperationOther
        {
            get { return _annualCostOperationOther; }
            set { ChangeAndNotify(ref _annualCostOperationOther, value, nameof(AnnualCostOperationOther), CalcPropertiesStringArray); }
        }

        private double _buildingMaintenanceHoursPerYear;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double BuildingMaintenanceHoursPerYear
        {
            get { return _buildingMaintenanceHoursPerYear; }
            set { ChangeAndNotify(ref _buildingMaintenanceHoursPerYear, value, nameof(BuildingMaintenanceHoursPerYear), CalcPropertiesStringArray); }
        }

        private decimal _parkingAreaMaintenanceUnitCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal ParkingAreaMaintenanceUnitCost
        {
            get { return _parkingAreaMaintenanceUnitCost; }
            set { ChangeAndNotify(ref _parkingAreaMaintenanceUnitCost, value, nameof(ParkingAreaMaintenanceUnitCost), CalcPropertiesStringArray); }
        }

        private double _roadMaintenanceHoursPerYear;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double RoadMaintenanceHoursPerYear
        {
            get { return _roadMaintenanceHoursPerYear; }
            set { ChangeAndNotify(ref _roadMaintenanceHoursPerYear, value, nameof(RoadMaintenanceHoursPerYear), CalcPropertiesStringArray); }
        }

        private double _mowingHoursPerYear;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double MowingHoursPerYear
        {
            get { return _mowingHoursPerYear; }
            set { ChangeAndNotify(ref _mowingHoursPerYear, value, nameof(MowingHoursPerYear), CalcPropertiesStringArray); }
        }

        private double _snowRemovalHoursPerYear;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double SnowRemovalHoursPerYear
        {
            get { return _snowRemovalHoursPerYear; }
            set { ChangeAndNotify(ref _snowRemovalHoursPerYear, value, nameof(SnowRemovalHoursPerYear), CalcPropertiesStringArray); }
        }

        private double _ditchCulvertCleaningHoursPerYear;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double DitchCulvertCleaningHoursPerYear
        {
            get { return _ditchCulvertCleaningHoursPerYear; }
            set { ChangeAndNotify(ref _ditchCulvertCleaningHoursPerYear, value, nameof(DitchCulvertCleaningHoursPerYear), CalcPropertiesStringArray); }
        }

        private double _leafRemovalHoursPerYear;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double LeafRemovalHoursPerYear
        {
            get { return _leafRemovalHoursPerYear; }
            set { ChangeAndNotify(ref _leafRemovalHoursPerYear, value, nameof(LeafRemovalHoursPerYear), CalcPropertiesStringArray); }
        }

        private double _siteInspectionsHoursPerYear;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double SiteInspectionsHoursPerYear
        {
            get { return _siteInspectionsHoursPerYear; }
            set { ChangeAndNotify(ref _siteInspectionsHoursPerYear, value, nameof(SiteInspectionsHoursPerYear), CalcPropertiesStringArray); }
        }

        private double _annualOtherHoursPerYear;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double AnnualOtherHoursPerYear
        {
            get { return _annualOtherHoursPerYear; }
            set { ChangeAndNotify(ref _annualOtherHoursPerYear, value, nameof(AnnualOtherHoursPerYear), CalcPropertiesStringArray); }
        }
        #endregion

        #region Properties - Shared Properies from Main User Interface

        private double _clearAndGrubAreaPassiveModules;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double ClearAndGrubAreaPassiveModules
        {
            get
            {
                double clearAndGrubAreaPassiveModules = new double();

                foreach (var data in SharedDataCollection)
                {
                    clearAndGrubAreaPassiveModules = (double)data.Data["ClearAndGrubAreaPassive_SharedData"];
                }

                _clearAndGrubAreaPassiveModules = clearAndGrubAreaPassiveModules;

                return _clearAndGrubAreaPassiveModules;
            }

            set { ChangeAndNotify(ref _clearAndGrubAreaPassiveModules, value, nameof(ClearAndGrubAreaPassiveModules), CalcPropertiesStringArray); }
        }

        private double _calcClearAndGrubAreaPassiveModules;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcClearAndGrubAreaPassiveModules
        {
            get { return ClearAndGrubAreaPassiveModules; }
            set { ChangeAndNotify(ref _calcClearAndGrubAreaPassiveModules, value); }
        }

        private double _foundationAreaPassiveAndActiveModules;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double FoundationAreaPassiveAndActiveModules
        {
            get
            {
                double foundationAreaPassiveAndActiveModules = new double();

                foreach (var data in SharedDataCollection)
                {
                    foundationAreaPassiveAndActiveModules = (double)data.Data["FoundationAreaPassiveAndActive_SharedData"];
                }

                _foundationAreaPassiveAndActiveModules = foundationAreaPassiveAndActiveModules;

                return _foundationAreaPassiveAndActiveModules;
            }
            set { ChangeAndNotify(ref _foundationAreaPassiveAndActiveModules, value, nameof(FoundationAreaPassiveAndActiveModules), CalcPropertiesStringArray); }
        }

        private double _calcFoundationAreaPassiveAndActiveModules;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcFoundationAreaPassiveAndActiveModules
        {
            get { return FoundationAreaPassiveAndActiveModules; }
            set { ChangeAndNotify(ref _calcFoundationAreaPassiveAndActiveModules, value); }
        }

        private double _foundationAreaTimesDepthPassiveAndActiveModules;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double FoundationAreaTimesDepthPassiveAndActiveModules
        {
            get
            {
                double foundationAreaTimesDepthPassiveAndActiveModules = new double();

                foreach (var data in SharedDataCollection)
                {
                    foundationAreaTimesDepthPassiveAndActiveModules = (double)data.Data["FoundationAreaTimesDepthPassiveAndActive_SharedData"];
                }

                _foundationAreaTimesDepthPassiveAndActiveModules = foundationAreaTimesDepthPassiveAndActiveModules;

                return _foundationAreaTimesDepthPassiveAndActiveModules;
            }
            set { ChangeAndNotify(ref _foundationAreaTimesDepthPassiveAndActiveModules, value, nameof(FoundationAreaTimesDepthPassiveAndActiveModules), CalcPropertiesStringArray); }
        }

        private double _calcFoundationAreaTimesDepthPassiveAndActiveModules;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcFoundationAreaTimesDepthPassiveAndActiveModules
        {
            get { return FoundationAreaTimesDepthPassiveAndActiveModules; }
            set { ChangeAndNotify(ref _calcFoundationAreaTimesDepthPassiveAndActiveModules, value); }
        }

        private decimal _capitalCostPassiveModules;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal CapitalCostPassiveModules
        {
            get
            {
                decimal capitalCostPassiveModules = new decimal();

                foreach (var data in SharedDataCollection)
                {
                    capitalCostPassiveModules = (decimal)data.Data["CapitalCostPassive_SharedData"];
                }

                _capitalCostPassiveModules = capitalCostPassiveModules;

                return _capitalCostPassiveModules;
            }
            set { ChangeAndNotify(ref _capitalCostPassiveModules, value, nameof(CapitalCostPassiveModules), CalcPropertiesStringArray); }
        }

        private decimal _calcCapitalCostPassiveModules;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcCapitalCostPassiveModules
        {
            get { return CapitalCostPassiveModules; }
            set { ChangeAndNotify(ref _calcCapitalCostPassiveModules, value); }
        }

        private decimal _capitalCostActiveModules;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal CapitalCostActiveModules
        {
            get
            {
                decimal capitalCostActiveModules = new decimal();

                foreach (var data in SharedDataCollection)
                {
                    capitalCostActiveModules = (decimal)data.Data["CapitalCostActive_SharedData"];
                }
                _capitalCostActiveModules = capitalCostActiveModules;

                return _capitalCostActiveModules;
            }
            set { ChangeAndNotify(ref _capitalCostActiveModules, value, nameof(CapitalCostActiveModules), CalcPropertiesStringArray); }
        }

        private decimal _calcCapitalCostActiveModules;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcCapitalCostActiveModules
        {
            get { return CapitalCostActiveModules; }
            set { ChangeAndNotify(ref _calcCapitalCostActiveModules, value); }
        }

        private decimal _capitalCostPassiveAndActiveModules;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal CapitalCostPassiveAndActiveModules
        {
            get
            {
                _capitalCostPassiveAndActiveModules = CapitalCostPassiveModules + CapitalCostActiveModules;

                return _capitalCostPassiveAndActiveModules;
            }
            set { ChangeAndNotify(ref _capitalCostPassiveAndActiveModules, value, nameof(CapitalCostPassiveAndActiveModules), CalcPropertiesStringArray); }
        }

        private decimal _calcCapitalCostPassiveAndActiveModules;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcCapitalCostPassiveAndActiveModules
        {
            get { return CapitalCostPassiveAndActiveModules; }
            set { ChangeAndNotify(ref _calcCapitalCostPassiveAndActiveModules, value); }
        }
        #endregion

        #region Properties - Sizing Summary

        private double _calcAccessRoadSurfaceAreaSquareFeet;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcAccessRoadSurfaceAreaSquareFeet
        {
            get { return SiteDevelopmentCalculations.CalcAccessRoadSurfaceAreaSquareFeet(AccessRoadLength, AccessRoadWidth); }
            set { ChangeAndNotify(ref _calcAccessRoadSurfaceAreaSquareFeet, value); }
        }

        private double _calcAccessRoadSurfaceAreaSquareYards;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcAccessRoadSurfaceAreaSquareYards
        {
            get { return SiteDevelopmentCalculations.CalcAccessRoadSurfaceAreaSquareYards(AccessRoadLength, AccessRoadWidth); }
            set { ChangeAndNotify(ref _calcAccessRoadSurfaceAreaSquareYards, value); }
        }

        private double _calcAccessRoadGeotextileArea;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcAccessRoadGeotextileArea
        {
            get
            {
                if (IsAccessRoadGeotextile)
                {
                    _calcAccessRoadGeotextileArea = SiteDevelopmentCalculations.CalcAccessRoadGeotextileArea(AccessRoadGeotextileLength, AccessRoadWidth);
                }
                else
                {
                    _calcAccessRoadGeotextileArea = 0;
                }
                return _calcAccessRoadGeotextileArea;
            }
            set { ChangeAndNotify(ref _calcAccessRoadGeotextileArea, value); }
        }

        private double _calcParkingSurfaceAreaSquareYards;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcParkingSurfaceAreaSquareYards
        {
            get {  return SiteDevelopmentCalculations.CalcParkingSurfaceAreaSquareYards(ParkingSpacesQuantity, AverageParkingSpaceArea, DeliveryTruckTurnaroundArea); }
            set { ChangeAndNotify(ref _calcParkingSurfaceAreaSquareYards, value); }
        }

        private double _calcParkingSurfaceAreaSquareFeet;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcParkingSurfaceAreaSquareFeet
        {
            get { return SiteDevelopmentCalculations.CalcParkingSurfaceAreaSquareFeet(ParkingSpacesQuantity, AverageParkingSpaceArea, DeliveryTruckTurnaroundArea); }
            set { ChangeAndNotify(ref _calcParkingSurfaceAreaSquareFeet, value); }
        }

        private double _calcAccessRoadCutFillVolumeCubicYards;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcAccessRoadCutFillVolumeCubicYards
        {
            get { return SiteDevelopmentCalculations.CalcAccessRoadCutFillVolumeCubicYards(CutFillSlope, AverageSiteSlope, AccessRoadWidth, AccessRoadLength);}
            set { ChangeAndNotify(ref _calcAccessRoadCutFillVolumeCubicYards, value); }
        }

        private double _calcParkingAreaWidth;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcParkingAreaWidth
        {
            get { return SiteDevelopmentCalculations.CalcParkingAreaWidth(CalcParkingSurfaceAreaSquareFeet); }
            set { ChangeAndNotify(ref _calcParkingAreaWidth, value); }
        }

        private double _calcParkingAreaLength;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcParkingAreaLength
        {
            get { return SiteDevelopmentCalculations.CalcParkingAreaLength(CalcParkingAreaWidth); }
            set { ChangeAndNotify(ref _calcParkingAreaLength, value); }
        }

        private double _calcParkingAreaCutFillVolumeCubicYards;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcParkingAreaCutFillVolumeCubicYards
        {
            get { return SiteDevelopmentCalculations.CalcParkingAreaCutFillVolumeCubicYards(CutFillSlope, AverageSiteSlope, CalcParkingAreaWidth, CalcParkingAreaLength); }
            set { ChangeAndNotify(ref _calcParkingAreaCutFillVolumeCubicYards, value); }
        }

        private double _calcClearAndGrubArea;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcClearAndGrubArea
        {
            get { return SiteDevelopmentCalculations.CalcClearAndGrubArea(CalcAccessRoadSurfaceAreaSquareFeet, CalcParkingSurfaceAreaSquareFeet, CalcControlBuildingArea); }
            set { ChangeAndNotify(ref _calcClearAndGrubArea, value); }
        }

        private double _calcClearAndGrubAreaTotal;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcClearAndGrubAreaTotal
        {
            get { return SiteDevelopmentCalculations.CalcClearAndGrubAreaTotal(CalcClearAndGrubArea, ClearAndGrubAreaPassiveModules); }
            set { ChangeAndNotify(ref _calcClearAndGrubAreaTotal, value); }
        }

        private double _calcLandAreaAcres;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcLandAreaAcres
        {
            get
            {
                switch (LandQuantityOptionsProperty)
                {
                    case RadioButtonsLandQuantityOptionsEnum.OptionEstimate:
                        _calcLandAreaAcres = SiteDevelopmentCalculations.CalcLandAreaAcres(CalcClearAndGrubAreaTotal);
                        break;
                    case RadioButtonsLandQuantityOptionsEnum.OptionUserSpecified:
                        _calcLandAreaAcres = SiteDevelopmentCalculations.CalcLandAreaAcres(LandQuantityUserSpecified);
                        break;
                    default:
                        break;
                }
                return _calcLandAreaAcres;
            }
            set { ChangeAndNotify(ref _calcLandAreaAcres, value); }
        }

        private double _calcRockLinedDitchCrossSectionalArea;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcRockLinedDitchCrossSectionalArea
        {
            get { return SiteDevelopmentCalculations.CalcRockLinedDitchCrossSectionalArea(RockLinedDitchAggregateThickness, RockLinedDitchDepth, RockLinedDitchBottomWidth, RockLinedDitchSideSlope); }
            set { ChangeAndNotify(ref _calcRockLinedDitchCrossSectionalArea, value); }
        }

        private double _calcRockLinedDitchVolume;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcRockLinedDitchVolume
        {
            get { return SiteDevelopmentCalculations.CalcRockLinedDitchVolume(CalcRockLinedDitchCrossSectionalArea, RockLinedDitchLength); }
            set { ChangeAndNotify(ref _calcRockLinedDitchVolume, value); }
        }

        private double _calcRockLinedDitchWeight;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcRockLinedDitchWeight
        {
            get { return SiteDevelopmentCalculations.CalcRockLinedDitchWeight(CalcRockLinedDitchVolume, RockLinedDitchAggregateBulkDensity); }
            set { ChangeAndNotify(ref _calcRockLinedDitchWeight, value); }
        }

        private double _calcRockLinedDitchGeotextileQuantity;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcRockLinedDitchGeotextileQuantity
        {
            get { return SiteDevelopmentCalculations.CalcRockLinedDitchGeotextileQuantity(RockLinedDitchGeotextileLength, RockLinedDitchAggregateThickness, RockLinedDitchBottomWidth, RockLinedDitchSideSlope); }
            set { ChangeAndNotify(ref _calcRockLinedDitchGeotextileQuantity, value); }
        }

        private double _calcControlBuildingArea;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcControlBuildingArea
        {
            get { return SiteDevelopmentCalculations.CalcControlBuildingArea(ControlBuildingLength, ControlBuildingWidth); }
            set { ChangeAndNotify(ref _calcControlBuildingArea, value); }
        }

        private double _calcFoundationVolume;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcFoundationVolume
        {
            get { return SiteDevelopmentCalculations.CalcFoundationVolume(CalcControlBuildingArea, FoundationDepth); }
            set { ChangeAndNotify(ref _calcFoundationVolume, value); }
        }

        private double _calcControlBuildingFooterVolume;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcControlBuildingFooterVolume
        {
            get { return SiteDevelopmentCalculations.CalcControlBuildingFooterVolume(ControlBuildingLength, ControlBuildingWidth, FoundationSiteSoilFactor); }
            set { ChangeAndNotify(ref _calcControlBuildingFooterVolume, value); }
        }

        private double _calcFoundationSlabVolume;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcFoundationSlabVolume
        {
            get
            {
                switch (FoundationVolumeOptionsProperty)
                {
                    case RadioButtonsFoundationVolumeOptionsEnum.OptionEstimate:
                        _calcFoundationSlabVolume = SiteDevelopmentCalculations.CalcFoundationSlabVolume(CalcFoundationVolume, CalcControlBuildingFooterVolume);
                        break;
                    case RadioButtonsFoundationVolumeOptionsEnum.OptionUserSpecified:
                        _calcFoundationSlabVolume = FoundationSlabVolumeUserSpecified;
                        break;
                    default:
                        break;
                }
                return _calcFoundationSlabVolume;
            }
            set { ChangeAndNotify(ref _calcFoundationSlabVolume, value); }
        }

        private double _calcControlBuildingCutFillVolumeCubicYards;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcControlBuildingCutFillVolumeCubicYards
        {
            get { return SiteDevelopmentCalculations.CalcControlBuildingCutFillVolumeCubicYards(CutFillSlope, AverageSiteSlope, ControlBuildingWidth, ControlBuildingLength); }
            set { ChangeAndNotify(ref _calcControlBuildingCutFillVolumeCubicYards, value); }
        }

        private double _calcCutFillVolumeCubicYardsTotal;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcCutFillVolumeCubicYardsTotal
        {
            get { return SiteDevelopmentCalculations.CalcCutFillVolumeCubicYardsTotal(CalcAccessRoadCutFillVolumeCubicYards, CalcParkingAreaCutFillVolumeCubicYards, CalcControlBuildingCutFillVolumeCubicYards); }
            set { ChangeAndNotify(ref _calcCutFillVolumeCubicYardsTotal, value); }
        }

        private double _calcFoundationOverExcavationVolumeTotal;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcFoundationOverExcavationVolumeTotal
        {
            get { return SiteDevelopmentCalculations.CalcFoundationOverExcavationVolumeTotal(FoundationAreaPassiveAndActiveModules, CalcControlBuildingFooterVolume, FoundationSiteSoilFactor); }
            set { ChangeAndNotify(ref _calcFoundationOverExcavationVolumeTotal, value); }
        }

        private double _calcFoundationExcavationVolumeTotal;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcFoundationExcavationVolumeTotal
        {
            get { return SiteDevelopmentCalculations.CalcFoundationExcavationVolumeTotal(FoundationAreaTimesDepthPassiveAndActiveModules, CalcFoundationVolume); }
            set { ChangeAndNotify(ref _calcFoundationExcavationVolumeTotal, value); }
        }

        private double _calcFencingPerimeter;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcFencingPerimeter
        {
            get { return SiteDevelopmentCalculations.CalcFencingPerimeter(CalcClearAndGrubAreaTotal); }
            set { ChangeAndNotify(ref _calcFencingPerimeter, value); }
        }

        private double _calcFencingQuantity;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcFencingQuantity
        {
            get
            {
                switch (FencingOptionsProperty)
                {
                    case RadioButtonsFencingOptionsEnum.OptionKnown:
                        _calcFencingQuantity = FencingLength;
                        break;
                    case RadioButtonsFencingOptionsEnum.OptionEstimate:
                        _calcFencingQuantity = CalcFencingPerimeter;
                        break;
                    case RadioButtonsFencingOptionsEnum.OptionUserSpecified:
                        _calcFencingQuantity = 0;
                        break;
                    default:
                        break;
                }
                return _calcFencingQuantity;
            }
            set { ChangeAndNotify(ref _calcFencingQuantity, value); }
        }
        #endregion

        #region Properties - Capital Costs

        private decimal _calcAccessRoadGeotextileCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcAccessRoadGeotextileCost
        {
            get { return SiteDevelopmentCalculations.CalcAccessRoadGeotextileCost(CalcAccessRoadGeotextileArea, GeotextileUnitCost); }
            set { ChangeAndNotify(ref _calcAccessRoadGeotextileCost, value); }
        }

        private decimal _calcAccessRoadMaterialCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcAccessRoadMaterialCost
        {
            get { return SiteDevelopmentCalculations.CalcAccessRoadMaterialCost(CalcAccessRoadSurfaceAreaSquareYards, RoadMaterialUnitCost, RoadMaterialUnitCostSubbase); }
            set { ChangeAndNotify(ref _calcAccessRoadMaterialCost, value); }
        }

        private decimal _calcAccessRoadCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcAccessRoadCost
        {
            get
            {
                if (IsAccessRoad)
                {
                    _calcAccessRoadCost = SiteDevelopmentCalculations.CalcAccessRoadCost(CalcAccessRoadGeotextileCost, CalcAccessRoadMaterialCost);
                }
                else
                {
                    _calcAccessRoadCost = 0m;
                }
                return _calcAccessRoadCost;
            }
            set { ChangeAndNotify(ref _calcAccessRoadCost, value); }
        }

        private decimal _calcParkingAreaGeotextileCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcParkingAreaGeotextileCost
        {
            get
            {
                if (IsParkingArea)
                {
                    _calcParkingAreaGeotextileCost = SiteDevelopmentCalculations.CalcParkingAreaGeotextileCost(CalcParkingSurfaceAreaSquareYards, GeotextileUnitCost);
                }
                else
                {
                    _calcParkingAreaGeotextileCost = 0m;
                }
                return _calcParkingAreaGeotextileCost;
            }
            set { ChangeAndNotify(ref _calcParkingAreaGeotextileCost, value); }
        }

        private decimal _calcParkingAreaMaterialCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcParkingAreaMaterialCost
        {
            get
            {
                if (IsParkingArea)
                {
                    _calcParkingAreaMaterialCost = SiteDevelopmentCalculations.CalcParkingAreaMaterialCost(CalcParkingSurfaceAreaSquareYards, ParkingMaterialUnitCost, RoadMaterialUnitCostSubbase);
                }
                else
                {
                    _calcParkingAreaMaterialCost = 0m;
                }
                return _calcParkingAreaMaterialCost;
            }
            set { ChangeAndNotify(ref _calcParkingAreaMaterialCost, value); }
        }

        private decimal _calcParkingAreaCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcParkingAreaCost
        {
            get
            {
                if (IsParkingArea)
                {
                    _calcParkingAreaCost = SiteDevelopmentCalculations.CalcParkingAreaCost(ParkingLotAccessoriesCost, CalcParkingAreaGeotextileCost, CalcParkingAreaMaterialCost);
                }
                else
                {
                    _calcParkingAreaCost = 0m;
                }
                return _calcParkingAreaCost;
            }
            set { ChangeAndNotify(ref _calcParkingAreaCost, value); }
        }

        private decimal _calcLandPurchaseCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcLandPurchaseCost
        {
            get
            {
                switch (LandCostOptionsProperty)
                {
                    case RadioButtonsLandCostOptionsEnum.OptionPurchase:
                        _calcLandPurchaseCost = SiteDevelopmentCalculations.CalcLandPurchaseCost(CalcLandAreaAcres, LandPurchaseUnitCost, LandPurchaseClosingCost);
                        break;
                    case RadioButtonsLandCostOptionsEnum.OptionLease:
                        _calcLandPurchaseCost = 0m;
                        break;
                    default:
                        break;
                }
                return _calcLandPurchaseCost;
            }
            set { ChangeAndNotify(ref _calcLandPurchaseCost, value); }
        }

        private decimal _calcESControlEstimatedCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcESControlEstimatedCost
        {
            get { return SiteDevelopmentCalculations.CalcESControlEstimatedCost(CalcClearAndGrubAreaTotal, ESControlUnitCostEstimate); }
            set { ChangeAndNotify(ref _calcESControlEstimatedCost, value); }
        }

        private decimal _calcESControlUserSpecifiedSiltFenceCompostFilterSockCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcESControlUserSpecifiedSiltFenceCompostFilterSockCost
        {
            get
            {
                if (IsESControlSiltFenceCompostFilterSock)
                {
                    _calcESControlUserSpecifiedSiltFenceCompostFilterSockCost = SiteDevelopmentCalculations.CalcESControlUserSpecifiedSiltFenceCompostFilterSockCost(ESControlLength, ESControlUnitCost);
                }
                else
                {
                    _calcESControlUserSpecifiedSiltFenceCompostFilterSockCost = 0m;
                }
                return _calcESControlUserSpecifiedSiltFenceCompostFilterSockCost;
            }
            set { ChangeAndNotify(ref _calcESControlUserSpecifiedSiltFenceCompostFilterSockCost, value); }
        }

        private decimal _calcESControlUserSpecifiedSedimentBasinsCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcESControlUserSpecifiedSedimentBasinsCost
        {
            get
            {
                if (IsSedimentBasins)
                {
                    _calcESControlUserSpecifiedSedimentBasinsCost = SiteDevelopmentCalculations.CalcESControlUserSpecifiedSedimentBasinsCost(SedimentBasinsQuantity, SedimentBasinsUnitCost);
                }
                else
                {
                    _calcESControlUserSpecifiedSedimentBasinsCost = 0m;
                }
                return _calcESControlUserSpecifiedSedimentBasinsCost;
            }
            set { ChangeAndNotify(ref _calcESControlUserSpecifiedSedimentBasinsCost, value); }
        }

        private decimal _calcESControlUserSpecifiedOtherCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcESControlUserSpecifiedOtherCost
        {
            get
            {
                if (IsESControlOther)
                {
                    _calcESControlUserSpecifiedOtherCost = SiteDevelopmentCalculations.CalcESControlUserSpecifiedOtherCost(ESControlUnitCostOther);
                }
                else
                {
                    _calcESControlUserSpecifiedOtherCost = 0m;
                }
                return _calcESControlUserSpecifiedOtherCost;
            }
            set { ChangeAndNotify(ref _calcESControlUserSpecifiedOtherCost, value); }
        }

        private decimal _calcESControlUserSpecifiedCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcESControlUserSpecifiedCost
        {
            get { return SiteDevelopmentCalculations.CalcESControlUserSpecifiedCost(CalcESControlUserSpecifiedSiltFenceCompostFilterSockCost, CalcESControlUserSpecifiedSedimentBasinsCost, CalcESControlUserSpecifiedOtherCost); }
            set { ChangeAndNotify(ref _calcESControlUserSpecifiedCost, value); }
        }

        private decimal _calcESControlCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcESControlCost
        {
            get
            {
                switch (ESControlOptionsProperty)
                {
                    case RadioButtonsESControlOptionsEnum.OptionEstimate:
                        _calcESControlCost = CalcESControlEstimatedCost;
                        break;
                    case RadioButtonsESControlOptionsEnum.OptionUserSpecified:
                        _calcESControlCost = CalcESControlUserSpecifiedCost;
                        break;
                    default:
                        break;
                }
                return _calcESControlCost;
            }
            set { ChangeAndNotify(ref _calcESControlCost, value); }
        }

        private decimal _calcRockLinedDitchPlacementCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRockLinedDitchPlacementCost
        {
            get
            {
                if (IsRockLinedDitch)
                {
                    _calcRockLinedDitchPlacementCost = SiteDevelopmentCalculations.CalcRockLinedDitchPlacementCost(CalcRockLinedDitchVolume, RockLinedDitchAggregatePlacementUnitCost);
                }
                else
                {
                    _calcRockLinedDitchPlacementCost = 0m;
                }
                return _calcRockLinedDitchPlacementCost;
            }
            set { ChangeAndNotify(ref _calcRockLinedDitchPlacementCost, value); }
        }

        private decimal _calcRockLinedDitchMaterialCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRockLinedDitchMaterialCost
        {
            get
            {
                if (IsRockLinedDitch)
                {
                    _calcRockLinedDitchMaterialCost = SiteDevelopmentCalculations.CalcRockLinedDitchMaterialCost(CalcRockLinedDitchWeight, RockLinedDitchAggregateUnitCost);
                }
                else
                {
                    _calcRockLinedDitchMaterialCost = 0m;
                }
                return _calcRockLinedDitchMaterialCost;
            }
            set { ChangeAndNotify(ref _calcRockLinedDitchMaterialCost, value); }
        }

        private decimal _calcRockLinedDitchGeotextileCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRockLinedDitchGeotextileCost
        {
            get
            {
                if (IsRockLinedDitch)
                {
                    _calcRockLinedDitchGeotextileCost = SiteDevelopmentCalculations.CalcRockLinedDitchGeotextileCost(CalcRockLinedDitchGeotextileQuantity, GeotextileUnitCost);
                }
                else
                {
                    _calcRockLinedDitchGeotextileCost = 0m;
                }
                return _calcRockLinedDitchGeotextileCost;
            }
            set { ChangeAndNotify(ref _calcRockLinedDitchGeotextileCost, value); }
        }

        private decimal _calcRockLinedDitchCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRockLinedDitchCost
        {
            get
            {
                if (IsRockLinedDitch)
                {
                    _calcRockLinedDitchCost = SiteDevelopmentCalculations.CalcRockLinedDitchCost(CalcRockLinedDitchPlacementCost, CalcRockLinedDitchMaterialCost, CalcRockLinedDitchGeotextileCost);
                }
                else
                {
                    _calcRockLinedDitchCost = 0m;
                }
                return _calcRockLinedDitchCost;
            }
            set { ChangeAndNotify(ref _calcRockLinedDitchMaterialCost, value); }
        }

        private decimal _calcVegetatedDitchCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcVegetatedDitchCost
        {
            get
            {
                if (IsVegetatedDitch)
                {
                    _calcVegetatedDitchCost = SiteDevelopmentCalculations.CalcVegetatedDitchCost(VegetatedDitchLength, VegetatedDitchUnitCost);
                }
                else
                {
                    _calcVegetatedDitchCost = 0m;
                }
                return _calcVegetatedDitchCost;
            }
            set { ChangeAndNotify(ref _calcVegetatedDitchCost, value); }
        }

        private decimal _calcCulvertCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcCulvertCost
        {
            get
            {
                if (IsCulvert)
                {
                    _calcCulvertCost = SiteDevelopmentCalculations.CalcCulvertCost(CulvertLength, CulvertUnitCost);
                }
                else
                {
                    _calcCulvertCost = 0m;
                }
                return _calcCulvertCost;
            }
            set { ChangeAndNotify(ref _calcCulvertCost, value); }
        }

        private decimal _calcDitchAndCulvertCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcDitchAndCulvertCost
        {
            get { return SiteDevelopmentCalculations.CalcDitchAndCulvertCost(CalcRockLinedDitchCost, CalcVegetatedDitchCost, CalcCulvertCost); }
            set { ChangeAndNotify(ref _calcDitchAndCulvertCost, value); }
        }

        private decimal _calcControlBuildingCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcControlBuildingCost
        {
            get
            {
                switch (ControlBuildingOptionsProperty)
                {
                    case RadioButtonsControlBuildingOptionsEnum.OptionEstimate:
                        _calcControlBuildingCost = SiteDevelopmentCalculations.CalcControlBuildingCost(CalcControlBuildingArea, ControlBuildingUnitCost);
                        break;
                    case RadioButtonsControlBuildingOptionsEnum.OptionUserSpecified:
                        _calcControlBuildingCost = ControlBuildingCostUserSpecified;
                        break;
                    default:
                        break;
                }
                return _calcControlBuildingCost;
            }
            set { ChangeAndNotify(ref _calcControlBuildingCost, value); }
        }

        private decimal _calcFoundationSlabCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcFoundationSlabCost
        {
            get
            {
                switch (ControlBuildingOptionsProperty)
                {
                    case RadioButtonsControlBuildingOptionsEnum.OptionEstimate:
                        _calcFoundationSlabCost = SiteDevelopmentCalculations.CalcFoundationSlabCost(CalcFoundationSlabVolume, FoundationConcreteUnitCost);
                        break;
                    case RadioButtonsControlBuildingOptionsEnum.OptionUserSpecified:
                        _calcFoundationSlabCost = 0m;
                        break;
                    default:
                        break;
                }
                return _calcFoundationSlabCost;
            }
            set { ChangeAndNotify(ref _calcFoundationSlabCost, value); }
        }

        private decimal _calcControlBuildingLabCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcControlBuildingLabCost
        {
            get
            {
                if (IsLab)
                {
                    _calcControlBuildingLabCost = SiteDevelopmentCalculations.CalcControlBuildingLabCost(ControlBuildingLabCost);
                }
                else
                {
                    _calcControlBuildingLabCost = 0m;
                }
                return _calcControlBuildingLabCost;
            }
            set { ChangeAndNotify(ref _calcControlBuildingLabCost, value); }
        }

        private decimal _calcControlBuildingWaterSewerCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcControlBuildingWaterSewerCost
        {
            get
            {
                if (IsWaterSewer)
                {
                    _calcControlBuildingWaterSewerCost = SiteDevelopmentCalculations.CalcControlBuildingWaterSewerCost(ControlBuildingWaterSewerCost);
                }
                else
                {
                    _calcControlBuildingWaterSewerCost = 0m;
                }
                return _calcControlBuildingWaterSewerCost;
            }
            set { ChangeAndNotify(ref _calcControlBuildingWaterSewerCost, value); }
        }

        private decimal _calcControlBuildingHVACCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcControlBuildingHVACCost
        {
            get
            {
                if (IsHVAC)
                {
                    _calcControlBuildingHVACCost = SiteDevelopmentCalculations.CalcControlBuildingHVACCost(ControlBuildingHVACCost);
                }
                else
                {
                    _calcControlBuildingHVACCost = 0m;
                }
                return _calcControlBuildingHVACCost;
            }
            set { ChangeAndNotify(ref _calcControlBuildingHVACCost, value); }
        }

        private decimal _calcControlBuildingCostEstimated;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcControlBuildingCostEstimated
        {
            get { return SiteDevelopmentCalculations.CalcControlBuildingCostEstimated(CalcControlBuildingCost, CalcFoundationSlabCost, CalcControlBuildingLabCost, CalcControlBuildingWaterSewerCost, CalcControlBuildingHVACCost); }
            set { ChangeAndNotify(ref _calcControlBuildingCostEstimated, value); }
        }

        private decimal _calcControlBuildingCostTotal;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcControlBuildingCostTotal
        {
            get
            {
                switch (ControlBuildingOptionsProperty)
                {
                    case RadioButtonsControlBuildingOptionsEnum.OptionEstimate:
                        _calcControlBuildingCostTotal = CalcControlBuildingCostEstimated;
                        break;
                    case RadioButtonsControlBuildingOptionsEnum.OptionUserSpecified:
                        _calcControlBuildingCostTotal = ControlBuildingCostUserSpecified;
                        break;
                    default:
                        break;
                }
                return _calcControlBuildingCostTotal;
            }
                set { ChangeAndNotify(ref _calcControlBuildingCostTotal, value); }
        }

        private decimal _calcCutFillCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcCutFillCost
        {
            get { return SiteDevelopmentCalculations.CalcCutFillCost(CalcCutFillVolumeCubicYardsTotal, ExcavationUnitCost); }
            set { ChangeAndNotify(ref _calcCutFillCost, value); }
        }

        private decimal _calcFoundationOverExcavationCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcFoundationOverExcavationCost
        {
            get
            {
                if (IsFoundationOverExcavation)
                {
                    _calcFoundationOverExcavationCost = SiteDevelopmentCalculations.CalcFoundationOverExcavationCost(CalcFoundationOverExcavationVolumeTotal, FoundationOverExcavationUnitCost);
                }
                else
                {
                    _calcFoundationOverExcavationCost = 0m;
                }
                return _calcFoundationOverExcavationCost;
            }
            set { ChangeAndNotify(ref _calcFoundationOverExcavationCost, value); }
        }

        private decimal _calcFoundationExcavationCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcFoundationExcavationCost
        {
            get { return SiteDevelopmentCalculations.CalcFoundationExcavationCost(CalcFoundationExcavationVolumeTotal, ExcavationUnitCost); }
            set { ChangeAndNotify(ref _calcFoundationExcavationCost, value); }
        }

        private decimal _calcFoundationImprovementCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcFoundationImprovementCost
        {
            get { return SiteDevelopmentCalculations.CalcFoundationImprovementCost(CalcFoundationOverExcavationCost, FoundationImprovementUnitCost); }
            set { ChangeAndNotify(ref _calcFoundationImprovementCost, value); }
        }

        private decimal _calcElectricServiceExtendCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcElectricServiceExtendCost
        {
            get { return SiteDevelopmentCalculations.CalcElectricServiceExtendCost(ElectricLineExtensionLength, ElectricLineExtensionUnitCost); }
            set { ChangeAndNotify(ref _calcElectricServiceExtendCost, value); }
        }

        private decimal _calcElectricStepdownTransformerCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcElectricStepdownTransformerCost
        {
            get
            {
                if (IsStepdownTransformer)
                {
                    _calcElectricStepdownTransformerCost = ElectricStepdownTransformerCost;
                }
                else
                {
                    _calcElectricStepdownTransformerCost = 0m;
                }
                return _calcElectricStepdownTransformerCost;
            }
            set { ChangeAndNotify(ref _calcElectricStepdownTransformerCost, value); }
        }

        private decimal _calcElectricTelecommunicationsCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcElectricTelecommunicationsCost
        {
            get
            {
                if (IsTelecommunications)
                {
                    _calcElectricTelecommunicationsCost = ElectricTelecommunicationsCost;
                }
                else
                {
                    _calcElectricTelecommunicationsCost = 0m;
                }
                return _calcElectricTelecommunicationsCost;
            }
            set { ChangeAndNotify(ref _calcElectricTelecommunicationsCost, value); }
        }

        private decimal _calcElectricPlantAutomationCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcElectricPlantAutomationCost
        {
            get
            {
                if (IsPlantAutomation)
                {
                    _calcElectricPlantAutomationCost = ElectricPlantAutomationCost;
                }
                else
                {
                    _calcElectricPlantAutomationCost = 0m;
                }
                return _calcElectricPlantAutomationCost;
            }
            set { ChangeAndNotify(ref _calcElectricPlantAutomationCost, value); }
        }

        private decimal _calcElectricOtherUtilityCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcElectricOtherUtilityCost
        {
            get
            {
                if (IsElectricOtherUtility)
                {
                    _calcElectricOtherUtilityCost = ElectricOtherUtilityCost;
                }
                else
                {
                    _calcElectricOtherUtilityCost = 0m;
                }
                return _calcElectricOtherUtilityCost;
            }
            set { ChangeAndNotify(ref _calcElectricOtherUtilityCost, value); }
        }

        private decimal _calcElectricUtilityCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcElectricUtilityCost
        {
            get
            {
                return SiteDevelopmentCalculations.CalcElectricUtilityCost(CalcElectricServiceExtendCost, ElectricBackupGeneratorCost, CalcElectricStepdownTransformerCost,
                                                      ElectricPanelCost, CalcElectricTelecommunicationsCost, CalcElectricPlantAutomationCost,
                                                      CalcElectricOtherUtilityCost);
            }
            set { ChangeAndNotify(ref _calcElectricUtilityCost, value); }
        }

        private decimal _calcElectricWiringCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcElectricWiringCost
        {
            get { return SiteDevelopmentCalculations.CalcElectricWiringCost(ElectricWiringPercentage, CalcElectricUtilityCost, CapitalCostActiveModules); }
            set { ChangeAndNotify(ref _calcElectricWiringCost, value); }
        }

        private decimal _calcElectricUtilityCostTotal;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcElectricUtilityCostTotal
        {
            get { return SiteDevelopmentCalculations.CalcElectricUtilityCostTotal(CalcElectricWiringCost, CalcElectricUtilityCost); }
            set { ChangeAndNotify(ref _calcElectricUtilityCostTotal, value); }
        }

        private decimal _calcFencingCostKnownQuantity;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcFencingCostKnownQuantity
        {
            get { return SiteDevelopmentCalculations.CalcFencingCostKnownQuantity(FencingLength, FencingUnitCost); }
            set { ChangeAndNotify(ref _calcFencingCostKnownQuantity, value); }
        }

        private decimal _calcFencingCostEstimate;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcFencingCostEstimate
        {
            get { return SiteDevelopmentCalculations.CalcFencingCostEstimate(CalcFencingPerimeter, FencingUnitCost); }
            set { ChangeAndNotify(ref _calcFencingCostEstimate, value); }
        }

        private decimal _calcFencingCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcFencingCost
        {
            get
            {
                switch (FencingOptionsProperty)
                {
                    case RadioButtonsFencingOptionsEnum.OptionKnown:
                        _calcFencingCost = CalcFencingCostKnownQuantity;
                        break;
                    case RadioButtonsFencingOptionsEnum.OptionEstimate:
                        _calcFencingCost = CalcFencingCostEstimate;
                        break;
                    case RadioButtonsFencingOptionsEnum.OptionUserSpecified:
                        _calcFencingCost = FencingCostUserSpecified;
                        break;
                    default:
                        break;
                }
                return _calcFencingCost;
            }
            set { ChangeAndNotify(ref _calcFencingCost, value); }
        }

        private decimal _calcAccessGateCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcAccessGateCost
        {
            get
            {
                if (IsAccessGate)
                {
                    _calcAccessGateCost = SiteDevelopmentCalculations.CalcAccessGateCost(AccessGateQuantity, AccessGateUnitCost);
                }
                else
                {
                    _calcAccessGateCost = 0m;
                }
                return _calcAccessGateCost;
            }
            set { ChangeAndNotify(ref _calcAccessGateCost, value); }
        }

        private decimal _calcClearAndGrubCostTotal;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcClearAndGrubCostTotal
        {
            get { return SiteDevelopmentCalculations.CalcClearAndGrubCostTotal(CalcClearAndGrubAreaTotal, ClearAndGrubUnitCost); }
            set { ChangeAndNotify(ref _calcClearAndGrubCostTotal, value); }
        }

        private decimal _calcRevegetationCostTotal;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRevegetationCostTotal
        {
            get { return SiteDevelopmentCalculations.CalcRevegetationCostTotal(CalcClearAndGrubAreaTotal, ClearAndGrubRevegetationPercentage, RevegetationUnitCost); }
            set { ChangeAndNotify(ref _calcRevegetationCostTotal, value); }
        }

        private decimal _calcOtherCapitalItemsCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcOtherCapitalItemsCost
        {
            get
            {
                return SiteDevelopmentCalculations.CalcOtherCapitalItemsCost(OtherCapitalItemQuantity1, OtherCapitalItemUnitCost1,
                                                                  OtherCapitalItemQuantity2, OtherCapitalItemUnitCost2,
                                                                  OtherCapitalItemQuantity3, OtherCapitalItemUnitCost3,
                                                                  OtherCapitalItemQuantity4, OtherCapitalItemUnitCost4,
                                                                  OtherCapitalItemQuantity5, OtherCapitalItemUnitCost5);
            }
            set { ChangeAndNotify(ref _calcOtherCapitalItemsCost, value); }

        }

        private decimal _calcConstructionOfficeTrailerCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcConstructionOfficeTrailerCost
        {
            get
            {
                if (IsConstructionOfficeTrailer)
                {
                    _calcConstructionOfficeTrailerCost = ConstructionOfficeTrailerCost;
                }
                else
                {
                    _calcConstructionOfficeTrailerCost = 0m;
                }
                return _calcConstructionOfficeTrailerCost;
            }
            set { ChangeAndNotify(ref _calcConstructionOfficeTrailerCost, value); }
        }

        private decimal _calcCapitalCostSiteDevelopment;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcCapitalCostSiteDevelopment
        {
            get
            {
                return SiteDevelopmentCalculations.CalcCapitalCostSiteDevelopment(CalcAccessRoadCost, CalcParkingAreaCost, CalcLandPurchaseCost,
                                                                  CalcConstructionOfficeTrailerCost, CalcESControlCost, CalcDitchAndCulvertCost,
                                                                  CalcControlBuildingCostTotal, CalcCutFillCost, CalcFoundationOverExcavationCost,
                                                                  CalcFoundationExcavationCost, CalcFoundationImprovementCost, CalcElectricUtilityCostTotal,
                                                                  CalcFencingCost, CalcAccessGateCost, CalcClearAndGrubCostTotal,
                                                                  CalcRevegetationCostTotal, CalcOtherCapitalItemsCost);
            }
            set { ChangeAndNotify(ref _calcCapitalCostSiteDevelopment, value); }
        }

        private decimal _calcMobilizationDemobilizationCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcMobilizationDemobilizationCost
        {
            get { return SiteDevelopmentCalculations.CalcMobilizationDemobilizationCost(MobilizationDemobilizationPercentage, CapitalCostPassiveAndActiveModules, CalcCapitalCostSiteDevelopment, CalcLandPurchaseCost); }
            set { ChangeAndNotify(ref _calcMobilizationDemobilizationCost, value); }
        }

        private decimal _calcEngineeringPermittingCostEstimated;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcEngineeringPermittingCostEstimated
        {
            get { return SiteDevelopmentCalculations.CalcEngineeringPermittingCost(EngineeringPermittingPercentage, CapitalCostPassiveAndActiveModules, CalcCapitalCostSiteDevelopment, CalcLandPurchaseCost); }
            set { ChangeAndNotify(ref _calcEngineeringPermittingCostEstimated, value); }
        }

        private decimal _calcEngineeringPermittingCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcEngineeringPermittingCost
        {
            get
            {
                if (IsEngineeringPermitting)
                {
                    switch (EngineeringPermittingOptionsProperty)
                    {
                        case RadioButtonsEngineeringPermittingOptionsEnum.OptionEstimate:
                            _calcEngineeringPermittingCost = CalcEngineeringPermittingCostEstimated;
                            break;
                        case RadioButtonsEngineeringPermittingOptionsEnum.OptionUserSpecified:
                            _calcEngineeringPermittingCost = EngineeringPermittingCostUserSpecified;
                            break;
                        default:
                            break;
                    }                    
                }
                else
                {
                    _calcEngineeringPermittingCost = 0m;
                }
                return _calcEngineeringPermittingCost;
            }
            set { ChangeAndNotify(ref _calcEngineeringPermittingCost, value); }
        }

        private decimal _calcSurveyingCostEstimated;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcSurveyingCostEstimated
        {
            get { return SiteDevelopmentCalculations.CalcSurveyingCost(SurveyingPercentage, CapitalCostPassiveModules, CalcCapitalCostSiteDevelopment, CalcLandPurchaseCost); }
            set { ChangeAndNotify(ref _calcSurveyingCostEstimated, value); }
        }

        private decimal _calcSurveyingCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcSurveyingCost
        {
            get
            {
                if (IsSurveying)
                {
                    switch (SurveyingOptionsProperty)
                    {
                        case RadioButtonsSurveyingOptionsEnum.OptionEstimate:
                            _calcSurveyingCost = CalcSurveyingCostEstimated;
                            break;
                        case RadioButtonsSurveyingOptionsEnum.OptionUserSpecified:
                            _calcSurveyingCost = SurveyingCostUserSpecified;
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    _calcSurveyingCost = 0m;
                }
                return _calcSurveyingCost;
            }
            set { ChangeAndNotify(ref _calcSurveyingCost, value); }
        }

        private decimal _calcConstructionInspectionCostEstimated;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcConstructionInspectionCostEstimated
        {
            get { return SiteDevelopmentCalculations.CalcConstructionInspectionCost(ConstructionInspectionPercentage, CapitalCostPassiveModules, CalcCapitalCostSiteDevelopment, CalcLandPurchaseCost); }
            set { ChangeAndNotify(ref _calcConstructionInspectionCostEstimated, value); }
        }

        private decimal _calcConstructionInspectionCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcConstructionInspectionCost
        {
            get
            {
                if (IsConstructionInspection)
                {
                    switch (ConstructionInspectionOptionsProperty)
                    {
                        case RadioButtonsConstructionInspectionOptionsEnum.OptionEstimate:
                            _calcConstructionInspectionCost = CalcConstructionInspectionCostEstimated;
                            break;
                        case RadioButtonsConstructionInspectionOptionsEnum.OptionUserSpecified:
                            _calcConstructionInspectionCost = ConstructionInspectionCostUserSpecified;
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    _calcConstructionInspectionCost = 0m;
                }
                return _calcConstructionInspectionCost;
            }
            set { ChangeAndNotify(ref _calcConstructionInspectionCost, value); }
        }

        private decimal _calcContingencyPercentageCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcContingencyPercentageCost
        {
            get { return SiteDevelopmentCalculations.CalcContingencyPercentageCost(ContingencyPercentage, CapitalCostPassiveModules, CalcCapitalCostSiteDevelopment, CalcLandPurchaseCost); }
            set { ChangeAndNotify(ref _calcContingencyPercentageCost, value); }
        }

        private decimal _calcContingencyCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcContingencyCost
        {
            get
            {
                switch (ContingencyOptionsProperty)
                {
                    case RadioButtonsContingencyOptionsEnum.OptionEstimate:
                        _calcContingencyCost = CalcContingencyPercentageCost;
                        break;
                    case RadioButtonsContingencyOptionsEnum.OptionUserSpecified:
                        _calcContingencyCost = ContingencyCostUserSpecified;
                        break;
                    default:
                        break;
                }
                return _calcContingencyCost;
            }
            set { ChangeAndNotify(ref _calcContingencyCost, value); }
        }

        private decimal _calcCapitalCostTotal;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcCapitalCostTotal
        {
            get
            {
                _calcCapitalCostTotal = SiteDevelopmentCalculations.CalcCapitalCostTotal(CapitalCostPassiveAndActiveModules, CalcCapitalCostSiteDevelopment, CalcMobilizationDemobilizationCost,
                                                                                         CalcEngineeringPermittingCost, CalcSurveyingCost, CalcConstructionInspectionCost,
                                                                                         CalcContingencyCost);

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
        /// Radio button binding with enumeration for annual costs
        /// </summary>
        public enum RadioButtonsAnnualCostOperationOptionsEnum
        {
            OptionEstimated,
            OptionUserSpecified
        }

        private RadioButtonsAnnualCostOperationOptionsEnum _annualCostOperationOptionsProperty;
        /// <summary>
        ///  User specified 
        /// </summary>
        public RadioButtonsAnnualCostOperationOptionsEnum AnnualCostOperationOptionsProperty
        {
            get { return _annualCostOperationOptionsProperty; }
            set { ChangeAndNotify(ref _annualCostOperationOptionsProperty, value, nameof(AnnualCostOperationOptionsProperty), CalcPropertiesStringArray); }
        }

        private double _annualCostOperationPercentage;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double AnnualCostOperationPercentage
        {
            get { return _annualCostOperationPercentage; }
            set { ChangeAndNotify(ref _annualCostOperationPercentage, value, nameof(AnnualCostOperationPercentage), CalcPropertiesStringArray); }
        }

        private decimal _annualCostOperationUserSpecified;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal AnnualCostOperationUserSpecified
        {
            get { return _annualCostOperationUserSpecified; }
            set { ChangeAndNotify(ref _annualCostOperationUserSpecified, value, nameof(AnnualCostOperationUserSpecified), CalcPropertiesStringArray); }
        }

        private decimal _calcLandLeaseCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcLandLeaseCost
        {
            get { return SiteDevelopmentCalculations.CalcLandLeaseCost(CalcLandAreaAcres, LandLeaseCost); }
            set { ChangeAndNotify(ref _calcLandLeaseCost, value); }
        }

        private decimal _calcAnnualCostLandLeaseCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcAnnualCostLandLeaseCost
        {
            get
            {
                switch (LandCostOptionsProperty)
                {
                    case RadioButtonsLandCostOptionsEnum.OptionPurchase:
                        _calcAnnualCostLandLeaseCost = 0m;
                        break;
                    case RadioButtonsLandCostOptionsEnum.OptionLease:
                        _calcAnnualCostLandLeaseCost = CalcLandLeaseCost;
                        break;
                    default:
                        break;
                }
                return _calcAnnualCostLandLeaseCost;
            }
            set { ChangeAndNotify(ref _calcAnnualCostLandLeaseCost, value); }
        }

        private decimal _calcAnnualCostOperationOther;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcAnnualCostOperationOther
        {
            get
            {
                if (IsAnnualCostOperationOther)
                {
                    _calcAnnualCostOperationOther = AnnualCostOperationOther;
                }
                else
                {
                    _calcAnnualCostOperationOther = 0m;
                }
                return _calcAnnualCostOperationOther;
            }
            set { ChangeAndNotify(ref _calcAnnualCostOperationOther, value); }
        }

        private decimal _calcAnnualCostOperationPercentageOption;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcAnnualCostOperationPercentageOption
        {
            get
            {
                return SiteDevelopmentCalculations.CalcAnnualCostOperationPercentageOption(AnnualCostOperationPercentage, CapitalCostPassiveAndActiveModules, CalcCapitalCostSiteDevelopment,
                                                                                           CalcLandPurchaseCost, CalcLandLeaseCost, AnnualCostPropertyTax,
                                                                                           AnnualCostUtility, CalcAnnualCostOperationOther);
            }
            set { ChangeAndNotify(ref _calcAnnualCostOperationPercentageOption, value); }
        }

        private decimal _calcAnnualCostOperationUserSpecifiedOption;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcAnnualCostOperationUserSpecifiedOption
        {
            get
            {
                return SiteDevelopmentCalculations.CalcAnnualCostOperationUserSpecifiedOption(AnnualCostOperationUserSpecified, CapitalCostPassiveAndActiveModules, CalcCapitalCostSiteDevelopment,
                                                                                              CalcLandPurchaseCost, LandLeaseCost, AnnualCostPropertyTax,
                                                                                              AnnualCostUtility, CalcAnnualCostOperationOther);
            }
            set { ChangeAndNotify(ref _calcAnnualCostOperationUserSpecifiedOption, value); }
        }

        private decimal _calcAnnualCostOperation;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcAnnualCostOperation
        {
            get
            {
                switch (AnnualCostOperationOptionsProperty)
                {
                    case RadioButtonsAnnualCostOperationOptionsEnum.OptionEstimated:
                        _calcAnnualCostOperation = CalcAnnualCostOperationPercentageOption;
                        break;
                    case RadioButtonsAnnualCostOperationOptionsEnum.OptionUserSpecified:
                        _calcAnnualCostOperation = CalcAnnualCostOperationUserSpecifiedOption;
                        break;
                    default:
                        break;
                }
                return _calcAnnualCostOperation;
            }
            set { ChangeAndNotify(ref _calcAnnualCostOperation, value); }
        }

        private decimal _calcAnnualCostMaintenance;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcAnnualCostMaintenance
        {
            get
            {
                return SiteDevelopmentCalculations.CalcAnnualCostMaintenance(MaintenanceRepairLaborRate, BuildingMaintenanceHoursPerYear, RoadMaintenanceHoursPerYear,
                                                                             MowingHoursPerYear, SnowRemovalHoursPerYear, DitchCulvertCleaningHoursPerYear,
                                                                             LeafRemovalHoursPerYear, SiteInspectionsHoursPerYear, AnnualOtherHoursPerYear,
                                                                             ParkingAreaMaintenanceUnitCost, CalcParkingSurfaceAreaSquareFeet);
            }
            set { ChangeAndNotify(ref _calcAnnualCostMaintenance, value); }
        }

        /// <summary>
        /// Radio button binding with enumeration for annual costs
        /// </summary>
        public enum RadioButtonsAnnualCostRepairsOptionsEnum
        {
            OptionEstimated,
            OptionUserSpecified
        }

        private RadioButtonsAnnualCostRepairsOptionsEnum _annualCostRepairsOptionsProperty;
        /// <summary>
        ///  User specified 
        /// </summary>
        public RadioButtonsAnnualCostRepairsOptionsEnum AnnualCostRepairsOptionsProperty
        {
            get { return _annualCostRepairsOptionsProperty; }
            set { ChangeAndNotify(ref _annualCostRepairsOptionsProperty, value, nameof(AnnualCostRepairsOptionsProperty), CalcPropertiesStringArray); }
        }

        private double _annualCostRepairsPercentage;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double AnnualCostRepairsPercentage
        {
            get { return _annualCostRepairsPercentage; }
            set { ChangeAndNotify(ref _annualCostRepairsPercentage, value, nameof(AnnualCostRepairsPercentage), CalcPropertiesStringArray); }
        }

        private decimal _annualCostRepairsUserSpecified;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal AnnualCostRepairsUserSpecified
        {
            get { return _annualCostRepairsUserSpecified; }
            set { ChangeAndNotify(ref _annualCostRepairsUserSpecified, value, nameof(AnnualCostRepairsUserSpecified), CalcPropertiesStringArray); }
        }

        private decimal _calcAnnualCostRepairsEstimate;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcAnnualCostRepairsEstimate
        {
            get
            {
                return SiteDevelopmentCalculations.CalcAnnualCostRepairsEstimate(AnnualCostRepairsPercentage, CapitalCostPassiveAndActiveModules, CalcCapitalCostSiteDevelopment, CalcLandPurchaseCost);
            }
            set { ChangeAndNotify(ref _calcAnnualCostRepairsEstimate, value); }
        }

        private decimal _calcAnnualCostRepairs;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcAnnualCostRepairs
        {
            get
            {
                switch (AnnualCostRepairsOptionsProperty)
                {
                    case RadioButtonsAnnualCostRepairsOptionsEnum.OptionEstimated:
                        _calcAnnualCostRepairs = CalcAnnualCostRepairsEstimate; 
                        break;
                    case RadioButtonsAnnualCostRepairsOptionsEnum.OptionUserSpecified:
                        _calcAnnualCostRepairs = AnnualCostRepairsUserSpecified;
                        break;
                    default:
                        break;
                }
                return _calcAnnualCostRepairs;
            }
            set { ChangeAndNotify(ref _calcAnnualCostRepairs, value); }
        }

        private decimal _calcAnnualCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcAnnualCost
        {
            get
            {
                _calcAnnualCost = SiteDevelopmentCalculations.CalcAnnualCost(CalcAnnualCostOperation, CalcAnnualCostMaintenance, CalcAnnualCostRepairs);

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

        private double _recapitalizationCostLifeCycleAccessRoads;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostLifeCycleAccessRoads
        {
            get { return _recapitalizationCostLifeCycleAccessRoads; }
            set { ChangeAndNotify(ref _recapitalizationCostLifeCycleAccessRoads, value, nameof(RecapitalizationCostLifeCycleAccessRoads), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostLifeCycleParkingArea;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostLifeCycleParkingArea
        {
            get { return _recapitalizationCostLifeCycleParkingArea; }
            set { ChangeAndNotify(ref _recapitalizationCostLifeCycleParkingArea, value, nameof(RecapitalizationCostLifeCycleParkingArea), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostLifeCycleDitchesAndCulverts;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostLifeCycleDitchesAndCulverts
        {
            get { return _recapitalizationCostLifeCycleDitchesAndCulverts; }
            set { ChangeAndNotify(ref _recapitalizationCostLifeCycleDitchesAndCulverts, value, nameof(RecapitalizationCostLifeCycleDitchesAndCulverts), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostLifeCycleControlBuilding;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostLifeCycleControlBuilding
        {
            get { return _recapitalizationCostLifeCycleControlBuilding; }
            set { ChangeAndNotify(ref _recapitalizationCostLifeCycleControlBuilding, value, nameof(RecapitalizationCostLifeCycleControlBuilding), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostLifeCycleFencing;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostLifeCycleFencing
        {
            get { return _recapitalizationCostLifeCycleFencing; }
            set { ChangeAndNotify(ref _recapitalizationCostLifeCycleFencing, value, nameof(RecapitalizationCostLifeCycleFencing), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostLifeCycleAccessGates;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostLifeCycleAccessGates
        {
            get { return _recapitalizationCostLifeCycleAccessGates; }
            set { ChangeAndNotify(ref _recapitalizationCostLifeCycleAccessGates, value, nameof(RecapitalizationCostLifeCycleAccessGates), CalcPropertiesStringArray); }
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

        private double _recapitalizationCostPercentReplacementAccessRoads;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostPercentReplacementAccessRoads
        {
            get { return _recapitalizationCostPercentReplacementAccessRoads; }
            set { ChangeAndNotify(ref _recapitalizationCostPercentReplacementAccessRoads, value, nameof(RecapitalizationCostPercentReplacementAccessRoads), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostPercentReplacementParkingArea;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostPercentReplacementParkingArea
        {
            get { return _recapitalizationCostPercentReplacementParkingArea; }
            set { ChangeAndNotify(ref _recapitalizationCostPercentReplacementParkingArea, value, nameof(RecapitalizationCostPercentReplacementParkingArea), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostPercentReplacementDitchesAndCulverts;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostPercentReplacementDitchesAndCulverts
        {
            get { return _recapitalizationCostPercentReplacementDitchesAndCulverts; }
            set { ChangeAndNotify(ref _recapitalizationCostPercentReplacementDitchesAndCulverts, value, nameof(RecapitalizationCostPercentReplacementDitchesAndCulverts), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostPercentReplacementControlBuilding;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostPercentReplacementControlBuilding
        {
            get { return _recapitalizationCostPercentReplacementControlBuilding; }
            set { ChangeAndNotify(ref _recapitalizationCostPercentReplacementControlBuilding, value, nameof(RecapitalizationCostPercentReplacementControlBuilding), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostPercentReplacementFencing;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostPercentReplacementFencing
        {
            get { return _recapitalizationCostPercentReplacementFencing; }
            set { ChangeAndNotify(ref _recapitalizationCostPercentReplacementFencing, value, nameof(RecapitalizationCostPercentReplacementFencing), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostPercentReplacementAccessGates;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostPercentReplacementAccessGates
        {
            get { return _recapitalizationCostPercentReplacementAccessGates; }
            set { ChangeAndNotify(ref _recapitalizationCostPercentReplacementAccessGates, value, nameof(RecapitalizationCostPercentReplacementAccessGates), CalcPropertiesStringArray); }
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

        private decimal _calcRapitalizationCostAccessRoads;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostAccessRoads
        {
            get
            {
                return SiteDevelopmentCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
                                                                    RecapitalizationCostInflationRate, RecapitalizationCostLifeCycleAccessRoads,
                                                                    CalcAccessRoadCost, RecapitalizationCostPercentReplacementAccessRoads);
            }
            set { ChangeAndNotify(ref _calcRapitalizationCostAccessRoads, value); }
        }

        private decimal _calcRapitalizationCostParkingArea;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostParkingArea
        {
            get
            {
                return SiteDevelopmentCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
                                                                    RecapitalizationCostInflationRate, RecapitalizationCostLifeCycleParkingArea,
                                                                    CalcParkingAreaCost, RecapitalizationCostPercentReplacementParkingArea);
            }
            set { ChangeAndNotify(ref _calcRapitalizationCostParkingArea, value); }
        }

        private decimal _calcRapitalizationCostDitchesAndCulverts;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostDitchesAndCulverts
        {
            get
            {
                return SiteDevelopmentCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
                                                                    RecapitalizationCostInflationRate, RecapitalizationCostLifeCycleDitchesAndCulverts,
                                                                    CalcDitchAndCulvertCost, RecapitalizationCostPercentReplacementDitchesAndCulverts);
            }
            set { ChangeAndNotify(ref _calcRapitalizationCostDitchesAndCulverts, value); }
        }


        private decimal _calcRecapitalizationCostControlBuildingMaterialCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostControlBuildingMaterialCost
        {
            get
            {
                return CalcControlBuildingCostTotal - CalcFoundationSlabCost;
            }
            set { ChangeAndNotify(ref _calcRecapitalizationCostControlBuildingMaterialCost, value); }
        }

        private decimal _calcRapitalizationCostControlBuilding;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostControlBuilding
        {
            get
            {
                return SiteDevelopmentCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
                                                                    RecapitalizationCostInflationRate, RecapitalizationCostLifeCycleControlBuilding,
                                                                    CalcRecapitalizationCostControlBuildingMaterialCost, RecapitalizationCostPercentReplacementControlBuilding);
            }
            set { ChangeAndNotify(ref _calcRapitalizationCostControlBuilding, value); }
        }

        private decimal _calcRapitalizationCostFencing;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostFencing
        {
            get
            {
                return SiteDevelopmentCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
                                                                    RecapitalizationCostInflationRate, RecapitalizationCostLifeCycleFencing,
                                                                    CalcFencingCost, RecapitalizationCostPercentReplacementFencing);
            }
            set { ChangeAndNotify(ref _calcRapitalizationCostFencing, value); }
        }

        private decimal _calcRapitalizationCostAccessGates;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostAccessGates
        {
            get
            {
                return SiteDevelopmentCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
                                                                    RecapitalizationCostInflationRate, RecapitalizationCostLifeCycleAccessGates,
                                                                    CalcAccessGateCost, RecapitalizationCostPercentReplacementAccessGates);
            }
            set { ChangeAndNotify(ref _calcRapitalizationCostAccessGates, value); }
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
            get { return SiteDevelopmentCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn, 
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
            {
                if (item.IsSelected)
                {
                    selectedItemsTotals.Add(item.TotalCost);
                }
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
                    case "AccessRoads":
                        item.MaterialCostDefault = CalcAccessRoadCost;
                        break;
                    case "ParkingArea":
                        item.MaterialCostDefault = CalcParkingAreaCost;
                        break;
                    case "DitchesAndCulverts":
                        item.MaterialCostDefault = CalcDitchAndCulvertCost;
                        break;
                    case "ControlBuilding":
                        item.MaterialCostDefault = CalcRecapitalizationCostControlBuildingMaterialCost;
                        break;
                    case "Fencing":
                        item.MaterialCostDefault = CalcFencingCost;
                        break;
                    case "AccessGate":
                        item.MaterialCostDefault = CalcAccessGateCost;
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
                    case "AccessRoads":
                        item.TotalCost = CalcRecapitalizationCostAccessRoads;
                        break;
                    case "ParkingArea":
                        item.TotalCost = CalcRecapitalizationCostParkingArea;
                        break;
                    case "DitchesAndCulverts":
                        item.TotalCost = CalcRecapitalizationCostDitchesAndCulverts;
                        break;
                    case "ControlBuilding":
                        item.TotalCost = CalcRecapitalizationCostControlBuilding;
                        break;
                    case "Fencing":
                        item.TotalCost = CalcRecapitalizationCostFencing;
                        break;
                    case "AccessGate":
                        item.TotalCost = CalcRecapitalizationCostAccessGates;
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
                Name = "Access Roads",
                NameFixed = "AccessRoads",
                LifeCycle = RecapitalizationCostLifeCycleAccessRoads,
                PercentReplacement = RecapitalizationCostPercentReplacementAccessRoads,
                MaterialCostDefault = CalcAccessRoadCost,
                UseCustomCost = false,
                MaterialCostCustom = 0m,
                TotalCost = CalcRecapitalizationCostAccessRoads
            });
            RecapMaterials.Add(new RecapMaterial
            {
                IsSelected = true,
                Name = "Parking Area",
                NameFixed = "ParkingArea",
                LifeCycle = RecapitalizationCostLifeCycleParkingArea,
                PercentReplacement = RecapitalizationCostPercentReplacementParkingArea,
                MaterialCostDefault = CalcParkingAreaCost,
                UseCustomCost = false,
                MaterialCostCustom = 0m,
                TotalCost = CalcRecapitalizationCostParkingArea
            });
            RecapMaterials.Add(new RecapMaterial
            {
                IsSelected = true,
                Name = "Ditches and Culverts",
                NameFixed = "DitchesAndCulverts",
                LifeCycle = RecapitalizationCostLifeCycleDitchesAndCulverts,
                PercentReplacement = RecapitalizationCostPercentReplacementDitchesAndCulverts,
                MaterialCostDefault = CalcDitchAndCulvertCost,
                UseCustomCost = false,
                MaterialCostCustom = 0m,
                TotalCost = CalcRecapitalizationCostDitchesAndCulverts
            });
            RecapMaterials.Add(new RecapMaterial
            {
                IsSelected = true,
                Name = "Control Building",
                NameFixed = "ControlBuilding",
                LifeCycle = RecapitalizationCostLifeCycleControlBuilding,
                PercentReplacement = RecapitalizationCostPercentReplacementControlBuilding,
                MaterialCostDefault = CalcRecapitalizationCostControlBuildingMaterialCost,
                UseCustomCost = false,
                MaterialCostCustom = 0m,
                TotalCost = CalcRecapitalizationCostControlBuilding
            });
            RecapMaterials.Add(new RecapMaterial
            {
                IsSelected = true,
                Name = "Fencing",
                NameFixed = "Fencing",
                LifeCycle = RecapitalizationCostLifeCycleFencing,
                PercentReplacement = RecapitalizationCostPercentReplacementFencing,
                MaterialCostDefault = CalcFencingCost,
                UseCustomCost = false,
                MaterialCostCustom = 0m,
                TotalCost = CalcRecapitalizationCostFencing
            });
            RecapMaterials.Add(new RecapMaterial
            {
                IsSelected = true,
                Name = "Access Gate",
                NameFixed = "AccessGate",
                LifeCycle = RecapitalizationCostLifeCycleAccessGates,
                PercentReplacement = RecapitalizationCostPercentReplacementAccessGates,
                MaterialCostDefault = CalcAccessRoadCost,
                UseCustomCost = false,
                MaterialCostCustom = 0m,
                TotalCost = CalcRecapitalizationCostAccessGates
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
            ((RecapMaterial)sender).TotalCost = SiteDevelopmentCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
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
        public ICommand SetRoadMaterialCommand { get; }
        public ICommand SetParkingAreaMaterialCommand { get; }
        public ICommand SetFoundationSiteSoilCommand { get; }
        public ICommand SetESControlCommand { get; }
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
            var customDialog = new CustomDialog() { Title = "About Sampling" };

            var customDialogViewModel = new CustomDialogViewModel(instance =>
            {
                _dialogCoordinator.HideMetroDialogAsync(this, customDialog);
            });
            customDialogViewModel.Message = Resources.infoSiteDevelopment;
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

        private ICommand _showMessageDialogCommandSiteDevelopmentItems;
        public ICommand ShowMessageDialogCommandSiteDevelopmentItems
        {
            get
            {
                return _showMessageDialogCommandSiteDevelopmentItems ?? (this._showMessageDialogCommandSiteDevelopmentItems = new SimpleCommand
                {
                    CanExecuteDelegate = x => true,
                    ExecuteDelegate = async x =>
                    {
                        string message = Resources.infoSiteDevelopmentItemsSiteDevelopment;
                        await _dialogCoordinator.ShowMessageAsync(this, "Site Development", message);
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
                        string message = Resources.infoOtherItemsCapitalSiteDevelopment;
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
                        string message = Resources.infoAnnualCostInputSiteDevelopment;
                        await _dialogCoordinator.ShowMessageAsync(this, "Annual Cost Input", message);
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
                        string message = Resources.infoSizingSummarySiteDevelopment;
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
                        string message = Resources.infoCapitalCostSiteDevelopment;
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
                        string message = Resources.infoAnnualCostSiteDevelopment;
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
                        string message = Resources.infoRecapitalizationCostSiteDevelopment;
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

        public void SetRoadMaterial(object roadMaterial)
        {
            GeneralCostItem itemRoadMaterial = (GeneralCostItem)roadMaterial;

            switch (itemRoadMaterial.Name)
            {
                case "Aggregate":
                    RoadMaterialName = RoadMaterialNameAggregate;
                    RoadMaterialUnitCost = RoadMaterialUnitCostAggregate;
                    break;
                case "Asphalt":
                    RoadMaterialName = RoadMaterialNameAsphalt;
                    RoadMaterialUnitCost = RoadMaterialUnitCostAsphalt;
                    break;
                default:
                    break;
            }
        }

        public void SetParkingAreaMaterial(object parkingAreaMaterial)
        {
            GeneralCostItem itemParkingAreaMaterial = (GeneralCostItem)parkingAreaMaterial;

            switch (itemParkingAreaMaterial.Name)
            {
                case "Aggregate":
                    ParkingAreaMaterialName = ParkingAreaMaterialNameAggregate;
                    ParkingMaterialUnitCost = RoadMaterialUnitCostAggregate;
                    break;
                case "Asphalt":
                    ParkingAreaMaterialName = ParkingAreaMaterialNameAsphalt;
                    ParkingMaterialUnitCost = RoadMaterialUnitCostAsphalt;
                    break;
                default:
                    break;
            }
        }


        public void SetFoundationSiteSoil(object foundationSiteSoil)
        {
            GeneralItem itemFoundationSiteSoil = (GeneralItem)foundationSiteSoil;

            switch (itemFoundationSiteSoil.Name)
            {
                case "Poor":
                    FoundationSiteSoilName = FoundationSiteSoilNamePoor;
                    FoundationSiteSoilFactor = FoundationSiteSoilFactorPoor;
                    break;
                case "Average":
                    FoundationSiteSoilName = FoundationSiteSoilNameAverage;
                    FoundationSiteSoilFactor = FoundationSiteSoilFactorAverage;
                    break;
                case "Excellent":
                    FoundationSiteSoilName = FoundationSiteSoilNameExcellent;
                    FoundationSiteSoilFactor = FoundationSiteSoilFactorExcellent;
                    break;
                default:
                    break;
            }
        }

        public void SetESControl(object esControl)
        {
            GeneralItem itemESControl = (GeneralItem)esControl;

            switch (itemESControl.Name)
            {
                case "Silt Fence":
                    ESControlName = ESControlNameSiltFence;
                    break;
                case "Compost Filter Sock":
                    ESControlName = ESControlNameCompostFilterSock;
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

        #region Saving
        public new void SaveFile()
        {
            SyncWithMainUi();

            // Lists to hold dictionaries of data
            List<Item> propertyItems = new List<Item>();

            // Get all properties based on type
            foreach (var prop in this.GetType().GetProperties())
            {
                // property items
                propertyItems.Add(new Item(prop.Name, prop.GetValue(this)));
            }

            // Open save dialog and write data
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML file (*.xml)|*.xml";
            if (saveFileDialog.ShowDialog() == true)
            {
                XDocument xmlDoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"));
                xmlDoc.Add(
                    new XElement("module",
                    new XElement("items",
                        propertyItems.Select(item => new XElement("item",
                            new XElement("name", item.Name),
                            new XElement("value", item.Value)
                        )))
                   )
                );
                xmlDoc.Save(saveFileDialog.FileName);
            }
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
            UpdateCapitalCostPassiveAndActiveModules();
            UpdateClearAndGrubAreaPassiveModules();
            UpdateFoundationAreaDepthPassiveAndActiveModules();
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
            UpdateCapitalCostPassiveAndActiveModules();
            UpdateClearAndGrubAreaPassiveModules();
            UpdateFoundationAreaDepthPassiveAndActiveModules();
            UpdateRecapitalizationValues();
        }

        public void UpdateCapitalCostPassiveAndActiveModules()
        {
            foreach (var data in SharedDataCollection.ToList())
            {
                CapitalCostPassiveModules = (decimal)data.Data["CapitalCostPassive_SharedData"];
                CapitalCostActiveModules = (decimal)data.Data["CapitalCostActive_SharedData"];
            }

            CapitalCostPassiveAndActiveModules = CapitalCostPassiveModules + CapitalCostActiveModules;
        }

        public void UpdateClearAndGrubAreaPassiveModules()
        {
            foreach (var data in SharedDataCollection.ToList())
            {
                ClearAndGrubAreaPassiveModules = Convert.ToDouble(data.Data["ClearAndGrubAreaPassive_SharedData"]);
            }
        }

        public void UpdateFoundationAreaDepthPassiveAndActiveModules()
        {
            foreach (var data in SharedDataCollection.ToList())
            {
                FoundationAreaPassiveAndActiveModules = Convert.ToDouble(data.Data["FoundationAreaPassiveAndActive_SharedData"]);
                FoundationAreaTimesDepthPassiveAndActiveModules = Convert.ToDouble(data.Data["FoundationAreaTimesDepthPassiveAndActive_SharedData"]);
            }
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
            Dummy = random.Next(1000);

            UpdateCapitalCostPassiveAndActiveModules();
            UpdateClearAndGrubAreaPassiveModules();
            UpdateFoundationAreaDepthPassiveAndActiveModules();
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

        public SiteDevelopmentViewModel(IDialogCoordinator dialogCoordinator)
        {
            // MahApps dialog coordinator
            _dialogCoordinator = dialogCoordinator;

            // Assign the proper functions to the open and save commands
            OpenCommand = new RelayCommand(OpenFile);
            SaveCommand = new RelayCommand(SaveFile);
            HelpCommand = new RelayCommand(ShowHelp);
            SyncCommand = new RelayCommand(SyncWithMainUi);

            SetRoadMaterialCommand = new RelayCommandWithParameter(SetRoadMaterial);
            SetParkingAreaMaterialCommand = new RelayCommandWithParameter(SetParkingAreaMaterial);
            SetFoundationSiteSoilCommand = new RelayCommandWithParameter(SetFoundationSiteSoil);
            SetESControlCommand = new RelayCommandWithParameter(SetESControl);

            // Get a list of property names and filter the names to include only those that start with "Calc" in order
            // to use with the NotifyAndChange.  This eliminates the need to write every property name that needs 
            // to be notified of changes by the user.
            PropertiesStringList = Helpers.GetNamesOfClassProperties(this);
            CalcPropertiesStringArray = Helpers.FilterPropertiesList(PropertiesStringList, "Calc");

            // Initialize the model name and user name
            ModuleType = "Site Development";
            ModuleName = "Untitled module";
            ModuleTreatmentType = "Project";
            ModuleId = random.Next(1000);

            IsError = false;
            IsMajorError = false;
            ErrorMessage = "";
            ErrorMessageShort = "";

            // Initialize radio buttons
            LandQuantityOptionsProperty = RadioButtonsLandQuantityOptionsEnum.OptionEstimate;
            LandCostOptionsProperty = RadioButtonsLandCostOptionsEnum.OptionPurchase;
            ESControlOptionsProperty = RadioButtonsESControlOptionsEnum.OptionEstimate;
            FoundationVolumeOptionsProperty = RadioButtonsFoundationVolumeOptionsEnum.OptionEstimate;
            ControlBuildingOptionsProperty = RadioButtonsControlBuildingOptionsEnum.OptionEstimate;
            FencingOptionsProperty = RadioButtonsFencingOptionsEnum.OptionKnown;
            ContingencyOptionsProperty = RadioButtonsContingencyOptionsEnum.OptionEstimate;

            AnnualCostOperationOptionsProperty = RadioButtonsAnnualCostOperationOptionsEnum.OptionEstimated;
            AnnualCostRepairsOptionsProperty = RadioButtonsAnnualCostRepairsOptionsEnum.OptionEstimated;

            // Initialize checkboxes
            IsConstructionOfficeTrailer = true;
            IsFoundationOverExcavation = true;
            IsAccessRoad = true;
            IsAccessRoadGeotextile = true;
            IsParkingArea = true;
            IsESControlSiltFenceCompostFilterSock = true;
            IsSedimentBasins = true;
            IsESControlOther = true;
            IsRockLinedDitch = true;
            IsVegetatedDitch = true;
            IsCulvert = true;
            IsLab = false;
            IsWaterSewer = false;
            IsHVAC = true;
            IsStepdownTransformer = true;
            IsTelecommunications = true;
            IsPlantAutomation = true;
            IsElectricOtherUtility = true;
            IsAccessGate = true;
            IsAnnualCostOperationOther = true;

            // TEMPORARY ASSIGNMENT UNTIL IMPLEMENTED FULLY
            ClearAndGrubAreaPassiveModules = 1;
            FoundationAreaPassiveAndActiveModules = 1;
            FoundationAreaTimesDepthPassiveAndActiveModules = 1;

            CapitalCostPassiveModules = 1;
            CapitalCostActiveModules = 1;
            CapitalCostPassiveAndActiveModules = 1;

            // Dummy variables to force syncing
            Dummy = 0;

            // Read the xml data file and assign property values accordingly.
            OpenXmlAndAssignValues(@"..\..\Data\default-data-site-development.xml");

            // Road Materials
            RoadMaterialName = RoadMaterialNameAggregate;
            RoadMaterialUnitCost = RoadMaterialUnitCostAggregate;

            RoadMaterials = new List<GeneralCostItem>
            {
                new GeneralCostItem {Name = RoadMaterialNameAggregate, Cost = RoadMaterialUnitCostAggregate },
                new GeneralCostItem {Name = RoadMaterialNameAsphalt, Cost = RoadMaterialUnitCostAsphalt },
            };

            // Parking Materials
            ParkingAreaMaterialName = ParkingAreaMaterialNameAsphalt;
            ParkingMaterialUnitCost = RoadMaterialUnitCostAsphalt;  //parking area cost uses aggregate or asphalt unit cost of road material

            ParkingAreaMaterials = new List<GeneralCostItem>
            {
                new GeneralCostItem {Name = ParkingAreaMaterialNameAggregate, Cost = RoadMaterialUnitCostAggregate },
                new GeneralCostItem {Name = ParkingAreaMaterialNameAsphalt, Cost = RoadMaterialUnitCostAsphalt },
            };

            // Foundation Site Soil Materials
            FoundationSiteSoilName = FoundationSiteSoilNamePoor;
            FoundationSiteSoilFactor = FoundationSiteSoilFactorPoor;

            FoundationSiteSoils = new List<GeneralItem>
            {
                new GeneralItem {Name = FoundationSiteSoilNameExcellent, Value = FoundationSiteSoilFactorExcellent },
                new GeneralItem {Name = FoundationSiteSoilNameAverage, Value = FoundationSiteSoilFactorAverage },
                new GeneralItem {Name = FoundationSiteSoilNamePoor, Value = FoundationSiteSoilFactorPoor },
            };

            // ES Controls
            ESControlName = ESControlNameCompostFilterSock;

            ESControlMaterials = new List<GeneralItem>
            {
                new GeneralItem {Name = ESControlNameCompostFilterSock },
                new GeneralItem {Name = ESControlNameSiltFence }
            };

            // Recapitalization parameters 
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
