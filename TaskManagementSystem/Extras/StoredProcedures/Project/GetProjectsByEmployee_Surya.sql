CREATE OR ALTER PROCEDURE GetProjectsByEmployee_Surya
    @EmployeeId INT
AS
BEGIN
    SELECT P.*, C.ClientName 
    FROM Projects_Surya P
    INNER JOIN Clients_Surya C ON P.ClientId = C.ClientId
    INNER JOIN ProjectEmployees_Surya PE ON P.ProjectId = PE.ProjectId
    WHERE P.IsDeleted = 0 AND PE.EmployeeId = @EmployeeId
END
