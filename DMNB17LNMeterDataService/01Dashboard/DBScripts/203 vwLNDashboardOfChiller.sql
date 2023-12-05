
/****** Object:  View [dbo].[vwLNDashboardOfChiller]    Script Date: 7/4/2023 2:41:12 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER VIEW [dbo].[vwLNDashboardOfChiller]
AS

Select  
	a.GroupCategoryName, 
	B.GroupName, 
	b.Sort,
	ss.HostIP, 
	em.MeterAddr,
	em.MeterName,
	em.MeterNo,
	wm.CurrDay,

	wmd.Instantflow				as [瞬时流量(m3/h)]
	,wmd.Instantaneousheat			as [瞬时热流量(GJ/h)]
	,wmd.Instantflowrate			as [瞬时流速(m/s)]
	,wmd.Accumulatedflow			as [正累积流量(m3)]
	,wmd.PreAccumulatedheat			as [前一小时正累积热量(GJ)]
	,wmd.CurrentAccumulatedheat		as [本次读取正累积热量(GJ)]
	,wmd.AccumulatedheatConsumption	as [本小时消耗累积热量]
	,wmd.WK
	,wmd.hh
	,wmd.CreationTime
	,CASE WHEN wmd.hh >= 8 AND
			 wmd.hh <= 23 THEN 'Day' ELSE 'Night' END AS [Shift]
	, em.ID as ChilledWaterFlowMeterID
	, ss.ID as SerialPortServerID
	, wm.ID as ChilledWaterFlowMeterMasterID
	, wmd.ID as ChilledWaterFlowMeterDetailID
from 
	udtLNMeterGroupCategory a inner join udtLNMeterGroup b on a.ID = b.MeterGroupCategoryID
		inner join udtLNMeterMap c on c.MeterGroupID = b.ID
		inner join udtLNChilledWaterFlowMeter em on c.MeterID = em.ID
		inner join udtLNSerialPortServer ss on em.SerialPortServerID = ss.ID
		inner join udtLNChilledWaterFlowMeterMaster wm on wm.ChilledWaterFlowMeterID = em.ID
		inner join udtLNChilledWaterFlowMeterDetail wmd on wmd.ChilledWaterFlowMeterMasterID = wm.ID
Where
	a.GroupCategoryName=N'空调' 

GO