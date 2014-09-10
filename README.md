# NUnit-VS-TagJump

* The filter allow that you can "tag jump" from NUnit output on Visual Studio output.

## Usage
* add a build event as "Post Build Event" and "On Output Updated" in the test project.
```Batchfile
cd /d "$(TargetDir)"
nunit-console-x86.exe "$(TargetPath)" | NUnit-VS-TagJump.exe
```

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
