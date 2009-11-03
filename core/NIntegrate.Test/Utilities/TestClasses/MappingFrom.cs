using System;
namespace NIntegrate.Test.Utilities.TestClasses
{
    public enum MappingFromStatus
    {
        Value1 = 0,
        Value2 = 1
    }

    public class MappingFrom
    {
        public int FromID { get; set; }
        public string Name;

        public int Other;

        public MappingFromStatus Status;

        public Guid Guid;
    }
}
