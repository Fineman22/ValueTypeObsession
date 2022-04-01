using System;
using ValueTypeObsession;
using SystemTextJsonConverter = System.Text.Json.Serialization.JsonConverterAttribute;

namespace ValueTypeObsessionTests
{
    [SystemTextJsonConverter(typeof(RepresentedByJsonConverterSystemTextFactory))]
    public class WhiteSpace : RepresentedBy<char, WhiteSpace>
    {
        public WhiteSpace(char value) : base(value)
        {
        }

        protected override void Validate()
        {
            if(!char.IsWhiteSpace(Value))
            {
                throw new ArgumentException("Whitespace should be a whitespace char!");
            }
        }
    }
}
