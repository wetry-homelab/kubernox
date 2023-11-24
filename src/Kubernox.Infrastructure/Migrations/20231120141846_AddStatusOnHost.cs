using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kubernox.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusOnHost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "HostConfiguration",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "HostConfiguration");
        }
    }
}
