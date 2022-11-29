using Application.Common.Dtos;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositories;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System.Net;

namespace Application.Students.Queries
{
    public sealed record GetStudentByIdQuery(int Id): IRequest<Response<StudentDto>>{}
    public sealed class GetStudentByIdQueryHandler : IRequestHandler<GetStudentByIdQuery, Response<StudentDto>>
    {
        private readonly IMapper _mapper;
        private readonly IStudentRepository _repo;
        public GetStudentByIdQueryHandler(IMapper mapper, IStudentRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }
        public async Task<Response<StudentDto>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            var student = await _repo.FindAsync(x => x.StudentId == request.Id, cancellationToken);
            if (student is null)
                return Response.Fail<StudentDto>("Not Found", HttpStatusCode.NotFound);
            var studentDto =_mapper.Map<Student,StudentDto>(student.FirstOrDefault());
            return Response.Ok<StudentDto>(studentDto, HttpStatusCode.OK, "Success");
        }
    }
}
