using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Student_Result_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class OptionalHocKyOfCLO : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "HocKyId",
                table: "CLOs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "HocKyId",
                table: "CLOs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
