using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMDTreat.Models
{
    /// <summary>
    /// Calculations for Wetland.
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
    public static class WetlandCalculations
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
        /// Calculate the volume of a layer in cubic yards.
        /// </summary>
        /// <param name="topLength">The top width of the layer in feet, a calculated value</param>
        /// <param name="topWidth">The top width of the layer in feet, a calculated value</param>
        /// <param name="bottomWidth">The bottom width of the layer in feet, a calculated value</param>
        /// <param name="bottomLength">The bottom length of the layer in feet, a calculated value</param>
        /// <param name="depth">The limestone depth in feet, a user defined value</param>
        /// <returns>The volume of a layer in cubic yards</returns>
        public static double CalcLayerVolume(double topLength, double topWidth, double bottomLength, double bottomWidth, double depth)
        {

            return ((topWidth * topLength + bottomWidth * bottomLength) / 2.0) * depth / 27.0;
        }

        /// <summary>
        /// Calculate bulk density in pounds per cubic feet (lbs/ft3).
        /// </summary>
        /// <remarks>
        /// Notes:
        ///     Density of limestone = 165.43 pounds per cubic feet (lbs/ft3)
        /// <param name="voidSpace">The void space of the limestone bed as a percentage, a user defined value</param>
        /// <returns>The bulk density of limestone in pounds per cubic feet</returns>
        public static double CalcBulkDensity(double voidSpace)
        {
            return (1 - voidSpace / 100.0) * 165.43;
        }

        #endregion

        #region Sizing Calculations for Retention Time
        /// <summary>
        /// Calculate water volume in cubic feet. Used in sizing calculation based on retention time.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="retentionTime">The retention time in hours, a user defined value</param>
        /// <param name="designFlow">The design flow in gallons per minute, a water quality input value</param>
        /// <returns>The volume of water in cubic feet</returns>
        public static double CalcWaterVolumeBasedOnRetentionTime(double retentionTime, double designFlow)
        {
            return retentionTime * designFlow * (60.0 / (7.4805));
        }

        /// <summary>
        /// Calculate the bottom length of the water layer in feet based on the Retention Time sizing method.
        /// </summary>
        /// <param name="waterVolume">The volume of water in cubic feet, a calculated value</param>
        /// <param name="waterDepth">The depth of water in feet, a user defined value</param>
        /// <param name="pondInsideSlope">The slope of the pond, a user defined value</param>
        /// <param name="bottomLengthToWidthRatio">The bottom length to width ratio of the pond, a user defined value (calculated for Dimensions sizing method)</param>
        /// <returns></returns>
        public static double CalcWaterLayerBottomLengthBasedOnRetentionTime(double waterVolume, double waterDepth, double pondInsideSlope, double bottomLengthToWidthRatio)
        {
            double a = waterDepth / bottomLengthToWidthRatio;
            double b = pondInsideSlope * Math.Pow(waterDepth, 2) + pondInsideSlope * (Math.Pow(waterDepth, 2) / bottomLengthToWidthRatio);
            double c = 2 * Math.Pow(pondInsideSlope, 2) * Math.Pow(waterDepth, 3) - waterVolume;

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
        /// Calculate the bottom width of the water layer in feet.
        /// </summary>
        /// <param name="waterLayerBottomLength">The bottom length of the water layer in feet, a calculated value</param>
        /// <param name="bottomLengthToWidthRatio">The bottom length to width ratio, a user defined value or a calculate valued</param>
        /// <returns>The bottom width of the water layer in feet</returns>
        public static double CalcWaterLayerBottomWidthBasedOnRetentionTime(double waterLayerBottomLength, double bottomLengthToWidthRatio)
        {
            return waterLayerBottomLength / bottomLengthToWidthRatio;
        }

        /// <summary>
        /// Calculate the water surface area in square feet based on Retention Time sizing option.
        /// </summary>
        /// <param name="waterBottomLength">The bottom length of the water layer in feet, a calculated value</param>
        /// <param name="waterBottomWidth">The bottom width of the water layer in feet, a calculated value</param>
        /// <param name="waterDepth">The depth of water in feet, a user defined value</param>
        /// <param name="pondInsideSlope">The slope of the pond, a user defined value</param>
        /// <returns></returns>
        public static double CalcWaterSurfaceAreaBasedOnRetentionTime(double waterBottomLength, double waterBottomWidth,
                                                                      double waterDepth, double pondInsideSlope)
        {

            double waterTopLength = CalcTopDimension(waterBottomLength, waterDepth, pondInsideSlope);
            double waterTopWidth = CalcTopDimension(waterBottomWidth, waterDepth, pondInsideSlope);

            return CalcLayerTopArea(waterTopLength, waterTopWidth);
        }

        #endregion

        #region Sizing Calculations for Metal Removal Rates

        /// <summary>
        /// Calculate the metal (iron or manganese) loading in grams per day.
        /// </summary>
        /// <param name="designFlow">The design flow in gallons per minute , a user defined value</param>
        /// <param name="dissolvedIron">The dissolved iron in milligrams per liter, a user defined value</param>
        /// <returns>The metal (iron or manganese) loading in grams per day</returns>
        public static double CalcMetalLoadingMetalRemovalRates(double designFlow, double dissolvedMetal)
        {
            return (dissolvedMetal / 1000.0) * (designFlow * 3.785) * 1440;
        }

        /// <summary>
        /// Calculate the metal surface area in square feet.
        /// </summary>
        /// <param name="metalLoading">The metal loading in grams per day, a calculated value</param>
        /// <param name="metalRemovalRate">The metal removal rate in g/m^2/day</param>
        /// <returns>The metal surface area in square feet</returns>
        public static double CalcMetalSurfaceAreaMetalRemovalRates(double metalLoading, double metalRemovalRate)
        {
            return (metalLoading / metalRemovalRate) * 10.76;
        }

        /// <summary>
        /// Calculate the wetland water surface area in square feet.
        /// </summary>
        /// <param name="ironRemovalSurfaceArea">The iron surface area in square feet, a calculated value</param>
        /// <param name="manganeseRemovalSurfaceArea">The manganese surface area in square feet, a calculated value</param>
        /// <returns></returns>
        public static double CalcWaterSurfaceAreaMetalRemovalRates(double ironRemovalSurfaceArea, double manganeseRemovalSurfaceArea)
        {
            return ironRemovalSurfaceArea + manganeseRemovalSurfaceArea;
        }

        #endregion

        #region Sizing Calculations for Dimensions Entered by User

        /// <summary>
        /// Calculate the wetland freeboard length in feet.
        /// </summary>
        /// <param name="freeboardTopLength">The freeboard top length in feet, a user defined value</param>
        /// <param name="inletPoolLength">The inlet pool length in feet, a user defined value</param>
        /// <param name="outletPoolLength">The outlet pool length in feet, a user defined value</param>
        /// <returns>The wetland freeboard length in feet</returns>
        public static double CalcWetlandFreeboardLengthBasedOnDimensionsEntered(double freeboardTopLength, double inletPoolLength, double outletPoolLength)
        {
            return freeboardTopLength - inletPoolLength - outletPoolLength;
        }

        /// <summary>
        /// Calculate the wetland freeboard surface area in square feet.
        /// </summary>
        /// <param name="wetlandFreeBoardTopLength">The wetland freeboard length in feet</param>
        /// <param name="freeboardTopWidth">The freeboard top width in feet, a user defined value</param>
        /// <returns>The wetland freeboard surface area in square feet</returns>
        public static double CalcWetlandFreeboardSurfaceAreaBasedOnDimensionsEntered(double wetlandFreeBoardTopLength, double freeboardTopWidth)
        {
            return wetlandFreeBoardTopLength * freeboardTopWidth;
        }

        /// <summary>
        /// Calculate the wetland water top dimension in feet.
        /// </summary>
        /// <param name="freeboardTopDimension">The wetland freeboard dimension (length or width) in feet, a user defined value </param>
        /// <param name="freeboardDepth">The depth of freeboard in feet, a user defined value</param>
        /// <param name="pondInsideSlope">The side slope of the pond, a user defined value</param>
        /// <returns>The wetland water top dimension in feet</returns>
        public static double CalcWetlandWaterTopDimensionBasedOnDimensionsEntered(double freeboardTopDimension, double freeboardDepth, double pondInsideSlope)
        {
            return freeboardTopDimension - (2 * freeboardDepth * pondInsideSlope);
        }

        /// <summary>
        /// Calculate the wetland water surface area in square feet.
        /// </summary>
        /// <param name="wetlandWaterLength">The wetland freeboard length in feet, a user defined value </param>
        /// <param name="wetlandWaterWidth">The wetland freeboard length in feet, a user defined value </param>
        /// <returns>The wetland water surface area in square feet</returns>
        public static double CalcWetlandWaterSurfaceAreaBasedOnDimensionsEntered(double wetlandWaterLength, double wetlandWaterWidth)
        {
            return wetlandWaterLength * wetlandWaterWidth;
        }

        /// <summary>
        /// Calculate the wetland bottom dimension (length or width) in feet.
        /// </summary>
        /// <param name="wetlandWaterTopDimension">The wetland water top dimension (length or width), a calculated value</param>
        /// <param name="freeStandingWaterDepth">The freestanding water depth, a user defined value</param>
        /// <param name="compostMixDepth">The compost mix depth, a user defined value</param>
        /// <param name="pondInsideSlope">The side slope of the pond, a user defined value</param>
        /// <returns>The wetland bottom dimension (length or width) in feet</returns>
        public static double CalcWetlandBottomDimensionBasedOnDimensionsEntered(double wetlandWaterTopDimension, double freeStandingWaterDepth, double compostMixDepth, double pondInsideSlope)
        {
            return wetlandWaterTopDimension - (2 * (freeStandingWaterDepth + compostMixDepth) * pondInsideSlope);
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

        #region System Footprint Calculations - Compost Mix

        /// <summary>
        /// Calculate the volume of the compost mix layer in cubic yards.
        /// </summary>
        /// <param name="topLength">The top length of the compost mix layer in feet</param>
        /// <param name="topWidth">The top width of the compost mix layer layer in feet</param>
        /// <param name="bottomLength">The bottom length of the wetland in feet</param>
        /// <param name="bottomWidth">The bottom width of the wetland in feet</param>
        /// <param name="depth">The depth of the compost mix layer in feet</param>
        /// <returns>The volume of the compost mix layer in cubic yards</returns>
        public static double CalcCompostMixVolume(double topLength, double topWidth, double bottomLength, double bottomWidth, double depth)
        {

            return ((topWidth * topLength + bottomWidth * bottomLength) / 2.0) * depth / 27.0;
        }

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

        #region System Footprint Calculations - Freeboard

        public static double CalcFreeboardVolume(double freeboardTopLength, double freeboardTopWidth,
                                                 double inletPoolTopLength, double outletTopWidth,
                                                 double waterTopLength, double waterTopWidth,
                                                 double freeboardDepth)
        {
            return ((freeboardTopWidth * freeboardTopLength + (waterTopLength + inletPoolTopLength + outletTopWidth) * waterTopWidth) / 2.0) * freeboardDepth / 27.0;
        }

        #endregion

        #region System Footprint Calculations - Water, Inlet and Outlet Pool

        /// <summary>
        /// Calculate the dimension (length or width) of the inlet or outlet pool
        /// </summary>
        /// <param name="dimension">The dimension (length or width) in feet, a user defined or calculated value</param>
        /// <param name="depth">The depth in feet, a user defined value</param>
        /// <param name="slope">The slope, a user defined value</param>
        /// <returns>The top length of the water layer in the inlet or outlet pool</returns>
        public static double CalcPoolDimension(double dimension, double depth, double slope)
        {
            return dimension - 2 * slope * depth;
        }

        /// <summary>
        /// Calculate the bottom length of the wetland in feet.
        /// </summary>
        /// <param name="waterSurfaceArea">The surface area of the water layer in square square feet, a calculated value</param>
        /// <param name="waterDepth">The depth of the water layer in feet, a user defined value</param>
        /// <param name="compostMixDepth">The depth of the compost layer in feet, a user defined value</param>
        /// <param name="bottomLengthToWidthRatio">The bottom length to width ratio, a user defined or calculated value</param>
        /// <param name="slope">The slope of the side slopes of the pond, a user defined value</param>
        /// <returns>The bottom length of the wetland in feet</returns>
        public static double CalcWetlandBottomLength(double waterSurfaceArea, double waterDepth, double compostMixDepth, double bottomLengthToWidthRatio, double slope)
        {
            double z = 2 * slope * (waterDepth + compostMixDepth);
            double a = 1 / bottomLengthToWidthRatio;
            double b = (z / bottomLengthToWidthRatio) + z;
            double c = Math.Pow(z, 2) - waterSurfaceArea;

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
        /// Calculate the bottom width of the wetland in feet.
        /// </summary>
        /// <param name="wetlandBottomLength">The bottom length of the wetland in feet, a calculated value</param>
        /// <param name="bottomWidthToWidthRatio">The bottom length to width ratio, a user defined value</param>
        /// <returns>The bottom width of the wetland in feet</returns>
        public static double CalcWetlandBottomWidth(double wetlandBottomLength, double bottomWidthToWidthRatio)
        {
            return wetlandBottomLength / bottomWidthToWidthRatio;
        }

        /// <summary>
        /// Calculate the slope length of the liner for the inlet or outlet pool in feet.
        /// </summary>
        /// <param name="poolDepth">The inlet or outlet pool depth in feet, a user defined value</param>
        /// <param name="freeboardDepth">The freeboard depth in feet, a user defined value</param>
        /// <param name="pondInsideSlope">The side slope of the pond, a user defined value</param>
        /// <returns>The slope length of the liner for the inlet or outlet pool in feet</returns>
        public static double CalcPoolLinerSlopeLength(double poolDepth, double freeboardDepth, double pondInsideSlope)
        {
            double depthSum = poolDepth + freeboardDepth;

            return Math.Sqrt(Math.Pow(depthSum, 2) + Math.Pow(pondInsideSlope * depthSum, 2));
        }

        /// <summary>
        /// Calculate the area of a synthetic liner in square yards. Calculation adds a 2.0 ft extentsion to liner slope length to 
        /// account for tie-in.
        /// </summary>
        /// <param name="poolLength">The inlet or outlet pool length in feet</param>
        /// <param name="poolTopLength">The pool top length in feet, a calculated value</param>
        /// <param name="poolBottomWidth">The pool bottom width in feet</param>
        /// <param name="freeboardLayerTopWidth">The freeboard layer top width in feet, a calculated value</param>
        /// <param name="poolLinerSlopeLength">The slope length of the liner in feet, a calculated value</param>
        /// <returns>The area of a synthetic liner in square yards</returns>
        public static double CalcPoolSyntheticLinerArea(double poolLength,
                                                        double poolBottomLength, double poolBottomWidth,
                                                        double freeboardLayerTopWidth,
                                                        double poolLinerSlopeLength)
        {
            double linerExtension = 2.0;
            return ((poolLength + poolBottomLength) * (poolLinerSlopeLength + linerExtension) +
                    (freeboardLayerTopWidth + poolBottomWidth) * (poolLinerSlopeLength + linerExtension) +
                    (poolBottomWidth * poolBottomLength)) / 9.0;
        }

        /// <summary>
        /// Calculate the area of a clay liner in square yards.
        /// </summary>
        /// <remarks>
        /// Same equation is used for geosynthetic clay liner area.
        /// </remarks>
        /// <param name="poolLength">The inlet or outlet pool length in feet</param>
        /// <param name="poolTopLength">The pool top length in feet, a calculated value</param>
        /// <param name="poolBottomWidth">The pool bottom width in feet</param>
        /// <param name="freeboardLayerTopWidth">The freeboard layer top width in feet, a calculated value</param>
        /// <param name="poolLinerSlopeLength">The slope length of the liner in feet, a calculated value</param>
        /// <returns>The area of a clay liner in square yards</returns>
        public static double CalcPoolClayLinerArea(double poolLength,
                                                   double poolBottomLength, double poolBottomWidth,
                                                   double freeboardLayerTopWidth,
                                                   double poolLinerSlopeLength)
        {
            return ((poolLength + poolBottomLength) * (poolLinerSlopeLength) +
                    (freeboardLayerTopWidth + poolBottomWidth) * (poolLinerSlopeLength) +
                    (poolBottomWidth * poolBottomLength)) / 9.0;
        }

        /// <summary>
        /// Calculate the area of a geosynthetic liner in square yards. Calculation adds a 2.0 ft extentsion to liner slope length to 
        /// account for tie-in.
        /// </summary>
        /// <param name="poolLength">The inlet or outlet pool length in feet</param>
        /// <param name="poolTopLength">The pool top length in feet, a calculated value</param>
        /// <param name="poolBottomWidth">The pool bottom width in feet</param>
        /// <param name="freeboardLayerTopWidth">The freeboard layer top width in feet, a calculated value</param>
        /// <param name="poolLinerSlopeLength">The slope length of the liner in feet, a calculated value</param>
        /// <returns>The area of a geosynthetic liner in square yards</returns>
        public static double CalcPoolGeosyntheticLinerArea(double poolLength,
                                                           double poolBottomLength, double poolBottomWidth,
                                                           double freeboardLayerTopWidth,
                                                           double poolLinerSlopeLength)
        {
            double linerExtension = 2.0;
            return ((poolLength + poolBottomLength) * (poolLinerSlopeLength + linerExtension) +
                    (freeboardLayerTopWidth + poolBottomWidth) * (poolLinerSlopeLength + linerExtension) +
                    (poolBottomWidth * poolBottomLength)) / 9.0;
        }


        public static double CalcWaterVolumeTotal(double waterVolume, double inletPoolVolume, double outletPoolVolume)
        {
            return waterVolume + inletPoolVolume + outletPoolVolume;
        }
        #endregion

        #region System Footprint Calculation - Pipe

        /// <summary>
        /// Calculate the length of header pipe in feet.  Note the length of header pipe is equal to
        /// the bottom width of the outlet pool.
        /// </summary>
        /// <param name="outletPoolBottomWidth">The outlet pool bottom width in feet, a calculated value</param>
        /// <returns>The length of header pipe in feet</returns>
        public static double CalcHeaderPipeLength(double outletPoolBottomWidth)
        {
            return outletPoolBottomWidth;
        }

        /// <summary>
        /// Calculate the quantity (number) of header pipe couplers
        /// </summary>
        /// <param name="headerPipeSegmentLength">The length of header pipe segments, a user defined value</param>
        /// <param name="headerPipeLengthTotal">The total length of header pipe, a calculate value</param>
        /// <returns>The quantity (number) of header pipe couplers</returns>
        public static double CalcHeaderPipeCouplerQuantity(double headerPipeLength, double headerPipeSegmentLength, double headerPipeQuantity)
        {
            return Math.Ceiling(headerPipeLength / headerPipeSegmentLength) * headerPipeQuantity;
        }
        #endregion

        #region System Footprint Calculations - Liner

        /// <summary>
        /// Calculate the slope length of the liner in feet.
        /// </summary>
        /// <param name="CompostMixDepth">The organic matter depth in feet, a user defined value</param>
        /// <param name="freeStandingWaterDepth">The free standing water depth in feet, a user defined value</param>
        /// <param name="freeboardDepth">The freeboard depth in feet, a user defined value</param>
        /// <param name="pondInsideSlope">The side slope of the pond, a user defined value</param>
        /// <returns>The slope length of the liner in feet</returns>
        public static double CalcLinerSlopeLength(double CompostMixDepth, double freeStandingWaterDepth, double freeboardDepth, double pondInsideSlope)
        {
            double depthSum = CompostMixDepth + freeStandingWaterDepth + freeboardDepth;

            return Math.Sqrt(Math.Pow(depthSum, 2) + Math.Pow(pondInsideSlope * depthSum, 2));
        }

        /// <summary>
        /// Calculate the area of a synthetic liner in square yards. Calculation adds a 2.0 ft extentsion to liner slope length to 
        /// account for tie-in.
        /// </summary>
        /// <param name="wetlandBottomLength">The wetland bottom length in feet</param>
        /// <param name="wetlandBottomWidth">The wetland bottom width in feet</param>
        /// <param name="freeboardLayerTopLength">The freeboard layer top length in feet, a calculated value</param>
        /// <param name="freeboardLayerTopWidth">The freeboard layer top width in feet, a calculated value</param>
        /// <param name="linerSlopeLength">The slope length of the liner in feet, a calculated value</param>
        /// <returns>The area of a synthetic liner in square yards</returns>
        public static double CalcSyntheticLinerArea(double wetlandBottomLength, double wetlandBottomWidth,
                                                    double freeboardLayerTopLength, double freeboardLayerTopWidth,
                                                    double linerSlopeLength)
        {
            double linerExtension = 2.0;
            return ((freeboardLayerTopLength + wetlandBottomLength) * (linerSlopeLength + linerExtension) +
                    (freeboardLayerTopWidth + wetlandBottomWidth) * (linerSlopeLength + linerExtension) +
                    (wetlandBottomWidth * wetlandBottomLength)) / 9.0;
        }

        /// <summary>
        /// Calculate the area of a clay liner in square yards.
        /// </summary>
        /// <remarks>
        /// Same equation is used for geosynthetic clay liner area.
        /// </remarks>
        /// <param name="wetlandBottomLength">The wetland bottom length in feet</param>
        /// <param name="wetlandBottomWidth">The wetland bottom width in feet</param>
        /// <param name="freeboardLayerTopLength">The freeboard layer top length in feet, a calculated value</param>
        /// <param name="freeboardLayerTopWidth">The freeboard layer top width in feet, a calculated value</param>
        /// <param name="linerSlopeLength">The slope length of the liner in feet, a calculated value</param>
        /// <returns>The area of a clay liner in square yards</returns>
        public static double CalcClayLinerArea(double wetlandBottomLength, double wetlandBottomWidth,
                                               double freeboardLayerTopLength, double freeboardLayerTopWidth,
                                               double linerSlopeLength)
        {
            return ((freeboardLayerTopLength + wetlandBottomLength) * (linerSlopeLength) +
                    (freeboardLayerTopWidth + wetlandBottomWidth) * (linerSlopeLength) +
                    (wetlandBottomWidth * wetlandBottomLength)) / 9.0;
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
        /// <param name="wetlandBottomLength">The wetland bottom length in feet</param>
        /// <param name="wetlandBottomWidth">The wetland bottom width in feet</param>
        /// <param name="freeboardLayerTopLength">The freeboard layer top length in feet, a calculated value</param>
        /// <param name="freeboardLayerTopWidth">The freeboard layer top width in feet, a calculated value</param>
        /// <param name="linerSlopeLength">The slope length of the liner in feet, a calculated value</param>
        /// <returns>The area of a geosynthetic liner in square yards</returns>
        public static double CalcGeosyntheticClayLinerArea(double wetlandBottomLength, double wetlandBottomWidth,
                                                           double freeboardLayerTopLength, double freeboardLayerTopWidth,
                                                           double linerSlopeLength)
        {
            double linerExtension = 2.0;
            return ((freeboardLayerTopLength + wetlandBottomLength) * (linerSlopeLength + linerExtension) +
                    (freeboardLayerTopWidth + wetlandBottomWidth) * (linerSlopeLength + linerExtension) +
                    (wetlandBottomWidth * wetlandBottomLength)) / 9.0;
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
        /// <param name="compostMixDepth">The compost mix depth in feet, a user defined value</param>
        /// <param name="pondInsideSlope">The side slope of the pond, a user defined value</param>
        /// <returns>The slope length of the non woven geotextile in feet</returns>
        public static double CalcNonWovenGeotextileSlopeLength(double compostMixDepth, double pondInsideSlope)
        {
            return Math.Sqrt(Math.Pow(compostMixDepth, 2) + Math.Pow(pondInsideSlope * compostMixDepth, 2));
        }

        /// <summary>
        /// Calculate the area of a non woven geotextile in square yards. 
        /// </summary>
        /// <param name="wetlandBottomLength">The wetland bottom length in feet</param>
        /// <param name="wetlandBottomWidth">The wetland bottom width in feet</param>
        /// <param name="compostMixTopLength">The compost mix layer top length in feet, a calculated value</param>
        /// <param name="compostTopWidth">The compost mix top width in feet, a calculated value</param>
        /// <param name="nonWovenGeotextileSlopeLength">The slope length of the non woven geotextile in feet, a calculated value</param>
        /// <returns>The area of a non woven geotextile in square yards</returns>
        public static double CalcNonWovenGeotextileArea(double compostMixTopLength, double compostMixTopWidth,
                                                        double wetlandBottomLength, double wetlandBottomWidth,
                                                        double nonWovenGeotextileSlopeLength)
        {
            return ((compostMixTopLength + wetlandBottomLength) * (nonWovenGeotextileSlopeLength) +
                    (compostMixTopWidth + wetlandBottomWidth) * (nonWovenGeotextileSlopeLength) +
                    (wetlandBottomWidth * wetlandBottomLength)) / 9.0;
        }
        #endregion

        #region System Footprint Calculation - Rock Baffles

        /// <summary>
        /// Calculate the quantity (number) of rock baffles.
        /// </summary>
        /// <param name="rockBaffleSpace">The spacing of rock baffles in feet, a user defined value</param>
        /// <param name="freeStandingWaterTopLength">The top length of the free standing water layer, a calculated value</param>
        /// <returns>The quantity (number) of rock baffles</returns>
        public static double CalcRockBaffleQuantity(double rockBaffleSpace, double freeStandingWaterTopLength)
        {
            return Math.Floor(freeStandingWaterTopLength / rockBaffleSpace);
        }

        /// <summary>
        /// Calculate the height of the rock baffles in feet.
        /// </summary>
        /// <param name="freeStandingWaterDepth"></param>
        /// <param name="compostMixDepth"></param>
        /// <returns></returns>
        public static double CalcRockBaffleHeight(double freeStandingWaterDepth, double compostMixDepth)
        {
            return freeStandingWaterDepth + compostMixDepth;
        }

        /// <summary>
        /// Calculate the rock baffle limestone volume in cubic yards.
        /// </summary>
        /// <param name="rockBaffleQuantity">The quantity (number) of rock baffles</param>
        /// <param name="rockBaffleHeight">The height of rock baffles in feet, a user defined value</param>
        /// <param name="freeStandingWaterTopWidth">The top width of the free standing water layer in feet, a calculated value</param>
        /// <param name="slope">The rock baffle limestone volume in cubic yards</param>
        /// <returns></returns>
        public static double CalcRockBaffleLimestoneVolume(double rockBaffleQuantity, double rockBaffleHeight, double freeStandingWaterTopWidth, double slope)
        {
            return rockBaffleQuantity * (((freeStandingWaterTopWidth * 8) + (freeStandingWaterTopWidth - 2 * slope * rockBaffleHeight) * (8 + 2 * slope * rockBaffleHeight)) / 2.0) * (rockBaffleHeight / 27.0);
        }

        /// <summary>
        /// Calculate the weight of the limestone in rock baffles in tons.
        /// </summary>
        /// <param name="rockBaffleLimestoneVolume">The rock baffle limestone volume in cubic yards, a calculated value</param>
        /// <param name="limestoneBulkDensity">The bulk density of limestone in pounds per cubic feet, a calculated value</param>
        /// <returns></returns>
        public static double CalcRockBaffleLimestoneWeight(double rockBaffleLimestoneVolume, double limestoneBulkDensity)
        {
            return rockBaffleLimestoneVolume * (limestoneBulkDensity * 27) / 2000;
        }
        #endregion

        #region System Footprint Calculations - Excavation, Clear and Grub Area

        /// <summary>
        /// Calculate the volume of the excavation in cubic yards.
        /// </summary>
        /// <param name="compostMixVolume">The volume of the organic matter layer in cubic yards, a calculated value</param>
        /// <param name="freeStandingWaterVolume">The volume of the free standing water layer in cubic yards, a calculated value</param>
        /// <param name="inletPoolVolume">The volume of the inlet pool in cubic yards, a calculated value</param>
        /// <param name="outletPoolVolume">The volume of the outlet pool in cubic yards, a calculated value</param>
        /// <param name="linerVolume">The volume of the liner in cubic yards, a calculated value</param>
        /// <returns>The volume of the excavation in cubic yards</returns>
        public static double CalcExcavationVolume(double compostMixVolume, double freeStandingWaterVolume, double inletPoolVolume, double outletPoolVolume, double linerVolume)
        {
            return compostMixVolume + freeStandingWaterVolume + inletPoolVolume + outletPoolVolume + linerVolume;
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
        /// Calculate the pool retention time in hours.
        /// </summary>
        /// <param name="poopVolume">The volume of the pool in cubic yards, a calculated value</param>
        /// <param name="designFlow">The design flow in gallons per minute, a water quality input value</param>
        /// <returns></returns>
        public static double CalcPoolRetentionTime(double poolVolume, double designFlow)
        {
            return poolVolume / (designFlow * (60.0 / (7.4805 * 27.0)));
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

        #region Misc. - Pond Depth, Max Pond Depth, ...
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
        /// <param name="freeStandingWaterDepth">The free standing water depth in feet, a user defined value</param>
        /// <param name="compostMixDepth">The organic matter depth in feet, a user defined value</param>
        /// <returns>The total depth of a pond in feet</returns>
        public static double CalcPondDepth(double freeStandingWaterDepth, double compostMixDepth)
        {
            return freeStandingWaterDepth + compostMixDepth;
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
        /// <param name="limestoneFinesUnitCost">The unit cost of limestone fines in dollar per tons, a user defined value</param>
        /// <returns>The cost of the organic mixture material in dollars</returns>
        public static decimal CalcCompostMixMaterialCost(double compostMixLayerOrganicMaterialVolume, decimal compostMixUnitCost,
                                                         double compostMixLayerLimestoneFinesWeight, decimal limestoneFinesUnitCost)
        {
            return ((decimal)compostMixLayerOrganicMaterialVolume * compostMixUnitCost) + ((decimal)compostMixLayerLimestoneFinesWeight * limestoneFinesUnitCost);
        }

        /// <summary>
        /// Calculate the cost of the rock baffle limestone material in dollars.
        /// </summary>
        /// <param name="rockBaffleLimestoneWeight">The weight limestone in the rock baffles in tons, a calculated value</param>
        /// <param name="limestoneUnitCost">The unit cost of limestone in dollar per tons, a user defined value</param>
        /// <returns>The cost of the rock baffle limestone material in dollars</returns>
        public static decimal CalcRockBaffleLimestoneMaterialCost(double rockBaffleLimestoneWeight, decimal limestoneUnitCost)
        {
            return (decimal)rockBaffleLimestoneWeight * limestoneUnitCost;
        }

        /// <summary>
        /// Calculate the cost of organic matter and limestone placement in dollars.
        /// </summary>
        /// <param name="compostMixLayerVolume">The volume of the organic matter layer in cubic yards</param>
        /// <param name="compostMixPlacementCost">The unit cost of organic matter placement in dollar per cubic yards</param>
        /// <returns></returns>
        public static decimal CalcCompostMixAndLimestonePlacementCost(double compostMixLayerVolume, decimal compostMixPlacementCost)
        {
            return ((decimal)compostMixLayerVolume * compostMixPlacementCost);
        }

        /// <summary>
        /// Calculate the wetland planting cost based on surface area of organic mixture in dollars.
        /// </summary>
        /// <param name="compostMixTopArea">The top area of the compost mix in square feet</param>
        /// <param name="wetlandPlantingUnitCost">The wetland planting unit cost in dollar per acre</param>
        /// <returns>The wetland planting cost based on surface area of organic mixture in dollars</returns>
        public static decimal CalcWetlandPlantingCost(double compostMixTopArea, decimal wetlandPlantingUnitCost)
        {
            return ((decimal)(compostMixTopArea / 43560) * wetlandPlantingUnitCost);
        }

        /// <summary>
        /// Calculate the cost of the rock baffle limestone placement cost in dollars.
        /// </summary>
        /// <param name="rockBaffleLimestoneVolume">The volume of limestone in rock baffles in cubic yards</param>
        /// <param name="limestonePlacementUnitCost">The limestone placement cost in dollar per cubic yards</param>
        /// <returns>The cost of the rock baffle limestone placement cost in dollars</returns>
        public static decimal CalcRockBaffleLimestonePlacementCost(double rockBaffleLimestoneVolume, decimal limestonePlacementUnitCost)
        {
            return ((decimal)rockBaffleLimestoneVolume * limestonePlacementUnitCost);
        }

        /// <summary>
        /// Calculate the cost of influent/effluent pipe in dollars.
        /// </summary>
        /// <param name="InOutPipeLength">The length of influent/effluent pipe in feet, a user defined value</param>
        /// <param name="InOutPipeUnitCost">The unit cost of influent/effluent pipe in dollar per each, a user defined value</param>
        /// <returns>The cost of influent/effluent pipe in dollars></returns>
        public static decimal CalcInOutPipeCost(double InOutPipeLength, decimal InOutPipeUnitCost)
        {
            return (decimal)InOutPipeLength * InOutPipeUnitCost;
        }

        /// <summary>
        /// Calculate the total cost of header pipe in dollars.
        /// </summary>
        /// <param name="headerPipeLength">The length of header pipe in feet, a calculate value</param>
        /// <param name="headerPipeQuantity">The quantity (number) of header pipe, a calculate value</param>
        /// <param name="headerPipeUnitCost">The unit cost of header pipe, a user defined value</param>
        /// <returns>The total cost of header pipe in dollars</returns>
        public static decimal CalcHeaderPipeCost(double headerPipeLength, double headerPipeQuantity, decimal headerPipeUnitCost)
        {
            return (decimal)headerPipeLength * (decimal)headerPipeQuantity * headerPipeUnitCost;
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
        /// <param name="headerPipeLength">The total length of header pipe in feet, a calculated value</param>
        /// <param name="headerPipeQuantity">The quantity (number) of header pipe, a user defined value</param>
        /// <param name="inOutPipeInstallRate">The rate to install inout pipe in feet per hour, a user defined value</param>
        /// <param name="headerPipeInstallRate">The rate to install header pipe in feet per hour, a user defined value</param>
        /// <param name="laborRate">The rate of labor in dollar per hour, a user defined value</param>
        /// <returns>The cost of pipe in dollars</returns>
        public static decimal CalcAmdtreatPipeCost(decimal inOutPipeCost,
                                                   decimal headerPipeCost,
                                                   decimal headerPipeCouplerCost,
                                                   decimal teeConnectorCost,
                                                   double inOutPipeLength,
                                                   double headerPipeLength,
                                                   double headerPipeQuantity,
                                                   double inOutPipeInstallRate,
                                                   double headerPipeInstallRate,
                                                   decimal laborRate)
        {
            return inOutPipeCost + headerPipeCost + headerPipeCouplerCost + teeConnectorCost +
                   ((decimal)(inOutPipeLength / inOutPipeInstallRate) * laborRate) + ((decimal)((headerPipeLength * headerPipeQuantity) / headerPipeInstallRate) * laborRate);
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
        /// <param name="flowDistributionQuantity">The quantity of flow distribution structures, a user defined value</param>
        /// <param name="flowDistributionUnitCost">The unit cost of flow distribution structures in dollar per each, a user defined value</param>
        /// <param name="waterLevelControlQuantity">The quantity of water level control structures, a user defined value</param>
        /// <param name="waterLevelControlUnitCost">The unit cost of water level control structures in dollar per each, a user defined value</param>
        /// <param name="outletProtectionQuantity">The quantity of outlet protection structures, a user defined value</param>
        /// <param name="outletProtectionUnitCost">The unit cost of outlet protection structures in dollar per each, a user defined value</param>
        /// <returns>The appurtenance cost in dollars</returns>
        public static decimal CalcOtherItemsCost(double flowDistributionQuantity, decimal flowDistributionUnitCost,
                                                 double waterLevelControlQuantity, decimal waterLevelControlUnitCost,
                                                 double outletProtectionQuantity, decimal outletProtectionUnitCost)
        {
            return ((decimal)flowDistributionQuantity * flowDistributionUnitCost) +
                   ((decimal)waterLevelControlQuantity * waterLevelControlUnitCost) +
                   ((decimal)outletProtectionQuantity * outletProtectionUnitCost);
        }

        /// <summary>
        /// Calculate the total capital cost in dollars.
        /// </summary>
        /// <param name="compostMixMaterialCost">The compost mixture material cost in dollars, a calculated value.</param>
        /// <param name="compostMixAndLimestonePlacementCost">The organic matter and limestone placement cost in dollars, a calculated value.</param>
        /// <param name="wetlandPlantingCost">The wetland planting cost in dollars, a calculated value.</param>
        /// <param name="rockBaffleLimestoneMaterialCost">The rock baffle limestone material cost in dollars, a calculated value.</param>
        /// <param name="rockBaffleLimestonePlacementCost">The rock baffle limestone placement cost in dollars, a calculated value.</param>
        /// <param name="excavationCost">The excavation cost in dollars, a calculated value.</param>
        /// <param name="syntheticLinerCost">The synthetic liner cost in dollars, a calculated value.</param>
        /// <param name="clayLinerCost">The clay liner cost in dollars, a calculated value.</param>
        /// <param name="pipeCost">The pipe cost in dollars, a calculated value.</param>
        /// <param name="appurtenanceCost">The appurtenance cost in dollars, a calculated value.</param>
        /// <returns>The total cost in dollars</returns>
        public static decimal CalcCapitalCostTotal(decimal compostMixMaterialCost, decimal compostMixAndLimestonePlacementCost, 
                                                   decimal wetlandPlantingCost, 
                                                   decimal rockBaffleLimestoneMaterialCost, decimal rockBaffleLimestonePlacementCost,
                                                   decimal excavationCost, decimal linerCost, decimal pipeCost, decimal appurtenanceCost)
        {
            return compostMixMaterialCost + compostMixAndLimestonePlacementCost +
                   wetlandPlantingCost +
                   rockBaffleLimestoneMaterialCost + rockBaffleLimestonePlacementCost +
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
        /// <param name="rockBaffleLimestoneVolume">The volume of rock baffle limestone in cubic yards, a calculated value</param>
        /// <param name="rockBaffleLimestoneWeight">The weight of rock baffle limestone in tons, a calculated value</param>
        /// <param name="limestoneUnitCost">The unit cost of limestone in dollar per ton, a user specified value</param>
        /// <param name="limestonePlacementUnitCost">The unit placement cost of limestone in dollar per cubic yards, a user specified value</param>
        /// <returns>The total cost of limestone material for recapitalization costs</returns>
        public static decimal CalcRecapitalizationCostMaterialTotalCostRockBaffleLimestone(double rockBaffleLimestoneVolume, double rockBaffleLimestoneWeight, decimal limestoneUnitCost, decimal limestonePlacementUnitCost)
        {
            return (decimal)rockBaffleLimestoneVolume * limestonePlacementUnitCost + (decimal)rockBaffleLimestoneWeight * limestoneUnitCost;
        }

        /// <summary>
        /// Calculate the total cost of compost material for recapitalization costs.
        /// </summary>
        /// <param name="compostMixMaterialCost">The compost mix material capital cost, a calculated value</param>
        /// <param name="compostMixAndLimestonePlacementCost">The placement cost compost mix and limestone, a calculated value</param>
        /// <param name="wetlandPlantingCost">The wetland planing cost, a calculated value</param>
        /// <returns>The total cost of compost material for recapitalization costs</returns>
        public static decimal CalcRecapitalizationCostMaterialTotalCostCompost(decimal compostMixMaterialCost, decimal compostMixAndLimestonePlacementCost, decimal wetlandPlantingCost)
        {
            return compostMixMaterialCost + compostMixAndLimestonePlacementCost + wetlandPlantingCost;
        }

        /// <summary>
        /// Calculate the total recapitalization cost.
        /// </summary>
        /// <param name="recapitalizationCostCompost"></param>
        /// <param name="recapitalizationCostRockBaffleLimestone"></param>
        /// <param name="recapitalizationCostLiner"></param>
        /// <param name="recapitalizationCostPipe"></param>
        /// <param name="recapitalizationCostOtherItems"></param>
        /// <param name="recapitalizationCostAnnual"></param>
        /// <returns></returns>
        public static decimal CalcRecapitalizationCostTotal(decimal recapitalizationCostCompost, decimal recapitalizationCostRockBaffleLimestone,
                                                            decimal recapitalizationCostLiner, decimal recapitalizationCostPipe,
                                                            decimal recapitalizationCostOtherItems, decimal recapitalizationCostAnnual)
        {
            return recapitalizationCostCompost + recapitalizationCostRockBaffleLimestone +
                   recapitalizationCostLiner + recapitalizationCostPipe +
                   recapitalizationCostOtherItems + recapitalizationCostAnnual;
        }

        #endregion

    }
}
