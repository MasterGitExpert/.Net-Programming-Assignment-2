using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliveryLogisticsAndTracking.Migrations
{
    /// <inheritdoc />
    public partial class deliveryModelUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ETADateTime",
                table: "Delivery",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ETADateTime",
                table: "Delivery");
        }
    }
}
