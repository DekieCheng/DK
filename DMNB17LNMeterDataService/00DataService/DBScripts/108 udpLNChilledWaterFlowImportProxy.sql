IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udpLNChilledWaterFlowImportProxy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[udpLNChilledWaterFlowImportProxy]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*
***************************************************************************
Author			: Dekie Cheng
Creation Date	: 16th Jun, 2023
Explanation		:  
Script Version	: 1.0
Parameter	:
Output			:		
Is used			:  	
ModifiedHistory		:	
**************************************************************************
*/

CREATE procedure [dbo].[udpLNChilledWaterFlowImportProxy]
	@MeterCategoryID int,
    @SerialPortServerID int,
	@MeterID int,
	@xmlRecData varchar(max)
AS
SET NOCOUNT ON

declare @ret int,
	@CurrDay varchar(20),
	@OrgData varchar(max),
	@IF  numeric(20,4),
	@IS numeric(20,4),
	@IFR numeric(20,4),
	@AF numeric(20,4),
	@AH numeric(20,4),
	@IWT numeric(20,4),
	@OWT numeric(20,4),
	@PreAH numeric(20,4)

Declare @ChilledWaterFlowMeterMasterID int

--Extract extra parameter values
Set @xmlRecData = REPLACE(@xmlRecData,'<?xml version="1.0" encoding="ISO-8859-1"?>','')
Set @xmlRecData = REPLACE(@xmlRecData,'<?xml version="1.0" encoding="UTF-16"?>','')

exec @ret = udpLNXMLExtraData @xmlRecData, 'OrgData', @OrgData output
exec @ret = udpLNXMLExtraData @xmlRecData, 'IF', @IF output
exec @ret = udpLNXMLExtraData @xmlRecData, 'IS', @IS output
exec @ret = udpLNXMLExtraData @xmlRecData, 'IFR', @IFR output
exec @ret = udpLNXMLExtraData @xmlRecData, 'AF', @AF output
exec @ret = udpLNXMLExtraData @xmlRecData, 'AH', @AH output
exec @ret = udpLNXMLExtraData @xmlRecData, 'IWT', @IWT output
exec @ret = udpLNXMLExtraData @xmlRecData, 'OWT', @OWT output
Set @CurrDay = CONVERT(varchar(10),getdate(),121)

Select 
	@ChilledWaterFlowMeterMasterID = ID,
	@PreAH = [CurrAccumulatedheat]
from 
	udtLNChilledWaterFlowMeterMaster with(nolock) 
Where 
	ChilledWaterFlowMeterID = @MeterID And CurrDay = @CurrDay
Set @PreAH =  IsNull(@PreAH,@AH) 
if (@AH-@PreAH)<0 Set @AH = @PreAH

begin tran
if (IsNull(@ChilledWaterFlowMeterMasterID,0)=0)
Begin
	Select 
		@PreAH = [CurrAccumulatedheat]
	from 
		udtLNChilledWaterFlowMeterMaster with(nolock) 
	Where 
		ID = (Select Max(ID) from udtLNChilledWaterFlowMeterMaster with(nolock) 
			Where ChilledWaterFlowMeterID = @MeterID And CurrDay <> @CurrDay )  --拿取前一天的最后一次的读数

	Set @PreAH =  IsNull(@PreAH,@AH) 

	Insert into udtLNChilledWaterFlowMeterMaster (
		[ChilledWaterFlowMeterID]  ,
		[CurrDay] ,
		[Accumulatedflow]			 , --累积流量(Nm3)
		[Instantflow]				 , --瞬时流量(Nm3/h)
		[Instantflowrate]			 , --瞬时流速(m/s)
		[InletWaterTemperature]		 , --进水温度
		[OutletWaterTemperature]	 , --出水温度
		[Instantaneousheat]			 , --瞬时热流量(GJ/h)
		[StartedAccumulatedheat]	 , --当日开始累积热量(GJ)
		[CurrAccumulatedheat]		 --当前累积热量(GJ)
	)
	Values (
		@MeterID,
		@CurrDay,
		@AF,
		@IF,
		@IFR,
		@IWT,
		@OWT,
		@IS,
		@PreAH,
		@AH
	)
	Set @ChilledWaterFlowMeterMasterID = SCOPE_IDENTITY()
End 
else
Begin
	Update udtLNChilledWaterFlowMeterMaster 
	Set 
		[Accumulatedflow]			=@AF , --累积流量(Nm3)
		[Instantflow]				=@IF , --瞬时流量(Nm3/h)
		[Instantflowrate]			=@IFR , --瞬时流速(m/s)
		[InletWaterTemperature]		=@IWT , --进水温度
		[OutletWaterTemperature]	=@OWT , --出水温度
		[Instantaneousheat]			=@IS , --瞬时热流量(GJ/h)
		[CurrAccumulatedheat]		=@AH ,  --正累积热量(GJ)
		LastUpdate = getdate()
	Where
	ID = @ChilledWaterFlowMeterMasterID
End

Insert into udtLNChilledWaterFlowMeterDetail (
	[ChilledWaterFlowMeterMasterID]  ,
	[CurrDay] ,
	[Accumulatedflow]			 , --累积流量(Nm3)
	[Instantflow]				 , --瞬时流量(Nm3/h)
	[Instantflowrate]			 , --瞬时流速(m/s)
	[InletWaterTemperature]		 , --进水温度
	[OutletWaterTemperature]	 , --出水温度
	[Instantaneousheat]			 , --瞬时热流量(GJ/h)
	[PreAccumulatedheat]		 , --上一次读取累积热量(GJ)
	[CurrentAccumulatedheat]	   --本次读取累积热量(GJ)
)
Values (
	@ChilledWaterFlowMeterMasterID,
	@CurrDay,
	@AF,
	@IF,
	@IFR,
	@IWT,
	@OWT,
	@IS,
	IsNull(@PreAH,0),
	@AH
)

if (@@ERROR <> 0)
	rollback
else
	Commit

return 0


GO

