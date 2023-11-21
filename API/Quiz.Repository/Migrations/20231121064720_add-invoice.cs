using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quiz.Repository.Migrations
{
    public partial class addinvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserStructures",
                table: "UserStructures");

            migrationBuilder.AddColumn<int>(
                name: "UserStructureId",
                table: "UserStructures",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "PurchaseDate",
                table: "UserStructures",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserStructures",
                table: "UserStructures",
                columns: new[] { "UserId", "TestStructureId", "UserStructureId" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "69bd714f-9576-45ba-b5b7-f00649be00de",
                column: "ConcurrencyStamp",
                value: "e94045b7-a195-47a1-bae7-fe4ee489a925");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8d04dce2-969a-435d-bba4-df3f325983dc",
                column: "ConcurrencyStamp",
                value: "f8752b8d-ec35-4bad-9602-0440155ee256");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "85a3ebb5-f310-4676-8d27-4d80c9c7fdef",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f549b582-a613-42bc-9b2e-0474189c5423", "AQAAAAEAACcQAAAAEC4PCwEgtjMl4WhCTHSznzvRvdOmHDno7AuU1wrH53h5gTsUGBIuEfdbCa9M1JWssQ==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserStructures",
                table: "UserStructures");

            migrationBuilder.DropColumn(
                name: "UserStructureId",
                table: "UserStructures");

            migrationBuilder.DropColumn(
                name: "PurchaseDate",
                table: "UserStructures");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserStructures",
                table: "UserStructures",
                columns: new[] { "UserId", "TestStructureId" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "69bd714f-9576-45ba-b5b7-f00649be00de",
                column: "ConcurrencyStamp",
                value: "2442fcbd-1c9e-4b1c-ba03-9e708706c182");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8d04dce2-969a-435d-bba4-df3f325983dc",
                column: "ConcurrencyStamp",
                value: "0429fe79-eaaf-4f49-b711-7155bbfd8af9");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "85a3ebb5-f310-4676-8d27-4d80c9c7fdef",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d4981056-5812-417c-9aa8-d2145c2fdf3a", "AQAAAAEAACcQAAAAEBV01x325sp/H2BM8bKs2mN1zNP8uN8kA1t/V6qsTgTRtSQd6FpFaVFGTtEgcA8h3Q==" });
        }
    }
}
