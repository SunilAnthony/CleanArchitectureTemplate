using Application.Common.Dtos;
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
    public sealed record CreateStudentCommand(string FirstName, string LastName, string EmailAddress) : IRequest<Response<StudentDto>>{}
    public sealed class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, Response<StudentDto>>
    {
        private readonly IMapper _mapper;
        private readonly IStudentRepository _context;
        public CreateStudentCommandHandler(IMapper mapper, IStudentRepository context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<Response<StudentDto>> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            var newStudentDto = new StudentDto
            { 
                FirstName = request.FirstName, 
                LastName = request.LastName, 
                EmailAddress = request.EmailAddress,
                
            };
            var newStudent = _mapper.Map<StudentDto, Student>(newStudentDto);
            try
            {
                await _context.CreateAsync(newStudent, cancellationToken);
                newStudentDto.StudentId = newStudent.StudentId;
                return Response.Ok<StudentDto>(newStudentDto, HttpStatusCode.Created, "New Student Created");
            }
            catch (Exception)
            {
                return Response.Fail<StudentDto>("Fail to create Student",HttpStatusCode.BadRequest);
            }


        }
    }
}
