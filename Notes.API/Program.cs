using Notes.Application;
using Notes.Presentation;
using Notes.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(
    optionsBuilder =>
    {
        string connectionString = builder.Configuration.GetConnectionString("ConnectionString")!;
        optionsBuilder.UseSqlServer(connectionString);
    });

builder.Services
    .AddApplication()
    .AddInfrastructure()
    .AddPresentation();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
