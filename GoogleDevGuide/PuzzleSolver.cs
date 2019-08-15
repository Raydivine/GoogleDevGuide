using GoogleDevGuide.CustomExceptions;
using GoogleDevGuide.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GoogleDevGuide
{
    public class PuzzleSolver : IPuzzleSolver
    {
        public int GetMaxNumOfCandiesToGain(int[] arr)
        {
            if (arr.Length == 0 || arr.Length%2 !=0)
            {
                throw new NumberIsNotEvenException();
            }

            return Math.Min(arr.Distinct().ToArray().Length, arr.Length/2);         
        }
    }
}
