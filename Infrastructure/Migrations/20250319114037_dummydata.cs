using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class dummydata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Schools",
                columns: new[] { "Id", "Address", "Name", "ReportHeaderOneAr", "ReportHeaderOneEn", "ReportHeaderTwoAr", "ReportHeaderTwoEn", "ReportImage" },
                values: new object[,]
                {
                    { "01HWH8B1ZB4G0K3N00YZH7WCE1", "123 Elm St", "Learning Oasis", "مرحبًا بكم في مدرسة الواحة", "Welcome to Learning Oasis", "نسعى للتميز", "Striving for Excellence", "school1_logo.png" },
                    { "01HWH8B1ZB4G0K3N00YZH7WCE2", "456 Maple Ave", "Future Leaders Academy", "المستقبل يبدأ هنا", "Future Starts Here", "نبني قادة المستقبل", "Building Tomorrow’s Leaders", "school2_logo.png" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Gender", "MobileNumber", "Name", "Nationality" },
                values: new object[,]
                {
                    { "01HWH8B1ZB4G0K3N00YZH7WCE3", "Male", "123-456", "John Doe", "American" },
                    { "01HWH8B1ZB4G0K3N00YZH7WCE4", "Female", "987-654", "Jane Smith", "Canadian" }
                });

            migrationBuilder.InsertData(
                table: "AcademicYears",
                columns: new[] { "Id", "DateFrom", "DateTo", "IsActive", "Name", "SchoolId" },
                values: new object[,]
                {
                    { "01HWH8B1ZB4G0K3N00YZH7WCEA", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "2024-2025", "01HWH8B1ZB4G0K3N00YZH7WCE1" },
                    { "01HWH8B1ZB4G0K3N00YZH7WCEB", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "2025-2026", "01HWH8B1ZB4G0K3N00YZH7WCE2" }
                });

            migrationBuilder.InsertData(
                table: "Sections",
                columns: new[] { "Id", "Name", "SchoolId" },
                values: new object[,]
                {
                    { "01HWH8B1ZB4G0K3N00YZH7WCEX", "Primary", "01HWH8B1ZB4G0K3N00YZH7WCE1" },
                    { "01HWH8B1ZB4G0K3N00YZH7WCEY", "Secondary", "01HWH8B1ZB4G0K3N00YZH7WCE2" }
                });

            migrationBuilder.InsertData(
                table: "Grades",
                columns: new[] { "Id", "DateFrom", "DateTo", "Name", "SectionId" },
                values: new object[,]
                {
                    { "01HWH8B1ZB4G0K3N00YZH7WCEG", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Grade 1", "01HWH8B1ZB4G0K3N00YZH7WCEX" },
                    { "01HWH8B1ZB4G0K3N00YZH7WCEH", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Grade 2", "01HWH8B1ZB4G0K3N00YZH7WCEY" }
                });

            migrationBuilder.InsertData(
                table: "Semesters",
                columns: new[] { "Id", "AcademicYearId", "DateFrom", "DateTo", "Name" },
                values: new object[,]
                {
                    { "01HWH8B1ZB4G0K3N00YZH7WCEC", "01HWH8B1ZB4G0K3N00YZH7WCEA", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fall 2024" },
                    { "01HWH8B1ZB4G0K3N00YZH7WCED", "01HWH8B1ZB4G0K3N00YZH7WCEB", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Spring 2025" }
                });

            migrationBuilder.InsertData(
                table: "Classrooms",
                columns: new[] { "Id", "AcademicYearId", "GradeId", "Name", "Number" },
                values: new object[,]
                {
                    { "01HWH8B1ZB4G0K3N00YZH7WCEE", "01HWH8B1ZB4G0K3N00YZH7WCEA", "01HWH8B1ZB4G0K3N00YZH7WCEG", "Class A", 0 },
                    { "01HWH8B1ZB4G0K3N00YZH7WCEF", "01HWH8B1ZB4G0K3N00YZH7WCEB", "01HWH8B1ZB4G0K3N00YZH7WCEH", "Class B", 0 }
                });

            migrationBuilder.InsertData(
                table: "StudentAcademicYears",
                columns: new[] { "Id", "ClassroomId", "GradeId", "SchoolId", "SemesterId", "StudentId" },
                values: new object[,]
                {
                    { "01HWH8B1ZB4G0K3N00YZH7WCEJ", "01HWH8B1ZB4G0K3N00YZH7WCEF", "01HWH8B1ZB4G0K3N00YZH7WCEH", "01HWH8B1ZB4G0K3N00YZH7WCE2", "01HWH8B1ZB4G0K3N00YZH7WCED", "01HWH8B1ZB4G0K3N00YZH7WCE4" },
                    { "01HWH8B1ZB4G0K3N00YZH7WCFZ", "01HWH8B1ZB4G0K3N00YZH7WCEE", "01HWH8B1ZB4G0K3N00YZH7WCEG", "01HWH8B1ZB4G0K3N00YZH7WCE1", "01HWH8B1ZB4G0K3N00YZH7WCEC", "01HWH8B1ZB4G0K3N00YZH7WCE3" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "StudentAcademicYears",
                keyColumn: "Id",
                keyValue: "01HWH8B1ZB4G0K3N00YZH7WCEJ");

            migrationBuilder.DeleteData(
                table: "StudentAcademicYears",
                keyColumn: "Id",
                keyValue: "01HWH8B1ZB4G0K3N00YZH7WCFZ");

            migrationBuilder.DeleteData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: "01HWH8B1ZB4G0K3N00YZH7WCEE");

            migrationBuilder.DeleteData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: "01HWH8B1ZB4G0K3N00YZH7WCEF");

            migrationBuilder.DeleteData(
                table: "Semesters",
                keyColumn: "Id",
                keyValue: "01HWH8B1ZB4G0K3N00YZH7WCEC");

            migrationBuilder.DeleteData(
                table: "Semesters",
                keyColumn: "Id",
                keyValue: "01HWH8B1ZB4G0K3N00YZH7WCED");

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: "01HWH8B1ZB4G0K3N00YZH7WCE3");

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: "01HWH8B1ZB4G0K3N00YZH7WCE4");

            migrationBuilder.DeleteData(
                table: "AcademicYears",
                keyColumn: "Id",
                keyValue: "01HWH8B1ZB4G0K3N00YZH7WCEA");

            migrationBuilder.DeleteData(
                table: "AcademicYears",
                keyColumn: "Id",
                keyValue: "01HWH8B1ZB4G0K3N00YZH7WCEB");

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: "01HWH8B1ZB4G0K3N00YZH7WCEG");

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: "01HWH8B1ZB4G0K3N00YZH7WCEH");

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: "01HWH8B1ZB4G0K3N00YZH7WCEX");

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "Id",
                keyValue: "01HWH8B1ZB4G0K3N00YZH7WCEY");

            migrationBuilder.DeleteData(
                table: "Schools",
                keyColumn: "Id",
                keyValue: "01HWH8B1ZB4G0K3N00YZH7WCE1");

            migrationBuilder.DeleteData(
                table: "Schools",
                keyColumn: "Id",
                keyValue: "01HWH8B1ZB4G0K3N00YZH7WCE2");
        }
    }
}
