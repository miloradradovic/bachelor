using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class model2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Radius",
                table: "Locations",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CircleId",
                table: "HandyMen",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HandyMen_CircleId",
                table: "HandyMen",
                column: "CircleId");

            migrationBuilder.AddForeignKey(
                name: "FK_HandyMen_Locations_CircleId",
                table: "HandyMen",
                column: "CircleId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HandyMen_Locations_CircleId",
                table: "HandyMen");

            migrationBuilder.DropIndex(
                name: "IX_HandyMen_CircleId",
                table: "HandyMen");

            migrationBuilder.DropColumn(
                name: "Radius",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "CircleId",
                table: "HandyMen");
        }
    }
}
