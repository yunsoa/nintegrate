using System;
using ScriptFX;
using NIntegrate.Scripts;

namespace jqMock
{
    /// <summary>
    /// jqMock - The JavaScript Mock framework for QUnit
    /// http://code.google.com/p/jqmock/
    /// version 1.1
    /// Copyright 2008 Kenneth Ko, under the GNU Lesser General Public License
    /// </summary>
    [Imported]
    public class Mock
    {
        /// <summary>
        /// Constructor, creates a Mock on the function obj.fnName
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="fnName"></param>
        public Mock(object obj, string fnName) { }

        /// <summary>
        /// Returns a Modifier object, which represent a single expectation.
        /// </summary>
        /// <returns></returns>
        public Modifier Modify() { return null; }

        /// <summary>
        /// Whether the mock will use ordered expectation
        /// </summary>
        /// <param name="isOrdered"></param>
        public void SetOrdered(bool isOrdered) { }

        /// <summary>
        /// Verfies whether the expectations have been satisfied, recording the results into the QUnit framework.
        /// </summary>
        public void Verify() { }

        /// <summary>
        /// Removes all expectations and resets all counts, but leave the interception. allows a mock to be reused.
        /// </summary>
        public void Reset() { }

        /// <summary>
        /// Call reset(), and removes the interception
        /// </summary>
        public void Restore() { }
    }

    [Imported]
    [IgnoreNamespace]
    public class Modifier
    {
        /// <summary>
        /// The expectation is matched using these arguments, can be any number of arguments. 
        /// Arguments may be of any type, or may be an Expression.
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public Modifier Args(params object[] parms) { return null; }

        /// <summary>
        /// Number of times you this expectation should match. 
        /// A number parameter means exactly that number of times.
        /// </summary>
        /// <param name="number"></param>
        public void Multiplicity(int number) { }

        /// <summary>
        /// Number of times you this expectation should match, parameter is a Multiplicity object.
        /// </summary>
        /// <param name="multiplicity"></param>
        public void Multiplicity(Multiplicity multiplicity) { }

        /// <summary>
        /// Intercept the method, and return nothing.
        /// </summary>
        public void ReturnValue() { }

        /// <summary>
        /// Intercept the method, and return the given returnValue instead.
        /// </summary>
        /// <param name="returnValue"></param>
        public void ReturnValue(object returnValue) { }

        /// <summary>
        /// Intercept the method, and call specified callback before return. Extended by Teddy.
        /// </summary>
        /// <param name="fnIndex">The index of the fn in the arguments Array to be callbacked</param>
        /// <param name="fnArgs"></param>
        /// <returns></returns>
        public Modifier Callback(int fnIndex, Array fnArgs) { return null; }

        /// <summary>
        /// Intercept the method, and call specified callback before return. Extended by Teddy.
        /// </summary>
        /// <param name="getCallbackMethod">The delegate to get the callback method.</param>
        /// <param name="fnArgs"></param>
        /// <returns></returns>
        public Modifier Callback(ReturnSomethingHandler getCallbackMethod, Array fnArgs) { return null; }
    }

    [Imported]
    [IgnoreNamespace]
    public class Multiplicity
    {
    }

    [Imported]
    [IgnoreNamespace]
    public static class Expect
    {
        /// <summary>
        /// Expect that an argument is matched exactly n times
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static Multiplicity Exactly(int n) { return null; }

        /// <summary>
        /// An alias for exactly(n)
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static Multiplicity Times(int n) { return null; }

        /// <summary>
        /// range multiplicity. Expect that an argument is matched at least n times, and at most m times.
        /// </summary>
        /// <param name="n"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Multiplicity Times(int n, int m) { return null; }

        /// <summary>
        /// Expect that an argument is matched at least n times.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static Multiplicity AtLeast(int n) { return null; }

        /// <summary>
        /// Expect that an argument is matched at most n times. 
        /// This argument is initially satisfied, and becomes unsatisfied if it matches more than n times.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static Multiplicity AtMost(int n) { return null; }
    }

    [Imported]
    [IgnoreNamespace]
    public static class Is
    {
        /// <summary>
        /// Always matches anything
        /// </summary>
        public static object Anything;

        /// <summary>
        /// Will match if the value is NOT the parameter
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static object Not(object x) { return null; }

        /// <summary>
        /// Applies the instanceof operator, whether obj instanceof Class
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object InstanceOf(object type) { return null; }

        /// <summary>
        /// Whether it matches the regex expression provided
        /// </summary>
        /// <param name="regex"></param>
        /// <returns></returns>
        public static object Regex(RegularExpression regex) { return null; }

        /// <summary>
        /// Matches one of the expressions in the array provided
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static object AnyOf(Array values) { return null; }

        /// <summary>
        /// Must match all of the expressions in the array provided.
        /// Useful for matching multiple not expressions
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static object AllOf(Array values) { return null; }

        /// <summary>
        /// matches if the actual object has every field specified in the expected object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static object ObjectThatIncludes(object obj) { return null; }

        /// <summary>
        /// Specify a custom expression as a function that takes in a single argument and returns a boolean value
        /// </summary>
        /// <param name="fn"></param>
        /// <returns></returns>
        public static object Custom(Function fn) { return null; }
    }
}
