using System;
using Xunit;
using GoogleDevGuide;
using GoogleDevGuide.Interface;
using GoogleDevGuideTest.Common;

namespace GoogleDevGuideTest
{
    public class CompressorTest : IDisposable
    {
        ICompressor _ICompressor;

        public CompressorTest()
        {
            _ICompressor = new Compressor();
        }

        public void Dispose()
        {

        }

        [Fact]
        public void Compress_InputIsEmpty_ReturnInput()
        {
            string input = "";
            var result = _ICompressor.Compress(input);

            Assert.Equal(input, result);
        }

        [Fact]
        public void Compress_OneCharacter_ReturnInput()
        {
            string input = "a";
            string expected = "a";

            var result = _ICompressor.Compress(input);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Compress_ThreeDifferentCharacters_ReturnInput()
        {
            string input = "a1@";

            var result = _ICompressor.Compress(input);

            Assert.Equal(input, result);
        }

        /// <summary>
        /// Given there is repeated word
        /// Do compression only when the pattern's size is greater than 4
        ///  
        /// Example
        /// Input  : aaaa  (the size is 4)
        /// Output : 4[a]  (the size is 4 too, which does not reduce)
        ///  
        /// therefore we don't do compression 
        /// </summary>
        [Fact]
        public void Compress_FourSameCharacters_ReturnInput()
        {
            string input = "aaaa";

            var result = _ICompressor.Compress(input);

            Assert.Equal(input, result);
        }

        /// <summary>
        /// Given there is repeated word
        /// Do compression only when the pattern's size is greater than 4
        ///  
        /// Example
        /// Input  : aaaaa  (the size is 5)
        /// Output : 5[a]   (the size is 4 , which reduced)
        ///  
        /// therefore we do compression
        /// </summary>
        [Fact]
        public void Compress_FiveSameCharacters_ReturnCompressedResult()
        {
            string input = "aaaaa";  
            string expected = "5[a]";

            var result = _ICompressor.Compress(input);

            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Given there is repeated pattern of 2 words
        /// Do compression when repeated above 2 times
        ///  
        /// Example
        /// Input  : abab   (the size is 4)
        /// Output : 2[ab]  (the size is 5, which increased)
        ///  
        /// therefore we don't do compression 
        /// </summary>
        [Fact]
        public void Compress_PartternTwoWordsReapatedTwice_ReturnInput()
        {
            string input = "abab";

            var result = _ICompressor.Compress(input);
            Assert.Equal(input, result);
        }

        /// <summary>
        /// Given there is repeated pattern of 2 words
        /// Do compression when repeated above 2 times
        ///  
        /// Example
        /// Input  : ababab (the size is 6)
        /// Output : 3[ab]  (the size is 5, which reduced)
        ///  
        /// therefore we do compression 
        /// </summary>
        [Fact]
        public void Compress_PartternTwoWordsReapatedThreeTimes_ReturnInput()
        {
            string input = "ababab";
            string expected = "3[ab]";

            var result = _ICompressor.Compress(input);
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Given there is repeated pattern of 3 words
        /// Do compression when repeated above 3 times
        ///  
        /// Example
        /// Input  : abcabc  (the size is 6)
        /// Output : 2[abc]  (the size is 6 too, which does not reduce)
        ///  
        /// therefore we don't do compression 
        /// </summary>
        [Fact]
        public void Compress_PartternThreeWordsReapatedTwice_ReturnInput()
        {
            string input = "abcabc";

            var result = _ICompressor.Compress(input);
            Assert.Equal(input, result);
        }

        /// <summary>
        /// Given there is repeated pattern of 3 words
        /// Do compression when  repeated above 3 times
        ///  Word
        /// Example
        /// Input  : abcabcabc  (the size is 9)
        /// Output : 3[abc]  (the size is 6, which reduced)
        ///  
        /// therefore we do compression 
        /// </summary>
        [Fact]
        public void Compress_PartternThreeWordsReapatedThreeTimes_ReturnInput()
        {
            string input = "abcabcabc";
            string expected = "3[abc]";

            var result = _ICompressor.Compress(input);

            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Given there is repeated pattern of 4 words or above
        /// We just do compression
        ///  
        /// Example
        /// Input  : abcdabcd  (the size is 8)
        /// Output : 2[abcd]  (the size is 7 which reduced)
        ///  
        /// therefore we do compression 
        /// </summary>
        [Fact]
        public void Compress_PartternFourWordsReapatedTwice_ReturnInput()
        {
            string input = "abcdabcd";
            string expected = "2[abcd]";

            var result = _ICompressor.Compress(input);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Compress_InputCanBePartialCompress_ReturnCompressedResult()
        {
            string input = "abcabcabcababababc";
            string expected = "3[abc]4[ab]c";

            var result = _ICompressor.Compress(input);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Compress_InputIsNumerics_ReturnCompressedResult()
        {
            string input = "1231231233213213216677";
            string expected = "3[123]3[321]6677";

            var result = _ICompressor.Compress(input);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Compress_InputContainBothCharacterAndNumeric_ReturnCompressedResult()
        {
            string input = "1231231233213213216677abcabcabcababababc";
            string expected = "3[123]3[321]66773[abc]4[ab]c";

            var result = _ICompressor.Compress(input);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Compress_InputIsSymbols_ReturnCompressedResult()
        {
            string input = "@#$@#$@#$%%%^&*()^&*()";
            string expected = "3[@#$]%%%2[^&*()]";

            var result = _ICompressor.Compress(input);

            Assert.Equal(expected, result);
        }
    }

    public class EncodeStringTest : IDisposable
    {
        ICompressor _ICompressor;

        public EncodeStringTest()
        {
            _ICompressor = new Compressor();
        }

        public void Dispose()
        {

        }

        [Fact]
        public void EncodeStringIn_ArrayIsEmpty_ThrowEx()
        {
            int i = 0;
            int j = 0;
            string input = null;
            string[,] dp = null;

            Assert.Throws<NullReferenceException>(() => _ICompressor.EncodeStringIn(dp, input, i, j));
        }

        [Fact]
        public void EncodeStringIn_SubStringIsNull_ThrowEx()
        {
            int i = 0;
            int j = 0;
            string input = null;
            string[,] dp = new string[3, 3];

            Assert.Throws<NullReferenceException>(() => _ICompressor.EncodeStringIn(dp, input, i, j));
        }

        [Fact]
        public void EncodeStringIn_RangeOutOfArray_ThrowEx()
        {
            int i = 4;
            int j = 4;
            string input = "qwwerewrtfdsfd";
            string[,] dp = new string[3, 3];

            Assert.Throws<IndexOutOfRangeException>(() => _ICompressor.EncodeStringIn(dp, input, i, j));
        }

        [Fact]
        public void EncodeStringIn_SubStringCantEncode_DoNothing()
        {
            string input = "abc";
            string[,] dp = Tools.GetComboArr(input);

            _ICompressor.EncodeStringIn(dp, input, 0, input.Length - 1);
            Assert.Equal(input, dp[0, dp.GetLength(0) - 1]);
        }

        [Fact]
        public void EncodeStringIn_OneCharacterCantEncode_DoNothing()
        {
            string input = "aaaa";
            string[,] dp = Tools.GetComboArr(input);

            _ICompressor.EncodeStringIn(dp, input, 0, input.Length - 1);
            Assert.Equal(input, dp[0, dp.GetLength(0) - 1]);
        }

        [Fact]
        public void EncodeStringIn_TwoCharacterCantEncode_DoNothing()
        {
            string input = "abab";
            string[,] dp = Tools.GetComboArr(input);

            _ICompressor.EncodeStringIn(dp, input, 0, input.Length - 1);
            Assert.Equal(input, dp[0, dp.GetLength(0) - 1]);
        }

        [Fact]
        public void EncodeStringIn_ThreeCharacterCantEncode_DoNothing()
        {
            string input = "abcabc";
            string[,] dp = Tools.GetComboArr(input);

            _ICompressor.EncodeStringIn(dp, input, 0, input.Length - 1);
            Assert.Equal(input, dp[0, dp.GetLength(0) - 1]);
        }

        [Fact]
        public void EncodeStringIn_SubStringCanEncode_Encoded()
        {
            string input = "aaaaa";
            string expected = "5[a]";
            string[,] dp = Tools.GetComboArr(input);

            _ICompressor.EncodeStringIn(dp, input, 0, input.Length-1);
            Assert.Equal(expected, dp[0, dp.GetLength(0) - 1]);
        }

        [Fact]
        public void EncodeStringIn_TwoCharacter_Encoded()
        {
            string input = "ababab";
            string expected = "3[ab]";
            string[,] dp = Tools.GetComboArr(input);

            _ICompressor.EncodeStringIn(dp, input, 0, input.Length - 1);
            Assert.Equal(expected, dp[0, dp.GetLength(0) - 1]);
        }

        [Fact]
        public void EncodeStringIn_ThreeCharacter_Encoded()
        {
            string input = "abcabcabc";
            string expected = "3[abc]";
            string[,] dp = Tools.GetComboArr(input);

            _ICompressor.EncodeStringIn(dp, input, 0, input.Length - 1);
            Assert.Equal(expected, dp[0, dp.GetLength(0) - 1]);
        }

        [Fact]
        public void EncodeStringIn_FourCharacter_Encoded()
        {
            string input = "abcdabcd";
            string expected = "2[abcd]";
            string[,] dp = Tools.GetComboArr(input);

            _ICompressor.EncodeStringIn(dp, input, 0, input.Length - 1);
            Assert.Equal(expected, dp[0, dp.GetLength(0) - 1]);
        }
    }
}
