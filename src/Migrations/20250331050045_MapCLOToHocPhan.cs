using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Student_Result_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class MapCLOToHocPhan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CLOs_LopHocPhans_LopHocPhanId",
                table: "CLOs");

            migrationBuilder.RenameColumn(
                name: "LopHocPhanId",
                table: "CLOs",
                newName: "HocPhanId");

            migrationBuilder.RenameIndex(
                name: "IX_CLOs_LopHocPhanId",
                table: "CLOs",
                newName: "IX_CLOs_HocPhanId");

            migrationBuilder.AddColumn<int>(
                name: "HocKyId",
                table: "CLOs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CLOs_HocKyId",
                table: "CLOs",
                column: "HocKyId");

            migrationBuilder.AddForeignKey(
                name: "FK_CLOs_HocKies_HocKyId",
                table: "CLOs",
                column: "HocKyId",
                principalTable: "HocKies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CLOs_HocPhans_HocPhanId",
                table: "CLOs",
                column: "HocPhanId",
                principalTable: "HocPhans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CLOs_HocKies_HocKyId",
                table: "CLOs");

            migrationBuilder.DropForeignKey(
                name: "FK_CLOs_HocPhans_HocPhanId",
                table: "CLOs");

            migrationBuilder.DropIndex(
                name: "IX_CLOs_HocKyId",
                table: "CLOs");

            migrationBuilder.DropColumn(
                name: "HocKyId",
                table: "CLOs");

            migrationBuilder.RenameColumn(
                name: "HocPhanId",
                table: "CLOs",
                newName: "LopHocPhanId");

            migrationBuilder.RenameIndex(
                name: "IX_CLOs_HocPhanId",
                table: "CLOs",
                newName: "IX_CLOs_LopHocPhanId");

            migrationBuilder.AddForeignKey(
                name: "FK_CLOs_LopHocPhans_LopHocPhanId",
                table: "CLOs",
                column: "LopHocPhanId",
                principalTable: "LopHocPhans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
