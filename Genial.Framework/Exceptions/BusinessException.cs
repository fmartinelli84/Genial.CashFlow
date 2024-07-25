using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace Genial.Framework.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class BusinessException : ApplicationException
    {
        public BusinessException()
            : base()
        {
        }

        public BusinessException(string message)
            : base(message)
        {
            AddError(message);
        }

        public BusinessException(string message, Exception innerException)
            : base(message, innerException)
        {
            AddError(message);
        }

        public BusinessException(int code, string message)
            : this(message)
        {
            Code = code;
        }

        public BusinessException(int code, string message, Exception innerException)
            : this(message, innerException)
        {
            Code = code;
        }

        public BusinessException(int code)
            : base()
        {
            Code = code;
        }

        public BusinessException(int code, Exception innerException)
            : base(null, innerException)
        {
            Code = code;
        }

        public BusinessException(ProblemDetails problemDetails)
            : base(problemDetails?.Title)
        {
            Code = problemDetails?.Status;

            if (problemDetails is BusinessProblemDetails businessProblemDetails)
                errors = businessProblemDetails.Errors.ToList();
        }

        public BusinessException(ProblemDetails problemDetails, Exception innerException)
            : base(problemDetails?.Title, innerException)
        {
            Code = problemDetails?.Status;

            if (problemDetails is BusinessProblemDetails businessProblemDetails)
                errors = businessProblemDetails.Errors.ToList();
        }

        public int? Code { get; protected set; }

        private List<string> errors = new List<string>();
        public IEnumerable<string> Errors
        {
            get { return errors; }
        }

        public BusinessException AddError(string message)
        {
            errors.Add(message);

            return this;
        }

        public void ThrowIfHasErrors()
        {
            if (Errors.Any())
                throw this;
        }
    }
}
