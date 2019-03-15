using System.Collections.Generic;
using Agero.TestsRunner.NUnit.Models;

namespace Agero.TestsRunner.NUnit
{
    /// <summary>Test service which can run Nunit tests</summary>
    public interface ITestService
    {
        /// <summary>Runs NUnit tests in specified assembly and returns results</summary>
        /// <param name="testAssemblyPath">Path to assembly with NUnit tests</param>
        /// <param name="testAssemblyConfigPath">Path to configuration file for assembly with tests</param>
        ///   <param name="parameters">Parameters</param>
        /// <returns>Test results</returns>
        TestRunResponse Run(string testAssemblyPath, string testAssemblyConfigPath = null, IDictionary<string, string> parameters = null);
    }
}
