using GoogleDevGuide;
using GoogleDevGuide.Interface;
using GoogleDevGuideTest.Common;
using System;
using Xunit;

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

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]
        public void Compress_InputIsBlank_ReturnInput(string input)
        {
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
        public void Compress_PatternTwoWordsReapatedTwice_ReturnInput()
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
        public void Compress_PatternTwoWordsReapatedThreeTimes_ReturnInput()
        {
            string input = "ababab";
            string expected = "3[ab]";

            var result = _ICompressor.Compress(input);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Compress_PatternTwoWordsReapated12Times_ReturnInput()
        {
            string input = "abababababababababababab";
            string expected = "12[ab]";

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
        public void Compress_PatternThreeWordsReapatedTwice_ReturnInput()
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
        public void Compress_PatternThreeWordsReapatedThreeTimes_ReturnInput()
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
        public void Compress_PatternFourWordsReapatedTwice_ReturnInput()
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

        [Fact]
        public void Compress_InputIsChinese_ReturnCompressedResult()
        {
            string input = "水水水水水水嘎嘎嘎哈哈哈哈哈但是事实上";
            string expected = "6[水]嘎嘎嘎5[哈]但是事实上";

            var result = _ICompressor.Compress(input);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Compress_InputCanBeDoubleCompress_ReturnCompressedResult()
        {
            string input = "aaaaabaaaaab";
            string expected = "2[5[a]b]";

            var result = _ICompressor.Compress(input);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Compress_InputCanBeTripleCompress_ReturnCompressedResult()
        {
            string input = "aaaaabaaaaabccaaaaabaaaaabcc";
            string expected = "2[2[5[a]b]cc]";

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

            _ICompressor.EncodeStringIn(dp, input, 0, input.Length - 1);
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

    public class ExtractTest : IDisposable
    {
        ICompressor _ICompressor;

        public ExtractTest()
        {
            _ICompressor = new Compressor();
        }

        public void Dispose()
        {

        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]
        public void Extract_InputIsBlank_ReturnInput(string input)
        {
            var result = _ICompressor.Extract(input);

            Assert.Equal(input, result);
        }

        [Theory]
        [InlineData("abcdf")]
        [InlineData("8[a")]
        [InlineData("8a]")]
        public void Extract_NothingToExtract_ReturnInput(string input)
        {
            var result = _ICompressor.Extract(input);

            Assert.Equal(input, result);
        }

        [Fact]
        public void Extract_CanBeExtract_ReturnExtractedResult()
        {
            string input = "1[a]";
            string expected = "a";

            var result = _ICompressor.Extract(input);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Extract_ZippedOneChar_ReturnExtractedResult()
        {
            string input = "16[a]";
            string expected = "aaaaaaaaaaaaaaaa";

            var result = _ICompressor.Extract(input);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Extract_ZippedTwoChar_ReturnExtractedResult()
        {
            string input = "4[ab]";
            string expected = "abababab";

            var result = _ICompressor.Extract(input);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Extract_ZippedThreeCharAmbiguous_ReturnExtractedResult()
        {
            string input = "3[abc]5[ab]5[bc]";
            string expected = "abcabcabcabababababbcbcbcbcbc";

            var result = _ICompressor.Extract(input);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Extract_ZippedThreeChar_ReturnExtractedResult()
        {
            string input = "3[abc]";
            string expected = "abcabcabc";

            var result = _ICompressor.Extract(input);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Extract_ZippedFourChar_ReturnExtractedResult()
        {
            string input = "2[abcd]";
            string expected = "abcdabcd";

            var result = _ICompressor.Extract(input);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Extract_ZippedChars_ReturnExtractedResult()
        {
            string input = "3[abc]4[ab]c";
            string expected = "abcabcabcababababc";

            var result = _ICompressor.Extract(input);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Extract_ZippedNums_ReturnExtractedResult()
        {
            string input = "3[123]3[321]6677";
            string expected = "1231231233213213216677";

            var result = _ICompressor.Extract(input);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Extract_ZippedCharsAndNums_ReturnExtractedResult()
        {
            string input = "3[123]3[321]3[abc]4[ab]c";
            string expected = "123123123321321321abcabcabcababababc";

            var result = _ICompressor.Extract(input);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Extract_ZippedSymbols_ReturnExtractedResult()
        {
            string input = "3[@#$]%%%2[^&*()]";
            string expected = "@#$@#$@#$%%%^&*()^&*()";

            var result = _ICompressor.Extract(input);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Extract_InputIsChinese_ReturnExtractedResult()
        {
            string input = "6[水]嘎嘎嘎5[哈]但是事实上";
            string expected = "水水水水水水嘎嘎嘎哈哈哈哈哈但是事实上";

            var result = _ICompressor.Extract(input);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Extract_NestedZipped_ReturnExtractedResult()
        {
            string input = "2[5[a]b]";
            string expected = "aaaaabaaaaab";

            var result = _ICompressor.Extract(input);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Extract_TripleNestedZipped_ReturnExtractedResult()
        {
            string input = "2[2[5[a]b]cc]";
            string expected = "aaaaabaaaaabccaaaaabaaaaabcc";

            var result = _ICompressor.Extract(input);

            Assert.Equal(expected, result);
        }
    }

    public class DecodeTest : IDisposable
    {
        ICompressor _ICompressor;

        public DecodeTest()
        {
            _ICompressor = new Compressor();
        }

        public void Dispose()
        {

        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]
        public void Decode_StringIsBlank_returnInput(string input)
        {
            string result = _ICompressor.Decode(input);

            Assert.Equal(input, result);
          
        }

        [Theory]
        [InlineData("abcdf")]
        [InlineData("8[a")]
        [InlineData("8a]")]
        public void Decode_NothingToDecode_ReturnInput(string input)
        {
            var result = _ICompressor.Decode(input);

            Assert.Equal(input, result);
        }

        /// <summary>
        /// This method can only extract string of single Zipped : 3[abc] 
        /// Other than that should return input
        /// </summary>
        [Theory]
        [InlineData("wert2[abcd]")]
        [InlineData("2[abcd]wert")]
        [InlineData("3[abc]4[ab]c")]
        [InlineData("2[5[a]b]")]
        [InlineData("2[2[5[a]b]cc]")]
        public void Decode_ComplicatedZipped_ReturnInput(string input)
        {
            var result = _ICompressor.Decode(input);

            Assert.Equal(input, result);
        }

        [Fact]
        public void Decode_CanBeDecode_ReturnDecodedResult()
        {
            string input = "1[a]";
            string expected = "a";

            var result = _ICompressor.Decode(input);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Decode_ZippedOneChar_ReturnDecodedResult()
        {
            string input = "16[a]";
            string expected = "aaaaaaaaaaaaaaaa";

            var result = _ICompressor.Decode(input);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Decode_ZippedTwoChar_ReturnDecodedResult()
        {
            string input = "4[ab]";
            string expected = "abababab";

            var result = _ICompressor.Decode(input);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Decode_ZippedThreeChar_ReturnDecodedResult()
        {
            string input = "3[abc]";
            string expected = "abcabcabc";

            var result = _ICompressor.Decode(input);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Decode_ZippedFourChar_ReturnDecodedResult()
        {
            string input = "2[abcd]";
            string expected = "abcdabcd";

            var result = _ICompressor.Decode(input);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Decode_ZippedSymbols_ReturnDecodedResult()
        {
            string input = "3[@#$]";
            string expected = "@#$@#$@#$";

            var result = _ICompressor.Decode(input);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Decode_InputIsChinese_ReturnDecodedResult()
        {
            string input = "6[水]";
            string expected = "水水水水水水";

            var result = _ICompressor.Decode(input);

            Assert.Equal(expected, result);
        }


    }


}
