using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreApp.API.Migrations
{
    /// <inheritdoc />
    public partial class CategoryIsActiveAdded2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Categories",
                newName: "IsDeleted");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Categories",
                newName: "IsActive");
        }
    }
}
