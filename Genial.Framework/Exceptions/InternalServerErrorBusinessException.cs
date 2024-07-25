using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace Genial.Framework.Exceptions;

[ExcludeFromCodeCoverage]
public class InternalServerErrorBusinessException : BusinessException
{
    public InternalServerErrorBusinessException()
        : base(StatusCodes.Status500InternalServerError)
    {
    }

    public InternalServerErrorBusinessException(string message)
        : base(StatusCodes.Status500InternalServerError, message)
    {
    }

    public InternalServerErrorBusinessException(string message, Exception innerException)
        : base(StatusCodes.Status500InternalServerError, message, innerException)
    {
    }

    public InternalServerErrorBusinessException(ProblemDetails problemDetails)
        : base(problemDetails)
    {
        Code = StatusCodes.Status500InternalServerError;
    }

    public InternalServerErrorBusinessException(ProblemDetails problemDetails, Exception innerException)
        : base(problemDetails, innerException)
    {
        Code = StatusCodes.Status500InternalServerError;
    }
}
