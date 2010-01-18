using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NIntegrate.Threading;

namespace NIntegrate.Test.Utilities.TestClasses
{
    public class SingleThreadLogger : SingleThreadQueue<string>
    {
        public SingleThreadLogger()
            : base(10)
        {
        }

        protected override void Process(string item)
        {
            Console.WriteLine(item);
        }
    }
}
