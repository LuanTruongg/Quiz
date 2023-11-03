using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quiz.Repository.Migrations
{
    public partial class seed_data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetRoles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "69bd714f-9576-45ba-b5b7-f00649be00de", "ac92afbb-ec0c-4d8a-b802-82dc6cf0ef96", "IdentityRole", "Admin", "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "8d04dce2-969a-435d-bba4-df3f325983dc", "ec099e63-1af7-43ad-921f-859e9f71470f", "IdentityRole", "Teacher", "Teacher" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "CCCD", "ConcurrencyStamp", "DoB", "Email", "EmailConfirmed", "Fullname", "LockoutEnabled", "LockoutEnd", "Money", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Sex", "TwoFactorEnabled", "UserName" },
                values: new object[] { "85a3ebb5-f310-4676-8d27-4d80c9c7fdef", 0, "Bến Tre", "202011237083", "0b9c47ec-b6cf-41df-ac8b-6caf299eb510", new DateTime(2002, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "luantruong020302@gmail.com", false, "Trương Hoàng Luân", false, null, 1000000f, "luantruong020302@gmail.com,", "luantruong020302@gmail.com", "AQAAAAEAACcQAAAAECIN93DXE6Zz1Opz/kv60o8igX2sWzQ3GhQ/MOX/qkRhbtUKw08ymUC18dFD7TX4tw==", "0836151007", false, "", true, false, "luantruong020302@gmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "69bd714f-9576-45ba-b5b7-f00649be00de", "85a3ebb5-f310-4676-8d27-4d80c9c7fdef" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "8d04dce2-969a-435d-bba4-df3f325983dc", "85a3ebb5-f310-4676-8d27-4d80c9c7fdef" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "69bd714f-9576-45ba-b5b7-f00649be00de", "85a3ebb5-f310-4676-8d27-4d80c9c7fdef" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "8d04dce2-969a-435d-bba4-df3f325983dc", "85a3ebb5-f310-4676-8d27-4d80c9c7fdef" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "69bd714f-9576-45ba-b5b7-f00649be00de");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8d04dce2-969a-435d-bba4-df3f325983dc");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "85a3ebb5-f310-4676-8d27-4d80c9c7fdef");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetRoles");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
