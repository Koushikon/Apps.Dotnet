CREATE PROCEDURE [dbo].[UpdateTodo]
@Id			INT				= NULL,
@Task		NVARCHAR(50)	= NULL,
@AssignTo	INT				= NULL
AS
BEGIN
	UPDATE Todos
	SET
		Task = @Task
	WHERE
		Id = @Id
		AND AssignTo = @AssignTo
END