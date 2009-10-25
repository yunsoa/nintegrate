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
        public void TestMappers()
        {
            var fac = new MapperFactory();
            //fac.ConfigureMapper<TestClass, TestClass>()
            //    .From(from => from.StringProperty)
            //    .To<string>((to, value) => to.WriteonlyStringValue = value)
            //    .From(from => from.IntField).From(from => from.StringProperty)
            //    .To<int, string>((to, value1, value2) => to.WriteonlyStringValue = value1 + value2);

            var intToNullableDecimalMapper = fac.GetMapper<int, decimal?>();
            Assert.AreEqual(1, intToNullableDecimalMapper(1));

            var intListToNullableDecimalArrayMapper = fac.GetMapper<IList<int>, decimal?[]>();
            var nullableDecimalArray = intListToNullableDecimalArrayMapper(new List<int> {1, 2, 3});
            Assert.AreEqual(2, nullableDecimalArray[1]);

            var intArrayToLongListMapper = fac.GetMapper<int[], List<long>>();
            var longList = intArrayToLongListMapper(new[] {1, 2, 3});
            Assert.AreEqual(2L, longList[1]);

            //fac.ConfigureMapper<string, double>()
            //    .From(from => from).To<string>((to, val) => double.Parse(val));
            //var stringListToDoubleArrayMapper = fac.GetMapper<List<string>, double[]>();
            //var doubleArray = stringListToDoubleArrayMapper(new List<string> { "1.1", "2.2", "3.3" });
            //Assert.AreEqual(2.2, doubleArray[1]);
        }

        private void temp(ref uint a, ref ulong b)
        {
            var aa = a;
            a = 1;
            var bb = b;
            b = 2L;

            int? i = 1;
            i = null;
            object obj = i;
        }

        private string temp2(MapperFactory fac)
        {
            return fac.GetMapper<int, string>()(1);
        }
    }
}
