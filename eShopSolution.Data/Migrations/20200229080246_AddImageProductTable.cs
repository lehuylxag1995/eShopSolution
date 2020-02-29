using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eShopSolution.Data.Migrations
{
    public partial class AddImageProductTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 2, 23, 20, 5, 54, 477, DateTimeKind.Local).AddTicks(1765));

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    ImagePath = table.Column<string>(maxLength: 200, nullable: false),
                    Caption = table.Column<string>(maxLength: 200, nullable: true),
                    SortOrder = table.Column<int>(nullable: false),
                    IsDefault = table.Column<bool>(nullable: false, defaultValue: false),
                    FileSize = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 2, 23, 20, 5, 54, 477, DateTimeKind.Local).AddTicks(1765),
                oldClrType: typeof(DateTime));

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("d48f23bb-2138-48dc-81e0-318910aae84c"),
                column: "ConcurrencyStamp",
                value: "0cc6499a-7a74-4e2b-9a7a-30ebf050f3e2");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("d3ac39d2-d541-4ce9-8362-2a92b8af37e5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a8a0ce7e-ed69-41fd-b2b6-89b5510cbf9d", "AQAAAAEAACcQAAAAEGDIIIHzskXgYKgeYJqaANJ8EEI6WIERbVk9thyp0kk1Qg2Dr4/kFY8C6jvNDyEXcw==" });

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
                value: new DateTime(2020, 2, 23, 20, 5, 54, 497, DateTimeKind.Local).AddTicks(6497));
        }
    }
}
