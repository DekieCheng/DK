
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udtLNMeterCategory]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[udtLNMeterCategory](
		[ID]					[int] IDENTITY(1,1) NOT NULL,
		[CategoryName]			[varchar](200) NOT NULL,
		[CategoryDesc]			[nvarchar](200) NOT NULL,
		[CycleTime]				[numeric](10,2) NOT NULL Default(3600), -- Unit: is Second
		[UDPToSaveData]			[varchar](200) NOT  NULL,
		[UDPToSaveRuntimeLog]	[varchar](200) NOT NULL,
		[AssemblyName]			[varchar](200)    NULL,
		[ClassName]				[varchar](200)    NULL,
		[CreationTime]			[datetime] not null default(GETDATE())
	PRIMARY KEY CLUSTERED
	(
		[ID] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
GO

IF Not EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[udtLNMeterCategory]') 
	AND [Name]='udtLNMeterCategory_IDX01')
BEGIN
	CREATE UNIQUE INDEX [udtLNMeterCategory_IDX01] ON [dbo].[udtLNMeterCategory]
	(
		[CategoryName] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
END

IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udtLNCommand]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[udtLNCommand](
		[ID]					[int] IDENTITY(1,1) NOT NULL,
		[MeterCategoryID]		INT NOT NULL,
		[CommandName]			[varchar](200) NOT NULL,
		[CommandValue]				[varchar](400) NOT NULL,
		[Status]				[bit] default(1) not null, --0:InActive will be skipped, 1: Active
		[CreationTime]			[datetime] not null default(GETDATE())
	PRIMARY KEY CLUSTERED
	(
		[ID] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
GO

IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udtLNSerialPortServer]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[udtLNSerialPortServer](
		[ID] [int] IDENTITY(1,1) NOT NULL,
		[MeterCategoryID] INT NOT NULL,
		[HostIP] varchar(200) NOT NULL,
		[Port] INT NOT NULL,
		[Location] nvarchar(200) NULL,
		[Status] bit default(1) not null, --0:InActive will be skipped, 1: Active
		[CreationTime] datetime not null default(GETDATE())
	PRIMARY KEY CLUSTERED
	(
		[ID] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
GO

IF Not EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[udtLNSerialPortServer]') 
	AND [Name]='udtLNSerialPortServer_IDX01')
BEGIN
	CREATE UNIQUE INDEX [udtLNSerialPortServer_IDX01] ON [dbo].[udtLNSerialPortServer]
	(
		[HostIP] ASC,
		[Port] ASC,
		[MeterCategoryID] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
END

IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udtLNRuntimeLogInfo]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[udtLNRuntimeLogInfo](
		[ID] [int] IDENTITY(1,1) NOT NULL,
		[MeterCategoryID] int,
		[SerialPortServerID] int,
		[MeterID] int,
		[Description] [nvarchar](max) NOT NULL,
		[Time] datetime NOT NULL default(Getdate()),
	 CONSTRAINT [PK_udtLNRuntimeLogInfo] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
END