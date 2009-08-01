using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NIntegrate.Data;
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
            var table = new BindingTypeTable();
            var criteria = table.CreateCriteria();
            var cmdFac = new QueryCommandFactory();
            using (var cmd = cmdFac.CreateCommand(criteria))
            {
                try
                {
                    cmd.Connection.Open();
                    using (IDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Type type = Type.GetType(rdr.GetString(rdr.GetOrdinal(table.BindingTypeClassName.ColumnName)));
                            Assert.IsNotNull(type);
                            Assert.IsNotNull(Activator.CreateInstance(type));
                            type = Type.GetType(rdr.GetString(rdr.GetOrdinal(table.BindingConfigurationElementTypeClassName.ColumnName)));
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
