using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace CPE200Lab1
{
    class RPNCalculatorEngine : CalculatorEngine
    {
        public string Process(string str)
        {
            string result;
            Stack stack = new Stack();
            List<string> parts = str.Split(' ').ToList<string>();
            for (int i = 0; i < parts.Count; i++) Console.WriteLine(parts[i]);
            for (int i=0;i<parts.Count;i++)
            {
                //Console.WriteLine(parts[i] + "++");
                if (parts[i] == "+" || parts[i] == "-" || parts[i] == "X" || parts[i] == "÷")
                {
                    double firstPop = Convert.ToDouble(stack.Pop());
                    double secondPop = Convert.ToDouble(stack.Pop());
                    result = calculate(parts[i], secondPop.ToString(), firstPop.ToString(), 4);
                    stack.Push(Convert.ToDouble(result));
                }
                else // must be number
                {
                    stack.Push(Convert.ToDouble(parts[i]));
                    //Console.WriteLine("-" + stack.Peek());
                }
            }

            return stack.Pop().ToString();
        }

    }
}
