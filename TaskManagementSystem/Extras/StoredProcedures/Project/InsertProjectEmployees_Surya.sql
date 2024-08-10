CREATE OR ALTER PROCEDURE InsertProjectEmployees_Surya
	@ProjectId INT,
	@EmployeeIds VARCHAR(MAX)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @EmployeeId INT;
	DECLARE @EmployeeIdList TABLE (Id INT);

	INSERT INTO @EmployeeIdList (Id)
	SELECT value
	FROM STRING_SPLIT(@EmployeeIds, ',');

	INSERT INTO ProjectEmployees_Surya (ProjectId, EmployeeId)
	SELECT @ProjectId, Id
	FROM @EmployeeIdList AS e
	WHERE NOT EXISTS (
		SELECT 1
		FROM ProjectEmployees_Surya AS pe
		WHERE pe.ProjectId = @ProjectId
			AND pe.EmployeeId = e.Id
	);

END;
