-- Create a new table with the identity column
CREATE TABLE Animal (
    IdAnimal INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(200),
    Description NVARCHAR(200),
    Category NVARCHAR(200),
    Area NVARCHAR(200)
);

-- Copy data from old table to new table
SET IDENTITY_INSERT Animal ON;
INSERT INTO Animal (IdAnimal, Name, Description, Category, Area)
SELECT IdAnimal, Name, Description, Category, Area
FROM Animal;

SET IDENTITY_INSERT Animal OFF;

