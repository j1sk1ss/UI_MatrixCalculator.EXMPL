using System.Windows;

namespace UI_MatrixCalculator.EXMPL.Windows
{
    public partial class History : Window
    {
        public History(string history)
        {
            InitializeComponent();
            Answer.Content = history;
        }
    }
}