using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Student_Result_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class CTDTDeletionBehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PLOs_CTDTId",
                table: "PLOs",
                column: "CTDTId");

            migrationBuilder.AddForeignKey(
                name: "FK_PLOs_CTDTs_CTDTId",
                table: "PLOs",
                column: "CTDTId",
                principalTable: "CTDTs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PLOs_CTDTs_CTDTId",
                table: "PLOs");

            migrationBuilder.DropIndex(
                name: "IX_PLOs_CTDTId",
                table: "PLOs");
        }
    }
}
