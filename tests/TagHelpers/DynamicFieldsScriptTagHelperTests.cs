
using Microsoft.AspNetCore.Razor.TagHelpers;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using xingyi.TagHelpers;

namespace xingyi.Tests.TagHelpers
{
    [TestFixture]
    public class DynamicFieldsScriptTagHelperTests
    {
        private TagHelperContext context = new TagHelperContext(
            new TagHelperAttributeList(),
            new Dictionary<object, object>(),
            "uniqueId"
        );

        private TagHelperOutput output;

        [SetUp]
        public void SetUp()
        {
            output = new TagHelperOutput(
                "dynamic-fields-script",
                new TagHelperAttributeList(),
                (_, __) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent())
            );
        }


        [Test]
        public void Process_GeneratesExpectedScriptFunction()
        {
            var tagHelper = new ChildListScriptTagHelper
            {
                BindTo = "TestContainer"
            };

            tagHelper.Process(context, output);

            var expectedFunctionName = "function addTestContainerFields() {";
            Assert.IsTrue(output.Content.GetContent().Contains(expectedFunctionName));
        }

        [Test]
        [TestCase("Title", "text", "<input type='text' name='TestContainer[0].Title' />")]
        [TestCase("Content", "textarea", "<textarea name='TestContainer[0].Content'></textarea>")]
        [TestCase("IsPublished", "checkbox", "<input type='checkbox' name='TestContainer[0].IsPublished' />")]
        public void Process_GeneratesExpectedHtmlContent(string field, string fieldType, string expectedHtml)
        {
            var tagHelper = new ChildListScriptTagHelper
            {
                BindTo = "TestContainer"
              
            };

            tagHelper.Process(context, output);

            Assert.IsTrue(output.Content.GetContent().Contains(expectedHtml));
        }
    }
}
