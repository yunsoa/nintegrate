﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NIntegrate.Utilities.Mapping;
using NIntegrate.Test.Utilities.TestClasses;
using System.Data;

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

            var dt = new DataTable("table");
            dt.Columns.Add(new DataColumn("FromID", typeof(int)));
            dt.Columns.Add(new DataColumn("Name", typeof(string)));
            dt.Columns[1].AllowDBNull = true;
            dt.Columns.Add(new DataColumn("Other", typeof(int)));
            dt.Rows.Add(1, "name", 0);

            fac.ConfigureMapper<DataRow, MappingTo>(true, true, true)
                .From(from => (int) from["Other"])
                .To<double>(
                (to, val) =>
                    {
                        to.Other2 = val.ToString();
                        return to;
                    }
                );
            var dataRowToCustomMapper = fac.GetMapper<DataRow, MappingTo>();
            customTo = dataRowToCustomMapper(dt.Rows[0]);
            Assert.AreEqual(1, customTo.From_id);
            Assert.AreEqual("name", customTo.Name);
            Assert.AreEqual("0", customTo.Other2);
            dt.Rows[0]["name"] = DBNull.Value;
            customTo = dataRowToCustomMapper(dt.Rows[0]);
            Assert.AreEqual(null, customTo.Name);

            var dataTableToCustomCollectionMapper = fac.GetMapper<DataTable, List<MappingTo>>();
            var customToCollection = dataTableToCustomCollectionMapper(dt);
            Assert.AreEqual(1, customToCollection.Count);
            Assert.AreEqual(1, customToCollection[0].From_id);
            Assert.AreEqual(null, customToCollection[0].Name);
            Assert.AreEqual("0", customToCollection[0].Other2);

            fac.ConfigureMapper<DataTableReader, MappingTo>(true, true, true)
                .From(from => from.GetInt32(from.GetOrdinal("Other")))
                .To<double>(
                (to, val) =>
                    {
                        to.Other2 = val.ToString();
                        return to;
                    }
                );
            var dataReaderToCustomMapper = fac.GetMapper<DataTableReader, MappingTo>();
            var rdr = dt.CreateDataReader();
            rdr.Read();
            customTo = dataReaderToCustomMapper(rdr);
            rdr.Close();
            Assert.AreEqual(1, customTo.From_id);
            Assert.AreEqual(null, customTo.Name);
            Assert.AreEqual("0", customTo.Other2);

            var dataReaderToCustomCollectionMapper = fac.GetMapper<DataTableReader, List<MappingTo>>();
            customToCollection = dataReaderToCustomCollectionMapper(dt.CreateDataReader());
            Assert.AreEqual(1, customToCollection.Count);
            Assert.AreEqual(1, customToCollection[0].From_id);
            Assert.AreEqual(null, customToCollection[0].Name);
            Assert.AreEqual("0", customToCollection[0].Other2);

            var dataReaderToCustomArrayMapper = fac.GetMapper<DataTableReader, MappingTo[]>();
            var customToArray = dataReaderToCustomArrayMapper(dt.CreateDataReader());
            Assert.AreEqual(1, customToArray.Length);
            Assert.AreEqual(1, customToArray[0].From_id);
            Assert.AreEqual(null, customToArray[0].Name);
            Assert.AreEqual("0", customToArray[0].Other2);
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
            object obj = 1;
            int? i = (int?) obj;
            return fac.GetMapper<int, string>()(1);
        }
    }
}
