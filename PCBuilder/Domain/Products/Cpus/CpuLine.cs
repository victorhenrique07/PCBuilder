using PCBuilder.Domain.Products.MotherboardAndMemory;
using System;

namespace PCBuilder.Domain.Products.Cpus
{
    public class CpuLine : IEquatable<CpuLine>
    {
        protected CpuLine() { }

        public CpuLine(string codename, CpuMicroarchitecture microarchitecture, CpuSocket socket,
            decimal motherboardPricesValueModifier, decimal stabilityValueFactor)
        {
            this.Codename = codename;
            this.Microarchitecture = microarchitecture;
            this.Socket = socket;
            this.MotherboardPricesValueModifier = motherboardPricesValueModifier;
            this.StabilityValueFactor = stabilityValueFactor;
        }

        public virtual int Id { get; protected set; }

        public virtual string Codename { get; protected set; }

        public virtual CpuMicroarchitecture Microarchitecture { get; protected set; }

        public virtual CpuSocket Socket { get; protected set; }

        public virtual MotherboardChipset RecommendedChipset { get; protected set; }

        public virtual bool EndOfLine { get; protected set; }

        public virtual decimal MotherboardPricesValueModifier { get; protected set; }

        public virtual decimal StabilityValueFactor { get; protected set; }

        public virtual bool Equals(CpuLine other)
        {
            if (other == null)
                return false;

            return this.Codename.Equals(other.Codename);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as CpuLine);
        }

        public override int GetHashCode()
        {
            return this.Codename.GetHashCode();
        }

        public override string ToString()
        {
            return $"{this.Codename} ({this.Microarchitecture.Codename})";
        }
    }
}
