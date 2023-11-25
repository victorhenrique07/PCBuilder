using PCBuilder.Domain.Products.Cpus;
using PCBuilder.Domain.Products.Graphics;
using PCBuilder.Domain.Products.MotherboardAndMemory;
using PCBuilder.Domain.Products.Peripherals;
using PCBuilder.Domain.Recommendations;
using System;

namespace PCBuilder.Domain.Products.Shared
{
    public class ValueMeasurementContext
    {
        public Type ProductCategory { get; protected set; }

        public BuildSpecification BuildSpecification { get; protected set; }

        public BuildGeneralParams BuildGeneralParams { get; protected set; }

        public UseProfile UseProfile { get { return BuildSpecification?.UseProfile; } }

        public FpsTarget FpsTarget { get { return BuildSpecification?.FpsTarget; } }

        public bool NeedsIntegratedGpu
        {
            get { return this.BuildSpecification != null ? BuildSpecification.NeedsIntegratedGpu : false; }
        }
    }

    public class ValueMeasurementContext<T> : ValueMeasurementContext where T : Product
    {
        public ValueMeasurementContext(BuildSpecification buildSpecification, BuildGeneralParams buildGeneralParams)
        {
            this.ProductCategory = typeof(T);
            this.BuildSpecification = buildSpecification;
            this.BuildGeneralParams = buildGeneralParams;
            this.BaselinePart = buildGeneralParams.GetBaselinePart<T>();
            this.DaysOfUse = buildGeneralParams.DaysOfUse;
            this.UtilizationInHoursPerDay = buildSpecification != null ?
                buildSpecification.UseProfile.DefaultUtilizationInHoursPerDay : 2m;
            this.EnergyCostPerKWh = buildGeneralParams.LocalMoneyEnergyCostPerKWh /
                buildGeneralParams.LocalMoneyPerDolar;

            if (typeof(T) == typeof(Cpu))
            {
                this.IntegratedGpuValueMeasurementContext =
                    new ValueMeasurementContext<VideoCard>(buildSpecification, buildGeneralParams);
            }

            if (typeof(T) == typeof(Memory))
            {
                this.MemoryValueFactorFor20PercentMorePerformance = buildGeneralParams
                    .MemoryValueFactorFor20PercentMorePerformance;
            }

            if (typeof(T) == typeof(Memory) || typeof(T) == typeof(Ssd) || typeof(T) == typeof(HardDrive))
            {
                this.ValueFactorForDoubleCapacity = buildGeneralParams.ValueFactorForDoubleCapacity;
            }

            if (typeof(T) == typeof(VideoCard))
            {
                this.VideoCardMinQualityLevel = buildGeneralParams.VideoCardMinQualityLevel;
                this.IntegratedGpuMinQualityLevel = buildGeneralParams.IntegratedGpuMinQualityLevel;
                this.VideoCardValueFactorFor20PercentMorePerformance = buildSpecification.UseProfile
                    .VideoCardValueFactorFor20PercentMorePerformance;
            }
        }

        public BaselineProduct<T> BaselinePart { get; }

        public ValueMeasurementContext<VideoCard> IntegratedGpuValueMeasurementContext { get; }

        public decimal VideoCardMinQualityLevel { get; }

        public decimal IntegratedGpuMinQualityLevel { get; }

        public decimal VideoCardMinSuitableMemoryCapacity { get { return 2m; } }

        public decimal VideoCardValueFactorFor20PercentMorePerformance { get; }

        public decimal LowPcieVersion { get; set; } = 3.0m; 

        public decimal LowPcieVersionWeigth { get; set; } = 1m / 2m;

        public decimal MemoryValueFactorFor20PercentMorePerformance { get; }

        public decimal ValueFactorForDoubleCapacity { get; }

        public decimal DaysOfUse { get; internal set; }

        public decimal UtilizationInHoursPerDay { get; internal set; }

        public decimal EnergyCostPerKWh { get; internal set; }
    }
}
