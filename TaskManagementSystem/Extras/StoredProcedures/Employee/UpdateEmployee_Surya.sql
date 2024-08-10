CREATE OR ALTER PROCEDURE UpdateEmployee_Surya
    @EmployeeId INT,
    @EmployeeName NVARCHAR(100),
    @Address NVARCHAR(200),
    @Email NVARCHAR(100),
    @PhoneNumber NVARCHAR(20),
    @RoleId INT,
    @ProjectId INT
AS
BEGIN
    UPDATE Employees_Surya
    SET EmployeeName=@EmployeeName,
        Address = @Address,
        Email = @Email,
        PhoneNumber = @PhoneNumber,
        RoleId = @RoleId,
        ProjectId=@ProjectId
    WHERE EmployeeId = @EmployeeId
END