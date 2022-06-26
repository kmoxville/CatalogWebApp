namespace CatalogWebApp.DAL.Models
{
    public class ProductImageModel
    {
        public int Id { get; set; }

        public int ProductModelId { get; set; }

        public byte[] Image { get; set; } = null!;
    }
}
