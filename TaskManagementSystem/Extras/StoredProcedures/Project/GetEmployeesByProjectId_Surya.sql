CREATE OR ALTER PROCEDURE GetEmployeesByProjectId_Surya
    @ProjectId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT E.*
    FROM Employees_Surya E
    INNER JOIN ProjectEmployees_Surya PE ON E.EmployeeId = PE.EmployeeId
    WHERE PE.ProjectId = @ProjectId;
END
