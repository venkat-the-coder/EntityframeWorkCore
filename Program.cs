﻿// See https://aka.ms/new-console-template for more information
using EntityframeWorkCore.Data;
using EntityframeWorkCore.Models;
using Microsoft.EntityFrameworkCore;


using (var dbContext = new DbContextClass())
{
    var manager = new Manager
    {
        Name = "pari",
    };

    dbContext.Managers.Add(manager);


    var employee = new Employee
    {
        Name = "kumar",
        Manager = manager
    };

    dbContext.Employees.Add(employee);

    dbContext.SaveChanges();

    var responses = dbContext.Employees.ToList();

    foreach (var employ in responses)
    {
        Console.WriteLine(employee.Name);
    }

    var response2 = dbContext.Employees.SingleOrDefault(c => c.EmployeeId == 4);
    Console.WriteLine(employee.Name);



    //Eager Loading

    var employyeInfo = dbContext.Employees.Include(c => c.Manager).ToList();

    foreach (var emp in employyeInfo)
    {
        Console.WriteLine($"Employye Name : {emp.Name}\n ManagerName:{emp.Manager.Name ?? "-"}");
    }


    //Explict Loading for employee side 

    var emp2 = dbContext.Employees.First();

    dbContext.Entry(emp2)
        .Reference(e => e.Manager).Load();

    if (emp2 != null)
    {
        // Explicitly load Manager navigation property
        dbContext.Entry(emp2)
            .Reference(e => e.Manager)
            .Load();

        Console.WriteLine(emp2.Manager?.Name);
    }


    //explict loading for manager side
    var manager2 = dbContext.Managers.First();

    if (manager2 != null)
    {
        dbContext.Entry(manager2).Collection(e => e.Employees).Load();

        int index = 0;

        Console.WriteLine($"Below are the employee list of : {manager2.Name}");

        foreach (var emp in manager2.Employees)
        {
            Console.WriteLine($"Index: {index}, EmployeeId: {emp.EmployeeId}, Name: {emp.Name}");
            index++;
        }
    }


    //lazy laoding concept not implementation

    //setup
    //services.AddDbContext<YourContext>(options =>
    //options.UseLazyLoadingProxies().UseSqlServer("YourConnStr"));

    //var employee = dbContext.Employees.First();
    //var managerName = employee.Manager.Name;



    var employees = dbContext.Employees
        .AsNoTracking()
        .Include(c => c.Manager)
        .Where(e => e.Name.Contains("M"))
        .ToList();

    foreach (var emp in employees)
    {
        Console.WriteLine($"{emp.Name}: {emp.Manager.Name}");
    }


}



// new version of c# using pattern
await using var _dbContext = new DbContextClass();
await using var transaction = await _dbContext.Database.BeginTransactionAsync();

if (_dbContext != null)
{
    var employee = await _dbContext.Employees.FirstOrDefaultAsync(employee => employee.Name == "kumar");


    if (employee != null)
    {
        employee.Name = "valli";
        await _dbContext.SaveChangesAsync();
    }

    var changedEmployee = await _dbContext.Employees.FirstOrDefaultAsync(employee => employee.Name == "valli");
    if (changedEmployee != null)
        Console.WriteLine($"Employee found and the name is : {changedEmployee.Name}");

    try
    {

        var employeedsset = _dbContext.Employees.Single(x => x.Name == "valli");
        employeedsset.Name = "valli new";

        await _dbContext.SaveChangesAsync();

        await transaction.CommitAsync();

    }
    catch
    {
        await transaction.RollbackAsync();
    }
}


//transacitional ef core





