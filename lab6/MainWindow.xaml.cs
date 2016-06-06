using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace lab6
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            plot.clearFunctions();
            Func<double, double> yF = BoundingTask.getSolveOfTask(x => 1, x => 9 + 3.5 * x * (1 - x), int.Parse(countOfPoints.Text), 1, -3.5, 1, 2 * Math.E + 1.5, 0, 1);
            plot.addFunction(new PlotView.FunctionAppearance(yF, 0, 0, 1, 3, 0xff00), "numeric");
            Func<double, double> yFE = x => Math.Exp(-x) * (1 - 2 * Math.Exp(x) + Math.Exp(2 * x) - 3.5 * Math.Exp(x) * x + 3.5 * x * x * Math.Exp(x));
            plot.addFunction(new PlotView.FunctionAppearance(yFE, 0x888888, 0, 1, 3, 0xcfcf), "exact");
        }
    }
}
