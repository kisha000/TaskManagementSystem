CREATE OR ALTER PROCEDURE InsertTask_Surya
    @TaskName NVARCHAR(100),
    @ProjectId INT,
    @TaskDescription NVARCHAR(MAX),
    @StartDate DATETIME,
    @EstimateDate DATETIME,
    @PriorityLevel NVARCHAR(50),
    @AttachmentFile NVARCHAR(255),
    @Status NVARCHAR(50),
    @ModifiedDate DATETIME,
    @EmployeeId INT,
    @IsDeleted BIT
AS
BEGIN
    INSERT INTO Tasks_Surya (TaskName, ProjectId, TaskDescription, StartDate, EstimateDate, PriorityLevel, AttachmentFile, Status, CreatedDate, ModifiedDate, EmployeeId)
    VALUES (@TaskName, @ProjectId, @TaskDescription, @StartDate, @EstimateDate, @PriorityLevel, @AttachmentFile, @Status, GETDATE(), @ModifiedDate, @EmployeeId)
END
