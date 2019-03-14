# TestRunner

[![NuGet Version](http://img.shields.io/nuget/v/Agero.TestsRunner.NUnit.svg?style=flat)](https://www.nuget.org/packages/Agero.TestsRunner.NUnit/) 
[![NuGet Downloads](http://img.shields.io/nuget/dt/Agero.TestsRunner.NUnit.svg?style=flat)](https://www.nuget.org/packages/Agero.TestsRunner.NUnit/)

## Usage:
Library this helps to run Tests.

Create Run: 

```csharp 

var response = Container.Get<ITestService>().Run("<Your RESTAPI Integration Tests dll Path>", "<Your RESTAPI config path>");
```

