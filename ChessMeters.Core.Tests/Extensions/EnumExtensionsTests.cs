using ChessMeters.Core.Attributes;
using ChessMeters.Core.Extensions;
using System.ComponentModel;
using Xunit;

namespace ChessMeters.Core.Tests.Extensions
{
    public class EnumExtensionsTests
    {
        private enum DescriptionTestEnum
        {
            [Description("Description test")]
            [EnumDisplay(UI = "Enum display test")]
            Test,

            Test2,

            [EnumDisplay()]
            Test3
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void GetEnumDescription_Should_ReturnDescriptionAttributeForEnum()
        {
            var descriptionEnum = DescriptionTestEnum.Test;

            var description = descriptionEnum.GetEnumDescription();

            Assert.Equal("Description test", description);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void GetEnumDescription_Should_ReturnNullIfDescriptionAttributeIsMissing()
        {
            var descriptionEnum = DescriptionTestEnum.Test2;

            var description = descriptionEnum.GetEnumDescription();

            Assert.Null(description);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void GetEnumDisplayUI_Should_ReturnEnumDisplayUIAttributeForEnum()
        {
            var descriptionEnum = DescriptionTestEnum.Test;

            var description = descriptionEnum.GetEnumDisplayUI();

            Assert.Equal("Enum display test", description);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void GetEnumDisplayUI_Should_ReturnNullIfEnumDisplayAttributeIsMissing()
        {
            var descriptionEnum = DescriptionTestEnum.Test2;

            var description = descriptionEnum.GetEnumDescription();

            Assert.Null(description);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void GetEnumDisplayUI_Should_ReturnNullIfUIIsMissingFromEnumDisplayAttribute()
        {
            var descriptionEnum = DescriptionTestEnum.Test3;

            var description = descriptionEnum.GetEnumDescription();

            Assert.Null(description);
        }
    }
}
