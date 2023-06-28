----Compressed Air Flow Meter
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udtLNCompressedAirFlowMeter]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[udtLNCompressedAirFlowMeter](
		[ID] [int] IDENTITY(1,1) NOT NULL,
		[SerialPortServerID] int NOT null,
		[MeterAddr] varchar(200) NOT NULL,
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

IF Not EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[udtLNCompressedAirFlowMeter]') 
	AND [Name]='udtLNCompressedAirFlowMeter_IDX01')
BEGIN
	CREATE UNIQUE INDEX [udtLNCompressedAirFlowMeter_IDX01] ON [dbo].[udtLNCompressedAirFlowMeter]
	(
		[SerialPortServerID] ASC,
		[MeterAddr] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
END
GO

IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udtLNCompressedAirFlowMeterMaster]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[udtLNCompressedAirFlowMeterMaster](
		[ID]  INT Identity (1,1),
		[CompressedAirFlowMeterID]  INT,
		[CurrDay]  varchar(200),
		[StartedAccumulatedflow]			numeric(20,4), --开始累积流量(Nm3)
		[CurrentAccumulatedflow]			numeric(20,4), --当前累积流量(Nm3)
		[CompressedAirConsumption] as [CurrentAccumulatedflow] - [StartedAccumulatedflow],  --压缩空气消耗量
		[Instantflow]				numeric(20,4), --瞬时流量(Nm3/h)
		[Instantflowrate]			numeric(20,4), --瞬时流速(m/s)
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
		[LastUpdate] datetime not null default(GETDATE()),
		[CreationTime] datetime not null default(GETDATE())
	PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
GO
IF Not EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[udtLNCompressedAirFlowMeterMaster]') 
	AND [Name]='udtLNCompressedAirFlowMeterMaster_IDX01')
BEGIN
	CREATE NONCLUSTERED INDEX [udtLNCompressedAirFlowMeterMaster_IDX01] ON [dbo].[udtLNCompressedAirFlowMeterMaster]
	(
		[CompressedAirFlowMeterID] ASC,
		[CurrDay] ASC,
		[WK] asc
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
END
GO


IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udtLNCompressedAirFlowMeterDetail]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[udtLNCompressedAirFlowMeterDetail](
		[ID]  INT Identity (1,1),
		[CompressedAirFlowMeterMasterID]  INT,
		[CurrDay]  varchar(200),
		[PreAccumulatedflow]			numeric(20,4),		--上一次累积流量(Nm3)
		[CurrentAccumulatedflow]			numeric(20,4),	--当前累积流量(Nm3)
		[CompressedAirConsumption] as [CurrentAccumulatedflow] - [PreAccumulatedflow],  --压缩空气消耗量
		[Instantflow]				numeric(20,4), --瞬时流量(Nm3/h)
		[Instantflowrate]			numeric(20,4), --瞬时流速(m/s)
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
IF Not EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[udtLNCompressedAirFlowMeterDetail]') 
	AND [Name]='udtLNCompressedAirFlowMeterDetail_IDX01')
BEGIN
	CREATE NONCLUSTERED INDEX [udtLNCompressedAirFlowMeterDetail_IDX01] ON [dbo].[udtLNCompressedAirFlowMeterDetail]
	(
		[CompressedAirFlowMeterMasterID] ASC,
		[CurrDay] ASC,
		[WK] asc,
		[HH] asc
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
END
GO
