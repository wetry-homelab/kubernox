using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kubernox.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSystemInfos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "Node",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreateBy",
                table: "Node",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteAt",
                table: "Node",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeleteBy",
                table: "Node",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaxCpu",
                table: "Node",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "MaxMemory",
                table: "Node",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAt",
                table: "Node",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdateBy",
                table: "Node",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "Node");

            migrationBuilder.DropColumn(
                name: "CreateBy",
                table: "Node");

            migrationBuilder.DropColumn(
                name: "DeleteAt",
                table: "Node");

            migrationBuilder.DropColumn(
                name: "DeleteBy",
                table: "Node");

            migrationBuilder.DropColumn(
                name: "MaxCpu",
                table: "Node");

            migrationBuilder.DropColumn(
                name: "MaxMemory",
                table: "Node");

            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "Node");

            migrationBuilder.DropColumn(
                name: "UpdateBy",
                table: "Node");
        }
    }
}
