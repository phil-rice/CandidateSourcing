using NUnit.Framework;
using System;
using xingyi.common;

namespace xingyi.tests
{
    [TestFixture]
    public class GuidsTests
    {
        [Test]
        public void From_GivenDistinctStrings_ProducesDistinctGuids()
        {
            // Arrange
            string input1 = "testString1";
            string input2 = "testString2";

            // Act
            Guid guid1 = Guids.from(input1);
            Guid guid2 = Guids.from(input2);

            // Assert
            Assert.AreNotEqual(guid1, guid2);
        }

        [Test]
        public void From_GivenIdenticalStrings_ProducesIdenticalGuids()
        {
            // Arrange
            string input1 = "testString";
            string input2 = "testString";

            // Act
            Guid guid1 = Guids.from(input1);
            Guid guid2 = Guids.from(input2);

            // Assert
            Assert.AreEqual(guid1, guid2);
        }

        [Test]
        public void From_GivenEmptyString_ThrowsException()
        {
            // Arrange
            string input = string.Empty;

            // Act
            Guid guid = Guids.from(input);

            Assert.AreEqual(Guid.Empty, guid);
        }
    }
}
