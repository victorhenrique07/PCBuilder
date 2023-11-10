using PCBuilder.Domain.Products.Cpus;
using PCBuilder.Domain.Products.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PCBuilder.Domain.Products.MotherboardAndMemory
{
    public class MotherboardChipset : PCComponent, IEquatable<MotherboardChipset>
    {
        private readonly ISet<SupportedCpuLine> supportedCpuLines;

        protected MotherboardChipset() { }

        public MotherboardChipset(string name, Manufacturer manufacturer, CpuSocket cpuSocket, short generation,
            bool hasCpuOverclockingSupport, decimal maxMemoryFrequency, decimal maxPcieVersion)
            : base(name, manufacturer)
        {
            if (cpuSocket == null)
                throw new ArgumentNullException(nameof(cpuSocket));

            this.CpuSocket = cpuSocket;
            this.Generation = generation;
            this.HasCpuOverclockingSupport = hasCpuOverclockingSupport;
            this.MaxMemoryFrequency = maxMemoryFrequency;
            this.MaxPcieVersion = maxPcieVersion;

            this.supportedCpuLines = new HashSet<SupportedCpuLine>();
        }

        public virtual int Id { get; set; }

        public virtual CpuSocket CpuSocket { get; protected set; }

        public virtual short Generation { get; protected set; }

        public virtual bool HasCpuOverclockingSupport { get; protected set; }

        public virtual decimal MaxMemoryFrequency { get; protected set; }

        public virtual decimal MaxPcieVersion { get; protected set; }

        public virtual decimal PcieSupportValueModifier { get; protected set; }

        public virtual decimal ResizableBarValueModifier { get; protected set; }

        public virtual decimal OtherFeaturesValueModifier { get; protected set; }

        public virtual decimal MotherboardValueModifier
        {
            get
            {
                return this.PcieSupportValueModifier + this.ResizableBarValueModifier +
                    this.OtherFeaturesValueModifier;
            }
        }

        public virtual decimal CpuSupportValueFactor { get; protected set; }

        public virtual decimal OverclockingValueFactor { get; protected set; }

        public virtual IEnumerable<SupportedCpuLine> SupportedCpuLines { get { return this.supportedCpuLines; } }

        public virtual bool Supports(CpuLine cpuLine, out bool needsBiosUpdate)
        {
            needsBiosUpdate = false;

            SupportedCpuLine supportedLine = this.supportedCpuLines.SingleOrDefault(s => s.Line.Equals(cpuLine));

            if (supportedLine == null)
                return false;

            needsBiosUpdate = supportedLine.NeedsBiosUpdate;

            return true;
        }

        public virtual bool Equals(MotherboardChipset other)
        {
            if (other == null)
                return false;

            return new { this.Name, this.CpuSocket }
                .Equals(new { other.Name, other.CpuSocket });
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as MotherboardChipset);
        }

        public override int GetHashCode()
        {
            return new { this.Name, this.CpuSocket }.GetHashCode();
        }

        public override string ToString()
        {
            return $"{this.Manufacturer} {this.Name}";
        }
    }
}