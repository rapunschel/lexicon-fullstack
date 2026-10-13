using System;
using System.Security.Cryptography;

namespace EncapsulationLab;

public class Person
{

    private string firstNameError = "First name cannot contain fewer than 3 symbols!";
    private string lastNameError = "Last name cannot contain fewer than 3 symbols!";
    private string ageError = "Age cannot be zero or a negative integer!";
    private string salaryError = "Salary cannot be less than 460 dollar!";



    public Person(string firstName, string lastName, int age, decimal salary)
    {
        FirstName = firstName;
        LastName = lastName;
        Age = age;
        Salary = salary;
    }
    public string FirstName
    {
        get;
        private set
        {
            field = value.Length >= 3 ? value : throw new ArgumentException(firstNameError);
        }
    }


    public string LastName
    {
        get;
        private set
        {
            field = value.Length >= 3 ? value : throw new ArgumentException(lastNameError);
        }
    }


    public int Age
    {
        get;
        private set
        {
            field = value > 0 ? value : throw new ArgumentException(ageError);
        }
    }

    public decimal Salary
    {
        get;
        private set
        {

            field = value >= 460 ? value : throw new ArgumentException(salaryError);
        }
    }



    public void IncreaseSalary(decimal percentage)
    {
        decimal bonus = Salary * percentage / 100;
        Salary += Age > 30 ? bonus : bonus / 2;
    }

    public override string ToString()
    {
        return $"{FirstName} {LastName} receives {Salary} dollars.";
    }

}
