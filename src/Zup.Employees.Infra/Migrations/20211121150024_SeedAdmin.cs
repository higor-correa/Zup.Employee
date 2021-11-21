using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zup.Employees.Infra.Migrations
{
    public partial class SeedAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employee",
                columns: new[] { "Id", "Email", "IsLeader", "LeaderId", "Name", "PasswordHash", "PlateNumber", "Surname" },
                values: new object[] { new Guid("661b8028-6ce0-4544-950d-18837c2bcd7e"), "admin@admin.com", true, null, "Admin", "18a948b42a6f1fa8b84bfc73c8a967b1df15ee4dbd08e9bd150441b5e576698c", 0, "" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employee",
                keyColumn: "Id",
                keyValue: new Guid("661b8028-6ce0-4544-950d-18837c2bcd7e"));
        }
    }
}
