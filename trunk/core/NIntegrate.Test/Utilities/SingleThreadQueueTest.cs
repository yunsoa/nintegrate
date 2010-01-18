using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NIntegrate.Test.Utilities.TestClasses;

namespace NIntegrate.Test.Utilities
{
    [TestClass]
    public class SingleThreadQueueTest
    {
        [TestMethod]
        public void TestSingleThreadQueue()
        {
            var logger = new SingleThreadLogger();
            for (var i = 0; i < 20; ++i)
            {
                logger.Enqueue(i.ToString());
            }
        }
    }
}
