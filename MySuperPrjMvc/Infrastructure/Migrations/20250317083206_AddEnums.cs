using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEnums : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryAttributes_Categories_CategoryId",
                table: "CategoryAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategoryAttributes_CategoryAttributes_CategoryAttributeId",
                table: "ProductCategoryAttributes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryAttributes",
                table: "CategoryAttributes");

            migrationBuilder.RenameTable(
                name: "CategoryAttributes",
                newName: "CategoryAttribute");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryAttributes_CategoryId",
                table: "CategoryAttribute",
                newName: "IX_CategoryAttribute_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryAttribute",
                table: "CategoryAttribute",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryAttribute_Categories_CategoryId",
                table: "CategoryAttribute",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategoryAttributes_CategoryAttribute_CategoryAttributeId",
                table: "ProductCategoryAttributes",
                column: "CategoryAttributeId",
                principalTable: "CategoryAttribute",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryAttribute_Categories_CategoryId",
                table: "CategoryAttribute");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategoryAttributes_CategoryAttribute_CategoryAttributeId",
                table: "ProductCategoryAttributes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryAttribute",
                table: "CategoryAttribute");

            migrationBuilder.RenameTable(
                name: "CategoryAttribute",
                newName: "CategoryAttributes");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryAttribute_CategoryId",
                table: "CategoryAttributes",
                newName: "IX_CategoryAttributes_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryAttributes",
                table: "CategoryAttributes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryAttributes_Categories_CategoryId",
                table: "CategoryAttributes",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategoryAttributes_CategoryAttributes_CategoryAttributeId",
                table: "ProductCategoryAttributes",
                column: "CategoryAttributeId",
                principalTable: "CategoryAttributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
