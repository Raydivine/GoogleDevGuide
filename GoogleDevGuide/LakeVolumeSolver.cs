using System.Collections.Generic;
using System.Linq;

namespace GoogleDevGuide
{
    public class LakeVolumeSolver
    {
        /// <summary>
        /// https://techdevguide.withgoogle.com/paths/advanced/volume-of-water/#!
        /// </summary>
        /// <param name="groundHeights"></param>
        /// <returns></returns>
        public double LakeSizeCalculate(double[] groundHeights)
        {
            double result = 0, current;
            int start = 0, end = groundHeights.Length - 1;

            while(start<end)
            {
                if (groundHeights[start] <= groundHeights[end])
                {
                    current = groundHeights[start];
                    while (groundHeights[++start] < current)
                    {
                        result += current - groundHeights[start];
                    }
                }
                else
                {
                    current = groundHeights[end];
                    while (groundHeights[--end] < current)
                    {
                        result += current - groundHeights[end];
                    }
                }
            }
            return result;
        }

        private double GetSizeBetweenPeaks(double[] groundHeights, ref int i)
        {
            double lakeSize = 0;
            var floors = new List<double>();

            if (IsThereVolume(groundHeights, i))
            {
                int start = i++;
                int end = i;

                do
                {
                    floors.Add(groundHeights[i++]);
                    end = groundHeights[i] > floors.Max() ? i : end;

                } while (i < groundHeights.Length - 1 && groundHeights[end] < groundHeights[start]);

                double shorterPeak = (groundHeights[start] <= groundHeights[end]) ? groundHeights[start] : groundHeights[end];

                for (int j = start + 1; j < end; j++)
                {
                    lakeSize += (shorterPeak - groundHeights[j]);
                }
            }
            else
            {
                i++;
            }

            return lakeSize;
        }

        private bool IsThereVolume(double[] groundHeights, int i)
        {
            if (groundHeights != null &&
                groundHeights.Length - i >= 3 &&
                groundHeights[i] > groundHeights[i + 1])
            {
                double[] elmntsAfter = groundHeights.Skip(i + 2).ToArray();

                if (elmntsAfter.Any(x => x > groundHeights[i + 1]))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
