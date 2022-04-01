using System;
using ValueTypeObsession;
using SystemTextJsonConverter = System.Text.Json.Serialization.JsonConverterAttribute;

namespace ValueTypeObsessionTests
{
    [SystemTextJsonConverter(typeof(RepresentedByJsonConverterSystemTextFactory))]
    public class Username : RepresentedBy<string, Username>
    {
        public Username(string value) : base(value)
        {
        }

        protected override void Validate()
        {
            if (string.IsNullOrWhiteSpace(Value))
            {
                throw new UsernameCantBeNullOrWhiteSpaceException("Username can't be null or empty.");
            }
        }

        [Serializable]
        public class UsernameCantBeNullOrWhiteSpaceException : Exception
        {
            public UsernameCantBeNullOrWhiteSpaceException(string message) : base(message)
            {
            }
        }
    }
}
