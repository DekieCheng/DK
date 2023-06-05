truncate table udtLNMeterCategory
Insert into udtLNMeterCategory([CategoryName],[CategoryDesc],[Command],[CycleTime],[UDPToSaveData],[UDPToSaveRuntimeLog],[AssemblyName],[ClassName])
	Values
	( 'Water', N'Water Meter','03 20 01 00 02',3600,'udpLNWaterMeterImportProxy','udpLNSaveRuntimeLogProxy',null,null),
	( 'Electricity',N'Electricity Meter','03 20 01 00 02',3600,'udpLNElectricityMeterImportProxy','udpLNSaveRuntimeLogProxy','LNElectricityMeterParser','ElectricityDataPaser'),
	( 'WaterFlow',N'Air-conditioning chilled water flow meter','03 20 01 00 02',3600,'udpLNAirConditionMeterImportProxy','udpLNSaveRuntimeLogProxy',null,null),
	( 'AirFlow', N'Compressed air flow meter','03 20 01 00 02',3600,'udpLNCompressedAirMeterImportProxy','udpLNSaveRuntimeLogProxy',null,null);
GO

truncate table udtLNSerialPortServer
Insert into udtLNSerialPortServer(MeterCategoryID, HostIP, Port,CreationTime)
	Values
	( 2,'10.200.7.167',5300, getdate()),
	( 2,'10.200.7.182',5300, getdate()),
	( 2,'10.200.7.183',5300, getdate()),
	( 2,'10.200.7.184',5300, getdate()),
	( 2,'10.200.7.185',5300, getdate()),
	( 2,'10.200.7.186',5300, getdate()),
	( 2,'10.200.7.187',5300, getdate()),
	( 2,'10.200.7.188',5300, getdate());
GO

truncate table udtLNElectricityMeter
Insert into udtLNElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[MeterLevel],[AreaLocation],[AreaFunction]) Values (3, '11', N'3#主变','3P1','1',N'',N'') 
Insert into udtLNElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[MeterLevel],[AreaLocation],[AreaFunction]) Values (3, '43', N'1F 注塑B动力电柜','2P8-1','2',N'B17-Mceh',N'') 
Insert into udtLNElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[MeterLevel],[AreaLocation],[AreaFunction]) Values (3, '46', N'1F 注塑B动力电柜','2P9-2','2',N'B17-Mceh',N'') 
Insert into udtLNElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[MeterLevel],[AreaLocation],[AreaFunction]) Values (3, '47', N'B10冷冻水加压泵','3P5-2','2',N'B10-Mech',N'') 
Insert into udtLNElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[MeterLevel],[AreaLocation],[AreaFunction]) Values (3, '48', N'1F C车间空调电柜','3P5-3','2',N'1F车间',N'') 
Insert into udtLNElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[MeterLevel],[AreaLocation],[AreaFunction]) Values (3, '49', N'1F C车间动力电柜','3P6-2','2',N'1F车间',N'') 
Insert into udtLNElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[MeterLevel],[AreaLocation],[AreaFunction]) Values (3, '50', N'PCBA 1、2F空调、注塑A排风','3P8-2','2',N'1F车间',N'') 
Insert into udtLNElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[MeterLevel],[AreaLocation],[AreaFunction]) Values (3, '51', N'2F QA lab动力电柜','3P9-3','2',N'2F车间',N'') 
Insert into udtLNElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[MeterLevel],[AreaLocation],[AreaFunction]) Values (3, '52', N'1F 注塑B照明','3P9-4','2',N'B17-Mceh',N'') 
Insert into udtLNElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[MeterLevel],[AreaLocation],[AreaFunction]) Values (3, '53', N'2F车间生产线','3P6-1','2',N'2F车间',N'') 
Insert into udtLNElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[MeterLevel],[AreaLocation],[AreaFunction]) Values (3, '54', N'1F 注塑B动力电柜','3P10','2',N'B17-Mceh',N'') 
Insert into udtLNElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[MeterLevel],[AreaLocation],[AreaFunction]) Values (3, '55', N'1F 注塑B动力电柜','3P11','2',N'B17-Mceh',N'') 
Insert into udtLNElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[MeterLevel],[AreaLocation],[AreaFunction]) Values (3, '56', N'2F QA lab动力电柜','3P12','2',N'2F车间',N'') 
Insert into udtLNElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[MeterLevel],[AreaLocation],[AreaFunction]) Values (3, '57', N'B10 新喷油车间配电','3P13','2',N'B10-Mech',N'') 
Insert into udtLNElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[MeterLevel],[AreaLocation],[AreaFunction]) Values (3, '18', N'太阳能（2）','AT9','1',N'',N'') 
Insert into udtLNElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[MeterLevel],[AreaLocation],[AreaFunction]) Values (4, '10', N'1#主变','1P1','1',N'',N'') 
Insert into udtLNElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[MeterLevel],[AreaLocation],[AreaFunction]) Values (4, '58', N'1F C车间动力电柜','1P7-1','2',N'1F车间',N'') 
Insert into udtLNElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[MeterLevel],[AreaLocation],[AreaFunction]) Values (4, '59', N'1F 注塑A区及应急照明','1P8-2','2',N'1楼货仓',N'') 
Insert into udtLNElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[MeterLevel],[AreaLocation],[AreaFunction]) Values (4, '44', N'1F 注塑A动力电柜','2P8-2','2',N'1楼货仓',N'') 
Insert into udtLNElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[MeterLevel],[AreaLocation],[AreaFunction]) Values (4, '45', N'B10 新喷油DK14配电','2P9-1','2',N'B10-Mech',N'') 
Insert into udtLNElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[MeterLevel],[AreaLocation],[AreaFunction]) Values (4, '60', N'1F C车间车间照明','1P8-4','2',N'1F车间',N'照明') 
Insert into udtLNElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[MeterLevel],[AreaLocation],[AreaFunction]) Values (4, '61', N'2F车间照明','1P8-3','2',N'2F车间',N'照明') 
Insert into udtLNElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[MeterLevel],[AreaLocation],[AreaFunction]) Values (4, '62', N'2F生产线电源','1P7-2','2',N'2F车间',N'') 
Insert into udtLNElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[MeterLevel],[AreaLocation],[AreaFunction]) Values (4, '63', N'B10 新喷油车间配电','1P5-1','2',N'B10-Mech',N'') 
Insert into udtLNElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[MeterLevel],[AreaLocation],[AreaFunction]) Values (4, '64', N'1F C车间空调电柜','4P6-1','2',N'1F车间',N'') 
Insert into udtLNElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[MeterLevel],[AreaLocation],[AreaFunction]) Values (4, '65', N'1F C车间空调电柜','4P6-2','2',N'1F车间',N'') 
Insert into udtLNElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[MeterLevel],[AreaLocation],[AreaFunction]) Values (4, '12', N'4#冷水机电源','4P7-1','2',N'',N'空调') 
Insert into udtLNElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[MeterLevel],[AreaLocation],[AreaFunction]) Values (4, '66', N'2F车间F柱子电柜','4P7-2','2',N'2F车间',N'') 
Insert into udtLNElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[MeterLevel],[AreaLocation],[AreaFunction]) Values (4, '17', N'太阳能（1）','AT8','1',N'',N'') 
Insert into udtLNElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[MeterLevel],[AreaLocation],[AreaFunction]) Values (2, '01', N'1#冷水机组','','1',N'',N'空调') 
Insert into udtLNElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[MeterLevel],[AreaLocation],[AreaFunction]) Values (2, '02', N'2#冷水机组','','1',N'',N'空调') 
Insert into udtLNElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[MeterLevel],[AreaLocation],[AreaFunction]) Values (2, '03', N'3#冷水机组','','1',N'',N'空调') 
Insert into udtLNElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[MeterLevel],[AreaLocation],[AreaFunction]) Values (2, '04', N'5#冷水机组','','1',N'',N'空调') 
Insert into udtLNElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[MeterLevel],[AreaLocation],[AreaFunction]) Values (2, '05', N'1#空压机','','1',N'',N'空压') 
Insert into udtLNElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[MeterLevel],[AreaLocation],[AreaFunction]) Values (2, '06', N'2#空压机','','1',N'',N'空压') 
Insert into udtLNElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[MeterLevel],[AreaLocation],[AreaFunction]) Values (2, '07', N'3#空压机','','1',N'',N'空压') 
Insert into udtLNElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[MeterLevel],[AreaLocation],[AreaFunction]) Values (5, '08', N'6#变总表','6p1','1',N'',N'') 
Insert into udtLNElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[MeterLevel],[AreaLocation],[AreaFunction]) Values (5, '19', N'楼顶排风机电源','5P2-2','2',N'',N'排风&其它') 
Insert into udtLNElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[MeterLevel],[AreaLocation],[AreaFunction]) Values (5, '20', N'3F B车间母线','5P6-1','2',N'3F车间B区',N'') 
Insert into udtLNElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[MeterLevel],[AreaLocation],[AreaFunction]) Values (5, '21', N'3F B车间母线','5P6-2','2',N'3F车间B区',N'') 
Insert into udtLNElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[MeterLevel],[AreaLocation],[AreaFunction]) Values (5, '22', N'3F B车间照明电源','5P7-1','2',N'3F车间B区',N'照明') 
Insert into udtLNElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[MeterLevel],[AreaLocation],[AreaFunction]) Values (5, '23', N'3F A车间照明电源','5P7-2','2',N'3F车间A区',N'照明') 
Insert into udtLNElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[MeterLevel],[AreaLocation],[AreaFunction]) Values (5, '24', N'写字楼4F电脑机房电源','5P7-3','2',N'',N'排风&其它') 
Insert into udtLNElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[MeterLevel],[AreaLocation],[AreaFunction]) Values (5, '15', N'1#-6#冷冻泵及冷却塔电源','5P10-1','2',N'',N'空调') 
Insert into udtLNElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[MeterLevel],[AreaLocation],[AreaFunction]) Values (5, '16', N'1#-6#冷却水泵电源','5P10-2','2',N'',N'空调') 
Insert into udtLNElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[MeterLevel],[AreaLocation],[AreaFunction]) Values (5, '25', N'3F A车间生产线电源','6P5-1','2',N'3F车间A区',N'') 
Insert into udtLNElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[MeterLevel],[AreaLocation],[AreaFunction]) Values (5, '26', N'3F A车间生产线电源','6P5-2','2',N'3F车间A区',N'') 
Insert into udtLNElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[MeterLevel],[AreaLocation],[AreaFunction]) Values (5, '27', N'4F B车间照明电源','6P6-1','2',N'4F车间B区',N'照明') 
Insert into udtLNElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[MeterLevel],[AreaLocation],[AreaFunction]) Values (5, '28', N'4F A车间照明电源','6P6-2','2',N'4F车间A区',N'照明') 
Insert into udtLNElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[MeterLevel],[AreaLocation],[AreaFunction]) Values (5, '29', N'3F车间空调及真空泵房','6P7-1','2',N'3F车间A区',N'') 
Insert into udtLNElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[MeterLevel],[AreaLocation],[AreaFunction]) Values (5, '30', N'4F车间空调及真空泵房','6P7-2','2',N'4F车间A区',N'') 
Insert into udtLNElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[MeterLevel],[AreaLocation],[AreaFunction]) Values (5, '31', N'写字楼3F A2区照明总电源','6P9-1','2',N'写字楼',N'照明') 
Insert into udtLNElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[MeterLevel],[AreaLocation],[AreaFunction]) Values (1, '09', N'7#变总表','7p1','1',N'',N'') 
Insert into udtLNElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[MeterLevel],[AreaLocation],[AreaFunction]) Values (1, '33', N'4F B车间生产线电源','7P5-1','2',N'4F车间B区',N'') 
Insert into udtLNElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[MeterLevel],[AreaLocation],[AreaFunction]) Values (1, '34', N'4F B车间生产线电源','7P5-2','2',N'4F车间B区',N'') 
Insert into udtLNElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[MeterLevel],[AreaLocation],[AreaFunction]) Values (1, '35', N'4F A车间生产线电源','7P7-2','2',N'4F车间A区',N'') 
Insert into udtLNElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[MeterLevel],[AreaLocation],[AreaFunction]) Values (1, '36', N'4F A车间生产线电源','7P7-1','2',N'4F车间A区',N'') 
Insert into udtLNElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[MeterLevel],[AreaLocation],[AreaFunction]) Values (1, '13', N'5#，6#空压机及冷干机电源','7P8-1','2',N'',N'空压') 
Insert into udtLNElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[MeterLevel],[AreaLocation],[AreaFunction]) Values (1, '37', N'注塑5F新工艺水处理电源','7P9-1','2',N'B17-Mceh',N'') 
Insert into udtLNElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[MeterLevel],[AreaLocation],[AreaFunction]) Values (1, '38', N'1#-4#电梯及纯净水电源','7P9-3','2',N'',N'排风&其它') 
Insert into udtLNElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[MeterLevel],[AreaLocation],[AreaFunction]) Values (1, '99', N'1#-4#生活水泵电源','7P9-4','2',N'',N'排风&其它') 
Insert into udtLNElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[MeterLevel],[AreaLocation],[AreaFunction]) Values (1, '39', N'5F机房辅助电源','7P9-5','2',N'',N'排风&其它') 
Insert into udtLNElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[MeterLevel],[AreaLocation],[AreaFunction]) Values (1, '40', N'5#，6#电梯电源','7P9-7','2',N'',N'排风&其它') 
Insert into udtLNElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[MeterLevel],[AreaLocation],[AreaFunction]) Values (1, '14', N'7#空压机','8P7-1','2',N'',N'空压') 
Insert into udtLNElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[MeterLevel],[AreaLocation],[AreaFunction]) Values (1, '41', N'B10 新喷油车间配电','8P5-2','2',N'B10-Mech',N'') 
Insert into udtLNElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[MeterLevel],[AreaLocation],[AreaFunction]) Values (1, '42', N'B10 新喷油车间配电','8P5-1','2',N'B10-Mech',N'') 
Insert into udtLNElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[MeterLevel],[AreaLocation],[AreaFunction]) Values (1, '32', N'写字楼4F A2区照明总电源','6P9-2','2',N'写字楼',N'') 
GO
