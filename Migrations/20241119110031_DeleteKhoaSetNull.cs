using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Student_Result_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class DeleteKhoaSetNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HocPhans_Khoas_KhoaId",
                table: "HocPhans");

            migrationBuilder.CreateIndex(
                name: "IX_Nganhs_KhoaId",
                table: "Nganhs",
                column: "KhoaId");

            migrationBuilder.AddForeignKey(
                name: "FK_HocPhans_Khoas_KhoaId",
                table: "HocPhans",
                column: "KhoaId",
                principalTable: "Khoas",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Nganhs_Khoas_KhoaId",
                table: "Nganhs",
                column: "KhoaId",
                principalTable: "Khoas",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HocPhans_Khoas_KhoaId",
                table: "HocPhans");

            migrationBuilder.DropForeignKey(
                name: "FK_Nganhs_Khoas_KhoaId",
                table: "Nganhs");

            migrationBuilder.DropIndex(
                name: "IX_Nganhs_KhoaId",
                table: "Nganhs");

            migrationBuilder.AddForeignKey(
                name: "FK_HocPhans_Khoas_KhoaId",
                table: "HocPhans",
                column: "KhoaId",
                principalTable: "Khoas",
                principalColumn: "Id");
        }
    }
}
