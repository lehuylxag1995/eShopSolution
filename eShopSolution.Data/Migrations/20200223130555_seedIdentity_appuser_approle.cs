using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eShopSolution.Data.Migrations
{
    public partial class seedIdentity_appuser_approle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2020, 2, 23, 20, 5, 54, 477, DateTimeKind.Local).AddTicks(1765),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 2, 23, 19, 45, 17, 701, DateTimeKind.Local).AddTicks(1454));

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { new Guid("d48f23bb-2138-48dc-81e0-318910aae84c"), "0cc6499a-7a74-4e2b-9a7a-30ebf050f3e2", "Administrator role", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Dob", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("d3ac39d2-d541-4ce9-8362-2a92b8af37e5"), 0, "a8a0ce7e-ed69-41fd-b2b6-89b5510cbf9d", new DateTime(1995, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "lehuylxag1995@gmail.com", true, "Huy", "Lê", false, null, "lehuylxag1995@gmail.com", "admin", "AQAAAAEAACcQAAAAEGDIIIHzskXgYKgeYJqaANJ8EEI6WIERbVk9thyp0kk1Qg2Dr4/kFY8C6jvNDyEXcw==", null, false, "", false, "admin" });

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

            migrationBuilder.InsertData(
                table: "AppUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { new Guid("d3ac39d2-d541-4ce9-8362-2a92b8af37e5"), new Guid("d48f23bb-2138-48dc-81e0-318910aae84c") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { new Guid("d3ac39d2-d541-4ce9-8362-2a92b8af37e5"), new Guid("d48f23bb-2138-48dc-81e0-318910aae84c") });

            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("d48f23bb-2138-48dc-81e0-318910aae84c"));

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("d3ac39d2-d541-4ce9-8362-2a92b8af37e5"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 2, 23, 19, 45, 17, 701, DateTimeKind.Local).AddTicks(1454),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 2, 23, 20, 5, 54, 477, DateTimeKind.Local).AddTicks(1765));

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
                value: new DateTime(2020, 2, 23, 19, 45, 17, 718, DateTimeKind.Local).AddTicks(202));
        }
    }
}
