using System;
using System.Collections.Generic;
using System.Text;

namespace NIntegrate.Collections.Generic
{
    public sealed class DependingKey<TKey>
    {
        #region Private Fields

        private readonly TKey _keyValue;
        private readonly string[] _dependencies;

        #endregion

        #region Public Properties

        public TKey KeyValue
        {
            get
            {
                return _keyValue;
            }
        }

        #endregion

        #region Constructors

        public DependingKey(TKey keyValue, params string[] dependencies)
        {
            _keyValue = keyValue;
            _dependencies = dependencies;
        }

        #endregion

        #region Public Methods

        public static bool operator ==(DependingKey<TKey> left, DependingKey<TKey> right)
        {
            return object.Equals(left, right);
        }

        public static bool operator !=(DependingKey<TKey> left, DependingKey<TKey> right)
        {
            return !object.Equals(left, right);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            DependingKey<TKey> dpendingKey = obj as DependingKey<TKey>;
            if (dpendingKey != null)
                return object.Equals(this.KeyValue, dpendingKey.KeyValue);

            return false;
        }

        public override int GetHashCode()
        {
            return this.KeyValue.GetHashCode();
        }

        public override string ToString()
        {
            return this.KeyValue.ToString();
        }

        public bool Depends(string dependency, bool ignoreCase, bool partiallyMatch)
        {
            if (_dependencies != null)
            {
                for (int i = 0; i < _dependencies.Length; ++i)
                {
                    if (string.Compare(_dependencies[i], dependency, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal) == 0)
                        return true;

                    if (partiallyMatch && dependency != null && _dependencies[i] != null)
                    {
                        if (_dependencies[i].IndexOf(dependency, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal) >= 0)
                            return true;
                    }
                }
            }

            return false;
        }

        public bool Depends(string dependency)
        {
            return Depends(dependency, false, false);
        }

        #endregion
    }
}
