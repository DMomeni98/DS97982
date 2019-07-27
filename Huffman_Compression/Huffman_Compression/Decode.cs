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
        readonly string FileToDecode;
        readonly string FileToDecodeDoc;

        public Decode(string fileToDecode, string fileToDecodeDoc)
        {
            FileToDecode = fileToDecode;
            FileToDecodeDoc = fileToDecodeDoc;
        }



        public byte bitsOfLastByte { get; set; }
        public Dictionary<string, char> getCharactersCode()
        {
            string line;
            string code;
            char character;
            Dictionary<string, char> charactersCode = new Dictionary<string, char>();
            using (StreamReader reader = new StreamReader(this.FileToDecodeDoc))
            {
                this.bitsOfLastByte = Convert.ToByte(reader.ReadLine());
                while ((line = reader.ReadLine()) != "_end_")
                {
                    character = Convert.ToChar(int.Parse(line.Split(':')[0]));
                    code = line.Split(':')[1];
                    charactersCode.Add(code, character);
                }
            }
            return charactersCode;
        }



        public void decodeToString()
        {
            Dictionary<string, char> charactersCode = getCharactersCode();

            byte[] fileBytes = File.ReadAllBytes(FileToDecode);
            StringBuilder sb = new StringBuilder();
            foreach (byte b in fileBytes)
            {
                sb.Append(Convert.ToString(b, 2).PadLeft(8, '0'));
            }

            File.WriteAllText("alaki.txt", sb.ToString());
            byte[] fileToDecode = File.ReadAllBytes(FileToDecode);
            string result="";
            string allBytes =sb.ToString();
            string code = "";
            Console.WriteLine();
            Console.WriteLine(allBytes);
            StreamWriter writer = new StreamWriter("Decode_Result.txt");
            allBytes = allBytes.Substring(0, allBytes.Length - 8 + this.bitsOfLastByte);
            foreach (var i in allBytes)
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
            writer.Write(result);
            Console.Write(result);
            writer.Close();
        }
    }
}
