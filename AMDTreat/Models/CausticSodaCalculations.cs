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
    public static class CausticSodaCalculations
    {

        #region Chemical Consumption Calculations for Stoichiometric

        public static double CalcMolesAcidityPerYear(double netAcidity, double typicalFlow)
        {
            return netAcidity * typicalFlow * 3.785 * 525600 * (1 / 1000.0) * (1 / 100.0);
        }

        public static double CalcPoundsCausticSodaPerYear(double molesAcidityPerYear)
        {
            return molesAcidityPerYear * 2 * 1 * 40 * (1 / 1000.0) * 2.2046;
        }

        public static double CalcGallonsCausticSodaPerYearStoichiometric(double poundsCausticSodaPerYear, double poundsCausticSodaPerGallon, double mixingEfficiency, double causticSodaPurity)
        {
            return poundsCausticSodaPerYear * (1 / poundsCausticSodaPerGallon) / (mixingEfficiency / 100.0) / (causticSodaPurity / 100.0);
        }

        public static double CalcGallonsCausticSodaPerMonthStoichiometric(double gallonsCausticSodaPerYearStoichiometric)
        {
            return gallonsCausticSodaPerYearStoichiometric / 12.0;
        }

        public static double CalcGallonsCausticSodaPerDayStoichiometric(double gallonsCausticSodaPerYearStoichiometric)
        {
            return gallonsCausticSodaPerYearStoichiometric / 365.0;
        }

        #endregion

        #region Chemical Consumption Calculations for Titration

        public static double CalcGallonsCausticSodaPerYearTitration(double titrationAmount, double typicalFlow, double mixingEfficiency)
        {
            return (titrationAmount * typicalFlow * 525600) / (mixingEfficiency / 100.0);
        }

        public static double CalcGallonsCausticSodaPerMonthTitration(double gallonsCausticSodaPerYearTitration)
        {
            return gallonsCausticSodaPerYearTitration / 12.0;
        }

        public static double CalcGallonsCausticSodaPerDayTitration(double gallonsCausticSodaPerYearTitration)
        {
            return gallonsCausticSodaPerYearTitration / 365.0;
        }

        #endregion

        #region Chemical Consumption Calculations for User Specified Quantity

        public static double CalcGallonsCausticSodaPerYearUserSpecifiedQuantity(double userSpecifiedQuantity)
        {
            return userSpecifiedQuantity;
        }

        public static double CalcGallonsCausticSodaPerMonthUserSpecifiedQuantity(double gallonsCausticSodaPerYearUserSpecifiedQuantity)
        {
            return gallonsCausticSodaPerYearUserSpecifiedQuantity / 12.0;
        }

        public static double CalcGallonsCausticSodaPerDayUserSpecifiedQuantity(double gallonsCausticSodaPerYearUserSpecifiedQuantity)
        {
            return gallonsCausticSodaPerYearUserSpecifiedQuantity / 365.0;
        }

        #endregion

        #region System Footprint Calculations - Tank Refill Frequency

        public static double CalcTankRefillFrequency(double gallonsCausticSodaPerYear, double tankVolume)
        {
            return Math.Ceiling(gallonsCausticSodaPerYear / tankVolume);
        }

        #endregion

        #region System Footprint Calculations - Operational Period of Chemical Pump

        public static double CalcChemicalMeteringPumpOperationPeriodTotalDaysPerYear(double hoursPerDay, double daysPerYear)
        {
            return (hoursPerDay * daysPerYear) / 24.0;
        }

        public static double CalcChemicalMeteringPumpOperationPeriodTotalWeeksPerYear(double chemicalPumpOperationPeriodTotalDaysPerYear)
        {
            return chemicalPumpOperationPeriodTotalDaysPerYear / 7.0;
        }

        public static double CalcChemicalMeteringPumpOperationPeriodTotalMonthsPerYear(double chemicalPumpOperationPeriodTotalWeeksPerYear)
        {
            return chemicalPumpOperationPeriodTotalWeeksPerYear / 4.345;
        }

        #endregion

        #region Capital Costs

        public static decimal CalcAutomatedSystemCost(decimal PIDpHControllerCost, decimal pHProbeCost, decimal chemicalMeteringPumpCost)
        {
            return PIDpHControllerCost + pHProbeCost + chemicalMeteringPumpCost;
        }

        public static decimal CalcTankCost(double tankVolume, decimal tankUnitCost)
        {
            return (decimal)tankVolume * tankUnitCost;
        }

        public static decimal CalcValveCost(double valveQuantity, decimal valveUnitCost)
        {
            return (decimal)valveQuantity * valveUnitCost;
        }

        public static decimal CalcFeederLineCost(double feederLineLength, decimal feederLineUnitCost)
        {
            return (decimal)feederLineLength * feederLineUnitCost;
        }

        public static decimal CalcStorageAndDispensingCost(decimal automatedSystemCost, decimal tankCost, decimal valveCost, decimal feederLineCost)
        {
            return automatedSystemCost + tankCost + valveCost + feederLineCost;
        }

        public static decimal CalcOtherCapitalItemsCost(double itemQuantity1, decimal itemUnitCost1,
                                                 double itemQuantity2, decimal itemUnitCost2,
                                                 double itemQuantity3, decimal itemUnitCost3,
                                                 double itemQuantity4, decimal itemUnitCost4,
                                                 double itemQuantity5, decimal itemUnitCost5)
        {
            return (decimal)itemQuantity1 * itemUnitCost1 + (decimal)itemQuantity2 * itemUnitCost2 + (decimal)itemQuantity3 * itemUnitCost3 + (decimal)itemQuantity4 * itemUnitCost4 + (decimal)itemQuantity5 * itemUnitCost5;
        }

        public static decimal CalcCapitalCostSubTotal(decimal automatedSystemCost, decimal tankCost, decimal valveCost, decimal feederLineCost, decimal otherCapitalItemsCost)
        {
            return automatedSystemCost + tankCost + valveCost + feederLineCost + otherCapitalItemsCost;
        }

        public static decimal CalcSystemInstallCost(double systemInstallCostMultiplier, decimal capitalCostSubTotal)
        {
            return (decimal)(systemInstallCostMultiplier / 100.0) * capitalCostSubTotal;
        }

        public static decimal CalcCapitalCostTotal(decimal capitalCostSubTotal, decimal systemInstallCost)
        {
            return capitalCostSubTotal + systemInstallCost;
        }

        #endregion

        #region Annual (Operations and Maintenance) Costs

        public static decimal CalcAnnualCostChemical(double gallonsCausticSodaPerYear, decimal causticSodaUnitCost)
        {
            return (decimal)gallonsCausticSodaPerYear * causticSodaUnitCost;
        }

        public static decimal CalcAnnualCostpHProbe(decimal pHProbeCost)
        {
            return pHProbeCost;
        }

        public static decimal CalcAnnualCostOperationAndMaintanence(double annualCostMultiplier, decimal capitalCostTotal)
        {
            return (decimal)(annualCostMultiplier / 100.0) * capitalCostTotal;
        }

        public static decimal CalcAnnualCostElectric(double chemicalMeteringPumpPowerRequirement, double chemicalMeteringPumpHoursPerDay, double chemicalMeteringPumpDaysPerYear, decimal chemicalMeteringPumpElectricityUnitCost)
        {
            return (decimal)((chemicalMeteringPumpPowerRequirement * chemicalMeteringPumpHoursPerDay) / 1000.0) * (decimal)chemicalMeteringPumpDaysPerYear * chemicalMeteringPumpElectricityUnitCost;
        }

        public static decimal CalcOtherAnnualItemsCost(double itemQuantity1, decimal itemUnitCost1,
                                         double itemQuantity2, decimal itemUnitCost2,
                                         double itemQuantity3, decimal itemUnitCost3,
                                         double itemQuantity4, decimal itemUnitCost4,
                                         double itemQuantity5, decimal itemUnitCost5)
        {
            return (decimal)itemQuantity1 * itemUnitCost1 + (decimal)itemQuantity2 * itemUnitCost2 + (decimal)itemQuantity3 * itemUnitCost3 + (decimal)itemQuantity4 * itemUnitCost4 + (decimal)itemQuantity5 * itemUnitCost5;
        }

        public static decimal CalcAnnualCostTotal(decimal annualChemicalCost, decimal annualOperationAndMaintanenceCost, decimal annualElectricCost, decimal annualpHProbeCost, decimal otherAnnualItemsCost)
        {
            return annualChemicalCost + annualOperationAndMaintanenceCost + annualElectricCost + annualpHProbeCost + otherAnnualItemsCost;
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

        /// <summary>
        /// Calculate the total cost of limestone material for recapitalization costs.
        /// </summary>
        /// <returns></returns>
        public static decimal CalcRecapitalizationCostMaterialTotalCostTank(decimal tankCost, decimal feederLineCost, decimal valveCost)
        {
            return tankCost + feederLineCost + valveCost;
        }


        #endregion

    }
}
