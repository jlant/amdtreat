using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMDTreat.Models
{
    /// <summary>
    /// Calculations for Clarifier.
    /// </summary>
    public static class ClarifierCalculations
    {


        #region Sizing Summary: Clarifier Design

        public static double CalcRateOfRise(double flowPerArea)
        {
            return flowPerArea / 7.48;
        }

        public static double CalcClarifierDiameter(double designFlow, double flowPerArea)
        {
            return Math.Round(Math.Sqrt(designFlow / flowPerArea / Math.PI) * 2);
        }

        public static double CalcClarifierVolume(double clarifierDiameter, double clarifierWaterHeight, double clarifierFreeboard)
        {
            return Math.PI * Math.Pow((clarifierDiameter / 2), 2) * (clarifierWaterHeight + clarifierFreeboard);
        }

        public static double CalcClarifierRetentionTime(double designFlow, double clarifierDiameter, double clarifierWaterHeight)
        {
            return ((Math.PI * Math.Pow((clarifierDiameter / 2), 2) * clarifierWaterHeight) * 7.48) / designFlow / 60.0;
        }

        public static double CalcSludgeBlanketVolume(double clarifierDiameter, double sludgeBlanketDepth)
        {
            return Math.PI * Math.Pow((clarifierDiameter / 2), 2) * sludgeBlanketDepth * 7.48;
        }

        public static double CalcSludgeBlanketRetentionTime(double sludgeBlanketVolume, double sludgeDisposalPumpFlowRate)
        {
            return sludgeBlanketVolume / sludgeDisposalPumpFlowRate / 60.0;
        }

        public static double CalcTankProtectiveCoatingSurfaceArea(double clarifierDiameter, double clarifierWaterHeight, double clarifierFreeboard)
        {
            return (Math.PI * Math.Pow((clarifierDiameter / 2), 2)) + (2 * Math.PI * (clarifierDiameter / 2) * (clarifierWaterHeight + clarifierFreeboard)); 
        }

        public static double CalcFoundationArea(double clarifierDiameter)
        {
            return Math.PI * Math.Pow(((clarifierDiameter + 2) / 2), 2);
        }

        public static double CalcFoundationDepth(double foundationDepth)
        {
            return foundationDepth;
        }

        public static double CalcFoundationVolume(double foundationDepth, double foundationArea)
        {
            return (foundationDepth * foundationArea) / 27;
        }

        public static double CalcImpellerMotorPower(double clarifierDiameter)
        {
            double power = 0;
            if (clarifierDiameter < 90)
            {
                power = 15;
            }
            else if (clarifierDiameter > 90 && clarifierDiameter < 125)
            {
                power = 25;
            }
            else // clarifierDiameter > 125
            {
                power = 30;
            }
            
            return power;
        }
        #endregion

        #region Sizing Summary: High Density Sludge Recirculation Pump

        public static double CalcSludgeRecirculationPumpFlowRate(double designFlow, double sludgeVolume)
        {
            return Math.Round(designFlow * (sludgeVolume / 100) * 5);
        }

        public static double CalcSludgeRecirculationPumpPower(double sludgeRecirculationPumpFlowRate, double clarifierWaterHeight, double clarifierFreeboard, double sludgeDisposalPumpEfficiency,  double sludgeDisposalPumpSizingSafetyFactor, double sludgeThickened)
        {
            return Math.Round(sludgeRecirculationPumpFlowRate * (clarifierWaterHeight + clarifierFreeboard + 10) * 144 / 2.3 / 7.48 / 60 / 550 / sludgeDisposalPumpEfficiency / (1 - sludgeDisposalPumpSizingSafetyFactor) * (sludgeThickened / 8.34));
        }

        #endregion

        #region Sizing Summary: Sludge Disposal Pump

        public static double CalcSludgeDisposalPumpFlowRate(double designFlow, double sludgeVolume)
        {
            return Math.Round(designFlow * (sludgeVolume / 100) * 1.5);
        }

        public static double CalcSludgeDisposalPumpPower(double sludgeDisposalPumpFlowRate, double sludgeDisposalPipelineTotalDynamicHead, double sludgeDisposalPumpEfficiency, double sludgeDisposalPumpSizingSafetyFactor, double sludgeThickened)
        {
            return Math.Round(sludgeDisposalPumpFlowRate * sludgeDisposalPipelineTotalDynamicHead * 144 / 2.3 / 7.48 / 60 / 550 / sludgeDisposalPumpEfficiency / (1 - sludgeDisposalPumpSizingSafetyFactor) * (sludgeThickened / 8.34));
        }

        public static double CalcSludgeDisposalPumpTimeHoursPerDay(double sludgeProductionGallonsPerDay, double sludgeDisposalPumpFlowRate)
        {
            return sludgeProductionGallonsPerDay / (sludgeDisposalPumpFlowRate * 60);
        }

        public static double CalcAluminumConcentrationAsSolid(double aluminum)
        {
            return aluminum * 2.88;
        }

        public static double CalcFerrousIronConcentrationAsSolid(double ferrousIron)
        {
            return ferrousIron * 1.6;
        }

        public static double CalcFerricIronConcentrationAsSolid(double ferricIron)
        {
            return ferricIron * 1.9;
        }

        public static double CalcManganeseConcentrationAsSolid(double manganese)
        {
            return manganese * 1.6;
        }

        public static double CalcCalciteConcentrationAsSolid(double calcite)
        {
            return calcite;
        }

        public static double CalcMiscellaneousSolidsConcentrationAsSolid(double miscellaneousSolids)
        {
            return miscellaneousSolids;
        }

        public static double CalcTotalConcentrationAsSolid(double aluminumConcentrationAsSolid, double ferrousIronConcentrationAsSolid, double ferricIronConcentrationAsSolid, double manganeseConcentrationAsSolid, double calciteConcentrationAsSolid, double miscellaneousSolidsConcentrationAsSolid)
        {
            return aluminumConcentrationAsSolid + ferrousIronConcentrationAsSolid + ferricIronConcentrationAsSolid + manganeseConcentrationAsSolid + calciteConcentrationAsSolid + miscellaneousSolidsConcentrationAsSolid;
        }

        public static double CalcAluminumFraction(double aluminumConcentrationAsSolid, double ferrousIronConcentrationAsSolid, double ferricIronConcentrationAsSolid, double manganeseConcentrationAsSolid, double calciteConcentrationAsSolid, double miscellaneousSolidsConcentrationAsSolid)
        {
            return aluminumConcentrationAsSolid / (aluminumConcentrationAsSolid + ferrousIronConcentrationAsSolid + ferricIronConcentrationAsSolid + manganeseConcentrationAsSolid + calciteConcentrationAsSolid + miscellaneousSolidsConcentrationAsSolid);
        }

        public static double CalcFerrousIronFraction(double aluminumConcentrationAsSolid, double ferrousIronConcentrationAsSolid, double ferricIronConcentrationAsSolid, double manganeseConcentrationAsSolid, double calciteConcentrationAsSolid, double miscellaneousSolidsConcentrationAsSolid)
        {
            return ferrousIronConcentrationAsSolid / (aluminumConcentrationAsSolid + ferrousIronConcentrationAsSolid + ferricIronConcentrationAsSolid + manganeseConcentrationAsSolid + calciteConcentrationAsSolid + miscellaneousSolidsConcentrationAsSolid);
        }

        public static double CalcFerricIronFraction(double aluminumConcentrationAsSolid, double ferrousIronConcentrationAsSolid, double ferricIronConcentrationAsSolid, double manganeseConcentrationAsSolid, double calciteConcentrationAsSolid, double miscellaneousSolidsConcentrationAsSolid)
        {
            return ferricIronConcentrationAsSolid / (aluminumConcentrationAsSolid + ferrousIronConcentrationAsSolid + ferricIronConcentrationAsSolid + manganeseConcentrationAsSolid + calciteConcentrationAsSolid + miscellaneousSolidsConcentrationAsSolid);
        }

        public static double CalcManganeseFraction(double aluminumConcentrationAsSolid, double ferrousIronConcentrationAsSolid, double ferricIronConcentrationAsSolid, double manganeseConcentrationAsSolid, double calciteConcentrationAsSolid, double miscellaneousSolidsConcentrationAsSolid)
        {
            return manganeseConcentrationAsSolid / (aluminumConcentrationAsSolid + ferrousIronConcentrationAsSolid + ferricIronConcentrationAsSolid + manganeseConcentrationAsSolid + calciteConcentrationAsSolid + miscellaneousSolidsConcentrationAsSolid);
        }

        public static double CalcCalciteFraction(double aluminumConcentrationAsSolid, double ferrousIronConcentrationAsSolid, double ferricIronConcentrationAsSolid, double manganeseConcentrationAsSolid, double calciteConcentrationAsSolid, double miscellaneousSolidsConcentrationAsSolid)
        {
            return calciteConcentrationAsSolid / (aluminumConcentrationAsSolid + ferrousIronConcentrationAsSolid + ferricIronConcentrationAsSolid + manganeseConcentrationAsSolid + calciteConcentrationAsSolid + miscellaneousSolidsConcentrationAsSolid);
        }

        public static double CalcMiscellaneousSolidsFraction(double aluminumConcentrationAsSolid, double ferrousIronConcentrationAsSolid, double ferricIronConcentrationAsSolid, double manganeseConcentrationAsSolid, double calciteConcentrationAsSolid, double miscellaneousSolidsConcentrationAsSolid)
        {
            return miscellaneousSolidsConcentrationAsSolid / (aluminumConcentrationAsSolid + ferrousIronConcentrationAsSolid + ferricIronConcentrationAsSolid + manganeseConcentrationAsSolid + calciteConcentrationAsSolid + miscellaneousSolidsConcentrationAsSolid);
        }

        public static double CalcMiscellaneousSolidsSpecificGravity(double miscellaneousSolidsDensity)
        {
            return miscellaneousSolidsDensity / 8.34;
        }

        public static double CalcTotalSpecificGravity(double totalWeightedDensity)
        {
            return totalWeightedDensity / 8.34;
        }

        public static double CalcAluminumWeightedDensity(double aluminumFraction, double aluminumSpecificGravity)
        {
            return aluminumSpecificGravity * aluminumFraction * 3785.41 / 453.592;
        }

        public static double CalcFerrousIronWeightedDensity(double ferrousIronFraction, double ferrousIronSpecificGravity)
        {
            return ferrousIronSpecificGravity * ferrousIronFraction * 3785.41 / 453.592;
        }

        public static double CalcFerricIronWeightedDensity(double ferricIronFraction, double ferricIronSpecificGravity)
        {
            return ferricIronSpecificGravity * ferricIronFraction * 3785.41 / 453.592;
        }

        public static double CalcManganeseWeightedDensity(double manganeseFraction, double manganeseSpecificGravity)
        {
            return manganeseSpecificGravity * manganeseFraction * 3785.41 / 453.592;
        }

        public static double CalcCalciteWeightedDensity(double calciteFraction, double calciteSpecificGravity)
        {
            return calciteSpecificGravity * calciteFraction * 3785.41 / 453.592;
        }

        public static double CalcMiscellaneousSolidsWeightedDensity(double miscellaneousSolidsFraction, double miscellaneousSolidsSpecificGravity)
        {
            return miscellaneousSolidsSpecificGravity * miscellaneousSolidsFraction * 3785.41 / 453.592;
        }

        public static double CalcTotalWeightedDensity(double aluminumWeightedDensity, double ferrousIronWeightedDensity, double ferricIronWeightedDensity, double manganeseWeightedDensity, double calciteWeightedDensity, double miscellaneousSolidsWeightedDensity)
        {
            return aluminumWeightedDensity + ferrousIronWeightedDensity + ferricIronWeightedDensity + manganeseWeightedDensity + calciteWeightedDensity + miscellaneousSolidsWeightedDensity;
        }
        #endregion

        #region Sizing Summary: Sludge Characteristics

        public static double CalcSludgeThickened(double sludgeSpecificGravity)
        {
            return sludgeSpecificGravity * 8.34;
        }

        public static double CalcSludgeSpecificGravity(double totalSpecificGravity, double sludgeSolidsPercentage)
        {
            return totalSpecificGravity / (totalSpecificGravity - (sludgeSolidsPercentage / 100 * (totalSpecificGravity - 1)));
        }

        public static double CalcSludgeVolumeSolidsPercentage(double totalSpecificGravity, double sludgeSolidsPercentage)
        {
            return (((totalSpecificGravity / (totalSpecificGravity - ((sludgeSolidsPercentage / 100) * (totalSpecificGravity - 1)))) - 1) / (totalSpecificGravity - 1)) * 100;
        }

        public static double CalcSludgeProductionPoundsPerMin(double typicalFlow, double totalConcentrationAsSolid)
        {
            return totalConcentrationAsSolid * typicalFlow / 1000 / 1000 * 2.2 * 3.785;
        }

        public static double CalcSludgeProductionPoundsPerDay(double sludgeProductionPoundsPerMin)
        {
            return sludgeProductionPoundsPerMin * 60 * 24;
        }

        public static double CalcSludgeProductionGallonsPerMin(double sludgeProductionPoundsPerMin, double sludgeThickened)
        {
            return sludgeProductionPoundsPerMin / sludgeThickened;
        }

        public static double CalcSludgeProductionGallonsPerDay(double sludgeProductionGallonsPerMin)
        {
            return sludgeProductionGallonsPerMin * 60 * 24;
        }

        public static double CalcSludgeProductionGallonsPerYear(double sludgeProductionGallonsPerDay)
        {
            return sludgeProductionGallonsPerDay * 365;
        }
        #endregion

        #region Sizing Summary: Sludge Disposal Pipeline

        public static double CalcPipeBeddingAggregateWeight(double nominalPipeOutsideDiameter, double pipeBeddingAggregateThickness, double pipelineLength)
        {
            return ((nominalPipeOutsideDiameter / 12) + 2 * pipeBeddingAggregateThickness * pipelineLength) * 95 / 2000;
        }

        public static double CalcPipeInsideDiameter(double pipeInsideDiameter)
        {
            return pipeInsideDiameter;
        }

        public static double CalcPipeDynamicLosses(double pipelineLength, double pipeInsideDiameter, double sludgeDisposalPumpFlowRate)
        {
            return (0.0009015 * pipelineLength / (Math.Pow(pipeInsideDiameter, 4.8655)) * Math.Pow(100 * sludgeDisposalPumpFlowRate / 150, 1.85)) * 2.309;
        }

        public static double CalcTotalDynamicHead(double pipeDynamicLosses, double totalStaticHead, double incidentalHeadLosses)
        {
            return (pipeDynamicLosses + totalStaticHead) * (1 + (incidentalHeadLosses / 100));
        }

        public static double CalcTotalDynamicHeadPressure(double totalDynamicHead)
        {
            return totalDynamicHead / 2.309;
        }

        public static double CalcPressureClass(double pipeSDR)
        {
            return pipeSDR;
        }

        public static double CalcPipeFluidVelocity(double sludgeDisposalPumpFlowRate, double pipeInsideDiameter)
        {
            return sludgeDisposalPumpFlowRate / 7.481 / 60 / (Math.Pow((pipeInsideDiameter / 24), 2) * Math.PI);
        }
        #endregion

        #region Sizing Summary: Geotube  Sludge Disposal

        public static double CalcGeotubeVolume(double geotubeCapacity)
        {
            return geotubeCapacity;
        }

        public static double CalcGeotubeFillVolume(double geotubeVolume, double geotubeFillCapacity)
        {
            return geotubeVolume * (geotubeFillCapacity / 100);
        }

        public static double CalcDewateredSludge(double dewateredSludgeSpecificGravity)
        {
            return dewateredSludgeSpecificGravity * 8.34;
        }

        public static double CalcDewateredSludgeSpecificGravity(double totalSpecificGravity, double dewateredSolidsPercentage)
        {
            return totalSpecificGravity / (totalSpecificGravity - ((dewateredSolidsPercentage / 100) * (totalSpecificGravity - 1)));
        }

        public static double CalcDewateredSludgeGenerationRateCubicYardsPerDay(double sludgeProductionPoundsPerDay, double dewateredSludge)
        {
            return sludgeProductionPoundsPerDay / dewateredSludge / 201.974;
        }

        public static double CalcDewateredSludgeGenerationRateCubicYardsPerYear(double dewateredSludgeGenerationRateCubicYardsPerDay)
        {
            return dewateredSludgeGenerationRateCubicYardsPerDay * 365;
        }

        public static double CalcGeotubeFillTime(double geotubeFillVolume, double dewateredSludgeGenerationRateCubicYardsPerDay)
        {
            return geotubeFillVolume / dewateredSludgeGenerationRateCubicYardsPerDay;
        }

        public static double CalcGeotubeRequiredPerYear(double geotubeFillTime)
        {
            return 365 / geotubeFillTime;
        }

        public static double CalcTriaxleLoadsRequiredPerYear(double dewateredSludgeGenerationRateCubicYardsPerYear, double truckVolume)
        {
            return dewateredSludgeGenerationRateCubicYardsPerYear / truckVolume;
        }

        public static double CalcPolymerRequirementPoundsPerDay(double sludgeDisposalPumpFlowRate, double targetPolymerDose, double polymerActivePercentage, double sludgePumpingTime)
        {
            return (sludgeDisposalPumpFlowRate * (targetPolymerDose / 1000000) * 60 * 8.35 / (polymerActivePercentage / 100)) * sludgePumpingTime;
        }

        public static double CalcPolymerRequirementPoundsPerYear(double polymerRequirementPoundsPerDay)
        {
            return polymerRequirementPoundsPerDay * 365;
        }

        public static double CalcEmulsionPolymerRequirementGallonsPerYear(double polymerRequirementPoundsPerYear, double emulsionPolymerDensity)
        {
            return polymerRequirementPoundsPerYear / emulsionPolymerDensity;
        }
        #endregion

        #region Capital Costs

        public static decimal CalcClarifierTankCost(double clarifierVolume, double clarifierQuantity, decimal clarifierCostFactor)
        {            
            return (decimal)(clarifierVolume * 7.481) * clarifierCostFactor * (decimal)clarifierQuantity;
        }

        public static decimal CalcClarifierInternalsCost(decimal clarifierInternalsCostFactor, double clarifierDiameter, double clarifierQuantity)
        {
            return clarifierInternalsCostFactor * (decimal)Math.Log(clarifierDiameter) * (decimal)clarifierQuantity;
        }

        public static decimal CalcOverflowWeirCost(double clarifierDiameter, double clarifierQuantity, decimal overflowWeirUnitCost)
        {
            return (decimal)(clarifierDiameter * Math.PI) * overflowWeirUnitCost * (decimal)clarifierQuantity;
        }

        public static decimal CalcCatwalkCost(double clarifierDiameter, double clarifierQuantity, decimal catwalkUnitCost)
        {
            return (decimal)clarifierDiameter * catwalkUnitCost * (decimal)clarifierQuantity;
        }

        public static decimal CalcDensityCurrentBaffleCost(double clarifierDiameter, double clarifierQuantity, decimal densityCurrentBaffleUnitCost)
        {
            return (decimal)(clarifierDiameter * Math.PI) * densityCurrentBaffleUnitCost * (decimal)clarifierQuantity;
        }

        public static decimal CalcTankProtectiveCoatingCost(double tankProtectiveCoatingSurfaceArea, double clarifierQuantity, decimal tankProtectiveCoatingUnitCost)
        {
            return (decimal)tankProtectiveCoatingSurfaceArea * tankProtectiveCoatingUnitCost * (decimal)clarifierQuantity;
        }

        public static decimal CalcSludgeRecirculationPumpCost(double sludgeRecirculationPumpPower, double clarifierQuantity)
        {
            return (decimal)(sludgeRecirculationPumpPower * 300 * 1.2) * (decimal)clarifierQuantity;
        }

        public static decimal CalcSludgeDisposalPumpCost(double sludgeRecirculationPumpPower, double clarifierQuantity, double sludgeDisposalHorizontalCentrifugalPumpQuantity)
        {
            return (decimal)(sludgeRecirculationPumpPower * 300 * 1.2) * (decimal)clarifierQuantity * (decimal)sludgeDisposalHorizontalCentrifugalPumpQuantity;
        }

        public static decimal CalcSludgeDisposalPumpConcretePadCost(double clarifierQuantity, double sludgeDisposalHorizontalCentrifugalPumpQuantity, double foundationSiteSoilMultiplier, decimal foundationConcreteMaterialAndPlacementCost)
        {
            return (decimal)(sludgeDisposalHorizontalCentrifugalPumpQuantity * (4 * 6 * foundationSiteSoilMultiplier / 27)) * foundationConcreteMaterialAndPlacementCost * 5 * (decimal)clarifierQuantity;
        }

        public static decimal CalcClarifierEquipmentCost(decimal clarifierTankCost, decimal clarifierInternalsCost, decimal overflowWeirCost, 
                                                        decimal catwalkCost, decimal densityCurrentBaffleCost, decimal tankProtectiveCoatingCost,
                                                        decimal sludgeRecirculationPumpCost, decimal sludgeDisposalPumpCost, decimal sludgeDisposalPumpConcretePadCost)
        {
            return clarifierTankCost + clarifierInternalsCost + overflowWeirCost + catwalkCost + densityCurrentBaffleCost + tankProtectiveCoatingCost + sludgeRecirculationPumpCost + sludgeDisposalPumpCost + sludgeDisposalPumpConcretePadCost;
        }

        public static decimal CalcFoundationCost(double foundationVolume, double clarifierQuantity, decimal foundationConcreteMaterialAndPlacementCost)
        {
            return (decimal)foundationVolume * foundationConcreteMaterialAndPlacementCost * (decimal)clarifierQuantity;
        }

        public static decimal CalcInstallationCost(decimal clarifierTankCost, decimal clarifierInternalsCost, decimal overflowWeirCost, decimal catwalkCost, decimal densityCurrentBaffleCost, decimal tankProtectiveCoatingCost, decimal foundationCost, double capitalCostSystemInstallationMultiplier)
        {
            return (clarifierTankCost + clarifierInternalsCost + overflowWeirCost + catwalkCost + densityCurrentBaffleCost + tankProtectiveCoatingCost + foundationCost) * (decimal)(capitalCostSystemInstallationMultiplier / 100);
        }

        public static decimal CalcSludgeDisposalBoreholeCost(double boreholeSizingMultiplier, double boreholeDrillingDepth, decimal boreholeDrillingAndCastingInstallationUnitCost)
        {
            return ((decimal)(boreholeSizingMultiplier * boreholeDrillingDepth) * boreholeDrillingAndCastingInstallationUnitCost);
        }

        public static decimal CalcSludgeDisposalBoreholeCostAvg(double boreholeQuantity, decimal boreholeCost)
        {
            return boreholeCost / (decimal)boreholeQuantity;
        }

        public static decimal CalcExcavationCost(double nominalPipeOutsideDiameter, double pipelineLength, decimal excavationCost)
        {
            return (decimal)((nominalPipeOutsideDiameter + 24) * (nominalPipeOutsideDiameter + 42)) / 144 / 27 * (decimal)pipelineLength * excavationCost;
        }

        public static decimal CalcBackfillAndCompactionCost(double nominalPipeOutsideDiameter, double pipelineLength, decimal backfillAndCompactionUnitCost)
        {
            return ((decimal)(nominalPipeOutsideDiameter + 24) * (decimal)(nominalPipeOutsideDiameter + 42) / 144 / 27 * (decimal)pipelineLength * backfillAndCompactionUnitCost) + ((decimal)((nominalPipeOutsideDiameter / 12) + 2 * 1.5 * pipelineLength) / 27 * backfillAndCompactionUnitCost);
        }

        public static decimal CalcPipeBeddingAggregateCost(double pipeBeddingAggregateWeight, decimal aggregateUnitCost)
        {
            return (decimal)pipeBeddingAggregateWeight * aggregateUnitCost;
        }

        public static decimal CalcPipeCostNOD15(double pipeLayingLength)
        {
            return (decimal)(pipeLayingLength * 1 + ((Math.Round(pipeLayingLength / 44760, 0) + 1) * 800));
        }

        public static decimal CalcPipeCostNOD2(double pipeLayingLength)
        {
            return (decimal)(pipeLayingLength * 1.25 + ((Math.Round(pipeLayingLength / 45760, 0) + 1) * 800));
        }

        public static decimal CalcPipeCostNOD3(double pipeLayingLength)
        {
            return (decimal)(pipeLayingLength * 1.45 + ((Math.Round(pipeLayingLength / 24400, 0) + 1) * 800));
        }

        public static decimal CalcPipeCostNOD4(double pipeLayingLength)
        {
            return (decimal)(pipeLayingLength * 2.5 + ((Math.Round(pipeLayingLength / 10000, 0) + 1) * 800));
        }

        public static decimal CalcPipeCostNOD6(double pipeLayingLength)
        {
            return (decimal)(pipeLayingLength * 7 + ((Math.Round(pipeLayingLength / 8000, 0) + 1) * 800));
        }

        public static decimal CalcPipeAndShippingCost(decimal pipeCost)
        {
            return pipeCost;
        }

        public static decimal CalcPipeFusionAndInstallationCost(double pipelineLength, decimal pipeFusionCost)
        {
            return (decimal)pipelineLength * pipeFusionCost;
        }

        public static decimal CalcAirVacuumReleaseAssembliesCost(double airVacuumReleaseAssembliesQuantity, decimal airVacuumReleaseAssembliesUnitCost)
        {
            return (decimal)airVacuumReleaseAssembliesQuantity * airVacuumReleaseAssembliesUnitCost;
        }

        public static decimal CalcPigLaunchersReceiversAssembliesCost(double pigLaunchersReceiversAssembliesQuantity, decimal pigLaunchersReceiversAssembliesUnitCost)
        {
            return (decimal)pigLaunchersReceiversAssembliesQuantity * pigLaunchersReceiversAssembliesUnitCost;
        }

        public static decimal CalcSludgeDisposalPipelineCost(decimal excavationCost, decimal backfillAndCompactionCost, 
                                                             decimal pipeBeddingAggregateCost, decimal pipeAndShippingCost, 
                                                             decimal pipeFusionAndInstallationCost, decimal airVacuumReleaseAssembliesCost,
                                                             decimal pigLaunchersReceiversAssembliesCost)
        {
            return excavationCost + backfillAndCompactionCost + pipeBeddingAggregateCost + pipeAndShippingCost + pipeFusionAndInstallationCost + airVacuumReleaseAssembliesCost + pigLaunchersReceiversAssembliesCost;
        }

        public static decimal CalcOtherCapitalItemsCost(double itemQuantity1, decimal itemUnitCost1,
                                                 double itemQuantity2, decimal itemUnitCost2,
                                                 double itemQuantity3, decimal itemUnitCost3,
                                                 double itemQuantity4, decimal itemUnitCost4,
                                                 double itemQuantity5, decimal itemUnitCost5)
        {
            return (decimal)itemQuantity1 * itemUnitCost1 + (decimal)itemQuantity2 * itemUnitCost2 + (decimal)itemQuantity3 * itemUnitCost3 + (decimal)itemQuantity4 * itemUnitCost4 + (decimal)itemQuantity5 * itemUnitCost5;
        }



        public static decimal CalcCapitalCostTotal(decimal clarifierEquipmentCost, decimal foundationCost, decimal installationCost, decimal sludgeDisposalBoreholeCost, decimal sludgeDisposalPipelineCost, decimal otherCapitalItemsCost)
        {
            return clarifierEquipmentCost + foundationCost + installationCost + sludgeDisposalBoreholeCost + sludgeDisposalPipelineCost + otherCapitalItemsCost;
        }

        #endregion

        #region Annual (Operations and Maintenance) Costs


        public static decimal CalcAnnualCostOperationAndMaintenance(double annualCostMultiplier, decimal capitalCostTotal)
        {
            return (decimal)(annualCostMultiplier / 100.0) * capitalCostTotal;
        }

        public static decimal CalcClarifierElectricCost(double rakeDriveMotorPower, double clarifierMotorOperationalTimeHoursPerDay, double clarifierMotorOperationalTimeDaysPerYear, double clarifierQuantity, decimal electricUnitCost)
        {
            return (decimal)(rakeDriveMotorPower * 0.7456 * clarifierMotorOperationalTimeHoursPerDay * clarifierMotorOperationalTimeDaysPerYear) * (decimal)clarifierQuantity * electricUnitCost;
        }

        public static decimal CalcImpellerMotorElectricCost(double impellerMotorPower, double clarifierMotorOperationalTimeHoursPerDay, double clarifierMotorOperationalTimeDaysPerYear, decimal electricUnitCost)
        {
            return (decimal)(0.7456 * impellerMotorPower * clarifierMotorOperationalTimeHoursPerDay * clarifierMotorOperationalTimeDaysPerYear) * electricUnitCost;
        }

        public static decimal CalcSludgeRecirculationPumpElectricCost(double sludgeRecirculationPumpPower, double clarifierMotorOperationalTimeHoursPerDay, double clarifierMotorOperationalTimeDaysPerYear, double clarifierQuantity, decimal electricUnitCost)
        {
            return (decimal)(sludgeRecirculationPumpPower * 0.7456 * clarifierMotorOperationalTimeHoursPerDay * clarifierMotorOperationalTimeDaysPerYear) * (decimal)clarifierQuantity * electricUnitCost;
        }

        public static decimal CalcSludgeDisposalPumpElectricCost(double sludgeDisposalPumpPower, double sludgeDisposalPumpTimeHoursPerDay, decimal electricUnitCost)
        {
            return (decimal)(sludgeDisposalPumpPower * 0.7456 * sludgeDisposalPumpTimeHoursPerDay * 365) * electricUnitCost;
        }

        public static decimal CalcPolymerPumpElectricCost(double polymerPumpPower, double sludgeDisposalPumpTimeHoursPerDay, decimal electricUnitCost)
        {
            return  (decimal)(polymerPumpPower / 1000 * sludgeDisposalPumpTimeHoursPerDay * 365) * electricUnitCost;
        }

        public static decimal CalcAnnualCostElectric(decimal clarifierElectricCost, decimal impellerMotorElectricCost,
                                                     decimal sludgeRecirculationPumpElectricCost, decimal sludgeDisposalPumpElectricCost,                                                    
                                                     decimal polymerPumpElectricCost)
        {
            return clarifierElectricCost + impellerMotorElectricCost + sludgeRecirculationPumpElectricCost + sludgeDisposalPumpElectricCost + polymerPumpElectricCost;
        }

        public static decimal CalcGeotubeCost(double geotubeQuantity, decimal geotubeUnitCost)
        {
            return (decimal)geotubeQuantity * geotubeUnitCost;
        }

        public static decimal CalcGeotubeDisposalCost(double triaxleLoadsRequiredPerYear, double roundtripDistance, decimal haulingTransporationUnitCost, decimal landfillTippingFee, double excavationHaulDisposeTime, decimal excavatorUnitCost)
        {
            return ((decimal)(triaxleLoadsRequiredPerYear * roundtripDistance) * haulingTransporationUnitCost) + landfillTippingFee + ((decimal)(triaxleLoadsRequiredPerYear) * (decimal)excavationHaulDisposeTime / 60 * excavatorUnitCost);
        }

        public static decimal CalcGeotubeDryPolymerCost(double dryPolymerRequirementPoundsPerYear, decimal dryPolymerUnitCost)
        {
            return (decimal)dryPolymerRequirementPoundsPerYear * dryPolymerUnitCost;
        }

        public static decimal CalcGeotubeEmulsionPolymerCost(double emulsionPolymerRequirementGallonsPerYear, decimal emulsionPolymerUnitCost)
        {
            return (decimal)emulsionPolymerRequirementGallonsPerYear * emulsionPolymerUnitCost;
        }

        public static decimal CalcTotalGeotubeSludgeDisposalCost(decimal geotubeCost, decimal geotubeDisposalCost, decimal geotubePolymerCost)
        {
            return geotubeCost + geotubeDisposalCost + geotubePolymerCost;
        }

        public static decimal CalcOtherAnnualItemsCost(double itemQuantity1, decimal itemUnitCost1,
                                                       double itemQuantity2, decimal itemUnitCost2,
                                                       double itemQuantity3, decimal itemUnitCost3,
                                                       double itemQuantity4, decimal itemUnitCost4,
                                                       double itemQuantity5, decimal itemUnitCost5)
        {
            return (decimal)itemQuantity1 * itemUnitCost1 + (decimal)itemQuantity2 * itemUnitCost2 + (decimal)itemQuantity3 * itemUnitCost3 + (decimal)itemQuantity4 * itemUnitCost4 + (decimal)itemQuantity5 * itemUnitCost5;
        }

        public static decimal CalcAnnualCostTotal(decimal annualOperationAndMaintanenceCost, decimal annualElectricCost, decimal annualGeotubeSludgeDisposalCost, decimal otherAnnualItemsCost)
        {
            return annualOperationAndMaintanenceCost + annualElectricCost + annualGeotubeSludgeDisposalCost + otherAnnualItemsCost;
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
