using System;
using System.Text.RegularExpressions;

namespace PCBuilder.Domain.Products.Shared
{
    public class Gtin13
    {
        protected Gtin13() { }

        public Gtin13(string number)
        {
            if (!IsValid(number))
                throw new ArgumentOutOfRangeException(nameof(number));

            this.Number = number.PadLeft(13, '0');
        }

        public static bool IsValid(string number)
        {
            return number != null && Regex.IsMatch(number, "^[0-9]{7,9}-?[0-9]{4}$");
        }

        public virtual string Number { get; set; }

        public virtual bool Equals(Gtin13 other)
        {
            if (other == null)
                return false;

            return this.Number.Equals(other.Number);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Gtin13);
        }

        public override int GetHashCode()
        {
            return this.Number.GetHashCode();
        }

        public override string ToString()
        {
            return this.Number.Insert(1, " ").Insert(8, " ");
        }
    }
}
