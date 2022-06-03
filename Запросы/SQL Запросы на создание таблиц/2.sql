CREATE TABLE [dbo].[Pupils](
	[PupilID] [int] IDENTITY(1,1) NOT NULL Primary Key,
	[Surname] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Midname] [nvarchar](50) NULL,
	[ClassID] [int] NOT NULL FOREIGN KEY REFERENCES Class(ClassID),
	)