CREATE PROCEDURE UpdateResetLinkTimestamp_Surya
    @Email NVARCHAR(100)
AS
BEGIN
    UPDATE Employees_Surya
    SET ResetLinkGeneratedAt = GETDATE() 
    WHERE Email = @Email
END
