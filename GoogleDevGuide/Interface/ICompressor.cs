using System;
using System.Collections.Generic;
using System.Text;

namespace GoogleDevGuide.Interface
{
    public interface ICompressor
    {
        string Compress(string input);
        string Decompress(string input);

    }
}
