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
            //this.CharsCountDictionary = new Dictionary<char, int>();
            //this.CharsCodeDictionary = new Dictionary<char, string>();
            //this.Root = new Node();
        }

        private string RawFilePath;

        private Node Root;

        private Dictionary<char, int> CharsCountDictionary;

        private Dictionary<char, string> CharsCodeDictionary;

        public void Compress()
        {
            this.CharsCountDictionary=BuildCharsDictionary(this.RawFilePath);
            List<Node> tree = ConvertToTree(this.CharsCountDictionary);
            this.Root = BuildHuffmanTree(tree);
            this.CharsCodeDictionary = FindCharsHuffmanCode(this.Root);
            Encode();
        }

        private static Dictionary<char, int> BuildCharsDictionary(string filePath)
        {
            Dictionary<char, int> dictionary = new Dictionary<char, int>();
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    for (int i = 0; i < line.Length; i++)
                    {
                        if (dictionary.ContainsKey(line[i]))
                            dictionary[line[i]]++;
                        else
                            dictionary.Add(line[i], 1);
                    }
                }

            }
            return dictionary.OrderByDescending(x => x.Value).
                ToDictionary(x => x.Key, y => y.Value);
        }
        
        private static List<Node> ConvertToTree(Dictionary<char, int> charsDictionary)
        {
            List<Node> tree = new List<Node>();
            foreach (var item in charsDictionary)
            {
                Node node = new Node(item.Key.ToString(), item.Value);
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

        private Dictionary<char, string> FindCharsHuffmanCode(Node root)
        {
            Dictionary<char, string> dictionary = new Dictionary<char, string>();
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
                    dictionary.Add(char.Parse(node.Key), node.HuffmanCode);
            }
            return dictionary;
        }

        private void Encode()
        {
            StreamWriter write = new StreamWriter("result1_doc.txt", false,Encoding.ASCII);
            foreach (var item in CharsCodeDictionary)
                write.WriteLine(item.Key + ":" + item.Value);
            write.WriteLine("_end_");
            write.Close();
            write.Dispose();
            using (StreamReader read = new StreamReader(RawFilePath))
            {
                using (StreamWriter writer = new StreamWriter(File.Open("result", FileMode.Create)))
                { 
                    string line;
                    while ((line = read.ReadLine()) != null)
                    {
                        for (int i = 0; i < line.Length; i++)
                        {
                            writer.Write(CharsCodeDictionary[line[i]]);
                        }
                    }
                }
            }
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
                Console.Write(item.Key + ":\t" + item.Value + "\n");
            Console.WriteLine("total count: " + this.CharsCountDictionary.Sum(x => x.Value));
            Console.WriteLine("_____end_____\n");
        }

        public void PrintCharsCodeDictionary()
        {
            Console.WriteLine("chars Huffman code: ");
            foreach (var item in this.CharsCodeDictionary)
                Console.Write(item.Key + ":\t" + item.Value + "\n");
            Console.WriteLine("_____end_____\n");
        }
    }
}
