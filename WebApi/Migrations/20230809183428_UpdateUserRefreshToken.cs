using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserRefreshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "194cccb1-09d5-43e7-bce7-9df9c288e2ba");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a8d0d0f4-8b11-437d-b356-f64fad20ff02");

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("03d09745-76a6-46f3-85b9-937ee82fa2ff"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("1d7c32f7-42ac-4627-9f41-030ec3b4beef"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("2a4aca1d-3fd0-44ef-b645-a50e22f4b8ec"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("3d9b8846-a3d6-4126-ae94-3138578c385d"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("d2d12b5f-feb7-4106-86f0-1b28f3e13ca6"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("e2f3d134-3ddd-4bdc-9d62-543060cd340b"));

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1d4f7857-0293-4ea7-8210-b0dae63041e2", null, "Admin", "ADMIN" },
                    { "d35b2776-0983-4846-bf50-8393cf4ce4c5", null, "Manager", "MANAGER" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "Age", "CompanyId", "Name", "Position" },
                values: new object[,]
                {
                    { new Guid("034736db-dcb0-4334-9617-36db201468ec"), 28, new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), "Frank", "Lazy man" },
                    { new Guid("09f652ae-aca2-482f-9a52-78da63aea1f1"), 27, new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "John", "Developer" },
                    { new Guid("69a7e9b9-3384-40fc-b56d-7cbc48fb8a48"), 25, new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "Matt", "QA" },
                    { new Guid("dc71bce8-1a5c-4d3b-bb47-fd3402ce25fb"), 25, new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), "Jade", "QA" },
                    { new Guid("e43965f8-2c96-4789-ba17-99ac14d385db"), 100, new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "Josh", "Lazy man" },
                    { new Guid("ea569b6d-b7f4-43bd-b869-5dca8fdcae41"), 31, new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), "Bob", "Developer" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1d4f7857-0293-4ea7-8210-b0dae63041e2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d35b2776-0983-4846-bf50-8393cf4ce4c5");

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("034736db-dcb0-4334-9617-36db201468ec"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("09f652ae-aca2-482f-9a52-78da63aea1f1"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("69a7e9b9-3384-40fc-b56d-7cbc48fb8a48"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("dc71bce8-1a5c-4d3b-bb47-fd3402ce25fb"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("e43965f8-2c96-4789-ba17-99ac14d385db"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("ea569b6d-b7f4-43bd-b869-5dca8fdcae41"));

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "194cccb1-09d5-43e7-bce7-9df9c288e2ba", null, "Admin", "ADMIN" },
                    { "a8d0d0f4-8b11-437d-b356-f64fad20ff02", null, "Manager", "MANAGER" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "Age", "CompanyId", "Name", "Position" },
                values: new object[,]
                {
                    { new Guid("03d09745-76a6-46f3-85b9-937ee82fa2ff"), 27, new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "John", "Developer" },
                    { new Guid("1d7c32f7-42ac-4627-9f41-030ec3b4beef"), 31, new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), "Bob", "Developer" },
                    { new Guid("2a4aca1d-3fd0-44ef-b645-a50e22f4b8ec"), 100, new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "Josh", "Lazy man" },
                    { new Guid("3d9b8846-a3d6-4126-ae94-3138578c385d"), 25, new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "Matt", "QA" },
                    { new Guid("d2d12b5f-feb7-4106-86f0-1b28f3e13ca6"), 28, new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), "Frank", "Lazy man" },
                    { new Guid("e2f3d134-3ddd-4bdc-9d62-543060cd340b"), 25, new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), "Jade", "QA" }
                });
        }
    }
}
