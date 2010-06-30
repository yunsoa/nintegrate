using System;
using ScriptFX;

namespace NIntegrate.Scripts.Test.Demo.GoogleTracer
{
    public static class Container
    {
        private static Dictionary _cache = new Dictionary();

        public static void RegisterInstance(Type type, object instance)
        {
            _cache[type.FullName] = instance;
        }

        public static object GetInstance(Type type)
        {
            return _cache[type.FullName];
        }
    }
}
