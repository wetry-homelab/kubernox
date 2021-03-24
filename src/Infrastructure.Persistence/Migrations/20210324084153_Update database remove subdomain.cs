using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class Updatedatabaseremovesubdomain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubDomain");

            migrationBuilder.RenameColumn(
                name: "RootDomain",
                table: "DomainName",
                newName: "Value");

            migrationBuilder.AddColumn<string>(
                name: "RootDomainId",
                table: "DomainName",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateAt",
                table: "Cluster",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 24, 8, 41, 52, 511, DateTimeKind.Utc).AddTicks(3434),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2021, 3, 22, 13, 50, 46, 846, DateTimeKind.Utc).AddTicks(1870));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RootDomainId",
                table: "DomainName");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "DomainName",
                newName: "RootDomain");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateAt",
                table: "Cluster",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 22, 13, 50, 46, 846, DateTimeKind.Utc).AddTicks(1870),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2021, 3, 24, 8, 41, 52, 511, DateTimeKind.Utc).AddTicks(3434));

            migrationBuilder.CreateTable(
                name: "SubDomain",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Challenge = table.Column<string>(type: "text", nullable: false),
                    ChallengeType = table.Column<string>(type: "text", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DeleteAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DomainNameId = table.Column<string>(type: "text", nullable: true),
                    ExpiryDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    FullChain = table.Column<string>(type: "text", nullable: true),
                    PrivateKey = table.Column<string>(type: "text", nullable: true),
                    Sub = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubDomain", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubDomain_DomainName_DomainNameId",
                        column: x => x.DomainNameId,
                        principalTable: "DomainName",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubDomain_DomainNameId",
                table: "SubDomain",
                column: "DomainNameId");
        }
    }
}
