using GoogleDevGuide;
using GoogleDevGuide.Interface;
using System;
using System.Linq;
using System.Reflection;
using Xunit;

namespace GoogleDevGuideTest
{
    public class LakeVolumeCalculateTest
    {
        IPuzzleSolver _IPuzzleSolver;

        public LakeVolumeCalculateTest()
        {
            _IPuzzleSolver = new PuzzleSolver();
        }

        [Theory]
        [InlineData(new double[0])]
        [InlineData(new double[3]{0,0,0})]
        [InlineData(null)]
        public void LakeVolumeCalculate_emptyInput_Return0(double[] groundHeights)
        {
            double expected = 0;
           
            double result = _IPuzzleSolver.LakeVolumeCalculate(groundHeights);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(new double[1] {3})]
        [InlineData(new double[2] {3,7})]
        [InlineData(new double[3] {2,3,7})]
        [InlineData(new double[3] {3,3,3})]
        [InlineData(new double[3] {5,4,4})]
        public void LakeVolumeCalculate_CannotFormLake_Return0(double[] groundHeights)
        {
            double expected = 0;

            double result = _IPuzzleSolver.LakeVolumeCalculate(groundHeights);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void LakeVolumeCalculate_LakeWithOneGround_ReturnResult()
        {
            double[] groundHeights = new double[3] { 3, 1, 2 };
            double expected = 1;

            double result = _IPuzzleSolver.LakeVolumeCalculate(groundHeights);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void LakeVolumeCalculate_LakeWithTwoGroundOfSameHeight_ReturnResult()
        {
            double[] groundHeights = new double[4] { 3, 1, 1, 2};
            double expected = 1 + 1 ;

            double result = _IPuzzleSolver.LakeVolumeCalculate(groundHeights);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void LakeVolumeCalculate_LakeWithTwoGroundOfDifferentHeight_ReturnResult()
        {
            double[] groundHeights = new double[4] { 4, 1, 2, 4 };
            double expected = 3 + 2;

            double result = _IPuzzleSolver.LakeVolumeCalculate(groundHeights);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void LakeVolumeCalculate_LakeWithThreeGroundOfDifferentHeight_ReturnResult()
        {
            double[] groundHeights = new double[5] { 4, 1, 3, 1, 4 };
            double expected = 3 + 1 + 3;

            double result = _IPuzzleSolver.LakeVolumeCalculate(groundHeights);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void LakeVolumeCalculate_LakeWithThreeGroundInSlope_ReturnResult()
        {
            double[] groundHeights = new double[5] { 5, 1, 2, 4, 6 };
            double expected = 4 + 3 + 1;

            double result = _IPuzzleSolver.LakeVolumeCalculate(groundHeights);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void LakeVolumeCalculate_TwoLake_ReturnResult()
        {
            double[] groundHeights = new double[5] { 3, 1, 7, 2 , 5};
            double expected = 2 + 3;

            double result = _IPuzzleSolver.LakeVolumeCalculate(groundHeights);

            Assert.Equal(expected, result);
        }


        [Fact]
        public void LakeVolumeCalculate_ThreeLakesCase1_ReturnResult()
        {
            double[] groundHeights = new double[15] { 1, 3, 2, 4, 1, 3, 1, 4, 5, 2, 2, 1, 4, 2, 2 };
            double expected = 15;

            double result = _IPuzzleSolver.LakeVolumeCalculate(groundHeights);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetVolumeTillEndPeak_ThreeLakesCase1_ReturnResult()
        {
            double[] groundHeights = new double[15] { 1, 3, 2, 4, 1, 3, 1, 4, 5, 2, 2, 1, 4, 2, 2 };
            double expected = 15;

            double result = _IPuzzleSolver.LakeVolumeCalculate(groundHeights);

            Assert.Equal(expected, result);
        }
    }

    public class PuzzleSolverPrvtMthdTest
    {
        object _obj;
        Type _type;

        public PuzzleSolverPrvtMthdTest()
        {
            _type = typeof(PuzzleSolver);
            _obj = Activator.CreateInstance(_type);
        }

        [Theory]
        [InlineData(new double[0],0)]
        [InlineData(new double[3]{ 0, 0, 0 },0)]
        [InlineData(null,0)]
        public void GetSumOfVolumeBetweenTwoPeak_InputIsBlank_Return0(double[] groundHeights, int i)
        {
            MethodInfo method = _type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
            .Where(x => x.Name == "GetSumOfVolumeBetweenTwoPeak" && x.IsPrivate)
            .First();

            double expected = 0;

            double result = (double)method.Invoke(_obj, new object[] { groundHeights, i });

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(new double[3] { 3, 3, 3 }, 0)]
        [InlineData(new double[3] { 3, 2, 2 }, 0)]
        [InlineData(new double[3] { 3, 2, 1 }, 0)]
        public void GetSumOfVolumeBetweenTwoPeak_NoPeaks_Return0(double[] groundHeights, int i)
        {
            MethodInfo method = _type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
            .Where(x => x.Name == "GetSumOfVolumeBetweenTwoPeak" && x.IsPrivate)
            .First();

            double expected = 0;

            double result = (double)method.Invoke(_obj, new object[] { groundHeights, i });

            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetSumOfVolumeBetweenTwoPeak_OneValue_ReturnResult()
        {
            MethodInfo method = _type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
            .Where(x => x.Name == "GetSumOfVolumeBetweenTwoPeak" && x.IsPrivate)
            .First();

            int i = 0;
            double[] groundHeights = new double[3] { 3, 0, 3 };
            double expected = 3;

            double result = (double)method.Invoke(_obj, new object[] { groundHeights, i });

            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetSumOfVolumeBetweenTwoPeak_OneValueCase2_ReturnResult()
        {
            MethodInfo method = _type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
            .Where(x => x.Name == "GetSumOfVolumeBetweenTwoPeak" && x.IsPrivate)
            .First();

            int i = 0;
            double[] groundHeights = new double[3] { 3, 2, 8 };
            double expected = 1;

            double result = (double)method.Invoke(_obj, new object[] { groundHeights, i });

            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetSumOfVolumeBetweenTwoPeak_LakeSizeTwo_ReturnResult()
        {
            MethodInfo method = _type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
            .Where(x => x.Name == "GetSumOfVolumeBetweenTwoPeak" && x.IsPrivate)
            .First();

            int i = 0;
            double[] groundHeights = new double[4] { 3, 0, 1, 3 };
            double expected = 3 + 2;

            double result = (double)method.Invoke(_obj, new object[] { groundHeights, i });

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(new double[0], 0)]
        [InlineData(new double[3] { 0, 0, 0 }, 0)]
        [InlineData(null, 0)]
        [InlineData(new double[3] { 3, 3, 3 }, 0)]
        [InlineData(new double[3] { 3, 2, 2 }, 0)]
        [InlineData(new double[3] { 3, 2, 1 }, 0)]
        [InlineData(new double[3] { 1, 2, 3 }, 0)]
        public void IsThereVolume_NoPeaks_ReturnFalse(double[] groundHeights, int i)
        {
            MethodInfo method = _type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
            .Where(x => x.Name == "IsThereVolume" && x.IsPrivate)
            .First();

            bool result = (bool)method.Invoke(_obj, new object[] { groundHeights, i });

            Assert.False(result);
        }
    }
}
