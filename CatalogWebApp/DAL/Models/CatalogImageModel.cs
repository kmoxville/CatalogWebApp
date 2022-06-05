namespace CatalogWebApp.DAL.Models
{
    public class CatalogImageModel
    {
        public int Id { get; set; }

        public byte[] Image { get; set; } = null!;
    }
}
