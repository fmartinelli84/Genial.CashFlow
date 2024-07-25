using Genial.Framework.Serialization;
using Genial.Framework.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace Genial.Framework.Exceptions
{
    [ExcludeFromCodeCoverage]
    public static class ExceptionHandlerExtensions
    {
        public static IApplicationBuilder UseExceptionHandler(this IApplicationBuilder app, IHostEnvironment env)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async httpContext =>
                {
                    var exceptionHandlerFeature = httpContext.Features.Get<IExceptionHandlerFeature>();

                    if (exceptionHandlerFeature is not null)
                    {
                        var exception = exceptionHandlerFeature.Error;
                        var statusCode = BusinessProblemDetails.GetStatusCode(exception);

                        httpContext.Response.ContentType = "application/problem+json";
                        httpContext.Response.StatusCode = statusCode;

                        var problemDetailsFactory = app.ApplicationServices.GetRequiredService<ProblemDetailsFactory>();

                        var problemDetails = BusinessProblemDetails.Create(httpContext, problemDetailsFactory);

                        var problemDetailsJson = problemDetails.ToJson();

                        await httpContext.Response.WriteAsync(problemDetailsJson);
                    }
                });
            });

            return app;
        }
    }
}
