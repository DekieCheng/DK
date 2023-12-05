
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*
***************************************************************************
Author			: Dekie Cheng
Creation Date	: 4th Jul, 2023
Explanation		:  
Script Version	: 1.0
Parameter	:
Output			:		
Is used			:  	
ModifiedHistory		:	
**************************************************************************
*/

CREATE OR ALTER procedure [dbo].[udpLNLoadElectricityByCategoryName]
	@CategoryName nvarchar(200) = null
	,@IsGroupByMeterName bit = 0
AS
SET NOCOUNT ON

If (@CategoryName is null)
Begin
	Select
		GroupName,
		CurrDay, 
		sum(TPAEConsumption) as TPAEConsumption  
	from 
		vwLNDashboardOfElectricity 
	where 
		CurrDay > DATEADD(day, -7, GETDATE()) 
	group by 
		Groupname,CurrDay
	Order By
		CurrDay, Groupname
End
else
Begin
	If (@IsGroupByMeterName=1)
		Select
			MeterName,
			CurrDay, 
			sum(TPAEConsumption) as TPAEConsumption  
		from 
			vwLNDashboardOfElectricity 
		where 
			Groupname =@CategoryName  And CurrDay > DATEADD(day, -7, GETDATE()) 
		group by 
			MeterName, currDay
		Order By
			CurrDay, MeterName
	else
		Select
			GroupName,
			CurrDay, 
			sum(TPAEConsumption) as TPAEConsumption  
		from 
			vwLNDashboardOfElectricity 
		where 
			Groupname =@CategoryName  And CurrDay > DATEADD(day, -7, GETDATE()) 
		group by 
			Groupname, currDay
		Order By
			CurrDay, Groupname
End

return 0


GO

