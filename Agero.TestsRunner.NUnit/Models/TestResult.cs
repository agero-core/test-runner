using System.Runtime.Serialization;

namespace Agero.TestsRunner.NUnit.Models
{
    /// <summary>Test run response</summary>
    [DataContract(Name = "testResult")]
    public class TestResult
    {
        /// <summary>Test name or test method name</summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>Overall run result, ex. "Passed" or "Failed"</summary>
        [DataMember(Name = "result")]
        public string Result { get; set; }

        /// <summary>Duration of run</summary>
        [DataMember(Name = "duration")]
        public double Duration { get; set; }

        /// <summary>Test result message. Usually it is null if test passed.</summary>
        [DataMember(Name = "message")]
        public string Message { get; set; }

        /// <summary>Test result call stack. Usually it is null if test passed.</summary>
        [DataMember(Name = "stackTrace")]
        public string StackTrace { get; set; }
    }
}
