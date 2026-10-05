using System;
using exercise1.Models;

namespace exercise1.Storage;

public interface IStorage
{
    public Task<bool> UpdateEmployeeAsync(Employee employee);
    public Task<bool> RemoveEmployeeAsync(string id);

    public Task<bool> AddEmployeeAsync(Employee employee);

    public Task<Dictionary<string, Employee>> FetchEmployeesAsync();
}
