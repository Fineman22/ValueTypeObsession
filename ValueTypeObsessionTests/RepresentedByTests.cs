using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using JsonConverterSystemText = System.Text.Json.JsonSerializer;
using JsonConverterNewtonSoft = Newtonsoft.Json.JsonConvert;

namespace ValueTypeObsessionTests
{
    [TestClass]
    public class RepresentedByTests
    {
        [TestMethod]
        public void RepresentedByIsSerializedAsBaseType()
        {
            string obj = JsonConverterNewtonSoft.SerializeObject(new GeneralIdentifier(10));
            int objAsInt = JsonConverterNewtonSoft.DeserializeObject<GeneralIdentifier>(obj);
            Console.WriteLine(obj);
            Console.WriteLine(objAsInt);
        }

        [TestMethod]
        public void RepresentedByIsSerializedAsBaseTypeSystemText()
        {
            string obj = JsonConverterSystemText.Serialize(new GeneralIdentifier(10));
            int objAsInt = JsonConverterSystemText.Deserialize<GeneralIdentifier>(obj);
            Console.WriteLine(obj);
            Console.WriteLine(objAsInt);
        }

        [TestMethod]
        public void RepresentedByIsComparable()
        {
            GeneralIdentifier obj = new GeneralIdentifier(1000);
            GeneralIdentifier obj2 = new GeneralIdentifier(100);
            Assert.IsTrue(obj > obj2);
            Assert.IsFalse(obj < obj2); 
            Assert.IsFalse(obj2 > obj);
            Assert.IsTrue(obj2 < obj);
        }
    }
}
