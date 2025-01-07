using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Database.Migrations
{
    /// <inheritdoc />
    public partial class SeedinginitalData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TodoItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoItems", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "TodoItems",
                columns: new[] { "Id", "DateCreated", "IsCompleted", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 6, 16, 18, 23, 914, DateTimeKind.Local).AddTicks(4471), false, "Sleeping" },
                    { 2, new DateTime(2025, 1, 6, 16, 18, 23, 914, DateTimeKind.Local).AddTicks(4482), false, "Cooking" },
                    { 3, new DateTime(2025, 1, 6, 16, 18, 23, 914, DateTimeKind.Local).AddTicks(4483), false, "Playing" },
                    { 4, new DateTime(2025, 1, 6, 16, 18, 23, 914, DateTimeKind.Local).AddTicks(4484), false, "Playing" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TodoItems");
        }
    }
}
