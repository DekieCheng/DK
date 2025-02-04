SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
***************************************************************************
Author			: Dekie Cheng
Creation Date	: 5th July, 2023
Explanation		: 
Script Version	: 1.0
Parameter	

@xmlSample =N'' 
Output			:		
Is used			:  	
ModifiedHistory	: 
**************************************************************************
*/
Create Or ALTER Proc [dbo].[udpLNEPPSToGetProductionPlan]
	@LineName varchar(100),
	@msg NVarchar(600) =null output
as 

declare @currentdate datetime,@hours int,@plandate date
set @currentdate=getdate()
select @hours=DATEPART(hh,@currentdate)

if @hours<8
	set @plandate=dateadd(day,-1,getdate())
else
	set @plandate=getdate()

select 
	a.Line_Name,
	a.Plan_Date,
	a.Plan_Qty,
	b.WO,
	b.PN,
	b.WOQty,
	b.StartTime,
	b.EndTime
from 
	UDT_FLEX_CapacityAnalysis_Prod_Schedule a inner join 
	UDT_FLEX_CapacityAnalysis_Prod_ScheduleDetail b on a.PSID=b.PSID 
where 
	a.Line_Name=@LineName 
	And Plan_Date=@plandate

return  0