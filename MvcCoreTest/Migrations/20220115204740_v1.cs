using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcCoreTest.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Arabalar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UretimYili = table.Column<short>(type: "smallint", nullable: true),
                    Fiyat = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arabalar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ureticiler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirmaAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirmaLokasyon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UretimDurumu = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ureticiler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Aciklamalar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Detay = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ArabaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aciklamalar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Aciklamalar_Arabalar_ArabaId",
                        column: x => x.ArabaId,
                        principalTable: "Arabalar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Modellers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArabaId = table.Column<int>(type: "int", nullable: false),
                    UreticiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modellers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Modellers_Arabalar_ArabaId",
                        column: x => x.ArabaId,
                        principalTable: "Arabalar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Modellers_Ureticiler_UreticiId",
                        column: x => x.UreticiId,
                        principalTable: "Ureticiler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aciklamalar_ArabaId",
                table: "Aciklamalar",
                column: "ArabaId");

            migrationBuilder.CreateIndex(
                name: "IX_Modellers_ArabaId",
                table: "Modellers",
                column: "ArabaId");

            migrationBuilder.CreateIndex(
                name: "IX_Modellers_UreticiId",
                table: "Modellers",
                column: "UreticiId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Aciklamalar");

            migrationBuilder.DropTable(
                name: "Modellers");

            migrationBuilder.DropTable(
                name: "Arabalar");

            migrationBuilder.DropTable(
                name: "Ureticiler");
        }
    }
}
