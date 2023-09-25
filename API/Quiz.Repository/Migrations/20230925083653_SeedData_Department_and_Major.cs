using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quiz.Repository.Migrations
{
    public partial class SeedData_Department_and_Major : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "DepartmentId", "Name" },
                values: new object[,]
                {
                    { "KTCN", "Khoa Kỹ thuật Công nghệ" },
                    { "KTL", "Khoa Kinh tế - Luật" },
                    { "LLCTGDQP&TC", "Khoa Lý luận chính trị - GD Quốc phòng và Thể chất" },
                    { "NN&CNTP", "Khoa Nông nghiệp và Công nghệ Thực phẩm" },
                    { "SP&KHCB", "Khoa Sư phạm và Khoa học cơ bản" }
                });

            migrationBuilder.InsertData(
                table: "Majors",
                columns: new[] { "MajorId", "DepartmentId", "Name" },
                values: new object[] { "CNTT", "KTCN", "Công nghệ thông tin" });

            migrationBuilder.InsertData(
                table: "Majors",
                columns: new[] { "MajorId", "DepartmentId", "Name" },
                values: new object[] { "DK&TDH", "KTCN", "Điều khiển và tự động hoá" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "DepartmentId",
                keyValue: "KTL");

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "DepartmentId",
                keyValue: "LLCTGDQP&TC");

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "DepartmentId",
                keyValue: "NN&CNTP");

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "DepartmentId",
                keyValue: "SP&KHCB");

            migrationBuilder.DeleteData(
                table: "Majors",
                keyColumn: "MajorId",
                keyValue: "CNTT");

            migrationBuilder.DeleteData(
                table: "Majors",
                keyColumn: "MajorId",
                keyValue: "DK&TDH");

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "DepartmentId",
                keyValue: "KTCN");
        }
    }
}
