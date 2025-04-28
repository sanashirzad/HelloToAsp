using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HelloToAsp.Migrations
{
    /// <inheritdoc />
    public partial class SeedUsersAndTasks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, null, "Sana", "Shirzad", "09917878501" },
                    { 2, null, "AmirAli", "Mahmoodi", "09917878502" },
                    { 3, null, "John", "Bosch", "09917878503" }
                });

            migrationBuilder.InsertData(
                table: "ToDoLists",
                columns: new[] { "Id", "Description", "Duration", "EndDateTime", "IsCompleted", "StartDateTime", "Task", "UserId" },
                values: new object[,]
                {
                    { 1, null, 50, null, false, null, "finish C#", 1 },
                    { 2, null, 150, new DateOnly(2025, 5, 26), false, new DateOnly(2025, 5, 21), "start asp.net core", 3 },
                    { 3, "test", 20, null, false, null, "start git", 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ToDoLists",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ToDoLists",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ToDoLists",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
