using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopMVC.Data.Migrations
{
    /// <inheritdoc />
    public partial class changeddesriptionlength371chars : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Item",
                type: "nvarchar(371)",
                maxLength: 371,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Item",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(371)",
                oldMaxLength: 371,
                oldNullable: true);
        }
    }
}
