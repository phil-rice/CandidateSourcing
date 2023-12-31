﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using xingyi.common;

namespace xingyi.tests
{
    [TestFixture]
    public class AttributeParserTests
    {
        [Test]
        public void ParseAttributes_ValidInput_ParsesCorrectly()
        {
            // Arrange
            string input = "attr1:value1, attr2:value2";

            // Act
            var result = AttributeParser.ParseAttributes(input);

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(("value1", ""), result["attr1"]);
            Assert.AreEqual(("value2", ""), result["attr2"]);
        }

        [Test]
        public void ParseAttributes_ValidInputWithHelpText_ParsesCorrectly()
        {
            // Arrange
            string input = "attr1:value1?help1, attr2:value2?help2";

            // Act
            var result = AttributeParser.ParseAttributes(input);

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(("value1", "help1"), result["attr1"]);
            Assert.AreEqual(("value2", "help2"), result["attr2"]);
        }

        [Test]
        public void ParseAttributes_EmptyString_ThrowsException()
        {
            // Arrange
            string input = "";

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => AttributeParser.ParseAttributes(input));
            Assert.AreEqual($"Invalid attribute input: '{input}'. The input is null or whitespace.", ex.Message);
        }

        [Test]
        public void ParseAttributes_MultipleCommasWithoutContent_ThrowsException()
        {
            // Arrange
            string input = "attr1:value1,,attr2:value2";

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => AttributeParser.ParseAttributes(input));
            Assert.AreEqual($"Invalid attribute format in input: '{input}'. Each attribute should be of the form 'attribute:value' or 'attribute:value?helptext'.", ex.Message);
        }

        [Test]
        public void ParseAttributes_AttributeWithoutValue_ThrowsException()
        {
            // Arrange
            string input = "attr1:,attr2:value2";

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => AttributeParser.ParseAttributes(input));
            Assert.AreEqual($"Invalid attribute format in input: '{input}'. Neither attribute name nor its value can be empty.", ex.Message);
        }



        [Test]
        public void ParseAttributes_ValueWithHelpTextOnly_ParsesCorrectly()
        {
            // Arrange
            string input = "attr1:value1?Help text for value1";

            // Act
            var result = AttributeParser.ParseAttributes(input);

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(("value1", "Help text for value1"), result["attr1"]);
        }

        [Test]
        public void ParseAttributes_ValueWithoutHelpText_ParsesCorrectly()
        {
            // Arrange
            string input = "attr1:value1?";

            // Act
            var result = AttributeParser.ParseAttributes(input);

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(("value1", ""), result["attr1"]);
        }

        [Test]
        public void ParseAttributes_MissingValueBeforeHelpText_ThrowsException()
        {
            // Arrange
            string input = "attr1:?Help text without value";

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => AttributeParser.ParseAttributes(input));
            Assert.AreEqual($"Invalid attribute format in input: '{input}'. Neither attribute name nor its value can be empty.", ex.Message);
        }

      

    }
}
