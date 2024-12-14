using MediatR;
using Microsoft.EntityFrameworkCore;
using MyApplication.Application.Handlers;
using MyApplication.Application.Mapping;
using MyApplication.Application.Services;
using MyApplication.Infrastructure.Persistence;
using MyApplication.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(typeof(GetPersonsQueryHandler).Assembly);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register Database Context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperConfig));

// Register Repositories
builder.Services.AddScoped<IPersonRepository, PersonRepository>();

// Register MediatR for CQRS
builder.Services.AddMediatR(typeof(Program).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Use CORS
//app.UseCors("AllowReactApp");
app.UseCors(policy =>
    policy.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader());

// Enable serving static files
app.UseDefaultFiles(); // Serves default files like index.html
app.UseStaticFiles(); // Enables serving static files like JavaScript, CSS, images, etc.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Redirect any unmatched routes to the React app
app.MapFallbackToFile("index.html");

app.Run();
