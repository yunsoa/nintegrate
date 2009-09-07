// Design By Contract Framework C# Implementation
//
// Last Updated: 
//
// By Teddy (shijie.ma@gmail.com) @ 14 Aug, 2008
//
// Introduction Article to the Latest Enhanced Version:
//
// http://www.cnblogs.com/teddyma/archive/2007/10/05/914656.html
//
// Change History:
//
// 14 Aug, 2008
// Did necessary code change for passing Code Analysis check.
//
// 13 Jul, 2002      
// The initial version got from http://www.codeproject.com/csharp/designbycontract.asp
//
// Description:
//
// Provides support for Design By Contract
// as described by Bertrand Meyer in his seminal book,
// Object-Oriented Software Construction (2nd Ed) Prentice Hall 1997
// (See chapters 11 and 12).
//
// See also Building Bug-free O-O Software: An Introduction to Design by Contract
// http://www.eiffel.com/doc/manuals/technology/contract/
//
// The following conditional compilation symbols are supported:
// 
// These suggestions are based on Bertrand Meyer's Object-Oriented Software Construction (2nd Ed) p393
// 
// DBC_CHECK_ALL           - Check assertions - implies checking preconditions, postconditions and invariants
// DBC_CHECK_INVARIANT     - Check invariants - implies checking preconditions and postconditions
// DBC_CHECK_POSTCONDITION - Check postconditions - implies checking preconditions 
// DBC_CHECK_PRECONDITION  - Check preconditions only, e.g., in Release build
// 
// A suggested default usage scenario is the following:
//
//#if DEBUG
#define DBC_CHECK_ALL
//#else
//#define DBC_CHECK_PRECONDITION
//#endif
//
// Alternatively, you can define these in the project properties dialog.
//
// If you wish to use trace or debug assertion statements, intended for Debug scenarios,
// rather than exception handling then you can specify the following line in your application entry point 
// and maybe make it dependent on conditional compilation flags or configuration file settings, e.g.,
// Default is to use exception handling, or uncomment the following lines to use trace or debug assertion.
//
// #define USE_TRACE_ASSERTION
// or
// #define USE_DEBUG_ASSERTION
//
// You can direct output to a Trace listener. For example, you could insert
// (You can replace the System.Diagnostics.Trace here with System.Diagnostics.Debug
// if you are using Debug Assertion)
//
// System.Diagnostics.Trace.Listeners.Clear();
// System.Diagnostics.Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
// 
// or direct output to a file or the Event Log.
// 
// (Note: For ASP.NET clients use the Listeners collection
// of the Debug, not the Trace, object and, for a Release build, only exception-handling
// is possible.)
//

using System;
using System.Diagnostics;
using System.Collections;
using System.Runtime.Serialization;
using System.Globalization;
using NIntegrate.Data.Exceptions;

namespace NIntegrate.Data
{
    /// <summary>
    /// Design By Contract Checks.
    /// 
    /// Each method generates an exception or
    /// a trace assertion statement if the contract is broken.
    /// </summary>
    /// <remarks>
    /// This example shows how to call the Require method.
    /// Assume DBC_CHECK_PRECONDITION is defined.
    /// <code>
    /// public void Test(int x)
    /// {
    /// 	try
    /// 	{
    ///			Check.Require(x > 1, "x must be > 1");
    ///		}
    ///		catch (System.Exception ex)
    ///		{
    ///			Console.WriteLine(ex.ToString());
    ///		}
    ///	}
    /// </code>
    /// </remarks>
    /// 
    internal sealed class Check
    {
        #region Const Literals

        private const string DBC_CHECK_ALL = "DBC_CHECK_ALL";
        private const string DBC_CHECK_INVARIANT = "DBC_CHECK_INVARIANT";
        private const string DBC_CHECK_POSTCONDITION = "DBC_CHECK_POSTCONDITION";
        private const string DBC_CHECK_PRECONDITION = "DBC_CHECK_PRECONDITION";

        private const string PRECONDITION_COLON = "Precondition: ";
        private const string POSTCONDITION_COLON = "Postcondition: ";
        private const string ASSERTION_COLON = "Assertion: ";
        private const string INVARIANT_COLON = "Invariant: ";

        private const string PRECONDITION_FALIED = "Precondition failed.";
        private const string POSTCONDITION_FALIED = "Postcondition failed.";
        private const string ASSERTION_FAILED = "Assertion failed.";
        private const string INVARIANT_FALIED = "Invariant failed.";

        private const string NOT_NULL_FAILING_MESSAGE = "{0} could not be null";
        private const string NOT_NULL_OR_EMPTY_FAILING_MESSAGE = "{0} could not be null or empty";
        private const string IS_ASSIGNABLE_TO_FAILING_MESSAGE = "{0} is not assignable to {1}";
        private const string GREATER_THAN_FAILING_MESSAGE = "{0} must be > {1}";
        private const string GREATER_THAN_OR_EQUAL_FAILING_MESSAGE = "{0} must be >= {1}";
        private const string LESS_THAN_FAILING_MESSAGE = "{0} must be < {1}";
        private const string LESS_THAN_OR_EQUAL_FAILING_MESSAGE = "{0} must be <= {1}";

        #endregion  // End Const Literals

        #region Pluginable Check Strategies

        private static void CheckByStrategies(object value, string name, ICheckStrategy[] strategies, ref bool assertion, ref string message)
        {
            if (strategies == null || strategies.Length == 0)
            {
                if (!NotNull.Pass(value))
                {
                    assertion = false;
                    message = NotNull.GetFailingMessage(name);
                }
            }
            else
            {
                for (int i = 0; i < strategies.Length; ++i)
                {
                    if (!strategies[i].Pass(value))
                    {
                        assertion = false;
                        message = strategies[i].GetFailingMessage(name);
                        break;
                    }
                }
            }
        }

        #region Predefined Check Strategies

        #region Nested Classes

        /// <summary>
        /// IDesignByContractCheckStrategy
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
        public interface ICheckStrategy
        {
            /// <summary>
            /// Chech the value with the strategy
            /// </summary>
            /// <param name="value">the value</param>
            /// <returns>true for pass, or return false</returns>
            bool Pass(object value);
            /// <summary>
            /// Get the message when check failed
            /// </summary>
            /// <param name="name"></param>
            /// <returns></returns>
            string GetFailingMessage(string name);
        }

        private sealed class NotNullCheckStrategy : ICheckStrategy
        {
            #region ICheckStrategy Members

            public bool Pass(object value)
            {
                return value != null;
            }

            public string GetFailingMessage(string name)
            {
                return string.Format(CultureInfo.InvariantCulture, NOT_NULL_FAILING_MESSAGE, name);
            }

            #endregion
        }

        private sealed class NotNullOrEmptyStrategy : ICheckStrategy
        {
            #region ICheckStrategy Members

            public bool Pass(object value)
            {
                if (value == null)
                    return false;
                else if (value is string)
                    return value.ToString().Length > 0;

                ICollection castToCollection = value as ICollection;
                if (castToCollection != null)
                    return castToCollection.Count > 0;

                return true;
            }

            public string GetFailingMessage(string name)
            {
                return string.Format(CultureInfo.InvariantCulture, NOT_NULL_OR_EMPTY_FAILING_MESSAGE, name);
            }

            #endregion
        }

        private sealed class IsAssignableToStrategy<TargetType> : ICheckStrategy
        {
            #region ICheckStrategy Members

            public bool Pass(object value)
            {
                return value is TargetType;
            }

            public string GetFailingMessage(string name)
            {
                return string.Format(CultureInfo.InvariantCulture, IS_ASSIGNABLE_TO_FAILING_MESSAGE, name, typeof(TargetType));
            }

            #endregion
        }

        private sealed class GreaterThanStrategy<T> : ICheckStrategy
        {
            private T compareValue;

            public GreaterThanStrategy(T compareValue)
            {
                this.compareValue = compareValue;
            }

            #region ICheckStrategy Members

            public bool Pass(object value)
            {
                if (value is T && ((IComparable)value).CompareTo(compareValue) > 0)
                    return true;

                return false;
            }

            public string GetFailingMessage(string name)
            {
                return string.Format(CultureInfo.InvariantCulture, GREATER_THAN_FAILING_MESSAGE, name, compareValue);
            }

            #endregion
        }

        private sealed class GreaterThanOrEqualStrategy<T> : ICheckStrategy
        {
            private T compareValue;

            public GreaterThanOrEqualStrategy(T compareValue)
            {
                this.compareValue = compareValue;
            }

            #region ICheckStrategy Members

            public bool Pass(object value)
            {
                if (value is T && ((IComparable)value).CompareTo(compareValue) >= 0)
                    return true;

                return false;
            }

            public string GetFailingMessage(string name)
            {
                return string.Format(CultureInfo.InvariantCulture, GREATER_THAN_OR_EQUAL_FAILING_MESSAGE, name, compareValue);
            }

            #endregion
        }

        private sealed class LessThanStrategy<T> : ICheckStrategy
        {
            private T compareValue;

            public LessThanStrategy(T compareValue)
            {
                this.compareValue = compareValue;
            }

            #region ICheckStrategy Members

            public bool Pass(object value)
            {
                if (value is T && ((IComparable)value).CompareTo(compareValue) < 0)
                    return true;

                return false;
            }

            public string GetFailingMessage(string name)
            {
                return string.Format(CultureInfo.InvariantCulture, LESS_THAN_FAILING_MESSAGE, name, compareValue);
            }

            #endregion
        }

        private sealed class LessThanOrEqualStrategy<T> : ICheckStrategy
        {
            private T compareValue;

            public LessThanOrEqualStrategy(T compareValue)
            {
                this.compareValue = compareValue;
            }

            #region ICheckStrategy Members

            public bool Pass(object value)
            {
                if (value is T && ((IComparable)value).CompareTo(compareValue) <= 0)
                    return true;

                return false;
            }

            public string GetFailingMessage(string name)
            {
                return string.Format(CultureInfo.InvariantCulture, LESS_THAN_OR_EQUAL_FAILING_MESSAGE, name, compareValue);
            }

            #endregion
        }

        #endregion

        /// <summary>
        /// NotNullCheckStrategy singleton
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly ICheckStrategy NotNull = new NotNullCheckStrategy();
        /// <summary>
        /// NotNullOrEmptyStrategy singleton
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly ICheckStrategy NotNullOrEmpty = new NotNullOrEmptyStrategy();
        /// <summary>
        /// Create IsAssignableToStrategy inatance
        /// </summary>
        /// <typeparam name="TargetType">The type</typeparam>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        public static ICheckStrategy IsAssignableTo<T>()
        {
            return new IsAssignableToStrategy<T>();
        }
        /// <summary>
        /// &gt;
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="compareValue"></param>
        /// <returns></returns>
        public static ICheckStrategy GreaterThan<T>(T compareValue)
        {
            return new GreaterThanStrategy<T>(compareValue);
        }
        /// <summary>
        /// &lt;
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="compareValue"></param>
        /// <returns></returns>
        public static ICheckStrategy LessThan<T>(T compareValue)
        {
            return new LessThanStrategy<T>(compareValue);
        }
        /// <summary>
        /// &gt;=
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="compareValue"></param>
        /// <returns></returns>
        public static ICheckStrategy GreaterThanOrEqual<T>(T compareValue)
        {
            return new GreaterThanOrEqualStrategy<T>(compareValue);
        }
        /// <summary>
        /// &lt;=
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="compareValue"></param>
        /// <returns></returns>
        public static ICheckStrategy LessThanOrEqual<T>(T compareValue)
        {
            return new LessThanOrEqualStrategy<T>(compareValue);
        }

        #endregion

        #endregion

        #region Check Methods

        #region Precondition

        /// <summary>
        /// Precondition check.
        /// </summary>
        [Conditional(DBC_CHECK_ALL)]
        [Conditional(DBC_CHECK_INVARIANT)]
        [Conditional(DBC_CHECK_POSTCONDITION)]
        [Conditional(DBC_CHECK_PRECONDITION)]
        public static void Require(bool assertion, string message)
        {
            if (UseExceptions)
            {
                if (!assertion) throw new PreconditionException(message);
            }
            else
            {
#if USE_TRACE_ASSERTION
                Trace.Assert(assertion, PRECONDITION_COLON + message);
#elif USE_DEBUG_ASSERTION
                Debug.Assert(assertion, PRECONDITION_COLON + message);
#endif
            }
        }

        /// <summary>
        /// Precondition check.
        /// </summary>
        [Conditional(DBC_CHECK_ALL)]
        [Conditional(DBC_CHECK_INVARIANT)]
        [Conditional(DBC_CHECK_POSTCONDITION)]
        [Conditional(DBC_CHECK_PRECONDITION)]
        public static void Require(bool assertion, string message, Exception inner)
        {
            if (UseExceptions)
            {
                if (!assertion) throw new PreconditionException(message, inner);
            }
            else
            {
#if USE_TRACE_ASSERTION
                Trace.Assert(assertion, PRECONDITION_COLON + message);
#elif USE_DEBUG_ASSERTION
                Debug.Assert(assertion, PRECONDITION_COLON + message);
#endif
            }
        }

        /// <summary>
        /// Precondition check.
        /// </summary>
        [Conditional(DBC_CHECK_ALL)]
        [Conditional(DBC_CHECK_INVARIANT)]
        [Conditional(DBC_CHECK_POSTCONDITION)]
        [Conditional(DBC_CHECK_PRECONDITION)]
        public static void Require(bool assertion)
        {
            if (UseExceptions)
            {
                if (!assertion) throw new PreconditionException(PRECONDITION_FALIED);
            }
            else
            {
#if USE_TRACE_ASSERTION
                Trace.Assert(assertion, PRECONDITION_FALIED);
#elif USE_DEBUG_ASSERTION
                Debug.Assert(assertion, PRECONDITION_FALIED);
#endif
            }
        }

        /// <summary>
        /// Precondition check.
        /// </summary>
        [Conditional(DBC_CHECK_ALL)]
        [Conditional(DBC_CHECK_INVARIANT)]
        [Conditional(DBC_CHECK_POSTCONDITION)]
        [Conditional(DBC_CHECK_PRECONDITION)]
        public static void Require(object value, string name, params ICheckStrategy[] strategies)
        {
            bool assertion = true;
            string message = null;

            CheckByStrategies(value, name, strategies, ref assertion, ref message);

            if (UseExceptions)
            {
                if (!assertion) throw new PreconditionException(message);
            }
            else
            {
#if USE_TRACE_ASSERTION
                Trace.Assert(assertion, PRECONDITION_COLON + message);
#elif USE_DEBUG_ASSERTION
                Debug.Assert(assertion, PRECONDITION_COLON + message);
#endif
            }
        }

        #endregion  // End Precondition

        #region Postcondition

        /// <summary>
        /// Postcondition check.
        /// </summary>
        [Conditional(DBC_CHECK_ALL)]
        [Conditional(DBC_CHECK_INVARIANT)]
        [Conditional(DBC_CHECK_POSTCONDITION)]
        public static void Ensure(bool assertion, string message)
        {
            if (UseExceptions)
            {
                if (!assertion) throw new PostconditionException(message);
            }
            else
            {
#if USE_TRACE_ASSERTION
                Trace.Assert(assertion, POSTCONDITION_COLON + message);
#elif USE_DEBUG_ASSERTION
                Debug.Assert(assertion, POSTCONDITION_COLON + message);
#endif
            }
        }

        /// <summary>
        /// Postcondition check.
        /// </summary>
        [Conditional(DBC_CHECK_ALL)]
        [Conditional(DBC_CHECK_INVARIANT)]
        [Conditional(DBC_CHECK_POSTCONDITION)]
        public static void Ensure(bool assertion, string message, Exception inner)
        {
            if (UseExceptions)
            {
                if (!assertion) throw new PostconditionException(message, inner);
            }
            else
            {
#if USE_TRACE_ASSERTION
                Trace.Assert(assertion, POSTCONDITION_COLON + message);
#elif USE_DEBUG_ASSERTION
                Debug.Assert(assertion, POSTCONDITION_COLON + message);
#endif
            }
        }

        /// <summary>
        /// Postcondition check.
        /// </summary>
        [Conditional(DBC_CHECK_ALL)]
        [Conditional(DBC_CHECK_INVARIANT)]
        [Conditional(DBC_CHECK_POSTCONDITION)]
        public static void Ensure(bool assertion)
        {
            if (UseExceptions)
            {
                if (!assertion) throw new PostconditionException(POSTCONDITION_FALIED);
            }
            else
            {
#if USE_TRACE_ASSERTION
                Trace.Assert(assertion, POSTCONDITION_FALIED);
#elif USE_DEBUG_ASSERTION
                Debug.Assert(assertion, POSTCONDITION_FALIED);
#endif
            }
        }

        /// <summary>
        /// Postcondition check.
        /// </summary>
        [Conditional(DBC_CHECK_ALL)]
        [Conditional(DBC_CHECK_INVARIANT)]
        [Conditional(DBC_CHECK_POSTCONDITION)]
        public static void Ensure(object value, string name, params ICheckStrategy[] strategies)
        {
            bool assertion = true;
            string message = null;

            CheckByStrategies(value, name, strategies, ref assertion, ref message);

            if (UseExceptions)
            {
                if (!assertion) throw new PostconditionException(message);
            }
            else
            {
#if USE_TRACE_ASSERTION
                Trace.Assert(assertion, POSTCONDITION_COLON + message);
#elif USE_DEBUG_ASSERTION
                Debug.Assert(assertion, POSTCONDITION_COLON + message);
#endif
            }
        }

        #endregion  // End Postcondition

        #region Invariant

        /// <summary>
        /// Invariant check.
        /// </summary>
        [Conditional(DBC_CHECK_ALL)]
        [Conditional(DBC_CHECK_INVARIANT)]
        public static void Invariant(bool assertion, string message)
        {
            if (UseExceptions)
            {
                if (!assertion) throw new InvariantException(message);
            }
            else
            {
#if USE_TRACE_ASSERTION
                Trace.Assert(assertion, INVARIANT_COLON + message);
#elif USE_DEBUG_ASSERTION
                Debug.Assert(assertion, INVARIANT_COLON + message);
#endif
            }
        }

        /// <summary>
        /// Invariant check.
        /// </summary>
        [Conditional(DBC_CHECK_ALL)]
        [Conditional(DBC_CHECK_INVARIANT)]
        public static void Invariant(bool assertion, string message, Exception inner)
        {
            if (UseExceptions)
            {
                if (!assertion) throw new InvariantException(message, inner);
            }
            else
            {
#if USE_TRACE_ASSERTION
                Trace.Assert(assertion, INVARIANT_COLON + message);
#elif USE_DEBUG_ASSERTION
                Debug.Assert(assertion, INVARIANT_COLON + message);
#endif
            }
        }

        /// <summary>
        /// Invariant check.
        /// </summary>
        [Conditional(DBC_CHECK_ALL)]
        [Conditional(DBC_CHECK_INVARIANT)]
        public static void Invariant(bool assertion)
        {
            if (UseExceptions)
            {
                if (!assertion) throw new InvariantException(INVARIANT_FALIED);
            }
            else
            {
#if USE_TRACE_ASSERTION
                Trace.Assert(assertion, INVARIANT_FALIED);
#elif USE_DEBUG_ASSERTION
                Debug.Assert(assertion, INVARIANT_FALIED);
#endif
            }
        }

        /// <summary>
        /// Invariant check.
        /// </summary>
        [Conditional(DBC_CHECK_ALL)]
        [Conditional(DBC_CHECK_INVARIANT)]
        public static void Invariant(object value, string name, params ICheckStrategy[] strategies)
        {
            bool assertion = true;
            string message = null;

            CheckByStrategies(value, name, strategies, ref assertion, ref message);

            if (UseExceptions)
            {
                if (!assertion) throw new InvariantException(message);
            }
            else
            {
#if USE_TRACE_ASSERTION
                Trace.Assert(assertion, INVARIANT_COLON + message);
#elif USE_DEBUG_ASSERTION
                Debug.Assert(assertion, INVARIANT_COLON + message);
#endif
            }
        }

        #endregion  // End Invariant

        #region Assertion

        /// <summary>
        /// Assertion check.
        /// </summary>
        [Conditional(DBC_CHECK_ALL)]
        public static void Assert(bool assertion, string message)
        {
            if (UseExceptions)
            {
                if (!assertion) throw new AssertionException(message);
            }
            else
            {
#if USE_TRACE_ASSERTION
                Trace.Assert(assertion, ASSERTION_COLON + message);
#elif USE_DEBUG_ASSERTION
                Debug.Assert(assertion, ASSERTION_COLON + message);
#endif
            }
        }

        /// <summary>
        /// Assertion check.
        /// </summary>
        [Conditional(DBC_CHECK_ALL)]
        public static void Assert(bool assertion, string message, Exception inner)
        {
            if (UseExceptions)
            {
                if (!assertion) throw new AssertionException(message, inner);
            }
            else
            {
#if USE_TRACE_ASSERTION
                Trace.Assert(assertion, ASSERTION_COLON + message);
#elif USE_DEBUG_ASSERTION
                Debug.Assert(assertion, ASSERTION_COLON + message);
#endif
            }
        }

        /// <summary>
        /// Assertion check.
        /// </summary>
        [Conditional(DBC_CHECK_ALL)]
        public static void Assert(bool assertion)
        {
            if (UseExceptions)
            {
                if (!assertion) throw new AssertionException(ASSERTION_FAILED);
            }
            else
            {
#if USE_TRACE_ASSERTION
                Trace.Assert(assertion, ASSERTION_FAILED);
#elif USE_DEBUG_ASSERTION
                Debug.Assert(assertion, ASSERTION_FAILED);
#endif
            }
        }

        /// <summary>
        /// Assertion check.
        /// </summary>
        [Conditional(DBC_CHECK_ALL)]
        public static void Assert(object value, string name, params ICheckStrategy[] strategies)
        {
            bool assertion = true;
            string message = null;

            CheckByStrategies(value, name, strategies, ref assertion, ref message);

            if (UseExceptions)
            {
                if (!assertion) throw new AssertionException(message);
            }
            else
            {
#if USE_TRACE_ASSERTION
                Trace.Assert(assertion, ASSERTION_COLON + message);
#elif USE_DEBUG_ASSERTION
                Debug.Assert(assertion, ASSERTION_COLON + message);
#endif
            }
        }

        #endregion  // End Assertion

        #endregion // Check Methods

        #region Use Exception Or Trace/Debug Assertion?

        // No creation
        private Check() { }

        /// <summary>
        /// Is exception handling being used?
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        private static bool UseExceptions
        {
            get
            {
                return !useAssertions;
            }
            set
            {
                useAssertions = !value;
            }
        }

        // Are trace assertion statements being used? 
        // Default is to use exception handling.
#if USE_TRACE_ASSERTION || USE_DEBUG_ASSERTION
        private static bool useAssertions = true;
#else
        private static bool useAssertions;
#endif
        #endregion // End Use Exception Or Trace/Debug Assertion?

    } // End Check

    #region Exceptions

    namespace Exceptions
    {
        /// <summary>
        /// Exception raised when a contract is broken.
        /// Catch this exception type if you wish to differentiate between 
        /// any DesignByContract exception and other runtime exceptions.
        ///  
        /// </summary>
        [Serializable]
        internal abstract class DesignByContractException : Exception
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="DesignByContractException"/> class.
            /// </summary>
            protected DesignByContractException() { }
            /// <summary>
            /// Initializes a new instance of the <see cref="DesignByContractException"/> class.
            /// </summary>
            /// <param name="message">The message.</param>
            protected DesignByContractException(string message) : base(message) { }
            /// <summary>
            /// Initializes a new instance of the <see cref="DesignByContractException"/> class.
            /// </summary>
            /// <param name="message">The message.</param>
            /// <param name="inner">The inner.</param>
            protected DesignByContractException(string message, Exception inner) : base(message, inner) { }

            protected DesignByContractException(SerializationInfo info, StreamingContext context)
                : base(info, context)
            {
            }
        }

        /// <summary>
        /// Exception raised when a precondition fails.
        /// </summary>
        [Serializable]
        internal sealed class PreconditionException : DesignByContractException
        {
            /// <summary>
            /// Precondition Exception.
            /// </summary>
            public PreconditionException() { }
            /// <summary>
            /// Precondition Exception.
            /// </summary>
            public PreconditionException(string message) : base(message) { }
            /// <summary>
            /// Precondition Exception.
            /// </summary>
            public PreconditionException(string message, Exception inner) : base(message, inner) { }

            private PreconditionException(SerializationInfo info, StreamingContext context)
                : base(info, context)
            {
            }
        }

        /// <summary>
        /// Exception raised when a postcondition fails.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Postcondition"), Serializable]
        internal sealed class PostconditionException : DesignByContractException
        {
            /// <summary>
            /// Postcondition Exception.
            /// </summary>
            public PostconditionException() { }
            /// <summary>
            /// Postcondition Exception.
            /// </summary>
            public PostconditionException(string message) : base(message) { }
            /// <summary>
            /// Postcondition Exception.
            /// </summary>
            public PostconditionException(string message, Exception inner) : base(message, inner) { }

            private PostconditionException(SerializationInfo info, StreamingContext context)
                : base(info, context)
            {
            }
        }

        /// <summary>
        /// Exception raised when an invariant fails.
        /// </summary>
        [Serializable]
        internal sealed class InvariantException : DesignByContractException
        {
            /// <summary>
            /// Invariant Exception.
            /// </summary>
            public InvariantException() { }
            /// <summary>
            /// Invariant Exception.
            /// </summary>
            public InvariantException(string message) : base(message) { }
            /// <summary>
            /// Invariant Exception.
            /// </summary>
            public InvariantException(string message, Exception inner) : base(message, inner) { }

            private InvariantException(SerializationInfo info, StreamingContext context)
                : base(info, context)
            {
            }
        }

        /// <summary>
        /// Exception raised when an assertion fails.
        /// </summary>
        [Serializable]
        internal sealed class AssertionException : DesignByContractException
        {
            /// <summary>
            /// Assertion Exception.
            /// </summary>
            public AssertionException() { }
            /// <summary>
            /// Assertion Exception.
            /// </summary>
            public AssertionException(string message) : base(message) { }
            /// <summary>
            /// Assertion Exception.
            /// </summary>
            public AssertionException(string message, Exception inner) : base(message, inner) { }

            private AssertionException(SerializationInfo info, StreamingContext context)
                : base(info, context)
            {
            }
        }
    }

    #endregion // Exception classes

} // End Design By Contract
