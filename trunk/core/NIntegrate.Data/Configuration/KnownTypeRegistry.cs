using System;
using System.Collections.Generic;

namespace NIntegrate.Data.Configuration
{
    /// <summary>
    /// Registry for all known types for serializing QueryCriteria class.
    /// </summary>
    public sealed class KnownTypeRegistry
    {
        /// <summary>
        /// The singleton.
        /// </summary>
        public static readonly KnownTypeRegistry Instance;

        private readonly List<Type> _knownTypes;
        private readonly object _knownTypesLock;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="KnownTypeRegistry"/> class.
        /// </summary>
        private KnownTypeRegistry()
        {
            _knownTypes = new List<Type>();
            _knownTypesLock = new object();
        }

        /// <summary>
        /// Initializes the <see cref="KnownTypeRegistry"/> class.
        /// </summary>
        static KnownTypeRegistry()
        {
            Instance = new KnownTypeRegistry();

            AddPredefinedKnownTypes();
        }

        #endregion

        #region Non-Public Properties

        public Type[] KnownTypes { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Add a known type to registry.
        /// </summary>
        /// <param name="type">The known type to add.</param>
        /// <returns></returns>
        public bool AddKnownType(Type type)
        {
            if (type != null)
            {
                if (!_knownTypes.Contains(type))
                {
                    lock (_knownTypesLock)
                    {
                        if (!_knownTypes.Contains(type))
                        {
                            _knownTypes.Add(type);
                            KnownTypes = _knownTypes.ToArray();
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Remove a known type from registry.
        /// </summary>
        /// <param name="type">The known type to remove.</param>
        /// <returns></returns>
        public bool RemoveKnownType(Type type)
        {
            if (type != null)
            {
                if (_knownTypes.Contains(type))
                {
                    lock (_knownTypesLock)
                    {
                        if (_knownTypes.Contains(type))
                        {
                            var result = _knownTypes.Remove(type);
                            KnownTypes = _knownTypes.ToArray();
                            return result;
                        }
                    }
                }
            }

            return false;
        }

        #endregion

        #region Non-Public Methods

        private static void AddPredefinedKnownTypes()
        {
            var knownTypes = new[]
                       {
                           typeof(Condition),
                           typeof(Assignment),
                           typeof(ParameterEqualsCondition),
                           typeof(NullExpression),
                           typeof(BooleanExpression),
                           typeof(ByteExpression),
                           typeof(Int16Expression),
                           typeof(Int32Expression),
                           typeof(Int64Expression),
                           typeof(DateTimeExpression),
                           typeof(StringExpression),
                           typeof(GuidExpression),
                           typeof(DoubleExpression),
                           typeof(DecimalExpression),
                           typeof(ExpressionCollection),
                           typeof(BooleanParameterExpression),
                           typeof(ByteParameterExpression),
                           typeof(Int16ParameterExpression),
                           typeof(Int32ParameterExpression),
                           typeof(Int64ParameterExpression),
                           typeof(DateTimeParameterExpression),
                           typeof(StringParameterExpression),
                           typeof(GuidParameterExpression),
                           typeof(DoubleParameterExpression),
                           typeof(DecimalParameterExpression),
                           typeof(BooleanColumn),
                           typeof(ByteColumn),
                           typeof(Int16Column),
                           typeof(Int32Column),
                           typeof(Int64Column),
                           typeof(DateTimeColumn),
                           typeof(StringColumn),
                           typeof(GuidColumn),
                           typeof(DoubleColumn),
                           typeof(DecimalColumn)
                       };

            foreach (var type in knownTypes)
            {
                Instance.AddKnownType(type);
            }
        }

        #endregion
    }
}
