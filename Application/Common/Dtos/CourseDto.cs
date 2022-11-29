using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Dtos
{
    public sealed class CourseDto
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }

        public List<EnrollmentDto> Enrollments { get; set; }
    }
}
