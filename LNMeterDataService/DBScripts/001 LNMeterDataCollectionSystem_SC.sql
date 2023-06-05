
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udtLNMeterCategory]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[udtLNMeterCategory](
		[ID]					[int] IDENTITY(1,1) NOT NULL,
		[CategoryName]			[varchar](200) NOT NULL,
		[CategoryDesc]			[varchar](200) NOT NULL,
		[Command]				[varchar](200) NOT  NULL,
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

IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udtLNSerialPortServer]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[udtLNSerialPortServer](
		[ID] [int] IDENTITY(1,1) NOT NULL,
		[MeterCategoryID] INT NOT NULL,
		[HostIP] varchar(200) NOT NULL,
		[Port] INT NOT NULL,
		[Location] nvarchar(200) NULL,
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

IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udtLNElectricityMeter]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[udtLNElectricityMeter](
		[ID] [int] IDENTITY(1,1) NOT NULL,
		[SerialPortServerID] int NOT null,
		[MeterAddr] nvarchar(200) NOT NULL,
		[MeterName] nvarchar(200),
		[MeterNo] varchar(200),
		[MeterLevel] varchar(200),
		[IsSolarEnergy] bit not null default (0),
		[AreaLocation] nvarchar(200),
		[AreaFunction] nvarchar(200),
		[CreationTime] datetime not null default(GETDATE())
	PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
GO
IF Not EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[udtLNElectricityMeter]') 
	AND [Name]='udtLNElectricityMeter_IDX01')
BEGIN
	CREATE UNIQUE INDEX [udtLNElectricityMeter_IDX01] ON [dbo].[udtLNElectricityMeter]
	(
		[SerialPortServerID] ASC,
		[MeterAddr] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
END
GO

IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udtLNElectricityMeterMaster]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[udtLNElectricityMeterMaster](
		[ID] [int] IDENTITY(1,1) NOT NULL,
		[ElectricityMeterID] int NOT null,
		[CurrDay] varchar(200),
		[KWhAmount] numeric(10,4),
		[CreationTime] datetime not null default(GETDATE()),
		[LastUpdate] datetime not null default(GETDATE())
	PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
GO
IF Not EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[udtLNElectricityMeterMaster]') 
	AND [Name]='udtLNElectricityMeterMaster_IDX01')
BEGIN
	CREATE NONCLUSTERED INDEX [udtLNElectricityMeterMaster_IDX01] ON [dbo].[udtLNElectricityMeterMaster]
	(
		[ElectricityMeterID] ASC,
		[CurrDay] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
END
GO

IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udtLNElectricityMeterDetail]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[udtLNElectricityMeterDetail](
		[ID] [int] IDENTITY(1,1) NOT NULL,
		[ElectricityMeterMasterID] int NOT null,
		[CurrDay] varchar(200),
		[PreKWhAmount] numeric(10,4),
		[CurrKWhAmount] numeric(10,4),
		[DeltaKWhAmount] numeric(10,4),
		[CreationTime] datetime not null default(GETDATE())
	PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
GO

-----
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udtLNWaterMeter]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[udtLNWaterMeter](
		[ID] [int] IDENTITY(1,1) NOT NULL,
		[SerialPortServerID] int NOT null,
		[MeterAddr] varchar(200),
		[MeterName] nvarchar(200),
		[MeterNo] varchar(200),
		[AreaLocation] nvarchar(200),
		[AreaFunction] nvarchar(200),
		[CreationTime] datetime not null default(GETDATE())
	PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
GO
IF Not EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[udtLNWaterMeter]') 
	AND [Name]='udtLNWaterMeter_IDX01')
BEGIN
	CREATE UNIQUE INDEX [udtLNWaterMeter_IDX01] ON [dbo].[udtLNWaterMeter]
	(
		[SerialPortServerID] ASC,
		[MeterAddr] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
END
GO

IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udtLNWaterMeterMaster]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[udtLNWaterMeterMaster](
		[ID] [int] IDENTITY(1,1) NOT NULL,
		[WaterMeterID] int NOT null,
		[CurrDay] varchar(200),
		[WaterAmount] numeric(10,4),
		[CreationTime] datetime not null default(GETDATE()),
		[LastUpdate] datetime not null default(GETDATE())
	PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
GO
IF Not EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[udtLNWaterMeterMaster]') 
	AND [Name]='udtLNWaterMeterMaster_IDX01')
BEGIN
	CREATE NONCLUSTERED INDEX [udtLNWaterMeterMaster_IDX01] ON [dbo].[udtLNWaterMeterMaster]
	(
		[WaterMeterID] ASC,
		[CurrDay] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
END
GO

IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udtLNWaterMeterDetail]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[udtLNWaterMeterDetail](
		[ID] [int] IDENTITY(1,1) NOT NULL,
		[WaterMeterMasterID] int NOT null,
		[CurrDay] varchar(200),
		[PreWaterAmount] numeric(10,4),
		[CurrWaterAmount] numeric(10,4),
		[DeltaWaterAmount] numeric(10,4),
		[CreationTime] datetime not null default(GETDATE())
	PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
GO
-------

IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udtLNRuntimeLogInfo]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[udtLNRuntimeLogInfo](
		[ID] [int] IDENTITY(1,1) NOT NULL,
		[Source] varchar(200) not null,
		[Description] [nvarchar](max) NOT NULL,
		[Time] datetime NOT NULL default(Getdate()),
	 CONSTRAINT [PK_udtLNRuntimeLogInfo] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
END