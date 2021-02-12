using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Infrastructure.Persistence.Migrations
{
    public partial class IDasString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cluster",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ProxmoxNodeId = table.Column<int>(type: "integer", nullable: false),
                    Node = table.Column<int>(type: "integer", nullable: false),
                    Cpu = table.Column<int>(type: "integer", nullable: false),
                    Memory = table.Column<int>(type: "integer", nullable: false),
                    Storage = table.Column<int>(type: "integer", nullable: false),
                    Ip = table.Column<string>(type: "text", nullable: false),
                    KubeConfig = table.Column<string>(type: "text", nullable: true),
                    KubeConfigJson = table.Column<string>(type: "text", nullable: true),
                    SshKey = table.Column<string>(type: "text", nullable: false),
                    User = table.Column<string>(type: "text", nullable: false),
                    State = table.Column<string>(type: "text", nullable: false, defaultValue: "Provisionning"),
                    CreateAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValue: new DateTime(2021, 2, 12, 0, 48, 40, 111, DateTimeKind.Utc).AddTicks(6879)),
                    DeleteAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cluster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DatacenterNode",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Ip = table.Column<string>(type: "text", nullable: false),
                    Online = table.Column<bool>(type: "boolean", nullable: false),
                    PveVersion = table.Column<string>(type: "text", nullable: true),
                    KernelVersion = table.Column<string>(type: "text", nullable: true),
                    Uptime = table.Column<int>(type: "integer", nullable: false),
                    Mhz = table.Column<double>(type: "double precision", nullable: false),
                    Model = table.Column<string>(type: "text", nullable: true),
                    Hvm = table.Column<bool>(type: "boolean", nullable: false),
                    Core = table.Column<int>(type: "integer", nullable: false),
                    UserHz = table.Column<int>(type: "integer", nullable: false),
                    Socket = table.Column<int>(type: "integer", nullable: false),
                    Flag = table.Column<string>(type: "text", nullable: true),
                    Thread = table.Column<int>(type: "integer", nullable: false),
                    RootFsUsed = table.Column<long>(type: "bigint", nullable: false),
                    RootFsTotal = table.Column<long>(type: "bigint", nullable: false),
                    RootFsFree = table.Column<long>(type: "bigint", nullable: false),
                    RootFsAvailable = table.Column<long>(type: "bigint", nullable: false),
                    RamTotal = table.Column<long>(type: "bigint", nullable: false),
                    RamFree = table.Column<long>(type: "bigint", nullable: false),
                    SwapUsed = table.Column<long>(type: "bigint", nullable: false),
                    SwapTotal = table.Column<long>(type: "bigint", nullable: false),
                    SwapFree = table.Column<long>(type: "bigint", nullable: false),
                    RamUsed = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatacenterNode", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Metric",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CpuValue = table.Column<long>(type: "bigint", nullable: false),
                    MemoryValue = table.Column<long>(type: "bigint", nullable: false),
                    EntityId = table.Column<string>(type: "text", nullable: true),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Metric", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SshKey",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Public = table.Column<string>(type: "text", nullable: false),
                    Private = table.Column<string>(type: "text", nullable: true),
                    Fingerprint = table.Column<string>(type: "text", nullable: false),
                    Pem = table.Column<string>(type: "text", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DeleteAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SshKey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Template",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    BaseTemplate = table.Column<string>(type: "text", nullable: false),
                    CpuCount = table.Column<int>(type: "integer", nullable: false),
                    MemoryCount = table.Column<int>(type: "integer", nullable: false),
                    DiskSpace = table.Column<int>(type: "integer", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DeleteAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Template", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClusterNode",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ClusterId = table.Column<string>(type: "text", nullable: true),
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    Ip = table.Column<string>(type: "text", nullable: false),
                    State = table.Column<string>(type: "text", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DeleteAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClusterNode", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClusterNode_Cluster_ClusterId",
                        column: x => x.ClusterId,
                        principalTable: "Cluster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClusterNode_ClusterId",
                table: "ClusterNode",
                column: "ClusterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClusterNode");

            migrationBuilder.DropTable(
                name: "DatacenterNode");

            migrationBuilder.DropTable(
                name: "Metric");

            migrationBuilder.DropTable(
                name: "SshKey");

            migrationBuilder.DropTable(
                name: "Template");

            migrationBuilder.DropTable(
                name: "Cluster");
        }
    }
}
