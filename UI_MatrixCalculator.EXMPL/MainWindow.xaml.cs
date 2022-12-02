using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using UI_MatrixCalculator.EXMPL.GUInterface;
using UI_MatrixCalculator.EXMPL.Windows;
using Matrix = UI_MatrixCalculator.EXMPL.Objects.Matrix;

namespace UI_MatrixCalculator.EXMPL {
    public partial class MainWindow {
        public MainWindow() {
            Matrix  = new List<Matrix>();
            _number = new List<double>();
            InitializeComponent();
        }

        public readonly List<Matrix> Matrix;
        private List<double> _number;
        public void CreateMatrix(object sender, RoutedEventArgs e) {
            new Constructor(this, int.Parse((sender as Button)!.Name.Split("_")[2])).Show();
        }
        public void ChosenType(object sender, SelectionChangedEventArgs selectionChangedEventArgs) {
            
            var comboBox  = sender as ComboBox; 
            Button button = null;
            TextBox text  = null;

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
                    text!.Visibility   = Visibility.Visible;
                    break;
                case "число":
                    button!.Visibility = Visibility.Visible;
                    text!.Visibility   = Visibility.Hidden;
                    break;
                default:
                    button!.Visibility = Visibility.Visible;
                    text!.Visibility   = Visibility.Hidden;
                    break;
            }
        }

        private int _size;
        public void ExtendEquation(object sender, RoutedEventArgs e) {
            ParentGrid.Children.Clear();
            Matrix.Clear();
            _number.Clear();
            
            ParentGrid.Children.Add(GetEquation.GetEquationPart(++_size, this));
            
            for (var i = 0; i < _size; i++) {
                Matrix.Add(new Matrix(null));
            }
        }
        public void DeletePart(object sender, RoutedEventArgs e) {
            if (_size == 0) return;
            
            ParentGrid.Children.Clear();
            Matrix.Clear();
            _number.Clear();

            ParentGrid.Children.Add(GetEquation.GetEquationPart(_size, this));
        }
        private void ResolveEquation(object sender, RoutedEventArgs e) {
            try {
                var tempGrid = ParentGrid.Children[^1] as Grid;
                _number      = ReturnEquation.NumsFromGrid(tempGrid) ?? new List<double>();
                
                var matrix = new Matrix((double[,])Matrix[0].Body.Clone()) * 
                             (tempGrid!.Children[0] as ComboBox)!.Text switch {
                                "+" => 1,
                                "-" => -1,
                                _   => 1
                            };

                if ((tempGrid!.Children[4] as CheckBox)!.IsChecked == true) {
                    matrix = matrix.Pow(int.Parse((tempGrid!.Children[5] as TextBox)!.Text));
                }

                var numPos    = 0;
                var matrixPos = 0;

                const int countOfElements = 7;
                
                for (var i = countOfElements; i < tempGrid!.Children.Count; i += countOfElements) {
                    if (tempGrid!.Children.Count - i < countOfElements) break;
                    if (tempGrid!.Children[i + 2].GetType() != typeof(Button)) continue;

                    if ((tempGrid!.Children[i + 2] as Button)!.Visibility == Visibility.Visible) {
                        var tempMatrix = Matrix[++matrixPos];
                        if (tempMatrix.Body == null) continue;
                        var nextMatrix = new Matrix((double[,])tempMatrix.Body.Clone());

                        if ((tempGrid!.Children[i + 4] as CheckBox)!.IsChecked == true) {
                            nextMatrix = nextMatrix.Pow(int.Parse((tempGrid!.Children[i + 5] as TextBox)!.Text));
                        }
                        
                        matrix = (tempGrid!.Children[i] as ComboBox)!.Text switch {
                            "+" => matrix + nextMatrix,
                            "-" => matrix - nextMatrix,
                            "*" => matrix * nextMatrix,
                            _   => matrix + nextMatrix
                        };
                    }
                    else {
                        var nextNumber = _number[numPos++];
                        if ((tempGrid!.Children[i + 4] as CheckBox)!.IsChecked == true) {
                            nextNumber = Math.Pow(nextNumber, int.Parse((tempGrid!.Children[i + 5] as TextBox)!.Text));
                        }
                        
                        matrix = (tempGrid!.Children[i] as ComboBox)!.Text switch {
                            "+" => matrix + nextNumber,
                            "-" => matrix - nextNumber,
                            "*" => matrix * nextNumber,
                            "/" => matrix / nextNumber,
                            _   => matrix + nextNumber
                        };
                    }
                }
                Answer.Content = $"Ответ:\n{matrix.Print()}";
            }
            catch (Exception exception) {
                MessageBox.Show($"{exception}");
            }
        }
        private void SendToBrowser(object sender, RequestNavigateEventArgs e) {
            try {
                Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri){ UseShellExecute=true});
                e.Handled = true;
            }
            catch (Exception exception) {
                MessageBox.Show($"{exception}");
            }
        }
        private void CopyToClipBoard(object sender, RoutedEventArgs e) => Clipboard.SetText(Answer.Content + "\n");
    }
}