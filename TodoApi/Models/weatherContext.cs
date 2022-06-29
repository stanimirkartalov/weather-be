using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WeatherApi.Models
{
    public partial class weatherContext : DbContext
    {
        //public weatherContext()
        //{
        //}

        public weatherContext(DbContextOptions<weatherContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Reading> Readings { get; set; }
        public virtual DbSet<Settings> Settings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySQL("server=localhost;port=3306;database=weather;user=root;password=password;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("locations");

                entity.Property(e => e.Id)
                    .HasColumnType("int unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Reading>(entity =>
            {
                entity.ToTable("readings");

                entity.HasIndex(e => e.LocationId, "readings_locations");

                entity.Property(e => e.Id)
                    .HasColumnType("int unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.AthmosphericPreasure).HasColumnName("athmospheric_preasure");

                entity.Property(e => e.Humidity).HasColumnName("humidity");

                entity.Property(e => e.Lightness).HasColumnName("lightness");

                entity.Property(e => e.LocationId)
                    .HasColumnType("int unsigned")
                    .HasColumnName("location_id");

                entity.Property(e => e.Temperature).HasColumnName("temperature");

                entity.Property(e => e.Timestamp).HasColumnName("timestamp");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Readings)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("readings_locations");
            });

            modelBuilder.Entity<Settings>(entity =>
            {
                entity.ToTable("settings");

                entity.Property(e => e.Id)
                    .HasColumnType("int unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.Ip)
                    .HasMaxLength(256)
                    .HasColumnName("ip");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
