using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class UpdateUptimetype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Uptime",
                table: "DatacenterNode",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateAt",
                table: "Cluster",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2021, 2, 14, 22, 41, 25, 91, DateTimeKind.Utc).AddTicks(5965),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2021, 2, 12, 18, 57, 9, 119, DateTimeKind.Utc).AddTicks(235));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Uptime",
                table: "DatacenterNode",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateAt",
                table: "Cluster",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2021, 2, 12, 18, 57, 9, 119, DateTimeKind.Utc).AddTicks(235),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2021, 2, 14, 22, 41, 25, 91, DateTimeKind.Utc).AddTicks(5965));
        }
    }
}
