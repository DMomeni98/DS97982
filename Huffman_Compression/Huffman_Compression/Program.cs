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
            string filePath = @"sample1.txt";
            CompressText newFile = new CompressText(filePath);
            newFile.Compress();
            newFile.PrintCharsCountDictionary();
            newFile.PrintTree();
            newFile.PrintCharsCodeDictionary();
            string fileToDecode = "result.txt";
            Decode decode = new Decode(fileToDecode);
            decode.decodeToString();
        }

    }
}
