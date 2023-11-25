using Jint.Parser;
using PCBuilder.Domain.Products.Shared;
using PCBuilder.Domain.Recommendations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PCBuilder.Domain.Price
{
    public class Store : IEquatable<Store>
    {
        public static string ExtractShortName(string storeReference)
        {
            string referenceWihoutWww = storeReference.StartsWith("https://") ? storeReference.Remove(0, 8) : storeReference;
            referenceWihoutWww = referenceWihoutWww.StartsWith("http://") ? referenceWihoutWww.Remove(0, 7) : referenceWihoutWww;
            referenceWihoutWww = referenceWihoutWww.StartsWith("www.") ? referenceWihoutWww.Remove(0, 4) : referenceWihoutWww;
            referenceWihoutWww = referenceWihoutWww.StartsWith("s.click.") ? referenceWihoutWww.Remove(0, 8) : referenceWihoutWww;

            return referenceWihoutWww.Remove(referenceWihoutWww.IndexOf("."));
        }

        private ISet<StoreWarranty> specificWarranties;

        protected Store() { }

        public Store(string shortName, string name, string website)
        {   
            this.ShortName = shortName;
            this.Name = name;
            this.Website = website;
        }

        public virtual int Id { get; set; }

        public virtual string ShortName { get; protected set; }

        public virtual string Name { get; protected set; }

        public virtual string Website { get; protected set; }

        public virtual string CountryCode { get; protected set; }

        public virtual bool HasExternalIds { get; protected set; }

        public virtual bool AppendsTargetMediaToAffiliateUrl { get; protected set; }

        public virtual StoreAvailability Availability { get; protected set; }

        //public virtual int SizeTier { get; protected set; }

        public virtual string GoogleMerchantXmlUrl { get; protected set; }

        public virtual bool RequiresFewInstallmentsPrice { get; protected set; }

        public virtual bool RequiresManyInstallmentsPrice { get; protected set; }

        public virtual AffiliateReferenceGenerator AffiliateReferenceGenerator { get; protected set; }

        public virtual bool HasMarketplace { get; protected set; }

        public virtual bool ChargesExtraTaxes { get; protected set; }

        public virtual string FreeShippingDetails { get; protected set; }

        public virtual IEnumerable<StoreWarranty> SpecificWarranties { get { return specificWarranties; } }
        
        public virtual decimal? MinInstallmentAmount { get; protected set; }

        public virtual int? MaxInstallmentMonths { get; protected set; }

        public virtual bool AllowsMarketplaceSellerAnnulment { get { return this.ShortName != "magazinevoce"; } }

        public virtual bool PricesFeedContainsOnlyValidPrices
        {
            get { return new List<string> { "girafa", "fastshgop", "gigantec" }.Contains(this.ShortName); }
        }

        public virtual bool BadAtReadingPricesDirectly
        {
            get
            {
                return new List<string>
                {
                    "casasbahia",
                    "extra",
                    "pontofrio",
                    "carrefour",
                    "girafa",
                    "fastshop",
                    "waz",
                    "terabyteshop",
                    "pichau"
                }.Contains(this.ShortName);
            }
        }

        public virtual bool EasyToResolverExternalIds
        {
            get
            {
                return new List<string>
                {
                    "kabum",
                    "terabyteshop"
                }.Contains(this.ShortName);
            }
        }

        public virtual bool HasFixedShippingCost { get { return this.ShortName == "terabyteshop"; } }

        public virtual decimal? FixedShippingCost
        {
            get { return this.HasFixedShippingCost ? 1.5m : throw new InvalidOperationException(); }
        }

        public virtual int InstallmentMonths(decimal installmentPrice)
        {
            if (!this.MinInstallmentAmount.HasValue || !this.MaxInstallmentMonths.HasValue)
                throw new InvalidOperationException();

            return Math.Min((int)Math.Floor(installmentPrice / this.MinInstallmentAmount.Value),
                this.MaxInstallmentMonths.Value);
        }

        public virtual decimal? SpecificWarranty(Manufacturer manufacturer, ProductCategory productCategory)
        {
            StoreWarranty warranty = specificWarranties
                .Where(w => w.Manufacturer.Equals(manufacturer))
                .Where(w => w.ProductCategory == null || w.ProductCategory.Equals(productCategory))
                .SingleOrDefault();

            return warranty?.WarrantyTime;
        }

        public virtual bool Equals(Store other)
        {
            if (other == null)
                return false;

            return this.ShortName.Equals(other.ShortName);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Store);
        }

        public override int GetHashCode()
        {
            return this.ShortName.GetHashCode();
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}