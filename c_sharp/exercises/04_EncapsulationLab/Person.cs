using System;

namespace EncapsulationLab;

public class Person(string firstName, string lastName, int age)
{
    public string FirstName { get; set; } = firstName;
    public string LastName { get; set; } = lastName;
    public int Age { get; set; } = age;




    public override string ToString()
    {
        return $"{FirstName} {LastName} is {Age} years old.";
    }

}
