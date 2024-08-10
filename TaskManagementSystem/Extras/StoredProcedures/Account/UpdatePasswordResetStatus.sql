CREATE OR ALTER PROCEDURE UpdatePasswordResetStatus
    @Email NVARCHAR(100),
    @IsReset BIT
AS
BEGIN
    UPDATE Employees_Surya
    SET IsPasswordReset = @IsReset
    WHERE Email = @Email;
END
