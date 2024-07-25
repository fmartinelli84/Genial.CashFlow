using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace Genial.Framework.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class NotFoundBusinessException : BusinessException
    {
        public NotFoundBusinessException()
            : base(StatusCodes.Status404NotFound)
        {
        }

        public NotFoundBusinessException(string message)
            : base(StatusCodes.Status404NotFound, message)
        {
        }

        public NotFoundBusinessException(string message, Exception innerException)
            : base(StatusCodes.Status404NotFound, message, innerException)
        {
        }

        public NotFoundBusinessException(ProblemDetails problemDetails)
            : base(problemDetails)
        {
            Code = StatusCodes.Status404NotFound;
        }

        public NotFoundBusinessException(ProblemDetails problemDetails, Exception innerException)
            : base(problemDetails, innerException)
        {
            Code = StatusCodes.Status404NotFound;
        }
    }
}
