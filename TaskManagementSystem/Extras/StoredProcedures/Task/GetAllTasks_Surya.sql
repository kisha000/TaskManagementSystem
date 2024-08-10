CREATE OR ALTER PROCEDURE GetAllTasks_Surya
AS
BEGIN
   SELECT T.*, ISNULL(E.EmployeeName, 'No Employee') AS EmployeeName, P.ProjectName
    FROM Tasks_Surya T
    INNER JOIN Projects_Surya P ON T.ProjectId = P.ProjectId
    LEFT JOIN Employees_Surya E ON T.EmployeeId = E.EmployeeId
    WHERE T.IsDeleted = 0
END
