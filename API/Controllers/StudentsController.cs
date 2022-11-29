using Application.Common.Dtos;
using Application.Common.Models;
using Application.Students.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StudentsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet(Name ="Get All Student")]
        public async Task<IEnumerable<StudentDto>> Get()
        {
            return await _mediator.Send(new GetAllStudentsQuery());
        }

        [HttpGet("{id}", Name = "Get Student by Id")]
        
        public async Task<Response<StudentDto>> GetById(int id)
        {
            return await _mediator.Send(new GetStudentByIdQuery(id));
        }
    }
}
