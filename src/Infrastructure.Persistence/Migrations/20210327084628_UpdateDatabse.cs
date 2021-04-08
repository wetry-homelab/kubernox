using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class UpdateDatabse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "ClusterDomain",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "RootDomainId",
                table: "ClusterDomain",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Resolver",
                table: "ClusterDomain",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ClusterId",
                table: "ClusterDomain",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateAt",
                table: "Cluster",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 27, 8, 46, 27, 605, DateTimeKind.Utc).AddTicks(1127),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2021, 3, 25, 8, 44, 30, 358, DateTimeKind.Utc).AddTicks(6560));

            migrationBuilder.CreateTable(
                name: "TraefikRouteValue",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ClusterId = table.Column<string>(type: "text", nullable: false),
                    RuleId = table.Column<string>(type: "text", nullable: true),
                    Domain = table.Column<string>(type: "text", nullable: true),
                    Key = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TraefikRouteValue", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TraefikRouteValue");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "ClusterDomain",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RootDomainId",
                table: "ClusterDomain",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Resolver",
                table: "ClusterDomain",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "ClusterId",
                table: "ClusterDomain",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateAt",
                table: "Cluster",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 25, 8, 44, 30, 358, DateTimeKind.Utc).AddTicks(6560),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2021, 3, 27, 8, 46, 27, 605, DateTimeKind.Utc).AddTicks(1127));
        }
    }
}
