using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetConnect.Migrations
{
    /// <inheritdoc />
    public partial class AddAnimalTypeAttributes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnimalTypeAttribute_AnimalTypes_AnimalTypeId",
                table: "AnimalTypeAttribute");

            migrationBuilder.DropForeignKey(
                name: "FK_AnimalTypeAttribute_AttributeDefinitions_AttributeDefinitionId",
                table: "AnimalTypeAttribute");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AnimalTypeAttribute",
                table: "AnimalTypeAttribute");

            migrationBuilder.RenameTable(
                name: "AnimalTypeAttribute",
                newName: "AnimalTypeAttributes");

            migrationBuilder.RenameIndex(
                name: "IX_AnimalTypeAttribute_AttributeDefinitionId",
                table: "AnimalTypeAttributes",
                newName: "IX_AnimalTypeAttributes_AttributeDefinitionId");

            migrationBuilder.RenameIndex(
                name: "IX_AnimalTypeAttribute_AnimalTypeId",
                table: "AnimalTypeAttributes",
                newName: "IX_AnimalTypeAttributes_AnimalTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AnimalTypeAttributes",
                table: "AnimalTypeAttributes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AnimalTypeAttributes_AnimalTypes_AnimalTypeId",
                table: "AnimalTypeAttributes",
                column: "AnimalTypeId",
                principalTable: "AnimalTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AnimalTypeAttributes_AttributeDefinitions_AttributeDefinitionId",
                table: "AnimalTypeAttributes",
                column: "AttributeDefinitionId",
                principalTable: "AttributeDefinitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnimalTypeAttributes_AnimalTypes_AnimalTypeId",
                table: "AnimalTypeAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_AnimalTypeAttributes_AttributeDefinitions_AttributeDefinitionId",
                table: "AnimalTypeAttributes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AnimalTypeAttributes",
                table: "AnimalTypeAttributes");

            migrationBuilder.RenameTable(
                name: "AnimalTypeAttributes",
                newName: "AnimalTypeAttribute");

            migrationBuilder.RenameIndex(
                name: "IX_AnimalTypeAttributes_AttributeDefinitionId",
                table: "AnimalTypeAttribute",
                newName: "IX_AnimalTypeAttribute_AttributeDefinitionId");

            migrationBuilder.RenameIndex(
                name: "IX_AnimalTypeAttributes_AnimalTypeId",
                table: "AnimalTypeAttribute",
                newName: "IX_AnimalTypeAttribute_AnimalTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AnimalTypeAttribute",
                table: "AnimalTypeAttribute",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AnimalTypeAttribute_AnimalTypes_AnimalTypeId",
                table: "AnimalTypeAttribute",
                column: "AnimalTypeId",
                principalTable: "AnimalTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AnimalTypeAttribute_AttributeDefinitions_AttributeDefinitionId",
                table: "AnimalTypeAttribute",
                column: "AttributeDefinitionId",
                principalTable: "AttributeDefinitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
