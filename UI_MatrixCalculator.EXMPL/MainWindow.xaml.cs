using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using UI_MatrixCalculator.EXMPL.GUInterface;
using UI_MatrixCalculator.EXMPL.Windows;
using Matrix = UI_MatrixCalculator.EXMPL.Objects.Matrix;

namespace UI_MatrixCalculator.EXMPL
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Matrix = new List<Matrix>();
            _number = new List<int>();
            InitializeComponent();
        }

        public readonly List<Matrix> Matrix;
        private List<int> _number;
        public void CreateMatrix(object sender, RoutedEventArgs e) {
            new Constructor(this, int.Parse((sender as Button)!.Name.Split("_")[2])).Show();
        }
        public void ChosenType(object sender, SelectionChangedEventArgs selectionChangedEventArgs) {
            try {
                var comboBox = sender as ComboBox; // i
                var button = new Button();
                var text = new TextBox();
                foreach (var elem in (ParentGrid.Children[^1] as Grid)!.Children) {
                    var temp = new Button();
                    var secondTemp = new TextBox();
                    
                    if (elem.GetType() == typeof(Button)) {
                        temp = elem as Button;
                    } 
                    else if (elem.GetType() == typeof(TextBox)) {
                        secondTemp = elem as TextBox;
                    }
                
                    if ((temp ?? new Button()).Name == $"button_1_{comboBox!.Name.Split("_")[1]}") {
                        button = temp;
                    }
                    if ((secondTemp ?? new TextBox()).Name == $"numSetter_{comboBox!.Name.Split("_")[1]}") {
                        text = secondTemp;
                    }
                }

                switch (comboBox!.Text) {
                    case "матрица":
                        button!.Visibility = Visibility.Hidden;
                        text!.Visibility = Visibility.Visible;
                        break;
                    case "число":
                        button!.Visibility = Visibility.Visible;
                        text!.Visibility = Visibility.Hidden;
                        break;
                }
            }
            catch (Exception e) {
                MessageBox.Show($"{e}");
                throw;
            }
        }

        private int _size;
        public void ExtendEquation(object sender, RoutedEventArgs e) {

            ParentGrid.Children.Add(GetEquation.GetEquationPart(++_size, this));
            
        }
           
        public void DeletePart(object sender, RoutedEventArgs e) =>
            ParentGrid.Children.Add(GetEquation.GetEquationPart(--_size, this));

        private void ResolveEquation(object sender, RoutedEventArgs e)
        {
            //_number = ReturnEquation.NumsFromGrid(ParentGrid.Children[^1] as Grid);
            var matrix = Matrix[0];
            //var number = _number[0];
            var count = 0;
            var secCount = 0;
            try
            {
                foreach (var element in (ParentGrid.Children[^1] as Grid)!.Children)
                {
                    if (element.GetType() == typeof(ComboBox))
                    {
                        switch ((element as ComboBox)!.Text)
                        {
                            case "+":
                                //if (element.GetType() == typeof(Button)) {
                                    matrix += Matrix[++count];
                                //}
                                if (element.GetType() == typeof(TextBox))
                                {
                                
                                }
                                break;
                        
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show($"{exception}");
                throw;
            }
            

            Answer.Content = $"{matrix.Print()}";
        }
    }
}