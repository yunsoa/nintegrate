using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NIntegrate.Data;
using System.Runtime.Serialization;

namespace NIntegrate.Test.Query.TestClasses
{
    namespace QueryClasses
    {
        public partial class Farm : NIntegrate.Data.QueryTable
        {

            private NIntegrate.Data.Int32Column _Farm_id = new NIntegrate.Data.Int32Column("Farm_id");

            private NIntegrate.Data.StringColumn _FarmAddress = new NIntegrate.Data.StringColumn("FarmAddress", true);

            private NIntegrate.Data.StringColumn _LoadBalancePath = new NIntegrate.Data.StringColumn("LoadBalancePath", true);

            public Farm() :
                base("Farm", "ActiveRecordTest", false)
            {
            }

            public NIntegrate.Data.Int32Column Farm_id
            {
                get
                {
                    return this._Farm_id;
                }
            }

            public NIntegrate.Data.StringColumn FarmAddress
            {
                get
                {
                    return this._FarmAddress;
                }
            }

            public NIntegrate.Data.StringColumn LoadBalancePath
            {
                get
                {
                    return this._LoadBalancePath;
                }
            }
        }
    }

    [DataContract]
    public class Farm : ActiveRecord<Farm, QueryClasses.Farm>
    {
        [DataMember]
        public int FarmID { get; set; }
        [DataMember]
        public string FarmAddress { get; set; }
        [DataMember]
        public string LoadBalancePath { get; set; }

        public override ObjectId<Farm, QueryClasses.Farm> GetObjectId()
        {
            return new ObjectId<Farm, QueryClasses.Farm>(Farm.Q.Farm_id == FarmID, false);
        }

        public override Assignment[] GetSaveAssignments()
        {
            return new Assignment[] { Farm.Q.Farm_id.Set(FarmID), Farm.Q.FarmAddress.Set(FarmAddress), Farm.Q.LoadBalancePath.Set(LoadBalancePath) };
        }
    }
}
