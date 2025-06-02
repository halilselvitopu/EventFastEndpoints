using EventFastEndpoints.Context;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using FastEndpoints.Swagger;
using Mapster;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// FastEndpoints + Swagger
builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument();

// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddAuthorization();     
builder.Services.AddAuthentication();
builder.Services.AddMapster();


var app = builder.Build();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerGen();
}

app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();

app.UseFastEndpoints(); 

app.Run();
