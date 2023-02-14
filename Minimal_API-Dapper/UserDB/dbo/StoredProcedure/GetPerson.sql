CREATE PROCEDURE [dbo].[GetPerson]
@Id				INT             = NULL
AS
BEGIN

	SELECT [P].[PersonId], [P].[FirstName], [P].[LastName], [AD].[StreetAddress], [AD].[Ciy], [AD].[State], [AD].[ZipCode]
	
	FROM Person P INNER JOIN Address AD ON P.[PersonId] = AD.[PersonId]

	WHERE P.[PersonId] = @Id
END