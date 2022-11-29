using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Students.Commands.CreateStudent
{
    internal class CreateStudentCommandValidator : AbstractValidator<CreateStudentCommand>
    {
        public CreateStudentCommandValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();   
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.EmailAddress).NotEmpty().EmailAddress();
        }
    }
}
