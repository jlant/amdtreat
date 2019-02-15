using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMDTreat.Models
{
    /// <summary>
    /// Calculations for Bio Reactor.
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
    public static class BioReactorCalculations
    {

        #region Common Equations

        /// <summary>
        /// Calculate the weight of a material in tons. Used in sizing calculations.
        /// </summary>
        /// <remarks>
        /// Notes:
        ///     Material density is calculated in the calcLimestoneBulkDensity() function.
        /// </remarks>       
        /// <param name="materialVolume">The material volume in cubic yards, a calculated value</param>
        /// <param name="materialDensity">The bulk density of material in pounds per cubic feet, a calculated value</param>
        /// <returns>The weight of a material in tons</returns>
        public static double CalcMaterialWeight(double materialVolume, double materialDensity)
        {
            return materialVolume * (materialDensity / 2000.0) * 27.0;
        }

        /// <summary>
        /// Calculate the top dimension of a layer in feet.
        /// </summary>
        /// <param name="dimension">The dimension in feet, a calculated value</param>
        /// <param name="depth">The depth of a layer in feet, a user defined value</param>
        /// <param name="slope">The side slope of the pond, a user defined value</param>
        /// <returns>the top length of a layer in feet</returns>
        public static double CalcLayerTopDimension(double dimension, double depth, double slope)
        {
            return dimension + 2 * slope * depth;
        }

        /// <summary>
        /// Calculate the bottom dimension of a layer in feet.
        /// </summary>
        /// <param name="dimension">The dimension in feet, a calculated value</param>
        /// <param name="depth">The depth of a layer in feet, a user defined value</param>
        /// <param name="slope">The side slope of the pond, a user defined value</param>
        /// <returns>the bottom length of a layer in feet</returns>
        public static double CalcLayerBottomDimension(double dimension, double depth, double slope)
        {
            return dimension - 2 * slope * depth;
        }

        /// <summary>
        /// Calculate the area of a layer in square feet.
        /// </summary>
        /// <param name="layerTopLength">The top length of the layer in feet, a calculated value</param>
        /// <param name="layerTopWidth">The top width of the layer in feet, a calculated value</param>
        /// <returns></returns>
        public static double CalcLayerTopArea(double layerTopLength, double layerTopWidth)
        {
            return layerTopLength * layerTopWidth;
        }

        /// <summary>
        /// Calculate the volume of a layer in cubic yards.  The bottom width and bottom length can be from
        /// another layer.  The top width and top length are calculated from the bottom width and bottom length.
        /// </summary>
        /// <param name="bottomWidth">The bottom width of the layer in feet, a calculated value</param>
        /// <param name="bottomLength">The bottom length of the layer in feet, a calculated value</param>
        /// <param name="depth">The limestone depth in feet, a user defined value</param>
        /// <returns>The volume of a layer in cubic yards</returns>
        public static double CalcLayerVolume(double topLength, double topWidth, double bottomLength, double bottomWidth, double depth)
        {
            return ((topLength * topWidth + bottomLength * bottomWidth) / 2.0) * depth / 27.0;
        }

        /// <summary>
        /// Calculate bulk density in pounds per cubic feet (lbs/ft3).
        /// </summary>
        /// <remarks>
        /// Notes:
        ///     Density of limestone = 165.43 pounds per cubic feet (lbs/ft3)
        /// <param name="voidSpace">The void space of the limestone bed as a percentage, a user defined value</param>
        /// <returns>The bulk density in pounds per cubic feet</returns>
        public static double CalcBulkDensity(double voidSpace)
        {
            return (1 - voidSpace / 100.0) * 165.43;
        }

        public static double CalcLinerSlopeLength(double depth, double slope)
        {
            return Math.Sqrt(Math.Pow(depth, 2) + Math.Pow(slope * depth, 2));
        }

        #endregion

        #region Specific Equations

        /// <summary>
        /// Calculate bulk density in pounds per cubic feet (lbs/ft3).
        /// </summary>
        /// <returns>The bulk density in pounds per cubic feet</returns>
        public static double CalcBioMixBulkDensity(double bioMixManureDensity, double bioMixManurePercentage,
                                                   double bioMixHayDensity, double bioMixHayPercentage,
                                                   double bioMixWoodChipsDensity, double bioMixWoodChipsPercentage,
                                                   double bioMixOtherDensity, double bioMixOtherPercentage,
                                                   double bioMixLimestoneFinesDensity, double bioMixLimestoneFinesPercentage)
        {
            return bioMixManureDensity * (bioMixManurePercentage / 100.0) +
                   bioMixHayDensity * (bioMixHayPercentage / 100.0) +
                   bioMixWoodChipsDensity * (bioMixWoodChipsPercentage / 100.0) +
                   bioMixOtherDensity * (bioMixOtherPercentage / 100.0) +
                   bioMixLimestoneFinesDensity * (bioMixLimestoneFinesPercentage / 100.0);
        }

        public static double CalcBioMixWoodChipsPercentage(double bioMixManurePercentage, double bioMixHayPercentage, double bioMixOtherPercentage, double bioMixLimestoneFinesPercentage)
        {
            return 100 - (bioMixManurePercentage + bioMixHayPercentage +  bioMixOtherPercentage + bioMixLimestoneFinesPercentage); 
        }

        public static double CalcBioMixMaterialVolume(double bioMixVolume, double bioMixMaterialPercentage)
        {
            return bioMixVolume * (bioMixMaterialPercentage / 100); 
        }
        #endregion

        #region Sizing Calculations for Sulfate Reduction
        /// <summary>
        /// Calculate bio mix volume in cubic yards. 
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="designFlow">The design flow in gallons per minute, a water quality input value</param>
        /// <param name="bioMixSulfate">The amount of sulfate in mg/L, a user defined value</param>
        /// <param name="bioMixSulfateReductionRate">The rate of sulfate reduction in moles of sulfate per cubic meter per day, a user defined value</param>
        /// <param name="bioMixShrinkage">The bio mix shrinkage factor in percent, a user defined value</param>
        /// <returns>The volume of bio mix in cubic yards</returns>
        public static double CalcBioMixVolumeBasedOnSulfateReduction(double designFlow, double bioMixSulfate, double bioMixSulfateReductionRate, double bioMixShrinkage)
        {
            return ((designFlow * bioMixSulfate * 0.056775) / (bioMixSulfateReductionRate)) * (1 + (bioMixShrinkage / 100.0));
        }


        #endregion

        #region Sizing Calculations for Alkalinity Generation Rate

        /// <summary>
        /// Calculate the acidity rate in grams per day.  
        /// </summary>
        /// <param name="designFlow">The design flow in gallons per minute, a water quality input value</param>
        /// <param name="netAcidity">The net acidity in milligrams per liter, a water quality input value</param>
        /// <returns>The acidity rate in grams per day</returns>
        public static double CalcAcidityRateBasedOnAlkalinityGenerationRate(double designFlow, double netAcidity)
        {
            return (designFlow * 3.785412 * netAcidity * 1440.0) / 1000.0;
        }

        /// <summary>
        /// Calculate the surface area of the top of bio mix in square yards.
        /// </summary>
        /// <param name="acidityRate">The acidity rate in grams per day, a calculated value</param>
        /// <param name="alkalinityGenerationRate">The alkalinity generation rate in milligrams per liter, a user defined value</param>
        /// <returns>The surface area of the top of bio mix in square yards</returns>
        public static double CalcSurfaceAreaTopOfBioMixBasedOnAlkalinityGenerationRate(double acidityRate, double alkalinityGenerationRate)
        {
            return (acidityRate / alkalinityGenerationRate) * 1.196;
        }

        /// <summary>
        /// Calculate the bottom length of the bio mix layer in feet. 
        /// </summary>
        /// <param name="surfaceArea">The surface area of the top of bio mix square yards, a calculated value</param>
        /// <param name="bioMixDepth">The bio mix depth in feet, a user defined value</param>
        /// <param name="slope">The side slope of the pond, a user defined value</param>
        /// <param name="bottomLengthToWidthRatio">The bottom length to width ratio, a user defined value or a calculate valued</param>
        /// <returns>The bottom length of the bioMix layer in feet</returns>
        public static double CalcBioMixBottomLengthBasedOnAlkalinityGenerationRate(double surfaceArea, double bioMixDepth, double slope, double bottomLengthToWidthRatio)
        {

            double z = 2 * slope * bioMixDepth;

            double a = 1 / bottomLengthToWidthRatio;
            double b = (z / bottomLengthToWidthRatio) + z;
            double c = Math.Pow(z, 2) - surfaceArea * 9;

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

        /// <summary>
        /// Calculate the volume of bio mix in cubic yards.  Used in sizing calculation based on Alkalinity Generation.
        /// </summary>
        /// <param name="bioMixBottomLength">The bottom length of the bio mix layer in feet, a calculated value</param>
        /// <param name="bioMixDepth">The depth of bio mix, a user define value</param>
        /// <param name="pondInsideSlope">The side slope of the pond, a user defined value</param>
        /// <param name="bottomLengthToWidthRatio">The bottom length to width ratio, a user defined value or a calculate valued</param>
        /// <returns>The volume of bio mix in cubic yards</returns>
        public static double CalcBioMixVolumeBasedOnAlkalinityGenerationRate(double bioMixBottomLength,
                                                                             double bioMixDepth,
                                                                             double pondInsideSlope,
                                                                             double bottomLengthToWidthRatio)
        {
            double bioMixBottomWidth = bioMixBottomLength / bottomLengthToWidthRatio;
            double bioMixTopLength = bioMixBottomLength + (2 * pondInsideSlope * bioMixDepth);
            double bioMixTopWidth = bioMixBottomWidth + (2 * pondInsideSlope * bioMixDepth);

            return (((bioMixBottomLength * bioMixBottomWidth) + (bioMixTopLength * bioMixTopWidth)) / 2.0) * (bioMixDepth / 27.0);
        }

        #endregion

        #region Sizing Calculations for Pilot Testing

        /// <summary>
        /// Calculate the volume of bio mix in cubic yards.
        /// </summary>
        /// <param name="designFlow">The design flow in gallons per minute, a water quality input value</param>
        /// <param name="volumetricLoading">The volumetric loading in cubic feet per gpm of flow, a user defined value</param>
        /// <returns>The volume of bio mix in cubic yards</returns>
        public static double CalcBioMixVolumeBasedOnPilotTesting(double designFlow, double volumetricLoading)
        {
            return (designFlow * volumetricLoading) / 27.0;
        }

        #endregion

        #region Sizing Calculations for Dimensions Entered by User

        /// <summary>
        /// Calculate bio mix bottom width in feet.
        /// </summary>
        /// <param name="freeboardTopWidth">The freeboard top width in feet, a user defined value</param>
        /// <param name="freeboardDepth">The freeboard depth in feet, a user defined value</param>
        /// <param name="freeStandingWaterDepth">The free standing water depth in feet, a user defined value</param>
        /// <param name="bioMixDepth">The bioMix depth in feet, a user defined value</param>
        /// <param name="slope">The inside slope of the pond, a user defined value</param>
        /// <returns>The bio mix bottom width in feet</returns>
        public static double CalcBioMixBottomWidthBasedOnDimensionsEntered(double freeboardTopWidth, double freeboardDepth, double freeStandingWaterDepth, double bioMixDepth, double slope)
        {
            double depthTotal = freeboardDepth + freeStandingWaterDepth + bioMixDepth;
            return CalcLayerBottomDimension(freeboardTopWidth, depthTotal, slope);
        }

        /// <summary>
        /// Calculate bio mix bottom length in feet.
        /// </summary>
        /// <param name="freeboardTopLength">The freeboard top length in feet, a user defined value</param>
        /// <param name="freeboardDepth">The freeboard depth in feet, a user defined value</param>
        /// <param name="freeStandingWaterDepth">The free standing water depth in feet, a user defined value</param>
        /// <param name="bioMixDepth">The bioMix depth in feet, a user defined value</param>
        /// <param name="slope">The inside slope of the pond, a user defined value</param>
        /// <returns>The bio mix bottom length in feet</returns>
        public static double CalcBioMixBottomLengthBasedOnDimensionsEntered(double freeboardTopLength, double freeboardDepth, double freeStandingWaterDepth, double bioMixDepth, double slope)
        {
            double depthTotal = freeboardDepth + freeStandingWaterDepth + bioMixDepth;
            return CalcLayerBottomDimension(freeboardTopLength, depthTotal, slope);
        }

        /// <summary>
        /// Calculate bio mix top width dimension, in feet.
        /// </summary>
        /// <param name="bioMixBottomDimension">The bio mix bottom width in feet, a user defined value</param>
        /// <param name="bioMixDepth">The bio mix depth in feet, a user defined value</param>
        /// <param name="slope">The inside slope of the pond, a user defined value</param>
        /// <returns>The bio mix top width in feet</returns>
        public static double CalcBioMixTopWidthBasedOnDimensionsEntered(double bioMixBottomWidth, double bioMixDepth, double slope)
        { 
            return CalcLayerTopDimension(bioMixBottomWidth, bioMixDepth, slope);
        }

        /// <summary>
        /// Calculate bio mix top length dimension, in feet.
        /// </summary>
        /// <param name="bioMixBottomDimension">The bio mix bottom length in feet, a user defined value</param>
        /// <param name="bioMixDepth">The bio mix depth in feet, a user defined value</param>
        /// <param name="slope">The inside slope of the pond, a user defined value</param>
        /// <returns>The bio mix top length in feet</returns>
        public static double CalcBioMixTopLengthBasedOnDimensionsEntered(double bioMixBottomLength, double bioMixDepth, double slope)
        { 
            return CalcLayerTopDimension(bioMixBottomLength, bioMixDepth, slope);
        }

        /// <summary>
        /// Calculate bio mix volume in cubic yards.  Used in sizing calculations based on Dimensions Entered by User
        /// </summary>
        /// <param name="bioMixTopLength">The bio mix top length in feet, a user defined value</param>
        /// <param name="bioMixTopWidth">The bio mix top width in feet, a user defined value</param>
        /// <param name="bioMixBottomLength">The bio mix bottom length in feet, a user defined value</param>
        /// <param name="bioMixBottomWidth">The bio mix bottom width in feet, a user defined value</param>
        /// <param name="bioMixDepth">The bioMix depth in feet, a user defined value</param>
        /// <returns>The volume of bioMix in cubic yards</returns>
        public static double CalcBioMixVolumeBasedOnDimensionsEntered(double bioMixTopLength, double bioMixTopWidth,
                                                                      double bioMixBottomLength, double bioMixBottomWidth,
                                                                      double bioMixDepth)
        {
            return CalcLayerVolume(bioMixTopLength, bioMixTopWidth, bioMixBottomLength, bioMixBottomWidth, bioMixDepth);
        }

        /// <summary>
        /// Calculate the bottom layer length to width ratio.  Used in sizing calculations based on Dimensions Entered by User
        /// </summary>
        /// <param name="bottomLength">The bottom length dimension of a layer in feet, a calculated value</param>
        /// <param name="bottomWidth">The bottom width dimension in feet, a calculated value</param>
        /// <returns>The bottom layer length to width ratio</returns>
        public static double CalcBottomLengthToWidthRatioBasedOnDimensionsEntered(double bottomLength, double bottomWidth)
        {
            return bottomLength / bottomWidth;
        }


        #endregion

        #region System Footprint Calculations - Freeboard

        public static double CalcFreeboardTopLength(double waterTopLength, double freeboardDepth, double slope)
        {
            return CalcLayerTopDimension(waterTopLength, freeboardDepth, slope);
        }

        public static double CalcFreeboardTopWidth(double waterTopWidth, double freeboardDepth, double slope)
        {
            return CalcLayerTopDimension(waterTopWidth, freeboardDepth, slope);
        }

        public static double CalcFreeboardVolume(double freeboardTopLength, double freeboardTopWidth, double waterTopLength, double waterTopWidth, double freeboardDepth)
        {
            return CalcLayerVolume(freeboardTopLength, freeboardTopWidth, waterTopLength, waterTopWidth, freeboardDepth);
        }

        #endregion

        #region System Footprint Calculations - Water

        public static double CalcWaterTopLength(double bioMixTopLength, double waterDepth, double slope)
        {
            return CalcLayerTopDimension(bioMixTopLength, waterDepth, slope);
        }

        public static double CalcWaterTopWidth(double bioMixTopWidth, double waterDepth, double slope)
        {
            return CalcLayerTopDimension(bioMixTopWidth, waterDepth, slope);
        }

        public static double CalcWaterTopArea(double waterTopLength, double waterTopWidth)
        {
            return CalcLayerTopArea(waterTopLength, waterTopWidth);
        }

        public static double CalcWaterVolume(double waterTopLength, double waterTopWidth, double bioMixTopLength, double bioMixTopWidth, double waterDepth)
        {
            return CalcLayerVolume(waterTopLength, waterTopWidth, bioMixTopLength, bioMixTopWidth, waterDepth);
        }

        #endregion

        #region System Footprint Calculations - Compost Mix

        /// <summary>
        /// Calculate the porosity of the compost mix in percent.
        /// </summary>
        /// <param name="limestoneFinesPercentage">Percentage of limestone fines in compost mix in percent, a user defined value</param>
        /// <returns>The porosity of the compost mix in percent</returns>
        public static double CalcCompostMixPorosity(double limestoneFinesPercentage)
        {
            return (-0.0002 * Math.Pow(limestoneFinesPercentage, 3)) + (0.0176 * Math.Pow(limestoneFinesPercentage, 2)) + (-0.4826 * limestoneFinesPercentage) + 90.0;
        }

        #endregion

        #region System Footprint Calculations - Bio Mix

        /// <summary>
        /// Calculate the weight of a bio mix in tons. Used in sizing calculations.
        /// </summary>
        /// <param name="bioMixVolume">The bio mix volume in cubic yards, a calculated value</param>
        /// <param name="bioMixDensity">The bulk density of bio mix in pounds per cubic feet, a calculated value</param>
        /// <returns>The weight of a bio mix in tons</returns>
        public static double CalcBioMixWeight(double bioMixVolume, double bioMixDensity)
        {
            return CalcMaterialWeight(bioMixVolume, bioMixDensity);
        }

        /// <summary>
        /// Calculate the bottom length of the bio mix layer in feet.  Uses the quadratic equation to solve for the bottom length
        /// of the pond based on known volume, length to width ratio, and depth of bioMix.
        /// </summary>
        /// <param name="bioMixVolume">The volume of bio mix in cubic yards, a calculated value</param>
        /// <param name="bioMixDepth">The bio mix depth in feet, a user defined value</param>
        /// <param name="slope">The side slope of the pond, a user defined value</param>
        /// <param name="bottomLengthToWidthRatio">The bottom length to width ratio, a user defined value or a calculate valued</param>
        /// <returns>The bottom length of the bio mix layer in feet</returns>
        public static double CalcBioMixBottomLength(double bioMixVolume, double bioMixDepth, double slope, double bottomLengthToWidthRatio)
        {

            double a = bioMixDepth / bottomLengthToWidthRatio;
            double b = (slope * Math.Pow(bioMixDepth, 2)) + (slope * Math.Pow(bioMixDepth, 2) / bottomLengthToWidthRatio);
            double c = (2 * Math.Pow(slope, 2) * Math.Pow(bioMixDepth, 3)) - (bioMixVolume * 27);

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

        /// <summary>
        /// Calculate the bottom width of the bio mix layer in feet.
        /// </summary>
        /// <param name="bioMixBottomLength">The bottom length of the bio mix layer in feet, a calculated value</param>
        /// <param name="bottomLengthToWidthRatio">The bottom length to width ratio, a user defined value or a calculate valued</param>
        /// <returns>The bottom width of the bio mix layer in feet</returns>
        public static double CalcBioMixBottomWidth(double bioMixBottomLength, double bottomLengthToWidthRatio)
        {
            return bioMixBottomLength / bottomLengthToWidthRatio;
        }

        public static double CalcBioMixTopLength(double bioMixBottomLength, double bioMixDepth, double slope)
        {
            return CalcLayerTopDimension(bioMixBottomLength, bioMixDepth, slope);
        }

        public static double CalcBioMixTopWidth(double bioMixBottomWidth, double bioMixDepth, double slope)
        {
            return CalcLayerTopDimension(bioMixBottomWidth, bioMixDepth, slope);
        }

        public static double CalcBioMixTopArea(double bioMixTopLength, double bioMixTopWidth)
        {
            return CalcLayerTopArea(bioMixTopLength, bioMixTopWidth);
        }

        public static double CalcBioMixVolume(double bioMixTopLength, double bioMixTopWidth, double bioMixBottomLength, double bioMixBottomWidth, double bioMixDepth)
        {
            return CalcLayerVolume(bioMixTopLength, bioMixTopWidth, bioMixBottomLength, bioMixBottomWidth, bioMixDepth);
        }

        public static double CalcBioMixLimestoneFinesVolume(double bioMixVolume, double bioMixLimestoneFinesPercentage)
        {
            return bioMixVolume * (bioMixLimestoneFinesPercentage / 100);
        }

        public static double CalcBioMixLimestoneFinesWeight(double bioMixLimestoneFinesVolume, double bioMixLimestoneFinesDensity)
        {
            return CalcMaterialWeight(bioMixLimestoneFinesVolume, bioMixLimestoneFinesDensity);
        }

        public static double CalcBioMixManureVolume(double bioMixVolume, double bioMixManurePercentage)
        {
            return CalcBioMixMaterialVolume(bioMixVolume, bioMixManurePercentage); 
        }

        public static double CalcBioMixHayVolume(double bioMixVolume, double bioMixHayPercentage)
        {
            return CalcBioMixMaterialVolume(bioMixVolume, bioMixHayPercentage); 
        }

        public static double CalcBioMixWoodChipsVolume(double bioMixVolume, double bioMixWoodChipsPercentage)
        {
            return CalcBioMixMaterialVolume(bioMixVolume, bioMixWoodChipsPercentage); 
        }

        public static double CalcBioMixOtherVolume(double bioMixVolume, double bioMixOtherPercentage)
        {
            return CalcBioMixMaterialVolume(bioMixVolume, bioMixOtherPercentage); 
        }

        public static double CalcBioMixManureWeight(double bioMixManureVolume, double bioMixManureDensity)
        {
            return CalcMaterialWeight(bioMixManureVolume, bioMixManureDensity);
        }

        public static double CalcBioMixHayWeight(double bioMixHayVolume, double bioMixHayDensity)
        {
            return CalcMaterialWeight(bioMixHayVolume, bioMixHayDensity);
        }

        public static double CalcBioMixWoodChipsWeight(double bioMixWoodChipsVolume, double bioMixWoodChipsDensity)
        {
            return CalcMaterialWeight(bioMixWoodChipsVolume, bioMixWoodChipsDensity);
        }

        public static double CalcBioMixOtherWeight(double bioMixOtherVolume, double bioMixOtherDensity)
        {
            return CalcMaterialWeight(bioMixOtherVolume, bioMixOtherDensity);
        }

        public static double CalcBioMixOrganicsVolume(double bioMixVolume, double bioMixLimestoneFinesVolume)
        {
            return bioMixVolume - bioMixLimestoneFinesVolume;
        }

        public static double CalcBioMixOrganicsVoidsVolume(double bioMixVolume, double compostMixPorosity)
        {
            return CalcBioMixMaterialVolume(bioMixVolume, compostMixPorosity); 
        }
        #endregion

        #region System Footprint Calculations - Drainage Aggregate

        public static double CalcAggregateTopWidth(double bioMixBottomWidth)
        {
            return bioMixBottomWidth;
        }

        public static double CalcAggregateTopLength(double bioMixBottomLength)
        {
            return bioMixBottomLength;
        }

        public static double CalcAggregateBottomWidth(double aggregateTopWidth, double aggregateDepth, double slope)
        {
            return CalcLayerBottomDimension(aggregateTopWidth, aggregateDepth, slope);
        }

        public static double CalcAggregateBottomLength(double aggregateTopLength, double aggregateDepth, double slope)
        {
            return CalcLayerBottomDimension(aggregateTopLength, aggregateDepth, slope);
        }

        public static double CalcAggregateVolume(double aggregateTopLength, double aggregateTopWidth, double aggregateBottomLength, double aggregateBottomWidth, double aggregateDepth)
        {
            return CalcLayerVolume(aggregateTopLength, aggregateTopWidth, aggregateBottomLength, aggregateBottomWidth, aggregateDepth);
        }

        public static double CalcAggregateWeight(double aggregateVolume, double aggregateDensity)
        {
            return CalcMaterialWeight(aggregateVolume, aggregateDensity);
        }
        #endregion

        #region System Footprint Calculations - Pipe

        /// <summary>
        /// Calculate the total length of trunk pipe in feet.
        /// </summary>
        /// <returns>The total length of trunk pipe in feet</returns>
        public static double CalcTrunkPipeSegmentLengthTotal(double aggregateBottomLength)
        {
            return aggregateBottomLength;
        }

        /// <summary>
        /// Calculate the quantity (number) of trunk pipe couplers.
        /// </summary>
        /// <param name="trunkPipeSegmentLengthTotal">The total length of trunk pipe in feet, a calculated value</param>
        /// <param name="truckPipeSegmentLength">The length of trunk pipe segment in feet, a user defined value</param>
        /// <returns>The quantity (number) of trunk pipe couplers</returns>
        public static double CalcTrunkPipeCouplerQuantity(double trunkPipeSegmentLengthTotal, double truckPipeSegmentLength)
        {
            return Math.Ceiling(trunkPipeSegmentLengthTotal / truckPipeSegmentLength);
        }

        /// <summary>
        /// Calculate the quantity of spur pipe.
        /// </summary>
        /// <param name="spurPipeSpacing">The spur pipe spacing in feet, a user define value</param>
        /// <param name="aggregateBottomLength">The length of the aggregate layer bottom in feet, a calculate value</param>
        /// <returns></returns>
        public static double CalcSpurPipeQuantity(double spurSpacing, double aggregateBottomLength)
        {
            return Math.Floor(aggregateBottomLength / spurSpacing) + 1.0;
        }

        /// <summary>
        /// Calculate the total length of spur pipe in feet.
        /// </summary>
        /// <param name="spurPipeQuantity">The quantity (number) of spur pipe, a calculated value</param>
        /// <param name="aggregateBottomWidth">The width of the aggregate layer bottom in feet, a calculated value</param>
        /// <returns>The total length of spur pipe in feet</returns>
        public static double CalcSpurPipeSegmentLengthTotal(double spurPipeQuantity, double aggregateBottomWidth)
        {
            return spurPipeQuantity * aggregateBottomWidth;
        }

        /// <summary>
        /// Calculate the quantity (number) of spur couplers.
        /// </summary>
        /// <param name="spurPipeQuantity">The quantity (number) of spur pipe, a calculated value</param>
        /// <param name="spurPipeSegmentLength">The length of a spur segment, a user defined value</param>
        /// <param name="aggregateBottomWidth">The width of the aggregate layer bottom in feet, a calculated value</param>
        /// <returns>The quantity (number) of spur couplers</returns>
        public static double CalcSpurPipeCouplerQuantity(double spurPipeQuantity, double spurPipeSegmentLength, double aggregateBottomWidth)
        {
            return spurPipeQuantity * Math.Floor(aggregateBottomWidth / spurPipeSegmentLength);
        }

        #endregion

        #region System Footprint Calculations - Liner

        /// <summary>
        /// Calculate the slope length of the liner in feet.
        /// </summary>
        /// <returns>The slope length of the liner in feet</returns>
        public static double CalcLinerSlopeLength(double freeboardDepth, double waterDepth, double bioMixDepth, double aggregateDepth, double slope)
        {
            double depthTotal = freeboardDepth + waterDepth + bioMixDepth + aggregateDepth;

            return CalcLinerSlopeLength(depthTotal, slope);
        }

        /// <summary>
        /// Calculate the slope length of the non woven geotextile in feet.
        /// </summary>
        /// <returns>The slope length of the non woven geotextile in feet</returns>
        public static double CalcNonWovenGeotextileSlopeLength(double bioMixDepth, double aggregateDepth, double slope)
        {
            double depthTotal = bioMixDepth + aggregateDepth;

            return CalcLinerSlopeLength(depthTotal, slope);
        }

        /// <summary>
        /// Calculate the area of a synthetic liner in square yards. Calculation adds a 2.0 ft extentsion to liner slope length to 
        /// account for tie-in.
        /// </summary>
        /// <returns>The area of a synthetic liner in square yards</returns>
        public static double CalcSyntheticLinerArea(double freeboardTopLength, double freeboardTopWidth,
                                                    double aggregateBottomLength, double aggregateBottomWidth,
                                                    double linerSlopeLength)
        {
            return ((freeboardTopLength + aggregateBottomLength) * (linerSlopeLength + 2) +
                    (freeboardTopWidth + aggregateBottomWidth) * (linerSlopeLength + 2) +
                    (aggregateBottomWidth * aggregateBottomLength)) / 9.0;
        }

        /// <summary>
        /// Calculate the area of a clay liner in square yards.
        /// </summary>
        /// <returns>The area of a clay liner in square yards</returns>
        public static double CalcClayLinerArea(double freeboardTopLength, double freeboardTopWidth,
                                               double aggregateBottomLength, double aggregateBottomWidth,
                                               double bioMixBottomLength, double linerSlopeLength)
        {
            return ((freeboardTopLength + bioMixBottomLength) * (linerSlopeLength) +
                    (freeboardTopWidth + aggregateBottomWidth) * (linerSlopeLength) +
                    (aggregateBottomWidth * aggregateBottomLength)) / 9.0;
        }

        /// <summary>
        /// Calculate the area of a non woven geotextile in square yards.
        /// </summary>
        /// <returns>The area of a non woven geotextile in square yards</returns>
        public static double CalcNonWovenGeoTextileArea(double bioMixDepth, double aggregateDepth,
                                                        double aggregateBottomLength, double aggregateBottomWidth,
                                                        double bioMixBottomWidth, double nonWovenGeoTextileSlopeLength)
        {
            return ((bioMixDepth + aggregateDepth) * (nonWovenGeoTextileSlopeLength) +
                    (bioMixBottomWidth + aggregateBottomWidth) * (nonWovenGeoTextileSlopeLength) +
                    (aggregateBottomWidth * aggregateBottomLength)) / 9.0;
        }

        /// <summary>
        /// Calculate the area of a geosynthetic liner in square yards.
        /// </summary>
        /// <returns>The area of a geosynthetic liner in square yards</returns>
        public static double CalcGeosyntheticClayLinerArea(double freeboardTopLength, double freeboardTopWidth,
                                                           double aggregateBottomLength, double aggregateBottomWidth,
                                                           double linerSlopeLength)
        {
            return ((freeboardTopLength + aggregateBottomLength) * (linerSlopeLength + 2) +
                    (freeboardTopWidth + aggregateBottomWidth) * (linerSlopeLength + 2) +
                    (aggregateBottomWidth * aggregateBottomLength)) / 9.0;
        }

        /// <summary>
        /// Calculate the volume of a clay liner in cubic yards.
        /// </summary>
        /// <param name="clayLinerArea">The area of a clay liner in square yards</param>
        /// <param name="clayThickness">The thickness of a clay liner in feet</param>
        /// <returns>The volume of a clay liner in cubic yards</returns>
        public static double CalcClayLinerVolume(double clayLinerArea, double clayThickness)
        {
            return clayLinerArea * (clayThickness / 3.0);
        }

        /// <summary>
        /// Calculate the volume of a geosynthetic clay liner in cubic yards.
        /// </summary>
        /// <param name="geosyntheticClayLinerArea">The area of a geosynthetic clay liner in square yards</param>
        /// <param name="geosyntheticSoilCover">The soil cover thickness of a geosynthetic clay liner in feet</param>
        /// <returns>The volume of a geosynthetic clay liner in cubic yards</returns>
        public static double CalcGeosyntheticClayLinerVolume(double geosyntheticClayLinerArea, double geosyntheticSoilCover)
        {
            return geosyntheticClayLinerArea * (geosyntheticSoilCover / 3.0);
        }


        #endregion

        #region System Footprint Calculations - Excavation, Clear and Grub

        /// <summary>
        /// Calculate the volume of the excavation in cubic yards.
        /// </summary>
        /// <returns>The volume of the excavation in cubic yards</returns>
        public static double CalcExcavationVolume(double bioMixVolume, double waterVolume, double freeboardVolume, double linerVolume)
        {
            return bioMixVolume + waterVolume + freeboardVolume + linerVolume;
        }

        /// <summary>
        /// Calculate the area of the clear and grub in acres
        /// </summary>
        /// <param name="freeboardTopLength">The freeboard top length in feet, a user defined value</param>
        /// <param name="freeboardTopWidth">The freeboard top width in feet, a user defined value</param>
        /// <param name="freeboardDepth">The freeboard depth in feet, a user defined value</param>
        /// <returns>The area of the clear and grub in acres</returns>
        public static double CalcClearAndGrubArea(double freeboardTopLength, double freeboardTopWidth, double freeboardDepth)
        {
            double top_berm_width = 8;
            double outslope = 3;

            return ((freeboardTopWidth + 2 * top_berm_width + 2 * outslope * freeboardDepth) * (freeboardTopLength + 2 * top_berm_width + 2 * outslope * freeboardDepth)) / 43560.0;
        }
        #endregion

        #region System Footprint Calculations - Retention Times

        /// <summary>
        /// Calculate the water layer retention time in hours.
        /// </summary>
        /// <param name="waterVolume">The volume of the free standing water layer in cubic yards, a calculated value</param>
        /// <param name="designFlow">The design flow in gallons per minute, a water quality input value</param>
        /// <returns></returns>
        public static double CalcWaterRetentionTime(double waterVolume, double designFlow)
        {
            return waterVolume / (designFlow * (60.0 / (7.4805 * 27.0)));
        }

        /// <summary>
        /// Calculate the bio mix layer retention time in hours.
        /// </summary>
        /// <returns>The limestone layer retention time in hours</returns>
        public static double CalcBioMixLayerRetentionTime(double bioMixVolume, double compostMixPorosity, double designFlow)
        {
            return bioMixVolume * ((compostMixPorosity / 100.0) / (designFlow * (60.0 / (7.4805 * 27.0))));
        }

        #endregion

        #region Capital Costs

        /// <summary>
        /// Calculate the cost of excavation in dollars.
        /// </summary>
        /// <param name="excavationVolume">The volume of excavation in cubic yards</param>
        /// <param name="excavationUnitCost">The unit cost of excavation in dollar per cubic yards</param>
        /// <returns>The cost of excavation in dollars</returns>
        public static decimal CalcExcavationCost(double excavationVolume, decimal excavationUnitCost)
        {
            return (decimal)excavationVolume * excavationUnitCost;
        }

        /// <summary>
        /// Calculate the cost of manure in dollars.
        /// </summary>
        /// <param name="bioMixManureWeight">The manure weight in tons</param>
        /// <param name="bioMixManureUnitCost">The manure unit cost in dollar per ton</param>
        /// <returns>The cost of excavation in dollars</returns>
        public static decimal CalcBioMixManureCost(double bioMixManureWeight, decimal bioMixManureUnitCost)
        {
            return (decimal)bioMixManureWeight * bioMixManureUnitCost;
        }

        /// <summary>
        /// Calculate the cost of hay in dollars.
        /// </summary>
        /// <param name="bioMixHayWeight">The hay weight in tons</param>
        /// <param name="bioMixHayUnitCost">The hay unit cost in dollar per ton</param>
        /// <returns>The cost of excavation in dollars</returns>
        public static decimal CalcBioMixHayCost(double bioMixHayWeight, decimal bioMixHayUnitCost)
        {
            return (decimal)bioMixHayWeight * bioMixHayUnitCost;
        }

        /// <summary>
        /// Calculate the cost of woodChips in dollars.
        /// </summary>
        /// <param name="bioMixWoodChipsWeight">The wood chips weight in tons</param>
        /// <param name="bioMixWoodChipsUnitCost">The wood chips unit cost in dollar per ton</param>
        /// <returns>The cost of excavation in dollars</returns>
        public static decimal CalcBioMixWoodChipsCost(double bioMixWoodChipsWeight, decimal bioMixWoodChipsUnitCost)
        {
            return (decimal)bioMixWoodChipsWeight * bioMixWoodChipsUnitCost;
        }

        /// <summary>
        /// Calculate the cost of other in dollars.
        /// </summary>
        /// <param name="bioMixOtherWeight">The other weight in tons</param>
        /// <param name="bioMixOtherUnitCost">The other unit cost in dollar per ton</param>
        /// <returns>The cost of excavation in dollars</returns>
        public static decimal CalcBioMixOtherCost(double bioMixOtherWeight, decimal bioMixOtherUnitCost)
        {
            return (decimal)bioMixOtherWeight * bioMixOtherUnitCost;
        }

        /// <summary>
        /// Calculate the cost of limestoneFines in dollars.
        /// </summary>
        /// <param name="bioMixLimestoneFinesWeight">The limestoneFines weight in tons</param>
        /// <param name="bioMixLimestoneFinesUnitCost">The limestoneFines unit cost in dollar per ton</param>
        /// <returns>The cost of excavation in dollars</returns>
        public static decimal CalcBioMixLimestoneFinesCost(double bioMixLimestoneFinesWeight, decimal bioMixLimestoneFinesUnitCost)
        {
            return (decimal)bioMixLimestoneFinesWeight * bioMixLimestoneFinesUnitCost;
        }

        /// <summary>
        /// Calculate the cost of bio mix placement in dollars.
        /// </summary>
        /// <param name="bioMixVolume">The bio mix volume in cubic yards</param>
        /// <param name="bioMixPlacementUnitCost">The bio mix placement unit cost in dollar per cubic yards</param>
        /// <returns>The cost of bio mix placement in dollars</returns>
        public static decimal CalcBioMixPlacementCost(double bioMixVolume, decimal bioMixPlacementUnitCost)
        {
            return (decimal)bioMixVolume * bioMixPlacementUnitCost;
        }

        /// <summary>
        /// Calculate the total cost of bio mix in dollars.
        /// </summary>
        /// <returns>The total cost of bio mix in dollars</returns>
        public static decimal CalcBioMixCost(decimal bioMixManureCost, decimal bioMixHayCost, decimal bioMixWoodChipsCost,
                                             decimal bioMixOtherCost, decimal bioMixLimestoneFinesCost, decimal bioMixPlacementCost)
        {
            return bioMixManureCost + bioMixHayCost + bioMixWoodChipsCost + bioMixOtherCost + bioMixLimestoneFinesCost + bioMixPlacementCost;
        }

        public static decimal CalcAggregateCost(double aggregateVolume, double aggregateWeight, decimal aggregateUnitCost, decimal aggregatePlacementUnitCost)
        {
            return ((decimal)aggregateWeight * aggregateUnitCost) + ((decimal)aggregateVolume * aggregatePlacementUnitCost);
        }

        /// <summary>
        /// Calculate the cost of influent/effluent pipe in dollars.
        /// </summary>
        /// <param name="InOutPipeLength">The length of influent/effluent pipe in feet, a user defined value</param>
        /// <param name="InOutPipeUnitCost">The unit cost of influent/effluent pipe in dollar per each, a user defined value</param>
        /// <returnsThe cost of influent/effluent pipe in dollars></returns>
        public static decimal CalcInOutPipeCost(double InOutPipeLength, decimal InOutPipeUnitCost)
        {
            return (decimal)InOutPipeLength * InOutPipeUnitCost;
        }

        /// <summary>
        /// Calculate the cost of trunk pipe in dollars.
        /// </summary>
        /// <param name="trunkPipeSegmentLengthTotal">The total length of trunk pipe in feet, a calculated value</param>
        /// <param name="trunkPipeUnitCost">The unit cost of trunk pipe in dollar per each, a user defined value</param>
        /// <returnsThe cost of trunk pipe in dollars></returns>
        public static decimal CalcTrunkPipeCost(double trunkPipeSegmentLengthTotal, decimal trunkPipeUnitCost)
        {
            return (decimal)trunkPipeSegmentLengthTotal * trunkPipeUnitCost;
        }

        /// <summary>
        /// Calculate the unit cost of trunk pipe couplers.
        /// </summary>
        /// <param name="trunkPipeCouplerQuantity">The quantity (number) of trunk pipe couplers, a calculated value</param>
        /// <param name="trunkPipeUnitCost">The unit cost of trunk pipe couplers in dollar per each, a user defined value</param>
        /// <returns></returns>
        public static decimal CalcTrunkPipeCouplerCost(double trunkPipeCouplerQuantity, decimal trunkPipeUnitCost)
        {
            return (decimal)trunkPipeCouplerQuantity * trunkPipeUnitCost;
        }

        /// <summary>
        /// Calculate the cost of spur pipe in dollars.
        /// </summary>
        /// <param name="SpurPipeSegmentLengthTotal">The total length of spur in feet, a calculated value</param>
        /// <param name="spurPipeUnitCost">The unit cost of spur pipe in dollar per foot, a user defined value</param>
        /// <returns>The cost of spur pipe in dollars</returns>
        public static decimal CalcSpurPipeCost(double SpurPipeSegmentLengthTotal, decimal spurPipeUnitCost)
        {
            return (decimal)SpurPipeSegmentLengthTotal * spurPipeUnitCost;
        }

        /// <summary>
        /// Calculate the cost of tee connectors in dollars.
        /// </summary>
        /// <param name="spurPipeQuantity">The quantity (number) of spur pipe, a calculated value</param>
        /// <param name="teeUnitCost">The unit cost of tee connectors, a user defined value</param>
        /// <returns>The cost of tee connectors in dollars</returns>
        public static decimal CalcTeeConnectorCost(double spurPipeQuantity, decimal teeConnectorUnitCost)
        {
            return (decimal)spurPipeQuantity * teeConnectorUnitCost;
        }


        /// <summary>
        /// Calculate the cost of spur couplers in dollars.
        /// </summary>
        /// <param name="spurCouplerQuantity">The quantity (number) of spur couplers, a calculated value</param>
        /// <param name="spurCouplerUnitCost">The unit cost of spur couplers, a user defined value</param>
        /// <returns></returns>
        public static decimal CalcSpurPipeCouplerCost(double spurCouplerQuantity, decimal spurCouplerUnitCost)
        {
            return (decimal)spurCouplerQuantity * spurCouplerUnitCost;
        }

        /// <summary>
        /// Calculate the cost of pipe in dollars.
        /// </summary>
        /// <param name="inOutPipeCost">The cost of influent/effluent pipe in dollars, a calculated value</param>
        /// <param name="trunkPipeCost">The cost of trunk pipe in dollars, a calculated value</param>
        /// <param name="trunkPipeCouplerCost">The cost of trunk pipe couplers in dollars, a calculated value</param>
        /// <param name="teeConnectorCost">The cost of tee connectors in dollars, a calculated value</param>
        /// <param name="spurPipeCost">The cost of spur pipe in dollars, a calculated value</param>
        /// <param name="spurCouplerCost">The cost of spur couplers in dollars, a calculated value</param>
        /// <param name="inOutPipeLength">The length of inout pipe in feet, a user defined value</param>
        /// <param name="aggregateBottomLength">The bottom length of aggregate in feet, a calculated value</param>
        /// <param name="spurPipeLengthTotal">The total length of spur pipe in feet, a calculated value</param>
        /// <param name="inOutPipeInstallRate">The rate to install inout pipe in feet per hour, a user defined value</param>
        /// <param name="trunkSpurPipeInstallRate">The rate to install trunk/spur pipe in feet per hour, a user defined value</param>
        /// <param name="laborRate">The rate of labor in dollar per hour, a user defined value</param>
        /// <returns>The cost of pipe in dollars</returns>
        public static decimal CalcAmdtreatPipeCost(decimal inOutPipeCost,
                                                   decimal trunkPipeCost,
                                                   decimal trunkPipeCouplerCost,
                                                   decimal teeConnectorCost,
                                                   decimal spurPipeCost,
                                                   decimal spurCouplerCost,
                                                   double inOutPipeLength,
                                                   double aggregateBottomLength,
                                                   double spurPipeLengthTotal,
                                                   double inOutPipeInstallRate,
                                                   double trunkSpurPipeInstallRate,
                                                   decimal laborRate)
        {
            return inOutPipeCost + trunkPipeCost + trunkPipeCouplerCost + teeConnectorCost + spurPipeCost + spurCouplerCost +
                   ((decimal)(inOutPipeLength / inOutPipeInstallRate) * laborRate) + ((decimal)((aggregateBottomLength + spurPipeLengthTotal) / trunkSpurPipeInstallRate) * laborRate);
        }

        /// <summary>
        /// Calculate the cost of custom pipe in dollars
        /// </summary>
        /// <param name="customPipeLength1">The length of custom pipe in feet, a user defined value</param>
        /// <param name="customPipeUnitCost1">The unit cost of custom pipe in dollar per foot, a user defined value</param>
        /// <param name="customPipeLength2">The length of custom pipe in feet, a user defined value</param>
        /// <param name="customPipeUnitCost2">The unit cost of custom pipe in dollar per foot, a user defined value</param>
        /// <param name="customPipeLength3">The length of custom pipe in feet, a user defined value</param>
        /// <param name="customPipeUnitCost3">The unit cost of custom pipe in dollar per foot, a user defined value</param>
        /// <returns>The cost of pipe in dollars</returns>
        public static decimal CalcCustomPipeCost(double customPipeLength1, decimal customPipeUnitCost1, double customPipeLength2, decimal customPipeUnitCost2, double customPipeLength3, decimal customPipeUnitCost3)
        {
            return (decimal)customPipeLength1 * customPipeUnitCost1 + (decimal)customPipeLength2 * customPipeUnitCost2 + (decimal)customPipeLength3 * customPipeUnitCost3;
        }

        /// <summary>
        /// Calculate the cost of clay liner in dollars.
        /// </summary>
        /// <param name="clayLinerVolume">The volume of clay liner in cubic yards</param>
        /// <param name="clayLinerUnitCost">The unit cost of clay liner in dollar per cubic yards</param>
        /// <returns>The cost of clay liner in dollars</returns>
        public static decimal CalcClayLinerCost(double clayLinerVolume, decimal clayLinerUnitCost)
        {
            return (decimal)clayLinerVolume * clayLinerUnitCost;
        }

        /// <summary>
        /// Calculate the cost of synthetic liner in dollars.
        /// </summary>
        /// <param name="syntheticLinerArea">The area of synthetic liner in square yards</param>
        /// <param name="syntheticLinerUnitCost">The unit cost of synthetic liner in dollar per square yards</param>
        /// <returns>The cost of synthetic liner in dollars</returns>
        public static decimal CalcSyntheticLinerCost(double syntheticLinerArea, decimal syntheticLinerUnitCost)
        {
            return (decimal)syntheticLinerArea * syntheticLinerUnitCost;
        }

        /// <summary>
        /// Calculate the cost of geosynthetic clay liner in dollars.
        /// </summary>
        /// <param name="geosyntheticClayLinerArea">The area of geosynthetic clay liner in square yards</param>
        /// <param name="geosyntheticClayLinerUnitCost">The unit cost of geosynthetic clay liner in dollar per square yards</param>
        /// <returns>The cost of clay liner in dollars</returns>
        public static decimal CalcGeosyntheticClayLinerCost(double geosyntheticClayLinerArea, decimal geosyntheticClayLinerUnitCost)
        {
            return (decimal)geosyntheticClayLinerArea * geosyntheticClayLinerUnitCost;
        }

        /// <summary>
        /// Calculate the cost of geosynthetic clay liner cover in dollars.
        /// </summary>
        /// <param name="geosyntheticClayLinerVolume">The volume of geosynthetic clay liner in cubic yards</param>
        /// <param name="geosyntheticClayLinerCoverUnitCost">The unit cost of geosynthetic clay liner cover in dollar per cubic yards</param>
        /// <returns>The cost of clay liner in dollars</returns>
        public static decimal CalcGeosyntheticClayLinerCoverCost(double geosyntheticClayLinerVolume, decimal geosyntheticClayLinerCoverUnitCost)
        {
            return (decimal)geosyntheticClayLinerVolume * geosyntheticClayLinerCoverUnitCost;
        }

        /// <summary>
        /// Calculate the cost of non woven geotextile in dollars.
        /// </summary>
        /// <param name="nonWovenGeotextileArea">The area of non woven geotextile in square yards</param>
        /// <param name="nonWovenGeotextileUnitCost">The unit cost of non woven geotextile in dollar per square yards</param>
        /// <returns>The cost of clay liner in dollars</returns>
        public static decimal CalcNonWovenGeotextileCost(double nonWovenGeotextileArea, decimal nonWovenGeotextileUnitCost)
        {
            return (decimal)nonWovenGeotextileArea * nonWovenGeotextileUnitCost;
        }

        /// <summary>
        /// Calculate the cost of valves in dollars.
        /// </summary>
        /// <param name="valveQuantity">The quantity (number) of valves</param>
        /// <param name="valveUnitCost">The unit cost of the values in dollar per each</param>
        /// <returns>The cost of valves in dollars</returns>
        public static decimal CalcValveCost(double valveQuantity, decimal valveUnitCost)
        {
            return (decimal)valveQuantity * valveUnitCost;
        }

        /// <summary>
        /// Calculate the appurtenance cost in dollars.
        /// </summary>
        /// <param name="valveCost">The cost of valves in dollars, a calculated value.</param>
        /// <param name="intakeQuantity">The quantity of intake structures, a user defined value</param>
        /// <param name="intakeUnitCost">The unit cost of intake structures in dollar per each, a user defined value</param>
        /// <param name="flowDistributionQuantity">The quantity of flow distribution structures, a user defined value</param>
        /// <param name="flowDistributionUnitCost">The unit cost of flow distribution structures in dollar per each, a user defined value</param>
        /// <param name="waterLevelControlQuantity">The quantity of water level control structures, a user defined value</param>
        /// <param name="waterLevelControlUnitCost">The unit cost of water level control structures in dollar per each, a user defined value</param>
        /// <param name="outletProtectionQuantity">The quantity of outlet protection structures, a user defined value</param>
        /// <param name="outletProtectionUnitCost">The unit cost of outlet protection structures in dollar per each, a user defined value</param>
        /// <returns>The appurtenance cost in dollars</returns>
        public static decimal CalcOtherItemsCost(decimal valveCost,
                                                 double intakeQuantity, decimal intakeUnitCost,
                                                 double flowDistributionQuantity, decimal flowDistributionUnitCost,
                                                 double waterLevelControlQuantity, decimal waterLevelControlUnitCost,
                                                 double outletProtectionQuantity, decimal outletProtectionUnitCost)
        {
            return valveCost +
                   ((decimal)intakeQuantity * intakeUnitCost) +
                   ((decimal)flowDistributionQuantity * flowDistributionUnitCost) +
                   ((decimal)waterLevelControlQuantity * waterLevelControlUnitCost) +
                   ((decimal)outletProtectionQuantity * outletProtectionUnitCost);
        }

        /// <summary>
        /// Calculate the total capital cost in dollars.
        /// </summary>
        /// <returns>The total cost in dollars</returns>
        public static decimal CalcCapitalCostTotal(decimal bioMixCost, decimal aggregateCost, decimal excavationCost, decimal linerCost, decimal pipeCost, decimal otherItemsCost)
        {
            return bioMixCost + aggregateCost + excavationCost + linerCost + pipeCost + otherItemsCost;
        }

        #endregion

        #region Annual (Operations and Maintenance) Costs

        /// <summary>
        /// Calculate the annual (operations and maintenance) costs in dollars.  
        /// </summary>
        /// <param name="capitalCostTotal">The total capital cost in dollars, a calculated value</param>
        /// <param name="annualCostMultiplier">The annual cost multipler in percent, a user defined value</param>
        /// <returns>The annual (operations and maintenance) costs in dollars</returns>
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

        /// <summary>
        /// Calculate the total recapitalization cost.
        /// </summary>
        /// <returns>The total recapitalization cost</returns>
        public static decimal CalcRecapitalizationCostTotal(decimal bioMixRecapCost, decimal aggregateRecapCost,
                                                            decimal linerRecapCost, decimal pipeRecapCost,
                                                            decimal otherItemsRecapCost, decimal annualRecapCost)
        {
            return bioMixRecapCost + aggregateRecapCost +
                   linerRecapCost + pipeRecapCost +
                   otherItemsRecapCost + annualRecapCost;
        }

        #endregion

        #region Error Checking

        /// <summary>
        /// Calculate the maximum allowable depth of a pond.
        /// </summary>
        /// <param name="freeboardTopWidth">The freeboard top width in feet, a user defined value</param>
        /// <returns>The maximum allowable depth for a pond in feet</returns>
        public static double CalcMaxPondDepth(double freeboardTopWidth)
        {
            return (freeboardTopWidth - 10.0) / 4.0;
        }

        /// <summary>
        /// Calculate the current total depth 
        /// </summary>
        /// <param name="freeboardDepth">The freeboard depth in feet, a user defined value</param>
        /// <param name="freeStandingWaterDepth">The free standing water depth in feet, a user defined value</param>
        /// <param name="bioMixDepth">The bioMix depth in feet, a user defined value</param>
        /// <param name="aggregateDepth">The organic matter depth in feet, a user defined value</param>
        /// <returns>The total depth of a pond in feet</returns>
        public static double CalcPondDepth(double freeboardDepth, double freeStandingWaterDepth, double bioMixDepth, double aggregateDepth)
        {
            return freeboardDepth + freeStandingWaterDepth + bioMixDepth + aggregateDepth;
        }

        #endregion

    }
}
