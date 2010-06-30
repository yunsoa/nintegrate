using System;
using ScriptFX;

namespace NIntegrate.Scripts
{
    public delegate void DoSomethingHandler();
    public delegate object ReturnSomethingHandler(Array args);
    public delegate void ProcessSomethingHandler(object data);
}
