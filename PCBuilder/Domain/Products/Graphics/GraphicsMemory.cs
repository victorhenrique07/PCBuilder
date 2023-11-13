using PCBuilder.Domain.Products.MotherboardAndMemory;
using System;

namespace PCBuilder.Domain.Products.Graphics
{
    public class GraphicsMemory : IEquatable<GraphicsMemory>
    {
        public virtual MemoryChipType Type { get; protected set; }

        public virtual decimal Capacity { get; protected set; }

        public decimal Frequency { get; protected set; }

        public virtual int BusWidth { get; protected set; }

        public virtual decimal Bandwidth { get { return Frequency * BusWidth / 8m; } }

        protected GraphicsMemory() { }

        public GraphicsMemory(MemoryChipType type, decimal capacity, decimal frequency, int busWidth)
        {
            this.Type = type;
            this.Capacity = capacity;
            this.Frequency = frequency;
            this.BusWidth = busWidth;
        }

        public virtual bool Equals(GraphicsMemory other)
        {
            if (other == null)
                return false;

            return new { this.Type, this.Capacity, this.Frequency, this.BusWidth }
                .Equals(new { other.Type, other.Capacity, other.Frequency, other.BusWidth });
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as GraphicsMemory);
        }

        public override int GetHashCode()
        {
            return new { this.Type, this.Capacity, this.Frequency, this.BusWidth }.GetHashCode();
        }

        public override string ToString()
        {
            return $"{this.Type} {this.Capacity} GB {this.Frequency} MHz {this.BusWidth} bits";
        }
    }
}
