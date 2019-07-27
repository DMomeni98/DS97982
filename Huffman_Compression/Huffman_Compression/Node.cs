using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huffman_Compression
{
    class Node
    {
        public Node() { }

        public Node(string key, int value)
        {
            Key = key;
            Value = value;
            //Level = -1;
            //HuffmanCode = null;
            //Left = new Node();
            //Right = new Node();
        }

        public string HuffmanCode;
        public string Key;
        public int Value;
        public int Level;
        public Node Left;
        public Node Right;

        public bool IsLeaf()
        {
            if (this.Left == null && this.Right == null)
                return true;
            return false;
        }

        public override string ToString()
        {
            return Key + "," + Value + " #" + HuffmanCode + " @" + Level;
        }

    }
}
