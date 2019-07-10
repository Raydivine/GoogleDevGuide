using GoogleDevGuide.Interface;
using System.Collections.Generic;
using System.Linq;

namespace GoogleDevGuide
{
    public class PuzzleSolver : IPuzzleSolver
    {
        public string GetLongestWordInSequence(string target, string[] words)
        {
            return GetLongestWordInSequence(target, words.ToList());
        }

        public string GetLongestWordInSequence(string target, List<string> words)
        {
            string result = null;

            return result;
        }
    }
}
