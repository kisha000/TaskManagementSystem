CREATE OR ALTER PROCEDURE GetAllOpenedTasks_Surya
AS
BEGIN
    SELECT T.*, E.EmployeeName, P.ProjectName
    FROM Tasks_Surya T
    INNER JOIN Projects_Surya P ON T.ProjectId = P.ProjectId
    INNER JOIN Employees_Surya E ON T.EmployeeId = E.EmployeeId
    WHERE T.IsDeleted = 0 AND T.Status='Open'
END
