using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StatistikkApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kommuner",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Navn = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    KommuneNummer = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kommuner", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StatistikkKategorier",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Navn = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatistikkKategorier", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StatistikkData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Aar = table.Column<int>(type: "INTEGER", nullable: false),
                    Verdi = table.Column<int>(type: "INTEGER", nullable: false),
                    KommuneId = table.Column<int>(type: "INTEGER", nullable: false),
                    StatistikkKategoriId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatistikkData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StatistikkData_Kommuner_KommuneId",
                        column: x => x.KommuneId,
                        principalTable: "Kommuner",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StatistikkData_StatistikkKategorier_StatistikkKategoriId",
                        column: x => x.StatistikkKategoriId,
                        principalTable: "StatistikkKategorier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StatistikkData_KommuneId",
                table: "StatistikkData",
                column: "KommuneId");

            migrationBuilder.CreateIndex(
                name: "IX_StatistikkData_StatistikkKategoriId",
                table: "StatistikkData",
                column: "StatistikkKategoriId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StatistikkData");

            migrationBuilder.DropTable(
                name: "Kommuner");

            migrationBuilder.DropTable(
                name: "StatistikkKategorier");
        }
    }
}
