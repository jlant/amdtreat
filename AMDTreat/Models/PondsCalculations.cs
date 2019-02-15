using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMDTreat.Models
{
    /// <summary>
    /// Calculations for Vertical Flow Pond (VFP).
    /// 
    /// Unit Conversions:
    ///    1 gallon = 3.785412 liters                                          
    ///    1 ton = 2000 lbs
    ///    1 m^2 = 1.19599 yd^2
    ///    1 ft^3 = 7.4805 gallon                     
    ///    1 yd^2 = 9 ft^2
    ///    1 yd^3 = 27 ft^3
    ///    1 kg = 2.2 lbs 
    ///    1 g = 0.001 kg
    ///    1 mg = 0.001 g 
    ///    1 year = 525600 minutes
    ///    1 hr = 60 min                       
    ///    1 day = 1440 min
    ///    1 acre = 43560 ft^2
    /// </summary>
    public static class PondsCalculations
    {

        #region Sizing Calculations for Settling, Oxidation, Sludge Layers

        public static double CalcSettlingWaterVolumeBasedOnRetention(double settlingRetentionTime, double designFlow)
        {
            return settlingRetentionTime * designFlow * (60.0 / (7.4805 * 27.0));
        }

        public static double CalcSettlingBottomLengthBasedOnRetention(double waterVolume, double settlingDepth, double slope, double bottomLengthToWidthRatio)
        {

            double a = settlingDepth / bottomLengthToWidthRatio;
            double b = slope * Math.Pow(settlingDepth, 2) + slope * (Math.Pow(settlingDepth, 2) / bottomLengthToWidthRatio);
            double c = 2 * Math.Pow(slope, 2) * Math.Pow(settlingDepth, 3) - (waterVolume * 27);

            double d = (-b + Math.Sqrt((Math.Pow(b, 2) - 4 * a * c))) / (2 * a);
            double e = (-b - Math.Sqrt((Math.Pow(b, 2) - 4 * a * c))) / (2 * a);

            if (d > 0)
            {
                return d;
            }
            else
            {
                return e;
            }
        }

        public static double CalcSettlingBottomWidthBasedOnRetention(double bottomLengthSettling, double bottomLengthToWidthRatio)
        {
            return bottomLengthSettling / bottomLengthToWidthRatio;
        }

        public static double CalcSettlingTopLengthBasedOnRetention(double bottomLengthSettling, double settlingDepth, double slope)
        {
            return bottomLengthSettling + (2 * slope * settlingDepth);
        }

        public static double CalcSettlingTopWidthBasedOnRetention(double bottomWidthSettling, double settlingDepth, double slope)
        {
            return bottomWidthSettling + (2 * slope * settlingDepth);
        }

        public static double CalcSettlingTopAreaBasedOnRetention(double topLengthSettling, double topWidthSettling)
        {
            return topLengthSettling * topWidthSettling;
        }

        public static double CalcSettlingBottomAreaBasedOnRetention(double bottomLengthSettling, double bottomWidthSettling)
        {
            return bottomLengthSettling * bottomWidthSettling;
        }

        public static double CalcSettlingVolumeBasedOnRetention(double topLengthSettling, double topWidthSettling, double bottomLengthSettling, double bottomWidthSettling, double settlingDepth)
        {
            return (((topLengthSettling * topWidthSettling) + (bottomLengthSettling * bottomWidthSettling)) / 2) * (settlingDepth / 27);
        }

        public static double CalcSludgeBottomLengthBasedOnRetention(double bottomLengthSettling, double sludgeDepth, double slope)
        {
            return bottomLengthSettling - (2 * slope * sludgeDepth);
        }

        public static double CalcSludgeBottomWidthBasedOnRetention(double bottomWidthSettling, double sludgeDepth, double slope)
        {
            return bottomWidthSettling - (2 * slope * sludgeDepth);
        }

        public static double CalcSludgeTopLengthBasedOnRetention(double bottomLengthSettling)
        {
            return bottomLengthSettling;
        }

        public static double CalcSludgeTopWidthBasedOnRetention(double bottomWidthSettling)
        {
            return bottomWidthSettling;
        }

        public static double CalcSludgeTopAreaBasedOnRetention(double topLengthSludge, double topWidthSludge)
        {
            return topLengthSludge * topWidthSludge;
        }

        public static double CalcSludgeVolumeBasedOnRetention(double topLengthSludge, double topWidthSludge, double bottomLengthSludge, double bottomWidthSludge, double sludgeDepth)
        {
            return (((topLengthSludge * topWidthSludge) + (bottomLengthSludge * bottomWidthSludge)) / 2) * (sludgeDepth / 27);
        }

        public static double CalcOxidationBottomLengthBasedOnRetention(double topLengthSettling)
        {
            return topLengthSettling;
        }

        public static double CalcOxidationBottomWidthBasedOnRetention(double topWidthSettling)
        {
            return topWidthSettling;
        }

        public static double CalcOxidationTopLengthBasedOnRetention(double bottomLengthOxidation, double oxidationDepth, double slope)
        {
            return bottomLengthOxidation + (2 * slope * oxidationDepth);
        }

        public static double CalcOxidationTopWidthBasedOnRetention(double bottomWidthOxidation, double oxidationDepth, double slope)
        {
            return bottomWidthOxidation + (2 * slope * oxidationDepth);
        }

        public static double CalcOxidationTopAreaBasedOnRetention(double topLengthOxidation, double topWidthOxidation)
        {
            return topLengthOxidation * topWidthOxidation;
        }

        public static double CalcOxidationVolumeBasedOnRetention(double topLengthOxidation, double topWidthOxidation, double bottomLengthOxidation, double bottomWidthOxidation, double oxidationDepth)
        {
            return (((topLengthOxidation * topWidthOxidation) + (bottomLengthOxidation * bottomWidthOxidation)) / 2) * (oxidationDepth / 27);
        }

        public static double CalcFreeboardTopLengthBasedOnRetention(double topLengthOxidation, double freeboardDepth, double slope)
        {
            return topLengthOxidation + (2 * slope * freeboardDepth);
        }

        public static double CalcFreeboardTopWidthBasedOnRetention(double topWidthOxidation, double freeboardDepth, double slope)
        {
            return topWidthOxidation + (2 * slope * freeboardDepth);
        }

        public static double CalcFreeboardTopAreaBasedOnRetention(double topLengthFreeboard, double topWidthFreeboard)
        {
            return topLengthFreeboard * topWidthFreeboard;
        }

        public static double CalcFreeboardVolumeBasedOnRetention(double topLengthFreeboard, double topWidthFreeboard, double topLengthSettling, double topWidthSettling, double freeboardDepth)
        {
            return (((topLengthFreeboard * topWidthFreeboard) + (topLengthSettling * topWidthSettling)) / 2) * (freeboardDepth / 27);
        }
        #endregion

        #region Sizing Calculations for Dimensions Entered by User


        public static double CalcSettlingBottomLengthBasedOnDimensions(double freeboardTopLength, double freeboardDepth, double oxidationDepth, double settlingDepth, double slope)
        {
            return freeboardTopLength - (2.0 * slope * (freeboardDepth + oxidationDepth + settlingDepth));
        }

        public static double CalcSettlingBottomWidthBasedOnDimensions(double freeboardTopWidth, double freeboardDepth, double oxidationDepth, double settlingDepth, double slope)
        {
            return freeboardTopWidth - (2.0 * slope * (freeboardDepth + oxidationDepth + settlingDepth));
        }

        public static double CalcSettlingTopLengthBasedOnDimensions(double bottomLengthSettling, double settlingDepth, double slope)
        {
            return bottomLengthSettling + (2 * slope * settlingDepth);
        }

        public static double CalcSettlingTopWidthBasedOnDimensions(double bottomWidthSettling, double settlingDepth, double slope)
        {
            return bottomWidthSettling + (2 * slope * settlingDepth);
        }

        public static double CalcSettlingTopAreaBasedOnDimensions(double topLengthSettling, double topWidthSettling)
        {
            return topLengthSettling * topWidthSettling;
        }

        public static double CalcSettlingVolumeBasedOnDimensions(double topLengthSettling, double topWidthSettling, double bottomLengthSettling, double bottomWidthSettling, double settlingDepth)
        {
            return (((topLengthSettling * topWidthSettling) + (bottomLengthSettling * bottomWidthSettling)) / 2) * (settlingDepth / 27);
        }

        public static double CalcBottomLengthToWidthRatioBasedOnDimensions(double bottomLength, double bottomWidth)
        {
            return bottomLength / bottomWidth;
        }

        public static double CalcSludgeBottomLengthBasedOnDimensions(double bottomLengthSettling, double sludgeDepth, double slope)
        {
            return bottomLengthSettling - (2 * slope * sludgeDepth);
        }

        public static double CalcSludgeBottomWidthBasedOnDimensions(double bottomWidthSettling, double sludgeDepth, double slope)
        {
            return bottomWidthSettling - (2 * slope * sludgeDepth);
        }

        public static double CalcSludgeTopLengthBasedOnDimensions(double bottomLengthSettling)
        {
            return bottomLengthSettling;
        }

        public static double CalcSludgeTopWidthBasedOnDimensions(double bottomWidthSettling)
        {
            return bottomWidthSettling;
        }

        public static double CalcSludgeTopAreaBasedOnDimensions(double topLengthSludge, double topWidthSludge)
        {
            return topLengthSludge * topWidthSludge;
        }

        public static double CalcSludgeVolumeBasedOnDimensions(double topLengthSludge, double topWidthSludge, double bottomLengthSludge, double bottomWidthSludge, double sludgeDepth)
        {
            return (((topLengthSludge * topWidthSludge) + (bottomLengthSludge * bottomWidthSludge)) / 2) * (sludgeDepth / 27);
        }

        public static double CalcOxidationBottomLengthBasedOnDimensions(double topLengthSettling)
        {
            return topLengthSettling;
        }

        public static double CalcOxidationBottomWidthBasedOnDimensions(double topWidthSettling)
        {
            return topWidthSettling;
        }

        public static double CalcOxidationTopLengthBasedOnDimensions(double bottomLengthOxidation, double oxidationDepth, double slope)
        {
            return bottomLengthOxidation + (2 * slope * oxidationDepth);
        }

        public static double CalcOxidationTopWidthBasedOnDimensions(double bottomWidthOxidation, double oxidationDepth, double slope)
        {
            return bottomWidthOxidation + (2 * slope * oxidationDepth);
        }

        public static double CalcOxidationTopAreaBasedOnDimensions(double topLengthOxidation, double topWidthOxidation)
        {
            return topLengthOxidation * topWidthOxidation;
        }

        public static double CalcOxidationVolumeBasedOnDimensions(double topLengthOxidation, double topWidthOxidation, double bottomLengthOxidation, double bottomWidthOxidation, double oxidationDepth)
        {
            return (((topLengthOxidation * topWidthOxidation) + (bottomLengthOxidation * bottomWidthOxidation)) / 2) * (oxidationDepth / 27);
        }

        public static double CalcFreeboardTopAreaBasedOnDimensions(double topLengthFreeboard, double topWidthFreeboard)
        {
            return topLengthFreeboard * topWidthFreeboard;
        }

        public static double CalcFreeboardVolumeBasedOnDimensions(double topLengthFreeboard, double topWidthFreeboard, double topLengthOxidation, double topWidthOxidation, double freeboardDepth)
        {
            return (((topLengthFreeboard * topWidthFreeboard) + (topLengthOxidation * topWidthOxidation)) / 2) * (freeboardDepth / 27);
        }

        #endregion

        #region System Footprint Calculations - Annual sludge produced, total removal of metals, partial removal of metals

        public static double CalcAluminumHydroxideConcentration(double aluminum)
        {
            return aluminum * 2.88;
        }

        public static double CalcFerrousHydroxideConcentration(double ferrous)
        {
            return ferrous * 1.6;
        }

        public static double CalcFerricHydroxideConcentration(double ferric)
        {
            return ferric * 1.9;
        }

        public static double CalcManganeseHydroxideConcentration(double manganese)
        {
            return manganese * 1.6;
        }

        public static double CalcCalciteConcentration(double calcite)
        {
            return calcite;
        }

        public static double CalcMiscellaneousConcentration(double miscellaneousSolids)
        {
            return miscellaneousSolids;
        }

        public static double CalcAluminumHydroxideFraction(double aluminumHydroxideConcentration, double ferrousHydroxideConcentration, double ferricHydroxideConcentration, 
                                                           double manganeseHydroxideConcentration, double calciteConcentration, double miscellaneousConcentration)
        {
            return aluminumHydroxideConcentration / (aluminumHydroxideConcentration + ferrousHydroxideConcentration + ferricHydroxideConcentration + manganeseHydroxideConcentration + calciteConcentration + miscellaneousConcentration);
        }

        public static double CalcFerrousHydroxideFraction(double aluminumHydroxideConcentration, double ferrousHydroxideConcentration, double ferricHydroxideConcentration,
                                                   double manganeseHydroxideConcentration, double calciteConcentration, double miscellaneousConcentration)
        {
            return ferrousHydroxideConcentration / (aluminumHydroxideConcentration + ferrousHydroxideConcentration + ferricHydroxideConcentration + manganeseHydroxideConcentration + calciteConcentration + miscellaneousConcentration);
        }

        public static double CalcFerricHydroxideFraction(double aluminumHydroxideConcentration, double ferrousHydroxideConcentration, double ferricHydroxideConcentration,
                                           double manganeseHydroxideConcentration, double calciteConcentration, double miscellaneousConcentration)
        {
            return ferricHydroxideConcentration / (aluminumHydroxideConcentration + ferrousHydroxideConcentration + ferricHydroxideConcentration + manganeseHydroxideConcentration + calciteConcentration + miscellaneousConcentration);
        }

        public static double CalcManganeseHydroxideFraction(double aluminumHydroxideConcentration, double ferrousHydroxideConcentration, double ferricHydroxideConcentration,
                                   double manganeseHydroxideConcentration, double calciteConcentration, double miscellaneousConcentration)
        {
            return manganeseHydroxideConcentration / (aluminumHydroxideConcentration + ferrousHydroxideConcentration + ferricHydroxideConcentration + manganeseHydroxideConcentration + calciteConcentration + miscellaneousConcentration);
        }

        public static double CalcCalciteFraction(double aluminumHydroxideConcentration, double ferrousHydroxideConcentration, double ferricHydroxideConcentration,
                           double manganeseHydroxideConcentration, double calciteConcentration, double miscellaneousConcentration)
        {
            return calciteConcentration / (aluminumHydroxideConcentration + ferrousHydroxideConcentration + ferricHydroxideConcentration + manganeseHydroxideConcentration + calciteConcentration + miscellaneousConcentration);
        }

        public static double CalcMiscellaneousFraction(double aluminumHydroxideConcentration, double ferrousHydroxideConcentration, double ferricHydroxideConcentration,
                   double manganeseHydroxideConcentration, double calciteConcentration, double miscellaneousConcentration)
        {
            return miscellaneousConcentration / (aluminumHydroxideConcentration + ferrousHydroxideConcentration + ferricHydroxideConcentration + manganeseHydroxideConcentration + calciteConcentration + miscellaneousConcentration);
        }

        public static double CalcAluminumHydroxideDensity(double aluminumHydroxideFraction)
        {
            return 2.42 * aluminumHydroxideFraction * 3785.41 / 453.592;
        }

        public static double CalcFerrousHydroxideDensity(double ferrousHydroxideFraction)
        {
            return 3.4 * ferrousHydroxideFraction * 3785.41 / 453.592;
        }

        public static double CalcFerricHydroxideDensity(double ferricHydroxideFraction)
        {
            return 4.25 * ferricHydroxideFraction * 3785.41 / 453.592;
        }

        public static double CalcManganeseHydroxideDensity(double manganeseHydroxideFraction)
        {
            return 3.528 * manganeseHydroxideFraction * 3785.41 / 453.592;
        }

        public static double CalcCalciteDensity(double calciteFraction)
        {
            return 2.71 * calciteFraction * 3785.41 / 453.592;
        }

        public static double CalcMiscellaneousDensity(double miscellaneousFraction, double miscellaneousSoildsDensity)
        {
            return (miscellaneousSoildsDensity / 8.34) * miscellaneousFraction * 3785.41 / 453.592;
        }

        public static double CalcTotalDensity(double aluminumHydroxideDensity, double ferrousHydroxideDensity, double ferricHydroxideDensity,
                                               double manganeseHydroxideDensity, double calciteDensity, double miscellaneousDensity)
        {
            return aluminumHydroxideDensity + ferrousHydroxideDensity + ferricHydroxideDensity + manganeseHydroxideDensity + calciteDensity + miscellaneousDensity;
        }

        public static double CalcSludgeSpecificGravity(double totalDensity)
        {
            return totalDensity / 8.34;
        }

        public static double CalcSludgeDensity(double sludgeSpecificGravity, double percentSolids)
        {
            return sludgeSpecificGravity / (sludgeSpecificGravity - ((percentSolids / 100.0) * (sludgeSpecificGravity - 1))) * 8.34;
        }

        public static double CalcSludgeProducedPerMinute(double aluminumHydroxideConcentration, double ferrousHydroxideConcentration, double ferricHydroxideConcentration,
                                                       double manganeseHydroxideConcentration, double calciteConcentration, double miscellaneousConcentration, double typicalFlow)
        {
            return ((aluminumHydroxideConcentration + ferrousHydroxideConcentration + ferricHydroxideConcentration + manganeseHydroxideConcentration + calciteConcentration + miscellaneousConcentration) / (1000 * 1000)) * typicalFlow * 2.2 * 3.785;
        }

        public static double CalcSludgeProducedPerDay(double sludgeProducedPerMinute)
        {
            return sludgeProducedPerMinute * 60 * 24;
        }

        public static double CalcSludgeProducedPerYear(double sludgeProducedPerDay)
        {
            return sludgeProducedPerDay * 365;
        }

        public static double CalcSludgeProducedVolumeGallonsPerYear(double sludgeProducedPerYear, double sludgeDensity)
        {
            return sludgeProducedPerYear / sludgeDensity;
        }

        public static double CalcSludgeProducedVolumeCubicYardsPerYear(double sludgeProducedPerYear, double sludgeDensity)
        {
            return sludgeProducedPerYear / (sludgeDensity * 7.48 * 27);
        }

        public static double CalcDewateredSludgeDensity(double sludgeSpecificGravity, double dewateredPercentSolids)
        {
            return (sludgeSpecificGravity / (sludgeSpecificGravity - ((dewateredPercentSolids / 100.0) * (sludgeSpecificGravity - 1)))) * 8.34;
        }

        public static double CalcDewateredSludgeProducedCubicYardsPerYear(double sludgeProducedPerYear, double dewateredSludgeDensity)
        {
            return sludgeProducedPerYear / (dewateredSludgeDensity * 7.48 * 27);
        }

        public static double CalcSludgeRemovalPerYear(double sludgeProducedVolumeCubicYardsPerYear, double sludgeVolume)
        {
            return sludgeProducedVolumeCubicYardsPerYear / sludgeVolume;
        }

        public static double CalcSludgeRemovalPerMonth(double sludgeRemovalPerYear)
        {
            return 12 / sludgeRemovalPerYear;
        }

        #endregion


        #region Sludge removal and disposal estimates

        public static decimal CalcSludgeRemovalCostPerGallon(double sludgeProducedVolumeGal, decimal sludgeRemovalUnitCost)
        {
            return (decimal)sludgeProducedVolumeGal  * sludgeRemovalUnitCost;
        }

        public static double CalcVacuumTruckTrips(double sludgeProducedVolumeCubicYards, double vacuumTruckVolume)
        {
            return Math.Ceiling(sludgeProducedVolumeCubicYards / (vacuumTruckVolume / 201.974));
        }

        public static decimal CalcVacuumTruckHandlingCost(double vacuumTruckTrips, double vacuumTruckHandlingHours, decimal vacuumTruckHourlyRate)
        {
            return (decimal)vacuumTruckTrips * (decimal)vacuumTruckHandlingHours * vacuumTruckHourlyRate;
        }

        public static decimal CalcVacuumTruckDisposalCost(double vacuumTruckTrips, double vacuumTruckDisposingHours, decimal vacuumTruckHourlyRate)
        {
            return (decimal)vacuumTruckTrips * (decimal)vacuumTruckDisposingHours * vacuumTruckHourlyRate;
        }

        public static decimal CalcMechanicalExcavationCost(double dewateredSludgeProducedVolume, decimal mechanicalExcavationUnitCost)
        {
            return (decimal)dewateredSludgeProducedVolume * mechanicalExcavationUnitCost;
        }

        public static double CalcGeotubeQuantity(double dewateredSludgeProducedVolume, double geotubeVolume)
        {
            return Math.Ceiling(dewateredSludgeProducedVolume) / geotubeVolume;
        }

        public static decimal CalcGeotubeCost(double geotubeQuantity, decimal geotubeUnitCost)
        {
            return (decimal)geotubeQuantity * geotubeUnitCost;
        }

        public static decimal CalcGeotubeLoadingCost(double dewateredSludgeProducedVolume, double geotubeTruckVolume, double geotubeTimeToLoadTruck, decimal geotubeExcavatorHourlyRate)
        {
            return (decimal)(Math.Ceiling(dewateredSludgeProducedVolume / geotubeTruckVolume) * geotubeTimeToLoadTruck) * geotubeExcavatorHourlyRate;
        }

        public static decimal CalcGeotubeTransportationCost(double dewateredSludgeProducedVolume, double geotubeTruckVolume, double geotubeRoundtripDistance, decimal geotubeTransportationUnitCost)
        {
            return (decimal)(Math.Ceiling(dewateredSludgeProducedVolume / geotubeTruckVolume) * geotubeRoundtripDistance) * geotubeTransportationUnitCost;
        }

        public static decimal CalcGeotubeTotalCost(decimal geotubeCost, decimal geotubeLoadingCost, decimal geotubeTransportationCost)
        {
            return geotubeCost + geotubeLoadingCost + geotubeTransportationCost;
        }

        public static decimal CalcTipCost(double sludgeProducedPerYear, decimal tippingFee)
        {
            return (decimal)(sludgeProducedPerYear / 2000.0) * tippingFee;
        }

        public static decimal CalcTriaxleDisposalCost(double dewateredSludgeProducedVolume, double triaxleVolume, double triaxleRoundtripDistance, decimal triaxleTransportationUnitCost)
        {
            return (decimal)((dewateredSludgeProducedVolume / triaxleVolume) * triaxleRoundtripDistance) * triaxleTransportationUnitCost;
        }
       
        public static double CalcPumpingTime(double sludgeProducedVolumeGallonsPerYear, double pumpRate)
        {
            return Math.Ceiling((sludgeProducedVolumeGallonsPerYear / pumpRate) / (60 * 24));
        }

        public static decimal CalcPumpRentalCost(double pumpingTime, decimal pumpRentalCost)
        {
            return (decimal)pumpingTime * pumpRentalCost;
        }

        public static decimal CalcElectricCost(double sludgeProducedVolumeGallonsPerYear, double pumpRate, double powerRequirement, decimal electricRate)
        {
            return (decimal)(powerRequirement * 0.7457 * (sludgeProducedVolumeGallonsPerYear / pumpRate ) / 60.0) * electricRate;
        }

        public static decimal CalcFuelCost(double sludgeProducedVolumeGallonsPerYear, double pumpRate, double fuelRate, decimal fuelUnitCost)
        {
            return (decimal)(((sludgeProducedVolumeGallonsPerYear) / pumpRate ) / 60.0) * (decimal)fuelRate * fuelUnitCost;
        }
        #endregion

        #region System Footprint Calculations - Baffle

        public static double CalcBaffleLength(double baffleQuantity, double freeboardTopWidth, double freeboardDepth, double slope)
        {
            return (freeboardTopWidth + (2 * slope * freeboardDepth)) * baffleQuantity;
        }

        public static double CalcBaffleLengthUserSpecified(double baffleQuantity, double baffleLengthUserSpecified)
        {
            return baffleLengthUserSpecified * baffleQuantity;
        }
        #endregion

        #region System Footprint Calculations - Liner


        public static double CalcLinerSlopeLength(double freeboardDepth, double oxidationDepth, double settlingDepth, double sludgeDepth, double slope)
        {
            double depthSum = freeboardDepth + oxidationDepth + settlingDepth + sludgeDepth;

            return Math.Sqrt(Math.Pow(depthSum, 2) + Math.Pow(slope * depthSum, 2));
        }


        public static double CalcSyntheticLinerArea(double sludgeBottomLength, double sludgeBottomWidth,
                                                    double freeboardTopLength, double freeboardTopWidth,
                                                    double linerSlopeLength)
        {
            double linerExtension = 2.0;
            return ((freeboardTopLength + sludgeBottomLength) * (linerSlopeLength + linerExtension) +
                    (freeboardTopWidth + sludgeBottomWidth) * (linerSlopeLength + linerExtension) +
                    (sludgeBottomWidth * sludgeBottomLength)) / 9.0;
        }


        public static double CalcClayLinerArea(double sludgeBottomLength, double sludgeBottomWidth,
                                                    double freeboardTopLength, double freeboardTopWidth,
                                                    double linerSlopeLength)
        {
            return ((freeboardTopLength + sludgeBottomLength) * (linerSlopeLength) +
                    (freeboardTopWidth + sludgeBottomWidth) * (linerSlopeLength) +
                    (sludgeBottomWidth * sludgeBottomLength)) / 9.0;
        }


        public static double CalcClayLinerVolume(double clayLinerArea, double clayThickness)
        {
            return clayLinerArea * (clayThickness / 3.0);
        }

        public static double CalcGeosyntheticClayLinerArea(double sludgeBottomLength, double sludgeBottomWidth,
                                                    double freeboardTopLength, double freeboardTopWidth,
                                                    double linerSlopeLength)
        {
            double linerExtension = 2.0;
            return ((freeboardTopLength + sludgeBottomLength) * (linerSlopeLength + linerExtension) +
                    (freeboardTopWidth + sludgeBottomWidth) * (linerSlopeLength + linerExtension) +
                    (sludgeBottomWidth * sludgeBottomLength)) / 9.0;
        }
        
        public static double CalcGeosyntheticClayLinerVolume(double geosyntheticClayerLinerArea, double geosyntheticSoilCover)
        {
            return geosyntheticClayerLinerArea * (geosyntheticSoilCover / 3.0);
        }

        public static double CalcNonWovenGeotextileArea(double sludgeBottomLength, double sludgeBottomWidth,
                                            double freeboardTopLength, double freeboardTopWidth,
                                            double linerSlopeLength)
        {
            double linerExtension = 2.0;
            return ((freeboardTopLength + sludgeBottomLength) * (linerSlopeLength + linerExtension) +
                    (freeboardTopWidth + sludgeBottomWidth) * (linerSlopeLength + linerExtension) +
                    (sludgeBottomWidth * sludgeBottomLength)) / 9.0;
        }

        #endregion

        #region System Footprint Calculations - Max Pond Depth and Current Pond Depth, Excavation, Clear and Grub, Retention Time

        public static double CalcMaxPondDepth(double topWidthFreeboard)
        {
            return (topWidthFreeboard - 10.0) / 4.0;
        }

        public static double CalcPondDepth(double freeboardDepth, double oxidationDepth, double settlingDepth, double sludgeDepth)
        {
            return freeboardDepth + oxidationDepth + settlingDepth + sludgeDepth;
        }

        public static double CalcExcavationVolume(double sludgeVolume, double settlingVolume, double oxidationVolume, double linerVolume)
        {
            return sludgeVolume + settlingVolume + oxidationVolume + linerVolume;
        }


        public static double CalcSettlingRetentionTime(double designFlow, double settlingVolume)
        {
            return settlingVolume / (designFlow * 0.00495113 * 60);
        }

        public static double CalcOxidationRetentionTime(double designFlow, double oxidationVolume)
        {
            return oxidationVolume / (designFlow * 0.00495113 * 60);
        }

        public static double CalcTotalRetentionTime(double designFlow, double sludgeVolume, double settlingVolume, double oxidationVolume)
        {
            return (sludgeVolume + settlingVolume + oxidationVolume) / (designFlow * 60 * 0.00495294);
        }

        public static double CalcClearAndGrubArea(double freeboardTopLength, double freeboardTopWidth, double freeboardDepth)
        {
            double top_berm_width = 8;
            double outslope = 3;

            return ((freeboardTopWidth + 2 * top_berm_width + 2 * outslope * freeboardDepth) * (freeboardTopLength + 2 * top_berm_width + 2 * outslope * freeboardDepth)) / 43560.0;
        }

        public static decimal CalcClearAndGrubCost(double clearAndGrubArea, decimal clearAndGrubUnitCost)
        {
            return (decimal)clearAndGrubArea * clearAndGrubUnitCost;
        }

        public static decimal CalcClearAndGrubCostWithMultiplier(double clearAndGrubArea, double clearAndGrubMultiplier, decimal clearAndGrubUnitCost)
        {
            return (decimal)clearAndGrubArea * (decimal)clearAndGrubMultiplier * clearAndGrubUnitCost;
        }
        #endregion

        #region Capital Costs

        public static decimal CalcExcavationCost(double excavationVolume, decimal excavationUnitCost)
        {
            return (decimal)excavationVolume * excavationUnitCost;
        }

        public static decimal CalcValveCost(double valveQuantity, decimal valveUnitCost)
        {
            return (decimal)valveQuantity * valveUnitCost;
        }


        public static decimal CalcInOutPipeCost(double InOutPipeLength, decimal InOutPipeUnitCost)
        {
            return (decimal)InOutPipeLength * InOutPipeUnitCost;
        }

        public static decimal CalcAmdtreatPipeCost(double inOutPipeLength, decimal inOutPipeCost, double inOutPipeInstallRate, decimal laborRate)
        {
            return inOutPipeCost + ((decimal)(inOutPipeLength / inOutPipeInstallRate) * laborRate);
        }


        public static decimal CalcCustomPipeCost(double customPipeLength1, decimal customPipeUnitCost1, double customPipeLength2, decimal customPipeUnitCost2, double customPipeLength3, decimal customPipeUnitCost3)
        {
            return (decimal)customPipeLength1 * customPipeUnitCost1 + (decimal)customPipeLength2 * customPipeUnitCost2 + (decimal)customPipeLength3 * customPipeUnitCost3;
        }

        public static decimal CalcClayLinerCost(double clayLinerVolume, decimal clayLinerUnitCost)
        {
            return (decimal)clayLinerVolume * clayLinerUnitCost;
        }

        public static decimal CalcSyntheticLinerCost(double syntheticLinerArea, decimal syntheticLinerUnitCost)
        {
            return (decimal)syntheticLinerArea * syntheticLinerUnitCost;
        }

        public static decimal CalcGeosyntheticClayLinerCost(double geosyntheticClayLinerArea, decimal geosyntheticClayLinerUnitCost)
        {
            return (decimal)geosyntheticClayLinerArea * geosyntheticClayLinerUnitCost;
        }

        public static decimal CalcGeosyntheticClayLinerCoverCost(double geosyntheticClayLinerVolume, decimal geosyntheticClayLinerCoverUnitCost)
        {
            return (decimal)geosyntheticClayLinerVolume * geosyntheticClayLinerCoverUnitCost;
        }

        public static decimal CalcNonWovenGeotextileCost(double nonWovenGeotextileArea, decimal nonWovenGeotextileUnitCost)
        {
            return (decimal)nonWovenGeotextileArea * nonWovenGeotextileUnitCost;
        }

        public static decimal CalcBaffleCost(double baffleLength, decimal baffleUnitCost)
        {
            return (decimal)baffleLength * baffleUnitCost;
        }

        public static decimal CalcOtherItemsCost(double valveQuantity, decimal valveUnitCost,
                                                 double pumpQuantity, decimal pumpUnitCost,
                                                 double intakeQuantity, decimal intakeUnitCost,
                                                 double flowDistributionQuantity, decimal flowDistributionUnitCost,
                                                 double waterLevelControlQuantity, decimal waterLevelControlUnitCost,
                                                 double outletProtectionQuantity, decimal outletProtectionUnitCost)
        {
            return ((decimal)valveQuantity * valveUnitCost) +
                   ((decimal)pumpQuantity * pumpUnitCost) +
                   ((decimal)intakeQuantity * intakeUnitCost) +
                   ((decimal)flowDistributionQuantity * flowDistributionUnitCost) +
                   ((decimal)waterLevelControlQuantity * waterLevelControlUnitCost) +
                   ((decimal)outletProtectionQuantity * outletProtectionUnitCost);
        }

        public static decimal CalcBoreholeCost(double boreholeDepth, decimal boreholeUnitCost)
        {
            return (decimal)boreholeDepth * boreholeUnitCost;
        }

        public static decimal CalcOtherCapitalItemsCost(double itemQuantity1, decimal itemUnitCost1,
                                         double itemQuantity2, decimal itemUnitCost2,
                                         double itemQuantity3, decimal itemUnitCost3,
                                         double itemQuantity4, decimal itemUnitCost4,
                                         double itemQuantity5, decimal itemUnitCost5)
        {
            return (decimal)itemQuantity1 * itemUnitCost1 + (decimal)itemQuantity2 * itemUnitCost2 + (decimal)itemQuantity3 * itemUnitCost3 + (decimal)itemQuantity4 * itemUnitCost4 + (decimal)itemQuantity5 * itemUnitCost5;
        }

        public static decimal CalcCapitalCostTotal(decimal excavationCost, decimal linerCost, decimal pipeCost, decimal otherItemsCost, decimal baffleCost, decimal otherCapitalCostItemsCost, decimal boreholeCost)
        {
            return excavationCost + linerCost + pipeCost + otherItemsCost + baffleCost + otherCapitalCostItemsCost + boreholeCost;
        }

        #endregion

        #region Annual (Operation and Maintenance) Costs


        public static decimal CalcAnnualCostOperationAndMaintenance(double annualCostMultiplier, decimal capitalCostTotal)
        {
            return capitalCostTotal * (decimal)(annualCostMultiplier / 100.0);
        }

        public static decimal CalcAnnualCostSludgeHandling(decimal mobilizationDemobilizationCost, decimal sludgeRemovalCostPerGallon, decimal vacuumTruckHandlingCost, 
                                                           decimal mechanicalExcavationCost, decimal pumpCost)
        {
            return mobilizationDemobilizationCost + sludgeRemovalCostPerGallon + vacuumTruckHandlingCost + mechanicalExcavationCost + pumpCost;
        }

        public static decimal CalcAnnualCostSludgeDisposal(decimal sludgeDisposalCost)
        {
            return sludgeDisposalCost;
        }

        public static decimal CalcAnnualCostOtherAnnualItems(double itemQuantity1, decimal itemUnitCost1,
                                               double itemQuantity2, decimal itemUnitCost2,
                                               double itemQuantity3, decimal itemUnitCost3,
                                               double itemQuantity4, decimal itemUnitCost4,
                                               double itemQuantity5, decimal itemUnitCost5)
        {
            return (decimal)itemQuantity1 * itemUnitCost1 + (decimal)itemQuantity2 * itemUnitCost2 + (decimal)itemQuantity3 * itemUnitCost3 + (decimal)itemQuantity4 * itemUnitCost4 + (decimal)itemQuantity5 * itemUnitCost5;
        }


        public static decimal CalcAnnualCost(decimal annualCostOperationAndMaintenance, decimal annualCostSludgeHandling, decimal annualCostSludgeDisposal, decimal annualCostOtherAnnualItems)
        {
            return annualCostOperationAndMaintenance + annualCostSludgeHandling + annualCostSludgeDisposal + annualCostOtherAnnualItems;
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
