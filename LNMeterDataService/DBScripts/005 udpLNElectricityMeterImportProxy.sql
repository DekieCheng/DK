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
	@CurrKWh numeric(10,4),
	@PreKWh numeric(10,4),
	@DeltaKWh numeric(10,4),
	@CurrDay varchar(10),
	@ElectricityMeterMasterID int

--Extract extra parameter values
Set @xmlRecData = REPLACE(@xmlRecData,'<?xml version="1.0" encoding="ISO-8859-1"?>','')
Set @xmlRecData = REPLACE(@xmlRecData,'<?xml version="1.0" encoding="UTF-16"?>','')

exec @ret = udpLNXMLExtraData @xmlRecData, 'KWh', @CurrKWh output
Set @CurrDay = CONVERT(varchar(10),getdate(),121)

Select 
	@ElectricityMeterMasterID = ID, 
	@PreKWh = KWhAmount 
from 
	udtLNElectricityMeterMaster with(nolock) 
Where 
	ElectricityMeterID = @MeterID And CurrDay = @CurrDay

begin tran

	if (@ElectricityMeterMasterID is null)
	Begin
		insert udtLNElectricityMeterMaster (
			[ElectricityMeterID] ,
			[CurrDay] ,
			[KWhAmount]
			)
		Values(
			@MeterID,
			@CurrDay,
			@CurrKWh
		)
		Set @ElectricityMeterMasterID = SCOPE_IDENTITY()
	End 
	else
	Begin
		Update udtLNElectricityMeterMaster Set KWhAmount = @CurrKWh, LastUpdate = getdate() Where ID = @ElectricityMeterMasterID
	End
	Set @DeltaKWh = @CurrKWh - IsNull(@PreKWh,0)

	Insert into udtLNElectricityMeterDetail (
		[ElectricityMeterMasterID]  ,
		[CurrDay] ,
		[PreKWhAmount] ,
		[CurrKWhAmount] ,
		[DeltaKWhAmount]
	)
	Values (
		@ElectricityMeterMasterID,
		@CurrDay,
		@PreKWh,
		@CurrKWh,
		@DeltaKWh
	)
	
Commit tran

return 0


GO

