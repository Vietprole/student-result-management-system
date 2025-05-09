using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Student_Result_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class AddTaiKhoanToNganh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TaiKhoanId",
                table: "Nganhs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Nganhs_TaiKhoanId",
                table: "Nganhs",
                column: "TaiKhoanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Nganhs_TaiKhoans_TaiKhoanId",
                table: "Nganhs",
                column: "TaiKhoanId",
                principalTable: "TaiKhoans",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nganhs_TaiKhoans_TaiKhoanId",
                table: "Nganhs");

            migrationBuilder.DropIndex(
                name: "IX_Nganhs_TaiKhoanId",
                table: "Nganhs");

            migrationBuilder.DropColumn(
                name: "TaiKhoanId",
                table: "Nganhs");
        }
    }
}
