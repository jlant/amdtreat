using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMDTreat.Models
{
    /// <summary>
    /// Calculations for Anoxic Limestone Drain.
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
    public static class AnoxicLimestoneDrainCalculations
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

        #region Sizing Calculations for Retention Time
        /// <summary>
        /// Calculate water volume in cubic yards. Used in sizing calculation based on retention time.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="retentionTime">The retention time in hours, a user defined value</param>
        /// <param name="designFlow">The design flow in gallons per minute, a water quality input value</param>
        /// <returns>The volume of water in cubic yards</returns>
        public static double CalcWaterVolumeRetentionTime(double retentionTime, double designFlow)
        {
            return retentionTime * designFlow * (60.0 / (7.4805 * 27.0));
        }

        /// <summary>
        /// Calculate the volume of limestone in cubic yards. Used in sizing calculation based on retention time.
        /// </summary>
        /// <remarks>
        /// Notes:
        ///     Water volume is calculated in the calcWaterVolumeRetentionTime() function.
        /// </remarks>
        /// <param name="waterVolume">The volume of water in cubic yards, a calculated value</param>
        /// <param name="voidSpace">The void space of the limestone bed as a percentage, a user defined value</param>
        /// <returns>The volume of limestone in cubic yards</returns>
        public static double CalcLimestoneVolumeRetentionTime(double waterVolume, double voidSpace)
        {
            return waterVolume / (voidSpace / 100.0);
        }

        public static double CalcLimestoneWeightRetentionTime(double limestoneVolume, double limestoneBulkDensity)
        {
            return CalcMaterialWeight(limestoneVolume, limestoneBulkDensity);
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
        public static double CalcAcidityLoadingBureauOfMines(double designFlow, double netAcidity, double neutralizationPeriod, double limestonePurity, double limestoneEfficiency)
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
        public static double CalcLimestoneVolumeBureauOfMines(double acidLoading, double limestoneBulkDensity)
        {
            return (acidLoading / limestoneBulkDensity) * (2000.0 / 27.0);
        }

        /// <summary>
        /// Calculate the total weight of limestone in tons. Used in sizing calculation based on Bureau of Mines. 
        /// </summary>
        /// <remarks>
        /// Notes:
        ///     Limestone volume based on retention time is calculated in the calcLimestoneVolumeRetentionTime() function.
        ///     Acidity is calculated in the calcAcidityLoading() function.
        /// </remarks>
        /// <param name="limestoneWeightRetentionTime">The weight of limestone in tons based on retention time, a calculated value</param>
        /// <param name="acidityLoading">The acidity loading in tons, a calculated value</param>
        /// <returns>The total weight of limestone in tons</returns>
        public static double CalcTotalLimestoneWeightBureauOfMines(double limestoneWeightRetentionTime, double acidityLoading)
        {
            return limestoneWeightRetentionTime + acidityLoading;
        }

        /// <summary>
        /// Calculate the total volume of limestone in cubic yards.  Used in sizing calculation based on Bureau of Mines.  
        /// </summary>
        /// <remarks>
        /// Notes:
        ///     Limestone volume based on retention time is calculated in the calcLimestoneVolumeRetentionTime() function.
        ///     Limestone volume based on bureau of mines is calculated in the calcTotalLimestoneWeightBureauOfMines() function.
        /// </remarks>
        /// <param name="limestoneVolumeRetentionTime">The volume of limestone in cubic yards based on retention time, a calculated value</param>
        /// <param name="limestoneVolumeBureauOfMines">The volume of limestone in cubic yards based on Bureau of Mines, a calculated value</param>
        /// <returns>The total volume of limestone in cubic yards</returns>
        public static double CalcTotalLimestoneVolumeBureauOfMines(double limestoneVolumeRetentionTime, double limestoneVolumeBureauOfMines)
        {
            return limestoneVolumeRetentionTime + limestoneVolumeBureauOfMines;
        }

        #endregion

        #region Sizing Calculations for Alkalinity Generation

        /// <summary>
        /// Calculate the acidity rate in grams per day.  Used in sizing calculation based on Alkalinity Generation
        /// </summary>
        /// <param name="designFlow">The design flow in gallons per minute, a water quality input value</param>
        /// <param name="netAcidity">The net acidity in milligrams per liter, a water quality input value</param>
        /// <returns>The acidity rate in grams per day</returns>
        public static double CalcAcidityRateAlkalinityGeneration(double designFlow, double netAcidity)
        {
            return (designFlow * 3.785412 * netAcidity * 1440.0) / 1000.0;
        }

        /// <summary>
        /// Calculate the surface area of the top of limestone in square yards.  Used in sizing calculation based on Alkalinity Generation.
        /// </summary>
        /// <param name="acidityRate">The acidity rate in grams per day, a calculated value</param>
        /// <param name="alklinityGenerationRate">The alklinity generation rate in milligrams per liter, a user defined value</param>
        /// <returns>The surface area of the top of limestone in square yards</returns>
        public static double CalcSurfaceAreaTopOfLimestoneAlkalinityGeneration(double acidityRate, double alklinityGenerationRate)
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
        public static double CalcLimestoneBottomLengthAlkalinityGeneration(double surfaceArea, double limestoneDepth, double pondInsideSlope, double bottomLengthToWidthRatio)
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

        public static double CalcLimestoneBottomWidthAlkalinityGeneration(double limestoneBottomLength, double bottomLengthToWidthRatio)
        {
            return limestoneBottomLength / bottomLengthToWidthRatio;
        }

        public static double CalcLimestoneTopLengthAlkalinityGeneration(double limestoneBottomLength, double limestoneDepth, double slope)
        {
            return CalcTopDimension(limestoneBottomLength, limestoneDepth, slope);
        }

        public static double CalcLimestoneTopWidthAlkalinityGeneration(double limestoneBottomWidth, double limestoneDepth, double slope)
        {
            return CalcTopDimension(limestoneBottomWidth, limestoneDepth, slope);
        }

        public static double CalcLimestoneVolumeAlkalinityGeneration(double limestoneTopLength, double limestoneTopWidth,
                                                                     double limestoneBottomLength, double limestoneBottomWidth,
                                                                     double limestoneDepth)
        {
            return CalcLayerVolume(limestoneTopLength, limestoneTopWidth, limestoneBottomLength, limestoneBottomWidth, limestoneDepth);
        }

        public static double CalcLimestoneWeightAlkalinityGeneration(double limestoneVolume, double limestoneBulkDensity)
        {
            return CalcMaterialWeight(limestoneVolume, limestoneBulkDensity);
        }

        #endregion

        #region Sizing Calculations for User-Specified Limestone 

        /// <summary>
        /// Calculate the volume of limestone in cubic yards.  Used in sizing calculations based on Limestone Entered by User
        /// </summary>
        /// <param name="limestoneQuantity">The limestone amount needed in tons, a user defined value</param>
        /// <param name="limestoneBulkDensity">The bulk density of limestone in pounds per cubic feet, a calculated value</param>
        /// <returns>The volume of limestone in cubic yards</returns>
        public static double CalcLimestoneVolumeLimestoneEntered(double limestoneQuantity, double limestoneBulkDensity)
        {
            return limestoneQuantity * (2000.0 / (limestoneBulkDensity * 27.0));
        }

        public static double CalcLimestoneWeightLimestoneEntered(double limestoneVolume, double limestoneBulkDensity)
        {
            return CalcMaterialWeight(limestoneVolume, limestoneBulkDensity);
        }
        #endregion

        #region Sizing Calculations for Dimensions Entered by User

        public static double CalcLimestoneBottomLengthDimensions(double limestoneTopLength, double limestoneDepth, double pondInsideSlope)
        {
            return limestoneTopLength - (2.0 * pondInsideSlope * limestoneDepth);
        }


        public static double CalcLimestoneBottomWidthDimensions(double limestoneTopWidth, double limestoneDepth, double pondInsideSlope)
        {
            return limestoneTopWidth - (2.0 * pondInsideSlope * limestoneDepth);
        }


        public static double CalcLimestoneVolumeDimensions(double limestoneTopLength, double limestoneTopWidth,
                                                                  double limestoneBottomLength, double limestoneBottomWidth,
                                                                  double limestoneDepth)
        {
            return CalcLayerVolume(limestoneTopLength, limestoneTopWidth, limestoneBottomLength, limestoneBottomWidth, limestoneDepth);
        }

        public static double CalcBottomLengthToWidthRatioDimensions(double bottomLength, double bottomWidth)
        {
            return bottomLength / bottomWidth;
        }

        public static double CalcLimestoneWeightDimensions(double limestoneVolume, double limestoneBulkDensity)
        {
            return CalcMaterialWeight(limestoneVolume, limestoneBulkDensity);
        }

        #endregion

        #region System Footprint Calculation - Soil Cover

        public static double CalcSoilCoverTopLength(double limestoneTopLength, double soilCoverDepth, double slope)
        {
            return CalcTopDimension(limestoneTopLength, soilCoverDepth, slope);
        }

        public static double CalcSoilCoverTopWidth(double limestoneTopWidth, double soilCoverDepth, double slope)
        {
            return CalcTopDimension(limestoneTopWidth, soilCoverDepth, slope);
        }

        public static double CalcSoilCoverVolume(double soilCoverTopLength, double soilCoverTopWidth,
                                                 double limestoneTopLength, double limestoneTopWidth,
                                                 double soilCoverDepth)
        {
            return CalcLayerVolume(soilCoverTopLength, soilCoverTopWidth, limestoneTopLength, limestoneTopWidth, soilCoverDepth);
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
        public static double CalcLimestoneBottomLength(double limestoneVolume, double limestoneDepth, double slope, double bottomLengthToWidthRatio)
        {

            double a = limestoneDepth / bottomLengthToWidthRatio;
            double b = (slope * Math.Pow(limestoneDepth, 2)) + (slope * Math.Pow(limestoneDepth, 2) / bottomLengthToWidthRatio);
            double c = (2 * Math.Pow(slope, 2) * Math.Pow(limestoneDepth, 3)) - (limestoneVolume * 27);

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
        /// <param name="limestoneBottomLength">The bottom length of the limestone layer in feet, a calculated value</param>
        /// <param name="bottomLengthToWidthRatio">The bottom length to width ratio, a user defined value or a calculate valued</param>
        /// <returns>The bottom width of the limestone layer in feet</returns>
        public static double CalcLimestoneBottomWidth(double limestoneBottomLength, double bottomLengthToWidthRatio)
        {
            return limestoneBottomLength / bottomLengthToWidthRatio;
        }

        public static double CalcLimestoneTopLength(double limestoneBottomLength, double limestoneDepth, double slope)
        {
            return CalcTopDimension(limestoneBottomLength, limestoneDepth, slope);
        }

        public static double CalcLimestoneTopWidth(double limestoneBottomWidth, double limestoneDepth, double slope)
        {
            return CalcTopDimension(limestoneBottomWidth, limestoneDepth, slope);
        }

        public static double CalcLimestoneSurfaceArea(double limestoneTopLength, double limestoneTopWidth)
        {
            return CalcLayerTopArea(limestoneTopLength, limestoneTopWidth);
        }

        //public static double CalcLimestoneVolume(double limestoneTopLength, double limestoneTopWidth,
        //                                         double limestoneBottomLength, double limestoneBottomWidth,
        //                                         double limestoneDepth)
        //{
        //    return CalcLayerVolume(limestoneTopLength, limestoneTopWidth, limestoneBottomLength, limestoneBottomWidth, limestoneDepth);
        //}

        #endregion

        #region System Footprint Calculations - Pipe

        /// <summary>
        /// Calculate the total length of header pipe.
        /// </summary>
        /// <param name="headerPipeQuantity">The quantity (number) of header pipe</param>
        /// <param name="limestoneBottomWidth">The limestone layer bottom width in feet</param>
        /// <returns>The total length of header pipe</returns>
        public static double CalcHeaderPipeLengthTotal(double headerPipeQuantity, double limestoneBottomWidth)
        {
            return headerPipeQuantity * limestoneBottomWidth;
        }

        /// <summary>
        /// Calculate the quantity (number) of header pipe.
        /// </summary>
        /// <param name="headerPipeSegmentLength">The length of header pipe segments, a user defined value</param>
        /// <param name="headerPipeLengthTotal">The total length of header pipe, a calculate value</param>
        /// <returns>The quantity (number) of header pipe segments</returns>
        public static double CalcHeaderPipeSegmentQuantity(double headerPipeSegmentLength, double headerPipeLengthTotal)
        {
            return Math.Ceiling(headerPipeLengthTotal / headerPipeSegmentLength);
        }

        /// <summary>
        /// Calculate the quantity (number) of header pipe couplers
        /// </summary>
        /// <param name="headerPipeSegmentLength">The length of header pipe segments, a user defined value</param>
        /// <param name="headerPipeLengthTotal">The total length of header pipe, a calculate value</param>
        /// <returns>The quantity (number) of header pipe couplers</returns>
        public static double CalcHeaderPipeCouplerQuantity(double headerPipeSegmentLength, double headerPipeLengthTotal)
        {
            return Math.Ceiling(headerPipeLengthTotal / headerPipeSegmentLength) - 1;
        }

        #endregion

        #region System Footprint Calculations - Liner

        public static double CalcNonWovenGeotextileSlopeLength(double limestoneDepth, double slope)
        {
            return CalcLinerSlopeLength(limestoneDepth, slope);
        }

        /// <summary>
        /// Calculate the area of a synthetic liner in square yards.
        /// </summary>
        /// <returns>The area of a synthetic liner in square yards</returns>
        public static double CalcSyntheticLinerArea(double limestoneBottomLength, double limestoneBottomWidth,
                                                    double limestoneTopLength, double limestoneTopWidth,
                                                    double linerSlopeLength)
        {
            return ((limestoneTopLength + limestoneBottomLength) * (linerSlopeLength) +
                    (limestoneTopWidth + limestoneBottomWidth) * (linerSlopeLength) +
                    (limestoneBottomWidth * limestoneBottomLength) + 
                    (limestoneTopWidth * limestoneTopLength)) / 9.0;
        }

        /// <summary>
        /// Calculate the area of a clay liner in square yards.
        /// </summary>
        /// <returns>The area of a clay liner in square yards</returns>
        public static double CalcClayLinerArea(double limestoneBottomLength, double limestoneBottomWidth,
                                               double limestoneTopLength, double limestoneTopWidth,
                                               double linerSlopeLength)
        {
            return ((limestoneTopLength + limestoneBottomLength) * (linerSlopeLength) +
                    (limestoneTopWidth + limestoneBottomWidth) * (linerSlopeLength) +
                    (limestoneBottomWidth * limestoneBottomLength)) / 9.0;
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
        /// Calculate the area of a geosynthetic liner in square yards.
        /// </summary>
        /// <returns>The area of a geosynthetic liner in square yards</returns>
        public static double CalcGeosyntheticClayLinerArea(double limestoneBottomLength, double limestoneBottomWidth,
                                                           double limestoneTopLength, double limestoneTopWidth,
                                                           double linerSlopeLength)
        {
            return ((limestoneTopLength + limestoneBottomLength) * (linerSlopeLength + 2) +
                    (limestoneTopWidth + limestoneBottomWidth) * (linerSlopeLength + 2) +
                    (limestoneBottomWidth * limestoneBottomLength)) / 9.0;
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
        /// Calculate the area of a non woven geotextile in square yards. 
        /// </summary>
        /// <param name="limestoneTopLength">The limestone layer top length in feet</param>
        /// <param name="limestoneBottomLength">The limestone layer bottom length in feet</param>
        /// <param name="limestoneTopWidth">The limestone layer top length in feet, a calculated value</param>
        /// <param name="limestoneBottomWidth">The limestone layer bottom width in feet, a calculated value</param>
        /// <param name="nonWovenGeotextileSlopeLength">The slope length of the non woven geotextile in feet, a calculated value</param>
        /// <returns>The area of a non woven geotextile in square yards</returns>
        public static double CalcNonWovenGeotextileArea(double limestoneTopLength, double limestoneBottomLength,
                                                        double limestoneTopWidth, double limestoneBottomWidth,
                                                        double nonWovenGeotextileSlopeLength)
        {
            return ((limestoneTopLength + limestoneBottomLength) * (nonWovenGeotextileSlopeLength) +
                    (limestoneTopWidth + limestoneBottomWidth) * (nonWovenGeotextileSlopeLength) +
                    (limestoneBottomWidth * limestoneBottomLength) +
                    (limestoneTopWidth * limestoneTopLength)) / 9.0;
        }
        #endregion

        #region System Footprint Calculations - Excavation, Clear and Grub

        /// <summary>
        /// Calculate the volume of the excavation in cubic yards.
        /// </summary>
        /// <returns>The volume of the excavation in cubic yards</returns>
        public static double CalcExcavationVolume(double limestoneVolume, double soilCoverVolume, double linerVolume)
        {
            return limestoneVolume + soilCoverVolume + linerVolume;
        }

        /// <summary>
        /// Calculate the area of the clear and grub in acres
        /// </summary>
        /// <returns>The area of the clear and grub in acres</returns>
        public static double CalcClearAndGrubArea(double soilCoverTopLength, double soilCoverTopWidth)
        {
            return ((soilCoverTopWidth + 2 * 2) * (soilCoverTopLength + 2 * 2)) / 43560.0;
        }
        #endregion

        #region System Footprint Calculations - Retention Times


        public static double CalcLimestoneLayerRetentionTime(double limestoneVolume, double limestonePorosity, double designFlow)
        {
            return limestoneVolume * ((limestonePorosity / 100.0) / (designFlow * (60.0 / (7.4805 * 27.0))));
        }

        #endregion

        #region Capital Costs

        public static decimal CalcSoilCoverCost(double soilCoverVolume, decimal soilCoverUnitCost)
        {
            return (decimal)soilCoverVolume * soilCoverUnitCost;
        }

        public static decimal CalcLimestoneMaterialCost(double limestoneWeight, decimal limestoneUnitCost)
        {
            return (decimal)limestoneWeight * limestoneUnitCost;
        }

        public static decimal CalcLimestonePlacementCost(double limestoneVolume, decimal limestonePlacementUnitCost)
        {
            return (decimal)limestoneVolume * limestonePlacementUnitCost;
        }

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
        /// Calculate the total cost of header pipe in dollars.
        /// </summary>
        /// <param name="headerPipeSegmentQuantity">The quantity (number) of header pipe segments, a calculated value</param>
        /// <param name="headerPipeUnitCost">The unit cost of header pipe, a user defined value</param>
        /// <returns>The total cost of header pipe in dollars</returns>
        public static decimal CalcHeaderPipeCost(double headerPipeSegmentQuantity, decimal headerPipeUnitCost)
        {
            return (decimal)headerPipeSegmentQuantity * headerPipeUnitCost;
        }

        /// <summary>
        /// Calculate the unit cost of header pipe couplers.
        /// </summary>
        /// <param name="headerPipeCouplerQuantity">The quantity (number) of header pipe couplers, a calculated value</param>
        /// <param name="headerCouplerUnitCost">The unit cost of header pipe couplers in dollar per each, a user defined value</param>
        /// <returns></returns>
        public static decimal CalcHeaderPipeCouplerCost(double headerPipeCouplerQuantity, decimal headerCouplerUnitCost)
        {
            return (decimal)headerPipeCouplerQuantity * headerCouplerUnitCost;
        }

        /// <summary>
        /// Calculate the cost of tee connectors in dollars.
        /// </summary>
        /// <param name="headerPipeQuantity">The quantity (number) of header pipe, a user defined</param>
        /// <param name="teeUnitCost">The unit cost of tee connectors, a user defined value</param>
        /// <returns>The cost of tee connectors in dollars</returns>
        public static decimal CalcTeeConnectorCost(double headerPipeQuantity, decimal teeConnectorUnitCost)
        {
            return (decimal)headerPipeQuantity * teeConnectorUnitCost;
        }


        /// <summary>
        /// Calculate the cost of pipe in dollars.
        /// </summary>
        /// <param name="inOutPipeCost">The cost of influent/effluent pipe in dollars, a calculated value</param>
        /// <param name="headerPipeCost">The cost of header pipe in dollars, a calculated value</param>
        /// <param name="headerPipeCouplerCost">The cost of header pipe couplers in dollars, a calculated value</param>
        /// <param name="teeConnectorCost">The cost of tee connectors in dollars, a calculated value</param>
        /// <param name="inOutPipeLength">The length of inout pipe in feet, a user defined value</param>
        /// <param name="headerPipeLengthTotal">The total length of header pipe in feet, a calculated value</param>
        /// <param name="inOutPipeInstallRate">The rate to install inout pipe in feet per hour, a user defined value</param>
        /// <param name="headerPipeInstallRate">The rate to install header pipe in feet per hour, a user defined value</param>
        /// <param name="laborRate">The rate of labor in dollar per hour, a user defined value</param>
        /// <returns>The cost of pipe in dollars</returns>
        public static decimal CalcAmdtreatPipeCost(decimal inOutPipeCost,
                                                   decimal headerPipeCost,
                                                   decimal headerPipeCouplerCost,
                                                   decimal teeConnectorCost,
                                                   double inOutPipeLength,
                                                   double headerPipeLengthTotal,
                                                   double inOutPipeInstallRate,
                                                   double headerPipeInstallRate,
                                                   decimal laborRate)
        {
            return inOutPipeCost + headerPipeCost + headerPipeCouplerCost + teeConnectorCost +
                   ((decimal)(inOutPipeLength / inOutPipeInstallRate) * laborRate) + ((decimal)(headerPipeLengthTotal / headerPipeInstallRate) * laborRate);
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
        /// <returns>The total cost in dollars</returns>
        public static decimal CalcCapitalCostTotal(decimal soilCoverCost, decimal limestoneMaterialCost, decimal limestonePlacementCost,
                                                   decimal excavationCost, decimal linerCost, decimal pipeCost, decimal appurtenanceCost)
        {
            return soilCoverCost + limestoneMaterialCost + limestonePlacementCost +
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
        /// <returns></returns>
        public static decimal CalcRecapitalizationCostMaterialTotalCostLimestone(decimal limestoneMaterialCost, decimal limestonePlacementCost)
        {
            return limestoneMaterialCost + limestonePlacementCost;
        }


        /// <summary>
        /// Calculate the total recapitalization cost.
        /// </summary>
        /// <returns></returns>
        public static decimal CalcRecapitalizationCostTotal(decimal recapitalizationCostSoilCover, decimal recapitalizationCostLimestone,
                                                            decimal recapitalizationCostLiner, decimal recapitalizationCostPipe,
                                                            decimal recapitalizationCostOtherItems, decimal recapitalizationCostAnnual)
        {
            return recapitalizationCostSoilCover + recapitalizationCostLimestone +
                   recapitalizationCostLiner + recapitalizationCostPipe +
                   recapitalizationCostOtherItems + recapitalizationCostAnnual;
        }

        #endregion

        #region ErrorChecking


        public static double CalcMaxPondDepth(double limestoneTopWidth)
        {
            return (limestoneTopWidth - 10.0) / 4.0;
        }

        public static double CalcPondDepth(double limestoneDepth)
        {
            return limestoneDepth;
        }

        #endregion
    }
}
