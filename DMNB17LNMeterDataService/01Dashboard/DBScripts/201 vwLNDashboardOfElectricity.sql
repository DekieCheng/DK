
/****** Object:  View [dbo].[vwLNDashboardOfElectricity]    Script Date: 7/4/2023 2:41:12 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER VIEW [dbo].[vwLNDashboardOfElectricity]
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

	md.TAP, 
	md.PreTPAE, 
	md.CurrTPAE, 
	md.TPAEConsumption, 
	md.CreationTime, 
	md.HH, CASE WHEN md.hh >= 8 AND
			 md.hh <= 23 THEN 'Day' ELSE 'Night' END AS [Shift],
	em.ID as ElectricityMeterID,
	ss.ID as SerialPortServerID,
	m.ID as ElectricityMeterMasterID,
	md.ID as ElectricityMeterDetailID
FROM
	dbo.udtLNMeterGroupCategory AS a INNER JOIN
		dbo.udtLNMeterGroup AS b ON a.ID = b.MeterGroupCategoryID LEFT OUTER JOIN
		dbo.udtLNMeterMap AS c ON c.MeterGroupID = b.ID LEFT OUTER JOIN
		dbo.udtLNElectricityMeter AS em ON c.MeterID = em.ID LEFT OUTER JOIN
		dbo.udtLNSerialPortServer AS ss ON em.SerialPortServerID = ss.ID LEFT OUTER JOIN
		dbo.udtLNElectricityMeterMaster AS m ON m.ElectricityMeterID = em.ID INNER JOIN
		dbo.udtLNElectricityMeterDetail AS md ON md.ElectricityMeterMasterID = m.ID
WHERE 
	(a.GroupCategoryName = N'电')
GO