using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Dtos
{
    public class EnrollmentDto
    {
        public int EnrollmentId { get; set; }
        public Grade? Grade { get; set; }

        public int CourseId { get; set; }
        public CourseDto Course { get; set; }

        public int StudentId { get; set; }
        public StudentDto Student { get; set; }
    }
}
