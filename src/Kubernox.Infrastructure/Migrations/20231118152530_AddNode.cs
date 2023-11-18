using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kubernox.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ClusterConfigurationId",
                table: "Node",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "NodeId",
                table: "Node",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClusterConfigurationId",
                table: "Node");

            migrationBuilder.DropColumn(
                name: "NodeId",
                table: "Node");
        }
    }
}
