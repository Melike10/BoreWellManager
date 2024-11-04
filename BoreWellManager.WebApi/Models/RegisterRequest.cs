using BoreWellManager.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace BoreWellManager.WebApi.Models
{
    public class RegisterRequest
    {
        [Required]
        [StringLength(11)]
        [MaxLength(11)]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "Eksik veya fazla giriş yaptınız.")]
        public string TC { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [NumberWithSpacesValidation(ErrorMessage = "Yalnızca sayılar ve boşluklara izin verilir.")]
        public string Phone { get; set; }
        [Required]
        public string Adress { get; set; }
       
        [Required]
        public bool IsResponsible { get; set; }
        [Required]
        public UserType UserType { get; set; }
    }
}
