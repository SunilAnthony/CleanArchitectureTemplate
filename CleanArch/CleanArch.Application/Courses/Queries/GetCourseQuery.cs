using CleanArch.Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Application.Courses.Queries
{
    public class GetCourseQuery
    {
        private readonly UniversityDBContext _context;

        public GetCourseQuery(UniversityDBContext context)
        {
            _context = context;
        }
    }
}
