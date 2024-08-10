CREATE OR ALTER PROCEDURE GetRoleById_Surya
    @RoleId INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM UserRoles_Surya WHERE RoleId = @RoleId AND IsDeleted = 0;
END
