using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Student_Result_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class ModifyHocPhanToNganhId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
