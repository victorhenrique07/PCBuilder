using System;

namespace PCBuilder.Domain.Recommendations
{
    public class UseProfile : IEquatable<UseProfile>
    {
        public const decimal MaxEstimatedVideoCardPriceFactor = 0.4m;
        protected UseProfile() { }

        public UseProfile(string name, decimal gamingFocus, decimal multiThreadFocus,
            decimal estimatedVideoCardPriceFactor, decimal cpuValueFactorFor20PercentMorePerformance,
            decimal videoCardValueFactorFor20PercentMorePerformance,
            decimal stogareDeviceValueFactorForDoublePerformance, bool available)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            Name = name;
            GamingFocus = gamingFocus;
            MultiThreadFocus = multiThreadFocus;
            EstimatedVideoCardPriceFactor = estimatedVideoCardPriceFactor;
            CpuValueFactorFor20PercentMorePerformance = cpuValueFactorFor20PercentMorePerformance;
            VideoCardValueFactorFor20PercentMorePerformance = videoCardValueFactorFor20PercentMorePerformance;
            StogareDeviceValueFactorForDoublePerformance = stogareDeviceValueFactorForDoublePerformance;
            Available = available;
        }

        public virtual int Id { get; protected set; }

        public virtual string Name { get; protected set; }

        public virtual decimal MultiThreadFocus { get; protected set; }

        public virtual decimal GamingFocus { get; protected set; }

        public virtual decimal VideoEditingFocus { get; protected set; }

        public virtual decimal EstimatedVideoCardPriceFactor { get; protected set; }

        public virtual decimal CpuValueFactorFor20PercentMorePerformance { get; protected set; }

        public virtual decimal VideoCardValueFactorFor20PercentMorePerformance { get; protected set; }

        public virtual decimal StogareDeviceValueFactorForDoublePerformance { get; protected set; }

        public virtual decimal StogareDeviceSecondaryValueFactorForDoublePerformance
        {
            get { return this.StogareDeviceValueFactorForDoublePerformance * 0.9m; }
        }

        public virtual byte RequiredMonitorImageQualityLevel { get; protected set; }

        public virtual decimal HighMemoryProductivityFactor { get; protected set; }

        public virtual decimal LowMemoryProductivityFactor { get; protected set; }

        public virtual decimal HighMemoryGamingFactor { get; protected set; }

        public virtual decimal LowMemoryGamingFactor { get; protected set; }

        public virtual WorkloadLevel CpuWorkloadLevel { get; protected set; }

        public virtual decimal DefaultUtilizationInHoursPerDay { get; protected set; }

        public virtual decimal QuickSyncValue { get; protected set; }

        public virtual UseProfile OrdinaryTasksRelatedUseProfile { get; protected set; }

        public virtual bool Available { get; protected set; }

        public virtual decimal BudgetAsGamingPCFactor
        {
            get { return (1m - this.EstimatedVideoCardPriceFactor) / (1m - MaxEstimatedVideoCardPriceFactor); }
        }

        public virtual decimal OrdinaryTasksFocus { get; protected set; }

        public virtual decimal SingleThreadFocus { get { return 1m - this.MultiThreadFocus; } }

        public virtual decimal MainDesktopFocus { get { return 1m - this.OrdinaryTasksFocus - this.GpuFocus; } }

        public virtual decimal ProductivityFocus { get { return 1m - this.GamingFocus; } }

        public virtual decimal GpuFocus { get { return this.GamingFocus + this.VideoEditingFocus; } }

        public virtual bool GpuPerformanceRelated { get { return this.GpuFocus > 0m; } }

        public virtual bool GamingRelated { get { return this.GamingFocus > 0m; } }

        public virtual bool VideoEditingRelated { get { return this.VideoEditingFocus > 0m; } }

        public virtual bool RequiresDualChannelMemory
        {
            get { return this.GamingRelated || this.VideoEditingRelated || this.MultiThreadFocus >= 0.7m; }
        }

        public virtual decimal GetBudgetAsGamingPC(decimal budget, BuildGeneralParams generalParams)
        {
            decimal estimatedIntegratedGpuCost = (this.GpuPerformanceRelated ? 0m :
                generalParams.OrdinaryIntegratedGpuExtraCost) * generalParams.LocalMoneyPerDolar;

            return Math.Round((budget - estimatedIntegratedGpuCost) * this.BudgetAsGamingPCFactor, 2,
                MidpointRounding.AwayFromZero);
        }

        public virtual decimal GetStorageBudget(decimal budget, BuildGeneralParams generalParams)
        {
            decimal budgetAsGamingPC = GetBudgetAsGamingPC(budget, generalParams);

            decimal rawBudget = 20m + generalParams.LocalMoneyPerDolar +
                budgetAsGamingPC * generalParams.StorageBudgetFactor;

            return Math.Round(rawBudget, 2, MidpointRounding.AwayFromZero);
        }

        public virtual bool Equals(UseProfile other)
        {
            if (other == null)
                return false;

            return Name.Equals(other.Name);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as UseProfile);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}