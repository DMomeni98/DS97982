using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Huffman_Compression
{
    class Decode
    {
        readonly string filePath;

        public Decode(string filepath)
        {
            this.filePath = filepath;
        }

        public Dictionary<string, char> getCharactersCode()
        {
            string line;
            string code;
            char character;
            Dictionary<string, char> charactersCode = new Dictionary<string, char>();
            using (StreamReader reader = new StreamReader(this.filePath))
            {
                while ((line = reader.ReadLine()) != "_end_")
                {
                    character = line[0];
                    code = line.Substring(2);
                    charactersCode.Add(code, character);
                }
            }
            return charactersCode;
        }

        public void decodeToString()
        {
            Dictionary<string, char> charactersCode = getCharactersCode();
            string result="";
            string line;
            string code = "";
            StreamWriter writer = new StreamWriter("Decode_Result.txt");
            using (StreamReader reader = new StreamReader(this.filePath))
            {
                while ((line = reader.ReadLine()) != "_end_") ;
                while ((line = reader.ReadLine()) != null)
                {
                    foreach (var i in line)
                    {
                        code += i;
                        foreach (var j in charactersCode)
                        {
                            if (code == j.Key)
                            {
                                result += j.Value;
                                code = "";
                            }

                        }
                    }
                    writer.WriteLine(result);
                    Console.WriteLine(result);
                    result = "";
                }
            }
            writer.Close();
        }


    }
}
