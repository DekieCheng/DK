truncate table udtLNMeterGroupCategory
Insert into udtLNMeterGroupCategory([MeterCategoryID],[GroupCategoryName])
	Values
	( 1, N'水'),
	( 2, N'电'),
	( 3, N'空调'),
	( 4, N'气');
GO

truncate table udtLNMeterGroup

Declare @MeterGroupCategoryID int
Select @MeterGroupCategoryID = ID from udtLNMeterGroupCategory Where [GroupCategoryName] =N'水'
Insert into udtLNMeterGroup ([MeterGroupCategoryID], [GroupName],[GroupDesc], [Sort])
Values (@MeterGroupCategoryID,  N'B17厂房总用水量', N'B17厂房总用水量',1);
Go

Declare @MeterGroupCategoryID int
Select @MeterGroupCategoryID = ID from udtLNMeterCategory Where CategoryName ='Electricity'
Insert into udtLNMeterGroup ([MeterGroupCategoryID], [GroupName],[GroupDesc], [Sort])
Values (@MeterGroupCategoryID,  N'B17厂房总用电量', N'B17厂房总用电量',1),
(@MeterGroupCategoryID,  N'B17太阳能用电量',N'B17太阳能用电量',2),
(@MeterGroupCategoryID,  N'B10 Mech用电',N'B10 Mech用电',3),
(@MeterGroupCategoryID,  N'B17 Mech用电',N'B17 Mech用电',4),
(@MeterGroupCategoryID,  N'空调系统用电',N'空调系统用电',5),
(@MeterGroupCategoryID,  N'压缩空气系统用电',N'压缩空气系统用电',6),
(@MeterGroupCategoryID,  N'楼顶排风&其它用电',N'楼顶排风&其它用电',7),
(@MeterGroupCategoryID,  N'B17 PCBA照明用电',N'B17 PCBA照明用电',8),
(@MeterGroupCategoryID,  N'B17一楼原材料仓用电',N'B17一楼原材料仓用电',9),
(@MeterGroupCategoryID,  N'B17一楼PCBA车间用电',N'B17一楼PCBA车间用电',10),
(@MeterGroupCategoryID,  N'B17二楼车间用电',N'B17二楼车间用电',11),
(@MeterGroupCategoryID,  N'B17三楼A车间用电',N'B17三楼A车间用电',12),
(@MeterGroupCategoryID,  N'B17三楼B车间用电',N'B17三楼B车间用电',13),
(@MeterGroupCategoryID,  N'B17四楼A车间用电',N'B17四楼A车间用电',14),
(@MeterGroupCategoryID,  N'B17四楼B车间用电',N'B17四楼B车间用电',15),
(@MeterGroupCategoryID,  N'B17写字楼用电',N'B17写字楼用电',16),
(@MeterGroupCategoryID,  N'By Machine (按机器统计)',N'By Machine (按机器统计)',17);
Go

Declare @MeterGroupCategoryID int
Select @MeterGroupCategoryID = ID from udtLNMeterCategory Where CategoryName ='ChilledWaterFlow'
Insert into udtLNMeterGroup ([MeterGroupCategoryID], [GroupName],[GroupDesc], [Sort])
Values (@MeterGroupCategoryID,  N'B17厂房总用空调能量计', N'B17厂房总用空调能量计',1);
Go
Declare @MeterGroupCategoryID int

Select @MeterGroupCategoryID = ID from udtLNMeterCategory Where CategoryName ='CompressedAirFlow'
Insert into udtLNMeterGroup ([MeterGroupCategoryID], [GroupName],[GroupDesc], [Sort])
Values (@MeterGroupCategoryID,  N'B17厂房总用压缩空气流量', N'B17厂房总用压缩空气流量',1);
GO

truncate table udtLNMeterMap

Declare @udtLNMeterMap table (xGroupName nvarchar(200), xMeterAddr nvarchar(200), xMeterGroupID int, xMeterID int)
Insert into @udtLNMeterMap(xGroupName, xMeterAddr) Values 
(N'B17厂房总用电量','11')
,(N'B17厂房总用电量','18')
,(N'B17厂房总用电量','10')
,(N'B17厂房总用电量','17')
,(N'B17厂房总用电量','01')
,(N'B17厂房总用电量','02')
,(N'B17厂房总用电量','03')
,(N'B17厂房总用电量','04')
,(N'B17厂房总用电量','05')
,(N'B17厂房总用电量','06')
,(N'B17厂房总用电量','07')
,(N'B17厂房总用电量','08')
,(N'B17厂房总用电量','09')
,(N'B17太阳能用电量','18')
,(N'B17太阳能用电量','17')
,(N'B10 Mech用电','47')
,(N'B10 Mech用电','57')
,(N'B10 Mech用电','45')
,(N'B10 Mech用电','63')
,(N'B10 Mech用电','41')
,(N'B10 Mech用电','42')
,(N'B17 Mech用电','43')
,(N'B17 Mech用电','46')
,(N'B17 Mech用电','52')
,(N'B17 Mech用电','54')
,(N'B17 Mech用电','55')
,(N'B17 Mech用电','37')
,(N'空调系统用电','12')
,(N'空调系统用电','01')
,(N'空调系统用电','02')
,(N'空调系统用电','03')
,(N'空调系统用电','04')
,(N'空调系统用电','15')
,(N'空调系统用电','16')
,(N'压缩空气系统用电','05')
,(N'压缩空气系统用电','06')
,(N'压缩空气系统用电','07')
,(N'压缩空气系统用电','13')
,(N'压缩空气系统用电','14')
,(N'楼顶排风&其它用电','19')
,(N'楼顶排风&其它用电','24')
,(N'楼顶排风&其它用电','38')
,(N'楼顶排风&其它用电','99')
,(N'楼顶排风&其它用电','39')
,(N'楼顶排风&其它用电','40')
,(N'B17 PCBA照明用电','59')
,(N'B17 PCBA照明用电','60')
,(N'B17 PCBA照明用电','61')
,(N'B17 PCBA照明用电','22')
,(N'B17 PCBA照明用电','23')
,(N'B17 PCBA照明用电','27')
,(N'B17 PCBA照明用电','28')
,(N'B17 PCBA照明用电','31')
,(N'B17 PCBA照明用电','32')
,(N'B17一楼原材料仓用电','59')
,(N'B17一楼原材料仓用电','44')
,(N'B17一楼PCBA车间用电','48')
,(N'B17一楼PCBA车间用电','49')
,(N'B17一楼PCBA车间用电','50')
,(N'B17一楼PCBA车间用电','58')
,(N'B17一楼PCBA车间用电','60')
,(N'B17一楼PCBA车间用电','64')
,(N'B17一楼PCBA车间用电','65')
,(N'B17二楼车间用电','51')
,(N'B17二楼车间用电','53')
,(N'B17二楼车间用电','56')
,(N'B17二楼车间用电','61')
,(N'B17二楼车间用电','62')
,(N'B17二楼车间用电','66')
,(N'B17三楼A车间用电','23')
,(N'B17三楼A车间用电','25')
,(N'B17三楼A车间用电','26')
,(N'B17三楼A车间用电','29')
,(N'B17三楼B车间用电','20')
,(N'B17三楼B车间用电','21')
,(N'B17三楼B车间用电','22')
,(N'B17四楼A车间用电','28')
,(N'B17四楼A车间用电','30')
,(N'B17四楼A车间用电','35')
,(N'B17四楼A车间用电','36')
,(N'B17四楼B车间用电','27')
,(N'B17四楼B车间用电','33')
,(N'B17四楼B车间用电','34')
,(N'B17写字楼用电','31')
,(N'B17写字楼用电','32')
,(N'By Machine (按机器统计)','70')
,(N'By Machine (按机器统计)','71')
,(N'By Machine (按机器统计)','72')
,(N'By Machine (按机器统计)','73')
,(N'By Machine (按机器统计)','74')
,(N'By Machine (按机器统计)','75')
,(N'By Machine (按机器统计)','76')
,(N'By Machine (按机器统计)','77')
,(N'By Machine (按机器统计)','78')
,(N'By Machine (按机器统计)','79')

Update @udtLNMeterMap Set xMeterGroupID  = g.ID from udtLNMeterGroup g where xGroupName  = g.[GroupName]
Update @udtLNMeterMap Set xMeterID = e.ID from udtLNElectricityMeter e where xMeterAddr = e.MeterAddr
Insert into udtLNMeterMap([MeterGroupID],[MeterID])
select xMeterGroupID, xMeterID from @udtLNMeterMap 

GO

Declare @udtLNMeterMap table (xGroupName nvarchar(200), xMeterAddr nvarchar(200), xMeterGroupID int, xMeterID int)
Insert into @udtLNMeterMap(xGroupName, xMeterAddr) Values 
(N'B17厂房总用空调能量计','90')
,(N'B17厂房总用空调能量计','91')
,(N'B17厂房总用空调能量计','92')
,(N'B17厂房总用空调能量计','93')
,(N'B17厂房总用空调能量计','94')
,(N'B17厂房总用空调能量计','95');
Update @udtLNMeterMap Set xMeterGroupID  = g.ID from udtLNMeterGroup g where xGroupName  = g.[GroupName]
Update @udtLNMeterMap Set xMeterID = e.ID from udtLNChilledWaterFlowMeter e where xMeterAddr = e.MeterAddr
Insert into udtLNMeterMap([MeterGroupID],[MeterID])
select xMeterGroupID, xMeterID from @udtLNMeterMap 
GO

Declare @udtLNMeterMap table (xGroupName nvarchar(200), xMeterAddr nvarchar(200), xMeterGroupID int, xMeterID int)
Insert into @udtLNMeterMap(xGroupName, xMeterAddr) Values 
(N'B17厂房总用压缩空气流量','01')
,(N'B17厂房总用压缩空气流量','02');
Update @udtLNMeterMap Set xMeterGroupID  = g.ID from udtLNMeterGroup g where xGroupName  = g.[GroupName]
Update @udtLNMeterMap Set xMeterID = e.ID from udtLNCompressedAirFlowMeter e where xMeterAddr = e.MeterAddr
Insert into udtLNMeterMap([MeterGroupID],[MeterID])
select xMeterGroupID, xMeterID from @udtLNMeterMap 
GO
