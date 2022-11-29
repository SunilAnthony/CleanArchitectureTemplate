using Application.Common.Dtos;
using Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Students.Commands.UpdateStudent
{
    public sealed record UpdateStudentCommand(int Id, string FirstName, string LastName, string EmailAddress) : IRequest<Response<StudentDto>> {}
    public sealed class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, Response<StudentDto>>
    {
        public Task<Response<StudentDto>> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
