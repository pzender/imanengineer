using Microsoft.EntityFrameworkCore.Migrations;

namespace TV_App.Migrations
{
    public partial class mysql : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Channel",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false),
                    name = table.Column<string>(type: "STRING", nullable: false),
                    icon_url = table.Column<string>(type: "STRING", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Channel", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "FeatureTypes",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false),
                    type_name = table.Column<string>(type: "STRING", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeatureTypes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "GuideUpdate",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false),
                    source = table.Column<string>(type: "STRING", nullable: false),
                    posted = table.Column<string>(type: "DATETIME", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuideUpdate", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Series",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false),
                    title = table.Column<string>(type: "STRING", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Series", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    login = table.Column<string>(type: "STRING", nullable: false),
                    weight_actor = table.Column<double>(type: "DOUBLE", nullable: false, defaultValueSql: "0.1"),
                    weight_category = table.Column<double>(type: "DOUBLE", nullable: false, defaultValueSql: "0.3"),
                    weight_country = table.Column<double>(type: "DOUBLE", nullable: false, defaultValueSql: "0.1"),
                    weight_year = table.Column<double>(type: "DOUBLE", nullable: false, defaultValueSql: "0.1"),
                    weight_keyword = table.Column<double>(type: "DOUBLE", nullable: false, defaultValueSql: "0.3"),
                    weight_director = table.Column<double>(type: "DOUBLE", nullable: false, defaultValueSql: "0.1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.login);
                });

            migrationBuilder.CreateTable(
                name: "Feature",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false),
                    type = table.Column<long>(nullable: false),
                    value = table.Column<string>(type: "STRING", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feature", x => x.id);
                    table.ForeignKey(
                        name: "FK_Feature_FeatureTypes_type",
                        column: x => x.type,
                        principalTable: "FeatureTypes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Programme",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false),
                    title = table.Column<string>(type: "STRING", nullable: false),
                    icon_url = table.Column<string>(type: "STRING", nullable: true),
                    seq_number = table.Column<string>(type: "STRING", nullable: true),
                    series_id = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programme", x => x.id);
                    table.ForeignKey(
                        name: "FK_Programme_Series_series_id",
                        column: x => x.series_id,
                        principalTable: "Series",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Description",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false),
                    id_programme = table.Column<long>(nullable: false),
                    source = table.Column<long>(nullable: true),
                    content = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Description", x => x.id);
                    table.ForeignKey(
                        name: "FK_Description_Programme_id_programme",
                        column: x => x.id_programme,
                        principalTable: "Programme",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Description_GuideUpdate_source",
                        column: x => x.source,
                        principalTable: "GuideUpdate",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Emission",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false),
                    start = table.Column<string>(type: "DATETIME", nullable: false),
                    stop = table.Column<string>(type: "DATETIME", nullable: false),
                    programme_id = table.Column<long>(nullable: false),
                    channel_id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emission", x => x.id);
                    table.ForeignKey(
                        name: "FK_Emission_Channel_channel_id",
                        column: x => x.channel_id,
                        principalTable: "Channel",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Emission_Programme_programme_id",
                        column: x => x.programme_id,
                        principalTable: "Programme",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FeatureExample",
                columns: table => new
                {
                    feature_id = table.Column<long>(nullable: false),
                    programme_id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeatureExample", x => new { x.feature_id, x.programme_id });
                    table.ForeignKey(
                        name: "FK_FeatureExample_Feature_feature_id",
                        column: x => x.feature_id,
                        principalTable: "Feature",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FeatureExample_Programme_programme_id",
                        column: x => x.programme_id,
                        principalTable: "Programme",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Rating",
                columns: table => new
                {
                    user_login = table.Column<string>(type: "STRING", nullable: false),
                    programme_id = table.Column<long>(nullable: false),
                    rating_value = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rating", x => new { x.user_login, x.programme_id });
                    table.ForeignKey(
                        name: "FK_Rating_Programme_programme_id",
                        column: x => x.programme_id,
                        principalTable: "Programme",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rating_User_user_login",
                        column: x => x.user_login,
                        principalTable: "User",
                        principalColumn: "login",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Channel_name",
                table: "Channel",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Description_content",
                table: "Description",
                column: "content",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Description_source",
                table: "Description",
                column: "source");

            migrationBuilder.CreateIndex(
                name: "IX_Description_id_programme_source",
                table: "Description",
                columns: new[] { "id_programme", "source" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Emission_channel_id",
                table: "Emission",
                column: "channel_id");

            migrationBuilder.CreateIndex(
                name: "IX_Emission_programme_id",
                table: "Emission",
                column: "programme_id");

            migrationBuilder.CreateIndex(
                name: "IX_Emission_start_stop_programme_id_channel_id",
                table: "Emission",
                columns: new[] { "start", "stop", "programme_id", "channel_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IND_feature_id",
                table: "Feature",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Feature_type_value",
                table: "Feature",
                columns: new[] { "type", "value" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FeatureExample_programme_id",
                table: "FeatureExample",
                column: "programme_id");

            migrationBuilder.CreateIndex(
                name: "IX_GuideUpdate_posted",
                table: "GuideUpdate",
                column: "posted",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IND_programme_id",
                table: "Programme",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Programme_series_id",
                table: "Programme",
                column: "series_id");

            migrationBuilder.CreateIndex(
                name: "IX_Programme_title",
                table: "Programme",
                column: "title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rating_programme_id",
                table: "Rating",
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
                name: "Description");

            migrationBuilder.DropTable(
                name: "Emission");

            migrationBuilder.DropTable(
                name: "FeatureExample");

            migrationBuilder.DropTable(
                name: "Rating");

            migrationBuilder.DropTable(
                name: "GuideUpdate");

            migrationBuilder.DropTable(
                name: "Channel");

            migrationBuilder.DropTable(
                name: "Feature");

            migrationBuilder.DropTable(
                name: "Programme");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "FeatureTypes");

            migrationBuilder.DropTable(
                name: "Series");
        }
    }
}
