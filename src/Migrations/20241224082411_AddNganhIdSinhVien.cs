using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Student_Result_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class AddNganhIdSinhVien : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NganhId",
                table: "SinhViens",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SinhViens_NganhId",
                table: "SinhViens",
                column: "NganhId");

            migrationBuilder.AddForeignKey(
                name: "FK_SinhViens_Nganhs_NganhId",
                table: "SinhViens",
                column: "NganhId",
                principalTable: "Nganhs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SinhViens_Nganhs_NganhId",
                table: "SinhViens");

            migrationBuilder.DropIndex(
                name: "IX_SinhViens_NganhId",
                table: "SinhViens");

            migrationBuilder.DropColumn(
                name: "NganhId",
                table: "SinhViens");
        }
    }
}
