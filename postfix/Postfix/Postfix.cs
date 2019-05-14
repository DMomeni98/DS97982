using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Postfix
{
    class Postfix
    { 
        static void Main(string[] args)
        {
            int count = int.Parse(Console.ReadLine());
            string[] input = new string[count];
            for (int i = 0; i < count; i++)
                input[i] = Console.ReadLine();
            Compute(input);
        }

        private static Stack<int> Stack = new Stack<int>();

        public static int Compute(string[] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                int result = 0;
                if (int.TryParse(input[i], out result))
                    Stack.Push(result);
                else
                {
                    int op2 = Stack.Pop();
                    int op1 = Stack.Pop();
                    Stack.Push(StringToOperator(input[i], op1, op2));
                }
                ShowStack(i);
                Console.WriteLine("END");
            }
            if (Stack.Count != 1)
                throw new IncorrectInputFormatException();
            return Stack.Pop();
        }

        private static int StringToOperator(string s, int op1, int op2)
        {
            int result = -1;
            switch(s)
            {
                case "+":
                    result= op1 + op2;
                    break;
                case "-":
                    result= op1 - op2;
                    break;
                case "*":
                    result= op1* op2;
                    break;
                case "/":
                    result= op1 / op2;
                    break;
                case "^":
                    result = Convert.ToInt32(Math.Pow(op1, op2));
                    break;
            }
            return result;
        }

        public static void ShowStack(int phase)
        {
            Console.WriteLine($"stack level: {phase}\n");
            Stack<int> stackTemp = new Stack<int>(new Stack<int>(Stack));
            while(stackTemp.Count !=0)
            {
                Console.WriteLine("|" + stackTemp.Pop() + "|");
            }
            Console.WriteLine(" --");
            Thread.Sleep(1000);
        }
    }
}
