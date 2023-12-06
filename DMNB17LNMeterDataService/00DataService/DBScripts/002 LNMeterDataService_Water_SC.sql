
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
		[StartedStere]		numeric(20,4), --当日第一次水表读数
		[CurrentStere]		numeric(20,4), --当前水表读数
		[WaterConsumption] as [CurrentStere] - [StartedStere], --本日消耗水量(m3)
		[Reserved_001] varchar(400),
		[Reserved_002] varchar(400),
		[Reserved_003] varchar(400),
		[Reserved_004] varchar(400),
		[Reserved_005] varchar(400),
		[Reserved_006] varchar(400),
		[Reserved_007] varchar(400),
		[Reserved_008] varchar(400),
		[Reserved_009] varchar(400),
		[Reserved_010] varchar(400),
		[WK] varchar(400) not null default(DATEPART(ISO_WEEK,GETDATE())),
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
		[CurrDay] ASC,
		[WK] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
END
GO

IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udtLNWaterMeterDetail]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[udtLNWaterMeterDetail](
		[ID] [int] IDENTITY(1,1) NOT NULL,
		[WaterMeterMasterID] int NOT null,
		[CurrDay] varchar(200),
		[PreStere]			numeric(20,4), --上一次水表读数(m3)
		[CurrStere]			numeric(20,4), --本次水表读数(m3)
		[WaterConsumption] as [CurrStere] - [PreStere], --本时段消耗水量(m3)
		[Reserved_001] varchar(400),
		[Reserved_002] varchar(400),
		[Reserved_003] varchar(400),
		[Reserved_004] varchar(400),
		[Reserved_005] varchar(400),
		[Reserved_006] varchar(400),
		[Reserved_007] varchar(400),
		[Reserved_008] varchar(400),
		[Reserved_009] varchar(400),
		[Reserved_010] varchar(400),
		[WK] varchar(400) not null default(DATEPART(ISO_WEEK,GETDATE())),
		[HH] varchar(400) not null default(DATEPART(HOUR,GETDATE())),
		[CreationTime] datetime not null default(GETDATE())
	PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
GO
IF Not EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[udtLNWaterMeterDetail]') 
	AND [Name]='udtLNWaterMeterDetail_IDX01')
BEGIN
	CREATE NONCLUSTERED INDEX [udtLNWaterMeterDetail_IDX01] ON [dbo].[udtLNWaterMeterDetail]
	(
		[WaterMeterMasterID] ASC,
		[CurrDay] ASC,
		[WK] ASC,
		[HH] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
END
GO