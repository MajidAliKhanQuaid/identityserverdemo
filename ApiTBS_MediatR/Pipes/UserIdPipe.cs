using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTBS_MediatR.Pipes
{
    public class UserIdPipe<TIn, TOut> : IPipelineBehavior<TIn, TOut>
    {
        private HttpContext httpContext;

        public UserIdPipe(IHttpContextAccessor httpContextAccessor)
        {
            httpContext = httpContextAccessor.HttpContext;
        }
        public Task<TOut> Handle(TIn request, CancellationToken cancellationToken, RequestHandlerDelegate<TOut> next)
        {
            if (request is BaseRequest br)
            {
                br.UserId = httpContext.User?.Claims?.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;
            }
            return next();
        }
    }
}
