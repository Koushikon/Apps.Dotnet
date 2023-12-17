CREATE TABLE [dbo].[Todos]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Task] NVARCHAR(50) NOT NULL, 
    [AssignTo] INT NOT NULL, 
    [IsComplete] BIT NOT NULL DEFAULT 0
)
