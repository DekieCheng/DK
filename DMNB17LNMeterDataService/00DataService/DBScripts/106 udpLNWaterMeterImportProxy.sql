IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udpLNWaterMeterImportProxy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[udpLNWaterMeterImportProxy]
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

CREATE procedure [dbo].[udpLNWaterMeterImportProxy]
	@MeterCategoryID int,
    @SerialPortServerID int,
	@MeterID int,
	@xmlRecData varchar(max)
AS
SET NOCOUNT ON

declare @ret int,
	@PreStere  numeric(20,4),
	@Stere numeric(20,4),
	@IF numeric(20,4),
	@OrgData varchar(max),
	@CurrDay varchar(20),
	@WaterMeterMasterID int

--Extract extra parameter values
Set @xmlRecData = REPLACE(@xmlRecData,'<?xml version="1.0" encoding="ISO-8859-1"?>','')
Set @xmlRecData = REPLACE(@xmlRecData,'<?xml version="1.0" encoding="UTF-16"?>','')

exec @ret = udpLNXMLExtraData @xmlRecData, 'Stere', @Stere output
exec @ret = udpLNXMLExtraData @xmlRecData, 'OrgData', @OrgData output
Set @CurrDay = CONVERT(varchar(10),getdate(),121)

Select 
	@WaterMeterMasterID = ID, 
	@PreStere = CurrentStere
from 
	udtLNWaterMeterMaster with(nolock) 
Where 
	WaterMeterID = @MeterID And CurrDay = @CurrDay

Set @PreStere =  IsNull(@PreStere,@Stere) 

begin tran

	if (@WaterMeterMasterID is null)
	Begin
		Select 
			@PreStere = CurrentStere
		from 
			udtLNWaterMeterMaster with(nolock) 
		Where 
			ID = (Select Max(ID) from udtLNWaterMeterMaster with(nolock) 
				Where WaterMeterID = @MeterID And CurrDay <> @CurrDay )  --拿取前一天的最后一次的读数

		Set @PreStere =  IsNull(@PreStere,@Stere) 

		insert udtLNWaterMeterMaster (
			[WaterMeterID] ,
			[CurrDay] ,
			[StartedStere],
			[CurrentStere]
			)
		Values(
			@MeterID,
			@CurrDay,
			@PreStere,
			@Stere
		)
		Set @WaterMeterMasterID = SCOPE_IDENTITY()
	End 
	else
	Begin
		Update udtLNWaterMeterMaster Set [CurrentStere] = @Stere, LastUpdate = getdate() Where ID = @WaterMeterMasterID
	End

	Insert into udtLNWaterMeterDetail(
		[WaterMeterMasterID]  ,
		[CurrDay] ,
		[PreStere] ,
		[CurrStere]
	)
	Values (
		@WaterMeterMasterID,
		@CurrDay,
		@PreStere,
		@Stere
	)

if (@@ERROR <> 0)
	rollback
else
	Commit

return 0


GO


