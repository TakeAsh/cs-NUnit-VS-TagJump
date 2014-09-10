# NUnit-VS-TagJump

* The filter allow that you can "tag jump" from NUnit output on Visual Studio output.

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
<tr><th>Arguments</th><td>"$(BinDir)$(TargetName)$(TargetExt)"</td></tr>
<tr><th>Initial directory</th><td>"$(BinDir)"</td></tr>
<tr><th>use output window</th><td>check</td></tr>
<tr><th>NUnit_VS.bat</th><td>@echo off<br>NUinit-console-x86.exe "%1" | NUnit-VS-TagJump.exe</td></tr>
</table>

## NUnit-VS-TagJump.exe convert NUnit output like as below.
* before
> 1) Test Failure : Sample1Test.Class1Test.maxTest_double(3.0d,1.0d,1.0d)<br>
>     Expected: 1.0d<br>
>  But was:  3.0d
>
> at Sample1Test.Class1Test.maxTest_double(Double a, Double b, Double expected) in D:\\_temp\\cs-NUnit-VS-TagJump\\Sample1Test\\Class1Test.cs:line 25

* after
> 1) Test Failure : Sample1Test.Class1Test.maxTest_double(3.0d,1.0d,1.0d)<br>
>     Expected: 1.0d<br>
>  But was:  3.0d
>
> at Sample1Test.Class1Test.maxTest_double(Double a, Double b, Double expected)<br>
> D:\\_temp\\cs-NUnit-VS-TagJump\\Sample1Test\\Class1Test.cs(25): assert error : TestCase:Sample1Test.Class1Test.maxTest_double(3.0d,1.0d,1.0d), Expected:1.0d, Actual:3.0d
