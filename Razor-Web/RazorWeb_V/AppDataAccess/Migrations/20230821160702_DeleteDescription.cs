using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class DeleteDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "FoodType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "FoodType",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
