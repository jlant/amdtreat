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
    public static class VerticalFlowPondCalculations
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
        public static double CalcTopDimension(double dimension, double depth, double slope)
        {
            return dimension + 2 * slope * depth;
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
        /// <param name="slope">The side slope of the pond, a user defined value</param>
        /// <returns>The volume of a layer in cubic yards</returns>
        public static double CalcLayerVolume(double bottomLength, double bottomWidth, double depth, double slope)
        {
            double topLength = CalcTopDimension(bottomLength, depth, slope);
            double topWidth = CalcTopDimension(bottomWidth, depth, slope);

            return ((topWidth * topLength + bottomWidth * bottomLength) / 2.0) * depth / 27.0;
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

        #endregion

        #region Sizing Calculations for Retention Time
        /// <summary>
        /// Calculate water volume in cubic yards. Used in sizing calculation based on retention time.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="retentionTime">The retention time in hours, a user defined value</param>
        /// <param name="designFlow">The design flow in gallons per minute, a water quality input value</param>
        /// <returns>The volume of water in cubic yards</returns>
        public static double CalcWaterVolumeBasedOnRetentionTime(double retentionTime, double designFlow)
        {
            return retentionTime * designFlow * (60.0 / (7.4805 * 27.0));
        }

        /// <summary>
        /// Calculate the volume of limestone in cubic yards. Used in sizing calculation based on retention time.
        /// </summary>
        /// <remarks>
        /// Notes:
        ///     Water volume is calculated in the calcWaterVolumeBasedOnRetentionTime() function.
        /// </remarks>
        /// <param name="waterVolume">The volume of water in cubic yards, a calculated value</param>
        /// <param name="voidSpace">The void space of the limestone bed as a percentage, a user defined value</param>
        /// <returns>The volume of limestone in cubic yards</returns>
        public static double CalcLimestoneVolumeBasedOnRetentionTime(double waterVolume, double voidSpace)
        {
            return waterVolume / (voidSpace / 100.0);
        }

        #endregion

        #region Sizing Calculations for Bureau of Mines

        /// <summary>
        /// Calculate the acidity loading in tons. Used in sizing calculation based on Bureau of Mines.
        /// </summary>
        /// <remarks> 
        /// </remarks>       
        /// <param name="designFlow">The design flow in gallons per minute, a water quality input value</param>
        /// <param name="netAcidity">The net acidity in milligrams per liter, a water quality input value</param>
        /// <param name="neutralizationPeriod">The neutralization Period expectancy in years, a user defined value</param>
        /// <param name="limestonePurity">The purity of limestone as a percentage, a user defined value</param>
        /// <param name="limestoneEfficiency">The efficiency of limestone as a percentage, a user defined value</param>
        /// <returns>The acidity loading in tons</returns>
        public static double CalcAcidityLoadingBasedOnBureauOfMines(double designFlow, double netAcidity, double neutralizationPeriod, double limestonePurity, double limestoneEfficiency)
        {
            return designFlow * netAcidity * 0.002189 * (neutralizationPeriod / ((limestonePurity / 100.0) * (limestoneEfficiency / 100.0)));
        }

        /// <summary>
        /// Calculate the volume of limestone in cubic yards. Used in sizing calculation based on Bureau of Mines.
        /// </summary>
        /// <remarks>
        /// Notes:
        ///     Acidity is calculated in the calcAcidityLoading() function.
        /// </remarks>
        /// <param name="acidLoading">The acidity loading in tons, a calculated value</param>
        /// <param name="limestoneBulkDensity">The bulk density of limestone in pounds per cubic feet, a calculated value</param>
        /// <returns>The volume of limestone in cubic yards</returns>
        public static double CalcLimestoneVolumeBasedOnBureauOfMines(double acidLoading, double limestoneBulkDensity)
        {
            return (acidLoading / limestoneBulkDensity) * (2000.0 / 27.0);
        }

        /// <summary>
        /// Calculate the total weight of limestone in tons. Used in sizing calculation based on Bureau of Mines. 
        /// </summary>
        /// <remarks>
        /// Notes:
        ///     Limestone volume based on retention time is calculated in the calcLimestoneVolumeBasedOnRetentionTime() function.
        ///     Acidity is calculated in the calcAcidityLoading() function.
        /// </remarks>
        /// <param name="limestoneWeightBasedOnRetentionTime">The weight of limestone in tons based on retention time, a calculated value</param>
        /// <param name="acidityLoading">The acidity loading in tons, a calculated value</param>
        /// <returns>The total weight of limestone in tons</returns>
        public static double CalcTotalLimestoneWeightBasedOnBureauOfMines(double limestoneWeightBasedOnRetentionTime, double acidityLoading)
        {
            return limestoneWeightBasedOnRetentionTime + acidityLoading;
        }

        /// <summary>
        /// Calculate the total volume of limestone in cubic yards.  Used in sizing calculation based on Bureau of Mines.  
        /// </summary>
        /// <remarks>
        /// Notes:
        ///     Limestone volume based on retention time is calculated in the calcLimestoneVolumeBasedOnRetentionTime() function.
        ///     Limestone volume based on bureau of mines is calculated in the calcTotalLimestoneWeightBasedOnBureauOfMines() function.
        /// </remarks>
        /// <param name="limestoneVolumeBasedOnRetentionTime">The volume of limestone in cubic yards based on retention time, a calculated value</param>
        /// <param name="limestoneVolumeBasedOnBureauOfMines">The volume of limestone in cubic yards based on Bureau of Mines, a calculated value</param>
        /// <returns>The total volume of limestone in cubic yards</returns>
        public static double CalcTotalLimestoneVolumeBasedOnBureauOfMines(double limestoneVolumeBasedOnRetentionTime, double limestoneVolumeBasedOnBureauOfMines)
        {
            return limestoneVolumeBasedOnRetentionTime + limestoneVolumeBasedOnBureauOfMines;
        }

        #endregion

        #region Sizing Calculations for Alkalinity Generation

        /// <summary>
        /// Calculate the acidity rate in grams per day.  Used in sizing calculation based on Alkalinity Generation
        /// </summary>
        /// <param name="designFlow">The design flow in gallons per minute, a water quality input value</param>
        /// <param name="netAcidity">The net acidity in milligrams per liter, a water quality input value</param>
        /// <returns>The acidity rate in grams per day</returns>
        public static double CalcAcidityRateBasedOnAlkalinityGeneration(double designFlow, double netAcidity)
        {
            return (designFlow * 3.785412 * netAcidity * 1440.0) / 1000.0;
        }

        /// <summary>
        /// Calculate the surface area of the top of limestone in square yards.  Used in sizing calculation based on Alkalinity Generation.
        /// </summary>
        /// <param name="acidityRate">The acidity rate in grams per day, a calculated value</param>
        /// <param name="alklinityGenerationRate">The alklinity generation rate in milligrams per liter, a user defined value</param>
        /// <returns>The surface area of the top of limestone in square yards</returns>
        public static double CalcSurfaceAreaTopOfLimestoneBasedOnAlkalinityGeneration(double acidityRate, double alklinityGenerationRate)
        {
            return (acidityRate / alklinityGenerationRate) * 1.196;
        }


        /// <summary>
        /// Calculate the bottom length of the limestone layer in feet.  Uses the quadratic equation to solve for the bottom length
        /// of the pond based on known volume, length to width ratio, and depth of limestone.
        /// </summary>
        /// <param name="surfaceArea">The surface area of the top of limestone square yards, a calculated value</param>
        /// <param name="limestoneDepth">The limestone depth in feet, a user defined value</param>
        /// <param name="pondInsideSlope">The side slope of the pond, a user defined value</param>
        /// <param name="bottomLengthToWidthRatio">The bottom length to width ratio, a user defined value or a calculate valued</param>
        /// <returns>The bottom length of the limestone layer in feet</returns>
        public static double CalcLimestoneLayerBottomLengthBasedOnAlkalinityGeneration(double surfaceArea, double limestoneDepth, double pondInsideSlope, double bottomLengthToWidthRatio)
        {

            double z = 2 * pondInsideSlope * limestoneDepth;

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
        /// Calculate the volume of limestone in cubic yards.  Used in sizing calculation based on Alkalinity Generation.
        /// </summary>
        /// <param name="limestoneLayerBottomLength">The bottom length of the limestone layer in feet, a calculated value</param>
        /// <param name="pondInsideSlope">The side slope of the pond, a user defined value</param>
        /// <param name="limestoneDepth">The depth of limestone, a user define value</param>
        /// <param name="bottomLengthToWidthRatio">The bottom length to width ratio, a user defined value or a calculate valued</param>
        /// <returns>The volume of limestone in cubic yards</returns>
        public static double CalcLimestoneVolumeBasedOnAlkalinityGeneration(double limestoneLayerBottomLength,
                                                                            double limestoneDepth,
                                                                            double pondInsideSlope,
                                                                            double bottomLengthToWidthRatio)
        {
            double limestoneLayerBottomWidth = limestoneLayerBottomLength / bottomLengthToWidthRatio;
            double limestoneLayerTopLength = limestoneLayerBottomLength + (2 * pondInsideSlope * limestoneDepth);
            double limestoneLayerTopWidth = limestoneLayerBottomWidth + (2 * pondInsideSlope * limestoneDepth);

            return (((limestoneLayerBottomLength * limestoneLayerBottomWidth) + (limestoneLayerTopLength * limestoneLayerTopWidth)) / 2.0) * (limestoneDepth / 27.0);
        }

        // FIXME: Remove previous routine below
        // ------------------------------------

        ///// <summary>
        ///// Calculate the surface area of the bottom of limestone in square yards.  Used in sizing calculation based on Alkalinity Generation.
        ///// </summary>
        ///// <param name="surfaceAreaTopOfLimestoneBasedOnAlkalinityGeneration">The surface area of the top of limestone in square yards, a calculated value</param>
        ///// <param name="PondInsideSlope">The side slope of the pond, a user defined value</param>
        ///// <param name="limestoneDepth">The depth of limestone, a user define value</param>
        ///// <returns>The surface area of the bottom of limestone in square yards</returns>
        //public static double CalcSurfaceAreaBottomOfLimestoneBasedOnAlkalinityGeneration(double surfaceAreaTopOfLimestoneBasedOnAlkalinityGeneration,
        //                                                                                 double PondInsideSlope,
        //                                                                                 double limestoneDepth)
        //{
        //    double bottom_area = (Math.Sqrt(surfaceAreaTopOfLimestoneBasedOnAlkalinityGeneration) - 2.0 * PondInsideSlope * (limestoneDepth / 3.0));

        //    return Math.Pow(bottom_area, 2);
        //}

        ///// <summary>
        ///// Calculate the volume of limestone in cubic yards.  Used in sizing calculation based on Alkalinity Generation.
        ///// </summary>
        ///// <param name="surfaceAreaTopOfLimestoneBasedOnAlkalinityGeneration">The surface area of the top of limestone in square yards, a calculated value</param>
        ///// <param name="surfaceAreaBottomOfLimestoneBasedOnAlkalinityGeneration">The surface area of the bottom of limestone in square yards, a calculated value</param>
        ///// <param name="limestoneDepth">The depth of limestone, a user define value</param>
        ///// <returns>The volume of limestone in cubic yards</returns>
        //public static double CalcLimestoneVolumeBasedOnAlkalinityGeneration(double surfaceAreaTopOfLimestoneBasedOnAlkalinityGeneration,
        //                                                                    double surfaceAreaBottomOfLimestoneBasedOnAlkalinityGeneration,
        //                                                                    double limestoneDepth)
        //{
        //    return (((surfaceAreaTopOfLimestoneBasedOnAlkalinityGeneration + surfaceAreaBottomOfLimestoneBasedOnAlkalinityGeneration) / 2.0) * (limestoneDepth / 3.0));
        //}


        #endregion

        #region Sizing Calculations for User-Specified Limestone 

        /// <summary>
        /// Calculate the volume of limestone in cubic yards.  Used in sizing calculations based on Limestone Entered by User
        /// </summary>
        /// <param name="limestoneQuantity">The limestone amount needed in tons, a user defined value</param>
        /// <param name="limestoneBulkDensity">The bulk density of limestone in pounds per cubic feet, a calculated value</param>
        /// <returns>The volume of limestone in cubic yards</returns>
        public static double CalcLimestoneVolumeBasedOnLimestoneEntered(double limestoneQuantity, double limestoneBulkDensity)
        {
            return limestoneQuantity * (2000.0 / (limestoneBulkDensity * 27.0));
        }

        #endregion

        #region Sizing Calculations for Dimensions Entered by User

        /// <summary>
        /// Calculate limestone bottom dimension, length or width, in feet.  Used in sizing calculations based on Dimensions Entered by User
        /// </summary>
        /// <param name="freeboardTopDimension">The freeboard top dimension, length or width, in feet, a user defined value</param>
        /// <param name="freeboardDepth">The freeboard depth in feet, a user defined value</param>
        /// <param name="freeStandingWaterDepth">The free standing water depth in feet, a user defined value</param>
        /// <param name="CompostMixDepth">The organic matter depth in feet, a user defined value</param>
        /// <param name="limestoneDepth">The limestone depth in feet, a user defined value</param>
        /// <param name="PondInsideSlope">The inside slope of the pond, a user defined value</param>
        /// <returns>The limestone bottom length dimension in feet</returns>
        public static double CalcLimestoneBottomDimensionBasedOnDimensionsEntered(double freeboardTopDimension, double freeboardDepth, double freeStandingWaterDepth,
                                                                                  double compostMixDepth, double limestoneDepth, double PondInsideSlope)
        {
            return freeboardTopDimension - (2.0 * PondInsideSlope * (freeboardDepth + freeStandingWaterDepth + compostMixDepth + limestoneDepth));
        }

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
        /// <param name="compostMixDepth">The organic matter depth in feet, a user defined value</param>
        /// <param name="limestoneDepth">The limestone depth in feet, a user defined value</param>
        /// <returns>The total depth of a pond in feet</returns>
        public static double CalcPondDepth(double freeboardDepth, double freeStandingWaterDepth, double compostMixDepth, double limestoneDepth)
        {
            return freeboardDepth + freeStandingWaterDepth + compostMixDepth + limestoneDepth;
        }

        /// <summary>
        /// Calculate limestone volume in cubic yards.  Used in sizing calculations based on Dimensions Entered by User
        /// </summary>
        /// <param name="freeboardTopLength">The freeboard top length in feet, a user defined value</param>
        /// <param name="freeboardTopWidth">The freeboard top width in feet, a user defined value</param>
        /// <param name="freeboardDepth">The freeboard depth in feet, a user defined value</param>
        /// <param name="freeStandingWaterDepth">The freeboard standing water depth in feet, a user defined value</param>
        /// <param name="CompostMixDepth">The organic matter depth in feet, a user defined value</param>
        /// <param name="limestoneDepth">The limestone depth in feet, a user defined value</param>
        /// <param name="PondInsideSlope">The side slope of the pond, a user defined value</param>
        /// <returns>The volume of limestone in cubic yards</returns>
        public static double CalcLimestoneVolumeBasedOnDimensionsEntered(double freeboardTopLength, double freeboardTopWidth,
                                                                         double freeboardDepth, double freeStandingWaterDepth,
                                                                         double CompostMixDepth, double limestoneDepth,
                                                                         double PondInsideSlope)
        {
            double limestoneBottomWidthDimension = CalcLimestoneBottomDimensionBasedOnDimensionsEntered(freeboardTopWidth, freeboardDepth, freeStandingWaterDepth,
                                                                                                        CompostMixDepth, limestoneDepth, PondInsideSlope);

            double limestoneBottomLengthDimension = CalcLimestoneBottomDimensionBasedOnDimensionsEntered(freeboardTopLength, freeboardDepth, freeStandingWaterDepth,
                                                                                                         CompostMixDepth, limestoneDepth, PondInsideSlope);

            return CalcLayerVolume(limestoneBottomWidthDimension, limestoneBottomLengthDimension, limestoneDepth, PondInsideSlope);
        }

        /// <summary>
        /// Calculate the bottom layer length to width ratio.  Used in sizing calculations based on Dimensions Entered by User
        /// </summary>
        /// <param name="bottomLayerLength">The bottom length dimension of a layer in feet, a calculated value</param>
        /// <param name="bottomLayerWidth">The bottom width dimension in feet, a calculated value</param>
        /// <returns>The limestone bottom length to width ratio</returns>
        public static double CalcBottomLayerLengthToWidthRatioBasedOnDimensionsEntered(double bottomLayerLength, double bottomLayerWidth)
        {
            return bottomLayerLength / bottomLayerWidth;
        }


        #endregion

        #region System Footprint Calculations - Limestone Layer

        /// <summary>
        /// Calculate the bottom length of the limestone layer in feet.  Uses the quadratic equation to solve for the bottom length
        /// of the pond based on known volume, length to width ratio, and depth of limestone.
        /// </summary>
        /// <param name="limestoneVolume">The volume of limestone in cubic yards, a calculated value</param>
        /// <param name="limestoneDepth">The limestone depth in feet, a user defined value</param>
        /// <param name="PondInsideSlope">The side slope of the pond, a user defined value</param>
        /// <param name="bottomLengthToWidthRatio">The bottom length to width ratio, a user defined value or a calculate valued</param>
        /// <returns>The bottom length of the limestone layer in feet</returns>
        public static double CalcLimestoneLayerBottomLength(double limestoneVolume, double limestoneDepth, double PondInsideSlope, double bottomLengthToWidthRatio)
        {

            double a = limestoneDepth / bottomLengthToWidthRatio;
            double b = (PondInsideSlope * Math.Pow(limestoneDepth, 2)) + (PondInsideSlope * Math.Pow(limestoneDepth, 2) / bottomLengthToWidthRatio);
            double c = (2 * Math.Pow(PondInsideSlope, 2) * Math.Pow(limestoneDepth, 3)) - (limestoneVolume * 27);

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
        /// Calculate the bottom width of the limestone layer in feet.
        /// </summary>
        /// <param name="limestoneLayerBottomLength">The bottom length of the limestone layer in feet, a calculated value</param>
        /// <param name="bottomLengthToWidthRatio">The bottom length to width ratio, a user defined value or a calculate valued</param>
        /// <returns>The bottom width of the limestone layer in feet</returns>
        public static double CalcLimestoneLayerBottomWidth(double limestoneLayerBottomLength, double bottomLengthToWidthRatio)
        {
            return limestoneLayerBottomLength / bottomLengthToWidthRatio;
        }

        #endregion

        #region System Footprint Calculations - Compost Mix

        /// <summary>
        /// Calculate the volume of limestone fines in an organic matter layer in cubic yards.
        /// </summary>
        /// <param name="compostMixLayerVolume">The volume of the organic matter layer in cubic yards, a calculated value</param>
        /// <param name="limestoneFinesPercentage">The percentage of limestone fines mixed with organic matter, a user defined value</param>
        /// <returns>The volume of limestone fines in an organic matter layer in cubic yards</returns>
        public static double CalcCompostMixLimestoneFinesVolume(double compostMixLayerVolume, double limestoneFinesPercentage)
        {
            return compostMixLayerVolume * (limestoneFinesPercentage / 100.0);
        }

        /// <summary>
        /// Calculate the volume of organic material in an organic matter layer in cubic yards.
        /// </summary>
        /// <param name="compostMixLayerVolume">The volume of a organic matter layer in cubic yards, a calculated value</param>
        /// <param name="compostMixLayerLimestoneFinesVolume">The volume of limestone fines in an organic matter layer in cubic yards, a calculated value</param>
        /// <returns>The volume of organic material in an organic matter layer in cubic yards</returns>
        public static double CalcCompostMixLayerOrganicMaterialVolume(double compostMixLayerVolume, double compostMixLayerLimestoneFinesVolume)
        {
            return compostMixLayerVolume - compostMixLayerLimestoneFinesVolume;
        }

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

        #region System Footprint Calculations - Pipe

        /// <summary>
        /// Calculate the total length of trunk pipe in feet.
        /// Note: This just returns the limestone layer bottom length.  Keeping this function in case there are future changes to the equation.
        /// </summary>
        /// <param name="limestoneBottomLength">The length of the limestone layer bottom in feet, a calculated value</param>
        /// <returns>The total length of trunk pipe in feet</returns>
        public static double CalcTrunkPipeSegmentLengthTotal(double limestoneLayerBottomLength)
        {
            return limestoneLayerBottomLength;
        }

        /// <summary>
        /// Calculate the quantity (number) of trunk pipe couplers.
        /// </summary>
        /// <param name="TrunkPipeSegmentLengthTotal">The total length of trunk pipe in feet, a calculated value</param>
        /// <param name="truckPipeSegmentLength">The length of trunk pipe segment in feet, a user defined value</param>
        /// <returns>The quantity (number) of trunk pipe couplers</returns>
        public static double CalcTrunkPipeCouplerQuantity(double TrunkPipeSegmentLengthTotal, double truckPipeSegmentLength)
        {
            return Math.Ceiling(TrunkPipeSegmentLengthTotal / truckPipeSegmentLength);
        }

        /// <summary>
        /// Calculate the quantity of spur pipe.
        /// </summary>
        /// <param name="spurPipeSpacing">The spur pipe spacing in feet, a user define value</param>
        /// <param name="limestoneLayerBottomLength">The length of the limestone layer bottom in feet, a calculate value</param>
        /// <returns></returns>
        public static double CalcSpurPipeQuantity(double spurSpacing, double limestoneLayerBottomLength)
        {
            return Math.Floor(limestoneLayerBottomLength / spurSpacing) + 1.0;
        }

        /// <summary>
        /// Calculate the total length of spur pipe in feet.
        /// </summary>
        /// <param name="spurPipeQuantity">The quantity (number) of spur pipe, a calculated value</param>
        /// <param name="limestoneLayerBottomWidth">The width of the limestone layer bottom in feet, a calculated value</param>
        /// <returns>The total length of spur pipe in feet</returns>
        public static double CalcSpurPipeSegmentLengthTotal(double spurPipeQuantity, double limestoneLayerBottomWidth)
        {
            return spurPipeQuantity * limestoneLayerBottomWidth;
        }

        /// <summary>
        /// Calculate the quantity (number) of spur couplers.
        /// </summary>
        /// <param name="spurPipeQuantity">The quantity (number) of spur pipe, a calculated value</param>
        /// <param name="spurPipeSegmentLength">The length of a spur segment, a user defined value</param>
        /// <param name="limestoneLayerBottomWidth">The width of the limestone layer bottom in feet, a calculated value</param>
        /// <returns>The quantity (number) of spur couplers</returns>
        public static double CalcSpurPipeCouplerQuantity(double spurPipeQuantity, double spurPipeSegmentLength, double limestoneLayerBottomWidth)
        {
            return spurPipeQuantity * Math.Floor(limestoneLayerBottomWidth / spurPipeSegmentLength);
        }

        #endregion

        #region System Footprint Calculations - Liner

        /// <summary>
        /// Calculate the slope length of the liner in feet.
        /// </summary>
        /// <param name="limestoneDepth">The limestone depth in feet, a user defined value</param>
        /// <param name="CompostMixDepth">The organic matter depth in feet, a user defined value</param>
        /// <param name="freeStandingWaterDepth">The free standing water depth in feet, a user defined value</param>
        /// <param name="freeboardDepth">The freeboard depth in feet, a user defined value</param>
        /// <param name="pondInsideSlope">The side slope of the pond, a user defined value</param>
        /// <returns>The slope length of the liner in feet</returns>
        public static double CalcLinerSlopeLength(double limestoneDepth, double CompostMixDepth, double freeStandingWaterDepth, double freeboardDepth, double pondInsideSlope)
        {
            double depthSum = limestoneDepth + CompostMixDepth + freeStandingWaterDepth + freeboardDepth;

            return Math.Sqrt(Math.Pow(depthSum, 2) + Math.Pow(pondInsideSlope * depthSum, 2));
        }

        /// <summary>
        /// Calculate the area of a synthetic liner in square yards. Calculation adds a 2.0 ft extentsion to liner slope length to 
        /// account for tie-in.
        /// </summary>
        /// <param name="limestoneLayerBottomLength">The limestone layer bottom length in feet</param>
        /// <param name="limestoneLayerBottomWidth">The limestone layer bottom width in feet</param>
        /// <param name="freeboardLayerTopLength">The freeboard layer top length in feet, a calculated value</param>
        /// <param name="freeboardLayerTopWidth">The freeboard layer top width in feet, a calculated value</param>
        /// <param name="linerSlopeLength">The slope length of the liner in feet, a calculated value</param>
        /// <returns>The area of a synthetic liner in square yards</returns>
        public static double CalcSyntheticLinerArea(double limestoneLayerBottomLength, double limestoneLayerBottomWidth,
                                                    double freeboardLayerTopLength, double freeboardLayerTopWidth,
                                                    double linerSlopeLength)
        {
            double linerExtension = 2.0;
            return ((freeboardLayerTopLength + limestoneLayerBottomLength) * (linerSlopeLength + linerExtension) +
                    (freeboardLayerTopWidth + limestoneLayerBottomWidth) * (linerSlopeLength + linerExtension) +
                    (limestoneLayerBottomWidth * limestoneLayerBottomLength)) / 9.0;
        }

        /// <summary>
        /// Calculate the area of a clay liner in square yards.
        /// </summary>
        /// <remarks>
        /// Same equation is used for geosynthetic clay liner area.
        /// </remarks>
        /// <param name="limestoneLayerBottomLength">The limestone layer bottom length in feet</param>
        /// <param name="limestoneLayerBottomWidth">The limestone layer bottom width in feet</param>
        /// <param name="freeboardLayerTopLength">The freeboard layer top length in feet, a calculated value</param>
        /// <param name="freeboardLayerTopWidth">The freeboard layer top width in feet, a calculated value</param>
        /// <param name="linerSlopeLength">The slope length of the liner in feet, a calculated value</param>
        /// <returns>The area of a clay liner in square yards</returns>
        public static double CalcClayLinerArea(double limestoneLayerBottomLength, double limestoneLayerBottomWidth,
                                               double freeboardLayerTopLength, double freeboardLayerTopWidth,
                                               double linerSlopeLength)
        {
            return ((freeboardLayerTopLength + limestoneLayerBottomLength) * (linerSlopeLength) +
                    (freeboardLayerTopWidth + limestoneLayerBottomWidth) * (linerSlopeLength) +
                    (limestoneLayerBottomWidth * limestoneLayerBottomLength)) / 9.0;
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
        /// Calculate the area of a geosynthetic liner in square yards. Calculation adds a 2.0 ft extentsion to liner slope length to 
        /// account for tie-in. Same as synthetic liner area.  Having this calculation to prepare to any future changes/modifications 
        /// to the geosynthetic clay liner area.
        /// </summary>
        /// <param name="limestoneLayerBottomLength">The limestone layer bottom length in feet</param>
        /// <param name="limestoneLayerBottomWidth">The limestone layer bottom width in feet</param>
        /// <param name="freeboardLayerTopLength">The freeboard layer top length in feet, a calculated value</param>
        /// <param name="freeboardLayerTopWidth">The freeboard layer top width in feet, a calculated value</param>
        /// <param name="linerSlopeLength">The slope length of the liner in feet, a calculated value</param>
        /// <returns>The area of a geosynthetic liner in square yards</returns>
        public static double CalcGeosyntheticClayLinerArea(double limestoneLayerBottomLength, double limestoneLayerBottomWidth,
                                                           double freeboardLayerTopLength, double freeboardLayerTopWidth,
                                                           double linerSlopeLength)
        {
            double linerExtension = 2.0;
            return ((freeboardLayerTopLength + limestoneLayerBottomLength) * (linerSlopeLength + linerExtension) +
                    (freeboardLayerTopWidth + limestoneLayerBottomWidth) * (linerSlopeLength + linerExtension) +
                    (limestoneLayerBottomWidth * limestoneLayerBottomLength)) / 9.0;
        }

        /// <summary>
        /// Calculate the volume of a geosynthetic clay liner in cubic yards.
        /// </summary>
        /// <param name="geosyntheticClayerLinerArea">The area of a geosynthetic clay liner in square yards</param>
        /// <param name="geosyntheticSoilCover">The soil cover thickness of a geosynthetic clay liner in feet</param>
        /// <returns>The volume of a geosynthetic clay liner in cubic yards</returns>
        public static double CalcGeosyntheticClayLinerVolume(double geosyntheticClayerLinerArea, double geosyntheticSoilCover)
        {
            return geosyntheticClayerLinerArea * (geosyntheticSoilCover / 3.0);
        }

        /// <summary>
        /// Calculate the slope length of the non woven geotextile in feet.
        /// </summary>
        /// <param name="limestoneDepth">The limestone depth in feet, a user defined value</param>
        /// <param name="PondInsideSlope">The side slope of the pond, a user defined value</param>
        /// <returns>The slope length of the non woven geotextile in feet</returns>
        public static double CalcNonWovenGeotextileSlopeLength(double limestoneDepth, double PondInsideSlope)
        {
            return Math.Sqrt(Math.Pow(limestoneDepth, 2) + Math.Pow(PondInsideSlope * limestoneDepth, 2));
        }

        /// <summary>
        /// Calculate the area of a non woven geotextile in square yards. 
        /// </summary>
        /// <param name="limestoneLayerTopLength">The limestone layer top length in feet</param>
        /// <param name="limestoneLayerBottomLength">The limestone layer bottom length in feet</param>
        /// <param name="limestoneLayerTopWidth">The limestone layer top length in feet, a calculated value</param>
        /// <param name="limestoneLayerBottomWidth">The limestone layer bottom width in feet, a calculated value</param>
        /// <param name="nonWovenGeotextileSlopeLength">The slope length of the non woven geotextile in feet, a calculated value</param>
        /// <returns>The area of a non woven geotextile in square yards</returns>
        public static double CalcNonWovenGeotextileArea(double limestoneLayerTopLength, double limestoneLayerBottomLength,
                                                        double limestoneLayerTopWidth, double limestoneLayerBottomWidth,
                                                        double nonWovenGeotextileSlopeLength)
        {
            return ((limestoneLayerTopLength + limestoneLayerBottomLength) * (nonWovenGeotextileSlopeLength) +
                    (limestoneLayerTopWidth + limestoneLayerBottomWidth) * (nonWovenGeotextileSlopeLength) +
                    (limestoneLayerBottomWidth * limestoneLayerBottomLength)) / 9.0;
        }
        #endregion

        #region System Footprint Calculations - Excavation, Clear and Grub

        /// <summary>
        /// Calculate the volume of the excavation in cubic yards.
        /// </summary>
        /// <param name="limestoneVolume">The volume of the limestone layer in cubic yards, a calculated value</param>
        /// <param name="compostMixVolume">The volume of the organic matter layer in cubic yards, a calculated value</param>
        /// <param name="freeStandingWaterVolume">The volume of the free standing water layer in cubic yards, a calculated value</param>
        /// <param name="linerVolume">The volume of the liner in cubic yards, a calculated value</param>
        /// <returns>The volume of the excavation in cubic yards</returns>
        public static double CalcExcavationVolume(double limestoneVolume, double compostMixVolume, double freeStandingWaterVolume, double linerVolume)
        {
            return limestoneVolume + compostMixVolume + freeStandingWaterVolume + linerVolume;
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
        /// <param name="freeStandingWaterVolume">The volume of the free standing water layer in cubic yards, a calculated value</param>
        /// <param name="designFlow">The design flow in gallons per minute, a water quality input value</param>
        /// <returns></returns>
        public static double CalcFreeStandingWaterLayerRetentionTime(double freeStandingWaterVolume, double designFlow)
        {
            return freeStandingWaterVolume / (designFlow * (60.0 / (7.4805 * 27.0)));
        }

        /// <summary>
        /// Calculate the limestone layer retention time in hours.
        /// </summary>
        /// <param name="limestoneVolume">The volume of the limestone layer in cubic yards, a calculated value</param>
        /// <param name="porosityLimestoneLayer">The void space of the limestone layer in percent, a user defined value</param>
        /// <param name="designFlow">The design flow in gallons per minute, a water quality input value</param>
        /// <returns>The limestone layer retention time in hours</returns>
        public static double CalcLimestoneLayerRetentionTime(double limestoneVolume, double porosityLimestoneLayer, double designFlow)
        {
            return limestoneVolume * ((porosityLimestoneLayer / 100.0) / (designFlow * (60.0 / (7.4805 * 27.0))));
        }

        /// <summary>
        /// Calculate the compost mix layer retention time in hours.
        /// </summary>
        /// <param name="compostMixVolume">The volume of the compost mix layer in cubic yards, a calculated value</param>
        /// <param name="compostMixPorosity">The porosity of the compost mix layer in percent, a calculated value</param>
        /// <param name="designFlow">The design flow in gallons per minute, a water quality input value</param>
        /// <returns>The limestone layer retention time in hours</returns>
        public static double CalcCompostMixLayerRetentionTime(double compostMixVolume, double compostMixPorosity, double designFlow)
        {
            return compostMixVolume * ((compostMixPorosity / 100.0) / (designFlow * (60.0 / (7.4805 * 27.0))));
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
        /// Calculate the cost of the compost mixture material in dollars.
        /// </summary>
        /// <param name="compostMixLayerOrganicMaterialVolume">The volume of organic material in an organic matter layer in cubic yards, a calculated value</param>
        /// <param name="compostMixUnitCost">The unit cost of organic matter in dollar per cubic yards, a user defined value</param>
        /// <param name="limestoneCompostMixLayerLimestoneFinesWeight">The weight of organic material in an organic matter layer in tons, a calculated value</param>
        /// <param name="limestoneUnitCost">The unit cost of limestone in dollar per tons, a user defined value</param>
        /// <returns>The cost of the organic mixture material in dollars</returns>
        public static decimal CalcCompostMixMaterialCost(double compostMixLayerOrganicMaterialVolume, decimal compostMixUnitCost,
                                                         double compostMixLayerLimestoneFinesWeight, decimal limestoneUnitCost)
        {
            return ((decimal)compostMixLayerOrganicMaterialVolume * compostMixUnitCost) + ((decimal)compostMixLayerLimestoneFinesWeight * limestoneUnitCost);
        }

        /// <summary>
        /// Calculate the cost of the limestone material in dollars.
        /// </summary>
        /// <param name="limestoneWeight">The weight of limestone in tons, a calculated value</param>
        /// <param name="limestoneUnitCost">The unit cost of limestone in dollar per tons, a user defined value</param>
        /// <returns>The cost of the limestone material in dollars</returns>
        public static decimal CalcLimestoneMaterialCost(double limestoneWeight, decimal limestoneUnitCost)
        {
            return (decimal)limestoneWeight * limestoneUnitCost;
        }


        public static decimal CalcCompostMixAndLimestonePlacementCost(double compostMixLayerVolume, decimal compostMixPlacementCost,
                                                                      double limestoneFinesVolume, double limestoneFinesDensity, decimal limestoneFinesPlacementUnitCost,
                                                                      double limestoneVolume, decimal limestonePlacementCost)
        {
            return ((decimal)compostMixLayerVolume * compostMixPlacementCost) 
                   + ((decimal)(limestoneFinesVolume * 27.0 / 200.0) * (decimal)limestoneFinesDensity * limestoneFinesPlacementUnitCost)
                   + ((decimal)limestoneVolume * limestonePlacementCost);
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
        /// <param name="TrunkPipeSegmentLengthTotal">The total length of trunk pipe in feet, a calculated value</param>
        /// <param name="trunkPipeUnitCost">The unit cost of trunk pipe in dollar per each, a user defined value</param>
        /// <returnsThe cost of trunk pipe in dollars></returns>
        public static decimal CalcTrunkPipeCost(double TrunkPipeSegmentLengthTotal, decimal trunkPipeUnitCost)
        {
            return (decimal)TrunkPipeSegmentLengthTotal * trunkPipeUnitCost;
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
        /// <param name="trunkPipeLengthTotal">The total length of trunk pipe in feet, a calculated value</param>
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
                                                   double trunkPipeLengthTotal,
                                                   double spurPipeLengthTotal,
                                                   double inOutPipeInstallRate,
                                                   double trunkSpurPipeInstallRate,
                                                   decimal laborRate)
        {
            return inOutPipeCost + trunkPipeCost + trunkPipeCouplerCost + teeConnectorCost + spurPipeCost + spurCouplerCost +
                   ((decimal)(inOutPipeLength / inOutPipeInstallRate) * laborRate) + ((decimal)((trunkPipeLengthTotal + spurPipeLengthTotal) / trunkSpurPipeInstallRate) * laborRate);
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
        /// <param name="compostMixMaterialCost">The compost mixture material cost in dollars, a calculated value.</param>
        /// <param name="limestoneMaterialCost">The limestone material cost in dollars, a calculated value.</param>
        /// <param name="CompostMixAndLimestonePlacementCost">The organic matter and limestone placement cost in dollars, a calculated value.</param>
        /// <param name="excavationCost">The excavation cost in dollars, a calculated value.</param>
        /// <param name="syntheticLinerCost">The synthetic liner cost in dollars, a calculated value.</param>
        /// <param name="clayLinerCost">The clay liner cost in dollars, a calculated value.</param>
        /// <param name="valveCost">Thevalve cost in dollars, a calculated value.</param>
        /// <param name="pipeCost">The pipe cost in dollars, a calculated value.</param>
        /// <param name="appurtenanceCost">The appurtenance cost in dollars, a calculated value.</param>
        /// <returns>The total cost in dollars</returns>
        public static decimal CalcCapitalCostTotal(decimal compostMixMaterialCost, decimal limestoneMaterialCost, decimal CompostMixAndLimestonePlacementCost,
                                                   decimal excavationCost, decimal linerCost, decimal pipeCost, decimal appurtenanceCost)
        {
            return compostMixMaterialCost + limestoneMaterialCost + CompostMixAndLimestonePlacementCost +
                   excavationCost + linerCost + pipeCost + appurtenanceCost;
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
        /// Calculate the total cost of limestone material for recapitalization costs.
        /// </summary>
        /// <param name="limestoneVolume">The volume of limestone in cubic yards, a calculated value</param>
        /// <param name="limestoneUnitCost">The unit cost of limestone, a user specified value</param>
        /// <param name="limestonePlacementUnitCost">The unit placement cost of limestone, a user specified value</param>
        /// <returns></returns>
        public static decimal CalcRecapitalizationCostMaterialTotalCostLimestone(double limestoneVolume, double limestoneWeight, decimal limestoneUnitCost, decimal limestonePlacementUnitCost)
        {
            return ((decimal)limestoneVolume * limestonePlacementUnitCost) + ((decimal)limestoneWeight * limestoneUnitCost);
        }

        /// <summary>
        /// Calculate the total cost of compost material for recapitalization costs.
        /// </summary>
        /// <param name="compostMixMaterialCost">The compost mix material capital cost, a calculated value</param>
        /// <param name="compostMixAndLimestonePlacementCost">The placement cost compost mix and limestone, a calculated value</param>
        /// <returns>The total cost of compost material for recapitalization costs</returns>
        public static decimal CalcRecapitalizationCostMaterialTotalCostCompost(decimal compostMixMaterialCost, decimal compostMixAndLimestonePlacementCost)
        {
            return compostMixMaterialCost + compostMixAndLimestonePlacementCost;
        }

        /// <summary>
        /// Calculate the total recapitalization cost.
        /// </summary>
        /// <param name="recapitalizationCostCompost"></param>
        /// <param name="recapitalizationCostLimestone"></param>
        /// <param name="recapitalizationCostLiner"></param>
        /// <param name="recapitalizationCostPipe"></param>
        /// <param name="recapitalizationCostOtherItems"></param>
        /// <param name="recapitalizationCostAnnual"></param>
        /// <returns></returns>
        public static decimal CalcRecapitalizationCostTotal(decimal recapitalizationCostCompost, decimal recapitalizationCostLimestone,
                                                            decimal recapitalizationCostLiner, decimal recapitalizationCostPipe,
                                                            decimal recapitalizationCostOtherItems, decimal recapitalizationCostAnnual)
        {
            return recapitalizationCostCompost + recapitalizationCostLimestone +
                   recapitalizationCostLiner + recapitalizationCostPipe +
                   recapitalizationCostOtherItems + recapitalizationCostAnnual;
        }

        #endregion

    }
}
