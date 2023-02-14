CREATE PROCEDURE [dbo].[AddPerson]
@PersonId		INT             = NULL,
@FirstName		NVARCHAR(50)	= NULL,
@LastName		NVARCHAR(50)	= NULL,
@StreetAddress	NVARCHAR(150)	= NULL,
@Ciy			NVARCHAR(50)	= NULL,
@State			NVARCHAR(50)	= NULL,
@ZipCode		NVARCHAR(50)	= NULL
AS
BEGIN
	
	INSERT INTO Person (
		[FirstName], [LastName]
	) VALUES (
		@FirstName, @LastName
	)

	SET @PersonId = @@IDENTITY

	INSERT INTO Address (
		[PersonId], [StreetAddress], [Ciy], [State], [ZipCode]
	) VALUES (
		@PersonId, @StreetAddress, @Ciy, @State, @ZipCode
	)
END