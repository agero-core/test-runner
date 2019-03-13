using Agero.TestsRunner.NUnit.Exceptions;
using Agero.TestsRunner.NUnit.Helpers;
using Agero.TestsRunner.NUnit.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Agero.Core.Checker;
using NUnit.Engine;

namespace Agero.TestsRunner.NUnit
{
    /// <summary>Test service which can run Nunit tests</summary>
    public class TestService : ITestService
    {
        /// <summary>Runs NUnit tests in specified assembly and returns results</summary>
        /// <param name="testAssemblyPath">Path to assembly with NUnit tests</param>
        /// <param name="testAssemblyConfigPath">Path to configuration file for assembly with tests</param>
        /// <param name="parameters"></param>
        /// <returns>Test results</returns>
        public TestRunResponse Run(string testAssemblyPath, string testAssemblyConfigPath = null, IDictionary<string,string> parameters = null)
        {
            Check.ArgumentIsNullOrWhiteSpace(testAssemblyPath, "testAssemblyPath");

            var testPackage = new TestPackage(testAssemblyPath);

            if (!string.IsNullOrWhiteSpace(testAssemblyConfigPath))
                testPackage.Settings.Add("ConfigurationFile", testAssemblyConfigPath);

            if (parameters != null)
                testPackage.AddSetting("TestParametersDictionary", parameters);

            using (var testEngine = new TestEngine())
            {
                testEngine.WorkDirectory = Directory.GetCurrentDirectory();

                using (var testRunner = testEngine.GetRunner(testPackage))
                {
                    var testRunResponse = testRunner.Run(new EmptyTestEventListener(), TestFilter.Empty);

                    return CreateResponse(testRunResponse);
                }
            }
        }

        private static TestRunResponse CreateResponse(XmlNode xml)
        {
            Check.ArgumentIsNull(xml, "xml");

            var assembly = xml["test-suite"];
            Check.Assert(assembly != null, "assembly != null");

            // ReSharper disable once PossibleNullReferenceException
            var runState = assembly.Attributes["runstate"].Value;
            if (runState != "Runnable")
                throw new TestRunException($"Test assembly cannot be ran. {assembly.InnerText}");

            return
                new TestRunResponse
                {
                    Result = assembly.Attributes["result"].Value,
                    Duration = double.Parse(assembly.Attributes["duration"].Value),
                    Total = int.Parse(assembly.Attributes["total"].Value),
                    Passed = int.Parse(assembly.Attributes["passed"].Value),
                    Failed = int.Parse(assembly.Attributes["failed"].Value),
                    TestGroups = 
                        assembly.ChildNodes.Cast<XmlNode>()
                            .Where(n => n.Name == "test-suite")
                            .SelectMany(CreateTestGroups)
                            .ToArray()
                };
        }

        private static TestGroup[] CreateTestGroups(XmlNode xml)
        {
            Check.ArgumentIsNull(xml, "xml");
            Check.Argument(xml.Name == "test-suite", "xml.Name == 'test-suite'");

            // ReSharper disable once PossibleNullReferenceException
            if (xml.Attributes["type"].Value == "TestFixture")
            {
                return new[] 
                {
                    new TestGroup
                    {
                        Name = xml.Attributes["name"].Value,
                        Result = xml.Attributes["result"].Value,
                        Duration = double.Parse(xml.Attributes["duration"].Value),
                        Total = int.Parse(xml.Attributes["total"].Value),
                        Passed = int.Parse(xml.Attributes["passed"].Value),
                        Failed = int.Parse(xml.Attributes["failed"].Value),
                        TestResults = CreateTestResults(xml)
                    }
                };
            }

            return
                xml.ChildNodes.Cast<XmlNode>()
                    .Where(n => n.Name == "test-suite")
                    .SelectMany(CreateTestGroups)
                    .ToArray();
        }

        private static TestResult[] CreateTestResults(XmlNode xml)
        {
            Check.ArgumentIsNull(xml, "xml");
            Check.Argument(xml.Name == "test-suite", "xml.Name == 'test-suite'");

            var childNodes = xml.ChildNodes.Cast<XmlNode>().ToArray();

            var testResults =
                childNodes
                    .Where(n => n.Name == "test-suite")
                    .SelectMany(CreateTestResults)
                    .ToArray();

            return
                childNodes
                    .Where(n => n.Name == "test-case")
                    .Select(CreateTestResult)
                    .Concat(testResults)
                    .ToArray();
        }

        private static TestResult CreateTestResult(XmlNode xml)
        {
            Check.ArgumentIsNull(xml, "xml");
            Check.Argument(xml.Name == "test-case", "xml.Name == 'test-case'");
            
            return new TestResult
            {
                // ReSharper disable once PossibleNullReferenceException
                Name = xml.Attributes["name"].Value,
                Result = xml.Attributes["result"].Value,
                Duration = double.Parse(xml.Attributes["duration"].Value),
                Message = xml["failure"]?["message"]?.InnerText,
                StackTrace = xml["failure"]?["stack-trace"]?.InnerText
            };
        }
    }
}
