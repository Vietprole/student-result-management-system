using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Student_Result_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class DeleteNganhSetNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CTDTs_Nganhs_NganhId",
                table: "CTDTs");

            migrationBuilder.AddForeignKey(
                name: "FK_CTDTs_Nganhs_NganhId",
                table: "CTDTs",
                column: "NganhId",
                principalTable: "Nganhs",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CTDTs_Nganhs_NganhId",
                table: "CTDTs");

            migrationBuilder.AddForeignKey(
                name: "FK_CTDTs_Nganhs_NganhId",
                table: "CTDTs",
                column: "NganhId",
                principalTable: "Nganhs",
                principalColumn: "Id");
        }
    }
}
