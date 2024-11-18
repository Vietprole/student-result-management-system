using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Student_Result_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class AddGiangVienLopHocPhan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GiangViens_Khoas_KhoaId",
                table: "GiangViens");

            migrationBuilder.DropForeignKey(
                name: "FK_HocPhans_Khoas_KhoaId",
                table: "HocPhans");

            migrationBuilder.DropForeignKey(
                name: "FK_LopHocPhans_HocKies_HocKyId",
                table: "LopHocPhans");

            migrationBuilder.DropTable(
                name: "HocKies");

            migrationBuilder.DropIndex(
                name: "IX_LopHocPhans_HocKyId",
                table: "LopHocPhans");

            migrationBuilder.DropColumn(
                name: "HocKyId",
                table: "LopHocPhans");

            migrationBuilder.AddColumn<int>(
                name: "KhoaId",
                table: "Nganhs",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "KhoaId",
                table: "HocPhans",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "KhoaId",
                table: "GiangViens",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "GiangVienLopHocPhan",
                columns: table => new
                {
                    GiangViensId = table.Column<int>(type: "int", nullable: false),
                    LopHocPhansId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiangVienLopHocPhan", x => new { x.GiangViensId, x.LopHocPhansId });
                    table.ForeignKey(
                        name: "FK_GiangVienLopHocPhan_GiangViens_GiangViensId",
                        column: x => x.GiangViensId,
                        principalTable: "GiangViens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GiangVienLopHocPhan_LopHocPhans_LopHocPhansId",
                        column: x => x.LopHocPhansId,
                        principalTable: "LopHocPhans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GiangVienLopHocPhan_LopHocPhansId",
                table: "GiangVienLopHocPhan",
                column: "LopHocPhansId");

            migrationBuilder.AddForeignKey(
                name: "FK_GiangViens_Khoas_KhoaId",
                table: "GiangViens",
                column: "KhoaId",
                principalTable: "Khoas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HocPhans_Khoas_KhoaId",
                table: "HocPhans",
                column: "KhoaId",
                principalTable: "Khoas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GiangViens_Khoas_KhoaId",
                table: "GiangViens");

            migrationBuilder.DropForeignKey(
                name: "FK_HocPhans_Khoas_KhoaId",
                table: "HocPhans");

            migrationBuilder.DropTable(
                name: "GiangVienLopHocPhan");

            migrationBuilder.DropColumn(
                name: "KhoaId",
                table: "Nganhs");

            migrationBuilder.AddColumn<int>(
                name: "HocKyId",
                table: "LopHocPhans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "KhoaId",
                table: "HocPhans",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "KhoaId",
                table: "GiangViens",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "HocKies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HocKies", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LopHocPhans_HocKyId",
                table: "LopHocPhans",
                column: "HocKyId");

            migrationBuilder.AddForeignKey(
                name: "FK_GiangViens_Khoas_KhoaId",
                table: "GiangViens",
                column: "KhoaId",
                principalTable: "Khoas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HocPhans_Khoas_KhoaId",
                table: "HocPhans",
                column: "KhoaId",
                principalTable: "Khoas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LopHocPhans_HocKies_HocKyId",
                table: "LopHocPhans",
                column: "HocKyId",
                principalTable: "HocKies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
