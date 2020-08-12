using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace GraphQLCore.Data.Models.Migrations
{
    public partial class RecreateEmployeeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.CreateTable(
               name: "Employees",
               columns: table => new
               {
                   EmployeeId = table.Column<int>(nullable: false)
                       .Annotation("Sqlite:Autoincrement", true),
                   FirstName = table.Column<string>(maxLength: 50, nullable: false),
                   LastName = table.Column<string>(maxLength: 50, nullable: true),
                   DateOfBirth = table.Column<DateTime>(nullable: false),
                   Email = table.Column<string>(maxLength: 255, nullable: false),
                   PhoneNumber = table.Column<string>(maxLength: 10, nullable: true)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_Employees", x => x.EmployeeId);
               });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeID", "FirstName", "LastName", "DateOfBirth", "Email", "PhoneNumber" },
                values: new object[] { 1, "Santosh", "Varma", new DateTime(1989, 10, 08), "santosh.kosuri@imaginea.com", "9894283098" });

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
            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
