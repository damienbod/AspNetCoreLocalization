namespace Localization.SqlLocalizer.DbStringLocalizer
{
    using System;
    using System.Linq;

    using Microsoft.EntityFrameworkCore;

    // >dnx ef migration add LocalizationMigration
    public class LocalizationModelContext : DbContext
    {
        public DbSet<LocalizationRecord> LocalizationRecords { get; set; }
	
		//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
		//    var sqlConnectionString = "Data Source=C:\\git\\damienbod\\AspNet5Localization\\AspNet5Localization\\src\\AspNet5Localization\\LocalizationRecords.sqlite";
        //    optionsBuilder.UseSqlite(sqlConnectionString);
        //}
		
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<LocalizationRecord>().HasKey(m => m.Id);
            //builder.Entity<LocalizationRecord>().HasKey(m => m.LocalizationCulture + m.Key);

            // shadow properties
            builder.Entity<LocalizationRecord>().Property<DateTime>("UpdatedTimestamp");

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