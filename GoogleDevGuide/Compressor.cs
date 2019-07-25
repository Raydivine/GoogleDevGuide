using GoogleDevGuide.Extension;
using GoogleDevGuide.Interface;
using System;
using System.Text.RegularExpressions;

namespace GoogleDevGuide
{
    public class Compressor : ICompressor
    {
        private Regex _multiZipped = new Regex(@"\d+\[[^][]+\]");
        private Regex _singleZipped = new Regex(@"^\d+\[([^][]+)\]$");


        /// <summary>
        /// 
        /// 1. Checking if string length less than 5. In that case, we know that encoding will not help.
        /// 2. Loop for trying all results that we get after dividing the strings into 2 and combine the results of 2 substrings
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string Compress(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return input;
            }
                
            string[,] dp = new string[input.Length, input.Length];

            for (int z = 0; z < input.Length; z++)
            {
                for (int i = 0; i < input.Length - z; i++)
                {
                    int j = i + z;
                    string substr = input.Substring(i, z + 1);
                    dp[i, j] = substr;

                    if (j - i >= 4)
                    {                      
                        for (int k = i; k < j; k++)
                        {
                            if ((dp[i, k] + dp[k + 1, j]).Length < dp[i, j].Length)
                            {
                                dp[i, j] = dp[i, k] + dp[k + 1, j];
                            }
                        }
                        this.EncodeStringIn(dp, substr, i, j);
                    }
                }
            }
            return dp[0, input.Length - 1];
        }

        public void EncodeStringIn(string[,] dp, string substr, int i, int j)
        {
            for (int k = 0; k < substr.Length; k++)
            {
                string repeatStr = substr.Substring(0, k + 1);

                if (string.IsNullOrEmpty(repeatStr) == false
                   && substr.Length % repeatStr.Length == 0
                   && substr.Replace(repeatStr, string.Empty).Length == 0)
                {
                    string encodedStr = (substr.Length / repeatStr.Length) + "[" + dp[i, i + k] + "]";
                    if (encodedStr.Length < dp[i, j].Length)
                    {
                        dp[i, j] = encodedStr;
                    }
                }
            }
        }

        public string Extract(string input)
        {
            if(input != null)
            {
                while(_multiZipped.IsMatch(input))
                {
                    input = _multiZipped.Replace(input, Decode);
                }
            }

            return input;
        }

        public string Decode(Match match)
        {
            return Decode(match.Value);
        }
  

        public string Decode(string input)
        {        
            if (input == null || ! _singleZipped.IsMatch(input))
            {
                return input;
            }

            string word = _singleZipped.Match(input).Groups[1].Value;

            int num = int.Parse(input.Substring(0, input.IndexOf("[")));

            return word.Repeat(num);
        }
    }
}
