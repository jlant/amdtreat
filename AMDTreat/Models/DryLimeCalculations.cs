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
    public static class DryLimeCalculations
    {

        #region Stoichiometric Calculations 

        public static double CalcLimeDailyConsumptionTonsStoichiometric(double designFlow, double hotAcidity, double limePurity, double limeDissolutionEfficiency)
        { 
            return hotAcidity * designFlow * 3.785 / 1000 / 100.1 * 56.1 * 1000 / 453592 / 2000 * 60 * 24 / (limePurity / 100.0) / (limeDissolutionEfficiency / 100.0);
        }

        public static double CalcLimeMonthlyConsumptionTonsStoichiometric(double limeDailyConsumptionTonsStoichiometric)
        {
            return limeDailyConsumptionTonsStoichiometric * 30;
        }

        public static double CalcLimeAnnualConsumptionTonsStoichiometric(double limeDailyConsumptionTonsStoichiometric)
        {
            return limeDailyConsumptionTonsStoichiometric * 365;
        }


        #endregion

        #region Titration Calculations

        public static double CalcLimeDailyConsumptionTonsTitration(double titrationQuantity, double designFlow)
        {
            return titrationQuantity * designFlow * 60 * 24 / 2000;
        }

        public static double CalcLimeMonthlyConsumptionTonsTitration(double limeDailyConsumptionTonsTitration)
        {
            return limeDailyConsumptionTonsTitration * 30;
        }

        public static double CalcLimeAnnualConsumptionTonsTitration(double limeDailyConsumptionTonsTitration)
        {
            return limeDailyConsumptionTonsTitration * 365.25;
        }

        #endregion

        #region User Specified Quantity Calculations

        public static double CalcLimeDailyConsumptionTonsUserSpecified(double limeAnnualConsumptionTonsUserSpecified)
        {
            return limeAnnualConsumptionTonsUserSpecified / 365.25;
        }

        public static double CalcLimeMonthlyConsumptionTonsUserSpecified(double limeAnnualConsumptionTonsUserSpecified)
        {
            return limeAnnualConsumptionTonsUserSpecified / 12;
        }

        public static double CalcLimeAnnualConsumptionTonsUserSpecified(double limeAnnualConsumptionTonsUserSpecified)
        {
            return limeAnnualConsumptionTonsUserSpecified;
        }

        #endregion

        #region Lime Silo

        public static double CalcLimeSiloRefillFrequency(double siloSystemWeight, double limeDailyConsumptionTonsStoichiometric)
        {
            return siloSystemWeight / limeDailyConsumptionTonsStoichiometric;
        }

        public static double CalcSiloFoundationArea(double siloSytemFoundationArea)
        {
            return siloSytemFoundationArea;
        }

        public static double CalcSiloFoundationDepth(double foundationMultiplier, double siloSytemEstimatedConcreteThickness)
        {
             return siloSytemEstimatedConcreteThickness * foundationMultiplier;
        }

        public static double CalcSiloFoundationVolume(double siloFoundationDepth, double siloFoundationArea)
        {
            return (siloFoundationDepth * siloFoundationArea) / 27;
        }

        #endregion

        #region Capital Costs

        public static decimal CalcStorageAndDispensingSystemCost(double siloSystemQuantity, decimal siloSystemUnitCost)
        {
            return (decimal)siloSystemQuantity * siloSystemUnitCost;
        }

        public static decimal CalcFoundationCost(double siloFoundationVolume, decimal foundationConcreteMaterialAndPlacementCost)
        {
            return (decimal)siloFoundationVolume * foundationConcreteMaterialAndPlacementCost;
        }

        public static decimal CalcSystemInstallCost(decimal storageAndDispensingSystemCost, decimal foundationCost, double systemInstallCostMultiplier)
        {
            return (storageAndDispensingSystemCost + foundationCost) * (decimal)systemInstallCostMultiplier;
        }

        public static decimal CalcOtherCapitalItemsCost(double itemQuantity1, decimal itemUnitCost1,
                                         double itemQuantity2, decimal itemUnitCost2,
                                         double itemQuantity3, decimal itemUnitCost3,
                                         double itemQuantity4, decimal itemUnitCost4,
                                         double itemQuantity5, decimal itemUnitCost5)
        {
            return (decimal)itemQuantity1 * itemUnitCost1 + (decimal)itemQuantity2 * itemUnitCost2 + (decimal)itemQuantity3 * itemUnitCost3 + (decimal)itemQuantity4 * itemUnitCost4 + (decimal)itemQuantity5 * itemUnitCost5;
        }

        public static decimal CalcCapitalCostTotal(decimal storageAndDispensingSystemCost, decimal foundationCost, decimal systemInstallCost, decimal otherCapitalItemsCost)
        {
            return storageAndDispensingSystemCost + foundationCost + systemInstallCost + otherCapitalItemsCost;
        }

        public static decimal CalcSiloCost(decimal siloUnitCost, double systemInstallCostMultiplier)
        {
            return siloUnitCost * (decimal)(1 + systemInstallCostMultiplier);
        }

        public static decimal CalcBinActivatorCost(decimal binActivatorUnitCost, double systemInstallCostMultiplier)
        {
            return binActivatorUnitCost * (decimal)(1 + systemInstallCostMultiplier);
        }

        public static decimal CalcScrewFeederCost(decimal screwFeederUnitCost, double systemInstallCostMultiplier)
        {
            return screwFeederUnitCost * (decimal)(1 + systemInstallCostMultiplier);
        }

        public static decimal CalcSlurryMixerCost(decimal slurryMixerUnitCost, double systemInstallCostMultiplier)
        {
            return slurryMixerUnitCost * (decimal)(1 + systemInstallCostMultiplier);
        }

        public static decimal CalcSlurryPumpCost(decimal slurryPumpUnitCost, double systemInstallCostMultiplier)
        {
            return slurryPumpUnitCost * (decimal)(1 + systemInstallCostMultiplier);
        }

        public static decimal CalcSiloSpaceHeaterCost(decimal siloSpaceHeaterUnitCost, double systemInstallCostMultiplier)
        {
            return siloSpaceHeaterUnitCost * (decimal)(1 + systemInstallCostMultiplier);
        }

        public static decimal CalcSlurryTankCost(decimal slurryTankUnitCost, double systemInstallCostMultiplier)
        {
            return slurryTankUnitCost * (decimal)(1 + systemInstallCostMultiplier);
        }

        public static decimal CalcHardwareSoftwareCost(decimal hardwareSoftwareUnitCost, double systemInstallCostMultiplier)
        {
            return hardwareSoftwareUnitCost * (decimal)(1 + systemInstallCostMultiplier);
        }

        public static decimal CalcSlakerCost(decimal slakerUnitCost, double systemInstallCostMultiplier)
        {
            return slakerUnitCost * (decimal)(1 + systemInstallCostMultiplier);
        }
        #endregion

        #region Annual (Operations and Maintenance) Costs

        public static decimal CalcAnnualCostChemical(double tonsChemicalPerYear, decimal limeProductUnitCost)
        {
            return (decimal)tonsChemicalPerYear * limeProductUnitCost;
        }

        public static decimal CalcAnnualCostOperationAndMaintanence(double annualCostMultiplier, decimal capitalCostTotal)
        {
            return (decimal)(annualCostMultiplier / 100.0) * capitalCostTotal;
        }

        public static decimal CalcDustCollectorBlowerElectricAmount(double dustCollectorBlowerPowerRequirement, double limeSiloRefillFrequency, double dustCollectorBlowerTimeToFillSilo)
        {
            return (decimal)(dustCollectorBlowerPowerRequirement * 0.7457 * 365 / limeSiloRefillFrequency * dustCollectorBlowerTimeToFillSilo / 60);
        }

        public static decimal CalcScrewFeederElectricAmount(double screwFeederPowerRequirement, double screwFeederHoursPerDay, double screwFeederDaysPerYear)
        {
            return (decimal)(screwFeederPowerRequirement * 0.7457 * screwFeederHoursPerDay * screwFeederDaysPerYear);
        }

        public static decimal CalcBinActivatorElectricAmount(double binActivatorPowerRequirement, double binActivatorCycleFrequency, double binActivatorDaysPerYear)
        {
            return (decimal)(binActivatorPowerRequirement * 0.7457 * ((binActivatorCycleFrequency * 1441 / 5) / 60 / 24) * binActivatorDaysPerYear);
        }

        public static decimal CalcSlurryMixerAndPumpElectricAmount(double slurryMixerPowerRequirement, double slurryPumpPowerRequirement, double slurryMixerAndPumpHoursPerDay, double slurryMixerAndPumpDaysPerYear)
        {
            return (decimal)((slurryMixerPowerRequirement + slurryPumpPowerRequirement) * 0.7457 * slurryMixerAndPumpHoursPerDay * slurryMixerAndPumpDaysPerYear);
        }

        public static decimal CalcSiloExhaustFanElectricAmount(double siloExhaustFanPowerRequirement, double siloExhaustFanHoursPerDay, double siloExhaustFanDaysPerYear)
        {
            return (decimal)(siloExhaustFanPowerRequirement * 0.7457 * siloExhaustFanHoursPerDay * siloExhaustFanDaysPerYear);
        }

        public static decimal CalcSiloSpaceHeaterElectricAmount(double siloSpaceHeaterPowerRequirement, double siloSpaceHeaterHoursPerDay, double siloSpaceHeaterDaysPerYear)
        {
            return (decimal)(siloSpaceHeaterPowerRequirement * siloSpaceHeaterHoursPerDay * siloSpaceHeaterDaysPerYear);
        }

        public static decimal CalcAnnualCostElectric(decimal dustCollectorBlowerElectricAmount,
                                                     decimal screwFeederElectricAmount,
                                                     decimal binActivatorElectricAmount,
                                                     decimal slurryMixerAndPumpElectricAmount,
                                                     decimal siloExhaustFanElectricAmount,
                                                     decimal siloSpaceHeaterElectricAmount,
                                                     decimal electricRateCost)
        {

            return (dustCollectorBlowerElectricAmount + screwFeederElectricAmount + binActivatorElectricAmount +
                    slurryMixerAndPumpElectricAmount + siloExhaustFanElectricAmount + siloSpaceHeaterElectricAmount) *  electricRateCost;
        }

        public static decimal CalcOtherAnnualItemsCost(double itemQuantity1, decimal itemUnitCost1,
                                                       double itemQuantity2, decimal itemUnitCost2,
                                                       double itemQuantity3, decimal itemUnitCost3,
                                                       double itemQuantity4, decimal itemUnitCost4,
                                                       double itemQuantity5, decimal itemUnitCost5)
        {
            return (decimal)itemQuantity1 * itemUnitCost1 + (decimal)itemQuantity2 * itemUnitCost2 + (decimal)itemQuantity3 * itemUnitCost3 + (decimal)itemQuantity4 * itemUnitCost4 + (decimal)itemQuantity5 * itemUnitCost5;
        }

        public static decimal CalcAnnualCostTotal(decimal annualChemicalCost, decimal annualOperationAndMaintanenceCost, decimal annualElectricCost, decimal otherAnnualItemsCost)
        {
            return annualChemicalCost + annualOperationAndMaintanenceCost + annualElectricCost + otherAnnualItemsCost;
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
