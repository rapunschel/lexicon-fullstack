// See https://aka.ms/new-console-template for more information
using exercise1.Controllers;
using exercise1.Models;
using exercise1.Storage;
using exercise1.Views;



IStorage storage = await EmployeeStorage.InitStorage();
EmployeeRegister register = new(storage);
ConsoleApp app = new(register);

await app.run();