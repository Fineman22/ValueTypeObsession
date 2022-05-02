using System;
using ValueTypeObsession;
using SystemTextJsonConverter = System.Text.Json.Serialization.JsonConverterAttribute;

namespace ValueTypeObsessionTests
{
    [SystemTextJsonConverter(typeof(RepresentedByJsonConverterSystemTextFactory))]
    public class GeneralIdentifier : RepresentedBy<int, GeneralIdentifier>
    {
        public GeneralIdentifier(int value) : base(value)
        {
        }

        protected override void Validate()
        {
            if(Value == 42)
            {
                throw new ArgumentException("Value cant be 42");
            }
        }
    }
}
