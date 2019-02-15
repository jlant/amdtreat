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
    public static class PumpingCalculations
    {

        #region Sizing Summary

        public static double CalcDynamicPipeLosses(double pipeLayingLength, double pipeInsideDiameter, double flowRate)
        {
            return (0.0009015 * pipeLayingLength / (Math.Pow(pipeInsideDiameter, 4.8655)) * (Math.Pow((100 * flowRate / 150), 1.85))) * 2.309;
        }

        public static double CalcTotalDynamicHead(double dynamicPipeLoses, double totalStaticHead, double incidentalHeadLosses)
        {
            return (dynamicPipeLoses + totalStaticHead) * (1 + (incidentalHeadLosses / 100));
        }

        public static double CalcTotalDynamicHeadPressure(double totalDynamicHead)
        {
            return totalDynamicHead / 2.309;
        }

        public static double CalcFluidPipeVelocity(double flowRate, double pipeInsideDiameter)
        {
            return flowRate / 7.481 / 60 / (Math.Pow((pipeInsideDiameter / 24), 2)) / Math.PI;
        }

        public static double CalcShaftHorsePower(double flowRate, double totalDynamicHead, double estimatedPumpEfficiency, double pumpSizingSafetyFactor)
        {
            return flowRate * totalDynamicHead * 144 / 2.3 / 7.48 / 60 / 550 / estimatedPumpEfficiency / (1 - pumpSizingSafetyFactor);
        }

        #endregion

        #region Gravel Pipe Bedding

        public static double CalcGravelPipeBeddingWeight(double nominalPipeOutsideDiameter, double pipeLayingLength, double pipeBeddingThickness)
        {
            return ((nominalPipeOutsideDiameter / 12 + 2) * (pipeBeddingThickness / 12.0) * pipeLayingLength) * 95 / 2000;
        }
        #endregion

        #region Capital Costs

        public static decimal CalcBoreholeCost(double wellQuantity, double drillingDepthToMinePool, double boreholeSizingMultiplier, decimal drillingBoreholeAndCastingInstallationCost)
        {
            return (decimal)wellQuantity * (decimal)drillingDepthToMinePool * (decimal)boreholeSizingMultiplier * drillingBoreholeAndCastingInstallationCost;
        }

        public static decimal CalcExcavationCost(double nominalPipeOutsideDiameter, double pipeLayingLength, decimal excavationUnitCost)
        {
            return (((decimal)nominalPipeOutsideDiameter + 24) * ((decimal)nominalPipeOutsideDiameter + 42)) / 144 / 27 * (decimal)pipeLayingLength * excavationUnitCost;
        }

        public static decimal CalcBackfillAndCompactionCost(double nominalPipeOutsideDiameter, double pipeLayingLength, decimal backfillAndCompactionUnitCost)
        {
            return (((decimal)nominalPipeOutsideDiameter + 24) * ((decimal)nominalPipeOutsideDiameter + 42) / 144 / 27 * (decimal)pipeLayingLength * backfillAndCompactionUnitCost) + ((((decimal)nominalPipeOutsideDiameter / 12) + (decimal)(2 * 1.5 * pipeLayingLength)) / 27 * backfillAndCompactionUnitCost);
        }

        public static decimal CalcPipeBeddingCost(double gravelForPipeBeddingWeight, decimal gravelForPipeBeddingUnitCost)
        {
            return (decimal)gravelForPipeBeddingWeight * gravelForPipeBeddingUnitCost;
        }

        public static decimal CalcPipeCostNOD8(double pipeLayingLength)
        {
            return (decimal)(pipeLayingLength * 8.75 + ((Math.Round(pipeLayingLength / 4480, 0) + 1) * 800));
        }

        public static decimal CalcPipeCostNOD10(double pipeLayingLength)
        {
            return (decimal)(pipeLayingLength * 13.75 + ((Math.Round(pipeLayingLength / 2880, 0) + 1) * 800));
        }

        public static decimal CalcPipeCostNOD12(double pipeLayingLength)
        {
            return (decimal)(pipeLayingLength * 19 + ((Math.Round(pipeLayingLength / 2440, 0) + 1) * 800));
        }

        public static decimal CalcPipeCostNOD16(double pipeLayingLength)
        {
            return (decimal)(pipeLayingLength * 29.75 + ((Math.Round(pipeLayingLength / 1200, 0) + 1) * 800));
        }

        public static decimal CalcPipeCostNOD18(double pipeLayingLength)
        {
            return (decimal)(pipeLayingLength * 37.5 + ((Math.Round(pipeLayingLength / 1000, 0) + 1) * 800));
        }

        public static decimal CalcPipeFusionAndInstallationCost(double pipeLayingLength, decimal pipeFusionCost)
        {
            return (decimal)pipeLayingLength * pipeFusionCost;
        }

        public static decimal CalcAirVacuumReleaseAssembliesCost(double airVacuumReleaseAssembliesQuantity, decimal airVacuumReleaseAssembliesUnitCost)
        {
            return (decimal)airVacuumReleaseAssembliesQuantity * airVacuumReleaseAssembliesUnitCost;
        }

        public static decimal CalcTotalVerticalConveyancePipelineCost(decimal excavationCost, decimal backfillAndCompactionCost, decimal pipeBeddingCost, decimal pipeCost, decimal pipeFusionAndInstallationCost, decimal airVacuumReleaseAssembiesCost)
        {
            return excavationCost + backfillAndCompactionCost + pipeBeddingCost + pipeCost + pipeFusionAndInstallationCost + airVacuumReleaseAssembiesCost;
        }

        public static decimal CalcControlPanelAndScadaCost(double calculatedShaftHorsePower)
        {
            if (calculatedShaftHorsePower < 200)
            {
                return (decimal)calculatedShaftHorsePower * 85;
            }
            else
            {
                return (decimal)calculatedShaftHorsePower * 125;
            }
        }

        public static decimal CalcPumpCost(double calculatedShaftHorsePower)
        {
            if (calculatedShaftHorsePower < 150)
            {
                return (decimal)calculatedShaftHorsePower * 950;
            }
            else if (calculatedShaftHorsePower < 300 && calculatedShaftHorsePower > 150)
            {
                return (decimal)calculatedShaftHorsePower * 625;
            }
            else // calculatedShaftHorsePower >= 300
            {
                return (decimal)calculatedShaftHorsePower * 425;
            }

        }

        public static decimal CalcConcretePadCost(double pumpQuantity, decimal concreteUnitCost)
        {
            return concreteUnitCost * (5 * 5 * (decimal)0.6667) / 27 * (decimal)pumpQuantity * (decimal)2.5;
        }

        public static decimal CalcSoftStartVfdCost(double calculatedShaftHorsePower)
        {
            if (calculatedShaftHorsePower < 200)
            {
                return (decimal)calculatedShaftHorsePower * 350;
            }
            else
            {
                return (decimal)calculatedShaftHorsePower * 250;
            }
        }

        public static decimal CalcTotalVerticalTurbinePumpCost(decimal controlPanelAndScadaCost, decimal pumpCost, decimal concretePadCost, decimal softStartVfdCost, double pumpQuantity)
        {
            return (controlPanelAndScadaCost + pumpCost + concretePadCost + softStartVfdCost) * (decimal)pumpQuantity;
        }

        public static decimal CalcCapitalCostTotal(decimal boreholeCost, decimal totalVerticalConveyancePipelineCost, decimal totalVerticalTurbinePumpCost)
        {
            return boreholeCost + totalVerticalConveyancePipelineCost + totalVerticalTurbinePumpCost;
        }

        #endregion

        #region Annual (Operations and Maintenance) Costs


        public static decimal CalcAnnualCostElectric(decimal electricalUnitCost, double pumpingTimeHoursPerDay, double pumpingTimeDaysPerYear, double calculatedShaftHorsePower)
        {
            return (decimal)calculatedShaftHorsePower * (decimal)0.7456 * (decimal)pumpingTimeHoursPerDay * (decimal)pumpingTimeDaysPerYear * electricalUnitCost;
        }

        public static decimal CalcAnnualCostPumpMaintenance(decimal pumpCost, double pumpingTimeHoursPerDay, double pumpingTimeDaysPerYear, double pumpMaintenanceFactor)
        {
            return ((decimal)(pumpingTimeHoursPerDay * pumpingTimeDaysPerYear) / (24 * 365)) * ((decimal)pumpMaintenanceFactor / 100) * pumpCost;
        }

        public static decimal CalcAnnualCostPipelineMaintenance(decimal totalVerticalConveyancePipelineCost, double pipelineMaintenanceFactor)
        {
            return ((decimal)pipelineMaintenanceFactor / 100) * totalVerticalConveyancePipelineCost;
        }


        public static decimal CalcAnnualCostTotal(decimal annualElectricCost, decimal annualPumpMaintenanceCost, decimal annualPipelineMaintenanceCost)
        {
            return annualElectricCost + annualPumpMaintenanceCost + annualPipelineMaintenanceCost;
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

        #endregion

    }
}
