using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Utilities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;




namespace Infrastructure
{
    public class AppDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Year> Years { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<AcademicYear> AcademicYears { get; set; }
        public DbSet<StudentAcademicYear> StudentAcademicYears { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define a reusable ULID converter
            var ulidConverter = new ValueConverter<Ulid, string>(
                v => v.ToString(),
                v => Ulid.Parse(v)
            );

            // Apply ULID conversions using UlidTypeConverter
            // Apply ULID conversion to primary keys
            modelBuilder.Entity<Student>().Property(s => s.Id).HasConversion(ulidConverter).ValueGeneratedOnAdd();
            modelBuilder.Entity<School>().Property(s => s.Id).HasConversion(ulidConverter).ValueGeneratedOnAdd();
            modelBuilder.Entity<Grade>().Property(s => s.Id).HasConversion(ulidConverter).ValueGeneratedOnAdd();
            modelBuilder.Entity<Classroom>().Property(s => s.Id).HasConversion(ulidConverter).ValueGeneratedOnAdd();
            modelBuilder.Entity<Section>().Property(s => s.Id).HasConversion(ulidConverter).ValueGeneratedOnAdd();
            modelBuilder.Entity<Year>().Property(s => s.Id).HasConversion(ulidConverter).ValueGeneratedOnAdd();
            modelBuilder.Entity<Semester>().Property(s => s.Id).HasConversion(ulidConverter).ValueGeneratedOnAdd();
            modelBuilder.Entity<AcademicYear>().Property(s => s.Id).HasConversion(ulidConverter).ValueGeneratedOnAdd();
            modelBuilder.Entity<StudentAcademicYear>().Property(s => s.Id).HasConversion(ulidConverter).ValueGeneratedOnAdd();

            // Apply ULID conversion to foreign keys
            modelBuilder.Entity<Student>().Property(s => s.SchoolId).HasConversion(ulidConverter);
            modelBuilder.Entity<Student>().Property(s => s.GradeId).HasConversion(ulidConverter);
            modelBuilder.Entity<Student>().Property(s => s.ClassId).HasConversion(ulidConverter);
            modelBuilder.Entity<Student>().Property(s => s.YearId).HasConversion(ulidConverter);

            modelBuilder.Entity<AcademicYear>().Property(a => a.SchoolId).HasConversion(ulidConverter);
            modelBuilder.Entity<Semester>().Property(s => s.AcademicYearId).HasConversion(ulidConverter);
            modelBuilder.Entity<Grade>().Property(g => g.SectionId).HasConversion(ulidConverter);
            modelBuilder.Entity<Classroom>().Property(c => c.GradeId).HasConversion(ulidConverter);
            modelBuilder.Entity<Classroom>().Property(c => c.AcademicYearId).HasConversion(ulidConverter);
            modelBuilder.Entity<Section>().Property(s => s.SchoolId).HasConversion(ulidConverter);

            modelBuilder.Entity<StudentAcademicYear>().Property(sa => sa.StudentId).HasConversion(ulidConverter);
            modelBuilder.Entity<StudentAcademicYear>().Property(sa => sa.SchoolId).HasConversion(ulidConverter);
            modelBuilder.Entity<StudentAcademicYear>().Property(sa => sa.GradeId).HasConversion(ulidConverter);
            modelBuilder.Entity<StudentAcademicYear>().Property(sa => sa.ClassId).HasConversion(ulidConverter);
            modelBuilder.Entity<StudentAcademicYear>().Property(sa => sa.SemesterId).HasConversion(ulidConverter);

            // Define Relationships
            modelBuilder.Entity<StudentAcademicYear>()
             .HasOne(sa => sa.Student)
             .WithMany()
             .HasForeignKey(sa => sa.StudentId)
             .OnDelete(DeleteBehavior.NoAction); // Prevent cascade delete

            modelBuilder.Entity<StudentAcademicYear>()
                .HasOne(sa => sa.School)
                .WithMany()
                .HasForeignKey(sa => sa.SchoolId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<StudentAcademicYear>()
                .HasOne(sa => sa.Grade)
                .WithMany()
                .HasForeignKey(sa => sa.GradeId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<StudentAcademicYear>()
                .HasOne(sa => sa.Class)
                .WithMany()
                .HasForeignKey(sa => sa.ClassId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<StudentAcademicYear>()
                .HasOne(sa => sa.Semester)
                .WithMany()
                .HasForeignKey(sa => sa.SemesterId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
