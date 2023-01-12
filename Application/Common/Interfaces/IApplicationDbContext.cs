using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;
/// <summary>
/// Use this Interface if you want to use the repository pattern
/// In addition, you will need to use IUnitOfWork to Save your changes
/// </summary>
public interface IApplicationDbContext
{
    DbSet<TEntity> Set<TEntity>() where TEntity : class;
    DbSet<Student> Students { get; }

    DbSet<Course> Courses { get; }

    DbSet<Enrollment> Enrollments { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

   int SaveChanges();
}
