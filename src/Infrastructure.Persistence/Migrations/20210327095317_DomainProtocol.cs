using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class DomainProtocol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Protocol",
                table: "ClusterDomain",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateAt",
                table: "Cluster",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 27, 9, 53, 16, 766, DateTimeKind.Utc).AddTicks(6744),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2021, 3, 27, 8, 46, 27, 605, DateTimeKind.Utc).AddTicks(1127));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Protocol",
                table: "ClusterDomain");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateAt",
                table: "Cluster",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 27, 8, 46, 27, 605, DateTimeKind.Utc).AddTicks(1127),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2021, 3, 27, 9, 53, 16, 766, DateTimeKind.Utc).AddTicks(6744));
        }
    }
}
