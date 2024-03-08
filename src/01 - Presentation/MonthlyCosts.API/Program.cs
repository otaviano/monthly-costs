using MonthlyCosts.API.Filters;
using MonthlyCosts.Infra.IoC;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration
    .AddJsonFile("appsettings.json", false, true)
    .AddEnvironmentVariables();

var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger(configuration);
builder.Services.AddHealthChecks(configuration);
builder.Services.AddApiVersion();
builder.Services.AddSettings(configuration);
builder.Services.AddAutoMapper();
builder.Services.AddHttpConfiguration(typeof(HttpGlobalExceptionFilter));
builder.Services.AddDomain();
builder.Services.AddApplication();
builder.Services.AddRepositories();
builder.Services.AddSqlConnection(configuration);
builder.Services.AddNoSqlConnection();
builder.Services.AddMessageBrokerInMemmory();
builder.Services.AddMessageBroker(configuration);

var app = builder
    .Build();

// Configure the HTTP request pipeline.
app.ConfigureMigrations();
app.ConfigureSwagger(configuration);
app.ConfigureHealthCheckEndpoints(configuration);
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
