using BoreWellManager.Data.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoreWellManager.Data.Entitites
{
    public class WellEntity:BaseEntity
    {
        public int UserId { get; set; }
        public int LandId { get; set; }
        [NumberWithSpacesValidation(ErrorMessage = "Yalnızca sayılar ve boşluklara izin verilir.")]
        public string XCordinat { get; set; }
        [NumberWithSpacesValidation(ErrorMessage = "Yalnızca sayılar ve boşluklara izin verilir.")]
        public string YCordinat {  get; set; }
        public decimal? Debi { get; set; }
        public decimal? StaticLevel { get; set; }
        public decimal? DynamicLevel { get; set; }

        //Relational Property
        public UserEntity User { get; set; }
        public LandEntity Land { get; set; }
        public ICollection<DocumentEntity> Documents { get; set; }

    }
    public class WellConfiguration : BaseConfiguraiton<WellEntity>
    {
        public override void Configure(EntityTypeBuilder<WellEntity> builder)
        {
            base.Configure(builder);
        }
    }

  
}
