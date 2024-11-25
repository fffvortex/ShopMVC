using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopMVC.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedstats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Stats",
                table: "Item",
                type: "nvarchar(170)",
                maxLength: 170,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stats",
                table: "Item");
        }
    }
}
