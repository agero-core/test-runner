using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnitTests;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Agero.TestsRunner.NUnit.Tests
{
    [TestClass]
    public class TestServiceTests
    {
        [TestMethod]
        public void Run()
        {
            // Arrange
            var testService = new TestService();

            var currentFolder = Directory.GetCurrentDirectory();
            var assemblyPath = Path.Combine(currentFolder, "NUnitTests.dll");
            var parameters = new Dictionary<string, string> { { "key1", "value1" }, { "key2", "value2" } };

            // Act
            var response = testService.Run(assemblyPath, null, parameters);

            // Assert
            Assert.IsTrue(response.Duration > 0, "response.Duration > 0");
            Assert.AreEqual("Failed", response.Result);
            Assert.AreEqual(2, response.Total);
            Assert.AreEqual(1, response.Passed);
            Assert.AreEqual(1, response.Failed);
            Assert.AreEqual(1, response.TestGroups.Length);

            var testGroup = response.TestGroups[0];
            Assert.AreEqual(nameof(NUnitTestClass), testGroup.Name);
            Assert.IsTrue(testGroup.Duration > 0, "testGroup.Duration > 0");
            Assert.AreEqual("Failed", testGroup.Result);
            Assert.AreEqual(2, testGroup.Total);
            Assert.AreEqual(1, testGroup.Passed);
            Assert.AreEqual(1, testGroup.Failed);
            Assert.AreEqual(2, testGroup.TestResults.Length);

            var passedTest = testGroup.TestResults.SingleOrDefault(t => t.Name == nameof(NUnitTestClass.PassedTest));
            Assert.IsNotNull(passedTest);
            Assert.IsTrue(passedTest.Duration > 0, "passedTest.Duration > 0");
            Assert.AreEqual("Passed", passedTest.Result);
            Assert.IsNull(passedTest.Message);
            Assert.IsNull(passedTest.StackTrace);

            var failedTest = testGroup.TestResults.SingleOrDefault(t => t.Name == nameof(NUnitTestClass.FailedTest));
            Assert.IsNotNull(failedTest);
            Assert.IsTrue(failedTest.Duration > 0, "failedTest.Duration > 0");
            Assert.AreEqual("Failed", failedTest.Result);
            Assert.AreEqual("Error", failedTest.Message);
        }
    }
}
