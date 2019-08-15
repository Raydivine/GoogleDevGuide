using GoogleDevGuide;
using GoogleDevGuide.CustomExceptions;
using GoogleDevGuide.Interface;
using System;
using System.Linq;
using System.Reflection;
using Xunit;

namespace GoogleDevGuideTest
{
    public class DistributeCandiesTest 
    {
        PuzzleSolver _solver = new PuzzleSolver();

        [Fact]
        public void GetMaxNumOfCandiesToGain_InputIsNull_ThrowException()
        {
            int[] input = null;

            Assert.Throws<NullReferenceException>(() => _solver.GetMaxNumOfCandiesToGain(input));
        }

        [Theory]
        [InlineData(new int[0] {})]
        [InlineData(new int[1] { 1 })]
        [InlineData(new int[3] { 1, 1, 1})]
        public void GetMaxNumOfCandiesToGain_ArrayLengthIsOdd_ThrowException(int[] input)
        {
            Assert.Throws<NumberIsNotEvenException>(() => _solver.GetMaxNumOfCandiesToGain(input));
        }


        [Theory]
        [InlineData(new int[2] { 1, 1 })]
        [InlineData(new int[2] { 1, 2 })]
        [InlineData(new int[4] { 1, 1, 1, 1 })]
        public void GetMaxNumOfCandiesToGain_CanGetOneKind_Return1(int[] input)
        {
            int expected = 1;

            int result = _solver.GetMaxNumOfCandiesToGain(input);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(new int[4] { 1, 1, 1, 2 })]
        [InlineData(new int[4] { 1, 1, 2, 2 })]
        [InlineData(new int[4] { 1, 2, 2, 3 })]
        [InlineData(new int[4] { 1, 2, 3, 4 })]
        [InlineData(new int[4] { 1, 1, 2, 3 })]
        [InlineData(new int[6] { 1, 1, 1, 2, 2, 2 })]
        public void GetMaxNumOfCandiesToGain_CanGetTwoKind_Return2(int[] input)
        {
            int expected = 2;

            int result = _solver.GetMaxNumOfCandiesToGain(input);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(new int[6] { 1, 1, 2, 2, 3, 3 })]
        [InlineData(new int[10] { 1, 1, 1, 1, 2, 2, 2, 3, 3, 3 })]
        public void GetMaxNumOfCandiesToGain_CanGetThreeKind_Return3(int[] input)
        {
            int expected = 3;

            int result = _solver.GetMaxNumOfCandiesToGain(input);

            Assert.Equal(expected, result);
        }

     
       


    }

}
