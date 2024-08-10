CREATE OR ALTER PROCEDURE InsertProject_Surya
    @ProjectName NVARCHAR(100),
    @Description NVARCHAR(MAX),
    @StartDate DATE,
    @EndDate DATE,
    @ClientId INT,
    @Status NVARCHAR(50),
    @PriorityLevel NVARCHAR(50),
    @CreatedDate DATETIME,
    @IsDeleted BIT,
    @EmployeeIds NVARCHAR(MAX) 
AS
BEGIN
    DECLARE @ProjectId INT;

    INSERT INTO Projects_Surya (ProjectName, Description, StartDate, EndDate, ClientId, Status, PriorityLevel, CreatedDate, ModifiedDate, IsDeleted)
    VALUES (@ProjectName, @Description, @StartDate, @EndDate, @ClientId, @Status, @PriorityLevel, @CreatedDate, NULL, @IsDeleted);

    SET @ProjectId = SCOPE_IDENTITY();

    DECLARE @EmployeeIdTable TABLE (EmployeeId INT);
    INSERT INTO @EmployeeIdTable (EmployeeId)
    SELECT value FROM STRING_SPLIT(@EmployeeIds, ',');

    -- Assign employees to the project
    INSERT INTO ProjectEmployees_Surya (ProjectId, EmployeeId)
    SELECT @ProjectId, EmployeeId FROM @EmployeeIdTable;
END
