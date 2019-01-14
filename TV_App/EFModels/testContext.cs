using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TV_App.EFModels
{
    public partial class testContext : DbContext
    {
        public testContext()
        {
        }

        public testContext(DbContextOptions<testContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Channel> Channel { get; set; }
        public virtual DbSet<Description> Description { get; set; }
        public virtual DbSet<Emission> Emission { get; set; }
        public virtual DbSet<Feature> Feature { get; set; }
        public virtual DbSet<FeatureExample> FeatureExample { get; set; }
        public virtual DbSet<FeatureTypes> FeatureTypes { get; set; }
        public virtual DbSet<GuideUpdate> GuideUpdate { get; set; }

        internal Feature Include()
        {
            throw new NotImplementedException();
        }

        public virtual DbSet<Programme> Programme { get; set; }
        public virtual DbSet<Rating> Rating { get; set; }
        public virtual DbSet<Series> Series { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlite("Data Source=DataLayer\\\\\\\\test.sqlite");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.Entity<Channel>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.IconUrl)
                    .HasColumnName("icon_url")
                    .HasColumnType("STRING");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("STRING");
            });

            modelBuilder.Entity<Description>(entity =>
            {
                entity.HasIndex(e => e.Content)
                    .IsUnique();

                entity.HasIndex(e => new { e.IdProgramme, e.Source })
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnName("content");

                entity.Property(e => e.IdProgramme).HasColumnName("id_programme");

                entity.Property(e => e.Source).HasColumnName("source");

                entity.HasOne(d => d.IdProgrammeNavigation)
                    .WithMany(p => p.Description)
                    .HasForeignKey(d => d.IdProgramme)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.SourceNavigation)
                    .WithMany(p => p.Description)
                    .HasForeignKey(d => d.Source)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<Emission>(entity =>
            {
                entity.HasIndex(e => new { e.Start, e.Stop, e.ProgrammeId, e.ChannelId })
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.ChannelId).HasColumnName("channel_id");

                entity.Property(e => e.ProgrammeId).HasColumnName("programme_id");

                entity.Property(e => e.Start)
                    .IsRequired()
                    .HasColumnName("start")
                    .HasColumnType("DATETIME");

                entity.Property(e => e.Stop)
                    .IsRequired()
                    .HasColumnName("stop")
                    .HasColumnType("DATETIME");

                entity.HasOne(d => d.Channel)
                    .WithMany(p => p.Emission)
                    .HasForeignKey(d => d.ChannelId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Programme)
                    .WithMany(p => p.Emission)
                    .HasForeignKey(d => d.ProgrammeId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Feature>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("IND_feature_id")
                    .IsUnique();

                entity.HasIndex(e => new { e.Type, e.Value })
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Type).HasColumnName("type");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasColumnName("value")
                    .HasColumnType("STRING");

                entity.HasOne(d => d.TypeNavigation)
                    .WithMany(p => p.Feature)
                    .HasForeignKey(d => d.Type)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<FeatureExample>(entity =>
            {
                entity.HasKey(e => new { e.FeatureId, e.ProgrammeId });

                entity.Property(e => e.FeatureId).HasColumnName("feature_id");

                entity.Property(e => e.ProgrammeId).HasColumnName("programme_id");

                entity.HasOne(d => d.Feature)
                    .WithMany(p => p.FeatureExample)
                    .HasForeignKey(d => d.FeatureId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Programme)
                    .WithMany(p => p.FeatureExample)
                    .HasForeignKey(d => d.ProgrammeId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<FeatureTypes>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.TypeName)
                    .IsRequired()
                    .HasColumnName("type_name")
                    .HasColumnType("STRING");
            });

            modelBuilder.Entity<GuideUpdate>(entity =>
            {
                entity.HasIndex(e => e.Posted)
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Posted)
                    .IsRequired()
                    .HasColumnName("posted")
                    .HasColumnType("DATETIME");

                entity.Property(e => e.Source)
                    .IsRequired()
                    .HasColumnName("source")
                    .HasColumnType("STRING");
            });

            modelBuilder.Entity<Programme>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("IND_programme_id")
                    .IsUnique();

                entity.HasIndex(e => e.Title)
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.IconUrl)
                    .HasColumnName("icon_url")
                    .HasColumnType("STRING");

                entity.Property(e => e.SeqNumber)
                    .HasColumnName("seq_number")
                    .HasColumnType("STRING");

                entity.Property(e => e.SeriesId).HasColumnName("series_id");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasColumnType("STRING");

                entity.HasOne(d => d.Series)
                    .WithMany(p => p.Programme)
                    .HasForeignKey(d => d.SeriesId);
            });

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.HasKey(e => new { e.UserLogin, e.ProgrammeId });

                entity.Property(e => e.UserLogin)
                    .HasColumnName("user_login")
                    .HasColumnType("STRING");

                entity.Property(e => e.ProgrammeId).HasColumnName("programme_id");

                entity.Property(e => e.RatingValue).HasColumnName("rating_value");

                entity.HasOne(d => d.Programme)
                    .WithMany(p => p.Rating)
                    .HasForeignKey(d => d.ProgrammeId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.UserLoginNavigation)
                    .WithMany(p => p.Rating)
                    .HasForeignKey(d => d.UserLogin);
            });

            modelBuilder.Entity<Series>(entity =>
            {
                entity.HasIndex(e => e.Title)
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasColumnType("STRING");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Login);

                entity.Property(e => e.Login)
                    .HasColumnName("login")
                    .HasColumnType("STRING")
                    .ValueGeneratedNever();
            });
        }
    }
}
