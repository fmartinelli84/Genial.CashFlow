using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Protocol;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace Genial.Framework.Exceptions
{

    [ExcludeFromCodeCoverage]
    public class UnauthorizedBusinessException : BusinessException
    {
        public UnauthorizedBusinessException()
            : base(StatusCodes.Status401Unauthorized)
        {
        }

        public UnauthorizedBusinessException(string message)
            : base(StatusCodes.Status401Unauthorized, message)
        {
        }

        public UnauthorizedBusinessException(string message, Exception innerException)
            : base(StatusCodes.Status401Unauthorized, message, innerException)
        {
        }

        public UnauthorizedBusinessException(ProblemDetails problemDetails)
            : base(problemDetails)
        {
            Code = StatusCodes.Status401Unauthorized;
        }

        public UnauthorizedBusinessException(ProblemDetails problemDetails, Exception innerException)
            : base(problemDetails, innerException)
        {
            Code = StatusCodes.Status401Unauthorized;
        }
    }
}
