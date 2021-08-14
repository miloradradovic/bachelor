using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class address : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HandyMen_Locations_CircleId",
                table: "HandyMen");

            migrationBuilder.RenameColumn(
                name: "CircleId",
                table: "HandyMen",
                newName: "AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_HandyMen_CircleId",
                table: "HandyMen",
                newName: "IX_HandyMen_AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_HandyMen_Locations_AddressId",
                table: "HandyMen",
                column: "AddressId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HandyMen_Locations_AddressId",
                table: "HandyMen");

            migrationBuilder.RenameColumn(
                name: "AddressId",
                table: "HandyMen",
                newName: "CircleId");

            migrationBuilder.RenameIndex(
                name: "IX_HandyMen_AddressId",
                table: "HandyMen",
                newName: "IX_HandyMen_CircleId");

            migrationBuilder.AddForeignKey(
                name: "FK_HandyMen_Locations_CircleId",
                table: "HandyMen",
                column: "CircleId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
