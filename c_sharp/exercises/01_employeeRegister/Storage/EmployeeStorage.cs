using exercise1.Models;

namespace exercise1.Storage;


public class EmployeeStorage : IStorage
{

    private readonly Dictionary<string, Employee> _map;
    private EmployeeStorage(Dictionary<string, Employee> map)
    {
        _map = map;
    }

    public async static Task<EmployeeStorage> InitStorage()
    {
        return new EmployeeStorage(new Dictionary<String, Employee>());
    }

    public async Task<bool> UpdateEmployeeAsync(string id, string name)
    {
        if (!_map.ContainsKey(id)) return false;

        Employee employee = _map[id];
        employee.Name = name;
        return true;
    }
    public async Task<bool> UpdateEmployeeAsync(string id, int salary)
    {
        if (!_map.ContainsKey(id)) return false;

        Employee employee = _map[id];
        employee.Salary = salary;
        return true;
    }

    public async Task<bool> RemoveEmployeeAsync(string id)

    {
        return _map.Remove(id);

    }
    public async Task<bool> AddEmployeeAsync(Employee employee)
    {
        if (_map.ContainsKey(employee.ID)) return false;

        _map.Add(employee.ID, employee);

        return true;
    }

    public async Task<Dictionary<string, Employee>> FetchEmployeesAsync()
    {

        return _map;
    }
}
