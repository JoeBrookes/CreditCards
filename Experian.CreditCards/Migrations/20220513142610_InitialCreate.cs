using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Experian.CreditCards.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Applicants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Dob = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Income = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applicants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apr = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Promo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RuleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AssesssedApplicants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DecisioningMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssesssedApplicants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssesssedApplicants_Applicants_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "Applicants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssessedApplicantCard",
                columns: table => new
                {
                    AssessedApplicantsId = table.Column<int>(type: "int", nullable: false),
                    AvailableCardsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssessedApplicantCard", x => new { x.AssessedApplicantsId, x.AvailableCardsId });
                    table.ForeignKey(
                        name: "FK_AssessedApplicantCard_AssesssedApplicants_AssessedApplicantsId",
                        column: x => x.AssessedApplicantsId,
                        principalTable: "AssesssedApplicants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssessedApplicantCard_Cards_AvailableCardsId",
                        column: x => x.AvailableCardsId,
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "Id", "Apr", "Name", "Promo", "RuleName" },
                values: new object[] { 1, 10m, "Barclaycard", "Super Barclaycard promo", "Barclaycard" });

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "Id", "Apr", "Name", "Promo", "RuleName" },
                values: new object[] { 2, 20m, "Vanquis", "Ace Vanquis promo", "Vanquis" });

            migrationBuilder.CreateIndex(
                name: "IX_AssessedApplicantCard_AvailableCardsId",
                table: "AssessedApplicantCard",
                column: "AvailableCardsId");

            migrationBuilder.CreateIndex(
                name: "IX_AssesssedApplicants_ApplicantId",
                table: "AssesssedApplicants",
                column: "ApplicantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssessedApplicantCard");

            migrationBuilder.DropTable(
                name: "AssesssedApplicants");

            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "Applicants");
        }
    }
}
