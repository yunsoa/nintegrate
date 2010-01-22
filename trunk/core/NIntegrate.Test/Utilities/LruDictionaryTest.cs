using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NIntegrate.Collections.Generic;

namespace NIntegrate.Test.Utilities
{
    [TestClass]
    public class LruDictionaryTest
    {
        [TestMethod]
        public void TestLruDictionary()
        {
            var dic = new LruDictionary<int, string>(10);
            for (var i = 0; i < 12; ++i)
            {
                dic[i] = i.ToString();
            }
            //the dic[1] is null because the item exceeded the capacity of the dictionary and is not latest visited
            Assert.AreEqual(null, dic[1]);
            Assert.AreEqual("3", dic[3]);
            Assert.AreEqual("4", dic[4]);
            Assert.AreEqual("3", dic[3]);
        }

        [TestMethod]
        public void TestLruDependingDictionary()
        {
            var dic = new LruDependingDictionary<int, string>(10);
            for (var i = 0; i < 6; ++i)
            {
                dic[new DependingKey<int>(i, "testDependency")] = i.ToString();
            }
            for (var i = 6; i < 12; ++i)
            {
                dic[new DependingKey<int>(i, "testDependency2")] = i.ToString();
            }
            Assert.AreEqual(null, dic[1]);
            Assert.AreEqual("3", dic[3]);
            Assert.AreEqual("4", dic[4]);
            Assert.AreEqual("3", dic[3]);
            dic.NotifyDependencyChanged("testDependency");
            //dic[3] is null because the dependency is changed
            Assert.AreEqual(null, dic[3]);
            dic.NotifyDependencyChanged("Test", true, true);
            //all items whose dependency containing "test" is changed, so they are all expired
            Assert.AreEqual(0, dic.Count);
        }
    }
}
