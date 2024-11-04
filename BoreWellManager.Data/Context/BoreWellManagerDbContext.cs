using BoreWellManager.Data.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoreWellManager.Data.Context
{
    public class BoreWellManagerDbContext : DbContext
    {
        public BoreWellManagerDbContext(DbContextOptions<BoreWellManagerDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Document ile Payment arasındaki bire bir ilişki
            modelBuilder.Entity<DocumentEntity>()
                .HasOne(d => d.Payment)
                .WithOne(p => p.Document)
                .HasForeignKey<PaymentEntity>(p => p.DocumentId);
          

            //fluent Apply 
            modelBuilder.ApplyConfiguration(new DocumentConfiguraiton());
            modelBuilder.ApplyConfiguration(new InstitutionConfiguration());
            modelBuilder.ApplyConfiguration(new LandConfiguration());
            modelBuilder.ApplyConfiguration(new LandOwnersConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new WellConfiguration());

            modelBuilder.Entity<SettingEntity>().HasData(
                new SettingEntity {
                    Id=1,
                    MaintenenceMode=false
                
                }
                );
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<UserEntity> Users => Set<UserEntity>();
        public DbSet<DocumentEntity> Documents => Set<DocumentEntity>();
        public DbSet<InstitutionEntity> Institutions => Set<InstitutionEntity>();
        public DbSet<LandOwnersEntity> LandOwners => Set<LandOwnersEntity>();
        public DbSet<LandEntity> Lands => Set<LandEntity>();
        public DbSet<PaymentEntity> Payments => Set<PaymentEntity>();
        public DbSet<WellEntity> Wells => Set<WellEntity>();
        public DbSet<SettingEntity> Settings => Set<SettingEntity>();
    }
}
