CREATE OR ALTER PROCEDURE UpdateProject_Surya
    @ProjectId INT,
    @ProjectName NVARCHAR(100),
    @Description NVARCHAR(MAX),
    @StartDate DATE,
    @EndDate DATE,
    @ClientId INT,
    @Status NVARCHAR(50)
AS
BEGIN
    UPDATE Projects_Surya
    SET ProjectName = @ProjectName,
        Description = @Description,
        StartDate = @StartDate,
        EndDate = @EndDate,
        ClientId = @ClientId,
        Status = @Status,
        ModifiedDate =GETDATE()
    WHERE ProjectId = @ProjectId
END