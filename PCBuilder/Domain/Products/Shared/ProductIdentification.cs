using System;

namespace PCBuilder.Domain.Products.Shared
{
    public class ProductIdentification
    {
        protected ProductIdentification() { }

        public ProductIdentification(string partNumber, Gtin13 gtin)
        {
            if (partNumber == null)
            {
                if (gtin == null)
                    throw new ArgumentNullException(nameof(partNumber));
            }
            else if (string.IsNullOrWhiteSpace(partNumber))
            {
                throw new ArgumentOutOfRangeException(nameof(partNumber));
            }

            this.PartNumber = partNumber;
            this.Gtin = gtin;
        }

        public virtual string PartNumber { get; set; }

        public virtual Gtin13 Gtin { get; set; }

        public virtual bool Equals(ProductIdentification other)
        {
            if (other == null)
                return false;

            return this.PartNumber.Equals(other.PartNumber);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as ProductIdentification);
        }

        public override int GetHashCode()
        {
            return this.PartNumber.GetHashCode();
        }

        public override string ToString()
        {
            return this.PartNumber;
        }
    }
}
