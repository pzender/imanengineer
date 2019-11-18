using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace TV_App.Models
{
    [ExcludeFromCodeCoverage]
    public partial class TvAppContext : DbContext
    {
        private const string DB_HOST = "167.71.51.30";
        private const string DB_DOCKER = "db";

        public TvAppContext()
        {
        }

        public TvAppContext(DbContextOptions<TvAppContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Channel> Channels { get; set; }
        public virtual DbSet<Description> Descriptions { get; set; }
        public virtual DbSet<Emission> Emissions { get; set; }
        public virtual DbSet<FeatureType> FeatureTypes { get; set; }
        public virtual DbSet<Feature> Features { get; set; }
        public virtual DbSet<GuideUpdate> GuideUpdates { get; set; }
        public virtual DbSet<GroupedChannel> OfferedChannels { get; set; }
        public virtual DbSet<ChannelGroup> Offers { get; set; }
        public virtual DbSet<Programme> Programmes { get; set; }
        public virtual DbSet<ProgrammesFeature> ProgrammesFeatures { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }
        public virtual DbSet<Series> Series { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Subscription> Subscriptions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseLazyLoadingProxies();
                optionsBuilder.UseSqlServer($"Data Source={DB_DOCKER};Initial Catalog=tv_db;Persist Security Info=True;User ID=SA;Password=P@ssw0rd;MultipleActiveResultSets=true;");
                optionsBuilder.EnableSensitiveDataLogging();
                optionsBuilder.EnableDetailedErrors();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Channel>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("IX_Channel_name")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.IconUrl).IsUnicode(false);
                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<Description>(entity =>
            {
                entity.HasIndex(e => e.GuideUpdateId)
                    .HasName("IX_Description_GuideUpdateId");

                entity.HasIndex(e => e.ProgrammeId)
                    .HasName("IX_Description_id_programme");

                entity.HasOne(d => d.RelGuideUpdate)
                    .WithMany(p => p.Descriptions)
                    .HasForeignKey(d => d.GuideUpdateId);

                entity.HasOne(d => d.RelProgramme)
                    .WithMany(p => p.Descriptions)
                    .HasForeignKey(d => d.ProgrammeId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Emission>(entity =>
            {
                entity.HasIndex(e => e.ChannelId)
                    .HasName("IX_Emission_channel_id");

                entity.HasIndex(e => e.ProgrammeId)
                    .HasName("IX_Emission_programme_id");

                entity.HasIndex(e => new { e.Start, e.Stop, e.ProgrammeId, e.ChannelId })
                    .HasName("IX_Emission_start_stop_programme_id_channel_id")
                    .IsUnique();

                entity.HasOne(d => d.ChannelEmitted)
                    .WithMany(p => p.Emissions)
                    .HasForeignKey(d => d.ChannelId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.RelProgramme)
                    .WithMany(p => p.Emissions)
                    .HasForeignKey(d => d.ProgrammeId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

            });

            modelBuilder.Entity<FeatureType>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.TypeName).IsUnicode(false);
            });

            modelBuilder.Entity<Feature>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("IND_feature_id")
                    .IsUnique();

                entity.HasIndex(e => new { e.Type, e.Value })
                    .HasName("IX_Feature_type_value")
                    .IsUnique();

                entity.HasOne(d => d.RelType)
                    .WithMany(p => p.Features)
                    .HasForeignKey(d => d.Type)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Value).IsUnicode(false);
            });

            modelBuilder.Entity<GuideUpdate>(entity =>
            {
                entity.HasIndex(e => e.Started)
                    .HasName("IX_GuideUpdate_started")
                    .IsUnique();

                entity.HasIndex(e => e.Finished)
                    .HasName("IX_GuideUpdate_finished")
                    .IsUnique();


                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Source).IsUnicode(false);
            });

            modelBuilder.Entity<GroupedChannel>(entity =>
            {
                entity.HasKey(e => new { e.OfferId, e.ChannelId });
            });

            modelBuilder.Entity<ChannelGroup>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.IconUrl).IsUnicode(false);
                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<Programme>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("IND_programme_id")
                    .IsUnique();

                entity.HasIndex(e => e.SeriesId)
                    .HasName("IX_Programme_series_id");

                entity.HasIndex(e => e.Title)
                    .HasName("IX_Programme_title")
                    .IsUnique();

                entity.HasOne(d => d.RelSeries)
                    .WithMany(p => p.Programmes)
                    .HasForeignKey(d => d.SeriesId);

                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.IconUrl).IsUnicode(false);
                entity.Property(e => e.SeqNumber).IsUnicode(false);
                entity.Property(e => e.Title).IsUnicode(true);
            });

            modelBuilder.Entity<ProgrammesFeature>(entity =>
            {
                entity.HasKey(e => new { e.FeatureId, e.ProgrammeId })
                    .HasName("PK_FeatureExample");

                entity.HasIndex(e => e.ProgrammeId)
                    .HasName("IX_FeatureExample_programme_id");
            });

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.HasKey(e => new { e.UserLogin, e.ProgrammeId })
                    .HasName("PK_Rating");

                entity.HasIndex(e => e.ProgrammeId)
                    .HasName("IX_Rating_programme_id");

                entity.Property(e => e.UserLogin).IsUnicode(false);

                entity.HasOne(d => d.RelProgramme)
                    .WithMany(p => p.Ratings)
                    .HasForeignKey(d => d.ProgrammeId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.RelUser)
                    .WithMany(p => p.Ratings)
                    .HasForeignKey(d => d.UserLogin);

            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasKey(e => new { e.UserLogin, e.EmissionId })
                    .HasName("PK_Notification");

                entity.HasIndex(e => e.EmissionId)
                    .HasName("IX_Notification_Emission_id");

                entity.Property(e => e.UserLogin).IsUnicode(false);

                entity.HasOne(d => d.RelEmission)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.EmissionId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.RelUser)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.UserLogin);

            });

            modelBuilder.Entity<Subscription>(entity =>
            {
                entity.HasKey(s => s.Id)
                    .HasName("PK_Subscription");

                entity.Property(e => e.UserLogin).IsUnicode(false);

                entity.HasOne(d => d.RelUser)
                    .WithMany(p => p.Subscriptions)
                    .HasForeignKey(d => d.UserLogin);

            });




            modelBuilder.Entity<Series>(entity =>
            {
                entity.HasIndex(e => e.Title)
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Title).IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Login)
                    .HasName("PK_User");

                entity.Property(e => e.Login).IsUnicode(false);
                entity.Property(e => e.WeightActor).HasDefaultValueSql("((0.2))");
                entity.Property(e => e.WeightCategory).HasDefaultValueSql("((0.4))");
                entity.Property(e => e.WeightCountry).HasDefaultValueSql("((0.1))");
                entity.Property(e => e.WeightDirector).HasDefaultValueSql("((0.2))");
                entity.Property(e => e.WeightKeyword).HasDefaultValueSql("((0.0))");
                entity.Property(e => e.WeightYear).HasDefaultValueSql("((0.1))");
            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
