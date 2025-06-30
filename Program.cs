// See https://aka.ms/new-console-template for more information
using EntityframeWorkCore.Data;
using EntityframeWorkCore.Models;


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
    
}