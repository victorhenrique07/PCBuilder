using System;

namespace PCBuilder.Domain.Products.Shared
{
    public class Range<T> : IEquatable<Range<T>> where T : IComparable<T>
    {
        public T Begin { get; private set; }

        public T End { get; private set; }

        public Range(T begin, T end)
        {
            /* TODO: validations */

            this.Begin = begin;
            this.End = end;
        }

        public bool Contains(T value)
        {
            return Begin.CompareTo(value) <= 0 && End.CompareTo(value) >= 0;
        }

        public bool Equals(Range<T> other)
        {
            if (other == null)
                return false;

            return new { this.Begin, this.End }
                .Equals(new { other.Begin, other.End });
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Range<T>);
        }

        public override int GetHashCode()
        {
            return new { this.Begin, this.End }.GetHashCode();
        }
    }
}
