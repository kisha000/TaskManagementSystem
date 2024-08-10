CREATE OR ALTER PROCEDURE UpdateTask_Surya
	@TaskId INT,
	@TaskName NVARCHAR(100),
	@ProjectId INT,
	@TaskDescription NVARCHAR(MAX),
	@StartDate DATETIME,
	@EstimateDate DATETIME,
	@PriorityLevel NVARCHAR(50),
	@ModifiedDate DATETIME,
	@EmployeeId INT = NULL
AS
BEGIN
	UPDATE Tasks_Surya
	SET TaskName = @TaskName,
		ProjectId = @ProjectId,
		TaskDescription = @TaskDescription,
		StartDate = @StartDate,
		EstimateDate = @EstimateDate,
		PriorityLevel = @PriorityLevel,
		ModifiedDate = getdate(),
		EmployeeId =@EmployeeId
	WHERE TaskId = @TaskId
END
