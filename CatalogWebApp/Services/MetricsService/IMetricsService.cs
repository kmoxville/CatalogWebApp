namespace CatalogWebApp.Services.MetricsService
{
    public interface IMetricsService
    {
        Task AddVisit(string controllerName);

        Task<IEnumerable<MetricModel>> GetAll();
    }
}
