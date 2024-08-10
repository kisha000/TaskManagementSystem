CREATE OR ALTER PROCEDURE UpdateEmployeePassword_Surya
    @Email NVARCHAR(100),
    @NewPassword NVARCHAR(100)
AS
BEGIN
    UPDATE Employees_Surya
    SET Password = @NewPassword
    WHERE Email = @Email;
END
