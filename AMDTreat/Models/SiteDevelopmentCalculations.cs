using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMDTreat.Models
{
    /// <summary>
    /// Calculations for Site Development.
    /// </summary>
    public static class SiteDevelopmentCalculations
    {

        #region Sizing Summary

        public static double CalcAccessRoadSurfaceAreaSquareFeet(double accessRoadLength, double accessRoadWidth)
        {
            return accessRoadLength * accessRoadWidth;
        }

        public static double CalcAccessRoadSurfaceAreaSquareYards(double accessRoadLength, double accessRoadWidth)
        {
            return (accessRoadLength * accessRoadWidth) / 9;
        }

        public static double CalcAccessRoadGeotextileArea(double accessRoadGeotextileLength, double accessRoadWidth)
        {
            return (accessRoadGeotextileLength * accessRoadWidth) / 9;
        }

        public static double CalcParkingSurfaceAreaSquareYards(double parkingSpacesQuantity, double averageParkingSpaceArea, double deliveryTruckTurnaroundArea)
        {
            return ((parkingSpacesQuantity * averageParkingSpaceArea) + deliveryTruckTurnaroundArea) / 9;
        }

        public static double CalcParkingSurfaceAreaSquareFeet(double parkingSpacesQuantity, double averageParkingSpaceArea, double deliveryTruckTurnaroundArea)
        {
            return (parkingSpacesQuantity * averageParkingSpaceArea) + deliveryTruckTurnaroundArea;
        }

        public static double CalcAccessRoadCutFillVolumeCubicYards(double cutFillSlope, double averageSiteSlope, double accessRoadWidth, double accessRoadLength)
        {
            double temp = (cutFillSlope * (accessRoadWidth / 2) * averageSiteSlope * 0.01) / (1 - (cutFillSlope * averageSiteSlope * 0.01));
            double roadLinerFoot = (accessRoadWidth / 2) * (temp / cutFillSlope);
            double total = roadLinerFoot * (accessRoadLength / 27);

            return total;
        }

        public static double CalcParkingAreaWidth(double parkingSurfaceAreaSquareFeet)
        {
            return Math.Sqrt(parkingSurfaceAreaSquareFeet);
        }

        public static double CalcParkingAreaLength(double parkingAreaWidth)
        {
            return parkingAreaWidth;
        }

        public static double CalcParkingAreaCutFillVolumeCubicYards(double cutFillSlope, double averageSiteSlope, double parkingAreaWidth, double parkingAreaLength)
        {
            double temp = (cutFillSlope * (parkingAreaWidth / 2) * averageSiteSlope * 0.01) / (1 - (cutFillSlope * averageSiteSlope * 0.01));
            double roadLinerFoot = (parkingAreaWidth / 2) * (temp / cutFillSlope);
            double total = roadLinerFoot * (parkingAreaLength / 27);

            return total;
        }

        public static double CalcClearAndGrubArea(double accessRoadSurfaceAreaSquareFeet, double parkingSurfaceAreaSquareFeet, double controlBuildingArea)
        {
            return (accessRoadSurfaceAreaSquareFeet + parkingSurfaceAreaSquareFeet + controlBuildingArea) / 43560;
        }

        public static double CalcClearAndGrubAreaTotal(double clearAndGrubAreaSiteDevelopment, double clearAndGrubAreaPassiveModules)
        {
            return clearAndGrubAreaSiteDevelopment + clearAndGrubAreaPassiveModules;
        }

        public static double CalcLandAreaAcres(double landAcres)
        {
            return landAcres;
        }

        public static double CalcRockLinedDitchCrossSectionalArea(double rockLinedDitchAggregateThickness, double rockLinedDitchDepth, double rockLinedDitchBottomWidth, double rockLinedDitchSideSlope)
        {
            return rockLinedDitchAggregateThickness * (Math.Sqrt(Math.Pow(rockLinedDitchDepth, 2) + (Math.Pow(rockLinedDitchSideSlope * rockLinedDitchDepth, 2))) + rockLinedDitchBottomWidth);
        }

        public static double CalcRockLinedDitchVolume(double rockLinedDitchCrossSectionalArea, double rockLinedDitchLength)
        {
            return (rockLinedDitchCrossSectionalArea * rockLinedDitchLength) / 27;
        }

        public static double CalcRockLinedDitchWeight(double rockLinedDitchVolume, double rockLinedDitchAggregateBulkDensity)
        {
            return rockLinedDitchVolume * rockLinedDitchAggregateBulkDensity;
        }

        public static double CalcRockLinedDitchGeotextileQuantity(double rockLinedDitchGeotextileLength, double rockLinedDitchAggregateThickness, double rockLinedDitchBottomWidth, double rockLinedDitchSideSlope)
        {
            return rockLinedDitchGeotextileLength * (rockLinedDitchBottomWidth + (2 * Math.Sqrt(Math.Pow(rockLinedDitchAggregateThickness, 2) + Math.Pow(rockLinedDitchAggregateThickness * rockLinedDitchSideSlope, 2))));
        }

        public static double CalcControlBuildingArea(double buildingLength, double buildingWidth)
        {
            return buildingLength * buildingWidth;
        }

        public static double CalcFoundationVolume(double controlBuildingArea, double foundationDepth)
        {
            return (controlBuildingArea * foundationDepth) / 27;
        }

        public static double CalcControlBuildingFooterVolume(double buildingLength, double buildingWidth, double foundationSiteSoilFactor)
        {
            return (((2 * buildingLength) + (2 * buildingWidth)) * 3 * foundationSiteSoilFactor) / 27;
        }

        public static double CalcFoundationSlabVolume(double foundationVolume, double controlBuildingFooterVolume)
        {
            return foundationVolume + controlBuildingFooterVolume;
        }

        public static double CalcControlBuildingCutFillVolumeCubicYards(double cutFillSlope, double averageSiteSlope, double buildingWidth, double buildingLength)
        {
            double temp = (cutFillSlope * (buildingWidth / 2) * averageSiteSlope * 0.01) / (1 - (cutFillSlope * averageSiteSlope * 0.01));
            double roadLinerFoot = (buildingWidth / 2) * (temp / cutFillSlope);
            double total = roadLinerFoot * (buildingLength / 27);

            return total;
        }

        public static double CalcCutFillVolumeCubicYardsTotal(double accessRoadCutFillVolume, double parkingAreaCutFillVolume, double controlBuildingCutFillVolume)
        {
            return accessRoadCutFillVolume + parkingAreaCutFillVolume + controlBuildingCutFillVolume;
        }

        public static double CalcFoundationOverExcavationVolumeTotal(double foundationAreaTotal, double controlBuildingFooterVolume, double foundationOverExcavationDepth)
        {
            return foundationOverExcavationDepth *(foundationAreaTotal / 27) + controlBuildingFooterVolume;
        }

        public static double CalcFoundationExcavationVolumeTotal(double foundationAreaDepthTotal, double foundationVolume)
        {
            return (foundationAreaDepthTotal / 27) + foundationVolume;
        }

        public static double CalcFencingPerimeter(double clearAndGrubAreaTotal)
        {
            return Math.Sqrt(clearAndGrubAreaTotal * 43560) * 1.35 * 4;
        }
        #endregion

        #region Capital Costs

        public static decimal CalcAccessRoadGeotextileCost(double accessRoadGeotextileArea, decimal geotextileUnitCost)
        {
            return (decimal)accessRoadGeotextileArea * geotextileUnitCost;
        }

        public static decimal CalcAccessRoadMaterialCost(double accessRoadSurfaceArea, decimal roadMaterialUnitCost, decimal roadMaterialSubbaseUnitCost)
        {
            return (decimal)accessRoadSurfaceArea * (roadMaterialUnitCost + roadMaterialSubbaseUnitCost);
        }

        public static decimal CalcAccessRoadCost(decimal accessRoadGeotextileCost, decimal accessRoadMaterialCost)
        {
            return accessRoadGeotextileCost + accessRoadMaterialCost;
        }

        public static decimal CalcParkingAreaGeotextileCost(double parkingSurfaceAreaSquareYards, decimal geotextileUnitCost)
        {
            return (decimal)parkingSurfaceAreaSquareYards * geotextileUnitCost;
        }

        public static decimal CalcParkingAreaMaterialCost(double parkingSurfaceAreaSquareYards, decimal parkingMaterialUnitCost, decimal roadMaterialSubbaseUnitCost)
        {
            return (decimal)parkingSurfaceAreaSquareYards * (parkingMaterialUnitCost + roadMaterialSubbaseUnitCost);
        }

        public static decimal CalcParkingAreaCost(decimal parkingLotAccessoriesCost, decimal parkingAreaGeotextileCost, decimal parkingAreaMaterialCost)
        {
            return parkingLotAccessoriesCost + parkingAreaGeotextileCost + parkingAreaMaterialCost;
        }

        public static decimal CalcLandPurchaseCost(double landAreaAcres, decimal landPurchaseUnitCost, decimal landClosingCost)
        {
            return ((decimal)landAreaAcres * landPurchaseUnitCost) + landClosingCost;
        }

        public static decimal CalcESControlEstimatedCost(double clearAndGrubAreaTotal, decimal esControlUnitCostEstimate)
        {
            return (decimal)clearAndGrubAreaTotal * esControlUnitCostEstimate;
        }

        public static decimal CalcESControlUserSpecifiedSiltFenceCompostFilterSockCost(double esControlLength, decimal esControlUnitCost)
        {
            return (decimal)esControlLength * esControlUnitCost;
        }

        public static decimal CalcESControlUserSpecifiedSedimentBasinsCost(double sedimentBasinsQuantity, decimal sedimentBasinsUnitCost)
        {
            return (decimal)sedimentBasinsQuantity * sedimentBasinsUnitCost;
        }

        public static decimal CalcESControlUserSpecifiedOtherCost(decimal esControlUnitCostOther)
        {
            return esControlUnitCostOther;
        }

        public static decimal CalcESControlUserSpecifiedCost(decimal esControlUserSpecifiedSiltFenceCompostFilterSockCost, decimal esControlUserSpecifiedSedimentBasinsCost, decimal esControlUserSpecifiedOtherCost)
        {
            return esControlUserSpecifiedSiltFenceCompostFilterSockCost + esControlUserSpecifiedSedimentBasinsCost + esControlUserSpecifiedOtherCost;
        }

        public static decimal CalcRockLinedDitchPlacementCost(double rockLinedDitchVolume, decimal rockLinedDitchAggregatePlacementUnitCost)
        {
            return (decimal)rockLinedDitchVolume * rockLinedDitchAggregatePlacementUnitCost;
        }

        public static decimal CalcRockLinedDitchMaterialCost(double rockLinedDitchWeight, decimal rockLinedDitchAggregateUnitCost)
        {
            return (decimal)rockLinedDitchWeight * rockLinedDitchAggregateUnitCost;
        }

        public static decimal CalcRockLinedDitchGeotextileCost(double rockLinedDitchGeotextileQuantity, decimal rockLinedDitchGeotextileUnitCost)
        {
            return (decimal)rockLinedDitchGeotextileQuantity * rockLinedDitchGeotextileUnitCost;
        }

        public static decimal CalcRockLinedDitchCost(decimal rockLinedDitchPlacementCost, decimal rockLinedDitchMaterialCost, decimal rockLinedDitchGeotextileCost)
        {
            return rockLinedDitchPlacementCost + rockLinedDitchMaterialCost + rockLinedDitchGeotextileCost;
        }

        public static decimal CalcVegetatedDitchCost(double vegetatedDitchLength, decimal vegetatedDitchUnitCost)
        {
            return (decimal)vegetatedDitchLength * vegetatedDitchUnitCost;
        }

        public static decimal CalcCulvertCost(double culvertLength, decimal culvertUnitCost)
        {
            return (decimal)culvertLength * culvertUnitCost;
        }

        public static decimal CalcDitchAndCulvertCost(decimal rockLinedDitchCost, decimal vegetatedDitchCost, decimal culvertCost)
        {
            return rockLinedDitchCost + vegetatedDitchCost + culvertCost;
        }

        public static decimal CalcControlBuildingCost(double buildingArea, decimal buildingUnitCost)
        {
            return (decimal)buildingArea * buildingUnitCost; ;
        }

        public static decimal CalcFoundationSlabCost(double foundationSlabVolume, decimal concreteUnitCost)
        {
            return (decimal)foundationSlabVolume * concreteUnitCost; ;
        }

        public static decimal CalcControlBuildingLabCost(decimal controlBuildingLabCost)
        {
            return controlBuildingLabCost;
        }

        public static decimal CalcControlBuildingWaterSewerCost(decimal controlBuildingWaterSewerCost)
        {
            return controlBuildingWaterSewerCost;
        }

        public static decimal CalcControlBuildingHVACCost(decimal controlBuildingHVACCost)
        {
            return controlBuildingHVACCost;
        }

        public static decimal CalcControlBuildingCostEstimated(decimal controlBuildingCost, decimal foundationSlabCost, decimal controlBuildingLabCost, decimal controlBuildingWaterSewerCost, decimal controlBuildingHVACCost)
        {
            return controlBuildingCost + foundationSlabCost + controlBuildingLabCost + controlBuildingWaterSewerCost + controlBuildingHVACCost;
        }

        public static decimal CalcCutFillCost(double cutFillVolumeCubicYardsTotal, decimal excavationUnitCost)
        {
            return (decimal)cutFillVolumeCubicYardsTotal * excavationUnitCost;
        }
        
        public static decimal CalcFoundationOverExcavationCost(double foundationOverExcavationVolumeTotal, decimal foundationOverExcavationUnitCost)
        {
            return (decimal)foundationOverExcavationVolumeTotal * foundationOverExcavationUnitCost;
        }

        public static decimal CalcFoundationExcavationCost(double foundationExcavationVolumeTotal, decimal excavationUnitCost)
        {
            return (decimal)foundationExcavationVolumeTotal * excavationUnitCost;
        }

        public static decimal CalcFoundationImprovementCost(decimal foundationOverExcavationCost, decimal foundationImprovementUnitCost)
        {
            return foundationOverExcavationCost * foundationImprovementUnitCost;
        }

        public static decimal CalcElectricServiceExtendCost(double electricLineExtensionLength, decimal electricLineExtensionUnitCost)
        {
            return (decimal)electricLineExtensionLength * electricLineExtensionUnitCost;
        }

        public static decimal CalcElectricUtilityCost(decimal electricServiceExtendCost, decimal electricBackupGeneratorCost, decimal electricStepdownTransformerCost, 
                                                      decimal electricPanelCost, decimal electricTelecommuncationsCost, decimal electricPlantAutomationCost,
                                                      decimal electricOtherUtilityCost)
        {
            return electricServiceExtendCost + electricBackupGeneratorCost + electricStepdownTransformerCost + electricPanelCost + electricTelecommuncationsCost + electricPlantAutomationCost + electricOtherUtilityCost;
        }

        public static decimal CalcElectricWiringCost(double electricWiringPercentage, decimal electricUtilityCost, decimal captialCostActiveModules)
        {
            return ((decimal)electricWiringPercentage / 100) * (electricUtilityCost + captialCostActiveModules);
        }

        public static decimal CalcElectricUtilityCostTotal(decimal electricWiringCost, decimal electricUtilityCost)
        {
            return electricWiringCost + electricUtilityCost;
        }

        public static decimal CalcFencingCostKnownQuantity(double fencingLength, decimal fencingUnitCost)
        {
            return (decimal)fencingLength * fencingUnitCost;
        }

        public static decimal CalcFencingCostEstimate(double fencingPerimeter, decimal fencingUnitCost)
        {
            return (decimal)fencingPerimeter * fencingUnitCost;
        }

        public static decimal CalcAccessGateCost(double accessGateQuantity, decimal accessGateUnitCost)
        {
            return (decimal)accessGateQuantity * accessGateUnitCost;
        }

        public static decimal CalcClearAndGrubCostTotal(double clearAndGrubAreaTotal, decimal clearAndGrubUnitCost)
        {
            return (decimal)clearAndGrubAreaTotal * clearAndGrubUnitCost;
        }

        public static decimal CalcRevegetationCostTotal(double clearAndGrubAreaTotal, double clearAndGrubRevegetationPercentage, decimal revegetationUnitCost)
        {
            return (decimal)((clearAndGrubAreaTotal * (clearAndGrubRevegetationPercentage) / 100)) * revegetationUnitCost;
        }

        public static decimal CalcOtherCapitalItemsCost(double itemQuantity1, decimal itemUnitCost1,
                                                 double itemQuantity2, decimal itemUnitCost2,
                                                 double itemQuantity3, decimal itemUnitCost3,
                                                 double itemQuantity4, decimal itemUnitCost4,
                                                 double itemQuantity5, decimal itemUnitCost5)
        {
            return (decimal)itemQuantity1 * itemUnitCost1 + (decimal)itemQuantity2 * itemUnitCost2 + (decimal)itemQuantity3 * itemUnitCost3 + (decimal)itemQuantity4 * itemUnitCost4 + (decimal)itemQuantity5 * itemUnitCost5;
        }

        public static decimal CalcOtherCapitalItemsCostDummy(double itemQuantity1, decimal itemUnitCost1,
                                         double itemQuantity2, decimal itemUnitCost2,
                                         double itemQuantity3, decimal itemUnitCost3,
                                         double itemQuantity4, decimal itemUnitCost4,
                                         double itemQuantity6, decimal itemUnitCost6)
        {
            return (decimal)itemQuantity1 * itemUnitCost1 + (decimal)itemQuantity2 * itemUnitCost2 + (decimal)itemQuantity3 * itemUnitCost3 + (decimal)itemQuantity4 * itemUnitCost4 + (decimal)itemQuantity6 * itemUnitCost6;
        }

        public static decimal CalcCapitalCostSiteDevelopment(decimal accessRoadCost, decimal parkingAreaCost, decimal landPurchaseCost, 
                                                                  decimal constructionOfficeTrailerCost, decimal esControlCost, decimal ditchAndCulvertCost,
                                                                  decimal controlBuildingCostTotal, decimal cutFillCost, decimal foundationOverExcavationCost,
                                                                  decimal foundationExcavationCost, decimal foundationImprovementCost, decimal electricUtilityCostTotal,
                                                                  decimal fencingCost, decimal accessGateCost, decimal clearAndGrubCostTotal,
                                                                  decimal revegetationCostTotal, decimal otherCapitalItemsCost)
        {
            return accessRoadCost + parkingAreaCost + landPurchaseCost + 
                   constructionOfficeTrailerCost + esControlCost + ditchAndCulvertCost + 
                   controlBuildingCostTotal + cutFillCost + foundationOverExcavationCost +
                   foundationExcavationCost + foundationImprovementCost + electricUtilityCostTotal + 
                   fencingCost + accessGateCost + clearAndGrubCostTotal +
                   revegetationCostTotal + otherCapitalItemsCost;
        }

        public static decimal CalcMobilizationDemobilizationCost(double mobilizationDemobilizationPercentage, decimal capitalCostPassiveAndActiveModules, decimal siteDevelopmentCapitalCost, decimal landPurchaseCost)
        {
            return (decimal)(mobilizationDemobilizationPercentage / 100) * (capitalCostPassiveAndActiveModules + siteDevelopmentCapitalCost - landPurchaseCost);
        }

        public static decimal CalcEngineeringPermittingCost(double engineeringPermittingPercentage, decimal capitalCostPassiveAndActiveModules, decimal siteDevelopmentCapitalCost, decimal landPurchaseCost)
        {
            return (decimal)(engineeringPermittingPercentage / 100) * (capitalCostPassiveAndActiveModules + siteDevelopmentCapitalCost - landPurchaseCost);
        }

        public static decimal CalcSurveyingCost(double surveyingPercentage, decimal capitalCostPassiveModules, decimal siteDevelopmentCapitalCost, decimal landPurchaseCost)
        {
            return (decimal)(surveyingPercentage / 100) * (capitalCostPassiveModules + siteDevelopmentCapitalCost - landPurchaseCost);
        }

        public static decimal CalcConstructionInspectionCost(double constructionInspectionPercentage, decimal capitalCostPassiveModules, decimal siteDevelopmentCapitalCost, decimal landPurchaseCost)
        {
            return (decimal)(constructionInspectionPercentage / 100) * (capitalCostPassiveModules + siteDevelopmentCapitalCost - landPurchaseCost);
        }

        public static decimal CalcContingencyPercentageCost(double contingencyPercentage, decimal capitalCostPassiveModules, decimal siteDevelopmentCapitalCost, decimal landPurchaseCost)
        {
            return (decimal)(contingencyPercentage / 100) * (capitalCostPassiveModules + siteDevelopmentCapitalCost - landPurchaseCost);
        }

        public static decimal CalcCapitalCostTotal(decimal capitalCostPassiveAndActiveModules, decimal capitalCostSiteDevelopment, decimal mobilizationDemobilizationCost, 
                                                   decimal engineeringCost, decimal surveyingCost, decimal constructionInspectionPercentageCost,
                                                   decimal contingencyPercentageCost)
        {
            return capitalCostPassiveAndActiveModules + capitalCostSiteDevelopment + mobilizationDemobilizationCost + engineeringCost + surveyingCost + constructionInspectionPercentageCost + contingencyPercentageCost;
        }
        #endregion

        #region Annual (Operations and Maintenance) Costs

        public static decimal CalcLandLeaseCost(double landAreaAcres, decimal landLeaseCost)
        {
            return (decimal)landAreaAcres * landLeaseCost;
        }

        public static decimal CalcAnnualCostOperationPercentageOption(double annualCostOperationPercentage, decimal capitalCostOtherModules, decimal siteDevelopmentCapitalCost, 
                                                      decimal landPurchaseCost, decimal landLeaseCost, decimal annualCostPropertyTax,
                                                      decimal annualCostUtility, decimal annualCostOperationOther)
        {
            return (decimal)(annualCostOperationPercentage / 100) * (capitalCostOtherModules + siteDevelopmentCapitalCost - landPurchaseCost) +
                   landLeaseCost + annualCostPropertyTax + (annualCostUtility * 12) + (annualCostOperationOther * 12);
        }

        public static decimal CalcAnnualCostOperationUserSpecifiedOption(decimal annualCostOperationUserSpecified, decimal capitalCostOtherModules, decimal siteDevelopmentCapitalCost,
                                                      decimal landPurchaseCost, decimal landLeaseCost, decimal annualCostPropertyTax,
                                                      decimal annualCostUtility, decimal annualCostOperationOther)
        {
            return annualCostOperationUserSpecified + landPurchaseCost + annualCostPropertyTax + (annualCostUtility * 12) + (annualCostOperationOther * 12);
        }

        public static decimal CalcAnnualCostMaintenance(decimal maintenanceRepairLaborRate, double buildingMaintenanceHoursPerYear, double roadMaintenanceHoursPerYear,
                                                        double mowingHoursPerYear, double snowRemovalHoursPerYear, double ditchCulvertCleaningHoursPerYear,
                                                        double leafRemovalHoursPerYear, double siteInspectionsHoursPerYear, double annualOtherHoursPerYear,
                                                        decimal parkingAreaMaintenanceUnitCost, double parkingSurfaceAreaSquareFeet)                                                          
        {
            return (maintenanceRepairLaborRate * (decimal)(buildingMaintenanceHoursPerYear + roadMaintenanceHoursPerYear + mowingHoursPerYear + snowRemovalHoursPerYear + ditchCulvertCleaningHoursPerYear + leafRemovalHoursPerYear + siteInspectionsHoursPerYear + annualOtherHoursPerYear)) +
                   ((decimal)parkingSurfaceAreaSquareFeet * parkingAreaMaintenanceUnitCost);
        }


        public static decimal CalcAnnualCostRepairsEstimate(double annualCostRepairsPercentage, decimal capitalCostOtherModules, decimal siteDevelopmentCapitalCost, decimal landPurchaseCost)
        {
            return (decimal)(annualCostRepairsPercentage / 100) * (capitalCostOtherModules + siteDevelopmentCapitalCost - landPurchaseCost);
        }

        public static decimal CalcAnnualCost(decimal annualCostOperation, decimal annualCostMaintenance, decimal annualCostRepair)
        {
            return annualCostOperation + annualCostMaintenance + annualCostRepair;
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
