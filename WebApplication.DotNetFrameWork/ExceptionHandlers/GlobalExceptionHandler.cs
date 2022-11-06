using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;

namespace WebApplication.DotNetFrameWork.ExceptionHandlers
{
    public class GlobalExceptionHandler : ExceptionHandler
    {
        public override Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            //You can change the result and content of the result as you want.
            context.Result = new InternalServerErrorResult(context.Request);
            return Task.CompletedTask;
        }
    }
}