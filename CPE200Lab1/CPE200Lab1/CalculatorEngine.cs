using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPE200Lab1
{
    class CalculatorEngine
    {

        public string Calculate(string operate, string firstOperand, string secondOperand, int maxOutputSize = 8)
        {
            double result;
            switch (operate)
            {
                case "+":
                    return (Convert.ToDouble(firstOperand) + Convert.ToDouble(secondOperand)).ToString();
                case "-":
                    result = Convert.ToDouble(firstOperand) - Convert.ToDouble(secondOperand);
                    if (result.ToString().Length >= 8) return result.ToString("0.000000");
                    else return result.ToString();
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
