using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoreWellManager.Data.Entitites
{
    public class InstitutionEntity:BaseEntity
    {
        public string Name { get; set; }
        public string City { get; set; }
        public string Town { get; set; }

        //Relatinal Property
        public ICollection<DocumentEntity> Documents { get; set; }
    }
    public  class InstitutionConfiguration:BaseConfiguraiton<InstitutionEntity> {
        public override void Configure(EntityTypeBuilder<InstitutionEntity> builder)
        {
            builder.Property(x => x.Name).IsRequired();
            base.Configure(builder);
        }
    }
}
