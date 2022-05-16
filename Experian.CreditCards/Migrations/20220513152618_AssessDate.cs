using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Experian.CreditCards.Migrations
{
    public partial class AssessDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AssessDate",
                table: "AssesssedApplicants",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssessDate",
                table: "AssesssedApplicants");
        }
    }
}
