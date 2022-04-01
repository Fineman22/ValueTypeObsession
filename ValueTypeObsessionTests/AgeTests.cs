using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ValueTypeObsessionTests
{
    [TestClass]
    public class AgeTests
    {
        [TestMethod]
        public void ShouldThrowWhenAgeIsNegative()
        {
            Assert.ThrowsException<ArgumentException>(() => new Age(-1));
        }

        [TestMethod]
        public void ShouldThrowWhenAgeIsTooOld()
        {
            Assert.ThrowsException<Age.PersonTooOldException>(()=> new Age(151));
        }
    }
}
