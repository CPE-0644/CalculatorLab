﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;

namespace CPE200Lab1
{
    class CalculatorEngine
    {
        private bool isNumber(string str)
        {
            double retNum;
            return Double.TryParse(str, out retNum);
        }

        private bool isOperator(string str)
        {
            switch (str)
            {
                case "+":
                case "-":
                case "X":
                case "÷":
                    return true;
            }
            return false;
        }
        public string Process(string str)
        {
            //int index;
            //string value;
            //string[] parts = str.Split(' ');
            //for (int i = 0; i < parts.Length; i++) Console.Write(parts[i]);
            //  Console.WriteLine(Array.IndexOf(parts,"÷"));
            // while ((index = Array.IndexOf(parts, "÷")) != -1)
            //{
            //index = str.IndexOf("÷");
            //Console.WriteLine(parts[index-1] + " " + parts[index] + " " + parts[index + 1]);
            //value = calculate(parts[index], parts[index - 1], parts[index + 1], 4);
            //Console.WriteLine(index - 1 - parts[index].Length);
            //   Console.WriteLine(parts[index - 1].Length + parts[index].Length + parts[index + 1].Length );
            //    string substr = str.Substring(index - parts[index - 1].Length, parts[index - 1].Length + parts[index + 1].Length + 2);
            //    str = str.Replace(substr, value);
            //Console.WriteLine(str);
            //}

            //if (!(isNumber(parts[0]) && isOperator(parts[1]) && isNumber(parts[2])))
            //{
            //    return "E";
            //}
            //else
            //{
            //    return calculate(parts[1], parts[0], parts[2], 4);
            //}
            str = str.Replace("X", "*").Replace("÷", "/");
            string value = new DataTable().Compute(str, null).ToString();
            return value;

        }
        public string unaryCalculate(string operate, string operand, int maxOutputSize = 8)
        {
            switch (operate)
            {
                case "√":
                    {
                        double result;
                        string[] parts;
                        int remainLength;

                        result = Math.Sqrt(Convert.ToDouble(operand));
                        // split between integer part and fractional part
                        parts = result.ToString().Split('.');
                        // if integer part length is already break max output, return error
                        if (parts[0].Length > maxOutputSize)
                        {
                            return "E";
                        }
                        // calculate remaining space for fractional part.
                        remainLength = maxOutputSize - parts[0].Length - 1;
                        // trim the fractional part gracefully. =
                        return result.ToString("N" + remainLength);
                    }
                case "1/x":
                    if (operand != "0")
                    {
                        double result;
                        string[] parts;
                        int remainLength;

                        result = (1.0 / Convert.ToDouble(operand));
                        // split between integer part and fractional part
                        parts = result.ToString().Split('.');
                        // if integer part length is already break max output, return error
                        if (parts[0].Length > maxOutputSize)
                        {
                            return "E";
                        }
                        // calculate remaining space for fractional part.
                        remainLength = maxOutputSize - parts[0].Length - 1;
                        // trim the fractional part gracefully. =
                        return result.ToString("N" + remainLength);
                    }
                    break;
            }
            return "E";
        }

        public string calculate(string operate, string firstOperand, string secondOperand, int maxOutputSize = 8)
        {
            double result;
            switch (operate)
            {
                case "+":
                    return (Convert.ToDouble(firstOperand) + Convert.ToDouble(secondOperand)).ToString();
                case "-":
                    return (Convert.ToDouble(firstOperand) - Convert.ToDouble(secondOperand)).ToString();
                case "X":
                    return (Convert.ToDouble(firstOperand) * Convert.ToDouble(secondOperand)).ToString();
                case "÷":
                    // Not allow devide be zero
                    if (secondOperand != "0")
                    {
                        string[] parts;
                        int remainLength;

                        result = (Convert.ToDouble(firstOperand) / Convert.ToDouble(secondOperand));
                        // split between integer part and fractional part
                        parts = result.ToString().Split('.');
                        // if integer part length is already break max output, return error
                        if (parts[0].Length > maxOutputSize)
                        {
                            return "E";
                        }
                        // calculate remaining space for fractional part.
                        remainLength = maxOutputSize - parts[0].Length - 1;
                        // trim the fractional part gracefully. =
                        return result.ToString("N" + remainLength);
                    }
                    break;
                case "%":
                    //your code here

                    result = (Convert.ToDouble(secondOperand) / 100) * Convert.ToDouble(firstOperand);
                    return result.ToString();
                    break;
            }
            return "E";
        }
    }
}
