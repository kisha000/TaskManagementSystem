CREATE OR ALTER PROCEDURE DeleteClient_Surya
    @ClientId INT
AS
BEGIN
    UPDATE Clients_Surya
    SET IsDeleted = 1
    WHERE ClientId = @ClientId
END