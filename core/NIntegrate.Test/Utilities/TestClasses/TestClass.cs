using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NIntegrate.Test.Utilities.TestClasses
{
    public class TestClass
    {
        public int IntField = 0;
        public string StringProperty { get; set; }
        private const int _readonlyIntValue = 1;
        public static int ReadonlyIntValue
        {
            get { return _readonlyIntValue; }
        }

        private string _writeonlyStringValue;
        public string WriteonlyStringValue
        {
            set { _writeonlyStringValue = value; }            
        }

        public string GetWriteonlyStringValue()
        {
            return _writeonlyStringValue;
        }
    }
}
