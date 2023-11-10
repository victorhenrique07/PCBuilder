using PCBuilder.Domain.Products.Cpus;
using System;

namespace PCBuilder.Domain.Products.MotherboardAndMemory
{
    public class SupportedCpuLine : IEquatable<SupportedCpuLine>
    {
        protected virtual int Id { get; set; }

        public virtual CpuLine Line { get; protected set; }

        public virtual bool NeedsBiosUpdate { get; protected set; }

        protected SupportedCpuLine() { }

        public SupportedCpuLine(CpuLine line, bool needsBiosUpdate)
        {
            this.Line = line;
            this.NeedsBiosUpdate = needsBiosUpdate;
        }

        public virtual bool Equals(SupportedCpuLine other)
        {
            if (this == null)
                return false;

            return this.Line.Equals(other.Line);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as SupportedCpuLine);
        }

        public override int GetHashCode()
        {
            return this.Line.GetHashCode();
        }

        public override string ToString()
        {
            return this.Line.ToString();
        }
    }
}