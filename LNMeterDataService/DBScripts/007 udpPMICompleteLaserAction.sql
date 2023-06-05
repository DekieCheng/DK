IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udpPMICompleteLaserAction]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[udpPMICompleteLaserAction]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*
***************************************************************************
Author			: Dekie Cheng
Creation Date	: 1st August, 2022
Explanation		:  
Script Version	: 2.8
Parameter	:
Output			:		
Is used			:  	
ModifiedHistory		:	
**************************************************************************
*/

CREATE procedure [dbo].[udpPMICompleteLaserAction]
	@xmlUnitSN varchar(max)=null,
    @xmlProdOrder varchar(max)=null,
    @xmlPart varchar(max)=null,
    @xmlStation varchar(max)=null,
    @EmployeeID int,
    @xmlExtraData varchar(max)=null
AS
SET NOCOUNT ON

declare @ret int,
	@EnterTime datetime,
	@ExitTime datetime,
	@RouteID int,
	@PartID int,
	@CurrUnitStateID int,
	@NextUnitStateID int,
	@SNList varchar(max),
	@TrayID int,
	@xml xml
	
declare @ffUnit table (
	[cID] int identity (1,1) primary key,
	UnitSN varchar(200),
	UnitID int,
	UnitPartID int,
	CurrUnitStateID int,
	NextStateID int
)

Set @EnterTime = GETDATE()
Set @ExitTime = GETDATE()

--extract Station info
Set @xmlStation = REPLACE(@xmlStation,'<?xml version="1.0" encoding="ISO-8859-1"?>','')
Declare @StationID int, @StationTypeID int, @LineID int
exec @ret = dbo.uspXMLStation 
	@xmlStation, 
	@StationID out,
	default,
	@StationTypeID out,
	@LineID out
if @ret > 0 return @ret

--Extra Part Information
Set @xmlPart = REPLACE(@xmlPart,'<?xml version="1.0" encoding="ISO-8859-1"?>','')
exec @ret = uspXMLPart @xmlPart,@partid out

--Extract extra parameter values
Set @xmlExtraData = REPLACE(@xmlExtraData,'<?xml version="1.0" encoding="ISO-8859-1"?>','')
exec @ret = udpPMILaserXMLExtraData @xmlExtraData, 'TrayID', @TrayID output
exec @ret = udpPMILaserXMLExtraData @xmlExtraData, 'SNList', @SNList output

Set @xml = cast(@SNList as xml)
Insert into @ffUnit(UnitSN)
Select
	x.SN
from
	(
		Select 
			col.value('text()[1]','varchar(200)') as SN
		from  
			@xml.nodes('sn') as t(col)
	) x
Order By
	x.SN
Update @ffUnit Set UnitID = sn.UnitID from ffSerialNumber sn with(nolock) Where sn.Value = UnitSN
Update @ffUnit Set UnitPartID = u.PartID, CurrUnitStateID = u.UnitStateID from ffUnit u with(nolock) Where u.ID = UnitID
Select @PartID =UnitPartID, @CurrUnitStateID = CurrUnitStateID from @ffUnit Where UnitID is not null
Set @RouteID =dbo.ufnRTEGetRouteID(@LineID, @PartID, NULL)

exec @ret = dbo.uspRTEGetNextState @CurrUnitStateID, @StationTypeID, @LineID, @PartID, 'PMILaserOK', @RouteID out, @NextUnitStateID out
if @ret>0 return @ret
Update @ffUnit Set NextStateID = @NextUnitStateID

begin tran
	update ffUnit with(UPDLOCK) set  
		UnitStateID = u.NextStateID,
		LastUpdate =  @EnterTime,
		StationID = @StationID,
		LineID = @LineID,
		EmployeeID = @EmployeeID
	from
		@ffUnit u
	where 
		[ID] = u.UnitID  
	if @@error > 0 
	begin  
		if @@trancount = 1 rollback else commit  
		return 140308501 --failed to update ffUnit table  
	end

	insert into ffHistory(
		UnitID,
		UnitStateID,
		EmployeeID,
		StationID,
		EnterTime,
		ExitTime,
		ProductionOrderID, 
		LooperCount,
		Quantity,
		PartID,
		TaskComplete
	)
	Select 
		UnitID,
		NextStateID,
		@EmployeeID,
		@StationID,
		@EnterTime,
		@ExitTime,
		null,
		1,
		0,
		UnitPartID,
		1
	from 
		@ffUnit 
	Where
		UnitID is not null
	Order By cID
	if @@error > 0 
	begin  
		if @@trancount = 1 rollback else commit  
		return 140308502 --failed to update ffHistory table  
	end

	Insert into udtPMILaserTrayMap(TrayID,UnitID,StationID,EmployeeID,USN)
	Select 
		@TrayID, UnitID,@StationID, @EmployeeID , UnitSN
	from 
		@ffUnit 
	Order By 
		cID
	if @@error > 0 
	begin  
		if @@trancount = 1 rollback else commit  
		return 140308503 --failed to update udtPMILaserTrayMap table  
	end

Commit tran

return 0


GO

