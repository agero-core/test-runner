using System;

namespace Agero.TestsRunner.NUnit.Exceptions
{
    /// <summary>Test run exception</summary>
    [Serializable]
    public class TestRunException : Exception
    {
        /// <summary>Constructor</summary>
        public TestRunException(string message)
            : base(message)
        {
        }
    }
}
