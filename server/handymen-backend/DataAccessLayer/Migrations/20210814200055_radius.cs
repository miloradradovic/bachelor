using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class radius : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Radius",
                table: "Locations");

            migrationBuilder.AddColumn<int>(
                name: "Radius",
                table: "HandyMen",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Radius",
                table: "HandyMen");

            migrationBuilder.AddColumn<int>(
                name: "Radius",
                table: "Locations",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
