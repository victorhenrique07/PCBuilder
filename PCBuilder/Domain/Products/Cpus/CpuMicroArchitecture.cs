using PCBuilder.Domain.Products.Shared;
using System;

namespace PCBuilder.Domain.Products.Cpus
{
    public class CpuMicroarchitecture : IEquatable<CpuMicroarchitecture>
    {
        protected virtual int Id { get; set; }

        public virtual string Codename { get; protected set; }

        public virtual Manufacturer Manufacturer { get; protected set; }

        public virtual string ManufacturingProcess { get; protected set; }

        protected CpuMicroarchitecture() { }

        public CpuMicroarchitecture(string codename, Manufacturer manufacturer, string manufacturingProcess)
        {
            if (manufacturer == null)
                throw new ArgumentNullException(nameof(manufacturer));

            if (string.IsNullOrWhiteSpace(codename))
                throw new ArgumentNullException(nameof(codename));

            if (string.IsNullOrWhiteSpace(manufacturingProcess))
                throw new ArgumentNullException(nameof(manufacturingProcess));

            this.Codename = codename;
            this.Manufacturer = manufacturer;
            this.ManufacturingProcess = manufacturingProcess;
        }

        public virtual bool Equals(CpuMicroarchitecture other)
        {
            if (other == null)
                return false;

            return this.Codename.Equals(other.Codename);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as CpuMicroarchitecture);
        }

        public override int GetHashCode()
        {
            return this.Codename.GetHashCode();
        }

        public override string ToString()
        {
            return this.Codename;
        }
    }
}