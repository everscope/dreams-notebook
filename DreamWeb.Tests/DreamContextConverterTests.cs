using FluentAssertions;
using Org.BouncyCastle.Asn1;
using Xunit;

namespace DreamWeb.Tests
{
    public class DreamContextConverterTests
    {
        [Theory]
        [InlineData("first", "first")]
        [InlineData("first(%%#newpart#%%)second(%%#newpart#%%)third", "first", "second", "third")]
        public void Concatenate_ReturnsOneConcatenatedString(string expected, params string[] content)
        {
            string concatenated = DreamContentConverter.Concatenate(content);

            concatenated.Should().Be(expected);
        }

        [Theory]
        [InlineData("first", "first")]
        [InlineData("first(%%#newpart#%%)second(%%#newpart#%%)third", "first", "second", "third")]
        public void Split_ReturnsArrayOfSeparatedStrings(string content, params string[] expected)
        {
            string[] splited = DreamContentConverter.Split(content);

            splited.Should().Equal(expected);
        }

    }
}