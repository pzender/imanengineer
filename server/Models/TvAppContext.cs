using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TV_App.Models
{
    public partial class TvAppContext : DbContext
    {
        public TvAppContext()
        {
        }

        public TvAppContext(DbContextOptions<TvAppContext> options)
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
        public virtual DbSet<Programme> Programme { get; set; }
        public virtual DbSet<Rating> Rating { get; set; }
        public virtual DbSet<Series> Series { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=tv_db;Persist Security Info=True;User ID=SA;Password=yourStrong(!)Password");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Channel>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.IconUrl)
                    .HasColumnName("icon_url")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Description>(entity =>
            {
                entity.HasIndex(e => e.GuideUpdateId);

                entity.HasIndex(e => e.IdProgramme);

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnName("content")
                    .HasColumnType("text");

                entity.Property(e => e.IdProgramme).HasColumnName("id_programme");

                entity.HasOne(d => d.GuideUpdate)
                    .WithMany(p => p.Description)
                    .HasForeignKey(d => d.GuideUpdateId);

                entity.HasOne(d => d.IdProgrammeNavigation)
                    .WithMany(p => p.Description)
                    .HasForeignKey(d => d.IdProgramme)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Emission>(entity =>
            {
                entity.HasIndex(e => e.ChannelId);

                entity.HasIndex(e => e.ProgrammeId);

                entity.HasIndex(e => new { e.Start, e.Stop, e.ProgrammeId, e.ChannelId })
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.ChannelId).HasColumnName("channel_id");

                entity.Property(e => e.ProgrammeId).HasColumnName("programme_id");

                entity.Property(e => e.Start)
                    .HasColumnName("start")
                    .HasColumnType("datetime");

                entity.Property(e => e.Stop)
                    .HasColumnName("stop")
                    .HasColumnType("datetime");

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
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.TypeNavigation)
                    .WithMany(p => p.Feature)
                    .HasForeignKey(d => d.Type)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<FeatureExample>(entity =>
            {
                entity.HasKey(e => new { e.FeatureId, e.ProgrammeId });

                entity.HasIndex(e => e.ProgrammeId);

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
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<GuideUpdate>(entity =>
            {
                entity.HasIndex(e => e.Posted)
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Posted)
                    .HasColumnName("posted")
                    .HasColumnType("datetime");

                entity.Property(e => e.Source)
                    .IsRequired()
                    .HasColumnName("source")
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Programme>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("IND_programme_id")
                    .IsUnique();

                entity.HasIndex(e => e.SeriesId);

                entity.HasIndex(e => e.Title)
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.IconUrl)
                    .HasColumnName("icon_url")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.SeqNumber)
                    .HasColumnName("seq_number")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.SeriesId).HasColumnName("series_id");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.Series)
                    .WithMany(p => p.Programme)
                    .HasForeignKey(d => d.SeriesId);
            });

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.HasKey(e => new { e.UserLogin, e.ProgrammeId });

                entity.HasIndex(e => e.ProgrammeId);

                entity.Property(e => e.UserLogin)
                    .HasColumnName("user_login")
                    .HasMaxLength(200)
                    .IsUnicode(false);

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
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Login);

                entity.Property(e => e.Login)
                    .HasColumnName("login")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.WeightActor)
                    .HasColumnName("weight_actor")
                    .HasDefaultValueSql("((0.1))");

                entity.Property(e => e.WeightCategory)
                    .HasColumnName("weight_category")
                    .HasDefaultValueSql("((0.3))");

                entity.Property(e => e.WeightCountry)
                    .HasColumnName("weight_country")
                    .HasDefaultValueSql("((0.1))");

                entity.Property(e => e.WeightDirector)
                    .HasColumnName("weight_director")
                    .HasDefaultValueSql("((0.1))");

                entity.Property(e => e.WeightKeyword)
                    .HasColumnName("weight_keyword")
                    .HasDefaultValueSql("((0.3))");

                entity.Property(e => e.WeightYear)
                    .HasColumnName("weight_year")
                    .HasDefaultValueSql("((0.1))");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
