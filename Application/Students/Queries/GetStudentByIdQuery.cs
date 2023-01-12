using Application.Common.Contracts;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositories;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System.Net;

namespace Application.Students.Queries
{
    public sealed record GetStudentByIdQuery(int Id): IRequest<StudentResponse>{}
    public sealed class GetStudentByIdQueryHandler : IRequestHandler<GetStudentByIdQuery, StudentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentRepository _repo;
        public GetStudentByIdQueryHandler(IMapper mapper, IStudentRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }
        public async Task<StudentResponse> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            Student? student = await _repo.GetAsync(request.Id);
            if (student is null)
                new StudentResponse();
            StudentResponse studentResponse = _mapper.Map<Student, StudentResponse>(student!);
            return studentResponse;
        }
    }
}
