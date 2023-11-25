using PCBuilder.Domain.Products.Shared;

namespace PCBuilder.Domain.Products.Graphics
{
    public interface IGraphicsProvider
    {
        decimal GetGamingPerformance(ValueMeasurementContext<VideoCard> context, decimal pcieVersion);

        decimal GetVideoEditingPerformance(ValueMeasurementContext<VideoCard> context, decimal pcieVersion);

        decimal GetGraphicsWeightedPerformance(ValueMeasurementContext<VideoCard> context, decimal pcieVersion);
    }
}