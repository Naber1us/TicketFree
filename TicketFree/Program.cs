using Microsoft.OpenApi.Models;
using TicketFree.Db;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddUserSecrets<Program>();

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
    o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Введите токен для авторизации",
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

builder.Services.AddValidatorsFromAssemblyContaining<Program>(); // Или укажите конкретный валидатор

// 2. Настройка автоматической валидации
builder.Services.AddFluentValidationAutoValidation(config =>
{
    config.DisableDataAnnotationsValidation = true; // Отключаем стандартную валидацию DataAnnotations
});

// 3. Клиентская валидация (если нужно)
builder.Services.AddFluentValidationClientsideAdapters();

// Стандартная конфигурация MVC
builder.Services.AddControllers();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());

var app = builder.Build();

app.UseExceptionHandler(exceptionHandlerApp =>
{
    exceptionHandlerApp.Run(async context =>
    {
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

        if (exception is ValidationException validationEx)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new
            {
                Status = StatusCodes.Status400BadRequest,
                Type = "validation_error",
                Title = "Ошибка валидации",
                Errors = validationEx.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => new {
                            Message = e.ErrorMessage,
                            Code = e.ErrorCode,
                            Severity = e.Severity.ToString()
                        }).ToArray()
                    )
            });
        }
        else
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(new
            {
                Status = StatusCodes.Status500InternalServerError,
                Type = "internal_error",
                Title = "Внутренняя ошибка сервера",
                Detail = exception?.Message
            });
        }
    });
});

app.Use(async (context, next) =>
{
    try
    {
        await next(context);
    }
    catch (Exception ex)
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await context.Response.WriteAsJsonAsync(new
        {
            Status = context.Response.StatusCode,
            Type = "internal_error",
            Title = "Internal Server Error",
            Detail = ex.Message
        });
    }
});





if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
