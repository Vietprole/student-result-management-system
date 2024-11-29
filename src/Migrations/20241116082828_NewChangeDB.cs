using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Student_Result_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class NewChangeDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaiKhoans_ChucVu_ChucVuId",
                table: "TaiKhoans");

            migrationBuilder.DropTable(
                name: "ChucVuPhanQuyen");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChucVu",
                table: "ChucVu");

            migrationBuilder.RenameTable(
                name: "ChucVu",
                newName: "ChucVus");

            migrationBuilder.AddColumn<int>(
                name: "ChucVuId",
                table: "PhanQuyens",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChucVus",
                table: "ChucVus",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_PhanQuyens_ChucVuId",
                table: "PhanQuyens",
                column: "ChucVuId");

            migrationBuilder.AddForeignKey(
                name: "FK_PhanQuyens_ChucVus_ChucVuId",
                table: "PhanQuyens",
                column: "ChucVuId",
                principalTable: "ChucVus",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaiKhoans_ChucVus_ChucVuId",
                table: "TaiKhoans",
                column: "ChucVuId",
                principalTable: "ChucVus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhanQuyens_ChucVus_ChucVuId",
                table: "PhanQuyens");

            migrationBuilder.DropForeignKey(
                name: "FK_TaiKhoans_ChucVus_ChucVuId",
                table: "TaiKhoans");

            migrationBuilder.DropIndex(
                name: "IX_PhanQuyens_ChucVuId",
                table: "PhanQuyens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChucVus",
                table: "ChucVus");

            migrationBuilder.DropColumn(
                name: "ChucVuId",
                table: "PhanQuyens");

            migrationBuilder.RenameTable(
                name: "ChucVus",
                newName: "ChucVu");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChucVu",
                table: "ChucVu",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ChucVuPhanQuyen",
                columns: table => new
                {
                    ChucVusId = table.Column<int>(type: "int", nullable: false),
                    PhanQuyensId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChucVuPhanQuyen", x => new { x.ChucVusId, x.PhanQuyensId });
                    table.ForeignKey(
                        name: "FK_ChucVuPhanQuyen_ChucVu_ChucVusId",
                        column: x => x.ChucVusId,
                        principalTable: "ChucVu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChucVuPhanQuyen_PhanQuyens_PhanQuyensId",
                        column: x => x.PhanQuyensId,
                        principalTable: "PhanQuyens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChucVuPhanQuyen_PhanQuyensId",
                table: "ChucVuPhanQuyen",
                column: "PhanQuyensId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaiKhoans_ChucVu_ChucVuId",
                table: "TaiKhoans",
                column: "ChucVuId",
                principalTable: "ChucVu",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
