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

    public class ConveyanceDitchViewModel : PropertyChangedBase, IObserver<SharedData>
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

        #region Properties - Conveyance Ditch

        private bool _isAggregate;
        /// <summary>
        ///  User specified 
        /// </summary>
        public bool IsAggregate
        {
            get { return _isAggregate; }
            set { ChangeAndNotify(ref _isAggregate, value, nameof(IsAggregate), CalcPropertiesStringArray); }
        }

        private bool _isGrass;
        /// <summary>
        ///  User specified 
        /// </summary>
        public bool IsGrass
        {
            get { return _isGrass; }
            set { ChangeAndNotify(ref _isGrass, value, nameof(IsGrass), CalcPropertiesStringArray); }
        }

        private double _ditchLengthAggregate;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double DitchLengthAggregate
        {
            get { return _ditchLengthAggregate; }
            set { ChangeAndNotify(ref _ditchLengthAggregate, value, nameof(DitchLengthAggregate), CalcPropertiesStringArray); }
        }

        private double _ditchLengthGrass;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double DitchLengthGrass
        {
            get { return _ditchLengthGrass; }
            set { ChangeAndNotify(ref _ditchLengthGrass, value, nameof(DitchLengthGrass), CalcPropertiesStringArray); }
        }

        private double _ditchBottomWidth;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double DitchBottomWidth
        {
            get { return _ditchBottomWidth; }
            set { ChangeAndNotify(ref _ditchBottomWidth, value, nameof(DitchBottomWidth), CalcPropertiesStringArray); }
        }

        private double _ditchBottomSlope;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double DitchBottomSlope
        {
            get { return _ditchBottomSlope; }
            set { ChangeAndNotify(ref _ditchBottomSlope, value, nameof(DitchBottomSlope), CalcPropertiesStringArray); }
        }

        private double _ditchSideSlope;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double DitchSideSlope
        {
            get { return _ditchSideSlope; }
            set { ChangeAndNotify(ref _ditchSideSlope, value, nameof(DitchSideSlope), CalcPropertiesStringArray); }
        }

        private double _ditchDepth;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double DitchDepth
        {
            get { return _ditchDepth; }
            set { ChangeAndNotify(ref _ditchDepth, value, nameof(DitchDepth), CalcPropertiesStringArray); }
        }

        private double _aggregateDepth;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double AggregateDepth
        {
            get { return _aggregateDepth; }
            set { ChangeAndNotify(ref _aggregateDepth, value, nameof(AggregateDepth), CalcPropertiesStringArray); }
        }

        private double _aggregatePorosity;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double AggregatePorosity
        {
            get { return _aggregatePorosity; }
            set { ChangeAndNotify(ref _aggregatePorosity, value, nameof(AggregatePorosity), CalcPropertiesStringArray); }
        }

        private double _ditchSideSlopesRatio;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double DitchSideSlopesRatio
        {
            get { return _ditchSideSlopesRatio; }
            set { ChangeAndNotify(ref _ditchSideSlopesRatio, value, nameof(DitchSideSlopesRatio), CalcPropertiesStringArray); }
        }

        private double _nonWovenGeotextileLength;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double NonWovenGeotextileLength
        {
            get { return _nonWovenGeotextileLength; }
            set { ChangeAndNotify(ref _nonWovenGeotextileLength, value, nameof(NonWovenGeotextileLength), CalcPropertiesStringArray); }
        }

        private decimal _nonWovenGeotextileUnitCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal NonWovenGeotextileUnitCost
        {
            get { return _nonWovenGeotextileUnitCost; }
            set { ChangeAndNotify(ref _nonWovenGeotextileUnitCost, value, nameof(NonWovenGeotextileUnitCost), CalcPropertiesStringArray); }
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

        private decimal _aggregatePlacementUnitCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal AggregatePlacementUnitCost
        {
            get { return _aggregatePlacementUnitCost; }
            set { ChangeAndNotify(ref _aggregatePlacementUnitCost, value, nameof(AggregatePlacementUnitCost), CalcPropertiesStringArray); }
        }

        private decimal _grassLiningUnitCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal GrassLiningUnitCost
        {
            get { return _grassLiningUnitCost; }
            set { ChangeAndNotify(ref _grassLiningUnitCost, value, nameof(GrassLiningUnitCost), CalcPropertiesStringArray); }
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

        private decimal _revegetationUnitCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal RevegetationUnitCost
        {
            get { return _revegetationUnitCost; }
            set { ChangeAndNotify(ref _revegetationUnitCost, value, nameof(RevegetationUnitCost), CalcPropertiesStringArray); }
        }

        private double _siltFenceLength;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double SiltFenceLength
        {
            get { return _siltFenceLength; }
            set { ChangeAndNotify(ref _siltFenceLength, value, nameof(SiltFenceLength), CalcPropertiesStringArray); }
        }

        private decimal _siltFenceUnitCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal SiltFenceUnitCost
        {
            get { return _siltFenceUnitCost; }
            set { ChangeAndNotify(ref _siltFenceUnitCost, value, nameof(SiltFenceUnitCost), CalcPropertiesStringArray); }
        }
        #endregion

        #region Properties - Sizing Summary

        private double _calcTotalDitchLength;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcTotalDitchLength
        {
            get
            {
                if (IsAggregate && IsGrass)
                {
                    _calcTotalDitchLength = ConveyanceDitchCalculations.CalcTotalDitchLength(DitchLengthAggregate, DitchLengthGrass);
                }
                else if (IsAggregate && !IsGrass)
                {
                    _calcTotalDitchLength = ConveyanceDitchCalculations.CalcTotalDitchLength(DitchLengthAggregate, 0);
                }
                else if (!IsAggregate && IsGrass)
                {
                    _calcTotalDitchLength = ConveyanceDitchCalculations.CalcTotalDitchLength(0, DitchLengthGrass);
                }
                else
                {
                    _calcTotalDitchLength = ConveyanceDitchCalculations.CalcTotalDitchLength(0, 0);
                }
                return _calcTotalDitchLength;
            }
            set { ChangeAndNotify(ref _calcTotalDitchLength, value); }
        }

        private double _calcDitchVolume;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcDitchVolume
        {
            get { return ConveyanceDitchCalculations.CalcDitchVolume(DitchBottomWidth, DitchDepth, DitchSideSlope, CalcTotalDitchLength); }
            set { ChangeAndNotify(ref _calcDitchVolume, value); }
        }

        private double _calcClearAndGrubTopArea;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcClearAndGrubTopArea
        {
            get { return ConveyanceDitchCalculations.CalcClearAndGrubTopArea(DitchBottomWidth, DitchSideSlope, DitchDepth); }
            set { ChangeAndNotify(ref _calcClearAndGrubTopArea, value); }
        }

        private double _calcClearAndGrubArea;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcClearAndGrubArea
        {
            get { return ConveyanceDitchCalculations.CalcClearAndGrubArea(CalcClearAndGrubTopArea, CalcTotalDitchLength); }
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

        private double _calcDitchSlopeLength;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcDitchSlopeLength
        {
            get { return ConveyanceDitchCalculations.CalcDitchSlopeLength(DitchBottomWidth, DitchSideSlope, DitchDepth); }
            set { ChangeAndNotify(ref _calcDitchSlopeLength, value); }
        }

        private double _calcDitchElevationChange;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcDitchElevationChange
        {
            get { return ConveyanceDitchCalculations.CalcDitchElevationChange(DitchBottomSlope, CalcTotalDitchLength); }
            set { ChangeAndNotify(ref _calcDitchVolume, value); }
        }

        private double _calcAggregateDensity;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcAggregateDensity
        {
            get { return ConveyanceDitchCalculations.CalcAggregateDensity(AggregatePorosity); }
            set { ChangeAndNotify(ref _calcAggregateDensity, value); }
        }

        private double _calcAggregateVolume;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcAggregateVolume
        {
            get
            {
                if (IsAggregate)
                {
                    _calcAggregateVolume = ConveyanceDitchCalculations.CalcAggregateVolume(DitchLengthAggregate, DitchBottomWidth, AggregateDepth, DitchSideSlope);
                }
                else
                {
                    _calcAggregateVolume = 0;
                }
                return _calcAggregateVolume;
            }
            set { ChangeAndNotify(ref _calcAggregateVolume, value); }
        }

        private double _calcAggregateWeight;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcAggregateWeight
        {
            get { return ConveyanceDitchCalculations.CalcAggregateWeight(CalcAggregateVolume, CalcAggregateDensity); }
            set { ChangeAndNotify(ref _calcAggregateWeight, value); }
        }

        private double _calcRevegetationAggregateArea;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcRevegetationAggregateArea
        {
            get
            {
                if (IsAggregate)
                {
                    _calcRevegetationAggregateArea = ConveyanceDitchCalculations.CalcRevegetationAggregateArea(DitchLengthAggregate, CalcDitchSlopeLength);
                }
                else
                {
                    _calcRevegetationAggregateArea = ConveyanceDitchCalculations.CalcRevegetationAggregateArea(0, CalcDitchSlopeLength);
                }
                return _calcRevegetationAggregateArea;
            }
            set { ChangeAndNotify(ref _calcRevegetationAggregateArea, value); }
        }

        private double _calcRevegetationGrassArea;
        /// <summary>
        /// Calculated
        /// </summary>
        public double CalcRevegetationGrassArea
        {
            get
            {
                if (IsGrass)
                {
                    _calcRevegetationGrassArea = ConveyanceDitchCalculations.CalcRevegetationGrassArea(DitchLengthGrass, CalcDitchSlopeLength);
                }
                else
                {
                    _calcRevegetationGrassArea = ConveyanceDitchCalculations.CalcRevegetationGrassArea(0, CalcDitchSlopeLength);
                }
                return _calcRevegetationGrassArea;
            }
            set { ChangeAndNotify(ref _calcRevegetationGrassArea, value); }
        }

        #endregion

        #region Properties - Capital Costs

        private decimal _calcExcavationCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcExcavationCost
        {
            get { return ConveyanceDitchCalculations.CalcExcavationCost(ExcavationUnitCost, CalcDitchVolume); }
            set { ChangeAndNotify(ref _calcExcavationCost, value); }
        }

        private decimal _calcAggregateCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcAggregateCost
        {
            get { return ConveyanceDitchCalculations.CalcAggregateCost(CalcAggregateWeight, AggregateUnitCost); }
            set { ChangeAndNotify(ref _calcAggregateCost, value); }
        }

        private decimal _calcAggregatePlacementCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcAggregatePlacementCost
        {
            get { return ConveyanceDitchCalculations.CalcAggregatePlacementCost(CalcAggregateVolume, AggregatePlacementUnitCost); }
            set { ChangeAndNotify(ref _calcAggregatePlacementCost, value); }
        }

        private decimal _calcGrassLiningCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcGrassLiningCost
        {
            get
            {
                if (IsGrass)
                {
                    _calcGrassLiningCost = ConveyanceDitchCalculations.CalcGrassLiningCost(CalcDitchSlopeLength, DitchLengthGrass, GrassLiningUnitCost);
                }
                else
                {
                    _calcGrassLiningCost = 0m;
                }
                return _calcGrassLiningCost;
            }
            set { ChangeAndNotify(ref _calcGrassLiningCost, value); }
        }

        private decimal _calcNonWovenGeotextileCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcNonWovenGeotextileCost
        {
            get { return ConveyanceDitchCalculations.CalcNonWovenGeotextileCost(CalcDitchSlopeLength, NonWovenGeotextileLength, NonWovenGeotextileUnitCost); }
            set { ChangeAndNotify(ref _calcNonWovenGeotextileCost, value); }
        }

        private decimal _calcSiltFenceCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcSiltFenceCost
        {
            get { return ConveyanceDitchCalculations.CalcSiltFenceCost(SiltFenceLength, SiltFenceUnitCost); }
            set { ChangeAndNotify(ref _calcSiltFenceCost, value); }
        }

        private decimal _calcRevegetationAggregateCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRevegetationAggregateCost
        {
            get { return ConveyanceDitchCalculations.CalcRevegetationAggregateCost(CalcRevegetationAggregateArea, RevegetationUnitCost); }
            set { ChangeAndNotify(ref _calcRevegetationAggregateCost, value); }
        }

        private decimal _calcRevegetationGrassCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRevegetationGrassCost
        {
            get { return ConveyanceDitchCalculations.CalcRevegetationGrassCost(CalcRevegetationGrassArea, RevegetationUnitCost); }
            set { ChangeAndNotify(ref _calcRevegetationGrassCost, value); }
        }

        private decimal _calcRevegetationCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRevegetationCost
        {
            get { return ConveyanceDitchCalculations.CalcRevegetationCost(CalcRevegetationAggregateCost, CalcRevegetationGrassCost); }
            set { ChangeAndNotify(ref _calcRevegetationCost, value); }
        }

        private decimal _calcCapitalCostTotal;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcCapitalCostTotal
        {
            get
            {
                _calcCapitalCostTotal = ConveyanceDitchCalculations.CalcCapitalCostTotal(CalcExcavationCost, CalcAggregateCost, CalcGrassLiningCost, CalcNonWovenGeotextileCost, CalcSiltFenceCost, CalcRevegetationCost);

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
        public enum RadioButtonsAnnualCostOptionsEnum
        {
            OptionAnnualCostMultiplier,
            OptionAnnualCostFlatFee
        }

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
                        _calcAnnualCost = ConveyanceDitchCalculations.CalcAnnualCost(AnnualCostMultiplier, CalcCapitalCostTotal);
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

        private double _recapitalizationCostLifeCycleAggregate;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostLifeCycleAggregate
        {
            get { return _recapitalizationCostLifeCycleAggregate; }
            set { ChangeAndNotify(ref _recapitalizationCostLifeCycleAggregate, value, nameof(RecapitalizationCostLifeCycleAggregate), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostLifeCycleNonWovenGeotextile;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostLifeCycleNonWovenGeotextile
        {
            get { return _recapitalizationCostLifeCycleNonWovenGeotextile; }
            set { ChangeAndNotify(ref _recapitalizationCostLifeCycleNonWovenGeotextile, value, nameof(RecapitalizationCostLifeCycleNonWovenGeotextile), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostLifeCycleGrassLining;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostLifeCycleGrassLining
        {
            get { return _recapitalizationCostLifeCycleGrassLining; }
            set { ChangeAndNotify(ref _recapitalizationCostLifeCycleGrassLining, value, nameof(RecapitalizationCostLifeCycleGrassLining), CalcPropertiesStringArray); }
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

        private double _recapitalizationCostPercentReplacementNonWovenGeotextile;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostPercentReplacementNonWovenGeotextile
        {
            get { return _recapitalizationCostPercentReplacementNonWovenGeotextile; }
            set { ChangeAndNotify(ref _recapitalizationCostPercentReplacementNonWovenGeotextile, value, nameof(RecapitalizationCostPercentReplacementNonWovenGeotextile), CalcPropertiesStringArray); }
        }

        private double _recapitalizationCostPercentReplacementGrassLining;
        /// <summary>
        /// User specified
        /// </summary>
        public double RecapitalizationCostPercentReplacementGrassLining
        {
            get { return _recapitalizationCostPercentReplacementGrassLining; }
            set { ChangeAndNotify(ref _recapitalizationCostPercentReplacementGrassLining, value, nameof(RecapitalizationCostPercentReplacementGrassLining), CalcPropertiesStringArray); }
        }

        private decimal _calcRecapitalizationAggregateMaterialCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationAggregateMaterialCost
        {
            get { return ConveyanceDitchCalculations.CalcRecapitalizationAggregateMaterialCost(CalcAggregateCost, CalcAggregatePlacementCost); }
            set { ChangeAndNotify(ref _calcRecapitalizationAggregateMaterialCost, value); }
        }

        private decimal _calcRecapitalizationGrassLiningMaterialCost;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationGrassLiningMaterialCost
        {
            get { return ConveyanceDitchCalculations.CalcRecapitalizationGrassLiningMaterialCost(CalcGrassLiningCost, CalcRevegetationGrassCost); }
            set { ChangeAndNotify(ref _calcRecapitalizationGrassLiningMaterialCost, value); }
        }

        private decimal _calcRapitalizationCostAggregate;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostAggregate
        {
            get
            {
                return ConveyanceDitchCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
                                                                    RecapitalizationCostInflationRate, RecapitalizationCostLifeCycleAggregate,
                                                                    CalcRecapitalizationAggregateMaterialCost, RecapitalizationCostPercentReplacementAggregate);
            }
            set { ChangeAndNotify(ref _calcRapitalizationCostAggregate, value); }
        }

        private decimal _calcRapitalizationCostNonWovenGeotextile;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostNonWovenGeotextile
        {
            get
            {
                return ConveyanceDitchCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
                                                                        RecapitalizationCostInflationRate, RecapitalizationCostLifeCycleNonWovenGeotextile,
                                                                        CalcNonWovenGeotextileCost, RecapitalizationCostPercentReplacementNonWovenGeotextile);
            }
            set { ChangeAndNotify(ref _calcRapitalizationCostNonWovenGeotextile, value); }
        }

        private decimal _calcRapitalizationCostGrassLining;
        /// <summary>
        /// Calculated
        /// </summary>
        public decimal CalcRecapitalizationCostGrassLining
        {
            get
            {
                return ConveyanceDitchCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
                                                                        RecapitalizationCostInflationRate, RecapitalizationCostLifeCycleGrassLining,
                                                                        CalcRecapitalizationGrassLiningMaterialCost, RecapitalizationCostPercentReplacementGrassLining);
            }
            set { ChangeAndNotify(ref _calcRapitalizationCostGrassLining, value); }
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
            get { return ConveyanceDitchCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn, 
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
                    case "Aggregate":
                        item.MaterialCostDefault = CalcRecapitalizationAggregateMaterialCost;
                        break;
                    case "NonWovenGeotextile":
                        item.MaterialCostDefault = CalcNonWovenGeotextileCost;
                        break;
                    case "GrassLining":
                        item.MaterialCostDefault = CalcRecapitalizationGrassLiningMaterialCost;
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
                    case "Aggregate":
                        item.TotalCost = CalcRecapitalizationCostAggregate;
                        break;
                    case "NonWovenGeotextile":
                        item.TotalCost = CalcRecapitalizationCostNonWovenGeotextile;
                        break;
                    case "GrassLining":
                        item.TotalCost = CalcRecapitalizationCostGrassLining;
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
                Name = "Aggregate",
                NameFixed = "Aggregate",
                LifeCycle = RecapitalizationCostLifeCycleAggregate,
                PercentReplacement = RecapitalizationCostPercentReplacementAggregate,
                MaterialCostDefault = CalcRecapitalizationAggregateMaterialCost,
                UseCustomCost = false,
                MaterialCostCustom = 0m,
                TotalCost = CalcRecapitalizationCostAggregate
            });
            RecapMaterials.Add(new RecapMaterial
            {
                IsSelected = true,
                Name = "Non Woven Geotextile",
                NameFixed = "NonWovenGeotextile",
                LifeCycle = RecapitalizationCostLifeCycleNonWovenGeotextile,
                PercentReplacement = RecapitalizationCostPercentReplacementNonWovenGeotextile,
                MaterialCostDefault = CalcNonWovenGeotextileCost,
                UseCustomCost = false,
                MaterialCostCustom = 0m,
                TotalCost = CalcRecapitalizationCostNonWovenGeotextile
            });
            RecapMaterials.Add(new RecapMaterial
            { 
                IsSelected = true,
                Name = "Grass Lining",
                NameFixed = "GrassLining",
                LifeCycle = RecapitalizationCostLifeCycleGrassLining,
                PercentReplacement = RecapitalizationCostPercentReplacementGrassLining,
                MaterialCostDefault = CalcRecapitalizationGrassLiningMaterialCost,
                UseCustomCost = false,
                MaterialCostCustom = 0m,
                TotalCost = CalcRecapitalizationCostGrassLining
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
            ((RecapMaterial)sender).TotalCost = ConveyanceDitchCalculations.CalcRecapitalizationCost(RecapitalizationCostCalculationPeriod, RecapitalizationCostNetRateOfReturn,
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
            var customDialog = new CustomDialog() { Title = "About Conveyance Ditch" };

            var customDialogViewModel = new CustomDialogViewModel(instance =>
            {
                _dialogCoordinator.HideMetroDialogAsync(this, customDialog);
            });
            customDialogViewModel.Message = Resources.infoCD;
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
                        string message = Resources.infoSystemPropertiesCD;
                        await _dialogCoordinator.ShowMessageAsync(this, "Conveyance Ditch", message);
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
                        string message = Resources.infoSizingSummaryCD;
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
                        string message = Resources.infoCapitalCostCD;
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
                        string message = Resources.infoAnnualCostCD;
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
                        string message = Resources.infoRecapitalizationCostCD;
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
            ErrorMessageShort = "The pressure rating does not fit within the recommended range of 125-335 PSI. Calculated pressure class does not Match SDR. To use a HDPE, adjust SDR to match pressure conditions.";
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

        public ConveyanceDitchViewModel(IDialogCoordinator dialogCoordinator)
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
            ModuleType = "Conveyance Ditch";
            ModuleName = "Untitled module";
            ModuleTreatmentType = "Project";
            ModuleId = random.Next(1000);

            IsError = false;
            IsMajorError = false;
            ErrorMessage = "";
            ErrorMessageShort = "";

            // Initialize radio buttons
            AnnualCostOptionsProperty = RadioButtonsAnnualCostOptionsEnum.OptionAnnualCostMultiplier;

            // Initialize checkboxes
            IsAggregate = true;
            IsGrass = true;

            // Dummy variables to force syncing
            Dummy = 0;

            // Read the xml data file and assign property values accordingly.
            OpenXmlAndAssignValues(@"..\..\Data\default-data-conveyance-ditch.xml");

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
