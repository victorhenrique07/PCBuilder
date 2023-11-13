using PCBuilder.Domain.Products.Shared;
using System;
using System.Collections.Generic;

namespace PCBuilder.Domain.Products.MotherboardAndMemory
{
    public class MemoryModuleConfiguration : IEquatable<MemoryModuleConfiguration>
    {
        public static IReadOnlyCollection<MemoryModuleConfiguration> Create(Range<decimal> capacitiesRange,
            bool requiresDualChannelMemory)
        {
            var moduleConfigurations = new List<MemoryModuleConfiguration>();

            for (decimal capacity = capacitiesRange.Begin; capacity <= capacitiesRange.End; capacity *= 2m)
            {
                if (!requiresDualChannelMemory)
                    moduleConfigurations.Add(new MemoryModuleConfiguration(1, capacity));

                moduleConfigurations.Add(new MemoryModuleConfiguration(2, capacity / 2m));
                moduleConfigurations.Add(new MemoryModuleConfiguration(4, capacity / 4m));
            }

            return moduleConfigurations;
        }
        
        protected MemoryModuleConfiguration() { }

        public MemoryModuleConfiguration(short quantity, decimal capacity)
        {
            this.Quantity = quantity;
            this.Capacity = capacity;
        }

        public virtual short Quantity { get; protected set; }

        public virtual decimal Capacity { get; protected set; }

        public virtual decimal TotalCapacity { get { return this.Quantity * this.Capacity; } }

        //public virtual decimal IntegratedGpuPerformanceCapacityFactor { get; protected set; }

        //public virtual decimal IntegratedGpuPerformanceChannelsFactor { get; protected set; }

        //public virtual decimal IntegratedGpuPerformanceChannelsExponent { get; protected set; }

        //public virtual decimal VideoCardPerformanceCapacityFactor { get; protected set; }

        //public virtual decimal VideoCardPerformanceChannelsFactor { get; protected set; }

        //public virtual decimal VideoCardPerformanceChannelsExponent { get; protected set; }

        //public virtual decimal DesktopPerformanceCapacityFactor { get; protected set; }

        //public virtual decimal DesktopPerformanceChannelsFactor { get; protected set; }

        //public virtual decimal DesktopPerformanceChannelsExponent { get; protected set; }

        public virtual bool Equals(MemoryModuleConfiguration other)
        {
            if (other == null)
                return false;

            return new { this.Quantity, this.Capacity }.Equals(new { other.Quantity, other.Capacity });
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as MemoryModuleConfiguration);
        }

        public override int GetHashCode()
        {
            return new { this.Quantity, this.Capacity }.GetHashCode();
        }

        public override string ToString()
        {
            return $"{this.Quantity} x {this.Capacity:n0} GB";
        }
    }
}