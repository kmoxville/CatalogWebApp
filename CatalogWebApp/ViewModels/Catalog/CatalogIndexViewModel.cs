namespace CatalogWebApp.ViewModels.Catalog
{
    public class CatalogIndexViewModel
    {
        public string Name = "Catalog";
        public IList<ProductModel> Products { get; set; } = new List<ProductModel>();
    }
}
