using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace UI_MatrixCalculator.EXMPL.GUInterface {
    public static class ReturnEquation {
        public static List<double> NumsFromGrid(Grid grid) {
            var lstNums = new List<double>();
            foreach (var element in grid.Children) {
                if (element.GetType() != typeof(TextBox)) continue;
                var temp = element as TextBox;
                if (temp!.Visibility != Visibility.Visible) continue;
                if (double.TryParse(temp.Text,out var number)) lstNums.Add(number);
            }
            
            return lstNums;
        }
    }
}