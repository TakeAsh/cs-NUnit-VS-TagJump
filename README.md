# NUnit-VS-TagJump

* The filter allow that you can "tag jump" from NUnit output on Visual Studio output.
* capture not only "Test Failure" but also "Test Error" and "Not Runnable".

## Usage
### Build event
* add a build event as "Post Build Event" and "On Output Updated" in the test project.
```Batchfile
cd /d "$(TargetDir)"
nunit-console-x86.exe "$(TargetPath)" | NUnit-VS-TagJump.exe
```

### External tools
<table border="1">
<tr><th>Title</th><td>NUnit(&U)</td></tr>
<tr><th>Command</th><td>NUnit_VS.bat</td></tr>
<tr><th>Arguments</th><td>"$(BinDir)" "$(BinDir)$(TargetName)$(TargetExt)"</td></tr>
<tr><th>Initial directory</th><td>"$(BinDir)"</td></tr>
<tr><th>use output window</th><td>check</td></tr>
<tr><th>NUnit_VS.bat</th><td>@echo off<br>cd /d "%1"<br>nunit-console-x86.exe "%2" | NUnit-VS-TagJump.exe</td></tr>
</table>

## Environment
* NUnit 2.6.3
* Visual Studio 2010 SP1
* .NET framework 4.0

## NUnit-VS-TagJump.exe convert NUnit output like as below.
* before
> 1) Test Failure : [TestCase]<br>
>     Expected: [Expected]<br>
>  But was:  [Actual]
>
> at SomeProject.ClassTest.MethodTest(Type a, Type b) in [Path\\File.cs]:line [LineNo]

* after
> 1) Test Failure : [TestCase]<br>
>     Expected: [Expected]<br>
>  But was:  [Actual]
>
> at SomeProject.ClassTest.MethodTest(Type a, Type b)<br>
> \[Path\\File.cs\]([LineNo]): Error : TestCase:[TestCase], Expected:[Expected], Actual:[Actual]
