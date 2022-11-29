using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories
{
    public sealed class StudentRepository: Repository<Student>, IStudentRepository
    {
        private ApplicationDbContext _appContext => (ApplicationDbContext) _context;
        public StudentRepository(ApplicationDbContext context) : base(context) { }

    }
}
