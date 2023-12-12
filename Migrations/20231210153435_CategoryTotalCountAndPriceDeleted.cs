using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreApp.API.Migrations
{
    /// <inheritdoc />
    public partial class CategoryTotalCountAndPriceDeleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryTotalCount",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CategoryTotalPrice",
                table: "Categories");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryTotalCount",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "CategoryTotalPrice",
                table: "Categories",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CategoryTotalCount", "CategoryTotalPrice" },
                values: new object[] { 0, 0.0 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CategoryTotalCount", "CategoryTotalPrice" },
                values: new object[] { 0, 0.0 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CategoryTotalCount", "CategoryTotalPrice" },
                values: new object[] { 0, 0.0 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CategoryTotalCount", "CategoryTotalPrice" },
                values: new object[] { 0, 0.0 });
        }
    }
}
