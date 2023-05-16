using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VisitorManagement.API.Migrations
{
    /// <inheritdoc />
    public partial class Managementmigt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VisitorLogsDetails_ResidentDetails_ResidentId",
                table: "VisitorLogsDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_VisitorLogsDetails_Visitordetails_VisitorId",
                table: "VisitorLogsDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VisitorLogsDetails",
                table: "VisitorLogsDetails");

            migrationBuilder.RenameTable(
                name: "VisitorLogsDetails",
                newName: "LogDetails");

            migrationBuilder.RenameIndex(
                name: "IX_VisitorLogsDetails_VisitorId",
                table: "LogDetails",
                newName: "IX_LogDetails_VisitorId");

            migrationBuilder.RenameIndex(
                name: "IX_VisitorLogsDetails_ResidentId",
                table: "LogDetails",
                newName: "IX_LogDetails_ResidentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LogDetails",
                table: "LogDetails",
                column: "VisitorLogId");

            migrationBuilder.AddForeignKey(
                name: "FK_LogDetails_ResidentDetails_ResidentId",
                table: "LogDetails",
                column: "ResidentId",
                principalTable: "ResidentDetails",
                principalColumn: "ResidentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LogDetails_Visitordetails_VisitorId",
                table: "LogDetails",
                column: "VisitorId",
                principalTable: "Visitordetails",
                principalColumn: "VisitorId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LogDetails_ResidentDetails_ResidentId",
                table: "LogDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_LogDetails_Visitordetails_VisitorId",
                table: "LogDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LogDetails",
                table: "LogDetails");

            migrationBuilder.RenameTable(
                name: "LogDetails",
                newName: "VisitorLogsDetails");

            migrationBuilder.RenameIndex(
                name: "IX_LogDetails_VisitorId",
                table: "VisitorLogsDetails",
                newName: "IX_VisitorLogsDetails_VisitorId");

            migrationBuilder.RenameIndex(
                name: "IX_LogDetails_ResidentId",
                table: "VisitorLogsDetails",
                newName: "IX_VisitorLogsDetails_ResidentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VisitorLogsDetails",
                table: "VisitorLogsDetails",
                column: "VisitorLogId");

            migrationBuilder.AddForeignKey(
                name: "FK_VisitorLogsDetails_ResidentDetails_ResidentId",
                table: "VisitorLogsDetails",
                column: "ResidentId",
                principalTable: "ResidentDetails",
                principalColumn: "ResidentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VisitorLogsDetails_Visitordetails_VisitorId",
                table: "VisitorLogsDetails",
                column: "VisitorId",
                principalTable: "Visitordetails",
                principalColumn: "VisitorId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
