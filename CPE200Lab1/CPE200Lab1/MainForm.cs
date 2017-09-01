using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CPE200Lab1
{
    public partial class MainForm : Form
    {
        private bool hasDot;
        private bool isAllowBack;
        private bool isAfterOperater;
        private bool isAfterEqual;
        private bool isAlreadyOperater;
        private string firstOperand;
        private string operate, lastOperate;

        private CalculatorEngine engine;

        private void resetAll()
        {
            lblDisplay.Text = "0";
            isAllowBack = true;
            hasDot = false;
            isAfterOperater = false;
            isAfterEqual = false;
            isAlreadyOperater = false;
            lastOperate = "";
        }

        public MainForm()
        {
            InitializeComponent();
            engine = new CalculatorEngine();
            resetAll();
        }

        private void btnNumber_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterEqual)
            {
                isAllowBack = true;
                isAfterOperater = true;
                isAfterEqual = false;
            }
            if (isAfterOperater)
            {
                lblDisplay.Text = "0";
            }
            if (lblDisplay.Text.Length is 8)
            {
                return;
            }
            isAllowBack = true;
            string digit = ((Button)sender).Text;
            if (lblDisplay.Text is "0")
            {
                lblDisplay.Text = "";
            }
            lblDisplay.Text += digit;
            isAfterOperater = false;
        }

        private void btnOperator_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterOperater)
            {
                return;
            }
            operate = ((Button)sender).Text;
            string secondOperand, result;
            switch (operate)
            {
                case "+":
                case "-":
                case "X":
                case "÷":
                    if (isAlreadyOperater)
                    {
                        secondOperand = lblDisplay.Text;
                        Console.WriteLine(firstOperand + " " + lastOperate + " " + secondOperand);
                        result = engine.Calculate(lastOperate, firstOperand, secondOperand);
                        if (result is "E" || result.Length > 8)
                        {
                            lblDisplay.Text = "Error";
                        }
                        else
                        {
                            lblDisplay.Text = result;
                        }
                        lastOperate = operate;
                        firstOperand = lblDisplay.Text;
                        isAfterOperater = true;
                        isAlreadyOperater = true;
                    }
                    else
                    {
                        Console.WriteLine("operate in stack is empty");
                        if (lblDisplay.Text != null)
                        {

                        }
                        lastOperate = operate;
                        firstOperand = lblDisplay.Text;
                        isAfterOperater = true;
                        isAlreadyOperater = true;
                    }
                    break;
                case "%":
                    secondOperand = lblDisplay.Text;
                    result = engine.Calculate(operate, firstOperand, secondOperand);
                    lblDisplay.Text = result;
                    break;
                case "sqrt":
                case "1/x":
                    firstOperand = lblDisplay.Text;
                    result = engine.Calculate(operate, firstOperand, "0");
                    lblDisplay.Text = result;
                    break;
            }
            isAllowBack = false;
            hasDot = false;
        }

        private void btnEqual_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            string secondOperand = lblDisplay.Text;
            Console.WriteLine(firstOperand + " " + operate + " " + secondOperand);
            string result = engine.Calculate(lastOperate, firstOperand, secondOperand);
            if (result is "E" || result.Length > 8)
            {
                lblDisplay.Text = "Error";
            }
            else
            {
                lblDisplay.Text = result;
            }
            isAfterEqual = true;
            isAlreadyOperater = false;
        }

        private void btnDot_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterEqual)
            {
                resetAll();
            }
            if (lblDisplay.Text.Length is 8)
            {
                return;
            }
            if (!hasDot)
            {
                lblDisplay.Text += ".";
                hasDot = true;
            }
        }

        private void btnSign_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterEqual)
            {
                resetAll();
            }
            // already contain negative sign
            if (lblDisplay.Text.Length is 8)
            {
                return;
            }
            if (lblDisplay.Text[0] is '-')
            {
                lblDisplay.Text = lblDisplay.Text.Substring(1, lblDisplay.Text.Length - 1);
            }
            else
            {
                lblDisplay.Text = "-" + lblDisplay.Text;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            resetAll();
        }

        private string memory;
        private void btnMemory_Click(object sender, EventArgs e)
        {

            string function = ((Button)sender).Text;
            Console.WriteLine(function);
            switch (function)
            {
                case "MC":
                    memory = null;
                    break;
                case "MR":
                    lblDisplay.Text = memory;
                    break;
                case "M+":
                    if(memory == null)
                        memory = lblDisplay.Text;
                    else
                    {
                        memory = engine.Calculate("+", memory, lblDisplay.Text);
                    }
                    break;
                case "M-":
                    if (memory == null)
                        memory = lblDisplay.Text;
                    else
                    {
                        memory = engine.Calculate("-", memory,lblDisplay.Text);
                    }
                    break;
                case "MS":
                    memory = lblDisplay.Text;
                    break;
            }
            Console.WriteLine(memory);
            if (memory != null)
            {
                btnMemClear.ForeColor = System.Drawing.Color.Black;
                btnMemRecall.ForeColor = System.Drawing.Color.Black;
            }
            else
            {
                btnMemClear.ForeColor = System.Drawing.Color.Silver;
                btnMemRecall.ForeColor = System.Drawing.Color.Silver;
            }
        }
        
        private void btnBack_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterEqual)
            {
                return;
            }
            if (!isAllowBack)
            {
                return;
            }
            if (lblDisplay.Text != "0")
            {
                string current = lblDisplay.Text;
                char rightMost = current[current.Length - 1];
                if (rightMost is '.')
                {
                    hasDot = false;
                }
                lblDisplay.Text = current.Substring(0, current.Length - 1);
                if (lblDisplay.Text is "" || lblDisplay.Text is "-")
                {
                    lblDisplay.Text = "0";
                }
            }
        }
    }
}
