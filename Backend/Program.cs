using Backend.Middlewares;
using Backend.Modules.Absences.Application.Ports;
using Backend.Modules.Absences.Application.UseCases.Approve;
using Backend.Modules.Absences.Application.UseCases.Create;
using Backend.Modules.Absences.Application.UseCases.GetByUser;
using Backend.Modules.Absences.Application.UseCases.GetPending;
using Backend.Modules.Absences.Application.UseCases.GetPendingByUser;
using Backend.Modules.Absences.Application.UseCases.Reject;
using Backend.Modules.Absences.Infrastructure;
using Backend.Modules.Users.Api.Register;
using Backend.Modules.Users.Application;
using Backend.Modules.Users.Application.UseCases.Register;
using Backend.Modules.Users.Infrastructure;
using Backend.Modules.Users.Application.Ports;
using Backend.Shared;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Modules.Users.Application.Auth;
using Backend.Modules.Users.Infrastructure.Auth;
using Backend.Modules.Users.Application.UseCases.Login;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddScoped<IJwtProvider, JwtProvider>();

var jwtKey = builder.Configuration["Jwt:Key"]!;

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
         new TokenValidationParameters
         {
             ValidateIssuer = true,
             ValidateAudience = true,
             ValidateLifetime = true,
             ValidateIssuerSigningKey = true,

             ValidIssuer = builder.Configuration["Jwt:Issuer"],
             ValidAudience = builder.Configuration["Jwt:Audience"],
             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
         };
    });
builder.Services.AddAuthorization();

builder.Services.AddControllers();

builder.Services
    .AddFluentValidationAutoValidation(options =>
    {
        options.DisableDataAnnotationsValidation = true;
    })
    .AddFluentValidationClientsideAdapters();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = false;

    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(x => x.Value!.Errors.Count > 0)
            .ToDictionary(
                x => x.Key,
                x => x.Value!.Errors
                    .Select(e => e.ErrorMessage)
                    .ToArray()
            );

        return new BadRequestObjectResult(new
        {
            code = "validation.failed",
            message = "One or more validation errors occurred.",
            status = 400,
            errors
        });
    };
});

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("Default")
    );
});

builder.Services.AddValidatorsFromAssemblyContaining<RegisterUserValidator>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAbsenceRepository, AbsenceRepository>();
builder.Services.AddScoped<IAbsenceQueries, AbsenceQueries>();

builder.Services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<RegisterUserHandler>();
builder.Services.AddScoped<LoginUserHandler>();
builder.Services.AddScoped<CreateAbsenceHandler>();
builder.Services.AddScoped<ApproveAbsenceHandler>();
builder.Services.AddScoped<RejectAbsenceHandler>();
builder.Services.AddScoped<GetPendingAbsencesHandler>();
builder.Services.AddScoped<GetPendingAbsencesByUserHandler>();
builder.Services.AddScoped<GetAbsencesByUserHandler>();




var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
