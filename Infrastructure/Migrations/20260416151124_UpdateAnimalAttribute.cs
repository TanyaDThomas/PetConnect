using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetConnect.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAnimalAttribute : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "AnimalAttributes",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Value",
                table: "AnimalAttributes");
        }
    }
}
