using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication8.Migrations
{
    /// <inheritdoc />
    public partial class create_db : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ispitni_rok",
                columns: table => new
                {
                    ID_ROKA = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAZIV = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    SKOLSKA_GOD = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false),
                    STATUS_ROKA = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ispitni___C7D0FE72BF36341A", x => x.ID_ROKA);
                });

            migrationBuilder.CreateTable(
                name: "profesor",
                columns: table => new
                {
                    ID_PROFESORA = table.Column<short>(type: "smallint", nullable: false),
                    IME = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    PREZIME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ZVANJE = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: false),
                    DATUM_ZAP = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__profesor__63597FD7DB522E88", x => x.ID_PROFESORA);
                });

            migrationBuilder.CreateTable(
                name: "student",
                columns: table => new
                {
                    ID_STUDENTA = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IME = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    PREZIME = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    SMER = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    BROJ = table.Column<short>(type: "smallint", nullable: false),
                    GODINA_UPISA = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__student__0FD2897897FB3C85", x => x.ID_STUDENTA);
                });

            migrationBuilder.CreateTable(
                name: "predmet",
                columns: table => new
                {
                    ID_PREDMETA = table.Column<short>(type: "smallint", nullable: false),
                    ID_PROFESORA = table.Column<short>(type: "smallint", nullable: false),
                    NAZIV = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ESPB = table.Column<short>(type: "smallint", nullable: false),
                    STATUS = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__predmet__2C40E4E28279966B", x => x.ID_PREDMETA);
                    table.ForeignKey(
                        name: "FK_predmet_profesor",
                        column: x => x.ID_PROFESORA,
                        principalTable: "profesor",
                        principalColumn: "ID_PROFESORA");
                });

            migrationBuilder.CreateTable(
                name: "ispit",
                columns: table => new
                {
                    ID_ISPITA = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_ROKA = table.Column<int>(type: "int", nullable: false),
                    ID_PREDMETA = table.Column<short>(type: "smallint", nullable: false),
                    DATUM = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ispit__29C6AD7FDD02363B", x => x.ID_ISPITA);
                    table.ForeignKey(
                        name: "FK_ispit_ispitni_rok",
                        column: x => x.ID_ROKA,
                        principalTable: "ispitni_rok",
                        principalColumn: "ID_ROKA");
                    table.ForeignKey(
                        name: "FK_ispit_predmet",
                        column: x => x.ID_PREDMETA,
                        principalTable: "predmet",
                        principalColumn: "ID_PREDMETA",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "student_predmet",
                columns: table => new
                {
                    ID_STUDENTA = table.Column<int>(type: "int", nullable: false),
                    ID_PREDMETA = table.Column<short>(type: "smallint", nullable: false),
                    SKOLSKA_GODINA = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__student___BB07D41166382CA2", x => new { x.ID_STUDENTA, x.ID_PREDMETA, x.SKOLSKA_GODINA });
                    table.ForeignKey(
                        name: "FK_student_predmet_predmet",
                        column: x => x.ID_PREDMETA,
                        principalTable: "predmet",
                        principalColumn: "ID_PREDMETA");
                    table.ForeignKey(
                        name: "FK_student_predmet_student",
                        column: x => x.ID_STUDENTA,
                        principalTable: "student",
                        principalColumn: "ID_STUDENTA");
                });

            migrationBuilder.CreateTable(
                name: "zapisnik",
                columns: table => new
                {
                    ID_STUDENTA = table.Column<int>(type: "int", nullable: false),
                    ID_ISPITA = table.Column<int>(type: "int", nullable: false),
                    OCENA = table.Column<float>(type: "real", nullable: false),
                    BODOVI = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__zapisnik__ED4EE3AFE84A496D", x => new { x.ID_STUDENTA, x.ID_ISPITA });
                    table.ForeignKey(
                        name: "FK_zapisnik_ispit",
                        column: x => x.ID_ISPITA,
                        principalTable: "ispit",
                        principalColumn: "ID_ISPITA");
                    table.ForeignKey(
                        name: "FK_zapisnik_student",
                        column: x => x.ID_STUDENTA,
                        principalTable: "student",
                        principalColumn: "ID_STUDENTA");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ispit_ID_PREDMETA",
                table: "ispit",
                column: "ID_PREDMETA");

            migrationBuilder.CreateIndex(
                name: "IX_ispit_ID_ROKA",
                table: "ispit",
                column: "ID_ROKA");

            migrationBuilder.CreateIndex(
                name: "IX_predmet_ID_PROFESORA",
                table: "predmet",
                column: "ID_PROFESORA");

            migrationBuilder.CreateIndex(
                name: "IX_student_predmet_ID_PREDMETA",
                table: "student_predmet",
                column: "ID_PREDMETA");

            migrationBuilder.CreateIndex(
                name: "IX_zapisnik_ID_ISPITA",
                table: "zapisnik",
                column: "ID_ISPITA");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "student_predmet");

            migrationBuilder.DropTable(
                name: "zapisnik");

            migrationBuilder.DropTable(
                name: "ispit");

            migrationBuilder.DropTable(
                name: "student");

            migrationBuilder.DropTable(
                name: "ispitni_rok");

            migrationBuilder.DropTable(
                name: "predmet");

            migrationBuilder.DropTable(
                name: "profesor");
        }
    }
}
