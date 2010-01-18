using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NIntegrate.Test.Utilities.TestClasses;
using NIntegrate.Collections.Generic;

namespace NIntegrate.Test.Utilities
{
    [TestClass]
    public class LruItemCacheTest
    {
        [TestMethod]
        public void TestLruItemCache()
        {
            var cache = new LruItemCache<TestClass>(10, new TimeSpan(0, 0, 1), new TimeSpan(0, 1, 0), null);
            cache.AddIndex("testIndex", x => x.IntField, x => new TestClass { IntField = x, StringProperty = x.ToString() });
            Assert.AreEqual("1", cache.GetItem("testIndex", 1).StringProperty);
            cache.AddItem(new TestClass { IntField = 2, StringProperty = "test2" });
            Assert.AreEqual("test2", cache.GetItem("testIndex", 2).StringProperty);
        }
    }
}
