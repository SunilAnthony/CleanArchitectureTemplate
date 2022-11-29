using Application.Common.Dtos;
using Application.Common.Models;
using MediatR;


namespace Application.Students.Commands.DeleteStudent
{
    public sealed record DeleteStudentCommand(int Id) : IRequest<bool> { }

    public sealed class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand, bool>
    {
        public Task<bool> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
