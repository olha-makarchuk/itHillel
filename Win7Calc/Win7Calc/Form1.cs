using System;
using System.Text;
using System.Windows.Forms;
using Win7Calc;

namespace Calculator
{
    public partial class CalculatorProject : Form
    {
        private ICalculate _calculate;
        private bool _isResultDisplayed = true;
        private double? _memory;
        private double? _inputNumberFirst;
        private double? _inputNumberSecond;

        public CalculatorProject()
        {
            InitializeComponent();
            Table.Text = "0";
            KeyPreview = true;
        }

        private void PerformOperation(ICalculate operation)
        {
            Calculate();
            _calculate = operation;
        }

        private void Calculate()
        {
            if (_isResultDisplayed) return;

            if (TryGetCurrentValue(Table.Text, out double currentValue))
            {
                if (_inputNumberFirst == null)
                {
                    _inputNumberFirst = currentValue;
                    Table.Text = _inputNumberFirst.ToString();
                }
                else if (_inputNumberSecond == null)
                {
                    _inputNumberSecond = currentValue;
                    try
                    {
                        _inputNumberFirst = _calculate.Calculate(_inputNumberFirst.Value, _inputNumberSecond.Value);
                    }
                    catch (DivideByZeroException)
                    {
                        MessageBox.Show("Cannot divide by zero!", "Error!");
                        ClearInput();
                        return;
                    }
                    Table.Text = _inputNumberFirst.ToString();
                    _inputNumberSecond = null;
                }
                _isResultDisplayed = true;
            }
        }

        private void ClearInput()
        {
            _isResultDisplayed = true;
            _inputNumberFirst = null;
            _inputNumberSecond = null;
            Table.Text = "0";
        }
        private void ButtonPlus_Click(object sender, EventArgs e)
        {
            PerformOperation(new Plus());
        }

        private void ButtonMinus_Click(object sender, EventArgs e)
        {
            PerformOperation(new Minus());
        }

        private void ButtonMultiply_Click(object sender, EventArgs e)
        {
            PerformOperation(new Multiply());
        }

        private void ButtonDegree_Click(object sender, EventArgs e)
        {
            PerformOperation(new Degree());
        }

        private void ButtonEquels_Click(object sender, EventArgs e)
        {
            Calculate();
        }

        private void ButtonNum_Click(object sender, EventArgs e)
        {
            if (_isResultDisplayed)
            {
                _isResultDisplayed = false;
                Table.Text = "";
            }
            if (sender is Button button)
            {
                Table.Text += button.Text == "," && !Table.Text.Contains(",") ? button.Text : button.Text;
            }
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            if (!_isResultDisplayed && Table.Text.Length != 0)
            {
                Table.Text = Table.Text.Remove(Table.Text.Length - 1, 1);
            }
        }

        private void ButtonClearC_Click(object sender, EventArgs e)
        {
            ClearInput();
        }

        private void ButtonClearCE_Click(object sender, EventArgs e)
        {
            _isResultDisplayed = true;
            Table.Text = "0";
        }

        private void ButtonInverse_Click(object sender, EventArgs e)
        {
            if (Table.Text.Length != 0)
            {
                Table.Text = Table.Text.StartsWith("-") ? Table.Text.Substring(1) : "-" + Table.Text;

                if (_isResultDisplayed)
                    _inputNumberFirst = -_inputNumberFirst;
            }
        }

        private void ButtonSqrt_Click(object sender, EventArgs e)
        {
            if (TryGetCurrentValue(Table.Text, out double currentValue))
            {
                Table.Text = Math.Sqrt(currentValue).ToString();
                _isResultDisplayed = true;
            }
        }

        private void ButtonFraction_Click(object sender, EventArgs e)
        {
            if (TryGetCurrentValue(Table.Text, out double currentValue))
            {
                Table.Text = (1 / currentValue).ToString();
                _isResultDisplayed = true;
            }
        }

        private void ButtonDot_Click(object sender, EventArgs e)
        {
            if (_isResultDisplayed)
            {
                ButtonClearC_Click(sender, e);
                _isResultDisplayed = false;
            }
            ButtonNum_Click(sender, e);
        }

        private void buttonMS_Click(object sender, EventArgs e)
        {
            if (TryGetCurrentValue(Table.Text, out double currentValue) && Table.Text != "")
            {
                _memory = currentValue;
                buttonsMC_MR_Enabled(true);
            }
        }
        
        private void buttonMC_Click(object sender, EventArgs e)
        {
            _memory = null;
            buttonsMC_MR_Enabled(false);
        }

        private void buttonMR_Click(object sender, EventArgs e)
        {
            if (_memory != null)
                Table.Text = _memory.ToString();
        }

        private void buttonMPlus_Click(object sender, EventArgs e)
        {
            if (_memory != null)
                _memory += double.Parse(Table.Text);
            else buttonMS_Click(sender, e);
        }

        private void buttonMMinus_Click(object sender, EventArgs e)
        {
            if (_memory != null)
                _memory -= double.Parse(Table.Text);
            else buttonMS_Click(sender, e);
        }

        public void buttonsMC_MR_Enabled(bool isEnable)
        {
            buttonMC.Enabled = isEnable;
            buttonMR.Enabled = isEnable;
        }

        private void Calc_KeyPress(object sender, KeyPressEventArgs e)
        {
            var key = e.KeyChar;

            switch (key)
            {
                case var digit when digit >= '0' && digit <= '9':
                    if (_isResultDisplayed)
                    {
                        _isResultDisplayed = false;
                        Table.Text = "";
                    }
                    Table.Text += digit;
                    break;
                case '-':
                    PerformOperation(new Minus());
                    break;
                case '+':
                    PerformOperation(new Plus());
                    break;
                case '*':
                    PerformOperation(new Multiply());
                    break;
                case '/':
                    PerformOperation(new Degree());
                    break;
                case '=':
                    Calculate();
                    break;
                case ',':
                    if (!Table.Text.Contains(","))
                        Table.Text += key;
                    break;
                case (char)8:
                    if (!_isResultDisplayed && Table.Text.Length != 0)
                    {
                        Table.Text = Table.Text.Remove(Table.Text.Length - 1, 1);
                    }
                    break;
            }
        }

        private bool TryGetCurrentValue(string input, out double value)
        {
            value = 0;
            if (double.TryParse(input, out value))
                return true;
            return false;
        }

        private void ButtonPercent_Click(object sender, EventArgs e)
        {

        }
    }
}