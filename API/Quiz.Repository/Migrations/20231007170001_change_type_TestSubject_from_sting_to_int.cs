using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quiz.Repository.Migrations
{
    public partial class change_type_TestSubject_from_sting_to_int : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTests_TestSubjects_TestSubjectId",
                table: "UserTests");

            migrationBuilder.DropIndex(
                name: "IX_UserTests_TestSubjectId",
                table: "UserTests");

            migrationBuilder.AlterColumn<string>(
                name: "TestSubjectId",
                table: "UserTests",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "TestSubjectId1",
                table: "UserTests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "TestSubjectId",
                table: "TestSubjects",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.CreateIndex(
                name: "IX_UserTests_TestSubjectId1",
                table: "UserTests",
                column: "TestSubjectId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTests_TestSubjects_TestSubjectId1",
                table: "UserTests",
                column: "TestSubjectId1",
                principalTable: "TestSubjects",
                principalColumn: "TestSubjectId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTests_TestSubjects_TestSubjectId1",
                table: "UserTests");

            migrationBuilder.DropIndex(
                name: "IX_UserTests_TestSubjectId1",
                table: "UserTests");

            migrationBuilder.DropColumn(
                name: "TestSubjectId1",
                table: "UserTests");

            migrationBuilder.AlterColumn<string>(
                name: "TestSubjectId",
                table: "UserTests",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "TestSubjectId",
                table: "TestSubjects",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.CreateIndex(
                name: "IX_UserTests_TestSubjectId",
                table: "UserTests",
                column: "TestSubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTests_TestSubjects_TestSubjectId",
                table: "UserTests",
                column: "TestSubjectId",
                principalTable: "TestSubjects",
                principalColumn: "TestSubjectId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
