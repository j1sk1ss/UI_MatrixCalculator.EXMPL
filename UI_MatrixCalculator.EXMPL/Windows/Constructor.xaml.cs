using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using UI_MatrixCalculator.EXMPL.GUInterface;
using UI_MatrixCalculator.EXMPL.Objects;

namespace UI_MatrixCalculator.EXMPL.Windows {
    public partial class Constructor {
        public Constructor(MainWindow mainWindow, int position) {
            InitializeComponent();
            SetInterfaceMatrix(2,2);
            
            MainWindow = mainWindow;
             _position = position;
        }
        private MainWindow MainWindow { get; set; }
        private int _xSize = 2;
        private int _ySize = 2;
        private readonly int _position;
        public void IncreaseXSize(object sender, RoutedEventArgs routedEventArgs) => SetInterfaceMatrix(++_xSize, _ySize);
        public void IncreaseYSize(object sender, RoutedEventArgs routedEventArgs) => SetInterfaceMatrix(_xSize, ++_ySize);
        public void DecreaseXSize(object sender, RoutedEventArgs routedEventArgs) => SetInterfaceMatrix(--_xSize, _ySize);
        public void DecreaseYSize(object sender, RoutedEventArgs routedEventArgs) => SetInterfaceMatrix(_xSize, --_ySize);
        private void SetInterfaceMatrix(int x, int y) {
            if (y < 2) {
                _ySize = 2;
                return;
            }

            if (x < 2) {
                _xSize = 2;
                return;
            }
            
            if (y > 5) {
                _ySize = 5;
                return;
            }

            if (x > 5) {
                _xSize = 5;
                return;
            }
            
            MatrixParent.Children.Clear();
            MatrixParent.Children.Add(GetMatrixGui.GetMatrix(x, y, this));
        }
        private void SaveMatrix(object sender, EventArgs e) {
            try {
                
                var temp     = new double[_xSize, _ySize];
                var count    = 0;

                var matrixGrid = MatrixParent.Children[^1] as Grid;
                
                for (var i = _xSize - 1; i >= 0; i--) {
                    for (var j = _ySize - 1; j >= 0; j--) {
                        if (double.TryParse((matrixGrid!.Children[^++count] as TextBox)!.Text, out var tempDouble)) {
                            temp[i, j] = tempDouble;
                        }
                        else {
                            MessageBox.Show($"Неккоректное значение в матрице в {i};{j}");
                            temp[i, j] = 0;
                        }
                    }
                }
                AddToLabel(temp);
            }
            catch (Exception exception) {
                MessageBox.Show($"{exception}", "Ошибка с сохранением!");
            }
        }
        private void AddToLabel(double[,] temp) {
            var parentGrid = MainWindow.ParentGrid.Children[^1] as Grid;     
            var matrix = new Matrix(temp);
                
            foreach (var element in parentGrid!.Children) {
                if (element.GetType() != typeof(ScrollViewer)) continue;
                var label = (element as ScrollViewer)!.Content as Label;
                if (label!.Name == $"Label_{_position}") {
                    label!.Content = matrix.Print();
                }
            }
            
            MainWindow.Matrix[_position] = matrix;
            Close();
        }
        private void SetFromFile(object sender, RoutedEventArgs e) {
            var matrixFromFile = new OpenFileDialog();
            var filePath = "";
            if (matrixFromFile.ShowDialog() == true) filePath = matrixFromFile.FileName;
            
            if (!File.Exists(filePath)) return;

            using var streamReader = new StreamReader(filePath);
            var text = streamReader.ReadToEnd();

            var lines = text.Split("\n");

            var tempMatrix = new double[lines.Length, lines[0].Split(" ").Length];

            for (var i = 0; i < lines.Length; i++) {
                var symbols = lines[i].Split(" ");
                for (var j = 0; j < symbols.Length; j++)
                    if (int.TryParse(symbols[j], out var k)) tempMatrix[i, j] = k;
            }

            AddToLabel(tempMatrix);
        }
    }
}