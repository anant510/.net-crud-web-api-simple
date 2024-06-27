using firstPractice.Models;
using Microsoft.EntityFrameworkCore;
using firstPractice.Controllers;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<FirstiicContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("firstiic")));


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI(
   options =>
   {
       options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
       options.RoutePrefix = string.Empty;
       options.DocumentTitle = "My Swagger";

   }
);

app.MapEmployeeEndpoints();

app.Run();
