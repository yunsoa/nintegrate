using System;
using JQuerySharp;
using System.DHTML;

namespace NIntegrate.Scripts.Test
{
    /// <summary>
    /// QUnit is a powerful, easy-to-use, JavaScript test suite.
    /// http://docs.jquery.com/QUnit
    /// Copyright (c) 2009 John Resig, Jörn Zaefferer
    /// Dual licensed under the MIT (MIT-LICENSE.txt)
    /// and GPL (GPL-LICENSE.txt) licenses.
    /// </summary>
    [Imported]
    [IgnoreNamespace]
    public static class QUnit
    {
        /// <summary>
        /// Add a test to run.
        /// </summary>
        /// <param name="name">The name of the test</param>
        /// <param name="test">The actual testing code to run, should include at least one assertion.</param>
        public static void Test(string name, Function test) { }

        /// <summary>
        /// Add a test to run.
        /// </summary>
        /// <param name="name">The name of the test</param>
        /// <param name="test">The actual testing code to run, should include at least one assertion.</param>
        public static void Test(string name, DOMEventHandler test) { }

        /// <summary>
        /// Add a test to run.
        /// </summary>
        /// <param name="name">The name of the test</param>
        /// <param name="expected">How many assertions are expected to run.</param>
        /// <param name="test">The actual testing code to run, should include at least one assertion.</param>
        public static void Test(string name, int expected, Function test) { }

        /// <summary>
        /// Add a test to run.
        /// </summary>
        /// <param name="name">The name of the test</param>
        /// <param name="expected">How many assertions are expected to run.</param>
        /// <param name="test">The actual testing code to run, should include at least one assertion.</param>
        public static void Test(string name, int expected, DOMEventHandler test) { }

        /// <summary>
        /// Add an asynchronous test to run. The test must include a call to start().
        /// </summary>
        /// <param name="name">The name of the test</param>
        /// <param name="test">The actual testing code to run, should include at least one assertion.</param>
        public static void AsyncTest(string name, Function test) { }

        /// <summary>
        /// Add an asynchronous test to run. The test must include a call to start().
        /// </summary>
        /// <param name="name">The name of the test</param>
        /// <param name="test">The actual testing code to run, should include at least one assertion.</param>
        public static void AsyncTest(string name, DOMEventHandler test) { }

        /// <summary>
        /// Add an asynchronous test to run. The test must include a call to start().
        /// </summary>
        /// <param name="name">The name of the test</param>
        /// <param name="expected">How many assertions are expected to run.</param>
        /// <param name="test">The actual testing code to run, should include at least one assertion.</param>
        public static void AsyncTest(string name, int expected, Function test) { }

        /// <summary>
        /// Add an asynchronous test to run. The test must include a call to start().
        /// </summary>
        /// <param name="name">The name of the test</param>
        /// <param name="expected">How many assertions are expected to run.</param>
        /// <param name="test">The actual testing code to run, should include at least one assertion.</param>
        public static void AsyncTest(string name, int expected, DOMEventHandler test) { }

        /// <summary>
        /// Specify how many assertions are expected to run within a test.
        /// </summary>
        /// <param name="amount">The number of assertions you expect to run.</param>
        public static void Expect(int amount) { }

        /// <summary>
        /// Separate tests into modules.
        /// </summary>
        /// <param name="name">The name of the module</param>
        public static void Module(string name) { }

        /// <summary>
        /// Initialize the test runner (if the runner has already run it'll be re-initialized, effectively resetting it).
        /// This method does not need to be called in the normal use of QUnit.
        /// </summary>
        public static void Init() { }

        /// <summary>
        /// A boolean assertion, equivalent to JUnit's assertTrue. Passes if the first argument is truthy.
        /// </summary>
        /// <param name="state">A boolean expression, can be a boolean or any other type, its boolean default is evaluated.</param>
        public static void Ok(bool state) { }

        /// <summary>
        /// A boolean assertion, equivalent to JUnit's assertTrue. Passes if the first argument is truthy.
        /// </summary>
        /// <param name="state">A boolean expression, can be a boolean or any other type, its boolean default is evaluated.</param>
        /// <param name="message">A message to output with the assertion result.</param>
        public static void Ok(bool state, string message) { }

        /// <summary>
        /// A comparison assertion, equivalent to JUnit's assertEquals.
        /// </summary>
        /// <param name="actual">The actual result</param>
        /// <param name="expected">The expected result</param>
        public static void Equals(object actual, object expected) { }

        /// <summary>
        /// A comparison assertion, equivalent to JUnit's assertEquals.
        /// </summary>
        /// <param name="actual">The actual result</param>
        /// <param name="expected">The expected result</param>
        /// <param name="message">A message to output with the assertion result.</param>
        public static void Equals(object actual, object expected, string message) { }

        /// <summary>
        /// A deep recursive comparison assertion, working on primitive types, arrays and objects.
        /// </summary>
        /// <param name="actual">The actual result</param>
        /// <param name="expected">The expected result</param>
        public static void Same(object actual, object expected) { }

        /// <summary>
        /// A deep recursive comparison assertion, working on primitive types, arrays and objects.
        /// </summary>
        /// <param name="actual">The actual result</param>
        /// <param name="expected">The expected result</param>
        /// <param name="message">A message to output with the assertion result.</param>
        public static void Same(object actual, object expected, string message) { }

        /// <summary>
        /// Start running tests again after the testrunner was stopped.
        /// </summary>
        public static void Start() { }

        /// <summary>
        /// Stop the testrunner to wait to async tests to run.
        /// </summary>
        public static void Stop() { }

        /// <summary>
        /// Stop the testrunner to wait to async tests to run.
        /// </summary>
        /// <param name="timeout">Optional argument to fail the test after the given timeout. In milliseconds.</param>
        public static void Stop(int timeout) { }
    }
}
