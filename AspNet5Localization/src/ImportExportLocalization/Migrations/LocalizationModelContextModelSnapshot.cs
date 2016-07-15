using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Localization.SqlLocalizer.DbStringLocalizer;

namespace ImportExportLocalization.Migrations
{
    [DbContext(typeof(LocalizationModelContext))]
    partial class LocalizationModelContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431");

            modelBuilder.Entity("Localization.SqlLocalizer.DbStringLocalizer.ExportHistory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Exported");

                    b.Property<string>("Reason");

                    b.HasKey("Id");

                    b.ToTable("ExportHistoryDbSet");
                });

            modelBuilder.Entity("Localization.SqlLocalizer.DbStringLocalizer.ImportHistory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Imported");

                    b.Property<string>("Information");

                    b.HasKey("Id");

                    b.ToTable("ImportHistoryDbSet");
                });

            modelBuilder.Entity("Localization.SqlLocalizer.DbStringLocalizer.LocalizationRecord", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Key")
                        .IsRequired();

                    b.Property<string>("LocalizationCulture")
                        .IsRequired();

                    b.Property<string>("ResourceKey");

                    b.Property<string>("Text");

                    b.Property<DateTime>("UpdatedTimestamp");

                    b.HasKey("Id");

                    b.HasAlternateKey("Key", "LocalizationCulture");

                    b.ToTable("LocalizationRecords");
                });
        }
    }
}
