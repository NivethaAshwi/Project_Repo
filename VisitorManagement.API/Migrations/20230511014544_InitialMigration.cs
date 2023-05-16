using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VisitorManagement.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VisitorCategoryDetail",
                columns: table => new
                {
                    VisitorCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    categoryDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitorCategoryDetail", x => x.VisitorCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Visitordetails",
                columns: table => new
                {
                    VisitorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VisitorFirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisitorLastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisitorCategoryId = table.Column<int>(type: "int", nullable: false),
                    MobileNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VehicleNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visitordetails", x => x.VisitorId);
                    table.ForeignKey(
                        name: "FK_Visitordetails_VisitorCategoryDetail_VisitorCategoryId",
                        column: x => x.VisitorCategoryId,
                        principalTable: "VisitorCategoryDetail",
                        principalColumn: "VisitorCategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Visitordetails_VisitorCategoryId",
                table: "Visitordetails",
                column: "VisitorCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Visitordetails");

            migrationBuilder.DropTable(
                name: "VisitorCategoryDetail");
        }
    }
}
