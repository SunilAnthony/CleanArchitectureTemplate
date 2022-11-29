using Application.Common.Dtos;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Students.Queries
{
    public sealed class GetAllStudentsQuery : IRequest<IEnumerable<StudentDto>> { }
    public sealed class GetAllStudentsQueryHandler : IRequestHandler<GetAllStudentsQuery, IEnumerable<StudentDto>>
    {
        private readonly IMapper _mapper;
        private readonly IStudentRepository _repo;


        public GetAllStudentsQueryHandler(IMapper mapper, IStudentRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }
        public async Task<IEnumerable<StudentDto>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
        {
            // Place your Business logic
            
            var students = await _repo.GetAllEagerLoadAsync(cancellationToken);
            

            return _mapper.Map<IEnumerable<Student>, IEnumerable<StudentDto>>(students);

        }
    }
}
