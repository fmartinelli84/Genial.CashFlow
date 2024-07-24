using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genial.Framework.Exceptions
{
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

        public BusinessException(string code, string message)
            : base(message)
        {
            AddError(code, message);
        }

        public BusinessException(string code, string message, Exception innerException)
            : base(message, innerException)
        {
            AddError(code, message);
        }

        private List<ErrorDetail> errors = new List<ErrorDetail>();
        public List<ErrorDetail> Errors
        {
            get { return errors; }
        }

        public BusinessException AddError(string message)
        {
            return AddError(null, message);
        }

        public BusinessException AddError(string? code, string message)
        {
            errors.Add(new ErrorDetail()
            {
                Type = ErrorType.Business,
                Code = code,
                Message = message,
            });

            return this;
        }

        public void ThrowIfHasErrors()
        {
            if (Errors.Any())
                throw this;
        }
    }
}
