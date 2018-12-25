using Borg.Infrastructure.Core.Strings;
using Shouldly;
using Xunit;

namespace Test.Borg.Infrastructure.Core
{
    public class StringExtensionsTests
    {
        [Fact]
        public void RemoveWhitespace()
        {
            var source = "ab c d ef gh i ";
            var target = "abcdefghi";
            source.RemoveWhitespace().ShouldBe(target);
        }

        [Fact]
        public void EqualsWhitespaceAgnostic()
        {
            var source = "ab c d ef gh i ";
            var target = "abcdefghi";
            source.EqualsWhitespaceAgnostic(target).ShouldBeTrue();
        }
    }
}