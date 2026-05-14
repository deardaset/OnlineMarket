using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineMarket.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPhotoUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "Products",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "Products");
        }
    }
}
