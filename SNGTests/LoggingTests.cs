using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialNetworkGraph.Utilities;
using System.Threading.Tasks;
using System.Threading;

namespace SNGTests
{
    [TestClass]
    public class LoggingTests
    {
        [TestMethod]
        public void StressTest()
        {
            for (int i = 0; i < 1000; i++)
                ExceptionLogger.Instance.LogFile(i.ToString());
            Thread.Sleep(7000);
        }
    }
}
