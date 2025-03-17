using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IRepository<Student> Students { get; }
        public IRepository<School> Schools { get; }
        public IRepository<Grade> Grades { get; }
        public IRepository<Classroom> Classes { get; }
        public IRepository<Year> Years { get; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Students = new Repository<Student>(_context);
            Schools = new Repository<School>(_context);
            Grades = new Repository<Grade>(_context);
            Classes = new Repository<Classroom>(_context);
            Years = new Repository<Year>(_context);
        }

        public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();
    }
}
