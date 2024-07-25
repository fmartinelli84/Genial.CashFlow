using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using System.Security.Claims;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using System.Threading.Channels;
using System.Linq;
using System.Reflection;
using System.IO;
using System.Text.Json.Serialization;
using Genial.Framework.Exceptions;
using Genial.Framework.Web;
using Genial.Framework.Data;
using Genial.Framework.Serialization;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Genial.Framework.Api
{
    public static class ApiExtensions
    {
        public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<WebOptions>(configuration.GetSection("Web"));

            if (!string.IsNullOrWhiteSpace(configuration.GetSection("Web")?.Get<WebOptions>()?.Address))
            {
                services.AddCors(options =>
                {
                    var webAddresses = configuration.GetSection("Web").Get<WebOptions>()!.Address!
                        .Split(";")
                        .Where(a => !string.IsNullOrWhiteSpace(a))
                        .Select(a => new Uri(a.Trim()).GetLeftPart(UriPartial.Authority))
                        .ToArray();

                    options.AddDefaultPolicy(builder =>
                        builder.WithOrigins(webAddresses)
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials());
                });
            }

            var mvcBuilder = services.AddControllers(options =>
            {
                options.Filters.Add<NotFoundResultFilter>();
            })
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });

            services.AddHttpContextAccessor();

            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = Assembly.GetEntryAssembly()!.GetName().Name, Version = "V1" });

                // Set the comments path for the Swagger JSON and UI.
                foreach (var xmlFile in Directory.GetFiles(AppContext.BaseDirectory, "*.XML", SearchOption.AllDirectories))
                {
                    // pick comments from classes, include controller comments: another tip from StackOverflow
                    options.IncludeXmlComments(xmlFile, includeControllerXmlComments: true);
                }
            });

            services.AddHealthChecks();

            return services;
        }

        public static IApplicationBuilder UseApi(this IApplicationBuilder app, IHostEnvironment env)
        {
            app.UseExceptionHandler(env);

            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
            app.UseHttpsRedirection();


            app.UseStaticFiles();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{Assembly.GetEntryAssembly()!.GetName().Name} V1");
            });

            app.UseRouting();

            app.UseCors();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/healthz");
            });

            return app;
        }
    }
}
