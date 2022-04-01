using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using JsonConverterSystemText = System.Text.Json.JsonSerializer;
using JsonConverterNewtonSoft = Newtonsoft.Json.JsonConvert;

namespace ValueTypeObsessionTests
{
    [TestClass]
    public class UsernameTests
    {
        [TestMethod]
        public void RepresentedByIsSerializedAsBaseType()
        {
            string obj = JsonConverterNewtonSoft.SerializeObject(new Username("Timmy"));
            string objAsString = JsonConverterNewtonSoft.DeserializeObject<Username>(obj);
            Assert.AreEqual("Timmy", objAsString);
        }

        [TestMethod]
        public void RepresentedByIsSerializedAsBaseTypeSystemText()
        {
            string obj = JsonConverterSystemText.Serialize(new Username("Timmy"));
            string objAsString = JsonConverterSystemText.Deserialize<Username>(obj);
            Assert.AreEqual("Timmy", objAsString);
        }

        [TestMethod]
        public void ShouldThrowWhenUsernameIsStringEmpty()
        {
            Assert.ThrowsException<Username.UsernameCantBeNullOrWhiteSpaceException>(() => new Username(""));
        }

        [TestMethod]
        public void ShouldThrowWhenUsernameIsWhitespace()
        {
            Assert.ThrowsException<Username.UsernameCantBeNullOrWhiteSpaceException>(() => new Username("\t  "));
        }

        [TestMethod]
        public void ShouldThrowWhenUsernameIsNull()
        {
            Assert.ThrowsException<Username.UsernameCantBeNullOrWhiteSpaceException>(() => new Username(null));
        }
    }
}
