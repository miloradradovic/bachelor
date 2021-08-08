using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class rating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RatedJobId",
                table: "Ratings",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_RatedJobId",
                table: "Ratings",
                column: "RatedJobId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Jobs_RatedJobId",
                table: "Ratings",
                column: "RatedJobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Jobs_RatedJobId",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_RatedJobId",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "RatedJobId",
                table: "Ratings");
        }
    }
}
