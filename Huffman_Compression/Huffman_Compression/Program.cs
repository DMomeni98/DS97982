using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huffman_Compression
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"for_esme.txt";
            CompressText newFile = new CompressText(filePath);
            newFile.Compress();
            newFile.PrintCharsCountDictionary();
            //newFile.PrintTree();
            newFile.PrintCharsCodeDictionary();

        }

    }
}
