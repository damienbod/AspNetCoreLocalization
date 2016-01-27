namespace AspNet5Localization.DbStringLocalizer
{
    using System;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    // >dnx ef migration add LocalizationMigration
    public class LocalizationModelSqliteContext : DbContext
    {
        public DbSet<LocalizationRecord> DataEventRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<LocalizationRecord>().HasKey(m => m.Id);
            //builder.Entity<LocalizationRecord>().HasKey(m => m.LocalizationCulture + m.Key);

            // shadow properties
            builder.Entity<LocalizationRecord>().Property<DateTime>("UpdatedTimestamp");

            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           // var builder = new ConfigurationBuilder()
           //.AddJsonFile("appsettings.json")
           //.AddEnvironmentVariables();
           // var configuration = builder.Build();

           // var sqlConnectionString = configuration["DbStringLocalizer:ConnectionString"];

           // optionsBuilder.UseSqlite(sqlConnectionString);
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