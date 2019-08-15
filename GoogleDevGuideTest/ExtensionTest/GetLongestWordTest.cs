using GoogleDevGuide.Extension;
using System;
using Xunit;

namespace GoogleDevGuideTest
{
    public class GetLongestWordTest
    {
        /// <summary>
        /// MethodName_StateUnderTest_ExpectedBehavior
        /// </summary>
        [Fact]
        public void GetLongestSubSequence_Functional_ReturnResult()
        {
            string target = "abppplee";
            string[] words = { "able", "ale", "apple", "bale", "kangaroo" };

            var result = target.GetLongestSubSequence(words);

            Assert.Equal("apple", result);
        }

        [Fact]
        public void GetLongestSubSequence_NothingMatch_ReturnNull()
        {
            string target = "XXXXXXXXXX";
            string[] words = { "able", "ale", "apple", "bale", "kangaroo" };

            var result = target.GetLongestSubSequence(words);

            Assert.Empty(result);
        }

        [Fact]
        public void GetLongestSubSequence_TargetIsNull_ReturnNull()
        {
            string target = null;
            string[] words = { "able", "ale", "apple", "bale", "kangaroo" };

            var result = target.GetLongestSubSequence(words);

            Assert.Empty(result);
        }

        [Fact]
        public void GetLongestSubSequence_WordsAreEmpty_ReturnNull()
        {
            string target = "abppplee";
            string[] words = {};

            var result = target.GetLongestSubSequence(words);

            Assert.Empty(result);
        }

        [Fact]
        public void GetLongestSubSequence_WordsAreNull_ThrowEx()
        {
            string target = null;
            string[] words = null;

            Assert.Throws<ArgumentNullException>(() => target.GetLongestSubSequence(words));
        }

        [Fact]
        public void GetLongestSubSequence_MatchIgnoreSensitiveResult_ReturnResult()
        {
            string target = "aabbcc";
            string[] words = { "BB", "BBB", "BBBB" };

            var result = target.GetLongestSubSequence(words, StringComparison.CurrentCultureIgnoreCase);

            Assert.Equal("BB", result);
        }

        [Fact]
        public void GetLongestSubSequence_DiffSequence_ReturnEmpty()
        {
            string target = "cba";
            string[] words = { "abc", "acb", "cab" };

            var result = target.GetLongestSubSequence(words, StringComparison.CurrentCultureIgnoreCase);

            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void IsSubSequence_Match_ReturnTrue()
        {
            string target = "abppplee";
            string word = "apple";

            var result = target.IsSubSequence(word);

            Assert.True(result);
        }

        [Fact]
        public void IsSubSequence_NotMatch_ReturnFalse()
        {
            string target = "abppplee";
            string word = "appled";

            var result = target.IsSubSequence(word);
            Assert.False(result);
        }

        [Fact]
        public void IsSubSequence_WordIsNull_ReturnFalse()
        {
            string target = "abppplee";
            string word = null;

            var result = target.IsSubSequence(word);
            Assert.False(result);
        }

        [Fact]
        public void IsSubSequence_BothIsNull_ReturnFalse()
        {
            string target = null;
            string word = null;

            var result = target.IsSubSequence(word);
            Assert.False(result);
        }

        [Fact]
        public void IsSubSequence_BothSameIgnoreCaseSensitive_Returntrue()
        {
            string target = "ab";
            string word = "AB";

            var result = target.IsSubSequence(word, StringComparison.CurrentCultureIgnoreCase);

            Assert.True(result);
        }

        [Fact]
        public void IsSubSequence_MatchChinese_ReturnFalse()
        {
            string target = "你们我们他们";
            string word = "你们我";

            var result = target.IsSubSequence(word);

            Assert.True(result);
        }

        [Fact]
        public void IsSubSequence_MatchSymbol_ReturnFalse()
        {
            string target = "!@#$%^&";
            string word = "@#$";

            var result = target.IsSubSequence(word);

            Assert.True(result);
        }

    }



}
