using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class newmigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Somethings_Users_UserId",
                table: "Somethings");

            migrationBuilder.DropIndex(
                name: "IX_Somethings_UserId",
                table: "Somethings");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Somethings");

            migrationBuilder.CreateTable(
                name: "SomethingUser",
                columns: table => new
                {
                    SomethingsId = table.Column<int>(type: "integer", nullable: false),
                    usersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SomethingUser", x => new { x.SomethingsId, x.usersId });
                    table.ForeignKey(
                        name: "FK_SomethingUser_Somethings_SomethingsId",
                        column: x => x.SomethingsId,
                        principalTable: "Somethings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SomethingUser_Users_usersId",
                        column: x => x.usersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SomethingUser_usersId",
                table: "SomethingUser",
                column: "usersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SomethingUser");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Somethings",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Somethings_UserId",
                table: "Somethings",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Somethings_Users_UserId",
                table: "Somethings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
