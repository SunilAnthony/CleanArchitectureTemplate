using Application.Common.Dtos;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Students.Queries
{
    public class GetAllStudentsQuery : IRequest<IEnumerable<StudentDto>> { }
    public class GetAllStudentsQueryHandler : IRequestHandler<GetAllStudentsQuery, IEnumerable<StudentDto>>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        // DB ORM DI here if needed
        public GetAllStudentsQueryHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<IEnumerable<StudentDto>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
        {
            // Place your Business logic
            return await Task.Run(() =>
            {
                var students = _context.Students
                .Include(x => x.Enrollments)
                .ThenInclude(c => c.Course)
                .ToList();

                return _mapper.Map<IEnumerable<Student>, IEnumerable<StudentDto>>(students);

            });

        }
    }
}
