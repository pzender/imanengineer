﻿// <auto-generated />
using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TV_App.Models;

namespace TV_App.Migrations
{
    [ExcludeFromCodeCoverage]
    [DbContext(typeof(TvAppContext))]
    partial class TvAppContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0-preview8.19405.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TV_App.Models.Channel", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnName("id")
                        .HasColumnType("bigint");

                    b.Property<string>("IconUrl")
                        .HasColumnName("icon_url")
                        .HasColumnType("varchar(200)")
                        .HasMaxLength(200)
                        .IsUnicode(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("varchar(200)")
                        .HasMaxLength(200)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasName("IX_Channel_name");

                    b.ToTable("Channels");
                });

            modelBuilder.Entity("TV_App.Models.ChannelGroup", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnName("id")
                        .HasColumnType("bigint");

                    b.Property<string>("GroupType")
                        .HasColumnName("type")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("IconUrl")
                        .HasColumnName("icon_url")
                        .HasColumnType("varchar(150)")
                        .HasMaxLength(150)
                        .IsUnicode(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("varchar(30)")
                        .HasMaxLength(30)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.ToTable("Offers");
                });

            modelBuilder.Entity("TV_App.Models.Description", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnName("id")
                        .HasColumnType("bigint");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnName("content")
                        .HasColumnType("text");

                    b.Property<long?>("GuideUpdateId")
                        .HasColumnName("guide_update_id")
                        .HasColumnType("bigint");

                    b.Property<long>("ProgrammeId")
                        .HasColumnName("programme_id")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("GuideUpdateId")
                        .HasName("IX_Description_GuideUpdateId");

                    b.HasIndex("ProgrammeId")
                        .HasName("IX_Description_id_programme");

                    b.ToTable("Descriptions");
                });

            modelBuilder.Entity("TV_App.Models.Emission", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("ChannelId")
                        .HasColumnName("channel_id")
                        .HasColumnType("bigint");

                    b.Property<long>("ProgrammeId")
                        .HasColumnName("programme_id")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Start")
                        .HasColumnName("start")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("Stop")
                        .HasColumnName("stop")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.HasIndex("ChannelId")
                        .HasName("IX_Emission_channel_id");

                    b.HasIndex("ProgrammeId")
                        .HasName("IX_Emission_programme_id");

                    b.HasIndex("Start", "Stop", "ProgrammeId", "ChannelId")
                        .IsUnique()
                        .HasName("IX_Emission_start_stop_programme_id_channel_id");

                    b.ToTable("Emissions");
                });

            modelBuilder.Entity("TV_App.Models.Feature", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnName("id")
                        .HasColumnType("bigint");

                    b.Property<long>("Type")
                        .HasColumnName("type")
                        .HasColumnType("bigint");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnName("value")
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique()
                        .HasName("IND_feature_id");

                    b.HasIndex("Type", "Value")
                        .IsUnique()
                        .HasName("IX_Feature_type_value");

                    b.ToTable("Features");
                });

            modelBuilder.Entity("TV_App.Models.FeatureType", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnName("id")
                        .HasColumnType("bigint");

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasColumnName("type_name")
                        .HasColumnType("varchar(200)")
                        .HasMaxLength(200)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.ToTable("FeatureTypes");
                });

            modelBuilder.Entity("TV_App.Models.GroupedChannel", b =>
                {
                    b.Property<long>("OfferId")
                        .HasColumnName("offer_id")
                        .HasColumnType("bigint");

                    b.Property<long>("ChannelId")
                        .HasColumnName("channel_id")
                        .HasColumnType("bigint");

                    b.HasKey("OfferId", "ChannelId");

                    b.HasAlternateKey("ChannelId", "OfferId");

                    b.ToTable("OfferedChannels");
                });

            modelBuilder.Entity("TV_App.Models.GuideUpdate", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnName("id")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("Finished")
                        .HasColumnName("finished")
                        .HasColumnType("datetime");

                    b.Property<string>("Source")
                        .IsRequired()
                        .HasColumnName("source")
                        .HasColumnType("varchar(200)")
                        .HasMaxLength(200)
                        .IsUnicode(false);

                    b.Property<DateTime>("Started")
                        .HasColumnName("started")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.HasIndex("Finished")
                        .IsUnique()
                        .HasName("IX_GuideUpdate_finished")
                        .HasFilter("[finished] IS NOT NULL");

                    b.HasIndex("Started")
                        .IsUnique()
                        .HasName("IX_GuideUpdate_started");

                    b.ToTable("GuideUpdates");
                });

            modelBuilder.Entity("TV_App.Models.Notification", b =>
                {
                    b.Property<string>("UserLogin")
                        .HasColumnName("user_login")
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.Property<long>("EmissionId")
                        .HasColumnName("emission_id")
                        .HasColumnType("bigint");

                    b.Property<long>("Id")
                        .HasColumnName("id")
                        .HasColumnType("bigint");

                    b.Property<long>("RatingValue")
                        .HasColumnName("rating_value")
                        .HasColumnType("bigint");

                    b.HasKey("UserLogin", "EmissionId")
                        .HasName("PK_Notification");

                    b.HasAlternateKey("Id");

                    b.HasIndex("EmissionId")
                        .HasName("IX_Notification_Emission_id");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("TV_App.Models.Programme", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnName("id")
                        .HasColumnType("bigint");

                    b.Property<string>("IconUrl")
                        .HasColumnName("icon_url")
                        .HasColumnType("varchar(200)")
                        .HasMaxLength(200)
                        .IsUnicode(false);

                    b.Property<string>("SeqNumber")
                        .HasColumnName("seq_number")
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.Property<long?>("SeriesId")
                        .HasColumnName("series_id")
                        .HasColumnType("bigint");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnName("title")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200)
                        .IsUnicode(true);

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique()
                        .HasName("IND_programme_id");

                    b.HasIndex("SeriesId")
                        .HasName("IX_Programme_series_id");

                    b.HasIndex("Title")
                        .IsUnique()
                        .HasName("IX_Programme_title");

                    b.ToTable("Programmes");
                });

            modelBuilder.Entity("TV_App.Models.ProgrammesFeature", b =>
                {
                    b.Property<long>("FeatureId")
                        .HasColumnName("feature_id")
                        .HasColumnType("bigint");

                    b.Property<long>("ProgrammeId")
                        .HasColumnName("programme_id")
                        .HasColumnType("bigint");

                    b.HasKey("FeatureId", "ProgrammeId")
                        .HasName("PK_FeatureExample");

                    b.HasIndex("ProgrammeId")
                        .HasName("IX_FeatureExample_programme_id");

                    b.ToTable("ProgrammesFeatures");
                });

            modelBuilder.Entity("TV_App.Models.Rating", b =>
                {
                    b.Property<string>("UserLogin")
                        .HasColumnName("user_login")
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.Property<long>("ProgrammeId")
                        .HasColumnName("programme_id")
                        .HasColumnType("bigint");

                    b.Property<long>("RatingValue")
                        .HasColumnName("rating_value")
                        .HasColumnType("bigint");

                    b.HasKey("UserLogin", "ProgrammeId")
                        .HasName("PK_Rating");

                    b.HasIndex("ProgrammeId")
                        .HasName("IX_Rating_programme_id");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("TV_App.Models.Series", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnName("id")
                        .HasColumnType("bigint");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnName("title")
                        .HasColumnType("varchar(200)")
                        .HasMaxLength(200)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.HasIndex("Title")
                        .IsUnique();

                    b.ToTable("Series");
                });

            modelBuilder.Entity("TV_App.Models.Subscription", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PushAuth")
                        .HasColumnName("push_auth")
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.Property<string>("PushEndpoint")
                        .HasColumnName("push_endpoint")
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.Property<string>("PushP256dh")
                        .HasColumnName("push_p256dh")
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.Property<string>("UserLogin")
                        .IsRequired()
                        .HasColumnName("user_login")
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.HasKey("Id")
                        .HasName("PK_Subscription");

                    b.HasIndex("UserLogin");

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("TV_App.Models.User", b =>
                {
                    b.Property<string>("Login")
                        .HasColumnName("login")
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.Property<double>("WeightActor")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("weight_actor")
                        .HasColumnType("float")
                        .HasDefaultValueSql("((0.1))");

                    b.Property<double>("WeightCategory")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("weight_category")
                        .HasColumnType("float")
                        .HasDefaultValueSql("((0.3))");

                    b.Property<double>("WeightCountry")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("weight_country")
                        .HasColumnType("float")
                        .HasDefaultValueSql("((0.1))");

                    b.Property<double>("WeightDirector")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("weight_director")
                        .HasColumnType("float")
                        .HasDefaultValueSql("((0.1))");

                    b.Property<double>("WeightKeyword")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("weight_keyword")
                        .HasColumnType("float")
                        .HasDefaultValueSql("((0.3))");

                    b.Property<double>("WeightYear")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("weight_year")
                        .HasColumnType("float")
                        .HasDefaultValueSql("((0.1))");

                    b.HasKey("Login")
                        .HasName("PK_User");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TV_App.Models.Description", b =>
                {
                    b.HasOne("TV_App.Models.GuideUpdate", "RelGuideUpdate")
                        .WithMany("Descriptions")
                        .HasForeignKey("GuideUpdateId");

                    b.HasOne("TV_App.Models.Programme", "RelProgramme")
                        .WithMany("Descriptions")
                        .HasForeignKey("ProgrammeId")
                        .IsRequired();
                });

            modelBuilder.Entity("TV_App.Models.Emission", b =>
                {
                    b.HasOne("TV_App.Models.Channel", "ChannelEmitted")
                        .WithMany("Emissions")
                        .HasForeignKey("ChannelId")
                        .IsRequired();

                    b.HasOne("TV_App.Models.Programme", "RelProgramme")
                        .WithMany("Emissions")
                        .HasForeignKey("ProgrammeId")
                        .IsRequired();
                });

            modelBuilder.Entity("TV_App.Models.Feature", b =>
                {
                    b.HasOne("TV_App.Models.FeatureType", "RelType")
                        .WithMany("Features")
                        .HasForeignKey("Type")
                        .IsRequired();
                });

            modelBuilder.Entity("TV_App.Models.GroupedChannel", b =>
                {
                    b.HasOne("TV_App.Models.Channel", "RelChannel")
                        .WithMany("OfferedChannels")
                        .HasForeignKey("ChannelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TV_App.Models.ChannelGroup", "RelOffer")
                        .WithMany("OfferedChannels")
                        .HasForeignKey("OfferId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TV_App.Models.Notification", b =>
                {
                    b.HasOne("TV_App.Models.Emission", "RelEmission")
                        .WithMany("Notifications")
                        .HasForeignKey("EmissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TV_App.Models.User", "RelUser")
                        .WithMany("Notifications")
                        .HasForeignKey("UserLogin")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TV_App.Models.Programme", b =>
                {
                    b.HasOne("TV_App.Models.Series", "RelSeries")
                        .WithMany("Programmes")
                        .HasForeignKey("SeriesId");
                });

            modelBuilder.Entity("TV_App.Models.ProgrammesFeature", b =>
                {
                    b.HasOne("TV_App.Models.Feature", "RelFeature")
                        .WithMany("ProgrammesFeatures")
                        .HasForeignKey("FeatureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TV_App.Models.Programme", "RelProgramme")
                        .WithMany("ProgrammesFeatures")
                        .HasForeignKey("ProgrammeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TV_App.Models.Rating", b =>
                {
                    b.HasOne("TV_App.Models.Programme", "RelProgramme")
                        .WithMany("Ratings")
                        .HasForeignKey("ProgrammeId")
                        .IsRequired();

                    b.HasOne("TV_App.Models.User", "RelUser")
                        .WithMany("Ratings")
                        .HasForeignKey("UserLogin")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TV_App.Models.Subscription", b =>
                {
                    b.HasOne("TV_App.Models.User", "RelUser")
                        .WithMany("Subscriptions")
                        .HasForeignKey("UserLogin")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
