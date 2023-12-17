CREATE PROCEDURE [dbo].[CompleteTodo]
@Id			INT				= NULL,
@AssignTo	INT				= NULL
AS
BEGIN
	UPDATE Todos
	SET
		IsComplete = 1
	WHERE
		Id = @Id
		AND AssignTo = @AssignTo
END
