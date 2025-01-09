using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Student_Result_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class CtdtJoinTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ctdts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NganhId = table.Column<int>(type: "int", nullable: false),
                    HocPhanId = table.Column<int>(type: "int", nullable: false),
                    LaCotLoi = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ctdts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ctdts_HocPhans_HocPhanId",
                        column: x => x.HocPhanId,
                        principalTable: "HocPhans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ctdts_Nganhs_NganhId",
                        column: x => x.NganhId,
                        principalTable: "Nganhs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ctdts_HocPhanId",
                table: "Ctdts",
                column: "HocPhanId");

            migrationBuilder.CreateIndex(
                name: "IX_Ctdts_NganhId",
                table: "Ctdts",
                column: "NganhId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ctdts");
        }
    }
}
