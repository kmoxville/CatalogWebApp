using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogWebApp.DAL.Models
{
    public class CatalogModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(256)]
        public string Name { get; set; } = string.Empty;

        [ForeignKey(nameof(Image))]
        public int ImageId { get; set; }

        [Required]
        public CatalogImageModel Image { get; set; } = default!;

        [NotMapped]
        public IFormFile ImageFile { get; set; } = default!;
    }
}
