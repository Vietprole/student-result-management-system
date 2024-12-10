using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Student_Result_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class AddHovaTenToAspNetUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    { "05b78e0a-42e5-4151-8ad3-517f65f26559", null, "SinhVien", "SINHVIEN" },
                    { "13e127db-c359-489b-a5af-8a0b0c15fc0a", null, "PhongDaoTao", "PHONGDAOTAO" },
                    { "14d939f1-d5fb-4b02-b762-5ca4a015bf82", null, "GiangVien", "GIANGVIEN" },
                    { "2e87e4a0-c6f3-4bd6-9757-45d7f0bc5bf6", null, "TruongBoMon", "TRUONGBOMON" },
                    { "4758c395-62ff-47ad-84c4-941c3cce86dc", null, "TruongKhoa", "TRUONGKHOA" },
                    { "fbd52c58-902a-4e03-b652-0f439cb02c89", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "05b78e0a-42e5-4151-8ad3-517f65f26559");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "13e127db-c359-489b-a5af-8a0b0c15fc0a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "14d939f1-d5fb-4b02-b762-5ca4a015bf82");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2e87e4a0-c6f3-4bd6-9757-45d7f0bc5bf6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4758c395-62ff-47ad-84c4-941c3cce86dc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fbd52c58-902a-4e03-b652-0f439cb02c89");

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
        }
    }
}
