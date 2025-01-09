using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Student_Result_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class RemoveLaCotLoiHocPhan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LaCotLoi",
                table: "HocPhans");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "LaCotLoi",
                table: "HocPhans",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
