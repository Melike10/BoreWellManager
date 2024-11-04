using System.ComponentModel.DataAnnotations;

namespace BoreWellManager.WebApi.Models
{
    public class UpdateInstitutionRequest
    {
        [Required]
        public string City { get; set; }
        [Required]
        public string Town { get; set; }
    }
}
