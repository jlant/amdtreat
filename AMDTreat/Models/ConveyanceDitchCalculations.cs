using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMDTreat.Models
{
    /// <summary>
    /// Calculations for Caustic Soda.
    /// </summary>
    public static class ConveyanceDitchCalculations
    {

        #region Sizing Summary

        public static double CalcTotalDitchLength(double ditchLengthAggregate, double ditchLengthGrass)
        {
            return ditchLengthAggregate + ditchLengthGrass;
        }

        public static double CalcDitchVolume(double ditchBottomWidth, double ditchDepth, double ditchSideSlope, double ditchLength)
        {
            return ((ditchBottomWidth * ditchDepth) + ditchSideSlope * Math.Pow(ditchDepth, 2)) * ditchLength;
        }

        public static double CalcClearAndGrubTopArea(double ditchBottomWidth, double ditchSideSlope, double ditchDepth)
        {
            return ditchBottomWidth + 2 * (ditchSideSlope * ditchDepth);
        }

        public static double CalcClearAndGrubArea(double clearAndGrubTopArea, double ditchLength)
        {
            return (ditchLength * clearAndGrubTopArea * 1.2) / 43560.0;
        }

        public static double CalcDitchSlopeLength(double ditchBottomWidth, double ditchSideSlope, double ditchDepth)
        {
            return ditchBottomWidth + ((2 * ditchDepth) * Math.Sqrt(Math.Pow(ditchSideSlope, 2) + 1));
        }

        public static double CalcDitchElevationChange(double ditchBottomSlope, double ditchLengthTotal)
        {
            return ditchBottomSlope * ditchLengthTotal;
        }

        public static double CalcAggregateDensity(double aggregatePorosity)
        {
            return (1 - (aggregatePorosity / 100.0)) * 165.43;
        }

        public static double CalcAggregateVolume(double ditchLengthAggregate, double ditchBottomWidth, double aggregateDepth, double ditchSideSlope)
        {
            return (ditchLengthAggregate * (ditchBottomWidth * aggregateDepth) + (ditchSideSlope * Math.Pow(aggregateDepth,2))) / 27.0;
        }

        public static double CalcAggregateWeight(double aggregateVolume, double aggregateDensity)
        {
            return (aggregateVolume * (aggregateDensity * 27.0) / 2000.0);
        }

        public static double CalcRevegetationAggregateArea(double ditchLengthAggregate, double ditchSlopeLength)
        {
            return (ditchLengthAggregate * ditchSlopeLength * 0.4) / 43560.0;
        }

        public static double CalcRevegetationGrassArea(double ditchLengthGrass, double ditchSlopeLength)
        {
            return (ditchLengthGrass * ditchSlopeLength * 1.2) / 43560.0;
        }


        #endregion

        #region Capital Costs

        public static decimal CalcExcavationCost(decimal excavationUnitCost, double ditchVolume)
        {
            return (decimal)(ditchVolume / 27.0) * excavationUnitCost;
        }

        public static decimal CalcAggregateCost(double aggregateWeight, decimal aggregateUnitCost)
        {
            return (decimal)aggregateWeight * aggregateUnitCost ;
        }

        public static decimal CalcAggregatePlacementCost(double aggregateVolume, decimal aggregatePlacementUnitCost)
        {
            return (decimal)aggregateVolume * aggregatePlacementUnitCost;
        }

        public static decimal CalcGrassLiningCost(double ditchSlopeLength, double ditchLengthGrass, decimal grassLiningUnitCost)
        {
            return Math.Round((decimal)((ditchSlopeLength * ditchLengthGrass) / 9.0) * grassLiningUnitCost);
        }

        public static decimal CalcNonWovenGeotextileCost(double ditchSlopeLength, double nonWovenGeotextileLength, decimal nonWovenGeotextileUnitCost)
        {
            return Math.Round((decimal)((ditchSlopeLength * nonWovenGeotextileLength) / 9.0) * nonWovenGeotextileUnitCost);
        }

        public static decimal CalcSiltFenceCost(double siltFenceLength, decimal siltFenceUnitCost)
        {
            return (decimal)siltFenceLength * siltFenceUnitCost;
        }

        public static decimal CalcRevegetationAggregateCost(double revegetationAggregateArea, decimal revegetationUnitCost)
        {
            return (decimal)revegetationAggregateArea * revegetationUnitCost;
        }

        public static decimal CalcRevegetationGrassCost(double revegetationGrassArea, decimal revegetationUnitCost)
        {
            return (decimal)revegetationGrassArea * revegetationUnitCost;
        }

        public static decimal CalcRevegetationCost(decimal revegetationAggregateCost, decimal revegetationGrassCost)
        {
            return revegetationAggregateCost + revegetationGrassCost;
        }

        public static decimal CalcCapitalCostTotal(decimal excavationCost, decimal aggregateCost, decimal grassLiningCost, decimal nonWovenGeotextileCost, decimal siltFenceCost, decimal revegetationCost)
        {
            return excavationCost + aggregateCost + grassLiningCost + nonWovenGeotextileCost + siltFenceCost + revegetationCost;
        }

        #endregion

        #region Annual (Operations and Maintenance) Costs

        public static decimal CalcAnnualCost(double annualCostMultiplier, decimal capitalCostTotal)
        {
            return capitalCostTotal * (decimal)(annualCostMultiplier / 100.0);
        }

        #endregion

        #region Recapitalization Costs

        public static decimal CalcRecapitalizationCost(double calculationPeriod, double netRateOfReturn,
                                                       double inflationRate, double lifeCycleOfMaterial,
                                                       decimal materialTotalCost, double percentReplacement)
        {
            int numCalcPeriodsPerMaterial = (int)Math.Floor(calculationPeriod / lifeCycleOfMaterial);
            decimal[] futureValueArray = new decimal[numCalcPeriodsPerMaterial];
            decimal[] presentValueArray = new decimal[numCalcPeriodsPerMaterial];
            double exponent = 0;

            for (int i = 0; i < numCalcPeriodsPerMaterial; i++)
            {
                exponent = (i + 1) * lifeCycleOfMaterial;  // exponent must start at 1 
                futureValueArray[i] = materialTotalCost * (decimal)Math.Pow(1 + (inflationRate / 100.0), exponent);
                presentValueArray[i] = futureValueArray[i] * (decimal)(percentReplacement / 100.0) * (decimal)Math.Pow(1 + (netRateOfReturn / 100.0), -1 * exponent);
            }

            return presentValueArray.Sum();
        }


        public static decimal CalcRecapitalizationAggregateMaterialCost(decimal aggregateCost, decimal aggregatePlacementCost)
        {
            return aggregateCost + aggregatePlacementCost;
        }

        public static decimal CalcRecapitalizationGrassLiningMaterialCost(decimal grassLiningCost, decimal revegetationGrassCost)
        {
            return grassLiningCost + revegetationGrassCost;
        }
        #endregion

    }
}
