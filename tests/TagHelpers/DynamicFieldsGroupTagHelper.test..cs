using Microsoft.AspNetCore.Razor.TagHelpers;
using NUnit.Framework;
using xingyi.TagHelpers;
using System.Collections.Generic;
using System.Threading.Tasks;
using xingyi.job.Models;

namespace xingyi.tests.TagHelpers
{
    //[TestFixture]
    //public class DynamicFieldsGroupTagHelperTests
    //{
    //    private TagHelperOutput ProcessTagHelper(string itemDefinition, string groupName)
    //    {
    //        var tagHelper = new DynamicFieldsGroupTagHelper<Question>
    //        {
    //            ItemsDefinition = itemDefinition,
    //            BindTo = groupName
    //        };

    //        var context = new TagHelperContext(
    //            new TagHelperAttributeList(),
    //            new Dictionary<object, object>(),
    //            "");

    //        var output = new TagHelperOutput(
    //            "dynamic-fields-group",
    //            new TagHelperAttributeList(),
    //            (_, __) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent())
    //        );

    //        tagHelper.Process(context, output);
    //        return output;
    //    }


    //    [Test]
    //    public void ParseAttributesList_ParsesCorrectly()
    //    {
    //        // Arrange
    //        var attributesList = "Title:text,Content:textarea,IsPublished:checkbox";
    //        var _tagHelper = new DynamicFieldsGroupTagHelper<Question>();
    //        // Act
    //        var result = DynamicFieldsGroupTagHelper<Question>.ParseItemsDefinition(attributesList);

    //        // Assert
    //        var expected = new Dictionary<string, string>
    //        {
    //            { "Title", "text" },
    //            { "Content", "textarea" },
    //            { "IsPublished", "checkbox" }
    //        };
    //        CollectionAssert.AreEqual(expected, result);
    //    }

    //    [Test]
    //    [TestCase("Title:text", "ObjectName", "<input type='text' name='ObjectName[0].Title' />")]
    //    [TestCase("Content:textarea", "ObjectName", "<textarea name='ObjectName[0].Content'></textarea>")]
    //    [TestCase("IsPublished:checkbox", "ObjectName", "<input type='checkbox' name='ObjectName[0].IsPublished' />")]
    //    public void ItGeneratesExpectedOutputForFieldTypes(string itemDefinition, string groupName, string expected)
    //    {
    //        var output = ProcessTagHelper(itemDefinition, groupName);
    //        Assert.AreEqual(expected, output.Content.GetContent().Trim());
    //    }
    //}
}