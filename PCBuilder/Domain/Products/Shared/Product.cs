using PCBuilder.Application.Shared;
using PCBuilder.Domain.Price;
using PCBuilder.Domain.Products.Cpus;
using PCBuilder.Domain.Products.Graphics;
using PCBuilder.Domain.Products.Peripherals;
using PCBuilder.Domain.Recommendations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PCBuilder.Domain.Products.Shared
{
    public abstract class Product : ISellable
    {
        public const decimal DolarsPerQualityLevel = 5m;

        public const decimal ExtraPartEstimatedShippingCostFactor = 1m / 2m;

        private readonly ISet<ProductIdentification> alternativeIdentifications;

        protected Product() { }

        protected Product(ProductIdentification identification, Manufacturer manufacturer, string serie, string name,
            string details, ProductCategory category, decimal powerConsumption)
        {
            VerifyIdentification(identification);

            if (manufacturer == null)
                throw new ArgumentNullException(nameof(manufacturer));

            this.Identification = identification;
            this.Manufacturer = manufacturer;
            this.Serie = serie;
            this.Name = name;
            this.Details = details;
            this.BaseProduct = new BaseProduct(identification.PartNumber);
            this.Category = category;
            this.PowerConsumption = powerConsumption;
            this.IsForSale = true;
            this.EndOfLine = false;
            this.Enabled = true;
            this.RegistrationDate = DefaultTimeZoneDateTime.Now;

            this.alternativeIdentifications = new HashSet<ProductIdentification>();

            this.WarrantyTime = 1m;
        }

        private static void VerifyIdentification(ProductIdentification identification)
        {
            if (identification == null)
                throw new ArgumentNullException(nameof(identification));

            if (identification.PartNumber == null)
                throw new ArgumentNullException(nameof(identification));

            if (string.IsNullOrWhiteSpace(identification.PartNumber))
                throw new ArgumentOutOfRangeException(nameof(identification));
        }

        public virtual int Id { get; protected set; }

        public virtual ProductIdentification Identification { get; protected set; }

        public virtual Manufacturer Manufacturer { get; protected set; }

        public virtual string Serie { get; set; }

        public virtual string Name { get; protected set; }

        public virtual string Details { get; set; }

        public virtual BaseProduct BaseProduct { get; protected set; }

        public virtual ProductCategory Category { get; protected set; }

        public virtual decimal PowerConsumption { get; set; }

        public virtual decimal WarrantyTime { get; set; }

        public virtual bool IsForSale { get; protected set; }

        public virtual bool EndOfLine { get; protected set; }

        public virtual bool Enabled { get; protected set; }

        public virtual DateTime RegistrationDate { get; protected set; }

        public virtual bool IsNew { get { return this.RegistrationDate >= DefaultTimeZoneDateTime.Now.AddDays(-90); } }

        public virtual string PartNumber { get { return this.Identification.PartNumber; } }

        public virtual BaseProduct UpperBaseProduct
        {
            get { return this.BaseProduct.ParentBaseProduct ?? this.BaseProduct; }
        }

        public virtual IEnumerable<ProductIdentification> AlternativeIdentifications
        {
            get { return this.alternativeIdentifications; }
        }

        public virtual bool Oem
        {
            get
            {
                return this.Identification.PartNumber.EndsWith("MPK") ||
                    this.Identification.PartNumber.EndsWith("TRAY") ||
                    this.Name.EndsWith("OEM");
            }
        }

        public virtual string ShortestDescription
        {
            get
            {
                string result = this.Name;

                if (!string.IsNullOrWhiteSpace(this.Serie) && !this.Name.ToUpper().Contains(this.Serie.ToUpper()))
                    result = $"{this.Serie} {result}";

                return result;
            }
        }

        public virtual string ShortDescription
        {
            get
            {
                string result = this.ShortestDescription;

                if (!string.IsNullOrWhiteSpace(this.Manufacturer.Name))
                    result = $"{this.Manufacturer} {result}";

                return result;
            }
        }

        public virtual string Description
        {
            get
            {
                string result = this.ShortDescription;

                if (!string.IsNullOrWhiteSpace(this.Details))
                     result = $"{result}, {this.Details}";

                return result;
            }
        }

        public virtual string LongDescription
        {
            get
            {
                return $"{this.Category.LocalName} {this.Description}";
            }
        }

        public virtual string FullDescription
        {
            get
            {
                string result = this.LongDescription;

                if (!result.ToUpper().Contains(this.Identification.PartNumber.ToUpper()))
                    result = $"{result} - {this.Identification.PartNumber}";

                return result;
            }
        }

        public virtual int Units { get { return 1; } }

        public virtual bool HasPartNumber(string partNumber)
        {
            if (string.IsNullOrWhiteSpace(partNumber))
                return false;

            string upperPartNumber = partNumber.ToUpper();

            if (this.PartNumber.ToUpper() == upperPartNumber)
                return true;

            return this.alternativeIdentifications
                .Where(i => i.PartNumber != null)
                .Select(i => i.PartNumber.ToUpper())
                .Contains(upperPartNumber);
        }

        public virtual bool IdentifiedByGtin(Gtin13 gtin)
        {
            if (gtin == null)
                return false;

            var gtins = new List<Gtin13>();

            if (this.Identification.Gtin != null)
                gtins.Add(this.Identification.Gtin);

            gtins.AddRange(this.alternativeIdentifications.Where(i => i.Gtin != null).Select(i => i.Gtin));

            return gtins.Contains(gtin);
        }


        public virtual void ChangeIdentification(ProductIdentification identification)
        {
            VerifyIdentification(identification);
            this.Identification = identification;
        }

        public virtual void AddAlternativeIdentification(ProductIdentification identification)
        {
            if (identification == null)
                throw new ArgumentNullException(nameof(identification));

            if (identification.PartNumber != null && (this.Identification.PartNumber == identification.PartNumber ||
                alternativeIdentifications.Any(i => i.PartNumber == identification.PartNumber)))
            {
                throw new ArgumentOutOfRangeException(nameof(identification));
            }

            if (this.IdentifiedByGtin(identification.Gtin))
            {
                throw new ArgumentOutOfRangeException(nameof(identification));
            }

            this.alternativeIdentifications.Add(identification);
        }

        public virtual void ChangeManufacturer(Manufacturer manufacturer)
        {
            if (manufacturer == null)
                throw new ArgumentNullException(nameof(manufacturer));

            this.Manufacturer = manufacturer;
        }

        public virtual void ChangeName(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            this.Name = name;
        }

        public virtual decimal EstimatedShippingCost(int quantity, bool asSecondaryPart)
        {
            int totalUnits = this.Units * quantity;

            if (asSecondaryPart)
                return this.Category.EstimatedShippingCost * totalUnits * ExtraPartEstimatedShippingCostFactor;
            
            return this.Category.EstimatedShippingCost + this.Category.EstimatedShippingCost *
                (totalUnits - 1) * ExtraPartEstimatedShippingCostFactor;
        }

        public abstract bool IsSuppliedByPowerSupply { get; }

        public virtual decimal PeakPowerConsumption { get { return this.PowerConsumption; } }

        public virtual decimal GetPowerConsumption(WorkloadLevel workloadLevel)
        {
            return this.PowerConsumption;
        }

        public abstract decimal GetDefaultValue(BuildSpecification buildSpecification, BuildGeneralParams generalParams);

        public virtual decimal GetWarrantyExtraValueFactor(Store store, bool openBox)
        {
            var warrantyValueFactorByTime = new Dictionary<decimal, decimal>
            {
                { 0.00m, -0.0900m },
                { 0.25m, -0.0600m },
                { 0.50m, -0.0300m },
                { 0.75m, -0.0150m },
                { 1.00m,  0.0000m },
                { 1.25m,  0.0075m },
                { 1.50m,  0.0150m },
                { 1.75m,  0.0225m },
                { 2.00m,  0.0300m },
                { 2.50m,  0.0400m },
                { 3.00m,  0.0500m },
                { 3.50m,  0.0550m },
                { 4.00m,  0.0600m },
                { 4.50m,  0.0625m },
                { 5.00m,  0.0650m },
            };

            decimal? storeWarrantyTime = openBox ? 0.25m : store?.SpecificWarranty(this.Manufacturer, this.Category);

            decimal adjustedWarrantyTime;

            if (storeWarrantyTime.HasValue)
            {
                adjustedWarrantyTime = storeWarrantyTime.Value;
            }
            else
            {
                adjustedWarrantyTime = this.WarrantyTime;

                if (this is Cpu)
                    adjustedWarrantyTime += this.Manufacturer.CpuWarrantyQualityModifier;

                if (this is VideoCard)
                    adjustedWarrantyTime += this.Manufacturer.VideoCardWarrantyQualityModifier;

                if (this is Monitor)
                    adjustedWarrantyTime += this.Manufacturer.MonitorWarrantyQualityModifier;
            }

            if (adjustedWarrantyTime < warrantyValueFactorByTime.Keys.Min())
                return -0.5m;

            if (!warrantyValueFactorByTime.ContainsKey(adjustedWarrantyTime))
                return warrantyValueFactorByTime.Values.Max();

            return warrantyValueFactorByTime[adjustedWarrantyTime];
        }

        public static decimal RoundQualityLevel(decimal rawQualityLevel)
        {
            return Math.Round(rawQualityLevel, 1);
        }

        public static decimal RoundValue(decimal rawValue)
        {
            return RoundQualityLevel(rawValue / DolarsPerQualityLevel) * DolarsPerQualityLevel;
        }

        public virtual void SetGtin(Gtin13 gtin)
        {
            if (gtin == null)
                throw new ArgumentNullException(nameof(gtin));

            if (this.Identification.Gtin != null)
                throw new InvalidOperationException();

            this.Identification = new ProductIdentification(this.Identification.PartNumber, gtin);
        }

        public abstract bool Comparable(int quantiy, ISellable otherSellable, int otherSellerQuantity, bool toMakeBuild, out bool highlyComparable);

        public virtual bool Equals(Product other)
        {
            if (other == null)
                return false;

            return this.Identification.PartNumber.Equals(other.Identification.PartNumber);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Product);
        }

        public override int GetHashCode()
        {
            return this.Identification.PartNumber.GetHashCode();
        }

        public override string ToString()
        {
            return this.LongDescription;
        }
    }
}
