global using Microsoft.EntityFrameworkCore;
global using Api.Database;
using Api.Scopes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using System.Text;
using Api.Helpers.Exceptions;
using Api.Helpers.StatusCodes;

var builder = WebApplication.CreateBuilder(args);

var server = builder.Configuration["DbServer"];
var user = builder.Configuration["DbUser"];
var password = builder.Configuration["Password"];
var database = builder.Configuration["Database"];

string connectionString = builder.Configuration.GetConnectionString("Connection");

if (!string.IsNullOrEmpty(server) && !string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(database))
{
    connectionString = $"Server={server}; Initial Catalog={database}; User ID={user}; Password={password}; Persist Security Info=True;";
}

builder.Services.AddDbContext<Context>(options => options.UseSqlServer(connectionString));

Scope.OnScopeCreating(builder.Services);

builder.Services.AddHttpContextAccessor();

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Logging.ClearProviders();

builder.Logging.AddSerilog(logger);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Autorização padrão com header usando o esquema Bearer (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();

    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Api.BpmnAssignments",
        Description = "Api com funções do BPMN para construção de tarefas.",
    });

    options.DocInclusionPredicate((_, api) => true);
    options.TagActionsBy(api => new[] { api.GroupName ?? api.ActionDescriptor.RouteValues["controller"] });

    string[] methodsOrder = new string[] { "get", "post", "put", "delete", "head", "connect", "options", "trace", "patch" };
    options.OrderActionsBy(apiDesc => $"{apiDesc.GroupName ?? apiDesc.ActionDescriptor.RouteValues["controller"]}_{apiDesc.ActionDescriptor.RouteValues["controller"]}_{Array.IndexOf(methodsOrder, apiDesc?.HttpMethod?.ToLower())}");

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api.BpmnAssignments");
});

using (var scope = app.Services.CreateScope()) {
    var context = scope.ServiceProvider.GetRequiredService<Context>();
    context.Database.Migrate();
}

app.UseMiddleware<StatusCodeHandlerMiddleware>();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
