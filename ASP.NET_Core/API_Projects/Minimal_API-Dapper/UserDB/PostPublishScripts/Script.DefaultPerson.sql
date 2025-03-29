/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
			   SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

IF NOT EXISTS (SELECT [PersonId] FROM Person P WHERE P.[FirstName] = 'Koushik' AND P.[LastName] = 'Saha')
BEGIN
	INSERT INTO Person (
		[FirstName], [LastName]
	) VALUES
		('Koushik', 'Saha'),
		('Sourav', 'Das'),
		('Rakesh', 'Das'),
		('Anirban', 'Panja')
END