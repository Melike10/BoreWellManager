using System.ComponentModel.DataAnnotations;

namespace BoreWellManager.WebApi.Models
{
    public class InstitutesRequest
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Town { get; set; }
    }
}
