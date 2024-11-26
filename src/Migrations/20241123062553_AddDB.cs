using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Student_Result_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class AddDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "05a64a12-4389-45d8-b821-b6e478d3c939");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3cea1710-2785-4f4b-bc8e-ba5dd6d9ab89");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "46f92d83-7fb8-4b3f-8bef-7744315be62a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4e4024f2-7402-4c3f-aa45-341a5202e4e1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "94ec69c3-3f7f-42d9-8c98-9c7086001ebe");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b2c5bdfa-2406-4d17-945e-f19654d88a73");

            migrationBuilder.AddColumn<int>(
                name: "KhoaId",
                table: "SinhViens",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "09a5b334-2568-456a-916d-4e5eb3295a28", null, "TruongKhoa", "TRUONGKHOA" },
                    { "1721eb95-3e84-4b15-aac8-7101084977d7", null, "PhongDaoTao", "PHONGDAOTAO" },
                    { "782dfb73-d700-4f13-844c-39568485653e", null, "TruongBoMon", "TRUONGBOMON" },
                    { "83678753-7192-4ec0-87b4-6a3ff128091d", null, "SinhVien", "SINHVIEN" },
                    { "a84918cb-3c7b-4fe9-90dd-68f1731f9b97", null, "GiangVien", "GIANGVIEN" },
                    { "f963a90d-f990-45fa-8cae-0d962e1e1f06", null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_SinhViens_KhoaId",
                table: "SinhViens",
                column: "KhoaId");

            migrationBuilder.AddForeignKey(
                name: "FK_SinhViens_Khoas_KhoaId",
                table: "SinhViens",
                column: "KhoaId",
                principalTable: "Khoas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SinhViens_Khoas_KhoaId",
                table: "SinhViens");

            migrationBuilder.DropIndex(
                name: "IX_SinhViens_KhoaId",
                table: "SinhViens");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "09a5b334-2568-456a-916d-4e5eb3295a28");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1721eb95-3e84-4b15-aac8-7101084977d7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "782dfb73-d700-4f13-844c-39568485653e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "83678753-7192-4ec0-87b4-6a3ff128091d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a84918cb-3c7b-4fe9-90dd-68f1731f9b97");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f963a90d-f990-45fa-8cae-0d962e1e1f06");

            migrationBuilder.DropColumn(
                name: "KhoaId",
                table: "SinhViens");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "05a64a12-4389-45d8-b821-b6e478d3c939", null, "GiangVien", "GIANGVIEN" },
                    { "3cea1710-2785-4f4b-bc8e-ba5dd6d9ab89", null, "TruongKhoa", "TRUONGKHOA" },
                    { "46f92d83-7fb8-4b3f-8bef-7744315be62a", null, "TruongBoMon", "TRUONGBOMON" },
                    { "4e4024f2-7402-4c3f-aa45-341a5202e4e1", null, "PhongDaoTao", "PHONGDAOTAO" },
                    { "94ec69c3-3f7f-42d9-8c98-9c7086001ebe", null, "SinhVien", "SINHVIEN" },
                    { "b2c5bdfa-2406-4d17-945e-f19654d88a73", null, "Admin", "ADMIN" }
                });
        }
    }
}
