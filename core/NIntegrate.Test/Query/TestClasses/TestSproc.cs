using NIntegrate.Data;

namespace NIntegrate.Test.Query.TestClasses
{
    public class TestSproc : QuerySproc
    {
        public TestSproc()
            : base("TestSproc", "Test")
        {
        }

        public Int32ParameterExpression p1 = new Int32ParameterExpression("p1", SprocParameterDirection.Input);

        public Int32ParameterExpression p2 = new Int32ParameterExpression("p2", SprocParameterDirection.InputOutput);

        public Int32ParameterExpression p3 = new Int32ParameterExpression("p3", SprocParameterDirection.ReturnValue);
    }
}
