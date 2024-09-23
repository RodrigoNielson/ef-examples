using Ef.Examples.Infrastructure;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
   .AddFastEndpoints()
   .SwaggerDocument();

builder.Services.AddDbContext<SalesContext>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseFastEndpoints()
   .UseSwaggerGen();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
