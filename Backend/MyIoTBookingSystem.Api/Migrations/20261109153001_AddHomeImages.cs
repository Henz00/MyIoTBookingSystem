using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyIoTBookingSystem.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddHomeImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HomeImage_Homes_HomeId",
                table: "HomeImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HomeImage",
                table: "HomeImage");

            migrationBuilder.RenameTable(
                name: "HomeImage",
                newName: "HomeImages");

            migrationBuilder.RenameIndex(
                name: "IX_HomeImage_HomeId",
                table: "HomeImages",
                newName: "IX_HomeImages_HomeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HomeImages",
                table: "HomeImages",
                column: "HomeImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_HomeImages_Homes_HomeId",
                table: "HomeImages",
                column: "HomeId",
                principalTable: "Homes",
                principalColumn: "HomeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HomeImages_Homes_HomeId",
                table: "HomeImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HomeImages",
                table: "HomeImages");

            migrationBuilder.RenameTable(
                name: "HomeImages",
                newName: "HomeImage");

            migrationBuilder.RenameIndex(
                name: "IX_HomeImages_HomeId",
                table: "HomeImage",
                newName: "IX_HomeImage_HomeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HomeImage",
                table: "HomeImage",
                column: "HomeImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_HomeImage_Homes_HomeId",
                table: "HomeImage",
                column: "HomeId",
                principalTable: "Homes",
                principalColumn: "HomeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
