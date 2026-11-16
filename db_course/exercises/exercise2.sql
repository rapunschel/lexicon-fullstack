CREATE DATABASE School;
USE School;

CREATE TABLE Students (
    SID INT NOT NULL PRIMARY KEY,
    FName VARCHAR(20) NOT NULL,
    LName VARCHAR(20) NOT NULL,
    Comment TEXT NULL
);

INSERT INTO Students (SID, FName, LName, Comment) VALUES
(1, 'Sven', 'Svenson', 'Gillar matematik'),
(2, 'Erik', 'Erikson', NULL),
(3, 'SarNilsa', 'Nilsson', 'Ny student');
