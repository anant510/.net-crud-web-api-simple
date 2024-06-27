using System;
using System.Collections.Generic;
using firstPractice.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;

namespace firstPractice.Models;

public partial class Employee
{
    public int Id { get; set; }

    public string? Name { get; set; }
}


public static class EmployeeEndpoints
{
	public static void MapEmployeeEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Employee").WithTags(nameof(Employee));

        group.MapGet("/", async (FirstiicContext db) =>
        {
            return await db.Employees.ToListAsync();
        })
        .WithName("GetAllEmployees")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Employee>, NotFound>> (int id, FirstiicContext db) =>
        {
            return await db.Employees.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is Employee model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetEmployeeById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, Employee employee, FirstiicContext db) =>
        {
            var affected = await db.Employees
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.Id, employee.Id)
                  .SetProperty(m => m.Name, employee.Name)
                  );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateEmployee")
        .WithOpenApi();

        group.MapPost("/", async (Employee employee, FirstiicContext db) =>
        {
            db.Employees.Add(employee);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Employee/{employee.Id}",employee);
        })
        .WithName("CreateEmployee")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, FirstiicContext db) =>
        {
            var affected = await db.Employees
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteEmployee")
        .WithOpenApi();
    }
}