using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Student> Students { get; }
        IRepository<School> Schools { get; }
        IRepository<Grade> Grades { get; }
        IRepository<Classroom> Classes { get; }
        IRepository<Year> Years { get; }

        Task<int> CompleteAsync();
    }
}
