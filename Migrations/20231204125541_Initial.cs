using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StoreApp.API.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CategoryTotalPrice = table.Column<double>(type: "double", nullable: false),
                    CategoryTotalCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Price = table.Column<double>(type: "double", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryTotalCount", "CategoryTotalPrice", "Description", "Name" },
                values: new object[,]
                {
                    { 1, 0, 0.0, "This is electronics category", "Electronics" },
                    { 2, 0, 0.0, "This is books category", "Books" },
                    { 3, 0, 0.0, "This is food and drinks category", "Foods And Drinks" },
                    { 4, 0, 0.0, "This is clothes category", "Clothes" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "Name", "Price", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, "This has RTX-4090...", "Gaming Laptop", 2000.25, 10 },
                    { 2, 1, "This is ps5...", "PS5", 1000.0, 15 },
                    { 3, 2, "This has lotr...", "Lord Of The Rings - Two Tower", 24.0, 20 },
                    { 4, 3, "This is water", "Water - 5lt", 5.0, 100 },
                    { 5, 4, "This is hoodie for men", "Black Men Hoodie", 64.0, 40 },
                    { 6, 4, "This is parfume for men", "Men Parfume - BrandName", 39.990000000000002, 60 },
                    { 7, 1, "This is latest iphone", "IPhone 15 Pro Max - 1TB", 1299.99, 10 },
                    { 8, 1, "This is high-performance gaming pc", "Gaming Pc - 4090 | i9 | 16gbram", 2900.0, 15 },
                    { 9, 4, "This is sport men shoes", "Men Nike Air Max", 150.0, 100 },
                    { 10, 1, "This is gaming keyboard", "Logitech Gaming Keyboard", 75.0, 25 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
