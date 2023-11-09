using System;

namespace PCBuilder.Domain.Products.Peripherals
{
    public class Display : IEquatable<Display>
    {
        protected Display() { }

        public Display(decimal size, DisplayResolution resolution, PanelType panelType, short refreshRate)
        {
            if (size <= 0m)
                throw new ArgumentOutOfRangeException(nameof(size));

            if (resolution == null)
                throw new ArgumentNullException(nameof(resolution));

            if (!Enum.IsDefined(typeof(PanelType), panelType))
                throw new ArgumentOutOfRangeException(nameof(panelType));

            if (refreshRate <= 0)
                throw new ArgumentOutOfRangeException(nameof(refreshRate));

            this.Size = size;
            this.Resolution = resolution;
            this.PanelType = panelType;
            this.RefreshRate = refreshRate;
        }

        public virtual decimal Size { get; protected set; }

        public virtual DisplayResolution Resolution { get; protected set; }

        public virtual PanelType PanelType { get; protected set; }

        public virtual short RefreshRate { get; protected set; }

        public virtual decimal PixelsPerInch { get { return this.Resolution.Rows / this.Height; } }

        public virtual decimal AspectRatio { get { return (decimal)this.Resolution.Columns / this.Resolution.Rows; } }
        
        public virtual decimal Height
        {
            get
            {
                return this.Size / (decimal)Math.Sqrt(Math.Pow((double)this.AspectRatio, 2.0) + 1);
            }
        }

        public virtual decimal Width { get { return this.AspectRatio * this.Height; } }

        public virtual decimal Area { get { return this.Height * this.Width; } }

        public virtual bool Equals(Display other)
        {
            if (other == null)
                return false;

            return new { this.Size, this.Resolution, this.PanelType, this.RefreshRate }
                .Equals(new { other.Size, other.Resolution, other.PanelType, other.RefreshRate });
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Display);
        }

        public override int GetHashCode()
        {
            return new { this.Size, this.Resolution, this.PanelType, this.RefreshRate }.GetHashCode();
        }

        public override string ToString()
        {
            return $"{this.Size} {this.Resolution}";
        }
    }
}
