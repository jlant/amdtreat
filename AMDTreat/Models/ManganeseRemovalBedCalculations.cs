using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMDTreat.Models
{
    /// <summary>
    /// Calculations for Manganese Removal Bed.
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
    public static class ManganeseRemovalBedCalculations
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
        /// <returns>The bulk density of limestone in pounds per cubic feet</returns>
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

        #region Sizing Calculations for Kinetics 

        /// <summary>
        /// Calculate the surface area of the limestone.
        /// </summary>
        /// <param name="particleSurfaceArea">Particle surface area in square meters, set based on user selected option of AASHTO size</param>
        /// <param name="particleVolume">Particle volume in cubic meters, set based on user selected option of AASHTO size</param>
        /// <param name="porosity">Limestone porosity (void space) as a fraction, a user defined value </param>
        /// <returns>Limestone surface area in square meters/cubic meters (m^2/m^3)</returns>
        public static double CalcKineticsLimestoneSurfaceArea(double particleSurfaceArea, double particleVolume, double porosity)
        {
            return (particleSurfaceArea / particleVolume) * (1 - (porosity / 100.0));
        }

        /// <summary>
        /// Calculate the saturated area of limestone in square meters.
        /// </summary>
        /// <param name="designFlow">The design flow in gallons per minute, a water quality input value</param>
        /// <param name="effluentManganeseConcentration">The amount of manganese effluent concentration in mg/L, a user specified value</param>
        /// <param name="dissolvedManganese">The amount of dissolved manganese in mg/L, a water quality input value</param>
        /// <param name="rateConstant">The manganese removal rate constant in hr^-1(m^2/m^3)^-1, a user specified value</param>
        /// <param name="limestoneSurfaceArea">The limestone surface area in m^2/m^3, a calculated value</param>
        /// <param name="limestoneDepth">The depth of limestone in feet, a user specified value</param>
        /// <returns>The saturate area of limestone in square meters</returns>
        public static double CalcKineticsSaturatedLimestoneArea(double designFlow, double effluentManganeseConcentration, double dissolvedManganese,
                                                                double rateConstant, double limestoneSurfaceArea, double limestoneDepth)
        {
            double numerator = (-0.276 * (designFlow * 3.785) * Math.Log10((effluentManganeseConcentration / dissolvedManganese)));
            double denominator = (rateConstant * limestoneSurfaceArea * (limestoneDepth / 3.281));

            return numerator / denominator;
        }

        /// <summary>
        /// Calculate the saturated area of limestone in square feet.
        /// </summary>
        /// <param name="saturatedLimestoneArea">The saturate area of limestone in square meters</param>
        /// <returns>The saturate area of limestone in square feet</returns>
        public static double CalcKineticsSaturatedLimestoneAreaSquareFt(double saturatedLimestoneArea)
        {
            return saturatedLimestoneArea * 10.76;
        }

        /// <summary>
        /// Calculate the bottom length of the limestone layer in feet.  Uses the quadratic equation to solve for the bottom length
        /// of the pond based on known volume, length to width ratio, and depth of limestone.
        /// </summary>
        /// <param name="surfaceArea">The "saturated" surface area of the top of limestone square feet, a calculated value</param>
        /// <param name="limestoneDepth">The "saturated" limestone depth in feet, a user defined value</param>
        /// <param name="pondInsideSlope">The side slope of the pond, a user defined value</param>
        /// <param name="bottomLengthToWidthRatio">The bottom length to width ratio, a user defined value or a calculate valued</param>
        /// <returns>The bottom length of the limestone layer in feet</returns>
        public static double CalcLimestoneLayerBottomLengthBasedOnKinetics(double surfaceArea, double limestoneDepth, double pondInsideSlope, double bottomLengthToWidthRatio)
        {

            double z = 2 * pondInsideSlope * limestoneDepth;

            double a = 1 / bottomLengthToWidthRatio;
            double b = (z / bottomLengthToWidthRatio) + z;
            double c = Math.Pow(z, 2) - surfaceArea;

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

        public static double CalcLimestoneLayerBottomWidthBasedOnKinetics(double limestoneLayerBottomLength, double bottomWidthToWidthRatio)
        {
            return limestoneLayerBottomLength / bottomWidthToWidthRatio;
        }

        public static double CalcLimestoneLayerTopLengthBasedOnKinetics(double limestoneLayerBottomLength, double limestoneDepth, double slope)
        {
            return CalcTopDimension(limestoneLayerBottomLength, limestoneDepth, slope);
        }

        public static double CalcLimestoneLayerTopWidthBasedOnKinetics(double limestoneLayerBottomWidth, double limestoneDepth, double slope)
        {
            return CalcTopDimension(limestoneLayerBottomWidth, limestoneDepth, slope);
        }

        public static double CalcLimestoneLayerAreaBasedOnKinetics(double limestoneLayerTopLength, double limestoneLayerTopWidth)
        {
            return CalcLayerTopArea(limestoneLayerTopLength, limestoneLayerTopWidth);
        }

        /// <summary>
        /// Calculate the volume of limestone in cubic yards.  Used in sizing calculation based on Alkalinity Generation.
        /// </summary>
        /// <param name="limestoneLayerBottomLength">The bottom length of the limestone layer in feet, a calculated value</param>
        /// <param name="pondInsideSlope">The side slope of the pond, a user defined value</param>
        /// <param name="limestoneDepth">The depth of limestone, a user define value</param>
        /// <param name="bottomLengthToWidthRatio">The bottom length to width ratio, a user defined value or a calculate valued</param>
        /// <returns>The volume of limestone in cubic yards</returns>
        public static double CalcLimestoneVolumeBasedOnKinetics(double limestoneLayerBottomLength,
                                                                double limestoneLayerBottomWidth,
                                                                double limestoneLayerTopLength,
                                                                double limestoneLayerTopWidth,
                                                                double limestoneDepth)
        {
            return (((limestoneLayerBottomLength * limestoneLayerBottomWidth) + (limestoneLayerTopLength * limestoneLayerTopWidth)) / 2.0) * (limestoneDepth / 27.0);
        }


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
        /// <param name="CompostMixDepth">The organic matter depth in feet, a user defined value</param>
        /// <param name="limestoneDepth">The limestone depth in feet, a user defined value</param>
        /// <param name="pondInsideSlope">The inside slope of the pond, a user defined value</param>
        /// <returns>The limestone bottom length dimension in feet</returns>
        public static double CalcLimestoneBottomDimensionBasedOnDimensionsEntered(double freeboardTopDimension, double freeboardDepth,
                                                                                  double limestoneDepth, double pondInsideSlope)
        {
            return freeboardTopDimension - (2.0 * pondInsideSlope * (freeboardDepth + limestoneDepth));
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
        /// <param name="limestoneDepth">The limestone depth in feet, a user defined value</param>
        /// <returns>The total depth of a pond in feet</returns>
        public static double CalcPondDepth(double freeboardDepth, double limestoneDepth)
        {
            return freeboardDepth + limestoneDepth;
        }

        /// <summary>
        /// Calculate limestone volume in cubic yards.  Used in sizing calculations based on Dimensions Entered by User
        /// </summary>
        /// <param name="freeboardTopLength">The freeboard top length in feet, a user defined value</param>
        /// <param name="freeboardTopWidth">The freeboard top width in feet, a user defined value</param>
        /// <param name="freeboardDepth">The freeboard depth in feet, a user defined value</param>
        /// <param name="CompostMixDepth">The organic matter depth in feet, a user defined value</param>
        /// <param name="limestoneDepth">The limestone depth in feet, a user defined value</param>
        /// <param name="pondInsideSlope">The side slope of the pond, a user defined value</param>
        /// <returns>The volume of limestone in cubic yards</returns>
        public static double CalcLimestoneVolumeBasedOnDimensionsEntered(double freeboardTopLength, double freeboardTopWidth,
                                                                         double freeboardDepth, double limestoneDepth, double pondInsideSlope)
        {
            double limestoneBottomWidthDimension = CalcLimestoneBottomDimensionBasedOnDimensionsEntered(freeboardTopWidth, freeboardDepth,
                                                                                                        limestoneDepth, pondInsideSlope);

            double limestoneBottomLengthDimension = CalcLimestoneBottomDimensionBasedOnDimensionsEntered(freeboardTopLength, freeboardDepth, 
                                                                                                         limestoneDepth, pondInsideSlope);

            return CalcLayerVolume(limestoneBottomWidthDimension, limestoneBottomLengthDimension, limestoneDepth, pondInsideSlope);
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
        /// <param name="pondInsideSlope">The side slope of the pond, a user defined value</param>
        /// <param name="bottomLengthToWidthRatio">The bottom length to width ratio, a user defined value or a calculate valued</param>
        /// <returns>The bottom length of the limestone layer in feet</returns>
        public static double CalcLimestoneLayerBottomLength(double limestoneVolume, double limestoneDepth, double pondInsideSlope, double bottomLengthToWidthRatio)
        {

            double a = limestoneDepth / bottomLengthToWidthRatio;
            double b = (pondInsideSlope * Math.Pow(limestoneDepth, 2)) + (pondInsideSlope * Math.Pow(limestoneDepth, 2) / bottomLengthToWidthRatio);
            double c = (2 * Math.Pow(pondInsideSlope, 2) * Math.Pow(limestoneDepth, 3)) - (limestoneVolume * 27);

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

        #region System Footprint Calculation - Pipe

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
            return Math.Ceiling(headerPipeLengthTotal / headerPipeSegmentLength) - 2;
        }
        #endregion

        #region System Footprint Calculations - Liner

        /// <summary>
        /// Calculate the slope length of the liner in feet.
        /// </summary>
        /// <param name="limestoneDepth">The limestone depth in feet, a user defined value</param>
        /// <param name="CompostMixDepth">The organic matter depth in feet, a user defined value</param>
        /// <param name="freeboardDepth">The freeboard depth in feet, a user defined value</param>
        /// <param name="pondInsideSlope">The side slope of the pond, a user defined value</param>
        /// <returns>The slope length of the liner in feet</returns>
        public static double CalcLinerSlopeLength(double limestoneDepth, double freeboardDepth, double pondInsideSlope)
        {
            double depthSum = limestoneDepth + freeboardDepth;

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
        /// <param name="pondInsideSlope">The side slope of the pond, a user defined value</param>
        /// <returns>The slope length of the non woven geotextile in feet</returns>
        public static double CalcNonWovenGeotextileSlopeLength(double limestoneDepth, double pondInsideSlope)
        {
            return Math.Sqrt(Math.Pow(limestoneDepth, 2) + Math.Pow(pondInsideSlope * limestoneDepth, 2));
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
        /// <param name="linerVolume">The volume of the liner in cubic yards, a calculated value</param>
        /// <returns>The volume of the excavation in cubic yards</returns>
        public static double CalcExcavationVolume(double limestoneVolume, double linerVolume)
        {
            return limestoneVolume + linerVolume;
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

        #region System Footprint Calcuations - Limestone Volume Saturated Depth
        /// <summary>
        /// Calculate the volume of limestone saturated depth of the limestone bed in cubic yards.
        /// </summary>
        /// <param name="limestoneTopLength">The limestone layer top length in feet, a calculated value</param>
        /// <param name="limestoneTopWidth">The limestone layer top width in feet, a calculated value</param>
        /// <param name="limestoneBottomLength">The limestone layer bottom length in feet, a calculated value</param>
        /// <param name="limestoneBottomWidth">The limestone layer bottom width in feet, a calculated value</param>
        /// <param name="limestoneDepthSaturated">The saturated depth of limestone in feet, a user defined value</param>
        /// <returns>The volume of limestone within the user defined saturated depth of the limestone bed in cubic yards</returns>
        public static double CalcLimestoneVolumeSaturatedDepth(double limestoneTopLength, double limestoneTopWidth, double limestoneBottomLength, double limestoneBottomWidth, double limestoneDepthSaturated)
        {
            return (((limestoneTopLength * limestoneTopWidth) + (limestoneBottomLength * limestoneBottomWidth)) / 2.0) * (limestoneDepthSaturated / 27.0);
        }
        #endregion

        #region System Footprint Calculations - Retention Times

        /// <summary>
        /// Calculate the limestone layer retention time in hours.
        /// </summary>
        /// <param name="limestoneVolumeSaturatedDepth">The volume of the saturated depth of the limestone layer in cubic yards, a calculated value</param>
        /// <param name="porosityLimestoneLayer">The void space of the limestone layer in percent, a user defined value</param>
        /// <param name="designFlow">The design flow in gallons per minute, a water quality input value</param>
        /// <returns>The limestone layer retention time in hours</returns>
        public static double CalcLimestoneLayerRetentionTime(double limestoneVolumeSaturatedDepth, double porosityLimestoneLayer, double designFlow)
        {
            return limestoneVolumeSaturatedDepth * ((porosityLimestoneLayer / 100.0) / (designFlow * (60.0 / (7.4805 * 27.0))));
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
        /// Calculate the cost of the limestone material in dollars.
        /// </summary>
        /// <param name="limestoneWeight">The weight of limestone in tons, a calculated value</param>
        /// <param name="limestoneUnitCost">The unit cost of limestone in dollar per tons, a user defined value</param>
        /// <returns>The cost of the limestone material in dollars</returns>
        public static decimal CalcLimestoneMaterialCost(double limestoneWeight, decimal limestoneUnitCost)
        {
            return (decimal)limestoneWeight * limestoneUnitCost;
        }

        /// <summary>
        /// Calculate the cost of limestone placement.
        /// </summary>
        /// <param name="limestoneVolume">The volume of limestone in cubic yards, a calculated value</param>
        /// <param name="limestonePlacementUnitCost">The unit cost of limestone placement, a user defined value</param>
        /// <returns>The cost of limestone placement in dollars</returns>
        public static decimal CalcLimestonePlacementCost(double limestoneVolume, decimal limestonePlacementUnitCost)
        {
            return (decimal)limestoneVolume * limestonePlacementUnitCost;
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
        /// <returns>The cost of influent/effluent pipe in dollars></returns>
        public static decimal CalcInOutPipeCost(double InOutPipeLength, decimal InOutPipeUnitCost)
        {
            return (decimal)InOutPipeLength * InOutPipeUnitCost;
        }

        public static decimal CalcHeaderPipeCost(double headerPipeLengthTotal, decimal headerPipeUnitCost)
        {
            return (decimal)headerPipeLengthTotal * headerPipeUnitCost;
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
        /// <param name="flowDistributionQuantity">The quantity of flow distribution structures, a user defined value</param>
        /// <param name="flowDistributionUnitCost">The unit cost of flow distribution structures in dollar per each, a user defined value</param>
        /// <param name="waterLevelControlQuantity">The quantity of water level control structures, a user defined value</param>
        /// <param name="waterLevelControlUnitCost">The unit cost of water level control structures in dollar per each, a user defined value</param>
        /// <param name="outletProtectionQuantity">The quantity of outlet protection structures, a user defined value</param>
        /// <param name="outletProtectionUnitCost">The unit cost of outlet protection structures in dollar per each, a user defined value</param>
        /// <returns>The appurtenance cost in dollars</returns>
        public static decimal CalcOtherItemsCost(decimal valveCost,
                                                 double flowDistributionQuantity, decimal flowDistributionUnitCost,
                                                 double waterLevelControlQuantity, decimal waterLevelControlUnitCost,
                                                 double outletProtectionQuantity, decimal outletProtectionUnitCost)
        {
            return valveCost +
                   ((decimal)flowDistributionQuantity * flowDistributionUnitCost) +
                   ((decimal)waterLevelControlQuantity * waterLevelControlUnitCost) +
                   ((decimal)outletProtectionQuantity * outletProtectionUnitCost);
        }

        /// <summary>
        /// Calculate the total capital cost in dollars.
        /// </summary>
        /// <param name="limestoneMaterialCost">The limestone material cost in dollars, a calculated value.</param>
        /// <param name="limestonePlacementCost">The limestone placement cost in dollars, a calculated value.</param>
        /// <param name="excavationCost">The excavation cost in dollars, a calculated value.</param>
        /// <param name="syntheticLinerCost">The synthetic liner cost in dollars, a calculated value.</param>
        /// <param name="clayLinerCost">The clay liner cost in dollars, a calculated value.</param>
        /// <param name="pipeCost">The pipe cost in dollars, a calculated value.</param>
        /// <param name="appurtenanceCost">The appurtenance cost in dollars, a calculated value.</param>
        /// <returns>The total cost in dollars</returns>
        public static decimal CalcCapitalCostTotal(decimal limestoneMaterialCost, decimal limestonePlacementCost,
                                                   decimal excavationCost, decimal linerCost, decimal pipeCost,
                                                   decimal appurtenanceCost)
        {
            return limestoneMaterialCost + limestonePlacementCost + excavationCost + linerCost + pipeCost + appurtenanceCost;
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


        public static decimal CalcRecapitalizationCostMaterialTotalCostLimestone(decimal limestoneMaterialCost, decimal limestonePlacementCost)
        {
            return limestoneMaterialCost + limestonePlacementCost;
        }

        /// <summary>
        /// Calculate the total recapitalization cost.
        /// </summary>
        /// <param name="recapitalizationCostLimestone"></param>
        /// <param name="recapitalizationCostLiner"></param>
        /// <param name="recapitalizationCostPipe"></param>
        /// <param name="recapitalizationCostOtherItems"></param>
        /// <param name="recapitalizationCostAnnual"></param>
        /// <returns></returns>
        public static decimal CalcRecapitalizationCostTotal(decimal recapitalizationCostLimestone, decimal recapitalizationCostLiner, decimal recapitalizationCostPipe,
                                                            decimal recapitalizationCostOtherItems, decimal recapitalizationCostAnnual)
        {
            return recapitalizationCostLimestone + recapitalizationCostLiner + recapitalizationCostPipe + recapitalizationCostOtherItems + recapitalizationCostAnnual;
        }

        #endregion

    }
}
