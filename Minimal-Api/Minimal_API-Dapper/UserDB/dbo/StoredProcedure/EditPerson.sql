CREATE PROCEDURE [dbo].[EditPerson]
@PersonId		INT				= NULL,
@FirstName		NVARCHAR(50)	= NULL,
@LastName		NVARCHAR(50)	= NULL,
@StreetAddress	NVARCHAR(150)	= NULL,
@Ciy			NVARCHAR(50)	= NULL,
@State			NVARCHAR(50)	= NULL,
@ZipCode		NVARCHAR(50)	= NULL
AS
BEGIN

	UPDATE Person
	SET
		[FirstName] = @FirstName,
		[LastName] = @LastName
	WHERE
		[PersonId] = @PersonId

	UPDATE Address
	SET
		[StreetAddress] = @StreetAddress,
		[Ciy] = @Ciy,
		[State] = @State,
		[ZipCode] = @ZipCode
	WHERE [PersonId] = @PersonId
END