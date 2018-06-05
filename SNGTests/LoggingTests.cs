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
        public void RandomError()
        {
            Random rnd = new Random();
            ExceptionLogger.Instance.LogFile(rnd.Next().ToString());
        }
    }
}
