using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoogleDevGuide.Extension
{
    public static class StringExtension
    {
        public static string GetLongestSubSequence(this string str, string[] words, StringComparison comparison = StringComparison.CurrentCulture)
        {
            return str.GetLongestSubSequence(words.ToList(), comparison);
        }

        public static string GetLongestSubSequence(this string str, List<string> words, StringComparison comparison = StringComparison.CurrentCulture)
        {
            string result = string.Empty;

            foreach (var word in words)
            {
                if (str.IsSubSequence(word, comparison))
                {
                    if (word.Length > result.Length)
                    {
                        result = word;
                    }
                }
            }

            return result;
        }

        public static bool IsSubSequence(this string str, string word, StringComparison comparison = StringComparison.CurrentCulture)
        {
            string result = null;
            int j = 0;

            if (string.IsNullOrWhiteSpace(str) || string.IsNullOrWhiteSpace(word))
            {
                return false;
            }


            for (int i = 0; i < str.Length && j < word.Length; i++)
            {
                if (string.Equals(word[j].ToString(), str[i].ToString(), comparison))
                {
                    result += word[j];
                    j++;
                }


            }

            return string.Equals(result, word, comparison);
        }

        public static string Repeat(this string s, int n)
        {
            return new StringBuilder(s.Length * n)
                            .AppendJoin(s, new string[n + 1])
                            .ToString();
        }
    }
}
