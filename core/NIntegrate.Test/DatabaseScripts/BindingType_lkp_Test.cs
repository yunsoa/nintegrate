using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NIntegrate.Query.Command;
using NIntegrate.Test.DatabaseScripts.Criterias;
using System.Data;

namespace NIntegrate.Test.DatabaseScripts
{
    [TestClass]
    public class BindingType_lkp_Test
    {
        [TestMethod]
        public void TestBindingTypesInitialization()
        {
            var criteria = new BindingTypeCriteria();
            var cmdFac = new QueryCommandFactory(criteria);
            using (var cmd = cmdFac.GetQueryCommand())
            {
                try
                {
                    cmd.Connection.Open();
                    using (IDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Type type = null;
                            type = Type.GetType(rdr.GetString(rdr.GetOrdinal(criteria.BindingTypeClassName.ColumnName)));
                            Assert.IsNotNull(type);
                            Assert.IsNotNull(Activator.CreateInstance(type));
                            type = Type.GetType(rdr.GetString(rdr.GetOrdinal(criteria.BindingConfigurationElementTypeClassName.ColumnName)));
                            Assert.IsNotNull(type);
                            Assert.IsNotNull(Activator.CreateInstance(type));
                        }
                    }
                }
                finally
                {
                    cmd.Connection.Close();
                }
            }
        }
    }
}
