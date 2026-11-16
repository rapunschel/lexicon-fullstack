CREATE PROCEDURE getCustomer
AS SELECT Firstname, LastName,City 
FROM Customer 
WHERE FirstName IN ('Mark', 'Astrid')
ORDER BY FirstName DESC;

EXEC getCustomer
