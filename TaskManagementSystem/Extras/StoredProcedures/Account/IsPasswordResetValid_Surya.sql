CREATE PROCEDURE IsPasswordResetValid_Surya
    @Email NVARCHAR(100)
AS
BEGIN
    DECLARE @IsPasswordResetValid BIT

    SELECT @IsPasswordResetValid = IsPasswordReset
    FROM Employees_Surya
    WHERE Email = @Email

    IF @IsPasswordResetValid = 1
        SELECT 1
    ELSE
        SELECT 0
END
