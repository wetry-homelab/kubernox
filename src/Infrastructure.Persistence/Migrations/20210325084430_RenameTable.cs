using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class RenameTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DomainName");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateAt",
                table: "Cluster",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 25, 8, 44, 30, 358, DateTimeKind.Utc).AddTicks(6560),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2021, 3, 24, 20, 24, 26, 762, DateTimeKind.Utc).AddTicks(3640));

            migrationBuilder.CreateTable(
                name: "ClusterDomain",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Value = table.Column<string>(type: "text", nullable: false),
                    RootDomainId = table.Column<string>(type: "text", nullable: true),
                    ClusterId = table.Column<string>(type: "text", nullable: true),
                    Resolver = table.Column<string>(type: "text", nullable: true),
                    FullChain = table.Column<string>(type: "text", nullable: true),
                    PrivateKey = table.Column<string>(type: "text", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DeleteAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClusterDomain", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Domain",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Value = table.Column<string>(type: "text", nullable: false),
                    ValidationKey = table.Column<string>(type: "text", nullable: false),
                    ValidationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DeleteAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Domain", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClusterDomain");

            migrationBuilder.DropTable(
                name: "Domain");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateAt",
                table: "Cluster",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 24, 20, 24, 26, 762, DateTimeKind.Utc).AddTicks(3640),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2021, 3, 25, 8, 44, 30, 358, DateTimeKind.Utc).AddTicks(6560));

            migrationBuilder.CreateTable(
                name: "DomainName",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ClusterId = table.Column<string>(type: "text", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DeleteAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    RootDomainId = table.Column<string>(type: "text", nullable: true),
                    ValidationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ValidationKey = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DomainName", x => x.Id);
                });
        }
    }
}
