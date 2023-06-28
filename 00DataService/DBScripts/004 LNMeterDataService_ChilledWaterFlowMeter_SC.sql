
--Air Condition Water Flow Meter
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udtLNChilledWaterFlowMeter]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[udtLNChilledWaterFlowMeter](
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

IF Not EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[udtLNChilledWaterFlowMeter]') 
	AND [Name]='udtLNChilledWaterFlowMeter_IDX01')
BEGIN
	CREATE UNIQUE INDEX [udtLNChilledWaterFlowMeter_IDX01] ON [dbo].[udtLNChilledWaterFlowMeter]
	(
		[SerialPortServerID] ASC,
		[MeterAddr] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
END
GO

IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udtLNChilledWaterFlowMeterMaster]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[udtLNChilledWaterFlowMeterMaster](
		[ID]  INT Identity (1,1),
		[ChilledWaterFlowMeterID]  INT,
		[CurrDay]  varchar(200),
		[Accumulatedflow]			numeric(20,4), --累积流量(Nm3)
		[Instantflow]				numeric(20,4), --瞬时流量(Nm3/h)
		[Instantflowrate]			numeric(20,4), --瞬时流速(m/s)
		[InletWaterTemperature]		numeric(20,4), --进水温度
		[OutletWaterTemperature]	numeric(20,4), --出水温度
		[Instantaneousheat]			numeric(20,4), --瞬时热量(GJ/h)
		[StartedAccumulatedheat]	numeric(20,4), --当日开始累积热量(GJ)
		[CurrAccumulatedheat]		numeric(20,4), --当前累积热量(GJ)
		[AccumulatedheatConsumption] as [CurrAccumulatedheat]-[StartedAccumulatedheat], --当日累积热量总用量(GJ)
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

IF Not EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[udtLNChilledWaterFlowMeterMaster]') 
	AND [Name]='udtLNChilledWaterFlowMeterMaster_IDX01')
BEGIN
	CREATE NONCLUSTERED INDEX [udtLNChilledWaterFlowMeterMaster_IDX01] ON [dbo].[udtLNChilledWaterFlowMeterMaster]
	(
		[ChilledWaterFlowMeterID] ASC,
		[CurrDay] ASC,
		[WK] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
END
GO

IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udtLNChilledWaterFlowMeterDetail]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[udtLNChilledWaterFlowMeterDetail](
		[ID] [int] IDENTITY(1,1) NOT NULL,
		[ChilledWaterFlowMeterMasterID] int NOT null,
		[CurrDay] varchar(200),
		[Accumulatedflow]			numeric(20,4), --累积流量(Nm3)
		[Instantflow]				numeric(20,4), --瞬时流量(Nm3/h)
		[Instantflowrate]			numeric(20,4), --瞬时流速(m/s)
		[InletWaterTemperature]		numeric(20,4), --进水温度
		[OutletWaterTemperature]	numeric(20,4), --出水温度
		[Instantaneousheat]			numeric(20,4), --瞬时热量(GJ/h)
		[PreAccumulatedheat]		numeric(20,4), --上一次读取累积热量(GJ)
		[CurrentAccumulatedheat]	numeric(20,4), --本次读取累积热量(GJ)
		[AccumulatedheatConsumption] as [CurrentAccumulatedheat]-[PreAccumulatedheat], --当日累积热量总用量(GJ)
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

IF Not EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[udtLNChilledWaterFlowMeterDetail]') 
	AND [Name]='udtLNChilledWaterFlowMeterDetail_IDX01')
BEGIN
	CREATE NONCLUSTERED INDEX [udtLNChilledWaterFlowMeterDetail_IDX01] ON [dbo].[udtLNChilledWaterFlowMeterDetail]
	(
		[ChilledWaterFlowMeterMasterID] ASC,
		[CurrDay] ASC,
		[WK] ASC,
		[HH] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
END
GO