using System.Reflection;
using System.Text;
using AnimalProtecction.Generated;
using AnimalProtection.Api.Configuration;
using AnimalProtection.Api.Filters;
using AnimalProtection.Domain.Shared;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using ClinicMedicalAppointments.Api.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Agregar configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200") // Dominios permitidos
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

// Configuración de la cadena de conexión
var connectionString = builder.Configuration.GetConnectionString("AnimalProtectionDb");
builder.Services.AddDbContext<AnimalprotectionContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddHttpClient<RecaptchaMiddleware>();
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JWT"));
builder.Services.Configure<Settings>(builder.Configuration.GetSection("Settings"));
builder.Services.Configure<RateLimitingSettings>(builder.Configuration.GetSection("RateLimiting"));
builder.Services.Configure<GitHubSettings>(builder.Configuration.GetSection("GitHubSettings"));

//var redisConnectionString = builder.Configuration["Redis:ConnectionString"];
//var redis = ConnectionMultiplexer.Connect(redisConnectionString);

// Configurar Autofac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new ApplicationModule());
    containerBuilder.RegisterModule(new InfrastructureModule());
    containerBuilder.RegisterAssemblyTypes(typeof(Program).Assembly)
        .Where(t => t.Name.EndsWith("Controller"))
        .AsSelf();
});

// Configuración de versionado de API
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
});

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV"; // Formato de versión (v1, v2, etc.)
    options.SubstituteApiVersionInUrl = true; // Sustituye {version} en las rutas
});

// Configuración de Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Animal Protection API - Versión 1",
        Version = "v1",
        Description = "Documentación para la versión 1 de la API de Animal Protection."
    });

    options.SwaggerDoc("v2", new OpenApiInfo
    {
        Title = "Animal Protection API - Versión 2",
        Version = "v2",
        Description = "Documentación para la versión 2 de la API de Animal Protection."
    });

    // Incluir la documentación XML
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

// Configurar controladores
builder.Services.AddControllers();

// TODO: Corregir no se estan mostrando correctamente los caracteres especiales
// builder.Services.AddControllers()
//     .AddJsonOptions(options =>
//     {
//         options.JsonSerializerOptions.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
//         options.JsonSerializerOptions.PropertyNamingPolicy = null; // Evita que los nombres de las propiedades se serialicen en camelCase
//     });
// builder.Services.AddDbContext<AnimalprotectionContext>(options =>
//     options.UseNpgsql(connectionString, x => x.EnableRetryOnFailure())
//         .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
// );

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });
var app = builder.Build();

// Habilitar CORS antes de usar controladores
app.UseCors("AllowSpecificOrigins");

// Configuración del pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "API Versión 1");
        options.SwaggerEndpoint("/swagger/v2/swagger.json", "API Versión 2");
    });
}

//app.UseMiddleware<ValidationMiddleware>();
//app.UseMiddleware<CsrfProtectionMiddleware>();
app.UseMiddleware<ApiKeyMiddleware>();
app.UseMiddleware<RateLimitingMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();