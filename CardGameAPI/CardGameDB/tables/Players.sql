CREATE TABLE [dbo].[Players]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserName] VARCHAR(50) NULL, 
    [LastActivity] DATETIME NULL, 
    [Admin] BIT NOT NULL DEFAULT 0
)
