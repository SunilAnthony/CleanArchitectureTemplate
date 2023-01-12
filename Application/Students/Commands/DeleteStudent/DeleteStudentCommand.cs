using Application.Common.Contracts;
using Application.Common.Interfaces.Repositories;
using Application.Common.Models;
using Domain.Entities;
using MediatR;


namespace Application.Students.Commands.DeleteStudent
{
    public sealed record DeleteStudentCommand(int StudentId) : IRequest<bool> { }

    public sealed class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand, bool>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteStudentCommandHandler(IStudentRepository studentRepository, IUnitOfWork unitOfWork)
        {
            _studentRepository = studentRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            Student studentToDelete = await _studentRepository.GetAsync(request.StudentId);
            if (studentToDelete is null) 
            {
                return false;
            }
            else
            {
                _studentRepository.DeleteAsync(studentToDelete!);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return true;

            }
        }
    }
}
