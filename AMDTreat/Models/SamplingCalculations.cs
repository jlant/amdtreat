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
    public static class SamplingCalculations
    {

        #region Capital Costs

        public static decimal CalcSamplingEquipmentCost(decimal samplingEquipmentCost)
        {
            return samplingEquipmentCost;
        }

        public static decimal CalcCapitalCostTotal(decimal samplingEquipmentCost)
        {
            return samplingEquipmentCost;
        }

        #endregion

        #region Annual (Operations and Maintenance) Costs

        public static double CalcAnnualCostNPDESMonitoringSampleFrequencyTime(double npdesMonitoringSampleFrequency, double roundTripTravelTime)
        {
            return npdesMonitoringSampleFrequency * roundTripTravelTime * 12;
        }

        public static double CalcAnnualCostHydrologicMonitoringSampleFrequencyTime(double hydrologicMonitoringSampleFrequency, double roundTripTravelTime)
        {
            return hydrologicMonitoringSampleFrequency * roundTripTravelTime;
        }

        public static decimal CalcAnnualCostLabor(double npdesMonitoringNumberOfSamplePoints, double npdesMonitoringCollectionTimePerSample, double npdesMonitoringSampleFrequency,
                                                  double hydrologicMonitoringNumberOfSamplePoints, double hydrologicMonitoringCollectionTimePerSample, double hydrologicMonitoringSampleFrequency,
                                                  double roundTripTravelTime, double monitoringSampleFrequencyTime, decimal laborUnitCost)
        {
            return ((decimal)(npdesMonitoringNumberOfSamplePoints * npdesMonitoringCollectionTimePerSample * npdesMonitoringSampleFrequency * 12) +
                   (decimal)(hydrologicMonitoringNumberOfSamplePoints * hydrologicMonitoringCollectionTimePerSample * hydrologicMonitoringSampleFrequency) +
                   (decimal)monitoringSampleFrequencyTime) * laborUnitCost;
        }


        public static decimal CalcAnnualCostLab(double npdesMonitoringNumberOfSamplePoints, double npdesMonitoringNumberOfSampleFrequency,
                                                double hydrologicMonitoringNumberOfSamplePoints, double hydrologicMonitoringSampleFrequency,
                                                decimal labNPDESSampleUnitCost, decimal labHydrologicSampleUnitCost)
        {
            return labNPDESSampleUnitCost * (decimal)(npdesMonitoringNumberOfSamplePoints * npdesMonitoringNumberOfSampleFrequency * 12) +
                   labHydrologicSampleUnitCost * (decimal)(hydrologicMonitoringNumberOfSamplePoints * hydrologicMonitoringSampleFrequency);
        }

        public static double CalcAnnualCostNPDESMonitoringSampleFrequencyMileage(double npdesMonitoringSampleFrequency, double roundTripTravelMileage)
        {
            return npdesMonitoringSampleFrequency * roundTripTravelMileage * 12;
        }

        public static double CalcAnnualCostHydrologicMonitoringSampleFrequencyMileage(double hydrologicMonitoringSampleFrequency, double roundTripTravelMileage)
        {
            return hydrologicMonitoringSampleFrequency * roundTripTravelMileage;
        }

        public static decimal CalcAnnualCostMileage(double monitoringSampleFrequencyMileage, decimal mileageRateUnitCost)
        {
            return (decimal)monitoringSampleFrequencyMileage * mileageRateUnitCost;
        }

        public static decimal CalcAnnualCost(decimal annualCostLabor, decimal annualCostLab, decimal annualCostMileage)
        {
            return annualCostLabor + annualCostLab + annualCostMileage;
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
