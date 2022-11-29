using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Persistence.Configurations;

public sealed class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable(nameof(Course)).HasKey(k => k.CourseId);
        builder.Property(t => t.Title)
            .HasColumnType("varchar(50)")
            .IsRequired();
        builder.Property(t => t.Credits)
           .IsRequired();

        builder.HasData(

            new Course { CourseId = 1050, Title = "Chemistry", Credits = 3 },
            new Course { CourseId = 4022, Title = "Microeconomics", Credits = 3 },
            new Course { CourseId = 4041, Title = "Macroeconomics", Credits = 3 },
            new Course { CourseId = 1045, Title = "Calculus", Credits = 4 },
            new Course { CourseId = 3141, Title = "Trigonometry", Credits = 4 },
            new Course { CourseId = 2021, Title = "Composition", Credits = 3 },
            new Course { CourseId = 2042, Title = "Literature", Credits = 4 }
            );


    }
}
