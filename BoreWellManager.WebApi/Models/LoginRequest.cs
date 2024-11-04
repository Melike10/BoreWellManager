using System.ComponentModel.DataAnnotations;

namespace BoreWellManager.WebApi.Models
{
    public class LoginRequest
    {
        [Required]
        [StringLength(11)]
        [MaxLength(11)]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "Eksik veya fazla giriş yaptınız.")]
        public string TC { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
