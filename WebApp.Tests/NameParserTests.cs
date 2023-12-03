using WebApp.Helpers;

namespace WebApp.Tests
{
    public class NameParserTests
    {
        private const string FirstLast = "first last";

        [Fact]
        public void ReturnsFirstElementAsFirstName()
        {
            // Given
            var name = FirstLast;
            // When
            var studentName = NameParser.FromString(name);
            // Then
            Assert.Equal("first", studentName.FirstName);
        }

        [Fact]
        public void ReturnsLastElementAsLastName()
        {
            var name = FirstLast;
            var studentName = NameParser.FromString(name);
            Assert.Equal("last", studentName.LastName);
        }
    }
}