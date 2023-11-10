using PCBuilder.Domain.Products.MotherboardAndMemory;
using PCBuilder.Domain.Products.Shared;
using System;

namespace PCBuilder.Domain.Products.Cpus
{
    public class CpuSocket : IEquatable<CpuSocket>
    {
        public virtual int Id { get; protected set; }

        public virtual string Name { get; protected set; }

        public virtual Manufacturer Manufacturer { get; protected set; }

        public virtual MemoryType RecommendedMemoryType { get; protected set; }

        public virtual decimal CpuValueFactor { get; protected set; }

        public virtual decimal CpuCoresTypicalCurrent { get; protected set; }

        public virtual decimal CpuCoresMaxCurrent { get; protected set; }

        public virtual string Description { get { return $"{this.Manufacturer.Name} {this.Name}"; } }

        public virtual bool EndOfLine { get; protected set; }

        protected CpuSocket() { }

        public CpuSocket(string name, Manufacturer manufacturer)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            if (manufacturer == null)
                throw new ArgumentNullException(nameof(manufacturer));

            this.Name = name;
            this.Manufacturer = manufacturer;
            this.EndOfLine = false;
        }

        public virtual bool Equals(CpuSocket other)
        {
            if (other == null)
                return false;

            return this.Name.Equals(other.Name);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as CpuSocket);
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }

        public override string ToString()
        {
            return $"{this.Manufacturer} {this.Name}";
        }
    }
}