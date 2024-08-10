CREATE OR ALTER PROCEDURE UpdateClient_Surya
    @ClientId INT,
    @ClientName NVARCHAR(100),
    @ContactPerson NVARCHAR(100),
    @Email NVARCHAR(100),
    @Phone NVARCHAR(20),
    @Address NVARCHAR(255),
    @ModifiedBy NVARCHAR(100),
    @ModifiedDate DATETIME
AS
BEGIN
    UPDATE Clients_Surya
    SET ClientName = @ClientName,
        ContactPerson = @ContactPerson,
        Email = @Email,
        Phone = @Phone,
        Address = @Address
    WHERE ClientId = @ClientId
END