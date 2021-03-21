using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class UpdateSSHKeyjointure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SshKey",
                table: "Cluster",
                newName: "BaseTemplate");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateAt",
                table: "Cluster",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 21, 21, 41, 9, 406, DateTimeKind.Utc).AddTicks(2860),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2021, 2, 14, 22, 41, 25, 91, DateTimeKind.Utc).AddTicks(5965));

            migrationBuilder.AddColumn<int>(
                name: "SshKeyId",
                table: "Cluster",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cluster_SshKeyId",
                table: "Cluster",
                column: "SshKeyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cluster_SshKey_SshKeyId",
                table: "Cluster",
                column: "SshKeyId",
                principalTable: "SshKey",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cluster_SshKey_SshKeyId",
                table: "Cluster");

            migrationBuilder.DropIndex(
                name: "IX_Cluster_SshKeyId",
                table: "Cluster");

            migrationBuilder.DropColumn(
                name: "SshKeyId",
                table: "Cluster");

            migrationBuilder.RenameColumn(
                name: "BaseTemplate",
                table: "Cluster",
                newName: "SshKey");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateAt",
                table: "Cluster",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2021, 2, 14, 22, 41, 25, 91, DateTimeKind.Utc).AddTicks(5965),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2021, 3, 21, 21, 41, 9, 406, DateTimeKind.Utc).AddTicks(2860));
        }
    }
}
