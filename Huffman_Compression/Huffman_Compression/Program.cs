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
<<<<<<< HEAD
            string filePath = args[0];
            if (args[1] =="-compress")
            {
                CompressText newFile = new CompressText(filePath);
                newFile.Compress();
                newFile.PrintCharsCountDictionary();
                //newFile.PrintTree();
                newFile.PrintCharsCodeDictionary();
            }
            if (args[1] == "-decode")
            {
                //
            }

=======
            string filePath = @"sample1.txt";
            CompressText newFile = new CompressText(filePath);
            newFile.Compress();
            newFile.PrintCharsCountDictionary();
            newFile.PrintTree();
            newFile.PrintCharsCodeDictionary();
            string fileToDecode = "result.txt";
            Decode decode = new Decode(fileToDecode);
            decode.decodeToString();
>>>>>>> bc2be84c5407f16907b0822af0d92023fb53d9bd
        }

    }
}
