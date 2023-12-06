IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udpLNCompressedAirMeterImportProxy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[udpLNCompressedAirMeterImportProxy]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*
***************************************************************************
Author			: Dekie Cheng
Creation Date	: 13th Jun, 2023
Explanation		:  
Script Version	: 1.0
Parameter	:
Output			:		
Is used			:  	
ModifiedHistory		:	
**************************************************************************
*/

CREATE procedure [dbo].[udpLNCompressedAirMeterImportProxy]
	@MeterCategoryID int,
    @SerialPortServerID int,
	@MeterID int,
	@xmlRecData varchar(max)
AS
SET NOCOUNT ON

declare @ret int,
	@CurrDay varchar(20),
	@OrgData varchar(max),
	@IND numeric(20,4),
	@TFR numeric(20,4),
	@CT numeric(20,4),
	@PreCT numeric(20,4)

Declare @CompressedAirFlowMeterMasterID  int

--Extract extra parameter values
Set @xmlRecData = REPLACE(@xmlRecData,'<?xml version="1.0" encoding="ISO-8859-1"?>','')
Set @xmlRecData = REPLACE(@xmlRecData,'<?xml version="1.0" encoding="UTF-16"?>','')

exec @ret = udpLNXMLExtraData @xmlRecData, 'OrgData', @OrgData output
exec @ret = udpLNXMLExtraData @xmlRecData, 'IND', @IND output
exec @ret = udpLNXMLExtraData @xmlRecData, 'TFR', @TFR output
exec @ret = udpLNXMLExtraData @xmlRecData, 'CT', @CT output
Set @CurrDay = CONVERT(varchar(10),getdate(),121)

Select 
	@CompressedAirFlowMeterMasterID = ID,
	@PreCT = [CurrentAccumulatedflow]
from 
	udtLNCompressedAirFlowMeterMaster with(nolock) 
Where 
	[CompressedAirFlowMeterID] = @MeterID And CurrDay = @CurrDay
Set @PreCT =  IsNull(@PreCT,@CT) 
begin tran
if (IsNull(@CompressedAirFlowMeterMasterID,0)=0)
Begin
	Select 
		@PreCT = [CurrentAccumulatedflow]
	from 
		udtLNCompressedAirFlowMeterMaster with(nolock) 
	Where 
		ID = (Select Max(ID) from udtLNCompressedAirFlowMeterMaster with(nolock) 
			Where [CompressedAirFlowMeterID] = @MeterID And CurrDay <> @CurrDay)  --最后一次的读数

	Set @PreCT =  IsNull(@PreCT,@CT)

	Insert into udtLNCompressedAirFlowMeterMaster (
		[CompressedAirFlowMeterID]  ,
		[CurrDay] ,
		[StartedAccumulatedflow],		--开始累积流量(Nm3)
		[CurrentAccumulatedflow],		--当前累积流量(Nm3)
		[Instantflow]				 ,	--瞬时流量(Nm3/h)
		[Instantflowrate]				--瞬时流速(m/s)
	)
	Values (
		@MeterID,
		@CurrDay,
		@PreCT,
		@CT,
		@IND,
		@TFR
	)
	Set @CompressedAirFlowMeterMasterID= SCOPE_IDENTITY()
End 
else
Begin
	Update udtLNCompressedAirFlowMeterMaster 
	Set 
		[CurrentAccumulatedflow]	=@CT ,	--累积流量(Nm3)
		[Instantflow]				=@IND , --瞬时流量(Nm3/h)
		[Instantflowrate]			=@TFR , --瞬时流速(m/s)
		LastUpdate = getdate()
	Where
	ID = @CompressedAirFlowMeterMasterID
End

Insert into udtLNCompressedAirFlowMeterDetail (
	[CompressedAirFlowMeterMasterID] ,
	[CurrDay] ,
	[PreAccumulatedflow]		,	--上一次累积流量(Nm3)
	[CurrentAccumulatedflow]	,	--当前累积流量(Nm3)
	[Instantflow]				,	--瞬时流量(Nm3/h)
	[Instantflowrate]				--瞬时流速(m/s)
)
Values (
	@CompressedAirFlowMeterMasterID,
	@CurrDay,
	IsNull(@PreCT,0),
	@CT,
	@IND,
	@TFR
)

if (@@ERROR <> 0)
	rollback
else
	Commit
return 0


GO

