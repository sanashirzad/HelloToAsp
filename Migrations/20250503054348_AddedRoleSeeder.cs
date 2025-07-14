using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HelloToAsp.Migrations
{
    /// <inheritdoc />
    public partial class AddedRoleSeeder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "d7b5a2a1-5c8d-4a7f-bf3e-9c6d5e8f2a1b", null, "SuperAdmin", "SAdmin" },
                    { "e8f6b3c4-9a1d-4e7f-a2b3-c5d6e7f8a9b0", null, "Admin", "Admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d7b5a2a1-5c8d-4a7f-bf3e-9c6d5e8f2a1b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e8f6b3c4-9a1d-4e7f-a2b3-c5d6e7f8a9b0");
        }
    }
}
