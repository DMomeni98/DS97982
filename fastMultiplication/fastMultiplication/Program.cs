using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Karatsuba_Multiplication
{
    static class Program
    {
        static void Main()
        {
            long result= Karatsuba.multiply(123,456);
            Console.WriteLine(result);
        }
    }
}