using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Student_Result_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class UpdateGVSV : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "Ten",
                table: "SinhViens");

            migrationBuilder.DropColumn(
                name: "Ten",
                table: "GiangViens");

            migrationBuilder.AddColumn<string>(
                name: "HovaTen",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "32aa7f2a-2eee-44d0-afcc-4a1fb3038c89", null, "Admin", "ADMIN" },
                    { "4d295e9f-a5c4-4add-930e-e27716410262", null, "TruongKhoa", "TRUONGKHOA" },
                    { "85ae881d-834e-4b67-886e-665c53adcabc", null, "PhongDaoTao", "PHONGDAOTAO" },
                    { "b02a20c0-c770-4385-abf9-069e53e26c40", null, "GiangVien", "GIANGVIEN" },
                    { "b9c89950-df76-4ef3-bbee-ba72e255e2ef", null, "SinhVien", "SINHVIEN" },
                    { "e24578ef-8c64-46d5-bc6b-f9fa4ad766ac", null, "TruongBoMon", "TRUONGBOMON" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "32aa7f2a-2eee-44d0-afcc-4a1fb3038c89");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4d295e9f-a5c4-4add-930e-e27716410262");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "85ae881d-834e-4b67-886e-665c53adcabc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b02a20c0-c770-4385-abf9-069e53e26c40");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b9c89950-df76-4ef3-bbee-ba72e255e2ef");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e24578ef-8c64-46d5-bc6b-f9fa4ad766ac");

            migrationBuilder.DropColumn(
                name: "HovaTen",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "Ten",
                table: "SinhViens",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Ten",
                table: "GiangViens",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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
        }
    }
}
