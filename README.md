# TestRunner

[![NuGet Version](http://img.shields.io/nuget/v/Agero.TestsRunner.NUnit.svg?style=flat)](https://www.nuget.org/packages/Agero.TestsRunner.NUnit/) 
[![NuGet Downloads](http://img.shields.io/nuget/dt/Agero.TestsRunner.NUnit.svg?style=flat)](https://www.nuget.org/packages/Agero.TestsRunner.NUnit/)

## Usage:
Library this helps to run NUnit tests.

Create Test Service: 

```csharp 
var testService = new TestService();
var response = testService.Run("<Your path to assembly with NUnit tests>");
```

