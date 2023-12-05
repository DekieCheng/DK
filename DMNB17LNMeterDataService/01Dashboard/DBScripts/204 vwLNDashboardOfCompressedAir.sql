
/****** Object:  View [dbo].[vwLNDashboardOfCompressedAir]    Script Date: 7/4/2023 2:41:12 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER VIEW [dbo].[vwLNDashboardOfCompressedAir]
AS

Select  
	a.GroupCategoryName, 
	B.GroupName, 
	b.Sort,
	ss.HostIP, 
	em.MeterAddr,
	em.MeterName,
	em.MeterNo

	,mm.[CurrDay]
	,md.Instantflow as [瞬时流量(m3/h)]
	,md.Instantflowrate as [瞬时流速(m/s)]
	,md.PreAccumulatedflow as [前一小时最后读数累积流量(m3)]
	,md.CurrentAccumulatedflow as [本小时读数累积流量(m3)]
	,md.CompressedAirConsumption  as [本小时消耗流量(m3)]
	,md.HH 
	,md.CreationTime
	,CASE WHEN md.hh >= 8 AND
			 md.hh <= 23 THEN 'Day' ELSE 'Night' END AS [Shift]
	, em.ID as CompressedAirFlowMeterID
	, ss.ID as SerialPortServerID
	, mm.ID as CompressedAirFlowMeterMasterID
	, md.ID as CompressedAirFlowMeterDetailID
from udtLNMeterGroupCategory a inner join udtLNMeterGroup b on a.ID = b.MeterGroupCategoryID
	inner join udtLNMeterMap c on c.MeterGroupID = b.ID
	inner join udtLNCompressedAirFlowMeter em on c.MeterID = em.ID
	inner join udtLNSerialPortServer ss on em.SerialPortServerID = ss.ID
	inner join udtLNCompressedAirFlowMeterMaster mm on mm.CompressedAirFlowMeterID = em.ID 
	inner join udtLNCompressedAirFlowMeterDetail md on md.CompressedAirFlowMeterMasterID = mm.ID
Where
	a.GroupCategoryName=N'气'
GO