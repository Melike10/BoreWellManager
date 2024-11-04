using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BoreWellManager.Data.Entitites
{
    public class BaseEntity
    {
        public  int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
    }

    public abstract class BaseConfiguraiton<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasQueryFilter(x=>x.IsDeleted== false); // böylece hep bu filtre çalışmış olacak ve silinen kayıtları görmeyeceğiz
            builder.Property(x=>x.ModifiedDate).IsRequired(false);
        }
    }
}
