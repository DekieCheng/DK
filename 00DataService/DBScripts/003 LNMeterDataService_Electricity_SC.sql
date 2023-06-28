
----ElectricityMeter
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udtLNElectricityMeter]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[udtLNElectricityMeter](
		[ID] [int] IDENTITY(1,1) NOT NULL,
		[SerialPortServerID] int NOT null,
		[MeterAddr] varchar(200) NOT NULL,
		[IsNewProtocal] bit not null default (0),
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
		[TAP] numeric(20,4), --总有功功率
		[CurrTPAE] numeric(20,4), --当前正向有功总电能
		[StartedTPAE] numeric(20,4), --当天起始正向有功总电能
		[TPAEConsumption] as  [CurrTPAE] - [StartedTPAE], --每天累积正向有功总电能: [CurrTPAE] - [StartedTPAE]
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
IF Not EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[udtLNElectricityMeterMaster]') 
	AND [Name]='udtLNElectricityMeterMaster_IDX01')
BEGIN
	CREATE NONCLUSTERED INDEX [udtLNElectricityMeterMaster_IDX01] ON [dbo].[udtLNElectricityMeterMaster]
	(
		[ElectricityMeterID] ASC,
		[CurrDay] ASC,
		[WK] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
END
GO

IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udtLNElectricityMeterDetail]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[udtLNElectricityMeterDetail](
		[ID] [int] IDENTITY(1,1) NOT NULL,
		[ElectricityMeterMasterID] int NOT null,
		[CurrDay] varchar(200),
		[TAP] numeric(20,4),
		[PreTPAE] numeric(20,4),
		[CurrTPAE] numeric(20,4),
		[TPAEConsumption] As [CurrTPAE] - [PreTPAE],
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

IF Not EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[udtLNElectricityMeterDetail]') 
	AND [Name]='udtLNElectricityMeterDetail_IDX01')
BEGIN
	CREATE NONCLUSTERED INDEX [udtLNElectricityMeterDetail_IDX01] ON [dbo].[udtLNElectricityMeterDetail]
	(
		[ElectricityMeterMasterID] ASC,
		[CurrDay] ASC,
		[WK] ASC,
		[HH] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
END
GO

IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udtLNElectricityMeterExtraData]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[udtLNElectricityMeterExtraData](
		[ID] [int] IDENTITY(1,1) NOT NULL,
		[ElectricityMeterMasterID] int NOT null,
		[CurrDay] varchar(200),
		[APhaseVoltage] varchar(100),--numeric(10,4), --A相电压
		[BPhaseVoltage] varchar(100),--numeric(10,4), --B相电压
		[CPhaseVoltage] varchar(100),--numeric(10,4), --C相电压
		[APhaseCurrent] varchar(100),--numeric(10,4), --A相电流
		[BPhaseCurrent] varchar(100),--numeric(10,4), --B相电流
		[CPhaseCurrent] varchar(100),--numeric(10,4), --C相电流
		[CPPF]  numeric(20,4), --combined phase power factor (合相功率因数)
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
		[CreationTime] datetime not null default(GETDATE()),
		[LastUpdate] datetime not null default(GETDATE())
	PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
GO
IF Not EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[udtLNElectricityMeterExtraData]') 
	AND [Name]='udtLNElectricityMeterExtraData_IDX01')
BEGIN
	CREATE NONCLUSTERED INDEX [udtLNElectricityMeterExtraData_IDX01] ON [dbo].[udtLNElectricityMeterExtraData]
	(
		[ElectricityMeterMasterID] ASC,
		[CurrDay] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
END
GO
