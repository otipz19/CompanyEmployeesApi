using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "CompanyId", "Address", "Country", "Name" },
                values: new object[,]
                {
                    { new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), "Sample address", "USA", "Second company" },
                    { new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "Just some address", "Uganda", "First company" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "Age", "CompanyId", "Name", "Position" },
                values: new object[,]
                {
                    { new Guid("2b2dd2a4-2cf0-42e7-8fce-652e6da2d4f6"), 25, new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), "Jade", "QA" },
                    { new Guid("2dcf7474-f4d0-4e83-a246-d7fb7cf4a2ea"), 100, new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "Josh", "Lazy man" },
                    { new Guid("54f074f0-b51c-4296-b219-f625e8cb673e"), 25, new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "Matt", "QA" },
                    { new Guid("5abd4108-9a59-4494-9b23-33d734618bb7"), 27, new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "John", "Developer" },
                    { new Guid("7af90433-a917-412b-b5fc-8991be0e26b0"), 28, new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), "Frank", "Lazy man" },
                    { new Guid("e07db5af-aae3-4386-95d3-073ec6d640ed"), 31, new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), "Bob", "Developer" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("2b2dd2a4-2cf0-42e7-8fce-652e6da2d4f6"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("2dcf7474-f4d0-4e83-a246-d7fb7cf4a2ea"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("54f074f0-b51c-4296-b219-f625e8cb673e"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("5abd4108-9a59-4494-9b23-33d734618bb7"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("7af90433-a917-412b-b5fc-8991be0e26b0"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("e07db5af-aae3-4386-95d3-073ec6d640ed"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "CompanyId",
                keyValue: new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "CompanyId",
                keyValue: new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"));
        }
    }
}
