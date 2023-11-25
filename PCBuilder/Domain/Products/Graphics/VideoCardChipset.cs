using PCBuilder.Domain.Price;
using PCBuilder.Domain.Products.Peripherals;
using PCBuilder.Domain.Products.Shared;
using System;

namespace PCBuilder.Domain.Products.Graphics
{
    public class VideoCardChipset : IEquatable<VideoCardChipset>
    {
        protected VideoCardChipset() { }

        public VideoCardChipset(string serie, int serieGeneration, string name, string description, Gpu gpu,
            decimal baseFrequency, decimal boostFrequency, GraphicsMemory memory, decimal powerConsumption,
            decimal peakPowerConsumptionFactor, decimal? gamingPerformance, decimal memoryCapacityValueFactor)
        {
            this.Serie = serie;
            this.SerieGeneration = serieGeneration;
            this.Name = name;
            this.Description = description;
            this.Gpu = gpu;
            this.BaseFrequency = baseFrequency;
            this.BoostFrequency = boostFrequency;
            this.Memory = memory;
            this.PowerConsumption = powerConsumption;
            this.PeakPowerConsumptionFactor = peakPowerConsumptionFactor;
            this.GamingPerformance = gamingPerformance;
            this.MemoryCapacityValueFactor = memoryCapacityValueFactor;
            this.EndOfLine = false;
        }

        public virtual int Id { get; protected set; }

        public virtual string Serie { get; protected set; }

        public virtual int SerieGeneration { get; protected set; }

        public virtual string Name { get; protected set; }

        public virtual string Description { get; protected set; }

        public virtual Gpu Gpu { get; protected set; }

        public virtual decimal BaseFrequency { get; protected set; }

        public virtual decimal BoostFrequency { get; protected set; }

        public virtual GraphicsMemory Memory { get; protected set; }

        public virtual decimal PcieVersion { get; protected set; }

        public virtual short PcieLanes { get; protected set; }

        public virtual decimal LowPcieVersionPerformanceFactor { get; protected set; }

        public virtual decimal PowerConsumption { get; protected set; }

        public virtual decimal PeakPowerConsumptionFactor { get; protected set; }

        public virtual decimal? GamingPerformance { get; protected set; }

        public virtual decimal VideoEditingPerformanceFactor { get; protected set; }

        public virtual decimal MemoryCapacityValueFactor { get; protected set; }

        public virtual decimal MemoryBandwidthValueFactor { get; protected set; }

        public virtual bool EndOfLine { get; protected set; }

        public virtual string ShortDescription
        {
            get
            {
                if (this.Name.Contains(this.Serie))
                    return this.Name;

                return  $"{this.Serie} {this.Name}";
            }
        }

        public virtual decimal GetGamingPerformance(decimal pcieVersion, decimal lowPcieVersion)
        {
            decimal performance = this.GamingPerformance ?? 0m;

            decimal pcieVersionPerformanceFactor = pcieVersion > lowPcieVersion ?
                1m : this.LowPcieVersionPerformanceFactor;

            return performance * pcieVersionPerformanceFactor;
        }

        public virtual bool SameExtraTecnologies(VideoCardChipset other)
        {
            if (other == null)
                return false;

            return this.Gpu.HasRayTracing == other.Gpu.HasRayTracing && this.Gpu.HasDlss == other.Gpu.HasDlss;
        }

        public virtual bool SameOrMoreExtraTecnologies(VideoCardChipset other)
        {
            if (other == null)
                return false;

            return this.Gpu.SameOrMoreExtraTecnologies(other.Gpu);
        }

        public virtual decimal PowerConsumptionValueModifier(VideoCard videoCard, ValueMeasurementContext<VideoCard> context)
        {
            decimal totalHoursOfUtilization = context.UtilizationInHoursPerDay * context.DaysOfUse;

            decimal coreValueRatio = this.GetVideoCardCoreValue(videoCard, context) /
                context.BaselinePart.Product.Chipset_.GetVideoCardCoreValue(context.BaselinePart.Product, context);

            decimal expectedEnergyConsumption = coreValueRatio * context.BaselinePart.Product.PowerConsumption *
                totalHoursOfUtilization;

            decimal energyConsumption = videoCard.PowerConsumption * totalHoursOfUtilization;
            
            decimal energyCost = energyConsumption / 1000m * context.EnergyCostPerKWh;
            decimal expectedEnergyCost = expectedEnergyConsumption / 1000m * context.EnergyCostPerKWh;

            decimal extraCost = energyCost - expectedEnergyCost;

            return Product.RoundValue(-extraCost);
        }

        public virtual decimal GetVideoCardQualityLevel(VideoCard videoCard, ValueMeasurementContext<VideoCard> context)
        {
            if (!videoCard.Chipset_.Equals(this))
                throw new ArgumentOutOfRangeException(nameof(videoCard));

            decimal performance = videoCard.GetGraphicsWeightedPerformance(context, this.PcieVersion) *
                (1m - context.LowPcieVersionWeigth) +
                videoCard.GetGraphicsWeightedPerformance(context, context.LowPcieVersion) *
                context.LowPcieVersionWeigth;

            decimal baselinePerformance = context.BaselinePart.Product.GetGraphicsWeightedPerformance(context, this.PcieVersion) *
                (1m - context.LowPcieVersionWeigth) +
                context.BaselinePart.Product.GetGraphicsWeightedPerformance(context, context.LowPcieVersion) *
                context.LowPcieVersionWeigth;

            decimal rawQualityLevel = Gpu
                .GetGraphicsProviderRawQualityLevel(performance, baselinePerformance, context);

            decimal qualityLevel = Product.RoundQualityLevel(rawQualityLevel);

            if (qualityLevel == context.VideoCardMinQualityLevel)
            {
                decimal qualityLevelsPerGigabytes = 1m;
                qualityLevel += (this.Memory.Capacity - context.VideoCardMinSuitableMemoryCapacity) * qualityLevelsPerGigabytes;
            }

            return qualityLevel;
        }

        public virtual decimal GetVideoCardCoreValue(VideoCard videoCard, ValueMeasurementContext<VideoCard> context)
        {
            return this.GetVideoCardQualityLevel(videoCard, context) * Product.DolarsPerQualityLevel;
        }

        public virtual decimal GetExtraFeaturesValue(VideoCard videoCard,
            ValueMeasurementContext<VideoCard> context, Store store, bool openBox)
        {
            decimal rawExtraFeaturesValue = 0;

            decimal coreValue = GetVideoCardCoreValue(videoCard, context);

            rawExtraFeaturesValue += coreValue * (videoCard.BuildQualityValueFactor - 1m);

            rawExtraFeaturesValue += coreValue * (this.MemoryCapacityValueFactor - 1m);

            rawExtraFeaturesValue += coreValue * (this.MemoryBandwidthValueFactor - 1m);

            rawExtraFeaturesValue += coreValue * (this.Gpu.ExtraFeaturesValueFactor - 1m);
           
            rawExtraFeaturesValue += this.PowerConsumptionValueModifier(videoCard, context);

            decimal valueBeforeWarranty = coreValue + rawExtraFeaturesValue;

            rawExtraFeaturesValue += valueBeforeWarranty * videoCard.GetWarrantyExtraValueFactor(store, openBox);

            return Product.RoundValue(rawExtraFeaturesValue);
        }

        public virtual decimal GetValue(VideoCard videoCard, ValueMeasurementContext<VideoCard> context, Store store, bool openBox)
        {
            return this.GetVideoCardCoreValue(videoCard, context) + this.GetExtraFeaturesValue(videoCard, context, store, openBox);
        }

        public virtual bool Similar(VideoCardChipset other)
        {
            if (other == null)
                return false;

            return new
            {
                this.Gpu.Microarchitecture,
                this.Gpu.Cores,
                this.Memory.BusWidth
            }
            .Equals(new
            {
                other.Gpu.Microarchitecture,
                other.Gpu.Cores,
                other.Memory.BusWidth
            });
        }

        public virtual bool Equals(VideoCardChipset other)
        {
            if (other == null)
                return false;

            return new { this.Name, this.Gpu, this.Memory.Type }
                .Equals(new { other.Name, other.Gpu, this.Memory.Type });
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as VideoCardChipset);
        }

        public override int GetHashCode()
        {
            return new { this.Name, this.Gpu, this.Memory.Type }.GetHashCode();
        }

        public override string ToString()
        {
            return this.Description;
        }
    }
}