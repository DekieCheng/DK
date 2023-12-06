
/****** Object:  View [dbo].[vwLNDashboardOfWater]    Script Date: 7/4/2023 2:41:12 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER VIEW [dbo].[vwLNDashboardOfWater]
AS
SELECT       
	a.GroupCategoryName, 
	b.GroupName, 
	b.Sort, 
	ss.HostIP, 
	em.MeterAddr, 
	em.MeterName, 
	em.MeterNo, 
	m.CurrDay, 

	md.PreStere, 
	md.CurrStere, 
	md.WaterConsumption, 
	md.CreationTime, 
	md.HH, CASE WHEN md.hh >= 8 AND
			 md.hh <= 23 THEN 'Day' ELSE 'Night' END AS [Shift]
	, em.ID as WaterMeterID
	, ss.ID as SerialPortServerID
	, m.ID as WaterMeterMasterID
	, md.ID as WaterMeterDetailID
FROM
	dbo.udtLNMeterGroupCategory AS a INNER JOIN
		dbo.udtLNMeterGroup AS b ON a.ID = b.MeterGroupCategoryID LEFT OUTER JOIN
		dbo.udtLNMeterMap AS c ON c.MeterGroupID = b.ID LEFT OUTER JOIN
		dbo.udtLNWaterMeter AS em ON c.MeterID = em.ID LEFT OUTER JOIN
		dbo.udtLNSerialPortServer AS ss ON em.SerialPortServerID = ss.ID LEFT OUTER JOIN
		dbo.udtLNWaterMeterMaster AS m ON m.WaterMeterID = em.ID INNER JOIN
		dbo.udtLNWaterMeterDetail AS md ON md.WaterMeterMasterID = m.ID
WHERE 
	(a.GroupCategoryName = N'水')
GO