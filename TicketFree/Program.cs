using Microsoft.OpenApi.Models;
using TicketFree.Db;
<<<<<<< Updated upstream
=======
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using FluentValidation.AspNetCore;
using System.Reflection;
>>>>>>> Stashed changes

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddUserSecrets<Program>();

builder.Services.AddControllers();
builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
    o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "������� ����� ��� �����������",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "Bearer"
    });
    o.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()

        }
    });
});

<<<<<<< Updated upstream
=======
builder.Services.AddValidatorsFromAssemblyContaining<Program>(); // ��� ������� ���������� ���������

// 2. ��������� �������������� ���������
builder.Services.AddFluentValidationAutoValidation(config =>
{
    config.DisableDataAnnotationsValidation = true; // ��������� ����������� ��������� DataAnnotations
});

// 3. ���������� ��������� (���� �����)
builder.Services.AddFluentValidationClientsideAdapters();

// ����������� ������������ MVC
builder.Services.AddControllers();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());

>>>>>>> Stashed changes
var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
