using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AICenterAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddPinMember : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Pin",
                table: "Members",
                type: "tinyint(1)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pin",
                table: "Members");
        }
    }
}
