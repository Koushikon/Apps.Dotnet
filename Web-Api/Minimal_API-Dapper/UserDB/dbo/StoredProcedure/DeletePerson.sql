CREATE PROCEDURE [dbo].[DeletePerson]
@Id				INT             = NULL
AS
	DELETE FROM Address
	WHERE [PersonId] = @Id

	DELETE FROM Person
	WHERE [PersonId] = @Id
RETURN 0
