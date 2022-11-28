using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using UI_MatrixCalculator.EXMPL.GUInterface;
using UI_MatrixCalculator.EXMPL.Windows;
using Matrix = UI_MatrixCalculator.EXMPL.Objects.Matrix;

namespace UI_MatrixCalculator.EXMPL {
    public partial class MainWindow {
        public MainWindow() {
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
            var comboBox = sender as ComboBox; 
            Button button    = null;
            TextBox text     = null;

            var equationGrid = ParentGrid.Children[^1] as Grid;
            
            foreach (var elem in equationGrid!.Children) {
                if (elem.GetType() == typeof(Button) && button == null) {
                    button = (elem as Button)!.Name == $"button_1_{comboBox!.Name.Split("_")[1]}"
                        ? elem as Button
                        : null;
                } 
                else if (elem.GetType() == typeof(TextBox) && text == null) {
                    text = (elem as TextBox)!.Name == $"numSetter_{comboBox!.Name.Split("_")[1]}"
                        ? elem as TextBox
                        : null;
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

        private int _size;
        public void ExtendEquation(object sender, RoutedEventArgs e) {
            ParentGrid.Children.Clear();
            
            ParentGrid.Children.Add(GetEquation.GetEquationPart(++_size, this));
        }
        public void DeletePart(object sender, RoutedEventArgs e) {
            ParentGrid.Children.Clear();
            Matrix.Clear();
            _number.Clear();
            
            ParentGrid.Children.Add(GetEquation.GetEquationPart(--_size, this));
        }
        private void ResolveEquation(object sender, RoutedEventArgs e) {
            try {
                var tempGrid = ParentGrid.Children[^1] as Grid;
                _number      = ReturnEquation.NumsFromGrid(tempGrid) ?? new List<int>();
                
                var matrix = Matrix[0];

                var numPos    = 0;
                var matrixPos = 0;
                
                for (var i = 5; i < tempGrid!.Children.Count; i += 5) {
                    if (tempGrid!.Children.Count - i < 5) break;
                    if (tempGrid!.Children[i + 2].GetType() != typeof(Button)) continue;
                    
                    if ((tempGrid!.Children[i + 2] as Button)!.Visibility == Visibility.Visible) {
                        matrix = (tempGrid!.Children[i] as ComboBox)!.Text switch {
                            "+" => matrix + Matrix[++matrixPos],
                            "-" => matrix - Matrix[++matrixPos],
                            "*" => matrix * Matrix[++matrixPos],
                            _   => matrix + Matrix[++matrixPos]
                        };
                    }
                    else {
                        matrix = (tempGrid!.Children[i] as ComboBox)!.Text switch {
                            "+" => matrix + _number[numPos++],
                            "-" => matrix - _number[numPos++],
                            "*" => matrix * _number[numPos++],
                            "/" => matrix / _number[numPos++],
                            _   => matrix + _number[numPos++]
                        };
                    }
                }
                Answer.Content = $"{matrix.Print()}";
            }
            catch (Exception exception) {
                MessageBox.Show($"{exception}");
            }
        }
    }
}