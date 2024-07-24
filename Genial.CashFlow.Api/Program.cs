using Genial.CashFlow.Application;
using Genial.CashFlow.Domain;
using Genial.CashFlow.Infrastructure;
using Genial.CashFlow.Infrastructure.Data;
using Genial.Framework.Api;
using Genial.Framework.Data;
using Genial.Framework.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.ConfigureLogging();

builder.Services.AddApi(builder.Configuration);

builder.Services.AddApplication(builder.Configuration);
builder.Services.AddDomain(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseApi(app.Environment);

// Create a migrate database
app.MigrateDatabase();

app.Run();