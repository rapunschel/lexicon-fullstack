using System;
using exercise1.Controllers;
using exercise1.Models;
namespace exercise1.Views;


enum Command
{
    Add,
    Update,
    Show,
    Delete,
    Quit
}
public class ConsoleApp(EmployeeRegister register)
{
    private EmployeeRegister _register = register;


    private string commandMsg = "'A' to add |'S' to show table | 'U' to update | 'D' to delete | 'Q' to exit";
    public async Task run()
    {

        Command? cmd = null;
        do
        {

            cmd = GetValidCmd();
            switch (cmd)
            {
                case Command.Add:
                    await AddCmd();
                    break;
                case Command.Delete:
                    await RemoveCmdAsync();
                    break;
                case Command.Update:
                    System.Console.WriteLine("Not implemented yet. Enter anything to continue");

                    break;
                case Command.Show:
                    PrintTable(await _register.FetchEmployeesAsync());
                    break;
                case Command.Quit:
                    return;

            }
            System.Console.Write("...");
            Console.ReadKey();
            System.Console.WriteLine();

        } while (cmd != Command.Quit);

    }

    private void PrintTable(Employee[] employees)
    {
        System.Console.WriteLine();

        const int id_row = -5;
        const int name_col = -15;
        const int salary_col = -15;

        System.Console.WriteLine($"{"ID",id_row} {"Name",name_col} {"Salary",salary_col}");
        foreach (Employee employee in employees)
        {

            System.Console.WriteLine($"{employee.ID,id_row} {employee.Name,name_col} {employee.Salary,salary_col}");
        }

        System.Console.WriteLine();
    }

    private async Task AddCmd()
    {
        System.Console.WriteLine();
        System.Console.WriteLine("What's your employee's name?");

        string name = GetValidStringInput();
        System.Console.WriteLine("What's your employee's salary?");
        int salary = GetValidIntInput();
        bool result = await _register.AddAsync(name, salary);
        if (!result) System.Console.WriteLine("Failed to add employee");
        else System.Console.WriteLine("Employee added");
    }

    private async Task RemoveCmdAsync()
    {
        System.Console.WriteLine("Enter ID to remove: ");
        string input = GetValidStringInput();
        bool result = await _register.RemoveAsync(input);

        if (result) System.Console.WriteLine($"employee with ID: {input} has been removed");
        else System.Console.WriteLine("No employee with such ID");



    }


    private int GetValidIntInput()
    {
        while (true)
        {

            string? input = Console.ReadLine();

            if (int.TryParse(input, out int value))
                return value;

            Console.WriteLine("Invalid input. Enter a whole number.");
        }
    }

    private string GetValidStringInput()
    {

        while (true)
        {
            string? input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
                return input;
            else System.Console.WriteLine("Invalid input");
        }

    }


    private Command GetValidCmd()
    {
        System.Console.WriteLine();

        System.Console.WriteLine(commandMsg);

        while (true)
        {
            string? input = Console.ReadLine()?.Trim().ToUpper();

            if (input == "A")
                return Command.Add;

            if (input == "U")
                return Command.Update;

            if (input == "D")
                return Command.Delete;

            if (input == "Q")
                return Command.Quit;
            if (input == "S")
                return Command.Show;

            Console.WriteLine($"Invalid command. {commandMsg}");
        }
    }

}
