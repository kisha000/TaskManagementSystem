CREATE OR ALTER PROCEDURE DeleteProject_Surya
    @ProjectId INT
AS
BEGIN
    UPDATE Projects_Surya
    SET IsDeleted = 1
    WHERE ProjectId = @ProjectId
END