using System;
using System.Reflection;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NIntegrate.Utilities.Reflection;
using System.Reflection.Emit;
using NIntegrate.Test.Utilities.TestClasses;

namespace NIntegrate.Test.Utilities
{
    [TestClass]
    public class ILCodeGeneratorTest
    {
        public delegate object[] TestLoadMethodsDelegate(Type type);

        public delegate void TestFieldPropertyMethodsDelegate(TestClass obj);

        public delegate string TestCallStringFormatMethodDelegate(int intVal, string strVal, TestClass test);

        public delegate int TestConditionMethodsDelegate(int left1, int right1, int left2, int right2);

        public delegate void TestTryCatchMethodsDelegate(int intVal);

        [TestMethod]
        public void TestLoadMethods()
        {
            DynamicMethod dm;
            var gen = ILCodeGenerator.CreateDynamicMethodCodeGenerator(
                "TestLoadMethods", typeof(TestLoadMethodsDelegate), out dm);

            gen.DeclareLocalVariable(typeof (object[]));

            gen.StoreLocalVariable(0, val => val.NewArray(typeof (object), len => len.Load(9)))
                .StoreArrayElement(
                    arr => arr.LoadLocalVariable(0),
                    typeof(object),
                    0,
                    item => item.Box(typeof(bool), val => val.Load(true)),
                    typeof(object))
                .StoreArrayElement(
                    arr => arr.LoadLocalVariable(0),
                    typeof(object),
                    1,
                    item => item.Box(typeof(double), val => val.Load(1.1)),
                    typeof(object))
                .StoreArrayElement(
                    arr => arr.LoadLocalVariable(0),
                    typeof(object),
                    2,
                    item => item.Box(typeof(int), val => val.Load(1)),
                    typeof(object))
                .StoreArrayElement(
                    arr => arr.LoadLocalVariable(0),
                    typeof(object),
                    3,
                    item => item.Box(typeof(long), val => val.Load((long)2)),
                    typeof(object))
                .StoreArrayElement(
                    arr => arr.LoadLocalVariable(0),
                    typeof(object),
                    4,
                    item => item.Box(typeof(float), val => val.Load(3.3f)),
                    typeof(object))
                .StoreArrayElement(
                    arr => arr.LoadLocalVariable(0),
                    typeof(object),
                    5,
                    item => item.Load("str"),
                    typeof(string))
                .StoreArrayElement(
                    arr => arr.LoadLocalVariable(0),
                    typeof(object),
                    6,
                    item => item.LoadArgument(0),
                    typeof(Type))
                .StoreArrayElement(
                    arr => arr.LoadLocalVariable(0),
                    typeof(object),
                    7,
                    item => item.Box(typeof(Guid), val => val.LoadDefaultValue(typeof(Guid))),
                    typeof(object))
                .StoreArrayElement(
                    arr => arr.LoadLocalVariable(0),
                    typeof(object),
                    8,
                    item => item.LoadTypeOf(GetType()),
                    typeof(Type))
                .LoadLocalVariable(0)
                .Ret();

            var method = (TestLoadMethodsDelegate)dm.CreateDelegate(typeof(TestLoadMethodsDelegate));
            var result = method(GetType());
            Assert.AreEqual(true, result[0]);
            Assert.AreEqual(1.1, result[1]);
            Assert.AreEqual(1, result[2]);
            Assert.AreEqual((long)2, result[3]);
            Assert.AreEqual(3.3f, result[4]);
            Assert.AreEqual("str", result[5]);
            Assert.AreEqual(GetType(), result[6]);
            Assert.AreEqual(default(Guid), result[7]);
            Assert.AreEqual(GetType(), result[8]);
        }

        [TestMethod]
        public void TestFieldPropertyMethods()
        {
            DynamicMethod dm;
            var gen = ILCodeGenerator.CreateDynamicMethodCodeGenerator(
                "TestFieldPropertyMethods", typeof(TestFieldPropertyMethodsDelegate), out dm);

            gen.StoreField(thisObj => thisObj.LoadArgument(0),
                typeof(TestClass).GetField("IntField"),
                val => val.Add(
                    left => left.LoadField(thisObj2 => thisObj2.LoadArgument(0), typeof(TestClass).GetField("IntField")),
                    right => right.Load(1)))

                .StoreProperty(thisObj => thisObj.LoadArgument(0),
                    typeof(TestClass).GetProperty("StringProperty"),
                    val2 => val2.Load("str"))

                .StoreProperty(thisObj => thisObj.LoadArgument(0),
                    typeof(TestClass).GetProperty("WriteonlyStringValue"),
                    val2 => val2.CallToString(
                        typeof(int), val3 => val3.LoadStaticProperty(
                                typeof(TestClass).GetProperty("ReadonlyIntValue", 
                                BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance))))

                .Ret();

            var method = (TestFieldPropertyMethodsDelegate)dm.CreateDelegate(typeof(TestFieldPropertyMethodsDelegate));
            var obj = new TestClass();
            method(obj);
            Assert.AreEqual(1, obj.IntField);
            Assert.AreEqual("str", obj.StringProperty);
            Assert.AreEqual(1, TestClass.ReadonlyIntValue);
            Assert.AreEqual("1", obj.GetWriteonlyStringValue());
        }

        [TestMethod]
        public void TestCallStringFormatMethod()
        {
            DynamicMethod dm;
            var gen = ILCodeGenerator.CreateDynamicMethodCodeGenerator(
                "TestCallStringFormatMethod", typeof(TestCallStringFormatMethodDelegate), out dm);

            gen.CallStringFormat(
                msg => msg.Load("{0}{1}{2}"),
                intVal => intVal.Box(typeof(int), box1 => box1.LoadArgument(0)),
                strVal => strVal.LoadArgument(1),
                test => test.LoadArgument(2))
                .Ret();

            var method = (TestCallStringFormatMethodDelegate)dm.CreateDelegate(typeof(TestCallStringFormatMethodDelegate));
            var result = method(1, "2", new TestClass());

            Assert.AreEqual(string.Format("{0}{1}{2}", 1, "2", new TestClass()), result);
        }

        [TestMethod]
        public void TestConditionMethods()
        {
            DynamicMethod dm;
            var gen = ILCodeGenerator.CreateDynamicMethodCodeGenerator(
                "TestConditionMethods", typeof(TestConditionMethodsDelegate), out dm);

            gen.IfGreaterThan(
                left1 => left1.LoadArgument(0),
                right1 => right1.LoadArgument(1))
                
                .Load(1)

                .ElseIf(boolVal => boolVal.Equals(
                    left2 => left2.LoadArgument(2),
                    right2 => right2.LoadArgument(3)))

                    .Load(2)

                    .Else()

                    .Load(3)

                    .EndIf()

                    .Ret();

            var method = (TestConditionMethodsDelegate)dm.CreateDelegate(typeof(TestConditionMethodsDelegate));
            var result = method(1, 2, 3, 4);
            Assert.AreEqual(3, result);
            result = method(5, 2, 3, 4);
            Assert.AreEqual(1, result);
            result = method(1, 2, 3, 3);
            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void TestTryCatchMethods()
        {
            DynamicMethod dm;
            var gen = ILCodeGenerator.CreateDynamicMethodCodeGenerator(
                "TestTryCatchMethods", typeof(TestTryCatchMethodsDelegate), out dm);

            gen.Try()
                .IfEquals(left => left.LoadArgument(0), right => right.Load(1))
                .Throw(typeof(ArgumentNullException), ex => ex.New(typeof(ArgumentNullException).GetConstructor(new[] { typeof(string) }), p1 => p1.Load("test")))
                .Else()
                .Throw(typeof(ArgumentException), ex => ex.New(typeof(ArgumentException).GetConstructor(Type.EmptyTypes)))
                .EndIf()
                .Catch(typeof(ArgumentException))
                .Rethrow()
                .Finally()
                .EndTry()
                .Ret();

            var method = (TestTryCatchMethodsDelegate)dm.CreateDelegate(typeof(TestTryCatchMethodsDelegate));

            Exception expected = null;
            try
            {
                method(1);
            }
            catch (ArgumentNullException ex)
            {
                expected = ex;
            }

            Assert.IsNotNull(expected);
            expected = null;

            try
            {
                method(2);
            }
            catch (ArgumentException ex)
            {
                expected = ex;
            }

            Assert.IsNotNull(expected);
        }
    }
}
