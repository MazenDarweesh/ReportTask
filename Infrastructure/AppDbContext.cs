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
                .WithMany()
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
