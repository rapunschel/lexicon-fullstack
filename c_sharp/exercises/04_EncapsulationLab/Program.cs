using System;
using System.Collections.Generic;
using System.Text;
using EncapsulationLab;


Exercise_salary_increase();

void Exercise_salary_increase()
{
    try
    {
        System.Console.WriteLine("How many lines to read?");
        var lines = int.Parse(Console.ReadLine());
        var persons = new List<Person>();
        var sb = new StringBuilder();

        for (int i = 0; i < lines; i++)
        {
            try
            {
                var cmdArgs = Console.ReadLine().Split();
                var person = new Person(cmdArgs[0], cmdArgs[1], int.Parse(cmdArgs[2]), decimal.Parse(cmdArgs[3]
                ));
                persons.Add(person);
            }
            catch (Exception ex)
            {
                sb.Append($"{ex.Message}\n");

            }

        }
        System.Console.WriteLine("Give a bonus: ");
        var bonus = decimal.Parse(Console.ReadLine());
        persons.ForEach(p => p.IncreaseSalary(bonus));
        persons.ForEach(p => sb.Append(p.ToString()));
        System.Console.WriteLine(sb);
    }
    catch (ArgumentException ex)
    {
        System.Console.WriteLine(ex.Message);
    }
    catch (Exception ex)
    {
        System.Console.WriteLine($"Unknown error: {ex.Message}");
    }

}
