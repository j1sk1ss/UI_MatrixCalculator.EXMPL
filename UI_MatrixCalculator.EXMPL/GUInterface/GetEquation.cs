using System.Windows;
using System.Windows.Controls;

namespace UI_MatrixCalculator.EXMPL.GUInterface {
    public static class GetEquation {
        public static Grid GetEquationPart(int size, MainWindow mainWindow) { 
            mainWindow.ParentGrid.Children.Clear();
            var tempGrid = new Grid();
            
            const int mainIndent   = 155;
            const int secondIndent = 60;
            
            for (var i = 0; i < size; i++) {
                var cmb = new ComboBox() {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Name   = $"typeOperation_{i}",
                    Text   = "+",
                    Height = 30,
                    Width  = 40,
                    Margin = new Thickness(10 + mainIndent * i, 0, 0, 300),
                    Items  = { "+", "-", "*", "/" }
                };
                
                tempGrid.Children.Add(cmb);
                
                var tempCombobox = new ComboBox() {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Name   = $"combo_{i}",
                    Height = 25,
                    Width  = 100,
                    Margin = new Thickness(secondIndent + mainIndent * i, 0, 0, 300),
                    Items  = { "матрица", "число" }
                };
                
                tempCombobox.SelectionChanged += mainWindow.ChosenType;
                tempGrid.Children.Add(tempCombobox);
                
                var button = new Button() {
                    Visibility          = Visibility.Hidden,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Margin  = new Thickness(secondIndent + mainIndent * i, 0, 0, 230),
                    Name    = $"button_1_{i}",
                    Content = "Задать матрицу",
                    Height  = 25,
                    Width   = 100
                };
                button.Click += mainWindow.CreateMatrix;
                tempGrid.Children.Add(button);

                tempGrid.Children.Add(new TextBox() {
                    Visibility          = Visibility.Hidden,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Name   = $"numSetter_{i}",
                    Margin = new Thickness(secondIndent + mainIndent * i, 0, 0, 230),
                    Height = 25,
                    Width  = 100
                });
                
                tempGrid.Children.Add(new CheckBox() {
                    Content = "^",
                    HorizontalAlignment = HorizontalAlignment.Left,
                    FontSize = 10,
                    Name     = $"CheckBox_{i}",
                    Margin   = new Thickness(secondIndent + mainIndent * i, -125, 0, 230),
                    Height   = 40,
                    Width    = 40
                });
                
                tempGrid.Children.Add(new TextBox() {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    FontSize = 10,
                    Name     = $"Degree_{i}",
                    Margin   = new Thickness(40 + secondIndent + mainIndent * i, -145, 0, 230),
                    Height   = 20,
                    Width    = 40
                });
                
                tempGrid.Children.Add(new Label() {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    FontSize = 15,
                    Name     = $"Label_{i}",
                    Margin   = new Thickness(secondIndent + mainIndent * i, 120, 0, 230),
                    Height   = 100,
                    Width    = 100
                });
            }
            
            var extendButton = new Button() {
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin  = new Thickness(secondIndent + mainIndent * size, 45, 0, 0),
                Content = "+",
                Height  = 30,
                Width   = 30
            };
            extendButton.Click += mainWindow.ExtendEquation;
            tempGrid.Children.Add(extendButton);
            
            var decreaseButton = new Button() {
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin  = new Thickness(secondIndent + mainIndent * size, 80, 0, 0),
                Content = "-",
                Height  = 30,
                Width   = 30
            };
            decreaseButton.Click += mainWindow.DeletePart;
            tempGrid.Children.Add(decreaseButton);
            
            return tempGrid;
        }
    }
}