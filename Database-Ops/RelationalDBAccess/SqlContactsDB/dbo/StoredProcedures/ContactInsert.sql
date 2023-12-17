CREATE PROCEDURE [dbo].[ContactInsert]
@FirstName  NVARCHAR(50) = NULL,
@LastName   NVARCHAR(50) = NULL,
@ContactId  INT             OUTPUT,
@ContactMsg NVARCHAR(50)    OUTPUT
AS
BEGIN 

IF EXISTS(SELECT Id FROM [Contact] WHERE [FirstName] = @FirstName AND [LastName] = @LastName)
BEGIN
	SET @ContactMsg = 'There is already a contact with the same name'
    SET @ContactId = (SELECT Id FROM [Contact] WHERE [FirstName] = @FirstName AND [LastName] = @LastName)
END
ELSE
BEGIN
	INSERT INTO [dbo].[Contact]
        ([FirstName], [LastName])
    VALUES
        (@FirstName, @LastName)

    SET @ContactId = @@IDENTITY
END

END