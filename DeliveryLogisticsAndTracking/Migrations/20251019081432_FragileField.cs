using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliveryLogisticsAndTracking.Migrations
{
    /// <inheritdoc />
    public partial class FragileField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Fragile",
                table: "Shipment",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fragile",
                table: "Shipment");
        }
    }
}
