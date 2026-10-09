# Exercise 1.

## Storage:

- EmployeeRegister as a controller. Takes in a Storage object. AddEmployee/updateEmployee/ removeEmployee, delegate to Storage. In the future could do more than just being meaningless delegator.

- Abstract IStorage interface with update/add/remove/fetch methods.
- EmployeeStorage implements IStorage.

This approach allows to swap storage implementation without having to change the rest, ie if we want to add an actual database.

## Data

Employee class with ID, name, salary

## UI

- ConsolePrint | takes in a EmployeeRegister
