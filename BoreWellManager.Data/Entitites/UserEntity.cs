using BoreWellManager.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoreWellManager.Data.Entitites
{
    public class UserEntity:BaseEntity
    {
        [MinLength(11)]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "Eksik veya fazla giriş yaptınız.")]
        public string TC { get; set; }
        public string Name { get; set; }
        [NumberWithSpacesValidation(ErrorMessage = "Yalnızca sayılar ve boşluklara izin verilir.")]
        public string Phone { get; set; }
        public string Adress { get; set; }
        public UserType UserType { get; set; }
        public bool IsResponsible { get; set; }// iş sahibi mi

        // Relational Property
        public ICollection<LandOwnersEntity> LandOwners { get; set; }
        //public ICollection<DocumentEntity> Documents { get; set; }
        public ICollection<WellEntity> Wells { get; set; }
        
    }
    public  class UserConfiguration : BaseConfiguraiton<UserEntity>
    {
        public override void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.Property(t => t.Name).IsRequired();
            builder.Property(t => t.Phone).IsRequired();
            builder.Property(t=>t.UserType).IsRequired();
            builder.Property(t=>t.TC).IsRequired().HasMaxLength(11).IsFixedLength();
            base.Configure(builder);
        }
    }
}
