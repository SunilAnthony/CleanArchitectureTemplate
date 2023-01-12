using Application.Common.Contracts;
using Application.Common.Interfaces.Repositories;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Students.Commands.UpdateStudent
{
    public sealed record UpdateStudentCommand(int StudentId, string FirstName, string LastName, string EmailAddress) : IRequest<StudentResponse> {}
    public sealed class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, StudentResponse>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateStudentCommandHandler(IStudentRepository studentRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<StudentResponse> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            

            try
            {
                //Get Student by Id
                var student = await _studentRepository.GetAsync(request.StudentId);

                if (student != null)
                {
                    //Update Student
                    student.FirstName = request.FirstName;
                    student.LastName = request.LastName;
                    student.EmailAddress = request.EmailAddress;

                    _studentRepository.UpdateAsync(student);
                    await _unitOfWork.SaveChangesAsync(cancellationToken);
                    var userResponse = _mapper.Map<Student, StudentResponse>(student);
                    return userResponse;
                }
                return null;
            }
            catch (Exception)
            {

                return null;
            }
            
            
        }
    }
}
