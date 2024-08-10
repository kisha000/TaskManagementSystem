CREATE OR ALTER PROCEDURE GetTaskById_Surya
@TaskId INT
AS
BEGIN
  SELECT T.*, ISNULL(E.EmployeeName, 'No Employee') AS EmployeeName, P.ProjectName
    FROM Tasks_Surya T
    INNER JOIN Projects_Surya P ON T.ProjectId = P.ProjectId
    LEFT JOIN Employees_Surya E ON T.EmployeeId = E.EmployeeId
    WHERE T.TaskId=@TaskId AND T.IsDeleted = 0
END