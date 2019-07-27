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
            if (args[args.Length - 1] == "-compress")
            {
                CompressText newFile = new CompressText(args[0]);
                newFile.Compress();
                newFile.PrintCharsCountDictionary();
                //newFile.PrintTree();
                newFile.PrintCharsCodeDictionary();
            }
            if (args[args.Length - 1] == "-decode")
            {
                Decode decode = new Decode(args[0], args[1]);
                decode.decodeToString();
            }
            //CompressText newFile = new CompressText("for_esme.txt");
            //newFile.Compress();
            //newFile.PrintCharsCountDictionary();
            //newFile.PrintCharsCodeDictionary();
            //newFile.PrintTree();
        }
    }
}
