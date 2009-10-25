using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using NIntegrate.Utilities.Reflection;
using System.Reflection.Emit;

namespace NIntegrate.Utilities.Mapping
{
    public delegate TValue MappingFrom<TFrom, TValue>(TFrom from);
    public delegate TTo MappingTo<TTo, TValue>(TTo to, TValue value);
    public delegate TTo MappingTo2<TTo, TValue1, TValue2>(TTo to, TValue1 value1, TValue2 value2);
    public delegate TTo MappingTo3<TTo, TValue1, TValue2, TValue3>(TTo to, TValue1 value1, TValue2 value2, TValue3 value3);

    public abstract class MapperBuilder
    {
        internal abstract MapperCacheKey GetCacheKey();

        internal abstract Delegate BuildMapper();
    }

    public class MapperBuilder<TFrom, TTo> : MapperBuilder
    {
        private readonly bool _isToArray;
        private readonly bool _isToCollection;
        private readonly List<Delegate> _mappingChain = new List<Delegate>();

        #region Constructors

        internal MapperBuilder(bool autoMap, bool ignoreCase, bool ignoreUnderscore)
        {
            if (typeof(IEnumerable).IsAssignableFrom(typeof(TFrom)) || typeof(IDataReader).IsAssignableFrom(typeof(TFrom)))
            {
                if (typeof(TTo).IsArray)
                    _isToArray = true;
                else if (typeof(Collection<>).MakeGenericType(GetElementType(typeof(TTo))).IsAssignableFrom(typeof(TTo)))
                    _isToCollection = true;
            }
        }

        #endregion

        #region Public Methods

        public MapperBuilder<TFrom, TTo> From<TValue>(MappingFrom<TFrom, TValue> from)
        {
            if (from == null)
                throw new ArgumentNullException("from");

            _mappingChain.Add(from);

            return this;
        }

        public MapperBuilder<TFrom, TTo> To<TValue>(MappingTo<TTo, TValue> to)
        {
            if (to == null)
                throw new ArgumentNullException("to");

            _mappingChain.Add(to);

            return this;
        }

        public MapperBuilder<TFrom, TTo> To<TValue1, TValue2>(MappingTo2<TTo, TValue1, TValue2> to)
        {
            if (to == null)
                throw new ArgumentNullException("to");

            _mappingChain.Add(to);

            return this;
        }

        public MapperBuilder<TFrom, TTo> To<TValue1, TValue2, TValue3>(MappingTo3<TTo, TValue1, TValue2, TValue3> to)
        {
            if (to == null)
                throw new ArgumentNullException("to");

            _mappingChain.Add(to);

            return this;
        }

        public static void ExecuteFromTo<TFromValue, TToValue>(
            MapperFactory fac, TFrom fromObj, ref TTo toObj, 
            MappingFrom<TFrom, TFromValue> from, MappingTo<TTo, TToValue> to)
        {
            if (fac == null)
                throw new ArgumentNullException("fac");
            if (Equals(fromObj, default(TFrom)))
                throw new ArgumentNullException("fromObj");
            if (Equals(toObj, default(TTo)))
                throw new ArgumentNullException("toObj");
            if (from == null)
                throw new ArgumentNullException("from");
            if (to == null)
                throw new ArgumentNullException("to");

            var fromValue = from(fromObj);
            if (typeof(TFromValue) != typeof(TToValue))
            {
                var valueMapper = fac.GetMapper<TFromValue, TToValue>();
                toObj = to(toObj, valueMapper(fromValue));
            }
            else
            {
                toObj = to(toObj, (TToValue) (object) fromValue);
            }
        }

        //deal with MappingTo2 & MappingTo3
        //...

        #endregion

        #region Non-Public Methods

        internal sealed override MapperCacheKey GetCacheKey()
        {
            return new MapperCacheKey(typeof(TFrom), typeof(TTo));
        }

        internal sealed override Delegate BuildMapper()
        {
            Delegate result = null;

            if (_isToArray)
                result = MapToArray();
            else if (_isToCollection)
                result = MapToCollection();
            else
                result = MapToObject();

            return result;
        }

        private Delegate MapToArray()
        {
            var resultDelegate = typeof (InternalMapper<TFrom, TTo>);
            DynamicMethod toArrayMethod;
            var gen = ILCodeGenerator.CreateDynamicMethodCodeGenerator(
                "m" + Guid.NewGuid().ToString("N"), 
                resultDelegate,
                out toArrayMethod);

            var fromElementType = GetElementType(typeof(TFrom));
            var toElementType = GetElementType(typeof(TTo));
            var listType = typeof(List<>).MakeGenericType(toElementType);
            var list = gen.DeclareLocalVariable(listType);
            var en = gen.DeclareLocalVariable(typeof (IEnumerator));
            var foreachBegin = gen.DefineLabel();
            var foreachEnd = gen.DefineLabel();
            gen.StoreLocalVariable(
                list,
                val => val.New(listType.GetConstructor(Type.EmptyTypes)));
            gen.StoreLocalVariable(
                en,
                val => val.CallMethod(
                           thisObj => thisObj.ConvertValue(
                                          sourceVal => sourceVal.LoadArgument(1), 
                                          typeof(TFrom), 
                                          typeof(IEnumerable)),
                           typeof(IEnumerable).GetMethod("GetEnumerator")));
            gen.MarkLabel(foreachBegin);
            gen.If(boolVal => boolVal.CallMethod(
                                  thisObj => thisObj.LoadLocalVariable(en),
                                  typeof (IEnumerator).GetMethod("MoveNext")));
            if (fromElementType == typeof(IDataReader))
            {
                throw new NotImplementedException();
            }
            else if (fromElementType.IsValueType)
            {
                gen.CallMethod(
                    thisObj => thisObj.LoadLocalVariable(list),
                    listType.GetMethod("Add"),
                    val1 => val1.CallMethod(
                                thisObj2 => thisObj2.CallMethod(
                                                thisObj3 => thisObj3.LoadArgument(0),
                                                typeof(MapperFactory).GetMethod("GetMapper").MakeGenericMethod(fromElementType, toElementType)),
                                typeof(Mapper<,>).MakeGenericType(fromElementType, toElementType).GetMethod("Invoke"),
                                valFrom => valFrom.UnboxAny(
                                               fromElementType,
                                               val => val.LoadProperty(
                                                          thisObj2 => thisObj2.LoadLocalVariable(en),
                                                          typeof (IEnumerator).GetProperty("Current")))));
            }
            else
            {
                gen.CallMethod(
                    thisObj => thisObj.LoadLocalVariable(list),
                    listType.GetMethod("Add"),
                    val1 => val1.CallMethod(
                                thisObj2 => thisObj2.CallMethod(
                                                thisObj3 => thisObj3.LoadArgument(0),
                                                typeof(MapperFactory).GetMethod("GetMapper").MakeGenericMethod(fromElementType, toElementType)),
                                typeof(Mapper<,>).MakeGenericType(fromElementType, toElementType).GetMethod("Invoke"),
                                valFrom => valFrom.ConvertValue(
                                               sourceVal => sourceVal.LoadProperty(
                                                                thisObj2 => thisObj2.LoadLocalVariable(en),
                                                                typeof(IEnumerator).GetProperty("Current")),
                                               typeof(object),
                                               fromElementType)));
            }
            gen.GoTo(foreachBegin);
            gen.Else();
            gen.GoTo(foreachEnd);
            gen.EndIf();
            gen.MarkLabel(foreachEnd);
            gen.StoreArgumentIndirectly(
                2,
                typeof (TTo),
                val => val.CallMethod(
                           thisObj => thisObj.LoadLocalVariable(list),
                           listType.GetMethod("ToArray")));
            gen.Ret();

            var result = toArrayMethod.CreateDelegate(resultDelegate);
            return result;
        }

        private Delegate MapToCollection()
        {
            var resultDelegate = typeof(InternalMapper<TFrom, TTo>);
            DynamicMethod toCollectionMethod;
            var gen = ILCodeGenerator.CreateDynamicMethodCodeGenerator(
                "m" + Guid.NewGuid().ToString("N"),
                resultDelegate,
                out toCollectionMethod);

            var fromElementType = GetElementType(typeof(TFrom));
            var toElementType = GetElementType(typeof(TTo));
            var collectionType = typeof(Collection<>).MakeGenericType(toElementType);
            var collection = gen.DeclareLocalVariable(typeof(TTo));
            var en = gen.DeclareLocalVariable(typeof(IEnumerator));
            var foreachBegin = gen.DefineLabel();
            var foreachEnd = gen.DefineLabel();
            gen.StoreLocalVariable(
                collection,
                val => val.LoadArgument(2));
            gen.StoreLocalVariable(
                en,
                val => val.CallMethod(
                           thisObj => thisObj.ConvertValue(
                                          sourceVal => sourceVal.LoadArgument(1),
                                          typeof(TFrom),
                                          typeof(IEnumerable)),
                           typeof(IEnumerable).GetMethod("GetEnumerator")));
            gen.MarkLabel(foreachBegin);
            gen.If(boolVal => boolVal.CallMethod(
                                  thisObj => thisObj.LoadLocalVariable(en),
                                  typeof(IEnumerator).GetMethod("MoveNext")));
            if (fromElementType == typeof(IDataReader))
            {
                throw new NotImplementedException();
            }
            else if (fromElementType.IsValueType)
            {
                gen.CallMethod(
                    thisObj => thisObj.LoadLocalVariable(collection),
                    collectionType.GetMethod("Add"),
                    val1 => val1.CallMethod(
                                thisObj2 => thisObj2.CallMethod(
                                                thisObj3 => thisObj3.LoadArgument(0),
                                                typeof(MapperFactory).GetMethod("GetMapper").MakeGenericMethod(fromElementType, toElementType)),
                                typeof(Mapper<,>).MakeGenericType(fromElementType, toElementType).GetMethod("Invoke"),
                                valFrom => valFrom.UnboxAny(
                                               fromElementType,
                                               val => val.LoadProperty(
                                                          thisObj2 => thisObj2.LoadLocalVariable(en),
                                                          typeof(IEnumerator).GetProperty("Current")))));
            }
            else
            {
                gen.CallMethod(
                    thisObj => thisObj.LoadLocalVariable(collection),
                    collectionType.GetMethod("Add"),
                    val1 => val1.CallMethod(
                                thisObj2 => thisObj2.CallMethod(
                                                thisObj3 => thisObj3.LoadArgument(0),
                                                typeof(MapperFactory).GetMethod("GetMapper").MakeGenericMethod(fromElementType, toElementType)),
                                typeof(Mapper<,>).MakeGenericType(fromElementType, toElementType).GetMethod("Invoke"),
                                valFrom => valFrom.ConvertValue(
                                               sourceVal => sourceVal.LoadProperty(
                                                                thisObj2 => thisObj2.LoadLocalVariable(en),
                                                                typeof(IEnumerator).GetProperty("Current")),
                                               typeof(object),
                                               fromElementType)));
            }
            gen.GoTo(foreachBegin);
            gen.Else();
            gen.GoTo(foreachEnd);
            gen.EndIf();
            gen.MarkLabel(foreachEnd);
            ExecuteMappingChain(gen);
            gen.StoreArgumentIndirectly(
                2,
                typeof(TTo),
                val => val.LoadLocalVariable(collection));
            gen.Ret();

            var result = toCollectionMethod.CreateDelegate(resultDelegate);
            return result;
        }

        private Delegate MapToObject()
        {
            var resultDelegate = typeof(InternalMapper<TFrom, TTo>);
            DynamicMethod toObjectMethod;
            var gen = ILCodeGenerator.CreateDynamicMethodCodeGenerator(
                "m" + Guid.NewGuid().ToString("N"),
                resultDelegate,
                out toObjectMethod);

            var obj = gen.DeclareLocalVariable(typeof(TTo));
            gen.StoreLocalVariable(
                obj,
                val => val.LoadArgument(2));
            ExecuteMappingChain(gen);
            gen.StoreArgumentIndirectly(
                2,
                typeof(TTo),
                val => val.LoadLocalVariable(obj));
            gen.Ret();

            var result = toObjectMethod.CreateDelegate(resultDelegate);
            return result;
        }

        private void ExecuteMappingChain(ILCodeGenerator gen)
        {
            if (_mappingChain.Count == 0)
                return;

            //call ExecuteFromTos
            //...
            //foreach (var item in _mappingChain)
            //{
            //    throw new NotImplementedException();
            //}
        }

        private static Type GetElementType(Type type)
        {
            if (type.IsArray)
                return type.GetElementType();

            if (typeof (IDataReader).IsAssignableFrom(type))
                return typeof (IDataReader);

            if (type.IsGenericType)
            {
                var firstGenericArgument = type.GetGenericArguments()[0];
                if (typeof(IEnumerable<>).MakeGenericType(firstGenericArgument)
                    .IsAssignableFrom(type))
                {
                    return firstGenericArgument;
                }
            }

            return type.IsValueType ? typeof(ValueType) : typeof (object);
        }

        #endregion
    }
}
