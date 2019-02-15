using AMDTreat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMDTreat.ViewModels
{
    public class PlaceholderViewModel : PropertyChangedBase
    {
        private double _retentionTime;
        /// <summary>
        ///  User specified 
        /// </summary>
        public double RetentionTime
        {
            get { return _retentionTime; }
            set
            {
                ChangeAndNotify(ref _retentionTime, value, nameof(RetentionTime));
            }
        }

        private decimal _calcCapitalCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal CalcCapitalCost
        {
            get { return _calcCapitalCost; }
            set
            {
                ChangeAndNotify(ref _calcCapitalCost, value, nameof(CalcCapitalCost), new string[] { "CapitalCostData" });
            }
        }

        private decimal _calcRecapCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal CalcRecapCost
        {
            get { return _calcRecapCost; }
            set
            {
                ChangeAndNotify(ref _calcRecapCost, value, nameof(CalcRecapCost));
            }
        }

        private decimal _capitalCostData;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal CapitalCostData
        {
            get { return CalcCapitalCost; }
            set
            {
                ChangeAndNotify(ref _capitalCostData, value, nameof(CapitalCostData));
            }
        }

        private decimal _recapCostData;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal RecapCostData
        {
            get { return CalcRecapCost; }
            set
            {
                ChangeAndNotify(ref _recapCostData, value, nameof(RecapCostData));
            }
        }

        private decimal _annualCostData;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal AnnualCostData
        {
            get { return CalcAnnualCost; }
            set
            {
                ChangeAndNotify(ref _annualCostData, value, nameof(AnnualCostData));
            }
        }

        private decimal _calcAnnualCost;
        /// <summary>
        ///  User specified 
        /// </summary>
        public decimal CalcAnnualCost
        {
            get { return _calcAnnualCost; }
            set
            {
                ChangeAndNotify(ref _calcAnnualCost, value, nameof(CalcAnnualCost), new string[] { "AnnualCostData" });
            }
        }

        public PlaceholderViewModel()
        {
            ModuleName = "Placeholder";
            ModuleTreatmentType = "Placeholder";
            RetentionTime = 0;

            CalcCapitalCost = 0m;
            CalcRecapCost = 0m;
            CalcAnnualCost = 0m;

            ModuleId = random.Next(1000);
        }

    }
}
