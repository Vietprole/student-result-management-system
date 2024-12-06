using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Student_Result_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class ModifyHocPhanToKhoaId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HocPhans_Nganhs_NganhId",
                table: "HocPhans");

            migrationBuilder.DropIndex(
                name: "IX_HocPhans_NganhId",
                table: "HocPhans");

            migrationBuilder.DropColumn(
                name: "NganhId",
                table: "HocPhans");

            migrationBuilder.AlterColumn<float>(
                name: "SoTinChi",
                table: "HocPhans",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SoTinChi",
                table: "HocPhans",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddColumn<int>(
                name: "NganhId",
                table: "HocPhans",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HocPhans_NganhId",
                table: "HocPhans",
                column: "NganhId");

            migrationBuilder.AddForeignKey(
                name: "FK_HocPhans_Nganhs_NganhId",
                table: "HocPhans",
                column: "NganhId",
                principalTable: "Nganhs",
                principalColumn: "Id");
        }
    }
}
