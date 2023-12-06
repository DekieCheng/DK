
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*
***************************************************************************
Author			: Dekie Cheng
Creation Date	: 28th Jun, 2023
Explanation		: 

--Get EPPS Planning PO --call UDP  by LineName
--炉前的工位多久没扫描最后一次生产的工单
--炉前的工位最后生产的PO Qty是否和炉前扫入的产品数量一直，如果一致，表明此PO已经做完

Script Version	: 1.0
Parameter	

@xmlSample =N'<DT>
				<D N="F"><![CDATA[1]]></D>
			</DT>' 
@EPPSPOInfo =N'
	<POInfo>
	  <PackageInfo LineName="B17-SMT-3AS08A" PlanDate="7/7/2023 12:00:00 AM" PlanQty="2940" WO="DYP002008-BOT" PN="DYGZ-240004-01-M-BOT" WOQty="2940" StartTime="7/7/2023 8:00:00 AM" EndTime="7/7/2023 11:12:00 AM" />
	  <PackageInfo LineName="B17-SMT-3AS08A" PlanDate="7/7/2023 12:00:00 AM" PlanQty="2940" WO="DYP002008-TOP" PN="DYGZ-240004-01-M-TOP" WOQty="2940" StartTime="7/7/2023 11:00:00 AM" EndTime="7/7/2023 1:03:00 PM" />
	  <PackageInfo LineName="B17-SMT-3AS08A" PlanDate="7/7/2023 12:00:00 AM" PlanQty="3396" WO="DYP002009-BOT" PN="DYGZ-517226-01-M-BOT" WOQty="3396" StartTime="7/7/2023 1:00:00 PM" EndTime="7/7/2023 2:53:24 PM" />
	  <PackageInfo LineName="B17-SMT-3AS08A" PlanDate="7/7/2023 12:00:00 AM" PlanQty="3396" WO="DYP002009-TOP" PN="DYGZ-517226-01-M-TOP" WOQty="3396" StartTime="7/7/2023 3:00:00 PM" EndTime="7/7/2023 6:06:36 PM" />
	  <PackageInfo LineName="B17-SMT-3AS08A" PlanDate="7/7/2023 12:00:00 AM" PlanQty="3990" WO="DYP002010-BOT" PN="DYGZ-517227-01-M-BOT" WOQty="3990" StartTime="7/7/2023 6:00:00 PM" EndTime="7/7/2023 10:57:00 PM" />
	  <PackageInfo LineName="B17-SMT-3AS08A" PlanDate="7/7/2023 12:00:00 AM" PlanQty="3990" WO="DYP002010-TOP" PN="DYGZ-517227-01-M-TOP" WOQty="3990" StartTime="7/7/2023 11:00:00 PM" EndTime="7/8/2023 4:03:00 AM" />
	</POInfo>'

Output			:		
Is used			:  	
ModifiedHistory	: 
**************************************************************************
*/
CREATE OR ALTER procedure [dbo].[udpLNFFCoolDownDecision]
	@xmlExtraData nvarchar(max)=null
AS
SET NOCOUNT ON

Set @xmlExtraData = REPLACE(@xmlExtraData,N'<?xml version="1.0" encoding="ISO-8859-1"?>','')
Set @xmlExtraData = REPLACE(@xmlExtraData,N'<?xml version="1.0" encoding="UTF-16"?>','')

Declare @tblEPPSPOInfo table (
	ID int identity(1,1),
	LName varchar(200),
	PlanDate datetime,
	PQty int,
	WO varchar(200),
	PN varchar(200),
	WOQty int,
	StartTime datetime,
	EndTime datetime
)

Declare 
	 @Systime datetime
	,@LineName varchar(200)
	,@LineID int

	,@StationNameBefore varchar(200)
	,@StationBeforeID int
	,@POIDBefore int
	,@PONumberBefore varchar(200)
	,@QtyBefore int

	,@StationNameAfter varchar(200)
	,@StationAfterID int
	,@POIDAfter int
	,@PONumberAfter varchar(200)
	,@QtyAfter int

	,@PlanPOQty int
	,@EPPSPOInfo varchar(max)
	,@xmlEPPSPOInfo xml
	,@DelayPlanTime int
Set @Systime = GETDATE()

exec dbo.udpLNXMLExtraData @xmlExtraData, 'LineName', @LineName output
exec dbo.udpLNXMLExtraData @xmlExtraData, 'StationNameBefore', @StationNameBefore output
exec dbo.udpLNXMLExtraData @xmlExtraData, 'StationNameAfter', @StationNameAfter output
exec dbo.udpLNXMLExtraData @xmlExtraData, 'EPPSPOInfo', @EPPSPOInfo output
exec dbo.udpLNXMLExtraData @xmlExtraData, 'DelayPlanTime', @DelayPlanTime output

Set @xmlEPPSPOInfo = cast(@EPPSPOInfo as xml)
Insert into @tblEPPSPOInfo (LName,PlanDate,PQty,WO,PN,WOQty,StartTime,EndTime )
Select 
	t.col.value('(@LineName)[1]','varchar(200)') as LineName
	,t.col.value('(@PlanDate)[1]','varchar(200)') as PlanDate
	,t.col.value('(@PlanQty)[1]','varchar(200)') as PlanQty
	,t.col.value('(@WO)[1]','varchar(200)') as WO
	,t.col.value('(@PN)[1]','varchar(200)') as PN
	,t.col.value('(@WOQty)[1]','varchar(200)') as WOQty
	,t.col.value('(@StartTime)[1]','varchar(200)') as StartTime
	,t.col.value('(@EndTime)[1]','varchar(200)') as EndTime
from 
	@xmlEPPSPOInfo.nodes('POInfo/PO') as t(col)
Set @PlanPOQty = @@ROWCOUNT 

Select @StationBeforeID= ID from  ffStation With(nolock) Where Description = @StationNameBefore 
Select @StationAfterID= ID from  ffStation With(nolock) Where Description = @StationNameAfter 

--检查炉前工位PO Qty
Select @POIDBefore =ProductionOrderID from ffHistory  with(nolock) Where ID = (Select Max(ID) as MaxID from ffHistory with(nolock) Where StationID = @StationBeforeID )
Select @QtyBefore = count(Distinct UnitID ) from ffHistory  with(nolock) Where StationID = @StationBeforeID And ProductionOrderID = @POIDBefore
Select @PONumberBefore = ProductionOrderNumber from  ffProductionOrder With(nolock) Where ID = @POIDBefore

Select @POIDAfter =ProductionOrderID from ffHistory  with(nolock) Where ID = (Select Max(ID) as MaxID from ffHistory with(nolock) Where StationID = @StationAfterID )
Select @QtyAfter = count(Distinct UnitID ) from ffHistory  with(nolock) Where StationID = @StationAfterID And ProductionOrderID = @POIDAfter
Select @PONumberAfter = ProductionOrderNumber from  ffProductionOrder With(nolock) Where ID = @POIDAfter

if (@POIDBefore <> @POIDAfter)
Begin
	Select 0 as IsCoolDown
	, N'当前时刻不符合Cool Down要求, 炉前和炉后不是生产同一个工单, 请继续生产.' as Msg
	return 0
End
if (@QtyBefore<>@QtyAfter)
Begin
	Select 0 as IsCoolDown
	, N'当前时刻不符合Cool Down要求, 炉前扫入产品和炉后产品数量不一致, 请继续生产.' as Msg
	return 0
End

if (@PlanPOQty=0)
Begin
	Select 0 as IsCoolDown
	, N'当前时刻不符合Cool Down要求, 炉前和炉后不是生产同一个工单, 请继续生产.' as Msg
	return 0
End

if exists(Select top 1 1 from @tblEPPSPOInfo Where StartTime >= DATEADD(mm, @DelayPlanTime, @Systime))
Begin
	Select 0 as IsCoolDown
	, N'根据EPPS计划，未来'+cast(@DelayPlanTime as nvarchar(10))+N'分钟内会有生产, 请继续生产.' as Msg
	return 0
End

Select 0 as IsCoolDown
, N'当前时刻不符合Cool Down要求, 请继续生产.' as Msg

--Select 1 as IsCoolDown --1: perform cool down, else not trigger cool down
--	, N'当前时刻满足Cool Down要求, 系统自动触发降温操作.' as Msg --Report to client on the latest message
