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
            var intToNullableDecimalMapper = fac.GetMapper<int, decimal?>();
            Assert.AreEqual(1, intToNullableDecimalMapper(1));

            var intListToNullableDecimalArrayMapper = fac.GetMapper<IList<int>, decimal?[]>();
            var nullableDecimalArray = intListToNullableDecimalArrayMapper(new List<int> {1, 2, 3});
            Assert.AreEqual(2, nullableDecimalArray[1]);

            var intArrayToLongListMapper = fac.GetMapper<int[], List<long>>();
            var longList = intArrayToLongListMapper(new[] {1, 2, 3});
            Assert.AreEqual(2L, longList[1]);

            fac.ConfigureMapper<string, double>()
                .From(from => from).To<string>((to, val) => double.Parse(val));
            var stringListToDoubleArrayMapper = fac.GetMapper<List<string>, double[]>();
            var doubleArray = stringListToDoubleArrayMapper(new List<string> { "1.1", "2.2", "3.3" });
            Assert.AreEqual(2.2, doubleArray[1]);

            fac.ConfigureMapper<MappingFrom, MappingTo>(true, true, true)
                .From(from => from.Other)
                .To<double>(
                (to, val) =>
                    {
                        to.Other2 = val.ToString();
                        return to;
                    });
            var customMapper = fac.GetMapper<MappingFrom, MappingTo>();
            var customFrom = new MappingFrom { FromID = 1, Name = "name" };
            var customTo = customMapper(customFrom);
            Assert.AreEqual(1, customTo.From_id);
            Assert.AreEqual("name", customTo.Name);
            Assert.AreEqual("0", customTo.Other2);
        }

        private void temp(ref uint a, ref ulong b, ref MappingTo to)
        {
            var aa = a;
            a = 1;
            var bb = b;
            b = 2L;

            int? i = 1;
            i = null;
            object obj = i;

            var c = to;
            c.From_id = 1;
            var d = c.From_id;
            to = c;
        }

        private string temp2(MapperFactory fac)
        {
            return fac.GetMapper<int, string>()(1);
        }
    }
}
