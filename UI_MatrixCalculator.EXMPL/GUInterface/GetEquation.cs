﻿using System.Windows;
using System.Windows.Controls;

namespace UI_MatrixCalculator.EXMPL.GUInterface {
    public static class GetEquation {
        public static Grid GetEquationPart(int size, MainWindow mainWindow) { // Combobox -> Button -> TextBox -> Label -> ComboBox
            mainWindow.ParentGrid.Children.Clear();
            var tempGrid = new Grid();
            for (var i = 0; i < size; i++) {
                var tempCombobox = new ComboBox() {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Text = "число",
                    Name = $"combo_{i}",
                    Height = 25,
                    Width = 100,
                    Margin = new Thickness(155 * i, 0, 0, 300),
                    Items = { "матрица", "число" }
                };
                tempCombobox.SelectionChanged += mainWindow.ChosenType;
                tempGrid.Children.Add(tempCombobox);
                
                var button = new Button() {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Margin = new Thickness(155 * i, 0, 0, 230),
                    Name = $"button_1_{i}",
                    Content = "Задать матрицу",
                    Height = 25,
                    Width = 100
                };
                button.Click += mainWindow.CreateMatrix;
                tempGrid.Children.Add(button);

                tempGrid.Children.Add(new TextBox() {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Name = $"numSetter_{i}",
                    Margin = new Thickness(155 * i, 0, 0, 230),
                    Height = 25,
                    Width = 100
                });
                
                tempGrid.Children.Add(new Label() {
                    FontSize = 15,
                    Name = $"Label_{i}",
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Margin = new Thickness(155 * i, 120, 0, 230),
                    Height = 100,
                    Width = 100
                });
                
                tempGrid.Children.Add(new ComboBox() {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Name = $"typeOperation_{i}",
                    Text = "+",
                    Height = 30,
                    Width = 40,
                    Margin = new Thickness(107 + 155 * i, 0, 0, 300),
                    Items = { "+", "-", "/", "*" }
                });
            }
            
            var extendButton = new Button() {
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(155 * size, 0, 0, 290),
                Content = "+",
                Height = 30,
                Width = 30
            };
            extendButton.Click += mainWindow.ExtendEquation;
            tempGrid.Children.Add(extendButton);
            
            var decreaseButton = new Button() {
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(155 * size, 0, 0, 220),
                Content = "-",
                Height = 30,
                Width = 30
            };
            decreaseButton.Click += mainWindow.DeletePart;
            tempGrid.Children.Add(decreaseButton);
            
            tempGrid.Children.Add(new Label() {
                FontSize = 20,
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(50 + 155 * size, 50, 0, 230),
                Content = "="
            });
            
            return tempGrid;
        }
    }
}