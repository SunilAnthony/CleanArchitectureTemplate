using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TEntity> Set<TEntity>() where TEntity : class;
    DbSet<Student> Students { get; }

    DbSet<Course> Courses { get; }

    DbSet<Enrollment> Enrollments { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
