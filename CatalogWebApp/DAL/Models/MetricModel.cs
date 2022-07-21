using System.ComponentModel.DataAnnotations;

namespace CatalogWebApp.DAL.Models
{
    public class MetricModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(256)]
        public string ControllerName { get; set; } = string.Empty;

        [Required]
        public int VisitCounts { get; set; }
    }
}
