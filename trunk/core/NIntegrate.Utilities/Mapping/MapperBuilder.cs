using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections;
using System.Data;
using System.Reflection;
using NIntegrate.Utilities.Reflection;
using System.Reflection.Emit;

namespace NIntegrate.Utilities.Mapping
{
    public abstract class MapperBuilder
    {
        internal MapperBuilder()
        {
        }

        internal abstract MapperCacheKey CacheKey { get; }

        internal abstract Delegate BuildMapper();
    }

    public sealed class MapperBuilder<TFrom, TTo> : MapperBuilder
    {
        private readonly bool _autoMap;
        private readonly bool _ignoreCase;
        private readonly bool _ignoreUnderscore;
        private readonly bool _isToArray;
        private readonly bool _isToCollection;
        private readonly List<Delegate> _mappingChain = new List<Delegate>();
        private bool _expectsTo;
        private static readonly MapperCacheKey _cacheKey = new MapperCacheKey(typeof (TFrom), typeof (TTo));

        #region Constructors

        internal MapperBuilder(bool autoMap, bool ignoreCase, bool ignoreUnderscore)
        {
            _autoMap = autoMap;
            _ignoreCase = ignoreCase;
            _ignoreUnderscore = ignoreUnderscore;

            if (typeof(IEnumerable).IsAssignableFrom(typeof(TFrom)) || typeof(IDataReader).IsAssignableFrom(typeof(TFrom)))
            {
                if (typeof(TTo).IsArray)
                    _isToArray = true;
                else if (typeof(ICollection<>).MakeGenericType(GetElementType(typeof(TTo))).IsAssignableFrom(typeof(TTo)))
                    _isToCollection = true;
            }
        }

        #endregion

        #region Public Methods

        public MapperBuilder<TFrom, TTo> From<TValue>(MappingFrom<TFrom, TValue> from)
        {
            if (from == null)
                throw new ArgumentNullException("from");
            if (_expectsTo)
                throw new InvalidOperationException("Expects To() but called From().");

            _mappingChain.Add(from);

            _expectsTo = true;

            return this;
        }

        public MapperBuilder<TFrom, TTo> To<TValue>(MappingTo<TTo, TValue> to)
        {
            if (to == null)
                throw new ArgumentNullException("to");
            if (!_expectsTo)
                throw new InvalidOperationException("Expects From() but called To().");

            _mappingChain.Add(to);

            _expectsTo = false;

            return this;
        }

        #endregion

        #region Non-Public Methods

        internal static void ExecuteFromTo<TFromValue, TToValue>(
            MapperFactory fac, TFrom fromObj, ref TTo toObj, int fromToIndex)
        {
            if (fac == null)
                throw new ArgumentNullException("fac");
            if (Equals(fromObj, default(TFrom)))
                throw new ArgumentNullException("fromObj");

            MapperBuilder builder;
            fac.MapperCache.TryGetValue(new MapperCacheKey(typeof(TFrom), typeof(TTo)), out builder);
            var genericBuilder = builder as MapperBuilder<TFrom, TTo>;
            if (genericBuilder != null && fromToIndex < genericBuilder._mappingChain.Count / 2)
            {
                var from = (MappingFrom<TFrom, TFromValue>)genericBuilder._mappingChain[fromToIndex * 2];
                var to = (MappingTo<TTo, TToValue>)genericBuilder._mappingChain[fromToIndex * 2 + 1];
                var fromValue = from(fromObj);
                if (typeof (TFromValue) != typeof (TToValue))
                {
                    var valueMapper = fac.GetMapper<TFromValue, TToValue>();
                    toObj = to(toObj, valueMapper(fromValue));
                }
                else
                {
                    toObj = to(toObj, (TToValue) (object) fromValue);
                }
            }
        }

        internal override MapperCacheKey CacheKey
        {
            get { return _cacheKey; }
        }

        internal override Delegate BuildMapper()
        {
            Delegate result;

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
            var collectionType = typeof(List<>).MakeGenericType(toElementType);
            var collection = gen.DeclareLocalVariable(collectionType);
            var en = gen.DeclareLocalVariable(typeof (IEnumerator));
            var foreachBegin = gen.DefineLabel();
            var foreachEnd = gen.DefineLabel();
            gen.StoreLocalVariable(
                collection,
                val => val.New(collectionType.GetConstructor(Type.EmptyTypes))
            );
            EmitGetFromEnumerator(gen, en);
            gen.MarkLabel(foreachBegin);
            EmitIfEnumeratorMoveNext(gen, en);
            if (fromElementType == typeof(IDataReader))
            {
                throw new NotImplementedException();
            }
            else if (fromElementType.IsValueType)
                EmitMapValueTypeItem(toElementType, gen, collectionType, collection, en, fromElementType);
            else
                EmitMapReferenceTypeItem(toElementType, gen, collectionType, collection, en, fromElementType);
            gen.GoTo(foreachBegin);
            gen.Else();
            gen.GoTo(foreachEnd);
            gen.EndIf();
            gen.MarkLabel(foreachEnd);
            gen.StoreArgumentIndirectly(
                2,
                typeof (TTo),
                val => val.CallMethod(
                    thisObj => thisObj.LoadLocalVariable(collection),
                    collectionType.GetMethod("ToArray")
                )
            );
            gen.Ret();

            var result = toArrayMethod.CreateDelegate(resultDelegate);
            return result;
        }

        private void EmitIfEnumeratorMoveNext(ILCodeGenerator gen, LocalBuilder en)
        {
            gen.If(
                boolVal => boolVal.CallMethod(
                               thisObj => thisObj.LoadLocalVariable(en),
                               typeof (IEnumerator).GetMethod("MoveNext")
                               )
                );
        }

        private void EmitMapReferenceTypeItem(Type toElementType, ILCodeGenerator gen, Type collectionType, LocalBuilder collection, LocalBuilder en, Type fromElementType)
        {
            gen.CallMethod(
                thisObj => thisObj.LoadLocalVariable(collection),
                typeof(ICollection<>).MakeGenericType(toElementType).GetMethod("Add"),
                val1 => val1.ConvertValue(
                    sourceVal => sourceVal.CallMethod(
                        thisObj2 => thisObj2.CallMethod(
                            thisObj3 => thisObj3.LoadArgument(0),
                            typeof(MapperFactory).GetMethod("GetMapper").MakeGenericMethod(fromElementType, toElementType)
                        ),
                        typeof(Mapper<,>).MakeGenericType(fromElementType, toElementType).GetMethod("Invoke"),
                        valFrom => valFrom.ConvertValue(
                            sourceVal2 => sourceVal2.LoadProperty(
                                thisObj2 => thisObj2.LoadLocalVariable(en),
                                typeof(IEnumerator).GetProperty("Current")
                            ),
                           typeof(object),
                           fromElementType
                        )
                    ),
                    collectionType,
                    typeof(ICollection<>).MakeGenericType(toElementType)
                )
            );
        }

        private void EmitMapValueTypeItem(Type toElementType, ILCodeGenerator gen, Type collectionType, LocalBuilder collection, LocalBuilder en, Type fromElementType)
        {
            gen.CallMethod(
                thisObj => thisObj.LoadLocalVariable(collection),
                typeof(ICollection<>).MakeGenericType(toElementType).GetMethod("Add"),
                val1 => val1.ConvertValue(
                    sourceValue => sourceValue.CallMethod(
                        thisObj2 => thisObj2.CallMethod(
                            thisObj3 => thisObj3.LoadArgument(0),
                            typeof(MapperFactory).GetMethod("GetMapper").MakeGenericMethod(
                                fromElementType, toElementType)
                        ),
                        typeof(Mapper<,>).MakeGenericType(fromElementType, toElementType).GetMethod("Invoke"),
                        valFrom => valFrom.UnboxAny(
                            fromElementType,
                            val => val.LoadProperty(
                                thisObj2 =>
                                thisObj2.LoadLocalVariable(en),
                                typeof(IEnumerator).GetProperty("Current")
                            )
                        )
                    ),
                    collectionType,
                    typeof(ICollection<>).MakeGenericType(toElementType)
                )
            );
        }

        private Delegate MapToCollection()
        {
            var resultDelegate = typeof(InternalMapper<TFrom, TTo>);
            DynamicMethod toCollectionMethod;
            var gen = ILCodeGenerator.CreateDynamicMethodCodeGenerator(
                "m" + Guid.NewGuid().ToString("N"),
                resultDelegate,
                out toCollectionMethod
            );

            EmitMappingChain(gen);
            var fromElementType = GetElementType(typeof(TFrom));
            var toElementType = GetElementType(typeof(TTo));
            var collectionType = typeof(TTo);
            var collection = gen.DeclareLocalVariable(collectionType);
            var en = gen.DeclareLocalVariable(typeof(IEnumerator));
            var foreachBegin = gen.DefineLabel();
            var foreachEnd = gen.DefineLabel();
            gen.StoreLocalVariable(
                collection,
                val => val.LoadArgumentIndirectly(2, typeof(TTo))
            );
            EmitGetFromEnumerator(gen, en);
            gen.MarkLabel(foreachBegin);
            EmitIfEnumeratorMoveNext(gen, en);
            if (fromElementType == typeof(IDataReader))
            {
                throw new NotImplementedException();
            }
            else if (fromElementType.IsValueType)
                EmitMapValueTypeItem(toElementType, gen, collectionType, collection, en, fromElementType);
            else
                EmitMapReferenceTypeItem(toElementType, gen, collectionType, collection, en, fromElementType);
            gen.GoTo(foreachBegin);
            gen.Else();
            gen.GoTo(foreachEnd);
            gen.EndIf();
            gen.MarkLabel(foreachEnd);
            if (_autoMap)
                EmitAutoMap(gen, collection);
            gen.StoreArgumentIndirectly(
                2,
                typeof(TTo),
                val => val.LoadLocalVariable(collection)
            );
            gen.Ret();

            var result = toCollectionMethod.CreateDelegate(resultDelegate);
            return result;
        }

        private void EmitGetFromEnumerator(ILCodeGenerator gen, LocalBuilder en)
        {
            gen.StoreLocalVariable(
                en,
                val => val.CallMethod(
                    thisObj => thisObj.ConvertValue(
                        sourceVal => sourceVal.LoadArgument(1),
                        typeof(TFrom),
                        typeof(IEnumerable)
                    ),
                    typeof(IEnumerable).GetMethod("GetEnumerator")
                )
            );
        }

        private Delegate MapToObject()
        {
            var resultDelegate = typeof(InternalMapper<TFrom, TTo>);
            DynamicMethod toObjectMethod;
            var gen = ILCodeGenerator.CreateDynamicMethodCodeGenerator(
                "m" + Guid.NewGuid().ToString("N"),
                resultDelegate,
                out toObjectMethod);

            EmitMappingChain(gen);
            if (typeof(IDataReader).IsAssignableFrom(typeof(TFrom)))
            {
                throw new NotImplementedException();
            }
            var obj = gen.DeclareLocalVariable(typeof(TTo));
            gen.StoreLocalVariable(
                obj,
                val => val.LoadArgumentIndirectly(2, typeof(TTo))
            );
            if (_autoMap)
                EmitAutoMap(gen, obj);
            gen.StoreArgumentIndirectly(
                2,
                typeof(TTo),
                val => val.LoadLocalVariable(obj)
            );
            gen.Ret();

            var result = toObjectMethod.CreateDelegate(resultDelegate);
            return result;
        }

        private void EmitAutoMap(ILCodeGenerator gen, LocalBuilder local)
        {
            //map from object properties/fields to to object properties/fields
            var baseType = typeof(TFrom);
            while (baseType != typeof(object) && baseType != typeof(ValueType))
            {
                foreach (var property in baseType.GetProperties())
                {
                    if (!property.CanRead)
                        continue;

                    var targetProperty = GetPropertyInfo(typeof (TTo), property.Name, _ignoreCase, _ignoreUnderscore);
                    if (targetProperty != null && targetProperty.CanWrite)
                    {
                        var sourceProperty = property;
                        gen.StoreProperty(
                            typeof(TTo).IsValueType ?
                            new ILExpression(thisObj => thisObj.LoadLocalVariableAddress(local)) 
                            :
                            new ILExpression(thisObj => thisObj.LoadLocalVariable(local)),
                            targetProperty,
                            val => val.CallMethod(
                                thisObj2 => thisObj2.CallMethod(
                                    thisObj3 => thisObj3.LoadArgument(0),
                                    typeof(MapperFactory).GetMethod("GetMapper").MakeGenericMethod(
                                        sourceProperty.PropertyType, targetProperty.PropertyType)
                                ),
                                typeof(Mapper<,>).MakeGenericType(
                                    sourceProperty.PropertyType,
                                    targetProperty.PropertyType).GetMethod("Invoke"),
                                typeof(TFrom).IsValueType ?
                                new ILExpression(valFrom => valFrom.LoadProperty(
                                    thisObj2 => thisObj2.LoadArgumentAddress(1),
                                    sourceProperty
                                ))
                                :
                                new ILExpression(valFrom => valFrom.LoadProperty(
                                    thisObj2 => thisObj2.LoadArgument(1),
                                    sourceProperty
                                ))
                            )
                        );
                    }
                    else
                    {
                        var targetField = GetFieldInfo(typeof (TTo), property.Name, _ignoreCase, _ignoreUnderscore);
                        if (targetField != null)
                        {
                            var sourceProperty = property;
                            gen.StoreField(
                                typeof(TTo).IsValueType ?
                                new ILExpression(thisObj => thisObj.LoadLocalVariableAddress(local))
                                :
                                new ILExpression(thisObj => thisObj.LoadLocalVariable(local)),
                                targetField,
                                val => val.CallMethod(
                                    thisObj2 => thisObj2.CallMethod(
                                        thisObj3 => thisObj3.LoadArgument(0),
                                        typeof(MapperFactory).GetMethod("GetMapper").MakeGenericMethod(
                                            sourceProperty.PropertyType, targetField.FieldType)
                                    ),
                                    typeof(Mapper<,>).MakeGenericType(
                                        sourceProperty.PropertyType,
                                        targetField.FieldType).GetMethod("Invoke"),
                                    typeof(TFrom).IsValueType ?
                                    new ILExpression(valFrom => valFrom.LoadProperty(
                                        thisObj2 => thisObj2.LoadArgumentAddress(1),
                                        sourceProperty
                                    ))
                                    :
                                    new ILExpression(valFrom => valFrom.LoadProperty(
                                        thisObj2 => thisObj2.LoadArgument(1),
                                        sourceProperty
                                    ))
                                )
                            );
                        }
                    }
                }
                foreach (var field in baseType.GetFields())
                {
                    var targetProperty = GetPropertyInfo(typeof(TTo), field.Name, _ignoreCase, _ignoreUnderscore);
                    if (targetProperty != null && targetProperty.CanWrite)
                    {
                        var sourceField = field;
                        gen.StoreProperty(
                            typeof(TTo).IsValueType ?
                            new ILExpression(thisObj => thisObj.LoadLocalVariableAddress(local))
                            :
                            new ILExpression(thisObj => thisObj.LoadLocalVariable(local)),
                            targetProperty,
                            val => val.CallMethod(
                                thisObj2 => thisObj2.CallMethod(
                                    thisObj3 => thisObj3.LoadArgument(0),
                                    typeof(MapperFactory).GetMethod("GetMapper").MakeGenericMethod(
                                        sourceField.FieldType, targetProperty.PropertyType)
                                ),
                                typeof(Mapper<,>).MakeGenericType(
                                    sourceField.FieldType,
                                    targetProperty.PropertyType).GetMethod("Invoke"),
                                typeof(TFrom).IsValueType ?
                                new ILExpression(valFrom => valFrom.LoadField(
                                    thisObj2 => thisObj2.LoadArgumentAddress(1),
                                    sourceField
                                ))
                                :
                                new ILExpression(valFrom => valFrom.LoadField(
                                    thisObj2 => thisObj2.LoadArgument(1),
                                    sourceField
                                ))
                            )
                        );
                    }
                    else
                    {
                        var targetField = GetFieldInfo(typeof(TTo), field.Name, _ignoreCase, _ignoreUnderscore);
                        if (targetField != null)
                        {
                            var sourceField = field;
                            gen.StoreField(
                                typeof(TTo).IsValueType ?
                                new ILExpression(thisObj => thisObj.LoadLocalVariableAddress(local))
                                :
                                new ILExpression(thisObj => thisObj.LoadLocalVariable(local)),
                                targetField,
                                val => val.CallMethod(
                                    thisObj2 => thisObj2.CallMethod(
                                        thisObj3 => thisObj3.LoadArgument(0),
                                        typeof(MapperFactory).GetMethod("GetMapper").MakeGenericMethod(
                                            sourceField.FieldType, targetField.FieldType)
                                    ),
                                    typeof(Mapper<,>).MakeGenericType(
                                        sourceField.FieldType,
                                        targetField.FieldType).GetMethod("Invoke"),
                                    typeof(TFrom).IsValueType ?
                                    new ILExpression(valFrom => valFrom.LoadField(
                                        thisObj2 => thisObj2.LoadArgumentAddress(1),
                                        sourceField
                                    ))
                                    :
                                    new ILExpression(valFrom => valFrom.LoadField(
                                        thisObj2 => thisObj2.LoadArgument(1),
                                        sourceField
                                    ))
                                )
                            );
                        }
                    }
                }

                baseType = baseType.BaseType;
            }
        }

        private static PropertyInfo GetPropertyInfo(Type type, string propertyName, bool ignoreCase, bool ignoreUnderscore)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            var baseType = type;
            while (baseType != typeof(object) && baseType != typeof(ValueType))
            {
                foreach (var property in baseType.GetProperties())
                {
                    if (ignoreCase && ignoreUnderscore)
                    {
                        if (string.Compare(
                            propertyName.Replace("_", string.Empty),
                            property.Name.Replace("_", string.Empty),
                            ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal) == 0)
                        {
                            return property;
                        }
                    }
                    else
                    {
                        if (string.Compare(
                            propertyName,
                            property.Name,
                            ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal) == 0)
                        {
                            return property;
                        }
                    }
                }

                baseType = baseType.BaseType;
            }

            return null;
        }

        private static FieldInfo GetFieldInfo(Type type, string fieldName, bool ignoreCase, bool ignoreUnderscore)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            var baseType = type;
            while (baseType != typeof(object) && baseType != typeof(ValueType))
            {
                foreach (var field in baseType.GetFields())
                {
                    if (ignoreCase && ignoreUnderscore)
                    {
                        if (string.Compare(
                            fieldName.Replace("_", string.Empty),
                            field.Name.Replace("_", string.Empty),
                            ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal) == 0)
                        {
                            return field;
                        }
                    }
                    else
                    {
                        if (string.Compare(
                            fieldName,
                            field.Name,
                            ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal) == 0)
                        {
                            return field;
                        }
                    }
                }

                baseType = baseType.BaseType;
            }

            return null;
        }

        private void EmitMappingChain(ILCodeGenerator gen)
        {
            if (_mappingChain.Count == 0)
                return;

            for (var i = 0; i < _mappingChain.Count / 2; i += 2)
            {
                var fromToIndex = i;
                var fromDelegate = _mappingChain[i];
                var fromValueType = fromDelegate.GetType().GetGenericArguments()[1];
                var toDelegate = _mappingChain[i + 1];
                var toValueType = toDelegate.GetType().GetGenericArguments()[1];

                gen.CallStaticMethod(
                    typeof (MapperBuilder<TFrom, TTo>).GetMethod("ExecuteFromTo", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static).MakeGenericMethod(fromValueType, toValueType),
                    val1 => val1.LoadArgument(0),
                    val2 => val2.LoadArgument(1),
                    val3 => val3.LoadArgument(2),
                    val4 => val4.Load(fromToIndex)
                );
            }
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
