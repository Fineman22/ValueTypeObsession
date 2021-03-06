using Microsoft.VisualStudio.TestTools.UnitTesting;
using JsonConverterSystemText = System.Text.Json.JsonSerializer;
using JsonConverterNewtonSoft = Newtonsoft.Json.JsonConvert;
using System;
using Microsoft.CSharp.RuntimeBinder;
using System.Collections.Generic;

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
            Assert.IsTrue(obj == 10.ToString());
            Assert.IsTrue(objAsInt == 10);
        }

        [TestMethod]
        public void RepresentedByIsSerializedAsBaseTypeInBiggerObject()
        {
            string obj = JsonConverterNewtonSoft.SerializeObject(new GeneralIdentifierHolderClass());
            string objWithInts = JsonConverterNewtonSoft.SerializeObject(new IntHolderClass());
            GeneralIdentifierHolderClass objDeserialized = JsonConverterNewtonSoft.DeserializeObject<GeneralIdentifierHolderClass>(obj);
            Assert.IsTrue(objWithInts == obj);
            Assert.IsTrue(objDeserialized is GeneralIdentifierHolderClass);
            Assert.IsTrue(objDeserialized.GeneralIdentifierFirst == 1000);
            Assert.IsTrue(objDeserialized.GeneralIdentifierSecond == 100);
        }

        [TestMethod]
        public void RepresentedByIsSerializedAsBaseTypeSystemText()
        {
            string obj = JsonConverterSystemText.Serialize(new GeneralIdentifier(10));
            int objAsInt = JsonConverterSystemText.Deserialize<GeneralIdentifier>(obj);
            Assert.IsTrue(obj == 10.ToString());
            Assert.IsTrue(objAsInt == 10);
        }

        [TestMethod]
        public void RepresentedByIsSerializedAsBaseTypeSystemTextInBiggerObject()
        {
            string obj = JsonConverterSystemText.Serialize(new GeneralIdentifierHolderClass());
            string objWithInts = JsonConverterSystemText.Serialize(new IntHolderClass());
            GeneralIdentifierHolderClass objDeserialized = JsonConverterSystemText.Deserialize<GeneralIdentifierHolderClass>(obj);
            Assert.IsTrue(objWithInts == obj);
            Assert.IsTrue(objDeserialized is GeneralIdentifierHolderClass);
            Assert.IsTrue(objDeserialized.GeneralIdentifierFirst == 1000);
            Assert.IsTrue(objDeserialized.GeneralIdentifierSecond == 100);
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

        [TestMethod]
        public void RepresentedByIsEqualIfComparedToEncapulatedValue()
        {
            GeneralIdentifier obj = new GeneralIdentifier(1000);
            int obj2 = 1000;
            Assert.IsTrue(obj == obj2);
            Assert.IsTrue(obj2 == obj);
            Assert.IsTrue(obj.Equals(obj2));
            Assert.IsTrue(obj2.Equals(obj));
        }

        [TestMethod]
        public void GetHashCodeShouldReturnHashcodeOfValue()
        {
            GeneralIdentifier obj = new GeneralIdentifier(1000);
            int obj2 = 1000;
            Assert.IsTrue(obj.GetHashCode() == obj2.GetHashCode());
            Assert.IsTrue(obj2.GetHashCode() == obj.GetHashCode());
        }

        [TestMethod]
        public void ShouldWorkInAHashTable()
        {
            HashSet<Age> set = new HashSet<Age>
            {
                new Age(42),
                new Age(50)
            };
            Assert.IsTrue(set.Contains(new Age(42)));
            Assert.IsTrue(!set.Contains(new Age(43)));
        }

        [TestMethod]
        public void ShouldWorkInADictionary()
        {
            Dictionary<Age, char> dictionary = new Dictionary<Age, char>
            {
                { new Age(42), 'a' },
                { new Age(50), 'a' },
            };
            Assert.IsTrue(dictionary.ContainsKey(new Age(42)));
            Assert.IsTrue(!dictionary.ContainsKey(new Age(43)));
        }

        [TestMethod]
        public void AssignmentDoesNotWorkForDifferentDefinedTypes()
        {
            GeneralIdentifier obj = new GeneralIdentifier(1000);
            Age age;
            Assert.ThrowsException<RuntimeBinderException>(() => age = (dynamic)obj);
        }

        private class GeneralIdentifierHolderClass
        {
            public GeneralIdentifier GeneralIdentifierFirst { get; set; } = new GeneralIdentifier(1000);
            public GeneralIdentifier GeneralIdentifierSecond { get; set; } = new GeneralIdentifier(100);
        }

        private class IntHolderClass
        {
            public int GeneralIdentifierFirst { get; set; } = 1000;
            public int GeneralIdentifierSecond { get; set; } = 100;
        }

    }
}
