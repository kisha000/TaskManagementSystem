CREATE OR ALTER PROCEDURE InsertClient_Surya
    @ClientName NVARCHAR(100),
    @ContactPerson NVARCHAR(100),
    @Email NVARCHAR(100),
    @Phone NVARCHAR(20),
    @Address NVARCHAR(255)
AS
BEGIN
    INSERT INTO Clients_Surya (ClientName, ContactPerson, Email, Phone, Address)
    VALUES (@ClientName, @ContactPerson, @Email, @Phone, @Address)
END