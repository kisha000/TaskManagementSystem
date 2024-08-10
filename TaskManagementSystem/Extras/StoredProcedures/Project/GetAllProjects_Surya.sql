CREATE OR ALTER PROCEDURE GetAllProjects_Surya
AS
BEGIN
    SELECT P.*, C.ClientName 
    FROM Projects_Surya P
    INNER JOIN Clients_Surya C ON P.ClientId = C.ClientId
    WHERE P.IsDeleted = 0
END
