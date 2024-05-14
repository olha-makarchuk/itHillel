using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using WPFCalculator.Model;

namespace WPFCalculator
{
    public partial class MainWindow : Window
    {
        private ICalculate _calculate;
        private bool _isResultDisplayed = true;
        private double? _memory;
        private double? _inputNumberFirst;
        private double? _inputNumberSecond;

        public MainWindow()
        {
            InitializeComponent();
            ResultTextBox.Text = "0";
        }

        private void PerformOperation(ICalculate operation)
        {
            Calculate();
            _calculate = operation;
        }

        private bool TryGetCurrentValue(string input, out double value)
        {
            value = 0;
            if (double.TryParse(input, out value))
                return true;
            return false;
        }

        private void ClearInput()
        {
            _isResultDisplayed = true;
            _inputNumberFirst = null;
            _inputNumberSecond = null;
        }

        private void Calculate()
        {
            if (_isResultDisplayed) return;

            if (TryGetCurrentValue(ResultTextBox.Text, out double currentValue))
            {
                if (_inputNumberFirst == null)
                {
                    _inputNumberFirst = currentValue;
                    ResultTextBox.Text = _inputNumberFirst.ToString();
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
                    ResultTextBox.Text = _inputNumberFirst.ToString();
                    _inputNumberSecond = null;
                }
                _isResultDisplayed = true;
            }
        }

        private void PlusButton_Click(object sender, RoutedEventArgs e)
        {
            PerformOperation(new Plus());
        }

        private void MinusButton_Click(object sender, RoutedEventArgs e)
        {
            PerformOperation(new Minus());
        }

        private void MultiplicationButton_Click(object sender, RoutedEventArgs e)
        {
            PerformOperation(new Multiply());
        }

        private void DigitButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isResultDisplayed)
            {
                _isResultDisplayed = false;
                ResultTextBox.Text = "";
            }
            if (sender is Button button)
            {
                ResultTextBox.Text += button.Content.ToString() == "," && !ResultTextBox.Text.Contains(",") ? button.Content.ToString() : button.Content.ToString();
            }
        }

        private void FractionButton_Click(object sender, RoutedEventArgs e)
        {
            if (TryGetCurrentValue(ResultTextBox.Text, out double currentValue))
            {
                ResultTextBox.Text = (1 / currentValue).ToString();
                _isResultDisplayed = true;
            }
        }



        private void SquareButton_Click(object sender, RoutedEventArgs e)
        {
            if (TryGetCurrentValue(ResultTextBox.Text, out double currentValue))
            {
                ResultTextBox.Text = Math.Sqrt(currentValue).ToString();
                _isResultDisplayed = true;
            }
        }

        private void PlusMinusButton_Click(object sender, RoutedEventArgs e)
        {
            if (ResultTextBox.Text.Length != 0)
            {
                ResultTextBox.Text = ResultTextBox.Text.StartsWith("-") ? ResultTextBox.Text.Substring(1) : "-" + ResultTextBox.Text;

                if (_isResultDisplayed)
                    _inputNumberFirst = -_inputNumberFirst;
            }
        }

        private void CEButton_Click(object sender, RoutedEventArgs e)
        {
            _isResultDisplayed = true;
            ResultTextBox.Text = "0";
        }

        private void BackspaceButton_Click(object sender, RoutedEventArgs e)
        {
            if (!_isResultDisplayed && ResultTextBox.Text.Length != 0)
            {
                ResultTextBox.Text = ResultTextBox.Text.Remove(ResultTextBox.Text.Length - 1, 1);
            }
        }

        private void CButton_Click(object sender, RoutedEventArgs e)
        {
            ClearInput();
        }

        private void MCButton_Click(object sender, RoutedEventArgs e)
        {
            _memory = null;
            buttonsMC_MR_Enabled(false);
        }

        private void MRButton_Click(object sender, RoutedEventArgs e)
        {
            if (_memory != null)
                ResultTextBox.Text = _memory.ToString();
        }

        private void MMinusButton_Click(object sender, RoutedEventArgs e)
        {
            if (_memory != null)
                _memory -= double.Parse(ResultTextBox.Text);
            else MSButton_Click(sender, e);
        }

        private void EquelsPlusButton_Click(object sender, RoutedEventArgs e)
        {
            Calculate();
        }

        private void MPlusButton_Click(object sender, RoutedEventArgs e)
        {
            if (_memory != null)
                _memory += double.Parse(ResultTextBox.Text);
            else MSButton_Click(sender, e);
        }

        private void MSButton_Click(object sender, RoutedEventArgs e)
        {
            if (TryGetCurrentValue(ResultTextBox.Text, out double currentValue) && ResultTextBox.Text != "")
            {
                _memory = currentValue;
                buttonsMC_MR_Enabled(true);
            }
        }

        private void DivisionButton_Click(object sender, RoutedEventArgs e)
        {
            PerformOperation(new Degree());
        }

        private void PercentButton_Click(object sender, RoutedEventArgs e)
        {
            if (TryGetCurrentValue(ResultTextBox.Text, out double currentValue))
            {
                double result = currentValue / 100.0;
                ResultTextBox.Text = result.ToString();
                _isResultDisplayed = true;
            }
        }

        public void buttonsMC_MR_Enabled(bool isEnable)
        {
            MCButton.IsEnabled = isEnable;
            MRButton.IsEnabled = isEnable;
        }

        private void Calc_KeyPress(object sender, KeyEventArgs e)
        {
            var key = e.Key;

            switch (key)
            {
                case Key.D0:
                case Key.D1:
                case Key.D2:
                case Key.D3:
                case Key.D4:
                case Key.D5:
                case Key.D6:
                case Key.D7:
                case Key.D8:
                case Key.D9:
                    if (_isResultDisplayed)
                    {
                        _isResultDisplayed = false;
                        ResultTextBox.Text = "";
                    }
                    ResultTextBox.Text += key.ToString().Substring(1);
                    break;
                case Key.Subtract:
                    PerformOperation(new Minus());
                    break;
                case Key.Add:
                    PerformOperation(new Plus());
                    break;
                case Key.Multiply:
                    PerformOperation(new Multiply());
                    break;
                case Key.Divide:
                    PerformOperation(new Degree());
                    break;
                case Key.Enter:
                    Calculate();
                    break;
                case Key.Decimal:
                    if (!ResultTextBox.Text.Contains(","))
                        ResultTextBox.Text += ",";
                    break;
                case Key.Back:
                    if (!_isResultDisplayed && ResultTextBox.Text.Length != 0)
                    {
                        ResultTextBox.Text = ResultTextBox.Text.Remove(ResultTextBox.Text.Length - 1, 1);
                    }
                    break;
            }
        }

    }
}