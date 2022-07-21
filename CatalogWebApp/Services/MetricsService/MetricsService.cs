namespace CatalogWebApp.Services.MetricsService
{
    public class MetricsService : IMetricsService
    {
        private readonly CatalogContext _context;

        public MetricsService(CatalogContext context)
        {
            _context = context;
        }

        public async Task AddVisit(string controllerName)
        {
            var metric = await _context.Metrics.FirstOrDefaultAsync(x => x.ControllerName == controllerName);
            if (metric == null)
            {
                metric = new MetricModel();
                _context.Metrics.Add(metric);
            }

            metric.ControllerName = controllerName;
            metric.VisitCounts++;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                // ignore
            }
            
        }

        public async Task<IEnumerable<MetricModel>> GetAll()
        {
            return await _context.Metrics.ToListAsync();
        }
    }
}
