using System.Collections.Generic;
using System.Linq;

namespace GoogleDevGuide.Extension
{
    public static class StringEx
    {
        public static string GetLongestSubSequence(this string str, string[] words)
        {
            return str.GetLongestSubSequence(words.ToList());
        }

        public static string GetLongestSubSequence(this string str, List<string> words)
        {
            string result = null;
        
            return result;
        }
    }
}
