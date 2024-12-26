using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Student_Result_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class RemoveFKKetQuaId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiemDinhChinhs_KetQuas_KetQuaId",
                table: "DiemDinhChinhs");

            migrationBuilder.DropIndex(
                name: "IX_DiemDinhChinhs_KetQuaId",
                table: "DiemDinhChinhs");

            migrationBuilder.DropColumn(
                name: "KetQuaId",
                table: "DiemDinhChinhs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KetQuaId",
                table: "DiemDinhChinhs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DiemDinhChinhs_KetQuaId",
                table: "DiemDinhChinhs",
                column: "KetQuaId");

            migrationBuilder.AddForeignKey(
                name: "FK_DiemDinhChinhs_KetQuas_KetQuaId",
                table: "DiemDinhChinhs",
                column: "KetQuaId",
                principalTable: "KetQuas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
