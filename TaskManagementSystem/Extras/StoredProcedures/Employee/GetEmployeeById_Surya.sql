CREATE OR ALTER PROCEDURE GetAllEmployeeById_Surya
@EmployeeId INT
AS
BEGIN
    SELECT E.EmployeeId, E.EmployeeName, E.[Address], E.Email, E.PhoneNumber, E.[Password],
           E.RoleId, E.ProjectId,
           CASE WHEN R.RoleName IS NULL THEN 'None' ELSE R.RoleName END AS RoleName,
           CASE WHEN P.ProjectName IS NULL THEN 'None' ELSE P.ProjectName END AS ProjectName
    FROM Employees_Surya AS E
    LEFT JOIN UserRoles_Surya AS R ON E.RoleId = R.RoleId
    LEFT JOIN Projects_Surya AS P ON E.ProjectId = P.ProjectId
    WHERE E.EmployeeId=@EmployeeId AND E.IsDeleted = 0
END
