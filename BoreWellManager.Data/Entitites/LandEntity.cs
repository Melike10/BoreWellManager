using BoreWellManager.Data.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoreWellManager.Data.Entitites
{
    public class LandEntity:BaseEntity
    {
        public string City { get; set; }
        public string Town { get; set; }
        public string Street { get; set; }
        public string Block { get; set; } // ada
        public string Plot { get; set; }//parsel
        public string? Location { get; set; }//mevki
        public string LandType { get; set; }
        public bool HasLien { get; set; }//şerh,irtifak,beyan var mı?
        public bool IsCksRequired { get; set; }//ÇKS GEREKLİ Mİ
        public LienType? LienType{ get; set; }

        // Relational Property
        public ICollection<LandOwnersEntity> LandOwners { get; set; }
        //public ICollection<DocumentEntity> Documents { get; set; }
        public ICollection<WellEntity> Wells { get; set; }

    }
    public class LandConfiguration : BaseConfiguraiton<LandEntity>
    {
        public override void Configure(EntityTypeBuilder<LandEntity> builder)
        {
            builder.Property(t => t.City).IsRequired();
            builder.Property(t => t.Town).IsRequired();
            builder.Property(t => t.Block).IsRequired();
            builder.Property(t => t.Plot).IsRequired();
            builder.Property(t => t.LandType).IsRequired();
            base.Configure(builder);
        }
    }
}
