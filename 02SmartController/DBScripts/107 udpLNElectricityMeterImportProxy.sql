IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udpLNElectricityMeterImportProxy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[udpLNElectricityMeterImportProxy]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*
***************************************************************************
Author			: Dekie Cheng
Creation Date	: 4th Jun, 2023
Explanation		:  
Script Version	: 1.0
Parameter	:
Output			:		
Is used			:  	
ModifiedHistory		:	
**************************************************************************
*/

CREATE procedure [dbo].[udpLNElectricityMeterImportProxy]
	@MeterCategoryID int,
    @SerialPortServerID int,
	@MeterID int,
	@xmlRecData varchar(max)
AS
SET NOCOUNT ON

declare @ret int,
	@TAP  numeric(20,4),
	@TPAE numeric(20,4),
	@OrgData varchar(max),
	@CurrTPAE numeric(20,4),
	@PreTPAE numeric(20,4),
	@CurrDay varchar(20),
	@ElectricityMeterMasterID int,
	@APV varchar(100), --numeric(10,4),
	@BPV varchar(100), --numeric(10,4),
	@CPV varchar(100), --numeric(10,4),
	@APC varchar(100), --numeric(10,4),
	@BPC varchar(100), --numeric(10,4),
	@CPC varchar(100), --numeric(10,4),
	@CPPF varchar(100)--numeric(10,4)

--Extract extra parameter values
Set @xmlRecData = REPLACE(@xmlRecData,'<?xml version="1.0" encoding="ISO-8859-1"?>','')
Set @xmlRecData = REPLACE(@xmlRecData,'<?xml version="1.0" encoding="UTF-16"?>','')

exec @ret = udpLNXMLExtraData @xmlRecData, 'TAP', @TAP output
exec @ret = udpLNXMLExtraData @xmlRecData, 'TPAE', @CurrTPAE output
exec @ret = udpLNXMLExtraData @xmlRecData, 'OrgData', @OrgData output
exec @ret = udpLNXMLExtraData @xmlRecData, 'APV', @APV output
exec @ret = udpLNXMLExtraData @xmlRecData, 'BPV', @BPV output
exec @ret = udpLNXMLExtraData @xmlRecData, 'CPV', @CPV output
exec @ret = udpLNXMLExtraData @xmlRecData, 'APC', @APC output
exec @ret = udpLNXMLExtraData @xmlRecData, 'BPC', @BPC output
exec @ret = udpLNXMLExtraData @xmlRecData, 'CPC', @CPC output
exec @ret = udpLNXMLExtraData @xmlRecData, 'CPPF', @CPPF output
Set @CurrDay = CONVERT(varchar(10),getdate(),121)

Select 
	@ElectricityMeterMasterID = ID, 
	@PreTPAE = [CurrTPAE]
from 
	udtLNElectricityMeterMaster with(nolock) 
Where 
	ElectricityMeterID = @MeterID And CurrDay = @CurrDay

Set @PreTPAE =  IsNull(@PreTPAE,@CurrTPAE) 

begin tran
	if (@ElectricityMeterMasterID is null)
	Begin
		Select 
			@PreTPAE = [CurrTPAE]
		from 
			udtLNElectricityMeterMaster with(nolock) 
		Where 
			ID = (Select Max(ID) from udtLNElectricityMeterMaster with(nolock) 
				Where ElectricityMeterID = @MeterID  And CurrDay <> @CurrDay )  --拿取前一天的最后一次的读数

		Set @PreTPAE =  IsNull(@PreTPAE,@CurrTPAE) 

		insert udtLNElectricityMeterMaster (
			[ElectricityMeterID] ,
			[CurrDay] ,
			[TAP],
			[CurrTPAE],
			[StartedTPAE]
			)
		Values(
			@MeterID,
			@CurrDay,
			@TAP,
			@CurrTPAE,
			@PreTPAE
		)
		Set @ElectricityMeterMasterID = SCOPE_IDENTITY()
	End 
	else
	Begin
		Update udtLNElectricityMeterMaster Set [CurrTPAE] = @CurrTPAE, [TAP] = @TAP, LastUpdate = getdate() Where ID = @ElectricityMeterMasterID
	End

	Insert into udtLNElectricityMeterDetail (
		[ElectricityMeterMasterID]  ,
		[CurrDay] ,
		[TAP],
		[PreTPAE] ,
		[CurrTPAE]
	)
	Values (
		@ElectricityMeterMasterID,
		@CurrDay,
		@TAP,
		@PreTPAE,
		@CurrTPAE
	)

	Insert into udtLNElectricityMeterExtraData (
		[ElectricityMeterMasterID]  ,
		[CurrDay] ,
		[APhaseVoltage],
		[BPhaseVoltage],
		[CPhaseVoltage],
		[APhaseCurrent],
		[BPhaseCurrent],
		[CPhaseCurrent],
		[CPPF]
	)
	Values (
		@ElectricityMeterMasterID,
		@CurrDay,
		@APV,
		@BPV,
		@CPV,
		@APC,
		@BPC,
		@CPC,
		@CPPF
	)
	
if (@@ERROR <> 0)
	rollback
else
	Commit
return 0


GO

