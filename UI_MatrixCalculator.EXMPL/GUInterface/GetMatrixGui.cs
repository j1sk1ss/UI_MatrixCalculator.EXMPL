using System.Windows;
using System.Windows.Controls;
using UI_MatrixCalculator.EXMPL.Windows;

namespace UI_MatrixCalculator.EXMPL.GUInterface
{
    public static class GetMatrixGui
    {
        public static Grid GetMatrix(int xSize, int ySize, Constructor constructor)
        {
            var tempGrid = new Grid();
            var button = new Button {
                Height = 20,
                Width  = 20,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment   = VerticalAlignment.Top,
                Margin  = new Thickness(ySize * 25,0,0,0),
                Content = "+"
            };
            button.Click += constructor.IncreaseYSize;
            tempGrid.Children.Add(button);
            
            button = new Button {
                Height = 20,
                Width  = 20,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment   = VerticalAlignment.Top,
                Margin  = new Thickness(ySize * 25,20,0,0),
                Content = "-"
            };
            button.Click += constructor.DecreaseYSize;
            tempGrid.Children.Add(button);
            
            button = new Button {
                Height = 20,
                Width  = 20,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment   = VerticalAlignment.Top,
                Margin  = new Thickness(0,xSize * 25,0,0),
                Content = "+"
            };
            button.Click += constructor.IncreaseXSize;
            tempGrid.Children.Add(button);
            
            button = new Button {
                Height = 20,
                Width  = 20,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment   = VerticalAlignment.Top,
                Margin  = new Thickness(20,xSize * 25,0,0),
                Content = "-"
            };
            button.Click += constructor.DecreaseXSize;
            tempGrid.Children.Add(button);
            
            for (var i = 0; i < xSize; i++) {
                for (var j = 0; j < ySize; j++) {
                    tempGrid.Children.Add(new TextBox() {
                        Height = 20,
                        Width  = 20,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment   = VerticalAlignment.Top,
                        Margin = new Thickness(j * 25,i * 25,0,0)
                    });
                }
            }
            return tempGrid;
        }
    }
}