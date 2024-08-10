CREATE OR ALTER PROCEDURE DeleteTask_Surya
    @TaskId INT
AS
BEGIN
    UPDATE Tasks_Surya
    SET IsDeleted = 1
    WHERE TaskId = @TaskId
END