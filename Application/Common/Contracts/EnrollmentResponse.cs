using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Contracts
{
    public sealed class EnrollmentResponse
    {
        public int EnrollmentId { get; set; }
        public Grade? Grade { get; set; }

        public int CourseId { get; set; }
        public CourseResponse Course { get; set; }

        public int StudentId { get; set; }
        public StudentResponse Student { get; set; }
    }
}
