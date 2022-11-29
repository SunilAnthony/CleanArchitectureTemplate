using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Persistence.Configurations;

public sealed class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable(nameof(Student)).HasKey(k => k.StudentId);
        builder.Property(t => t.FirstName)
            .HasColumnType("varchar(100)")
            .IsRequired();
        builder.Property(t => t.LastName)
           .HasColumnType("varchar(100)")
           .IsRequired();
        builder.Property(t => t.EmailAddress)
           .HasColumnType("varchar(150)")
           .IsRequired();

        builder.HasData
            (
            new Student { StudentId = 1, FirstName = "Carson", LastName = "Alexander", EmailAddress = "calexander@school.edu", EnrollmentDate = DateTime.Parse("2005-09-01") },
            new Student { StudentId = 2, FirstName = "Meredith", LastName = "Alonso", EmailAddress = "malonso@school.edu", EnrollmentDate = DateTime.Parse("2002-09-01") },
            new Student { StudentId = 3, FirstName = "Arturo", LastName = "Anand", EmailAddress = "aanand@school.edu", EnrollmentDate = DateTime.Parse("2003-09-01") },
            new Student { StudentId = 4, FirstName = "Gytis", LastName = "Barzdukas", EmailAddress = "gbarzdukas@school.edu", EnrollmentDate = DateTime.Parse("2002-09-01") },
            new Student { StudentId = 5, FirstName = "Yan", LastName = "Li", EmailAddress = "yli@school.edu", EnrollmentDate = DateTime.Parse("2002-09-01") },
            new Student { StudentId = 6, FirstName = "Peggy", LastName = "Justice", EmailAddress = "pjustice@school.edu", EnrollmentDate = DateTime.Parse("2001-09-01") },
            new Student { StudentId = 7, FirstName = "Laura", LastName = "Norman", EmailAddress = "lnorman@school.edu", EnrollmentDate = DateTime.Parse("2003-09-01") },
            new Student { StudentId = 8, FirstName = "Nino", LastName = "Olivetto", EmailAddress = "nolivetto@school.edu", EnrollmentDate = DateTime.Parse("2005-09-01") }
            );
    }
}
