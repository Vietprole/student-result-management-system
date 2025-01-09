using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Student_Result_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class RemoveHocPhanNganh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HocPhanNganh");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HocPhanNganh",
                columns: table => new
                {
                    HocPhansId = table.Column<int>(type: "int", nullable: false),
                    NganhsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HocPhanNganh", x => new { x.HocPhansId, x.NganhsId });
                    table.ForeignKey(
                        name: "FK_HocPhanNganh_HocPhans_HocPhansId",
                        column: x => x.HocPhansId,
                        principalTable: "HocPhans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HocPhanNganh_Nganhs_NganhsId",
                        column: x => x.NganhsId,
                        principalTable: "Nganhs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HocPhanNganh_NganhsId",
                table: "HocPhanNganh",
                column: "NganhsId");
        }
    }
}
