using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace GraphQLCore.Data.Models.Migrations
{
    public partial class SeedEmployeeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeID", "FirstName", "LastName", "DateOfBirth", "Email", "PhoneNumber" },
                values: new object[] { 2, "Captain", "America", new DateTime(1923, 04, 03), "captain.america@imaginea.com", "9832285698" });
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeID", "FirstName", "LastName", "DateOfBirth", "Email", "PhoneNumber" },
                values: new object[] { 3, "Iron", "Man", new DateTime(1974, 03, 23), "iron.man@imaginea.com", "9895686798" });
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeID", "FirstName", "LastName", "DateOfBirth", "Email", "PhoneNumber" },
                values: new object[] { 4, "Black", "Widow", new DateTime(1979, 02, 13), "black.widow@imaginea.com", "3494283048" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
