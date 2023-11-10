using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quiz.Repository.Migrations
{
    public partial class change_properties_missing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFree",
                table: "UserStructures");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "UserStructures");

            migrationBuilder.AddColumn<bool>(
                name: "IsFree",
                table: "TestStructures",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "Price",
                table: "TestStructures",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFree",
                table: "TestStructures");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "TestStructures");

            migrationBuilder.AddColumn<bool>(
                name: "IsFree",
                table: "UserStructures",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "Price",
                table: "UserStructures",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

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
        }
    }
}
