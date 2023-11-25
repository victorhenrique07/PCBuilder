using System;

namespace PCBuilder.Domain.Products.Shared
{
    public class BaseProduct : IEquatable<BaseProduct>
    {
        protected BaseProduct() { }

        public BaseProduct(string description)
        {
            this.Description = description;
        }

        public virtual string Description { get; protected set; }

        public virtual BaseProduct ParentBaseProduct
        {
            get
            {
                if (!this.Description.Contains("."))
                    return null;

                return new BaseProduct(this.Description.Substring(0, this.Description.IndexOf(".")));
            }
        }

        public virtual bool Equals(BaseProduct other)
        {
            if (other == null)
                return false;

            return this.Description.Equals(other.Description);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as BaseProduct);
        }

        public override int GetHashCode()
        {
            return this.Description.GetHashCode();
        }

        public override string ToString()
        {
            return this.Description;
        }
    }
}
