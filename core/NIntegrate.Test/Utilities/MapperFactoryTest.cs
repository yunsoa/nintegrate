using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NIntegrate.Utilities.Mapping;
using NIntegrate.Test.Utilities.TestClasses;

namespace NIntegrate.Test.Utilities
{
    [TestClass]
    public class MapperFactoryTest
    {
        [TestMethod]
        public void TestConfigureMapper()
        {
            var fac = new MapperFactory();
            fac.ConfigureMapper<TestClass, TestClass>()
                .From(from => from.StringProperty)
                .To<string>((to, value) => to.WriteonlyStringValue = value)
                .From(from => from.IntField).From(from => from.StringProperty)
                .To<int, string>((to, value1, value2) => to.WriteonlyStringValue = value1 + value2);
        }
    }
}
