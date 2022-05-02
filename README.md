# ValueTypeObsession
Code that shows how to get rid of value types instead of classes

This code should be used like this:

    public class Age : RepresentedBy<int, GeneralIdentifier>
    {
        public Age(int value) : base(value){}
    }

By doing this the following is not possible anymore:

    object.identifier = new Age(10) // compiler error because identifier would be of type "identifier"
    
    
Comparing objects is possible:

    Assert.True(10 == new Age(10)) // true
    Assert.True(9 < new Age(10)) // true
    Assert.True(11 > new Age(10)) // true
    

Hashcodes are also overwritten, so feel free to use them as keys in dictionaries.
    
    Dictionary<Age, char> dictionary = new Dictionary<Age, char>() // works like you would expect
    HashSet<Age> set = new HashSet<Age>() // works like you would expect

The code also features validation, done by overriding the validate method, this will make sure your object never has a wrong value:

    public class Age : RepresentedBy<int, GeneralIdentifier>
    {
        public Age(int value) : base(value){}

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
    }

The objects are automatically converted from and to JSON when using Newtonsoft JSON:

    new { Age = new Age(10); } // json equivalent: { "Age" : 10 }

When using System.Text.Json you need to decorate your object like so, rest is automatic:

    [SystemTextJsonConverter(typeof(RepresentedByJsonConverterSystemTextFactory))]
    public class Age : RepresentedBy<int, GeneralIdentifier>
