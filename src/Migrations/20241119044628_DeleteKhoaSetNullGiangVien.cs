using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Student_Result_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class DeleteKhoaSetNullGiangVien : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GiangViens_Khoas_KhoaId",
                table: "GiangViens");

            migrationBuilder.AddForeignKey(
                name: "FK_GiangViens_Khoas_KhoaId",
                table: "GiangViens",
                column: "KhoaId",
                principalTable: "Khoas",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GiangViens_Khoas_KhoaId",
                table: "GiangViens");

            migrationBuilder.AddForeignKey(
                name: "FK_GiangViens_Khoas_KhoaId",
                table: "GiangViens",
                column: "KhoaId",
                principalTable: "Khoas",
                principalColumn: "Id");
        }
    }
}
