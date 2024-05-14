using CRK.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

var connectionString =
    builder.Configuration.GetConnectionString("College")
    + builder.Configuration["CollegeDbPassword"];

builder.Services.AddDbContext<CollegeDbContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(options => options.WithOrigins("http://localhost:8080").AllowAnyHeader());
app.MapControllers();
app.Run();

public partial class Program { }
