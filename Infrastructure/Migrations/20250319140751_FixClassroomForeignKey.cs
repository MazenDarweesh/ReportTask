using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixClassroomForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classrooms_Grades_GradeId1",
                table: "Classrooms");

            migrationBuilder.DropIndex(
                name: "IX_Classrooms_GradeId1",
                table: "Classrooms");

            migrationBuilder.DropColumn(
                name: "GradeId1",
                table: "Classrooms");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GradeId1",
                table: "Classrooms",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: "01HWH8B1ZB4G0K3N00YZH7WCEE",
                column: "GradeId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: "01HWH8B1ZB4G0K3N00YZH7WCEF",
                column: "GradeId1",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Classrooms_GradeId1",
                table: "Classrooms",
                column: "GradeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Classrooms_Grades_GradeId1",
                table: "Classrooms",
                column: "GradeId1",
                principalTable: "Grades",
                principalColumn: "Id");
        }
    }
}
