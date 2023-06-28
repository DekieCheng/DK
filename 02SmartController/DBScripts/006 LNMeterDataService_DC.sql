truncate table udtLNMeterCategory
Insert into udtLNMeterCategory([CategoryName],[CategoryDesc],[CycleTime],[UDPToSaveData],[UDPToSaveRuntimeLog],[AssemblyName],[ClassName])
	Values
	( 'Water', N'Water Meter', 3600,'udpLNWaterMeterImportProxy','udpLNSaveRuntimeLogProxy','LNMeterDataParser','WaterDataParser'),
	( 'Electricity',N'Electricity Meter',3600,'udpLNElectricityMeterImportProxy','udpLNSaveRuntimeLogProxy','LNMeterDataParser','ElectricityDataParser'),
	( 'ChilledWaterFlow',N'Air-conditioning chilled water flow meter', 3600,'udpLNChilledWaterFlowImportProxy','udpLNSaveRuntimeLogProxy','LNMeterDataParser','ChilledWaterFlowDataParser'),
	( 'CompressedAirFlow', N'Compressed air flow meter', 3600,'udpLNCompressedAirMeterImportProxy','udpLNSaveRuntimeLogProxy','LNMeterDataParser','CompressedAirFlowDataParser');
GO

truncate table udtLNCommand
Insert into udtLNCommand([MeterCategoryID], [CommandName], [CommandValue])
	Values
	( 1,'KEYOFORGDATA','03 20 01 00 02'),
	( 2,'KEYOFORGDATA_OLD','03 00 01 00 1F'),
	( 2,'KEYOFORGDATA_NEW','03 20 01 00 5E{NEW}'),
	( 3,'KEYOFORGDATA','03 00 01 00 24'),
	( 4, 'KEYOFORGDATA','03 00 00 00 0E');
GO

truncate table udtLNSerialPortServer
Insert into udtLNSerialPortServer(MeterCategoryID, HostIP, Port,CreationTime,Location)
	Values
	--电表
	( 2,'10.200.2.83',5300, getdate(),N'B17二楼电表'),
	( 2,'10.200.2.84',5300, getdate(),N'B17二楼电表'),
	( 2,'10.200.2.85',5300, getdate(),N'B17五楼电表'),
	( 2,'10.200.2.89',5300, getdate(),N'B17五楼电表'),
	( 2,'10.200.2.90',5300, getdate(),N'B17五楼电表'),

	--空调能量计
	( 3,'10.200.2.88',5300, getdate(),N'B17空调能量计'),

	--压缩空气流量计
	( 4,'10.200.2.86',5300, getdate(), N'B17 压缩空气流量计'),  
	( 4,'10.200.2.87',5300, getdate(), N'B10 压缩空气流量计'),

	--电表(新增电表)
	( 2,'10.200.7.188',5300, getdate(),N'B17三楼对应设备电表');
GO

--Register the WaterMeter(电表)
GO

--Register the ElectricityMeter(电表)
Declare @tblElectricityMeter table ([SerialPortServerID] int,[MeterAddr] varchar(200),[MeterName] nvarchar(200),[MeterNo] nvarchar(200), 
[AreaLocation]  nvarchar(200),[AreaFunction]  nvarchar(200),[MeterLevel] varchar(20),[IsNewProtocal] bit,[IsSolarEnergy] bit, tmphostIP varchar(200))

Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'11',N'3#主变','22#',N'',N'',1,0,0,'10.200.2.90')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'43',N'1F 注塑B动力电柜','17#',N'B17-Mceh',N'',2,0,0,'10.200.2.90')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'46',N'1F 注塑B动力电柜','7#',N'B17-Mceh',N'',2,0,0,'10.200.2.90')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'47',N'B10冷冻水加压泵','9#',N'B10-Mech',N'',2,0,0,'10.200.2.90')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'48',N'1F C车间空调电柜','10#',N'1F车间',N'',2,0,0,'10.200.2.90')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'49',N'1F C车间动力电柜','11#',N'1F车间',N'',2,0,0,'10.200.2.90')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'50',N'PCBA 1、2F空调、注塑A排风','12#',N'1F车间',N'',2,0,0,'10.200.2.90')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'51',N'2F QA lab动力电柜','15#',N'2F车间',N'',2,0,0,'10.200.2.90')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'52',N'1F 注塑B照明','16#',N'B17-Mceh',N'',2,0,0,'10.200.2.90')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'53',N'2F车间生产线','23#',N'2F车间',N'',2,0,0,'10.200.2.90')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'54',N'1F 注塑B动力电柜','26#',N'B17-Mceh',N'',2,0,0,'10.200.2.90')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'55',N'1F 注塑B动力电柜','27#',N'B17-Mceh',N'',2,0,0,'10.200.2.90')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'56',N'2F QA lab动力电柜','28#',N'2F车间',N'',2,0,0,'10.200.2.90')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'57',N'B10 新喷油车间配电','29#',N'B10-Mech',N'',2,0,0,'10.200.2.90')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'18',N'太阳能（2）','31#',N'',N'',1,0,0,'10.200.2.90')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'10',N'1#主变','40#',N'',N'',1,0,0,'10.200.2.89')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'58',N'1F C车间动力电柜','2#',N'1F车间',N'',2,0,0,'10.200.2.89')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'59',N'1F 注塑A区及应急照明','4#',N'1楼货仓',N'照明',2,0,0,'10.200.2.89')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'44',N'1F 注塑A动力电柜','5#',N'1楼货仓',N'',2,0,0,'10.200.2.89')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'45',N'B10 新喷油DK14配电','6#',N'B10-Mech',N'',2,0,0,'10.200.2.89')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'60',N'1F C车间车间照明','46#',N'1F车间',N'照明',2,0,0,'10.200.2.89')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'61',N'2F车间照明','45#',N'2F车间',N'照明',2,0,0,'10.200.2.89')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'62',N'2F生产线电源','44#',N'2F车间',N'',2,0,0,'10.200.2.89')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'63',N'B10 新喷油车间配电','41#',N'B10-Mech',N'',2,0,0,'10.200.2.89')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'64',N'1F C车间空调电柜','37#',N'1F车间',N'',2,0,0,'10.200.2.89')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'65',N'1F C车间空调电柜','38#',N'1F车间',N'',2,0,0,'10.200.2.89')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'12',N'4#冷水机电源','35#',N'',N'空调',2,0,0,'10.200.2.89')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'66',N'2F车间F柱子电柜','36#',N'2F车间',N'',2,0,0,'10.200.2.89')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'17',N'太阳能（1）','30#',N'',N'',1,0,0,'10.200.2.89')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'01',N'1#冷水机组','48#',N'',N'空调',1,0,0,'10.200.2.83')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'02',N'2#冷水机组','49#',N'',N'空调',1,0,0,'10.200.2.83')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'03',N'3#冷水机组','50#',N'',N'空调',1,0,0,'10.200.2.83')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'04',N'5#冷水机组','51#',N'',N'空调',1,0,0,'10.200.2.83')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'05',N'1#空压机','52#',N'',N'空压',1,0,0,'10.200.2.83')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'06',N'2#空压机','53#',N'',N'空压',1,0,0,'10.200.2.83')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'07',N'3#空压机','54#',N'',N'空压',1,0,0,'10.200.2.83')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'08',N'6#变总表','58#',N'',N'',1,0,0,'10.200.2.84')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'19',N'楼顶排风机电源','66#',N'',N'排风&其它',2,0,0,'10.200.2.84')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'20',N'3F B车间母线','67#',N'3F车间B区',N'',2,0,0,'10.200.2.84')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'21',N'3F B车间母线','68#',N'3F车间B区',N'',2,0,0,'10.200.2.84')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'22',N'3F B车间照明电源','69#',N'3F车间B区',N'照明',2,0,0,'10.200.2.84')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'23',N'3F A车间照明电源','70#',N'3F车间A区',N'照明',2,0,0,'10.200.2.84')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'24',N'写字楼4F电脑机房电源','71#',N'',N'排风&其它',2,0,0,'10.200.2.84')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'15',N'1#-6#冷冻泵及冷却塔电源','72#',N'',N'空调',2,0,0,'10.200.2.84')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'16',N'1#-6#冷却水泵电源','73#',N'',N'空调',2,0,0,'10.200.2.84')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'25',N'3F A车间生产线电源','74#',N'3F车间A区',N'',2,0,0,'10.200.2.84')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'26',N'3F A车间生产线电源','75#',N'3F车间A区',N'',2,0,0,'10.200.2.84')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'27',N'4F B车间照明电源','76#',N'4F车间B区',N'照明',2,0,0,'10.200.2.84')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'28',N'4F A车间照明电源','77#',N'4F车间A区',N'照明',2,0,0,'10.200.2.84')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'29',N'3F车间空调及真空泵房','78#',N'3F车间A区',N'',2,0,0,'10.200.2.84')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'30',N'4F车间空调及真空泵房','79#',N'4F车间A区',N'',2,0,0,'10.200.2.84')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'31',N'写字楼3F A2区照明总电源','81#',N'写字楼',N'照明',2,0,0,'10.200.2.84')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'09',N'7#变总表','57#',N'',N'',1,0,0,'10.200.2.85')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'33',N'4F B车间生产线电源','84#',N'4F车间B区',N'',2,0,0,'10.200.2.85')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'34',N'4F B车间生产线电源','85#',N'4F车间B区',N'',2,0,0,'10.200.2.85')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'35',N'4F A车间生产线电源','86#',N'4F车间A区',N'',2,0,0,'10.200.2.85')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'36',N'4F A车间生产线电源','87#',N'4F车间A区',N'',2,0,0,'10.200.2.85')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'13',N'5#，6#空压机及冷干机电源','88#',N'',N'空压',2,0,0,'10.200.2.85')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'37',N'注塑5F新工艺水处理电源','89#',N'B17-Mceh',N'',2,0,0,'10.200.2.85')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'38',N'1#-4#电梯及纯净水电源','90#',N'',N'排风&其它',2,0,0,'10.200.2.85')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'99',N'1#-4#生活水泵电源','91#',N'',N'排风&其它',2,1,0,'10.200.2.85')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'39',N'5F机房辅助电源','92#',N'',N'排风&其它',2,0,0,'10.200.2.85')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'40',N'5#，6#电梯电源','93#',N'',N'排风&其它',2,0,0,'10.200.2.85')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'14',N'7#空压机','65#',N'',N'空压',2,0,0,'10.200.2.85')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'41',N'B10 新喷油车间配电','63#',N'B10-Mech',N'',2,0,0,'10.200.2.85')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'42',N'B10 新喷油车间配电','62#',N'B10-Mech',N'',2,0,0,'10.200.2.85')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'32',N'写字楼4F A2区照明总电源','82#',N'写字楼',N'照明',2,0,0,'10.200.2.85')

Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'70',N'1#贴片机','',N'',N'',2,1,0,'10.200.7.188')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'71',N'2#贴片机','',N'',N'',2,1,0,'10.200.7.188')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'72',N'回流炉','',N'',N'',2,1,0,'10.200.7.188')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'73',N'3#贴片机','',N'',N'',2,0,0,'10.200.7.188')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'74',N'自动拉','',N'',N'',2,1,0,'10.200.7.188')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'75',N'后端测试','',N'',N'',2,1,0,'10.200.7.188')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'76',N'SPI','',N'',N'',2,1,0,'10.200.7.188')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'77',N'DEK+前段接驳台','',N'',N'',2,1,0,'10.200.7.188')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'78',N'1#AOI+中段接驳台','',N'',N'',2,1,0,'10.200.7.188')
Insert into @tblElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[MeterLevel],[IsNewProtocal],[IsSolarEnergy], tmphostIP)  Values (-1,'79',N'2#AOI+后段接驳台','',N'',N'',2,1,0,'10.200.7.188')


Update @tblElectricityMeter Set SerialPortServerID = s.ID from udtLNSerialPortServer s where s.HostIP = tmphostIP
truncate table udtLNElectricityMeter
Insert into udtLNElectricityMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[IsNewProtocal])
Select [SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],[IsNewProtocal] from @tblElectricityMeter Order By SerialPortServerID
GO

--Register the AirConditioningChilledWaterFlowMeter (空调能量计)
Declare @tblChilledWaterFlowMeter table ([SerialPortServerID] int,[MeterAddr] varchar(200),[MeterName] nvarchar(200),[MeterNo] nvarchar(200)
, [AreaLocation]  nvarchar(200),[AreaFunction]  nvarchar(200),tmphostIP varchar(200))
Insert into @tblChilledWaterFlowMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],tmphostIP)  Values (-1,'91',N'1-4F办公楼空调','',N'1-4F办公楼空调',N'空调能量计','10.200.2.88')
Insert into @tblChilledWaterFlowMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],tmphostIP)  Values (-1,'90',N'3#管井（1-4F拉头空调）','',N'3#管井（1-4F拉头空调）',N'空调能量计','10.200.2.88')
Insert into @tblChilledWaterFlowMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],tmphostIP)  Values (-1,'93',N'6#管井（1-4F拉尾空调）','',N'6#管井（1-4F拉尾空调）',N'空调能量计','10.200.2.88')
Insert into @tblChilledWaterFlowMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],tmphostIP)  Values (-1,'94',N'B10冷冻水（生产+空调）','',N'B10冷冻水（生产+空调）',N'空调能量计','10.200.2.88')
Insert into @tblChilledWaterFlowMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],tmphostIP)  Values (-1,'92',N'新加建办公楼空调','',N'新加建办公楼空调',N'空调能量计','10.200.2.88')
Insert into @tblChilledWaterFlowMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],tmphostIP)  Values (-1,'95',N'B17 1F注塑','',N'B17 1F注塑',N'空调能量计','10.200.2.88')

Update @tblChilledWaterFlowMeter Set SerialPortServerID = s.ID from udtLNSerialPortServer s where s.HostIP = tmphostIP
truncate table udtLNChilledWaterFlowMeter
Insert into udtLNChilledWaterFlowMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction])
Select [SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction] from @tblChilledWaterFlowMeter Order By SerialPortServerID
GO

--Register the CompressedAirFlowMeter(压缩空气流量计)
Declare @tblCompressedAirFlowMeter table ([SerialPortServerID] int,[MeterAddr] varchar(200),[MeterName] nvarchar(200),[MeterNo] nvarchar(200)
, [AreaLocation]  nvarchar(200),[AreaFunction]  nvarchar(200) , tmphostIP varchar(200))
Insert into @tblCompressedAirFlowMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],tmphostIP)  Values (-1,'01',N'B17压缩空气监控','',N'B17压缩空气监控',N'流量计','10.200.2.87')
Insert into @tblCompressedAirFlowMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction],tmphostIP)  Values (-1,'02',N'B10压缩空气监控','',N'B10压缩空气监控',N'流量计','10.200.2.86 ')

Update @tblCompressedAirFlowMeter Set SerialPortServerID = s.ID from udtLNSerialPortServer s where s.HostIP = tmphostIP
truncate table udtLNCompressedAirFlowMeter
Insert into udtLNCompressedAirFlowMeter([SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction])
Select [SerialPortServerID],[MeterAddr],[MeterName],[MeterNo],[AreaLocation],[AreaFunction] from @tblCompressedAirFlowMeter Order By SerialPortServerID
GO