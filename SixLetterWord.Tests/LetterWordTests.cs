using LetterWord.Services;

namespace LetterWord.Tests
{
    public class LetterWordTests
    {
        [Fact]
        public void FindValidCombinations_FindsNoCombination()
        {
            // Arrange
            var sixLetterWordService = new LetterWordService(6);
            var words = new HashSet<string> { "foobar", "fo", "obar", "f", "oobar" };
            var validWords = new HashSet<string> { "foobaa", "boofaa" };

            // Act
            var result = sixLetterWordService.FindValidCombinations(words, validWords);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void FindValidCombinations_FindsCombinationWithTwoWords()
        {
            // Arrange
            var sixLetterWordService = new LetterWordService(6);
            var words = new HashSet<string> { "foobar", "fo", "obar", "f", "oobar" };
            var validWords = new HashSet<string> { "foobar" };

            // Act
            var result = sixLetterWordService.FindValidCombinations(words, validWords);

            // Assert
            Assert.Contains(["fo", "obar"], result);
            Assert.Contains(["f", "oobar"], result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void FindValidCombinations_FindsCombinationWithThreeWords()
        {
            // Arrange
            var sixLetterWordService = new LetterWordService(6);
            var words = new HashSet<string> { "foobar", "fo", "o", "bar", "f", "ooba" };
            var validWords = new HashSet<string> { "foobar" };

            // Act
            var result = sixLetterWordService.FindValidCombinations(words, validWords);

            // Assert
            Assert.Contains(["fo", "o", "bar"], result);
            Assert.Single(result);
        }
    }
}