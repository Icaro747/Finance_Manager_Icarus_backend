using Finance_Manager_Icarus.Authentication;
using Finance_Manager_Icarus.Data;
using Finance_Manager_Icarus.Extensions;
using Finance_Manager_Icarus.Repositories.Base;
using Finance_Manager_Icarus.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configurando o contexto do banco de dados MySQL
builder.Services.AddDbContext<FinanceManagerIcarusContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configurando JWT
builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtConfig"));
var jwtConfig = builder.Configuration.GetSection("JwtConfig").Get<JwtConfig>();
var key = Encoding.UTF8.GetBytes(jwtConfig.SecretKey);

// Adicionando Logging
builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddConsole();
    loggingBuilder.AddDebug();
    loggingBuilder.AddConfiguration(builder.Configuration.GetSection("Logging"));
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
            var request = context.HttpContext.Request;
            logger.LogError("Authentication failed. Reason: {Reason}. Path: {Path}, Method: {Method}",
                context.Exception.Message,
                request.Path,
                request.Method
            );
            return Task.CompletedTask;
        },
        OnTokenValidated = context =>
        {
            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
            var request = context.HttpContext.Request;
            logger.LogInformation("Token validated. User: {User}, Path: {Path}, Method: {Method}",
                context.Principal.Identity?.Name ?? "Unknown",
                request.Path,
                request.Method
            );
            return Task.CompletedTask;
        }
    };

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtConfig.ValidIssuer,
        ValidAudience = jwtConfig.ValidAudience,
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

// Configurando Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Akkadian Plataforma API", Version = "v1" });

    var securityScheme = new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = JwtBearerDefaults.AuthenticationScheme
        }
    };

    c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { securityScheme, new string[] { } }
    });
});

// Injetar serviços e repositórios
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IPasswordHasher, BcryptPasswordHasher>();


RepositoryConfigurator.AddRepositories(builder.Services, typeof(Program).Assembly, typeof(MainRepository));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
builder.Services.AddControllers(options =>
{
    options.Filters.Add<CustomValidationFilter>();
})
.AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors(option =>
{
    option.AllowAnyOrigin();
    option.AllowAnyHeader();
    option.AllowAnyMethod();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
