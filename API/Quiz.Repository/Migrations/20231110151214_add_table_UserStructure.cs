using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quiz.Repository.Migrations
{
    public partial class add_table_UserStructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserStructures",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TestStructureId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsFree = table.Column<bool>(type: "bit", nullable: false),
                    Price = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserStructures", x => new { x.UserId, x.TestStructureId });
                    table.ForeignKey(
                        name: "FK_UserStructures_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserStructures_TestStructures_TestStructureId",
                        column: x => x.TestStructureId,
                        principalTable: "TestStructures",
                        principalColumn: "TestStructureId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "69bd714f-9576-45ba-b5b7-f00649be00de",
                column: "ConcurrencyStamp",
                value: "2b09fac2-27d5-4249-a07f-d5710375ea27");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8d04dce2-969a-435d-bba4-df3f325983dc",
                column: "ConcurrencyStamp",
                value: "d2278410-14b0-4c32-aa30-abe383e2f8d0");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "85a3ebb5-f310-4676-8d27-4d80c9c7fdef",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3109102e-ec7e-4c6e-938f-5aaac73d9987", "AQAAAAEAACcQAAAAELvYRGAg76jUEg19d1x6pGmNpJQAt62/jJ/yapH/yvRwhUgiGxvkdlkIddGHFtmuVA==" });

            migrationBuilder.CreateIndex(
                name: "IX_UserStructures_TestStructureId",
                table: "UserStructures",
                column: "TestStructureId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserStructures");

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
        }
    }
}
