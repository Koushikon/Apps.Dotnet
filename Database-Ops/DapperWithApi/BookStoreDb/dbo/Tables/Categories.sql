CREATE TABLE [dbo].[Categories]
(
	[CategoryUId] uniqueidentifier NOT NULL PRIMARY KEY,
	[CategoryIId] int NOT NULL IDENTITY(1,1),
	[Name] varchar(255) NOT NULL, 
    [CreateDate] DATETIME NULL, 
    [UpdateDate] DATETIME NULL, 
    [DeleteDate] DATETIME NULL
)