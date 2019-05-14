using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyChange
{
    public class MoneyChange
    {
        private static readonly int[] Money = new int[] { 1, 3, 4 };

        public long Change(long n)
        {
            var minCoinCount = new List<long> { 0, 1, 2, 1, 1 };
            for (int i = 5; i <= n; i++)
            {
                minCoinCount.Add(MinCount(minCoinCount, i));
            }
            return minCoinCount[(int)n];
        }

        public long MinCount(List<long> counts, int index)
        {
            long min = Math.Min(counts[index - 1], counts[index - 3]);
            min = Math.Min(min, counts[index - 4]);
            return min + 1;
        }
    }
}
