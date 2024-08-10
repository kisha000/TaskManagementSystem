CREATE OR ALTER PROCEDURE GetNextTaskId
AS
BEGIN
    DECLARE @NextId INT;

    SELECT @NextId = ISNULL(MAX(TaskId), 0) + 1
    FROM Tasks_Surya;

    SELECT @NextId AS NextTaskId;
END
