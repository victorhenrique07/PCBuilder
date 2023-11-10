using System;

namespace PCBuilder.Domain.Products.MotherboardAndMemory
{
    public class MemoryType : IEquatable<MemoryType>
    {
        protected MemoryType() { }

        public MemoryType(MemoryChipType chipType, MemoryModuleType moduleType)
        {
            this.ModuleType = moduleType;
            this.ChipType = chipType;
        }

        public virtual short Id { get; protected set; }

        public virtual MemoryChipType ChipType { get; protected set; }

        public virtual MemoryModuleType ModuleType { get; protected set; }

        public virtual decimal CpuValueModifier { get; protected set; }

        public virtual decimal MotherboardValueFactor { get; protected set; }

        public virtual string Name
        {
            get { return this.ChipType.ToString().ToUpper(); }
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as MemoryType);
        }

        public virtual bool Equals(MemoryType other)
        {
            if (other == null)
                return false;

            return new { this.ModuleType, this.ChipType }.Equals(new { other.ModuleType, other.ChipType });
        }

        public override int GetHashCode()
        {
            return new { this.ModuleType, this.ChipType }.GetHashCode();
        }

        public override string ToString()
        {
            return $"{this.ChipType} {this.ModuleType}";
        }
    }
}