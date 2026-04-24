using Backend.Middlewares;
using Backend.Modules.Absences.Application.Ports;
using Backend.Modules.Absences.Application.UseCases.Approve;
using Backend.Modules.Absences.Application.UseCases.Create;
using Backend.Modules.Absences.Application.UseCases.GetPending;
using Backend.Modules.Absences.Application.UseCases.Reject;
using Backend.Modules.Absences.Infrastructure;
using Backend.Modules.Users.Api.Register;
using Backend.Modules.Users.Application;
using Backend.Modules.Users.Application.UseCases.Register;
using Backend.Modules.Users.Infrastructure;
using Backend.Modules.Users.Ports;
using Backend.Shared;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddControllers();

builder.Services
    .AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters();

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
builder.Services.AddScoped<CreateAbsenceHandler>();
builder.Services.AddScoped<ApproveAbsenceHandler>();
builder.Services.AddScoped<RejectAbsenceHandler>();
builder.Services.AddScoped<GetPendingAbsencesQuery>();



var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();
app.Run();
