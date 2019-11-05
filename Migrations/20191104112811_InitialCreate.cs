using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyScriptureJournal.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Journal",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JournalName = table.Column<string>(maxLength: 70, nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Journal", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Prophet",
                columns: table => new
                {
                    ProphetID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    DispDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prophet", x => x.ProphetID);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    CityID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    AssignedDate = table.Column<DateTime>(nullable: false),
                    ProphetID = table.Column<int>(nullable: true),
                    RevelatorProphetID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.CityID);
                    table.ForeignKey(
                        name: "FK_City_Prophet_RevelatorProphetID",
                        column: x => x.RevelatorProphetID,
                        principalTable: "Prophet",
                        principalColumn: "ProphetID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PriesthoodOffice",
                columns: table => new
                {
                    ProphetID = table.Column<int>(nullable: false),
                    priesthoodOffice = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriesthoodOffice", x => x.ProphetID);
                    table.ForeignKey(
                        name: "FK_PriesthoodOffice_Prophet_ProphetID",
                        column: x => x.ProphetID,
                        principalTable: "Prophet",
                        principalColumn: "ProphetID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reference",
                columns: table => new
                {
                    ReferenceID = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    ChapterAndVerse = table.Column<string>(maxLength: 50, nullable: true),
                    SpiritualNotes = table.Column<string>(nullable: true),
                    CityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reference", x => x.ReferenceID);
                    table.ForeignKey(
                        name: "FK_Reference_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "CityID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DispensationLinks",
                columns: table => new
                {
                    ProphetID = table.Column<int>(nullable: false),
                    ReferenceID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DispensationLinks", x => new { x.ReferenceID, x.ProphetID });
                    table.ForeignKey(
                        name: "FK_DispensationLinks_Prophet_ProphetID",
                        column: x => x.ProphetID,
                        principalTable: "Prophet",
                        principalColumn: "ProphetID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DispensationLinks_Reference_ReferenceID",
                        column: x => x.ReferenceID,
                        principalTable: "Reference",
                        principalColumn: "ReferenceID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Note",
                columns: table => new
                {
                    NoteID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Canon = table.Column<int>(nullable: true),
                    ReferenceID = table.Column<int>(nullable: false),
                    JournalID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Note", x => x.NoteID);
                    table.ForeignKey(
                        name: "FK_Note_Journal_JournalID",
                        column: x => x.JournalID,
                        principalTable: "Journal",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Note_Reference_ReferenceID",
                        column: x => x.ReferenceID,
                        principalTable: "Reference",
                        principalColumn: "ReferenceID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_City_RevelatorProphetID",
                table: "City",
                column: "RevelatorProphetID");

            migrationBuilder.CreateIndex(
                name: "IX_DispensationLinks_ProphetID",
                table: "DispensationLinks",
                column: "ProphetID");

            migrationBuilder.CreateIndex(
                name: "IX_Note_JournalID",
                table: "Note",
                column: "JournalID");

            migrationBuilder.CreateIndex(
                name: "IX_Note_ReferenceID",
                table: "Note",
                column: "ReferenceID");

            migrationBuilder.CreateIndex(
                name: "IX_Reference_CityId",
                table: "Reference",
                column: "CityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DispensationLinks");

            migrationBuilder.DropTable(
                name: "Note");

            migrationBuilder.DropTable(
                name: "PriesthoodOffice");

            migrationBuilder.DropTable(
                name: "Journal");

            migrationBuilder.DropTable(
                name: "Reference");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "Prophet");
        }
    }
}
