CREATE PROCEDURE [dbo].[ManageTodo]
@Id			INT = NULL,
@AssignTo	INT = NULL,
@IsComplete	BIT	= NULL
AS
BEGIN
	SELECT Id, Task, AssignTo, IsComplete
	FROM Todos
	WHERE
		Id = CASE WHEN @Id IS NULL THEN Id ELSE @Id END
		AND AssignTo = CASE WHEN @AssignTo IS NULL THEN AssignTo ELSE @AssignTo END
		AND IsComplete = CASE WHEN @IsComplete IS NULL THEN IsComplete ELSE @IsComplete END
END
