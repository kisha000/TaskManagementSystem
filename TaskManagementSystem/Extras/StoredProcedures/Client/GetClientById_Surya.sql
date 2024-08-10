CREATE OR ALTER PROCEDURE GetClientById_Surya
    @ClientId INT
AS
BEGIN
    SELECT * FROM Clients_Surya WHERE ClientId = @ClientId AND IsDeleted = 0
END