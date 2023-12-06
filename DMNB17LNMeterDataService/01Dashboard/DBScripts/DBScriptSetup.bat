@ECHO off
CLS
CD /d %~dp0

:Reset
SET "Server="
SET "DB="
SET "username="
SET "Pwd="
SET "pOption="
SET "pNextEvent="
SET "pFBI_BatchVer="
SET pMenu=%~dp0

:_Menu
          cls
          CD %~dp0
          
          SET "pOption="
          ECHO ******************************************************************************
          ECHO *                                                                             
                
          ECHO *                         Select needed option
          ECHO *                                                                             
          ECHO *     1) Install DMN B17 LNMeter Dashboard DB scripts.                           
          ECHO *                                                                             
          ECHO *     all) All options  (Options 1) 
          ECHO *       r) Reset Connection 
          ECHO *       x) Exit 
          ECHO *                                                                             
                
          ECHO ******************************************************************************
          
          IF NOT [%Server%] == [] (
               ECHO SQL Server: %Server% 
          )
          IF NOT [%DB%] == [] (
               ECHO DB: %DB% 
          )
          IF NOT [%username%] == [] (
               ECHO Username: %username% 
          )
          IF NOT [%Pwd%] == [] (
               ECHO Password: *****
          )
          ECHO.
          SET	/P pOption=Select your option: 

          SET pNextEvent=Menu
          IF %pOption%==1  CALL :_LNMeterDataService

          IF /I %pOption%==all (                   
                    CALL :_LNMeterDataService
                    )

          IF /I %pOption%==r  CALL :_CONN_STR
          IF /I %pOption%==x  GOTO :_EXIT

          GOTO :_Menu

:_CONN_STR 
          ECHO.
          set /p Server=Specify the SQL Server: 
          ECHO.
          set /p DB=Specify the DB: 
          ECHO.
          set /p username=Specify the Username: 
          ECHO.
          set /p Pwd=Specify the Password: 
          ECHO.
          GOTO :EOF


:_LNMeterDataService
CALL :_CONN_VER
CALL "Install.bat"
if %errorlevel% neq 0 goto ERROR
CD %~dp0


GOTO :EOF

:_CONN_VER
     IF [%Server%] == [] if [%DB%] == [] if [%username%] == [] if [%Pwd%] == [] CALL :_CONN_STR
     GOTO :EOF

:_EXIT
     Exit /b

:Error
ECHO Script Above Failed to Generate.The rest of the scripts will not executed.
Pause
GOTO _Menu
