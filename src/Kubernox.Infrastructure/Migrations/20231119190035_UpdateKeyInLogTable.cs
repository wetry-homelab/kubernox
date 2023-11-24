using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kubernox.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateKeyInLogTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Key",
                table: "Log",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Key",
                table: "Log");
        }
    }
}
