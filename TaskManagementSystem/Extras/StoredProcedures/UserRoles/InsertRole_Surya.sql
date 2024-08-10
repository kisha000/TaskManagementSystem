CREATE OR ALTER PROCEDURE InsertRole_Surya
    @RoleName NVARCHAR(50),
    @Description NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO UserRoles_Surya (RoleName, Description)
    VALUES (@RoleName, @Description);
END
