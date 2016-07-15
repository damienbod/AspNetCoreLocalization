using System;
using System.Linq;

using Microsoft.EntityFrameworkCore;

namespace Localization.SqlLocalizer.DbStringLocalizer
{
    // >dotnet ef migrations add LocalizationMigration
    public class LocalizationModelContext : DbContext
    {
        public LocalizationModelContext(DbContextOptions<LocalizationModelContext> options) :base(options)
        { }
        
        public DbSet<LocalizationRecord> LocalizationRecords { get; set; }
        public DbSet<ExportHistory> ExportHistoryDbSet { get; set; }
        public DbSet<ImportHistory> ImportHistoryDbSet { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<LocalizationRecord>().HasKey(m => m.Id);               
            builder.Entity<LocalizationRecord>().HasAlternateKey(c => new { c.Key, c.LocalizationCulture });

            // shadow properties
            builder.Entity<LocalizationRecord>().Property<DateTime>("UpdatedTimestamp");

            builder.Entity<ExportHistory>().HasKey(m => m.Id);

            builder.Entity<ImportHistory>().HasKey(m => m.Id);

            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            updateUpdatedProperty<LocalizationRecord>();
            return base.SaveChanges();
        }

        private void updateUpdatedProperty<T>() where T : class
        {
            var modifiedSourceInfo =
                ChangeTracker.Entries<T>()
                    .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in modifiedSourceInfo)
            {
                entry.Property("UpdatedTimestamp").CurrentValue = DateTime.UtcNow;
            }
        }
    }
}