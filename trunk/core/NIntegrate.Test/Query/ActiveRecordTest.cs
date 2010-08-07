﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NIntegrate.Test.Query.TestClasses;
using System.Runtime.Serialization;
using System.IO;
using NIntegrate.Data;

namespace NIntegrate.Test.Query
{
    /// <summary>
    /// Summary description for ActiveRecordTest
    /// </summary>
    [TestClass]
    public class ActiveRecordTest
    {
        [TestMethod]
        public void TestActiveRecord()
        {
            Farm farm = new Farm();
            farm.Attach(new NIntegrate.Data.QueryCommandFactory());

            //test insert
            farm.Farm_id = 1000;
            farm.FarmAddress = "Test Farm";
            farm.LoadBalancePath = "/TestPath/";
            Assert.IsTrue(farm.IsNew());
            Assert.AreEqual(true, farm.Save());
            Assert.IsFalse(farm.IsNew());

            //test find
            var loadFarm = farm.FindOne(farm.GetObjectId());
            Assert.AreEqual(farm.Farm_id, loadFarm.Farm_id);
            Assert.AreEqual(farm.FarmAddress, loadFarm.FarmAddress);
            Assert.AreEqual(farm.LoadBalancePath, loadFarm.LoadBalancePath);

            //test update
            farm.LoadBalancePath = "modified";
            farm.Save();

            //test find
            loadFarm = farm.FindOne(farm.GetObjectId());
            Assert.AreEqual(farm.Farm_id, loadFarm.Farm_id);
            Assert.AreEqual(farm.FarmAddress, loadFarm.FarmAddress);
            Assert.AreEqual(farm.LoadBalancePath, loadFarm.LoadBalancePath);

            //test findMany
            var loadManyFarms = farm.FindMany(Farm.Q.Select().SortBy(Farm.Q.Farm_id, true));
            Assert.IsTrue(loadManyFarms.Count > 0);
            loadFarm = loadManyFarms.First(f => f.Farm_id == farm.Farm_id);
            Assert.AreEqual(farm.FarmAddress, loadFarm.FarmAddress);
            Assert.AreEqual(farm.LoadBalancePath, loadFarm.LoadBalancePath);

            //test delete
            farm.Delete();
            Assert.IsTrue(farm.IsNew());
            loadFarm = farm.FindOne(farm.GetObjectId());
            Assert.IsNull(loadFarm);

            //test activerecord serialization
            var serializer = new DataContractSerializer(typeof(Farm));
            var stream = new MemoryStream();
            serializer.WriteObject(stream, farm);
            stream.Seek(0, SeekOrigin.Begin);
            var deserializedFarm = (Farm)serializer.ReadObject(stream);

            Assert.AreEqual(farm.Farm_id, deserializedFarm.Farm_id);
            Assert.AreEqual(farm.FarmAddress, deserializedFarm.FarmAddress);
            Assert.AreEqual(farm.LoadBalancePath, deserializedFarm.LoadBalancePath);
            Assert.IsFalse(deserializedFarm.IsAttached());
            Assert.IsTrue(deserializedFarm.IsNew());

            //test save/delete after deserialization
            Assert.IsFalse(deserializedFarm.Save());
            Assert.IsFalse(deserializedFarm.Delete());
            deserializedFarm.Attach(new QueryCommandFactory());
            Assert.IsTrue(deserializedFarm.Save());
            Assert.IsTrue(deserializedFarm.Delete());

            //test ObjectId serialization
            var objectId = farm.GetObjectId();
            serializer = new DataContractSerializer(typeof(ObjectId<Farm, TestClasses.QueryClasses.Farm>));
            stream = new MemoryStream();
            serializer.WriteObject(stream, objectId);
            stream.Seek(0, SeekOrigin.Begin);
            var deserializedObjectId = (ObjectId<Farm, TestClasses.QueryClasses.Farm>)serializer.ReadObject(stream);

            Assert.AreEqual(objectId.AutoGenerated, deserializedObjectId.AutoGenerated);
        }
    }
}
