namespace exercise1.Models;

public class Employee(string name, int salary)
{
    private static int _nextId = 0;
    private readonly string _ID = $"{_nextId++}";
    private string _name = name;
    private int _salary = salary;

    public string ID
    {
        get { return _ID; }
    }

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public int Salary
    {
        get { return _salary; }
        set { _salary = value; }
    }



}
