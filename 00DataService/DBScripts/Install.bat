@echo off
cls
CD %~dp0

ECHO ******************************************************************************
ECHO *
ECHO *                   Install DMN B17 LNMeterDataService
ECHO *
ECHO ******************************************************************************

echo ==============================
echo Folder:Create tables
echo ==============================
echo Start: 001 LNMeterDataService_BasicData_SC.sql
sqlcmd -U %username% -P %Pwd% -S %Server% -d %DB% -b -f 65001  -i "001 LNMeterDataService_BasicData_SC.sql"
if %errorlevel% neq 0 goto ERROR
echo Start: 002 LNMeterDataService_Water_SC.sql
sqlcmd -U %username% -P %Pwd% -S %Server% -d %DB% -b -f 65001  -i "002 LNMeterDataService_Water_SC.sql"
if %errorlevel% neq 0 goto ERROR
echo Start: 003 LNMeterDataService_Electricity_SC.sql
sqlcmd -U %username% -P %Pwd% -S %Server% -d %DB% -b -f 65001  -i "003 LNMeterDataService_Electricity_SC.sql"
if %errorlevel% neq 0 goto ERROR
echo Start: 004 LNMeterDataService_ChilledWaterFlowMeter_SC.sql
sqlcmd -U %username% -P %Pwd% -S %Server% -d %DB% -b -f 65001  -i "004 LNMeterDataService_ChilledWaterFlowMeter_SC.sql"
if %errorlevel% neq 0 goto ERROR
echo Start: 005 LNMeterDataService_CompressedAirFlowMeter_SC.sql
sqlcmd -U %username% -P %Pwd% -S %Server% -d %DB% -b -f 65001  -i "005 LNMeterDataService_CompressedAirFlowMeter_SC.sql"
if %errorlevel% neq 0 goto ERROR


echo ==============================
echo Folder:DataChanges
echo ==============================
echo Start: 006 LNMeterDataService_DC.sql
sqlcmd -U %username% -P %Pwd% -S %Server% -d %DB% -b -f 65001  -i "006 LNMeterDataService_DC.sql"
if %errorlevel% neq 0 goto ERROR


echo ==============================
echo Folder:StoredProcedures
echo ==============================
echo Start: 104 udpLNXMLExtraData.sql
sqlcmd -U %username% -P %Pwd% -S %Server% -d %DB% -b -f 65001  -i "104 udpLNXMLExtraData.sql"
if %errorlevel% neq 0 goto ERROR
echo Start: 105 udpLNExecuteDynamicQuery.sql
sqlcmd -U %username% -P %Pwd% -S %Server% -d %DB% -b -f 65001  -i "105 udpLNExecuteDynamicQuery.sql"
if %errorlevel% neq 0 goto ERROR
echo Start: 106 udpLNWaterMeterImportProxy.sql
sqlcmd -U %username% -P %Pwd% -S %Server% -d %DB% -b -f 65001  -i "106 udpLNWaterMeterImportProxy.sql"
if %errorlevel% neq 0 goto ERROR
echo Start: 107 udpLNElectricityMeterImportProxy.sql
sqlcmd -U %username% -P %Pwd% -S %Server% -d %DB% -b -f 65001  -i "107 udpLNElectricityMeterImportProxy.sql"
if %errorlevel% neq 0 goto ERROR
echo Start: 108 udpLNChilledWaterFlowImportProxy
sqlcmd -U %username% -P %Pwd% -S %Server% -d %DB% -b -f 65001  -i "108 udpLNChilledWaterFlowImportProxy.sql"
if %errorlevel% neq 0 goto ERROR
echo Start: 109 udpLNCompressedAirMeterImportProxy
sqlcmd -U %username% -P %Pwd% -S %Server% -d %DB% -b -f 65001  -i "109 udpLNCompressedAirMeterImportProxy.sql"
if %errorlevel% neq 0 goto ERROR
echo Start: 201 udpLNSaveRuntimeLogProxy.sql
sqlcmd -U %username% -P %Pwd% -S %Server% -d %DB% -b -f 65001  -i "201 udpLNSaveRuntimeLogProxy.sql"
if %errorlevel% neq 0 goto ERROR

:End
Pause
GOTO :EOF
:Error
ECHO Script Above Failed to Generate.The rest of the scripts will not executed.
Pause
exit /b %errorlevel%


