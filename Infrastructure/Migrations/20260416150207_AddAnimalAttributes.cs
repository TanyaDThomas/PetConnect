using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetConnect.Migrations
{
    /// <inheritdoc />
    public partial class AddAnimalAttributes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "AnimalAttributes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "AnimalAttributes");
        }
    }
}
