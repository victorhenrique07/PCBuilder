namespace PCBuilder.Domain.Products.Shared
{
    public interface ISellable
    {
        int Id { get; }

        string Name { get; }

        ProductCategory Category { get; }

        int Units { get; }

        bool Comparable(int quantiy, ISellable otherSellable, int otherSellerQuantity, bool toMakeBuild,
            out bool highlyComparable);

        decimal EstimatedShippingCost(int quantity, bool asSecondaryPart);
    }
}
