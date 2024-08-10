CREATE OR ALTER PROCEDURE DeleteEmployee_Surya
    @EmployeeId INT
AS
BEGIN
    UPDATE Employees_Surya
    SET IsDeleted = 1
    WHERE EmployeeId = @EmployeeId
END