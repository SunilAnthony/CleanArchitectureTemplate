using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Persistence.Configurations;

public class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
{
    public void Configure(EntityTypeBuilder<Enrollment> builder)
    {

        builder.ToTable(nameof(Enrollment)).HasKey(k => k.EnrollmentId);
        builder.Property(e => e.StudentId).IsRequired();
        builder.Property(e => e.CourseId).IsRequired();

        builder.HasData
            (
            new Enrollment {EnrollmentId = 1, StudentId = 1, CourseId = 1050, Grade = Grade.A },
            new Enrollment { EnrollmentId = 2, StudentId = 1, CourseId = 4022, Grade = Grade.C },
            new Enrollment { EnrollmentId = 3, StudentId = 1, CourseId = 4041, Grade = Grade.B },
            new Enrollment { EnrollmentId = 4, StudentId = 2, CourseId = 1045, Grade = Grade.B },
            new Enrollment { EnrollmentId = 5, StudentId = 2, CourseId = 3141, Grade = Grade.F },
            new Enrollment { EnrollmentId = 6, StudentId = 2, CourseId = 2021, Grade = Grade.F },
            new Enrollment { EnrollmentId = 7, StudentId = 3, CourseId = 1050 },
            new Enrollment { EnrollmentId = 8, StudentId = 4, CourseId = 1050 },
            new Enrollment { EnrollmentId = 9, StudentId = 4, CourseId = 4022, Grade = Grade.F },
            new Enrollment { EnrollmentId = 10, StudentId = 5, CourseId = 4041, Grade = Grade.C },
            new Enrollment { EnrollmentId = 11, StudentId = 6, CourseId = 1045 },
            new Enrollment { EnrollmentId = 12, StudentId = 7, CourseId = 3141, Grade = Grade.A }
            );

    }
}
