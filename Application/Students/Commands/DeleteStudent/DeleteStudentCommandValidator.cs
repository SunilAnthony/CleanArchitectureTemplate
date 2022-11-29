using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Students.Commands.DeleteStudent
{
    internal class DeleteStudentCommandValidator : AbstractValidator<DeleteStudentCommand>
    {
        public DeleteStudentCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
