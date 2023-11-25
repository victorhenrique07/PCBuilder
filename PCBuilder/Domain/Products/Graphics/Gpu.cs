using PCBuilder.Domain.Products.Shared;
using System;

namespace PCBuilder.Domain.Products.Graphics
{
    public class Gpu : PCComponent, IEquatable<Gpu>
    {
        public static decimal GetGraphicsProviderRawQualityLevel(decimal performance, decimal baselinePerformance,
            ValueMeasurementContext<VideoCard> context)
        {
            if (performance == 0m)
            {
                return context.VideoCardMinQualityLevel;
            }

            /* 20% more performance => X% more value */
            double performanceExponent = Math.Log((double)context
                .VideoCardValueFactorFor20PercentMorePerformance, 1.20d);

            decimal rawLevel = (decimal)Math.Pow((double)(performance / baselinePerformance), performanceExponent) *
                context.BaselinePart.QualityLevel;

            rawLevel = Math.Max(rawLevel, context.VideoCardMinQualityLevel);

            return rawLevel;
        }

        protected Gpu() { }

        public Gpu(string name, Manufacturer manufacturer, int cores, string microarchitecture,
            string manufacturingProcess, decimal extraFeaturesValueFactor)
            : base(name, manufacturer)
        {
            this.Cores = cores;
            this.Microarchitecture = microarchitecture;
            this.ManufacturingProcess = manufacturingProcess;
            this.ExtraFeaturesValueFactor = extraFeaturesValueFactor;
        }

        public virtual int Id { get; set; }

        public virtual bool MadeToVideoCard { get; set; }

        public virtual int Cores { get; protected set; }

        public virtual string Microarchitecture { get; protected set; }

        public virtual string ManufacturingProcess { get; protected set; }

        public virtual decimal RayracingQualityLevel { get; protected set; }

        public virtual bool HasQuickSync { get; protected set; }

        public virtual bool HasRayTracing { get { return this.RayracingQualityLevel > 0m; } }

        public virtual decimal DlssQualityLevel { get; protected set; }

        public virtual bool HasDlss { get { return this.DlssQualityLevel > 0m; } }

        public virtual decimal FsrQualityLevel { get; protected set; }

        public virtual bool HasFsr { get { return this.FsrQualityLevel > 0m; } }

        public virtual bool HasSomeSs { get { return this.HasDlss || this.HasFsr; } }

        public virtual decimal EncoderQualityLevel { get; protected set; }

        public virtual bool HasHighQualityEncoder { get { return this.EncoderQualityLevel > 0m; } }

        public virtual decimal ExtraFeaturesValueFactor { get; protected set; }

        public virtual decimal VideoEditingPerformanceFactor { get; protected set; }

        public override string Description
        {
            get
            {
                return $"{this.Manufacturer} {this.Name}";
            }
        }

        public virtual bool SameExtraTecnologies(Gpu other)
        {
            if (other == null)
                return false;

            return this.HasRayTracing == other.HasRayTracing && this.HasDlss == other.HasDlss;
        }

        public virtual bool SameOrMoreExtraTecnologies(Gpu other)
        {
            if (other == null)
                return false;

            return (this.HasRayTracing || !other.HasRayTracing) &&
                (this.HasDlss || !other.HasDlss) &&
                (this.HasHighQualityEncoder || !other.HasHighQualityEncoder);
        }

        public virtual bool Equals(Gpu other)
        {
            if (other == null)
                return false;

            return this.Name.Equals(other.Name);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Gpu);
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }
    }
}