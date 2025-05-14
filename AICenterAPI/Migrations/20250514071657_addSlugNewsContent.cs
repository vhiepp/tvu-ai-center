using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AICenterAPI.Migrations
{
    /// <inheritdoc />
    public partial class addSlugNewsContent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "News_Contents",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Slug",
                table: "News_Contents");
        }
    }
}
