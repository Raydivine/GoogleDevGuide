using System;
using System.Collections.Generic;
using System.Text;

namespace GoogleDevGuide.Interface
{
    public interface ICompressor
    {
        string Compress(string input);

        string Extract(string input);

        void EncodeStringIn(string[,] dp, string substr, int i, int j);

        string Decode(string input);
    }
}
