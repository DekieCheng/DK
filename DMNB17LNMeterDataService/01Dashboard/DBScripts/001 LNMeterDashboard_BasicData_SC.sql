IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udtLNMeterGroupCategory]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[udtLNMeterGroupCategory](
		[ID]					[int] IDENTITY(1,1) NOT NULL,
		[MeterCategoryID]		[int]			NOT NULL,
		[GroupCategoryName]		[nvarchar](200)	NOT NULL,
	PRIMARY KEY CLUSTERED
	(
		[ID] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
GO

IF Not EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[udtLNMeterGroupCategory]') 
	AND [Name]='udtLNMeterGroupCategory_IDX01')
BEGIN
	CREATE UNIQUE INDEX [udtLNMeterGroupCategory_IDX01] ON [dbo].[udtLNMeterGroupCategory]
	(
		[MeterCategoryID] ASC,
		[GroupCategoryName] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
END

IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udtLNMeterGroup]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[udtLNMeterGroup](
		[ID]					[int] IDENTITY(1,1) NOT NULL,
		[MeterGroupCategoryID]	[int]			NOT NULL,
		[GroupName]				[nvarchar](200)	NOT NULL,
		[GroupDesc]				[nvarchar](200) NOT NULL,
		[Sort]					[int] NOT NULL,
	PRIMARY KEY CLUSTERED
	(
		[ID] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
GO

IF Not EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[udtLNMeterGroup]') 
	AND [Name]='udtLNMeterGroup_IDX01')
BEGIN
	CREATE UNIQUE INDEX [udtLNMeterGroup_IDX01] ON [dbo].[udtLNMeterGroup]
	(
		[MeterGroupCategoryID] ASC,
		[GroupName] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
END

IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udtLNMeterMap]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[udtLNMeterMap](
		[ID] [int] IDENTITY(1,1) NOT NULL,
		[MeterGroupID] INT NOT NULL,
		[MeterID]  INT NOT NULL,
		[CreationTime] datetime not null default(GETDATE())
	PRIMARY KEY CLUSTERED
	(
		[ID] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
GO

IF Not EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[udtLNMeterMap]') 
	AND [Name]='udtLNMeterMap_IDX01')
BEGIN
	CREATE UNIQUE INDEX [udtLNMeterMap_IDX01] ON [dbo].[udtLNMeterMap]
	(
		[MeterGroupID] ASC,
		[MeterID] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
END
