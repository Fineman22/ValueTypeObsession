using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using JsonConverterSystemText = System.Text.Json.JsonSerializer;
using JsonConverterNewtonSoft = Newtonsoft.Json.JsonConvert;

namespace ValueTypeObsessionTests
{
    [TestClass]
    public class WhitespaceTests
    {
        [TestMethod]
        public void RepresentedByIsSerializedAsBaseType()
        {
            string obj = JsonConverterNewtonSoft.SerializeObject(new WhiteSpace(' '));
            char objAschar = JsonConverterNewtonSoft.DeserializeObject<WhiteSpace>(obj);
            Console.WriteLine(obj);
            Console.WriteLine(objAschar);
        }

        [TestMethod]
        public void RepresentedByIsSerializedAsBaseTypeSystemText()
        {
            string obj = JsonConverterSystemText.Serialize(new WhiteSpace('\t'));
            char objAschar = JsonConverterSystemText.Deserialize<WhiteSpace>(obj);
            Console.WriteLine(obj);
            Console.WriteLine(objAschar);
        }
    }
}
