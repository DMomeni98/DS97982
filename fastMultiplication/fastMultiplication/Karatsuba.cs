using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karatsuba_Multiplication
{
    public class Karatsuba
    {
        public static long multiply(long x, long y)
        {
            long result = 0;
            int size1 = GetSize(x);
            int size2 = GetSize(y);
            int N = Math.Max(size1, size2);
            if (N < 2)
                return x * y;
            N = (N / 2) + (N % 2);
            long m = (long)Math.Pow(10, N);
            long b = x % m;
            long a = x / m;
            long c = y / m;
            long d = y % m;
            long z0 = multiply(a, c);
            long z1 = multiply(b, d);
            long z2 = multiply(a + b, c + d);
            result = ((long)Math.Pow(10, N * 2) * z0) + z1 + ((z2 - z1 - z0) * m);
            return result;
        }
        
        public static int  GetSize(long num)
        {
            int len = 0;
            while (num != 0)
            {
                len++;
                num /= 10;
            }
            return len;
        }
    }
}