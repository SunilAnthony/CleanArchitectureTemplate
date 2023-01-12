using Application.Common.Contracts;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositories;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Students.Commands.CreateStudent
{
    public sealed record CreateStudentCommand(int StudentId, string FirstName, string LastName, string EmailAddress) : IRequest<StudentResponse>{}
    public sealed class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, StudentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentRepository _context;
        private readonly IUnitOfWork _unitOfWork;

        public CreateStudentCommandHandler(IMapper mapper, IStudentRepository context, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _context = context;
            _unitOfWork = unitOfWork;
        }
        public async Task<StudentResponse> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            var newStudent = new Student
            { 
                StudentId = request.StudentId,
                FirstName = request.FirstName, 
                LastName = request.LastName, 
                EmailAddress = request.EmailAddress,
                
            };
           
            try
            {
                _context.CreateAsync(newStudent);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                var userResponse = _mapper.Map<Student, StudentResponse>(newStudent);
                return userResponse;
            }
            catch (Exception)
            {
                return new StudentResponse();
            }


        }
    }
}
