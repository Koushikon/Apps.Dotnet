CREATE PROCEDURE [dbo].[AddTodo]
@Task		NVARCHAR(50)	= NULL,
@AssignTo	INT				= NULL
AS
BEGIN
	INSERT INTO Todos
		(Task, AssignTo)
	VALUES
		(@Task, @AssignTo)
	
	-- Getting the newly created data
	SELECT Id, Task, AssignTo, IsComplete
	FROM Todos
	WHERE
		Id = SCOPE_IDENTITY()
END