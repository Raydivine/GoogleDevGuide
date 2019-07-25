using System;
using System.Collections.Generic;
using System.Text;

namespace GoogleDevGuideTest.Common
{
    public static class Tools
    {
        public static string[,] GetComboArr(string str)
        {
            int len = str.Length;

            string[,] arr = new string[len,len];

            for (int i = 0; i < len; i++)
            {
                for(int j = 0; j < len - i; j++)
                {
                    arr[i, j+i] = str.Substring(i, j + 1);
                }
            }

            return arr;
        }
    }
}
