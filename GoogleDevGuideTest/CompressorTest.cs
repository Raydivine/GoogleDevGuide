using System;
using Xunit;
using GoogleDevGuide;
using GoogleDevGuide.Interface;

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
        public void Compress_InputIsEmpty_ReturnNull()
        {
            string input = "";
            var result = _ICompressor.Compress(input);

            Assert.Null(result);
        }

        [Fact]
        public void Compress_OneCharacter_ReturnSame()
        {
            string input = "a";
            string expected = "a";

            var result = _ICompressor.Compress(input);

            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Do compression only when the repeated pattern's size is greater than 4
        ///  
        /// Example
        /// Input             : aaaa  (the size is 4)
        /// Compressed Output : 4[a]  (the size is 4 too, which does not reduce)
        ///  
        /// therefore there is no meaning to do compression
        /// 
        /// </summary>
        [Fact]
        public void Compress_FourSameCharacters_ReturnSame()
        {
            string input = "aaaa";
            string expected = "aaaa";

            var result = _ICompressor.Compress(input);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Compress_FiveSameCharacters_ReturnCompressedResult()
        {
            string input = "aaaaaa";  
            string expected = "5[a]";

            var result = _ICompressor.Compress(input);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Compress_PartternReapatedTwice_ReturnCompressedResult()
        {
            string input = "abcabc";
            string expected = "2[abc]";

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
            string input = "1231233213216677";
            string expected = "2[123]2[321]6677";

            var result = _ICompressor.Compress(input);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Compress_InputContainBothCharacterAndNumeric_ReturnCompressedResult()
        {
            string input = "1231233213216677abcabcabcababababc";
            string expected = "2[123]2[321]66773[abc]4[ab]c";

            var result = _ICompressor.Compress(input);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Compress_InputIsSymbols_ReturnCompressedResult()
        {
            string input = "@#$@#$ %%% ^&*()^&*() ";
            string expected = "2[@#$]%%%2[^&*()]";

            var result = _ICompressor.Compress(input);

            Assert.Equal(expected, result);
        }
    }
}
