@ECHO OFF
@REM  ----------------------------------------------------------------------------
@REM  SetupDataBase.cmd file
@REM
@REM  This cmd file calls the SQL script to create the database and tables 
@REM  required for the reference implementation. The script creates the database
@REM  in the local SQLEXPRESS instance. The tables are created in the database named
@REM  EAppointments. If the database does not yet exist, the SQL script
@REM  will create it. If the EAppointments database already exists, the
@REM  script will delete it, and then create it.
@REM  
@REM  ----------------------------------------------------------------------------

echo.
echo ================================================================
echo   SetupDatabase
echo      Creates the database required for the ASAP Case Study reference
echo      implementation. The tables are created in the database
echo      named EAppointments in the local SQLEXPRESS instance. 
echo      The script creates the database if it does not exist. If
echo      the database exists, the script will delete it, and then
echo      recreate it. 
echo.
echo      You can modify this command file to create the database
echo      on a different instance of SQL (for example, to use SQL 
echo      Server 2005). If you do this, you must also update the
echo      SampleDatabaseConnectionString connection string in the
echo      Web.config file of the reference implementation 
echo      WebUI Web site. 
echo.
echo      If you do not have .NET 3.0 installed, you will need to 
echo      install .NET 3.0 for this script to work.
echo      If you use a language other than English, you will need to
echo      modify this script where noted.
echo ================================================================
echo.

if "%1"=="/?" goto HELP

PAUSE 

SETLOCAL
if not Exist DatabaseCreateScript.sql goto HELPSCRIPT
if not Exist DatabaseScript.sql goto HELPSCRIPT
if not Exist SampleDataScript.sql goto HELPSCRIPT
if not Exist UsersAndRolesScript.sql goto HELPSCRIPT

@REM  ------------------------------------------------
@REM  Shorten the command prompt for making the output
@REM  easier to read.
@REM  ------------------------------------------------
set savedPrompt=%prompt%
set prompt=*$g

SET serverName="(local)"
SET wfScriptRoot=%windir%\Microsoft.NET\Framework\v3.0\Windows Workflow Foundation\SQL\EN

@ECHO ----------------------------------------
@ECHO SetupDatabase.bat Started
@ECHO ----------------------------------------
@ECHO.

set cmd=OSQL -S %serverName% -E -n -i DatabaseCreateScript.sql

echo.
@echo %cmd%
@%cmd%

set cmd=OSQL -S %serverName% -E -n -i DatabaseScript.sql

echo.
@echo %cmd%
@%cmd%

set cmd=%SystemRoot%\Microsoft.NET\Framework\v2.0.50727\aspnet_regsql.exe -S %serverName% -E -A mr -d EAppointments -Q

echo.
@echo %cmd%
@%cmd%

set cmd=OSQL -S %serverName% -E -n -i UsersAndRolesScript.sql

echo.
@echo %cmd%
@%cmd%

set cmd=OSQL -S %serverName% -d "EAppointments_WF" -E -n -i "%wfScriptRoot%\SqlPersistenceService_Schema.sql"

echo.
@echo %cmd%
@%cmd%

set cmd=OSQL -S %serverName% -d "EAppointments_WF" -E -n -i "%wfScriptRoot%\SqlPersistenceService_Logic.sql"

echo.
@echo %cmd%
@%cmd%

set cmd=OSQL -S %serverName% -d "EAppointments_WF" -E -n -i "%wfScriptRoot%\Tracking_Schema.sql"

echo.
@echo %cmd%
@%cmd%

set cmd=OSQL -S %serverName% -d "EAppointments_WF" -E -n -i "%wfScriptRoot%\Tracking_Logic.sql"

echo.
@echo %cmd%
@%cmd%


set cmd=OSQL -S %serverName% -E -n -i SampleDataScript.sql

echo.
@echo %cmd%
@%cmd%

echo.
pause

@REM  ----------------------------------------
@REM  Restore the command prompt and exit
@REM  ----------------------------------------
@goto :exit

@REM  -------------------------------------------
@REM  Handle errors
@REM
@REM  Use the following after any call to exit
@REM  and return an error code when errors occur
@REM
@REM  if errorlevel 1 goto :error	
@REM  -------------------------------------------
:error
@ECHO An error occured in SetupDatabase.bat - %errorLevel%

@exit errorLevel

:HELPSCRIPT
echo.
echo Error: Following SQL scripts are required for the setup:-
echo         1. DatabaseCreateScript.sql
echo         2. DatabaseScript.sql
echo         3. SampleDataScript.sql
echo	     4. UsersAndRolesScript.sql
echo.
PAUSE
goto exit

:HELP
echo.
echo Usage: SetupDatabase.bat 
echo.

@REM  ----------------------------------------
@REM  The exit label
@REM  ----------------------------------------
:exit

set prompt=%savedPrompt%
set savedPrompt=

echo on

