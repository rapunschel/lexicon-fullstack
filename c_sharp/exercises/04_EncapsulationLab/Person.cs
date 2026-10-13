using System;

namespace EncapsulationLab;

public class Person(string firstName, string lastName, int age, decimal salary = 0)
{
    public string FirstName { get; set; } = firstName;
    public string LastName { get; set; } = lastName;
    public int Age { get; set; } = age;

    public decimal Salary { get; set; } = salary;



    public void IncreaseSalary(decimal percentage)
    {
        Salary += Salary * percentage / 100;
    }

    public override string ToString()
    {
        return $"{FirstName} {LastName} is {Age} years old{(Salary == 0 ? "." : $" receives {Salary}")}";
    }

}
