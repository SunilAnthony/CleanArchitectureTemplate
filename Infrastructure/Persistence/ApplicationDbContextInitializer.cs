using Domain.Entities;
using Domain.Enums;
using Infrastructure.Identity;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Infrastructure.Persistence;

public static class ApplicationDbContextInitializer
{
    public static void Initialize(ApplicationDbContext context)
    {
        context.Database.EnsureCreated();

        // Look for any students.
        if (context.Students.Any())
        {
            return;   // DB has been seeded
        }

        var students = new Student[]
        {
            new Student{ FirstName="Carson",LastName="Alexander", EmailAddress="calexander@school.edu", EnrollmentDate=DateTime.Parse("2005-09-01")},
            new Student{FirstName="Meredith",LastName="Alonso",EmailAddress="malonso@school.edu", EnrollmentDate=DateTime.Parse("2002-09-01")},
            new Student{FirstName="Arturo",LastName="Anand",EmailAddress="aanand@school.edu", EnrollmentDate=DateTime.Parse("2003-09-01")},
            new Student{FirstName="Gytis",LastName="Barzdukas",EmailAddress="gbarzdukas@school.edu", EnrollmentDate=DateTime.Parse("2002-09-01")},
            new Student{FirstName="Yan",LastName="Li",EmailAddress="yli@school.edu", EnrollmentDate=DateTime.Parse("2002-09-01")},
            new Student{FirstName="Peggy",LastName="Justice",EmailAddress="pjustice@school.edu", EnrollmentDate=DateTime.Parse("2001-09-01")},
            new Student{FirstName="Laura",LastName="Norman",EmailAddress="lnorman@school.edu", EnrollmentDate=DateTime.Parse("2003-09-01")},
            new Student{FirstName="Nino",LastName="Olivetto",EmailAddress="nolivetto@school.edu",EnrollmentDate=DateTime.Parse("2005-09-01")}
        };
        foreach (Student s in students)
        {
            context.Students.Add(s);
        }
        context.SaveChanges();

        var courses = new Course[]
        {
            new Course{CourseId=1050,Title="Chemistry",Credits=3},
            new Course{CourseId=4022,Title="Microeconomics",Credits=3},
            new Course{CourseId=4041,Title="Macroeconomics",Credits=3},
            new Course{CourseId=1045,Title="Calculus",Credits=4},
            new Course{CourseId=3141,Title="Trigonometry",Credits=4},
            new Course{CourseId=2021,Title="Composition",Credits=3},
            new Course{CourseId=2042,Title="Literature",Credits=4}
        };
        foreach (Course c in courses)
        {
            context.Courses.Add(c);
        }
        context.SaveChanges();

        var enrollments = new Enrollment[]
        {
            new Enrollment{StudentId=1,CourseId=1050,Grade=Grade.A},
            new Enrollment{StudentId=1,CourseId=4022,Grade=Grade.C},
            new Enrollment{StudentId=1,CourseId=4041,Grade=Grade.B},
            new Enrollment{StudentId=2,CourseId=1045,Grade=Grade.B},
            new Enrollment{StudentId=2,CourseId=3141,Grade=Grade.F},
            new Enrollment{StudentId=2,CourseId=2021,Grade=Grade.F},
            new Enrollment{StudentId=3,CourseId=1050},
            new Enrollment{StudentId=4,CourseId=1050},
            new Enrollment{StudentId=4,CourseId=4022,Grade=Grade.F},
            new Enrollment{StudentId=5,CourseId=4041,Grade=Grade.C},
            new Enrollment{StudentId=6,CourseId=1045},
            new Enrollment{StudentId=7,CourseId=3141,Grade=Grade.A},
        };
        foreach (Enrollment e in enrollments)
        {
            context.Enrollments.Add(e);
        }
        context.SaveChanges();
    }
}
