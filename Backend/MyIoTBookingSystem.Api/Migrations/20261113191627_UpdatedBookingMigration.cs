using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyIoTBookingSystem.Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedBookingMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaymentArrangement",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentArrangement",
                table: "Bookings");
        }
    }
}
