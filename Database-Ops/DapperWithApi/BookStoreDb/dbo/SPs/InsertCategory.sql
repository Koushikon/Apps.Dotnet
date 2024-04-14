CREATE PROCEDURE [dbo].[InsertCategory]
@CategoryUId	uniqueidentifier	= NULL,
@Name			varchar(255)		= NULL,
@CreateDate		datetime			= NULL
AS
BEGIN

	INSERT INTO Categories (
		[CategoryUId], [Name], [CreateDate]
	) VALUES (
		@CategoryUId, @Name, @CreateDate
	)

END
