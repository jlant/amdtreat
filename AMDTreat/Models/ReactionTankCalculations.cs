using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMDTreat.Models
{
    /// <summary>
    /// Calculations for Reaction Tank.
    /// </summary>
    public static class ReactionTankCalculations
    {

        #region Sizing Summary: Reaction Tank Design

        public static double CalcPlugFlowRetentionTime(double designFlow, double tankTotalHeight, double tankFreeboardHeight, double tankDiameter)
        {
            return (Math.PI * Math.Pow((tankDiameter / 2), 2) * (tankTotalHeight - tankFreeboardHeight)) * 7.48 / designFlow;
        }

        public static double CalcTankTotalHeight(double tankTotalHeight)
        {
            return tankTotalHeight;
        }

        public static double CalcTankFreeboardHeight(double tankFreeboardHeight)
        {
            return tankFreeboardHeight;
        }

        public static double CalcTankDiameter(double tankDiameter)
        {
            return tankDiameter;
        }

        public static double CalcTankProtectiveCoatingSurfaceArea(double tankDiameter, double tankTotalHeight)
        {
            return (Math.PI * Math.Pow((tankDiameter / 2), 2)) + (2 * Math.PI * (tankDiameter / 2) * tankTotalHeight); 
        }

        public static double CalcMixerPower(double mixerPower)
        {
            return mixerPower;
        }

        public static double CalcFoundationArea(double foundationMultiplier, double tankDiameter)
        {
            return foundationMultiplier * Math.PI * Math.Pow(((tankDiameter + 2) / 2), 2);
        }

        public static double CalcFoundationDepth(double foundationDepth)
        {
            return foundationDepth;
        }

        public static double CalcFoundationVolume(double foundationDepth, double foundationArea, double tankQuantity)
        {
            return ((foundationDepth * foundationArea) / 27) * tankQuantity;
        }

        #endregion

        #region Capital Costs

        public static decimal CalcTankMaterialCost(double tankQuantity, decimal tankMaterialCost)
        {
            return tankMaterialCost * (decimal)tankQuantity;
        }

        public static decimal CalcTankProtectiveCoatingCost(double tankProtectiveCoatingSurfaceArea, double tankQuantity, decimal tankProtectiveCoatingUnitCost)
        {
            return (decimal)tankProtectiveCoatingSurfaceArea * tankProtectiveCoatingUnitCost * (decimal)tankQuantity;
        }

        public static decimal CalcMixerCost(double tankQuantity, decimal tankMixerCost)
        {
            return tankMixerCost * (decimal)tankQuantity;
        }

        public static decimal CalcVariableFrequencyDriveCost(double tankQuantity, double mixerPower, decimal mixerVariableFrequencyDriveUnitCost)
        {
            return mixerVariableFrequencyDriveUnitCost * (decimal)mixerPower * (decimal)tankQuantity;
        }

        public static decimal CalcStairsCost(double tankTotalHeight, double tankQuantity, decimal stairsUnitCost)
        {
            return ((stairsUnitCost * (decimal)tankTotalHeight) / (decimal)0.707 / (decimal)12) * (decimal)tankQuantity;
        }

        public static decimal CalcCatwalkCost(double tankDiameter, double tankQuantity, decimal catwalkUnitCost)
        {
            return catwalkUnitCost * (decimal)tankDiameter * (decimal)tankQuantity;
        }

        public static decimal CalcTankEquipmentCost(decimal tankMaterialCost, decimal tankProtectiveCoatingCost, decimal mixerCost, 
                                                    decimal variableFrequencyDriveCost, decimal stairsCost, decimal catwalkCost)
        {
            return tankMaterialCost + tankProtectiveCoatingCost + mixerCost + variableFrequencyDriveCost + stairsCost + catwalkCost;
        }

        public static decimal CalcFoundationCost(double foundationVolume, decimal foundationConcreteMaterialAndPlacementCost)
        {
            return (decimal)foundationVolume * foundationConcreteMaterialAndPlacementCost;
        }

        public static decimal CalcInstallationCost(decimal tankEquipmentCost, decimal foundationCost, double capitalCostSystemInstallationMultiplier)
        {
            return (tankEquipmentCost + foundationCost) * (decimal)(capitalCostSystemInstallationMultiplier / 100);
        }
        public static decimal CalcOtherCapitalItemsCost(double itemQuantity1, decimal itemUnitCost1,
                                                 double itemQuantity2, decimal itemUnitCost2,
                                                 double itemQuantity3, decimal itemUnitCost3,
                                                 double itemQuantity4, decimal itemUnitCost4,
                                                 double itemQuantity5, decimal itemUnitCost5)
        {
            return (decimal)itemQuantity1 * itemUnitCost1 + (decimal)itemQuantity2 * itemUnitCost2 + (decimal)itemQuantity3 * itemUnitCost3 + (decimal)itemQuantity4 * itemUnitCost4 + (decimal)itemQuantity5 * itemUnitCost5;
        }



        public static decimal CalcCapitalCostTotal(decimal tankEquipmentCost, decimal foundationCost, decimal installationCost, decimal otherCapitalItemsCost)
        {
            return tankEquipmentCost + foundationCost + installationCost + otherCapitalItemsCost + otherCapitalItemsCost;
        }

        #endregion

        #region Annual (Operations and Maintenance) Costs


        public static decimal CalcAnnualCostOperationAndMaintenance(double annualCostMultiplier, decimal capitalCostTotal)
        {
            return (decimal)(annualCostMultiplier / 100.0) * capitalCostTotal;
        }

        public static decimal CalcTankElectricCost(double mixerPower, double mixerOperationalTimeHoursPerDay, double mixerOperationalTimeDaysPerYear, double tankQuantity, decimal electricUnitCost)
        {
            return (decimal)(mixerPower * 0.7456 * mixerOperationalTimeHoursPerDay * mixerOperationalTimeDaysPerYear) * (decimal)tankQuantity * electricUnitCost;
        }

        public static decimal CalcAnnualCostElectric(decimal tankElectricCost)
        {
            return tankElectricCost;
        }

        public static decimal CalcOtherAnnualItemsCost(double itemQuantity1, decimal itemUnitCost1,
                                                       double itemQuantity2, decimal itemUnitCost2,
                                                       double itemQuantity3, decimal itemUnitCost3,
                                                       double itemQuantity4, decimal itemUnitCost4,
                                                       double itemQuantity5, decimal itemUnitCost5)
        {
            return (decimal)itemQuantity1 * itemUnitCost1 + (decimal)itemQuantity2 * itemUnitCost2 + (decimal)itemQuantity3 * itemUnitCost3 + (decimal)itemQuantity4 * itemUnitCost4 + (decimal)itemQuantity5 * itemUnitCost5;
        }

        public static decimal CalcAnnualCostTotal(decimal annualOperationAndMaintanenceCost, decimal annualElectricCost, decimal otherAnnualItemsCost)
        {
            return annualOperationAndMaintanenceCost + annualElectricCost + otherAnnualItemsCost;
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
