using exercise1.Models;
using exercise1.Storage;

namespace exercise1.Controllers;

public class EmployeeRegister
{

    private IStorage _storage;

    public EmployeeRegister(IStorage storage)
    {
        _storage = storage;
    }

    public async Task<bool> UpdateAsync(string id, string name)
    {
        return await _storage.UpdateEmployeeAsync(id, name);
    }

    public async Task<bool> AddAsync(string name, int salary)
    {
        if (salary < 0) return false;
        return await _storage.AddEmployeeAsync(new Employee(name, salary));
    }

    public async Task<bool> RemoveAsync(string id)
    {
        return await _storage.RemoveEmployeeAsync(id);
    }

    public async Task<Employee[]> FetchEmployeesAsync()
    {
        Dictionary<string, Employee> map = await _storage.FetchEmployeesAsync();
        return [.. map.Values];
    }

}
