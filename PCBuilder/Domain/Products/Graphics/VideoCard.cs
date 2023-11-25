using NHibernate.Mapping;
using PCBuilder.Domain.Price;
using PCBuilder.Domain.Products.MotherboardAndMemory;
using PCBuilder.Domain.Products.Shared;
using PCBuilder.Domain.Recommendations;
using System;
using System.Collections.Generic;

namespace PCBuilder.Domain.Products.Graphics
{
    public class VideoCard : Shared.Product, IGraphicsProvider, IValueMesurable<VideoCard>, IColored
    {
        public const decimal DefaultVrmEfficiency = 0.9m;

        protected VideoCard() { }

        public VideoCard(ProductIdentification identification, Manufacturer manufacturer, string serie, string name,
            string details, VideoCardChipset chipset, decimal baseFrequency, decimal boostFrequency,
            decimal memoryFrequency, ProductDimensions dimensions, int fans, ProductColor mainColor, decimal coolingScore,
            decimal vrmScore, BackplateType? backplateType, decimal powerConsumption)
            : base(identification, manufacturer, serie, name, details, ProductCategory.VideoCard, powerConsumption)
        {
            if (chipset == null)
                throw new ArgumentNullException(nameof(chipset));

            this.Category = ProductCategory.VideoCard;
            this.Chipset_ = chipset;
            this.BaseFrequency = baseFrequency;
            this.BoostFrequency = boostFrequency;
            this.MemoryFrequency = memoryFrequency;
            this.Dimensions = dimensions;
            this.Fans = fans;
            this.MainColor = mainColor;
            this.CoolingScore = coolingScore;
            this.VrmScore = vrmScore;
            this.BackplateType = backplateType;
            this.WarrantyTime = manufacturer.VideoCardDefaultWarranty.Value;

            this.LedsValue = 0m;
            this.OtherFeaturesValue = 0m;
        }

        public virtual VideoCardChipset Chipset_ { get; protected set; }

        public virtual decimal BaseFrequency { get; set; }

        public virtual decimal BoostFrequency { get; set; }

        public virtual decimal MemoryFrequency { get; set; }

        public virtual decimal LedsValue { get; set; }

        public virtual decimal OtherFeaturesValue { get; set; }

        public virtual BackplateType? BackplateType { get; set; }

        public virtual bool HasBackplate { get { return this.BackplateType.HasValue; } }

        public virtual ProductDimensions Dimensions { get; protected set; }

        public virtual ProductColor MainColor { get; protected set; }

        public virtual decimal EffectiveHeight
        {
            get
            {
                if (!this.HasBackplate)
                    return this.Dimensions.Height;

                return this.Dimensions.Height - 2.5m;
            }
        }

        public virtual int Fans { get; set; }

        public virtual decimal CoolingScore { get; set; }

        public virtual decimal BuildQualityBackplateModifier
        {
            get
            {
                if (!this.HasBackplate)
                    return -1m / 3m;
                else if (this.BackplateType == PCBuilder.BackplateType.Plastic)
                    return -1m / 6m;
                else if (this.BackplateType == PCBuilder.BackplateType.Metal)
                    return 0m;
                else
                    throw new InvalidOperationException();
            }
        }
        
        public virtual decimal VrmScore { get; set; }

        public virtual decimal? MaxPowerLimit { get; set; }

        public virtual decimal VrmEfficiency
        {
            get { return DefaultVrmEfficiency + (this.VrmScore - 5m) / 100m; }
        }

        public virtual decimal WeightedEffectivePowerConsumption
        {
            get
            {
                decimal maxPowerLimit = this.MaxPowerLimit ?? this.PowerConsumption * 1.05m;
                var weightedPowerConsumption = this.PowerConsumption * 0.8m + maxPowerLimit * 0.2m;
                return weightedPowerConsumption * this.VrmEfficiency;
            }
        }

        public virtual decimal GamingPerformanceFactor
        {
            get
            {
                decimal gpuFrequencyRatio = this.BoostFrequency / this.Chipset_.BoostFrequency;
                decimal gpuFrequencyOffset = Math.Min(Math.Round((gpuFrequencyRatio - 1m) / 3m, 3), 0.05m);

                decimal defaultEffectivePower = this.Chipset_.PowerConsumption /* * DefaultOverclockPowerLimit*/ * DefaultVrmEfficiency;
                decimal effectivePower = this.WeightedEffectivePowerConsumption;
                decimal effectivePowerRatio = effectivePower / defaultEffectivePower;
                decimal powerOffset = Math.Min(Math.Round((effectivePowerRatio - 1m) / 6m, 3), 0.025m);

                decimal memoryFrequencyRatio = this.MemoryFrequency / this.Chipset_.Memory.Frequency;
                decimal memmoryFrequencyOffset =  Math.Min(Math.Round((memoryFrequencyRatio - 1m) / 3m, 3), 0.05m);

                return 1m + gpuFrequencyOffset + powerOffset + memmoryFrequencyOffset;
            }
        }

        public virtual decimal BuildQualityScore
        {
            get
            {
                decimal baseScore = this.CoolingScore * 0.8m + this.VrmScore * 0.2m;

                return baseScore + this.BuildQualityBackplateModifier;
            }
        }

        public virtual decimal BuildQualityValueFactor
        {
            get
            {
                return 1m + (this.BuildQualityScore - 5m) * 0.03m;
            }
        }

        public override bool IsSuppliedByPowerSupply { get { return true; } }

        public virtual void ChangeMainColor(ProductColor color)
        {
            if (!Enum.IsDefined(typeof(ProductColor), color))
                throw new ArgumentOutOfRangeException(nameof(color));

            this.MainColor = color;
        }

        public override decimal GetPowerConsumption(WorkloadLevel workloadLevel)
        {
            return this.PowerConsumption * (0.1m + (int)workloadLevel * 0.3m);
        }

        public override decimal PeakPowerConsumption
        {
            get { return this.PowerConsumption * this.Chipset_.PeakPowerConsumptionFactor; }
        }

        public virtual void ChangeModel(VideoCardChipset model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            this.Chipset_ = model;
        }

        public virtual void ChangeDimensions(ProductDimensions dimensions)
        {
            if (dimensions == null)
                throw new ArgumentNullException(nameof(dimensions));

            this.Dimensions = dimensions;
        }

        public virtual decimal GetGamingPerformance(ValueMeasurementContext<VideoCard> context, decimal pcieVersion)
        {
            decimal basePerformance = this.Chipset_.GetGamingPerformance(pcieVersion, context.LowPcieVersion);
            return basePerformance * this.GamingPerformanceFactor;
        }

        public virtual decimal GetVideoEditingPerformance(ValueMeasurementContext<VideoCard> context, decimal pcieVersion)
        {
            return this.GetGamingPerformance(context, pcieVersion) * this.Chipset_.VideoEditingPerformanceFactor *
                this.Chipset_.MemoryCapacityValueFactor * this.Chipset_.MemoryBandwidthValueFactor;
        }

        public virtual decimal GetGraphicsWeightedPerformance(ValueMeasurementContext<VideoCard> context, decimal pcieVersion)
        {
            if (!context.UseProfile.GpuPerformanceRelated)
                return 0m;

            if (context.UseProfile.GamingRelated && context.UseProfile.VideoEditingRelated)
            {
                /* TODO: review */
                return (this.GetGamingPerformance(context, pcieVersion) + this.GetVideoEditingPerformance(context, pcieVersion)) / 2m;
            }
            else if (context.UseProfile.GamingRelated)
            {
                return this.GetGamingPerformance(context, pcieVersion);
            }
            else
            {
                return this.GetVideoEditingPerformance(context, pcieVersion);
            }
        }

        public virtual bool Similar(VideoCard other)
        {
            return other != null && this.Chipset_.Similar(other.Chipset_);
        }

        public override decimal GetDefaultValue(BuildSpecification buildSpecification, BuildGeneralParams generalParams)
        {
            var context = new ValueMeasurementContext<VideoCard>(buildSpecification, generalParams);

            return this.GetValue(context, null, false);
        }

        public virtual decimal GetQualityLevel(ValueMeasurementContext<VideoCard> context)
        {
            if (this.Equals(context.BaselinePart.Product))
                return context.BaselinePart.QualityLevel;

            return this.Chipset_.GetVideoCardQualityLevel(this, context);
        }

        public virtual decimal GetCoreValue(ValueMeasurementContext<VideoCard> context)
        {
            return this.GetQualityLevel(context) * DolarsPerQualityLevel;
        }

        public virtual decimal GetAestheticsValue(ValueMeasurementContext<VideoCard> context)
        {
            //return this.LedsValue;
            return 0m;
        }

        public virtual decimal GetExtraFeaturesValue(ValueMeasurementContext<VideoCard> context, Store store, bool openBox)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            decimal extraFeaturesValue = this.Chipset_.GetExtraFeaturesValue(this, context, store, openBox);

            extraFeaturesValue += this.OtherFeaturesValue + this.GetAestheticsValue(context);

            return RoundValue(extraFeaturesValue);
        }

        public virtual decimal GetValue(ValueMeasurementContext<VideoCard> context, Store store, bool openBox)
        {
            return this.GetCoreValue(context) + this.GetExtraFeaturesValue(context, store, openBox);
        }

        public override bool Comparable(int quantiy, ISellable part, int partQuantity, bool toMakeBuild,
            out bool highlyComparable)
        {
            VideoCard other = part as VideoCard;

            if (other == null)
            {
                highlyComparable = false;
                return false;
            }

            bool sameGpuManufacturer = this.Chipset_.Gpu.Manufacturer.Equals(other.Chipset_.Gpu.Manufacturer);
            decimal enhancedMemoryCapacity = Math.Round(this.Chipset_.Memory.Capacity * 4m / 3m, 1);

            bool comparable;

            if (toMakeBuild)
            {
                comparable = this.Chipset_.SameOrMoreExtraTecnologies(other.Chipset_) && sameGpuManufacturer &&
                    enhancedMemoryCapacity >= other.Chipset_.Memory.Capacity;
                highlyComparable = comparable;
                return comparable;
            }

            comparable = this.Chipset_.SameOrMoreExtraTecnologies(other.Chipset_) &&
                enhancedMemoryCapacity >= other.Chipset_.Memory.Capacity;
            highlyComparable = comparable && sameGpuManufacturer;
            return comparable;
        }
    }
}
