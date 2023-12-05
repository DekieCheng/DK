@echo off
cls
CD %~dp0

ECHO ******************************************************************************
ECHO *
ECHO *                   Install DMN B17 LNMeter Dashboard
ECHO *
ECHO ******************************************************************************

echo ==============================
echo Folder:Create tables
echo ==============================
echo Start: 001 LNMeterDashboard_BasicData_SC.sql
sqlcmd -U %username% -P %Pwd% -S %Server% -d %DB% -b -f 65001  -i "001 LNMeterDashboard_BasicData_SC.sql"
if %errorlevel% neq 0 goto ERROR

echo ==============================
echo Folder:DataChanges
echo ==============================
echo Start: 002 LNMeterDashboard_DC.sql
sqlcmd -U %username% -P %Pwd% -S %Server% -d %DB% -b -f 65001  -i "002 LNMeterDashboard_DC.sql"
if %errorlevel% neq 0 goto ERROR

:End
Pause
GOTO :EOF
:Error
ECHO Script Above Failed to Generate.The rest of the scripts will not executed.
Pause
exit /b %errorlevel%


