using GoogleDevGuide.Interface;

namespace GoogleDevGuide
{
    public class Compressor : ICompressor
    {
        public string Compress(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }
                
            string[,] dp = new string[input.Length, input.Length];

            for (int z = 0; z < input.Length; z++)
            {
                for (int i = 0; i < input.Length - z; i++)
                {
                    int j = i + z;
                    string substr = input.Substring(i, j - i + 1);
                    // Checking if string length < 5. In that case, we know that encoding will not help.

                    dp[i, j] = substr;

                    if (j - i >= 4)
                    {                      
                        // Loop for trying all results that we get after dividing the strings into 2 and combine the results of 2 substrings
                        for (int k = i; k < j; k++)
                        {
                            string a1 = dp[i, k];
                            string b1 = dp[k + 1, j];
                            string c1 = dp[i, j];

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

        public string Decompress(string input)
        {
            string result = null;

            return result;
        }
    }
}
