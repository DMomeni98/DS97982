using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Huffman_Compression
{
    class CompressText
    {
        public CompressText(string filePath)
        {
            RawFilePath = filePath;
            EncodedFilePath = RawFilePath.Remove(RawFilePath.IndexOf(".txt")) + "_encoded";
            //this.CharsCountDictionary = new Dictionary<char, int>();
            //this.CharsCodeDictionary = new Dictionary<char, string>();
            //this.Root = new Node();
        }

        private string RawFilePath;

        private string EncodedFilePath;

        private Node Root;

        private Dictionary<int, int> CharsCountDictionary;

        private Dictionary<int, string> CharsCodeDictionary;

        public void Compress()
        {
            this.CharsCountDictionary=BuildCharsDictionary(this.RawFilePath);
            List<Node> tree = ConvertToTree(this.CharsCountDictionary);
            this.Root = BuildHuffmanTree(tree);
            this.CharsCodeDictionary = FindCharsHuffmanCode(this.Root);
            int lastByteLength=Encode();
            CreateDecodingDoc(lastByteLength);
        }

        private static Dictionary<int, int> BuildCharsDictionary(string filePath)
        {
            Dictionary<int, int> dictionary = new Dictionary<int, int>();
            using (StreamReader reader = new StreamReader(filePath))
            {
                int temp;
                while (!reader.EndOfStream)
                {
                    temp = reader.Read();
                        if (dictionary.ContainsKey(temp))
                            dictionary[temp]++;
                        else
                            dictionary.Add(temp, 1);
                }

            }
            return dictionary.OrderByDescending(x => x.Value).
                ToDictionary(x => x.Key, y => y.Value);
        }
        
        private static List<Node> ConvertToTree(Dictionary<int, int> charsDictionary)
        {
            List<Node> tree = new List<Node>();
            foreach (var item in charsDictionary)
            {
                Node node = new Node(((char)item.Key).ToString(), item.Value);
                tree.Add(node);
            }
            return tree;
        }

        private static Node BuildHuffmanTree(List<Node> tree)
        {
            while (tree.Count!=1)
            {
                Node minNode1 = tree[tree.Count - 1];
                Node minNode2 = tree[tree.Count - 2];
                Node newNode = new Node(minNode1.Key + minNode2.Key, minNode1.Value + minNode2.Value)
                {
                    Left = minNode1,
                    Right = minNode2
                };
                int index = 0;
                if(tree.Count!=0)
                    index = tree.IndexOf(tree.Where(x => x.Value <= newNode.Value).First());
                tree.Insert(index, newNode);
                tree.RemoveAt(tree.Count - 1);
                tree.RemoveAt(tree.Count - 1);

            }
            tree[0].Level = 0;
            return tree[0];
        }

        private Dictionary<int, string> FindCharsHuffmanCode(Node root)
        {
            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(root);
            Node node;
            while (queue.Count!=0)
            {
                node = queue.Dequeue();
                if (node.Left != null)
                {
                    node.Left.HuffmanCode += node.HuffmanCode + "0";
                    node.Left.Level = node.Level + 1;
                    queue.Enqueue(node.Left);
                }
                if (node.Right != null)
                {
                    node.Right.HuffmanCode += node.HuffmanCode + "1";
                    node.Right.Level = node.Level + 1;
                    queue.Enqueue(node.Right);
                }
                if (node.IsLeaf())
                    dictionary.Add((int)char.Parse(node.Key), node.HuffmanCode);
            }
            return dictionary;
        }

        private int Encode()
        {
            int lastByteLength;
            using (StreamReader read = new StreamReader(RawFilePath))
            {
                using (BinaryWriter writer = new BinaryWriter(File.Open(EncodedFilePath + ".bin", FileMode.Create)))
                { 
                    int temp;
                    StringBuilder byteBuilder = new StringBuilder(8);

                    while (!read.EndOfStream)
                    {
                        temp = read.Read();
                        StringBuilder charCode = new StringBuilder(CharsCodeDictionary[temp]);
                        while (charCode !=null)
                        {
                            if (byteBuilder.Length == 8)
                            {
                                writer.Write(byteBuilder.ToString());
                                byteBuilder.Clear();
                            }
                            if (byteBuilder.Length + charCode.Length <= 8)
                            {
                                byteBuilder.Append(charCode);
                                charCode = null;
                            }
                            else
                            {
                                int length = 8 - byteBuilder.Length;
                                byteBuilder.Append(charCode.ToString().Substring(0,length));
                                writer.Write(byteBuilder.ToString());
                                byteBuilder.Clear();
                                charCode.Remove(0, length);
                            }
                        }
                    }
                    lastByteLength = byteBuilder.Length < 8 ? byteBuilder.Length : 8;
                    while (byteBuilder.Length<8)
                    {
                        byteBuilder.Append(0);
                    }
                    writer.Write(byteBuilder.ToString());
                    byteBuilder.Clear();
                }
            }
            return lastByteLength;
        }

        private void CreateDecodingDoc(int lastByteLength)
        {
            StreamWriter write = new StreamWriter(EncodedFilePath+"Doc.txt", false,Encoding.ASCII);
            write.WriteLine(lastByteLength);
            foreach (var item in CharsCodeDictionary)
                write.WriteLine(item.Key + ":" + item.Value);
            write.WriteLine("_end_");
            write.Close();
            write.Dispose();
        }

        public void PrintTree()
        {
            Console.WriteLine("tree:");
            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(this.Root);
            int currentLevel = 0;
            Node node;
            StringBuilder line = new StringBuilder();
            while (!queue.All(x => x==null))
            {
                node = queue.Dequeue();
                if (node != null)
                {
                    queue.Enqueue(node.Left);
                    queue.Enqueue(node.Right);
                    if (currentLevel == node.Level)
                        line.Append(node + "\t");
                    else
                    {
                        Console.WriteLine(line);
                        line.Clear();
                        currentLevel++;
                        line.Append(node + "\t");
                    }
                }
                else
                    line.Append("***\t");
            }
                Console.WriteLine(line);
                line.Clear();
            Console.WriteLine("_____end_____\n");
        }

        public void PrintCharsCountDictionary()
        {
            Console.WriteLine("chars count:");
            foreach (var item in this.CharsCountDictionary)
                Console.Write((char)item.Key + ":\t" + item.Value + "\n");
            Console.WriteLine("total count: " + this.CharsCountDictionary.Sum(x => x.Value));
            Console.WriteLine("_____end_____\n");
        }

        public void PrintCharsCodeDictionary()
        {
            Console.WriteLine("chars Huffman code: ");
            foreach (var item in this.CharsCodeDictionary)
                Console.Write((char)item.Key + ":\t" + item.Value + "\n");
            Console.WriteLine("_____end_____\n");
        }
    }
}
