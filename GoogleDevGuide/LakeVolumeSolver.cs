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
            double sum = 0;
            int i = 0;

            if (groundHeights != null && groundHeights.Length >= 3)
            {
                while (groundHeights.Length - i > 1)
                {
                    sum += this.GetSizeBetweenPeaks(groundHeights, ref i);
                }
            }

            return sum;
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
