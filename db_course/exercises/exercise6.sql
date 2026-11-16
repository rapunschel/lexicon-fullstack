CREATE PROCEDURE getCustomersByFirstName
    @FirstName VARCHAR(40) = 'Mark'
AS
    SELECT FirstName, LastName, City
    FROM Customer
    WHERE FirstName = @FirstName;

EXEC getCustomersByFirstName @FirstName = 'Astrid';
GO

CREATE PROCEDURE getCustomersLastNameStartsWithP
AS
    SELECT FirstName, LastName, City
    FROM Customer
    WHERE LastName LIKE 'P%';


EXEC getCustomersLastNameStartsWithP;