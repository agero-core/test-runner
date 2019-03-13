using System;
using NUnit.Engine;

namespace Agero.TestsRunner.NUnit.Helpers
{
    [Serializable]
    internal class EmptyTestEventListener : ITestEventListener
    {
        public void OnTestEvent(string result)
        {
        }
    }
}
