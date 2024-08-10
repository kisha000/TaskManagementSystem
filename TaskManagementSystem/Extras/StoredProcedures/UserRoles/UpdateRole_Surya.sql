CREATE OR ALTER PROCEDURE UpdateRole_Surya
    @RoleId INT,
    @RoleName NVARCHAR(50),
    @Description NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE UserRoles_Surya
    SET RoleName = @RoleName,
        Description = @Description
    WHERE RoleId = @RoleId;
END
