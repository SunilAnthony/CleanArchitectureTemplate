using Application.Common.Interfaces.Services;
using Application.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;

        public TokenController(ITokenService tokenService, IConfiguration configuration)
        {
            _tokenService = tokenService;
            _configuration = configuration;
        }
        /// <summary>
        ///  Request a bearer token
        /// </summary>
        /// <param name="model"></param>
        /// <returns>A bearer token with a refresh token</returns>
        /// <response code="200">A bearer token with a refresh token</response>
        /// <response code="400">If their is a problem with the request</response>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] Login model)
        {
            var result = await _tokenService.Login(model.Email, model.Password);
            if(result is null)
                return BadRequest("Bad username and password");
            return Ok(result);
        }
        /// <summary>
        ///  Request a new bearer token with a refresh token
        /// </summary>
        /// <param name="model"></param>
        /// <returns>A bearer token with a refresh token</returns>
        /// <response code="200">A bearer token with a refresh token</response>
        /// <response code="400">If their is a problem with the request</response>
        [HttpPost("refresh")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Refresh([FromBody] RefreshLogin model)
        {
            var response = await _tokenService.Refresh(model.Token, model.RefreshToken);
            if (string.IsNullOrEmpty(response.Message))
                return BadRequest(response.Result);
            return new ObjectResult(new
            {
                token = response.Result.Token,
                refreshToken = response.Result.RefreshToken,
                token_type = response.Result.TokenType,
                expires_on = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["JWTSettings:AccessTokenDurationInMinutes"]))
            });

        }
        /// <summary>
        ///  if your bearer token is compromise, you can make a request to revoke further API request
        /// </summary>
        /// <returns>Nothing</returns>
        /// <response code="204">Nothing</response>
        /// <response code="400">If their is a problem with the request</response>
        [HttpPost("revoke")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Revoke()
        {
            var result = await _tokenService.Revoke();
            if (result)
                return Ok(new { message = "Token was successfuly revoke." });
            return BadRequest();
        }

    }
}
