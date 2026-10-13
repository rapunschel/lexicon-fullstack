using System;
using System.Collections.Generic;
using System.Linq;
using EncapsulationLab;




Exercise_salary_increase();


void Exercise_name_and_age()
{
    var lines = 5;
    var persons = new List<Person>();
    for (int i = 0; i < lines; i++)
    {
        var cmdArgs = Console.ReadLine().Split();
        var person = new Person(cmdArgs[0], cmdArgs[1], int.Parse(cmdArgs[2]));
        persons.Add(person);
    }
    persons.OrderBy(p => p.FirstName)
        .ThenBy(p => p.Age)
        .ToList()
        .ForEach(p => Console.WriteLine(p.ToString()));
}


void Exercise_salary_increase()
{
    System.Console.WriteLine("How many lines to read?");
    var lines = int.Parse(Console.ReadLine());
    var persons = new List<Person>();
    for (int i = 0; i < lines; i++)
    {
        var cmdArgs = Console.ReadLine().Split();
        var person = new Person(cmdArgs[0], cmdArgs[1], int.Parse(cmdArgs[2]), decimal.Parse(cmdArgs[3]
        ));
        persons.Add(person);
    }
    System.Console.WriteLine("Give a bonus: ");
    var bonus = decimal.Parse(Console.ReadLine());
    persons.ForEach(p => p.IncreaseSalary(bonus));
    persons.ForEach(p => Console.WriteLine(p.ToString()));
}
