CREATE PROCEDURE [dbo].[GetCategories]
@CategoryUId	uniqueidentifier = NULL
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT [CategoryUId], [CategoryIId], [Name], [CreateDate], [UpdateDate], [DeleteDate]
	FROM Categories
	WHERE
		[CategoryUId] = ISNULL(@CategoryUId, [CategoryUId])
END