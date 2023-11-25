using System;

namespace PCBuilder.Domain.Products.Shared
{
    public interface IColored
    {
        ProductColor MainColor { get; }

        //Color SecondaryColor { get; }

        //string ColorsDescription { get; }

        void ChangeMainColor(ProductColor color);
    }
}