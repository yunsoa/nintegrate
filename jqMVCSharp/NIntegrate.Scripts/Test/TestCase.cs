using System;
using ScriptFX;

namespace NIntegrate.Scripts.Test
{
    public abstract class TestCase
    {
        public virtual void Execute()
        {
            QUnit.Module(this.GetType().FullName);
        }
    }
}
