using System;
using System.Collections.Generic;
using System.Text;

namespace GoogleDevGuide.Interface
{
    public interface IPuzzleSolver
    {
        string GetLongestWordInSequence(string target, string[] words);

        string GetLongestWordInSequence(string target, List<string> words);
    }
}
