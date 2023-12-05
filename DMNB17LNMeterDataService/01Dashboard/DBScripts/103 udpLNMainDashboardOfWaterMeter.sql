
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*
***************************************************************************
Author			: Dekie Cheng
Creation Date	: 6th Jul, 2023
Explanation		:  
Script Version	: 1.0
Parameter	:
Output			:		
Is used			:  	
ModifiedHistory		:	
**************************************************************************
*/

CREATE OR ALTER procedure [dbo].[udpLNMainDashboardOfWaterMeter]

AS
SET NOCOUNT ON
Declare @currDay varchar(200)

Select  
	GroupCategoryName,
	GroupName,
	CurrDay,
	Sum(WaterConsumption)
from
	vwLNDashboardOfWater
Where
	 GroupCategoryName=N'水' 
	 And GroupName=N'B17厂房总用水量' 
	 And CurrDay > DATEADD(day, -7, GETDATE()) 
Group by
	GroupCategoryName,
	GroupName,
	CurrDay
return 0


GO

