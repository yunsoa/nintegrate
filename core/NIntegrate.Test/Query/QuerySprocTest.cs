using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NIntegrate.Data;
using NIntegrate.Test.Query.TestClasses;

namespace NIntegrate.Test.Query
{
    /// <summary>
    /// Summary description for QuerySprocTest
    /// </summary>
    [TestClass]
    public class QuerySprocTest
    {
        [TestMethod]
        public void TestQuerySproc()
        {
            var sproc = new TestSproc();
            var criteria = sproc.CreateSprocCriteria(
                sproc.p1 == 1,
                sproc.p2 == 2,
                sproc.p3 == null);
            using (var cmd = new QueryCommandFactory().CreateCommand(criteria, false))
            {
                using (var conn = cmd.Connection)
                {
                    try
                    {
                        conn.Open();

                        cmd.ExecuteNonQuery();

                        //assert
                        var sprocCmd = cmd as SprocDbCommand;
                        Assert.AreEqual(1, sprocCmd.GetOutputParameterValue(sproc.p2));
                        Assert.AreEqual(3, sprocCmd.GetOutputParameterValue(sproc.p3));
                    }
                    finally
                    {
                        conn.Close();
                    }

                }
            }
        }
    }
}
