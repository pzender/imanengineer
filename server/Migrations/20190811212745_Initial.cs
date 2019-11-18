using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TV_App.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Channels",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false),
                    name = table.Column<string>(unicode: false, maxLength: 200, nullable: false),
                    icon_url = table.Column<string>(unicode: false, maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Channels", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "FeatureTypes",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false),
                    type_name = table.Column<string>(unicode: false, maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeatureTypes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "GuideUpdates",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false),
                    source = table.Column<string>(unicode: false, maxLength: 200, nullable: false),
                    posted = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuideUpdates", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Offers",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false),
                    name = table.Column<string>(unicode: false, maxLength: 30, nullable: false),
                    icon_url = table.Column<string>(unicode: false, maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Series",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false),
                    title = table.Column<string>(unicode: false, maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Series", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    login = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    weight_actor = table.Column<double>(nullable: false, defaultValueSql: "((0.1))"),
                    weight_category = table.Column<double>(nullable: false, defaultValueSql: "((0.3))"),
                    weight_country = table.Column<double>(nullable: false, defaultValueSql: "((0.1))"),
                    weight_year = table.Column<double>(nullable: false, defaultValueSql: "((0.1))"),
                    weight_keyword = table.Column<double>(nullable: false, defaultValueSql: "((0.3))"),
                    weight_director = table.Column<double>(nullable: false, defaultValueSql: "((0.1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.login);
                });

            migrationBuilder.CreateTable(
                name: "Features",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false),
                    type = table.Column<long>(nullable: false),
                    value = table.Column<string>(unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Features", x => x.id);
                    table.ForeignKey(
                        name: "FK_Features_FeatureTypes_type",
                        column: x => x.type,
                        principalTable: "FeatureTypes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OfferedChannels",
                columns: table => new
                {
                    offer_id = table.Column<long>(nullable: false),
                    channel_id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferedChannels", x => new { x.offer_id, x.channel_id });
                    table.UniqueConstraint("AK_OfferedChannels_channel_id_offer_id", x => new { x.channel_id, x.offer_id });
                    table.ForeignKey(
                        name: "FK_OfferedChannels_Channels_channel_id",
                        column: x => x.channel_id,
                        principalTable: "Channels",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OfferedChannels_Offers_offer_id",
                        column: x => x.offer_id,
                        principalTable: "Offers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Programmes",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false),
                    title = table.Column<string>(unicode: false, maxLength: 400, nullable: false),
                    icon_url = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    seq_number = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    series_id = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programmes", x => x.id);
                    table.ForeignKey(
                        name: "FK_Programmes_Series_series_id",
                        column: x => x.series_id,
                        principalTable: "Series",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Descriptions",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false),
                    programme_id = table.Column<long>(nullable: false),
                    content = table.Column<string>(type: "text", nullable: false),
                    guide_update_id = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Descriptions", x => x.id);
                    table.ForeignKey(
                        name: "FK_Descriptions_GuideUpdates_guide_update_id",
                        column: x => x.guide_update_id,
                        principalTable: "GuideUpdates",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Descriptions_Programmes_programme_id",
                        column: x => x.programme_id,
                        principalTable: "Programmes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Emissions",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    start = table.Column<DateTime>(type: "datetime", nullable: false),
                    stop = table.Column<DateTime>(type: "datetime", nullable: false),
                    programme_id = table.Column<long>(nullable: false),
                    channel_id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emissions", x => x.id);
                    table.ForeignKey(
                        name: "FK_Emissions_Channels_channel_id",
                        column: x => x.channel_id,
                        principalTable: "Channels",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Emissions_Programmes_programme_id",
                        column: x => x.programme_id,
                        principalTable: "Programmes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProgrammesFeatures",
                columns: table => new
                {
                    feature_id = table.Column<long>(nullable: false),
                    programme_id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeatureExample", x => new { x.feature_id, x.programme_id });
                    table.ForeignKey(
                        name: "FK_ProgrammesFeatures_Features_feature_id",
                        column: x => x.feature_id,
                        principalTable: "Features",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgrammesFeatures_Programmes_programme_id",
                        column: x => x.programme_id,
                        principalTable: "Programmes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    user_login = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    programme_id = table.Column<long>(nullable: false),
                    rating_value = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rating", x => new { x.user_login, x.programme_id });
                    table.ForeignKey(
                        name: "FK_Ratings_Programmes_programme_id",
                        column: x => x.programme_id,
                        principalTable: "Programmes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ratings_Users_user_login",
                        column: x => x.user_login,
                        principalTable: "Users",
                        principalColumn: "login",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Channel_name",
                table: "Channels",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Description_GuideUpdateId",
                table: "Descriptions",
                column: "guide_update_id");

            migrationBuilder.CreateIndex(
                name: "IX_Description_id_programme",
                table: "Descriptions",
                column: "programme_id");

            migrationBuilder.CreateIndex(
                name: "IX_Emission_channel_id",
                table: "Emissions",
                column: "channel_id");

            migrationBuilder.CreateIndex(
                name: "IX_Emission_programme_id",
                table: "Emissions",
                column: "programme_id");

            migrationBuilder.CreateIndex(
                name: "IX_Emission_start_stop_programme_id_channel_id",
                table: "Emissions",
                columns: new[] { "start", "stop", "programme_id", "channel_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IND_feature_id",
                table: "Features",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Feature_type_value",
                table: "Features",
                columns: new[] { "type", "value" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GuideUpdate_posted",
                table: "GuideUpdates",
                column: "posted",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IND_programme_id",
                table: "Programmes",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Programme_series_id",
                table: "Programmes",
                column: "series_id");

            migrationBuilder.CreateIndex(
                name: "IX_Programme_title",
                table: "Programmes",
                column: "title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FeatureExample_programme_id",
                table: "ProgrammesFeatures",
                column: "programme_id");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_programme_id",
                table: "Ratings",
                column: "programme_id");

            migrationBuilder.CreateIndex(
                name: "IX_Series_title",
                table: "Series",
                column: "title",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Descriptions");

            migrationBuilder.DropTable(
                name: "Emissions");

            migrationBuilder.DropTable(
                name: "OfferedChannels");

            migrationBuilder.DropTable(
                name: "ProgrammesFeatures");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "GuideUpdates");

            migrationBuilder.DropTable(
                name: "Channels");

            migrationBuilder.DropTable(
                name: "Offers");

            migrationBuilder.DropTable(
                name: "Features");

            migrationBuilder.DropTable(
                name: "Programmes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "FeatureTypes");

            migrationBuilder.DropTable(
                name: "Series");
        }
    }
}
