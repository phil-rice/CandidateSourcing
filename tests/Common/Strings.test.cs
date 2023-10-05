using NUnit.Framework;
using xingyi.common;

namespace xingyi.tests
{
    [TestFixture]
    public class StringsTests
    {
        [TestCase("NewItem.isEnabled", "Is Enabled")]
        [TestCase("isEnabled", "Is Enabled")]
        [TestCase("Class.PropertyName", "Property Name")]
        [TestCase("camelCaseProperty", "Camel Case Property")]
        public void ExtractAndFormatLabel_ShouldFormatCorrectly(string input, string expected)
        {
            // Act
            var result = Strings.ExtractAndFormatLabel(input);

            // Assert
            Assert.AreEqual(expected, result);
        }
    }
}
