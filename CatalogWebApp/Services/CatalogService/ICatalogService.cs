using CatalogWebApp.ViewModels.Catalog;

namespace CatalogWebApp.Services.CatalogService
{
    public interface ICatalogService
    {
        Task<CatalogIndexViewModel> IndexAsync(CancellationToken cancellationToken);

        Task<byte[]?> GetImageAsync(int id, CancellationToken cancellationToken);

        Task<CatalogViewModel> Details(int? id, CancellationToken cancellationToken);

        Task<bool> Create(CatalogViewModel catalogModel, CancellationToken cancellationToken);

        Task<bool> Save(CatalogViewModel catalogModel, CancellationToken cancellationToken);

        Task<bool> Delete(int? id, CancellationToken cancellationToken);
    }
}
