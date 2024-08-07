using System.Text.Json.Serialization;
using Application;
using Application.Abstractions.Infrastructure;
using Asp.Versioning;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using Presentation.API.Endpoints;
using Presentation.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddPersistence(builder.Configuration);//builder.Configuration.GetConnectionString("DefaultConnection"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddExceptionHandler<BadHttpRequestExceptionHandler>();
builder.Services.AddExceptionHandler<NotFoundExceptionHandler>();
builder.Services.AddExceptionHandler<ValidationExceptionHandler>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1);
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'V";
    options.SubstituteApiVersionInUrl = true;
});

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

var apiVersionSet = app.NewApiVersionSet()
    .HasApiVersion(new ApiVersion(1))
    .ReportApiVersions()
    .Build();

app.MapGroup("/api")
    .RegisterEndpoints()
    .WithApiVersionSet(apiVersionSet);

/*app.MapPost("/api/uploads", async ([FromForm] IFormFile file, IFileStorageService fileStorageService, CancellationToken cancellationToken = default) =>
{
    if (file.Length == 0)
    {
        return Results.BadRequest("No file uploaded.");
    }
    
    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
    
    await fileStorageService.UploadFileAsync(file, fileName, cancellationToken);

    var fileUrl = await fileStorageService.GetFileUrlAsync(fileName);
    
    return Results.Ok(new { FileUrl = fileUrl });
}).DisableAntiforgery();*/

app.Run();