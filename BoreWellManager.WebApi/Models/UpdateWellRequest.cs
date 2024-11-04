using BoreWellManager.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace BoreWellManager.WebApi.Models
{
    public class UpdateWellRequest
    {
        [Required]
        [NumberWithSpacesValidation(ErrorMessage = "Yalnızca sayılar ve boşluklara izin verilir.")]

        public string XCordinat { get; set; }
        [Required]
        [NumberWithSpacesValidation(ErrorMessage = "Yalnızca sayılar ve boşluklara izin verilir.")]
        public string YCordinat { get; set; }
        public int UserId { get; set; }
        public int LandId { get; set; }
        public decimal Debi { get; set; }
        public decimal StaticLevel { get; set; }
        public decimal DynamicLevel { get; set; }
    }
}
