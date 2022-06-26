namespace CatalogWebApp.ViewModels.Catalog
{
    public class CatalogViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int ImageId { get; set; }

        public ProductImageModel Image { get; set; } = default!;

        public IFormFile? ImageFile { get; set; }
    }
}
