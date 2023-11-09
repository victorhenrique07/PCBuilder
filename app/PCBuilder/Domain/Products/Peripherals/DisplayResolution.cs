using System;

namespace PCBuilder.Domain.Products.Peripherals
{
    public class DisplayResolution : IEquatable<DisplayResolution>
    {
        protected DisplayResolution() { }

        public DisplayResolution(string name, int columns, int rows)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentOutOfRangeException(nameof(name));

            if (columns <= 0)
                throw new ArgumentOutOfRangeException(nameof(columns));

            if (rows <= 0)
                throw new ArgumentOutOfRangeException(nameof(rows));

            this.Name = name;
            this.Columns = columns;
            this.Rows = rows;
        }

        public virtual int Id { get; protected set; }

        public virtual string Name { get; protected set; }

        public virtual int Columns { get; protected set; }

        public virtual int Rows { get; protected set; }

        public virtual int Pixels { get { return this.Columns * this.Rows; } }

        public virtual string Description
        {
            get
            {
                return $"{this.Name} ({this.Columns} x {this.Rows})";
            }
        }

        public virtual bool Equals(DisplayResolution other)
        {
            if (other == null)
                return false;

            return new { this.Columns, this.Rows }.Equals(new { other.Columns, other.Rows });
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as DisplayResolution);
        }

        public override int GetHashCode()
        {
            return new { this.Columns, this.Rows }.GetHashCode();
        }

        public override string ToString()
        {
            return this.Description;
        }
    }
}
