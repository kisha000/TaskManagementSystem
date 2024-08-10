CREATE OR ALTER PROCEDURE InsertEmployee_Surya
    @Name NVARCHAR(50),
    @Address NVARCHAR(200),
    @Email NVARCHAR(100),
    @PhoneNumber NVARCHAR(20),
    @Password NVARCHAR(100),
    @RoleId INT
AS
BEGIN
    INSERT INTO Employees_Surya (EmployeeName, [Address], Email, PhoneNumber, [Password], RoleId)
    VALUES (@Name, @Address, @Email, @PhoneNumber, @Password, @RoleId)
END
