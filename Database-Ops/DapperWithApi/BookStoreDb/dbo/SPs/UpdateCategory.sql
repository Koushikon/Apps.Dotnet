CREATE PROCEDURE [dbo].[UpdateCategory]
@CategoryUId	uniqueidentifier	= NULL,
@Name			varchar(255)		= NULL,
@UpdateDate		datetime			= NULL
AS
BEGIN
	
	UPDATE Categories
	SET
		[Name] = @Name,
		[UpdateDate] = @UpdateDate
	WHERE
		[CategoryUId] = @CategoryUId
END