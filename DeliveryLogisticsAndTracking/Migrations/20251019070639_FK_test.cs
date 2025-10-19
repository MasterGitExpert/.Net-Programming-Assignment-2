using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliveryLogisticsAndTracking.Migrations
{
    /// <inheritdoc />
    public partial class FK_test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Delivery_Shipment_GoodsShipmentId",
                table: "Delivery");

            migrationBuilder.DropForeignKey(
                name: "FK_Delivery_User_DriverId",
                table: "Delivery");

            migrationBuilder.DropForeignKey(
                name: "FK_Delivery_Vehicle_DeliveryVehicleId",
                table: "Delivery");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Role_RoleId",
                table: "User");

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GoodsShipmentId",
                table: "Delivery",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DriverId",
                table: "Delivery",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DeliveryVehicleId",
                table: "Delivery",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Delivery_Shipment_GoodsShipmentId",
                table: "Delivery",
                column: "GoodsShipmentId",
                principalTable: "Shipment",
                principalColumn: "ShipmentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Delivery_User_DriverId",
                table: "Delivery",
                column: "DriverId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Delivery_Vehicle_DeliveryVehicleId",
                table: "Delivery",
                column: "DeliveryVehicleId",
                principalTable: "Vehicle",
                principalColumn: "VehicleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Role_RoleId",
                table: "User",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Delivery_Shipment_GoodsShipmentId",
                table: "Delivery");

            migrationBuilder.DropForeignKey(
                name: "FK_Delivery_User_DriverId",
                table: "Delivery");

            migrationBuilder.DropForeignKey(
                name: "FK_Delivery_Vehicle_DeliveryVehicleId",
                table: "Delivery");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Role_RoleId",
                table: "User");

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "User",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "GoodsShipmentId",
                table: "Delivery",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DriverId",
                table: "Delivery",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DeliveryVehicleId",
                table: "Delivery",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Delivery_Shipment_GoodsShipmentId",
                table: "Delivery",
                column: "GoodsShipmentId",
                principalTable: "Shipment",
                principalColumn: "ShipmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Delivery_User_DriverId",
                table: "Delivery",
                column: "DriverId",
                principalTable: "User",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Delivery_Vehicle_DeliveryVehicleId",
                table: "Delivery",
                column: "DeliveryVehicleId",
                principalTable: "Vehicle",
                principalColumn: "VehicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Role_RoleId",
                table: "User",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "RoleId");
        }
    }
}
