CREATE PROCEDURE GetResetLinkGeneratedAt
    @Email NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT ResetLinkGeneratedAt
    FROM Employees_Surya
    WHERE Email = @Email;
END
