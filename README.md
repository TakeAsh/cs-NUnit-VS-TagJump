# NUnit-VS-TagJump

* The filter allow that you can "tag jump" from NUnit output on Visual Studio.

## Usage
* add a build event as "Post Build Event" and "On Output Updated" in the test project.

```Batchfile
cd /d "$(TargetDir)"
nunit-console-x86.exe "$(TargetPath)" | NUnit-VS-TagJump.exe
```
