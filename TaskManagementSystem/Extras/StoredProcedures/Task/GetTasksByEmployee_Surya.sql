CREATE OR ALTER PROCEDURE GetTasksByEmployee_Surya
	@EmployeeId INT
AS
BEGIN
SELECT T.*,P.ProjectName
    FROM Tasks_Surya T INNER JOIN
	Projects_Surya P ON T.ProjectId=P.ProjectId
    WHERE T.IsDeleted = 0
        AND EmployeeId = @EmployeeId
END;

