using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quiz.Repository.Migrations
{
    public partial class datesubmit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DateSubmit",
                table: "UserTests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "69bd714f-9576-45ba-b5b7-f00649be00de",
                column: "ConcurrencyStamp",
                value: "882e114e-f360-4d46-81a7-e5622a1bbd9e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8d04dce2-969a-435d-bba4-df3f325983dc",
                column: "ConcurrencyStamp",
                value: "02abe34e-0199-4474-bd32-8f5c17805302");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "85a3ebb5-f310-4676-8d27-4d80c9c7fdef",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8c91e652-4e67-4bcc-999a-987bd9efe222", "AQAAAAEAACcQAAAAEHAO5S2cZ6xk5FKUfNO8ql0wIzlXi6+64tEj2w8QJj3Asg2+HSPRRoGFV+5T1uwfNg==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateSubmit",
                table: "UserTests");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "69bd714f-9576-45ba-b5b7-f00649be00de",
                column: "ConcurrencyStamp",
                value: "2b095d0b-1e3c-45d9-a16c-4077ac5f8e47");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8d04dce2-969a-435d-bba4-df3f325983dc",
                column: "ConcurrencyStamp",
                value: "d905c1cf-d63c-4144-afde-3739daa7ecd3");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "85a3ebb5-f310-4676-8d27-4d80c9c7fdef",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "838f6b26-aabe-4e13-af10-02412ed2e747", "AQAAAAEAACcQAAAAEBlJ/2SaXgaOfdzp1xuCi1MZ+Ex3ztSa0TkM1yAB4yjCdmMIzHIQiCzrlemAwBckPA==" });
        }
    }
}
