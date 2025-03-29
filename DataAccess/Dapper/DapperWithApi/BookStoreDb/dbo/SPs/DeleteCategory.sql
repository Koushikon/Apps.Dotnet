CREATE PROCEDURE [dbo].[DeleteCategory]
@CategoryUId	uniqueidentifier	= NULL,
@DeleteDate		datetime			= NULL
AS
BEGIN
	
	UPDATE Categories
	SET
		[DeleteDate] = @DeleteDate
	WHERE
		[CategoryUId] = @CategoryUId
END