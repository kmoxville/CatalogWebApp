using CatalogWebApp.ViewModels.Catalog;

namespace CatalogWebApp.Services.CatalogService
{
    public interface ICatalogService
    {
        Task<CatalogIndexViewModel> IndexAsync();

        Task<byte[]?> GetImageAsync(int id);

        Task<CatalogViewModel> Details(int? id);

        Task<bool> Create(CatalogViewModel catalogModel);

        Task<bool> Save(CatalogViewModel catalogModel);

        Task<bool> Delete(int? id);
    }
}
