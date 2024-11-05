using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Student_Result_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class AddLopHocPhanIdInCLO : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CLOCauHoi_CLOs_CLOsId",
                table: "CLOCauHoi");

            migrationBuilder.DropForeignKey(
                name: "FK_CLOs_LopHocPhans_LopHocPhanId",
                table: "CLOs");

            migrationBuilder.AlterColumn<int>(
                name: "LopHocPhanId",
                table: "CLOs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CLOCauHoi_CLOs_CLOsId",
                table: "CLOCauHoi",
                column: "CLOsId",
                principalTable: "CLOs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CLOs_LopHocPhans_LopHocPhanId",
                table: "CLOs",
                column: "LopHocPhanId",
                principalTable: "LopHocPhans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CLOCauHoi_CLOs_CLOsId",
                table: "CLOCauHoi");

            migrationBuilder.DropForeignKey(
                name: "FK_CLOs_LopHocPhans_LopHocPhanId",
                table: "CLOs");

            migrationBuilder.AlterColumn<int>(
                name: "LopHocPhanId",
                table: "CLOs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_CLOCauHoi_CLOs_CLOsId",
                table: "CLOCauHoi",
                column: "CLOsId",
                principalTable: "CLOs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CLOs_LopHocPhans_LopHocPhanId",
                table: "CLOs",
                column: "LopHocPhanId",
                principalTable: "LopHocPhans",
                principalColumn: "Id");
        }
    }
}
