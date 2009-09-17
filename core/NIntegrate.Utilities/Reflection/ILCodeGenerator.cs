using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Reflection.Emit;

namespace NIntegrate.Utilities.Reflection
{
    /// <summary>
    /// Helper class to simplify method body emitting
    /// </summary>
    public sealed class ILCodeGenerator
    {
        private readonly ILGenerator _gen;

        #region Constructors

        private ILCodeGenerator(ILGenerator gen)
        {
            _gen = gen;
        }

        #endregion

        #region Factory Methods

        public static ILCodeGenerator CreateDynamicMethodCodeGenerator(
            string methodName, Type delegateType, out DynamicMethod method)
        {
            if (string.IsNullOrEmpty(methodName))
                throw new ArgumentNullException("methodName");
            if (delegateType == null)
                throw new ArgumentNullException("delegateType");
            if (!typeof(Delegate).IsAssignableFrom(delegateType))
                throw new ArgumentException(
                    delegateType.FullName + " is not a delegate type");

            //...

            method = null;
            return new ILCodeGenerator(method.GetILGenerator());
        }

        public static ILCodeGenerator CreateDefaultConstructorCodeGenerator(ConstructorInfo defaultConstructor, TypeBuilder typeBuilder)
        {
            if (defaultConstructor == null)
                throw new ArgumentNullException("defaultConstructor");
            var parameters = defaultConstructor.GetParameters();
            if (parameters.Length == 0)
                throw new ArgumentException("Specified defaultConstructor is not a default contractor");
            if (typeBuilder == null)
                throw new ArgumentNullException("typeBuilder");

            var builder = typeBuilder.DefineDefaultConstructor(defaultConstructor.Attributes);
            return new ILCodeGenerator(builder.GetILGenerator());
        }

        public static ILCodeGenerator CreateConstructorCodeGenerator(ConstructorInfo constructor, TypeBuilder typeBuilder)
        {
            if (constructor == null)
                throw new ArgumentNullException("constructor");
            if (typeBuilder == null)
                throw new ArgumentNullException("typeBuilder");

            var parameters = constructor.GetParameters();
            var builder = typeBuilder.DefineConstructor(
                constructor.Attributes, constructor.CallingConvention,
                GetParameterTypes(parameters));
            return new ILCodeGenerator(builder.GetILGenerator());
        }

        #endregion

        #region Private Methods

        private static Type[] GetParameterTypes(ParameterInfo[] parameters)
        {
            var result = new Type[parameters.Length];
            for (var i = 0; i < parameters.Length; ++i)
            {
                result[i] = parameters[i].ParameterType;
            }
            return result;
        }

        #endregion
    }
}
