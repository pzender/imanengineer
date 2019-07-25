using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TV_App.EFModels
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
                optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=tv_db;Persist Security Info=True;User ID=SA;Password=yourStrong(!)Password"); //FIXME!
                optionsBuilder.EnableSensitiveDataLogging();
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
                    .HasColumnType("VARCHAR(200)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("VARCHAR(200)");
            });

            modelBuilder.Entity<Description>(entity =>
            {
                entity.HasIndex(e => e.Content)
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnName("content");

                entity.Property(e => e.IdProgramme).HasColumnName("id_programme");

                entity.HasOne(d => d.IdProgrammeNavigation)
                    .WithMany(p => p.Description)
                    .HasForeignKey(d => d.IdProgramme)
                    .OnDelete(DeleteBehavior.ClientSetNull);
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
                    .HasColumnType("VARCHAR(200)");

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
                    .HasColumnType("VARCHAR(200)");
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
                    .HasColumnType("VARCHAR(200)");
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
                    .HasColumnType("VARCHAR(200)");

                entity.Property(e => e.SeqNumber)
                    .HasColumnName("seq_number")
                    .HasColumnType("VARCHAR(200)");

                entity.Property(e => e.SeriesId).HasColumnName("series_id");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasColumnType("VARCHAR(200)");

                entity.HasOne(d => d.Series)
                    .WithMany(p => p.Programme)
                    .HasForeignKey(d => d.SeriesId);
            });

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.HasKey(e => new { e.UserLogin, e.ProgrammeId });

                entity.Property(e => e.UserLogin)
                    .HasColumnName("user_login")
                    .HasColumnType("VARCHAR(200)");

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
                    .HasColumnType("VARCHAR(200)");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Login);

                entity.Property(e => e.Login)
                    .HasColumnName("login")
                    .HasColumnType("VARCHAR(200)")
                    .ValueGeneratedNever();

                entity.Property(e => e.WeightActor)
                    .HasColumnName("weight_actor")
                    .HasColumnType("float")
                    .HasDefaultValueSql("0.1");

                entity.Property(e => e.WeightCategory)
                    .HasColumnName("weight_category")
                    .HasColumnType("float")
                    .HasDefaultValueSql("0.3");

                entity.Property(e => e.WeightCountry)
                    .HasColumnName("weight_country")
                    .HasColumnType("float")
                    .HasDefaultValueSql("0.1");

                entity.Property(e => e.WeightDirector)
                    .HasColumnName("weight_director")
                    .HasColumnType("float")
                    .HasDefaultValueSql("0.1");

                entity.Property(e => e.WeightKeyword)
                    .HasColumnName("weight_keyword")
                    .HasColumnType("float")
                    .HasDefaultValueSql("0.3");

                entity.Property(e => e.WeightYear)
                    .HasColumnName("weight_year")
                    .HasColumnType("float")
                    .HasDefaultValueSql("0.1");
            });
        }
    }
}
