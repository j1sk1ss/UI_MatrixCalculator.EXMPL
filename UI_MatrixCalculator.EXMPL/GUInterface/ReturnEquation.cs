using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using UI_MatrixCalculator.EXMPL.Objects;

namespace UI_MatrixCalculator.EXMPL.GUInterface
{
    public static class ReturnEquation
    {
        public static List<int> NumsFromGrid(Grid grid)
        {
            var lstNums = new List<int>();
            foreach (var element in grid.Children) {
                if (element.GetType() == typeof(TextBox))
                {
                    var temp = element as TextBox;
                    if (temp!.Visibility == Visibility.Visible)
                    {
                        lstNums.Add(int.Parse(temp.Text));
                    }
                }
            }
            
            return lstNums;
        }
    }
}