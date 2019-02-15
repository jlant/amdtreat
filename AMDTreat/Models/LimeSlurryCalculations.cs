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
    public static class LimeSlurryCalculations
    {

        #region Lime Slurry Specific Gravity and Density

        public static double CalcLimeSlurryDensity(double limeSlurryPercentage) 
        {
            return ((limeSlurryPercentage / 100.0) * 146.08) + (((100.0 - limeSlurryPercentage) / 100.0) * 64.4);
        }

        public static double CalcLimeSlurrySpecificGravity(double limeSlurryDensity)
        {
            return limeSlurryDensity / 64.4;
        }
        #endregion

        #region Stoichiometric Calculations 

        public static double CalcLimeSlurryFeedRateStoichiometric(double limeSlurryDailyConsumptionGallonsStoichiometric)
        {
            return limeSlurryDailyConsumptionGallonsStoichiometric / 1440.0;
        }

        public static double CalcLimeSlurryDailyConsumptionDryTonsStoichiometric(double designFlow, double hotAcidity, double limeSlurryPurity, double limeSlurryDissolutionEfficiency)
        {
            return hotAcidity * designFlow * 3.785 / 1000 / 100.1 * 74.13 * 1000 / 453592 / 2000 * 60 * 24 / (limeSlurryPurity / 100.0) / (limeSlurryDissolutionEfficiency / 100.0);
        }

        public static double CalcLimeSlurryDailyConsumptionGallonsStoichiometric(double designFlow, double hotAcidity, double limeSlurryPurity, double limeSlurryDissolutionEfficiency, double limeSlurryDensity)
        {

            return designFlow * hotAcidity * 3.785 / 1000.0 / 100.1 * 74.1 * 1000 / 453592 * 60 * 24 / (limeSlurryPurity / 100.0) / (limeSlurryDissolutionEfficiency / 100.0) / limeSlurryDensity * 7.48;
        }

        public static double CalcLimeSlurryAnnualConsumptionDryTonsStoichiometric(double limeSlurryDailyConsumptionDryTonsStoichiometric)
        {
            return limeSlurryDailyConsumptionDryTonsStoichiometric * 365.25;
        }

        public static double CalcLimeSlurryAnnualConsumptionGallonsStoichiometric(double limeSlurryDailyConsumptionGallonsStoichiometric)
        {
            return limeSlurryDailyConsumptionGallonsStoichiometric * 365.25;
        }

        public static double CalcLimeSlurryDaysOfTreatmentPerTruckLoadStoichiometric(double limeSlurryTankVolume, double limeSlurryRefillVolume, double limeSlurryDailyConsumptionGallonsStoichiometric)
        {
            if (limeSlurryRefillVolume < 0.9 * limeSlurryTankVolume)
            {
                return limeSlurryRefillVolume / limeSlurryDailyConsumptionGallonsStoichiometric;
            }
            else
            {
                return 0;
            }
        }

        public static double CalcLimeSlurryDaysOfExcessSupplyStoichiometric(double limeSlurryTankVolume, double limeSlurryRefillVolume, double limeSlurryDailyConsumptionGallonsStoichiometric)
        {
            return ((0.9 * limeSlurryTankVolume) - limeSlurryRefillVolume) / limeSlurryDailyConsumptionGallonsStoichiometric;
        }

        #endregion

        #region Titration Calculations

        public static double CalcLimeSlurryFeedRateTitration(double titrationQuantity, double designFlow)
        {
            return titrationQuantity * designFlow;
        }

        public static double CalcLimeSlurryDailyConsumptionDryTonsTitration(double limeSlurryDailyConsumptionGallonsTitration, double limeSlurryDensity)
        {
            return limeSlurryDailyConsumptionGallonsTitration / 7.48 * limeSlurryDensity / 2000;
        }

        public static double CalcLimeSlurryDailyConsumptionGallonsTitration(double titrationQuantity, double designFlow)
        {
            return titrationQuantity * designFlow * 60 * 24;
        }

        public static double CalcLimeSlurryAnnualConsumptionDryTonsTitration(double limeSlurryDailyConsumptionDryTonsTitration)
        {
            return limeSlurryDailyConsumptionDryTonsTitration * 365.25;
        }

        public static double CalcLimeSlurryAnnualConsumptionGallonsTitration(double limeSlurryDailyConsumptionGallonsTitration)
        {
            return limeSlurryDailyConsumptionGallonsTitration * 365.25;
        }

        public static double CalcLimeSlurryDaysOfTreatmentPerTruckLoadTitration(double limeSlurryTankVolume, double limeSlurryRefillVolume, double limeSlurryDailyConsumptionGallonsTitration)
        {
            if (limeSlurryRefillVolume < 0.9 * limeSlurryTankVolume)
            {
                return limeSlurryRefillVolume / limeSlurryDailyConsumptionGallonsTitration;
            }
            else
            {
                return 0;
            }
        }

        public static double CalcLimeSlurryDaysOfExcessSupplyTitration(double limeSlurryTankVolume, double limeSlurryRefillVolume)
        {
            return (0.9 * limeSlurryTankVolume) - limeSlurryRefillVolume;
        }

        #endregion

        #region User Specified Quantity Calculations

        public static double CalcLimeSlurryFeedRateUserSpecified(double limeSlurryDailyConsumptionGallonsUserSpecified)
        {
            return limeSlurryDailyConsumptionGallonsUserSpecified / 24 / 60;
        }

        public static double CalcLimeSlurryDailyConsumptionDryTonsUserSpecified(double limeSlurryAnnualConsumptionDryTonsUserSpecified)
        {
            return limeSlurryAnnualConsumptionDryTonsUserSpecified / 356.25;
        }

        public static double CalcLimeSlurryDailyConsumptionGallonsUserSpecified(double limeSlurryAnnualConsumptionGallonsUserSpecified)
        {
            return limeSlurryAnnualConsumptionGallonsUserSpecified / 365.25;
        }

        public static double CalcLimeSlurryAnnualConsumptionDryTonsUserSpecified(double limeSlurryDailyConsumptionDryTonsUserSpecified)
        {
            return limeSlurryDailyConsumptionDryTonsUserSpecified;
        }

        public static double CalcLimeSlurryAnnualConsumptionGallonsUserSpecified(double limeSlurryAnnualConsumptionDryTonsUserSpecified, double limeSlurryDensity)
        {
            return limeSlurryAnnualConsumptionDryTonsUserSpecified * 2000 / limeSlurryDensity * 7.48;
        }

        public static double CalcLimeSlurryDaysOfTreatmentPerTruckLoadUserSpecified(double limeSlurryTankVolume, double limeSlurryRefillVolume, double limeSlurryDailyConsumptionGallonsUserSpecified)
        {
            if (limeSlurryRefillVolume < 0.9 * limeSlurryTankVolume)
            {
                return limeSlurryRefillVolume / limeSlurryDailyConsumptionGallonsUserSpecified;
            }
            else
            {
                return 0;
            }
        }

        public static double CalcLimeSlurryDaysOfExcessSupplyUserSpecified(double limeSlurryTankVolume, double limeSlurryRefillVolume)
        {
            return (0.9 * limeSlurryTankVolume) - limeSlurryRefillVolume;
        }


        #endregion

        #region Lime Slurry Tank Foundation Area and Concrete Volume

        public static double CalcLimeSlurryTankFoundationAreaEstimate(double limeSlurryTankVolume, double limeSlurryTankHeight)
        {
            return (Math.Sqrt((limeSlurryTankVolume / 7.48) / (Math.PI * limeSlurryTankHeight)) * 2 + 2) * (Math.Sqrt((limeSlurryTankVolume / 7.48) / (Math.PI * limeSlurryTankHeight)) * 2 + 2);
        }

        public static double CalcLimeSlurryTankFoundationConcreteVolume(double limeSlurryTankFoundationArea, double limeSlurryTankFoundationConcreteSlabThickness)
        {
            return limeSlurryTankFoundationArea * limeSlurryTankFoundationConcreteSlabThickness / 27.0;
        }

        #endregion

        #region Capital Costs

        public static decimal CalcRubberTubingCost(double rubberTubingLength, decimal rubberTubingUnitCost)
        {
            return (decimal)rubberTubingLength * rubberTubingUnitCost;
        }

        public static decimal CalcChemicalMeteringPumpCost(double chemicalMeteringPumpQuantity, decimal chemicalMeteringPumpUnitCost)
        {
            return (decimal)chemicalMeteringPumpQuantity * chemicalMeteringPumpUnitCost;
        }

        public static decimal CalcSiliconeRubberHeatBlanketCost(double siliconeRubberHeatBlanketQuantity, decimal siliconeRubberHeatBlanketUnitCost)
        {
            return (decimal)siliconeRubberHeatBlanketQuantity * siliconeRubberHeatBlanketUnitCost;
        }

        public static decimal CalcTubingHeatTracingCost(double tubingHeatTracingLength, decimal tubingHeatTracingUnitCost)
        {
            return (decimal)tubingHeatTracingLength * tubingHeatTracingUnitCost;
        }

        public static decimal CalcStorageAndDispensingCost(decimal electricServicePanelCost, decimal limeSlurryTankUnitCost, decimal rubberTubingCost, 
                                                           decimal limeSlurryAgitatorCost, decimal chemicalMeteringPumpCost, decimal siliconeRubberHeatBlanketCost,
                                                           decimal tubingHeatTracingCost, decimal pHControllerCost, decimal pHProbeCost,
                                                           decimal cellularTelemetryCost, decimal cellularMonthlyCost)
        {
            return electricServicePanelCost + limeSlurryTankUnitCost + rubberTubingCost +
                   limeSlurryAgitatorCost + chemicalMeteringPumpCost + siliconeRubberHeatBlanketCost +
                   tubingHeatTracingCost + pHControllerCost + pHProbeCost +
                   cellularTelemetryCost + cellularMonthlyCost;
        }


        public static decimal CalcTankFoundationAndPumpHousingCost(double limeSlurryTankFoundationConcreteVolume, decimal limeSlurryTankFoundationConcreteMaterialAndPlacementCost, decimal pumpHousingCost)
        {
            return ((decimal)limeSlurryTankFoundationConcreteVolume * limeSlurryTankFoundationConcreteMaterialAndPlacementCost) + pumpHousingCost;
        }

        public static decimal CalcOtherCapitalItemsCost(double itemQuantity1, decimal itemUnitCost1,
                                                 double itemQuantity2, decimal itemUnitCost2,
                                                 double itemQuantity3, decimal itemUnitCost3,
                                                 double itemQuantity4, decimal itemUnitCost4,
                                                 double itemQuantity5, decimal itemUnitCost5)
        {
            return (decimal)itemQuantity1 * itemUnitCost1 + (decimal)itemQuantity2 * itemUnitCost2 + (decimal)itemQuantity3 * itemUnitCost3 + (decimal)itemQuantity4 * itemUnitCost4 + (decimal)itemQuantity5 * itemUnitCost5;
        }

        public static decimal CalcCapitalCostSubTotal(decimal storageAndDispensingCost, decimal tankFoundationAndPumpHousingCost, decimal otherCapitalItemsCost)
        {
            return storageAndDispensingCost + tankFoundationAndPumpHousingCost + otherCapitalItemsCost;
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

        public static double CalcLimeSlurryAgitatorElectricAmount(double limeSlurryAgitatorPowerRequirement, double limeSlurryAgitatorHoursPerDay, double limeSlurryAgitatorDaysPerYear)
        {
            return limeSlurryAgitatorPowerRequirement * 0.7457 * limeSlurryAgitatorHoursPerDay * limeSlurryAgitatorDaysPerYear;
        }

        public static double CalcChemicalMeteringPumpElectricAmount(double chemicalMeteringPumpPowerRequirement, double chemicalMeteringPumpHoursPerDay, double chemicalMeteringPumpDaysPerYear)
        {
            return chemicalMeteringPumpPowerRequirement * 0.7457 * chemicalMeteringPumpHoursPerDay * chemicalMeteringPumpDaysPerYear;
        }

        public static double CalcSiliconeRubberHeatBlanketElectricAmount(double siliconeRubberHeatBlanketPowerRequirement, double siliconeRubberHeatBlanketQuantity, 
                                                                        double siliconeRubberHeatBlanketHoursPerDay, double siliconeRubberHeatBlanketMonthsPerYear)
        {
            return ((siliconeRubberHeatBlanketPowerRequirement * siliconeRubberHeatBlanketQuantity) * siliconeRubberHeatBlanketHoursPerDay * 30 * siliconeRubberHeatBlanketMonthsPerYear / 1000);
        }

        public static double CalcTubingHeatTracingElectricAmount(double tubingHeatTracingPowerRequirement, double tubingHeatTracingLength,
                                                                 double siliconeRubberHeatBlanketHoursPerDay, double siliconeRubberHeatBlanketMonthsPerYear)
        {
            return ((tubingHeatTracingPowerRequirement * tubingHeatTracingLength) * siliconeRubberHeatBlanketHoursPerDay * 30 * siliconeRubberHeatBlanketMonthsPerYear / 1000);
        }

        public static decimal CalcAnnualCostElectric(double limeSlurryAgitatorElectricAmount, double chemicalMeteringPumpElectricAmount,
                                                     double siliconeRubberHeatBlanketElectricAmount, double tubingHeatTracingElectricAmount,                                                    
                                                     decimal electricRateCost)
        {
            return (decimal)(limeSlurryAgitatorElectricAmount + chemicalMeteringPumpElectricAmount + siliconeRubberHeatBlanketElectricAmount + tubingHeatTracingElectricAmount) * electricRateCost;
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
