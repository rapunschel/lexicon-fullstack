-- Create database

CREATE DATABASE Employee;
GO

USE Employee;
GO

-- Create Department table
CREATE TABLE Department
(
    DepartmentCode CHAR(1) NOT NULL,
    Name NVARCHAR(12),
    Manager INT NULL,
    CONSTRAINT PK_Department PRIMARY KEY (DepartmentCode)
);
GO

-- Create Employee table
CREATE TABLE EmployeeDetails
(
    EmployeeNumber INT NOT NULL,
    Name NVARCHAR(6),
    Phone VARCHAR(10),
    Salary INT,
    Manager INT NULL,
    DepartmentCode CHAR(1),
    CONSTRAINT PK_EmployeeDetails PRIMARY KEY (EmployeeNumber)
);
GO

-- Foreign keys
ALTER TABLE EmployeeDetails
ADD CONSTRAINT FK_EmployeeDetails_Manager
FOREIGN KEY (Manager)
REFERENCES EmployeeDetails (EmployeeNumber);
GO

ALTER TABLE EmployeeDetails
ADD CONSTRAINT FK_EmployeeDetails_Department
FOREIGN KEY (DepartmentCode)
REFERENCES Department (DepartmentCode);
GO

ALTER TABLE Department
ADD CONSTRAINT FK_Department_Manager
FOREIGN KEY (Manager)
REFERENCES EmployeeDetails (EmployeeNumber);
GO

-- Insert departments first
INSERT INTO Department (DepartmentCode, Name)
VALUES ('H', N'Headquarters');

INSERT INTO Department (DepartmentCode, Name)
VALUES ('S', N'Security');

INSERT INTO Department (DepartmentCode, Name)
VALUES ('C', N'Data');
GO

-- Insert employees
INSERT INTO EmployeeDetails
(EmployeeNumber, Name, Phone, Salary, DepartmentCode)
VALUES (2, N'Stina', '2677', 30000, 'H');

INSERT INTO EmployeeDetails
(EmployeeNumber, Name, Phone, Salary, Manager, DepartmentCode)
VALUES (3, N'Saddam', '1088', 22000, 2, 'S');

INSERT INTO EmployeeDetails
(EmployeeNumber, Name, Phone, Salary, Manager, DepartmentCode)
VALUES (4, N'Lotta', '4590', 28000, 2, 'H');

INSERT INTO EmployeeDetails
(EmployeeNumber, Name, Phone, Salary, Manager, DepartmentCode)
VALUES (1, N'Olle', '2688', 20000, 3, 'S');

INSERT INTO EmployeeDetails
(EmployeeNumber, Name, Phone, Salary, Manager, DepartmentCode)
VALUES (8, N'Maria', '2690', 25000, 4, 'C');

INSERT INTO EmployeeDetails
(EmployeeNumber, Name, Phone, Salary, Manager, DepartmentCode)
VALUES (9, N'Ulrik', '2698', 26000, 8, 'C');

INSERT INTO EmployeeDetails
(EmployeeNumber, Name, Phone, Salary, Manager, DepartmentCode)
VALUES (10, N'Petter', '2645', 22000, 8, 'C');
GO

-- Assign department managers
UPDATE Department
SET Manager = 2
WHERE DepartmentCode = 'H';

UPDATE Department
SET Manager = 3
WHERE DepartmentCode = 'S';

UPDATE Department
SET Manager = 8
WHERE DepartmentCode = 'C';
GO

-- Test
SELECT *
FROM EmployeeDetails;

SELECT *
FROM Department;
GO