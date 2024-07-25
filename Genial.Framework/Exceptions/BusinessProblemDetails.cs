using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Genial.Framework.Exceptions
{
    public class BusinessProblemDetails : ProblemDetails
    {
        public BusinessProblemDetails()
        {
        }

        public BusinessProblemDetails(IEnumerable<string> errors)
            : this()
        {
            Errors = errors;
        }

        public IEnumerable<string> Errors { get; set; } = new List<string>();


        public static int GetStatusCode(Exception exception)
        {
            if (exception is BusinessException businessException)
            {
                if (businessException.Code is not null &&
                    businessException.Code >= StatusCodes.Status100Continue &&
                    businessException.Code <= StatusCodes.Status511NetworkAuthenticationRequired)
                {
                    return businessException.Code.Value;
                }

                return StatusCodes.Status400BadRequest;
            }
            else if (exception is ValidationException)
            {
                return StatusCodes.Status400BadRequest;
            }

            return StatusCodes.Status500InternalServerError;
        }

        public static ProblemDetails Create(HttpContext httpContext, ProblemDetailsFactory problemDetailsFactory)
        {
            var exceptionHandlerFeature = httpContext.Features.Get<IExceptionHandlerFeature>();

            if (exceptionHandlerFeature is null)
                throw new InvalidOperationException("IExceptionHandlerFeature cannot bee null.");

            var exception = exceptionHandlerFeature.Error;
            var statusCode = GetStatusCode(exception);

            var defaultProblemDetails = problemDetailsFactory.CreateProblemDetails(httpContext, statusCode: statusCode);
            var defaultValidationProblemDetails = problemDetailsFactory.CreateValidationProblemDetails(httpContext, new Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary(), statusCode: statusCode);


            ProblemDetails problemDetails;

            if (exception is BusinessException businessException)
            {
                if (businessException.Errors.Any())
                {
                    problemDetails = new BusinessProblemDetails(businessException.Errors)
                    {
                        Title = statusCode == StatusCodes.Status400BadRequest ? defaultValidationProblemDetails?.Title : defaultProblemDetails?.Title,
                        Detail = businessException.InnerException?.ToString()
                    };
                }
                else
                {
                    problemDetails = new ProblemDetails()
                    {
                        Title = defaultProblemDetails.Title,
                        Detail = businessException.InnerException?.ToString()
                    };
                }
            }
            else if (exception is ValidationException validationException)
            {
                var dict = validationException.Errors
                     .GroupBy(e => e.PropertyName)
                     .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());

                problemDetails = new HttpValidationProblemDetails(dict)
                {
                    Title = defaultValidationProblemDetails?.Title,
                };
            }
            else
            {
                problemDetails = new ProblemDetails()
                {
                    Title = !string.IsNullOrWhiteSpace(exception.Message) ? exception.Message : defaultProblemDetails.Title,
                    Detail = exception.ToString()
                };
            }

            problemDetails.Type = defaultProblemDetails?.Type;
            problemDetails.Status = statusCode;

            defaultProblemDetails?.Extensions?.ToList()?.ForEach(extension => problemDetails.Extensions.Add(extension.Key, extension.Value));

            return problemDetails;
        }


    }
}
