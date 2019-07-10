using GoogleDevGuide;
using GoogleDevGuide.Interface;
using System;
using System.Collections.Generic;
using Xunit;

namespace GoogleDevGuideTest
{
    public class PuzzleSolverTest : IDisposable
    {
        IPuzzleSolver _solver;

        public PuzzleSolverTest()
        {
            _solver = new PuzzleSolver();
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

            var result = _solver.GetLongestWordInSequence(target, words);

            Assert.Equal("apple", result);
        }

        [Fact]
        public void GetLongestWordInSequence_nothingMatch_returnNull()
        {
            string target = "XXXXXXXXXX";
            string[] words = { "able", "ale", "apple", "bale", "kangaroo" };

            var result = _solver.GetLongestWordInSequence(target, words);

            Assert.Null(result);
        }

        [Fact]
        public void GetLongestWordInSequence_TargetIsNull_ReturnNull()
        {
            string target = null;
            string[] words = { "able", "ale", "apple", "bale", "kangaroo" };

            var result = _solver.GetLongestWordInSequence(target, words);

            Assert.Null(result);
        }

        [Fact]
        public void GetLongestWordInSequence_wordsAreEmpty_ReturnNull()
        {
            string target = "abppplee";
            string[] words = {};

            var result = _solver.GetLongestWordInSequence(target, words);

            Assert.Null(result);
        }
    }
}
