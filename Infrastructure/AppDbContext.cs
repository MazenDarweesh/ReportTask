using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace Infrastructure
{
    public class AppDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Year> Years { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply ULID conversions using UlidTypeConverter
            modelBuilder.Entity<Student>().Property(s => s.Id)
                .HasConversion(v => v.ConvertFromUlid(), v => v.ConvertToUlid());

            modelBuilder.Entity<Student>().Property(s => s.SchoolId)
                .HasConversion(v => v.ConvertFromUlid(), v => v.ConvertToUlid());

            modelBuilder.Entity<Student>().Property(s => s.GradeId)
                .HasConversion(v => v.ConvertFromUlid(), v => v.ConvertToUlid());

            modelBuilder.Entity<Student>().Property(s => s.ClassId)
                .HasConversion(v => v.ConvertFromUlid(), v => v.ConvertToUlid());

            modelBuilder.Entity<Student>().Property(s => s.YearId)
                .HasConversion(v => v.ConvertFromUlid(), v => v.ConvertToUlid());

            modelBuilder.Entity<School>().Property(s => s.Id)
                .HasConversion(v => v.ConvertFromUlid(), v => v.ConvertToUlid());

            modelBuilder.Entity<Grade>().Property(s => s.Id)
                .HasConversion(v => v.ConvertFromUlid(), v => v.ConvertToUlid());

            modelBuilder.Entity<Class>().Property(s => s.Id)
                .HasConversion(v => v.ConvertFromUlid(), v => v.ConvertToUlid());

            modelBuilder.Entity<Year>().Property(s => s.Id)
                .HasConversion(v => v.ConvertFromUlid(), v => v.ConvertToUlid());
        }
    }
}
