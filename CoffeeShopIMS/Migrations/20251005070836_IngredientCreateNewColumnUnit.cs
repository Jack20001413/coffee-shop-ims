using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeeShopIMS.Migrations
{
    /// <inheritdoc />
    public partial class IngredientCreateNewColumnUnit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "Ingredients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Unit",
                table: "Ingredients");
        }
    }
}
