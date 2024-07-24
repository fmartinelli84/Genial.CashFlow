using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Genial.Framework.Api
{
    public class NotFoundResultFilter : IActionFilter
    {
        public NotFoundResultFilter()
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is ObjectResult objectResult &&
                objectResult.DeclaredType != null &&
                objectResult.StatusCode == 200 &&
                objectResult.Value == null)
            {
                context.Result = new NotFoundResult();
            }
        }
    }
}
