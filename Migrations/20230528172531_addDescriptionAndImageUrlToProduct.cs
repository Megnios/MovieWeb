using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieWeb.Migrations
{
    /// <inheritdoc />
    public partial class addDescriptionAndImageUrlToProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "imageUrl",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "imageUrl" },
                values: new object[] { "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec ornare tortor ligula, nec congue justo vulputate sed. Sed convallis tellus.", "" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "imageUrl" },
                values: new object[] { "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec ornare tortor ligula, nec congue justo vulputate sed. Sed convallis tellus.", "" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "imageUrl" },
                values: new object[] { "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec ornare tortor ligula, nec congue justo vulputate sed. Sed convallis tellus.", "" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "imageUrl",
                table: "Products");
        }
    }
}
