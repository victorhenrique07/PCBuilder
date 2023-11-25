using PCBuilder.Domain.Products.Shared;
using System;

namespace PCBuilder.Domain.Price
{
    public class StoreWarranty : IEquatable<StoreWarranty>
    {
        protected StoreWarranty() { }

        public StoreWarranty(string marketplaceSellerName, Manufacturer manufacturer, ProductCategory productCategory, decimal warrantyTime)
        {
            if (manufacturer == null)
                throw new ArgumentNullException(nameof(manufacturer));

            this.MarketplaceSellerName = marketplaceSellerName;
            this.Manufacturer = manufacturer;
            this.ProductCategory = productCategory;
            this.WarrantyTime = warrantyTime;
        }

        protected virtual int Id { get; set; }

        public virtual string MarketplaceSellerName { get; protected set; }

        public virtual Manufacturer Manufacturer { get; protected set; }

        public virtual ProductCategory ProductCategory { get; protected set; }

        public virtual decimal WarrantyTime { get; protected set; }

        public virtual bool Equals(StoreWarranty other)
        {
            if (other == null)
                return false;

            return new { this.Manufacturer, this.ProductCategory }
                .Equals(new { other.Manufacturer, other.ProductCategory });
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as MarketplaceSeller);
        }

        public override int GetHashCode()
        {
            return new { this.Manufacturer, this.ProductCategory }.GetHashCode();
        }

        public override string ToString()
        {
            if (this.ProductCategory != null)
                return $"{this.Manufacturer} - {this.ProductCategory}";
            else
                return this.Manufacturer.ToString();
        }
    }
}
