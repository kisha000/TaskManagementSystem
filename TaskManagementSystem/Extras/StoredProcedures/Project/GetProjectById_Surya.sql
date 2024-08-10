CREATE OR ALTER PROCEDURE GetProjectById_Surya
    @ProjectId INT
AS
BEGIN
    SELECT * FROM Projects_Surya WHERE ProjectId = @ProjectId AND IsDeleted = 0
END