using Application.Common.Contracts;
using Application.Common.Models;
using Application.Students.Commands.CreateStudent;
using Application.Students.Commands.DeleteStudent;
using Application.Students.Commands.UpdateStudent;
using Application.Students.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Administrator")]
    public class StudentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StudentsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Get All Students
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet(Name ="Get All Student")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(List<StudentResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IEnumerable<StudentResponse>> Get(CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetAllStudentsQuery());
        }

        /// <summary>
        /// Get Student by Id
        /// </summary>
        /// <param name="StudentId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{StudentId}", Name = "GetById")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(StudentResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<StudentResponse> GetById(int StudentId, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetStudentByIdQuery(StudentId));
        }
        /// <summary>
        /// Create a new Student Record
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(StudentResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
 
        public async Task<IActionResult> CreateStudent([FromBody] CreateStudentCommand request, CancellationToken cancellationToken)
        {
            var studentResponse = await _mediator.Send(request, cancellationToken);

            return CreatedAtAction(nameof(GetById), new { StudentId = studentResponse.StudentId}, studentResponse);
        }
        /// <summary>
        /// Update a student record
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(StudentResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
       
        public async Task<IActionResult> UpdateStudent([FromBody] UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            var studentResponse = await _mediator.Send(request, cancellationToken);

            if(studentResponse != null)
                return Ok(studentResponse);

            return NotFound();
        }
        /// <summary>
        /// Delete a student by id
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
       
        public async Task<IActionResult> DeleteStudent([FromBody] DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            var isDeleted = await _mediator.Send(request, cancellationToken);
            if (!isDeleted)
                return NotFound();

            return NoContent();
        }

    }
}
