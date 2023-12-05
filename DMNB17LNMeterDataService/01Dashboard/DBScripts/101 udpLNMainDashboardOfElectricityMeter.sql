/*
1. B17厂房总用电量
2. B17 PCBA = B17厂房总用电量  - (B17 Mech+B10 Mech)
3. Mech = B17 Mech用电  + (B10 Mech用电 + 压缩空气系统用电 (by hour) * 38%+ 空调系统用电(by hour) * 40%）

3. Mech = B17 Mech用电  + B10 Mech用电
(*B10 Mech用电 = B10 Mech用电 + 压缩空气系统用电 (by hour) * 压缩空气流量比例 + 空调系统用电(by hour) * 空调能量比例 )
--压缩空气流量比例(by hour)	= B10流量计by hour  / （B10+B17）流量计by hour 
--空调能量比例 (by hour)		= B10冷冻水（生产+空调)分摊比例 + B17 1F注塑分摊比例
	B10冷冻水（生产+空调)分摊比例 = B10冷冻水(生产+空调)(by hour)  / B17厂房总用空调能量计(by hour)
	B17 1F注塑分摊比例 = 新加建办公楼空调/B17总的空调能量 * B17 1F注塑/新加建办公楼空调

上个月比例：	空调能量比例 = 40%
			压缩空气流量比例 = 38%
*/


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

CREATE OR ALTER procedure [dbo].[udpLNMainDashboardOfElectricityMeter]

AS
SET NOCOUNT ON

--B17 每日用电
Declare @tblB17TotalTPAE table(CurrDay varchar(200), B17TotalTPAE numeric(20,4))
Insert into @tblB17TotalTPAE(CurrDay, B17TotalTPAE)
select  
	currDay, 
	sum(TPAEConsumption) as TPAEConsumption  
from 
	vwLNDashboardOfElectricity 
where 
	Groupname =N'B17厂房总用电量'  And CurrDay > DATEADD(day, -7, GETDATE()) 
group by 
	Groupname, currDay

--B17 Mech用电  + (B10 Mech用电 + 压缩空气系统用电 (by hour) * 38%+ 空调系统用电(by hour) * 40%）

--Mech (B17 + B10 Mech)每日用电
Declare @tblB17MechTPAE table(xCurrDay varchar(200), MechTPAE numeric(20,4))
Insert into @tblB17MechTPAE(xCurrDay, MechTPAE)
select 
	x.currDay, 
	sum(x.TPAEConsumption)
from 
	(
		select  currDay,  TPAEConsumption  as TPAEConsumption from vwLNDashboardOfElectricity 
		where GroupName in (N'B17 Mech用电')  and CurrDay > DATEADD(day, -7, GETDATE()) 
		union all
		select  currDay,  TPAEConsumption  as TPAEConsumption from vwLNDashboardOfElectricity 
		where GroupName in (N'B10 Mech用电')  and CurrDay > DATEADD(day, -7, GETDATE()) 

		union all
		select  currDay, TPAEConsumption  * 0.38 as TPAEConsumption from vwLNDashboardOfElectricity 
		where GroupName in (N'压缩空气系统用电')  and CurrDay > DATEADD(day, -7, GETDATE()) 

		union all
		select  currDay,  TPAEConsumption  * 0.40 as TPAEConsumption from vwLNDashboardOfElectricity 
		where GroupName in (N'空调系统用电')  and CurrDay > DATEADD(day, -7, GETDATE()) 
	) x
group by  
	x.currDay

--B17 PCBA 每日总用电 = B17总用电-Mech总用电
Declare @tblB17PCBATPAE table(xCurrDay varchar(200),PCBATPAE numeric(20,4))
Insert into @tblB17PCBATPAE(xCurrDay, PCBATPAE)
Select 
	a.CurrDay, 
	(a.B17TotalTPAE-b.MechTPAE) as PCBATPAE
from @tblB17TotalTPAE a inner join @tblB17MechTPAE b on a.CurrDay=b.xCurrDay 

Select 
	a.CurrDay,
	cast(Round(a.B17TotalTPAE/10000.00,3) as numeric(20,3))	as B17TotalTPAE,--[B17总用电量], --B17总用电量
	cast(Round(b.MechTPAE/10000.00,3) as numeric(20,3))		as MechTPAE,--[Mech总用电量],--Mech总用电量
	cast(Round(c.PCBATPAE/10000.00,3) as numeric(20,3))		as PCBATPAE--[PCBA总用电量] --PCBA总用电量
from 
	@tblB17TotalTPAE a inner join @tblB17MechTPAE b on a.CurrDay=b.xCurrDay 
		inner join @tblB17PCBATPAE c on a.CurrDay =c.xCurrDay
Order By
	a.CurrDay

return 0


GO

