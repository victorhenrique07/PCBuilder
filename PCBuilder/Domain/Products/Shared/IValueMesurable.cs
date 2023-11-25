using PCBuilder.Domain.Price;
using PCBuilder.Domain.Products.Cpus;

namespace PCBuilder.Domain.Products.Shared
{
    public interface IValueMesurable<T> where T : Product
    {
        decimal GetQualityLevel(ValueMeasurementContext<T> context);

        decimal GetCoreValue(ValueMeasurementContext<T> context);

        decimal GetAestheticsValue(ValueMeasurementContext<T> context);

        decimal GetExtraFeaturesValue(ValueMeasurementContext<T> context, Store store, bool openBox);

        decimal GetValue(ValueMeasurementContext<T> context, Store store, bool openBox);
    }
}