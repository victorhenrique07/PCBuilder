using PCBuilder.Domain.Products.Shared;
using System;

namespace PCBuilder.Domain.Products.Shared
{
    public abstract class PCComponent
    {
        public virtual Manufacturer Manufacturer { get; protected set; }

        public virtual string Name { get; protected set; }

        public virtual string Description
        {
            get
            {
                if (this.Manufacturer != null)
                    return $"{this.Manufacturer} {this.Name}";
                else
                    return this.Name;
            }
        }

        protected PCComponent() { }

        protected PCComponent(string name, Manufacturer manufacturer)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Invalid name.", nameof(name));

            if (manufacturer == null)
                throw new ArgumentNullException(nameof(manufacturer));

            this.Name = name;
            this.Manufacturer = manufacturer;
        }
        
        public override string ToString()
        {
            return this.Description;
        }
    }
}
