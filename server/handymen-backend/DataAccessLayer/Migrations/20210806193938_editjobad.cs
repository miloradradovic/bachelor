using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class editjobad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateFrom",
                table: "JobAd");

            migrationBuilder.DropColumn(
                name: "PriceFrom",
                table: "AdditionalJobAdInfos");

            migrationBuilder.RenameColumn(
                name: "DateTo",
                table: "JobAd",
                newName: "DateWhen");

            migrationBuilder.RenameColumn(
                name: "PriceTo",
                table: "AdditionalJobAdInfos",
                newName: "PriceMax");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateWhen",
                table: "JobAd",
                newName: "DateTo");

            migrationBuilder.RenameColumn(
                name: "PriceMax",
                table: "AdditionalJobAdInfos",
                newName: "PriceTo");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateFrom",
                table: "JobAd",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "PriceFrom",
                table: "AdditionalJobAdInfos",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
