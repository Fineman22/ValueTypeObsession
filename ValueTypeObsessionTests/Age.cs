using System;
using ValueTypeObsession;
using SystemTextJsonConverter = System.Text.Json.Serialization.JsonConverterAttribute;

namespace ValueTypeObsessionTests
{
    [SystemTextJsonConverter(typeof(RepresentedByJsonConverterSystemTextFactory))]
    public class Age : RepresentedBy<int, GeneralIdentifier>
    {
        public Age(int value) : base(value)
        {
        }

        protected override void Validate()
        {
            if (Value < 0)
            {
                throw new ArgumentException("Person can't have a negative age");
            }
            if(Value > 150)
            {
                throw new PersonTooOldException($"Age of {Value} is not an acceptable age.");
            }
        }

        [Serializable]
        public class PersonTooOldException : Exception
        {
            public PersonTooOldException(string message) : base(message)
            {
            }
        }
    }
}
