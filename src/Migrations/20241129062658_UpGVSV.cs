using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Student_Result_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class UpGVSV : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SinhViens_TaiKhoanId",
                table: "SinhViens");

            migrationBuilder.DropIndex(
                name: "IX_GiangViens_TaiKhoanId",
                table: "GiangViens");

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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "26bf66c8-0edc-417c-a5ef-6c553019fc14", null, "GiangVien", "GIANGVIEN" },
                    { "8e8da6b3-07df-4fd5-b4f7-82f0635a9cb0", null, "TruongKhoa", "TRUONGKHOA" },
                    { "dc608489-0e5e-4945-bca6-688d6cf06400", null, "PhongDaoTao", "PHONGDAOTAO" },
                    { "dcd22d3d-471b-4c95-a0e7-fc9bedf5a3f8", null, "SinhVien", "SINHVIEN" },
                    { "e820b120-4f48-4c64-881b-96cc138400d9", null, "TruongBoMon", "TRUONGBOMON" },
                    { "f1e36b8b-b646-447d-bf82-460672b7ae98", null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_SinhViens_TaiKhoanId",
                table: "SinhViens",
                column: "TaiKhoanId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GiangViens_TaiKhoanId",
                table: "GiangViens",
                column: "TaiKhoanId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SinhViens_TaiKhoanId",
                table: "SinhViens");

            migrationBuilder.DropIndex(
                name: "IX_GiangViens_TaiKhoanId",
                table: "GiangViens");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "26bf66c8-0edc-417c-a5ef-6c553019fc14");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8e8da6b3-07df-4fd5-b4f7-82f0635a9cb0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dc608489-0e5e-4945-bca6-688d6cf06400");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dcd22d3d-471b-4c95-a0e7-fc9bedf5a3f8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e820b120-4f48-4c64-881b-96cc138400d9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f1e36b8b-b646-447d-bf82-460672b7ae98");

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

            migrationBuilder.CreateIndex(
                name: "IX_SinhViens_TaiKhoanId",
                table: "SinhViens",
                column: "TaiKhoanId");

            migrationBuilder.CreateIndex(
                name: "IX_GiangViens_TaiKhoanId",
                table: "GiangViens",
                column: "TaiKhoanId");
        }
    }
}
