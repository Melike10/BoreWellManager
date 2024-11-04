using BoreWellManager.Data.Entitites;
using BoreWellManager.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace BoreWellManager.WebApi.Models
{
    public class LandRequest
    {
        [Required]
        public string City { get; set; }
        [Required]
        public string Town { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        [MaxLength(6)]
        public string Block { get; set; } // ada
        [Required]
        [MaxLength(6)]
        public string Plot { get; set; }//parsel
    
        public string? Location { get; set; }//mevki
        [Required]
        public string LandType { get; set; }
        [Required]
        public bool HasLien { get; set; } = false;//şerh,irtifak,beyan var mı?
        [Required]
        public bool IsCksRequired { get; set; } = false;//ÇKS GEREKLİ Mİ
        public LienType? LienType { get; set; }

        public List<int> UserIds { get; set; }
    }
}
