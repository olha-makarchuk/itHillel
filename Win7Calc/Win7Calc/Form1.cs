using System;
using System.Text;
using System.Windows.Forms;
using Win7Calc;

internal enum Doing
{
    Plus,
    Minus,
    Degree,
    Multyple,
    Equels,
    Sqrt
}

namespace Calculator
{
    public partial class Calc : Form
    {
        private ICalculate _calculate;
        private bool _isShowResult = true;
        private double? _memory;
        private double? _number1;
        private double? _number2;

        public Calc()
        {
            InitializeComponent();
            Table.Text = 0.ToString();
            KeyPreview = true;
        }

        private void Culculate()
        {
            if (_isShowResult)
            {
                return;
            }
            if (_number1 == null)
            {
                _number1 = double.Parse(Table.Text);
                Table.Text = _number1.ToString();
                _isShowResult = true;
            }
            else if (_number2 == null)
            {
                _number2 = double.Parse(Table.Text);
                try
                {
                    _number1 = _calculate.Calculate(_number1.Value, _number2.Value);
                }
                catch (DivideByZeroException ex)
                {
                    MessageBox.Show("Делить на 0 нельзя!", "Ошибка!");
                    ButtonClearC_Click(null, null);
                    return;
                }
                Table.Text = _number1.ToString();
                _isShowResult = true;
                _number2 = null;
            }
        }

        private void ButtonPlus_Click(object sender, EventArgs e)
        {
            Culculate();
            _calculate = new Plus();
        }

        private void ButtonMinus_Click(object sender, EventArgs e)
        {
            Culculate();
            _calculate = new Minus();
        }

        private void ButtonMultiply_Click(object sender, EventArgs e)
        {
            Culculate();
            _calculate = new Multiply();
        }

        private void ButtonDegree_Click(object sender, EventArgs e)
        {
            Culculate();
            _calculate = new Degree();
        }

        private void ButtonNum_Click(object sender, EventArgs e)
        {
            if (_isShowResult)
            {
                _isShowResult = false;
                Table.Text = "";
            }
            if ((sender as Button).Text == ",")
            {
                if (Table.Text.Contains(",") == false)
                    Table.Text += (sender as Button).Text;
            }
            else
            {
                Table.Text += (sender as Button).Text;
            }
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            if (!_isShowResult && Table.Text.Length != 0)
            {
                var TText = new StringBuilder(Table.Text);
                TText.Remove(TText.Length - 1, 1);
                Table.Text = TText.ToString();
            }
        }

        private void ButtonClearC_Click(object sender, EventArgs e)
        {
            _isShowResult = true;
            _number1 = null;
            _number2 = null;
            Table.Text = "0";
        }

        private void ButtonClearCE_Click(object sender, EventArgs e)
        {
            _isShowResult = true;
            Table.Text = "0";
        }

        private void ButtonInverse_Click(object sender, EventArgs e)
        {
            if (Table.Text.Length != 0)
            {
                if (Table.Text[0] != '-')
                    Table.Text = Table.Text.Insert(0, "-");
                else
                    Table.Text = Table.Text.Remove(0, 1);

                if (_isShowResult)
                    _number1 = -_number1;
            }
        }

        private void ButtonSqrt_Click(object sender, EventArgs e)
        {
            Table.Text = Math.Sqrt(double.Parse(Table.Text)).ToString();
            _isShowResult = true;
        }

        private void ButtonFraction_Click(object sender, EventArgs e)
        {
            Table.Text = (1 / double.Parse(Table.Text)).ToString();
            _isShowResult = true;
        }

        private void ButtonEquels_Click(object sender, EventArgs e)
        {
            Culculate();
        }

        private void ButtonDot_Click(object sender, EventArgs e)
        {
            if (_isShowResult)
            {
                ButtonClearC_Click(sender, e);
                _isShowResult = false;
            }
            ButtonNum_Click(sender, e);
        }

        private void buttonMS_Click(object sender, EventArgs e)
        {
            if (Table.Text != "")
            {
                _memory = double.Parse(Table.Text);
                buttonMC.Enabled = true;
                buttonMR.Enabled = true;
            }
        }

        private void buttonMC_Click(object sender, EventArgs e)
        {
            _memory = null;
            buttonMC.Enabled = false;
            buttonMR.Enabled = false;
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

        private void Calc_KeyPress(object sender, KeyPressEventArgs e)
        {
            Text = string.Format("{0}", (int) e.KeyChar);
            var key = e.KeyChar;
            if (key >= '0' && key <= '9')
            {
                if (_isShowResult)
                {
                    _isShowResult = false;
                    Table.Text = "";
                }
                Table.Text += key;
            }
            else if (key == '-')
            {
                Culculate();
                _calculate = new Minus();
            }
            else if (key == '+')
            {
                Culculate();
                _calculate = new Plus();
            }
            else if (key == '*')
            {
                Culculate();
                _calculate = new Multiply();
            }
            else if (key == '/')
            {
                Culculate();
                _calculate = new Degree();
            }
            else if (key == '=')
            {
                Culculate();
            }
            else if (key == ',')
            {
                if (Table.Text.Contains(",") == false)
                    Table.Text += key;
            }
            else if (key == 8)
            {
                if (!_isShowResult && Table.Text.Length != 0)
                {
                    var TText = new StringBuilder(Table.Text);
                    TText.Remove(TText.Length - 1, 1);
                    Table.Text = TText.ToString();
                }
            }
        }
    }
}