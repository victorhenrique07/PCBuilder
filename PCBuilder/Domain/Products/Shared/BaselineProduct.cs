namespace PCBuilder.Domain.Products.Shared
{
    public class BaselineProduct<T> where T : Product
    {
        public BaselineProduct(T part, decimal qualityLevel)
        {
            this.Product = part;
            this.QualityLevel = qualityLevel;
        }

        public T Product { get; }

        public decimal QualityLevel { get; }
    }
}