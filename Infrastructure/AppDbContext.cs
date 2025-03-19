using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Utilities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;


namespace Infrastructure
{
    public class AppDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentAcademicYear> StudentAcademicYears { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<AcademicYear> AcademicYears { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }
        public DbSet<Section> Sections { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define a reusable ULID converter
            var ulidConverter = new ValueConverter<Ulid, string>(
                v => v.ToString(),
                v => Ulid.Parse(v)
            );

            // Static ULIDs
            var school1Id = Ulid.Parse("01HWH8B1ZB4G0K3N00YZH7WCE1");
            var school2Id = Ulid.Parse("01HWH8B1ZB4G0K3N00YZH7WCE2");

            var academicYear1Id = Ulid.Parse("01HWH8B1ZB4G0K3N00YZH7WCEA");
            var academicYear2Id = Ulid.Parse("01HWH8B1ZB4G0K3N00YZH7WCEB");

            var semester1Id = Ulid.Parse("01HWH8B1ZB4G0K3N00YZH7WCEC");
            var semester2Id = Ulid.Parse("01HWH8B1ZB4G0K3N00YZH7WCED");

            var classroom1Id = Ulid.Parse("01HWH8B1ZB4G0K3N00YZH7WCEE");
            var classroom2Id = Ulid.Parse("01HWH8B1ZB4G0K3N00YZH7WCEF");

            var grade1Id = Ulid.Parse("01HWH8B1ZB4G0K3N00YZH7WCEG");
            var grade2Id = Ulid.Parse("01HWH8B1ZB4G0K3N00YZH7WCEH");

            var student1Id = Ulid.Parse("01HWH8B1ZB4G0K3N00YZH7WCE3");
            var student2Id = Ulid.Parse("01HWH8B1ZB4G0K3N00YZH7WCE4");

            var section1Id = Ulid.Parse("01HWH8B1ZB4G0K3N00YZH7WCEX");
            var section2Id = Ulid.Parse("01HWH8B1ZB4G0K3N00YZH7WCEY");

            // ✅ 1️⃣ Seed Schools
            modelBuilder.Entity<School>().HasData(
                new School
                {
                    Id = school1Id,
                    Name = "Learning Oasis",
                    Address = "123 Elm St",
                    ReportHeaderOneEn = "Welcome to Learning Oasis",
                    ReportHeaderOneAr = "مرحبًا بكم في مدرسة الواحة",
                    ReportHeaderTwoEn = "Striving for Excellence",
                    ReportHeaderTwoAr = "نسعى للتميز",
                    ReportImage = "school1_logo.png"
                },
                new School
                {
                    Id = school2Id,
                    Name = "Future Leaders Academy",
                    Address = "456 Maple Ave",
                    ReportHeaderOneEn = "Future Starts Here",
                    ReportHeaderOneAr = "المستقبل يبدأ هنا",
                    ReportHeaderTwoEn = "Building Tomorrow’s Leaders",
                    ReportHeaderTwoAr = "نبني قادة المستقبل",
                    ReportImage = "school2_logo.png"
                }
            );

            // ✅ 2️⃣ Seed Academic Years (Before Semesters)
            modelBuilder.Entity<AcademicYear>().HasData(
                new AcademicYear { Id = academicYear1Id, Name = "2024-2025", SchoolId = school1Id },
                new AcademicYear { Id = academicYear2Id, Name = "2025-2026", SchoolId = school2Id }
            );

            // ✅ 3️⃣ Seed Semesters (Before Classrooms)
            modelBuilder.Entity<Semester>().HasData(
                new Semester { Id = semester1Id, Name = "Fall 2024", AcademicYearId = academicYear1Id },
                new Semester { Id = semester2Id, Name = "Spring 2025", AcademicYearId = academicYear2Id }
            );
            // ✅ 2️⃣ Seed Sections (Before Grades)
            modelBuilder.Entity<Section>().HasData(
                new Section { Id = section1Id, Name = "Primary", SchoolId = school1Id },
                new Section { Id = section2Id, Name = "Secondary", SchoolId = school2Id }
            );

            // ✅ 3️⃣ Seed Grades (After Sections)
            modelBuilder.Entity<Grade>().HasData(
                new Grade { Id = grade1Id, Name = "Grade 1", SectionId = section1Id },
                new Grade { Id = grade2Id, Name = "Grade 2", SectionId = section2Id }
            );

            // ✅ 5️⃣ Seed Classrooms (Must Reference Valid AcademicYear)
            modelBuilder.Entity<Classroom>().HasData(
                new Classroom { Id = classroom1Id, Name = "Class A", GradeId = grade1Id, AcademicYearId = academicYear1Id },
                new Classroom { Id = classroom2Id, Name = "Class B", GradeId = grade2Id, AcademicYearId = academicYear2Id }
            );

            // ✅ 6️⃣ Seed Students
            modelBuilder.Entity<Student>().HasData(
                new Student { Id = student1Id, Name = "John Doe", MobileNumber = "123-456", Nationality = "American", Gender = "Male" },
                new Student { Id = student2Id, Name = "Jane Smith", MobileNumber = "987-654", Nationality = "Canadian", Gender = "Female" }
            );

            // ✅ 7️⃣ Seed StudentAcademicYears (AFTER All Dependencies Exist)
            modelBuilder.Entity<StudentAcademicYear>().HasData(
                new StudentAcademicYear
                {
                    Id = Ulid.Parse("01HWH8B1ZB4G0K3N00YZH7WCEI"),
                    StudentId = student1Id,
                    SchoolId = school1Id,
                    ClassroomId = classroom1Id,
                    GradeId = grade1Id,
                    SemesterId = semester1Id
                },
                new StudentAcademicYear
                {
                    Id = Ulid.Parse("01HWH8B1ZB4G0K3N00YZH7WCEJ"),
                    StudentId = student2Id,
                    SchoolId = school2Id,
                    ClassroomId = classroom2Id,
                    GradeId = grade2Id,
                    SemesterId = semester2Id
                }
            );



            // Apply ULID conversion to primary keys
            modelBuilder.Entity<Student>().Property(s => s.Id).HasConversion(ulidConverter).ValueGeneratedOnAdd();
            modelBuilder.Entity<School>().Property(s => s.Id).HasConversion(ulidConverter).ValueGeneratedOnAdd();
            modelBuilder.Entity<Grade>().Property(s => s.Id).HasConversion(ulidConverter).ValueGeneratedOnAdd();
            modelBuilder.Entity<Classroom>().Property(s => s.Id).HasConversion(ulidConverter).ValueGeneratedOnAdd();
            modelBuilder.Entity<Section>().Property(s => s.Id).HasConversion(ulidConverter).ValueGeneratedOnAdd();
            modelBuilder.Entity<Semester>().Property(s => s.Id).HasConversion(ulidConverter).ValueGeneratedOnAdd();
            modelBuilder.Entity<AcademicYear>().Property(s => s.Id).HasConversion(ulidConverter).ValueGeneratedOnAdd();
            modelBuilder.Entity<StudentAcademicYear>().Property(s => s.Id).HasConversion(ulidConverter).ValueGeneratedOnAdd();


            // Relationships
            modelBuilder.Entity<StudentAcademicYear>()
                .HasOne(say => say.Student)
                .WithMany(s => s.StudentAcademicYears)
                .HasForeignKey(say => say.StudentId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<StudentAcademicYear>()
                .HasOne(say => say.School)
                .WithMany()
                .HasForeignKey(say => say.SchoolId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<StudentAcademicYear>()
                .HasOne(say => say.Classroom)
                .WithMany()
                .HasForeignKey(say => say.ClassroomId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<StudentAcademicYear>()
                .HasOne(say => say.Grade)
                .WithMany()
                .HasForeignKey(say => say.GradeId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<StudentAcademicYear>()
                .HasOne(say => say.Semester)
                .WithMany()
                .HasForeignKey(say => say.SemesterId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<AcademicYear>()
                .HasOne(ay => ay.School)
                .WithMany()
                .HasForeignKey(ay => ay.SchoolId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Semester>()
                .HasOne(s => s.AcademicYear)
                .WithMany()
                .HasForeignKey(s => s.AcademicYearId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Grade>()
                .HasOne(g => g.Section)
                .WithMany()
                .HasForeignKey(g => g.SectionId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Classroom>()
               .HasOne(c => c.Grade)
               .WithMany(g => g.Classrooms)
               .HasForeignKey(c => c.GradeId)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Classroom>()
                .HasOne(c => c.AcademicYear)
                .WithMany()
                .HasForeignKey(c => c.AcademicYearId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Section>()
                .HasOne(s => s.School)
                .WithMany()
                .HasForeignKey(s => s.SchoolId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
