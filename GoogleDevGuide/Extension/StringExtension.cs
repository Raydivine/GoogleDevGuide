using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoogleDevGuide.Extension
{
    public static class StringExtension
    {
        public static string GetLongestSubSequence(this string str, string[] words)
        {
            return str.GetLongestSubSequence(words.ToList());
        }

        public static string GetLongestSubSequence(this string str, List<string> words)
        {
            string result = string.Empty;

            foreach (var word in words)
            {
                if(str.IsSubSequence(word))
                {
                    if (word.Length>result.Length)
                    {
                        result = word;
                    }
                }
            }

            return result;
        }

        public static bool IsSubSequence(this string str, string word)
        {
            string result = null;
            int j = 0;

            if (string.IsNullOrWhiteSpace(str) || string.IsNullOrWhiteSpace(word))
            {
                return false;
            }
                

            for (int i = 0; i < str.Length && j < word.Length; i++)
            {
                if (word[j] == str[i])
                {
                    result += word[j];
                    j++;
                }
            }

            return string.Equals(result, word);
        }

        public static string Repeat(this string s, int n)
        {
            return new StringBuilder(s.Length * n)
                            .AppendJoin(s, new string[n + 1])
                            .ToString();
        }
    }
}
