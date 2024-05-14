using System.Windows;
using System.Windows.Documents;
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

        private void DigitButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PlusButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MinusButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FractionButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PersentButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SquareButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PlusMinusButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BackspaceButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MCButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MRButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MMinusButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CEButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EquelsPlusButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MultiplicationButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MPlusButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MSButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DivisionButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ResultTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }
    }
}