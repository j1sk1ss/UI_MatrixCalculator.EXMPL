using System.Windows;
using System.Windows.Controls;
using UI_MatrixCalculator.EXMPL.Windows;
using Matrix = UI_MatrixCalculator.EXMPL.Objects.Matrix;

namespace UI_MatrixCalculator.EXMPL
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public Matrix mainMatrix;
        private Matrix secondMatrix;
        private int number;
        private void CreateMatrix(object sender, RoutedEventArgs e) {
            new Constructor(this).Show();
        }
        private void CreateSecondMatrix(object sender, RoutedEventArgs e)
        {
            secondMatrix = mainMatrix;
            FirstMatrixView.Content = mainMatrix.Print();
            new Constructor(this).Show();
        }
        private void ChoseSecondElement(object sender, SelectionChangedEventArgs selectionChangedEventArgs)
        {
            switch ((sender as ComboBox)!.Text)
            {
                case "матрица":
                    SetSecondMatrix.Visibility = Visibility.Hidden;
                    SetNumber.Visibility = Visibility.Visible;
                    break;
                case "число":
                    SetSecondMatrix.Visibility = Visibility.Visible;
                    SetNumber.Visibility = Visibility.Hidden;
                    break;
            }
        }

        private void GetAnswer(object sender, RoutedEventArgs e)
        {
            switch (SecondType.Text) 
            {
                case "матрица":
                    MatrixAnswer();
                    break;
                case "число":
                    NumAnswer();
                    break;
            }
        }

        private void MatrixAnswer() {
            Answer.Content = Type.Text switch {
                "*" => $"{(secondMatrix * mainMatrix).Print()}",
                "+" => $"{(secondMatrix + mainMatrix).Print()}",
                "-" => $"{(secondMatrix - mainMatrix).Print()}",
                _ => Answer.Content
            };
        }

        private void NumAnswer()
        {
            Answer.Content = Type.Text switch {
                "*" => $"{secondMatrix.Multiple(double.Parse(SetNumber.Text)).Print()}",
                "/" => $"{secondMatrix.Divide(double.Parse(SetNumber.Text)).Print()}",
                _ => Answer.Content
            };
        }
    }
}