using System;
using System.Windows;
using System.Windows.Controls;
using UI_MatrixCalculator.EXMPL.Objects;

namespace UI_MatrixCalculator.EXMPL.Windows {
    public partial class Constructor : Window {
        public Constructor(MainWindow mainWindow) {
            InitializeComponent();
            UpdateInterface();
            MainWindow = mainWindow;
        }
        private MainWindow MainWindow { get; set; }
        private int xSize = 2;
        private int ySize = 2;

        private void IncreaseXSize(object sender, RoutedEventArgs routedEventArgs) {
            xSize++;
            UpdateInterface();
        }
        private void IncreaseYSize(object sender, RoutedEventArgs routedEventArgs) {
            ySize++;
            UpdateInterface();
        }
        private void DecreaseXSize(object sender, RoutedEventArgs routedEventArgs) {
            if (xSize > 2) xSize--;
            UpdateInterface();
        }
        private void DecreaseYSize(object sender, RoutedEventArgs routedEventArgs) {
            if (ySize > 2) ySize--;
            UpdateInterface();
        }
        private void UpdateInterface() {
            MatrixParent.Children.Clear();
            var button = new Button() {
                Height = 20,
                Width = 20,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(xSize * 25,0,0,0),
                Content = "+"
            };
            button.Click += IncreaseXSize;
            MatrixParent.Children.Add(button);
            
            button = new Button() {
                Height = 20,
                Width = 20,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(xSize * 25,20,0,0),
                Content = "-"
            };
            button.Click += DecreaseXSize;
            MatrixParent.Children.Add(button);
            
            button = new Button() {
                Height = 20,
                Width = 20,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(0,ySize * 25,0,0),
                Content = "+"
            };
            button.Click += IncreaseYSize;
            MatrixParent.Children.Add(button);
            
            button = new Button() {
                Height = 20,
                Width = 20,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(20,ySize * 25,0,0),
                Content = "-"
            };
            button.Click += DecreaseYSize;
            MatrixParent.Children.Add(button);
            
            for (var i = 0; i < xSize; i++) {
                for (var j = 0; j < ySize; j++) {
                    MatrixParent.Children.Add(new TextBox() {
                        Height = 20,
                        Width = 20,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment = VerticalAlignment.Top,
                        Margin = new Thickness(i * 25,j * 25,0,0)
                    });
                }
            }
        }

        private void SaveMatrix(object sender, EventArgs e) {
            var temp = new double[xSize, ySize];
            for (var i = 0; i < xSize; i++) {
                for (var j = 0; j < ySize; j++) {
                    temp[i, j] = double.Parse((MatrixParent.Children[^1] as TextBox)!.Text);
                }
            }

            MainWindow.mainMatrix = new Matrix(temp);
            MainWindow.FirstMatrixView.Content = MainWindow.mainMatrix.Print();
        }
    }
}