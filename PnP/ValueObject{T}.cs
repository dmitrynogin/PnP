using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PnP
{
    public abstract class ValueObject<T> : IEquatable<ValueObject<T>>
                where T : ValueObject<T>
    {
        protected abstract IEnumerable<object> EqualityCheckAttributes { get; }

        public override int GetHashCode() =>
            EqualityCheckAttributes
                .Aggregate(0, (hash, a) => hash = hash * 31 + (a?.GetHashCode() ?? 0));

        public override bool Equals(object obj) =>
            Equals(obj as ValueObject<T>);

        public virtual bool Equals(ValueObject<T> other) =>
            other != null &&
            GetType() == other.GetType() &&
            EqualityCheckAttributes.SequenceEqual(other.EqualityCheckAttributes);

        public static bool operator ==(ValueObject<T> left, ValueObject<T> right) =>
            Equals(left, right);

        public static bool operator !=(ValueObject<T> left, ValueObject<T> right) =>
            !Equals(left, right);
    }
}
