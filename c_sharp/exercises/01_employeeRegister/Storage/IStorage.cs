using System;
using System.Data.Common;
using exercise1.Models;

namespace exercise1.Storage;

public interface IStorage
{
    Task<bool> UpdateEmployeeAsync(string id, string name);
    Task<bool> UpdateEmployeeAsync(string id, int salary);
    public Task<bool> RemoveEmployeeAsync(string id);

    public Task<bool> AddEmployeeAsync(Employee employee);

    public Task<Dictionary<string, Employee>> FetchEmployeesAsync();
}
