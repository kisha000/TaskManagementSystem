CREATE OR ALTER PROCEDURE GetEmployeeByEmail_Surya
    @Email NVARCHAR(100)
AS
BEGIN
    SELECT E.*, R.RoleName
    FROM Employees_Surya AS E
    INNER JOIN UserRoles_Surya AS R ON E.RoleId = R.RoleId
    WHERE E.Email = @Email
END
