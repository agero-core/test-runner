using System.Runtime.Serialization;

namespace Agero.TestsRunner.NUnit.Models
{
    /// <summary>Test assembly run response</summary>
    [DataContract(Name = "response")]
    public class TestRunResponse
    {
        /// <summary>Overall run result, ex. "Passed" or "Failed"</summary>
        [DataMember(Name = "result")]
        public string Result { get; set; }

        /// <summary>Duration of run</summary>
        [DataMember(Name = "duration")]
        public double Duration { get; set; }

        /// <summary>Number of tests</summary>
        [DataMember(Name = "total")]
        public int Total { get; set; }

        /// <summary>Number of passed tests</summary>
        [DataMember(Name = "passed")]
        public int Passed { get; set; }

        /// <summary>Number of failed tests</summary>
        [DataMember(Name = "failed")]
        public int Failed { get; set; }

        /// <summary>Test group results or test class results</summary>
        [DataMember(Name = "testGroups")]
        public TestGroup[] TestGroups { get; set; }
    }
}
