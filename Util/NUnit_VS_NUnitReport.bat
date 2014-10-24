@echo off
rem format TestResult.xml by NUnitReport # http://hccweb1.bai.ne.jp/tsune-1/
cd /d "%1"
nunit-console-x86.exe "%2" > NUnitOutput.tmp
set NUnitExitCode=%ERRORLEVEL%
NUnitReport.CUI.exe TestResult.xml
type NUnitOutput.tmp | NUnit-VS-TagJump.exe
del NUnitOutput.tmp
exit /b %NUnitExitCode%
