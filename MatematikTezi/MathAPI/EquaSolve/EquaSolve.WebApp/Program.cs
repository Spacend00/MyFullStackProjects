using EquaSolve.Application.Features.Equatiions.Commands;
using EquaSolve.Application.Interfaces;
using EquaSolve.Application.Mappings;
using EquaSolve.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(opts =>
{
    opts.AddPolicy("AngularAppPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:4200", "http://localhost:4200/")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(SolveEquationCommand).Assembly));
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<MappingProfile>();
});
builder.Services.AddScoped<IMathSolverService, AngouriMathSolver>();
builder.Services.AddScoped<IGraphSolverService, GraphSolverService>();
var app = builder.Build();

app.UseCors("AngularAppPolicy");

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(opt =>
    {
        opt.WithTitle("EquaSolve API")
           .WithTheme(ScalarTheme.DeepSpace)
           .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
    });
}

app.MapPost("/api/solve", async (IMediator mediator, [FromBody] SolveEquationCommand command) =>
{
    try
    {
        var result = await mediator.Send(command);

        // Eğer her şey yolundaysa sonucu dön
        return Results.Ok(result);
    }
    catch (Exception ex)
    {
        // 🔥 İşte burası sihirli nokta! Serileştirme veya mapping hatasını burada yakalayacağız.
        return Results.Problem(
            detail: ex.ToString(),
            title: "Serileştirme veya Çalışma Zamanı Hatası",
            statusCode: 500
        );
    }
})
.WithName("SolveEquation")
.WithOpenApi();

app.MapPost("/api/graph", async (IMediator mediator, [FromBody] GetGraphPointsCommand command) =>
{
    var result = await mediator.Send(command);
    return Results.Ok(result);
})
.WithName("GetGraphPoints")
.WithOpenApi();

app.Run();
