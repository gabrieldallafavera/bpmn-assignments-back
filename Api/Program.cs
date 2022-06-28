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
using Api.Helpers.Exceptions.CustomExceptions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<Context>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));

Scope.OnScopeCreating(builder.Services);

builder.Services.AddHttpContextAccessor();

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Logging.ClearProviders();

builder.Logging.AddSerilog(logger);

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Autoriza��o padr�o com header usando o esquema Bearer (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();

    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Api.BpmnAssignments",
        Description = "Api com fun��es do BPMN para constru��o de tarefas.",
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
    //.AddCookie(options =>
    //{
    //    options.LoginPath = "/unauthorized";
    //    options.AccessDeniedPath = "/forbidden";
    //})
    .AddJwtBearer(options =>
    {
        //options.Audience = "https://localhost:7074/";
        //options.Authority = "https://localhost:7074/";
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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api.BpmnAssignments");
    });
}
app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

//app.MapGet("unauthorized", () => { throw new UnauthorizedAccessException("Acesso n�o autorizado."); });

//app.MapGet("forbidden", () => { throw new ForbiddenException("Acesso negado."); });

app.Run();
