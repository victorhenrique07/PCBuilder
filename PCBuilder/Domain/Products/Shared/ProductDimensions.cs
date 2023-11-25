using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace PCBuilder.Domain.Products.Shared
{
    public class ProductDimensions
    {
        protected ProductDimensions() { }

        public ProductDimensions(string text)
        {
            if (text == null)
                throw new ArgumentNullException(nameof(text));

            List<decimal> dimensions = text.Split('x').Select(d => decimal.Parse(d)).ToList();

            this.Length = dimensions[0];
            this.Width = dimensions[1];
            this.Height = dimensions[2];
        }

        public ProductDimensions(decimal length, decimal height, decimal width)
        {
            if (length <= 0)
                throw new ArgumentOutOfRangeException(nameof(length));

            if (width <= 0)
                throw new ArgumentOutOfRangeException(nameof(width));

            if (height <= 0)
                throw new ArgumentOutOfRangeException(nameof(height));

            this.Length = length;
            this.Width = width;
            this.Height = height;
        }


        public virtual decimal Length { get; protected set; }

        public virtual decimal Width { get; protected set; }

        public virtual decimal Height { get; protected set; }

        public virtual bool Equals(ProductDimensions other)
        {
            if (other == null)
                return false;

            return new { this.Length, this.Width, this.Height }.Equals(new { other.Length, other.Width, other.Height });
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as ProductDimensions);
        }

        public override int GetHashCode()
        {
            return new { this.Length, this.Height, this.Width }.GetHashCode();
        }

        public override string ToString()
        {
            return $"{this.Length} x {this.Width} x {this.Height}";
        }
    }
}
