using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.Serialization;
using System.Diagnostics;
using System.Globalization;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Schema;
using NIntegrate.Data.Exceptions;

namespace NIntegrate.Data
{
    /// <summary>
    /// CodeGenerator
    /// </summary>
    internal sealed class CodeGenerator
    {
        #region Nested Classes

        /// <summary>
        /// Cmp
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Cmp")]
        public enum Cmp
        {
            /// <summary>
            /// LessThan
            /// </summary>
            LessThan,
            /// <summary>
            /// EqualTo
            /// </summary>
            EqualTo,
            /// <summary>
            /// LessThanOrEqualTo
            /// </summary>
            LessThanOrEqualTo,
            /// <summary>
            /// GreaterThan
            /// </summary>
            GreaterThan,
            /// <summary>
            /// NotEqualTo
            /// </summary>
            NotEqualTo,
            /// <summary>
            /// GreaterThanOrEqualTo
            /// </summary>
            GreaterThanOrEqualTo
        }

        /// <summary>
        /// ArgBuilder
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
        public sealed class ArgBuilder
        {
            /// <summary>
            /// ArgType
            /// </summary>
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
            public Type ArgType;
            /// <summary>
            /// Index
            /// </summary>
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
            public int Index;

            /// <summary>
            /// ArgBuilder
            /// </summary>
            /// <param name="index"></param>
            /// <param name="argType"></param>
            public ArgBuilder(int index, Type argType)
            {
                this.Index = index;
                this.ArgType = argType;
            }
        }

        /// <summary>
        /// IfState
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
        public sealed class IfState
        {
            // Fields
            private Label elseBegin;
            private Label endIf;

            /// <summary>
            /// ElseBegin
            /// </summary>
            public Label ElseBegin
            {
                get
                {
                    return this.elseBegin;
                }
                set
                {
                    this.elseBegin = value;
                }
            }

            /// <summary>
            /// EndIf
            /// </summary>
            public Label EndIf
            {
                get
                {
                    return this.endIf;
                }
                set
                {
                    this.endIf = value;
                }
            }
        }

        /// <summary>
        /// SwitchState
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
        public sealed class SwitchState
        {
            private bool defaultDefined;
            private Label defaultLabel;
            private Label endOfSwitchLabel;

            /// <summary>
            /// SwitchState
            /// </summary>
            /// <param name="defaultLabel"></param>
            /// <param name="endOfSwitchLabel"></param>
            public SwitchState(Label defaultLabel, Label endOfSwitchLabel)
            {
                this.defaultLabel = defaultLabel;
                this.endOfSwitchLabel = endOfSwitchLabel;
            }

            /// <summary>
            /// DefaultDefined
            /// </summary>
            public bool DefaultDefined
            {
                get
                {
                    return this.defaultDefined;
                }
                set
                {
                    this.defaultDefined = value;
                }
            }

            /// <summary>
            /// DefaultLabel
            /// </summary>
            public Label DefaultLabel
            {
                get
                {
                    return this.defaultLabel;
                }
            }

            /// <summary>
            /// EndOfSwitchLabel
            /// </summary>
            public Label EndOfSwitchLabel
            {
                get
                {
                    return this.endOfSwitchLabel;
                }
            }
        }

        /// <summary>
        /// ForState
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
        public sealed class ForState
        {
            // Fields
            private Label beginLabel;
            private object end;
            private Label endLabel;
            private LocalBuilder indexVar;
            private bool requiresEndLabel;
            private Label testLabel;

            /// <summary>
            /// ForState
            /// </summary>
            /// <param name="indexVar"></param>
            /// <param name="beginLabel"></param>
            /// <param name="testLabel"></param>
            /// <param name="end"></param>
            public ForState(LocalBuilder indexVar, Label beginLabel, Label testLabel, object end)
            {
                this.indexVar = indexVar;
                this.beginLabel = beginLabel;
                this.testLabel = testLabel;
                this.end = end;
            }

            /// <summary>
            /// BeginLabel
            /// </summary>
            public Label BeginLabel
            {
                get
                {
                    return this.beginLabel;
                }
            }

            /// <summary>
            /// End
            /// </summary>
            public object End
            {
                get
                {
                    return this.end;
                }
            }

            /// <summary>
            /// EndLabel
            /// </summary>
            public Label EndLabel
            {
                get
                {
                    return this.endLabel;
                }
                set
                {
                    this.endLabel = value;
                }
            }

            /// <summary>
            /// Index
            /// </summary>
            public LocalBuilder Index
            {
                get
                {
                    return this.indexVar;
                }
            }

            /// <summary>
            /// RequiresEndLabel
            /// </summary>
            public bool RequiresEndLabel
            {
                get
                {
                    return this.requiresEndLabel;
                }
                set
                {
                    this.requiresEndLabel = value;
                }
            }

            /// <summary>
            /// TestLabel
            /// </summary>
            public Label TestLabel
            {
                get
                {
                    return this.testLabel;
                }
            }
        }

        #endregion

        #region Private Fields

        private ArrayList _argList;
        private Stack _blockStack;
        private Type _delegateType;
        private DynamicMethod _dynamicMethod;
        private MethodBase _methodOrConstructorBuilder;
        private static MethodInfo _getTypeFromHandle;
        private ILGenerator _ilGen;
        private Hashtable _localNames;
        private Label _methodEndLabel;
        private static MethodInfo _objectEquals;
        private static MethodInfo _objectToString;
        private Module _serializationModule;
        private static MethodInfo _stringConcat2;
        private static MethodInfo _stringConcat3;
        private static MethodInfo _stringFormat;
        private LocalBuilder _stringFormatArray;

        #endregion

        #region Constructors

        /// <summary>
        /// Initialize a CodeGenerator instance
        /// </summary>
        public CodeGenerator()
        {
        }

        /// <summary>
        /// Initialize a CodeGenerator instance
        /// </summary>
        /// <param name="targetModule"></param>
        public CodeGenerator(Module targetModule)
            : this()
        {
            Check.Require(targetModule, "targetModule");

            this._serializationModule = targetModule;
        }

        /// <summary>
        /// Initialize a CodeGenerator instance
        /// </summary>
        /// <param name="ownerTypeBuilder"></param>
        /// <param name="methodName"></param>
        /// <param name="methodAttrs"></param>
        /// <param name="callingConversion"></param>
        /// <param name="returnType"></param>
        /// <param name="argTypes"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Attrs")]
        public CodeGenerator(TypeBuilder ownerTypeBuilder, string methodName, MethodAttributes methodAttrs,
            CallingConventions callingConversion, Type returnType, Type[] argTypes)
            : this()
        {
            Check.Require(ownerTypeBuilder, "ownerTypeBuilder");
            Check.Require(methodName, "methodName", Check.NotNullOrEmpty);

            if (methodName == "ctor") //constructor
            {
                this._methodOrConstructorBuilder = ownerTypeBuilder.DefineConstructor(
                    methodAttrs, callingConversion, argTypes);
                this._ilGen = (this._methodOrConstructorBuilder as ConstructorBuilder).GetILGenerator();
            }
            else // method
            {
                this._methodOrConstructorBuilder = ownerTypeBuilder.DefineMethod(methodName,
                    methodAttrs, callingConversion, returnType, argTypes);
                this._ilGen = (this._methodOrConstructorBuilder as MethodBuilder).GetILGenerator();
            }
            this._blockStack = new Stack();
            this._argList = new ArrayList();
            if (argTypes != null)
            {
                for (int i = 0; i < argTypes.Length; i++)
                {
                    this._argList.Add(new ArgBuilder(i, argTypes[i]));
                }
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Add
        /// </summary>
        public void Add()
        {
            this._ilGen.Emit(OpCodes.Add);
        }

        /// <summary>
        /// And
        /// </summary>
        public void And()
        {
            this._ilGen.Emit(OpCodes.And);
        }

        /// <summary>
        /// BeginMethod
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="_delegateType"></param>
        public void BeginMethod(string methodName, Type delegateType)
        {
            Check.Require(this._methodOrConstructorBuilder == null, "BeginMethod() could not be called in this context.");
            Check.Require(methodName, "methodName", Check.NotNullOrEmpty);
            Check.Require(delegateType, "delegateType");

            MethodInfo method = delegateType.GetMethod("Invoke");
            ParameterInfo[] parameters = method.GetParameters();
            Type[] argTypes = new Type[parameters.Length];
            for (int i = 0; i < parameters.Length; i++)
            {
                argTypes[i] = parameters[i].ParameterType;
            }
            this.BeginMethod(method.ReturnType, methodName, argTypes);
            this._delegateType = delegateType;
        }

        /// <summary>
        /// BeginMethod
        /// </summary>
        /// <param name="returnType"></param>
        /// <param name="methodName"></param>
        /// <param name="argTypes"></param>
        private void BeginMethod(Type returnType, string methodName, params Type[] argTypes)
        {
            if (_serializationModule != null)
                this._dynamicMethod = new DynamicMethod(methodName, returnType, argTypes, _serializationModule, true);
            else
                this._dynamicMethod = new DynamicMethod(methodName, returnType, argTypes, true);
            this._ilGen = this._dynamicMethod.GetILGenerator();
            this._methodEndLabel = this._ilGen.DefineLabel();
            this._blockStack = new Stack();
            this._argList = new ArrayList();
            for (int i = 0; i < argTypes.Length; i++)
            {
                this._argList.Add(new ArgBuilder(i, argTypes[i]));
            }
        }

        /// <summary>
        /// Bgt
        /// </summary>
        /// <param name="label"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Bgt")]
        public void Bgt(Label label)
        {
            this._ilGen.Emit(OpCodes.Bgt, label);
        }

        /// <summary>
        /// Ble
        /// </summary>
        /// <param name="label"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ble")]
        public void Ble(Label label)
        {
            this._ilGen.Emit(OpCodes.Ble, label);
        }

        /// <summary>
        /// Blt
        /// </summary>
        /// <param name="label"></param>
        public void Blt(Label label)
        {
            this._ilGen.Emit(OpCodes.Blt, label);
        }

        /// <summary>
        /// Box
        /// </summary>
        /// <param name="type"></param>
        public void Box(Type type)
        {
            Check.Require(type, "type");
            Check.Require(type.IsValueType, "type MUST be ValueType");

            this._ilGen.Emit(OpCodes.Box, type);
        }

        /// <summary>
        /// Br
        /// </summary>
        /// <param name="label"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Br")]
        public void Br(Label label)
        {
            this._ilGen.Emit(OpCodes.Br, label);
        }

        /// <summary>
        /// Break
        /// </summary>
        /// <param name="forState"></param>
        public void Break(object forState)
        {
            this.InternalBreakFor(forState, OpCodes.Br);
        }

        /// <summary>
        /// Brfalse
        /// </summary>
        /// <param name="label"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Brfalse")]
        public void Brfalse(Label label)
        {
            this._ilGen.Emit(OpCodes.Brfalse, label);
        }

        /// <summary>
        /// Brtrue
        /// </summary>
        /// <param name="label"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Brtrue")]
        public void Brtrue(Label label)
        {
            this._ilGen.Emit(OpCodes.Brtrue, label);
        }

        /// <summary>
        /// Call
        /// </summary>
        /// <param name="ctor"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "ctor")]
        public void Call(ConstructorInfo ctor)
        {
            Check.Require(ctor, "ctor");

            this._ilGen.Emit(OpCodes.Call, ctor);
        }

        /// <summary>
        /// Call
        /// </summary>
        /// <param name="methodInfo"></param>
        public void Call(MethodInfo methodInfo)
        {
            Check.Require(methodInfo, "methodInfo");

            if (methodInfo.IsVirtual)
            {
                this._ilGen.Emit(OpCodes.Callvirt, methodInfo);
            }
            else if (methodInfo.IsStatic)
            {
                this._ilGen.Emit(OpCodes.Call, methodInfo);
            }
            else
            {
                this._ilGen.Emit(OpCodes.Call, methodInfo);
            }
        }

        /// <summary>
        /// Call
        /// </summary>
        /// <param name="thisObj"></param>
        /// <param name="methodInfo"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "obj")]
        public void Call(object thisObj, MethodInfo methodInfo)
        {
            Check.Require(thisObj, "thisObj");
            Check.Require(methodInfo, "methodInfo");

            VerifyParameterCount(methodInfo, 0);
            this.LoadThis(thisObj, methodInfo);
            this.Call(methodInfo);
        }

        /// <summary>
        /// Call
        /// </summary>
        /// <param name="thisObj"></param>
        /// <param name="methodInfo"></param>
        /// <param name="param1"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "obj"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "param")]
        public void Call(object thisObj, MethodInfo methodInfo, object param1)
        {
            Check.Require(thisObj, "thisObj");
            Check.Require(methodInfo, "methodInfo");

            VerifyParameterCount(methodInfo, 1);
            this.LoadThis(thisObj, methodInfo);
            this.LoadParam(param1, 1, methodInfo);
            this.Call(methodInfo);
        }

        /// <summary>
        /// Call
        /// </summary>
        /// <param name="thisObj"></param>
        /// <param name="methodInfo"></param>
        /// <param name="param1"></param>
        /// <param name="param2"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "obj"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "param")]
        public void Call(object thisObj, MethodInfo methodInfo, object param1, object param2)
        {
            Check.Require(thisObj, "thisObj");
            Check.Require(methodInfo, "methodInfo");

            VerifyParameterCount(methodInfo, 2);
            this.LoadThis(thisObj, methodInfo);
            this.LoadParam(param1, 1, methodInfo);
            this.LoadParam(param2, 2, methodInfo);
            this.Call(methodInfo);
        }

        /// <summary>
        /// Call
        /// </summary>
        /// <param name="thisObj"></param>
        /// <param name="methodInfo"></param>
        /// <param name="param1"></param>
        /// <param name="param2"></param>
        /// <param name="param3"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "obj"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "param")]
        public void Call(object thisObj, MethodInfo methodInfo, object param1, object param2, object param3)
        {
            Check.Require(thisObj, "thisObj");
            Check.Require(methodInfo, "methodInfo");

            VerifyParameterCount(methodInfo, 3);
            this.LoadThis(thisObj, methodInfo);
            this.LoadParam(param1, 1, methodInfo);
            this.LoadParam(param2, 2, methodInfo);
            this.LoadParam(param3, 3, methodInfo);
            this.Call(methodInfo);
        }

        /// <summary>
        /// Call
        /// </summary>
        /// <param name="thisObj"></param>
        /// <param name="methodInfo"></param>
        /// <param name="param1"></param>
        /// <param name="param2"></param>
        /// <param name="param3"></param>
        /// <param name="param4"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "obj"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "param")]
        public void Call(object thisObj, MethodInfo methodInfo, object param1, object param2, object param3, object param4)
        {
            Check.Require(thisObj, "thisObj");
            Check.Require(methodInfo, "methodInfo");

            VerifyParameterCount(methodInfo, 4);
            this.LoadThis(thisObj, methodInfo);
            this.LoadParam(param1, 1, methodInfo);
            this.LoadParam(param2, 2, methodInfo);
            this.LoadParam(param3, 3, methodInfo);
            this.LoadParam(param4, 4, methodInfo);
            this.Call(methodInfo);
        }

        /// <summary>
        /// Call
        /// </summary>
        /// <param name="thisObj"></param>
        /// <param name="methodInfo"></param>
        /// <param name="param1"></param>
        /// <param name="param2"></param>
        /// <param name="param3"></param>
        /// <param name="param4"></param>
        /// <param name="param5"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "obj"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "param")]
        public void Call(object thisObj, MethodInfo methodInfo, object param1, object param2, object param3, object param4, object param5)
        {
            Check.Require(thisObj, "thisObj");
            Check.Require(methodInfo, "methodInfo");

            VerifyParameterCount(methodInfo, 5);
            this.LoadThis(thisObj, methodInfo);
            this.LoadParam(param1, 1, methodInfo);
            this.LoadParam(param2, 2, methodInfo);
            this.LoadParam(param3, 3, methodInfo);
            this.LoadParam(param4, 4, methodInfo);
            this.LoadParam(param5, 5, methodInfo);
            this.Call(methodInfo);
        }

        /// <summary>
        /// Call
        /// </summary>
        /// <param name="thisObj"></param>
        /// <param name="methodInfo"></param>
        /// <param name="param1"></param>
        /// <param name="param2"></param>
        /// <param name="param3"></param>
        /// <param name="param4"></param>
        /// <param name="param5"></param>
        /// <param name="param6"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "obj"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "param")]
        public void Call(object thisObj, MethodInfo methodInfo, object param1, object param2, object param3, object param4, object param5, object param6)
        {
            Check.Require(thisObj, "thisObj");
            Check.Require(methodInfo, "methodInfo");

            VerifyParameterCount(methodInfo, 6);
            this.LoadThis(thisObj, methodInfo);
            this.LoadParam(param1, 1, methodInfo);
            this.LoadParam(param2, 2, methodInfo);
            this.LoadParam(param3, 3, methodInfo);
            this.LoadParam(param4, 4, methodInfo);
            this.LoadParam(param5, 5, methodInfo);
            this.LoadParam(param6, 6, methodInfo);
            this.Call(methodInfo);
        }

        /// <summary>
        /// CallStringFormat
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="values"></param>
        public void CallStringFormat(string msg, params object[] values)
        {
            Check.Require(msg, "msg", Check.NotNullOrEmpty);

            this.NewArray(typeof(object), values.Length);
            if (this._stringFormatArray == null)
            {
                this._stringFormatArray = this.DeclareLocal(typeof(object[]), "stringFormatArray");
            }
            this.Stloc(this._stringFormatArray);
            for (int i = 0; i < values.Length; i++)
            {
                this.StoreArrayElement(this._stringFormatArray, i, values[i]);
            }
            this.Load(msg);
            this.Load(this._stringFormatArray);
            this.Call(StringFormat);
        }

        /// <summary>
        /// Case
        /// </summary>
        /// <param name="caseLabel1"></param>
        public void Case(Label caseLabel1)
        {
            this.MarkLabel(caseLabel1);
        }

        /// <summary>
        /// Castclass
        /// </summary>
        /// <param name="target"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Castclass")]
        public void Castclass(Type target)
        {
            Check.Require(target, "target");

            this._ilGen.Emit(OpCodes.Castclass, target);
        }

        /// <summary>
        /// Ceq
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ceq")]
        public void Ceq()
        {
            this._ilGen.Emit(OpCodes.Ceq);
        }

        /// <summary>
        /// Concat2
        /// </summary>
        public void Concat2()
        {
            this.Call(StringConcat2);
        }

        /// <summary>
        /// Concat3
        /// </summary>
        public void Concat3()
        {
            this.Call(StringConcat3);
        }

        /// <summary>
        /// ConvertAddress
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        public void ConvertAddress(Type source, Type target)
        {
            Check.Require(source, "source");
            Check.Require(target, "target");

            this.InternalConvert(source, target, true);
        }

        /// <summary>
        /// ConvertValue
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        public void ConvertValue(Type source, Type target)
        {
            Check.Require(source, "source");
            Check.Require(target, "target");

            this.InternalConvert(source, target, false);
        }

        /// <summary>
        /// Dec
        /// </summary>
        /// <param name="var"></param>
        public void Dec(object var)
        {
            this.Load(var);
            this.Load(1);
            this.Subtract();
            this.Store(var);
        }

        /// <summary>
        /// DeclareLocal
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public LocalBuilder DeclareLocal(Type type)
        {
            Check.Require(type, "type");

            return this.DeclareLocal(type, null, false);
        }

        /// <summary>
        /// DeclareLocal
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public LocalBuilder DeclareLocal(Type type, string name)
        {
            Check.Require(type, "type");

            return this.DeclareLocal(type, false);
        }

        /// <summary>
        /// DeclareLocal
        /// </summary>
        /// <param name="type"></param>
        /// <param name="isPinned"></param>
        /// <returns></returns>
        public LocalBuilder DeclareLocal(Type type, bool isPinned)
        {
            Check.Require(type, "type");

            LocalBuilder builder = this._ilGen.DeclareLocal(type, isPinned);
            return builder;
        }

        /// <summary>
        /// DeclareLocal
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="initialValue"></param>
        /// <returns></returns>
        public LocalBuilder DeclareLocal(Type type, string name, object initialValue)
        {
            Check.Require(type, "type");

            LocalBuilder var = this.DeclareLocal(type, name);
            this.Load(initialValue);
            this.Store(var);
            return var;
        }

        /// <summary>
        /// DefaultCase
        /// </summary>
        public void DefaultCase()
        {
            object expected = this._blockStack.Peek();
            SwitchState state = expected as SwitchState;
            if (state == null)
            {
                ThrowMismatchException(expected);
            }
            this.MarkLabel(state.DefaultLabel);
            state.DefaultDefined = true;
        }

        /// <summary>
        /// DefineLabel
        /// </summary>
        /// <returns></returns>
        public Label DefineLabel()
        {
            return this._ilGen.DefineLabel();
        }

        /// <summary>
        /// Dup
        /// </summary>
        public void Dup()
        {
            this._ilGen.Emit(OpCodes.Dup);
        }

        /// <summary>
        /// Else
        /// </summary>
        public void Else()
        {
            IfState state = this.PopIfState();
            this.Br(state.EndIf);
            this.MarkLabel(state.ElseBegin);
            state.ElseBegin = state.EndIf;
            this._blockStack.Push(state);
        }

        /// <summary>
        /// ElseIf
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="cmpOp"></param>
        /// <param name="value2"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "cmp"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Op")]
        public void ElseIf(object value1, Cmp cmpOp, object value2)
        {
            IfState state = (IfState)this._blockStack.Pop();
            this.Br(state.EndIf);
            this.MarkLabel(state.ElseBegin);
            this.Load(value1);
            this.Load(value2);
            state.ElseBegin = this.DefineLabel();
            this._ilGen.Emit(GetBranchCode(cmpOp), state.ElseBegin);
            this._blockStack.Push(state);
        }

        /// <summary>
        /// EndCase
        /// </summary>
        public void EndCase()
        {
            object expected = this._blockStack.Peek();
            SwitchState state = expected as SwitchState;
            if (state == null)
            {
                ThrowMismatchException(expected);
            }
            this.Br(state.EndOfSwitchLabel);
        }

        /// <summary>
        /// EndFor
        /// </summary>
        public void EndFor()
        {
            object expected = this._blockStack.Pop();
            ForState state = expected as ForState;
            if (state == null)
            {
                ThrowMismatchException(expected);
            }
            if (state.Index != null)
            {
                this.Ldloc(state.Index);
                this.Ldc(1);
                this.Add();
                this.Stloc(state.Index);
                this.MarkLabel(state.TestLabel);
                this.Ldloc(state.Index);
                this.Load(state.End);
                if (GetVariableType(state.End).IsArray)
                {
                    this.Ldlen();
                }
                this.Blt(state.BeginLabel);
            }
            else
            {
                this.Br(state.BeginLabel);
            }
            if (state.RequiresEndLabel)
            {
                this.MarkLabel(state.EndLabel);
            }
        }

        /// <summary>
        /// EndForEach
        /// </summary>
        /// <param name="moveNextMethod"></param>
        public void EndForEach(MethodInfo moveNextMethod)
        {
            Check.Require(moveNextMethod, "moveNextMethod");

            object expected = this._blockStack.Pop();
            ForState state = expected as ForState;
            if (state == null)
            {
                ThrowMismatchException(expected);
            }
            this.MarkLabel(state.TestLabel);
            object var = state.End;
            if (GetVariableType(var) == moveNextMethod.DeclaringType)
            {
                this.LoadThis(var, moveNextMethod);
                this._ilGen.Emit(OpCodes.Call, moveNextMethod);
            }
            else
            {
                this.Call(var, moveNextMethod);
            }
            this.Brtrue(state.BeginLabel);
            if (state.RequiresEndLabel)
            {
                this.MarkLabel(state.EndLabel);
            }
        }

        /// <summary>
        /// EndIf
        /// </summary>
        public void EndIf()
        {
            IfState state = this.PopIfState();
            if (!state.ElseBegin.Equals(state.EndIf))
            {
                this.MarkLabel(state.ElseBegin);
            }
            this.MarkLabel(state.EndIf);
        }

        /// <summary>
        /// EndMethod
        /// </summary>
        /// <returns></returns>
        public Delegate EndMethod()
        {
            Check.Require(this._methodOrConstructorBuilder == null, "EndMethod() could not be called in this context.");

            this.MarkLabel(this._methodEndLabel);
            this.Ret();
            Delegate delegate2 = null;
            delegate2 = this._dynamicMethod.CreateDelegate(this._delegateType);
            this._dynamicMethod = null;
            this._delegateType = null;
            this._ilGen = null;
            this._blockStack = null;
            this._argList = null;
            return delegate2;
        }

        /// <summary>
        /// EndSwitch
        /// </summary>
        public void EndSwitch()
        {
            object expected = this._blockStack.Pop();
            SwitchState state = expected as SwitchState;
            if (state == null)
            {
                ThrowMismatchException(expected);
            }
            if (!state.DefaultDefined)
            {
                this.MarkLabel(state.DefaultLabel);
            }
            this.MarkLabel(state.EndOfSwitchLabel);
        }

        /// <summary>
        /// For
        /// </summary>
        /// <param name="local"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public object For(LocalBuilder local, object start, object end)
        {
            Check.Require(local, "local");

            ForState state = new ForState(local, this.DefineLabel(), this.DefineLabel(), end);
            if (state.Index != null)
            {
                this.Load(start);
                this.Stloc(state.Index);
                this.Br(state.TestLabel);
            }
            this.MarkLabel(state.BeginLabel);
            this._blockStack.Push(state);
            return state;
        }

        /// <summary>
        /// ForEach
        /// </summary>
        /// <param name="local"></param>
        /// <param name="elementType"></param>
        /// <param name="enumeratorType"></param>
        /// <param name="enumerator"></param>
        /// <param name="getCurrentMethod"></param>
        public void ForEach(LocalBuilder local, Type elementType, Type enumeratorType, LocalBuilder enumerator, MethodInfo getCurrentMethod)
        {
            Check.Require(local, "local");
            Check.Require(elementType, "elementType");
            Check.Require(enumeratorType, "enumeratorType");
            Check.Require(enumerator, "enumerator");
            Check.Require(getCurrentMethod, "getCurrentMethod");

            ForState state = new ForState(local, this.DefineLabel(), this.DefineLabel(), enumerator);
            this.Br(state.TestLabel);
            this.MarkLabel(state.BeginLabel);
            if (enumeratorType == getCurrentMethod.DeclaringType)
            {
                this.LoadThis(enumerator, getCurrentMethod);
                this._ilGen.Emit(OpCodes.Call, getCurrentMethod);
            }
            else
            {
                this.Call(enumerator, getCurrentMethod);
            }
            this.ConvertValue(elementType, GetVariableType(local));
            this.Stloc(local);
            this._blockStack.Push(state);
        }

        /// <summary>
        /// GetArg
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public ArgBuilder GetArg(int index)
        {
            return (ArgBuilder)this._argList[index];
        }

        private static OpCode GetBranchCode(Cmp cmp)
        {
            switch (cmp)
            {
                case Cmp.LessThan:
                    return OpCodes.Bge;

                case Cmp.EqualTo:
                    return OpCodes.Bne_Un;

                case Cmp.LessThanOrEqualTo:
                    return OpCodes.Bgt;

                case Cmp.GreaterThan:
                    return OpCodes.Ble;

                case Cmp.NotEqualTo:
                    return OpCodes.Beq;
            }
            return OpCodes.Blt;
        }

        private static Cmp GetCmpInverse(Cmp cmp)
        {
            switch (cmp)
            {
                case Cmp.LessThan:
                    return Cmp.GreaterThanOrEqualTo;

                case Cmp.EqualTo:
                    return Cmp.NotEqualTo;

                case Cmp.LessThanOrEqualTo:
                    return Cmp.GreaterThan;

                case Cmp.GreaterThan:
                    return Cmp.LessThanOrEqualTo;

                case Cmp.NotEqualTo:
                    return Cmp.EqualTo;
            }
            return Cmp.LessThan;
        }

        private static OpCode GetConvOpCode(TypeCode typeCode)
        {
            switch (typeCode)
            {
                case TypeCode.Boolean:
                    return OpCodes.Conv_I1;

                case TypeCode.Char:
                    return OpCodes.Conv_I2;

                case TypeCode.SByte:
                    return OpCodes.Conv_I1;

                case TypeCode.Byte:
                    return OpCodes.Conv_U1;

                case TypeCode.Int16:
                    return OpCodes.Conv_I2;

                case TypeCode.UInt16:
                    return OpCodes.Conv_U2;

                case TypeCode.Int32:
                    return OpCodes.Conv_I4;

                case TypeCode.UInt32:
                    return OpCodes.Conv_U4;

                case TypeCode.Int64:
                    return OpCodes.Conv_I8;

                case TypeCode.UInt64:
                    return OpCodes.Conv_I8;

                case TypeCode.Single:
                    return OpCodes.Conv_R4;

                case TypeCode.Double:
                    return OpCodes.Conv_R8;
            }
            return OpCodes.Nop;
        }

        private static OpCode GetLdelemOpCode(TypeCode typeCode)
        {
            switch (typeCode)
            {
                case TypeCode.Object:
                case TypeCode.DBNull:
                    return OpCodes.Ldelem_Ref;

                case TypeCode.Boolean:
                    return OpCodes.Ldelem_I1;

                case TypeCode.Char:
                    return OpCodes.Ldelem_I2;

                case TypeCode.SByte:
                    return OpCodes.Ldelem_I1;

                case TypeCode.Byte:
                    return OpCodes.Ldelem_U1;

                case TypeCode.Int16:
                    return OpCodes.Ldelem_I2;

                case TypeCode.UInt16:
                    return OpCodes.Ldelem_U2;

                case TypeCode.Int32:
                    return OpCodes.Ldelem_I4;

                case TypeCode.UInt32:
                    return OpCodes.Ldelem_U4;

                case TypeCode.Int64:
                    return OpCodes.Ldelem_I8;

                case TypeCode.UInt64:
                    return OpCodes.Ldelem_I8;

                case TypeCode.Single:
                    return OpCodes.Ldelem_R4;

                case TypeCode.Double:
                    return OpCodes.Ldelem_R8;

                case TypeCode.String:
                    return OpCodes.Ldelem_Ref;
            }
            return OpCodes.Nop;
        }

        private static OpCode GetLdindOpCode(TypeCode typeCode)
        {
            switch (typeCode)
            {
                case TypeCode.Boolean:
                    return OpCodes.Ldind_I1;

                case TypeCode.Char:
                    return OpCodes.Ldind_I2;

                case TypeCode.SByte:
                    return OpCodes.Ldind_I1;

                case TypeCode.Byte:
                    return OpCodes.Ldind_U1;

                case TypeCode.Int16:
                    return OpCodes.Ldind_I2;

                case TypeCode.UInt16:
                    return OpCodes.Ldind_U2;

                case TypeCode.Int32:
                    return OpCodes.Ldind_I4;

                case TypeCode.UInt32:
                    return OpCodes.Ldind_U4;

                case TypeCode.Int64:
                    return OpCodes.Ldind_I8;

                case TypeCode.UInt64:
                    return OpCodes.Ldind_I8;

                case TypeCode.Single:
                    return OpCodes.Ldind_R4;

                case TypeCode.Double:
                    return OpCodes.Ldind_R8;

                case TypeCode.String:
                    return OpCodes.Ldind_Ref;
            }
            return OpCodes.Nop;
        }

        private static OpCode GetStelemOpCode(TypeCode typeCode)
        {
            switch (typeCode)
            {
                case TypeCode.Object:
                case TypeCode.DBNull:
                    return OpCodes.Stelem_Ref;

                case TypeCode.Boolean:
                    return OpCodes.Stelem_I1;

                case TypeCode.Char:
                    return OpCodes.Stelem_I2;

                case TypeCode.SByte:
                    return OpCodes.Stelem_I1;

                case TypeCode.Byte:
                    return OpCodes.Stelem_I1;

                case TypeCode.Int16:
                    return OpCodes.Stelem_I2;

                case TypeCode.UInt16:
                    return OpCodes.Stelem_I2;

                case TypeCode.Int32:
                    return OpCodes.Stelem_I4;

                case TypeCode.UInt32:
                    return OpCodes.Stelem_I4;

                case TypeCode.Int64:
                    return OpCodes.Stelem_I8;

                case TypeCode.UInt64:
                    return OpCodes.Stelem_I8;

                case TypeCode.Single:
                    return OpCodes.Stelem_R4;

                case TypeCode.Double:
                    return OpCodes.Stelem_R8;

                case TypeCode.String:
                    return OpCodes.Stelem_Ref;
            }
            return OpCodes.Nop;
        }

        /// <summary>
        /// GetVariableType
        /// </summary>
        /// <param name="var"></param>
        /// <returns></returns>
        public static Type GetVariableType(object var)
        {
            ArgBuilder varCastToArgBuilder = var as ArgBuilder;
            if (varCastToArgBuilder != null)
                return varCastToArgBuilder.ArgType;

            LocalBuilder varCastToLocalBuilder = var as LocalBuilder;
            if (varCastToLocalBuilder != null)
                return varCastToLocalBuilder.LocalType;

            return var.GetType();
        }

        /// <summary>
        /// If
        /// </summary>
        public void If()
        {
            this.InternalIf(false);
        }

        /// <summary>
        /// If
        /// </summary>
        /// <param name="cmpOp"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "cmp"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Op")]
        public void If(Cmp cmpOp)
        {
            IfState state = new IfState();
            state.EndIf = this.DefineLabel();
            state.ElseBegin = this.DefineLabel();
            this._ilGen.Emit(GetBranchCode(cmpOp), state.ElseBegin);
            this._blockStack.Push(state);
        }

        /// <summary>
        /// If
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="cmpOp"></param>
        /// <param name="value2"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "cmp"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Op")]
        public void If(object value1, Cmp cmpOp, object value2)
        {
            this.Load(value1);
            this.Load(value2);
            this.If(cmpOp);
        }

        /// <summary>
        /// IfFalseBreak
        /// </summary>
        /// <param name="forState"></param>
        public void IfFalseBreak(object forState)
        {
            this.InternalBreakFor(forState, OpCodes.Brfalse);
        }

        /// <summary>
        /// IfNot
        /// </summary>
        public void IfNot()
        {
            this.InternalIf(true);
        }

        /// <summary>
        /// IfNotDefaultValue
        /// </summary>
        /// <param name="value"></param>
        public void IfNotDefaultValue(object value)
        {
            Type variableType = GetVariableType(value);
            TypeCode typeCode = Type.GetTypeCode(variableType);
            if (((typeCode == TypeCode.Object) && variableType.IsValueType) || ((typeCode == TypeCode.DateTime) || (typeCode == TypeCode.Decimal)))
            {
                this.LoadDefaultValue(variableType);
                this.ConvertValue(variableType, typeof(object));
                this.Load(value);
                this.ConvertValue(variableType, typeof(object));
                this.Call(ObjectEquals);
                this.IfNot();
            }
            else
            {
                this.LoadDefaultValue(variableType);
                this.Load(value);
                this.If(Cmp.NotEqualTo);
            }
        }

        /// <summary>
        /// IfTrueBreak
        /// </summary>
        /// <param name="forState"></param>
        public void IfTrueBreak(object forState)
        {
            this.InternalBreakFor(forState, OpCodes.Brtrue);
        }

        /// <summary>
        /// IgnoreReturnValue
        /// </summary>
        public void IgnoreReturnValue()
        {
            this.Pop();
        }

        /// <summary>
        /// Inc
        /// </summary>
        /// <param name="var"></param>
        public void Inc(object var)
        {
            this.Load(var);
            this.Load(1);
            this.Add();
            this.Store(var);
        }

        /// <summary>
        /// InitObj
        /// </summary>
        /// <param name="valueType"></param>
        public void InitObj(Type valueType)
        {
            Check.Require(valueType, "valueType");

            this._ilGen.Emit(OpCodes.Initobj, valueType);
        }

        /// <summary>
        /// InternalBreakFor
        /// </summary>
        /// <param name="userForState"></param>
        /// <param name="branchInstruction"></param>
        public void InternalBreakFor(object userForState, OpCode branchInstruction)
        {
            foreach (object obj2 in this._blockStack)
            {
                ForState state = obj2 as ForState;
                if ((state != null) && (state == userForState))
                {
                    if (!state.RequiresEndLabel)
                    {
                        state.EndLabel = this.DefineLabel();
                        state.RequiresEndLabel = true;
                    }
                    this._ilGen.Emit(branchInstruction, state.EndLabel);
                    break;
                }
            }
        }

        /// <summary>
        /// InternalConvert
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="isAddress"></param>
        private void InternalConvert(Type source, Type target, bool isAddress)
        {
            if (target != source)
            {
                if (target.IsValueType)
                {
                    if (source.IsValueType)
                    {
                        OpCode opcode = GetConvOpCode(Type.GetTypeCode(target));
                        Check.Assert(!opcode.Equals(OpCodes.Nop), "NoConversionPossible");

                        this._ilGen.Emit(opcode);
                    }
                    else
                    {
                        Check.Assert(source.IsAssignableFrom(target), "IsNotAssignableFrom");

                        this.Unbox(target);
                        if (!isAddress)
                        {
                            this.Ldobj(target);
                        }
                    }
                }
                else if (target.IsAssignableFrom(source))
                {
                    if (source.IsValueType)
                    {
                        if (isAddress)
                        {
                            this.Ldobj(source);
                        }
                        this.Box(source);
                    }
                }
                else if (source.IsAssignableFrom(target))
                {
                    this.Castclass(target);
                }
                else
                {
                    Check.Assert(!(!target.IsInterface && !source.IsInterface), "IsNotAssignableFrom");

                    this.Castclass(target);
                }
            }
        }

        private void InternalIf(bool negate)
        {
            IfState state = new IfState();
            state.EndIf = this.DefineLabel();
            state.ElseBegin = this.DefineLabel();
            if (negate)
            {
                this.Brtrue(state.ElseBegin);
            }
            else
            {
                this.Brfalse(state.ElseBegin);
            }
            this._blockStack.Push(state);
        }

        private static bool IsStruct(Type objType)
        {
            if (objType.IsValueType)
            {
                return !objType.IsPrimitive;
            }
            return false;
        }

        /// <summary>
        /// Ldarg
        /// </summary>
        /// <param name="slot">The slot.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ldarg")]
        public void Ldarg(int slot)
        {
            switch (slot)
            {
                case 0:
                    this._ilGen.Emit(OpCodes.Ldarg_0);
                    return;

                case 1:
                    this._ilGen.Emit(OpCodes.Ldarg_1);
                    return;

                case 2:
                    this._ilGen.Emit(OpCodes.Ldarg_2);
                    return;

                case 3:
                    this._ilGen.Emit(OpCodes.Ldarg_3);
                    return;
            }
            if (slot <= 0xff)
            {
                this._ilGen.Emit(OpCodes.Ldarg_S, slot);
            }
            else
            {
                this._ilGen.Emit(OpCodes.Ldarg, slot);
            }
        }

        /// <summary>
        /// Ldarg
        /// </summary>
        /// <param name="arg"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ldarg")]
        public void Ldarg(ArgBuilder arg)
        {
            Check.Require(arg, "arg");

            this.Ldarg(arg.Index);
        }

        /// <summary>
        /// Ldarga
        /// </summary>
        /// <param name="slot"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ldarga")]
        public void Ldarga(int slot)
        {
            if (slot <= 0xff)
            {
                this._ilGen.Emit(OpCodes.Ldarga_S, slot);
            }
            else
            {
                this._ilGen.Emit(OpCodes.Ldarga, slot);
            }
        }

        /// <summary>
        /// Ldarga
        /// </summary>
        /// <param name="argBuilder"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ldarga")]
        public void Ldarga(ArgBuilder argBuilder)
        {
            Check.Require(argBuilder, "argBuilder");

            this.Ldarga(argBuilder.Index);
        }

        /// <summary>
        /// LdargAddress
        /// </summary>
        /// <param name="argBuilder"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ldarg")]
        public void LdargAddress(ArgBuilder argBuilder)
        {
            Check.Require(argBuilder, "argBuilder");

            if (argBuilder.ArgType.IsValueType)
            {
                this.Ldarga(argBuilder);
            }
            else
            {
                this.Ldarg(argBuilder);
            }
        }

        /// <summary>
        /// Ldc    
        /// </summary>
        /// <param name="boolVar"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "bool"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ldc")]
        public void Ldc(bool boolVar)
        {
            if (boolVar)
            {
                this._ilGen.Emit(OpCodes.Ldc_I4_1);
            }
            else
            {
                this._ilGen.Emit(OpCodes.Ldc_I4_0);
            }
        }

        /// <summary>
        /// Ldc
        /// </summary>
        /// <param name="d"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "d"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ldc")]
        public void Ldc(double d)
        {
            this._ilGen.Emit(OpCodes.Ldc_R8, d);
        }

        /// <summary>
        /// Ldc
        /// </summary>
        /// <param name="intVar"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "int"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ldc")]
        public void Ldc(int intVar)
        {
            switch (intVar)
            {
                case -1:
                    this._ilGen.Emit(OpCodes.Ldc_I4_M1);
                    return;

                case 0:
                    this._ilGen.Emit(OpCodes.Ldc_I4_0);
                    return;

                case 1:
                    this._ilGen.Emit(OpCodes.Ldc_I4_1);
                    return;

                case 2:
                    this._ilGen.Emit(OpCodes.Ldc_I4_2);
                    return;

                case 3:
                    this._ilGen.Emit(OpCodes.Ldc_I4_3);
                    return;

                case 4:
                    this._ilGen.Emit(OpCodes.Ldc_I4_4);
                    return;

                case 5:
                    this._ilGen.Emit(OpCodes.Ldc_I4_5);
                    return;

                case 6:
                    this._ilGen.Emit(OpCodes.Ldc_I4_6);
                    return;

                case 7:
                    this._ilGen.Emit(OpCodes.Ldc_I4_7);
                    return;

                case 8:
                    this._ilGen.Emit(OpCodes.Ldc_I4_8);
                    return;
            }
            this._ilGen.Emit(OpCodes.Ldc_I4, intVar);
        }

        /// <summary>
        /// Ldc
        /// </summary>
        /// <param name="l">The l.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "l"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ldc")]
        public void Ldc(long l)
        {
            this._ilGen.Emit(OpCodes.Ldc_I8, l);
        }

        /// <summary>
        /// Ldc
        /// </summary>
        /// <param name="o"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "o"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ldc"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        public void Ldc(object o)
        {
            Type enumType = o.GetType();
            if (o is Type)
            {
                this.Ldtoken((Type)o);
                this.Call(GetTypeFromHandle);
            }
            else if (enumType.IsEnum)
            {
                this.Ldc(((IConvertible)o).ToType(Enum.GetUnderlyingType(enumType), null));
            }
            else
            {
                switch (Type.GetTypeCode(enumType))
                {
                    case TypeCode.Boolean:
                        this.Ldc((bool)o);
                        return;

                    case TypeCode.Char:
                    //throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("CharIsInvalidPrimitive")));

                    case TypeCode.SByte:
                    case TypeCode.Byte:
                    case TypeCode.Int16:
                    case TypeCode.UInt16:
                        this.Ldc(((IConvertible)o).ToInt32(CultureInfo.InvariantCulture));
                        return;

                    case TypeCode.Int32:
                        this.Ldc((int)o);
                        return;

                    case TypeCode.UInt32:
                        this.Ldc((int)((uint)o));
                        return;

                    case TypeCode.Int64:
                        this.Ldc((long)o);
                        return;

                    case TypeCode.UInt64:
                        this.Ldc((long)((ulong)o));
                        return;

                    case TypeCode.Single:
                        this.Ldc((float)o);
                        return;

                    case TypeCode.Double:
                        this.Ldc((double)o);
                        return;

                    case TypeCode.String:
                        this.Ldstr((string)o);
                        return;
                }
                throw new AssertionException("UnknownConstantType");
            }
        }

        /// <summary>
        /// Ldc
        /// </summary>
        /// <param name="f"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "f"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ldc")]
        public void Ldc(float f)
        {
            this._ilGen.Emit(OpCodes.Ldc_R4, f);
        }

        /// <summary>
        /// Ldelem
        /// </summary>
        /// <param name="arrayElementType"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ldelem")]
        public void Ldelem(Type arrayElementType)
        {
            Check.Require(arrayElementType, "arrayElementType");

            if (arrayElementType.IsEnum)
            {
                this.Ldelem(Enum.GetUnderlyingType(arrayElementType));
            }
            else
            {
                OpCode opcode = GetLdelemOpCode(Type.GetTypeCode(arrayElementType));
                Check.Assert(!opcode.Equals(OpCodes.Nop), "ArrayTypeIsNotSupported");
                this._ilGen.Emit(opcode);
            }
        }

        /// <summary>
        /// Ldelema
        /// </summary>
        /// <param name="arrayElementType"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ldelema")]
        public void Ldelema(Type arrayElementType)
        {
            Check.Require(arrayElementType, "arrayElementType");

            OpCode opcode = OpCodes.Ldelema;
            this._ilGen.Emit(opcode, arrayElementType);
        }

        /// <summary>
        /// Ldlen   
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ldlen")]
        public void Ldlen()
        {
            this._ilGen.Emit(OpCodes.Ldlen);
            this._ilGen.Emit(OpCodes.Conv_I4);
        }

        /// <summary>
        /// Ldloc
        /// </summary>
        /// <param name="slot"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ldloc")]
        public void Ldloc(int slot)
        {
            switch (slot)
            {
                case 0:
                    this._ilGen.Emit(OpCodes.Ldloc_0);
                    return;

                case 1:
                    this._ilGen.Emit(OpCodes.Ldloc_1);
                    return;

                case 2:
                    this._ilGen.Emit(OpCodes.Ldloc_2);
                    return;

                case 3:
                    this._ilGen.Emit(OpCodes.Ldloc_3);
                    return;
            }
            if (slot <= 0xff)
            {
                this._ilGen.Emit(OpCodes.Ldloc_S, slot);
            }
            else
            {
                this._ilGen.Emit(OpCodes.Ldloc, slot);
            }
        }

        /// <summary>
        /// Ldloc
        /// </summary>
        /// <param name="localBuilder"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ldloc")]
        public void Ldloc(LocalBuilder localBuilder)
        {
            this._ilGen.Emit(OpCodes.Ldloc, localBuilder);
        }

        /// <summary>
        /// Ldloca
        /// </summary>
        /// <param name="slot"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ldloca")]
        public void Ldloca(int slot)
        {
            if (slot <= 0xff)
            {
                this._ilGen.Emit(OpCodes.Ldloca_S, slot);
            }
            else
            {
                this._ilGen.Emit(OpCodes.Ldloca, slot);
            }
        }

        /// <summary>
        /// Ldloca
        /// </summary>
        /// <param name="localBuilder"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ldloca")]
        public void Ldloca(LocalBuilder localBuilder)
        {
            Check.Require(localBuilder, "localBuilder");

            this._ilGen.Emit(OpCodes.Ldloca, localBuilder);
        }

        /// <summary>
        /// LdlocAddress
        /// </summary>
        /// <param name="localBuilder"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ldloc")]
        public void LdlocAddress(LocalBuilder localBuilder)
        {
            Check.Require(localBuilder, "localBuilder");

            if (localBuilder.LocalType.IsValueType)
            {
                this.Ldloca(localBuilder);
            }
            else
            {
                this.Ldloc(localBuilder);
            }
        }

        /// <summary>
        /// Ldobj
        /// </summary>
        /// <param name="type"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ldobj")]
        public void Ldobj(Type type)
        {
            Check.Require(type, "type");

            OpCode opcode = GetLdindOpCode(Type.GetTypeCode(type));
            if (!opcode.Equals(OpCodes.Nop))
            {
                this._ilGen.Emit(opcode);
            }
            else
            {
                this._ilGen.Emit(OpCodes.Ldobj, type);
            }
        }

        /// <summary>
        /// Ldstr
        /// </summary>
        /// <param name="strVar"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "str"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ldstr")]
        public void Ldstr(string strVar)
        {
            this._ilGen.Emit(OpCodes.Ldstr, strVar);
        }

        /// <summary>
        /// Ldtoken
        /// </summary>
        /// <param name="t"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "t"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ldtoken")]
        public void Ldtoken(Type t)
        {
            Check.Require(t, "t");

            this._ilGen.Emit(OpCodes.Ldtoken, t);
        }

        /// <summary>
        /// Load
        /// </summary>
        /// <param name="value"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "obj"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        public void Load(object obj)
        {
            if (obj == null)
            {
                this._ilGen.Emit(OpCodes.Ldnull);
            }
            else if (obj is ArgBuilder)
            {
                this.Ldarg((ArgBuilder)obj);
            }
            else if (obj is LocalBuilder)
            {
                this.Ldloc((LocalBuilder)obj);
            }
            else
            {
                this.Ldc(obj);
            }
        }

        /// <summary>
        /// LoadAddress
        /// </summary>
        /// <param name="value"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "obj"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        public void LoadAddress(object obj)
        {
            if (obj is ArgBuilder)
            {
                this.LdargAddress((ArgBuilder)obj);
            }
            else if (obj is LocalBuilder)
            {
                this.LdlocAddress((LocalBuilder)obj);
            }
            else
            {
                this.Load(obj);
            }
        }

        /// <summary>
        /// LoadArrayElement
        /// </summary>
        /// <param name="value"></param>
        /// <param name="arrayIndex"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "obj")]
        public void LoadArrayElement(object obj, object arrayIndex)
        {
            Type objType = GetVariableType(obj).GetElementType();
            this.Load(obj);
            this.Load(arrayIndex);
            if (IsStruct(objType))
            {
                this.Ldelema(objType);
                this.Ldobj(objType);
            }
            else
            {
                this.Ldelem(objType);
            }
        }

        /// <summary>
        /// LoadDefaultValue
        /// </summary>
        /// <param name="type"></param>
        public void LoadDefaultValue(Type type)
        {
            Check.Require(type, "type");

            if (!type.IsValueType)
            {
                this.Load(null);
            }
            else
            {
                switch (Type.GetTypeCode(type))
                {
                    case TypeCode.Boolean:
                        this.Ldc(false);
                        return;

                    case TypeCode.Char:
                    case TypeCode.SByte:
                    case TypeCode.Byte:
                    case TypeCode.Int16:
                    case TypeCode.UInt16:
                    case TypeCode.Int32:
                    case TypeCode.UInt32:
                        this.Ldc(0);
                        return;

                    case TypeCode.Int64:
                    case TypeCode.UInt64:
                        this.Ldc((long)0);
                        return;

                    case TypeCode.Single:
                        this.Ldc((float)0f);
                        return;

                    case TypeCode.Double:
                        this.Ldc((double)0);
                        return;
                }
                LocalBuilder builder = this.DeclareLocal(type, "zero");
                this.LoadAddress(builder);
                this.InitObj(type);
                this.Load(builder);
            }
        }

        /// <summary>
        /// LoadMember
        /// </summary>
        /// <param name="memberInfo"></param>
        /// <returns></returns>
        public Type LoadMember(MemberInfo memberInfo)
        {
            Check.Require(memberInfo, "memberInfo");

            Type stackTopType = null;
            if (memberInfo.MemberType == MemberTypes.Field)
            {
                FieldInfo field = (FieldInfo)memberInfo;
                stackTopType = field.FieldType;
                if (field.IsStatic)
                {
                    this._ilGen.Emit(OpCodes.Ldsfld, field);
                }
                else
                {
                    this._ilGen.Emit(OpCodes.Ldfld, field);
                }
            }
            else if (memberInfo.MemberType == MemberTypes.Property)
            {
                PropertyInfo info2 = memberInfo as PropertyInfo;
                stackTopType = info2.PropertyType;
                if (info2 != null)
                {
                    MethodInfo methodInfo = info2.GetGetMethod(true);
                    Check.Assert(methodInfo, "methodInfo");
                    this.Call(methodInfo);
                }
            }
            else
            {
                Check.Assert(memberInfo.MemberType == MemberTypes.Method, "CannotLoadMemberType");
                MethodInfo info4 = (MethodInfo)memberInfo;
                stackTopType = info4.ReturnType;
                this.Call(info4);
            }
            return stackTopType;
        }

        /// <summary>
        /// LoadParam
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="oneBasedArgIndex"></param>
        /// <param name="methodInfo"></param>
        private void LoadParam(object arg, int oneBasedArgIndex, MethodBase methodInfo)
        {
            this.Load(arg);
            if (arg != null)
            {
                this.ConvertValue(GetVariableType(arg), methodInfo.GetParameters()[oneBasedArgIndex - 1].ParameterType);
            }
        }

        /// <summary>
        /// LoadThis
        /// </summary>
        /// <param name="thisObj"></param>
        /// <param name="methodInfo"></param>
        private void LoadThis(object thisObj, MethodInfo methodInfo)
        {
            if ((thisObj != null) && !methodInfo.IsStatic)
            {
                this.LoadAddress(thisObj);
                this.ConvertAddress(GetVariableType(thisObj), methodInfo.DeclaringType);
            }
        }

        /// <summary>
        /// MarkLabel
        /// </summary>
        /// <param name="label"></param>
        public void MarkLabel(Label label)
        {
            this._ilGen.MarkLabel(label);
        }

        /// <summary>
        /// New
        /// </summary>
        /// <param name="constructorInfo"></param>
        public void New(ConstructorInfo constructorInfo)
        {
            Check.Require(constructorInfo, "constructorInfo");

            this._ilGen.Emit(OpCodes.Newobj, constructorInfo);
        }

        /// <summary>
        /// New
        /// </summary>
        /// <param name="constructorInfo"></param>
        /// <param name="param1"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "param")]
        public void New(ConstructorInfo constructorInfo, object param1)
        {
            Check.Require(constructorInfo, "constructorInfo");

            this.LoadParam(param1, 1, constructorInfo);
            this.New(constructorInfo);
        }

        /// <summary>
        /// NewArray
        /// </summary>
        /// <param name="elementType"></param>
        /// <param name="len"></param>
        public void NewArray(Type elementType, object len)
        {
            Check.Require(elementType, "elementType");

            this.Load(len);
            this._ilGen.Emit(OpCodes.Newarr, elementType);
        }

        /// <summary>
        /// Not
        /// </summary>
        public void Not()
        {
            this._ilGen.Emit(OpCodes.Not);
        }

        /// <summary>
        /// Or
        /// </summary>
        public void Or()
        {
            this._ilGen.Emit(OpCodes.Or);
        }

        /// <summary>
        /// Pop
        /// </summary>
        public void Pop()
        {
            this._ilGen.Emit(OpCodes.Pop);
        }

        private IfState PopIfState()
        {
            object expected = this._blockStack.Pop();
            IfState state = expected as IfState;
            if (state == null)
            {
                ThrowMismatchException(expected);
            }
            return state;
        }

        /// <summary>
        /// Ret
        /// </summary>
        public void Ret()
        {
            this._ilGen.Emit(OpCodes.Ret);
        }

        /// <summary>
        /// Set
        /// </summary>
        /// <param name="local"></param>
        /// <param name="value"></param>
        public void Set(LocalBuilder local, object value)
        {
            Check.Require(local, "local");

            this.Load(value);
            this.Store(local);
        }

        /// <summary>
        /// Starg
        /// </summary>
        /// <param name="slot"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Starg")]
        public void Starg(int slot)
        {
            if (slot <= 0xff)
            {
                this._ilGen.Emit(OpCodes.Starg_S, slot);
            }
            else
            {
                this._ilGen.Emit(OpCodes.Starg, slot);
            }
        }

        /// <summary>
        /// Starg
        /// </summary>
        /// <param name="arg"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Starg")]
        public void Starg(ArgBuilder arg)
        {
            Check.Require(arg, "arg");

            this.Starg(arg.Index);
        }

        /// <summary>
        /// Stelem
        /// </summary>
        /// <param name="arrayElementType"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Stelem")]
        public void Stelem(Type arrayElementType)
        {
            Check.Require(arrayElementType, "arrayElementType");

            if (arrayElementType.IsEnum)
            {
                this.Stelem(Enum.GetUnderlyingType(arrayElementType));
            }
            else
            {
                OpCode opcode = GetStelemOpCode(Type.GetTypeCode(arrayElementType));
                Check.Assert(!opcode.Equals(OpCodes.Nop), "ArrayTypeIsNotSupported");

                this._ilGen.Emit(opcode);
            }
        }

        /// <summary>
        /// Stloc
        /// </summary>
        /// <param name="slot"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Stloc")]
        public void Stloc(int slot)
        {
            switch (slot)
            {
                case 0:
                    this._ilGen.Emit(OpCodes.Stloc_0);
                    return;

                case 1:
                    this._ilGen.Emit(OpCodes.Stloc_1);
                    return;

                case 2:
                    this._ilGen.Emit(OpCodes.Stloc_2);
                    return;

                case 3:
                    this._ilGen.Emit(OpCodes.Stloc_3);
                    return;
            }
            if (slot <= 0xff)
            {
                this._ilGen.Emit(OpCodes.Stloc_S, slot);
            }
            else
            {
                this._ilGen.Emit(OpCodes.Stloc, slot);
            }
        }

        /// <summary>
        /// Stloc
        /// </summary>
        /// <param name="local"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Stloc")]
        public void Stloc(LocalBuilder local)
        {
            Check.Require(local, "local");

            this._ilGen.Emit(OpCodes.Stloc, local);
        }

        /// <summary>
        /// Stobj
        /// </summary>
        /// <param name="type"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Stobj")]
        public void Stobj(Type type)
        {
            Check.Require(type, "type");

            this._ilGen.Emit(OpCodes.Stobj, type);
        }

        /// <summary>
        /// Store
        /// </summary>
        /// <param name="var"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        public void Store(object var)
        {
            if (var is ArgBuilder)
            {
                this.Starg((ArgBuilder)var);
            }
            else
            {
                Check.Assert(var is LocalBuilder, "CanOnlyStoreIntoArgOrLocGot0");
                this.Stloc((LocalBuilder)var);
            }
        }

        /// <summary>
        /// StoreArrayElement
        /// </summary>
        /// <param name="value"></param>
        /// <param name="arrayIndex"></param>
        /// <param name="value"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "obj")]
        public void StoreArrayElement(object obj, object arrayIndex, object value)
        {
            Type variableType = GetVariableType(obj);
            Type objType = (variableType == typeof(Array)) ? typeof(object) : variableType.GetElementType();
            this.Load(obj);
            this.Load(arrayIndex);
            if (IsStruct(objType))
            {
                this.Ldelema(objType);
            }
            this.Load(value);
            this.ConvertValue(GetVariableType(value), objType);
            if (IsStruct(objType))
            {
                this.Stobj(objType);
            }
            else
            {
                this.Stelem(objType);
            }
        }

        /// <summary>
        /// StoreMember
        /// </summary>
        /// <param name="memberInfo"></param>
        public void StoreMember(MemberInfo memberInfo)
        {
            Check.Require(memberInfo, "memberInfo");

            if (memberInfo.MemberType == MemberTypes.Field)
            {
                FieldInfo field = (FieldInfo)memberInfo;
                if (field.IsStatic)
                {
                    this._ilGen.Emit(OpCodes.Stsfld, field);
                }
                else
                {
                    this._ilGen.Emit(OpCodes.Stfld, field);
                }
            }
            else if (memberInfo.MemberType == MemberTypes.Property)
            {
                PropertyInfo info2 = memberInfo as PropertyInfo;
                if (info2 != null)
                {
                    MethodInfo methodInfo = info2.GetSetMethod(true);
                    Check.Assert(methodInfo, "methodInfo");
                    this.Call(methodInfo);
                }
            }
            else
            {
                Check.Assert(memberInfo.MemberType == MemberTypes.Method, "CannotLoadMemberType");

                this.Call((MethodInfo)memberInfo);
            }
        }

        /// <summary>
        /// Subtract
        /// </summary>
        public void Subtract()
        {
            this._ilGen.Emit(OpCodes.Sub);
        }

        /// <summary>
        /// Switch
        /// </summary>
        /// <param name="labelCount"></param>
        /// <returns></returns>
        public Label[] Switch(int labelCount)
        {
            SwitchState state = new SwitchState(this.DefineLabel(), this.DefineLabel());
            Label[] labels = new Label[labelCount];
            for (int i = 0; i < labels.Length; i++)
            {
                labels[i] = this.DefineLabel();
            }
            this._ilGen.Emit(OpCodes.Switch, labels);
            this.Br(state.DefaultLabel);
            this._blockStack.Push(state);
            return labels;
        }

        /// <summary>
        /// Throw
        /// </summary>
        public void Throw()
        {
            this._ilGen.Emit(OpCodes.Throw);
        }

        private static void ThrowMismatchException(object expected)
        {
            throw new AssertionException("ExpectingEnd: " + (expected ?? (object)"null"));
        }

        /// <summary>
        /// ToString
        /// </summary>
        /// <param name="type"></param>
        public void ToString(Type type)
        {
            Check.Require(type, "type");

            if (type.IsValueType)
            {
                this.Box(type);
                this.Call(ObjectToString);
            }
            else
            {
                this.Dup();
                this.Load(null);
                this.If(Cmp.EqualTo);
                this.Pop();
                this.Load("<null>");
                this.Else();
                if (type.IsArray)
                {
                    LocalBuilder var = this.DeclareLocal(type, "arrayVar");
                    this.Store(var);
                    this.Load("{ ");
                    LocalBuilder builder2 = this.DeclareLocal(typeof(string), "arrayValueString");
                    this.Store(builder2);
                    LocalBuilder local = this.DeclareLocal(typeof(int), "i");
                    this.For(local, 0, var);
                    this.Load(builder2);
                    this.LoadArrayElement(var, local);
                    this.ToString(var.LocalType.GetElementType());
                    this.Load(", ");
                    this.Concat3();
                    this.Store(builder2);
                    this.EndFor();
                    this.Load(builder2);
                    this.Load("}");
                    this.Concat2();
                }
                else
                {
                    this.Call(ObjectToString);
                }
                this.EndIf();
            }
        }

        /// <summary>
        /// Unbox
        /// </summary>
        /// <param name="type"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Unbox")]
        public void Unbox(Type type)
        {
            Check.Require(type, "type");
            Check.Require(type.IsValueType, "type MUST be ValueType");

            this._ilGen.Emit(OpCodes.Unbox, type);
        }

        /// <summary>
        /// UnboxAny
        /// </summary>
        /// <param name="type"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Unbox")]
        public void UnboxAny(Type type)
        {
            Check.Require(type, "type");
            Check.Require(type.IsValueType, "type MUST be ValueType");

            this._ilGen.Emit(OpCodes.Unbox_Any, type);
        }

        /// <summary>
        /// VerifyParameterCount
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <param name="expectedCount"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "methodInfo"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "expectedCount")]
        public static void VerifyParameterCount(MethodInfo methodInfo, int expectedCount)
        {
            Check.Require(methodInfo, "methodInfo");

            Check.Assert(methodInfo.GetParameters().Length == expectedCount, "ParameterCountMismatch");
        }

        #endregion

        #region Properties

        /// <summary>
        /// CurrentMethod
        /// </summary>
        public MethodInfo CurrentMethod
        {
            get
            {
                return (this._dynamicMethod as MethodInfo) ?? (this._methodOrConstructorBuilder as MethodInfo);
            }
        }

        /// <summary>
        /// InternalILGenerator
        /// </summary>
        public ILGenerator InternalILGenerator
        {
            get
            {
                return this._ilGen;
            }
        }

        /// <summary>
        /// SerializationModule
        /// </summary>
        public Module SerializationModule
        {
            get
            {
                return this._methodOrConstructorBuilder == null ? this._serializationModule : this._methodOrConstructorBuilder.Module;
            }
        }

        private static MethodInfo GetTypeFromHandle
        {
            get
            {
                if (_getTypeFromHandle == null)
                {
                    _getTypeFromHandle = typeof(Type).GetMethod("GetTypeFromHandle");
                }
                return _getTypeFromHandle;
            }
        }

        private Hashtable LocalNames
        {
            get
            {
                if (this._localNames == null)
                {
                    this._localNames = new Hashtable();
                }
                return this._localNames;
            }
        }

        private static MethodInfo ObjectEquals
        {
            get
            {
                if (_objectEquals == null)
                {
                    _objectEquals = typeof(object).GetMethod("Equals", BindingFlags.Public | BindingFlags.Static);
                }
                return _objectEquals;
            }
        }

        private static MethodInfo ObjectToString
        {
            get
            {
                if (_objectToString == null)
                {
                    _objectToString = typeof(object).GetMethod("ToString", new Type[0]);
                }
                return _objectToString;
            }
        }

        private static MethodInfo StringConcat2
        {
            get
            {
                if (_stringConcat2 == null)
                {
                    _stringConcat2 = typeof(string).GetMethod("Concat", new Type[] { typeof(string), typeof(string) });
                }
                return _stringConcat2;
            }
        }

        private static MethodInfo StringConcat3
        {
            get
            {
                if (_stringConcat3 == null)
                {
                    _stringConcat3 = typeof(string).GetMethod("Concat", new Type[] { typeof(string), typeof(string), typeof(string) });
                }
                return _stringConcat3;
            }
        }

        private static MethodInfo StringFormat
        {
            get
            {
                if (_stringFormat == null)
                {
                    _stringFormat = typeof(string).GetMethod("Format", new Type[] { typeof(string), typeof(object[]) });
                }
                return _stringFormat;
            }
        }

        #endregion
    }

    #region UnitTests

#if DEBUG

    public interface ITestCodeGenerator
    {
        string Wow(string message);
    }

    public static class UnitTestCodeGenerator
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "test")]
        public static void TestEmitInterface()
        {
            AssemblyName assName = new AssemblyName("TestEmitInterface");
            AssemblyBuilder assBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assName, AssemblyBuilderAccess.Run);
            ModuleBuilder modBuilder = assBuilder.DefineDynamicModule(assBuilder.GetName().Name);
            TypeBuilder typeBuilder = modBuilder.DefineType("TestEmitInterface.TestImpl", TypeAttributes.Public);
            typeBuilder.AddInterfaceImplementation(typeof(ITestCodeGenerator));

            CodeGenerator ctor = new CodeGenerator(typeBuilder, "ctor", MethodAttributes.Public, CallingConventions.Standard, null, Type.EmptyTypes);
            ctor.Ldarg(0);
            ctor.Call(typeof(object).GetConstructor(Type.EmptyTypes));
            ctor.Ret();

            MethodInfo mi = typeof(ITestCodeGenerator).GetMethod("Wow");

            CodeGenerator wow = new CodeGenerator(typeBuilder, mi.Name, mi.Attributes & (~MethodAttributes.Abstract) | MethodAttributes.Public, mi.CallingConvention, mi.ReturnType, new Type[] { typeof(string) });
            wow.Ldarg(1);
            wow.Ret();

            typeBuilder.DefineMethodOverride(wow.CurrentMethod, mi);

            Type testImplType = typeBuilder.CreateType();
            ITestCodeGenerator test = (ITestCodeGenerator)Activator.CreateInstance(testImplType);
            Check.Assert(test.Wow("hello") == "hello");
        }
    }

#endif

    #endregion
}
