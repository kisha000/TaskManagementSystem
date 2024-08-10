CREATE OR ALTER PROCEDURE CheckEmailExists_Surya
    @Email NVARCHAR(100),
    @Exists BIT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM Employees_Surya WHERE Email = @Email)
    BEGIN
        SET @Exists = 1; 
    END
    ELSE
    BEGIN
        SET @Exists = 0; 
    END
END
