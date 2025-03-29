CREATE PROCEDURE [dbo].[DeleteTodo]
@Id			INT				= NULL,
@AssignTo	INT				= NULL
AS
BEGIN
	DELETE Todos
	WHERE
		Id = @Id
		AND AssignTo = @AssignTo
END