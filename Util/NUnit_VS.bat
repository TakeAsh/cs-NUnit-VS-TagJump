@echo off
cd /d "%1"
nunit-console-x86.exe "%2" | NUnit-VS-TagJump.exe
