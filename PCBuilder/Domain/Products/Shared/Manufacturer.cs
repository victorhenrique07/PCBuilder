namespace PCBuilder.Domain.Products.Shared
{
    public class Manufacturer
    {
        protected Manufacturer() { }

        public Manufacturer(string name, string website, string twitterProfile, string facebookProfile,
            string instagramProfile, decimal warrantyQualityModifier, decimal? motherboardDefaultWarranty,
            decimal? motherboardDefaultBiosValueFactor, decimal? memoryDefaultOverallQualityFactor,
            decimal? videoCardDefaultWarranty)
        {
            this.Name = name;
            this.Website = website;
            this.TwitterProfile = twitterProfile;
            this.FacebookProfile = facebookProfile;
            this.InstagramProfile = instagramProfile;
            this.VideoCardWarrantyQualityModifier = warrantyQualityModifier;
            this.MotherboardDefaultWarranty = motherboardDefaultWarranty;
            this.MotherboardDefaultBiosValueFactor = motherboardDefaultBiosValueFactor;
            this.MemoryDefaultOverallQualityFactor = memoryDefaultOverallQualityFactor;
            this.VideoCardDefaultWarranty = videoCardDefaultWarranty;
        }

        public virtual int Id { get; protected set; }

        public virtual string Name { get; protected set; }

        public virtual string Website { get; protected set; }

        public virtual string TwitterProfile { get; protected set; }

        public virtual string FacebookProfile { get; protected set; }

        public virtual string InstagramProfile { get; protected set; }

        public virtual decimal CpuWarrantyQualityModifier { get { return 0m; } }

        public virtual decimal VideoCardWarrantyQualityModifier { get; protected set; }

        public virtual decimal MonitorWarrantyQualityModifier { get { return 0m; } }

        public virtual decimal? MotherboardDefaultWarranty { get; protected set; }

        public virtual decimal? MotherboardDefaultBiosValueFactor { get; protected set; }

        public virtual decimal? MemoryDefaultOverallQualityFactor { get; protected set; }

        public virtual decimal? VideoCardDefaultWarranty { get; protected set; }

        public virtual decimal? MonitorDefaultBuildQualityValueFactor { get; protected set; }

        public virtual bool Equals(Manufacturer other)
        {
            if (other == null)
                return false;

            return this.Name.Equals(other.Name);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Manufacturer);
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
