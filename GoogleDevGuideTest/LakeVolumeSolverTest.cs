using GoogleDevGuide;
using System;
using System.Linq;
using System.Reflection;
using Xunit;

namespace GoogleDevGuideTest
{
    public class LakeVolumeSolverTest
    {
        readonly LakeVolumeSolver _calculator = new LakeVolumeSolver();

        [Theory]
        [InlineData(new double[0])]
        [InlineData(new double[3] { 0, 0, 0 })]
        [InlineData(new double[1] { 3 })]
        [InlineData(new double[2] { 3, 7 })]
        [InlineData(new double[3] { 2, 3, 7 })]
        [InlineData(new double[3] { 3, 3, 3 })]
        [InlineData(new double[3] { 5, 4, 4 })]
        [InlineData(new double[3] { 3, 2, 2 })]
        [InlineData(new double[3] { 3, 2, 1 })]
        [InlineData(new double[3] { 1, 2, 3 })]
        [InlineData(new double[3] { 1, 1, 3 })]
        [InlineData(new double[4] { 1, 1, 3, 4 })]
        [InlineData(new double[5] { 1, 2, 3, 5, 6 })]
        [InlineData(new double[6] { 3, 3, 3, 2, 1, 1 })]
        public void LakeSizeCalculate_CannotFormLake_ReturnZero(double[] groundHeights)
        {
            double expected = 0;

            double result = _calculator.LakeSizeCalculate(groundHeights);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(new double[3] { 8, 0, 9 })]
        [InlineData(new double[3] { 9, 1, 10 })]
        [InlineData(new double[4] { 8, 4, 4, 9 })]
        [InlineData(new double[5] { 9, 6, 6, 7, 9 })]
        [InlineData(new double[5] { 9, 6, 7, 6, 9 })]
        [InlineData(new double[6] { 4, 2, 2, 2, 2, 4 })]
        [InlineData(new double[6] { 4, 0, 0, 8, 4, 4 })]
        [InlineData(new double[6] { 5, 0, 0, 4, 3, 3 })]
        [InlineData(new double[7] { 4, 0, 0, 9, 3, 3 , 2})]
        public void LakeSizeCalculate_OneLakeAndItsSizeIs8_ReturnResult(double[] groundHeights)
        {
            double expected = 8;

            double result = _calculator.LakeSizeCalculate(groundHeights);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(new double[5] { 4, 0, 5, 0, 4 })]
        [InlineData(new double[5] { 4, 3, 7, 0, 8 })]
        [InlineData(new double[7] { 4, 2, 2, 5, 2, 2, 4 })]
        public void LakeSizeCalculate_TwoLakeAndItsSizeIs8_ReturnResult(double[] groundHeights)
        {
            double expected = 8;

            double result = _calculator.LakeSizeCalculate(groundHeights);

            Assert.Equal(expected, result);
        }
        
        [Fact]
        public void LakeSizeCalculate_ThreeLakesCase1_ReturnResult()
        {
            double[] groundHeights = new double[15] { 1, 3, 2, 4, 1, 3, 1, 4, 5, 2, 2, 1, 4, 2, 2 };
            double expected = 15;

            double result = _calculator.LakeSizeCalculate(groundHeights);

            Assert.Equal(expected, result);
        }      
    }

    public class LakeVolumeSolverPrvtMthdTest
    {
        readonly object _obj;
        readonly Type _type;

        public LakeVolumeSolverPrvtMthdTest()
        {
            _type = typeof(LakeVolumeSolver);
            _obj = Activator.CreateInstance(_type);
        }

        [Theory]
        [InlineData(new double[0],0)]
        [InlineData(new double[3]{ 0, 0, 0 },0)]
        [InlineData(null,0)]
        public void GetSizeBetweenPeaks_InputIsBlank_Return0(double[] groundHeights, int i)
        {
            MethodInfo method = _type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
            .Where(x => x.Name == "GetSizeBetweenPeaks" && x.IsPrivate)
            .First();

            double expected = 0;
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
        [InlineData(new double[3] { 1, 1, 3 }, 0)]
        [InlineData(new double[4] { 1, 1, 3, 4 }, 0)]
        [InlineData(new double[5] { 1, 2, 3, 5, 6 }, 0)]
        [InlineData(new double[6] { 3, 1, 3, 2, 2, 2 }, 3)]
        [InlineData(new double[6] { 3, 3, 3, 4, 1, 1 }, 3)]
        public void GetSizeBetweenPeaks_NoPeaks_Return0(double[] groundHeights, int i)
        {
            MethodInfo method = _type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
            .Where(x => x.Name == "GetSizeBetweenPeaks" && x.IsPrivate)
            .First();

            double expected = 0;
            double result = (double)method.Invoke(_obj, new object[] { groundHeights, i });

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(new double[3] { 3, 0, 3 }, 0)]
        [InlineData(new double[3] { 4, 1, 7 }, 0)]
        [InlineData(new double[4] { 3, 1, 2, 5}, 0)]
        [InlineData(new double[5] { 2, 1, 1, 1, 2 }, 0)]
        [InlineData(new double[6] { 3, 1, 3, 3, 0, 5 }, 3)]
        [InlineData(new double[7] { 3, 1, 3, 3, 2, 1, 8 }, 3)]
        [InlineData(new double[8] { 3, 1, 3, 3, 2, 2, 2, 7 }, 3)]
        public void GetSizeBetweenPeaks_LakeSizeIs3_ReturnResult(double[] groundHeights, int i)
        {
            MethodInfo method = _type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
            .Where(x => x.Name == "GetSizeBetweenPeaks" && x.IsPrivate)
            .First();

            double expected = 3;

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
        [InlineData(new double[3] { 1, 1, 3 }, 0)]
        [InlineData(new double[4] { 1, 1, 3 ,4 }, 0)]
        [InlineData(new double[5] { 1, 2, 3 ,5, 6}, 0)]
        [InlineData(new double[6] { 3, 1, 3 , 2, 2, 2}, 3)]
        [InlineData(new double[6] { 3, 3, 3, 2, 1, 1 }, 3)]
        public void IsThereVolume_NoVolume_ReturnFalse(double[] groundHeights, int i)
        {
            MethodInfo method = _type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
            .Where(x => x.Name == "IsThereVolume" && x.IsPrivate)
            .First();

            bool result = (bool)method.Invoke(_obj, new object[] { groundHeights, i });

            Assert.False(result);
        }

        [Theory]
        [InlineData(new double[3] { 3, 1, 3 }, 0)]
        [InlineData(new double[3] { 3, 0, 2 }, 0)]
        [InlineData(new double[4] { 3, 0, 2 ,6}, 0)]
        [InlineData(new double[4] { 3, 0, 0, 5 }, 0)]
        [InlineData(new double[5] { 3, 0, 0, 3, 5 }, 0)]
        [InlineData(new double[6] { 3, 1, 3, 3, 1, 2 }, 3)]
        [InlineData(new double[6] { 3, 3, 3, 3, 2, 3 }, 3)]
        
        public void IsThereVolume_GotVolume_ReturnTrue(double[] groundHeights, int i)
        {
            MethodInfo method = _type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
            .Where(x => x.Name == "IsThereVolume" && x.IsPrivate)
            .First();

            bool result = (bool)method.Invoke(_obj, new object[] { groundHeights, i });

            Assert.True(result);
        }
    }
}
