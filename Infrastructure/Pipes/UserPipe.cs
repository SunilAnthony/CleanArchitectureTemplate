using Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Pipes
{
    public class UserPipe<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : MediatR.IRequest<TResponse>
    {
        private readonly HttpContext httpContext;

        public UserPipe(IHttpContextAccessor accessor)
        {
            httpContext = accessor.HttpContext;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            //var userId = httpContext.User.Claims
            //    .FirstOrDefault(x => x.Type.Equals(ClaimTypes.NameIdentifier)).Value;

            if(request is BaseRequest br)
            {
                br.UserId = "SunilAnthony";
            }

            return await  next();
        }
    }
}
