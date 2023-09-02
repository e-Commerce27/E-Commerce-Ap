using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace E_Commerce_App.Migrations
{
    /// <inheritdoc />
    public partial class addCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Prodects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prodects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prodects_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Amount", "Name", "Type" },
                values: new object[,]
                {
                    { 1, null, "Name1", "category1" },
                    { 2, null, "Name2", "category2" },
                    { 3, null, "Name3 ", "category3" }
                });

            migrationBuilder.InsertData(
                table: "Prodects",
                columns: new[] { "Id", "CategoryId", "Description", "ExpiryDate", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 1, "item1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Name1", 5m },
                    { 2, 2, "item2", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Name2", 10m },
                    { 3, 3, "item3", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Name3 ", 15m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Prodects_CategoryId",
                table: "Prodects",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Prodects");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
