using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication8.Migrations
{
    /// <inheritdoc />
    public partial class add_prijava : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prijava_BrojIndeksas_ispit_ID_PREDMETA",
                table: "Prijava_BrojIndeksas");

            migrationBuilder.DropForeignKey(
                name: "FK_Prijava_BrojIndeksas_student_ID_STUDENTA",
                table: "Prijava_BrojIndeksas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Prijava_BrojIndeksas",
                table: "Prijava_BrojIndeksas");

            migrationBuilder.RenameTable(
                name: "Prijava_BrojIndeksas",
                newName: "prijava_brojindeksa");

            migrationBuilder.RenameColumn(
                name: "ID_PREDMETA",
                table: "prijava_brojindeksa",
                newName: "ID_ISPITA");

            migrationBuilder.RenameIndex(
                name: "IX_Prijava_BrojIndeksas_ID_STUDENTA",
                table: "prijava_brojindeksa",
                newName: "IX_prijava_brojindeksa_ID_STUDENTA");

            migrationBuilder.AddPrimaryKey(
                name: "PK_prijava_brojindeksa",
                table: "prijava_brojindeksa",
                columns: new[] { "ID_ISPITA", "ID_STUDENTA" });

            migrationBuilder.AddForeignKey(
                name: "FK_prijava_brojindeksa_ispit_ID_ISPITA",
                table: "prijava_brojindeksa",
                column: "ID_ISPITA",
                principalTable: "ispit",
                principalColumn: "ID_ISPITA",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_prijava_brojindeksa_student_ID_STUDENTA",
                table: "prijava_brojindeksa",
                column: "ID_STUDENTA",
                principalTable: "student",
                principalColumn: "ID_STUDENTA",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_prijava_brojindeksa_ispit_ID_ISPITA",
                table: "prijava_brojindeksa");

            migrationBuilder.DropForeignKey(
                name: "FK_prijava_brojindeksa_student_ID_STUDENTA",
                table: "prijava_brojindeksa");

            migrationBuilder.DropPrimaryKey(
                name: "PK_prijava_brojindeksa",
                table: "prijava_brojindeksa");

            migrationBuilder.RenameTable(
                name: "prijava_brojindeksa",
                newName: "Prijava_BrojIndeksas");

            migrationBuilder.RenameColumn(
                name: "ID_ISPITA",
                table: "Prijava_BrojIndeksas",
                newName: "ID_PREDMETA");

            migrationBuilder.RenameIndex(
                name: "IX_prijava_brojindeksa_ID_STUDENTA",
                table: "Prijava_BrojIndeksas",
                newName: "IX_Prijava_BrojIndeksas_ID_STUDENTA");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Prijava_BrojIndeksas",
                table: "Prijava_BrojIndeksas",
                columns: new[] { "ID_PREDMETA", "ID_STUDENTA" });

            migrationBuilder.AddForeignKey(
                name: "FK_Prijava_BrojIndeksas_ispit_ID_PREDMETA",
                table: "Prijava_BrojIndeksas",
                column: "ID_PREDMETA",
                principalTable: "ispit",
                principalColumn: "ID_ISPITA",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prijava_BrojIndeksas_student_ID_STUDENTA",
                table: "Prijava_BrojIndeksas",
                column: "ID_STUDENTA",
                principalTable: "student",
                principalColumn: "ID_STUDENTA",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
