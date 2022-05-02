using System.Collections.Generic;
using NewtonSoftJsonConverter = Newtonsoft.Json.JsonConverterAttribute;

namespace ValueTypeObsession
{
    [NewtonSoftJsonConverter(typeof(RepresentedByJsonConverterNewtonSoft))]
    public class RepresentedBy<TValue, TThis> : RepresentedByBase where TThis : RepresentedBy<TValue, TThis>
    {
        public RepresentedBy(TValue value)
        {
            Value = value;
            Validate();
        }

        protected virtual void Validate()
        {
        }

        public TValue Value { get; protected set; }

        protected virtual bool Equals(RepresentedBy<TValue, TThis> other)
        {
            return EqualityComparer<TValue>.Default.Equals(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (obj is TValue objTValue)
                return objTValue.Equals(Value);

            return obj.GetType() == GetType() 
                && Equals((RepresentedBy<TValue, TThis>)obj);
        }

        public override int GetHashCode()
        {
            return EqualityComparer<TValue>.Default.GetHashCode(Value);
        }

        public static bool operator <(RepresentedBy<TValue, TThis> a, RepresentedBy<TValue, TThis> b)
        {
            return Comparer<TValue>.Default.Compare(a.Value, b.Value) < 0;
        }

        public static bool operator >(RepresentedBy<TValue, TThis> a, RepresentedBy<TValue, TThis> b)
        {
            return Comparer<TValue>.Default.Compare(a.Value, b.Value) > 0;
        }

        public static bool operator ==(RepresentedBy<TValue, TThis> a, RepresentedBy<TValue, TThis> b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(RepresentedBy<TValue, TThis> a, RepresentedBy<TValue, TThis> b)
        {
            return !(a == b);
        }

        public static implicit operator TValue(RepresentedBy<TValue, TThis> a)
        {
            return a.Value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }

}
