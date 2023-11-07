using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quiz.Repository.Migrations
{
    public partial class fix_duplicate_Test_of_UserTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserTests_TestStructureId",
                table: "UserTests");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "69bd714f-9576-45ba-b5b7-f00649be00de",
                column: "ConcurrencyStamp",
                value: "e588f3c4-d78b-4069-a231-4a56491604de");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8d04dce2-969a-435d-bba4-df3f325983dc",
                column: "ConcurrencyStamp",
                value: "2dfe4474-856b-4d55-a4cb-becb0416e677");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "85a3ebb5-f310-4676-8d27-4d80c9c7fdef",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e5b0bd38-6765-4949-866e-7840ffc3291b", "AQAAAAEAACcQAAAAEGdtDDJ5Pb2mFsBjsISa3B7I05tyme8N1gmz/VSEel0dUsTFpbpzKW6RypuFqL1sew==" });

            migrationBuilder.CreateIndex(
                name: "IX_UserTests_TestStructureId",
                table: "UserTests",
                column: "TestStructureId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserTests_TestStructureId",
                table: "UserTests");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "69bd714f-9576-45ba-b5b7-f00649be00de",
                column: "ConcurrencyStamp",
                value: "ee577c7e-eed2-4af0-bd27-6e408646e253");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8d04dce2-969a-435d-bba4-df3f325983dc",
                column: "ConcurrencyStamp",
                value: "b185a43e-3089-45e1-8580-b0e30e704bce");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "85a3ebb5-f310-4676-8d27-4d80c9c7fdef",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "671d7cd0-9e11-4272-83b3-c89f3a88352f", "AQAAAAEAACcQAAAAEJSU0qDOuUDPdlyg8XDvvxv33SmwGvx3e4ZaUNAmef+/1bNriYKBxtqtAI/+677pkA==" });

            migrationBuilder.CreateIndex(
                name: "IX_UserTests_TestStructureId",
                table: "UserTests",
                column: "TestStructureId",
                unique: true);
        }
    }
}
