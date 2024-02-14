using Microsoft.AspNetCore.Mvc.ApiExplorer;
using MonthlyCosts.Infra.IoC;
using System;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger(configuration);
builder.Services.AddHealthChecks(configuration);
builder.Services.AddApiVersion();
builder.Services.AddSettings(configuration);
builder.Services.AddAutoMapper();
builder.Services.AddDomain();
builder.Services.AddApplication();
builder.Services.AddRepositories();
builder.Services.AddNoSqlConnection();
builder.Services.AddMessageBrokerInMemmory();

var app = builder.Build();
var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

// Configure the HTTP request pipeline.
app.ConfigureSwagger(configuration, provider);
app.ConfigureHealthCheckEndpoints(configuration);
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
