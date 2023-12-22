using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace questionnaire.Migrations
{
    public partial class AddRelationBetweenAspNetUserAndWalkthrough : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "walkthrough",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_walkthrough_UserId",
                table: "walkthrough",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_walkthrough_AspNetUsers_UserId",
                table: "walkthrough",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_walkthrough_AspNetUsers_UserId",
                table: "walkthrough");

            migrationBuilder.DropIndex(
                name: "IX_walkthrough_UserId",
                table: "walkthrough");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "walkthrough");
        }
    }
}
