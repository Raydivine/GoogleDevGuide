using System.Collections.Generic;
using System.Linq;

namespace GoogleDevGuide
{
    public class LakeSizeCalculator
    {
        /// <summary>
        /// https://techdevguide.withgoogle.com/paths/advanced/volume-of-water/#!
        /// </summary>
        /// <param name="groundHeights"></param>
        /// <returns></returns>
        public double LakeSizeCalculate(double[] groundHeights)
        {
            double sum = 0;
            List<double> grounds = new List<double>();

            if (groundHeights != null || groundHeights.Length >= 3)
            {
                for (int i = 0; i < groundHeights.Length; i++)
                {
                    startPeak = groundHeights[i];

                    if (groundHeights[i + 1] >= startPeak)
                    {
                        continue;
                    }
                }
            }

            return sum;
        }

        private double GetSizeBetweenPeaks(double[] groundHeights, ref int i)
        {
            double lakeSize = 0, startPeak, current, endPeak, shorterPeak;
            var floors = new List<double>();

            if (IsThereVolume(groundHeights, i))
            {
                startPeak = groundHeights[i++];

                do
                {
                    current = groundHeights[i];
                    floors.Add(current);
                    endPeak = groundHeights[++i];

                } while (groundHeights.Length - i > 1 && endPeak < startPeak);

                shorterPeak = (startPeak <= endPeak) ? startPeak : endPeak;

                foreach (var floor in floors)
                {
                    lakeSize += (shorterPeak - floor);
                }
            }

            return lakeSize;
        }

        private bool IsThereVolume(double[] groundHeights, int i)
        {
            if (groundHeights != null &&
                groundHeights.Length - i >= 3 &&
                groundHeights[i] > groundHeights[i + 1])
            {
                var list = groundHeights.ToList();
                list.RemoveRange(0, i + 2);

                if (list.Any(x => x > groundHeights[i + 1]))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
