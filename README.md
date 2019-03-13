# TestRunner

[![NuGet Version](http://img.shields.io/nuget/v/Agero.TestsRunner.NUnit.svg?style=flat)](https://www.nuget.org/packages/Agero.TestsRunner.NUnit/) 
[![NuGet Downloads](http://img.shields.io/nuget/dt/Agero.TestsRunner.NUnit.svg?style=flat)](https://www.nuget.org/packages/Agero.TestsRunner.NUnit/)

## Usage:
Library this helps to run your api intergration Tests. Create TestController in your api and use below code. Replace testAssemblyPath with APi IntegrationTests Path

For example:

```csharp
   [RoutePrefix("tests")]
    public class TestController : BaseController
    {
        /// <remarks>
        /// POST /tests?action=run
        /// </remarks>
        [Route("")]
        [HttpPost]
        public HttpResponseMessage ExecuteAction([FromUri] TestAction? action = null)
        {
            if (!action.HasValue)
                throw new BadRequestException("Action must be specified and valid.");

            if (!Settings.Default.EnableIntegrationTests)
                throw new NotFoundException("Tests are disabled for current environment.", ResponseCode.INVALID_OPERATION);

            switch (action.Value)
            {
                case TestAction.Run:
                {
                    var testAssemblyPath = Path.Combine(HttpRuntime.BinDirectory, "Agero.YourAPI.RESTAPI.IntegrationTests.dll");
                    var testAssemblyConfigPath = Path.Combine(HttpRuntime.AppDomainAppPath, "Web.config");

                    var response = Container.Get<ITestService>().Run(testAssemblyPath, testAssemblyConfigPath);
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }

                default:
                    throw new InvalidOperationException(action.ToString());
            }
        }

        /// <remarks>
        /// GET /tests/actions
        /// </remarks>
        [Route("actions")]
        [HttpGet]
        public string[] GetActions()
        {
            return EnumHelper.GetNames<TestAction>();
        }
    }
}
```

