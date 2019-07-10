using GoogleDevGuide.Extension;
using System;
using Xunit;

namespace GoogleDevGuideTest
{
    public class GetLongestWordTest : IDisposable
    {
        public GetLongestWordTest()
        {
        }

        public void Dispose()
        {

        }

        /// <summary>
        /// MethodName_StateUnderTest_ExpectedBehavior
        /// </summary>
        [Fact]
        public void GetLongestWordInSequence_Functional_returnResult()
        {
            string target = "abppplee";
            string[] words = { "able", "ale", "apple", "bale", "kangaroo" };

            var result = target.GetLongestSubSequence(words);

            Assert.Equal("apple", result);
        }

        [Fact]
        public void GetLongestWordInSequence_nothingMatch_returnNull()
        {
            string target = "XXXXXXXXXX";
            string[] words = { "able", "ale", "apple", "bale", "kangaroo" };

            var result = target.GetLongestSubSequence(words);

            Assert.Null(result);
        }

        [Fact]
        public void GetLongestWordInSequence_TargetIsNull_ReturnNull()
        {
            string target = null;
            string[] words = { "able", "ale", "apple", "bale", "kangaroo" };

            var result = target.GetLongestSubSequence(words);

            Assert.Null(result);
        }

        [Fact]
        public void GetLongestWordInSequence_wordsAreEmpty_ReturnNull()
        {
            string target = "abppplee";
            string[] words = {};

            var result = target.GetLongestSubSequence(words);

            Assert.Null(result);
        }
    }
}
