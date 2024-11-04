using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoreWellManager.Data.Entitites
{
    public class LandOwnersEntity:BaseEntity
    {
        public int LandId { get; set; } // Arazi ID'si
        public int UserId { get; set; } // Sahip ID'si

        // Relational Property
        public UserEntity User { get; set; }
        public LandEntity Land { get; set; }
      
    }
    public  class LandOwnersConfiguration : BaseConfiguraiton<LandOwnersEntity>
    {
        public override void Configure(EntityTypeBuilder<LandOwnersEntity> builder)
        {

            builder.Ignore(x => x.Id);
            builder.HasKey("LandId", "UserId");
            base.Configure(builder);
        }
    }
}
