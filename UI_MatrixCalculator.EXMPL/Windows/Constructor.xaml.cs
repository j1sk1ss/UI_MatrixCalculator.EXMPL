using System;
using System.Windows;
using System.Windows.Controls;
using UI_MatrixCalculator.EXMPL.Objects;

namespace UI_MatrixCalculator.EXMPL.Windows {
    public partial class Constructor : Window {
        public Constructor(MainWindow mainWindow, int position) {
            InitializeComponent();
            UpdateInterface();
            
            MainWindow = mainWindow;
             _position = position;
        }
        private MainWindow MainWindow { get; set; }
        private int _xSize = 2;
        private int _ySize = 2;
        private readonly int _position;
        private void IncreaseXSize(object sender, RoutedEventArgs routedEventArgs) {
            _xSize++;
            UpdateInterface();
        }
        private void IncreaseYSize(object sender, RoutedEventArgs routedEventArgs) {
            _ySize++;
            UpdateInterface();
        }
        private void DecreaseXSize(object sender, RoutedEventArgs routedEventArgs) {
            if (_xSize > 2) _xSize--;
            UpdateInterface();
        }
        private void DecreaseYSize(object sender, RoutedEventArgs routedEventArgs) {
            if (_ySize > 2) _ySize--;
            UpdateInterface();
        }
        private void UpdateInterface() {
            MatrixParent.Children.Clear();
            var button = new Button() {
                Height = 20,
                Width  = 20,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment   = VerticalAlignment.Top,
                Margin  = new Thickness(_ySize * 25,0,0,0),
                Content = "+"
            };
            button.Click += IncreaseYSize;
            MatrixParent.Children.Add(button);
            
            button = new Button() {
                Height = 20,
                Width  = 20,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment   = VerticalAlignment.Top,
                Margin  = new Thickness(_ySize * 25,20,0,0),
                Content = "-"
            };
            button.Click += DecreaseYSize;
            MatrixParent.Children.Add(button);
            
            button = new Button() {
                Height = 20,
                Width  = 20,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment   = VerticalAlignment.Top,
                Margin  = new Thickness(0,_xSize * 25,0,0),
                Content = "+"
            };
            button.Click += IncreaseXSize;
            MatrixParent.Children.Add(button);
            
            button = new Button() {
                Height = 20,
                Width  = 20,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment   = VerticalAlignment.Top,
                Margin  = new Thickness(20,_xSize * 25,0,0),
                Content = "-"
            };
            button.Click += DecreaseXSize;
            MatrixParent.Children.Add(button);
            
            for (var i = 0; i < _xSize; i++) {
                for (var j = 0; j < _ySize; j++) {
                    MatrixParent.Children.Add(new TextBox() {
                        Height = 20,
                        Width  = 20,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment   = VerticalAlignment.Top,
                        Margin = new Thickness(j * 25,i * 25,0,0)
                    });
                }
            }
        }
        private void SaveMatrix(object sender, EventArgs e) {
            var temp     = new double[_xSize, _ySize];
            var count    = new int();
            for (var i = _xSize - 1; i >= 0; i--) {
                for (var j = _ySize - 1; j >= 0; j--) {
                    count++;
                    temp[i, j] = double.Parse((MatrixParent.Children[^(count + 1)] as TextBox)!.Text);
                }
            }

            var matrix = new Matrix(temp);
            foreach (var element in (MainWindow.ParentGrid.Children[^1] as Grid)!.Children) {
                if (element.GetType() != typeof(Label)) continue;
                var label = element as Label;
                if (label!.Name == $"Label_{_position}") {
                    label!.Content = matrix.Print();
                }
            }
            
            MainWindow.Matrix.Add(matrix);
        }
    }
}