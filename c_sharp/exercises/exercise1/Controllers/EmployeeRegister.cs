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

    public async Task<bool> UpdateAsync(Employee employee)
    {
        return await _storage.UpdateEmployeeAsync(employee);
    }

    public async Task<bool> AddAsync(Employee employee)
    {
        return await _storage.AddEmployeeAsync(employee);
    }

    public async Task<bool> RemoveAsync(string id)
    {
        return await _storage.RemoveEmployeeAsync(id);
    }

    public async Task<Dictionary<string, Employee>> FetchEmployeesAsync()
    {
        return await _storage.FetchEmployeesAsync();
    }

}
