using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eShopSolution.Data.Migrations
{
    public partial class ChangFileProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "FileSize",
                table: "ProductImages",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("d48f23bb-2138-48dc-81e0-318910aae84c"),
                column: "ConcurrencyStamp",
                value: "e314cf40-190d-413d-94ae-5788582acd94");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("d3ac39d2-d541-4ce9-8362-2a92b8af37e5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2fc6b1bd-6751-4f8a-af12-5d902264ffdf", "AQAAAAEAACcQAAAAEL1Mogf5sK53+KpmC5XjAqEis47j+oitUei92Hh25VrBMk7W4uFnMJ8dzqYzXKX0Xg==" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 3, 8, 13, 21, 52, 732, DateTimeKind.Local).AddTicks(7260));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "FileSize",
                table: "ProductImages",
                type: "int",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("d48f23bb-2138-48dc-81e0-318910aae84c"),
                column: "ConcurrencyStamp",
                value: "d4f371ae-029f-4565-9ea0-6ec30895cb47");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("d3ac39d2-d541-4ce9-8362-2a92b8af37e5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "cd739a9c-15b6-4229-8f4d-87c3617ea016", "AQAAAAEAACcQAAAAECdadJSFCgdgM1tUnr70xmd/ucz8rB6D4XWz6LoBmV7fk0XFrul3A+0VMbvy3jiQwQ==" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 2, 29, 15, 2, 44, 887, DateTimeKind.Local).AddTicks(7595));
        }
    }
}
